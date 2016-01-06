Imports Oracle.ManagedDataAccess.Client
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms

Public Class IAIPPrintOut

#Region " Local variables "
    Dim SQL As String
    Dim cmd As oraclecommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim ConfidentialData As String = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
    Dim ReportType As String
    Dim WitnessingEngineer As String
    Dim WitnessingEngineer2 As String
    Dim ds As DataSet
    Dim da As OracleDataAdapter
#End Region

#Region " Form events "

    Private Sub IAIPPrintOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

        LoadCorrectReport()

        CRViewerTabs(CRViewer, False)
    End Sub

    Private Sub IAIPPrintOut_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CRViewer.ReportSource = Nothing
        PrintOut = Nothing
    End Sub

#End Region

#Region "Load Report"

    Private Sub LoadCorrectReport()
        Dim temp As String = ""
        Dim da As OracleDataAdapter
        Dim ds As New DataSet
        Dim rpt As New ReportClass

        Try

            Select Case txtPrintType.Text

                Case "Letter"

                    rpt = New crAPBPrintOut2
                    ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
                    monitor.TrackFeature("Report." & rpt.ResourceName)

                    Dim Commissioner As String = ""
                    Dim Director As String = ""

                    Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
                    Dim ParameterField As CrystalDecisions.Shared.ParameterField
                    Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

                    'Do this just once at the start
                    ParameterFields = New CrystalDecisions.Shared.ParameterFields

                    SQL = "Select " & _
                    "strManagementName from " & _
                    "AIRBRANCH.LookUpAPBManagementType " & _
                    "where strCurrentContact = '1' " & _
                    "and strKey = '1' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strManagementName")) Then
                            Director = ""
                        Else
                            Director = dr.Item("strManagementName")
                        End If
                    End While
                    dr.Close()

                    SQL = "Select " & _
                    "strManagementName from " & _
                    "AIRBRANCH.LookUpAPBManagementType " & _
                    "where strCurrentContact = '1' " & _
                    "and strKey = '2' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strManagementName")) Then
                            Commissioner = ""
                        Else
                            Commissioner = dr.Item("strManagementName")
                        End If
                    End While
                    dr.Close()

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "Director"
                    spValue.Value = Director
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "Commissioner"
                    spValue.Value = Commissioner
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    Select Case txtOther.Text
                        Case "SSPP Confirm"
                            SQL = "Select * " & _
                            "from AIRBRANCH.VW_SSPP_Acknowledge " & _
                            "where strApplicationNumber = '" & txtAIRSNumber.Text & "' "

                            da = New OracleDataAdapter(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            ds.EnforceConstraints = False
                            da.Fill(ds, "VW_SSPP_Acknowledge")

                            'Do this at the beginning of every new entry 
                            ParameterField = New CrystalDecisions.Shared.ParameterField
                            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                            ParameterField.ParameterFieldName = "ReportType"
                            spValue.Value = txtOther.Text
                            ParameterField.CurrentValues.Add(spValue)
                            ParameterFields.Add(ParameterField)
                        Case Else
                    End Select

                    rpt.SetDataSource(ds)

                    'Load Variables into the Fields
                    CRViewer.ParameterFieldInfo = ParameterFields
                    CRViewer.ReportSource = rpt
                    CRViewer.Refresh()

                Case "ISMPTestReport"
                    If txtReferenceNumber.Text <> "" Then
                        SQL = "select AIRBRANCH.ISMPDocumentType.strDocumentType " & _
                         "from AIRBRANCH.ISMPDocumentType, AIRBRANCH.ISMPReportInformation " & _
                         "where AIRBRANCH.ISMPReportInformation.strDocumentType = AIRBRANCH.ISMPDocumentType.strKey and " & _
                         "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                        Dim cmd As New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        Dim dr As OracleDataReader = cmd.ExecuteReader
                        Dim recExist As Boolean = dr.Read
                        If recExist = True Then
                            temp = dr.Item("strDocumentType")
                        End If

                        Select Case temp
                            Case "Unassigned"
                            Case "One Stack (Two Runs)"
                                LoadOneStackTwoRun()
                            Case "One Stack (Three Runs)"
                                LoadOneStackThreeRun()
                            Case "One Stack (Four Runs)"
                                LoadOneStackFourRun()
                            Case "Two Stack (Standard)"
                                LoadTwoStackStandard()
                            Case "Two Stack (DRE)"
                                LoadTwoStackDRE()
                            Case "Loading Rack"
                                LoadLoadingRack()
                            Case "Pond Treatment"
                                LoadPondTreatment()
                            Case "Gas Concentration"
                                LoadGasConcentration()
                            Case "Flare"
                                LoadFlare()
                            Case "Rata"
                                LoadRata()
                            Case "Memorandum (Standard)"
                                LoadMemorandumStandard()
                            Case "Memorandum (To File)"
                                LoadMemorandumToFile()
                            Case "Method 9 (Multi.)"
                                LoadMethod9Multi()
                            Case "Method 22"
                                LoadMethod22()
                            Case "Method9 (Single)"
                                LoadMethod9Single()
                            Case "PEMS"
                                LoadPEMS()
                            Case "PTE (Perminate Total Enclosure)"
                                LoadPTE()
                            Case Else
                                MsgBox("Unable to Print at this time.")
                                Me.Close()
                        End Select
                    Else
                        MsgBox("Unable to Print at this time.")
                        Me.Close()
                    End If

                Case "SSCP"
                    If txtReferenceNumber.Text <> "" Then
                        SQL = "select AIRBRANCH.ISMPDocumentType.strDocumentType " & _
                         "from AIRBRANCH.ISMPDocumentType, AIRBRANCH.ISMPReportInformation " & _
                         "where AIRBRANCH.ISMPReportInformation.strDocumentType = AIRBRANCH.ISMPDocumentType.strKey and " & _
                         "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                        Dim cmd As New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        Dim dr As OracleDataReader = cmd.ExecuteReader
                        Dim recExist As Boolean = dr.Read
                        If recExist = True Then
                            temp = dr.Item("strDocumentType")
                        End If

                        Select Case temp
                            Case "Unassigned"
                            Case "One Stack (Two Runs)"
                                LoadOneStackTwoRun()
                            Case "One Stack (Three Runs)"
                                LoadOneStackThreeRun()
                            Case "One Stack (Four Runs)"
                                LoadOneStackFourRun()
                            Case "Two Stack (Standard)"
                                LoadTwoStackStandard()
                            Case "Two Stack (DRE)"
                                LoadTwoStackDRE()
                            Case "Loading Rack"
                                LoadLoadingRack()
                            Case "Pond Treatment"
                                LoadPondTreatment()
                            Case "Gas Concentration"
                                LoadGasConcentration()
                            Case "Flare"
                                LoadFlare()
                            Case "Rata"
                                LoadRata()
                            Case "Memorandum (Standard)"
                                LoadMemorandumStandard()
                            Case "Memorandum (To File)"
                                LoadMemorandumToFile()
                            Case "Method 9 (Multi.)"
                                LoadMethod9Multi()
                            Case "Method 22"
                                LoadMethod22()
                            Case "Method9 (Single)"
                                LoadMethod9Single()
                            Case "PEMS"
                                LoadPEMS()
                            Case "PTE (Perminate Total Enclosure)"
                                LoadPTE()
                            Case Else
                                MsgBox("Unable to Print at this time.")
                                Me.Close()
                        End Select
                    Else
                        MsgBox("Unable to Print at this time.")
                        Me.Close()
                    End If
                Case "TitleVRenewal"
                    PrintOutTitleVRenewals()

                Case Else
                    MsgBox("Unable to print; please contact the DMU.")
                    Me.Close()

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Test Reports"
    Private Sub LoadOneStackTwoRun()
        Dim rpt As New CROneStackTwoRuns
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPReportOneStack.strReferenceNumber,  " & _
           "strMaxOperatingCapacity,  " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strMaxOperatingCapacityUnit   " & _
           "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit,   " & _
           "strOperatingCapacity,   " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strOperatingCapacityUnit   " & _
           "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,    " & _
           "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3,   " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strAllowableEmissionRateUnit1   " & _
           "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1,   " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strAllowableEmissionRateUnit2   " & _
           "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2,   " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strAllowableEmissionRateUnit3   " & _
           "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3,   " & _
           "strRunNumber1a, strRunNumber1b, strGasTemperature1A, strGasTemperature1b,   " & _
           "strGasMoisture1A, strGasMoisture1B,   " & _
           "strGasFlowRateACFM1A, strGasFlowRateACFM1B, " & _
           "strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B,   " & _
           "strPollutantConcentration1A, strPollutantConcentration1B,   " & _
           "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitkey = strPollutantConcentrationUnit   " & _
           "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit,   " & _
           "strPOllutantConcentrationAvg,   " & _
           "strEmissionRate1A, strEmissionRate1B, " & _
           "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack   " & _
           "where strUnitKey = strEmissionRateUnit   " & _
           "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit,   " & _
           "strEmissionRateAvg, strPercentAllowable, strConfidentialData   " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportOneStack   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportOneStack.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCommissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                spValue.Value = dr.Item("strReportTYpe")
                ReportType = spValue.Value
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUSerProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUSerProfiles.numUserProfiles  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt
            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub      'Complete
    Private Sub LoadOneStackThreeRun()
        Dim rpt As New CROneStackThreeRuns
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields
         
            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPReportOneStack.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit1 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit2 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit3 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strRunNumber1a, strRunNumber1b, strRunNumber1c, strGasTemperature1A, strGasTemperature1b, " & _
            "strGasTemperature1C, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, " & _
            "strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, " & _
            "strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, " & _
            "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, " & _
            "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strPollutantConcentrationUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
            "strPOllutantConcentrationAvg, " & _
            "strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitKey = strEmissionRateUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
            "strEmissionRateAvg, strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportOneStack   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportOneStack.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            Dim recExist As Boolean = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)
                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                spValue.Value = dr.Item("strReportTYpe")
                ReportType = spValue.Value
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = ", --Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                Try
                     
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                 

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 57, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub    'Complete
    Private Sub LoadOneStackFourRun()
        Dim rpt As New CROneStackFourRuns
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPReportOneStack.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit1 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit2 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit3 " & _
            "and AIRBRANCH.ISMPReportOneStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber1D, strGasTemperature1A, strGasTemperature1b, " & _
            "strGasTemperature1C, strGasTemperature1D, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, strGasMoisture1D, " & _
            "strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, strGasFlowRateACFM1D, " & _
            "strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, strGasFlowRateDSCFM1D, " & _
            "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, strPollutantConcentration1D, " & _
            "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitkey = strPollutantConcentrationUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
            "strPOllutantConcentrationAvg, " & _
            "strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, strEmissionRate1D, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportOneStack " & _
            "where strUnitKey = strEmissionRateUnit " & _
            "and AIRBRANCH.ISMPReportOneStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
            "strEmissionRateAvg, strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportOneStack   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportOneStack.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            Dim recExist As Boolean = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = ", --Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf - -"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 57, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 64, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub     'Complete
    Private Sub LoadTwoStackStandard()
        Dim rpt As New CRTwoStackStandard
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPReportTwoStack.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit1 " & _
            "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit2 " & _
            "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strAllowableEmissionRateUnit3 " & _
            "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strStackOneName, strStackTwoName, " & _
            "strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber2a, strRunNumber2b, strRunNumber2c, " & _
            "strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, " & _
            "strGasTemperature2A, strGasTemperature2b, strGasTemperature2C, " & _
            "strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, " & _
            "strGasMoisture2A, strGasMoisture2B, strGasMoisture2C, " & _
            "strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, " & _
            "strGasFlowRateACFM2A, strGasFlowRateACFM2B, strGasFlowRateACFM2c, " & _
            "strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, " & _
            "strGasFlowRateDSCFM2A, strGasFlowRateDSCFM2B, strGasFlowRateDSCFM2C, " & _
            "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, " & _
            "strPollutantConcentration2A, strPollutantConcentration2B, strPollutantConcentration2C, " & _
            "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitkey = strPollutantConcentrationUnit " & _
            "and AIRBRANCH.ISMPReportTwoStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
            "strPOllutantConcentrationAvg1, " & _
            "strPOllutantConcentrationAvg2, " & _
            "strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, " & _
            "strEmissionRate2A, strEmissionRate2B, strEmissionRate2C, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
            "where strUnitKey = strEmissionRateUnit " & _
            "and AIRBRANCH.ISMPReportTwoStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
            "strEmissionRateAvg1, " & _
            "strEmissionRateAvg2, " & _
            "strEmissionRateTotal1, " & _
            "strEmissionRateTotal2, " & _
            "strEmissionRateTotal3, " & _
            "strEmissionRateTotalAvg, " & _
            "strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportTwoStack   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportTwoStack.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            Dim recExist As Boolean = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackOneName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackOneName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackTwoName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackTwoName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 72, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 73, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 67, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 74, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 68, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 75, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 69, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 76, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 78, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 79, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 80, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 70, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 77, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 81, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 82, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 83, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 84, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 85, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal3"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 86, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal3")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotalAverage"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 87, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotalAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 88, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub    'Complete
    Private Sub LoadTwoStackDRE()
        Dim rpt As New CRTwoStackDRE
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPReportTwoStack.strReferencenumber, strMaxoperatingCapacity, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strMaxOperatingCapacityUnit " & _
        "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
        "strOperatingCapacity, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strOperatingCapacityUnit " & _
        "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
        "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strAllowableEmissionRateUnit1 " & _
        "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strAllowableEmissionRateUnit2 " & _
        "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strAllowableEmissionRateUnit3 " & _
        "and AIRBRANCH.ISMPReportTwoStack.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
        "strStackOneName, strStackTwoName, " & _
        "strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber2a, strRunNumber2b, strRunNumber2c, " & _
        "strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, " & _
        "strGasTemperature2A, strGasTemperature2b, strGasTemperature2C, " & _
        "strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, " & _
        "strGasMoisture2A, strGasMoisture2B, strGasMoisture2C, " & _
        "strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, " & _
        "strGasFlowRateACFM2A, strGasFlowRateACFM2B, strGasFlowRateACFM2c, " & _
        "strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, " & _
        "strGasFlowRateDSCFM2A, strGasFlowRateDSCFM2B, strGasFlowRateDSCFM2C, " & _
        "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, " & _
        "strPollutantConcentration2A, strPollutantConcentration2B, strPollutantConcentration2C, " & _
        "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitkey = strPollutantConcentrationUnit " & _
        "and AIRBRANCH.ISMPReportTwoStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
        "strPOllutantConcentrationAvg1, " & _
        "strPOllutantConcentrationAvg2, " & _
        "strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, " & _
        "strEmissionRate2A, strEmissionRate2B, strEmissionRate2C, " & _
        "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportTwoStack " & _
        "where strUnitKey = strEmissionRateUnit " & _
        "and AIRBRANCH.ISMPReportTwoStack.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
        "strEmissionRateAvg1, " & _
        "strEmissionRateAvg2, " & _
        "strDestructionPercent, " & _
        "strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportTwoStack   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportTwoStack.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            Dim recExist As Boolean = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                    If spValue.Value = "  Unassigned" Then
                        spValue.Value = "No Manager"
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackOneName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackOneName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackTwoName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackTwoName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 72, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 73, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 67, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 74, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 68, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 75, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 69, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 76, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 78, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 79, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 80, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 70, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 77, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 81, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 82, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 83, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "DestructionEfficiency1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 84, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strDestructionPercent")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()


                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub         'Complete
    Private Sub LoadLoadingRack()
        Dim rpt As New CRLoadingRack
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
          "AIRBRANCH.ISMPReportFlare.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1A, strAllowableEmissionRate2A, strAllowableEmissionRate3A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strAllowEmissionRateUnit1A " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strAllowEmissionRateUnit2A " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strAllowEmissionRateUnit3A " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strTestDuration, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strTestDurationUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as TestDurationUnit, " & _
            "strPollutantConcenIn, strPollutantConcenOut, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strPollutantConcenUnitIN " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as PollutantConcentrationUnitIN, " & _
             "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strPollutantConcenUnitOUT " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as PollutantConcentrationUnitOUT, " & _
            "strEmissionRate, " & _
             "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strEmissionRateUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
            "strDestructionEfficiency, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportFlare   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportFlare.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2A")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3A")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If


                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TestDuration"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTestDuration") & " " & dr.Item("TestDurationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationIN"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcenIn") & " " & dr.Item("PollutantConcentrationUnitIN")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationOUT"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcenOUT") & " " & dr.Item("PollutantConcentrationUnitOUT")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "DestructionReductionEfficiency"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strDestructionEfficiency")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                     "(strFirstName||' '||strLastName) as WitnessingEng " & _
                     "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                     "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                     "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub         'Complete
    Private Sub LoadPondTreatment()
        Dim rpt As New CRPondTreatment
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
          "AIRBRANCH.ISMPReportPondAndGas.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit1 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit2 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit3 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strRunNumber1a, strRunNumber1b, strRunNumber1c, " & _
            "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, " & _
            "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strPollutantConcentrationUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
            "strPOllutantConcentrationAvg, " & _
            "strTreatmentRate1A, strTreatmentRate1B, strTreatmentRate1C, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitKey = strTreatmentRateUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber = '" & txtReferenceNumber.Text & "') as TreatmentRateUnit, " & _
            "strTreatmentRateAvg, strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportPondAndGas   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = dr.Item("ForTestDateStart")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strControlEquipmentData")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = ", " & dr.Item("CC")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("TreatmentRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub       'Complete
    Private Sub LoadGasConcentration()
        Dim rpt As New CRGasConcentration
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
          "AIRBRANCH.ISMPReportPondAndGas.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit1 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit2 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strAllowableEmissionRateUnit3 " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
            "strRunNumber1a, strRunNumber1b, strRunNumber1c, " & _
            "strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, " & _
            "(Select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitkey = strPollutantConcentrationUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber = '" & txtReferenceNumber.Text & "') as POllutantConcentrationUnit, " & _
            "strPOllutantConcentrationAvg, " & _
            "strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportPondAndGas " & _
            "where strUnitKey = strEmissionRateUnit " & _
            "and AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber = '" & txtReferenceNumber.Text & "') as EmissionRateUnit, " & _
            "strEmissionRateAvg, strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportPondAndGas   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportPondAndGas.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ReviewingEngineer")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                spValue.Value = dr.Item("strControlEquipmentData")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                spValue.Value = dr.Item("strMaxoperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                spValue.Value = dr.Item("stroperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                spValue.Value = dr.Item("strAllowableEmissionRate1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                spValue.Value = dr.Item("strRunNumber1a")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                spValue.Value = dr.Item("strRunNumber1B")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                spValue.Value = dr.Item("strRunNumber1C")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                spValue.Value = dr.Item("strPollutantConcentration1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                spValue.Value = dr.Item("strPollutantConcentration1B")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                spValue.Value = dr.Item("strPollutantConcentration1C")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                spValue.Value = dr.Item("POllutantConcentrationUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                spValue.Value = dr.Item("strEmissionRate1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                spValue.Value = dr.Item("strEmissionRate1B")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                spValue.Value = dr.Item("strEmissionRate1C")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                spValue.Value = dr.Item("EmissionRateUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                spValue.Value = dr.Item("strEmissionRateAvg")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                spValue.Value = dr.Item("strPercentAllowable")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        If dr.Item("WitnessingEng") = "  Unassigned" Then
                        Else
                            WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                        End If

                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Or WitnessingEngineer2 = "" Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub    'Complete
    Private Sub LoadFlare()
        Dim rpt As New CRFlare
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
          "AIRBRANCH.ISMPReportFlare.strReferencenumber, strMaxoperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strLimitationVelocity, strLimitationHeatCapacity, " & _
            "strHeatingValue1A, strHeatingValue2A, strHeatingValue3A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strHeatingValueUnits " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as HeatingValueUnits, " & _
            "strHeatingValueAvg, strVelocity1A, strVelocity2A, strVelocity3A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportFlare " & _
            "where strUnitkey = strVelocityUnits " & _
            "and AIRBRANCH.ISMPReportFlare.strreferencenumber = '" & txtReferenceNumber.Text & "') as VelocityUnits, " & _
            "strVelocityAvg, strPercentAllowable, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportFlare   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportFlare.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"
          
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                spValue.Value = dr.Item("strControlEquipmentData")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                spValue.Value = dr.Item("strMaxoperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                spValue.Value = dr.Item("stroperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "LimitationVelocity"
                spValue.Value = "Velocity less than " & dr.Item("strLimitationVelocity") & " ft/sec;"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "Velocity Limitation --Conf--;"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "LimitationHeatCapacity"
                spValue.Value = " Heat Content greater than or equal to " & dr.Item("strLimitationHeatCapacity") & " BTU/scf."
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = " Heat Content Limitation --Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "HeatingValue1A"
                spValue.Value = dr.Item("strHeatingValue1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "HeatingValue1B"
                spValue.Value = dr.Item("strHeatingValue2A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "HeatingValue1C"
                spValue.Value = dr.Item("strHeatingValue3A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "HeatingValueUnits"
                spValue.Value = dr.Item("HeatingValueUnits")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "HeatingValueAverage"
                spValue.Value = dr.Item("strHeatingValueAvg")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Velocity1A"
                spValue.Value = dr.Item("strVelocity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Velocity1B"
                spValue.Value = dr.Item("strVelocity2A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Velocity1C"
                spValue.Value = dr.Item("strVelocity3A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "VelocityUnits"
                spValue.Value = dr.Item("VelocityUnits")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "VelocityAverage"
                spValue.Value = dr.Item("strVelocityAvg")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                spValue.Value = dr.Item("strPercentAllowable")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub               'Complete
    Private Sub LoadMemorandumStandard()
        Dim rpt As New CRMemorandumStandard
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
         "AIRBRANCH.ISMPREportMemo.strReferencenumber, strMemorandumField, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportMemo   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportMemo.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"
           
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Memorandum"
                spValue.Value = dr.Item("strMemorandumField")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub  'Complete
    Private Sub LoadMemorandumToFile()
        Dim rpt As New CRMemorandumToFile
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
        "AIRBRANCH.ISMPREportMemo.strReferencenumber, strMemorandumField, " & _
      "strMonitormanufactureandmodel, strmonitorserialnumber, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportMemo   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportMemo.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = ReportType
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("UnitManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Memorandum"
                spValue.Value = dr.Item("strMemorandumField")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ManufacturerAndModel"
                spValue.Value = dr.Item("strMonitormanufactureandmodel")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "SerialNumber"
                spValue.Value = dr.Item("strmonitorserialnumber")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub    'Complete
    Private Sub LoadPTE()
        Dim rpt As New CRMemorandumPTE
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
       "AIRBRANCH.ISMPREportMemo.strReferencenumber, strMemorandumField, " & _
      "strMaxoperatingCapacity, " & _
      "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportMemo " & _
      "where strUnitkey = strMaxOperatingCapacityUnit " & _
      "and AIRBRANCH.ISMPREportMemo.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
      "strOperatingCapacity, " & _
      "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportMemo " & _
      "where strUnitkey = strOperatingCapacityUnit " & _
      "and AIRBRANCH.ISMPREportMemo.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
      "strAllowableEmissionrate1A, strAllowableEmissionRate1B, strAllowableEmissionRate1C, " & _
      "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportMemo " & _
      "where strUnitkey = strAllowableEmissionRateUnit1A " & _
      "and AIRBRANCH.ISMPREportMemo.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
      "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportMemo " & _
      "where strUnitkey = strAllowableEmissionRateUnit1B " & _
      "and AIRBRANCH.ISMPREportMemo.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit2, " & _
      "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportMemo " & _
      "where strUnitkey = strAllowableEmissionRateUnit1C " & _
      "and AIRBRANCH.ISMPREportMemo.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit3, " & _
      "strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportMemo   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportMemo.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                spValue.Value = dr.Item("strControlEquipmentData")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Memorandum"
                spValue.Value = dr.Item("strMemorandumField")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                spValue.Value = dr.Item("strMaxoperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                spValue.Value = dr.Item("stroperatingCapacity")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                spValue.Value = dr.Item("strAllowableEmissionrate1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("AllowableEmissionRateUnit2") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionrate1B")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    spValue.Value = dr.Item("AllowableEmissionRateUnit2")
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate1C")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = dr.Item("AllowableEmissionRateUnit3")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub                 'Complete
    Private Sub LoadMethod22()
        Dim rpt As New CRMethod22
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "(Select AIRBRANCH.LookUPISMPComplianceStatus.strComplianceStatus " & _
           "from AIRBRANCH.LookUPISMPComplianceStatus, AIRBRANCH.ISMPReportInformation " & _
           "where AIRBRANCH.LookUPISMPComplianceStatus.strComplianceKey = AIRBRANCH.ISMPReportInformation.strComplianceStatus " & _
            "and AIRBRANCH.ISMPReportInformation.strReferencenumber = '" & txtReferenceNumber.Text & "') as ComplianceStatement2, " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
       "AIRBRANCH.ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity1A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "STRALLOWABLEEMISSIONRATE22, " & _
            "strOpacityTestDuration, strAccumulatedEmissionTime, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportOpacity   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportOpacity.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                ReportType = dr.Item("strReportTYpe")
                Select Case ReportType
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus2"
                spValue.Value = dr.Item("ComplianceStatement2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)


                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                spValue.Value = dr.Item("strMaxOperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                spValue.Value = dr.Item("strOperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                spValue.Value = dr.Item("STRALLOWABLEEMISSIONRATE22")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityTestDuration"
                spValue.Value = dr.Item("strOpacityTestDuration") & " minutes "
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AccumulatedEmissionTime"
                spValue.Value = dr.Item("strAccumulatedEmissionTime")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub            'Complete
    Private Sub LoadMethod9Single()
        Dim rpt As New CRMethod9Single
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "(Select AIRBRANCH.LookUPISMPComplianceStatus.strComplianceStatus " & _
           "from AIRBRANCH.LookUPISMPComplianceStatus, AIRBRANCH.ISMPReportInformation " & _
           "where AIRBRANCH.LookUPISMPComplianceStatus.strComplianceKey = AIRBRANCH.ISMPReportInformation.strComplianceStatus " & _
           "and AIRBRANCH.ISMPReportInformation.strReferencenumber = '" & txtReferenceNumber.Text & "') as ComplianceStatement2, " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity1A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strAllowableEmissionRateUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "strOpacityTestDuration, " & _
            "strOpacityPointA, strConfidentialData, strOpacityStandard  " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportOpacity   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportOpacity.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"
           
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportTYpe"
                temp = dr.Item("strReportTYpe")
                Select Case temp
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus2"
                spValue.Value = dr.Item("ComplianceStatement2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                spValue.Value = dr.Item("strControlEquipmentData")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                spValue.Value = dr.Item("strMaxoperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                spValue.Value = dr.Item("stroperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                spValue.Value = dr.Item("strAllowableEmissionRate1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRateUnit1"
                spValue.Value = dr.Item("AllowableEmissionRateUnit1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityTestDuration"
                spValue.Value = dr.Item("strOpacityTestDuration")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointA"
                spValue.Value = dr.Item("strOpacityPointA") & " % Opacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)


                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityStandard"
                If IsDBNull(dr.Item("strOpacityStandard")) Then
                    temp = "6"
                Else
                    temp = dr.Item("strOpacityStandard")
                End If

                Select Case temp
                    Case "6"
                        spValue.Value = "(Highest 6-minute average)"
                    Case "30"
                        spValue.Value = "(30-minute average)"
                    Case Else
                        spValue.Value = "(Highest 6-minute average)"
                End Select

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

             
                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         

    End Sub       'Complete
    Private Sub LoadMethod9Multi()
        Dim rpt As New CRMethod9Multi
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "(Select AIRBRANCH.LookUPISMPComplianceStatus.strComplianceStatus " & _
           "from AIRBRANCH.LookUPISMPComplianceStatus, AIRBRANCH.ISMPReportInformation " & _
           "where AIRBRANCH.LookUPISMPComplianceStatus.strComplianceKey = AIRBRANCH.ISMPReportInformation.strComplianceStatus " & _
            "and AIRBRANCH.ISMPReportInformation.strReferencenumber = '" & txtReferenceNumber.Text & "') as ComplianceStatement2, " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
       "AIRBRANCH.ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, " & _
            "strMaxOperatingCapacity2A, strMaxOperatingCapacity3A, " & _
            "strMaxOperatingCapacity4A, strMaxOperatingCapacity5A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strMaxOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as MaxOperatingCapacityUnit, " & _
            "strOperatingCapacity1A, strOperatingCapacity2A, " & _
            "strOperatingCapacity3A, strOperatingCapacity4A, " & _
            "strOperatingCapacity5A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strOperatingCapacityUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as OperatingCapacityUnit,  " & _
            "strAllowableEmissionRate1A, strAllowableEmissionRate2A, " & _
            "strAllowableEmissionRate3A, strAllowableEmissionRate4A, " & _
            "strAllowableEmissionRate5A, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPREportOpacity " & _
            "where strUnitkey = strAllowableEmissionRateUnit " & _
            "and AIRBRANCH.ISMPREportOpacity.strreferencenumber = '" & txtReferenceNumber.Text & "') as AllowableEmissionRateUnit1, " & _
            "strOpacityTestDuration, " & _
            "strOpacityPointA, strOpacityPointB, strOpacityPointC, " & _
            "strOpacityPointD, strOpacityPointE, " & _
            "strEquipmentItem1, strEquipmentItem2, strEquipmentItem3, " & _
            "strEquipmentItem4, strEquipmentItem5, " & _
            "strConfidentialData, strOpacityStandard " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPREportOpacity   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPREportOpacity.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"
          
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportType"
                temp = dr.Item("strReportTYpe")
                Select Case temp
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = " "
                    Else
                        spValue.Value = ", --Conf--"
                    End If
                Else
                    If Mid(ConfidentialData, 9, 1) = "1" Then
                        spValue.Value = ", " & dr.Item("UnitManager")
                    Else
                        spValue.Value = ", " & dr.Item("UnitManager")
                    End If
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus2"
                spValue.Value = dr.Item("ComplianceStatement2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ControlEquipmentOperatingData"
                spValue.Value = dr.Item("strControlEquipmentData")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity1"
                spValue.Value = dr.Item("strMaxoperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity2"
                spValue.Value = dr.Item("strMaxoperatingCapacity2A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity3"
                spValue.Value = dr.Item("strMaxoperatingCapacity3A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity4"
                spValue.Value = dr.Item("strMaxoperatingCapacity4A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity5"
                spValue.Value = dr.Item("strMaxoperatingCapacity5A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity1"
                spValue.Value = dr.Item("stroperatingCapacity1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity2"
                spValue.Value = dr.Item("stroperatingCapacity2A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity3"
                spValue.Value = dr.Item("stroperatingCapacity3A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity4"
                spValue.Value = dr.Item("stroperatingCapacity4A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity5"
                spValue.Value = dr.Item("stroperatingCapacity5A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                spValue.Value = dr.Item("OperatingCapacityUnit")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                spValue.Value = dr.Item("strAllowableEmissionRate1A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                spValue.Value = dr.Item("strAllowableEmissionRate2A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                spValue.Value = dr.Item("strAllowableEmissionRate3A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate4"
                spValue.Value = dr.Item("strAllowableEmissionRate4A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate5"
                spValue.Value = dr.Item("strAllowableEmissionRate5A")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointA"
                spValue.Value = dr.Item("strOpacityPointA")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointB"
                spValue.Value = dr.Item("strOpacityPointB")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointC"
                spValue.Value = dr.Item("strOpacityPointC")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointD"
                spValue.Value = dr.Item("strOpacityPointD")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityPointE"
                spValue.Value = dr.Item("strOpacityPointE")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Attachment1"
                spValue.Value = dr.Item("strEquipmentItem1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Attachment2"
                spValue.Value = dr.Item("strEquipmentItem2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Attachment3"
                spValue.Value = dr.Item("strEquipmentItem3")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Attachment4"
                spValue.Value = dr.Item("strEquipmentItem4")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Attachment5"
                spValue.Value = dr.Item("strEquipmentItem5")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)


                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OpacityStandard"
                temp = dr.Item("strOpacityStandard")
                Select Case temp
                    Case "6"
                        spValue.Value = "(Highest 6-minute average)"
                    Case "30"
                        spValue.Value = "(30-minute average)"
                    Case Else
                        spValue.Value = "(Highest 6-minute average)"
                End Select

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)


                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub        'Complete
    Private Sub LoadRata()
        Dim rpt As New CRRata
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
             

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "AIRBRANCH.ISMPMaster.strAIRSNumber as AIRSNumber, " & _
            "AIRBRANCH.APBFacilityInformation.strFacilityName as FacilityName, " & _
            "strFacilityCity as FacilityCity, " & _
             "strFacilityState as FacilityState, " & _
            "strPollutantDescription as PollutantDescription, " & _
            "strEmissionSource, " & _
            "AIRBRANCH.ISMPReportType.strReportType, " & _
           "strApplicableRequirement, " & _
           "(strFirstName||' '||strLastName) as ReviewingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = '0' then 'N/W' " & _
           "Else " & _
             "(select (strFirstName||' '||strLastName) " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation " & _
             "where AIRBRANCH.ISMPReportInformation.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') " & _
           "End as WitnessingEngineer, " & _
           "Case " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng is Null then 'N/W' " & _
             "when AIRBRANCH.ISMPReportInformation.strOtherWitnessingEng = '0' then 'N/W' " & _
           "Else  " & _
             "'M/W' " & _
           "End WitnessingEngineer2,     " & _
           "Case  " & _
             "when AIRBRANCH.ISMPReportInformation.numReviewingManager is Null then 'N/A' " & _
           "Else  " & _
             "(Select (strFirstName||' '||strLastName) as UnitManager  " & _
             "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation  " & _
             "where AIRBRANCH.ISMPReportInformation.numReviewingManager = AIRBRANCH.EPDUserProfiles.numUserID " & _
             "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "')  " & _
           "END UnitManager,  " & _
           "datReviewedByUnitManager,  " & _
           "(Select (strFirstName||' '||strLastName) as ComplianceManager  " & _
           "from AIRBRANCH.EPDUserProfiles, AIRBRANCH.ISMPReportInformation   " & _
           "where AIRBRANCH.ISMPReportInformation.strComplianceManager = AIRBRANCH.EPDUserProfiles.nuMUserID " & _
           "and AIRBRANCH.ISMPReportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as ComplianceManager,  " & _
           "to_char(datTestDateStart, 'FMMonth DD, YYYY') as ForTestDateStart,  " & _
           "to_char(datTestDateEnd, 'FMMonth DD, YYYY') as ForTestDateEnd,  " & _
           "to_char(datReceivedDate, 'FMMonth DD, YYYY') as ForReceivedDate,  " & _
           "datCompleteDate,  " & _
           "mmoCommentArea, strCommissioner, strDirector, strProgramManager,  " & _
           "strComplianceStatement as ComplianceStatement,  " & _
           "strControlEquipmentData,  " & _
           "(Select (strFirstName||' '||strLastName) as CC  " & _
           "from AIRBRANCH.ISMPReportInformation, AIRBRANCH.EPDUserProfiles  " & _
           "where AIRBRANCH.ISMPReportInformation.strCC = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPREportInformation.strReferenceNumber = '" & txtReferenceNumber.Text & "') as CC,  " & _
           "AIRBRANCH.ismpreportinformation.strComplianceStatus, " & _
       "AIRBRANCH.ISMPReportRATA.strReferencenumber, " & _
            "(Select strPOllutantDescription " & _
            "from AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportRATA " & _
            "where AIRBRANCH.LookUPPollutants.strPOllutantCode = AIRBRANCH.ISMPReportRATA.strDiluent " & _
            "and AIRBRANCH.ISMPReportRATA.strReferenceNumber = '" & txtReferenceNumber.Text & "') as Diluent, " & _
            "strAPplicableStandard, strRelativeAccuracyPercent, " & _
            "strReferenceMethod1, strReferenceMethod2, strReferenceMethod3, " & _
            "strReferenceMethod4, strReferenceMethod5, strReferenceMethod6, " & _
            "strReferenceMethod7, strReferenceMethod8, strReferenceMethod9, " & _
            "strReferenceMethod10, strReferenceMethod11, strReferenceMethod12, " & _
            "(select strUnitDescription from AIRBRANCH.LookUPUnits, AIRBRANCH.ISMPReportRATA " & _
            "where strUnitkey = strRataUnits " & _
            "and AIRBRANCH.ISMPReportRATA.strreferencenumber = '" & txtReferenceNumber.Text & "') as RataUnits, " & _
            "StrCMS1, StrCMS2, StrCMS3, StrCMS4, StrCMS5, StrCMS6, " & _
            "StrCMS7, StrCMS8, StrCMS9, StrCMS10, StrCMS11, StrCMS12, " & _
            "strAccuracyRequiredPercent, strAccuracyREquiredStatement, strAccuracyChoice, " & _
            "strRunsINcludedKey, strConfidentialData " & _
           "From AIRBRANCH.ISMPMaster, AIRBRANCH.APBFacilityInformation,  " & _
           "AIRBRANCH.ISMPReportInformation, AIRBRANCH.LookUpPollutants,  " & _
           "AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
           "AIRBRANCH.LookUpISMPComplianceStatus, AIRBRANCH.ISMPReportRATA   " & _
           "where AIRBRANCH.ISMPMaster.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportInformation.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPReportInformation.strPollutant = AIRBRANCH.LookUpPollutants.strPollutantCode  " & _
           "and AIRBRANCH.ISMPReportInformation.strReportType = AIRBRANCH.ISMPReportType.strKey  " & _
           "and AIRBRANCH.ISMPReportInformation.strReviewingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
           "and AIRBRANCH.ISMPReportInformation.strComplianceStatus = AIRBRANCH.LookUpISMPComplianceStatus.strComplianceKey   " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = AIRBRANCH.ISMPReportRATA.strReferenceNumber  " & _
           "and AIRBRANCH.ISMPMaster.strReferenceNumber = '" & txtReferenceNumber.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If txtOther.Text = "ToFile" Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                spValue.Value = dr.Item("strProgrammanager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = dr.Item("FacilityName")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                spValue.Value = dr.Item("PollutantDescription")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                spValue.Value = dr.Item("strEmissionSource")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReportType"
                temp = dr.Item("strReportTYpe")
                Select Case temp
                    Case "SOURCE TEST"
                        spValue.Value = "SOURCE TEST REPORT REVIEW"
                    Case "RATA/CEMS"
                        spValue.Value = "RELATIVE ACCURACY TEST AUDIT REPORT REVIEW"
                    Case "Monitor Certification"
                        spValue.Value = "MONITOR CERTIFICATION"
                    Case Else
                        spValue.Value = spValue.Value
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 6, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                spValue.Value = dr.Item("strApplicableRequirement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReviewingEngineer"
                spValue.Value = dr.Item("ReviewingEngineer")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 7, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                If IsDBNull(dr.Item("WitnessingEngineer")) Then
                    WitnessingEngineer = "Not Witnessed"
                Else
                    WitnessingEngineer = dr.Item("WitnessingEngineer")
                End If

                If IsDBNull(dr.Item("WitnessingEngineer2")) Then
                    WitnessingEngineer2 = "N/W"
                Else
                    WitnessingEngineer2 = dr.Item("WitnessingEngineer2")
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("UnitManager")
                End If

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceManager"
                spValue.Value = dr.Item("ComplianceManager")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 24, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("forTestDateEnd") = dr.Item("ForTestDateStart") Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = dr.Item("ForTestDateStart")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TestDates"
                    spValue.Value = (dr.Item("ForTestDateStart") & " to " & dr.Item("ForTestDateEnd"))
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 19, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                spValue.Value = dr.Item("ForReceivedDate")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                spValue.Value = dr.Item("mmoCommentArea")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                spValue.Value = dr.Item("ComplianceStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PassFail"
                temp = dr.Item("strComplianceStatus")
                Select Case temp
                    Case "01"
                        spValue.Value = "N/A"
                    Case "02"
                        spValue.Value = "Pass"
                    Case "03"
                        spValue.Value = "Pass"
                    Case "04"
                        spValue.Value = "N/A"
                    Case "05"
                        spValue.Value = "Fail"
                    Case Else
                        spValue.Value = "N/A"
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If dr.Item("CC") <> "" And dr.Item("CC") <> "  Unassigned" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = ", " & dr.Item("CC")
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 25, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = spValue.Value
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Diluent"
                spValue.Value = dr.Item("Diluent")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableStandard"
                spValue.Value = dr.Item("strAPplicableStandard")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RelativeAccuracyPercent"
                spValue.Value = dr.Item("strRElativeAccuracyPercent")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RelativeAccuracyChoice"
                temp = dr.Item("strAccuracyChoice")
                Select Case temp
                    Case "N/A"
                        spValue.Value = " "
                    Case "RefMethod"
                        spValue.Value = "of the Reference Method"
                    Case "AppStandard"
                        spValue.Value = "of the Applicable Standard"
                    Case "Diluent"
                        spValue.Value = dr.Item("strAccuracyChoice")
                    Case Else
                        spValue.Value = " "
                End Select
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod1"
                spValue.Value = dr.Item("strReferenceMethod1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod2"
                spValue.Value = dr.Item("strReferenceMethod2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 30, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod3"
                spValue.Value = dr.Item("strReferenceMethod3")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod4"
                spValue.Value = dr.Item("strReferenceMethod4")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 32, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod5"
                spValue.Value = dr.Item("strReferenceMethod5")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod6"
                spValue.Value = dr.Item("strReferenceMethod6")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod7"
                spValue.Value = dr.Item("strReferenceMethod7")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod8"
                spValue.Value = dr.Item("strReferenceMethod8")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod9"
                spValue.Value = dr.Item("strReferenceMethod9")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod10"
                spValue.Value = dr.Item("strReferenceMethod10")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod11"
                spValue.Value = dr.Item("strReferenceMethod11")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReferenceMethod12"
                spValue.Value = dr.Item("strReferenceMethod12")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RATAUnits"
                spValue.Value = dr.Item("RataUnits")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS1"
                spValue.Value = dr.Item("StrCMS1")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS2"
                spValue.Value = dr.Item("StrCMS2")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS3"
                spValue.Value = dr.Item("StrCMS3")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS4"
                spValue.Value = dr.Item("StrCMS4")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS5"
                spValue.Value = dr.Item("StrCMS5")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS6"
                spValue.Value = dr.Item("StrCMS6")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS7"
                spValue.Value = dr.Item("StrCMS7")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS8"
                spValue.Value = dr.Item("StrCMS8")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS9"
                spValue.Value = dr.Item("StrCMS9")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS10"
                spValue.Value = dr.Item("StrCMS10")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS11"
                spValue.Value = dr.Item("StrCMS11")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CMS12"
                spValue.Value = dr.Item("StrCMS12")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RelativeAccuracyRequiredPercent"
                spValue.Value = dr.Item("strAccuracyRequiredPercent")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RelativeAccuracyRequiredStatement"
                spValue.Value = dr.Item("strAccuracyREquiredStatement")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                Dim OmitRuns As String = dr.Item("strRunsINcludedKey")

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1"
                If Mid(OmitRuns, 1, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber2"
                If Mid(OmitRuns, 2, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber3"
                If Mid(OmitRuns, 3, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber4"
                If Mid(OmitRuns, 4, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber5"
                If Mid(OmitRuns, 5, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber6"
                If Mid(OmitRuns, 6, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber7"
                If Mid(OmitRuns, 7, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber8"
                If Mid(OmitRuns, 8, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber9"
                If Mid(OmitRuns, 9, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber10"
                If Mid(OmitRuns, 10, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber11"
                If Mid(OmitRuns, 11, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber12"
                If Mid(OmitRuns, 12, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                dr.Close()

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " & _
                    "(strFirstName||' '||strLastName) as WitnessingEng " & _
                    "from AIRBRANCH.ISMPWitnessingEng, AIRBRANCH.EPDUserProfiles " & _
                    "where AIRBRANCH.ISMPWitnessingEng.strWitnessingEngineer = AIRBRANCH.EPDUserProfiles.numUserID  " & _
                    "and strReferenceNumber = '" & Me.txtReferenceNumber.Text & "'  "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    WitnessingEngineer2 = ""
                    While dr.Read
                        WitnessingEngineer2 = WitnessingEngineer2 & vbCrLf & dr.Item("WitnessingEng")
                    End While
                    dr.Close()
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = WitnessingEngineer2
                    End If
                Else
                    WitnessingEngineer2 = " "
                End If

                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 2) = "11" Then
                    WitnessingEngineer = "--Conf--"
                Else
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 12, 1) = "1" Then
                        If WitnessingEngineer2 = " " Then
                            WitnessingEngineer = "--Conf--"
                        Else
                            WitnessingEngineer = "--Conf--" & WitnessingEngineer2
                        End If
                    Else
                        If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                            If WitnessingEngineer = "N/W" Then
                                WitnessingEngineer = "--Conf--"
                            Else
                                WitnessingEngineer = WitnessingEngineer & vbCrLf & "--Conf--"
                            End If
                        Else
                            If WitnessingEngineer = "N/W" Then
                                If WitnessingEngineer2 = " " Then
                                    WitnessingEngineer = "Not Witnessed"
                                Else
                                    WitnessingEngineer = "--" & WitnessingEngineer2
                                End If
                            Else
                                WitnessingEngineer = WitnessingEngineer & WitnessingEngineer2
                            End If
                        End If
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                ParameterField.ParameterFieldName = "WitnessingEngineer"
                spValue.Value = WitnessingEngineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt

            Else
                MsgBox("Unable to Print at this time.")
                PrintOut.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
         
    End Sub                'Complete
    Private Sub LoadPEMS()
        Try
             

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
           
        End Try
         
    End Sub

#End Region

#Region "SSPP"
    Sub PrintOutTitleVRenewals()
        Dim Commissioner As String = ""
        Dim Director As String = ""
        Dim ProgramManager As String = ""
        Dim rpt As New ReportClass

        rpt = New CRTitleVRenewal10
        ApplicationInsights.TrackPageView(TelemetryPageViewType.IaipCrReport, rpt.ResourceName)
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue
        Try
            ds = New DataSet
            SQL = "Select * " & _
            "from AIRBRANCH.VW_Title_V_Renewals " & _
            "where datPermitIssued between '" & txtStartDate.Text & "' " & _
            "and '" & txtEndDate.Text & "' " & _
            "or datEffective between '" & txtStartDate.Text & "' " & _
            "and '" & txtEndDate.Text & "' "

            If txtSQLLine.Text <> "*" Then
                SQL = "Select * " & _
                "from AIRBRANCH.VW_Title_V_Renewals " & _
                "where strApplicationNumber = '" & Replace(txtSQLLine.Text, "'", "''") & "' "
            End If

            da = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            ds.EnforceConstraints = False
            da.Fill(ds, "VW_Title_V_Renewals")
            rpt.SetDataSource(ds)

            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields
            SQL = "Select strDirector, strCommissioner, " & _
            "strSSPPProgramMang " & _
            "from AIRBRANCH.LookUpAPBManagement "

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                Commissioner = dr.Item("strCommissioner")
                Director = dr.Item("strDirector")
                ProgramManager = dr.Item("strSSPPProgramMang")
            End While
            dr.Close()

            'Do this at the beginning of every new enTry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Commissioner"
            spValue.Value = Commissioner
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new enTry
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "Director"
            spValue.Value = Director
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Do this at the beginning of every new enTry
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "ProgramManager"
            spValue.Value = ProgramManager
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRViewer.ParameterFieldInfo = ParameterFields
            CRViewer.ReportSource = rpt

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region " Utilities "
    Private Sub CRViewerTabs(ByVal viewer As CrystalReportViewer, ByVal visible As Boolean)
        ' http://bloggingabout.net/blogs/jschreuder/archive/2005/08/03/8760.aspx
        If viewer IsNot Nothing Then
            For Each control As Control In viewer.Controls
                If TypeOf control Is PageView Then
                    Dim tab As TabControl = DirectCast(DirectCast(control, PageView).Controls(0), TabControl)
                    If Not visible Then
                        tab.ItemSize = New Size(0, 1)
                        tab.SizeMode = TabSizeMode.Fixed
                        tab.Appearance = TabAppearance.Buttons
                    Else
                        tab.ItemSize = New Size(67, 18)
                        tab.SizeMode = TabSizeMode.Normal
                        tab.Appearance = TabAppearance.Normal
                    End If
                End If
            Next
        End If
    End Sub
#End Region

End Class