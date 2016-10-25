Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports Iaip.DAL

Public Class IAIPPrintOut

#Region " Properties and fields "

    Public Property PrintoutType As PrintType
    Public Property PrintoutSubtype As PrintSubtype = PrintSubtype.Other
    Public Property ReferenceValue As String
    Public Property StartDate As Date
    Public Property EndDate As Date

    Public Enum PrintType
        IsmpTestReport
        SsppConfirm
        TitleVRenewal
    End Enum

    Public Enum PrintSubtype
        ToFile
        Other
    End Enum

    Private ConfidentialData As String = "0"
    Private ReportType As String = ""
    Private WitnessingEngineer As String = ""
    Private WitnessingEngineer2 As String = ""


#End Region

#Region " Form events "

    Private Sub IAIPPrintOut_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCorrectReport()
        CRViewer.ShowHideViewerTabs(VisibleOrNot.NotVisible)
    End Sub

    Private Sub IAIPPrintOut_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CRViewer.ReportSource = Nothing
    End Sub

#End Region

#Region " Load Correct Report "

    Private Sub LoadCorrectReport()
        Select Case PrintoutType
            Case PrintType.SsppConfirm
                PrintSsppAcknowledgmentLetter()
            Case PrintType.IsmpTestReport
                PrintIsmpReport()
            Case PrintType.TitleVRenewal
                PrintOutTitleVRenewals()
            Case Else
                MsgBox("Unable to print; please contact EPD IT.")
                Me.Close()
        End Select
    End Sub

#End Region

#Region " ISMP Test Reports "

    Private Sub PrintIsmpReport()
        Dim testType As String = Ismp.GetStackTestDocumentType(ReferenceValue)

        Select Case testType
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
            Case "PTE (Perminate Total Enclosure)"
                LoadPTE()
            Case Else
                MsgBox("Unable to Print at this time.")
                Me.Close()
        End Select
    End Sub

    Private Sub LoadOneStackTwoRun()
        Dim rpt As New CROneStackTwoRuns
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "
                SELECT m.strAIRSNumber AS AIRSNumber, f.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, 
                strPollutantDescription AS PollutantDescription, strEmissionSource, t.strReportType, strApplicableRequirement, 
                CONCAT(u1.strFirstName, ' ', u1.strLastName) AS ReviewingEngineer,
                CASE WHEN i.strWitnessingEngineer = '0' THEN 'N/W' ELSE CONCAT(u2.strFirstName, ' ', u2.strLastName) END AS WitnessingEngineer,
                CASE WHEN i.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN i.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN i.numReviewingManager IS NULL THEN 'N/A' ELSE CONCAT(u3.strFirstName, ' ', u3.strLastName) END AS UnitManager, datReviewedByUnitManager, 
                CONCAT(u4.strFirstName, ' ', u4.strLastName) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, 
                format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, 
                mmoCommentArea, strCommissioner, strDirector, strProgramManager, 
                strComplianceStatement AS ComplianceStatement, strControlEquipmentData, CONCAT(u5.strFirstName, ' ', u5.strLastName) AS CC, r.strReferenceNumber, 
                strMaxOperatingCapacity, l1.strUnitDescription AS MaxOperatingCapacityUnit, strOperatingCapacity, l2.strUnitDescription AS OperatingCapacityUnit, 
                strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, l3.strUnitDescription AS AllowableEmissionRateUnit1, 
                l4.strUnitDescription AS AllowableEmissionRateUnit2, l5.strUnitDescription AS AllowableEmissionRateUnit2, 
                l6.strUnitDescription AS AllowableEmissionRateUnit3, strRunNumber1a, strRunNumber1b, strGasTemperature1A, strGasTemperature1b, strGasMoisture1A, 
                strGasMoisture1B, strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strPollutantConcentration1A, 
                strPollutantConcentration1B, l7.strUnitDescription AS POllutantConcentrationUnit, strPOllutantConcentrationAvg, strEmissionRate1A, strEmissionRate1B, 
                l8.STRUNITDESCRIPTION AS EmissionRateUnit, strEmissionRateAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMASTER AS m
                INNER JOIN APBFACILITYINFORMATION AS f ON m.strAIRSNumber = f.strAIRSNumber
                INNER JOIN ISMPREPORTINFORMATION AS i ON m.strReferenceNumber = i.strReferenceNumber
                INNER JOIN LOOKUPPOLLUTANTS AS p ON i.strPollutant = p.strPollutantCode
                INNER JOIN ISMPREPORTTYPE AS t ON i.strReportType = t.strKey
                INNER JOIN EPDUSERPROFILES AS u1 ON i.strReviewingEngineer = u1.numUserID
                INNER JOIN EPDUSERPROFILES AS u2 ON i.strWitnessingEngineer = u2.numUserID
                INNER JOIN EPDUSERPROFILES AS u3 ON i.numReviewingManager = u3.numUserID
                INNER JOIN EPDUSERPROFILES AS u4 ON i.strComplianceManager = u4.numUserID
                INNER JOIN EPDUSERPROFILES AS u5 ON i.strCC = u5.numUserID
                INNER JOIN LOOKUPISMPCOMPLIANCESTATUS AS s ON i.strComplianceStatus = s.strComplianceKey
                INNER JOIN ISMPREPORTONESTACK AS r ON m.strReferenceNumber = r.strReferenceNumber
                INNER JOIN LOOKUPUNITS AS l1 ON l1.STRUNITKEY = r.strMaxOperatingCapacityUnit
                INNER JOIN LOOKUPUNITS AS l2 ON l2.STRUNITKEY = r.strOperatingCapacityUnit
                INNER JOIN LOOKUPUNITS AS l3 ON l3.STRUNITKEY = r.strAllowableEmissionRateUnit1
                INNER JOIN LOOKUPUNITS AS l4 ON l4.STRUNITKEY = r.strAllowableEmissionRateUnit2
                INNER JOIN LOOKUPUNITS AS l5 ON l5.STRUNITKEY = r.strAllowableEmissionRateUnit2
                INNER JOIN LOOKUPUNITS AS l6 ON l6.STRUNITKEY = r.strAllowableEmissionRateUnit3
                INNER JOIN LOOKUPUNITS AS l7 ON l7.STRUNITKEY = r.strPollutantConcentrationUnit
                INNER JOIN LOOKUPUNITS AS l8 ON l8.STRUNITKEY = r.STREMISSIONRATEUNIT
                WHERE m.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" And PrintoutSubtype = PrintSubtype.ToFile Then
                    ConfidentialData = "0" & Mid(ConfidentialData, 2)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCommissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadOneStackThreeRun()
        Dim rpt As New CROneStackThreeRuns
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "
                SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportOneStack.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportOneStack.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportOneStack.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strRunNumber1a, strRunNumber1b, strRunNumber1c, strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportOneStack.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg, strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitKey = strEmissionRateUnit AND ISMPReportOneStack.strReferenceNumber = @ref) AS EmissionRateUnit, strEmissionRateAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportOneStack
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportOneStack.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)
                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 57, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadOneStackFourRun()
        Dim rpt As New CROneStackFourRuns
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportOneStack.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportOneStack.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportOneStack.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportOneStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber1D, strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, strGasTemperature1D, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, strGasMoisture1D, strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, strGasFlowRateACFM1D, strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, strGasFlowRateDSCFM1D, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, strPollutantConcentration1D, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportOneStack.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg, strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, strEmissionRate1D, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportOneStack
                WHERE strUnitKey = strEmissionRateUnit AND ISMPReportOneStack.strReferenceNumber = @ref) AS EmissionRateUnit, strEmissionRateAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportOneStack
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportOneStack.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "; " & vbCrLf & "--Conf - -"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 57, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 64, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1D"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1D")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTwoStackStandard()
        Dim rpt As New CRTwoStackStandard
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportTwoStack.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportTwoStack.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportTwoStack.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strStackOneName, strStackTwoName, strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber2a, strRunNumber2b, strRunNumber2c, strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, strGasTemperature2A, strGasTemperature2b, strGasTemperature2C, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, strGasMoisture2A, strGasMoisture2B, strGasMoisture2C, strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, strGasFlowRateACFM2A, strGasFlowRateACFM2B, strGasFlowRateACFM2c, strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, strGasFlowRateDSCFM2A, strGasFlowRateDSCFM2B, strGasFlowRateDSCFM2C, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, strPollutantConcentration2A, strPollutantConcentration2B, strPollutantConcentration2C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportTwoStack.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg1, strPOllutantConcentrationAvg2, strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, strEmissionRate2A, strEmissionRate2B, strEmissionRate2C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitKey = strEmissionRateUnit AND ISMPReportTwoStack.strReferenceNumber = @ref) AS EmissionRateUnit, strEmissionRateAvg1, strEmissionRateAvg2, strEmissionRateTotal1, strEmissionRateTotal2, strEmissionRateTotal3, strEmissionRateTotalAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportTwoStack
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportTwoStack.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackOneName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackOneName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackTwoName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackTwoName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 72, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 73, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 67, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 74, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 68, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 75, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 69, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 76, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 78, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 79, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 80, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 70, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 77, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 81, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 82, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 83, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 84, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 85, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotal3"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 86, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotal3")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateTotalAverage"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 87, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateTotalAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 88, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTwoStackDRE()
        Dim rpt As New CRTwoStackDRE
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportTwoStack.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportTwoStack.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportTwoStack.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportTwoStack.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strStackOneName, strStackTwoName, strRunNumber1a, strRunNumber1b, strRunNumber1c, strRunNumber2a, strRunNumber2b, strRunNumber2c, strGasTemperature1A, strGasTemperature1b, strGasTemperature1C, strGasTemperature2A, strGasTemperature2b, strGasTemperature2C, strGasMoisture1A, strGasMoisture1B, strGasMoisture1C, strGasMoisture2A, strGasMoisture2B, strGasMoisture2C, strGasFlowRateACFM1A, strGasFlowRateACFM1B, strGasFlowRateACFM1c, strGasFlowRateACFM2A, strGasFlowRateACFM2B, strGasFlowRateACFM2c, strGasFlowRateDSCFM1A, strGasFlowRateDSCFM1B, strGasFlowRateDSCFM1C, strGasFlowRateDSCFM2A, strGasFlowRateDSCFM2B, strGasFlowRateDSCFM2C, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, strPollutantConcentration2A, strPollutantConcentration2B, strPollutantConcentration2C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportTwoStack.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg1, strPOllutantConcentrationAvg2, strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, strEmissionRate2A, strEmissionRate2B, strEmissionRate2C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportTwoStack
                WHERE strUnitKey = strEmissionRateUnit AND ISMPReportTwoStack.strReferenceNumber = @ref) AS EmissionRateUnit, strEmissionRateAvg1, strEmissionRateAvg2, strDestructionPercent, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportTwoStack
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportTwoStack.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackOneName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackOneName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "StackTwoName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strStackTwoName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 50, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 51, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 58, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 65, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasTemperature2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 72, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasTemperature2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 52, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 59, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 66, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasMoisture2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 73, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasMoisture2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 53, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 60, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 67, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(ACFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 74, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateACFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 54, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 61, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 68, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GasFlowRate(DSCFM)2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 75, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strGasFlowRateDSCFM2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 48, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 55, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 62, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 69, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 76, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 78, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 79, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 80, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 49, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 56, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 63, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 70, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate2C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 77, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate2C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 81, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 82, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateAverage2"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 83, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRateAvg2")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "DestructionEfficiency1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 84, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strDestructionPercent")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadLoadingRack()
        Dim rpt As New CRLoadingRack
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportFlare.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportFlare.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportFlare.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1A, strAllowableEmissionRate2A, strAllowableEmissionRate3A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strAllowEmissionRateUnit1A AND ISMPReportFlare.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strAllowEmissionRateUnit2A AND ISMPReportFlare.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strAllowEmissionRateUnit3A AND ISMPReportFlare.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strTestDuration, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strTestDurationUnit AND ISMPReportFlare.strreferencenumber = @ref) AS TestDurationUnit, strPollutantConcenIn, strPollutantConcenOut, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strPollutantConcenUnitIN AND ISMPReportFlare.strreferencenumber = @ref) AS PollutantConcentrationUnitIN, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strPollutantConcenUnitOUT AND ISMPReportFlare.strreferencenumber = @ref) AS PollutantConcentrationUnitOUT, strEmissionRate, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strEmissionRateUnit AND ISMPReportFlare.strreferencenumber = @ref) AS EmissionRateUnit, strDestructionEfficiency, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportFlare
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportFlare.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2A")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If


                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TestDuration"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTestDuration") & " " & dr.Item("TestDurationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationIN"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcenIn") & " " & dr.Item("PollutantConcentrationUnitIN")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationOUT"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcenOUT") & " " & dr.Item("PollutantConcentrationUnitOUT")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionRate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("EmissionRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "DestructionReductionEfficiency"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strDestructionEfficiency")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPondTreatment()
        Dim rpt As New CRPondTreatment
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportPondAndGas.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportPondAndGas.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportPondAndGas.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strRunNumber1a, strRunNumber1b, strRunNumber1c, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportPondAndGas.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg, strTreatmentRate1A, strTreatmentRate1B, strTreatmentRate1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitKey = strTreatmentRateUnit AND ISMPReportPondAndGas.strReferenceNumber = @ref) AS TreatmentRateUnit, strTreatmentRateAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportPondAndGas
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportPondAndGas.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ReceivedDate"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 21, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ForReceivedDate")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CommentArea"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 47, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("mmoCommentArea")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceStatus"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 18, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("ComplianceStatement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strMaxoperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "MaximumOperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 26, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("MaxOperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "operatingCapacity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("stroperatingCapacity")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "OperatingCapacityUnit"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("OperatingCapacityUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AllowableEmissionRate1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strAllowableEmissionRate1")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 29, 1) = "1" Then
                        spValue.Value = "--Conf--"
                    Else
                        spValue.Value = "; " & vbCrLf & dr.Item("strAllowableEmissionRate2")
                    End If
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1a"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 33, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1a")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 36, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 39, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strRunNumber1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 34, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 37, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentration1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 40, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPollutantConcentration1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 42, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("POllutantConcentrationUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PollutantConcentrationAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 43, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPOllutantConcentrationAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1A"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 35, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1A")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1B"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 38, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1B")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRate1C"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 41, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRate1C")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRateUnits"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 44, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("TreatmentRateUnit")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "TreatmentRateAverage1"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strTreatmentRateAvg")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strPercentAllowable")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadGasConcentration()
        Dim rpt As New CRGasConcentration
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportPondAndGas.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportPondAndGas.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportPondAndGas.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1, strAllowableEmissionRate2, strAllowableEmissionRate3, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit1 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit2 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strAllowableEmissionRateUnit3 AND ISMPReportPondAndGas.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strRunNumber1a, strRunNumber1b, strRunNumber1c, strPollutantConcentration1A, strPollutantConcentration1B, strPollutantConcentration1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitkey = strPollutantConcentrationUnit AND ISMPReportPondAndGas.strReferenceNumber = @ref) AS POllutantConcentrationUnit, strPOllutantConcentrationAvg, strEmissionRate1A, strEmissionRate1B, strEmissionRate1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportPondAndGas
                WHERE strUnitKey = strEmissionRateUnit AND ISMPReportPondAndGas.strReferenceNumber = @ref) AS EmissionRateUnit, strEmissionRateAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportPondAndGas
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportPondAndGas.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ProgramManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 9, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strProgrammanager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 3, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = Mid(dr.Item("AIRSNumber"), 5, 8)
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 4, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityName")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Pollutant"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 15, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("PollutantDescription")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EmissionSourceTested"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 14, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strEmissionSource")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ApplicableRequirement"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("strApplicableRequirement")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                spValue.Value = dr.Item("strPercentAllowable")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 46, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFlare()
        Dim rpt As New CRFlare
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPReportFlare.strReferencenumber, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPReportFlare.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPReportFlare.strreferencenumber = @ref) AS OperatingCapacityUnit, strLimitationVelocity, strLimitationHeatCapacity, strHeatingValue1A, strHeatingValue2A, strHeatingValue3A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strHeatingValueUnits AND ISMPReportFlare.strreferencenumber = @ref) AS HeatingValueUnits, strHeatingValueAvg, strVelocity1A, strVelocity2A, strVelocity3A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportFlare
                WHERE strUnitkey = strVelocityUnits AND ISMPReportFlare.strreferencenumber = @ref) AS VelocityUnits, strVelocityAvg, strPercentAllowable, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportFlare
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportFlare.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PercentAllowable"
                spValue.Value = dr.Item("strPercentAllowable")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 45, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMemorandumStandard()
        Dim rpt As New CRMemorandumStandard
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try


            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportMemo.strReferencenumber, strMemorandumField, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportMemo
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportMemo.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Memorandum"
                spValue.Value = dr.Item("strMemorandumField")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 27, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMemorandumToFile()
        Dim rpt As New CRMemorandumToFile
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try


            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportMemo.strReferencenumber, strMemorandumField, strMonitormanufactureandmodel, strmonitorserialnumber, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportMemo
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportMemo.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("UnitManager")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "SerialNumber"
                spValue.Value = dr.Item("strmonitorserialnumber")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 28, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadPTE()
        Dim rpt As New CRMemorandumPTE
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportMemo.strReferencenumber, strMemorandumField, strMaxoperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportMemo
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPREportMemo.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportMemo
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPREportMemo.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionrate1A, strAllowableEmissionRate1B, strAllowableEmissionRate1C, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportMemo
                WHERE strUnitkey = strAllowableEmissionRateUnit1A AND ISMPREportMemo.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportMemo
                WHERE strUnitkey = strAllowableEmissionRateUnit1B AND ISMPREportMemo.strreferencenumber = @ref) AS AllowableEmissionRateUnit2, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportMemo
                WHERE strUnitkey = strAllowableEmissionRateUnit1C AND ISMPREportMemo.strreferencenumber = @ref) AS AllowableEmissionRateUnit3, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportMemo
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportMemo.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit2"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If dr.Item("AllowableEmissionRateUnit3") <> "N/A" Then

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRate3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)

                    'Do this at the beginning of every new entry 
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "AllowableEmissionRateUnit3"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMethod22()
        Dim rpt As New CRMethod22
        monitor.TrackFeature("Report." & rpt.ResourceName)

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try
            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, (SELECT LookUPISMPComplianceStatus.strComplianceStatus
                FROM LookUPISMPComplianceStatus, ISMPReportInformation
                WHERE LookUPISMPComplianceStatus.strComplianceKey = ISMPReportInformation.strComplianceStatus AND ISMPReportInformation.strReferencenumber = @ref) AS ComplianceStatement2, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity1A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS OperatingCapacityUnit, STRALLOWABLEEMISSIONRATE22, strOpacityTestDuration, strAccumulatedEmissionTime, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportOpacity
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportOpacity.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AccumulatedEmissionTime"
                spValue.Value = dr.Item("strAccumulatedEmissionTime")
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 31, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = spValue.Value
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMethod9Single()
        Dim rpt As New CRMethod9Single
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try
            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, (SELECT LookUPISMPComplianceStatus.strComplianceStatus
                FROM LookUPISMPComplianceStatus, ISMPReportInformation
                WHERE LookUPISMPComplianceStatus.strComplianceKey = ISMPReportInformation.strComplianceStatus AND ISMPReportInformation.strReferencenumber = @ref) AS ComplianceStatement2, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity1A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strAllowableEmissionRateUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, strOpacityTestDuration, strOpacityPointA, strConfidentialData, strOpacityStandard
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportOpacity
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportOpacity.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMethod9Multi()
        Dim rpt As New CRMethod9Multi
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try

            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, (SELECT LookUPISMPComplianceStatus.strComplianceStatus
                FROM LookUPISMPComplianceStatus, ISMPReportInformation
                WHERE LookUPISMPComplianceStatus.strComplianceKey = ISMPReportInformation.strComplianceStatus AND ISMPReportInformation.strReferencenumber = @ref) AS ComplianceStatement2, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ISMPREportOpacity.strReferencenumber, strMaxOperatingCapacity1A, strMaxOperatingCapacity2A, strMaxOperatingCapacity3A, strMaxOperatingCapacity4A, strMaxOperatingCapacity5A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strMaxOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS MaxOperatingCapacityUnit, strOperatingCapacity1A, strOperatingCapacity2A, strOperatingCapacity3A, strOperatingCapacity4A, strOperatingCapacity5A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strOperatingCapacityUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS OperatingCapacityUnit, strAllowableEmissionRate1A, strAllowableEmissionRate2A, strAllowableEmissionRate3A, strAllowableEmissionRate4A, strAllowableEmissionRate5A, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPREportOpacity
                WHERE strUnitkey = strAllowableEmissionRateUnit AND ISMPREportOpacity.strreferencenumber = @ref) AS AllowableEmissionRateUnit1, strOpacityTestDuration, strOpacityPointA, strOpacityPointB, strOpacityPointC, strOpacityPointD, strOpacityPointE, strEquipmentItem1, strEquipmentItem2, strEquipmentItem3, strEquipmentItem4, strEquipmentItem5, strConfidentialData, strOpacityStandard
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPREportOpacity
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPREportOpacity.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadRata()
        Dim rpt As New CRRata
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String

        Dim ParameterFields As ParameterFields
        Dim ParameterField As ParameterField
        Dim spValue As ParameterDiscreteValue

        Try


            'Do this just once at the start
            ParameterFields = New ParameterFields

            Dim SQL As String = "SELECT ISMPMaster.strAIRSNumber AS AIRSNumber, APBFacilityInformation.strFacilityName AS FacilityName, strFacilityCity AS FacilityCity, strFacilityState AS FacilityState, strPollutantDescription AS PollutantDescription, strEmissionSource, ISMPReportType.strReportType, strApplicableRequirement, CONCAT(strFirstName, ' ', strLastName) AS ReviewingEngineer,
                CASE WHEN ISMPReportInformation.strWitnessingEngineer = '0' THEN 'N/W' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName)
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strWitnessingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS WitnessingEngineer,
                CASE WHEN ISMPReportInformation.strOtherWitnessingEng IS NULL THEN 'N/W' WHEN ISMPReportInformation.strOtherWitnessingEng = '0' THEN 'N/W' ELSE 'M/W' END AS WitnessingEngineer2,
                CASE WHEN ISMPReportInformation.numReviewingManager IS NULL THEN 'N/A' ELSE (SELECT CONCAT(strFirstName, ' ', strLastName) AS UnitManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.numReviewingManager = EPDUserProfiles.numUserID AND ISMPReportInformation.strReferenceNumber = @ref) END AS UnitManager, datReviewedByUnitManager, (SELECT CONCAT(strFirstName, ' ', strLastName) AS ComplianceManager
                FROM EPDUserProfiles, ISMPReportInformation
                WHERE ISMPReportInformation.strComplianceManager = EPDUserProfiles.nuMUserID AND ISMPReportInformation.strReferenceNumber = @ref) AS ComplianceManager, format(datTestDateStart, 'MMMM d, yyyy') AS ForTestDateStart, format(datTestDateEnd, 'MMMM d, yyyy') AS ForTestDateEnd, format(datReceivedDate, 'MMMM d, yyyy') AS ForReceivedDate, datCompleteDate, mmoCommentArea, strCommissioner, strDirector, strProgramManager, strComplianceStatement AS ComplianceStatement, strControlEquipmentData, (SELECT CONCAT(strFirstName, ' ', strLastName) AS CC
                FROM ISMPReportInformation, EPDUserProfiles
                WHERE ISMPReportInformation.strCC = EPDUserProfiles.numUserID AND ISMPREportInformation.strReferenceNumber = @ref) AS CC, ismpreportinformation.strComplianceStatus, ISMPReportRATA.strReferencenumber, (SELECT strPOllutantDescription
                FROM LookUPPollutants, ISMPReportRATA
                WHERE LookUPPollutants.strPOllutantCode = ISMPReportRATA.strDiluent AND ISMPReportRATA.strReferenceNumber = @ref) AS Diluent, strAPplicableStandard, strRelativeAccuracyPercent, strReferenceMethod1, strReferenceMethod2, strReferenceMethod3, strReferenceMethod4, strReferenceMethod5, strReferenceMethod6, strReferenceMethod7, strReferenceMethod8, strReferenceMethod9, strReferenceMethod10, strReferenceMethod11, strReferenceMethod12, (SELECT strUnitDescription
                FROM LookUPUnits, ISMPReportRATA
                WHERE strUnitkey = strRataUnits AND ISMPReportRATA.strreferencenumber = @ref) AS RataUnits, StrCMS1, StrCMS2, StrCMS3, StrCMS4, StrCMS5, StrCMS6, StrCMS7, StrCMS8, StrCMS9, StrCMS10, StrCMS11, StrCMS12, strAccuracyRequiredPercent, strAccuracyREquiredStatement, strAccuracyChoice, strRunsINcludedKey, strConfidentialData
                FROM ISMPMaster, APBFacilityInformation, ISMPReportInformation, LookUpPollutants, ISMPReportType, EPDUserProfiles, LookUpISMPComplianceStatus, ISMPReportRATA
                WHERE ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSNumber AND ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber AND ISMPReportInformation.strPollutant = LookUpPollutants.strPollutantCode AND ISMPReportInformation.strReportType = ISMPReportType.strKey AND ISMPReportInformation.strReviewingEngineer = EPDUserProfiles.numUserID AND ISMPReportInformation.strComplianceStatus = LookUpISMPComplianceStatus.strComplianceKey AND ISMPMaster.strReferenceNumber = ISMPReportRATA.strReferenceNumber AND ISMPMaster.strReferenceNumber = @ref"

            Dim p As New SqlParameter("@ref", ReferenceValue)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then

                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = "0"
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If Mid(ConfidentialData, 1, 1) = "1" Then
                    If PrintoutSubtype = PrintSubtype.ToFile Then
                        ConfidentialData = "0" & Mid(ConfidentialData, 2)
                    End If
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = dr.Item("strCOmmissioner")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Director"
                spValue.Value = dr.Item("strDirector")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityCity"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("FacilityCity") & ", "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityState"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 5, 1) = "1" Then
                    spValue.Value = " "
                Else
                    spValue.Value = dr.Item("FacilityState")
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "UnitManager"
                If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 10, 1) = "1" Then
                    spValue.Value = "--Conf--"
                Else
                    spValue.Value = dr.Item("UnitManager")
                End If

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

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
                    ParameterField = New ParameterField
                    spValue = New ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "CC"
                    spValue.Value = " "
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Referencenumber"
                spValue.Value = dr.Item("strReferencenumber")
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber1"
                If Mid(OmitRuns, 1, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber2"
                If Mid(OmitRuns, 2, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber3"
                If Mid(OmitRuns, 3, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If

                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber4"
                If Mid(OmitRuns, 4, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber5"
                If Mid(OmitRuns, 5, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber6"
                If Mid(OmitRuns, 6, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber7"
                If Mid(OmitRuns, 7, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber8"
                If Mid(OmitRuns, 8, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber9"
                If Mid(OmitRuns, 9, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber10"
                If Mid(OmitRuns, 10, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber11"
                If Mid(OmitRuns, 11, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue

                ParameterField.ParameterFieldName = "RunNumber12"
                If Mid(OmitRuns, 12, 1) = "1" Then
                    spValue.Value = "*"
                Else
                    spValue.Value = " "
                End If
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If WitnessingEngineer2 = "M/W" Then
                    SQL = "select " &
                    "concat(strFirstName,' ',strLastName) as WitnessingEng " &
                    "from ISMPWitnessingEng inner join EPDUSerProfiles " &
                    "on ISMPWitnessingEng.strWitnessingEngineer = EPDUSerProfiles.numUserProfiles  " &
                    "where strReferenceNumber = @ref "

                    If Mid(ConfidentialData, 1, 1) <> "0" And Mid(ConfidentialData, 13, 1) = "1" Then
                        WitnessingEngineer2 = "--Conf--"
                    Else
                        WitnessingEngineer2 = DB.GetSingleValue(Of String)(SQL, p)
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
                ParameterField = New ParameterField
                spValue = New ParameterDiscreteValue
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
                Me.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " SSPP Reports "

    Private Sub PrintSsppAcknowledgmentLetter()
        Dim ParameterFields As New ParameterFields

        ParameterFields.AddParameterField("Director", GetEpdManagerName(EpdManagementTypes.EpdDirector))
        ParameterFields.AddParameterField("Commissioner", GetEpdManagerName(EpdManagementTypes.DnrCommissioner))
        ParameterFields.AddParameterField("ReportType", "SSPP Confirm")

        Dim SQL As String = "Select * " &
            "from VW_SSPP_Acknowledge " &
            "where strApplicationNumber = @ref "

        Dim p As New SqlParameter("@ref", ReferenceValue)

        Dim dt As DataTable = DB.GetDataTable(SQL, p)
        dt.TableName = "VW_SSPP_Acknowledge"

        Dim rpt As New crAPBPrintOut2
        monitor.TrackFeature("Report." & rpt.ResourceName)
        rpt.SetDataSource(dt)

        CRViewer.ParameterFieldInfo = ParameterFields
        CRViewer.ReportSource = rpt
        CRViewer.Refresh()
    End Sub

    Private Sub PrintOutTitleVRenewals()
        Try
            Dim SQL As String

            If ReferenceValue = "*" Then
                SQL = "Select * " &
                    "from VW_Title_V_Renewals " &
                    "where datPermitIssued between @start " &
                    "and @end " &
                    "or datEffective between @start " &
                    "and @end "
            Else
                SQL = "Select * " &
                    "from VW_Title_V_Renewals " &
                    "where strApplicationNumber = @ref "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@start", StartDate),
                New SqlParameter("@end", EndDate),
                New SqlParameter("@ref", ReferenceValue)
            }

            Dim dt As DataTable = DB.GetDataTable(SQL, p)
            dt.TableName = "VW_Title_V_Renewals"

            Dim ParameterFields As New ParameterFields

            ParameterFields.AddParameterField("Director", GetEpdManagerName(EpdManagementTypes.EpdDirector))
            ParameterFields.AddParameterField("Commissioner", GetEpdManagerName(EpdManagementTypes.DnrCommissioner))
            ParameterFields.AddParameterField("ProgramManager", GetEpdManagerName(EpdManagementTypes.SsppProgramManager))

            Dim rpt As New CRTitleVRenewal10
            monitor.TrackFeature("Report." & rpt.ResourceName)
            rpt.SetDataSource(dt)

            CRViewer.ParameterFieldInfo = ParameterFields
            CRViewer.ReportSource = rpt
            CRViewer.Refresh()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class