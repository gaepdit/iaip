Imports System.Data.SqlClient

Public Class ISMPConfidentialData

    ' Positional numbers in the methods below are all "ONE"-based

    Private Property ConfidentialData As String = ""
    Private Property DocumentType As String = ""

    Public Sub LoadData()
        Try
            Dim query As String = "Select strConfidentialData, strDocumentType  " &
            "from ISMPReportInformation " &
            "where strReferenceNumber = @ref "

            Dim p As New SqlParameter("@ref", txtReferenceNumber.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = ""
                Else
                    ConfidentialData = dr.Item("strConfidentialData").ToString
                End If

                If IsDBNull(dr.Item("strDocumentType")) Then
                    DocumentType = ""
                Else
                    DocumentType = dr.Item("strDocumentType").ToString
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        Select Case DocumentType
            Case "002"
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCOneStack.TabPages.Remove(TPThreeRuns)
                TCOneStack.TabPages.Remove(TPFourRuns)

            Case "003"
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCOneStack.TabPages.Remove(TPTwoRuns)
                TCOneStack.TabPages.Remove(TPFourRuns)

            Case "004"
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCOneStack.TabPages.Remove(TPTwoRuns)
                TCOneStack.TabPages.Remove(TPThreeRuns)

            Case "005"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCTwoStack.TabPages.Remove(TPDRE)

            Case "006"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCTwoStack.TabPages.Remove(TPTwoStackStandard)

            Case "007"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)

            Case "008"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)

            Case "009"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)

            Case "010"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)

            Case "011"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)

            Case "012"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCMemorandum.TabPages.Remove(TPToFile)
                TCMemorandum.TabPages.Remove(TPPTE)

            Case "013"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCMemorandum.TabPages.Remove(TPStandard)
                TCMemorandum.TabPages.Remove(TPPTE)

            Case "014"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCMethod9.TabPages.Remove(TPMethod9Single)

            Case "015"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)

            Case "016"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCMethod9.TabPages.Remove(TPMethod9Multi)

            Case "018"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPMethod22)
                TCMemorandum.TabPages.Remove(TPStandard)
                TCMemorandum.TabPages.Remove(TPToFile)

            Case Else ' "001", "017"
                TCDocuments.TabPages.Remove(TPOneStack)
                TCDocuments.TabPages.Remove(TPLoadingRack)
                TCDocuments.TabPages.Remove(TPPulpCondensate)
                TCDocuments.TabPages.Remove(TPGasConcentration)
                TCDocuments.TabPages.Remove(TPFlare)
                TCDocuments.TabPages.Remove(TPMethod9)
                TCDocuments.TabPages.Remove(TPMemorandum)
                TCDocuments.TabPages.Remove(TPRATA)
                TCDocuments.TabPages.Remove(TPTwoStack)
                TCDocuments.TabPages.Remove(TPMethod22)

        End Select

        If ConfidentialData = "" OrElse Mid(ConfidentialData, 1, 1) = "0" Then
            Return
        End If

        chbAIRSNumber.Checked = Mid(ConfidentialData, 3, 1) = "1"
        chbFacilityName.Checked = Mid(ConfidentialData, 4, 1) = "1"
        chbLocation.Checked = Mid(ConfidentialData, 5, 1) = "1"
        chbReportType.Checked = Mid(ConfidentialData, 6, 1) = "1"
        chbISMPReviewer.Checked = Mid(ConfidentialData, 7, 1) = "1"
        chbISMPUnit.Checked = Mid(ConfidentialData, 8, 1) = "1"
        chbISMPProgramManager.Checked = Mid(ConfidentialData, 9, 1) = "1"
        chbISMPUnitManager.Checked = Mid(ConfidentialData, 10, 1) = "1"
        chbTestNotification.Checked = Mid(ConfidentialData, 11, 1) = "1"
        chbWitnessingEngineer.Checked = Mid(ConfidentialData, 12, 1) = "1"
        chbOtherWitnessingEngineer.Checked = Mid(ConfidentialData, 13, 1) = "1"
        chbSourceTested.Checked = Mid(ConfidentialData, 14, 1) = "1"
        chbPollutant.Checked = Mid(ConfidentialData, 15, 1) = "1"
        chbMethodUsed.Checked = Mid(ConfidentialData, 16, 1) = "1"
        chbTestingFirm.Checked = Mid(ConfidentialData, 17, 1) = "1"
        chbISMPComplianceDetermination.Checked = Mid(ConfidentialData, 18, 1) = "1"
        chbDatesTested.Checked = Mid(ConfidentialData, 19, 1) = "1"
        chbDaysInAPB.Checked = Mid(ConfidentialData, 20, 1) = "1"
        chbReceivedByAPB.Checked = Mid(ConfidentialData, 21, 1) = "1"
        chbAssignedToEngineer.Checked = Mid(ConfidentialData, 22, 1) = "1"
        chbCompletedByISMP.Checked = Mid(ConfidentialData, 23, 1) = "1"
        chbComplianceManager.Checked = Mid(ConfidentialData, 24, 1) = "1"
        chbCC.Checked = Mid(ConfidentialData, 25, 1) = "1"

        Select Case Mid(ConfidentialData, 2, 1)
            Case "A" 'One Stack 2 Run
                chbOneStackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbOneStackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbOneStackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbOneStackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbOneStackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbOneStackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbOneStackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbOneStackPercentAllow.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbOneStackOtherInfo.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbOneStack2Run1.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbOneStack2Temp1.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbOneStack2Moist1.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbOneStack2ACFM1.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbOneStack2DSCFM1.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbOneStack2Poll1.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbOneStack2Emiss1.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbOneStack2Run2.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbOneStack2Temp2.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbOneStack2Moist2.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbOneStack2ACFM2.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbOneStack2DSCFM2.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbOneStack2Poll2.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbOneStack2Emiss2.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbOneStack2PollUnit.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbOneStack2PollAvg.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbOneStack2EmissUnit.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbOneStack2EmissAvg.Checked = Mid(ConfidentialData, 52, 1) = "1"

            Case "B" 'One Stack 3 Run
                chbOneStackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbOneStackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbOneStackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbOneStackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbOneStackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbOneStackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbOneStackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbOneStackPercentAllow.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbOneStackOtherInfo.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbOneStack3Run1.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbOneStack3Temp1.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbOneStack3Moist1.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbOneStack3ACFM1.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbOneStack3DSCFM1.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbOneStack3Poll1.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbOneStack3Emiss1.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbOneStack3Run2.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbOneStack3Temp2.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbOneStack3Moist2.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbOneStack3ACFM2.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbOneStack3DSCFM2.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbOneStack3Poll2.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbOneStack3Emiss2.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbOneStack3Run3.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbOneStack3Temp3.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbOneStack3Moist3.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbOneStack3ACFM3.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbOneStack3DSCFM3.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbOneStack3Poll3.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbOneStack3Emiss3.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbOneStack3PollUnit.Checked = Mid(ConfidentialData, 56, 1) = "1"
                chbOneStack3PollAvg.Checked = Mid(ConfidentialData, 57, 1) = "1"
                chbOneStack3EmissUnit.Checked = Mid(ConfidentialData, 58, 1) = "1"
                chbOneStack3EmissAvg.Checked = Mid(ConfidentialData, 59, 1) = "1"

            Case "C" 'One Stack 4 Run
                chbOneStackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbOneStackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbOneStackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbOneStackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbOneStackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbOneStackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbOneStackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbOneStackPercentAllow.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbOneStackOtherInfo.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbOneStack4Run1.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbOneStack4Temp1.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbOneStack4Moist1.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbOneStack4ACFM1.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbOneStack4DSCFM1.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbOneStack4Poll1.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbOneStack4Emiss1.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbOneStack4Run2.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbOneStack4Temp2.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbOneStack4Moist2.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbOneStack4ACFM2.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbOneStack4DSCFM2.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbOneStack4Poll2.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbOneStack4Emiss2.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbOneStack4Run3.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbOneStack4Temp3.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbOneStack4Moist3.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbOneStack4ACFM3.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbOneStack4DSCFM3.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbOneStack4Poll3.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbOneStack4Emiss3.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbOneStack4Run4.Checked = Mid(ConfidentialData, 56, 1) = "1"
                chbOneStack4Temp4.Checked = Mid(ConfidentialData, 57, 1) = "1"
                chbOneStack4Moist4.Checked = Mid(ConfidentialData, 58, 1) = "1"
                chbOneStack4ACFM4.Checked = Mid(ConfidentialData, 59, 1) = "1"
                chbOneStack4DSCFM4.Checked = Mid(ConfidentialData, 60, 1) = "1"
                chbOneStack4Poll4.Checked = Mid(ConfidentialData, 61, 1) = "1"
                chbOneStack4Emiss4.Checked = Mid(ConfidentialData, 62, 1) = "1"
                chbOneStack4PollUnit.Checked = Mid(ConfidentialData, 63, 1) = "1"
                chbOneStack4PollAvg.Checked = Mid(ConfidentialData, 64, 1) = "1"
                chbOneStack4EmissUnit.Checked = Mid(ConfidentialData, 65, 1) = "1"
                chbOneStack4EmissAvg.Checked = Mid(ConfidentialData, 66, 1) = "1"

            Case "D" 'Two Stack Standard
                chbTwoStackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbTwoStackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbTwoStackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbTwoStackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbTwoStackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbTwoStackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbTwoStackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbTwoStackOtherInfo.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbTwoStackStandName1.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbTwoStackStandName2.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbTwoStackStandRun1a.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbTwoStackStandTemp1a.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbTwoStackStandMoist1a.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbTwoStackStandACFM1a.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbTwoStackStandDSCFM1a.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbTwoStackStandPoll1a.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbTwoStackStandEmiss1a.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbTwoStackStandRun2a.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbTwoStackStandTemp2a.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbTwoStackStandMoist2a.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbTwoStackStandACFM2a.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbTwoStackStandDSCFM2a.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbTwoStackStandPoll2a.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbTwoStackStandEmiss2a.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbTwoStackStandRun3a.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbTwoStackStandTemp3a.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbTwoStackStandMoist3a.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbTwoStackStandACFM3a.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbTwoStackStandDSCFM3a.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbTwoStackStandPoll3a.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbTwoStackStandEmiss3a.Checked = Mid(ConfidentialData, 56, 1) = "1"
                chbTwoStackStandRun1b.Checked = Mid(ConfidentialData, 57, 1) = "1"
                chbTwoStackStandTemp1b.Checked = Mid(ConfidentialData, 58, 1) = "1"
                chbTwoStackStandMoist1b.Checked = Mid(ConfidentialData, 59, 1) = "1"
                chbTwoStackStandACFM1b.Checked = Mid(ConfidentialData, 60, 1) = "1"
                chbTwoStackStandDSCFM1b.Checked = Mid(ConfidentialData, 61, 1) = "1"
                chbTwoStackStandPoll1b.Checked = Mid(ConfidentialData, 62, 1) = "1"
                chbTwoStackStandEmiss1b.Checked = Mid(ConfidentialData, 63, 1) = "1"
                chbTwoStackStandRun2b.Checked = Mid(ConfidentialData, 64, 1) = "1"
                chbTwoStackStandTemp2b.Checked = Mid(ConfidentialData, 65, 1) = "1"
                chbTwoStackStandMoist2b.Checked = Mid(ConfidentialData, 66, 1) = "1"
                chbTwoStackStandACFM2b.Checked = Mid(ConfidentialData, 67, 1) = "1"
                chbTwoStackStandDSCFM2b.Checked = Mid(ConfidentialData, 68, 1) = "1"
                chbTwoStackStandPoll2b.Checked = Mid(ConfidentialData, 69, 1) = "1"
                chbTwoStackStandEmiss2b.Checked = Mid(ConfidentialData, 70, 1) = "1"
                chbTwoStackStandRun3b.Checked = Mid(ConfidentialData, 71, 1) = "1"
                chbTwoStackStandTemp3b.Checked = Mid(ConfidentialData, 72, 1) = "1"
                chbTwoStackStandMoist3b.Checked = Mid(ConfidentialData, 73, 1) = "1"
                chbTwoStackStandACFM3b.Checked = Mid(ConfidentialData, 74, 1) = "1"
                chbTwoStackStandDSCFM3b.Checked = Mid(ConfidentialData, 75, 1) = "1"
                chbTwoStackStandPoll3b.Checked = Mid(ConfidentialData, 76, 1) = "1"
                chbTwoStackStandEmiss3b.Checked = Mid(ConfidentialData, 77, 1) = "1"
                chbTwoStackStandPollUnit.Checked = Mid(ConfidentialData, 78, 1) = "1"
                chbTwoStackStandPollAvg1.Checked = Mid(ConfidentialData, 79, 1) = "1"
                chbTwoStackStandPollAvg2.Checked = Mid(ConfidentialData, 80, 1) = "1"
                chbTwoStackStandEmissUnit.Checked = Mid(ConfidentialData, 81, 1) = "1"
                chbTwoStackStandEmissAvg1.Checked = Mid(ConfidentialData, 82, 1) = "1"
                chbTwoStackStandEmissAvg2.Checked = Mid(ConfidentialData, 83, 1) = "1"
                chbTwoStackStandTotal1.Checked = Mid(ConfidentialData, 84, 1) = "1"
                chbTwoStackStandTotal2.Checked = Mid(ConfidentialData, 85, 1) = "1"
                chbTwoStackStandTotal3.Checked = Mid(ConfidentialData, 86, 1) = "1"
                chbTwoStackStandTotalAvg.Checked = Mid(ConfidentialData, 87, 1) = "1"
                chbTwoStackStandPercentAllow.Checked = Mid(ConfidentialData, 88, 1) = "1"

            Case "E"  'Two Stack DRE
                chbTwoStackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbTwoStackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbTwoStackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbTwoStackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbTwoStackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbTwoStackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbTwoStackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbTwoStackOtherInfo.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbTwoStackDREName1.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbTwoStackDREName2.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbTwoStackDRERun1a.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbTwoStackDRETemp1a.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbTwoStackDREMoist1a.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbTwoStackDREACFM1a.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbTwoStackDREDSCFM1a.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbTwoStackDREPoll1a.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbTwoStackDREEmiss1a.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbTwoStackDRERun2a.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbTwoStackDRETemp2a.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbTwoStackDREMoist2a.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbTwoStackDREACFM2a.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbTwoStackDREDSCFM2a.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbTwoStackDREPoll2a.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbTwoStackDREEmiss2a.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbTwoStackDRERun3a.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbTwoStackDRETEmp3a.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbTwoStackDREMoist3a.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbTwoStackDREACFM3a.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbTwoStackDREDSCFM3a.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbTwoStackDREPoll3a.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbTwoStackDREEmiss3a.Checked = Mid(ConfidentialData, 56, 1) = "1"
                chbTwoStackDRERun1b.Checked = Mid(ConfidentialData, 57, 1) = "1"
                chbTwoStackDRETemp1b.Checked = Mid(ConfidentialData, 58, 1) = "1"
                chbTwoStackDREMoist1b.Checked = Mid(ConfidentialData, 59, 1) = "1"
                chbTwoStackDREACFM1b.Checked = Mid(ConfidentialData, 60, 1) = "1"
                chbTwoStackDREDSCFM1b.Checked = Mid(ConfidentialData, 61, 1) = "1"
                chbTwoStackDREPoll1b.Checked = Mid(ConfidentialData, 62, 1) = "1"
                chbTwoStackDREEmiss1b.Checked = Mid(ConfidentialData, 63, 1) = "1"
                chbTwoStackDRERun2b.Checked = Mid(ConfidentialData, 64, 1) = "1"
                chbTwoStackDRETemp2b.Checked = Mid(ConfidentialData, 65, 1) = "1"
                chbTwoStackDREMoist2b.Checked = Mid(ConfidentialData, 66, 1) = "1"
                chbTwoStackDREACFM2b.Checked = Mid(ConfidentialData, 67, 1) = "1"
                chbTwoStackDREDSCFM2b.Checked = Mid(ConfidentialData, 68, 1) = "1"
                chbTwoStackDREPoll2b.Checked = Mid(ConfidentialData, 69, 1) = "1"
                chbTwoStackDREEmiss2b.Checked = Mid(ConfidentialData, 70, 1) = "1"
                chbTwoStackDRERun3b.Checked = Mid(ConfidentialData, 71, 1) = "1"
                chbTwoStackDRETemp3b.Checked = Mid(ConfidentialData, 72, 1) = "1"
                chbTwoStackDREMoist3b.Checked = Mid(ConfidentialData, 73, 1) = "1"
                chbTwoStackDREACFM3b.Checked = Mid(ConfidentialData, 74, 1) = "1"
                chbTwoStackDREDSCFM3b.Checked = Mid(ConfidentialData, 75, 1) = "1"
                chbTwoStackDREPoll3b.Checked = Mid(ConfidentialData, 76, 1) = "1"
                chbTwoStackDREEmiss3b.Checked = Mid(ConfidentialData, 77, 1) = "1"
                chbTwoStackDREPollUnit.Checked = Mid(ConfidentialData, 78, 1) = "1"
                chbTwoStackDREPollAvg1.Checked = Mid(ConfidentialData, 79, 1) = "1"
                chbTwoStackDREPollAvg2.Checked = Mid(ConfidentialData, 80, 1) = "1"
                chbTwoStackDREEmissUnit.Checked = Mid(ConfidentialData, 81, 1) = "1"
                chbTwoStackDREEmissAvg1.Checked = Mid(ConfidentialData, 82, 1) = "1"
                chbTwoStackDREEmissAvg2.Checked = Mid(ConfidentialData, 83, 1) = "1"
                chbTwoStackDREDestructionEff.Checked = Mid(ConfidentialData, 84, 1) = "1"

            Case "F"  'Loading Rack
                chbLoadingRackMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbLoadingRackOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbLoadingRackAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbLoadingRackAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbLoadingRackAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbLoadingRackAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbLoadingRackControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbLoadingRackTestDuration.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbLoadingRackPollIN.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbLoadingRackPollOUT.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbLoadingRackDestReduction.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbLoadingRackEmiss.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbLoadingRackOtherInfo.Checked = Mid(ConfidentialData, 38, 1) = "1"

            Case "G"  'Pulp
                chbPulpMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbPulpOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbPulpAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbPulpAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbPulpAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbPulpAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbPulpControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbPulpRun1.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbPulpConc1.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbPulpTreatment1.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbPulpRun2.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbPulpConc2.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbPulpTreatment2.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbPulpRun3.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbPulpConc3.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbPulpTreatment3.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbPulpConcUnit.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbPulpConcAvg.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbPulpTreatmentUnit.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbPulpTreatmentAvg.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbPulpDestructEffic.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbPulpOtherInfo.Checked = Mid(ConfidentialData, 47, 1) = "1"

            Case "H" 'Gas
                chbGasMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbGasOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbGasAllowEmiss1.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbGasAllowEmiss2.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbGasAllowEmiss3.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbGasAppRequire.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbGasControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbGasRun1.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbGasPoll1.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbGasEmiss1.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbGasRun2.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbGasPoll2.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbGasEmiss2.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbGasRun3.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbGasPoll3.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbGasEmiss3.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbGasPollUnit.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbGasPollAvg.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbGasEmissUnit.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbGasEmissAvg.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbGasPercentAllow.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbGasOtherInfo.Checked = Mid(ConfidentialData, 47, 1) = "1"

            Case "I"  'Flare
                chbFlareMaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbFlareOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbFlareAllowLimitations.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbFlareHeatContent.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbFlareAppRequire.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbFlareMonitor.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbFlareRun1.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbFlareHeating1.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbFlareVelocity1.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbFlareRun2.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbFlareHeating2.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbFlareVelocity2.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbFlareRun3.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbFlareHeating3.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbFlareVelocity3.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbFlareHeatingUnit.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbFlareHeatingAvg.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbFlareVelocityUnit.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbFlareVelocityAvg.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbFlarePercentAllow.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbFlareOtherInfo.Checked = Mid(ConfidentialData, 46, 1) = "1"

            Case "J"  'RATA
                chbRATAAppStandard.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbRATAAppRegulation.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbRATADiluent.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbRATARef1.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbRATARef2.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbRATARef3.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbRATARef4.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbRATARef5.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbRATARef6.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbRATARef7.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbRATARef8.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbRATARef9.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbRATARef10.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbRATARef11.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbRATARef12.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbRATACMS1.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbRATACMS2.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbRATACMS3.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbRATACMS4.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbRATACMS5.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbRATACMS6.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbRATACMS7.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbRATACMS8.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbRATACMS9.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbRATACMS10.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbRATACMS11.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbRATACMS12.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbRATAUnits.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbRATARelativeAcc.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbRATAStatement.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbRATAOtherInformation.Checked = Mid(ConfidentialData, 56, 1) = "1"

            Case "K" 'Memorandum
                chbMemoAppRequire.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMemoStandardMemo.Checked = Mid(ConfidentialData, 27, 1) = "1"

            Case "L" 'Memo to File
                chbMemoAppRequire.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMemoToFileManufacture.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbMemoToFileSerial.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbMemoToFileMemo.Checked = Mid(ConfidentialData, 29, 1) = "1"

            Case "M" 'Method 9 Multi.
                chbMethod9MultiMaxOpCapacity1.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMethod9MultiMaxOpCapacity2.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbMethod9MultiMaxOpCapacity3.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbMethod9MultiMaxOpCapacity4.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbMethod9MultiMaxOpCapacity5.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbMethod9MultiMaxOpCapacityUnit.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbMethod9MultiOpCapacity1.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbMethod9MultiOpCapacity2.Checked = Mid(ConfidentialData, 33, 1) = "1"
                chbMethod9MultiOpCapacity3.Checked = Mid(ConfidentialData, 34, 1) = "1"
                chbMethod9MultiOpCapacity4.Checked = Mid(ConfidentialData, 35, 1) = "1"
                chbMethod9MultiOpCapacity5.Checked = Mid(ConfidentialData, 36, 1) = "1"
                chbMethod9MultiOpCapacityUnit.Checked = Mid(ConfidentialData, 37, 1) = "1"
                chbMethod9MultiAllowEmiss1.Checked = Mid(ConfidentialData, 38, 1) = "1"
                chbMethod9MultiAllowEmiss2.Checked = Mid(ConfidentialData, 39, 1) = "1"
                chbMethod9MultiAllowEmiss3.Checked = Mid(ConfidentialData, 40, 1) = "1"
                chbMethod9MultiAllowEmiss4.Checked = Mid(ConfidentialData, 41, 1) = "1"
                chbMethod9MultiAllowEmiss5.Checked = Mid(ConfidentialData, 42, 1) = "1"
                chbMethod9MultiAllowEmissUnit.Checked = Mid(ConfidentialData, 43, 1) = "1"
                chbMethod9MultiAppRequire.Checked = Mid(ConfidentialData, 44, 1) = "1"
                chbMethod9MultiControlEquip.Checked = Mid(ConfidentialData, 45, 1) = "1"
                chbMethod9MultiAvg1.Checked = Mid(ConfidentialData, 46, 1) = "1"
                chbMethod9MultiAvg2.Checked = Mid(ConfidentialData, 47, 1) = "1"
                chbMethod9MultiAvg3.Checked = Mid(ConfidentialData, 48, 1) = "1"
                chbMethod9MultiAvg4.Checked = Mid(ConfidentialData, 49, 1) = "1"
                chbMethod9MultiAvg5.Checked = Mid(ConfidentialData, 50, 1) = "1"
                chbMethod9MultiOtherInfor.Checked = Mid(ConfidentialData, 51, 1) = "1"
                chbMethod9MultiEquip1.Checked = Mid(ConfidentialData, 52, 1) = "1"
                chbMethod9MultiEquip2.Checked = Mid(ConfidentialData, 53, 1) = "1"
                chbMethod9MultiEquip3.Checked = Mid(ConfidentialData, 54, 1) = "1"
                chbMethod9MultiEquip4.Checked = Mid(ConfidentialData, 55, 1) = "1"
                chbMethod9MultiEquip5.Checked = Mid(ConfidentialData, 56, 1) = "1"

            Case "N" 'Method 22 
                chbMethod22MaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMethod22OpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbMethod22AllowEmiss.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbMethod22AppReg.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbMethod22TestDuration.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbMethod22Emission.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbMethod22OtherInfo.Checked = Mid(ConfidentialData, 32, 1) = "1"

            Case "O" 'Method 9 Single
                chbMethod9MaxOpCapacity.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMethod9OpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbMethod9AllowEmiss.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbMethod9AppRequire.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbMethod9ControlEquip.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbMethod9TestDuration.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbMethod9Opacity.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbMethod9OtherInfo.Checked = Mid(ConfidentialData, 33, 1) = "1"

            Case "Q" 'PTE
                chbMemoAppRequire.Checked = Mid(ConfidentialData, 26, 1) = "1"
                chbMemoPTEMaxOpCapacity.Checked = Mid(ConfidentialData, 27, 1) = "1"
                chbMemoPTEOpCapacity.Checked = Mid(ConfidentialData, 28, 1) = "1"
                chbMemoPTEAllowEmiss1.Checked = Mid(ConfidentialData, 29, 1) = "1"
                chbMemoPTEAllowEmiss2.Checked = Mid(ConfidentialData, 30, 1) = "1"
                chbMemoPTEAllowEmiss3.Checked = Mid(ConfidentialData, 31, 1) = "1"
                chbMemoPTEControlEquip.Checked = Mid(ConfidentialData, 32, 1) = "1"
                chbMemoPTEMemo.Checked = Mid(ConfidentialData, 33, 1) = "1"

            Case Else

        End Select

    End Sub

    Private Sub SaveConfidentialData()

        If txtReferenceNumber.Text = "" OrElse DocumentType = "" Then Return

        ConfidentialData = ""
        ConfidentialData &= If(chbAIRSNumber.Checked, "1", "0") ' 3
        ConfidentialData &= If(chbFacilityName.Checked, "1", "0") ' 4
        ConfidentialData &= If(chbLocation.Checked, "1", "0") ' 5
        ConfidentialData &= If(chbReportType.Checked, "1", "0") ' 6
        ConfidentialData &= If(chbISMPReviewer.Checked, "1", "0") ' 7
        ConfidentialData &= If(chbISMPUnit.Checked, "1", "0") ' 8
        ConfidentialData &= If(chbISMPProgramManager.Checked, "1", "0") ' 9
        ConfidentialData &= If(chbISMPUnitManager.Checked, "1", "0") ' 10
        ConfidentialData &= If(chbTestNotification.Checked, "1", "0") ' 11
        ConfidentialData &= If(chbWitnessingEngineer.Checked, "1", "0") ' 12
        ConfidentialData &= If(chbOtherWitnessingEngineer.Checked, "1", "0") ' 13
        ConfidentialData &= If(chbSourceTested.Checked, "1", "0") ' 14
        ConfidentialData &= If(chbPollutant.Checked, "1", "0") ' 15
        ConfidentialData &= If(chbMethodUsed.Checked, "1", "0") ' 16
        ConfidentialData &= If(chbTestingFirm.Checked, "1", "0") ' 17
        ConfidentialData &= If(chbISMPComplianceDetermination.Checked, "1", "0") ' 18
        ConfidentialData &= If(chbDatesTested.Checked, "1", "0") ' 19
        ConfidentialData &= If(chbDaysInAPB.Checked, "1", "0") ' 20
        ConfidentialData &= If(chbReceivedByAPB.Checked, "1", "0") ' 21
        ConfidentialData &= If(chbAssignedToEngineer.Checked, "1", "0") ' 22
        ConfidentialData &= If(chbCompletedByISMP.Checked, "1", "0") ' 23
        ConfidentialData &= If(chbComplianceManager.Checked, "1", "0") ' 24
        ConfidentialData &= If(chbCC.Checked, "1", "0") ' 25

        Select Case DocumentType
            Case "002"
                ConfidentialData = "A" & ConfidentialData ' 2

                ConfidentialData &= If(chbOneStackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbOneStackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbOneStackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbOneStackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbOneStackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbOneStackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbOneStackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbOneStackPercentAllow.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbOneStackOtherInfo.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbOneStack2Run1.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbOneStack2Temp1.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbOneStack2Moist1.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbOneStack2ACFM1.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbOneStack2DSCFM1.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbOneStack2Poll1.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbOneStack2Emiss1.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbOneStack2Run2.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbOneStack2Temp2.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbOneStack2Moist2.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbOneStack2ACFM2.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbOneStack2DSCFM2.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbOneStack2Poll2.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbOneStack2Emiss2.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbOneStack2PollUnit.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbOneStack2PollAvg.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbOneStack2EmissUnit.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbOneStack2EmissAvg.Checked, "1", "0") ' 52

            Case "003"
                ConfidentialData = "B" & ConfidentialData ' 2

                ConfidentialData &= If(chbOneStackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbOneStackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbOneStackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbOneStackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbOneStackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbOneStackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbOneStackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbOneStackPercentAllow.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbOneStackOtherInfo.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbOneStack3Run1.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbOneStack3Temp1.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbOneStack3Moist1.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbOneStack3ACFM1.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbOneStack3DSCFM1.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbOneStack3Poll1.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbOneStack3Emiss1.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbOneStack3Run2.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbOneStack3Temp2.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbOneStack3Moist2.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbOneStack3ACFM2.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbOneStack3DSCFM2.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbOneStack3Poll2.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbOneStack3Emiss2.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbOneStack3Run3.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbOneStack3Temp3.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbOneStack3Moist3.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbOneStack3ACFM3.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbOneStack3DSCFM3.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbOneStack3Poll3.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbOneStack3Emiss3.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbOneStack3PollUnit.Checked, "1", "0") ' 56
                ConfidentialData &= If(chbOneStack3PollAvg.Checked, "1", "0") ' 57
                ConfidentialData &= If(chbOneStack3EmissUnit.Checked, "1", "0") ' 58
                ConfidentialData &= If(chbOneStack3EmissAvg.Checked, "1", "0") ' 59

            Case "004"
                ConfidentialData = "C" & ConfidentialData ' 2

                ConfidentialData &= If(chbOneStackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbOneStackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbOneStackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbOneStackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbOneStackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbOneStackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbOneStackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbOneStackPercentAllow.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbOneStackOtherInfo.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbOneStack4Run1.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbOneStack4Temp1.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbOneStack4Moist1.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbOneStack4ACFM1.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbOneStack4DSCFM1.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbOneStack4Poll1.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbOneStack4Emiss1.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbOneStack4Run2.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbOneStack4Temp2.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbOneStack4Moist2.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbOneStack4ACFM2.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbOneStack4DSCFM2.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbOneStack4Poll2.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbOneStack4Emiss2.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbOneStack4Run3.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbOneStack4Temp3.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbOneStack4Moist3.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbOneStack4ACFM3.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbOneStack4DSCFM3.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbOneStack4Poll3.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbOneStack4Emiss3.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbOneStack4Run4.Checked, "1", "0") ' 56
                ConfidentialData &= If(chbOneStack4Temp4.Checked, "1", "0") ' 57
                ConfidentialData &= If(chbOneStack4Moist4.Checked, "1", "0") ' 58
                ConfidentialData &= If(chbOneStack4ACFM4.Checked, "1", "0") ' 59
                ConfidentialData &= If(chbOneStack4DSCFM4.Checked, "1", "0") ' 60
                ConfidentialData &= If(chbOneStack4Poll4.Checked, "1", "0") ' 61
                ConfidentialData &= If(chbOneStack4Emiss4.Checked, "1", "0") ' 62
                ConfidentialData &= If(chbOneStack4PollUnit.Checked, "1", "0") ' 63
                ConfidentialData &= If(chbOneStack4PollAvg.Checked, "1", "0") ' 64
                ConfidentialData &= If(chbOneStack4EmissUnit.Checked, "1", "0") ' 65
                ConfidentialData &= If(chbOneStack4EmissAvg.Checked, "1", "0") ' 66


            Case "005"
                ConfidentialData = "D" & ConfidentialData ' 2

                ConfidentialData &= If(chbTwoStackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbTwoStackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbTwoStackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbTwoStackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbTwoStackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbTwoStackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbTwoStackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbTwoStackOtherInfo.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbTwoStackStandName1.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbTwoStackStandName2.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbTwoStackStandRun1a.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbTwoStackStandTemp1a.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbTwoStackStandMoist1a.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbTwoStackStandACFM1a.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbTwoStackStandDSCFM1a.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbTwoStackStandPoll1a.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbTwoStackStandEmiss1a.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbTwoStackStandRun2a.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbTwoStackStandTemp2a.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbTwoStackStandMoist2a.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbTwoStackStandACFM2a.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbTwoStackStandDSCFM2a.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbTwoStackStandPoll2a.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbTwoStackStandEmiss2a.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbTwoStackStandRun3a.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbTwoStackStandTemp3a.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbTwoStackStandMoist3a.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbTwoStackStandACFM3a.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbTwoStackStandDSCFM3a.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbTwoStackStandPoll3a.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbTwoStackStandEmiss3a.Checked, "1", "0") ' 56
                ConfidentialData &= If(chbTwoStackStandRun1b.Checked, "1", "0") ' 57
                ConfidentialData &= If(chbTwoStackStandTemp1b.Checked, "1", "0") ' 58
                ConfidentialData &= If(chbTwoStackStandMoist1b.Checked, "1", "0") ' 59
                ConfidentialData &= If(chbTwoStackStandACFM1b.Checked, "1", "0") ' 60
                ConfidentialData &= If(chbTwoStackStandDSCFM1b.Checked, "1", "0") ' 61
                ConfidentialData &= If(chbTwoStackStandPoll1b.Checked, "1", "0") ' 62
                ConfidentialData &= If(chbTwoStackStandEmiss1b.Checked, "1", "0") ' 63
                ConfidentialData &= If(chbTwoStackStandRun2b.Checked, "1", "0") ' 64
                ConfidentialData &= If(chbTwoStackStandTemp2b.Checked, "1", "0") ' 65
                ConfidentialData &= If(chbTwoStackStandMoist2b.Checked, "1", "0") ' 66
                ConfidentialData &= If(chbTwoStackStandACFM2b.Checked, "1", "0") ' 67
                ConfidentialData &= If(chbTwoStackStandDSCFM2b.Checked, "1", "0") ' 68
                ConfidentialData &= If(chbTwoStackStandPoll2b.Checked, "1", "0") ' 69
                ConfidentialData &= If(chbTwoStackStandEmiss2b.Checked, "1", "0") ' 70
                ConfidentialData &= If(chbTwoStackStandRun3b.Checked, "1", "0") ' 71
                ConfidentialData &= If(chbTwoStackStandTemp3b.Checked, "1", "0") ' 72
                ConfidentialData &= If(chbTwoStackStandMoist3b.Checked, "1", "0") ' 73
                ConfidentialData &= If(chbTwoStackStandACFM3b.Checked, "1", "0") ' 74
                ConfidentialData &= If(chbTwoStackStandDSCFM3b.Checked, "1", "0") ' 75
                ConfidentialData &= If(chbTwoStackStandPoll3b.Checked, "1", "0") ' 76
                ConfidentialData &= If(chbTwoStackStandEmiss3b.Checked, "1", "0") ' 77
                ConfidentialData &= If(chbTwoStackStandPollUnit.Checked, "1", "0") ' 78
                ConfidentialData &= If(chbTwoStackStandPollAvg1.Checked, "1", "0") ' 79
                ConfidentialData &= If(chbTwoStackStandPollAvg2.Checked, "1", "0") ' 80
                ConfidentialData &= If(chbTwoStackStandEmissUnit.Checked, "1", "0") ' 81
                ConfidentialData &= If(chbTwoStackStandEmissAvg1.Checked, "1", "0") ' 82
                ConfidentialData &= If(chbTwoStackStandEmissAvg2.Checked, "1", "0") ' 83
                ConfidentialData &= If(chbTwoStackStandTotal1.Checked, "1", "0") ' 84
                ConfidentialData &= If(chbTwoStackStandTotal2.Checked, "1", "0") ' 85
                ConfidentialData &= If(chbTwoStackStandTotal3.Checked, "1", "0") ' 86
                ConfidentialData &= If(chbTwoStackStandTotalAvg.Checked, "1", "0") ' 87
                ConfidentialData &= If(chbTwoStackStandPercentAllow.Checked, "1", "0") ' 88

            Case "006"
                ConfidentialData = "E" & ConfidentialData ' 2

                ConfidentialData &= If(chbTwoStackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbTwoStackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbTwoStackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbTwoStackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbTwoStackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbTwoStackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbTwoStackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbTwoStackOtherInfo.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbTwoStackDREName1.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbTwoStackDREName2.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbTwoStackDRERun1a.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbTwoStackDRETemp1a.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbTwoStackDREMoist1a.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbTwoStackDREACFM1a.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbTwoStackDREDSCFM1a.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbTwoStackDREPoll1a.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbTwoStackDREEmiss1a.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbTwoStackDRERun2a.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbTwoStackDRETemp2a.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbTwoStackDREMoist2a.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbTwoStackDREACFM2a.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbTwoStackDREDSCFM2a.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbTwoStackDREPoll2a.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbTwoStackDREEmiss2a.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbTwoStackDRERun3a.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbTwoStackDRETEmp3a.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbTwoStackDREMoist3a.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbTwoStackDREACFM3a.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbTwoStackDREDSCFM3a.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbTwoStackDREPoll3a.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbTwoStackDREEmiss3a.Checked, "1", "0") ' 56
                ConfidentialData &= If(chbTwoStackDRERun1b.Checked, "1", "0") ' 57
                ConfidentialData &= If(chbTwoStackDRETemp1b.Checked, "1", "0") ' 58
                ConfidentialData &= If(chbTwoStackDREMoist1b.Checked, "1", "0") ' 59
                ConfidentialData &= If(chbTwoStackDREACFM1b.Checked, "1", "0") ' 60
                ConfidentialData &= If(chbTwoStackDREDSCFM1b.Checked, "1", "0") ' 61
                ConfidentialData &= If(chbTwoStackDREPoll1b.Checked, "1", "0") ' 62
                ConfidentialData &= If(chbTwoStackDREEmiss1b.Checked, "1", "0") ' 63
                ConfidentialData &= If(chbTwoStackDRERun2b.Checked, "1", "0") ' 64
                ConfidentialData &= If(chbTwoStackDRETemp2b.Checked, "1", "0") ' 65
                ConfidentialData &= If(chbTwoStackDREMoist2b.Checked, "1", "0") ' 66
                ConfidentialData &= If(chbTwoStackDREACFM2b.Checked, "1", "0") ' 67
                ConfidentialData &= If(chbTwoStackDREDSCFM2b.Checked, "1", "0") ' 68
                ConfidentialData &= If(chbTwoStackDREPoll2b.Checked, "1", "0") ' 69
                ConfidentialData &= If(chbTwoStackDREEmiss2b.Checked, "1", "0") ' 70
                ConfidentialData &= If(chbTwoStackDRERun3b.Checked, "1", "0") ' 71
                ConfidentialData &= If(chbTwoStackDRETemp3b.Checked, "1", "0") ' 72
                ConfidentialData &= If(chbTwoStackDREMoist3b.Checked, "1", "0") ' 73
                ConfidentialData &= If(chbTwoStackDREACFM3b.Checked, "1", "0") ' 74
                ConfidentialData &= If(chbTwoStackDREDSCFM3b.Checked, "1", "0") ' 75
                ConfidentialData &= If(chbTwoStackDREPoll3b.Checked, "1", "0") ' 76
                ConfidentialData &= If(chbTwoStackDREEmiss3b.Checked, "1", "0") ' 77
                ConfidentialData &= If(chbTwoStackDREPollUnit.Checked, "1", "0") ' 78
                ConfidentialData &= If(chbTwoStackDREPollAvg1.Checked, "1", "0") ' 79
                ConfidentialData &= If(chbTwoStackDREPollAvg2.Checked, "1", "0") ' 80
                ConfidentialData &= If(chbTwoStackDREEmissUnit.Checked, "1", "0") ' 81
                ConfidentialData &= If(chbTwoStackDREEmissAvg1.Checked, "1", "0") ' 82
                ConfidentialData &= If(chbTwoStackDREEmissAvg2.Checked, "1", "0") ' 83
                ConfidentialData &= If(chbTwoStackDREDestructionEff.Checked, "1", "0") ' 84

            Case "007"
                ConfidentialData = "F" & ConfidentialData ' 2

                ConfidentialData &= If(chbLoadingRackMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbLoadingRackOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbLoadingRackAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbLoadingRackAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbLoadingRackAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbLoadingRackAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbLoadingRackControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbLoadingRackTestDuration.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbLoadingRackPollIN.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbLoadingRackPollOUT.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbLoadingRackDestReduction.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbLoadingRackEmiss.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbLoadingRackOtherInfo.Checked, "1", "0") ' 38


            Case "008"
                ConfidentialData = "G" & ConfidentialData ' 2

                ConfidentialData &= If(chbPulpMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbPulpOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbPulpAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbPulpAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbPulpAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbPulpAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbPulpControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbPulpRun1.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbPulpConc1.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbPulpTreatment1.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbPulpRun2.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbPulpConc2.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbPulpTreatment2.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbPulpRun3.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbPulpConc3.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbPulpTreatment3.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbPulpConcUnit.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbPulpConcAvg.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbPulpTreatmentUnit.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbPulpTreatmentAvg.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbPulpDestructEffic.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbPulpOtherInfo.Checked, "1", "0") ' 47

            Case "009"
                ConfidentialData = "H" & ConfidentialData ' 2

                ConfidentialData &= If(chbGasMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbGasOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbGasAllowEmiss1.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbGasAllowEmiss2.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbGasAllowEmiss3.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbGasAppRequire.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbGasControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbGasRun1.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbGasPoll1.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbGasEmiss1.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbGasRun2.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbGasPoll2.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbGasEmiss2.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbGasRun3.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbGasPoll3.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbGasEmiss3.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbGasPollUnit.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbGasPollAvg.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbGasEmissUnit.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbGasEmissAvg.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbGasPercentAllow.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbGasOtherInfo.Checked, "1", "0") ' 47

            Case "010"
                ConfidentialData = "I" & ConfidentialData ' 2

                ConfidentialData &= If(chbFlareMaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbFlareOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbFlareAllowLimitations.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbFlareHeatContent.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbFlareAppRequire.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbFlareMonitor.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbFlareRun1.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbFlareHeating1.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbFlareVelocity1.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbFlareRun2.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbFlareHeating2.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbFlareVelocity2.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbFlareRun3.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbFlareHeating3.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbFlareVelocity3.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbFlareHeatingUnit.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbFlareHeatingAvg.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbFlareVelocityUnit.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbFlareVelocityAvg.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbFlarePercentAllow.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbFlareOtherInfo.Checked, "1", "0") ' 46

            Case "011"
                ConfidentialData = "J" & ConfidentialData ' 2

                ConfidentialData &= If(chbRATAAppStandard.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbRATAAppRegulation.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbRATADiluent.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbRATARef1.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbRATARef2.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbRATARef3.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbRATARef4.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbRATARef5.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbRATARef6.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbRATARef7.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbRATARef8.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbRATARef9.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbRATARef10.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbRATARef11.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbRATARef12.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbRATACMS1.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbRATACMS2.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbRATACMS3.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbRATACMS4.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbRATACMS5.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbRATACMS6.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbRATACMS7.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbRATACMS8.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbRATACMS9.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbRATACMS10.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbRATACMS11.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbRATACMS12.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbRATAUnits.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbRATARelativeAcc.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbRATAStatement.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbRATAOtherInformation.Checked, "1", "0") ' 56

            Case "012"
                ConfidentialData = "K" & ConfidentialData ' 2

                ConfidentialData &= If(chbMemoAppRequire.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMemoStandardMemo.Checked, "1", "0") ' 27

            Case "013"
                ConfidentialData = "L" & ConfidentialData ' 2

                ConfidentialData &= If(chbMemoAppRequire.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMemoToFileManufacture.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbMemoToFileSerial.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbMemoToFileMemo.Checked, "1", "0") ' 29

            Case "014"
                ConfidentialData = "M" & ConfidentialData ' 2

                ConfidentialData &= If(chbMethod9MultiMaxOpCapacity1.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMethod9MultiMaxOpCapacity2.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbMethod9MultiMaxOpCapacity3.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbMethod9MultiMaxOpCapacity4.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbMethod9MultiMaxOpCapacity5.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbMethod9MultiMaxOpCapacityUnit.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbMethod9MultiOpCapacity1.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbMethod9MultiOpCapacity2.Checked, "1", "0") ' 33
                ConfidentialData &= If(chbMethod9MultiOpCapacity3.Checked, "1", "0") ' 34
                ConfidentialData &= If(chbMethod9MultiOpCapacity4.Checked, "1", "0") ' 35
                ConfidentialData &= If(chbMethod9MultiOpCapacity5.Checked, "1", "0") ' 36
                ConfidentialData &= If(chbMethod9MultiOpCapacityUnit.Checked, "1", "0") ' 37
                ConfidentialData &= If(chbMethod9MultiAllowEmiss1.Checked, "1", "0") ' 38
                ConfidentialData &= If(chbMethod9MultiAllowEmiss2.Checked, "1", "0") ' 39
                ConfidentialData &= If(chbMethod9MultiAllowEmiss3.Checked, "1", "0") ' 40
                ConfidentialData &= If(chbMethod9MultiAllowEmiss4.Checked, "1", "0") ' 41
                ConfidentialData &= If(chbMethod9MultiAllowEmiss5.Checked, "1", "0") ' 42
                ConfidentialData &= If(chbMethod9MultiAllowEmissUnit.Checked, "1", "0") ' 43
                ConfidentialData &= If(chbMethod9MultiAppRequire.Checked, "1", "0") ' 44
                ConfidentialData &= If(chbMethod9MultiControlEquip.Checked, "1", "0") ' 45
                ConfidentialData &= If(chbMethod9MultiAvg1.Checked, "1", "0") ' 46
                ConfidentialData &= If(chbMethod9MultiAvg2.Checked, "1", "0") ' 47
                ConfidentialData &= If(chbMethod9MultiAvg3.Checked, "1", "0") ' 48
                ConfidentialData &= If(chbMethod9MultiAvg4.Checked, "1", "0") ' 49
                ConfidentialData &= If(chbMethod9MultiAvg5.Checked, "1", "0") ' 50
                ConfidentialData &= If(chbMethod9MultiOtherInfor.Checked, "1", "0") ' 51
                ConfidentialData &= If(chbMethod9MultiEquip1.Checked, "1", "0") ' 52
                ConfidentialData &= If(chbMethod9MultiEquip2.Checked, "1", "0") ' 53
                ConfidentialData &= If(chbMethod9MultiEquip3.Checked, "1", "0") ' 54
                ConfidentialData &= If(chbMethod9MultiEquip4.Checked, "1", "0") ' 55
                ConfidentialData &= If(chbMethod9MultiEquip5.Checked, "1", "0") ' 56

            Case "015"
                ConfidentialData = "N" & ConfidentialData ' 2

                ConfidentialData &= If(chbMethod22MaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMethod22OpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbMethod22AllowEmiss.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbMethod22AppReg.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbMethod22TestDuration.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbMethod22Emission.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbMethod22OtherInfo.Checked, "1", "0") ' 32

            Case "016"
                ConfidentialData = "O" & ConfidentialData ' 2

                ConfidentialData &= If(chbMethod9MaxOpCapacity.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMethod9OpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbMethod9AllowEmiss.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbMethod9AppRequire.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbMethod9ControlEquip.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbMethod9TestDuration.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbMethod9Opacity.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbMethod9OtherInfo.Checked, "1", "0") ' 33

            Case "018"
                ConfidentialData = "Q" & ConfidentialData ' 2

                ConfidentialData &= If(chbMemoAppRequire.Checked, "1", "0") ' 26
                ConfidentialData &= If(chbMemoPTEMaxOpCapacity.Checked, "1", "0") ' 27
                ConfidentialData &= If(chbMemoPTEOpCapacity.Checked, "1", "0") ' 28
                ConfidentialData &= If(chbMemoPTEAllowEmiss1.Checked, "1", "0") ' 29
                ConfidentialData &= If(chbMemoPTEAllowEmiss2.Checked, "1", "0") ' 30
                ConfidentialData &= If(chbMemoPTEAllowEmiss3.Checked, "1", "0") ' 31
                ConfidentialData &= If(chbMemoPTEControlEquip.Checked, "1", "0") ' 32
                ConfidentialData &= If(chbMemoPTEMemo.Checked, "1", "0") ' 33

        End Select

        If ConfidentialData.Contains("1") Then
            ConfidentialData = "1" & ConfidentialData ' 1
        Else
            ConfidentialData = "0" ' 1
        End If

        Try
            Dim query As String = "Update ISMPReportInformation set " &
            "strConfidentialData = @conf " &
            "where strReferencenumber = @ref "

            Dim p As SqlParameter() = {
                New SqlParameter("@conf", ConfidentialData),
                New SqlParameter("@ref", txtReferenceNumber.Text)
            }

            If DB.RunCommand(query, p) Then
                MessageBox.Show("Confidential data saved")
            End If

            Dim testReportForm As ISMPTestReports = OpenFormTestReportEntry(txtReferenceNumber.Text)
            If testReportForm IsNot Nothing Then
                testReportForm.LoadConfidentialData(ConfidentialData)
            End If


        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiSave_Click(sender As Object, e As EventArgs) Handles mmiSave.Click
        SaveConfidentialData()
    End Sub

End Class