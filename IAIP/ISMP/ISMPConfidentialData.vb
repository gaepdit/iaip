Imports Oracle.DataAccess.Client
'Imports System.IO

Public Class ISMPConfidentialData
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim RecExist As Boolean
    Dim ConfidentialData As String = ""
    Dim DocumentType As String


    Private Sub DEVConfidentialData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            Panel1.Text = "Mark field to be redacted..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub DEVConfidentialData_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            ISMPConfidential = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Sub LoadData()
        Try
            SQL = "Select strConfidentialData, strDocumentType  " & _
            "from AIRBRANCH.ISMPReportInformation " & _
            "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            RecExist = dr.Read
            If RecExist = True Then
                If IsDBNull(dr.Item("strConfidentialData")) Then
                    ConfidentialData = ""
                Else
                    ConfidentialData = dr.Item("strConfidentialData")
                End If
                If IsDBNull(dr.Item("strDocumentType")) Then
                    DocumentType = ""
                Else
                    DocumentType = dr.Item("strDocumentType")
                End If
            End If
            dr.Close()

            Select Case DocumentType
                Case "001"
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "002"
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCOneStack.TabPages.Remove(Me.TPThreeRuns)
                    TCOneStack.TabPages.Remove(Me.TPFourRuns)
                Case "003"
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCOneStack.TabPages.Remove(Me.TPTwoRuns)
                    TCOneStack.TabPages.Remove(Me.TPFourRuns)
                Case "004"
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCOneStack.TabPages.Remove(Me.TPTwoRuns)
                    TCOneStack.TabPages.Remove(Me.TPThreeRuns)
                Case "005"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCTwoStack.TabPages.Remove(Me.TPDRE)
                Case "006"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCTwoStack.TabPages.Remove(Me.TPTwoStackStandard)
                Case "007"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "008"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "009"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "010"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "011"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "012"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCMemorandum.TabPages.Remove(Me.TPToFile)
                    TCMemorandum.TabPages.Remove(Me.TPPTE)
                Case "013"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCMemorandum.TabPages.Remove(Me.TPStandard)
                    TCMemorandum.TabPages.Remove(Me.TPPTE)
                Case "014"
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCMethod9.TabPages.Remove(Me.TPMethod9Single)
                Case "015"
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "016"
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCMethod9.TabPages.Remove(Me.TPMethod9Multi)
                Case "017"
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
                Case "018"
                    TCDocuments.TabPages.Remove(TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)

                    TCMemorandum.TabPages.Remove(Me.TPStandard)
                    TCMemorandum.TabPages.Remove(Me.TPToFile)
                Case Else
                    TCDocuments.TabPages.Remove(Me.TPOneStack)
                    TCDocuments.TabPages.Remove(Me.TPLoadingRack)
                    TCDocuments.TabPages.Remove(Me.TPPulpCondensate)
                    TCDocuments.TabPages.Remove(Me.TPGasConcentration)
                    TCDocuments.TabPages.Remove(Me.TPFlare)
                    TCDocuments.TabPages.Remove(Me.TPPEM)
                    TCDocuments.TabPages.Remove(Me.TPMethod9)
                    TCDocuments.TabPages.Remove(Me.TPMemorandum)
                    TCDocuments.TabPages.Remove(Me.TPRATA)
                    TCDocuments.TabPages.Remove(Me.TPTwoStack)
                    TCDocuments.TabPages.Remove(Me.TPMethod22)
                    TCDocuments.TabPages.Remove(Me.TPSSCPWork)
            End Select

            If ConfidentialData <> "" And Mid(ConfidentialData, 1, 1) = "1" Then
                If Mid(ConfidentialData, 3, 1) = "1" Then
                    chbAIRSNumber.Checked = True
                Else
                    chbAIRSNumber.Checked = False
                End If
                If Mid(ConfidentialData, 4, 1) = "1" Then
                    chbFacilityName.Checked = True
                Else
                    chbFacilityName.Checked = False
                End If
                If Mid(ConfidentialData, 5, 1) = "1" Then
                    chbLocation.Checked = True
                Else
                    chbLocation.Checked = False
                End If
                If Mid(ConfidentialData, 6, 1) = "1" Then
                    chbReportType.Checked = True
                Else
                    chbReportType.Checked = False
                End If
                If Mid(ConfidentialData, 7, 1) = "1" Then
                    chbISMPReviewer.Checked = True
                Else
                    chbISMPReviewer.Checked = False
                End If
                If Mid(ConfidentialData, 8, 1) = "1" Then
                    chbISMPUnit.Checked = True
                Else
                    chbISMPUnit.Checked = False
                End If
                If Mid(ConfidentialData, 9, 1) = "1" Then
                    chbISMPProgramManager.Checked = True
                Else
                    chbISMPProgramManager.Checked = False
                End If
                If Mid(ConfidentialData, 10, 1) = "1" Then
                    chbISMPUnitManager.Checked = True
                Else
                    chbISMPUnitManager.Checked = False
                End If
                If Mid(ConfidentialData, 11, 1) = "1" Then
                    chbTestNotification.Checked = True
                Else
                    chbTestNotification.Checked = False
                End If
                If Mid(ConfidentialData, 12, 1) = "1" Then
                    chbWitnessingEngineer.Checked = True
                Else
                    chbWitnessingEngineer.Checked = False
                End If
                If Mid(ConfidentialData, 13, 1) = "1" Then
                    chbOtherWitnessingEngineer.Checked = True
                Else
                    chbOtherWitnessingEngineer.Checked = False
                End If
                If Mid(ConfidentialData, 14, 1) = "1" Then
                    chbSourceTested.Checked = True
                Else
                    chbSourceTested.Checked = False
                End If
                If Mid(ConfidentialData, 15, 1) = "1" Then
                    chbPollutant.Checked = True
                Else
                    chbPollutant.Checked = False
                End If
                If Mid(ConfidentialData, 16, 1) = "1" Then
                    chbMethodUsed.Checked = True
                Else
                    chbMethodUsed.Checked = False
                End If
                If Mid(ConfidentialData, 17, 1) = "1" Then
                    chbTestingFirm.Checked = True
                Else
                    chbTestingFirm.Checked = False
                End If
                If Mid(ConfidentialData, 18, 1) = "1" Then
                    chbISMPComplianceDetermination.Checked = True
                Else
                    chbISMPComplianceDetermination.Checked = False
                End If
                If Mid(ConfidentialData, 19, 1) = "1" Then
                    chbDatesTested.Checked = True
                Else
                    chbDatesTested.Checked = False
                End If
                If Mid(ConfidentialData, 20, 1) = "1" Then
                    chbDaysInAPB.Checked = True
                Else
                    chbDaysInAPB.Checked = False
                End If
                If Mid(ConfidentialData, 21, 1) = "1" Then
                    chbReceivedByAPB.Checked = True
                Else
                    chbReceivedByAPB.Checked = False
                End If
                If Mid(ConfidentialData, 22, 1) = "1" Then
                    chbAssignedToEngineer.Checked = True
                Else
                    chbAssignedToEngineer.Checked = False
                End If
                If Mid(ConfidentialData, 23, 1) = "1" Then
                    chbCompletedByISMP.Checked = True
                Else
                    chbCompletedByISMP.Checked = False
                End If

                If Mid(ConfidentialData, 24, 1) = "1" Then
                    chbComplianceManager.Checked = True
                Else
                    chbComplianceManager.Checked = False
                End If
                If Mid(ConfidentialData, 25, 1) = "1" Then
                    chbCC.Checked = True
                Else
                    chbCC.Checked = False
                End If
                Select Case Mid(ConfidentialData, 2, 1)
                    Case "A" 'One Stack 2 Run
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            chbOneStackMaxOpCapacity.Checked = True
                        Else
                            chbOneStackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            chbOneStackOpCapacity.Checked = True
                        Else
                            chbOneStackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            chbOneStackAllowEmiss1.Checked = True
                        Else
                            chbOneStackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            chbOneStackAllowEmiss2.Checked = True
                        Else
                            chbOneStackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            chbOneStackAllowEmiss3.Checked = True
                        Else
                            chbOneStackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            chbOneStackAppRequire.Checked = True
                        Else
                            chbOneStackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            chbOneStackControlEquip.Checked = True
                        Else
                            chbOneStackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbOneStackPercentAllow.Checked = True
                        Else
                            chbOneStackPercentAllow.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbOneStackOtherInfo.Checked = True
                        Else
                            chbOneStackOtherInfo.Checked = False
                        End If

                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbOneStack2Run1.Checked = True
                        Else
                            chbOneStack2Run1.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbOneStack2Temp1.Checked = True
                        Else
                            chbOneStack2Temp1.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbOneStack2Moist1.Checked = True
                        Else
                            chbOneStack2Moist1.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbOneStack2ACFM1.Checked = True
                        Else
                            chbOneStack2ACFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbOneStack2DSCFM1.Checked = True
                        Else
                            chbOneStack2DSCFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbOneStack2Poll1.Checked = True
                        Else
                            chbOneStack2Poll1.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbOneStack2Emiss1.Checked = True
                        Else
                            chbOneStack2Emiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbOneStack2Run2.Checked = True
                        Else
                            chbOneStack2Run2.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbOneStack2Temp2.Checked = True
                        Else
                            chbOneStack2Temp2.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            chbOneStack2Moist2.Checked = True
                        Else
                            chbOneStack2Moist2.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbOneStack2ACFM2.Checked = True
                        Else
                            chbOneStack2ACFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbOneStack2DSCFM2.Checked = True
                        Else
                            chbOneStack2DSCFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbOneStack2Poll2.Checked = True
                        Else
                            chbOneStack2Poll2.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbOneStack2Emiss2.Checked = True
                        Else
                            chbOneStack2Emiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbOneStack2PollUnit.Checked = True
                        Else
                            chbOneStack2PollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbOneStack2PollAvg.Checked = True
                        Else
                            chbOneStack2PollAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbOneStack2EmissUnit.Checked = True
                        Else
                            chbOneStack2EmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbOneStack2EmissAvg.Checked = True
                        Else
                            chbOneStack2EmissAvg.Checked = False
                        End If

                    Case "B" 'One Stack 3 Run
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            chbOneStackMaxOpCapacity.Checked = True
                        Else
                            chbOneStackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            chbOneStackOpCapacity.Checked = True
                        Else
                            chbOneStackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            chbOneStackAllowEmiss1.Checked = True
                        Else
                            chbOneStackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            chbOneStackAllowEmiss2.Checked = True
                        Else
                            chbOneStackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            chbOneStackAllowEmiss3.Checked = True
                        Else
                            chbOneStackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            chbOneStackAppRequire.Checked = True
                        Else
                            chbOneStackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            chbOneStackControlEquip.Checked = True
                        Else
                            chbOneStackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbOneStackPercentAllow.Checked = True
                        Else
                            chbOneStackPercentAllow.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbOneStackOtherInfo.Checked = True
                        Else
                            chbOneStackOtherInfo.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbOneStack3Run1.Checked = True
                        Else
                            chbOneStack3Run1.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbOneStack3Temp1.Checked = True
                        Else
                            chbOneStack3Temp1.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbOneStack3Moist1.Checked = True
                        Else
                            chbOneStack3Moist1.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbOneStack3ACFM1.Checked = True
                        Else
                            chbOneStack3ACFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbOneStack3DSCFM1.Checked = True
                        Else
                            chbOneStack3DSCFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbOneStack3Poll1.Checked = True
                        Else
                            chbOneStack3Poll1.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbOneStack3Emiss1.Checked = True
                        Else
                            chbOneStack3Emiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbOneStack3Run2.Checked = True
                        Else
                            chbOneStack3Run2.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbOneStack3Temp2.Checked = True
                        Else
                            chbOneStack3Temp2.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbOneStack3Moist2.Checked = True
                        Else
                            chbOneStack3Moist2.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbOneStack3ACFM2.Checked = True
                        Else
                            chbOneStack3ACFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbOneStack3DSCFM2.Checked = True
                        Else
                            chbOneStack3DSCFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbOneStack3Poll2.Checked = True
                        Else
                            chbOneStack3Poll2.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbOneStack3Emiss2.Checked = True
                        Else
                            chbOneStack3Emiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbOneStack3Run3.Checked = True
                        Else
                            chbOneStack3Run3.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbOneStack3Temp3.Checked = True
                        Else
                            chbOneStack3Temp3.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbOneStack3Moist3.Checked = True
                        Else
                            chbOneStack3Moist3.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbOneStack3ACFM3.Checked = True
                        Else
                            chbOneStack3ACFM3.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbOneStack3DSCFM3.Checked = True
                        Else
                            chbOneStack3DSCFM3.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbOneStack3Poll3.Checked = True
                        Else
                            chbOneStack3Poll3.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbOneStack3Emiss3.Checked = True
                        Else
                            chbOneStack3Emiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbOneStack3PollUnit.Checked = True
                        Else
                            chbOneStack3PollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 57, 1) = "1" Then
                            Me.chbOneStack3PollAvg.Checked = True
                        Else
                            chbOneStack3PollAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 58, 1) = "1" Then
                            Me.chbOneStack3EmissUnit.Checked = True
                        Else
                            chbOneStack3EmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 59, 1) = "1" Then
                            Me.chbOneStack3EmissAvg.Checked = True
                        Else
                            chbOneStack3EmissAvg.Checked = False
                        End If

                    Case "C" 'One Stack 4 Run
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            chbOneStackMaxOpCapacity.Checked = True
                        Else
                            chbOneStackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            chbOneStackOpCapacity.Checked = True
                        Else
                            chbOneStackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            chbOneStackAllowEmiss1.Checked = True
                        Else
                            chbOneStackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            chbOneStackAllowEmiss2.Checked = True
                        Else
                            chbOneStackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            chbOneStackAllowEmiss3.Checked = True
                        Else
                            chbOneStackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            chbOneStackAppRequire.Checked = True
                        Else
                            chbOneStackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            chbOneStackControlEquip.Checked = True
                        Else
                            chbOneStackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbOneStackPercentAllow.Checked = True
                        Else
                            chbOneStackPercentAllow.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbOneStackOtherInfo.Checked = True
                        Else
                            chbOneStackOtherInfo.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbOneStack4Run1.Checked = True
                        Else
                            chbOneStack4Run1.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbOneStack4Temp1.Checked = True
                        Else
                            chbOneStack4Temp1.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbOneStack4Moist1.Checked = True
                        Else
                            chbOneStack4Moist1.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbOneStack4ACFM1.Checked = True
                        Else
                            chbOneStack4ACFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbOneStack4DSCFM1.Checked = True
                        Else
                            chbOneStack4DSCFM1.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbOneStack4Poll1.Checked = True
                        Else
                            chbOneStack4Poll1.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbOneStack4Emiss1.Checked = True
                        Else
                            chbOneStack4Emiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbOneStack4Run2.Checked = True
                        Else
                            chbOneStack4Run2.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbOneStack4Temp2.Checked = True
                        Else
                            chbOneStack4Temp2.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbOneStack4Moist2.Checked = True
                        Else
                            chbOneStack4Moist2.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbOneStack4ACFM2.Checked = True
                        Else
                            chbOneStack4ACFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbOneStack4DSCFM2.Checked = True
                        Else
                            chbOneStack4DSCFM2.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbOneStack4Poll2.Checked = True
                        Else
                            chbOneStack4Poll2.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbOneStack4Emiss2.Checked = True
                        Else
                            chbOneStack4Emiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbOneStack4Run3.Checked = True
                        Else
                            chbOneStack4Run3.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbOneStack4Temp3.Checked = True
                        Else
                            chbOneStack4Temp3.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbOneStack4Moist3.Checked = True
                        Else
                            chbOneStack4Moist3.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbOneStack4ACFM3.Checked = True
                        Else
                            chbOneStack4ACFM3.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbOneStack4DSCFM3.Checked = True
                        Else
                            chbOneStack4DSCFM3.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbOneStack4Poll3.Checked = True
                        Else
                            chbOneStack4Poll3.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbOneStack4Emiss3.Checked = True
                        Else
                            chbOneStack4Emiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbOneStack4Run4.Checked = True
                        Else
                            chbOneStack4Run4.Checked = False
                        End If
                        If Mid(ConfidentialData, 57, 1) = "1" Then
                            Me.chbOneStack4Temp4.Checked = True
                        Else
                            chbOneStack4Temp4.Checked = False
                        End If
                        If Mid(ConfidentialData, 58, 1) = "1" Then
                            Me.chbOneStack4Moist4.Checked = True
                        Else
                            chbOneStack4Moist4.Checked = False
                        End If
                        If Mid(ConfidentialData, 59, 1) = "1" Then
                            Me.chbOneStack4ACFM4.Checked = True
                        Else
                            chbOneStack4ACFM4.Checked = False
                        End If
                        If Mid(ConfidentialData, 60, 1) = "1" Then
                            Me.chbOneStack4DSCFM4.Checked = True
                        Else
                            chbOneStack4DSCFM4.Checked = False
                        End If
                        If Mid(ConfidentialData, 61, 1) = "1" Then
                            Me.chbOneStack4Poll4.Checked = True
                        Else
                            chbOneStack4Poll4.Checked = False
                        End If
                        If Mid(ConfidentialData, 62, 1) = "1" Then
                            Me.chbOneStack4Emiss4.Checked = True
                        Else
                            chbOneStack4Emiss4.Checked = False
                        End If
                        If Mid(ConfidentialData, 63, 1) = "1" Then
                            Me.chbOneStack4PollUnit.Checked = True
                        Else
                            chbOneStack4PollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 64, 1) = "1" Then
                            Me.chbOneStack4PollAvg.Checked = True
                        Else
                            chbOneStack4PollAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 65, 1) = "1" Then
                            Me.chbOneStack4EmissUnit.Checked = True
                        Else
                            chbOneStack4EmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 66, 1) = "1" Then
                            Me.chbOneStack4EmissAvg.Checked = True
                        Else
                            chbOneStack4EmissAvg.Checked = False
                        End If
                    Case "D" 'Two Stack Standard
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbTwoStackMaxOpCapacity.Checked = True
                        Else
                            chbTwoStackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbTwoStackOpCapacity.Checked = True
                        Else
                            chbTwoStackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss1.Checked = True
                        Else
                            chbTwoStackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss2.Checked = True
                        Else
                            chbTwoStackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss3.Checked = True
                        Else
                            chbTwoStackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbTwoStackAppRequire.Checked = True
                        Else
                            chbTwoStackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbTwoStackControlEquip.Checked = True
                        Else
                            chbTwoStackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbTwoStackOtherInfo.Checked = True
                        Else
                            chbTwoStackOtherInfo.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbTwoStackStandName1.Checked = True
                        Else
                            chbTwoStackStandName1.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbTwoStackStandName2.Checked = True
                        Else
                            chbTwoStackStandName2.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbTwoStackStandRun1a.Checked = True
                        Else
                            chbTwoStackStandRun1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbTwoStackStandTemp1a.Checked = True
                        Else
                            chbTwoStackStandTemp1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbTwoStackStandMoist1a.Checked = True
                        Else
                            chbTwoStackStandMoist1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbTwoStackStandACFM1a.Checked = True
                        Else
                            chbTwoStackStandACFM1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM1a.Checked = True
                        Else
                            chbTwoStackStandDSCFM1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbTwoStackStandPoll1a.Checked = True
                        Else
                            chbTwoStackStandPoll1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbTwoStackStandEmiss1a.Checked = True
                        Else
                            chbTwoStackStandEmiss1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbTwoStackStandRun2a.Checked = True
                        Else
                            chbTwoStackStandRun2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbTwoStackStandTemp2a.Checked = True
                        Else
                            chbTwoStackStandTemp2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbTwoStackStandMoist2a.Checked = True
                        Else
                            chbTwoStackStandMoist2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbTwoStackStandACFM2a.Checked = True
                        Else
                            chbTwoStackStandACFM2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM2a.Checked = True
                        Else
                            chbTwoStackStandDSCFM2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbTwoStackStandPoll2a.Checked = True
                        Else
                            chbTwoStackStandPoll2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbTwoStackStandEmiss2a.Checked = True
                        Else
                            chbTwoStackStandEmiss2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbTwoStackStandRun3a.Checked = True
                        Else
                            chbTwoStackStandRun3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbTwoStackStandTemp3a.Checked = True
                        Else
                            chbTwoStackStandTemp3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbTwoStackStandMoist3a.Checked = True
                        Else
                            chbTwoStackStandMoist3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbTwoStackStandACFM3a.Checked = True
                        Else
                            chbTwoStackStandACFM3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM3a.Checked = True
                        Else
                            chbTwoStackStandDSCFM3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbTwoStackStandPoll3a.Checked = True
                        Else
                            chbTwoStackStandPoll3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbTwoStackStandEmiss3a.Checked = True
                        Else
                            chbTwoStackStandEmiss3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 57, 1) = "1" Then
                            Me.chbTwoStackStandRun1b.Checked = True
                        Else
                            chbTwoStackStandRun1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 58, 1) = "1" Then
                            Me.chbTwoStackStandTemp1b.Checked = True
                        Else
                            chbTwoStackStandTemp1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 59, 1) = "1" Then
                            Me.chbTwoStackStandMoist1b.Checked = True
                        Else
                            chbTwoStackStandMoist1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 60, 1) = "1" Then
                            Me.chbTwoStackStandACFM1b.Checked = True
                        Else
                            chbTwoStackStandACFM1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 61, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM1b.Checked = True
                        Else
                            chbTwoStackStandDSCFM1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 62, 1) = "1" Then
                            Me.chbTwoStackStandPoll1b.Checked = True
                        Else
                            chbTwoStackStandPoll1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 63, 1) = "1" Then
                            Me.chbTwoStackStandEmiss1b.Checked = True
                        Else
                            chbTwoStackStandEmiss1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 64, 1) = "1" Then
                            Me.chbTwoStackStandRun2b.Checked = True
                        Else
                            chbTwoStackStandRun2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 65, 1) = "1" Then
                            Me.chbTwoStackStandTemp2b.Checked = True
                        Else
                            chbTwoStackStandTemp2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 66, 1) = "1" Then
                            Me.chbTwoStackStandMoist2b.Checked = True
                        Else
                            chbTwoStackStandMoist2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 67, 1) = "1" Then
                            Me.chbTwoStackStandACFM2b.Checked = True
                        Else
                            chbTwoStackStandACFM2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 68, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM2b.Checked = True
                        Else
                            chbTwoStackStandDSCFM2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 69, 1) = "1" Then
                            Me.chbTwoStackStandPoll2b.Checked = True
                        Else
                            chbTwoStackStandPoll2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 70, 1) = "1" Then
                            Me.chbTwoStackStandEmiss2b.Checked = True
                        Else
                            chbTwoStackStandEmiss2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 71, 1) = "1" Then
                            Me.chbTwoStackStandRun3b.Checked = True
                        Else
                            chbTwoStackStandRun3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 72, 1) = "1" Then
                            Me.chbTwoStackStandTemp3b.Checked = True
                        Else
                            chbTwoStackStandTemp3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 73, 1) = "1" Then
                            Me.chbTwoStackStandMoist3b.Checked = True
                        Else
                            chbTwoStackStandMoist3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 74, 1) = "1" Then
                            Me.chbTwoStackStandACFM3b.Checked = True
                        Else
                            chbTwoStackStandACFM3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 75, 1) = "1" Then
                            Me.chbTwoStackStandDSCFM3b.Checked = True
                        Else
                            chbTwoStackStandDSCFM3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 76, 1) = "1" Then
                            Me.chbTwoStackStandPoll3b.Checked = True
                        Else
                            chbTwoStackStandPoll3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 77, 1) = "1" Then
                            Me.chbTwoStackStandEmiss3b.Checked = True
                        Else
                            chbTwoStackStandEmiss3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 78, 1) = "1" Then
                            Me.chbTwoStackStandPollUnit.Checked = True
                        Else
                            chbTwoStackStandPollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 79, 1) = "1" Then
                            Me.chbTwoStackStandPollAvg1.Checked = True
                        Else
                            chbTwoStackStandPollAvg1.Checked = False
                        End If
                        If Mid(ConfidentialData, 80, 1) = "1" Then
                            Me.chbTwoStackStandPollAvg2.Checked = True
                        Else
                            chbTwoStackStandPollAvg2.Checked = False
                        End If
                        If Mid(ConfidentialData, 81, 1) = "1" Then
                            Me.chbTwoStackStandEmissUnit.Checked = True
                        Else
                            chbTwoStackStandEmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 82, 1) = "1" Then
                            Me.chbTwoStackStandEmissAvg1.Checked = True
                        Else
                            chbTwoStackStandEmissAvg1.Checked = False
                        End If
                        If Mid(ConfidentialData, 83, 1) = "1" Then
                            Me.chbTwoStackStandEmissAvg2.Checked = True
                        Else
                            chbTwoStackStandEmissAvg2.Checked = False
                        End If
                        If Mid(ConfidentialData, 84, 1) = "1" Then
                            Me.chbTwoStackStandTotal1.Checked = True
                        Else
                            chbTwoStackStandTotal1.Checked = False
                        End If
                        If Mid(ConfidentialData, 85, 1) = "1" Then
                            Me.chbTwoStackStandTotal2.Checked = True
                        Else
                            chbTwoStackStandTotal2.Checked = False
                        End If
                        If Mid(ConfidentialData, 86, 1) = "1" Then
                            Me.chbTwoStackStandTotal3.Checked = True
                        Else
                            chbTwoStackStandTotal3.Checked = False
                        End If
                        If Mid(ConfidentialData, 87, 1) = "1" Then
                            Me.chbTwoStackStandTotalAvg.Checked = True
                        Else
                            chbTwoStackStandTotalAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 88, 1) = "1" Then
                            Me.chbTwoStackStandPercentAllow.Checked = True
                        Else
                            chbTwoStackStandPercentAllow.Checked = False
                        End If

                    Case "E"  'Two Stack DRE
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbTwoStackMaxOpCapacity.Checked = True
                        Else
                            chbTwoStackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbTwoStackOpCapacity.Checked = True
                        Else
                            chbTwoStackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss1.Checked = True
                        Else
                            chbTwoStackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss2.Checked = True
                        Else
                            chbTwoStackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbTwoStackAllowEmiss3.Checked = True
                        Else
                            chbTwoStackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbTwoStackAppRequire.Checked = True
                        Else
                            chbTwoStackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbTwoStackControlEquip.Checked = True
                        Else
                            chbTwoStackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbTwoStackOtherInfo.Checked = True
                        Else
                            chbTwoStackOtherInfo.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbTwoStackDREName1.Checked = True
                        Else
                            chbTwoStackDREName1.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbTwoStackDREName2.Checked = True
                        Else
                            chbTwoStackDREName2.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbTwoStackDRERun1a.Checked = True
                        Else
                            chbTwoStackDRERun1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbTwoStackDRETemp1a.Checked = True
                        Else
                            chbTwoStackDRETemp1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbTwoStackDREMoist1a.Checked = True
                        Else
                            chbTwoStackDREMoist1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbTwoStackDREACFM1a.Checked = True
                        Else
                            chbTwoStackDREACFM1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM1a.Checked = True
                        Else
                            chbTwoStackDREDSCFM1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbTwoStackDREPoll1a.Checked = True
                        Else
                            chbTwoStackDREPoll1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbTwoStackDREEmiss1a.Checked = True
                        Else
                            chbTwoStackDREEmiss1a.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbTwoStackDRERun2a.Checked = True
                        Else
                            chbTwoStackDRERun2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbTwoStackDRETemp2a.Checked = True
                        Else
                            chbTwoStackDRETemp2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbTwoStackDREMoist2a.Checked = True
                        Else
                            chbTwoStackDREMoist2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbTwoStackDREACFM2a.Checked = True
                        Else
                            chbTwoStackDREACFM2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM2a.Checked = True
                        Else
                            chbTwoStackDREDSCFM2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbTwoStackDREPoll2a.Checked = True
                        Else
                            chbTwoStackDREPoll2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbTwoStackDREEmiss2a.Checked = True
                        Else
                            chbTwoStackDREEmiss2a.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbTwoStackDRERun3a.Checked = True
                        Else
                            chbTwoStackDRERun3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbTwoStackDRETEmp3a.Checked = True
                        Else
                            chbTwoStackDRETEmp3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbTwoStackDREMoist3a.Checked = True
                        Else
                            chbTwoStackDREMoist3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbTwoStackDREACFM3a.Checked = True
                        Else
                            chbTwoStackDREACFM3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM3a.Checked = True
                        Else
                            chbTwoStackDREDSCFM3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbTwoStackDREPoll3a.Checked = True
                        Else
                            chbTwoStackDREPoll3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbTwoStackDREEmiss3a.Checked = True
                        Else
                            chbTwoStackDREEmiss3a.Checked = False
                        End If
                        If Mid(ConfidentialData, 57, 1) = "1" Then
                            Me.chbTwoStackDRERun1b.Checked = True
                        Else
                            chbTwoStackDRERun1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 58, 1) = "1" Then
                            Me.chbTwoStackDRETemp1b.Checked = True
                        Else
                            chbTwoStackDRETemp1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 59, 1) = "1" Then
                            Me.chbTwoStackDREMoist1b.Checked = True
                        Else
                            chbTwoStackDREMoist1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 60, 1) = "1" Then
                            Me.chbTwoStackDREACFM1b.Checked = True
                        Else
                            chbTwoStackDREACFM1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 61, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM1b.Checked = True
                        Else
                            chbTwoStackDREDSCFM1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 62, 1) = "1" Then
                            Me.chbTwoStackDREPoll1b.Checked = True
                        Else
                            chbTwoStackDREPoll1b.Checked = False
                        End If
                        If Mid(ConfidentialData, 63, 1) = "1" Then
                            Me.chbTwoStackDREEmiss1b.Checked = True
                        Else
                            chbTwoStackDREEmiss1b.Checked = False
                        End If

                        If Mid(ConfidentialData, 64, 1) = "1" Then
                            Me.chbTwoStackDRERun2b.Checked = True
                        Else
                            chbTwoStackDRERun2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 65, 1) = "1" Then
                            Me.chbTwoStackDRETemp2b.Checked = True
                        Else
                            chbTwoStackDRETemp2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 66, 1) = "1" Then
                            Me.chbTwoStackDREMoist2b.Checked = True
                        Else
                            chbTwoStackDREMoist2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 67, 1) = "1" Then
                            Me.chbTwoStackDREACFM2b.Checked = True
                        Else
                            chbTwoStackDREACFM2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 68, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM2b.Checked = True
                        Else
                            chbTwoStackDREDSCFM2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 69, 1) = "1" Then
                            Me.chbTwoStackDREPoll2b.Checked = True
                        Else
                            chbTwoStackDREPoll2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 70, 1) = "1" Then
                            Me.chbTwoStackDREEmiss2b.Checked = True
                        Else
                            chbTwoStackDREEmiss2b.Checked = False
                        End If
                        If Mid(ConfidentialData, 71, 1) = "1" Then
                            Me.chbTwoStackDRERun3b.Checked = True
                        Else
                            chbTwoStackDRERun3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 72, 1) = "1" Then
                            Me.chbTwoStackDRETemp3b.Checked = True
                        Else
                            chbTwoStackDRETemp3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 73, 1) = "1" Then
                            Me.chbTwoStackDREMoist3b.Checked = True
                        Else
                            chbTwoStackDREMoist3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 74, 1) = "1" Then
                            Me.chbTwoStackDREACFM3b.Checked = True
                        Else
                            chbTwoStackDREACFM3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 75, 1) = "1" Then
                            Me.chbTwoStackDREDSCFM3b.Checked = True
                        Else
                            chbTwoStackDREDSCFM3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 76, 1) = "1" Then
                            Me.chbTwoStackDREPoll3b.Checked = True
                        Else
                            chbTwoStackDREPoll3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 77, 1) = "1" Then
                            Me.chbTwoStackDREEmiss3b.Checked = True
                        Else
                            chbTwoStackDREEmiss3b.Checked = False
                        End If
                        If Mid(ConfidentialData, 78, 1) = "1" Then
                            Me.chbTwoStackDREPollUnit.Checked = True
                        Else
                            chbTwoStackDREPollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 79, 1) = "1" Then
                            Me.chbTwoStackDREPollAvg1.Checked = True
                        Else
                            chbTwoStackDREPollAvg1.Checked = False
                        End If
                        If Mid(ConfidentialData, 80, 1) = "1" Then
                            Me.chbTwoStackDREPollAvg2.Checked = True
                        Else
                            chbTwoStackDREPollAvg2.Checked = False
                        End If
                        If Mid(ConfidentialData, 81, 1) = "1" Then
                            Me.chbTwoStackDREEmissUnit.Checked = True
                        Else
                            chbTwoStackDREEmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 82, 1) = "1" Then
                            Me.chbTwoStackDREEmissAvg1.Checked = True
                        Else
                            chbTwoStackDREEmissAvg1.Checked = False
                        End If
                        If Mid(ConfidentialData, 83, 1) = "1" Then
                            Me.chbTwoStackDREEmissAvg2.Checked = True
                        Else
                            chbTwoStackDREEmissAvg2.Checked = False
                        End If
                        If Mid(ConfidentialData, 84, 1) = "1" Then
                            Me.chbTwoStackDREDestructionEff.Checked = True
                        Else
                            chbTwoStackDREDestructionEff.Checked = False
                        End If

                    Case "F"  'Loading Rack
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbLoadingRackMaxOpCapacity.Checked = True
                        Else
                            chbLoadingRackMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbLoadingRackOpCapacity.Checked = True
                        Else
                            chbLoadingRackOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbLoadingRackAllowEmiss1.Checked = True
                        Else
                            chbLoadingRackAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbLoadingRackAllowEmiss2.Checked = True
                        Else
                            chbLoadingRackAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbLoadingRackAllowEmiss3.Checked = True
                        Else
                            chbLoadingRackAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbLoadingRackAppRequire.Checked = True
                        Else
                            chbLoadingRackAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbLoadingRackControlEquip.Checked = True
                        Else
                            chbLoadingRackControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbLoadingRackTestDuration.Checked = True
                        Else
                            chbLoadingRackTestDuration.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbLoadingRackPollIN.Checked = True
                        Else
                            chbLoadingRackPollIN.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbLoadingRackPollOUT.Checked = True
                        Else
                            chbLoadingRackPollOUT.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbLoadingRackDestReduction.Checked = True
                        Else
                            chbLoadingRackDestReduction.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbLoadingRackEmiss.Checked = True
                        Else
                            chbLoadingRackEmiss.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbLoadingRackOtherInfo.Checked = True
                        Else
                            chbLoadingRackOtherInfo.Checked = False
                        End If
                    Case "G"  'Pulp
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbPulpMaxOpCapacity.Checked = True
                        Else
                            chbPulpMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbPulpOpCapacity.Checked = True
                        Else
                            chbPulpOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbPulpAllowEmiss1.Checked = True
                        Else
                            chbPulpAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbPulpAllowEmiss2.Checked = True
                        Else
                            chbPulpAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbPulpAllowEmiss3.Checked = True
                        Else
                            chbPulpAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbPulpAppRequire.Checked = True
                        Else
                            chbPulpAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbPulpControlEquip.Checked = True
                        Else
                            chbPulpControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbPulpRun1.Checked = True
                        Else
                            chbPulpRun1.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbPulpConc1.Checked = True
                        Else
                            chbPulpConc1.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbPulpTreatment1.Checked = True
                        Else
                            chbPulpTreatment1.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbPulpRun2.Checked = True
                        Else
                            chbPulpRun2.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbPulpConc2.Checked = True
                        Else
                            chbPulpConc2.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbPulpTreatment2.Checked = True
                        Else
                            chbPulpTreatment2.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbPulpRun3.Checked = True
                        Else
                            chbPulpRun3.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbPulpConc3.Checked = True
                        Else
                            chbPulpConc3.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbPulpTreatment3.Checked = True
                        Else
                            chbPulpTreatment3.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbPulpConcUnit.Checked = True
                        Else
                            chbPulpConcUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbPulpConcAvg.Checked = True
                        Else
                            chbPulpConcAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbPulpTreatmentUnit.Checked = True
                        Else
                            chbPulpTreatmentUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbPulpTreatmentAvg.Checked = True
                        Else
                            chbPulpTreatmentAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbPulpDestructEffic.Checked = True
                        Else
                            chbPulpDestructEffic.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbPulpOtherInfo.Checked = True
                        Else
                            chbPulpOtherInfo.Checked = False
                        End If

                    Case "H" 'Gas
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbGasMaxOpCapacity.Checked = True
                        Else
                            chbGasMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbGasOpCapacity.Checked = True
                        Else
                            chbGasOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbGasAllowEmiss1.Checked = True
                        Else
                            chbGasAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbGasAllowEmiss2.Checked = True
                        Else
                            chbGasAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbGasAllowEmiss3.Checked = True
                        Else
                            chbGasAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbGasAppRequire.Checked = True
                        Else
                            chbGasAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbGasControlEquip.Checked = True
                        Else
                            chbGasControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbGasRun1.Checked = True
                        Else
                            chbGasRun1.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbGasPoll1.Checked = True
                        Else
                            chbGasPoll1.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbGasEmiss1.Checked = True
                        Else
                            chbGasEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbGasRun2.Checked = True
                        Else
                            chbGasRun2.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbGasPoll2.Checked = True
                        Else
                            chbGasPoll2.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbGasEmiss2.Checked = True
                        Else
                            chbGasEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbGasRun3.Checked = True
                        Else
                            chbGasRun3.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbGasPoll3.Checked = True
                        Else
                            chbGasPoll3.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbGasEmiss3.Checked = True
                        Else
                            chbGasEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbGasPollUnit.Checked = True
                        Else
                            chbGasPollUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbGasPollAvg.Checked = True
                        Else
                            chbGasPollAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbGasEmissUnit.Checked = True
                        Else
                            chbGasEmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            chbGasEmissAvg.Checked = True
                        Else
                            chbGasEmissAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbGasPercentAllow.Checked = True
                        Else
                            chbGasPercentAllow.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbGasOtherInfo.Checked = True
                        Else
                            chbGasOtherInfo.Checked = False
                        End If
                    Case "I"  'Flare
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbFlareMaxOpCapacity.Checked = True
                        Else
                            chbFlareMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbFlareOpCapacity.Checked = True
                        Else
                            chbFlareOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbFlareAllowLimitations.Checked = True
                        Else
                            chbFlareAllowLimitations.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbFlareHeatContent.Checked = True
                        Else
                            chbFlareHeatContent.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbFlareAppRequire.Checked = True
                        Else
                            chbFlareAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbFlareMonitor.Checked = True
                        Else
                            chbFlareMonitor.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbFlareRun1.Checked = True
                        Else
                            chbFlareRun1.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbFlareHeating1.Checked = True
                        Else
                            chbFlareHeating1.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbFlareVelocity1.Checked = True
                        Else
                            chbFlareVelocity1.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbFlareRun2.Checked = True
                        Else
                            chbFlareRun2.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbFlareHeating2.Checked = True
                        Else
                            chbFlareHeating2.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbFlareVelocity2.Checked = True
                        Else
                            chbFlareVelocity2.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbFlareRun3.Checked = True
                        Else
                            chbFlareRun3.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbFlareHeating3.Checked = True
                        Else
                            chbFlareHeating3.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbFlareVelocity3.Checked = True
                        Else
                            chbFlareVelocity3.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbFlareHeatingUnit.Checked = True
                        Else
                            chbFlareHeatingUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbFlareHeatingAvg.Checked = True
                        Else
                            chbFlareHeatingAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbFlareVelocityUnit.Checked = True
                        Else
                            chbFlareVelocityUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbFlareVelocityAvg.Checked = True
                        Else
                            chbFlareVelocityAvg.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbFlarePercentAllow.Checked = True
                        Else
                            chbFlarePercentAllow.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbFlareOtherInfo.Checked = True
                        Else
                            chbFlareOtherInfo.Checked = False
                        End If
                    Case "J"  'RATA
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbRATAAppStandard.Checked = True
                        Else
                            chbRATAAppStandard.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbRATAAppRegulation.Checked = True
                        Else
                            chbRATAAppRegulation.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbRATADiluent.Checked = True
                        Else
                            chbRATADiluent.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbRATARef1.Checked = True
                        Else
                            chbRATARef1.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbRATARef2.Checked = True
                        Else
                            chbRATARef2.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbRATARef3.Checked = True
                        Else
                            chbRATARef3.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbRATARef4.Checked = True
                        Else
                            chbRATARef4.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbRATARef5.Checked = True
                        Else
                            chbRATARef5.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbRATARef6.Checked = True
                        Else
                            chbRATARef6.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbRATARef7.Checked = True
                        Else
                            chbRATARef7.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbRATARef8.Checked = True
                        Else
                            chbRATARef8.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbRATARef9.Checked = True
                        Else
                            chbRATARef9.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbRATARef10.Checked = True
                        Else
                            chbRATARef10.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbRATARef11.Checked = True
                        Else
                            chbRATARef11.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbRATARef12.Checked = True
                        Else
                            chbRATARef12.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbRATACMS1.Checked = True
                        Else
                            chbRATACMS1.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbRATACMS2.Checked = True
                        Else
                            chbRATACMS2.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbRATACMS3.Checked = True
                        Else
                            chbRATACMS3.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            Me.chbRATACMS4.Checked = True
                        Else
                            chbRATACMS4.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbRATACMS5.Checked = True
                        Else
                            chbRATACMS5.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbRATACMS6.Checked = True
                        Else
                            chbRATACMS6.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbRATACMS7.Checked = True
                        Else
                            chbRATACMS7.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbRATACMS8.Checked = True
                        Else
                            chbRATACMS8.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbRATACMS9.Checked = True
                        Else
                            chbRATACMS9.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbRATACMS10.Checked = True
                        Else
                            chbRATACMS10.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbRATACMS11.Checked = True
                        Else
                            chbRATACMS11.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbRATACMS12.Checked = True
                        Else
                            chbRATACMS12.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbRATAUnits.Checked = True
                        Else
                            chbRATAUnits.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbRATARelativeAcc.Checked = True
                        Else
                            chbRATARelativeAcc.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbRATAStatement.Checked = True
                        Else
                            chbRATAStatement.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbRATAOtherInformation.Checked = True
                        Else
                            chbRATAOtherInformation.Checked = False
                        End If

                    Case "K" 'Memorandum
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMemoAppRequire.Checked = True
                        Else
                            chbMemoAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMemoStandardMemo.Checked = True
                        Else
                            chbMemoStandardMemo.Checked = False
                        End If

                    Case "L" 'Memo to File
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMemoAppRequire.Checked = True
                        Else
                            chbMemoAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMemoToFileManufacture.Checked = True
                        Else
                            chbMemoToFileManufacture.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbMemoToFileSerial.Checked = True
                        Else
                            chbMemoToFileSerial.Checked = False
                        End If

                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbMemoToFileMemo.Checked = True
                        Else
                            chbMemoToFileMemo.Checked = False
                        End If

                    Case "M" 'Method 9 Multi.
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacity1.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacity1.Checked = False
                        End If

                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacity2.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacity2.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacity3.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacity3.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacity4.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacity4.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacity5.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacity5.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbMethod9MultiMaxOpCapacityUnit.Checked = True
                        Else
                            chbMethod9MultiMaxOpCapacityUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacity1.Checked = True
                        Else
                            chbMethod9MultiOpCapacity1.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacity2.Checked = True
                        Else
                            chbMethod9MultiOpCapacity2.Checked = False
                        End If
                        If Mid(ConfidentialData, 34, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacity3.Checked = True
                        Else
                            chbMethod9MultiOpCapacity3.Checked = False
                        End If
                        If Mid(ConfidentialData, 35, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacity4.Checked = True
                        Else
                            chbMethod9MultiOpCapacity4.Checked = False
                        End If
                        If Mid(ConfidentialData, 36, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacity5.Checked = True
                        Else
                            chbMethod9MultiOpCapacity5.Checked = False
                        End If
                        If Mid(ConfidentialData, 37, 1) = "1" Then
                            Me.chbMethod9MultiOpCapacityUnit.Checked = True
                        Else
                            chbMethod9MultiOpCapacityUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 38, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmiss1.Checked = True
                        Else
                            chbMethod9MultiAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 39, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmiss2.Checked = True
                        Else
                            chbMethod9MultiAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 40, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmiss3.Checked = True
                        Else
                            chbMethod9MultiAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 41, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmiss4.Checked = True
                        Else
                            chbMethod9MultiAllowEmiss4.Checked = False
                        End If
                        If Mid(ConfidentialData, 42, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmiss5.Checked = True
                        Else
                            chbMethod9MultiAllowEmiss5.Checked = False
                        End If
                        If Mid(ConfidentialData, 43, 1) = "1" Then
                            Me.chbMethod9MultiAllowEmissUnit.Checked = True
                        Else
                            chbMethod9MultiAllowEmissUnit.Checked = False
                        End If
                        If Mid(ConfidentialData, 44, 1) = "1" Then
                            chbMethod9MultiAppRequire.Checked = True
                        Else
                            chbMethod9MultiAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 45, 1) = "1" Then
                            Me.chbMethod9MultiControlEquip.Checked = True
                        Else
                            chbMethod9MultiControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 46, 1) = "1" Then
                            Me.chbMethod9MultiAvg1.Checked = True
                        Else
                            chbMethod9MultiAvg1.Checked = False
                        End If
                        If Mid(ConfidentialData, 47, 1) = "1" Then
                            Me.chbMethod9MultiAvg2.Checked = True
                        Else
                            chbMethod9MultiAvg2.Checked = False
                        End If
                        If Mid(ConfidentialData, 48, 1) = "1" Then
                            Me.chbMethod9MultiAvg3.Checked = True
                        Else
                            chbMethod9MultiAvg3.Checked = False
                        End If
                        If Mid(ConfidentialData, 49, 1) = "1" Then
                            Me.chbMethod9MultiAvg4.Checked = True
                        Else
                            chbMethod9MultiAvg4.Checked = False
                        End If
                        If Mid(ConfidentialData, 50, 1) = "1" Then
                            Me.chbMethod9MultiAvg5.Checked = True
                        Else
                            chbMethod9MultiAvg5.Checked = False
                        End If
                        If Mid(ConfidentialData, 51, 1) = "1" Then
                            Me.chbMethod9MultiOtherInfor.Checked = True
                        Else
                            chbMethod9MultiOtherInfor.Checked = False
                        End If
                        If Mid(ConfidentialData, 52, 1) = "1" Then
                            Me.chbMethod9MultiEquip1.Checked = True
                        Else
                            chbMethod9MultiEquip1.Checked = False
                        End If
                        If Mid(ConfidentialData, 53, 1) = "1" Then
                            Me.chbMethod9MultiEquip2.Checked = True
                        Else
                            chbMethod9MultiEquip2.Checked = False
                        End If
                        If Mid(ConfidentialData, 54, 1) = "1" Then
                            Me.chbMethod9MultiEquip3.Checked = True
                        Else
                            chbMethod9MultiEquip3.Checked = False
                        End If
                        If Mid(ConfidentialData, 55, 1) = "1" Then
                            Me.chbMethod9MultiEquip4.Checked = True
                        Else
                            chbMethod9MultiEquip4.Checked = False
                        End If
                        If Mid(ConfidentialData, 56, 1) = "1" Then
                            Me.chbMethod9MultiEquip5.Checked = True
                        Else
                            chbMethod9MultiEquip5.Checked = False
                        End If

                    Case "N" 'Method 22 
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMethod22MaxOpCapacity.Checked = True
                        Else
                            chbMethod22MaxOpCapacity.Checked = False
                        End If

                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMethod22OpCapacity.Checked = True
                        Else
                            chbMethod22OpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbMethod22AllowEmiss.Checked = True
                        Else
                            chbMethod22AllowEmiss.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbMethod22AppReg.Checked = True
                        Else
                            chbMethod22AppReg.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbMethod22TestDuration.Checked = True
                        Else
                            chbMethod22TestDuration.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbMethod22Emission.Checked = True
                        Else
                            chbMethod22Emission.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbMethod22OtherInfo.Checked = True
                        Else
                            chbMethod22OtherInfo.Checked = False
                        End If

                    Case "O" 'Method 9 Single
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMethod9MaxOpCapacity.Checked = True
                        Else
                            chbMethod9MaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMethod9OpCapacity.Checked = True
                        Else
                            chbMethod9OpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbMethod9AllowEmiss.Checked = True
                        Else
                            chbMethod9AllowEmiss.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbMethod9AppRequire.Checked = True
                        Else
                            chbMethod9AppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbMethod9ControlEquip.Checked = True
                        Else
                            chbMethod9ControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbMethod9TestDuration.Checked = True
                        Else
                            chbMethod9TestDuration.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbMethod9Opacity.Checked = True
                        Else
                            chbMethod9Opacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbMethod9OtherInfo.Checked = True
                        Else
                            chbMethod9OtherInfo.Checked = False
                        End If
                    Case "P" 'PEM

                    Case "Q" 'PTE
                        If Mid(ConfidentialData, 26, 1) = "1" Then
                            Me.chbMemoAppRequire.Checked = True
                        Else
                            chbMemoAppRequire.Checked = False
                        End If
                        If Mid(ConfidentialData, 27, 1) = "1" Then
                            Me.chbMemoPTEMaxOpCapacity.Checked = True
                        Else
                            chbMemoPTEMaxOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 28, 1) = "1" Then
                            Me.chbMemoPTEOpCapacity.Checked = True
                        Else
                            chbMemoPTEOpCapacity.Checked = False
                        End If
                        If Mid(ConfidentialData, 29, 1) = "1" Then
                            Me.chbMemoPTEAllowEmiss1.Checked = True
                        Else
                            chbMemoPTEAllowEmiss1.Checked = False
                        End If
                        If Mid(ConfidentialData, 30, 1) = "1" Then
                            Me.chbMemoPTEAllowEmiss2.Checked = True
                        Else
                            chbMemoPTEAllowEmiss2.Checked = False
                        End If
                        If Mid(ConfidentialData, 31, 1) = "1" Then
                            Me.chbMemoPTEAllowEmiss3.Checked = True
                        Else
                            chbMemoPTEAllowEmiss3.Checked = False
                        End If
                        If Mid(ConfidentialData, 32, 1) = "1" Then
                            Me.chbMemoPTEControlEquip.Checked = True
                        Else
                            chbMemoPTEControlEquip.Checked = False
                        End If
                        If Mid(ConfidentialData, 33, 1) = "1" Then
                            Me.chbMemoPTEMemo.Checked = True
                        Else
                            chbMemoPTEMemo.Checked = False
                        End If

                    Case Else

                End Select
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
    Sub SaveConfidentialData()
        Try

            If txtReferenceNumber.Text <> "" And DocumentType <> "" Then
                ConfidentialData = "00"
                If chbAIRSNumber.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If chbFacilityName.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If chbLocation.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If chbReportType.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbISMPReviewer.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbISMPUnit.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbISMPProgramManager.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbISMPUnitManager.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbTestNotification.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbWitnessingEngineer.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbOtherWitnessingEngineer.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbSourceTested.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbPollutant.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbMethodUsed.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbTestingFirm.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbISMPComplianceDetermination.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbDatesTested.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbDaysInAPB.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbReceivedByAPB.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbAssignedToEngineer.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbCompletedByISMP.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbComplianceManager.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If
                If Me.chbCC.Checked = True Then
                    ConfidentialData = ConfidentialData & "1"
                Else
                    ConfidentialData = ConfidentialData & "0"
                End If

                Select Case DocumentType
                    Case "002"
                        ConfidentialData = "0" & "A" & Mid(ConfidentialData, 3)
                        If Me.chbOneStackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackPercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Run1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Temp1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Moist1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2ACFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2DSCFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Poll1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Emiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Run2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Temp2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Moist2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2ACFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2DSCFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Poll2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2Emiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2PollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2PollAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2EmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack2EmissAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "003"
                        ConfidentialData = "0" & "B" & Mid(ConfidentialData, 3)
                        If Me.chbOneStackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackPercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Run1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Temp1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Moist1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3ACFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3DSCFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Poll1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Emiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Run2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Temp2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Moist2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3ACFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3DSCFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Poll2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Emiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Run3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Temp3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Moist3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3ACFM3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3DSCFM3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Poll3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3Emiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3PollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3PollAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3EmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack3EmissAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "004"
                        ConfidentialData = "0" & "C" & Mid(ConfidentialData, 3)
                        If Me.chbOneStackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackPercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Run1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Temp1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Moist1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4ACFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4DSCFM1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Poll1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Emiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Run2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Temp2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Moist2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4ACFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4DSCFM2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Poll2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Emiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Run3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Temp3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Moist3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4ACFM3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4DSCFM3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Poll3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Emiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Run4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Temp4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Moist4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4ACFM4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4DSCFM4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Poll4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4Emiss4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4PollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4PollAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4EmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbOneStack4EmissAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "005"
                        ConfidentialData = "0" & "D" & Mid(ConfidentialData, 3)
                        If Me.chbTwoStackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandName1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandName2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandRun3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTemp3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandMoist3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandACFM3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandDSCFM3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPoll3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmiss3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                        If Me.chbTwoStackStandPollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPollAvg1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPollAvg2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmissAvg1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandEmissAvg2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTotal1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTotal2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTotal3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandTotalAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackStandPercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "006"
                        ConfidentialData = "0" & "E" & Mid(ConfidentialData, 3)
                        If Me.chbTwoStackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREName1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREName2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETemp1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss1a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETemp2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss2a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETEmp3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss3a.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETemp1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss1b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETemp2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss2b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRERun3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDRETemp3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREMoist3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREACFM3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDSCFM3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPoll3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmiss3b.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPollAvg1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREPollAvg2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmissAvg1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREEmissAvg2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbTwoStackDREDestructionEff.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "007"
                        ConfidentialData = "0" & "F" & Mid(ConfidentialData, 3)
                        If Me.chbLoadingRackMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackTestDuration.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackPollIN.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackPollOUT.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackDestReduction.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackEmiss.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbLoadingRackOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "008"
                        ConfidentialData = "0" & "G" & Mid(ConfidentialData, 3)
                        If Me.chbPulpMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpRun1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpConc1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpTreatment1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpRun2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpConc2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpTreatment2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpRun3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpConc3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpTreatment3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpConcUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpConcAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpTreatmentUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpTreatmentAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpDestructEffic.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbPulpOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "009"
                        ConfidentialData = "0" & "H" & Mid(ConfidentialData, 3)
                        If Me.chbGasMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasRun1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPoll1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasRun2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPoll2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasRun3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPoll3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPollUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPollAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasEmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasEmissAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasPercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbGasOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "010"
                        ConfidentialData = "0" & "I" & Mid(ConfidentialData, 3)
                        If Me.chbFlareMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareAllowLimitations.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeatContent.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareMonitor.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareRun1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeating1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareVelocity1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareRun2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeating2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareVelocity2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareRun3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeating3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareVelocity3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeatingUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareHeatingAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareVelocityUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareVelocityAvg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlarePercentAllow.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbFlareOtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "011"
                        ConfidentialData = "0" & "J" & Mid(ConfidentialData, 3)
                        If Me.chbRATAAppStandard.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATAAppRegulation.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATADiluent.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                        If Me.chbRATARef1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef6.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef7.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef8.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef9.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef10.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef11.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARef12.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS6.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS7.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS8.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS9.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS10.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS11.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATACMS12.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATAUnits.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATARelativeAcc.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATAStatement.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbRATAOtherInformation.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "012"
                        ConfidentialData = "0" & "K" & Mid(ConfidentialData, 3)
                        If Me.chbMemoAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoStandardMemo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "013"
                        ConfidentialData = "0" & "L" & Mid(ConfidentialData, 3)
                        If Me.chbMemoAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoToFileManufacture.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoToFileSerial.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoToFileMemo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "014"
                        ConfidentialData = "0" & "M" & Mid(ConfidentialData, 3)
                        If Me.chbMethod9MultiMaxOpCapacity1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiMaxOpCapacity2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiMaxOpCapacity3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiMaxOpCapacity4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiMaxOpCapacity5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiMaxOpCapacityUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacity1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacity2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacity3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacity4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacity5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOpCapacityUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmiss4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmiss5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAllowEmissUnit.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If chbMethod9MultiAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAvg1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAvg2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAvg3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAvg4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiAvg5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiOtherInfor.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiEquip1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiEquip2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiEquip3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiEquip4.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9MultiEquip5.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "015"
                        ConfidentialData = "0" & "N" & Mid(ConfidentialData, 3)
                        If Me.chbMethod22MaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22OpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22AllowEmiss.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22AppReg.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22TestDuration.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22Emission.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod22OtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case "016"
                        ConfidentialData = "0" & "O" & Mid(ConfidentialData, 3)
                        If Me.chbMethod9MaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9OpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9AllowEmiss.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9AppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9ControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9TestDuration.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9Opacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMethod9OtherInfo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If

                    Case "017"
                        ConfidentialData = "0" & "P" & Mid(ConfidentialData, 3)

                    Case "018"
                        ConfidentialData = "0" & "Q" & Mid(ConfidentialData, 3)
                        If Me.chbMemoAppRequire.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEMaxOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEOpCapacity.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEAllowEmiss1.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEAllowEmiss2.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEAllowEmiss3.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEControlEquip.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                        If Me.chbMemoPTEMemo.Checked = True Then
                            ConfidentialData = ConfidentialData & "1"
                        Else
                            ConfidentialData = ConfidentialData & "0"
                        End If
                    Case Else
                End Select

                If ConfidentialData.Contains("1") Then
                    ConfidentialData = "1" & Mid(ConfidentialData, 2)
                Else
                    'ConfidentialData = ConfidentialData
                End If
                SQL = "Update AIRBRANCH.ISMPReportInformation set " & _
                "strConfidentialData = '" & Replace(ConfidentialData, "'", "''") & "' " & _
                "where strReferencenumber = '" & txtReferenceNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If MultiFormIsOpen(ISMPTestReports, txtReferenceNumber.Text) Then
                    Dim testReportForm As ISMPTestReports = MultiForm(ISMPTestReports.Name)(txtReferenceNumber.Text)
                    testReportForm.LoadConfidentialData(ConfidentialData)
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try

            SaveConfidentialData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSave.Click
        Try

            SaveConfidentialData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub mmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiBack.Click
        Try
            ISMPConfidential = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            ISMPConfidential = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub


    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class