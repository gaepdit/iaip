Imports System.Data.OracleClient


Public Class IAIPFacilitySummary
    Dim dsFacilityWideData As DataSet
    Dim daFacilityWideData As OracleDataAdapter
    Dim dsBottomData As DataSet
    Dim daBottomData As OracleDataAdapter
    Dim dsFees As DataSet
    Dim daFees As OracleDataAdapter
    Dim dsISMP As DataSet
    Dim daISMP As OracleDataAdapter
    Dim dsSSCP As DataSet
    Dim daSSCP As OracleDataAdapter
    Dim dsSSPP As DataSet
    Dim daSSPP As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim pnltemp As String
    Dim year As String
    Dim inventoryYear As Integer
    Dim recExist2 As Boolean
    Dim SQLLine As String
    Dim count As Integer


    Private Sub IAIPFacilitySummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            pnltemp = Panel1.Text
            Panel1.Text = ""
            Panel2.Text = UserName
            Panel3.Text = OracleDate
            TBFacilitySummary.Buttons.Item(0).Visible = False
            LoadCheckListBoxes()

            LoadPermissions()

            LoadToolBars()
            'loadCboEIYear()

            clbYear.SetItemCheckState(0, CheckState.Checked)
            clbSummaryChoices.SetItemCheckState(0, CheckState.Checked)
            clbSummaryChoices.Size = New System.Drawing.Size(125, 34)
            clbYear.Size = New System.Drawing.Size(125, 34)

            mmiPrintFacilitySummary.Visible = True

            Panel1.Text = pnltemp
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        Panel1.Text = pnltemp

    End Sub
    Private Sub IAIPFacilitySummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

            mtbAIRSNumber.Focus()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Page Load"
    Sub LoadCheckListBoxes()
        Dim YearValue As Integer = "00"
        Dim CurrentYear As String = Date.Today.Year

        Try

            clbSummaryChoices.Items.Add("All Data")
            clbSummaryChoices.Items.Add("ISMP Data")
            clbSummaryChoices.Items.Add("SSCP Data")
            clbSummaryChoices.Items.Add("SSPP Data")
            clbSummaryChoices.Items.Add("PASP Data")

            clbYear.Items.Clear()
            If CurrentYear.Length = 4 Then
                CurrentYear = Mid(CurrentYear, 3)
            Else
                CurrentYear = "00"
            End If

            clbYear.Items.Add("All Years")

            YearValue = CInt(CurrentYear)

            Do While YearValue <> 0
                Select Case CStr(YearValue).Length
                    Case "1"
                        clbYear.Items.Add("200" & YearValue)
                    Case "2"
                        clbYear.Items.Add("20" & YearValue)
                    Case Else
                        clbYear.Items.Add(YearValue)
                End Select
                YearValue -= 1
            Loop

            clbYear.Items.Add("2000")
            clbYear.Items.Add("1999")
            clbYear.Items.Add("1998")
            clbYear.Items.Add("1997")
            clbYear.Items.Add("1996")
            clbYear.Items.Add("1995")
            clbYear.Items.Add("1994")
            clbYear.Items.Add("1993")
            clbYear.Items.Add("1992")
            clbYear.Items.Add("1991")
            clbYear.Items.Add("1990")
            clbYear.Items.Add("1989")
            clbYear.Items.Add("1988")
            clbYear.Items.Add("1987")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadPermissions()
        Try


            mmiISMP.Visible = False
            mmiSubISMP.Visible = False
            mmiSSCP.Visible = False
            mmiSubSSCP.Visible = False
            mmiEditContactInformation.Visible = False
            mmiSubAddEditContacts.Visible = False

            Select Case UserBranch
                Case "1" 'Air Protection
                    mmiEditContactInformation.Visible = True
                    mmiSubAddEditContacts.Visible = True
                    Select Case UserProgram
                        Case "1" 'Mobile & Area

                        Case "2" 'Planning & Support 

                        Case "3" 'ISMP 
                            mmiISMP.Visible = True
                            mmiSubISMP.Visible = True

                            If UserUnit = "---" Then 'Program Manager
                                mmiISMPNewReport.Visible = True
                                mmiISMPNewLogEnTry.Visible = True
                                mmiISMPTestLogLink.Visible = False
                                llbClosePrintTestReport.Visible = True
                                mmiISMPClosePrint.Visible = True
                                mmiISMPNewReport.Visible = True
                                mmiEditData.Visible = True
                                mmiSeperator.Visible = True
                            Else
                                If AccountArray(17, 3) = "1" Then 'Unit Manager 
                                    mmiISMPNewReport.Visible = True
                                    mmiISMPNewLogEnTry.Visible = True
                                    mmiISMPTestLogLink.Visible = False
                                    llbClosePrintTestReport.Visible = False
                                    mmiISMPClosePrint.Visible = False
                                    mmiISMPNewReport.Visible = False
                                    mmiEditData.Visible = False
                                    mmiSeperator.Visible = False
                                Else
                                    If AccountArray(68, 3) = "1" Then 'ISMP Administrator
                                        mmiISMPNewReport.Visible = True
                                        mmiISMPNewLogEnTry.Visible = True
                                        mmiISMPTestLogLink.Visible = False
                                        llbClosePrintTestReport.Visible = True
                                        mmiISMPClosePrint.Visible = True
                                        mmiISMPNewReport.Visible = True
                                        mmiEditData.Visible = False
                                        mmiSeperator.Visible = False
                                    Else
                                        If AccountArray(68, 2) = "1" Then 'ISMP Specialist
                                            mmiISMPNewReport.Visible = True
                                            mmiISMPNewLogEnTry.Visible = True
                                            mmiISMPTestLogLink.Visible = False
                                            llbClosePrintTestReport.Visible = False
                                            mmiISMPClosePrint.Visible = True
                                            mmiISMPNewReport.Visible = True
                                            mmiEditData.Visible = False
                                            mmiSeperator.Visible = False
                                        Else
                                            mmiISMPNewReport.Visible = False
                                            mmiISMPNewLogEnTry.Visible = True
                                            mmiISMPTestLogLink.Visible = False
                                            llbClosePrintTestReport.Visible = False
                                            mmiISMPClosePrint.Visible = False
                                            mmiISMPNewReport.Visible = False
                                            mmiEditData.Visible = False
                                            mmiSeperator.Visible = False
                                        End If
                                    End If
                                End If
                            End If
                        Case "4" 'SSCP
                            mmiSSCP.Visible = True
                            mmiSubSSCP.Visible = True

                            If UserUnit = "---" Then 'Program Manager 
                                mmiSSCPAssignEngineer.Visible = True
                                mmiSSCPNewWork.Visible = True
                                mmiSSCPFCE.Visible = True
                            Else
                                If AccountArray(22, 3) = "1" Then 'Unit Manager 
                                    mmiSSCPAssignEngineer.Visible = True
                                    mmiSSCPNewWork.Visible = True
                                    mmiSSCPFCE.Visible = True
                                Else
                                    mmiSSCPAssignEngineer.Visible = False
                                    mmiSSCPNewWork.Visible = True
                                    mmiSSCPFCE.Visible = True
                                    'If AccountArray(10, 3) = "1" Then 'Distirct Liason 
                                    '    mmiSSCPAssignEngineer.Visible = False
                                    '    mmiSSCPNewWork.Visible = True
                                    '    mmiSSCPFCE.Visible = True
                                    'Else
                                    '    mmiSSCPAssignEngineer.Visible = False
                                    '    mmiSSCPNewWork.Visible = True
                                    '    mmiSSCPFCE.Visible = True
                                    'End If
                                End If
                            End If
                        Case "5" 'SSPP 
                            mmiSSPP.Visible = True
                            mmisubSSPP.Visible = True
                        Case "6" 'Ambient 

                    End Select
                Case "2" 'Watershed

                Case "3" 'Hazard Waste

                Case "4" 'Land Protection

                Case "5" 'Program Coordination 

                Case "6" 'Directors Office 

            End Select

            mmiNewFacility.Visible = False
            If AccountArray(138, 0) Is Nothing Then
            Else
                If AccountArray(138, 1) = "1" Then
                    If AccountArray(138, 1) = "1" Or AccountArray(138, 2) = "1" Or AccountArray(138, 3) = "1" Or AccountArray(138, 4) = "1" Then
                        mmiNewFacility.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadToolBars()
        Try

            If mmiStandard.Checked = True Then
                'TBFacilitySummary.Buttons.Item(0).Visible = True
                TBFacilitySummary.Buttons.Item(1).Visible = True
                TBFacilitySummary.Buttons.Item(2).Visible = True
                TBFacilitySummary.Buttons.Item(3).Visible = True
                TBFacilitySummary.Buttons.Item(4).Visible = True
                TBFacilitySummary.Buttons.Item(5).Visible = True
                TBFacilitySummary.Buttons.Item(6).Visible = True
                TBFacilitySummary.Buttons.Item(7).Visible = False
            Else
                TBFacilitySummary.Buttons.Item(0).Visible = False
                TBFacilitySummary.Buttons.Item(1).Visible = False
                TBFacilitySummary.Buttons.Item(2).Visible = False
                TBFacilitySummary.Buttons.Item(3).Visible = False
                TBFacilitySummary.Buttons.Item(4).Visible = False
                TBFacilitySummary.Buttons.Item(5).Visible = False
                TBFacilitySummary.Buttons.Item(6).Visible = False
                TBFacilitySummary.Buttons.Item(7).Visible = False
            End If

            'Shows the Edit Data button to PM2's Only
            If UserUnit = "---" Or AccountArray(22, 3) = "1" Then
                TBFacilitySummary.Buttons.Item(0).Visible = True
            Else
                TBFacilitySummary.Buttons.Item(0).Visible = False
            End If

            If UserProgram = "4" And AccountArray(22, 3) = "1" Then
                TBFacilitySummary.Buttons.Item(0).Visible = True
            End If

            If mmiSubAddEditContacts.Checked = True Then
                TBFacilitySummary.Buttons.Item(8).Visible = True
            Else
                TBFacilitySummary.Buttons.Item(8).Visible = False
            End If
            If mmiSubISMP.Checked = True Then
                If (UserUnit = "---" And AccountArray(17, 3) = "1") Or AccountArray(68, 3) = "1" Then
                    TBFacilitySummary.Buttons.Item(9).Visible = True
                    TBFacilitySummary.Buttons.Item(10).Visible = True
                End If
                TBFacilitySummary.Buttons.Item(11).Visible = True
                TBFacilitySummary.Buttons.Item(12).Visible = False
                TBFacilitySummary.Buttons.Item(13).Visible = True
            Else
                TBFacilitySummary.Buttons.Item(9).Visible = False
                TBFacilitySummary.Buttons.Item(10).Visible = False
                TBFacilitySummary.Buttons.Item(11).Visible = False
                TBFacilitySummary.Buttons.Item(12).Visible = False
                TBFacilitySummary.Buttons.Item(13).Visible = False
            End If

            If mmiSubSSCP.Checked = True Then
                If AccountArray(24, 3) = "1" Then
                    TBFacilitySummary.Buttons.Item(14).Visible = True
                End If
                TBFacilitySummary.Buttons.Item(15).Visible = True
                TBFacilitySummary.Buttons.Item(16).Visible = True
            Else
                TBFacilitySummary.Buttons.Item(14).Visible = False
                TBFacilitySummary.Buttons.Item(15).Visible = False
                TBFacilitySummary.Buttons.Item(16).Visible = False
            End If
            If mmisubSSPP.Checked = True Then
                TBFacilitySummary.Buttons.Item(17).Visible = True
            Else
                TBFacilitySummary.Buttons.Item(17).Visible = False
            End If

            If AccountArray(22, 3) = "1" Then
            Else
                TBFacilitySummary.Buttons.Item(0).Visible = False
            End If

            If AccountArray(1, 3) = "1" Then
                TBFacilitySummary.Buttons.Item(0).Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Sub LoadData()
        Try

            If mtbAIRSNumber.Text <> "" And mtbAIRSNumber.Text.Length = 8 Then
                ClearPage()

                pnltemp = Panel1.Text
                Panel1.Text = "Loading Data"
                ProgressBar.PerformStep()

                'This is the work to send the download to the background worker below.

                llbViewAll.Enabled = False

                bgwFacilityWideData.WorkerReportsProgress = True
                bgwFacilityWideData.WorkerSupportsCancellation = True
                bgwFacilityWideData.RunWorkerAsync()

                dsISMP = New DataSet
                dsSSCP = New DataSet
                dsSSPP = New DataSet
                ds = New DataSet

                If clbSummaryChoices.CheckedIndices.Contains(0) = True Then
                    bgwAllProgramData.WorkerReportsProgress = True
                    bgwAllProgramData.WorkerSupportsCancellation = True
                    bgwAllProgramData.RunWorkerAsync()
                Else
                    If clbSummaryChoices.CheckedIndices.Contains(1) = True Then
                        bgwLoadMonitoring.WorkerReportsProgress = True
                        bgwLoadMonitoring.WorkerSupportsCancellation = True
                        bgwLoadMonitoring.RunWorkerAsync()
                    End If
                    If clbSummaryChoices.CheckedIndices.Contains(2) = True Then
                        bgwLoadCompliance.WorkerReportsProgress = True
                        bgwLoadCompliance.WorkerSupportsCancellation = True
                        bgwLoadCompliance.RunWorkerAsync()

                    End If
                    If clbSummaryChoices.CheckedIndices.Contains(3) = True Then
                        bgwLoadPermitting.WorkerReportsProgress = True
                        bgwLoadPermitting.WorkerSupportsCancellation = True
                        bgwLoadPermitting.RunWorkerAsync()
                    End If

                    If clbSummaryChoices.CheckedIndices.Contains(4) = True Then
                        bgwLoadPlanningAndSupport.WorkerReportsProgress = True
                        bgwLoadPlanningAndSupport.WorkerSupportsCancellation = True
                        bgwLoadPlanningAndSupport.RunWorkerAsync()
                    End If
                End If

                SQL = "select strDistrictResponsible " & _
                "from " & connNameSpace & ".SSCPDistrictResponsible " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strDistrictResponsible")) Then
                        lblDistrictSource.Visible = False
                    Else
                        If dr.Item("strDistrictResponsible") = "True" Then
                            lblDistrictSource.Visible = True
                        Else
                            lblDistrictSource.Visible = False
                        End If
                    End If
                Else
                    lblDistrictSource.Visible = False
                End If
                dr.Close()


                Panel1.Text = pnltemp
            End If

        Catch ex As Exception
            ErrorReport(mtbAIRSNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub EditSubPart()
        Try

            If chbAPC8.Checked = True Or chbAPC9.Checked = True Or chbAPCM.Checked = True Or chbAPC0.Checked = True Then
                btnOpenSubpartEditior.Visible = True
            Else
                btnOpenSubpartEditior.Visible = False
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ClearPage()
        Try

            txtFacilityName.Clear()
            txtStreetAddress.Clear()
            txtStreetAddress2.Clear()
            txtFacilityCounty.Clear()
            txtFacilityCity.Clear()
            txtFacilityState.Clear()
            txtFacilityZipCode.Clear()
            txtFacilityLongitude.Clear()
            txtFacilityLatitude.Clear()
            txtClassification.Clear()
            txtOperationalStatus.Clear()
            txtSICCode.Clear()
            txtCMSState.Clear()
            txtStartUpDate.Clear()
            txtDateClosed.Clear()
            txtPhysicalShutDownDate.Clear()

            txt1hour.Clear()
            txt8HROzone.Clear()
            txtPM.Clear()

            txtPollutantStatus.Clear()
            txtPlantDescription.Clear()
            cboEIYear.Items.Clear()

            chbAPC0.Checked = False
            chbAPC1.Checked = False
            chbAPC3.Checked = False
            chbAPC4.Checked = False
            chbAPC6.Checked = False
            chbAPC7.Checked = False
            chbAPC8.Checked = False
            chbAPC9.Checked = False
            chbAPCA.Checked = False
            chbAPCF.Checked = False
            chbAPCI.Checked = False
            chbAPCM.Checked = False
            chbAPCV.Checked = False
            chbHAPsMajor.Checked = False
            chbNSRMajor.Checked = False

            txtDistrict.Clear()
            txtOffice.Clear()
            txtSSCPContact.Clear()
            txtSSCPUnit.Clear()
            txtSSPPContact.Clear()
            txtSSPPUnit.Clear()
            txtISMPContact.Clear()
            txtISMPUnit.Clear()
            txtDistrictEngineer.Clear()
            txtDistrictUnit.Clear()

            txtReferenceNumber.Clear()
            txtTestingNumber.Clear()
            txtReferenceNumber2.Clear()

            txtTrackingNumber.Clear()
            txtFCEYear.Clear()

            txtEnforcementNumber.Clear()



            If dsISMP Is Nothing Then
            Else
                'dsISMP.Clear()
                'dgvISMPWork.DataSource = dsISMP
                'dgvISMPTestNotification.DataSource = dsISMP
                'dgvISMPMemo.DataSource = dsISMP

                dgvISMPWork.DataSource = Nothing
                dgvISMPTestNotification.DataSource = Nothing
                dgvISMPMemo.DataSource = Nothing
                dgvISMPWork.DataMember = Nothing
                dgvISMPTestNotification.DataMember = Nothing
                dgvISMPMemo.DataMember = Nothing
            End If
            If dsSSCP Is Nothing Then
            Else
                'dsSSCP.Clear()
                'dgvSSCPEvents.DataSource = dsSSCP
                'dgvSSCPEnforcement.DataSource = dsSSCP
                'dgvFCEData.DataSource = dsSSCP

                dgvSSCPEvents.DataSource = Nothing
                dgvSSCPEnforcement.DataSource = Nothing
                dgvFCEData.DataSource = Nothing
                dgvSSCPEvents.DataMember = Nothing
                dgvSSCPEnforcement.DataMember = Nothing
                dgvFCEData.DataMember = Nothing
            End If
            If dsSSPP Is Nothing Then
            Else
                'dsSSPP.Clear()
                'dgvApplicationLog.DataSource = dsSSPP
                'dgvActiveRules.DataSource = dsSSPP
                'dgvRuleHistory.DataSource = dsSSPP

                dgvApplicationLog.DataSource = Nothing
                dgvActiveRules.DataSource = Nothing
                dgvRuleHistory.DataSource = Nothing
                dgvApplicationLog.DataMember = Nothing
                dgvActiveRules.DataMember = Nothing
                dgvRuleHistory.DataMember = Nothing
            End If

            If ds Is Nothing Then
            Else
                'ds.Clear()
                'dgvEIData.DataSource = ds
                'dgvFeeData.DataSource = ds
                'dgvFeeDeposits.DataSource = ds

                dgvEIData.DataSource = Nothing
                dgvFeeData.DataSource = Nothing
                dgvFeeDeposits.DataSource = Nothing
                dgvEIData.DataMember = Nothing
                dgvFeeData.DataMember = Nothing
                dgvFeeDeposits.DataMember = Nothing
            End If
            cboEIYear.Text = ""

            If dsFees Is Nothing Then
            Else
                dsFees.Clear()
            End If

            cboFeeYear.DataBindings.Clear()
            cboFeeYear.Text = ""
            txtFeesClassification.DataBindings.Clear()
            txtFeesClassification.Clear()
            txtFeesTotal.DataBindings.Clear()
            txtFeesTotal.Clear()
            txtFeesPart70.DataBindings.Clear()
            txtFeesPart70.Clear()
            txtFeesSM.DataBindings.Clear()
            txtFeesSM.Clear()
            txtFeesNSPS.DataBindings.Clear()
            txtFeesNSPS.Clear()
            txtFeesVOC.DataBindings.Clear()
            txtFeesVOC.Clear()
            txtFeesPM.DataBindings.Clear()
            txtFeesPM.Clear()
            txtFeesSO2.DataBindings.Clear()
            txtFeesSO2.Clear()
            txtFeesNOx.DataBindings.Clear()
            txtFeesNOx.Clear()
            txtFeesRate.DataBindings.Clear()
            txtFeesRate.Clear()
            txtFeesPollutantFee.DataBindings.Clear()
            txtFeesPollutantFee.Clear()
            chbFeesOperating.DataBindings.Clear()
            chbFeesOperating.Checked = False
            chbFeesPart70.DataBindings.Clear()
            chbFeesPart70.Checked = False
            chbNSPSExempt.DataBindings.Clear()
            chbNSPSExempt.Checked = False
            lblDistrictSource.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Sub LoadStateContactInformation()
        Try


            SQL = "Select (strLastName||', '||strFirstName) as SSCPEngineer, strUnitDesc " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".SSCPFacilityAssignment,  " & _
            "" & connNameSpace & ".LookUpEPDUnits  " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and numUserID = strSSCPEngineer   " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New OracleCommand(SQL, conn)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("SSCPEngineer")) Then
                    txtSSCPContact.Clear()
                Else
                    txtSSCPContact.Text = dr.Item("SSCPEngineer")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    txtSSCPUnit.Clear()
                Else
                    txtSSCPUnit.Text = dr.Item("strUnitDesc")
                End If
            Else
                txtSSCPContact.Clear()
                txtSSCPUnit.Clear()
            End If
            dr.Close()

            SQL = "Select " & _
            "Distinct((strLastName||', '||strFirstName)) as ISMPEngineer, strUnitDesc   " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".ISMPReportInformation,   " & _
            "" & connNameSpace & ".ISMPMaster, " & connNameSpace & ".LookUpEPDUnits    " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDunits.numunitCode (+) " & _
            "and numUserID = strReviewingEngineer   " & _
            "AND " & connNameSpace & ".ISMPMaster.strReferenceNumber = " & connNameSpace & ".ISMPReportInformation.strReferenceNumber   " & _
            "and strClosed = 'True'  " & _
            "and datCompleteDate = (Select Distinct(Max(datCompleteDate)) as CompleteDate  " & _
            "from " & connNameSpace & ".ISMPReportInformation, " & connNameSpace & ".ISMPMaster  " & _
            "where " & connNameSpace & ".ISMPReportInformation.strReferenceNumber = " & connNameSpace & ".ISMPMaster.strReferenceNumber   " & _
            "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "and strClosed = 'True')  " & _
            "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("ISMPEngineer")) Then
                    txtISMPContact.Clear()
                Else
                    txtISMPContact.Text = dr.Item("ISMPEngineer")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    txtISMPUnit.Clear()
                Else
                    txtISMPUnit.Text = dr.Item("strUnitDesc")
                End If
            Else
                txtISMPContact.Clear()
                txtISMPUnit.Clear()
            End If
            dr.Close()


            SQL = "select  " & _
             "Distinct((strLastName||', '||strFirstName)) as SSPPStaffResponsible, strUnitDesc   " & _
             "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".SSPPApplicationMaster, " & _
             "" & connNameSpace & ".LookUpEPDUnits " & _
             "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
             "and numUserID = strStaffResponsible  " & _
             "and " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
             "(select distinct(max(to_number(strApplicationNumber))) as GreatestApplication  " & _
             "from " & connNameSpace & ".SSPPApplicationMaster   " & _
             "where " & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "')  " & _
             "and " & connNameSpace & ".SSPPApplicationMaster.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "'  "


            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("SSPPStaffResponsible")) Then
                    txtSSPPContact.Clear()
                Else
                    txtSSPPContact.Text = dr.Item("SSPPStaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    txtSSPPUnit.Clear()
                Else
                    txtSSPPUnit.Text = dr.Item("strUnitDesc")
                End If
            Else
                txtSSPPContact.Clear()
                txtSSPPUnit.Clear()
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Try

            chbAPC0.Checked = False
            chbAPC1.Checked = False
            chbAPC3.Checked = False
            chbAPC4.Checked = False
            chbAPC6.Checked = False
            chbAPC7.Checked = False
            chbAPC8.Checked = False
            chbAPC9.Checked = False
            chbAPCF.Checked = False
            chbAPCA.Checked = False
            chbAPCI.Checked = False
            chbAPCM.Checked = False
            chbAPCV.Checked = False


            If Mid(AirProgramCode, 1, 1) = 1 Then
                chbAPC0.Checked = True
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                chbAPC1.Checked = True
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                chbAPC3.Checked = True
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                chbAPC4.Checked = True
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                chbAPC6.Checked = True
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                chbAPC7.Checked = True
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                chbAPC8.Checked = True
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                chbAPC9.Checked = True
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                chbAPCF.Checked = True
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                chbAPCA.Checked = True
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                chbAPCI.Checked = True
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                chbAPCM.Checked = True
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                chbAPCV.Checked = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#Region "ISMP Specific"

    Private Sub llbISMPTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbISMPTestReport.LinkClicked
        Dim temp As String = ""

        Try

            If txtReferenceNumber.Text <> "" Then
                If UserProgram = "3" Then
                    SQL = "select " & connNameSpace & ".ISMPDocumentType.strDocumentType " & _
                    "from " & connNameSpace & ".ISMPDocumentType, " & connNameSpace & ".ISMPReportInformation " & _
                    "where " & connNameSpace & ".ISMPReportInformation.strDocumentType = " & connNameSpace & ".ISMPDocumentType.strKey and " & _
                    "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        ISMPTestReportsEntry = Nothing
                        If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                        ISMPTestReportsEntry.txtReferenceNumber.Text = txtReferenceNumber.Text
                        ISMPTestReportsEntry.Show()
                        ISMPTestReportsEntry.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End If
                Else
                    SQL = "Select strClosed " & _
                    "from " & connNameSpace & ".ISMPReportInformation " & _
                    "where strReferenceNumber = '" & txtReferenceNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        temp = dr.Item("strClosed")
                    End While
                    If temp = "True" Then
                        PrintOut = Nothing
                        If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtReferenceNumber.Text
                        PrintOut.txtPrintType.Text = "SSCP"
                        PrintOut.Show()
                        PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Else
                        MsgBox("This Test Summary has not been completely reviewed by ISMP Engineer", MsgBoxStyle.Information, "Facility Summary")
                    End If

                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewTestReport2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewTestReportMemo.LinkClicked
        Try

            If txtReferenceNumber2.Text <> "" Then
                ISMPMemoEdit = Nothing
                If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
                ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber2.Text
                ISMPMemoEdit.Show()
                ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewTestNotification_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewTestNotification.LinkClicked
        Try

            If txtTestingNumber.Text <> "" Then
                DevTestLog = Nothing
                If DevTestLog Is Nothing Then DevTestLog = New ISMPNotificationLog
                DevTestLog.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                DevTestLog.Show()
                DevTestLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "SSCP Specific"
    Private Sub llbViewComplianceEvent_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewComplianceEvent.LinkClicked
        Try

            If txtTrackingNumber.Text <> "" Then
                SSCPREports = Nothing
                If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                SSCPREports.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCPREports.txtOrigin.Text = "Facility Summary"
                SSCPREports.Show()
                SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewSSCPEnforcement_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewSSCPEnforcement.LinkClicked
        Try
            If txtEnforcementNumber.Text <> "" Then
                If SSCP_Enforcement Is Nothing Then
                    If SSCP_Enforcement Is Nothing Then SSCP_Enforcement = New SSCPEnforcementAudit
                    SSCP_Enforcement.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    SSCP_Enforcement.txtOrigin.Text = "Facility Summary"
                    If txtEnforcementNumber.Text <> "" Then
                        SSCP_Enforcement.txtEnforcementNumber.Text = txtEnforcementNumber.Text
                    End If
                    SSCP_Enforcement.Show()
                Else
                    SSCP_Enforcement.BringToFront()
                    SSCP_Enforcement.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    SSCP_Enforcement.txtOrigin.Text = "Facility Summary"
                    If txtEnforcementNumber.Text <> "" Then
                        SSCP_Enforcement.txtEnforcementNumber.Text = txtEnforcementNumber.Text
                    End If
                End If
                SSCP_Enforcement.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewFCE_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewFCE.LinkClicked
        Try

            If txtFCEYear.Text <> "" Then
                ViewFCE()
                SSCPFCE.cboFCEYear.Text = txtFCEYear.Text

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "SSPP Specific"
    Private Sub llbViewApplication_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewApplication.LinkClicked
        Try

            If txtApplicationNumber.Text <> "" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = Nothing
                    If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = txtApplicationNumber.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
                PermitTrackingLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "General"
    Private Sub IAIPFacilitySummary_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            'FacilitySummary = Nothing
            Me.Dispose()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            mtbAIRSNumber.Text = Value
        End Set
    End Property
    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Try

            Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
                Case 0
                    SaveAll()
                Case 1
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
                Case 2
                    SendKeys.Send("^X")
                Case 3
                    SendKeys.Send("^C")
                Case 4
                    SendKeys.Send("^V")
                Case 5
                    ClearPage()
                    mtbAIRSNumber.Clear()
                    For Each index As Integer In Me.clbSummaryChoices.CheckedIndices
                        Me.clbSummaryChoices.SetItemChecked(index, False)
                    Next
                Case 6
                    Me.Hide()
                Case 7
                    Me.Close()
                Case 8
                    If EditContacts Is Nothing Then
                        If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                        EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                        EditContacts.Show()
                    Else
                        EditContacts.Dispose()
                        EditContacts = IAIPEditContacts
                        If EditContacts Is Nothing Then EditContacts = IAIPEditContacts
                        EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                        EditContacts.Show()
                    End If
                    EditContacts.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case 9
                    If mtbAIRSNumber.Text.Length <> 8 Then
                        MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
                    Else
                        If txtFacilityName.Text = "" Then
                            MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                        Else
                            ISMPTestReportInfo = Nothing
                            If ISMPTestReportInfo Is Nothing Then ISMPTestReportInfo = New ISMPFacilityInfo
                            ISMPTestReportInfo.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                            ISMPTestReportInfo.txtFacilityName.Text = Me.txtFacilityName.Text
                            ISMPTestReportInfo.Show()
                            ISMPTestReportInfo.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                    End If
                Case 10
                    If txtReferenceNumber.Text <> "" Then
                        SQL = "Select " & connNameSpace & ".ISMPDocumentType.strDocumentType " & _
                         "from " & connNameSpace & ".ISMPDocumentType, " & connNameSpace & ".ISMPReportInformation " & _
                         "where " & connNameSpace & ".ISMPReportInformation.strDocumentType = " & connNameSpace & ".ISMPDocumentType.strKey and " & _
                         "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                        Dim cmd As New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        Dim dr As OracleDataReader = cmd.ExecuteReader
                        Dim recExist As Boolean = dr.Read
                        If recExist = True Then
                            ISMPCloseAndPrint = Nothing
                            If ISMPCloseAndPrint Is Nothing Then ISMPCloseAndPrint = New ISMPClosePrint
                            ISMPCloseAndPrint.txtTestReportType.Text = dr.Item("strDocumentType")
                            ISMPCloseAndPrint.txtReferenceNumber.Text = txtReferenceNumber.Text
                            ISMPCloseAndPrint.txtAIRSNumber.Text = mtbAIRSNumber.Text
                            ISMPCloseAndPrint.txtFacilityName.Text = txtFacilityName.Text
                            ISMPCloseAndPrint.txtOrigin.Text = "Facility Summary"
                            ISMPCloseAndPrint.Show()
                            ISMPCloseAndPrint.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                    End If
                Case 11
                    If mtbAIRSNumber.Text.Length <> 8 Then
                        MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
                    Else
                        If txtFacilityName.Text = "" Then
                            MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                        Else
                            DevTestLog = Nothing
                            If DevTestLog Is Nothing Then DevTestLog = New ISMPNotificationLog
                            DevTestLog.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                            DevTestLog.Show()
                            DevTestLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                    End If
                Case 12
                    If mtbAIRSNumber.Text.Length <> 8 Then
                        MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
                    Else
                        If txtFacilityName.Text = "" Then
                            MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                        Else
                            MsgBox("UnderConstruction")
                        End If
                    End If
                Case 13
                    ISMPMemoEdit = Nothing
                    If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
                    ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
                    ISMPMemoEdit.Show()
                    ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                Case 14
                    If mtbAIRSNumber.Text.Length <> 8 Then
                        MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
                    Else
                        If txtFacilityName.Text = "" Then
                            MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                        Else
                            SSCPFacAssign = Nothing
                            If SSCPFacAssign Is Nothing Then SSCPFacAssign = New SSCPFacAssignment
                            SSCPFacAssign.txtFacilityName.Text = Me.txtFacilityName.Text
                            SSCPFacAssign.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                            SSCPFacAssign.Show()
                            SSCPFacAssign.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                    End If
                Case 15
                    If mtbAIRSNumber.Text.Length <> 8 Then
                        MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
                    Else
                        If txtFacilityName.Text = "" Then
                            MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                        Else
                            SSCPEngWork = Nothing
                            If SSCPEngWork Is Nothing Then SSCPEngWork = New SSCPWorkEnTry
                            SSCPEngWork.txtFacilityName.Text = Me.txtFacilityName.Text
                            SSCPEngWork.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                            SSCPEngWork.Show()
                            SSCPEngWork.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                    End If
                Case 16
                    ViewFCE()
                Case 17
                    'CDS = Nothing
                    'If CDS Is Nothing Then CDS = New SSPP_CDS_CodingSheet
                    'CDS.Show()
                Case Else
                    MsgBox("try clicking again", MsgBoxStyle.Information, "Facility Summary Toolbar Warning")
            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "Save Functions"
    Sub SaveAll()
        Try

            If mtbAIRSNumber.Text.Length = 8 And Me.txtFacilityName.Text <> "" Then
                If (UserUnit = "---" And UserBranch = "1") Or AccountArray(22, 3) = "1" Then
                    If TCFacilityData.TabPages.Item(0).Focus Then
                        If EditFacilityLocation Is Nothing Then
                            EditFacilityLocation = New IAIPEditFacilityLocation
                            EditFacilityLocation.txtAirsNumber.Text = mtbAIRSNumber.Text
                            EditFacilityLocation.Show()
                        Else
                            EditFacilityLocation.txtAirsNumber.Text = mtbAIRSNumber.Text
                            EditFacilityLocation.Show()
                            EditFacilityLocation.BringToFront()
                        End If
                        EditFacilityLocation.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End If
                End If
                If (UserUnit = "---" And UserBranch = "1") Or AccountArray(22, 3) = "1" Then
                    If TCFacilityData.TabPages.Item(1).Focus Then
                        If EditHeaderData Is Nothing Then
                            EditHeaderData = New IAIPEditHeaderData
                            EditHeaderData.txtAirsNumber.Text = mtbAIRSNumber.Text
                            EditHeaderData.txtFacilityName.Text = txtFacilityName.Text
                            EditHeaderData.Show()
                        Else
                            EditHeaderData.txtAirsNumber.Text = mtbAIRSNumber.Text
                            EditHeaderData.txtFacilityName.Text = txtFacilityName.Text
                            EditHeaderData.Show()
                            EditHeaderData.BringToFront()
                        End If
                        EditHeaderData.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    End If
                End If

                If TCFacilityData.TabPages.Item(2).Focus Then
                    If EditContacts Is Nothing Then
                        If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                        EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                        EditContacts.Show()
                    Else
                        EditContacts.Dispose()
                        EditContacts = New IAIPEditContacts
                        If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                        EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                        EditContacts.Show()
                    End If
                    EditContacts.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "Main Menu Item"
#Region "All"
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        Try

            Help.ShowHelp(Label1, "https://sites.google.com/a/dnr.state.ga.us/iaip-docs/")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            ClearPage()
            mtbAIRSNumber.Clear()
            For Each index As Integer In Me.clbSummaryChoices.CheckedIndices

                Me.clbSummaryChoices.SetItemChecked(index, False)

            Next
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "ISMP"
    Private Sub mmiISMPNewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPNewReport.Click
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                Else
                    ISMPTestReportInfo = Nothing
                    If ISMPTestReportInfo Is Nothing Then ISMPTestReportInfo = New ISMPFacilityInfo
                    ISMPTestReportInfo.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                    ISMPTestReportInfo.txtFacilityName.Text = Me.txtFacilityName.Text
                    ISMPTestReportInfo.Show()
                    ISMPTestReportInfo.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiISMPNewLogEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPNewLogEnTry.Click
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                Else
                    DevTestLog = Nothing
                    If DevTestLog Is Nothing Then DevTestLog = New ISMPNotificationLog
                    DevTestLog.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                    DevTestLog.Show()
                    DevTestLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiISMPTestLogLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPTestLogLink.Click
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    MsgBox("Underconstruction")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "SSCP"
    Private Sub mmiSSCPAssignEngineer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPAssignEngineer.Click
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary")
                Else
                    SSCPFacAssign = Nothing
                    If SSCPFacAssign Is Nothing Then SSCPFacAssign = New SSCPFacAssignment
                    SSCPFacAssign.txtFacilityName.Text = Me.txtFacilityName.Text
                    SSCPFacAssign.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPFacAssign.Show()
                    SSCPFacAssign.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiSSCPNewWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPNewWork.Click
        Try

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    SSCPEngWork = Nothing
                    If SSCPEngWork Is Nothing Then SSCPEngWork = New SSCPWorkEnTry
                    SSCPEngWork.txtFacilityName.Text = Me.txtFacilityName.Text
                    SSCPEngWork.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPEngWork.Show()
                    SSCPEngWork.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ViewFCE()
        Dim AirProgramCodes As String = ""

        Try

            If chbAPC0.Checked = True Then
                AirProgramCodes = "0 - SIP" & vbCrLf
            End If
            If chbAPC1.Checked = True Then
                AirProgramCodes = AirProgramCodes & "1 - Federal SIP" & vbCrLf
            End If
            If chbAPC3.Checked = True Then
                AirProgramCodes = AirProgramCodes & "3 - Non-Federal SIP" & vbCrLf
            End If
            If chbAPC4.Checked = True Then
                AirProgramCodes = AirProgramCodes & "4 - CFC Tracking" & vbCrLf
            End If
            If chbAPC6.Checked = True Then
                AirProgramCodes = AirProgramCodes & "6 - PSD" & vbCrLf
            End If
            If chbAPC7.Checked = True Then
                AirProgramCodes = AirProgramCodes & "7 - NSR" & vbCrLf
            End If
            If chbAPC8.Checked = True Then
                AirProgramCodes = AirProgramCodes & "8 - NESHAP" & vbCrLf
            End If
            If chbAPC9.Checked = True Then
                AirProgramCodes = AirProgramCodes & "9 - NSPS" & vbCrLf
            End If
            If chbAPCF.Checked = True Then
                AirProgramCodes = AirProgramCodes & "F - FESOP" & vbCrLf
            End If
            If chbAPCA.Checked = True Then
                AirProgramCodes = AirProgramCodes & "A - Acid Precipitation" & vbCrLf
            End If
            If chbAPCI.Checked = True Then
                AirProgramCodes = AirProgramCodes & "I - Native American" & vbCrLf
            End If
            If chbAPCM.Checked = True Then
                AirProgramCodes = AirProgramCodes & "M - MACT" & vbCrLf
            End If
            If chbAPCV.Checked = True Then
                AirProgramCodes = AirProgramCodes & "V - Title V Permit" & vbCrLf
            End If
            If AirProgramCodes = "" Then
                AirProgramCodes = "No Air Program Codes available" & vbCrLf
            End If

            AirProgramCodes = Mid(AirProgramCodes, 1, (Len(AirProgramCodes) - 2))


            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                If txtFacilityName.Text = "" Then
                    MsgBox("Please verify that the AIRS Number is correct", MsgBoxStyle.Information, "Facility Summary Warning")
                Else
                    SSCPFCE = Nothing
                    If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                    SSCPFCE.txtAirsNumber.Text = Me.mtbAIRSNumber.Text
                    SSCPFCE.txtOrigin.Text = "Facility Summary"
                    SSCPFCE.Show()
                    SSCPFCE.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiSSCPFCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSSCPFCE.Click
        Try

            ViewFCE()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#End Region
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try

            pnltemp = Panel1.Text
            Panel1.Text = ""
            If mtbAIRSNumber.Text.Length = 8 Then
                mtbAIRSNumber.BackColor = Color.White
                LoadData()
                loadCboEIYear()
            Else
                mtbAIRSNumber.BackColor = Color.Tomato
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        Panel1.Text = pnltemp
        ProgressBar.Value = 0
    End Sub

    Private Sub llbClosePrintTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClosePrintTestReport.LinkClicked
        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "Select " & connNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & connNameSpace & ".ISMPDocumentType, " & connNameSpace & ".ISMPReportInformation " & _
                 "where " & connNameSpace & ".ISMPReportInformation.strDocumentType = " & connNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    ISMPCloseAndPrint = Nothing
                    If ISMPCloseAndPrint Is Nothing Then ISMPCloseAndPrint = New ISMPClosePrint
                    ISMPCloseAndPrint.txtTestReportType.Text = dr.Item("strDocumentType")
                    ISMPCloseAndPrint.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPCloseAndPrint.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    ISMPCloseAndPrint.txtFacilityName.Text = txtFacilityName.Text
                    ISMPCloseAndPrint.txtOrigin.Text = "Facility Summary"
                    ISMPCloseAndPrint.Show()
                    ISMPCloseAndPrint.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiLookUpTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiLookUpTool.Click
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

        End Try

    End Sub
    Private Sub mmiISMPAddMemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPAddMemo.Click
        Try

            ISMPMemoEdit = Nothing
            If ISMPMemoEdit Is Nothing Then ISMPMemoEdit = New ISMPMemo
            ISMPMemoEdit.txtReferenceNumber.Text = Me.txtReferenceNumber.Text
            ISMPMemoEdit.Show()
            ISMPMemoEdit.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiISMPClosePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiISMPClosePrint.Click
        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "Select " & connNameSpace & ".ISMPDocumentType.strDocumentType " & _
                 "from " & connNameSpace & ".ISMPDocumentType, " & connNameSpace & ".ISMPReportInformation " & _
                 "where " & connNameSpace & ".ISMPReportInformation.strDocumentType = " & connNameSpace & ".ISMPDocumentType.strKey and " & _
                 "strReferenceNumber = '" & txtReferenceNumber.Text & "'"
                Dim cmd As New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim dr As OracleDataReader = cmd.ExecuteReader
                Dim recExist As Boolean = dr.Read
                If recExist = True Then
                    ISMPCloseAndPrint = Nothing
                    If ISMPCloseAndPrint Is Nothing Then ISMPCloseAndPrint = New ISMPClosePrint
                    ISMPCloseAndPrint.txtTestReportType.Text = dr.Item("strDocumentType")
                    ISMPCloseAndPrint.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPCloseAndPrint.txtAIRSNumber.Text = mtbAIRSNumber.Text
                    ISMPCloseAndPrint.txtFacilityName.Text = txtFacilityName.Text
                    ISMPCloseAndPrint.txtOrigin.Text = "Facility Summary"
                    ISMPCloseAndPrint.Show()
                    ISMPCloseAndPrint.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiEditContactInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiEditContactInformation.Click
        Try

            If EditContacts Is Nothing Then
                If EditContacts Is Nothing Then EditContacts = IAIPEditContacts
                EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                EditContacts.Show()
            Else
                EditContacts.Dispose()
                EditContacts = New IAIPEditContacts
                If EditContacts Is Nothing Then EditContacts = New IAIPEditContacts
                EditContacts.txtAIRSNumber.Text = mtbAIRSNumber.Text
                EditContacts.Show()
            End If
            EditContacts.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiStandard.Click
        Try

            If mmiStandard.Checked = True Then
                mmiStandard.Checked = False
            Else
                mmiStandard.Checked = True
            End If
            LoadToolBars()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiSubISMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSubISMP.Click
        Try

            If mmiSubISMP.Checked = True Then
                mmiSubISMP.Checked = False
            Else
                mmiSubISMP.Checked = True
            End If
            LoadToolBars()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiSubSSCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSubSSCP.Click
        Try

            If mmiSubSSCP.Checked = True Then
                mmiSubSSCP.Checked = False
            Else
                mmiSubSSCP.Checked = True
            End If
            LoadToolBars()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiSubAddEditContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiSubAddEditContacts.Click
        Try

            If mmiSubAddEditContacts.Checked = True Then
                mmiSubAddEditContacts.Checked = False
            Else
                mmiSubAddEditContacts.Checked = True
            End If
            LoadToolBars()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Permission Verifiers"
    Private Sub chbAPC0_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC0.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC0.Enabled = False
            'Else
            '    chbAPC0.Enabled = False
            'End If
            'chbAPC0.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC1.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC1.Enabled = False
            'Else
            '    chbAPC1.Enabled = False
            'End If
            'chbAPC1.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC3.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC3.Enabled = False
            'Else
            '    chbAPC3.Enabled = False
            'End If
            'chbAPC3.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC4.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC4.Enabled = False
            'Else
            '    chbAPC4.Enabled = False
            'End If
            'chbAPC4.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC6_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC6.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC6.Enabled = False
            'Else
            '    chbAPC6.Enabled = False
            'End If
            'chbAPC6.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC7_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC7.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC7.Enabled = False
            'Else
            '    chbAPC7.Enabled = False
            'End If
            'chbAPC7.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC8_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC8.MouseDown
        Try

            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC8.Enabled = False
            'Else
            '    chbAPC8.Enabled = False
            'End If
            'chbAPC8.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC9_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPC9.MouseDown
        Try
            'chbAPC9.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPC9.Enabled = False
            'Else
            '    chbAPC9.Enabled = False
            'End If
            '  chbAPC9.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCA_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPCA.MouseDown
        Try
            '  chbAPCA.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPCA.Enabled = False
            'Else
            '    chbAPCA.Enabled = False
            'End If
            ' chbAPCA.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCF_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPCF.MouseDown
        Try
            '  chbAPCF.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPCF.Enabled = False
            'Else
            '    chbAPCF.Enabled = False
            'End If
            ' chbAPCF.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCI_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPCI.MouseDown
        Try
            '   chbAPCI.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPCI.Enabled = False
            'Else
            '    chbAPCI.Enabled = False
            'End If
            ' chbAPCI.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCM_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPCM.MouseDown
        Try
            '   chbAPCM.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPCM.Enabled = False
            'Else
            '    chbAPCM.Enabled = False
            'End If
            '  chbAPCM.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCV_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbAPCV.MouseDown
        Try
            ' chbAPCV.Enabled = False
            'If (UserUnit = "---" And UserBranch = "1") Or UserProgram = "5" Then
            '    chbAPCV.Enabled = False
            'Else
            '    chbAPCV.Enabled = False
            'End If
            '  chbAPCV.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbFeesOperating_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbFeesOperating.MouseDown
        Try

            chbFeesOperating.Enabled = False
            chbFeesOperating.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbFeesPart70_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbFeesPart70.MouseDown
        Try

            chbFeesPart70.Enabled = False
            chbFeesPart70.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbNSPSExempt_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbNSPSExempt.MouseDown
        Try

            chbNSPSExempt.Enabled = False
            chbNSPSExempt.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbHAPsMajor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbHAPsMajor.MouseDown
        Try

            chbHAPsMajor.Enabled = False
            chbHAPsMajor.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbHAPsMajor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbHAPsMajor.MouseUp
        Try

            chbHAPsMajor.Enabled = False
            chbHAPsMajor.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbNSRMajor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbNSRMajor.MouseDown
        Try

            chbNSRMajor.Enabled = False
            chbNSRMajor.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbNSRMajor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chbNSRMajor.MouseUp
        Try

            chbNSRMajor.Enabled = False
            chbNSRMajor.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
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

        End Try

    End Sub
    Private Sub mmiEditData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiEditData.Click
        Try

            SaveAll()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Try


            EditAirProgramPollutants = Nothing
            If EditAirProgramPollutants Is Nothing Then EditAirProgramPollutants = New IAIPEditAirProgramPollutants
            EditAirProgramPollutants.txtAirsNumber.Text = Me.mtbAIRSNumber.Text
            EditAirProgramPollutants.Show()
            EditAirProgramPollutants.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCDSForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCDSForm.Click
        Try
            ' 
            'CDS = Nothing
            'If CDS Is Nothing Then CDS = New SSPP_CDS_CodingSheet
            'CDS.Show()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmisubSSPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmisubSSPP.Click
        Try

            LoadToolBars()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mtbAIRSNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mtbAIRSNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then


                pnltemp = Panel1.Text
                Panel1.Text = ""
                If mtbAIRSNumber.Text.Length = 8 Then
                    LoadData()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

        Panel1.Text = pnltemp
        ProgressBar.Value = 0
    End Sub
    Private Sub btnOpenSubpartEditior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenSubpartEditior.Click
        Try


            If EditSubParts Is Nothing Then
                If EditSubParts Is Nothing Then EditSubParts = New IAIPEditSubParts
            Else
                EditSubParts.Dispose()
                EditSubParts = New IAIPEditSubParts
            End If
            EditSubParts.Show()
            EditSubParts.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            If mtbAIRSNumber.Text <> "" Then
                EditSubParts.txtAIRSNumber.Text = Me.mtbAIRSNumber.Text
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPC0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC0.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC8.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC9.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPCM.CheckedChanged
        Try

            EditSubPart()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Jings Code"
    Sub loadCboEIYear()
        Try
            cboEIYear.Items.Clear()

            SQL = "select distinct(strInventoryYear)  as EIYear " & _
            "from " & connNameSpace & ".EISI " & _
            "where strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " & _
            "order by EIYear desc "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                cboEIYear.Items.Add(dr.Item("EIYear"))
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetData.Click
        Try
            If mtbAIRSNumber.Text <> "" Then
                chkNotNonAttain.Checked = False
                chkLessThan25.Checked = False

                'If cboEIYear.Text <> "" Then
                '    inventoryYear = CInt(cboEIYear.Text)
                'Else
                '    inventoryYear = Now.Year
                '    cboEIYear.Text = Now.Year
                'End If

                SQL = "Select * from " & connNameSpace & ".eiSI where strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " ' & _ 
                '"and strInventoryYear = '" & inventoryYear & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Select " & _
                    "distinct(EIEM.strInventoryYear),  " & _
                    "case  " & _
                    "when COTable.TotalEmissions is Null then  0 " & _
                    "else COTable.TotalEmissions " & _
                    "End CO,  " & _
                    "case  " & _
                    "when LeadTable.TotalEmissions is Null then 0 " & _
                    "else LeadTable. TotalEmissions " & _
                    "END Lead,   " & _
                    "case  " & _
                    "when NH3Table.TotalEmissions is Null then 0 " & _
                    "else NH3Table.TotalEmissions  " & _
                    "END NH3,   " & _
                    "case  " & _
                    "When NOXTable.TotalEmissions is Null then 0 " & _
                    "else NOXTable.TotalEmissions  " & _
                    "END NOX,  " & _
                    "case  " & _
                    "when PMTable.TotalEmissions is null then 0  " & _
                    "else PMTable.TotalEmissions  " & _
                    "end PM,   " & _
                    "case  " & _
                    "when PM10Table.TotalEmissions is NUll then 0  " & _
                    "else PM10Table.TotalEmissions  " & _
                    "end PM10,  " & _
                    "case  " & _
                    "when PM25Table.TotalEmissions is null then 0  " & _
                    "else PM25Table.TotalEmissions  " & _
                    "end PM25,  " & _
                    "case  " & _
                    "when SO2Table.TotalEmissions is NUll then 0  " & _
                    "else SO2Table.TotalEmissions  " & _
                    "End SO2,  " & _
                    "case  " & _
                    "when VOCTable.TotalEmissions is Null then 0  " & _
                    "else VOCTable.TotalEmissions  " & _
                    "end VOC,  " & _
                    "case  " & _
                    "when PMFILTable.TotalEmissions is Null then 0  " & _
                    "else PMFILTable.TotalEmissions  " & _
                    "end PMFIL " & _
                    "from " & connNameSpace & ".EIEM,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'CO'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) COTable,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = '7439921'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) LeadTable,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'NH3'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NH3Table,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'NOX'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) NOXTable,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-PRI'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMTable,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM10-PMI'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM10Table,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM25-PMI'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PM25Table,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'SO2'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) SO2Table,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'VOC'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) VOCTable,  " & _
                    "(Select  " & _
                    "" & connNameSpace & ".EIEM.strPollutantCode as PollutantCode,   " & _
                    "sum(" & connNameSpace & ".EIEM.dblEmissionNumericValue) as TotalEmissions,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear,  " & _
                    "strStateFacilityIdentifier  " & _
                    "from " & connNameSpace & ".EIEM   " & _
                    "where " & connNameSpace & ".EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "'  " & _
                    "and strPollutantCode = 'PM-FIL'  " & _
                    "group by " & connNameSpace & ".EIEM.strPollutantCode,  " & _
                    "" & connNameSpace & ".EIEM.strInventoryYear, strStateFacilityIdentifier) PMFILTable " & _
                    "where EIEM.strInventoryYear = COTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = LeadTable.strInventoryYear (+)   " & _
                    "and EIEM.strInventoryYear = NH3Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = NOXTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM10Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PM25Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = SO2Table.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = VOCTable.strInventoryYear  (+)  " & _
                    "and EIEM.strInventoryYear = PMFILTable.strInventoryYear  (+) " & _
                    "and EIEM.strStateFacilityIdentifier = '" & mtbAIRSNumber.Text & "' " & _
                    "order by EIEM.strInventoryYear DESC "

                    ds = New DataSet
                    da = New OracleDataAdapter(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    da.Fill(ds, "EM")
                    dgvEIData.DataSource = ds
                    dgvEIData.DataMember = "EM"

                    dgvEIData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvEIData.ReadOnly = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.AllowUserToOrderColumns = True
                    dgvEIData.RowHeadersVisible = False
                    dgvEIData.Columns("strInventoryYear").HeaderText = "Year"
                    dgvEIData.Columns("strInventoryYear").DisplayIndex = 0
                    dgvEIData.Columns("strInventoryYear").Width = 50
                    dgvEIData.Columns("CO").HeaderText = "CO"
                    dgvEIData.Columns("CO").DisplayIndex = 1
                    dgvEIData.Columns("CO").Width = 50
                    dgvEIData.Columns("LEAD").HeaderText = "LEAD"
                    dgvEIData.Columns("LEAD").DisplayIndex = 2
                    dgvEIData.Columns("LEAD").Width = 50
                    dgvEIData.Columns("NH3").HeaderText = "NH3"
                    dgvEIData.Columns("NH3").DisplayIndex = 3
                    dgvEIData.Columns("NH3").Width = 50
                    dgvEIData.Columns("NOX").HeaderText = "NOX"
                    dgvEIData.Columns("NOX").DisplayIndex = 4
                    dgvEIData.Columns("NOX").Width = 50
                    dgvEIData.Columns("PM").HeaderText = "PM"
                    dgvEIData.Columns("PM").DisplayIndex = 5
                    dgvEIData.Columns("PM").Width = 50
                    dgvEIData.Columns("PM10").HeaderText = "PM-10"
                    dgvEIData.Columns("PM10").DisplayIndex = 6
                    dgvEIData.Columns("PM10").Width = 50
                    dgvEIData.Columns("PM25").HeaderText = "PM-2.5"
                    dgvEIData.Columns("PM25").DisplayIndex = 7
                    dgvEIData.Columns("PM25").Width = 50
                    dgvEIData.Columns("SO2").HeaderText = "SO2"
                    dgvEIData.Columns("SO2").DisplayIndex = 8
                    dgvEIData.Columns("SO2").Width = 50
                    dgvEIData.Columns("VOC").HeaderText = "VOC"
                    dgvEIData.Columns("VOC").DisplayIndex = 9
                    dgvEIData.Columns("VOC").Width = 50
                    dgvEIData.Columns("PMFIL").HeaderText = "PM-FIL"
                    dgvEIData.Columns("PMFIL").DisplayIndex = 10
                    dgvEIData.Columns("PMFIL").Width = 50
                Else
                    MsgBox("There is no data available", MsgBoxStyle.OkOnly, "No EI data for this year, choose another year")
                    chkNotNonAttain.Checked = False
                End If

                If cboEIYear.Text <> "" Then
                    inventoryYear = CInt(cboEIYear.Text)
                Else
                    'inventoryYear = Now.Year
                    'cboEIYear.Text = Now.Year
                End If

                SQL = "select * " & _
                "from " & connNameSpace & ".ESSchema " & _
                "where strAirsNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and intESYear = '" & inventoryYear & "' "

                If conn.State = ConnectionState.Open Then
                Else
                    conn.Open()
                End If

                Dim county As String = Mid(mtbAIRSNumber.Text, 1, 3)

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    If county = "057" Or county = "063" Or county = "067" Or county = "077" Or county = "089" _
                                                Or county = "097" Or county = "113" Or county = "117" Or county = "121" _
                                                Or county = "135" Or county = "151" Or county = "223" Or county = "247" Then
                        SQL = "Select dblVOCEmission, dblNOXEmission, strOptOut " & _
                        "from " & connNameSpace & ".ESSchema " & _
                        "where intESYear = '" & inventoryYear & "' " & _
                        "and strAirsNumber = '0413" & mtbAIRSNumber.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If dr.Item("strOptOut") = "YES" Then
                                chkLessThan25.Checked = True
                            Else
                                chkLessThan25.Checked = False
                                txtNOXEmission.Text = dr.Item("dblNOXEmission")
                                txtVOCEmission.Text = dr.Item("dblVOCEmission")
                            End If
                        End While
                        dr.Close()
                    Else
                        chkNotNonAttain.Checked = True
                    End If
                Else
                    chkNotNonAttain.Checked = True
                    txtNOXEmission.Text = ""
                    txtVOCEmission.Text = ""
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ExporttoExcel()
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelApp As New Excel.Application
        Dim i As Integer
        Dim j As Integer

        Try

            If dgvEIData.RowCount <> 0 Then
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()


                    For i = 0 To dgvEIData.ColumnCount - 1
                        .Cells(1, i + 1) = dgvEIData.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvEIData.ColumnCount - 1
                        For j = 0 To dgvEIData.RowCount - 2
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvEIData.Item(i, j).Value.ToString
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
        Finally

        End Try


    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            ExporttoExcel()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
#End Region
    Private Sub llbViewGoogleMap_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewGoogleMap.LinkClicked
        Try

            Dim StreetAddress As String = "4244 International Parkway"
            Dim City As String = "Atlanta"
            Dim State As String = "GA"
            Dim ZipCode As String = "30354"
            Dim URL As String = ""


            If txtStreetAddress.Text <> "" Then
                StreetAddress = txtStreetAddress.Text
            End If
            If txtFacilityCity.Text <> "" Then
                City = txtFacilityCity.Text
            End If
            If txtFacilityState.Text <> "" Then
                State = txtFacilityState.Text
            End If
            If txtFacilityZipCode.Text <> "" Then
                ZipCode = txtFacilityZipCode.Text
            End If

            URL = "http://maps.google.com/maps?q=" & StreetAddress & "+" & _
                      City & "+" & State & "+" & ZipCode & "&z=14"

            System.Diagnostics.Process.Start(URL)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Ronalds Code"
    Private Sub btnExportEIExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEIExport.Click
        Try
            If mtbAIRSNumber.Text <> "" And Me.cboEIYear.Text <> "" Then
                BindDataGridSI()
                BindDataGridEU()
                BindDataGridER()
                BindDataGridEP()
                BindDataGridEM()
                export2Excel()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#Region " Bind Data Grid View Routines "

    Private Sub BindDataGridSI()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strFacilityName, " & _
                            "strLocationAddress, " & _
                            "strCity, " & _
                            "strState, " & _
                            "strZipCode, " & _
                            "strCounty, " & _
                            "dblXCoordinate, " & _
                            "dblYCoordinate, " & _
                            "strHorizontalCollectionCode, " & _
                            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
                               "from " & connNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
                               "where " & connNameSpace & ".EISI.STRHORIZONTALCOLLECTIONCODE = " & _
                               "" & connNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
                            "strHorizontalReferenceCode, " & _
                            "strHorizontalAccuracyMeasure, " & _
                            "(Select STRHORIZONTALREFERENCEDESC " & _
                               "from " & connNameSpace & ".EILOOKUPHORIZREFDATUM " & _
                               "where " & connNameSpace & ".EISI.STRHORIZONTALREFERENCECODE = " & _
                               "" & connNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc, " & _
                            "strContactPrefix, " & _
                            "strContactFirstName, " & _
                            "strContactLastName, " & _
                            "strContactTitle, " & _
                            "strContactEmail, " & _
                            "strContactPhoneNumber1, " & _
                            "strContactPhoneNumber2, " & _
                            "strContactFaxNumber, " & _
                            "strContactCompanyName, " & _
                            "strContactAddress1, " & _
                            "strContactCity, " & _
                            "strContactState, " & _
                            "strContactZipCode, " & _
                            "strSiteDescription, " & _
                            "strSICPrimary, " & _
                            "strNAICSPrimary " & _
                     "from " & connNameSpace & ".eiSI where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvSI.Rows.Clear()
            dgvSI.Columns.Clear()

            dgvSI.Columns.Add("strFacilityName", "Facility")
            dgvSI.Columns.Add("strLocationAddress", "Facility Location")
            dgvSI.Columns.Add("strCity", "City")
            dgvSI.Columns.Add("strState", "State")
            dgvSI.Columns.Add("strZipCode", "Zip Code")
            dgvSI.Columns.Add("strCounty", "County")
            dgvSI.Columns.Add("dblXCoordinate", "Longitude")
            dgvSI.Columns.Add("dblYCoordinate", "Latitude")
            dgvSI.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvSI.Columns.Add("HMCdesc", "Horizontal Collection Desc")
            dgvSI.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvSI.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvSI.Columns.Add("HDRCdesc", "Horizontal Datum Reference Desc")
            dgvSI.Columns.Add("strContactPrefix", "Contact Prefix")
            dgvSI.Columns.Add("strContactFirstName", "Contact First Name")
            dgvSI.Columns.Add("strContactLastName", "Contact Last Name")
            dgvSI.Columns.Add("strContactTitle", "Contact Title")
            dgvSI.Columns.Add("strContactEmail", "Contact Email")
            dgvSI.Columns.Add("strContactPhoneNumber1", "Contact Phone Number")
            dgvSI.Columns.Add("strContactPhoneNumber2", "Contact Phone Other")
            dgvSI.Columns.Add("strContactFaxNumber", "Contact Fax Number")
            dgvSI.Columns.Add("strContactCompanyName", "Contact Company Name")
            dgvSI.Columns.Add("strContactAddress1", "Contact Address")
            dgvSI.Columns.Add("strContactCity", "Contact City")
            dgvSI.Columns.Add("strContactState", "Contact State")
            dgvSI.Columns.Add("strContactZipCode", "Contact Zip Code")
            dgvSI.Columns.Add("strSiteDescription", "Site Description")
            dgvSI.Columns.Add("strSICPrimary", "SIC Primary")
            dgvSI.Columns.Add("strNAICSPrimary", "NAICS Primary")

            dgvSI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvSI.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvSI.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvSI.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEU()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionUnitID, " & _
                         "sngDesignCapacity, " & _
                         "strDesignCapUnitNum, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & connNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & connNameSpace & ".EIEU.strDesignCapUnitNum = " & _
                               "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as numDesc, " & _
                         "strDesignCapUnitDenom, " & _
                         "(Select STRUNITDESCRIPTION " & _
                               "from " & connNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & connNameSpace & ".EIEU.strDesignCapUnitDenom = " & _
                               "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as denomDesc, " & _
                         "sngMaxNameplateCapacity, " & _
                         "strEmissionUnitDesc " & _
                    "from " & connNameSpace & ".eiEU where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEU.Rows.Clear()
            dgvEU.Columns.Clear()

            dgvEU.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEU.Columns.Add("sngDesignCapacity", "Design Capacity")
            dgvEU.Columns.Add("strDesignCapUnitNum", "Design Capacity Unit Numerator")
            dgvEU.Columns.Add("numDesc", "Design Capacity Unit Numerator Desc")
            dgvEU.Columns.Add("strDesignCapUnitDenom", "Design Capacity Unit Denominator")
            dgvEU.Columns.Add("denomDesc", "Design Capacity Unit Denominator Desc")
            dgvEU.Columns.Add("sngMaxNameplateCapacity", "Max Name plate Capacity")
            dgvEU.Columns.Add("strEmissionUnitDesc", "Emission Unit Desc")

            dgvEU.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEU.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEU.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEU.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridER()
        Try
            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strEmissionReleasePointID, " & _
            "strEmissionReleaseType, " & _
            "(Select STREMISSIONTYPEDESC " & _
            "from " & connNameSpace & ".EILOOKUPEMISSIONTYPES " & _
            "where " & connNameSpace & ".EIER.STREMISSIONRELEASETYPE = " & _
            "" & connNameSpace & ".EILOOKUPEMISSIONTYPES.STREMISSIONTYPECODE) as stackType, " & _
            "sngStackHeight, " & _
            "sngStackDiameter, " & _
            "sngExitGasTemperature, " & _
            "sngExitGasVelocity, " & _
            "sngExitGasFlowRate, " & _
            "dblXCoordinate, " & _
            "dblYCoordinate, " & _
            "strEmissionReleasePtDesc, " & _
            "strHorizontalCollectionCode, " & _
            "(Select STRHORIZCOLLECTIONMETHODDESC " & _
            "from " & connNameSpace & ".EILOOKUPHORIZCOLMETHOD " & _
            "where " & connNameSpace & ".EIER.STRHORIZONTALCOLLECTIONCODE = " & _
            "" & connNameSpace & ".EILOOKUPHORIZCOLMETHOD.STRHORIZCOLLECTIONMETHODCODE) as HMCdesc, " & _
            "strHorizontalAccuracyMeasure, " & _
            "strHorizontalReferenceCode, " & _
            "(Select STRHORIZONTALREFERENCEDESC " & _
            "from " & connNameSpace & ".EILOOKUPHORIZREFDATUM " & _
            "where " & connNameSpace & ".EIER.STRHORIZONTALREFERENCECODE = " & _
            "" & connNameSpace & ".EILOOKUPHORIZREFDATUM.STRHORIZONTALREFERENCEDATUM) as HDRCdesc " & _
            "from " & connNameSpace & ".eiER where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvER.Rows.Clear()
            dgvER.Columns.Clear()

            dgvER.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvER.Columns.Add("strEmissionReleaseType", "Emission Release Type")
            dgvER.Columns.Add("stackType", "Stack Type")
            dgvER.Columns.Add("sngStackHeight", "Stack Height")
            dgvER.Columns.Add("sngStackDiameter", "Stack Diameter")
            dgvER.Columns.Add("sngExitGasTemperature", "Exit Gas Temperature")
            dgvER.Columns.Add("sngExitGasVelocity", "Exit Gas Velocity")
            dgvER.Columns.Add("sngExitGasFlowRate", "Exit Gas Flow Rate")
            dgvER.Columns.Add("dblXCoordinate", "Longitude")
            dgvER.Columns.Add("dblYCoordinate", "Latitude")
            dgvER.Columns.Add("strEmissionReleasePtDesc", "Emission Release Point Desc")
            dgvER.Columns.Add("strHorizontalCollectionCode", "Horizontal Collection Code")
            dgvER.Columns.Add("HMCdesc", "Horizontal Collection Code Desc")
            dgvER.Columns.Add("strHorizontalAccuracyMeasure", "Horizontal Accuracy Measure")
            dgvER.Columns.Add("strHorizontalReferenceCode", "Horizontal Datum Reference Code")
            dgvER.Columns.Add("HDRCdesc", "Horizontal Datum Reference Code Desc")

            dgvER.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvER.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvER.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvER.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEP()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select strSCC, " & _
                            "strEmissionProcessDescription, " & _
                            "strEmissionUnitID, " & _
                            "strEmissionReleasePointID, " & _
                            "strProcessID, " & _
                            "intWinterThroughputPCT, " & _
                            "intSpringThroughputPCT, " & _
                            "intSummerThroughputPCT, " & _
                            "intFallThroughputPCT, " & _
                            "intAnnualAvgDaysPerWeek, " & _
                            "intAnnualAvgWeeksPerYear, " & _
                            "intAnnualAvgHoursPerDay, " & _
                            "intAnnualAvgHoursPerYear, " & _
                            "sngHeatContent, " & _
                            "sngSulfurContent, " & _
                            "sngAshContent, " & _
                            "sngDailySummerProcessTPut, " & _
                            "strDailySummerProcessTPutNum, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & connNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & connNameSpace & ".EIEP.strDailySummerProcessTPutNum = " & _
                               "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as DailySummerTputNumDesc, " & _
                            "sngActualThroughput, " & _
                            "strThroughputUnitNumerator, " & _
                            "(Select STRUNITDESCRIPTION " & _
                               "from " & connNameSpace & ".EILOOKUPUNITCODES " & _
                               "where " & connNameSpace & ".EIEP.strThroughputUnitNumerator = " & _
                               "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as TputNumDesc, " & _
                            "strStartTime " & _
                       "from " & connNameSpace & ".eiEP " & _
                      "where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEP.Rows.Clear()
            dgvEP.Columns.Clear()

            dgvEP.Columns.Add("strSCC", "SCC")
            dgvEP.Columns.Add("strEmissionProcessDescription", "Emission Process Description")
            dgvEP.Columns.Add("strEmissionUnitID", "Emission Unit ID")
            dgvEP.Columns.Add("strEmissionReleasePointID", "Emission Release Point ID")
            dgvEP.Columns.Add("strProcessID", "Process ID")
            dgvEP.Columns.Add("intWinterThroughputPCT", "Winter Throughput Percent")
            dgvEP.Columns.Add("intSpringThroughputPCT", "Spring Throughput Percent")
            dgvEP.Columns.Add("intSummerThroughputPCT", "Summer Throughput Percent")
            dgvEP.Columns.Add("intFallThroughputPCT", "Fall Throughput Percent")
            dgvEP.Columns.Add("intAnnualAvgDaysPerWeek", "Annual Average Days Per Week")
            dgvEP.Columns.Add("intAnnualAvgWeeksPerYear", "Annual Average Weeks Per Year")
            dgvEP.Columns.Add("intAnnualAvgHoursPerDay", "Annual Average Hours Per Day")
            dgvEP.Columns.Add("intAnnualAvgHoursPerYear", "Annual Average Hours Per Year")
            dgvEP.Columns.Add("sngHeatContent", "Heat Content")
            dgvEP.Columns.Add("sngSulfurContent", "Sulfur Content")
            dgvEP.Columns.Add("sngAshContent", "Ash Content")
            dgvEP.Columns.Add("sngDailySummerProcessTPut", "Daily Summer Process Throughput")
            dgvEP.Columns.Add("strDailySummerProcessTPutNum", "Daily Summer Process Throughput Numerator")
            dgvEP.Columns.Add("DailySummerTputNumDesc", "Daily Summer Process Throughput Numerator Desc")
            dgvEP.Columns.Add("sngActualThroughput", "Actual Throughput")
            dgvEP.Columns.Add("strThroughputUnitNumerator", "Actual Throughput Unit Numerator")
            dgvEP.Columns.Add("TputNumDesc", "Actual Throughput Unit Numerator Desc")
            dgvEP.Columns.Add("strStartTime", "Start Time")


            dgvEP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEP.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEP.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEP.Rows.Add(objCells)
            End While

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Sub
    Private Sub BindDataGridEM()

        Try

            Dim SQL As String
            Dim AirsNumber As String = "0413" & mtbAIRSNumber.Text
            Dim inventoryYear As String = cboEIYear.SelectedItem
            Dim airsYear As String
            Dim dr As OracleDataReader
            ds = New DataSet

            airsYear = AirsNumber & inventoryYear

            SQL = "select STREMISSIONUNITID, "
            SQL += "STREMISSIONRELEASEPOINTID, "
            SQL += "STRPROCESSID, "
            SQL += "strPollutantCode, "
            SQL += "(Select STRPOLLUTANTDESC "
            SQL += "from " & connNameSpace & ".EILOOKUPPOLLUTANTCODES "
            SQL += "where " & connNameSpace & ".EIEM.STRPOLLUTANTCODE = "
            SQL += "" & connNameSpace & ".EILOOKUPPOLLUTANTCODES.STRPOLLUTANTCODE) as pollutantDesc, "
            SQL += "DBLEMISSIONNUMERICVALUE, "
            SQL += "STREMISSIONUNITNUMERATOR, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & connNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & connNameSpace & ".EIEM.STREMISSIONUNITNUMERATOR = "
            SQL += "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as EMISSIONUNITNUMERATORDesc, "
            SQL += "sngFactorNumericValue, "
            SQL += "strFactorUnitNumerator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & connNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & connNameSpace & ".EIEM.strFactorUnitNumerator = "
            SQL += "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitNumeratorDesc, "
            SQL += "strFactorUnitDenominator, "
            SQL += "(Select STRUNITDESCRIPTION "
            SQL += "from " & connNameSpace & ".EILOOKUPUNITCODES "
            SQL += "where " & connNameSpace & ".EIEM.strFactorUnitDenominator = "
            SQL += "" & connNameSpace & ".EILOOKUPUNITCODES.STRUNITCODE) as FactorUnitDenominatorDesc, "
            SQL += "strEmissionCalculationMetCode, "
            SQL += "(Select STREMISSIONCALCMETHODDESC "
            SQL += "from " & connNameSpace & ".EILOOKUPEMISSIONCALCMETHOD "
            SQL += "where " & connNameSpace & ".EIEM.strEmissionCalculationMetCode = "
            SQL += "" & connNameSpace & ".EILOOKUPEMISSIONCALCMETHOD.STREMISSIONCALCMETHODCODE) as EMISSIONCALCMETHODDESC, "
            SQL += "strControlStatus, "
            SQL += "strControlSystemDescription, "
            SQL += "strPrimaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & connNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & connNameSpace & ".EIEM.strPrimaryDeviceTypeCode = "
            SQL += "" & connNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as PrimaryDeviceTypeDesc, "
            SQL += "sngPrimaryPCTControlEffic, "
            SQL += "strSecondaryDeviceTypeCode, "
            SQL += "(Select STRCONTROLDEVICEDesc "
            SQL += "from " & connNameSpace & ".EILOOKUPCONTROLDEVICE "
            SQL += "where " & connNameSpace & ".EIEM.strSecondaryDeviceTypeCode = "
            SQL += "" & connNameSpace & ".EILOOKUPCONTROLDEVICE.STRCONTROLDEVICECODE) as SecondaryDeviceTypeDesc, "
            SQL += "sngPCTCaptureEfficiency, "
            SQL += "sngTotalCaptureControlEffic "
            SQL += "from " & connNameSpace & ".eiEM "
            SQL += "where strAirsYear = '" & airsYear & "'"

            'SQL = "Select * from " & connNameSpace & ".eiEM where strAirsYear = '" & airsYear & "'"

            Dim cmd As New OracleCommand(SQL, conn)
            cmd.CommandType = CommandType.Text

            If conn.State = ConnectionState.Open Then
            Else
                conn.Open()
            End If

            dr = cmd.ExecuteReader

            dgvEM.Rows.Clear()
            dgvEM.Columns.Clear()

            dgvEM.Columns.Add("STREMISSIONUNITID", "EMISSION UNIT ID")
            dgvEM.Columns.Add("STREMISSIONRELEASEPOINTID", "EMISSION RELEASE POINT ID")
            dgvEM.Columns.Add("STRPROCESSID", "PROCESS ID")
            dgvEM.Columns.Add("strPollutantCode", "Pollutant Code")
            dgvEM.Columns.Add("pollutantDesc", "pollutant Desc")
            dgvEM.Columns.Add("DBLEMISSIONNUMERICVALUE", "EMISSION NUMERIC VALUE")
            dgvEM.Columns.Add("STREMISSIONUNITNUMERATOR", "EMISSION UNIT NUMERATOR")
            dgvEM.Columns.Add("EMISSIONUNITNUMERATORDesc", "EMISSION UNIT NUMERATOR Desc")
            dgvEM.Columns.Add("sngFactorNumericValue", "Factor Numeric Value")
            dgvEM.Columns.Add("strFactorUnitNumerator", "Factor Unit Numerator")
            dgvEM.Columns.Add("FactorUnitNumeratorDesc", "Factor Unit Numerator Desc")
            dgvEM.Columns.Add("strFactorUnitDenominator", "Factor Unit Denominator")
            dgvEM.Columns.Add("FactorUnitDenominatorDesc", "Factor Unit Denominator Desc")
            dgvEM.Columns.Add("strEmissionCalculationMetCode", "Emission Calculation Method Code")
            dgvEM.Columns.Add("EMISSIONCALCMETHODDESC", "Emission Calculation Method Code Desc")
            dgvEM.Columns.Add("strControlStatus", "Control Status")
            dgvEM.Columns.Add("strControlSystemDescription", "Control System Description")
            dgvEM.Columns.Add("strPrimaryDeviceTypeCode", "Primary Device Type Code")
            dgvEM.Columns.Add("PrimaryDeviceTypeDesc", "Primary Device Type Code Desc")
            dgvEM.Columns.Add("sngPrimaryPCTControlEffic", "Primary Percentage Control Efficiency")
            dgvEM.Columns.Add("strSecondaryDeviceTypeCode", "Secondary Device Type Code")
            dgvEM.Columns.Add("SecondaryDeviceTypeDesc", "Secondary Device Type Code Desc")
            dgvEM.Columns.Add("sngPCTCaptureEfficiency", "Percent Capture Efficiency")
            dgvEM.Columns.Add("sngTotalCaptureControlEffic", "Total Capture Control Efficiency")



            dgvEM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            dgvEM.RowHeadersVisible = False

            While dr.Read
                'Get row data as an object array
                'objCells(2) means col[0], col[1], col[2]
                'which returns the first colums in the table
                Dim objCells(dgvEM.Columns.Count) As Object
                dr.GetValues(objCells)
                dgvEM.Rows.Add(objCells)
            End While

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
    Private Sub export2Excel()
        Try
            exportSI()
            'exportEU()
            'exportER()
            'exportEP()
            'exportEM()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region " Export Routines "

    Private Sub exportSI()

        Try
            'Response.AddHeader("content-disposition", "attachment;filename=EIData.xls")
            '' Set MIME type to Excel.
            'Response.ContentType = "application/vnd.ms-excel"
            '' Remove the charset from the Content-Type header.
            'Response.Charset = ""
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim col, row As Integer
            Dim x As String
            Dim y As String
            'Dim a As Integer
            'Dim b As Integer
            Dim c As Integer
            Dim d As Integer
            Dim startRow As Integer = 1



            'load SI data into Excel
            If dgvSI.RowCount <> 0 Then

                ExcelApp.SheetsInNewWorkbook = 1
                ExcelApp.Workbooks.Add()

                ExcelApp.Cells(startRow, 1).value = "Facility Information"

                'For displaying the column name in the the excel file.
                '29 columns in SI 
                'cells(x,y) x=row  y=col
                For col = 0 To dgvSI.ColumnCount - 1
                    y = dgvSI.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow + 1, col + 1).value = y
                Next

                For row = 0 To 0
                    For col = 0 To dgvSI.ColumnCount - 1
                        If IsDBNull(dgvSI.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvSI.Item(col, row).Value.ToString
                        End If
                        'x = dgvSI.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow + 2, col + 1).value = x
                    Next
                Next
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If

            End If

            txtRow.Text = startRow + 3

            'load EU data into Excel
            If dgvEU.RowCount <> 0 Then

                startRow = CInt(txtRow.Text) + 2
                ExcelApp.Cells(startRow, 1).value = "Emission Units"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEU.ColumnCount - 1
                    y = dgvEU.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvEU.ColumnCount - 1
                'b = dgvEU.RowCount - 1

                'For col = 0 To dgvEU.RowCount - 1
                '    For row = 0 To dgvEU.ColumnCount - 1
                startRow = startRow + 1
                d = dgvEU.RowCount - 2
                For row = 0 To d

                    c = dgvEU.ColumnCount - 1
                    For col = 0 To c
                        If IsDBNull(dgvEU.Item(col, row).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvEU.Item(col, row).Value.ToString
                        End If
                        'x = dgvEU.Item(col, row).Value.ToString
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load ER data into Excel
            If dgvER.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Stacks"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvER.ColumnCount - 1
                    y = dgvER.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvER.ColumnCount - 1
                'b = dgvER.RowCount - 1

                startRow = startRow + 1
                d = dgvER.RowCount - 2
                For row = 0 To d

                    c = dgvER.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvER.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvER.Item(col, row).Value) Then
                        Else
                            'x = dgvER.Item(col, row).Value.ToString
                            x = dgvER.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EP data into Excel
            If dgvEP.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Processes"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEP.ColumnCount - 1
                    y = dgvEP.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvEP.ColumnCount - 1
                'b = dgvEP.RowCount - 1

                startRow = startRow + 1
                d = dgvEP.RowCount - 2
                For row = 0 To d

                    c = dgvEP.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEP.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEP.Item(col, row).Value) Then
                        Else
                            'x = dgvEP.Item(col, row).Value.ToString
                            x = dgvEP.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            txtRow.Text = startRow + 2

            'load EM data into Excel
            If dgvEM.RowCount <> 0 Then

                startRow = CInt(txtRow.Text)
                ExcelApp.Cells(startRow, 1).value = "Pollutants and Control Equipment"
                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For col = 0 To dgvEM.ColumnCount - 1
                    y = dgvEM.Columns(col).HeaderText.ToString
                    ExcelApp.Cells(startRow, col + 1).value = y
                Next

                'a = dgvEM.ColumnCount - 1
                'b = dgvEM.RowCount - 1

                startRow = startRow + 1
                d = dgvEM.RowCount - 2
                For row = 0 To d

                    c = dgvEM.ColumnCount - 1
                    x = ""
                    For col = 0 To c
                        'If IsDBNull(dgvEM.Item(col, row).Value.ToString) Then
                        If IsDBNull(dgvEM.Item(col, row).Value) Then
                        Else
                            'x = dgvEM.Item(col, row).Value.ToString
                            x = dgvEM.Item(col, row).Value
                        End If
                        ExcelApp.Cells(startRow, col + 1).value = x
                    Next
                    startRow = startRow + 1
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

            'txtRow.Text = startRow + 2

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                MsgBox(ex.ToString)
            End If
        Finally

        End Try

    End Sub
    Private Sub exportEU()
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As Excel.Application = New Excel.ApplicationClass
            Dim i, j As Integer
            Dim x As String
            Dim y As String
            'Dim a As Integer
            'Dim b As Integer
            Dim startRow As Integer = CInt(txtRow.Text)

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If


            If dgvEU.RowCount <> 0 Then

                'ExcelApp.Worksheets.Add("EU Data")
                'ExcelApp.Worksheets("EU Data").Select()
                'ExcelApp.Worksheets("sheet1")
                'ExcelApp.Workbooks.Add()

                startRow = startRow + 1

                'For displaying the column name in the the excel file.
                For i = 0 To dgvEU.ColumnCount - 1
                    y = dgvEU.Columns(i).HeaderText.ToString
                    ExcelApp.Cells(startRow, i + 1).value = y
                Next

                'a = dgvEU.ColumnCount - 1
                'b = dgvEU.RowCount - 1




                'For i = 0 To dgvEU.RowCount - 1
                '    For j = 0 To dgvEU.ColumnCount - 1
                For j = startRow To dgvEU.RowCount + startRow
                    For i = 0 To dgvEU.ColumnCount - 1
                        If IsDBNull(dgvEU.Item(j, i).Value.ToString) Then
                            x = ""
                        Else
                            x = dgvEU.Item(j, i).Value.ToString
                        End If
                        'x = dgvEU.Item(i, j).Value.ToString
                        ExcelApp.Cells(j + 2, i + 1).value = x
                    Next
                Next

                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If


        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                MsgBox(ex.ToString)
            End If
        Finally

        End Try

    End Sub

#End Region
#End Region

    Private Sub llbViewAirPermits_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAirPermits.LinkClicked
        Try
            Dim URL As String = ""

            If mtbAIRSNumber.Text <> "" Then
                URL = "http://airpermit.dnr.state.ga.us/gaairpermits/default.aspx?AirsNumber='" & mtbAIRSNumber.Text & "'"
                System.Diagnostics.Process.Start(URL)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadFacilityWideData()
        Try
            dsFacilityWideData = New DataSet

            SQL = "select " & _
            "" & connNameSpace & ".VW_APBFacilityLocation.strAIRSnumber, " & _
            "" & connNameSpace & ".VW_APBFacilityLocation.strFacilityName, " & _
            "strFacilityStreet1, strFacilityStreet2, " & _
            "strFacilityCity, strFacilityState, " & _
            "strFacilityZipCode, " & _
            "numFacilityLongitude, numFacilityLatitude, " & _
            "strCountyName, strDistrictName, " & _
            "strOfficename, strOperationalStatus, " & _
            "strClass, strAirProgramCodes, " & _
            "strSICCode, strAttainmentStatus, " & _
            "datStartUpDate, datShutDownDate, " & _
            "strCMSMember, strPlantDescription, " & _
            "strStateProgramCodes, strNAICSCode " & _
            "from " & _
            "" & connNameSpace & ".VW_APBFacilityLocation, " & _
            "" & connNameSpace & ".VW_APBFacilityHeader " & _
            "where " & connNameSpace & ".VW_APBFacilityLocation.strAIRSNumber = " & connNameSpace & ".VW_APBFacilityHeader.strAIRSNumber " & _
            "and " & connNameSpace & ".VW_APBFacilityLocation.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "Location")

            SQL = "select distinct(strComplianceStatus) as PollutantStatus " & _
            "from " & connNameSpace & ".APBAirProgramPollutants " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "Pollutants")

            SQL = "select " & _
            "distinct(" & connNameSpace & ".OLAPUserAccess.numUserID), " & _
            "strUserType, " & _
            "(strSalutation||' '||strFirstName||' '||strLastName||', '||strTitle) as GECOContact, " & _
            "" & connNameSpace & ".OLAPUserLogIN.strUserEmail, " & _
            "strPhoneNumber, strFaxNumber, " & _
            "strCompanyName, strAddress, " & _
            "strCity, strState,  " & _
            "strZip " & _
            "from " & connNameSpace & ".OLAPUserAccess, " & connNameSpace & ".OLAPUserProfile,  " & _
            "" & connNameSpace & ".OLAPUserLogIN  " & _
            "where " & connNameSpace & ".OLAPUserAccess.numUserID = " & connNameSpace & ".OLAPUserProfile.NumUserID  " & _
            "and " & connNameSpace & ".OLAPUserAccess.numUserID = " & connNameSpace & ".OLAPUserLogIN.numUserID (+) " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "GECOContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & connNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '1%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "ISMPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & connNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '2%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "SSCPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & connNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '3%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "SSPPContacts")

            SQL = "Select strContactKey, " & _
            "(strContactPrefix||' '||strContactFirstName||' '||strContactLastName||' '||strContactSuffix||', '||strContactTitle) as ContactName, " & _
            "strContactCompanyName, " & _
            "strContactPhoneNumber1, strContactPhoneNumber2, " & _
            "strContactFaxNumber, strContactEmail, " & _
            "strContactAddress1, strContactAddress2, " & _
            "strContactCity, strContactState, " & _
            "strContactZipCode, strContactDescription " & _
            "from " & connNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and strKey like '4%' " & _
            "order by strContactKey "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "WebContacts")

            SQL = "Select (strLastName||', '||strFirstName) as SSCPEngineer, strUnitDesc " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".SSCPFacilityAssignment,  " & _
            "" & connNameSpace & ".LookUpEPDUnits  " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and numUserID = strSSCPEngineer   " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "ComplianceContact")

            SQL = "Select " & _
            "Distinct((strLastName||', '||strFirstName)) as ISMPEngineer, strUnitDesc   " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".ISMPReportInformation,   " & _
            "" & connNameSpace & ".ISMPMaster, " & connNameSpace & ".LookUpEPDUnits    " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDunits.numunitCode (+) " & _
            "and numUserID = strReviewingEngineer   " & _
            "AND " & connNameSpace & ".ISMPMaster.strReferenceNumber = " & connNameSpace & ".ISMPReportInformation.strReferenceNumber   " & _
            "and strClosed = 'True'  " & _
            "and datCompleteDate = (Select Distinct(Max(datCompleteDate)) as CompleteDate  " & _
            "from " & connNameSpace & ".ISMPReportInformation, " & connNameSpace & ".ISMPMaster  " & _
            "where " & connNameSpace & ".ISMPReportInformation.strReferenceNumber = " & connNameSpace & ".ISMPMaster.strReferenceNumber   " & _
            "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "and strClosed = 'True')  " & _
            "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "MonitoringContact")

            SQL = "select  " & _
            "Distinct((strLastName||', '||strFirstName)) as SSPPStaffResponsible, strUnitDesc   " & _
            "from " & connNameSpace & ".EPDUserProfiles, " & connNameSpace & ".SSPPApplicationMaster, " & _
            "" & connNameSpace & ".LookUpEPDUnits " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and numUserID = strStaffResponsible  " & _
            "and " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "(select distinct(max(to_number(strApplicationNumber))) as GreatestApplication  " & _
            "from " & connNameSpace & ".SSPPApplicationMaster   " & _
            "where " & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "')  " & _
            "and " & connNameSpace & ".SSPPApplicationMaster.strAIRSnumber = '0413" & mtbAIRSNumber.Text & "'  "

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "PermittingContact")

            SQL = "Select * " & _
            "from " & connNameSpace & ".VW_APBFacilityFees " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "order by intYear DESC"

            daFacilityWideData = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "Fees")


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwFacilityWideData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwFacilityWideData.DoWork
        LoadFacilityWideData()
    End Sub
    Private Sub bgwFacilityWideData_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwFacilityWideData.RunWorkerCompleted
        Dim dtFacilityWideData As New DataTable
        Dim drDSRow As DataRow
        Dim PollutantStatus As String

        dtFacilityWideData.Columns.Add("strairsnumber", GetType(System.String))
        dtFacilityWideData.Columns.Add("strfacilityname", GetType(System.String))
        dtFacilityWideData.Columns.Add("strFacilityStreet1", GetType(System.String))
        dtFacilityWideData.Columns.Add("strFacilityStreet2", GetType(System.String))
        dtFacilityWideData.Columns.Add("strFacilityCity", GetType(System.String))
        dtFacilityWideData.Columns.Add("strFacilityState", GetType(System.String))
        dtFacilityWideData.Columns.Add("strFacilityZipCode", GetType(System.String))
        dtFacilityWideData.Columns.Add("numFacilityLongitude", GetType(System.String))
        dtFacilityWideData.Columns.Add("numFacilityLatitude", GetType(System.String))
        dtFacilityWideData.Columns.Add("strCountyName", GetType(System.String))
        dtFacilityWideData.Columns.Add("strDistrictName", GetType(System.String))
        dtFacilityWideData.Columns.Add("strOfficeName", GetType(System.String))
        dtFacilityWideData.Columns.Add("strOperationalStatus", GetType(System.String))
        dtFacilityWideData.Columns.Add("strClass", GetType(System.String))
        dtFacilityWideData.Columns.Add("strAirProgramCodes", GetType(System.String))
        dtFacilityWideData.Columns.Add("strSICCode", GetType(System.String))
        dtFacilityWideData.Columns.Add("strAttainmentStatus", GetType(System.String))
        dtFacilityWideData.Columns.Add("datStartUpDate", GetType(System.String))
        dtFacilityWideData.Columns.Add("datShutDownDate", GetType(System.String))
        dtFacilityWideData.Columns.Add("strCMSMember", GetType(System.String))
        dtFacilityWideData.Columns.Add("strPlantDescription", GetType(System.String))
        dtFacilityWideData.Columns.Add("strStateProgramCodes", GetType(System.String))
        dtFacilityWideData.Columns.Add("strNAICSCode", GetType(System.String))

        For Each drDSRow In dsFacilityWideData.Tables("Location").Rows()
            If IsDBNull(drDSRow("strfacilityname")) Then
                txtFacilityName.Text = "N/A"
            Else
                txtFacilityName.Text = drDSRow("strfacilityname")
            End If
            If IsDBNull(drDSRow("strFacilityStreet1")) Then
                txtStreetAddress.Text = "N/A"
            Else
                txtStreetAddress.Text = drDSRow("strFacilityStreet1")
            End If
            If IsDBNull(drDSRow("strFacilityStreet2")) Then
                txtStreetAddress2.Text = "N/A"
            Else
                txtStreetAddress2.Text = drDSRow("strFacilityStreet2")
            End If
            If IsDBNull(drDSRow("strFacilityCity")) Then
                txtFacilityCity.Text = ""
            Else
                txtFacilityCity.Text = drDSRow("strFacilityCity")
            End If
            If IsDBNull(drDSRow("strFacilityState")) Then
                txtFacilityState.Text = ""
            Else
                txtFacilityState.Text = drDSRow("strFacilityState")
            End If
            If IsDBNull(drDSRow("strFacilityZipCode")) Then
                txtFacilityZipCode.Text = ""
            Else
                txtFacilityZipCode.Text = drDSRow("strFacilityZipCode")
            End If
            If IsDBNull(drDSRow("numFacilityLongitude")) Then
                txtFacilityLongitude.Text = ""
            Else
                txtFacilityLongitude.Text = drDSRow("numFacilityLongitude")
            End If
            If IsDBNull(drDSRow("numFacilityLatitude")) Then
                txtFacilityLatitude.Text = ""
            Else
                txtFacilityLatitude.Text = drDSRow("numFacilityLatitude")
            End If
            If IsDBNull(drDSRow("strCountyName")) Then
                txtFacilityCounty.Text = ""
            Else
                txtFacilityCounty.Text = drDSRow("strCountyName")
            End If
            If IsDBNull(drDSRow("strDistrictName")) Then
                txtDistrict.Text = ""
            Else
                txtDistrict.Text = drDSRow("strDistrictName")
            End If
            If IsDBNull(drDSRow("strOfficeName")) Then
                txtOffice.Text = ""
            Else
                txtOffice.Text = drDSRow("strOfficeName")
            End If
            If IsDBNull(drDSRow("strOperationalStatus")) Then
                txtOperationalStatus.Text = ""
            Else
                temp = drDSRow("strOperationalStatus")
                Select Case temp
                    Case "O"
                        txtOperationalStatus.Text = "O - Operational"
                    Case "P"
                        txtOperationalStatus.Text = "P - Planned"
                    Case "C"
                        txtOperationalStatus.Text = "C - Under Construction"
                    Case "T"
                        txtOperationalStatus.Text = "T - Temporarily Closed"
                    Case "X"
                        txtOperationalStatus.Text = "X - Closed/Dismantled"
                    Case "I"
                        txtOperationalStatus.Text = "I - Seasonal Operation"
                    Case Else
                        txtOperationalStatus.Text = "Unknown - Please Fix"
                End Select
            End If
            If IsDBNull(drDSRow("strClass")) Then
                txtClassification.Text = ""
            Else
                txtClassification.Text = drDSRow("strClass")
            End If
            If IsDBNull(drDSRow("strAirProgramCodes")) Then
                chbAPC0.Checked = False
                chbAPC1.Checked = False
                chbAPC3.Checked = False
                chbAPC4.Checked = False
                chbAPC6.Checked = False
                chbAPC7.Checked = False
                chbAPC8.Checked = False
                chbAPC9.Checked = False
                chbAPCA.Checked = False
                chbAPCF.Checked = False
                chbAPCI.Checked = False
                chbAPCM.Checked = False
                chbAPCV.Checked = False
            Else
                temp = drDSRow("strAirProgramCodes")
                If Mid(temp, 1, 1) = 1 Then
                    chbAPC0.Checked = True
                End If
                If Mid(temp, 2, 1) = 1 Then
                    chbAPC1.Checked = True
                End If
                If Mid(temp, 3, 1) = 1 Then
                    chbAPC3.Checked = True
                End If
                If Mid(temp, 4, 1) = 1 Then
                    chbAPC4.Checked = True
                End If
                If Mid(temp, 5, 1) = 1 Then
                    chbAPC6.Checked = True
                End If
                If Mid(temp, 6, 1) = 1 Then
                    chbAPC7.Checked = True
                End If
                If Mid(temp, 7, 1) = 1 Then
                    chbAPC8.Checked = True
                End If
                If Mid(temp, 8, 1) = 1 Then
                    chbAPC9.Checked = True
                End If
                If Mid(temp, 9, 1) = 1 Then
                    chbAPCF.Checked = True
                End If
                If Mid(temp, 10, 1) = 1 Then
                    chbAPCA.Checked = True
                End If
                If Mid(temp, 11, 1) = 1 Then
                    chbAPCI.Checked = True
                End If
                If Mid(temp, 12, 1) = 1 Then
                    chbAPCM.Checked = True
                End If
                If Mid(temp, 13, 1) = 1 Then
                    chbAPCV.Checked = True
                End If
            End If
            If IsDBNull(drDSRow("strSICCode")) Then
                txtSICCode.Text = ""
            Else
                txtSICCode.Text = drDSRow("strSICCode")
            End If
            If IsDBNull(drDSRow("strAttainmentStatus")) Then
                txt1hour.Text = "No"
                txt8HROzone.Text = "No"
                txtPM.Text = "No"
            Else
                temp = drDSRow("strAttainmentStatus")

                Select Case Mid(temp, 2, 1)
                    Case 0
                        txt1hour.Text = "No"
                    Case 1
                        txt1hour.Text = "Yes"
                    Case 2
                        txt1hour.Text = "Contributing"
                    Case Else
                        txt1hour.Text = "No"
                End Select
                Select Case Mid(temp, 3, 1)
                    Case 0
                        txt8HROzone.Text = "No"
                    Case 1
                        txt8HROzone.Text = "Atlanta"
                    Case 2
                        txt8HROzone.Text = "Macon"
                    Case Else
                        txt8HROzone.Text = "No"
                End Select
                Select Case Mid(temp, 4, 1)
                    Case 0
                        txtPM.Text = "No"
                    Case 1
                        txtPM.Text = "Atlanta"
                    Case 2
                        txtPM.Text = "Chattanooga"
                    Case 3
                        txtPM.Text = "Floyd"
                    Case 4
                        txtPM.Text = "Macon"
                    Case Else
                        txtPM.Text = "No"
                End Select
            End If
            If IsDBNull(drDSRow("datStartUpDate")) Then
                txtStartUpDate.Text = ""
            Else
                txtStartUpDate.Text = Format(drDSRow("datStartUpDate"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("datShutDownDate")) Then
                txtDateClosed.Text = ""
            Else
                txtDateClosed.Text = Format(drDSRow("datShutDownDate"), "dd-MMM-yyyy")
            End If
            If IsDBNull(drDSRow("strCMSMember")) Then
                txtCMSState.Text = ""
            Else
                txtCMSState.Text = drDSRow("strCMSMember")
            End If
            If IsDBNull(drDSRow("strPlantDescription")) Then
                txtPlantDescription.Text = ""
            Else
                txtPlantDescription.Text = drDSRow("strPlantDescription")
            End If
            If IsDBNull(drDSRow("strStateProgramCodes")) Then
                chbNSRMajor.Checked = False
                chbHAPsMajor.Checked = False
            Else
                temp = drDSRow("strStateProgramCodes")
                If Mid(temp, 1, 1) = "1" Then
                    chbNSRMajor.Checked = True
                Else
                    chbNSRMajor.Checked = False
                End If
                If Mid(temp, 2, 1) = "1" Then
                    chbHAPsMajor.Checked = True
                Else
                    chbHAPsMajor.Checked = False
                End If
            End If
            If IsDBNull(drDSRow("strNAICSCode")) Then

            Else
                temp = drDSRow("strNAICSCode")
            End If
        Next

        dtFacilityWideData.Columns.Add("PollutantStatus", GetType(System.String))
        PollutantStatus = ""

        For Each drDSRow In dsFacilityWideData.Tables("Pollutants").Rows()
            If IsDBNull(drDSRow("PollutantStatus")) Then
                'PollutantStatus = PollutantStatus
            Else
                PollutantStatus = PollutantStatus & drDSRow("PollutantStatus")
            End If
        Next

        If PollutantStatus.Contains("B") Then
            txtPollutantStatus.Text = "B - In violation, Procedural  Emissions"
            txtPollutantStatus.BackColor = Color.Pink
        Else
            If PollutantStatus.Contains("1") Then
                txtPollutantStatus.Text = "1 - In violation, No Schedule"
                txtPollutantStatus.BackColor = Color.Pink
            Else
                If PollutantStatus.Contains("6") Then
                    txtPollutantStatus.Text = "6 - In violation, Not Meeting Schedule"
                    txtPollutantStatus.BackColor = Color.Pink
                Else
                    If PollutantStatus.Contains("W") Then
                        txtPollutantStatus.Text = "W - In violation, procedural"
                        txtPollutantStatus.BackColor = Color.Pink
                    Else
                        If PollutantStatus.Contains("0") Then
                            txtPollutantStatus.Text = "0 - Unknown Compliance Status (SCAP)"
                            txtPollutantStatus.BackColor = Color.Pink
                        Else
                            If PollutantStatus.Contains("5") Then
                                txtPollutantStatus.Text = "5 - Meeting Compliance Schedule"
                                txtPollutantStatus.BackColor = Color.LightGreen
                            Else
                                If PollutantStatus.Contains("8") Then
                                    txtPollutantStatus.Text = "8 - No Applicable State Reg."
                                    txtPollutantStatus.BackColor = Color.Pink
                                Else
                                    If PollutantStatus.Contains("2") Then
                                        txtPollutantStatus.Text = "2 - In Compliance, Source Test"
                                        txtPollutantStatus.BackColor = Color.LightGreen
                                    Else
                                        If PollutantStatus.Contains("3") Then
                                            txtPollutantStatus.Text = "3 - In Compliance, Inspection"
                                            txtPollutantStatus.BackColor = Color.LightGreen
                                        Else
                                            If PollutantStatus.Contains("4") Then
                                                txtPollutantStatus.Text = "4 - In Compliance, Certification"
                                                txtPollutantStatus.BackColor = Color.LightGreen
                                            Else
                                                If PollutantStatus.Contains("9") Then
                                                    txtPollutantStatus.Text = "9 - In Compliance, Shut Down"
                                                    txtPollutantStatus.BackColor = Color.LightGreen
                                                Else
                                                    If PollutantStatus.Contains("C") Then
                                                        txtPollutantStatus.Text = "C - In Compliance, Procedural"
                                                        txtPollutantStatus.BackColor = Color.LightGreen
                                                    Else
                                                        If PollutantStatus.Contains("M") Then
                                                            txtPollutantStatus.Text = "M - In Complinace, CEMS Data"
                                                            txtPollutantStatus.BackColor = Color.LightGreen
                                                        Else
                                                            txtPollutantStatus.Text = ""
                                                            txtPollutantStatus.BackColor = Color.White
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        dgvGECOContacts.DataSource = dsFacilityWideData
        dgvGECOContacts.DataMember = "GECOContacts"

        dgvGECOContacts.RowHeadersVisible = False
        dgvGECOContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvGECOContacts.AllowUserToResizeColumns = True
        dgvGECOContacts.AllowUserToAddRows = False
        dgvGECOContacts.AllowUserToDeleteRows = False
        dgvGECOContacts.AllowUserToOrderColumns = True
        dgvGECOContacts.AllowUserToResizeRows = True
        dgvGECOContacts.ColumnHeadersHeight = "35"

        dgvGECOContacts.Columns("numUserID").HeaderText = "User ID"
        dgvGECOContacts.Columns("numUserID").DisplayIndex = 0
        dgvGECOContacts.Columns("numUserID").Visible = False
        dgvGECOContacts.Columns("strUserType").HeaderText = "User Type"
        dgvGECOContacts.Columns("strUserType").DisplayIndex = 2
        dgvGECOContacts.Columns("strUserType").Width = 100
        dgvGECOContacts.Columns("GECOContact").HeaderText = "Contact Name"
        dgvGECOContacts.Columns("GECOContact").DisplayIndex = 1
        dgvGECOContacts.Columns("GECOContact").Width = 200
        dgvGECOContacts.Columns("strUserEmail").HeaderText = "User Email"
        dgvGECOContacts.Columns("strUserEmail").DisplayIndex = 3
        dgvGECOContacts.Columns("strUserEmail").Width = 200
        dgvGECOContacts.Columns("strPhoneNumber").HeaderText = "Phone #"
        dgvGECOContacts.Columns("strPhoneNumber").DisplayIndex = 4
        dgvGECOContacts.Columns("strFaxNumber").HeaderText = "Fax #"
        dgvGECOContacts.Columns("strFaxNumber").DisplayIndex = 5
        dgvGECOContacts.Columns("strCompanyName").HeaderText = "Company Name"
        dgvGECOContacts.Columns("strCompanyName").DisplayIndex = 6
        dgvGECOContacts.Columns("strAddress").HeaderText = "Street Address"
        dgvGECOContacts.Columns("strAddress").DisplayIndex = 7
        dgvGECOContacts.Columns("strCity").HeaderText = "City"
        dgvGECOContacts.Columns("strCity").DisplayIndex = 8
        dgvGECOContacts.Columns("strState").HeaderText = "State"
        dgvGECOContacts.Columns("strState").DisplayIndex = 9
        dgvGECOContacts.Columns("strZip").HeaderText = "Zip Code"
        dgvGECOContacts.Columns("strZip").DisplayIndex = 10

        dgvISMPContacts.DataSource = dsFacilityWideData
        dgvISMPContacts.DataMember = "ISMPContacts"

        dgvISMPContacts.RowHeadersVisible = False
        dgvISMPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPContacts.AllowUserToResizeColumns = True
        dgvISMPContacts.AllowUserToAddRows = False
        dgvISMPContacts.AllowUserToDeleteRows = False
        dgvISMPContacts.AllowUserToOrderColumns = True
        dgvISMPContacts.AllowUserToResizeRows = True
        dgvISMPContacts.ColumnHeadersHeight = "35"

        dgvISMPContacts.Columns("strContactKey").HeaderText = "Contact Key"
        dgvISMPContacts.Columns("strContactKey").DisplayIndex = 0
        dgvISMPContacts.Columns("strContactKey").Visible = False
        dgvISMPContacts.Columns("strContactDescription").HeaderText = "User Type"
        dgvISMPContacts.Columns("strContactDescription").DisplayIndex = 2
        dgvISMPContacts.Columns("strContactDescription").Width = 100
        dgvISMPContacts.Columns("ContactName").HeaderText = "Contact Name"
        dgvISMPContacts.Columns("ContactName").DisplayIndex = 1
        dgvISMPContacts.Columns("ContactName").Width = 200
        dgvISMPContacts.Columns("strContactEmail").HeaderText = "User Email"
        dgvISMPContacts.Columns("strContactEmail").DisplayIndex = 3
        dgvISMPContacts.Columns("strContactEmail").Width = 200
        dgvISMPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
        dgvISMPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
        dgvISMPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
        dgvISMPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
        dgvISMPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
        dgvISMPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
        dgvISMPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
        dgvISMPContacts.Columns("strContactCompanyName").DisplayIndex = 7
        dgvISMPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
        dgvISMPContacts.Columns("strContactAddress1").DisplayIndex = 8
        dgvISMPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
        dgvISMPContacts.Columns("strContactAddress2").DisplayIndex = 12
        dgvISMPContacts.Columns("strContactAddress2").Visible = False
        dgvISMPContacts.Columns("strContactCity").HeaderText = "City"
        dgvISMPContacts.Columns("strContactCity").DisplayIndex = 9
        dgvISMPContacts.Columns("strContactState").HeaderText = "State"
        dgvISMPContacts.Columns("strContactState").DisplayIndex = 10
        dgvISMPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
        dgvISMPContacts.Columns("strContactZipCode").DisplayIndex = 11

        dgvSSCPContacts.DataSource = dsFacilityWideData
        dgvSSCPContacts.DataMember = "SSCPContacts"

        dgvSSCPContacts.RowHeadersVisible = False
        dgvSSCPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSCPContacts.AllowUserToResizeColumns = True
        dgvSSCPContacts.AllowUserToAddRows = False
        dgvSSCPContacts.AllowUserToDeleteRows = False
        dgvSSCPContacts.AllowUserToOrderColumns = True
        dgvSSCPContacts.AllowUserToResizeRows = True
        dgvSSCPContacts.ColumnHeadersHeight = "35"

        dgvSSCPContacts.Columns("strContactKey").HeaderText = "Contact Key"
        dgvSSCPContacts.Columns("strContactKey").DisplayIndex = 0
        dgvSSCPContacts.Columns("strContactKey").Visible = False
        dgvSSCPContacts.Columns("strContactDescription").HeaderText = "User Type"
        dgvSSCPContacts.Columns("strContactDescription").DisplayIndex = 2
        dgvSSCPContacts.Columns("strContactDescription").Width = 100
        dgvSSCPContacts.Columns("ContactName").HeaderText = "Contact Name"
        dgvSSCPContacts.Columns("ContactName").DisplayIndex = 1
        dgvSSCPContacts.Columns("ContactName").Width = 200
        dgvSSCPContacts.Columns("strContactEmail").HeaderText = "User Email"
        dgvSSCPContacts.Columns("strContactEmail").DisplayIndex = 3
        dgvSSCPContacts.Columns("strContactEmail").Width = 200
        dgvSSCPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
        dgvSSCPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
        dgvSSCPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
        dgvSSCPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
        dgvSSCPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
        dgvSSCPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
        dgvSSCPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
        dgvSSCPContacts.Columns("strContactCompanyName").DisplayIndex = 7
        dgvSSCPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
        dgvSSCPContacts.Columns("strContactAddress1").DisplayIndex = 8
        dgvSSCPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
        dgvSSCPContacts.Columns("strContactAddress2").DisplayIndex = 12
        dgvSSCPContacts.Columns("strContactAddress2").Visible = False
        dgvSSCPContacts.Columns("strContactCity").HeaderText = "City"
        dgvSSCPContacts.Columns("strContactCity").DisplayIndex = 9
        dgvSSCPContacts.Columns("strContactState").HeaderText = "State"
        dgvSSCPContacts.Columns("strContactState").DisplayIndex = 10
        dgvSSCPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
        dgvSSCPContacts.Columns("strContactZipCode").DisplayIndex = 11

        dgvSSPPContacts.DataSource = dsFacilityWideData
        dgvSSPPContacts.DataMember = "SSPPContacts"

        dgvSSPPContacts.RowHeadersVisible = False
        dgvSSPPContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSPPContacts.AllowUserToResizeColumns = True
        dgvSSPPContacts.AllowUserToAddRows = False
        dgvSSPPContacts.AllowUserToDeleteRows = False
        dgvSSPPContacts.AllowUserToOrderColumns = True
        dgvSSPPContacts.AllowUserToResizeRows = True
        dgvSSPPContacts.ColumnHeadersHeight = "35"

        dgvSSPPContacts.Columns("strContactKey").HeaderText = "Contact Key"
        dgvSSPPContacts.Columns("strContactKey").DisplayIndex = 0
        dgvSSPPContacts.Columns("strContactKey").Visible = False
        dgvSSPPContacts.Columns("strContactDescription").HeaderText = "User Type"
        dgvSSPPContacts.Columns("strContactDescription").DisplayIndex = 2
        dgvSSPPContacts.Columns("strContactDescription").Width = 100
        dgvSSPPContacts.Columns("ContactName").HeaderText = "Contact Name"
        dgvSSPPContacts.Columns("ContactName").DisplayIndex = 1
        dgvSSPPContacts.Columns("ContactName").Width = 200
        dgvSSPPContacts.Columns("strContactEmail").HeaderText = "User Email"
        dgvSSPPContacts.Columns("strContactEmail").DisplayIndex = 3
        dgvSSPPContacts.Columns("strContactEmail").Width = 200
        dgvSSPPContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
        dgvSSPPContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
        dgvSSPPContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
        dgvSSPPContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
        dgvSSPPContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
        dgvSSPPContacts.Columns("strContactFaxNumber").DisplayIndex = 6
        dgvSSPPContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
        dgvSSPPContacts.Columns("strContactCompanyName").DisplayIndex = 7
        dgvSSPPContacts.Columns("strContactAddress1").HeaderText = "Street Address"
        dgvSSPPContacts.Columns("strContactAddress1").DisplayIndex = 8
        dgvSSPPContacts.Columns("strContactAddress2").HeaderText = "Street Address"
        dgvSSPPContacts.Columns("strContactAddress2").DisplayIndex = 12
        dgvSSPPContacts.Columns("strContactAddress2").Visible = False
        dgvSSPPContacts.Columns("strContactCity").HeaderText = "City"
        dgvSSPPContacts.Columns("strContactCity").DisplayIndex = 9
        dgvSSPPContacts.Columns("strContactState").HeaderText = "State"
        dgvSSPPContacts.Columns("strContactState").DisplayIndex = 10
        dgvSSPPContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
        dgvSSPPContacts.Columns("strContactZipCode").DisplayIndex = 11

        dgvWebSiteContacts.DataSource = dsFacilityWideData
        dgvWebSiteContacts.DataMember = "WebContacts"

        dgvWebSiteContacts.RowHeadersVisible = False
        dgvWebSiteContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvWebSiteContacts.AllowUserToResizeColumns = True
        dgvWebSiteContacts.AllowUserToAddRows = False
        dgvWebSiteContacts.AllowUserToDeleteRows = False
        dgvWebSiteContacts.AllowUserToOrderColumns = True
        dgvWebSiteContacts.AllowUserToResizeRows = True
        dgvWebSiteContacts.ColumnHeadersHeight = "35"

        dgvWebSiteContacts.Columns("strContactKey").HeaderText = "Contact Key"
        dgvWebSiteContacts.Columns("strContactKey").DisplayIndex = 0
        dgvWebSiteContacts.Columns("strContactKey").Visible = False
        dgvWebSiteContacts.Columns("strContactDescription").HeaderText = "User Type"
        dgvWebSiteContacts.Columns("strContactDescription").DisplayIndex = 2
        dgvWebSiteContacts.Columns("strContactDescription").Width = 100
        dgvWebSiteContacts.Columns("ContactName").HeaderText = "Contact Name"
        dgvWebSiteContacts.Columns("ContactName").DisplayIndex = 1
        dgvWebSiteContacts.Columns("ContactName").Width = 200
        dgvWebSiteContacts.Columns("strContactEmail").HeaderText = "User Email"
        dgvWebSiteContacts.Columns("strContactEmail").DisplayIndex = 3
        dgvWebSiteContacts.Columns("strContactEmail").Width = 200
        dgvWebSiteContacts.Columns("strContactPhoneNumber1").HeaderText = "Phone #"
        dgvWebSiteContacts.Columns("strContactPhoneNumber1").DisplayIndex = 4
        dgvWebSiteContacts.Columns("strContactPhoneNumber2").HeaderText = "Phone #2"
        dgvWebSiteContacts.Columns("strContactPhoneNumber2").DisplayIndex = 5
        dgvWebSiteContacts.Columns("strContactFaxNumber").HeaderText = "Fax #"
        dgvWebSiteContacts.Columns("strContactFaxNumber").DisplayIndex = 6
        dgvWebSiteContacts.Columns("strContactCompanyName").HeaderText = "Company Name"
        dgvWebSiteContacts.Columns("strContactCompanyName").DisplayIndex = 7
        dgvWebSiteContacts.Columns("strContactAddress1").HeaderText = "Street Address"
        dgvWebSiteContacts.Columns("strContactAddress1").DisplayIndex = 8
        dgvWebSiteContacts.Columns("strContactAddress2").HeaderText = "Street Address"
        dgvWebSiteContacts.Columns("strContactAddress2").DisplayIndex = 12
        dgvWebSiteContacts.Columns("strContactAddress2").Visible = False
        dgvWebSiteContacts.Columns("strContactCity").HeaderText = "City"
        dgvWebSiteContacts.Columns("strContactCity").DisplayIndex = 9
        dgvWebSiteContacts.Columns("strContactState").HeaderText = "State"
        dgvWebSiteContacts.Columns("strContactState").DisplayIndex = 10
        dgvWebSiteContacts.Columns("strContactZipCode").HeaderText = "Zip Code"
        dgvWebSiteContacts.Columns("strContactZipCode").DisplayIndex = 11

        dtFacilityWideData.Columns.Add("SSCPEngineer", GetType(System.String))
        dtFacilityWideData.Columns.Add("strUnitDesc", GetType(System.String))

        For Each drDSRow In dsFacilityWideData.Tables("ComplianceContact").Rows()
            If IsDBNull(drDSRow("SSCPEngineer")) Then
                txtSSCPContact.Clear()
            Else
                txtSSCPContact.Text = drDSRow("SSCPEngineer")
            End If
            If IsDBNull(drDSRow("strUnitDesc")) Then
                txtSSCPUnit.Clear()
            Else
                txtSSCPUnit.Text = drDSRow("strUnitDesc")
            End If
        Next

        dtFacilityWideData.Columns.Add("ISMPEngineer", GetType(System.String))

        For Each drDSRow In dsFacilityWideData.Tables("MonitoringContact").Rows()
            If IsDBNull(drDSRow("ISMPEngineer")) Then
                txtISMPContact.Clear()
            Else
                txtISMPContact.Text = drDSRow("ISMPEngineer")
            End If
            If IsDBNull(drDSRow("strUnitDesc")) Then
                txtISMPUnit.Clear()
            Else
                txtISMPUnit.Text = drDSRow("strUnitDesc")
            End If
        Next

        dtFacilityWideData.Columns.Add("SSPPStaffResponsible", GetType(System.String))

        For Each drDSRow In dsFacilityWideData.Tables("PermittingContact").Rows()
            If IsDBNull(drDSRow("SSPPStaffResponsible")) Then
                txtSSPPContact.Clear()
            Else
                txtSSPPContact.Text = drDSRow("SSPPStaffResponsible")
            End If
            If IsDBNull(drDSRow("strUnitDesc")) Then
                txtSSPPUnit.Clear()
            Else
                txtSSPPUnit.Text = drDSRow("strUnitDesc")
            End If
        Next



        cboFeeYear.DataBindings.Clear()
        txtFeesClassification.DataBindings.Clear()
        txtFeesTotal.DataBindings.Clear()
        txtTotalFeesPaid.DataBindings.Clear()
        txtDateSubmitted.DataBindings.Clear()
        txtFeesPart70.DataBindings.Clear()
        txtFeesSM.DataBindings.Clear()
        txtFeesNSPS.DataBindings.Clear()
        txtFeesVOC.DataBindings.Clear()
        txtFeesPM.DataBindings.Clear()
        txtFeesSO2.DataBindings.Clear()
        txtFeesNOx.DataBindings.Clear()
        txtFeesRate.DataBindings.Clear()
        txtFeesPollutantFee.DataBindings.Clear()
        chbFeesOperating.DataBindings.Clear()
        chbFeesPart70.DataBindings.Clear()
        chbNSPSExempt.DataBindings.Clear()

        Dim dtFees As New DataTable
        Dim drNewRow As DataRow

        If dsFacilityWideData.Tables("Fees").Rows.Count = 0 Then
            cboFeeYear.Text = ""
            txtFeesClassification.Text = ""
            txtFeesTotal.Text = ""
            txtTotalFeesPaid.Text = ""
            txtDateSubmitted.Text = ""
            txtFeesPart70.Text = ""
            txtFeesSM.Text = ""
            txtFeesNSPS.Text = ""
            txtFeesVOC.Text = ""
            txtFeesPM.Text = ""
            txtFeesSO2.Text = ""
            txtFeesNOx.Text = ""
            txtFeesRate.Text = ""
            txtFeesPollutantFee.Text = ""
            chbFeesOperating.Checked = False
            chbFeesPart70.Checked = False
            chbNSPSExempt.Checked = False
        Else
            dtFees.Columns.Add("intYear", GetType(System.String))
            dtFees.Columns.Add("strClass", GetType(System.String))
            dtFees.Columns.Add("intVOCTons", GetType(System.String))
            dtFees.Columns.Add("intPMTons", GetType(System.String))
            dtFees.Columns.Add("intSO2Tons", GetType(System.String))
            dtFees.Columns.Add("intNOXtons", GetType(System.String))
            dtFees.Columns.Add("NumPart70Fee", GetType(System.String))
            dtFees.Columns.Add("NumSMFee", GetType(System.String))
            dtFees.Columns.Add("NumNSPSFee", GetType(System.String))
            dtFees.Columns.Add("NumTotalFee", GetType(System.String))
            dtFees.Columns.Add("strNSPSExempt", GetType(System.String))
            dtFees.Columns.Add("strOperate", GetType(System.String))
            dtFees.Columns.Add("NumFeeRate", GetType(System.String))
            dtFees.Columns.Add("numCalculatedFee", GetType(System.String))
            dtFees.Columns.Add("strPart70", GetType(System.String))
            dtFees.Columns.Add("TotalPaid", GetType(System.String))
            dtFees.Columns.Add("DateSubmit", GetType(System.String))

            For Each drDSRow In dsFacilityWideData.Tables("Fees").Rows()
                drNewRow = dtFees.NewRow()
                drNewRow("intYear") = drDSRow("intYear")
                drNewRow("strClass") = drDSRow("strClass")
                drNewRow("intVOCTons") = drDSRow("intVOCTons")
                drNewRow("intPMTons") = drDSRow("intPMTons")
                drNewRow("intSO2Tons") = drDSRow("intSO2Tons")
                drNewRow("intNOXtons") = drDSRow("intNOXtons")
                drNewRow("NumPart70Fee") = drDSRow("NumPart70Fee")
                drNewRow("NumSMFee") = drDSRow("NumSMFee")
                drNewRow("NumNSPSFee") = drDSRow("NumNSPSFee")
                drNewRow("NumTotalFee") = drDSRow("NumTotalFee")
                drNewRow("strNSPSExempt") = drDSRow("strNSPSExempt")
                drNewRow("strOperate") = drDSRow("strOperate")
                drNewRow("NumFeeRate") = drDSRow("NumFeeRate")
                drNewRow("numCalculatedFee") = drDSRow("numCalculatedFee")
                drNewRow("strPart70") = drDSRow("strPart70")
                drNewRow("TotalPaid") = drDSRow("TotalPaid")
                drNewRow("DateSubmit") = drDSRow("DateSubmit")
                dtFees.Rows.Add(drNewRow)
            Next

            With txtFeesClassification
                .DataBindings.Add(New Binding("Text", dtFees, "strClass"))
            End With

            With txtFeesTotal
                .DataBindings.Add(New Binding("Text", dtFees, "NumTotalFee"))
            End With

            With txtTotalFeesPaid
                .DataBindings.Add(New Binding("Text", dtFees, "TotalPaid"))
            End With

            With txtDateSubmitted
                .DataBindings.Add(New Binding("Text", dtFees, "DateSubmit"))
            End With

            With txtFeesPart70
                .DataBindings.Add(New Binding("Text", dtFees, "NumPart70Fee"))
            End With

            With txtFeesSM
                .DataBindings.Add(New Binding("Text", dtFees, "NumSMFee"))
            End With

            With txtFeesNSPS
                .DataBindings.Add(New Binding("Text", dtFees, "NumNSPSFee"))
            End With

            With txtFeesVOC
                .DataBindings.Add(New Binding("Text", dtFees, "intVOCTons"))
            End With

            With txtFeesPM
                .DataBindings.Add(New Binding("Text", dtFees, "intPMTons"))
            End With

            With txtFeesSO2
                .DataBindings.Add(New Binding("Text", dtFees, "intSO2Tons"))
            End With

            With txtFeesNOx
                .DataBindings.Add(New Binding("Text", dtFees, "intNOXtons"))
            End With

            With txtFeesRate
                .DataBindings.Add(New Binding("Text", dtFees, "NumFeeRate"))
            End With

            With txtFeesPollutantFee
                .DataBindings.Add(New Binding("Text", dtFees, "numCalculatedFee"))
            End With

            With chbFeesOperating
                .DataBindings.Add(New Binding("Checked", dtFees, "strOperate"))
            End With

            With chbFeesPart70
                .DataBindings.Add(New Binding("Checked", dtFees, "strPart70"))
            End With

            With chbNSPSExempt
                .DataBindings.Add(New Binding("Checked", dtFees, "strNSPSExempt"))
            End With

            With cboFeeYear
                .DataSource = dtFees
                .DisplayMember = "FeesData"
                .ValueMember = "intYear"
                .SelectedIndex = 0
            End With
        End If

        SQL = "select datNotificationSent " & _
        "from " & connNameSpace & ".SSCPNotifications, " & connNameSpace & ".SSCPItemMaster  " & _
        "where " & connNameSpace & ".SSCPItemMaster.strTrackingNumber = " & connNameSpace & ".SSCPNotifications.strTrackingNumber  " & _
        "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
        "and strNotificationType = '03' " & _
        "and datNotificationdue = '" & txtDateClosed.Text & "' "

        daFacilityWideData = New OracleDataAdapter(SQL, conn)

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        daFacilityWideData.Fill(dsFacilityWideData, "NotificationSent")

        dtFacilityWideData.Columns.Add("datNotificationSent", GetType(System.String))
        PollutantStatus = ""

        For Each drDSRow In dsFacilityWideData.Tables("NotificationSent").Rows()
            If IsDBNull(drDSRow("datNotificationSent")) Then
                txtPhysicalShutDownDate.Clear()
            Else
                txtPhysicalShutDownDate.Text = Format(drDSRow("datNotificationSent"), "dd-MMM-yyyy")
            End If
        Next

        llbViewAll.Enabled = True
    End Sub
    Sub LoadMonitoringData()
        Try
            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & " " & connNameSpace & ".ISMPReportInformation.datReceivedDate between '01-Jan-" & clbYear.Items.Item(count) & _
                        "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            '  SQL = "Select " & connNameSpace & ".VW_ISMPTestReportViewer.*, strPreComplianceStatus   " & _
            '"from  " & connNameSpace & ".VW_ISMPTestReportViewer, " & connNameSpace & ".ISMPReportInformation " & _
            '"where " & connNameSpace & ".VW_ISMPTestReportViewer.strReferenceNumber = " & _
            '  "" & connNameSpace & ".ISMPReportInformation.strReferenceNumber  " & _
            '"and  Status = 'Open' "

            SQL = "Select " & connNameSpace & ".VW_ISMPWorkDataGrid.*, strPreComplianceStatus  " & _
            "from " & connNameSpace & ".VW_ISMPWorkDataGrid, " & connNameSpace & ".ISMPReportInformation " & _
            "where " & connNameSpace & ".VW_ISMPWorkDataGrid.strReferenceNumber = " & _
            "" & connNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
            "and strAIRSNUmber = '0413" & mtbAIRSNumber.Text & "' " & _
            SQLLine & _
            "order by " & connNameSpace & ".ISMPReportInformation.strReferenceNumber DESC "

            daISMP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPWork")

            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & "datNotificationDate between '01-Jan-" & clbYear.Items.Item(count) & _
                        "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = "Select strTestLogNumber, " & _
            "(strLastName||', '||strFirstName) as Engineer,  " & _
            "strEmissionUnit, strUnitDesc, " & _
            "datTestNotification, datProposedstartDate,  " & _
            "datProposedEndDate, strComments  " & _
            "from " & connNameSpace & ".ISMPTestNotification, " & connNameSpace & ".EPDUserProfiles, " & _
            "" & connNameSpace & ".LookUpEPDUnits  " & _
            "where " & connNameSpace & ".EPDUserProfiles.numUserID = " & connNameSpace & ".ISMPTestNotification.strStaffResponsible  " & _
            "and " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  " & _
            "order by strTestLogNumber DESC "

            daISMP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPTestLog")


            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & " " & connNameSpace & ".ISMPMaster.datModifingDate between '01-Jan-" & clbYear.Items.Item(count) & _
                        "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = "Select " & connNameSpace & ".ISMPTestREportMemo.strReferenceNumber, " & _
            "strMemorandumField " & _
            "from " & connNameSpace & ".ISMPTestREportMemo, " & connNameSpace & ".ISMPMaster " & _
            "where " & connNameSpace & ".ISMPTestREportMemo.strReferenceNumber = " & connNameSpace & ".ISMPMaster.strReferenceNumber " & _
            "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            SQLLine & _
            "Order by " & connNameSpace & ".ISMPTestREportMemo.strReferenceNumber DESC "

            daISMP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daISMP.Fill(dsISMP, "ISMPMemo")

        Catch ex As Exception
            ErrorReport(SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwLoadMonitoring_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadMonitoring.DoWork
        LoadMonitoringData()
    End Sub
    Private Sub bgwLoadMonitoring_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadMonitoring.RunWorkerCompleted

        dgvISMPWork.DataSource = dsISMP
        dgvISMPWork.DataMember = "ISMPWork"

        dgvISMPWork.RowHeadersVisible = False
        dgvISMPWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPWork.AllowUserToResizeColumns = True
        dgvISMPWork.AllowUserToAddRows = False
        dgvISMPWork.AllowUserToDeleteRows = False
        dgvISMPWork.AllowUserToOrderColumns = True
        dgvISMPWork.AllowUserToResizeRows = True

        dgvISMPWork.Columns("strAIRSNumber").HeaderText = "AIRS Number"
        dgvISMPWork.Columns("strAIRSNumber").DisplayIndex = 0
        dgvISMPWork.Columns("strAIRSNumber").Visible = False
        dgvISMPWork.Columns("strReferenceNumber").HeaderText = "Reference Number"
        dgvISMPWork.Columns("strReferenceNumber").DisplayIndex = 1
        dgvISMPWork.Columns("strReferenceNumber").Width = 100
        dgvISMPWork.Columns("strEmissionSource").HeaderText = "Emission Source"
        dgvISMPWork.Columns("strEmissionSource").DisplayIndex = 2
        dgvISMPWork.Columns("strEmissionSource").Width = 200
        dgvISMPWork.Columns("strPollutantDescription").HeaderText = "Pollutant"
        dgvISMPWork.Columns("strPollutantDescription").DisplayIndex = 3
        dgvISMPWork.Columns("strReportType").HeaderText = "Report Type"
        dgvISMPWork.Columns("strReportType").DisplayIndex = 4
        dgvISMPWork.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
        dgvISMPWork.Columns("ReviewingEngineer").DisplayIndex = 7
        dgvISMPWork.Columns("TestDateStart").HeaderText = "Test Date"
        dgvISMPWork.Columns("TestDateStart").DisplayIndex = 8
        dgvISMPWork.Columns("ReceivedDate").HeaderText = "Received Date"
        dgvISMPWork.Columns("ReceivedDate").DisplayIndex = 9
        dgvISMPWork.Columns("DatReceivedDate").HeaderText = "Received Date"
        dgvISMPWork.Columns("DatReceivedDate").DisplayIndex = 10
        dgvISMPWork.Columns("CompleteDate").HeaderText = "Complete Date"
        dgvISMPWork.Columns("CompleteDate").DisplayIndex = 11
        dgvISMPWork.Columns("strComplianceStatus").HeaderText = "Compliance Status"
        dgvISMPWork.Columns("strComplianceStatus").DisplayIndex = 12
        dgvISMPWork.Columns("Status").HeaderText = "Compliance Status"
        dgvISMPWork.Columns("Status").DisplayIndex = 13
        dgvISMPWork.Columns("MMOCommentArea").HeaderText = "Comment Field"
        dgvISMPWork.Columns("MMOCommentArea").DisplayIndex = 14
        dgvISMPWork.Columns("strDocumentType").HeaderText = "Document Type"
        dgvISMPWork.Columns("strDocumentType").DisplayIndex = 5
        dgvISMPWork.Columns("strApplicableRequirement").HeaderText = "Applicable Requirement"
        dgvISMPWork.Columns("strApplicableRequirement").DisplayIndex = 15
        dgvISMPWork.Columns("strUnitTitle").HeaderText = "Reviewing Unit"
        dgvISMPWork.Columns("strUnitTitle").DisplayIndex = 6
        dgvISMPWork.Columns("strPreComplianceStatus").HeaderText = "Pre-Compliance Status"
        dgvISMPWork.Columns("strPreComplianceStatus").DisplayIndex = 16

        LoadCompliaceColor()

        dgvISMPTestNotification.DataSource = dsISMP
        dgvISMPTestNotification.DataMember = "ISMPTestLog"

        dgvISMPTestNotification.RowHeadersVisible = False
        dgvISMPTestNotification.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPTestNotification.AllowUserToResizeColumns = True
        dgvISMPTestNotification.AllowUserToAddRows = False
        dgvISMPTestNotification.AllowUserToDeleteRows = False
        dgvISMPTestNotification.AllowUserToOrderColumns = True
        dgvISMPTestNotification.AllowUserToResizeRows = True
        dgvISMPTestNotification.Columns("strTestLogNumber").HeaderText = "Test Log Number"
        dgvISMPTestNotification.Columns("strTestLogNumber").DisplayIndex = 0
        dgvISMPTestNotification.Columns("Engineer").HeaderText = "ISMP Engineer"
        dgvISMPTestNotification.Columns("Engineer").DisplayIndex = 3
        dgvISMPTestNotification.Columns("strEmissionUnit").HeaderText = "Emission Source"
        dgvISMPTestNotification.Columns("strEmissionUnit").DisplayIndex = 1
        dgvISMPTestNotification.Columns("strEmissionUnit").Width = 100
        dgvISMPTestNotification.Columns("strUnitDesc").HeaderText = "ISMP Unit"
        dgvISMPTestNotification.Columns("strUnitDesc").DisplayIndex = 2
        dgvISMPTestNotification.Columns("strUnitDesc").Width = 200
        dgvISMPTestNotification.Columns("datTestNotification").HeaderText = "Date Notified"
        dgvISMPTestNotification.Columns("datTestNotification").DisplayIndex = 4
        dgvISMPTestNotification.Columns("datProposedstartDate").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("datProposedstartDate").DisplayIndex = 5
        dgvISMPTestNotification.Columns("datProposedEndDate").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("datProposedEndDate").DisplayIndex = 6
        dgvISMPTestNotification.Columns("strComments").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("strComments").DisplayIndex = 7


        dgvISMPMemo.DataSource = dsISMP
        dgvISMPMemo.DataMember = "ISMPMemo"

        dgvISMPMemo.RowHeadersVisible = False
        dgvISMPMemo.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPMemo.AllowUserToResizeColumns = True
        dgvISMPMemo.AllowUserToAddRows = False
        dgvISMPMemo.AllowUserToDeleteRows = False
        dgvISMPMemo.AllowUserToOrderColumns = True
        dgvISMPMemo.AllowUserToResizeRows = True
        dgvISMPMemo.Columns("strReferenceNumber").HeaderText = "Reference Number"
        dgvISMPMemo.Columns("strReferenceNumber").DisplayIndex = 0
        dgvISMPMemo.Columns("strMemorandumField").HeaderText = "Memo Field"
        dgvISMPMemo.Columns("strMemorandumField").DisplayIndex = 1
        dgvISMPMemo.Columns("strMemorandumField").Width = 500

    End Sub
    Sub LoadComplianceData()
        Try
            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    '---- This is for a Calander Year
                    'If clbYear.CheckedIndices.Contains(count) = True Then
                    '    SQLLine = SQLLine & "datReceivedDate between '01-Jan-" & clbYear.Items.Item(count) & _
                    '    "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    'End If
                    '---- This is for a Fiscal Year
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & "datReceivedDate between '01-Oct-" & clbYear.Items.Item(count) - 1 & _
                        "' and '30-Sep-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = "Select * " & _
            "From " & connNameSpace & ".VW_SSCPWorkDataGrid " & _
            "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            SQLLine & _
            "Order by strTrackingNumber DESC "

            daSSCP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPEvents")



            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    '---- This is for a Calander Year
                    'If clbYear.CheckedIndices.Contains(count) = True Then
                    '    SQLLine = SQLLine & "DatViolationDiscovery between '01-Jan-" & clbYear.Items.Item(count) & _
                    '    "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    'End If
                    '---- This is for a Fiscal Year
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & "datDiscoveryDate between '01-Oct-" & clbYear.Items.Item(count) - 1 & _
                        "' and '30-Sep-" & clbYear.Items.Item(count) & "' or "
                    End If

                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = "Select distinct(" & connNameSpace & ".SSCP_AuditedEnforcement.strEnforcementNumber), " & _
            "Case  " & _
            "	when datDiscoveryDate is Null then '' " & _
            "	else to_char(datDiscoveryDate, 'dd-Mon-yyyy') " & _
            "END as Violationdate,  " & _
            "case  " & _
            " 	when strHPV IS NULL then strActionType " & _
            " 	When strHPV IS Not Null then 'HPV'  " & _
            "   Else 'HPV' " & _
            "END as HPVStatus, " & _
            "Case " & _
            "	when datEnforcementFinalized Is Not NULL then 'Closed' " & _
            "	when datEnforcementFinalized is NUll then 'Open' " & _
            "Else 'Open' " & _
            "End as Status, " & _
            "substr(strAIRSNumber, 5) as AIRSNumber " & _
            "from " & connNameSpace & ".SSCP_AuditedEnforcement  " & _
            "Where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "order by strENforcementNumber DESC "

            daSSCP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPEnforcement")

            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = ""
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & "datFCECompleted between '01-Jan-" & clbYear.Items.Item(count) & _
                        "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = "Select " & connNameSpace & ".SSCPFCEMaster.strFCENumber, " & _
                    "strFCEStatus, " & _
                    "(strLastname||', '||strFirstName) as ReviewingEngineer, " & _
                    "to_char(DatFCECompleted, 'dd-Mon-yyyy') as FCECompleted, " & _
                    "strFCEYear as FCEYear, " & _
                    "strFCEComments " & _
                    "from " & connNameSpace & ".SSCPFCE, " & connNameSpace & ".SSCPFCEMaster, " & connNameSpace & ".EPDuserProfiles " & _
                    "where StrAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and " & connNameSpace & ".SSCPFCEMaster.strFCENumber = " & connNameSpace & ".SSCPFCE.strFCENumber " & _
                    "and " & connNameSpace & ".EPDuserProfiles.numUserID = " & connNameSpace & ".SSCPFCE.strReviewer  " & _
                    SQLLine & _
                    "order by DatFCECompleted DESC "

            daSSCP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSCP.Fill(dsSSCP, "SSCPFCE")



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwLoadCompliance_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadCompliance.DoWork
        LoadComplianceData()
    End Sub
    Private Sub bgwLoadCompliance_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadCompliance.RunWorkerCompleted

        dgvSSCPEvents.DataSource = dsSSCP
        dgvSSCPEvents.DataMember = "SSCPEvents"

        dgvSSCPEvents.RowHeadersVisible = False
        dgvSSCPEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSCPEvents.AllowUserToResizeColumns = True
        dgvSSCPEvents.AllowUserToAddRows = False
        dgvSSCPEvents.AllowUserToDeleteRows = False
        dgvSSCPEvents.AllowUserToOrderColumns = True
        dgvSSCPEvents.AllowUserToResizeRows = True
        dgvSSCPEvents.Columns("strAIRSNumber").HeaderText = "AIRS Number"
        dgvSSCPEvents.Columns("strAIRSNumber").DisplayIndex = 0
        dgvSSCPEvents.Columns("strAIRSNumber").Visible = False
        dgvSSCPEvents.Columns("strTrackingNumber").HeaderText = "Tracking Number"
        dgvSSCPEvents.Columns("strTrackingNumber").DisplayIndex = 1
        dgvSSCPEvents.Columns("strActivityName").HeaderText = "Event Type"
        dgvSSCPEvents.Columns("strActivityName").DisplayIndex = 2
        dgvSSCPEvents.Columns("ReceivedDate").HeaderText = "Received Date"
        dgvSSCPEvents.Columns("ReceivedDate").DisplayIndex = 3
        dgvSSCPEvents.Columns("datReceivedDate").HeaderText = "ReceivedDate"
        dgvSSCPEvents.Columns("datReceivedDate").DisplayIndex = 4
        dgvSSCPEvents.Columns("datReceivedDate").Visible = False

        dgvSSCPEnforcement.DataSource = dsSSCP
        dgvSSCPEnforcement.DataMember = "SSCPEnforcement"

        dgvSSCPEnforcement.RowHeadersVisible = False
        dgvSSCPEnforcement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSCPEnforcement.AllowUserToResizeColumns = True
        dgvSSCPEnforcement.AllowUserToAddRows = False
        dgvSSCPEnforcement.AllowUserToDeleteRows = False
        dgvSSCPEnforcement.AllowUserToOrderColumns = True
        dgvSSCPEnforcement.AllowUserToResizeRows = True
        dgvSSCPEnforcement.Columns("strEnforcementNumber").HeaderText = "Enforcement Number"
        dgvSSCPEnforcement.Columns("strEnforcementNumber").DisplayIndex = 0
        dgvSSCPEnforcement.Columns("Violationdate").HeaderText = "Violation Discovery Date"
        dgvSSCPEnforcement.Columns("Violationdate").DisplayIndex = 1
        dgvSSCPEnforcement.Columns("HPVStatus").HeaderText = "HPV Status"
        dgvSSCPEnforcement.Columns("HPVStatus").DisplayIndex = 2
        dgvSSCPEnforcement.Columns("Status").HeaderText = "Open/Closed"
        dgvSSCPEnforcement.Columns("Status").DisplayIndex = 3
        dgvSSCPEnforcement.Columns("AIRSNumber").HeaderText = "AIRS Number"
        dgvSSCPEnforcement.Columns("AIRSNumber").DisplayIndex = 4
        dgvSSCPEnforcement.Columns("AIRSNumber").Visible = False

        dgvFCEData.DataSource = dsSSCP
        dgvFCEData.DataMember = "SSCPFCE"

        dgvFCEData.RowHeadersVisible = False
        dgvFCEData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvFCEData.AllowUserToResizeColumns = True
        dgvFCEData.AllowUserToAddRows = False
        dgvFCEData.AllowUserToDeleteRows = False
        dgvFCEData.AllowUserToOrderColumns = True
        dgvFCEData.AllowUserToResizeRows = True
        dgvFCEData.Columns("strFCENumber").HeaderText = "FCE Number"
        dgvFCEData.Columns("strFCENumber").DisplayIndex = 5
        dgvFCEData.Columns("strFCENumber").Visible = False
        dgvFCEData.Columns("strFCEStatus").HeaderText = "FCE Status"
        dgvFCEData.Columns("strFCEStatus").DisplayIndex = 4
        dgvFCEData.Columns("strFCEStatus").Visible = False
        dgvFCEData.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
        dgvFCEData.Columns("ReviewingEngineer").DisplayIndex = 2
        dgvFCEData.Columns("FCECompleted").HeaderText = "FCE Complete Date"
        dgvFCEData.Columns("FCECompleted").DisplayIndex = 1
        dgvFCEData.Columns("FCEYear").HeaderText = "FCE Year"
        dgvFCEData.Columns("FCEYear").DisplayIndex = 0
        dgvFCEData.Columns("strFCEComments").HeaderText = "FCE Comments"
        dgvFCEData.Columns("strFCEComments").DisplayIndex = 3

    End Sub
    Sub LoadPermittingData()
        Try
            SQL = "Select  " & _
    "distinct(to_Number(" & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber)) as strApplicationNumber,   " & _
    "  case   " & _
    "	when strApplicationTypeDesc IS Null then ' '   " & _
    "Else strApplicationTypeDesc   " & _
    "End as strApplicationType,   " & _
    "case   " & _
    "	when datReceivedDate is Null then ' '   " & _
    "Else to_char(datReceivedDate, 'RRRR-MM-dd')   " & _
    "End as datReceivedDate,   " & _
    "case    " & _
 " when strPermitNumber is NULL then ' '    " & _
 "  else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'    " & _
 " ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'   " & _
 " ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1)   " & _
   " end As strPermitNumber,   " & _
    "case   " & _
    "	when datPermitIssued is Null then ' '   " & _
    "else to_char(datPermitIssued, 'RRRR-MM-dd')   " & _
    "end as datPermitIssued,   " & _
    "case   " & _
    "	when strStaffResponsible = '0' then ' '   " & _
    "	when strStaffResponsible is Null then ' '   " & _
    "else (strLastName||', '||strFirstName)   " & _
    "end as StaffResponsible,   " & _
    "case   " & _
    "	when " & connNameSpace & ".SSPPApplicationData.strFacilityName is Null then ' '   " & _
    "else " & connNameSpace & ".SSPPApplicationData.strFacilityName   " & _
    "end as strFacilityName,   " & _
    "case   " & _
    "	when " & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber is Null then ' '   " & _
    "	when " & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber = '0413' then ' '   " & _
    "else substr(" & connNameSpace & ".SSPPApplicationMaster.strAIRSNumber, 5)   " & _
    "end as strAIRSNumber,   " & _
   "case   " & _
   "when datPermitIssued is Not Null OR datFinalizedDate IS NOT NULL then '11 - Closed Out'   " & _
   "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then '10 - To DO'   " & _
   "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then '09 - To BC'   " & _
   "when datEPAEnds is not Null then '08 - EPA 45-day Review'   " & _
   "when datPNExpires is Not Null and datPNExpires < sysdate then '07 - Public Notice Expired'   " & _
   "when datPNExpires is Not Null and datPNExpires >= sysdate then '06 - Public Notice'    " & _
   "when datDraftIssued is Not Null and datPNExpires is Null then '05 - Draft Issued'    " & _
   "when dattoPMII is Not Null then '04 - AT PM'    " & _
   "when dattoPMI is Not Null then '03 - At UC'    " & _
   "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then '02 - Internal Review'   " & _
   "when strStaffResponsible is Null or strStaffResponsible ='0' then '0 - Unassigned'     " & _
   "else '01 - At Engineer'    " & _
   "end as AppStatus,   " & _
   "   case   " & _
   "	when strPermitTypeDescription is Null then ''   " & _
   "else strPermitTypeDescription   " & _
   "End as strPermitType,   " & _
   "case    " & _
   "when datPermitIssued is Not Null then to_char(datPermitIssued, 'RRRR-MM-dd')      " & _
   "when datFinalizedDate is Not Null then to_char(datFinalizedDate, 'RRRR-MM-dd')   " & _
   "when datToDirector is Not Null and datFinalizedDate is Null and (datDraftIssued is Null or datDraftIssued < datToDirector) then to_char(datToDirector, 'RRRR-MM-dd')   " & _
   "when datToBranchCheif is Not Null and datFinalizedDate is Null and datToDirector is Null and (datDraftIssued is Null or datDraftIssued < datToBranchCheif) then to_char(DatTOBranchCheif, 'RRRR-MM-dd')    " & _
   "when datEPAEnds is not Null then to_char(datEPAEnds, 'RRRR-MM-dd')     " & _
   "when datPNExpires is Not Null and datPNExpires < sysdate then to_char(datPNExpires, 'RRRR-MM-dd')     " & _
   "when datPNExpires is Not Null and datPNExpires >= sysdate then to_char(datPNExpires, 'RRRR-MM-dd')      " & _
   "when datDraftIssued is Not Null and datPNExpires is Null then to_char(datDraftIssued, 'RRRR-MM-dd')      " & _
   "when dattoPMII is Not Null then to_char(datToPMII, 'RRRR-MM-dd')      " & _
   "when dattoPMI is Not Null then to_char(datToPMI, 'RRRR-MM-dd')      " & _
   "when datReviewSubmitted is Not Null and (strSSCPUnit <> '0' or strISMPUnit <> '0') then to_char(datReviewSubmitted, 'RRRR-MM-dd')     " & _
   "when strStaffResponsible is Null or strStaffResponsible ='0' then 'Unknown'      " & _
   "else to_char(datAssignedToEngineer, 'RRRR-MM-dd')      " & _
   "end as StatusDate    " & _
    "from " & connNameSpace & ".SSPPApplicationMaster, " & connNameSpace & ".SSPPApplicationTracking,   " & _
    "" & connNameSpace & ".SSPPApplicationData,   " & _
    "" & connNameSpace & ".LookUpApplicationTypes, " & connNameSpace & ".LookUPPermitTypes,   " & _
    "" & connNameSpace & ".EPDUserProfiles  " & _
    "where " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & connNameSpace & ".SSPPApplicationData.strApplicationNumber (+)    " & _
    "and " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & connNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+)   " & _
    "and strApplicationType = strApplicationTypeCode (+)   " & _
    "and strPermitType = strPermitTypeCode (+)   " & _
    "and " & connNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & connNameSpace & ".EPDUserProfiles.numUserID (+)   " & _
          "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'   "

            If clbYear.CheckedIndices.Contains(0) = True Then
                SQLLine = " "
            Else
                SQLLine = "And ("
                For count = 1 To (clbYear.Items.Count - 1)
                    '---- This is for a Calander Year
                    If clbYear.CheckedIndices.Contains(count) = True Then
                        SQLLine = SQLLine & "datReceivedDate between '01-Jan-" & clbYear.Items.Item(count) & _
                        "' and '31-Dec-" & clbYear.Items.Item(count) & "' or "
                    End If
                Next
                If SQLLine <> "And (" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 4))
                    SQLLine = SQLLine & ") "
                Else
                    SQLLine = ""
                End If
            End If

            SQL = SQL & SQLLine & "order by strApplicationNumber DESC "

            daSSPP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSPP.Fill(dsSSPP, "ApplictionLog")

            SQL = "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'0-SIP' as Subpart, " & _
            "" & connNameSpace & ".LookUpSUBPartSip.strSubpart, " & _
            "" & connNameSpace & ".LookUpSubpartSIP.strDescription, " & _
            "" & connNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & connNameSpace & ".APBSubpartData, " & connNameSpace & ".LookUpSubPartSIP " & _
            "where " & connNameSpace & ".APBSubpartData.strSubpart = " & connNameSpace & ".LookUpSubpartSIP.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '0' " & _
            "Union " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'9-NSPS(Part 60)' as Subpart, " & _
            "" & connNameSpace & ".LookUpSUBPart60.strSubpart, " & _
            "" & connNameSpace & ".LookUpSubpart60.strDescription, " & _
            "" & connNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & connNameSpace & ".APBSubpartData, " & connNameSpace & ".LookUpSubPart60 " & _
            "where " & connNameSpace & ".APBSubpartData.strSubpart = " & connNameSpace & ".LookUpSubpart60.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '9' " & _
            "Union " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'8-NESHAP(Part 61)' as Subpart, " & _
            "" & connNameSpace & ".LookUpSUBPart61.strSubpart, " & _
            "" & connNameSpace & ".LookUpSubpart61.strDescription, " & _
            "" & connNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & connNameSpace & ".APBSubpartData, " & connNameSpace & ".LookUpSubPart61 " & _
            "where " & connNameSpace & ".APBSubpartData.strSubpart = " & connNameSpace & ".LookUpSubpart61.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = '8' " & _
            "UNION " & _
            "select " & _
            "substr(strAIRSNumber, 5) as AIRSNumber,  " & _
            "'M-MACT(Part 63)' as Subpart, " & _
            "" & connNameSpace & ".LookUpSUBPart63.strSubpart, " & _
            "" & connNameSpace & ".LookUpSubpart63.strDescription, " & _
            "" & connNameSpace & ".APBSubpartData.CreateDateTime " & _
            "from " & connNameSpace & ".APBSubpartData, " & connNameSpace & ".LookUpSubPart63 " & _
            "where " & connNameSpace & ".APBSubpartData.strSubpart = " & connNameSpace & ".LookUpSubpart63.strSubpart " & _
            "and ACTIVE <> '0' " & _
            "and strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 13, 1) = 'M' "

            daSSPP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSPP.Fill(dsSSPP, "ActiveRules")

            SQL = "select " & _
            "'0-SIP' as Subpart,  " & _
            "" & connNameSpace & ".LookUpSubpartSIP.strSubpart,  " & _
            "" & connNameSpace & ".LookupSubpartSIP.strDescription,  " & _
            "" & connNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & connNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & connNameSpace & ".SSPPApplicationMaster, " & connNameSpace & ".SSPPSubpartData,  " & _
            "" & connNameSpace & ".LookUpSubpartSIP  " & _
            "where " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & connNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & connNameSpace & ".SSPPSubpartData.strSubPart = " & connNameSpace & ".LookUPSubpartSIP.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '0'  " & _
            "union  " & _
            "select  " & _
            "'9-NSPS(Part 60)' as Subpart,  " & _
            "" & connNameSpace & ".LookUpSubpart60.strSubpart,  " & _
            "" & connNameSpace & ".LookupSubpart60.strDescription,  " & _
            "" & connNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & connNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & connNameSpace & ".SSPPApplicationMaster, " & connNameSpace & ".SSPPSubpartData,  " & _
            "" & connNameSpace & ".LookUpSubpart60  " & _
            "where " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & connNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & connNameSpace & ".SSPPSubpartData.strSubPart = " & connNameSpace & ".LookUPSubpart60.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '9' " & _
            "Union  " & _
            "select  " & _
            "'8-NESHAP(Part 61)' as Subpart,  " & _
            "" & connNameSpace & ".LookUpSubpart61.strSubpart,  " & _
            "" & connNameSpace & ".LookupSubpart61.strDescription,  " & _
            "" & connNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & connNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & connNameSpace & ".SSPPApplicationMaster, " & connNameSpace & ".SSPPSubpartData,  " & _
            "" & connNameSpace & ".LookUpSubpart61  " & _
            "where " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & connNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & connNameSpace & ".SSPPSubpartData.strSubPart = " & connNameSpace & ".LookUPSubpart61.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = '8' " & _
            "Union  " & _
            "select  " & _
            "'M-MACT(Part 63)' as Subpart,  " & _
            "" & connNameSpace & ".LookUpSubpart63.strSubpart,  " & _
            "" & connNameSpace & ".LookupSubpart63.strDescription,  " & _
            "" & connNameSpace & ".SSPPSubpartData.strApplicationNumber,  " & _
            "" & connNameSpace & ".SSPPSubpartData.CreateDateTime,  " & _
            "case  " & _
            "when strApplicationActivity = '0' then 'Removed'  " & _
            "when strApplicationActivity = '1' then 'Added'  " & _
            "when strApplicationActivity = '2' then 'Modified'  " & _
            "End AppActivity  " & _
            "from " & connNameSpace & ".SSPPApplicationMaster, " & connNameSpace & ".SSPPSubpartData,  " & _
            "" & connNameSpace & ".LookUpSubpart63  " & _
            "where " & connNameSpace & ".SSPPApplicationMaster.strApplicationNumber =  " & _
            "   " & connNameSpace & ".SSPPSubpartData.strApplicationNumber  " & _
            "and " & connNameSpace & ".SSPPSubpartData.strSubPart = " & connNameSpace & ".LookUPSubpart63.strSubpart  " & _
            "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
            "and substr(strSubpartKey, 6,1) = 'M'"

            daSSPP = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daSSPP.Fill(dsSSPP, "RuleHistory")

        Catch ex As Exception
            ErrorReport(mtbAIRSNumber.Text & vbCrLf & SQL & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwLoadPermitting_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadPermitting.DoWork
        LoadPermittingData()
    End Sub
    Private Sub bgwLoadPermitting_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadPermitting.RunWorkerCompleted

        dgvApplicationLog.DataSource = dsSSPP
        dgvApplicationLog.DataMember = "ApplictionLog"

        dgvApplicationLog.RowHeadersVisible = False
        dgvApplicationLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvApplicationLog.AllowUserToResizeColumns = True
        dgvApplicationLog.AllowUserToAddRows = False
        dgvApplicationLog.AllowUserToDeleteRows = False
        dgvApplicationLog.AllowUserToOrderColumns = True
        dgvApplicationLog.AllowUserToResizeRows = True
        dgvApplicationLog.Columns("strApplicationNumber").HeaderText = "APL #"
        dgvApplicationLog.Columns("strApplicationNumber").DisplayIndex = 0
        dgvApplicationLog.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvApplicationLog.Columns("strFacilityName").DisplayIndex = 1
        dgvApplicationLog.Columns("strAIRSNumber").HeaderText = "AIRS #"
        dgvApplicationLog.Columns("strAIRSNumber").DisplayIndex = 2
        dgvApplicationLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        dgvApplicationLog.Columns("StaffResponsible").DisplayIndex = 3
        dgvApplicationLog.Columns("strApplicationType").HeaderText = "APL Type"
        dgvApplicationLog.Columns("strApplicationType").DisplayIndex = 4
        dgvApplicationLog.Columns("datReceivedDate").HeaderText = "APL Rcvd"
        dgvApplicationLog.Columns("datReceivedDate").DisplayIndex = 5
        dgvApplicationLog.Columns("datPermitIssued").HeaderText = "Permit Issued"
        dgvApplicationLog.Columns("datPermitIssued").DisplayIndex = 6
        dgvApplicationLog.Columns("strPermitNumber").HeaderText = "Permit Number"
        dgvApplicationLog.Columns("strPermitNumber").DisplayIndex = 7
        dgvApplicationLog.Columns("strPermitType").HeaderText = "Action Type"
        dgvApplicationLog.Columns("strPermitType").DisplayIndex = 8
        dgvApplicationLog.Columns("AppStatus").HeaderText = "App Status"
        dgvApplicationLog.Columns("AppStatus").DisplayIndex = 9
        dgvApplicationLog.Columns("StatusDate").HeaderText = "Status Date"
        dgvApplicationLog.Columns("StatusDate").DisplayIndex = 10


        dgvActiveRules.DataSource = dsSSPP
        dgvActiveRules.DataMember = "ActiveRules"

        dgvActiveRules.RowHeadersVisible = False
        dgvActiveRules.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvActiveRules.AllowUserToResizeColumns = True
        dgvActiveRules.AllowUserToAddRows = False
        dgvActiveRules.AllowUserToDeleteRows = False
        dgvActiveRules.AllowUserToOrderColumns = True
        dgvActiveRules.AllowUserToResizeRows = True
        dgvActiveRules.Columns("Subpart").HeaderText = "Subpart"
        dgvActiveRules.Columns("Subpart").DisplayIndex = 0
        dgvActiveRules.Columns("strSubpart").HeaderText = "Rule"
        dgvActiveRules.Columns("strSubpart").DisplayIndex = 1
        dgvActiveRules.Columns("strDescription").HeaderText = "Description"
        dgvActiveRules.Columns("strDescription").DisplayIndex = 2
        dgvActiveRules.Columns("strDescription").Width = 400
        dgvActiveRules.Columns("CreateDateTime").HeaderText = "Initial Applicability"
        dgvActiveRules.Columns("CreateDateTime").DisplayIndex = 3
        dgvActiveRules.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvActiveRules.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvActiveRules.Columns("AIRSNumber").DisplayIndex = 4
        dgvActiveRules.Columns("AIRSNumber").Visible = False

        dgvRuleHistory.DataSource = dsSSPP
        dgvRuleHistory.DataMember = "RuleHistory"

        dgvRuleHistory.RowHeadersVisible = False
        dgvRuleHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvRuleHistory.AllowUserToResizeColumns = True
        dgvRuleHistory.AllowUserToAddRows = False
        dgvRuleHistory.AllowUserToDeleteRows = False
        dgvRuleHistory.AllowUserToOrderColumns = True
        dgvRuleHistory.AllowUserToResizeRows = True
        dgvRuleHistory.Columns("strApplicationNumber").HeaderText = "App #"
        dgvRuleHistory.Columns("strApplicationNumber").DisplayIndex = 0
        dgvRuleHistory.Columns("AppActivity").HeaderText = "Action"
        dgvRuleHistory.Columns("AppActivity").DisplayIndex = 1
        dgvRuleHistory.Columns("Subpart").HeaderText = "Subpart"
        dgvRuleHistory.Columns("Subpart").DisplayIndex = 2
        dgvRuleHistory.Columns("strSubpart").HeaderText = "Rule"
        dgvRuleHistory.Columns("strSubpart").DisplayIndex = 3
        dgvRuleHistory.Columns("strDescription").HeaderText = "Description"
        dgvRuleHistory.Columns("strDescription").DisplayIndex = 4
        dgvRuleHistory.Columns("strDescription").Width = 250
        dgvRuleHistory.Columns("CreateDateTime").HeaderText = "Action Date"
        dgvRuleHistory.Columns("CreateDateTime").DisplayIndex = 5
        dgvRuleHistory.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

    End Sub
    Sub LoadPlanningAndSupportData()
        Try

            SQL = "select " & _
           "substr(" & connNameSpace & ".FSCalculations.strAIRSNumber, 5) as AIRSNumber, " & _
           "" & connNameSpace & ".FSCalculations.intYear, intVOCTons, intPMTons, " & _
           "intSO2Tons, intNOXTons, " & _
           "numPart70Fee, numSMFee, numTotalFee, " & _
           "strNSPSExempt, strNSPSReason, strOperate, " & _
           "numFeeRate, strNSPSExemptReason, " & _
           "strPart70, strSyntheticMinor, " & _
           "numCalculatedFee, strClass1, " & _
           "strNSPS1, ShutDate, " & _
           "varianceCheck, varianceComments, " & _
           "strPaymentType, strOfficialName, " & _
           "strOfficialTitle, dateSubmit, " & _
           "strComments " & _
           "from " & connNameSpace & ".FSCalculations, " & connNameSpace & ".FSPayAndSubmit   " & _
           "where " & connNameSpace & ".FSCalculations.strAIRSNumber = " & connNameSpace & ".FSPayAndSubmit.strAIRSnumber (+) " & _
           "and " & connNameSpace & ".FSCalculations.intYear = " & connNameSpace & ".FSPayAndSubmit.intYear (+) " & _
           "and " & connNameSpace & ".FSCalculations.strAIRSNumber = '0413" & mtbAIRSNumber.Text & "'  "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "FeeData")

            '            Select  
            'substr(strAIRSNumber, 5) as AIRSNumber, intyear,  
            'numPayment, datPaydate, strInvoiceNo, strCheckNo,  
            'strDepositNo, strPayType, strBatchNo,  
            'case 
            'when strEntryPerson is null then ' ' 
            'else (strLastName||', '||strFirstName) 
            'end strEntryPerson,  
            'strComments, intFiscalYear,  
            '                   intPayId()
            '                   from(airbranch.fsaddpaid, airbranch.epduserprofiles)
            'where airbranch.fsaddpaid.strentryperson = airbranch.epduserprofiles.numuserid (+)  
            'and strAIRSnumber = '041305100008' 
            'order by intyear desc, datpaydate desc 


            SQL = "Select " & _
          "substr(strAIRSNumber, 5) as AIRSNumber, intyear,  " & _
          "numPayment, datPaydate, strInvoiceNo, strCheckNo,  " & _
          "strDepositNo, strPayType, strBatchNo,  " & _
          "case " & _
          "when strEntryPerson is null then '' " & _
          "else (strLastName||', '||strFirstName) " & _
          "end strEntryPerson, " & _
          "strComments, intFiscalYear,  " & _
          "intPayId  " & _
          "from " & connNameSpace & ".fsaddpaid, " & connNameSpace & ".EPDUserProfiles " & _
          "where " & connNameSpace & ".FSAddPaid.strEntryPerson = " & connNameSpace & ".EPDUserProfiles.numUserID (+) " & _
          "and strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
          "order by intyear desc, datpaydate desc "

            da = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            da.Fill(ds, "FeeDeposits")



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwLoadPlanningAndSupport_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadPlanningAndSupport.DoWork
        LoadPlanningAndSupportData()
    End Sub
    Private Sub bgwLoadPlanningAndSupport_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadPlanningAndSupport.RunWorkerCompleted
        dgvFeeData.DataSource = ds
        dgvFeeData.DataMember = "FeeData"

        dgvFeeData.RowHeadersVisible = False
        dgvFeeData.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvFeeData.Columns("AIRSNumber").DisplayIndex = 0
        dgvFeeData.Columns("AIRSNumber").Width = 100
        dgvFeeData.Columns("intyear").HeaderText = "Year"
        dgvFeeData.Columns("intyear").DisplayIndex = 1
        dgvFeeData.Columns("intyear").Width = 80
        dgvFeeData.Columns("intVOCTons").HeaderText = "VOC Tons"
        dgvFeeData.Columns("intVOCTons").DisplayIndex = 2
        dgvFeeData.Columns("intVOCTons").Width = 100
        dgvFeeData.Columns("intPMTons").HeaderText = "PM Tons"
        dgvFeeData.Columns("intPMTons").DisplayIndex = 3
        dgvFeeData.Columns("intPMTons").Width = 80
        dgvFeeData.Columns("intSO2Tons").HeaderText = "SO2 Tons"
        dgvFeeData.Columns("intSO2Tons").DisplayIndex = 4
        dgvFeeData.Columns("intSO2Tons").Width = 100
        dgvFeeData.Columns("intNOXTons").HeaderText = "NOx Tons"
        dgvFeeData.Columns("intNOXTons").DisplayIndex = 5
        dgvFeeData.Columns("intNOXTons").Width = 100
        dgvFeeData.Columns("numPart70Fee").HeaderText = "Part 70 Fees"
        dgvFeeData.Columns("numPart70Fee").DisplayIndex = 6
        dgvFeeData.Columns("numPart70Fee").Width = 100
        dgvFeeData.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("numSMFee").HeaderText = "SM Fees"
        dgvFeeData.Columns("numSMFee").DisplayIndex = 7
        dgvFeeData.Columns("numSMFee").Width = 100
        dgvFeeData.Columns("numSMFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("numTotalFee").HeaderText = "Total Fees"
        dgvFeeData.Columns("numTotalFee").DisplayIndex = 8
        dgvFeeData.Columns("numTotalFee").Width = 100
        dgvFeeData.Columns("numTotalFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strNSPSExempt").HeaderText = "NSPS Exempt"
        dgvFeeData.Columns("strNSPSExempt").DisplayIndex = 9
        dgvFeeData.Columns("strNSPSExempt").Width = 100
        dgvFeeData.Columns("strNSPSReason").HeaderText = "NSPS Exempt Reason"
        dgvFeeData.Columns("strNSPSReason").DisplayIndex = 10
        dgvFeeData.Columns("strNSPSReason").Width = 200
        dgvFeeData.Columns("strOperate").HeaderText = "Operating"
        dgvFeeData.Columns("strOperate").DisplayIndex = 11
        dgvFeeData.Columns("strOperate").Width = 100
        dgvFeeData.Columns("numFeeRate").HeaderText = "Fee Rate"
        dgvFeeData.Columns("numFeeRate").DisplayIndex = 12
        dgvFeeData.Columns("numFeeRate").Width = 100
        dgvFeeData.Columns("numFeeRate").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strNSPSExemptReason").HeaderText = "NSPS Exempt Reason"
        dgvFeeData.Columns("strNSPSExemptReason").DisplayIndex = 13
        dgvFeeData.Columns("strNSPSExemptReason").Width = 200
        dgvFeeData.Columns("strPart70").HeaderText = "Part 70 Status"
        dgvFeeData.Columns("strPart70").DisplayIndex = 14
        dgvFeeData.Columns("strPart70").Width = 100
        dgvFeeData.Columns("strSyntheticMinor").HeaderText = "SM Status"
        dgvFeeData.Columns("strSyntheticMinor").DisplayIndex = 15
        dgvFeeData.Columns("strSyntheticMinor").Width = 100
        dgvFeeData.Columns("numCalculatedFee").HeaderText = "Calculated Fees"
        dgvFeeData.Columns("numCalculatedFee").DisplayIndex = 16
        dgvFeeData.Columns("numCalculatedFee").Width = 100
        dgvFeeData.Columns("numCalculatedFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strClass1").HeaderText = "Classification"
        dgvFeeData.Columns("strClass1").DisplayIndex = 17
        dgvFeeData.Columns("strClass1").Width = 100
        dgvFeeData.Columns("strNSPS1").HeaderText = "NSPS"
        dgvFeeData.Columns("strNSPS1").DisplayIndex = 18
        dgvFeeData.Columns("strNSPS1").Width = 100
        dgvFeeData.Columns("ShutDate").HeaderText = "Shutdown Date"
        dgvFeeData.Columns("ShutDate").DisplayIndex = 19
        dgvFeeData.Columns("ShutDate").Width = 100
        dgvFeeData.Columns("varianceCheck").HeaderText = "Variances"
        dgvFeeData.Columns("varianceCheck").DisplayIndex = 20
        dgvFeeData.Columns("varianceCheck").Width = 100
        dgvFeeData.Columns("varianceComments").HeaderText = "Variance Comments"
        dgvFeeData.Columns("varianceComments").DisplayIndex = 21
        dgvFeeData.Columns("varianceComments").Width = 100
        dgvFeeData.Columns("strPaymentType").HeaderText = "Payment Type"
        dgvFeeData.Columns("strPaymentType").DisplayIndex = 22
        dgvFeeData.Columns("strPaymentType").Width = 100
        dgvFeeData.Columns("strOfficialName").HeaderText = "Official Name"
        dgvFeeData.Columns("strOfficialName").DisplayIndex = 23
        dgvFeeData.Columns("strOfficialName").Width = 100
        dgvFeeData.Columns("strOfficialTitle").HeaderText = "Official Title"
        dgvFeeData.Columns("strOfficialTitle").DisplayIndex = 24
        dgvFeeData.Columns("strOfficialTitle").Width = 100
        dgvFeeData.Columns("dateSubmit").HeaderText = "Date Submitted"
        dgvFeeData.Columns("dateSubmit").DisplayIndex = 25
        dgvFeeData.Columns("dateSubmit").Width = 100
        dgvFeeData.Columns("dateSubmit").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvFeeData.Columns("strComments").HeaderText = "Comments"
        dgvFeeData.Columns("strComments").DisplayIndex = 26
        dgvFeeData.Columns("strComments").Width = 100

        dgvFeeDeposits.DataSource = ds
        dgvFeeDeposits.DataMember = "FeeDeposits"

        dgvFeeDeposits.RowHeadersVisible = False
        dgvFeeDeposits.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvFeeDeposits.Columns("AIRSNumber").DisplayIndex = 0
        dgvFeeDeposits.Columns("AIRSNumber").Width = 100
        dgvFeeDeposits.Columns("intyear").HeaderText = "Year"
        dgvFeeDeposits.Columns("intyear").DisplayIndex = 1
        dgvFeeDeposits.Columns("intyear").Width = 80
        dgvFeeDeposits.Columns("numPayment").HeaderText = "Amount Paid"
        dgvFeeDeposits.Columns("numPayment").DisplayIndex = 2
        dgvFeeDeposits.Columns("numPayment").Width = 100
        dgvFeeDeposits.Columns("numPayment").DefaultCellStyle.Format = "c"
        dgvFeeDeposits.Columns("datPaydate").HeaderText = "Pay date"
        dgvFeeDeposits.Columns("datPaydate").DisplayIndex = 3
        dgvFeeDeposits.Columns("datPaydate").Width = 80
        dgvFeeDeposits.Columns("datPaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"

        dgvFeeDeposits.Columns("strInvoiceNo").HeaderText = "Invoice #"
        dgvFeeDeposits.Columns("strInvoiceNo").DisplayIndex = 4
        dgvFeeDeposits.Columns("strInvoiceNo").Width = 100
        dgvFeeDeposits.Columns("strCheckNo").HeaderText = "Check No."
        dgvFeeDeposits.Columns("strCheckNo").DisplayIndex = 5
        dgvFeeDeposits.Columns("strCheckNo").Width = 100
        dgvFeeDeposits.Columns("strDepositNo").HeaderText = "Deposit #"
        dgvFeeDeposits.Columns("strDepositNo").DisplayIndex = 6
        dgvFeeDeposits.Columns("strDepositNo").Width = 100
        dgvFeeDeposits.Columns("strPayType").HeaderText = "Pay Type"
        dgvFeeDeposits.Columns("strPayType").DisplayIndex = 7
        dgvFeeDeposits.Columns("strPayType").Width = 150
        dgvFeeDeposits.Columns("strBatchNo").HeaderText = "Batch No"
        dgvFeeDeposits.Columns("strBatchNo").DisplayIndex = 8
        dgvFeeDeposits.Columns("strBatchNo").Width = 100
        dgvFeeDeposits.Columns("strEntryPerson").HeaderText = "Entry Person"
        dgvFeeDeposits.Columns("strEntryPerson").DisplayIndex = 9
        dgvFeeDeposits.Columns("strEntryPerson").Width = 150
        dgvFeeDeposits.Columns("strComments").HeaderText = "Comments"
        dgvFeeDeposits.Columns("strComments").DisplayIndex = 10
        dgvFeeDeposits.Columns("strComments").Width = 200
        dgvFeeDeposits.Columns("intFiscalYear").HeaderText = "Fiscal Year"
        dgvFeeDeposits.Columns("intFiscalYear").DisplayIndex = 11
        dgvFeeDeposits.Columns("intFiscalYear").Width = 80
        dgvFeeDeposits.Columns("intPayId").HeaderText = "Pay ID"
        dgvFeeDeposits.Columns("intPayId").DisplayIndex = 12
        dgvFeeDeposits.Columns("intPayId").Width = 50

    End Sub
    Private Sub dgvISMPWork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPWork.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPWork.HitTest(e.X, e.Y)

        Try
            If dgvISMPWork.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPWork.Columns(1).HeaderText = "Reference Number" Then
                    txtReferenceNumber.Text = dgvISMPWork(1, hti.RowIndex).Value
                End If
            End If
            LoadCompliaceColor()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvISMPTestNotification_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPTestNotification.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPTestNotification.HitTest(e.X, e.Y)

        Try
            If dgvISMPTestNotification.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPTestNotification.Columns(0).HeaderText = "Test Log Number" Then
                    txtTestingNumber.Text = dgvISMPTestNotification(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvISMPMemo_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvISMPMemo.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvISMPMemo.HitTest(e.X, e.Y)

        Try
            If dgvISMPMemo.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvISMPMemo.Columns(0).HeaderText = "Reference Number" Then
                    txtReferenceNumber2.Text = dgvISMPMemo(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvSSCPEvents_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEvents.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEvents.HitTest(e.X, e.Y)

        Try
            If dgvSSCPEvents.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSSCPEvents.Columns(1).HeaderText = "Tracking Number" Then
                    txtTrackingNumber.Text = dgvSSCPEvents(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvSSCPEnforcement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEnforcement.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEnforcement.HitTest(e.X, e.Y)

        Try
            If dgvSSCPEnforcement.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSSCPEnforcement.Columns(0).HeaderText = "Enforcement Number" Then
                    txtEnforcementNumber.Text = dgvSSCPEnforcement(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvFCEData_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFCEData.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFCEData.HitTest(e.X, e.Y)

        Try
            If dgvFCEData.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFCEData.Columns(4).HeaderText = "FCE Year" Then
                    txtFCEYear.Text = dgvFCEData(4, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub dgvApplicationLog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvApplicationLog.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvApplicationLog.HitTest(e.X, e.Y)

        Try
            If dgvApplicationLog.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvApplicationLog.Columns(0).HeaderText = "APL #" Then
                    txtApplicationNumber.Text = dgvApplicationLog(0, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwAllProgramData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwAllProgramData.DoWork
        LoadMonitoringData()
        LoadComplianceData()
        LoadPermittingData()
        LoadPlanningAndSupportData()
    End Sub
    Private Sub bgwAllProgramData_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAllProgramData.RunWorkerCompleted
        dgvISMPWork.DataSource = dsISMP
        dgvISMPWork.DataMember = "ISMPWork"

        dgvISMPWork.RowHeadersVisible = False
        dgvISMPWork.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPWork.AllowUserToResizeColumns = True
        dgvISMPWork.AllowUserToAddRows = False
        dgvISMPWork.AllowUserToDeleteRows = False
        dgvISMPWork.AllowUserToOrderColumns = True
        dgvISMPWork.AllowUserToResizeRows = True

        dgvISMPWork.Columns("strAIRSNumber").HeaderText = "AIRS Number"
        dgvISMPWork.Columns("strAIRSNumber").DisplayIndex = 0
        dgvISMPWork.Columns("strAIRSNumber").Visible = False
        dgvISMPWork.Columns("strReferenceNumber").HeaderText = "Reference Number"
        dgvISMPWork.Columns("strReferenceNumber").DisplayIndex = 1
        dgvISMPWork.Columns("strReferenceNumber").Width = 100
        dgvISMPWork.Columns("strEmissionSource").HeaderText = "Emission Source"
        dgvISMPWork.Columns("strEmissionSource").DisplayIndex = 2
        dgvISMPWork.Columns("strEmissionSource").Width = 200
        dgvISMPWork.Columns("strPollutantDescription").HeaderText = "Pollutant"
        dgvISMPWork.Columns("strPollutantDescription").DisplayIndex = 3
        dgvISMPWork.Columns("strReportType").HeaderText = "Report Type"
        dgvISMPWork.Columns("strReportType").DisplayIndex = 4
        dgvISMPWork.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
        dgvISMPWork.Columns("ReviewingEngineer").DisplayIndex = 7
        dgvISMPWork.Columns("TestDateStart").HeaderText = "Test Date"
        dgvISMPWork.Columns("TestDateStart").DisplayIndex = 8
        dgvISMPWork.Columns("ReceivedDate").HeaderText = "Received Date"
        dgvISMPWork.Columns("ReceivedDate").DisplayIndex = 9
        dgvISMPWork.Columns("DatReceivedDate").HeaderText = "Received Date"
        dgvISMPWork.Columns("DatReceivedDate").DisplayIndex = 10
        dgvISMPWork.Columns("CompleteDate").HeaderText = "Complete Date"
        dgvISMPWork.Columns("CompleteDate").DisplayIndex = 11
        dgvISMPWork.Columns("strComplianceStatus").HeaderText = "Compliance Status"
        dgvISMPWork.Columns("strComplianceStatus").DisplayIndex = 12
        dgvISMPWork.Columns("Status").HeaderText = "Compliance Status"
        dgvISMPWork.Columns("Status").DisplayIndex = 13
        dgvISMPWork.Columns("MMOCommentArea").HeaderText = "Comment Field"
        dgvISMPWork.Columns("MMOCommentArea").DisplayIndex = 14
        dgvISMPWork.Columns("strDocumentType").HeaderText = "Document Type"
        dgvISMPWork.Columns("strDocumentType").DisplayIndex = 5
        dgvISMPWork.Columns("strApplicableRequirement").HeaderText = "Applicable Requirement"
        dgvISMPWork.Columns("strApplicableRequirement").DisplayIndex = 15
        dgvISMPWork.Columns("strUnitTitle").HeaderText = "Reviewing Unit"
        dgvISMPWork.Columns("strUnitTitle").DisplayIndex = 6
        dgvISMPWork.Columns("strPreComplianceStatus").HeaderText = "Pre-Compliance Status"
        dgvISMPWork.Columns("strPreComplianceStatus").DisplayIndex = 16

        LoadCompliaceColor()

        dgvISMPTestNotification.DataSource = dsISMP
        dgvISMPTestNotification.DataMember = "ISMPTestLog"

        dgvISMPTestNotification.RowHeadersVisible = False
        dgvISMPTestNotification.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPTestNotification.AllowUserToResizeColumns = True
        dgvISMPTestNotification.AllowUserToAddRows = False
        dgvISMPTestNotification.AllowUserToDeleteRows = False
        dgvISMPTestNotification.AllowUserToOrderColumns = True
        dgvISMPTestNotification.AllowUserToResizeRows = True
        dgvISMPTestNotification.Columns("strTestLogNumber").HeaderText = "Test Log Number"
        dgvISMPTestNotification.Columns("strTestLogNumber").DisplayIndex = 0
        dgvISMPTestNotification.Columns("Engineer").HeaderText = "ISMP Engineer"
        dgvISMPTestNotification.Columns("Engineer").DisplayIndex = 3
        dgvISMPTestNotification.Columns("strEmissionUnit").HeaderText = "Emission Source"
        dgvISMPTestNotification.Columns("strEmissionUnit").DisplayIndex = 1
        dgvISMPTestNotification.Columns("strEmissionUnit").Width = 100
        dgvISMPTestNotification.Columns("strUnitDesc").HeaderText = "ISMP Unit"
        dgvISMPTestNotification.Columns("strUnitDesc").DisplayIndex = 2
        dgvISMPTestNotification.Columns("strUnitDesc").Width = 200
        dgvISMPTestNotification.Columns("datTestNotification").HeaderText = "Date Notified"
        dgvISMPTestNotification.Columns("datTestNotification").DisplayIndex = 4
        dgvISMPTestNotification.Columns("datProposedstartDate").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("datProposedstartDate").DisplayIndex = 5
        dgvISMPTestNotification.Columns("datProposedEndDate").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("datProposedEndDate").DisplayIndex = 6
        dgvISMPTestNotification.Columns("strComments").HeaderText = "Contact Name"
        dgvISMPTestNotification.Columns("strComments").DisplayIndex = 7


        dgvISMPMemo.DataSource = dsISMP
        dgvISMPMemo.DataMember = "ISMPMemo"

        dgvISMPMemo.RowHeadersVisible = False
        dgvISMPMemo.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvISMPMemo.AllowUserToResizeColumns = True
        dgvISMPMemo.AllowUserToAddRows = False
        dgvISMPMemo.AllowUserToDeleteRows = False
        dgvISMPMemo.AllowUserToOrderColumns = True
        dgvISMPMemo.AllowUserToResizeRows = True
        dgvISMPMemo.Columns("strReferenceNumber").HeaderText = "Reference Number"
        dgvISMPMemo.Columns("strReferenceNumber").DisplayIndex = 0
        dgvISMPMemo.Columns("strMemorandumField").HeaderText = "Memo Field"
        dgvISMPMemo.Columns("strMemorandumField").DisplayIndex = 1
        dgvISMPMemo.Columns("strMemorandumField").Width = 500


        dgvSSCPEvents.DataSource = dsSSCP
        dgvSSCPEvents.DataMember = "SSCPEvents"

        dgvSSCPEvents.RowHeadersVisible = False
        dgvSSCPEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSCPEvents.AllowUserToResizeColumns = True
        dgvSSCPEvents.AllowUserToAddRows = False
        dgvSSCPEvents.AllowUserToDeleteRows = False
        dgvSSCPEvents.AllowUserToOrderColumns = True
        dgvSSCPEvents.AllowUserToResizeRows = True
        dgvSSCPEvents.Columns("strAIRSNumber").HeaderText = "AIRS Number"
        dgvSSCPEvents.Columns("strAIRSNumber").DisplayIndex = 0
        dgvSSCPEvents.Columns("strAIRSNumber").Visible = False
        dgvSSCPEvents.Columns("strTrackingNumber").HeaderText = "Tracking Number"
        dgvSSCPEvents.Columns("strTrackingNumber").DisplayIndex = 1
        dgvSSCPEvents.Columns("strActivityName").HeaderText = "Event Type"
        dgvSSCPEvents.Columns("strActivityName").DisplayIndex = 2
        dgvSSCPEvents.Columns("ReceivedDate").HeaderText = "Received Date"
        dgvSSCPEvents.Columns("ReceivedDate").DisplayIndex = 3
        dgvSSCPEvents.Columns("datReceivedDate").HeaderText = "ReceivedDate"
        dgvSSCPEvents.Columns("datReceivedDate").DisplayIndex = 4
        dgvSSCPEvents.Columns("datReceivedDate").Visible = False

        dgvSSCPEnforcement.DataSource = dsSSCP
        dgvSSCPEnforcement.DataMember = "SSCPEnforcement"

        dgvSSCPEnforcement.RowHeadersVisible = False
        dgvSSCPEnforcement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvSSCPEnforcement.AllowUserToResizeColumns = True
        dgvSSCPEnforcement.AllowUserToAddRows = False
        dgvSSCPEnforcement.AllowUserToDeleteRows = False
        dgvSSCPEnforcement.AllowUserToOrderColumns = True
        dgvSSCPEnforcement.AllowUserToResizeRows = True
        dgvSSCPEnforcement.Columns("strEnforcementNumber").HeaderText = "Enforcement Number"
        dgvSSCPEnforcement.Columns("strEnforcementNumber").DisplayIndex = 0
        dgvSSCPEnforcement.Columns("Violationdate").HeaderText = "Violation Discovery Date"
        dgvSSCPEnforcement.Columns("Violationdate").DisplayIndex = 1
        dgvSSCPEnforcement.Columns("HPVStatus").HeaderText = "HPV Status"
        dgvSSCPEnforcement.Columns("HPVStatus").DisplayIndex = 2
        dgvSSCPEnforcement.Columns("Status").HeaderText = "Open/Closed"
        dgvSSCPEnforcement.Columns("Status").DisplayIndex = 3
        dgvSSCPEnforcement.Columns("AIRSNumber").HeaderText = "AIRS Number"
        dgvSSCPEnforcement.Columns("AIRSNumber").DisplayIndex = 4
        dgvSSCPEnforcement.Columns("AIRSNumber").Visible = False

        dgvFCEData.DataSource = dsSSCP
        dgvFCEData.DataMember = "SSCPFCE"

        dgvFCEData.RowHeadersVisible = False
        dgvFCEData.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvFCEData.AllowUserToResizeColumns = True
        dgvFCEData.AllowUserToAddRows = False
        dgvFCEData.AllowUserToDeleteRows = False
        dgvFCEData.AllowUserToOrderColumns = True
        dgvFCEData.AllowUserToResizeRows = True
        dgvFCEData.Columns("strFCENumber").HeaderText = "FCE Number"
        dgvFCEData.Columns("strFCENumber").DisplayIndex = 5
        dgvFCEData.Columns("strFCENumber").Visible = False
        dgvFCEData.Columns("strFCEStatus").HeaderText = "FCE Status"
        dgvFCEData.Columns("strFCEStatus").DisplayIndex = 4
        dgvFCEData.Columns("strFCEStatus").Visible = False
        dgvFCEData.Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
        dgvFCEData.Columns("ReviewingEngineer").DisplayIndex = 2
        dgvFCEData.Columns("FCECompleted").HeaderText = "FCE Complete Date"
        dgvFCEData.Columns("FCECompleted").DisplayIndex = 1
        dgvFCEData.Columns("FCEYear").HeaderText = "FCE Year"
        dgvFCEData.Columns("FCEYear").DisplayIndex = 0
        dgvFCEData.Columns("strFCEComments").HeaderText = "FCE Comments"
        dgvFCEData.Columns("strFCEComments").DisplayIndex = 3

        dgvApplicationLog.DataSource = dsSSPP
        dgvApplicationLog.DataMember = "ApplictionLog"

        dgvApplicationLog.RowHeadersVisible = False
        dgvApplicationLog.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvApplicationLog.AllowUserToResizeColumns = True
        dgvApplicationLog.AllowUserToAddRows = False
        dgvApplicationLog.AllowUserToDeleteRows = False
        dgvApplicationLog.AllowUserToOrderColumns = True
        dgvApplicationLog.AllowUserToResizeRows = True
        dgvApplicationLog.Columns("strApplicationNumber").HeaderText = "APL #"
        dgvApplicationLog.Columns("strApplicationNumber").DisplayIndex = 0
        dgvApplicationLog.Columns("strFacilityName").HeaderText = "Facility Name"
        dgvApplicationLog.Columns("strFacilityName").DisplayIndex = 1
        dgvApplicationLog.Columns("strAIRSNumber").HeaderText = "AIRS #"
        dgvApplicationLog.Columns("strAIRSNumber").DisplayIndex = 2
        dgvApplicationLog.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        dgvApplicationLog.Columns("StaffResponsible").DisplayIndex = 3
        dgvApplicationLog.Columns("strApplicationType").HeaderText = "APL Type"
        dgvApplicationLog.Columns("strApplicationType").DisplayIndex = 4
        dgvApplicationLog.Columns("datReceivedDate").HeaderText = "APL Rcvd"
        dgvApplicationLog.Columns("datReceivedDate").DisplayIndex = 5
        dgvApplicationLog.Columns("datPermitIssued").HeaderText = "Permit Issued"
        dgvApplicationLog.Columns("datPermitIssued").DisplayIndex = 6
        dgvApplicationLog.Columns("strPermitNumber").HeaderText = "Permit Number"
        dgvApplicationLog.Columns("strPermitNumber").DisplayIndex = 7
        dgvApplicationLog.Columns("strPermitType").HeaderText = "Action Type"
        dgvApplicationLog.Columns("strPermitType").DisplayIndex = 8
        dgvApplicationLog.Columns("AppStatus").HeaderText = "App Status"
        dgvApplicationLog.Columns("AppStatus").DisplayIndex = 9
        dgvApplicationLog.Columns("StatusDate").HeaderText = "Status Date"
        dgvApplicationLog.Columns("StatusDate").DisplayIndex = 10

        dgvActiveRules.DataSource = dsSSPP
        dgvActiveRules.DataMember = "ActiveRules"

        dgvActiveRules.RowHeadersVisible = False
        dgvActiveRules.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvActiveRules.AllowUserToResizeColumns = True
        dgvActiveRules.AllowUserToAddRows = False
        dgvActiveRules.AllowUserToDeleteRows = False
        dgvActiveRules.AllowUserToOrderColumns = True
        dgvActiveRules.AllowUserToResizeRows = True
        dgvActiveRules.Columns("Subpart").HeaderText = "Subpart"
        dgvActiveRules.Columns("Subpart").DisplayIndex = 0
        dgvActiveRules.Columns("strSubpart").HeaderText = "Rule"
        dgvActiveRules.Columns("strSubpart").DisplayIndex = 1
        dgvActiveRules.Columns("strDescription").HeaderText = "Description"
        dgvActiveRules.Columns("strDescription").DisplayIndex = 2
        dgvActiveRules.Columns("strDescription").Width = 400
        dgvActiveRules.Columns("CreateDateTime").HeaderText = "Initial Applicability"
        dgvActiveRules.Columns("CreateDateTime").DisplayIndex = 3
        dgvActiveRules.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvActiveRules.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvActiveRules.Columns("AIRSNumber").DisplayIndex = 4
        dgvActiveRules.Columns("AIRSNumber").Visible = False

        dgvRuleHistory.DataSource = dsSSPP
        dgvRuleHistory.DataMember = "RuleHistory"

        dgvRuleHistory.RowHeadersVisible = False
        dgvRuleHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvRuleHistory.AllowUserToResizeColumns = True
        dgvRuleHistory.AllowUserToAddRows = False
        dgvRuleHistory.AllowUserToDeleteRows = False
        dgvRuleHistory.AllowUserToOrderColumns = True
        dgvRuleHistory.AllowUserToResizeRows = True
        dgvRuleHistory.Columns("strApplicationNumber").HeaderText = "App #"
        dgvRuleHistory.Columns("strApplicationNumber").DisplayIndex = 0
        dgvRuleHistory.Columns("AppActivity").HeaderText = "Action"
        dgvRuleHistory.Columns("AppActivity").DisplayIndex = 1
        dgvRuleHistory.Columns("Subpart").HeaderText = "Subpart"
        dgvRuleHistory.Columns("Subpart").DisplayIndex = 2
        dgvRuleHistory.Columns("strSubpart").HeaderText = "Rule"
        dgvRuleHistory.Columns("strSubpart").DisplayIndex = 3
        dgvRuleHistory.Columns("strDescription").HeaderText = "Description"
        dgvRuleHistory.Columns("strDescription").DisplayIndex = 4
        dgvRuleHistory.Columns("strDescription").Width = 250
        dgvRuleHistory.Columns("CreateDateTime").HeaderText = "Action Date"
        dgvRuleHistory.Columns("CreateDateTime").DisplayIndex = 5
        dgvRuleHistory.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"

        dgvFeeData.DataSource = ds
        dgvFeeData.DataMember = "FeeData"

        dgvFeeData.RowHeadersVisible = False
        dgvFeeData.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvFeeData.Columns("AIRSNumber").DisplayIndex = 0
        dgvFeeData.Columns("AIRSNumber").Width = 100
        dgvFeeData.Columns("intyear").HeaderText = "Year"
        dgvFeeData.Columns("intyear").DisplayIndex = 1
        dgvFeeData.Columns("intyear").Width = 80
        dgvFeeData.Columns("intVOCTons").HeaderText = "VOC Tons"
        dgvFeeData.Columns("intVOCTons").DisplayIndex = 2
        dgvFeeData.Columns("intVOCTons").Width = 100
        dgvFeeData.Columns("intPMTons").HeaderText = "PM Tons"
        dgvFeeData.Columns("intPMTons").DisplayIndex = 3
        dgvFeeData.Columns("intPMTons").Width = 80
        dgvFeeData.Columns("intSO2Tons").HeaderText = "SO2 Tons"
        dgvFeeData.Columns("intSO2Tons").DisplayIndex = 4
        dgvFeeData.Columns("intSO2Tons").Width = 100
        dgvFeeData.Columns("intNOXTons").HeaderText = "NOx Tons"
        dgvFeeData.Columns("intNOXTons").DisplayIndex = 5
        dgvFeeData.Columns("intNOXTons").Width = 100
        dgvFeeData.Columns("numPart70Fee").HeaderText = "Part 70 Fees"
        dgvFeeData.Columns("numPart70Fee").DisplayIndex = 6
        dgvFeeData.Columns("numPart70Fee").Width = 100
        dgvFeeData.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("numSMFee").HeaderText = "SM Fees"
        dgvFeeData.Columns("numSMFee").DisplayIndex = 7
        dgvFeeData.Columns("numSMFee").Width = 100
        dgvFeeData.Columns("numSMFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("numTotalFee").HeaderText = "Total Fees"
        dgvFeeData.Columns("numTotalFee").DisplayIndex = 8
        dgvFeeData.Columns("numTotalFee").Width = 100
        dgvFeeData.Columns("numTotalFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strNSPSExempt").HeaderText = "NSPS Exempt"
        dgvFeeData.Columns("strNSPSExempt").DisplayIndex = 9
        dgvFeeData.Columns("strNSPSExempt").Width = 100
        dgvFeeData.Columns("strNSPSReason").HeaderText = "NSPS Exempt Reason"
        dgvFeeData.Columns("strNSPSReason").DisplayIndex = 10
        dgvFeeData.Columns("strNSPSReason").Width = 200
        dgvFeeData.Columns("strOperate").HeaderText = "Operating"
        dgvFeeData.Columns("strOperate").DisplayIndex = 11
        dgvFeeData.Columns("strOperate").Width = 100
        dgvFeeData.Columns("numFeeRate").HeaderText = "Fee Rate"
        dgvFeeData.Columns("numFeeRate").DisplayIndex = 12
        dgvFeeData.Columns("numFeeRate").Width = 100
        dgvFeeData.Columns("numFeeRate").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strNSPSExemptReason").HeaderText = "NSPS Exempt Reason"
        dgvFeeData.Columns("strNSPSExemptReason").DisplayIndex = 13
        dgvFeeData.Columns("strNSPSExemptReason").Width = 200
        dgvFeeData.Columns("strPart70").HeaderText = "Part 70 Status"
        dgvFeeData.Columns("strPart70").DisplayIndex = 14
        dgvFeeData.Columns("strPart70").Width = 100
        dgvFeeData.Columns("strSyntheticMinor").HeaderText = "SM Status"
        dgvFeeData.Columns("strSyntheticMinor").DisplayIndex = 15
        dgvFeeData.Columns("strSyntheticMinor").Width = 100
        dgvFeeData.Columns("numCalculatedFee").HeaderText = "Calculated Fees"
        dgvFeeData.Columns("numCalculatedFee").DisplayIndex = 16
        dgvFeeData.Columns("numCalculatedFee").Width = 100
        dgvFeeData.Columns("numCalculatedFee").DefaultCellStyle.Format = "c"
        dgvFeeData.Columns("strClass1").HeaderText = "Classification"
        dgvFeeData.Columns("strClass1").DisplayIndex = 17
        dgvFeeData.Columns("strClass1").Width = 100
        dgvFeeData.Columns("strNSPS1").HeaderText = "NSPS"
        dgvFeeData.Columns("strNSPS1").DisplayIndex = 18
        dgvFeeData.Columns("strNSPS1").Width = 100
        dgvFeeData.Columns("ShutDate").HeaderText = "Shutdown Date"
        dgvFeeData.Columns("ShutDate").DisplayIndex = 19
        dgvFeeData.Columns("ShutDate").Width = 100
        dgvFeeData.Columns("varianceCheck").HeaderText = "Variances"
        dgvFeeData.Columns("varianceCheck").DisplayIndex = 20
        dgvFeeData.Columns("varianceCheck").Width = 100
        dgvFeeData.Columns("varianceComments").HeaderText = "Variance Comments"
        dgvFeeData.Columns("varianceComments").DisplayIndex = 21
        dgvFeeData.Columns("varianceComments").Width = 100
        dgvFeeData.Columns("strPaymentType").HeaderText = "Payment Type"
        dgvFeeData.Columns("strPaymentType").DisplayIndex = 22
        dgvFeeData.Columns("strPaymentType").Width = 100
        dgvFeeData.Columns("strOfficialName").HeaderText = "Official Name"
        dgvFeeData.Columns("strOfficialName").DisplayIndex = 23
        dgvFeeData.Columns("strOfficialName").Width = 100
        dgvFeeData.Columns("strOfficialTitle").HeaderText = "Official Title"
        dgvFeeData.Columns("strOfficialTitle").DisplayIndex = 24
        dgvFeeData.Columns("strOfficialTitle").Width = 100
        dgvFeeData.Columns("dateSubmit").HeaderText = "Date Submitted"
        dgvFeeData.Columns("dateSubmit").DisplayIndex = 25
        dgvFeeData.Columns("dateSubmit").Width = 100
        dgvFeeData.Columns("dateSubmit").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvFeeData.Columns("strComments").HeaderText = "Comments"
        dgvFeeData.Columns("strComments").DisplayIndex = 26
        dgvFeeData.Columns("strComments").Width = 100

        dgvFeeDeposits.DataSource = ds
        dgvFeeDeposits.DataMember = "FeeDeposits"

        dgvFeeDeposits.RowHeadersVisible = False
        dgvFeeDeposits.Columns("AIRSNumber").HeaderText = "AIRS #"
        dgvFeeDeposits.Columns("AIRSNumber").DisplayIndex = 0
        dgvFeeDeposits.Columns("AIRSNumber").Width = 100
        dgvFeeDeposits.Columns("intyear").HeaderText = "Year"
        dgvFeeDeposits.Columns("intyear").DisplayIndex = 1
        dgvFeeDeposits.Columns("intyear").Width = 80
        dgvFeeDeposits.Columns("numPayment").HeaderText = "Amount Paid"
        dgvFeeDeposits.Columns("numPayment").DisplayIndex = 2
        dgvFeeDeposits.Columns("numPayment").Width = 100
        dgvFeeDeposits.Columns("numPayment").DefaultCellStyle.Format = "c"
        dgvFeeDeposits.Columns("datPaydate").HeaderText = "Pay date"
        dgvFeeDeposits.Columns("datPaydate").DisplayIndex = 3
        dgvFeeDeposits.Columns("datPaydate").Width = 80
        dgvFeeDeposits.Columns("datPaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvFeeDeposits.Columns("strInvoiceNo").HeaderText = "Invoice #"
        dgvFeeDeposits.Columns("strInvoiceNo").DisplayIndex = 4
        dgvFeeDeposits.Columns("strInvoiceNo").Width = 100
        dgvFeeDeposits.Columns("strCheckNo").HeaderText = "Check No."
        dgvFeeDeposits.Columns("strCheckNo").DisplayIndex = 5
        dgvFeeDeposits.Columns("strCheckNo").Width = 100
        dgvFeeDeposits.Columns("strDepositNo").HeaderText = "Deposit #"
        dgvFeeDeposits.Columns("strDepositNo").DisplayIndex = 6
        dgvFeeDeposits.Columns("strDepositNo").Width = 100
        dgvFeeDeposits.Columns("strPayType").HeaderText = "Pay Type"
        dgvFeeDeposits.Columns("strPayType").DisplayIndex = 7
        dgvFeeDeposits.Columns("strPayType").Width = 150
        dgvFeeDeposits.Columns("strBatchNo").HeaderText = "Batch No"
        dgvFeeDeposits.Columns("strBatchNo").DisplayIndex = 8
        dgvFeeDeposits.Columns("strBatchNo").Width = 100
        dgvFeeDeposits.Columns("strEntryPerson").HeaderText = "Entry Person"
        dgvFeeDeposits.Columns("strEntryPerson").DisplayIndex = 9
        dgvFeeDeposits.Columns("strEntryPerson").Width = 150
        dgvFeeDeposits.Columns("strComments").HeaderText = "Comments"
        dgvFeeDeposits.Columns("strComments").DisplayIndex = 10
        dgvFeeDeposits.Columns("strComments").Width = 200
        dgvFeeDeposits.Columns("intFiscalYear").HeaderText = "Fiscal Year"
        dgvFeeDeposits.Columns("intFiscalYear").DisplayIndex = 11
        dgvFeeDeposits.Columns("intFiscalYear").Width = 80
        dgvFeeDeposits.Columns("intPayId").HeaderText = "Pay ID"
        dgvFeeDeposits.Columns("intPayId").DisplayIndex = 12
        dgvFeeDeposits.Columns("intPayId").Width = 50
    End Sub
    Private Sub btnCopyWebSiteContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyWebSiteContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvWebSiteContacts.RowCount > 0 Then
                i = dgvWebSiteContacts.CurrentCell.RowIndex
                MailingAddress = dgvWebSiteContacts(1, i).Value & vbCrLf & _
                dgvWebSiteContacts(2, i).Value & vbCrLf & _
                dgvWebSiteContacts(7, i).Value & vbCrLf & _
                dgvWebSiteContacts(9, i).Value & " " & dgvWebSiteContacts(10, i).Value & ", " & _
                dgvWebSiteContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)


                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyPermittingContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyPermittingContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvSSPPContacts.RowCount > 0 Then
                i = dgvSSPPContacts.CurrentCell.RowIndex
                MailingAddress = dgvSSPPContacts(1, i).Value & vbCrLf & _
                dgvSSPPContacts(2, i).Value & vbCrLf & _
                dgvSSPPContacts(7, i).Value & vbCrLf & _
                dgvSSPPContacts(9, i).Value & " " & dgvSSPPContacts(10, i).Value & ", " & _
                dgvSSPPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyMointoringContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyMointoringContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvISMPContacts.RowCount > 0 Then
                i = dgvISMPContacts.CurrentCell.RowIndex
                MailingAddress = dgvISMPContacts(1, i).Value & vbCrLf & _
                dgvISMPContacts(2, i).Value & vbCrLf & _
                dgvISMPContacts(7, i).Value & vbCrLf & _
                dgvISMPContacts(9, i).Value & " " & dgvISMPContacts(10, i).Value & ", " & _
                dgvISMPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)


                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyComplianceContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyComplianceContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvSSCPContacts.RowCount > 0 Then
                i = dgvSSCPContacts.CurrentCell.RowIndex
                MailingAddress = dgvSSCPContacts(1, i).Value & vbCrLf & _
                dgvSSCPContacts(2, i).Value & vbCrLf & _
                dgvSSCPContacts(7, i).Value & vbCrLf & _
                dgvSSCPContacts(9, i).Value & " " & dgvSSCPContacts(10, i).Value & ", " & _
                dgvSSCPContacts(11, i).Value

                Clipboard.SetDataObject(MailingAddress, True)


                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCopyGECOContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyGECOContact.Click
        Try
            Dim MailingAddress As String = ""
            Dim i As Integer = 0

            If dgvGECOContacts.RowCount > 0 Then
                i = dgvGECOContacts.CurrentCell.RowIndex
                MailingAddress = dgvGECOContacts(2, i).Value & vbCrLf & _
                dgvGECOContacts(6, i).Value & vbCrLf & _
                dgvGECOContacts(7, i).Value & vbCrLf & _
                dgvGECOContacts(8, i).Value & " " & dgvGECOContacts(9, i).Value & ", " & _
                dgvGECOContacts(10, i).Value

                Clipboard.SetDataObject(MailingAddress, True)

                MsgBox(MailingAddress, MsgBoxStyle.Information, "Mailing Address")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadCompliaceColor()
        Try
            For Each row As DataGridViewRow In dgvISMPWork.Rows
                If Not row.IsNewRow Then
                    If Not row.Cells(16).Value Is DBNull.Value Then
                        temp = row.Cells(16).Value
                        If row.Cells(16).Value = "True" Then
                            row.DefaultCellStyle.BackColor = Color.Pink
                        End If
                    End If
                    If Not row.Cells(10).Value Is DBNull.Value Then
                        temp = row.Cells(10).Value
                        If row.Cells(10).Value = "Not In Compliance" Then
                            row.DefaultCellStyle.BackColor = Color.Tomato
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub mmiPrintFacilitySummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPrintFacilitySummary.Click
        Try
            If mtbAIRSNumber.Text = "" Or mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Enter a valid AIRS # first", MsgBoxStyle.Information, "Facility Summary")
                Exit Sub
            End If
            If FacilityPrintOut Is Nothing Then
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IAIPFacilitySummaryPrint
                FacilityPrintOut.Show()
            Else
                FacilityPrintOut.Dispose()
                FacilityPrintOut = New IAIPFacilitySummaryPrint
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IAIPFacilitySummaryPrint
                FacilityPrintOut.Show()
            End If
            FacilityPrintOut.mtbAIRSNumber.Text = mtbAIRSNumber.Text
            FacilityPrintOut.txtFacilityName.Text = txtFacilityName.Text

            FacilityPrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtDateSubmitted_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateSubmitted.TextChanged
        Try
            If txtTotalFeesPaid.Text <> "" Then
                If txtTotalFeesPaid.Text < txtFeesTotal.Text Then
                    txtTotalFeesPaid.BackColor = Color.Tomato
                Else
                    If txtTotalFeesPaid.Text > txtFeesTotal.Text Then
                        txtTotalFeesPaid.BackColor = Color.LightBlue
                    Else
                        txtTotalFeesPaid.BackColor = Color.LightGray
                    End If
                End If
            Else
                txtTotalFeesPaid.BackColor = Color.LightGray
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiNewFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiNewFacility.Click

    End Sub
End Class