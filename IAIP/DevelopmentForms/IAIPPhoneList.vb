Public Class IAIPPhoneList

    Private Sub IAIPPhoneList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        monitor.TrackFeature("Dev." & Me.Name)
        Try
            pnlAirProtection.Enabled = False
            chbAmbientMonitoring.Checked = False
            chbIndustrialSourceMonitoring.Checked = False
            chbMobileAreaSource.Checked = False
            chbPlanningSupport.Checked = False
            chbStationarySourceCompliance.Checked = False
            chbStationarySourcePermitting.Checked = False

            pnlHazardWaste.Enabled = False
            chbCorrectiveAction.Checked = False
            chbFacilitiesCompliance.Checked = False
            chbGeneratorCompliance.Checked = False
            chbHazardousSitesResponse.Checked = False

            pnlLandProtection.Enabled = False
            chbSolidWasteManagement.Checked = False
            chbUndergroundStorageTankManagement.Checked = False
            chbWasteReductionAndAbatement.Checked = False

            pnlProgramCoordination.Enabled = False
            chbDistrictAlbany.Checked = False
            chbDistrictAthens.Checked = False
            chbDistrictAtlanta.Checked = False
            chbDistrictAugusta.Checked = False
            chbDistrictcartersville.Checked = False
            chbDistrictMacon.Checked = False
            chbDistrictSavannah.Checked = False
            chbDistrictBrunswick.Checked = False
            chbEmergencyResponse.Checked = False
            chbEnvironmentalRadiation.Checked = False
            chbEnvironmentalEmergency.Checked = False
            chbLaboratoryOperations.Checked = False
            chbQualityAssurance.Checked = False
            chbRadioactiveMaterials.Checked = False
            chbSmallBusinessAssistance.Checked = False

            pnlWatershedProtection.Enabled = False
            chbDrikingWaterCompliance.Checked = False
            chbDrinkingWaterPermitting.Checked = False
            chbEngineeringAndTechnical.Checked = False
            chbFloodplainManagement.Checked = False
            chbNonPointSource.Checked = False
            chbPermitComplianceAndEnforcement.Checked = False
            chbSafeDams.Checked = False
            chbTotalMaximumDailiyLoadImplementation.Checked = False
            chbWaterWithdrawalPermitting.Checked = False
            chbWatershedPlanningAndMonitoring.Checked = False
            chbWatershedProtrectionBasinAnalysis.Checked = False
            chbWatershedProtectionRegulatorySupport.Checked = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbAirProtectionBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAirProtectionBranch.CheckedChanged
        Try
            If chbAirProtectionBranch.Checked = True Then
                pnlAirProtection.Enabled = True
                chbAmbientMonitoring.Checked = True
                chbIndustrialSourceMonitoring.Checked = True
                chbMobileAreaSource.Checked = True
                chbPlanningSupport.Checked = True
                chbStationarySourceCompliance.Checked = True
                chbStationarySourcePermitting.Checked = True
            Else
                pnlAirProtection.Enabled = False
                chbAmbientMonitoring.Checked = False
                chbIndustrialSourceMonitoring.Checked = False
                chbMobileAreaSource.Checked = False
                chbPlanningSupport.Checked = False
                chbStationarySourceCompliance.Checked = False
                chbStationarySourcePermitting.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbHazardWaste_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbHazardWaste.CheckedChanged
        Try
            If chbHazardWaste.Checked = True Then
                pnlHazardWaste.Enabled = True
                chbCorrectiveAction.Checked = True
                chbFacilitiesCompliance.Checked = True
                chbGeneratorCompliance.Checked = True
                chbHazardousSitesResponse.Checked = True
            Else
                pnlHazardWaste.Enabled = False
                chbCorrectiveAction.Checked = False
                chbFacilitiesCompliance.Checked = False
                chbGeneratorCompliance.Checked = False
                chbHazardousSitesResponse.Checked = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbLandProtectionBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbLandProtectionBranch.CheckedChanged
        Try
            If chbLandProtectionBranch.Checked = True Then
                pnlLandProtection.Enabled = True
                chbSolidWasteManagement.Checked = True
                chbUndergroundStorageTankManagement.Checked = True
                chbWasteReductionAndAbatement.Checked = True
            Else
                pnlLandProtection.Enabled = False
                chbSolidWasteManagement.Checked = False
                chbUndergroundStorageTankManagement.Checked = False
                chbWasteReductionAndAbatement.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbProgramCoordinationBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbProgramCoordinationBranch.CheckedChanged
        Try
            If chbProgramCoordinationBranch.Checked = True Then
                pnlProgramCoordination.Enabled = True
                chbDistrictAlbany.Checked = True
                chbDistrictAthens.Checked = True
                chbDistrictAtlanta.Checked = True
                chbDistrictAugusta.Checked = True
                chbDistrictcartersville.Checked = True
                chbDistrictMacon.Checked = True
                chbDistrictSavannah.Checked = True
                chbDistrictBrunswick.Checked = True
                chbEmergencyResponse.Checked = True
                chbEnvironmentalRadiation.Checked = True
                chbEnvironmentalEmergency.Checked = True
                chbLaboratoryOperations.Checked = True
                chbQualityAssurance.Checked = True
                chbRadioactiveMaterials.Checked = True
                chbSmallBusinessAssistance.Checked = True
            Else
                pnlProgramCoordination.Enabled = False
                chbDistrictAlbany.Checked = False
                chbDistrictAthens.Checked = False
                chbDistrictAtlanta.Checked = False
                chbDistrictAugusta.Checked = False
                chbDistrictcartersville.Checked = False
                chbDistrictMacon.Checked = False
                chbDistrictSavannah.Checked = False
                chbDistrictBrunswick.Checked = False
                chbEmergencyResponse.Checked = False
                chbEnvironmentalRadiation.Checked = False
                chbEnvironmentalEmergency.Checked = False
                chbLaboratoryOperations.Checked = False
                chbQualityAssurance.Checked = False
                chbRadioactiveMaterials.Checked = False
                chbSmallBusinessAssistance.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbWatershedProtectionBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbWatershedProtectionBranch.CheckedChanged
        Try
            If chbWatershedProtectionBranch.Checked = True Then
                pnlWatershedProtection.Enabled = True
                chbDrikingWaterCompliance.Checked = True
                chbDrinkingWaterPermitting.Checked = True
                chbEngineeringAndTechnical.Checked = True
                chbFloodplainManagement.Checked = True
                chbNonPointSource.Checked = True
                chbPermitComplianceAndEnforcement.Checked = True
                chbSafeDams.Checked = True
                chbTotalMaximumDailiyLoadImplementation.Checked = True
                chbWaterWithdrawalPermitting.Checked = True
                chbWatershedPlanningAndMonitoring.Checked = True
                chbWatershedProtrectionBasinAnalysis.Checked = True
                chbWatershedProtectionRegulatorySupport.Checked = True
            Else
                pnlWatershedProtection.Enabled = False
                chbDrikingWaterCompliance.Checked = False
                chbDrinkingWaterPermitting.Checked = False
                chbEngineeringAndTechnical.Checked = False
                chbFloodplainManagement.Checked = False
                chbNonPointSource.Checked = False
                chbPermitComplianceAndEnforcement.Checked = False
                chbSafeDams.Checked = False
                chbTotalMaximumDailiyLoadImplementation.Checked = False
                chbWaterWithdrawalPermitting.Checked = False
                chbWatershedPlanningAndMonitoring.Checked = False
                chbWatershedProtrectionBasinAnalysis.Checked = False
                chbWatershedProtectionRegulatorySupport.Checked = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnPrintOutPhoneList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintOutPhoneList.Click
        Try
            Dim SQLWhere As String = ""

            Dim SQL As String = "select " & _
            "(strLastName||', '||strFirstName) as PhoneListName, " & _
            "strOffice, strPhone " & _
            "from AIRBRANCH.EPDUSerProfiles " & _
            "where  numEmployeeStatus = '1' "

            SQLWhere = " and ( "

            If chbAirProtectionBranch.Checked = True Then
                SQLWhere = SQLWhere & " (numBranch = '1' "
                If chbAmbientMonitoring.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '6' "
                End If
                If chbIndustrialSourceMonitoring.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '3' "
                End If
                If chbMobileAreaSource.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '1' "
                End If
                If chbPlanningSupport.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '2' "
                End If
                If chbStationarySourceCompliance.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '4' "
                End If
                If chbStationarySourcePermitting.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '5' "
                End If
                SQLWhere = SQLWhere & ") or "
            End If
            If chbDirectorOffice.Checked = True Then
                SQLWhere = SQLWhere & " (numbranch = '6') or "
            End If
            If chbHazardWaste.Checked = True Then
                SQLWhere = SQLWhere & " ( numBranch = '3' "
                If chbCorrectiveAction.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '25' "
                End If
                If chbFacilitiesCompliance.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '28' "
                End If
                If chbGeneratorCompliance.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '27' "
                End If
                If chbHazardousSitesResponse.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '26' "
                End If
                SQLWhere = SQLWhere & ") or "
            End If
            If chbLandProtectionBranch.Checked = True Then
                SQLWhere = SQLWhere & " (numBranch = '4' "
                If chbSolidWasteManagement.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '23' "
                End If
                If chbUndergroundStorageTankManagement.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '24'"
                End If
                If chbWasteReductionAndAbatement.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '22' "
                End If
                SQLWhere = SQLWhere & ") or "
            End If
            If chbProgramCoordinationBranch.Checked = True Then
                SQLWhere = SQLWhere & " (numBranch = '5' "
                If chbDistrictAlbany.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '14' "
                End If
                If chbDistrictAthens.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '13' "
                End If
                If chbDistrictAtlanta.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '12' "
                End If
                If chbDistrictAugusta.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '7' "
                End If
                If chbDistrictBrunswick.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '9' "
                End If
                If chbDistrictcartersville.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '10' "
                End If
                If chbDistrictMacon.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '15' "
                End If
                If chbDistrictSavannah.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '11' "
                End If
                If chbEmergencyResponse.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '30' "
                End If
                If chbEnvironmentalEmergency.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '29' "
                End If
                If chbEnvironmentalRadiation.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '31' "
                End If
                If chbLaboratoryOperations.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '32' "
                End If
                If chbQualityAssurance.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '33' "
                End If
                If chbRadioactiveMaterials.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '34' "
                End If
                If chbSmallBusinessAssistance.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '35' "
                End If
                SQLWhere = SQLWhere & ") or "
            End If
            If chbWatershedProtectionBranch.Checked = True Then
                SQLWhere = SQLWhere & " (numBranch = '2' "
                If chbDrikingWaterCompliance.Checked = False Then
                    SQLWhere = SQLWhere & " (numProgram <> '36' "
                End If
                If chbDrinkingWaterPermitting.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '37' "
                End If
                If chbEngineeringAndTechnical.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '38' "
                End If
                If chbFloodplainManagement.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '39' '"
                End If
                If chbNonPointSource.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '40 ' "
                End If
                If chbPermitComplianceAndEnforcement.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '41' "
                End If
                If chbSafeDams.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '42' "
                End If
                If chbTotalMaximumDailiyLoadImplementation.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '43' "
                End If
                If chbWaterWithdrawalPermitting.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '44' "
                End If
                If chbWatershedPlanningAndMonitoring.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '45' "
                End If
                If chbWatershedProtrectionBasinAnalysis.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '46' "
                End If
                If chbWatershedProtectionRegulatorySupport.Checked = False Then
                    SQLWhere = SQLWhere & " and numProgram <> '47' "
                End If
                SQLWhere = SQLWhere & ") or "
            End If

            If SQLWhere = " and ( " Then
                SQL = SQL & " order by strLastName "
            Else
                SQL = SQL & Mid(SQLWhere, 1, (SQLWhere.Length - 3)) & ") order by strLastName "
            End If


            If PrintOut Is Nothing Then
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            Else
                PrintOut.Dispose()
                PrintOut = New IAIPPrintOut
            End If
            PrintOut.txtOther.Text = SQL
            PrintOut.txtPrintType.Text = "PhoneList"

            PrintOut.Show()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub


End Class