Imports Oracle.ManagedDataAccess.Client

Public Class SSCPFCEWork
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsFCE As DataSet
    Dim daFCE As OracleDataAdapter
    Dim dsISMP As DataSet
    Dim daISMP As OracleDataAdapter
    Dim dsInspections As DataSet
    Dim daInspections As OracleDataAdapter
    Dim dsACC As DataSet
    Dim daACC As OracleDataAdapter
    Dim dsReport As DataSet
    Dim daReport As OracleDataAdapter
    Dim dsNotifications As DataSet
    Dim daNotifications As OracleDataAdapter
    Dim dsEnforcement As DataSet
    Dim daEnforcement As OracleDataAdapter
    Dim dsPerformanceTest As DataSet
    Dim daPerformanceTest As OracleDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter


    Private Sub SSCPFCE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            DTPFCECompleteDate.Text = OracleDate
            LoadHeaderData()
            LoadFCEDataset()
            FillFCEData()

            FormatFCEInspection()
            FormatFCEACC()
            FormatFCEReports()
            FormatFCECorrespondance()
            FormatFCEPerformanceTests()
            FormatFCEEnforcement()
            FormatISMPSummaryReports()

            DTPFCECompleteDate.Value = OracleDate
            DTPFilterStartDate.Value = Format(Date.Today.AddDays(-365), "dd-MMM-yyyy")
            DTPFilterEndDate.Value = OracleDate

            If AccountFormAccess(50, 1) = "1" Or AccountFormAccess(50, 2) = "1" Or AccountFormAccess(50, 3) = "1" Or AccountFormAccess(50, 4) = "1" Then
            Else
                MenuSave.Visible = False
                TBFCE.Buttons.Remove(TbbSave)
            End If

            TabControlFCE.TabPages.Remove(TabPageFCEPrint)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        '   Me.rvFCE.RefreshReport()
    End Sub
#Region "Page Load Subs"
    Sub LoadHeaderData()
        Dim temp As String
        Dim Street2 As String

        Try

            SQL = "Select strFacilityName, " & _
            "strFacilityStreet1, strFacilityStreet2, " & _
            "strFacilityCity, strFacilityZipCode, " & _
            "strCountyName " & _
            "from AIRBRANCH.VW_APBFacilityLocation " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist Then
                If txtFacilityInformation.Text = "" Then
                    txtFacilityInformation.Text = txtAirsNumber.Text
                End If
                temp = Mid(dr.Item("strFacilityZipCode"), 1, 5)
                If Mid(dr.Item("strFacilityZipCode"), 6) <> "" Then
                    temp = temp & "-" & Mid(dr.Item("strFacilityZipCode"), 6)
                End If
                If dr.Item("strFacilityStreet2") = "N/A" Then
                    Street2 = ""
                Else
                    Street2 = dr.Item("strFacilityStreet2") & vbCrLf
                End If

                txtFacilityInformation.Text = txtFacilityInformation.Text & " - " & _
                dr.Item("strFacilityName") & vbCrLf & _
                dr.Item("strFacilityStreet1") & vbCrLf & _
                Street2 & _
                dr.Item("StrFacilityCity") & ", GA " & temp & _
                vbCrLf & _
                "County - " & dr.Item("strCountyName")
            End If
            dr.Close()

            SQL = "Select " & _
            "strClass, strAIRProgramCodes " & _
            "from AIRBRANCH.APBHeaderData " & _
            "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            recExist = dr.Read
            If recExist = True Then
                txtFacilityInformation.Text = txtFacilityInformation.Text & vbCrLf & vbCrLf & "Classification - " & dr.Item("strClass") & vbCrLf & _
                "Air Program Code(s) - " & vbCrLf
                AddAirProgramCodes(dr.Item("StrAirProgramCodes"))
            End If
            dr.Close()

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadFCEDataset()
        Try


            SQL = "select " & _
            "AIRBRANCH.SSCPFCE.strFCENumber, " & _
            "strFCEYear as FCEYear " & _
            "from AIRBRANCH.SSCPFCE, AIRBRANCH.SSCPFCEMaster " & _
            "Where AIRBRANCH.SSCPFCE.strFCENumber = AIRBRANCH.SSCPFCEMaster.strFCENumber " & _
            "and AIRBRANCH.SSCPFCEMaster.strairsnumber = '0413" & txtAirsNumber.Text & "' " & _
            "order by datFCECompleted DESC "

            'SQL2 = "Select distinct(numUserID), " & _
            '"(strLastName||', '||strFirstName) as StaffName, " & _
            '"strLastName " & _
            '"from AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCP_AuditedEnforcement " & _
            '"where numProgram = '4' " & _
            '"or numUserID = numStaffResponsible " & _
            '"or (numBranch = '5' " & _
            '"and strLastName = 'District') " & _
            '"order by strLastName "

            SQL2 = "select numuserID, Staff as StaffName, strLastName " & _
            "from AIRBranch.VW_ComplianceStaff "

            dsFCE = New DataSet
            dsStaff = New DataSet

            daFCE = New OracleDataAdapter(SQL, CurrentConnection)
            daStaff = New OracleDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFCE.Fill(dsFCE, "FCEdata")
            daStaff.Fill(dsStaff, "Staff")

            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub FillFCEData()
        Dim dtFCE As New DataTable
        Dim dtStaff As New DataTable
        Dim drDSRow As DataRow
        Dim drDSRow2 As DataRow
        Dim drNewRow As DataRow
        Dim i As Integer
        Dim flag As String

        Try


            dtStaff.Columns.Add("StaffName", GetType(System.String))
            dtStaff.Columns.Add("numUserID", GetType(System.String))

            For Each drDSRow2 In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow()
                drNewRow("StaffName") = drDSRow2("StaffName")
                drNewRow("numUserID") = drDSRow2("numUserID")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboReviewer
                .DataSource = dtStaff
                .DisplayMember = "StaffName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            cboReviewer.SelectedValue = UserGCode

            If dsFCE.Tables(0).Rows.Count = 0 Then
                cboFCEYear.Text = Date.Today.Year
                cboFCEYear.Items.Add(Date.Today.AddYears(1).Year)
                cboFCEYear.Items.Add(Date.Today.Year)
            Else
                dtFCE.Columns.Add("strFCENumber", GetType(System.String))
                dtFCE.Columns.Add("FCEYear", GetType(System.String))

                ' Only add next (calendar) year after October 1 of this year
                If Today >= New Date(Today.Year, 10, 1) Then
                    flag = False
                    For i = 0 To dsFCE.Tables(0).Rows.Count - 1
                        If dsFCE.Tables(0).Rows(i).Item("FCEYear") = Date.Today.AddYears(1).Year Then
                            flag = True
                        End If
                    Next
                    If flag = False Then
                        drNewRow = dtFCE.NewRow()
                        drNewRow("strFCENumber") = ""
                        drNewRow("FCEyear") = Date.Today.AddYears(1).Year
                        dtFCE.Rows.Add(drNewRow)
                    End If
                End If

                flag = False
                For i = 0 To dsFCE.Tables(0).Rows.Count - 1
                    If dsFCE.Tables(0).Rows(i).Item("FCEYear") = Date.Today.Year Then
                        flag = True
                    End If
                Next
                If flag = False Then
                    drNewRow = dtFCE.NewRow()
                    drNewRow("strFCENumber") = ""
                    drNewRow("FCEyear") = Date.Today.Year
                    dtFCE.Rows.Add(drNewRow)
                End If
                flag = False
                For i = 0 To dsFCE.Tables(0).Rows.Count - 1
                    If dsFCE.Tables(0).Rows(i).Item("FCEYear") = Date.Today.AddYears(-1).Year Then
                        flag = True
                    End If
                Next
                If flag = False Then
                    drNewRow = dtFCE.NewRow()
                    drNewRow("strFCENumber") = ""
                    drNewRow("FCEyear") = Date.Today.AddYears(-1).Year
                    dtFCE.Rows.Add(drNewRow)
                End If

                For Each drDSRow In dsFCE.Tables("FCEdata").Rows()
                    drNewRow = dtFCE.NewRow()
                    drNewRow("strFCENumber") = drDSRow("strFCENumber")
                    drNewRow("FCEYear") = drDSRow("FCEYear")
                    dtFCE.Rows.Add(drNewRow)
                Next

                txtFCENumber.DataBindings.Clear()

                With txtFCENumber
                    .DataBindings.Add(New Binding("Text", dtFCE, "strFCENumber"))
                End With

                With cboFCEYear
                    .DataSource = dtFCE
                    .DisplayMember = "FCEYear"
                    .ValueMember = "strFCENumber"
                    .SelectedIndex = 0
                End With
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub AddAirProgramCodes(ByRef AirProgramCode As String)
        Dim AirList As String = ""

        Try

            If Mid(AirProgramCode, 1, 1) = 1 Then
                AirList = vbTab & "0 - SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 2, 1) = 1 Then
                AirList = AirList & vbTab & "1 - Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 3, 1) = 1 Then
                AirList = AirList & vbTab & "3 - Non-Federal SIP" & vbCrLf
            End If
            If Mid(AirProgramCode, 4, 1) = 1 Then
                AirList = AirList & vbTab & "4 - CFC Tracking" & vbCrLf
            End If
            If Mid(AirProgramCode, 5, 1) = 1 Then
                AirList = AirList & vbTab & "6 - PSD" & vbCrLf
            End If
            If Mid(AirProgramCode, 6, 1) = 1 Then
                AirList = AirList & vbTab & "7 - NSR" & vbCrLf
            End If
            If Mid(AirProgramCode, 7, 1) = 1 Then
                AirList = AirList & vbTab & "8 - NESHAP" & vbCrLf
            End If
            If Mid(AirProgramCode, 8, 1) = 1 Then
                AirList = AirList & vbTab & "9 - NSPS" & vbCrLf
            End If
            If Mid(AirProgramCode, 9, 1) = 1 Then
                AirList = AirList & vbTab & "F - FESOP" & vbCrLf
            End If
            If Mid(AirProgramCode, 10, 1) = 1 Then
                AirList = AirList & vbTab & "A - Acid Precipitation" & vbCrLf
            End If
            If Mid(AirProgramCode, 11, 1) = 1 Then
                AirList = AirList & vbTab & "I - Native American" & vbCrLf
            End If
            If Mid(AirProgramCode, 12, 1) = 1 Then
                AirList = AirList & vbTab & "M - MACT" & vbCrLf
            End If
            If Mid(AirProgramCode, 13, 1) = 1 Then
                AirList = AirList & vbTab & "V - Title V Permit" & vbCrLf
            End If
            If AirList = "" Then
                AirList = vbTab & "No Air Program Codes available" & vbCrLf
            End If
            AirList = Mid(AirList, 1, (Len(AirList) - 2))

            txtFacilityInformation.Text = txtFacilityInformation.Text & AirList

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
#Region "Load Datasets"
    Sub LoadFCEInspectionData()
        Try

            SQL = "select AIRBRANCH.SSCPInspections.strTrackingNumber, " & _
            "to_char(datReceivedDate, 'dd-Mon-yyyy') as ReceivedDate,  " & _
            "(strLastName|| ', ' ||strFirstName) as ReviewingEngineer, " & _
            "to_char(datInspectionDateStart, 'dd-Mon-yyyy') as InspectionDateStart,  " & _
            "to_char(datInspectionDateStart, 'HH24:mi:ss') as InspectionTimeStart,  " & _
            "to_char(datInspectionDateEnd, 'dd-Mon-yyyy') as InspectionDateEnd,  " & _
            "to_char(datInspectionDateEnd, 'HH24:mi:ss') as InspectionTimeEnd,  " & _
            "strInspectionReason, strWeatherConditions, strInspectionGuide,  " & _
            "strFacilityOperating, strInspectionComplianceStatus,  " & _
            "to_char(datCompleteDate, 'dd-Mon-yyyy') as InspectionReportComplete,  " & _
            "to_char(datAcknoledgmentLetterSent, 'dd-Mon-yyyy') as AcknowledgmentLetterSent,  " & _
            "strInspectionComments  " & _
            "from AIRBRANCH.SSCPInspections, AIRBRANCH.EPDUserProfiles, AIRBRANCH.SSCPItemMaster  " & _
            "where  " & _
            "AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff  " & _
            "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPInspections.strTrackingNumber  " & _
            "and AIRBRANCH.SSCPItemMaster.strAIRSNumber = '0413" & txtAirsNumber.Text & "'  " & _
            "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "') " & _
            "or (datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')) " & _
            "and (strDelete is Null or strDelete <> 'True') " & _
            "Order by AIRBRANCH.SSCPInspections.strTrackingNumber DESC  "

            dsInspections = New DataSet

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            daInspections = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daInspections.Fill(dsInspections, "Inspections")
            dgrFCEInspections.DataSource = dsInspections
            dgrFCEInspections.DataMember = "Inspections"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadFCEACCData()
        Try

            SQL = "select AIRBRANCH.SSCPItemMaster.strTrackingNumber, " & _
                "to_char(datReceivedDate, 'dd-Mon-yyyy') as ReceivedDate, " & _
                "(strLastName|| ', ' ||strFirstName) as ReviewingEngineer, " & _
                "strPostMarkedOnTime, " & _
                "to_char(datPostMarkDate, 'dd-Mon-yyyy') as PostmarkDate, " & _
                "strSignedByRO, strCorrectACCForms, strTitleVConditionsListed, " & _
                "strACCCorrectlyFilledOut, strReportedDeviations, " & _
                "strDeviationsUnreported, strComments, strEnforcementNeeded, " & _
                "to_char(datCompleteDate, 'dd-Mon-yyyy') as CompleteDate " & _
                "from AIRBRANCH.SSCPACCS, AIRBRANCH.SSCPItemMaster, AIRBRANCH.EPDUSerProfiles " & _
                "where " & _
                "AIRBRANCH.EPDUSerProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strModifingperson " & _
                "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPACCS.strTrackingNumber " & _
                "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "') " & _
                " or " & _
                "(datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')) " & _
                "and AIRBRANCH.SSCPItemMaster.strAIrsnumber = '0413" & txtAirsNumber.Text & "' " & _
                "and (strDelete is Null or strDelete <> 'True') "

            dsACC = New DataSet

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            daACC = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daACC.Fill(dsACC, "ACC")
            dgrFCEACC.DataSource = dsACC
            dgrFCEACC.DataMember = "ACC"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadFCEReports()
        Try

            SQL = "select AIRBRANCH.SSCPItemMaster.strTrackingNumber, " & _
                 "to_char(datReceivedDate, 'dd-Mon-yyyy') as ReceivedDate, " & _
                 "strReportPeriod, " & _
                 "to_char(DatReportingPeriodStart, 'dd-Mon-yyyy') as ReportingStartDate, " & _
                 "to_char(datReportingPeriodEnd, 'dd-Mon-yyyy') as ReportingEndDate, " & _
                 "strReportingPeriodComments, " & _
                 "to_char(datReportDueDate, 'dd-Mon-yyyy') as ReportDueDate, " & _
                 "to_char(datSentByFacilityDate, 'dd-Mon-yyyy') as DateSentByFacility, " & _
                 "strCompleteStatus, strEnforcementNeeded, strShowDeviation, " & _
                 "strGeneralComments " & _
                 "from AIRBRANCH.SSCPREports, AIRBRANCH.SSCPItemMaster " & _
                 "where AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPREports.strTrackingNumber " & _
                 "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "') " & _
                 "or " & _
                 "(datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')) " & _
                  "and (strDelete is Null or strDelete <> 'True') " & _
                 "and AIRBRANCH.SSCPItemMaster.strAIrsnumber = '0413" & txtAirsNumber.Text & "' "

            dsReport = New DataSet

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            daReport = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daReport.Fill(dsReport, "Reports")
            dgrFCEReports.DataSource = dsReport
            dgrFCEReports.DataMember = "Reports"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadFCECorrespondance()
        Try

            SQL = "select AIRBRANCH.SSCPItemMaster.strTrackingNumber, " & _
                 "to_char(datReceivedDate, 'dd-Mon-yyyy') as ReceivedDate, " & _
                 "CASE " & _
                 "    when strNotificationDue = 'True' then to_char(datNotificationDue, 'dd-Mon-yyyy') " & _
                 "    when strNotificationDue = 'False' then 'No Due Date' " & _
                 "End as NotificationDate, " & _
                 "CASE " & _
                 "    when strNotificationSent = 'True' then to_char(DatNotificationSent, 'dd-Mon-yyyy') " & _
                 "    when strNotificationSent = 'False' then 'Unknown' " & _
                 "End as NotificationSent, " & _
                 "CASE " & _
                 "    when strNotificationType = '01' then strNotificationTypeOther " & _
                 "    ELSE (select strNotificationDesc " & _
                 "     from AIRBRANCH.LookUPSSCPNotifications, AIRBRANCH.SSCPNotifications " & _
                 "     where AIRBRANCH.LookUPSSCPNotifications.strNotificationKey = AIRBRANCH.sscpNotifications.strnotificationType " & _
                 "     and AIRBRANCH.sscpNotifications.strTrackingNumber = AIRBRANCH.SSCPItemMaster.strTrackingNumber) " & _
                 "END as Notification, " & _
                 "strNotificationComment " & _
                 "from AIRBRANCH.SSCPNotifications, AIRBRANCH.SSCPItemMaster " & _
                 "where AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPNotifications.strTrackingNumber " & _
                 "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "') " & _
                 "or " & _
                 "(datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')) " & _
                  "and (strDelete is Null or strDelete <> 'True') " & _
                 "and AIRBRANCH.SSCPItemMaster.strAIrsnumber = '0413" & txtAirsNumber.Text & "' "

            dsNotifications = New DataSet

            Dim cmd As New OracleCommand(SQL, CurrentConnection)

            daNotifications = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daNotifications.Fill(dsNotifications, "Notifications")
            dgrFCECorrespondance.DataSource = dsNotifications
            dgrFCECorrespondance.DataMember = "Notifications"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub       'Add Word documents when appropriate
    Sub LoadFCESummaryReports()
        Try

            SQL = "Select AIRBRANCH.ISMPReportInformation.strReferenceNumber,  " & _
             "strEmissionSource, AIRBRANCH.LookUPPollutants.strPollutantDescription,  " & _
             "AIRBRANCH.ISMPReportType.strReportType,  " & _
             "(strLastName|| ', ' ||strFirstName) as ReviewingEngineer,  " & _
             "to_char(datTestDateStart, 'dd-Mon-yyyy') as TestDateStart,  " & _
             "to_char(datReceivedDate, 'dd-Mon-yyyy') as REceivedDate,  " & _
             "Case  " & _
             "  when datCompleteDate = '04-Jul-1776' Then 'Open'  " & _
             "  when datCompleteDate <> '04-Jul-1776' Then to_char(datCompleteDate, 'dd-Mon-yyyy')  " & _
             "END as CompleteDate,  " & _
             "AIRBRANCH.LookUPISMPComplianceStatus.strComplianceStatus,  " & _
             "Case  " & _
             "  when strClosed = 'False' then 'Open'   " & _
             "  when strClosed = 'True' then 'Closed'  " & _
             "END as Status,  " & _
             "mmoCommentArea, AIRBRANCH.ISMPDocumentType.strDocumentType,   " & _
             "strApplicableRequirement,  " & _
             "AIRBRANCH.LookUpEPDUnits.strUnitDesc  " & _
             "from AIRBRANCH.ISMPMaster, AIRBRANCH.ISMPReportInformation,  " & _
             "AIRBRANCH.LookUPPollutants, AIRBRANCH.ISMPReportType, AIRBRANCH.EPDUserProfiles,  " & _
             "AIRBRANCH.LookUPISMPComplianceStatus, AIRBRANCH.ISMPDocumentType,  " & _
             "AIRBRANCH.LookUpEPDUnits    " & _
             "where  " & _
             "AIRBRANCH.ISMPReportInformation.strREferenceNumber = AIRBRANCH.ISMPMaster.strReferenceNumber  " & _
             "and AIRBRANCH.LookUPPollutants.strPOllutantCode = AIRBRANCH.ISMPReportInformation.strPOllutant  " & _
             "and AIRBRANCH.ISMPReportType.strKey = AIRBRANCH.ISMPReportInformation.strReportType  " & _
             "and AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.ISMPReportInformation.strReviewingEngineer  " & _
             "and AIRBRANCH.LookUPISMPComplianceStatus.strComplianceKey = AIRBRANCH.ISMPReportInformation.strComplianceStatus  " & _
             "and AIRBRANCH.ISMPDocumentType.strKey = AIRBRANCH.ISMPReportInformation.strDocumentType  " & _
             "and strDelete is Null  " & _
             "and AIRBRANCH.ISMPReportInformation.strReviewingUnit = to_char(AIRBRANCH.LookUpEPDUnits.numUnitCode)   " & _
             "and strAIRSNumber = '0413" & txtAirsNumber.Text & "'   " & _
             "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')   " & _
              "or  (datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "'))   " & _
             "and (strDelete is Null or strDelete <> 'True')   " & _
             "order by AIRBRANCH.ISMPReportInformation.strReferenceNumber DESC  "

            dsISMP = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)
            daISMP = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daISMP.Fill(dsISMP, "ISMPWork")
            dgrISMPSummaryReports.DataSource = dsISMP
            dgrISMPSummaryReports.DataMember = "ISMPWork"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadFCEPerformanceTests()
        Try

            SQL = "Select " & _
                 "AIRBRANCH.SSCPTestReports.strTrackingNumber, " & _
                 "to_char(datReceivedDate, 'dd-Mon-yyyy') as ReceivedDate, " & _
                 "(strLastName|| ', ' ||strFirstName) as ReviewingEngineer, " & _
                 "strReferenceNumber, strTestReportComments, " & _
                 "to_Char(datCompleteDate, 'dd-Mon-yyyy') as CompleteDate, " & _
                 "strTestReportFollowUp " & _
                 "from AIRBRANCH.SSCPItemMaster, AIRBRANCH.SSCPTestReports, " & _
                 "AIRBRANCH.EPDUserProfiles  " & _
                 "where " & _
                 "AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCPItemMaster.strResponsibleStaff (+) " & _
                 "and AIRBRANCH.SSCPItemMaster.strTrackingNumber = AIRBRANCH.SSCPTestReports.strTrackingNumber " & _
                 "and ((datCompleteDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "') " & _
                 " or " & _
                 "(datReceivedDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "')) " & _
                  "and (strDelete is Null or strDelete <> 'True') " & _
                 "and AIRBRANCH.SSCPItemMaster.strAIrsnumber = '0413" & txtAirsNumber.Text & "' "

            dsPerformanceTest = New DataSet

            daPerformanceTest = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daPerformanceTest.Fill(dsPerformanceTest, "PerformanceTests")
            dgrPerformanceTests.DataSource = dsPerformanceTest
            dgrPerformanceTests.DataMember = "PerformanceTests"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub LoadFCEEnforcement()
        Try

            SQL = "Select " & _
                 "AIRBRANCH.SSCP_AuditedEnforcement.strEnforcementNumber, " & _
                 "case " & _
                 "when datEnforcementFinalized IS null then 'Open' " & _
                 "else to_char(datEnforcementFinalized, 'dd-Mon-yyyy') " & _
                 "ENd as EnforcementFinalized, " & _
                 "(strLastName|| ', ' ||strFirstName) as StaffResponsible, " & _
                 "strActionType  " & _
                 "from AIRBRANCH.SSCP_AuditedEnforcement, AIRBRANCH.EPDUserProfiles " & _
                 "where AIRBRANCH.EPDUserProfiles.numUserID = AIRBRANCH.SSCP_AuditedEnforcement.numStaffResponsible " & _
                  "and datDiscoveryDate between '" & DTPFilterStartDate.Text & "' and '" & DTPFilterEndDate.Text & "' " & _
                  "and (strStatus is Null or strStatus <> 'True') " & _
                 "and strAIRSnumber = '0413" & txtAirsNumber.Text & "' "

            dsEnforcement = New DataSet

            cmd = New OracleCommand(SQL, CurrentConnection)

            daEnforcement = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daEnforcement.Fill(dsEnforcement, "Enforcements")
            dgrFCEEnforcement.DataSource = dsEnforcement
            dgrFCEEnforcement.DataMember = "Enforcements"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
#End Region
#Region "FormatDataGrids"
    Sub FormatFCEInspection()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Inspections"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReviewingEngineer"
            objtextcol.HeaderText = "Reviewing Engineer"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionDateStart"
            objtextcol.HeaderText = "Inspection Start Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionTimeStart"
            objtextcol.HeaderText = "Inspection Start Time"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionTimeEnd"
            objtextcol.HeaderText = "Inspection End Time"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strInspectionReason"
            objtextcol.HeaderText = "Inspection Reason"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strWeatherConditions"
            objtextcol.HeaderText = "Weather Conditions"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strInspectionGuide"
            objtextcol.HeaderText = "Inspection Guide"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    10
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "InspectionReportComplete"
            objtextcol.HeaderText = "Inspection Complete Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    11
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "AcknowledgmentLetterSent"
            objtextcol.HeaderText = "Date Acknowledgement Letter Sent"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    12
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strInspectionComments"
            objtextcol.HeaderText = "Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFCEInspections.TableStyles.Clear()
            dgrFCEInspections.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFCEInspections.CaptionText = "Inspections"
            dgrFCEInspections.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub FormatFCEACC()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ACC"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReviewingEngineer"
            objtextcol.HeaderText = "Reviewing Engineer"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPostMarkedOnTime"
            objtextcol.HeaderText = "Postmarked On Time"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "PostmarkDate"
            objtextcol.HeaderText = "Date Postmarked"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strSignedByRO"
            objtextcol.HeaderText = "Signed by Responsible Official"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strCorrectACCForms"
            objtextcol.HeaderText = "Correct Forms Used"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTitleVConditionsListed"
            objtextcol.HeaderText = "Listed Title V Conditions"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strACCCorrectlyFilledOut"
            objtextcol.HeaderText = "ACC Correctly Filled Out"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    10
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReportedDeviations"
            objtextcol.HeaderText = "Deviations Reported"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    11
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strDeviationsUnreported"
            objtextcol.HeaderText = "Any Unreported Deviations"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    12
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strEnforcementNeeded"
            objtextcol.HeaderText = "Enforcement Needed"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    13
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "CompleteDate"
            objtextcol.HeaderText = "Date Completed"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    14
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strComments"
            objtextcol.HeaderText = "Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFCEACC.TableStyles.Clear()
            dgrFCEACC.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFCEACC.CaptionText = "Title V Annual Certifications"
            dgrFCEACC.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub FormatFCEReports()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Reports"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReportPeriod"
            objtextcol.HeaderText = "Reporting Period"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReportingStartDate"
            objtextcol.HeaderText = "Report Start Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReportingEndDate"
            objtextcol.HeaderText = "Report End Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReportingPeriodComments"
            objtextcol.HeaderText = "Reporting Period Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 225
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReportDueDate"
            objtextcol.HeaderText = "Report Due Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "DateSentByFacility"
            objtextcol.HeaderText = "Date Report Sent by Facility"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strCompleteStatus"
            objtextcol.HeaderText = "Report Complete Status"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    10
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strEnforcementNeeded"
            objtextcol.HeaderText = "Enforcement Needed"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    11
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strShowDeviation"
            objtextcol.HeaderText = "Deviations"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    12
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strGeneralComments"
            objtextcol.HeaderText = "Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFCEReports.TableStyles.Clear()
            dgrFCEReports.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFCEReports.CaptionText = "Reports"
            dgrFCEReports.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub FormatFCECorrespondance()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Notifications"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "NotificationDate"
            objtextcol.HeaderText = "Notification Due Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "NotificationSent"
            objtextcol.HeaderText = "Date Notification Sent"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Notification"
            objtextcol.HeaderText = "Notification Type"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strNotificationComment"
            objtextcol.HeaderText = "Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 225
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFCECorrespondance.TableStyles.Clear()
            dgrFCECorrespondance.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFCECorrespondance.CaptionText = "Notifications"
            dgrFCECorrespondance.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub     'Add Word documents when appropriate
    Sub FormatISMPSummaryReports()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "ISMPWork"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReferenceNumber"
            objtextcol.HeaderText = "Reference Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strEmissionSource"
            objtextcol.HeaderText = "Emission Source"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPollutantDescription"
            objtextcol.HeaderText = "Pollutant"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReportType"
            objtextcol.HeaderText = "Report Type"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings     5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strDocumentType"
            objtextcol.HeaderText = "Document Type"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6 
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strUnitTitle"
            objtextcol.HeaderText = "Reveiewing Unit"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReviewingEngineer"
            objtextcol.HeaderText = "Reveiewing Engineer"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "TestDateStart"
            objtextcol.HeaderText = "Test Date"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 90
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings     9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Received Date"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 90
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    10
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "CompleteDate"
            objtextcol.HeaderText = "Complete Date"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 90
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    11
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Status"
            objtextcol.HeaderText = "Report Open/Closed"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings     12
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strComplianceStatus"
            objtextcol.HeaderText = "Compliance Status"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 180
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings     13
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "mmoCommentAREA"
            objtextcol.HeaderText = "Comment Field"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrISMPSummaryReports.TableStyles.Clear()
            dgrISMPSummaryReports.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrISMPSummaryReports.CaptionText = "Performance Tests"
            dgrISMPSummaryReports.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub FormatFCEEnforcement()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Enforcements"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strEnforcementNumber"
            objtextcol.HeaderText = "Enforcement Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strActionType"
            objtextcol.HeaderText = "Enforcement Type"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "EnforcementFinalized"
            objtextcol.HeaderText = "Enforcement Resolved"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "StaffResponsible"
            objtextcol.HeaderText = "Staff Responsible"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrFCEEnforcement.TableStyles.Clear()
            dgrFCEEnforcement.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFCEEnforcement.CaptionText = "Enforcement Activity"
            dgrFCEEnforcement.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub FormatFCEPerformanceTests()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "PerformanceTests"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTrackingNumber"
            objtextcol.HeaderText = "Tracking Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReceivedDate"
            objtextcol.HeaderText = "Date Received"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ReviewingEngineer"
            objtextcol.HeaderText = "Staff Responsible"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReferenceNumber"
            objtextcol.HeaderText = "ISMP Reference Number"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "CompleteDate"
            objtextcol.HeaderText = "Complete Date"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestReportFollowUp"
            objtextcol.HeaderText = "Enforcement Needed"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestReportComments"
            objtextcol.HeaderText = "Comments"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 135
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrPerformanceTests.TableStyles.Clear()
            dgrPerformanceTests.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrPerformanceTests.CaptionText = "Performance Tests"
            dgrPerformanceTests.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region

#End Region
#Region "Delarations"
    Private Sub txtFCENumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFCENumber.TextChanged
        Try
            If txtFCENumber.Text = "" Then
                cboReviewer.SelectedValue = UserGCode
                rdbFCEComplete.Checked = True
                'rdbFCEIncomplete.Checked = False
                rdbFCEOnSite.Checked = False
                rdbFCENoOnsite.Checked = False
                DTPFCECompleteDate.Text = OracleDate
                txtFCEComments.Clear()
            Else
                SQL = "select " & _
                "strFCEStatus, " & _
                "to_char(datFCECompleted, 'dd-Mon-yyyy') as datFCECompleted,  " & _
                "strFCEComments,  " & _
                "Case " & _
                "  when strSiteInspection is Null then 'False' " & _
                "else strSiteInspection  " & _
                "End strSiteInspection,  " & _
                "strReviewer,  " & _
                "strFCEYear " & _
                "from AIRBRANCH.SSCPFCE  " & _
                "where strFCENumber = '" & txtFCENumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If dr.Item("strFCEStatus") = "True" Then
                        rdbFCEComplete.Checked = True
                        'rdbFCEIncomplete.Checked = False
                    Else
                        rdbFCEComplete.Checked = True
                        'rdbFCEIncomplete.Checked = True
                    End If
                    If IsDBNull(dr.Item("datFCECompleted")) Then
                        DTPFCECompleteDate.Text = OracleDate
                    Else
                        DTPFCECompleteDate.Text = dr.Item("datFCECompleted")
                    End If
                    If IsDBNull(dr.Item("strFCEComments")) Then
                        txtFCEComments.Text = "No Comments"
                    Else
                        txtFCEComments.Text = dr.Item("strFCEComments")
                    End If
                    If IsDBNull(dr.Item("strReviewer")) Then
                        cboReviewer.SelectedValue = UserGCode
                    Else
                        cboReviewer.SelectedValue = dr.Item("strReviewer")
                    End If
                    If IsDBNull(dr.Item("strSiteInspection")) Then
                        rdbFCEOnSite.Checked = False
                        rdbFCENoOnsite.Checked = True
                    Else
                        If dr.Item("strSiteInspection") = "True" Then
                            rdbFCEOnSite.Checked = True
                            rdbFCENoOnsite.Checked = False
                        Else
                            rdbFCEOnSite.Checked = False
                            rdbFCENoOnsite.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("strFCEYear")) Then
                        cboFCEYear.Text = cboFCEYear.Text
                    Else
                        cboFCEYear.Text = dr.Item("strFCEYear")
                    End If
                End If
                dr.Close()
                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewFCEData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewFCEData.LinkClicked
        Try

            LoadFCEInspectionData()
            LoadFCEACCData()
            LoadFCEReports()
            LoadFCECorrespondance()
            LoadFCEPerformanceTests()
            LoadFCESummaryReports()
            LoadFCEEnforcement()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#Region "Data Grid Mouse Up's"
    Private Sub dgrFCEACC_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFCEACC.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFCEACC.HitTest(e.X, e.Y)
        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFCEACC(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFCEACC(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFCEACC(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFCEACC(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrFCEACC(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrFCEACC(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrFCEACC(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrFCEACC(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrFCEACC(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrFCEACC(hti.Row, 9)) Then
                                                    Else
                                                        If IsDBNull(dgrFCEACC(hti.Row, 10)) Then
                                                        Else
                                                            If IsDBNull(dgrFCEACC(hti.Row, 11)) Then
                                                            Else
                                                                If IsDBNull(dgrFCEACC(hti.Row, 12)) Then
                                                                Else
                                                                    If IsDBNull(dgrFCEACC(hti.Row, 13)) Then
                                                                    Else
                                                                        txtACCTrackingNumber.Text = dgrFCEACC(hti.Row, 0)
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
                End If
            End If

        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrFCECorrespondance_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFCECorrespondance.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFCECorrespondance.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFCECorrespondance(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFCECorrespondance(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFCECorrespondance(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFCECorrespondance(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrFCECorrespondance(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrFCECorrespondance(hti.Row, 5)) Then
                                    Else
                                        txtNotificationTrackingNumber.Text = dgrFCECorrespondance(hti.Row, 0)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrFCEEnforcement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFCEEnforcement.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFCEEnforcement.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFCEEnforcement(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFCEEnforcement(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFCEEnforcement(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFCEEnforcement(hti.Row, 3)) Then
                            Else
                                txtEnforcement.Text = dgrFCEEnforcement(hti.Row, 0)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrFCEInspections_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFCEInspections.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFCEInspections.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFCEInspections(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFCEInspections(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFCEInspections(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFCEInspections(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrFCEInspections(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrFCEInspections(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrFCEInspections(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrFCEInspections(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrFCEInspections(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrFCEInspections(hti.Row, 9)) Then
                                                    Else
                                                        If IsDBNull(dgrFCEInspections(hti.Row, 10)) Then
                                                        Else
                                                            If IsDBNull(dgrFCEInspections(hti.Row, 11)) Then
                                                            Else
                                                                txtInspectionTrackingNumber.Text = dgrFCEInspections(hti.Row, 0)
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
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrISMPSummaryReports_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrISMPSummaryReports.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrISMPSummaryReports.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrISMPSummaryReports(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrISMPSummaryReports(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrISMPSummaryReports(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrISMPSummaryReports(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrISMPSummaryReports(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrISMPSummaryReports(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrISMPSummaryReports(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrISMPSummaryReports(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrISMPSummaryReports(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrISMPSummaryReports(hti.Row, 9)) Then
                                                    Else
                                                        If IsDBNull(dgrISMPSummaryReports(hti.Row, 10)) Then
                                                        Else
                                                            If IsDBNull(dgrISMPSummaryReports(hti.Row, 11)) Then
                                                            Else
                                                                If IsDBNull(dgrISMPSummaryReports(hti.Row, 12)) Then
                                                                Else
                                                                    txtISMPReferenceNumber.Text = dgrISMPSummaryReports(hti.Row, 0)
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
            End If
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrFCEReports_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrFCEReports.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrFCEReports.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrFCEReports(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrFCEReports(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrFCEReports(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrFCEReports(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrFCEReports(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrFCEReports(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrFCEReports(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrFCEReports(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrFCEReports(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrFCEReports(hti.Row, 9)) Then
                                                    Else
                                                        If IsDBNull(dgrFCEReports(hti.Row, 10)) Then
                                                        Else
                                                            If IsDBNull(dgrFCEReports(hti.Row, 11)) Then
                                                            Else
                                                                txtReportTrackingNumber.Text = dgrFCEReports(hti.Row, 0)
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
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub dgrPerformanceTests_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrPerformanceTests.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrPerformanceTests.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrPerformanceTests(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrPerformanceTests(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrPerformanceTests(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrPerformanceTests(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrPerformanceTests(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrPerformanceTests(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrPerformanceTests(hti.Row, 6)) Then
                                        Else
                                            txtPerformanceTests.Text = dgrPerformanceTests(hti.Row, 0)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAirsNumber.Text = Value
        End Set
    End Property
    Private Sub SSCPFCECheckList_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            SSCPFCE = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

#Region "Functions and Subs"
    Sub SaveFCE()
        Try

            If AccountFormAccess(50, 2) = "0" And AccountFormAccess(50, 3) = "0" And AccountFormAccess(50, 4) = "0" Then
                MsgBox("Insufficent permissions to save Full Compliance Evaluations.", MsgBoxStyle.Information, "Full Compliance Evaluation.")
            Else
                Dim FCENumber As String = ""
                Dim FCEStatus As String = ""
                Dim FCECompleteDate As String = ""
                Dim FCEComments As String = ""
                Dim ActionNumber As String = ""
                Dim StaffResponsible As String = ""
                Dim FCEOnSite As String = ""
                Dim FCEYear As String = ""
                Dim Classification As String = ""

                SQL = "Select strClass " & _
                "from AIRBRANCH.APBHeaderData " & _
                "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    Classification = dr.Item("strClass")
                End While
                dr.Close()

                FCEStatus = True
                'If rdbFCEComplete.Checked = True Then
                '    FCEStatus = True
                'Else
                '    FCEStatus = True
                'End If
                FCECompleteDate = DTPFCECompleteDate.Text
                If txtFCEComments.Text = "" Then
                    FCEComments = "N/A"
                Else
                    FCEComments = Replace(txtFCEComments.Text, "'", "''")
                End If
                If cboReviewer.SelectedValue = "" Then
                    StaffResponsible = UserGCode
                Else
                    StaffResponsible = cboReviewer.SelectedValue
                End If
                If rdbFCEOnSite.Checked = True Then
                    FCEOnSite = "True"
                Else
                    FCEOnSite = "False"
                End If
                If cboFCEYear.Text <> "" Then
                    FCEYear = cboFCEYear.Text
                Else
                    FCEYear = Date.Now.Year
                End If

                If txtFCENumber.Text = "" Then
                    SQL = "Select Max(strFCENumber) as FCENumber " & _
                    "from AIRBRANCH.SSCPFCEMaster "

                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FCENumber = dr.Item("FCENumber")
                    End While
                    FCENumber += 1

                    SQL = "Insert into AIRBRANCH.SSCPFCEMaster " & _
                    "(strFCENumber, strAIRSNumber, " & _
                    "strModifingPerson, datModifingDate) " & _
                    "values " & _
                    "('" & FCENumber & "', '0413" & txtAirsNumber.Text & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader

                    SQL = "Insert into AIRBRANCH.SSCPFCE " & _
                    "(strFCENumber, strFCEStatus, strReviewer, " & _
                    "datFCECompleted, strFCEComments, strModifingPerson, " & _
                    "datModifingDate, strSiteInspection, strFCEYear) " & _
                    "values " & _
                    "('" & FCENumber & "', '" & FCEStatus & "',  '" & StaffResponsible & "', " & _
                    "'" & FCECompleteDate & "', '" & FCEComments & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '" & FCEOnSite & "', '" & FCEYear & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader

                    If Classification = "A" Or Classification = "SM" Then
                        SQL = "Select strAFSActionNumber " & _
                        "from AIRBRANCH.APBSupplamentalData " & _
                        "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            ActionNumber = dr.Item("strAFSActionNumber")
                        End While
                        dr.Close()

                        'This was removed when AFS went to 5 digits. 
                        'Select Case ActionNumber.Length
                        '    Case 0
                        '        ActionNumber = "001"
                        '    Case 1
                        '        ActionNumber = "00" & ActionNumber
                        '    Case 2
                        '        ActionNumber = "0" & ActionNumber
                        '    Case 3
                        '        ActionNumber = ActionNumber
                        '    Case Else
                        '        ActionNumber = ActionNumber
                        'End Select

                        SQL = "Insert into AIRBRANCH.AFSSSCPFCERecords " & _
                        "(strFCENumber, strAFSActionNumber, " & _
                        "strUpDateStatus, strModifingPerson, " & _
                        "datModifingDate) " & _
                        "values " & _
                        "('" & FCENumber & "', '" & ActionNumber & "', " & _
                        "'A', '" & UserGCode & "', " & _
                        "'" & OracleDate & "') "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        ActionNumber = CStr(CInt(ActionNumber) + 1)

                        'This was removed when AFS went to 5 digits. 
                        'Select Case ActionNumber.Length
                        '    Case 0
                        '        ActionNumber = "001"
                        '    Case 1
                        '        ActionNumber = "00" & ActionNumber
                        '    Case 2
                        '        ActionNumber = "0" & ActionNumber
                        '    Case 3
                        '        ActionNumber = ActionNumber
                        '    Case Else
                        '        ActionNumber = ActionNumber
                        'End Select

                        SQL = "Update AIRBRANCH.APBSupplamentalData set " & _
                        "strAFSActionNUmber = '" & ActionNumber & "' " & _
                        "where strAIRSNumber = '0413" & txtAirsNumber.Text & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If
                Else
                    FCENumber = txtFCENumber.Text

                    SQL = "select strFCENumber " & _
                    "from AIRBRANCH.SSCPFCE " & _
                    "where strFCENumber = '" & FCENumber & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        SQL = "Update AIRBRANCH.SSCPFCEMaster set " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "' " & _
                        "where strFCENumber = '" & FCENumber & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        dr = cmd.ExecuteReader

                        SQL = "Update AIRBRANCH.SSCPFCE Set " & _
                        "strFCEStatus = '" & FCEStatus & "', " & _
                        "strReviewer = '" & StaffResponsible & "', " & _
                        "DatFCECompleted = '" & FCECompleteDate & "', " & _
                        "strFCEComments = '" & FCEComments & "', " & _
                        "strModifingPerson = '" & UserGCode & "', " & _
                        "datModifingDate = '" & OracleDate & "', " & _
                        "strSiteInspection = '" & FCEOnSite & "', " & _
                        "strFCEYear = '" & FCEYear & "' " & _
                        "where strFCENumber = '" & FCENumber & "'"

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        dr = cmd.ExecuteReader
                    End If
                End If

                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If

                txtFCENumber.Text = FCENumber
                LoadFCEDataset()
                FillFCEData()
                txtFCENumber.Text = FCENumber
                MsgBox("FCE Saved", MsgBoxStyle.Information, "Full Compliance Evaluation")

            End If

        Catch ex As Exception
            ErrorReport(txtFCENumber.Text & vbCrLf & txtAirsNumber.Text & vbCrLf & ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Sub Clear()
        Try
            rdbFCEComplete.Checked = True
            'rdbFCEIncomplete.Checked = True
            DTPFCECompleteDate.Text = OracleDate
            DTPFCECompleteDate.Checked = False
            txtFCEComments.Clear()

            dsISMP = New DataSet
            dgrISMPSummaryReports.Refresh()

            dsInspections = New DataSet
            dgrFCEInspections.DataSource = dsInspections

            dsACC = New DataSet
            dgrFCEACC.DataSource = dsACC

            dsReport = New DataSet
            dgrFCEReports.DataSource = dsReport

            dsNotifications = New DataSet
            dgrFCECorrespondance.DataSource = dsNotifications

            dsEnforcement = New DataSet
            dgrISMPSummaryReports.DataSource = dsISMP
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub

#Region "Open Subborting Documents"
    Private Sub llbFCEInspections_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFCEInspections.LinkClicked
        Try

            If txtInspectionTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtInspectionTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "FCE Checklist"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbFCEACC_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFCEACC.LinkClicked
        Try

            If txtACCTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtACCTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "FCE Checklist"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbFCEReports_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFCEReports.LinkClicked
        Try

            If txtReportTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtReportTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "FCE Checklist"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbPerformanceTests_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbPerformanceTests.LinkClicked
        Try

            If txtPerformanceTests.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtPerformanceTests.Text
                SSCPReports.txtOrigin.Text = "FCE Checklist"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbNotification_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbNotification.LinkClicked
        Try

            If txtNotificationTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtNotificationTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "FCE Checklist"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbISMPSummaryReports_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbISMPSummaryReports.LinkClicked
        Dim temp As String = ""
        Try

            If txtISMPReferenceNumber.Text <> "" Then
                SQL = "Select strClosed " & _
                "from AIRBRANCH.ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtISMPReferenceNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strClosed")
                End While
                If temp = "True" Then
                    PrintOut = Nothing
                    If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                    PrintOut.txtReferenceNumber.Text = txtISMPReferenceNumber.Text
                    PrintOut.txtPrintType.Text = "SSCP"
                    PrintOut.Show()
                Else
                    MsgBox("This Test Summary has not been completely reviewed by ISMP Engineer", MsgBoxStyle.Information, "FCE Form")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub llbFCEEnforcement_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbFCEEnforcement.LinkClicked
        Try

            If txtEnforcement.Text <> "" Then
                Dim enfNum As String = txtEnforcement.Text
                If DAL.SSCP.EnforcementExists(enfNum) Then
                    OpenMultiForm("SscpEnforcement", enfNum)
                Else
                    MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region


#End Region

#Region "Change to Printer method"
    Sub LoadSSCPFCEReport()
        Dim Commissioner As String = ""
        Dim Director As String = ""
        Dim ProgramManager As String = ""
        Dim FiscalYear As String = ""
        Dim AIRSNumber As String = ""
        Dim FacilityName As String = ""
        Dim FacilityAddress As String = ""
        Dim Engineer As String = ""
        Dim UserUnit As String = ""
        Dim UserAFSGCode As String = ""
        Dim AirProgramCode As String = ""
        Dim FCECompleteDate As String = ""
        Dim FCEComment As String = ""
        Dim FiveYearDate As String = ""
        Dim OneYearDate As String = ""
        Dim TomorrowDate As String = ""

        Dim FCEInspections As String = ""
        Dim FCEPerformanceTests As String = ""
        Dim FCEReports As String = ""
        Dim FCEAnnualCertifications As String = ""
        Dim CTSInfo As String = ""
        Dim FeesInfo As String = ""
        Dim EnforcementInfo As String = ""
        Dim EISInfo As String = ""

        Dim rpt As New CRFCE3
        monitor.TrackFeature("Report." & rpt.ResourceName)
        Dim temp As String = ""

        If DTPFCECompleteDate.Text <> "" Then
            FiveYearDate = Format(DTPFCECompleteDate.Value.AddDays(-1825), "dd-MMM-yyyy")
            OneYearDate = Format(DTPFCECompleteDate.Value.AddDays(-365), "dd-MMM-yyyy")
            TomorrowDate = Format(DTPFCECompleteDate.Value.AddDays(1), "dd-MMM-yyyy")
        Else
            FiveYearDate = Format(Date.Today.AddDays(-1825), "dd-MMM-yyyy")
            OneYearDate = Format(Date.Today.AddDays(-365), "dd-MMM-yyyy")
            TomorrowDate = Format(Date.Today.AddDays(1), "dd-MMM-yyyy")
        End If



        Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
        Dim ParameterField As CrystalDecisions.Shared.ParameterField
        Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

        Try
            'Do this just once at the start
            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            SQL = "Select " & _
            "strCommissioner, strDirector, " & _
            "STRSSCPPROGRAMMANG, AIRBRANCH.APBFacilityInformation.strAIRSNumber, " & _
            "strFacilitYname, strFacilityStreet1, strFirstName, " & _
            "strLastName, strUnitDesc, " & _
            "'G36' as strAFSGCode, " & _
            "strAirProgramCodes, strFCEYear, datFCECompleted, " & _
            "strFCEComments " & _
            "from AIRBRANCH.SSCPFCE, AIRBRANCH.SSCPFCEMaster, " & _
            "AIRBRANCH.APBFacilityInformation, AIRBRANCH.APBHeaderData, " & _
            "AIRBRANCH.EPDUserProfiles, AIRBRANCH.LookUpEPDUnits, " & _
            "AIRBRANCH.LookUpAPBManagement " & _
            "Where AIRBRANCH.SSCPFCE.strFCENumber = AIRBRANCH.SSCPFCEMaster.strFCENumber " & _
            "and AIRBRANCH.APBHeaderData.strAIRSNumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " & _
            "and AIRBRANCH.SSCPFCEMaster.strAIRSNumber = AIRBRANCH.APBHeaderData.strAIRSNumber " & _
            "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitCode (+) " & _
            "and strReviewer = numUserID  " & _
            "and AIRBRANCH.SSCPFCE.strFCENumber = '" & txtFCENumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                ' Show and select the printout tab
                If Not TabControlFCE.TabPages.Contains(TabPageFCEPrint) Then _
                    TabControlFCE.TabPages.Add(TabPageFCEPrint)
                TabControlFCE.SelectedTab = TabPageFCEPrint

                If IsDBNull(dr.Item("strFCEYear")) Then
                    FiscalYear = "N/A"
                Else
                    FiscalYear = dr.Item("strFCEYear")
                End If
                If IsDBNull(dr.Item("strCommissioner")) Then
                    Commissioner = "N/A"
                Else
                    Commissioner = dr.Item("strCommissioner")
                End If
                If IsDBNull(dr.Item("strDirector")) Then
                    Director = "N/A"
                Else
                    Director = dr.Item("strDirector")
                End If
                If IsDBNull(dr.Item("STRSSCPPROGRAMMANG")) Then
                    ProgramManager = "N/A"
                Else
                    ProgramManager = dr.Item("STRSSCPPROGRAMMANG")
                End If
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = "N/A"
                Else
                    AIRSNumber = Mid(dr.Item("strAIRSNumber"), 5)
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = "N/A"
                Else
                    FacilityName = dr.Item("strFacilityname")
                End If
                If IsDBNull(dr.Item("strFacilityStreet1")) Then
                    FacilityAddress = "N/A"
                Else
                    FacilityAddress = dr.Item("strFacilityStreet1")
                End If
                If IsDBNull(dr.Item("strFirstName")) Then
                    Engineer = ""
                Else
                    Engineer = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strLastName")) Then
                    Engineer = Engineer
                Else
                    Engineer = Engineer & " " & dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    UserUnit = "N/A"
                Else
                    UserUnit = dr.Item("strUnitDesc")
                End If
                If IsDBNull(dr.Item("strAFSGCode")) Then
                    UserAFSGCode = "N/A"
                Else
                    UserAFSGCode = dr.Item("strAFSGCode")
                End If
                If IsDBNull(dr.Item("strAirProgramCodes")) Then
                    AirProgramCode = "N/A"
                Else
                    AirProgramCode = dr.Item("strAirProgramCodes")
                End If
                If IsDBNull(dr.Item("datFCECompleted")) Then
                    FCECompleteDate = "N/A"
                Else
                    FCECompleteDate = Format(dr.Item("datFCECompleted"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr.Item("strFCEComments")) Then
                    FCEComment = "N/A"
                Else
                    FCEComment = dr.Item("strFCEComments")
                End If
            End If

            dr.Close()

            If recExist = True Then
                FCEInspections = ""

                'FCE Inspections

                'SQL = "Select " & _
                '"STRAIRSNUMBER, STRTRACKINGNUMBER, " & _
                '"INSPECTIONDATE, INSFOLLOWUP, " & _
                '"INSPECTIONGUIDE, INSPECTINGSTAFF, " & _
                '"STRINSPECTIONCOMMENTS " & _
                '"from AIRBranch.VW_FCE_InspectionData " & _
                '"where strAIRSNumber  = '0413" & txtAirsNumber.Text & "'  " & _
                '"and Inspectiondate between '" & FiveYearDate & "' and '" & DTPFCECompleteDate.Text & "' " & _
                '"order by INSPECTIONDATE desc "

                SQL = "Select " & _
               "tblVW_FCE_INSPECTIONDATA.strTrackingNumber, tblVW_FCE_INSPECTIONDATA.strAIRSNumber, " & _
               "TO_CHAR(tblVW_FCE_INSPECTIONDATA.datInspectionDate, 'dd-Mon-yyyy') AS STRINSPECTIONDATE, " & _
               "tblVW_FCE_INSPECTIONDATA.STRINSPFOLLOWUP, tblVW_FCE_INSPECTIONDATA.STRINSPECTINGSTAFF, " & _
               "tblVW_FCE_INSPECTIONDATA.STRINSPECTIONCOMMENTS, tblVW_FCE_INSPECTIONDATA.STRINSPECTIONREASON " & _
               "from (SELECT AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER, " & _
               "AIRBRANCH.SSCPITEMMASTER.STRAIRSNUMBER, " & _
               "AIRBRANCH.SSCPINSPECTIONS.DATINSPECTIONDATESTART AS DATINSPECTIONDATE, " & _
               "CASE WHEN AIRBRANCH.SSCPINSPECTIONS.STRINSPECTIONFOLLOWUP = 'False' " & _
               "THEN 'No' ELSE 'Yes' END STRINSPFOLLOWUP, " & _
               "(AIRBRANCH.EPDUSERPROFILES.STRLASTNAME || ', ' " & _
               "|| AIRBRANCH.EPDUSERPROFILES.STRFIRSTNAME) AS STRINSPECTINGSTAFF, " & _
               "AIRBRANCH.SSCPINSPECTIONS.STRINSPECTIONCOMMENTS, " & _
               "AIRBRANCH.SSCPINSPECTIONS.STRINSPECTIONREASON " & _
               "FROM AIRBRANCH.SSCPITEMMASTER INNER JOIN AIRBRANCH.SSCPINSPECTIONS " & _
               "ON AIRBRANCH.SSCPITEMMASTER.STRTRACKINGNUMBER = AIRBRANCH.SSCPINSPECTIONS.STRTRACKINGNUMBER " & _
               "LEFT JOIN AIRBRANCH.EPDUSERPROFILES " & _
               "ON AIRBRANCH.EPDUSERPROFILES.NUMUSERID = AIRBRANCH.SSCPITEMMASTER.STRRESPONSIBLESTAFF) " & _
               "tblVW_FCE_INSPECTIONDATA " & _
               "where tblVW_FCE_INSPECTIONDATA.strAIRSNumber  = '0413" & txtAirsNumber.Text & "' " & _
               "and tblVW_FCE_INSPECTIONDATA.datInspectionDate between '" & FiveYearDate & "' and '" & _
               TomorrowDate & "' " & _
               "order by datInspectionDate desc "


                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FCEInspections = FCEInspections & "GA EPD Tracking Number:  " & vbTab & dr.Item("strTrackingNumber") & vbCrLf & _
                             "Staff Responsible:       " & vbTab & vbTab & dr.Item("STRINSPECTINGSTAFF") & vbCrLf & _
                             "Inspection Date:         " & vbTab & vbTab & dr.Item("STRINSPECTIONDATE") & vbCrLf & _
                             "Reason for Inspection:          " & vbTab & vbTab & dr.Item("STRINSPECTIONREASON") & vbCrLf & _
                             "Follow Up Action Taken:  " & vbTab & dr.Item("STRINSPFOLLOWUP") & vbCrLf & _
                             "Comments:                " & vbCrLf & dr.Item("STRINSPECTIONCOMMENTS") & _
                             vbCrLf & vbCrLf

                    End While
                Else
                    FCEInspections = "N/A"

                End If

                'Performance Tests
                SQL = "select " & _
                "AIRSNUMBER, REVEIWINGSTAFF,  " & _
                "STRREFERENCENUMBER, TRFOLLOWUP,  " & _
                "STRTESTREPORTCOMMENTS, STRDOCUMENTTYPE,  " & _
                "STRREPORTTYPE, DATTESTDATESTART,  " & _
                "STRPOLLUTANTDESCRIPTION, STREMISSIONSOURCE,  " & _
                "STRAPPLICABLEREQUIREMENT, MMOCOMMENTAREA,  " & _
                "STRCOMPLIANCESTATUS  " & _
                "from airbranch.VW_FCE_PerformanceTest " & _
                "where AIRSNumber  = '" & txtAirsNumber.Text & "'   " & _
                "and datTestDateStart between '" & FiveYearDate & "' and '" & TomorrowDate & "' " & _
                "order by DATTESTDATESTART desc "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FCEPerformanceTests = FCEPerformanceTests & _
                           "GA EPD Reference Number:  " & dr.Item("strReferenceNumber") & vbCrLf & _
                           "Date Test Performed:      " & vbTab & dr.Item("DATTESTDATESTART") & vbCrLf & _
                           "Follow Up Action Taken:   " & vbTab & dr.Item("TRFOLLOWUP") & vbCrLf & _
                           "SSCP Comments: " & vbCrLf & vbTab & vbTab & vbTab & dr.Item("strTestReportComments") & vbCrLf & vbCrLf & _
                           "Test Type:                " & vbTab & dr.Item("strReportType") & " - " & dr.Item("strDocumentType") & vbCrLf & _
                           "Emission Source:          " & vbTab & dr.Item("strEmissionSource") & vbCrLf & _
                           "Pollutant:                " & vbTab & vbTab & dr.Item("strPollutantDescription") & vbCrLf & _
                           "ISMP Test Status:         " & vbTab & dr.Item("STRCOMPLIANCESTATUS") & vbCrLf & _
                           "ISMP Comments:" & vbCrLf & vbTab & vbTab & dr.Item("mmoCommentArea") & vbCrLf & vbCrLf & vbCrLf

                    End While
                Else
                    FCEPerformanceTests = "N/A"
                End If

                'Reports
                SQL = "select " & _
                "AIRSNUMBER, STRTRACKINGNUMBER, REPORTSTAFF,  " & _
                "STRREPORTPERIOD, REPORTSTART,  " & _
                "REPORTEND, STRREPORTINGPERIODCOMMENTS,  " & _
                "RECEIVEDDATE, REPORTFOLLOWUP, DATCOMPLETEDATE  " & _
                "from airbranch.VW_FCE_ReportData  " & _
                "where AIRSNumber  = '" & txtAirsNumber.Text & "' and " & _
                "to_date(reportstart) between  '" & FiveYearDate & "' and '" & TomorrowDate & "' " & _
                "order by reportstart desc "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FCEReports = FCEReports & "GA EPD Tracking Number:  " & vbTab & dr.Item("strTrackingNumber") & vbCrLf & _
                          "Type of Report:          " & vbTab & vbTab & dr.Item("strReportPeriod") & vbCrLf & _
                          "Date Report Received:   " & vbTab & dr.Item("RECEIVEDDATE") & vbCrLf & _
                          "Follow Up Action Taken:  " & vbTab & dr.Item("ReportFollowUp") & vbCrLf & _
                          "Comments:                " & vbCrLf & vbTab & vbTab & vbTab & dr.Item("STRREPORTINGPERIODCOMMENTS") & _
                            vbCrLf & vbCrLf
                    End While
                Else
                    FCEReports = "N/A"
                End If

                'ACCs 
                SQL = "select " & _
                "AIRSNUMBER, STRTRACKINGNUMBER, " & _
                "ACCSTAFF, RECEIVEDDATE, " & _
                "COMPLETEDATE, ACCFOLLOWUP, " & _
                "STRCOMMENTS " & _
                "from airbranch.VW_FCE_ACCData  " & _
                "where AIRSNumber  = '" & txtAirsNumber.Text & "' " & _
                "and to_date(CompleteDate)  between  '" & OneYearDate & "' and '" & TomorrowDate & "'  " & _
                "order by CompleteDate desc "

                cmd = New OracleCommand(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FCEAnnualCertifications = FCEAnnualCertifications & _
                        "GA EPD Tracking Number:  " & vbTab & dr.Item("strTrackingNumber") & vbCrLf & _
                          "Date Received:             " & vbTab & vbTab & dr.Item("RECEIVEDDATE") & vbCrLf & _
                          "Date Report Completed:     " & vbTab & dr.Item("COMPLETEDATE") & vbCrLf & _
                          "Follow Up Action Taken:  " & vbTab & dr.Item("ACCFollowUp") & vbCrLf & _
                          "Comments:                " & vbCrLf & vbTab & vbTab & vbTab & dr.Item("strComments") & vbCrLf & vbCrLf

                    End While
                Else
                    FCEAnnualCertifications = "N/A"
                End If
                'NOTE THAT V_Complaint_Information DOES NOT HAVE AIRBRANCH NAMESPACE. 
                'THIS VIEW IS NOT IN THE ACUTAL NAMESPACE SO IT DOES NOT NEED TO HAVE THE NAMESPACE IN THE SQL STATEMENT. 
                SQL = "select " & _
                "COMPLAINT_ID, NATURE, RECEIVED_DATE " & _
                "from V_Complaint_Information " & _
                "where Facility_ID like '%" & txtAirsNumber.Text & "%' " & _
                "and RECEIVED_DATE between '" & OneYearDate & "' and '" & TomorrowDate & "'  "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        CTSInfo = CTSInfo & _
                        "CTS Complaint ID:  " & vbTab & dr.Item("COMPLAINT_ID") & vbCrLf & _
                          "Date Received:             " & Format(dr.Item("RECEIVED_DATE"), "dd-MMM-yyyy") & vbCrLf & _
                          "Nature of Call:          " & vbCrLf & vbTab & vbTab & vbTab & dr.Item("NATURE") & vbCrLf & vbCrLf
                    End While
                Else
                    CTSInfo = "N/A"
                End If

                SQL = "select  " & _
                "AIRSNUMBER, NUMFEEYEAR,  " & _
                "PAYMENTSTATUS, STRIAIPDESC,  " & _
                "INVOICEDAMOUNT, PAYMENTAMOUNT " & _
                "from airbranch.VW_FCE_FeesData  " & _
                "where AIRSNumber  = '" & txtAirsNumber.Text & "'  " & _
                "and numFeeyear between '" & CDate(FiveYearDate).Year.ToString & "' " & _
                     "and '" & CDate(DTPFCECompleteDate.Text).Year.ToString & "'  " & _
                "order by numFeeyear desc "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        FeesInfo = FeesInfo & _
                        "Fee Year:  " & vbTab & dr.Item("NUMFEEYEAR") & vbCrLf & _
                        "Payment Stutus:          " & vbTab & dr.Item("PAYMENTSTATUS") & vbCrLf & _
                        "IAIP Status:             " & vbTab & dr.Item("STRIAIPDESC") & vbCrLf & _
                        "Invoiced Amount:             " & FormatCurrency(dr.Item("INVOICEDAMOUNT"), 2) & vbCrLf & _
                        "Payment Amount:            " & FormatCurrency(dr.Item("PAYMENTAMOUNT"), 2) & vbCrLf & vbCrLf
                    End While
                Else
                    FeesInfo = "N/A"
                End If

                'Enforcement
                SQL = "select " & _
                "AIRSNUMBER, strEnforcementNumber, " & _
                "STRENFORCEMENTFINALIZED,  " & _
                "FINALIZEDDATE, DISCOVERYDATE,  " & _
                "STAFFRESPONSIBLE, STRACTIONTYPE,  " & _
                "ACTIONDATE, ACTIONCOMMENT,  " & _
                "STRGENERALCOMMENTS " & _
                "from airbranch.VW_FCE_enforcement  " & _
                "where AIRSNumber  = '" & txtAirsNumber.Text & "'  " & _
                "and to_date(discoverydate) between '" & FiveYearDate & "' and '" & TomorrowDate & "'  " & _
                "order by discoverydate desc "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    cmd = New OracleCommand(SQL, CurrentConnection)

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If

                    dr = cmd.ExecuteReader
                    While dr.Read
                        EnforcementInfo = EnforcementInfo & _
                        "Enforcement #:    " & vbTab & dr.Item("strEnforcementNumber") & vbCrLf & _
                        "Type:             " & vbTab & vbTab & dr.Item("STRACTIONTYPE") & vbCrLf & _
                        "Date Resolved:    " & vbTab & dr.Item("FINALIZEDDATE") & vbCrLf & _
                        "Action Comments:  " & vbCrLf & dr.Item("ACTIONCOMMENT") & vbCrLf & vbCrLf & _
                        "General Comments: " & vbCrLf & dr.Item("STRGENERALCOMMENTS") & vbCrLf & vbCrLf
                    End While
                Else
                    EnforcementInfo = "N/A"
                End If

                EISInfo = "N/A"


                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Commissioner"
                spValue.Value = Commissioner
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

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

                ParameterField.ParameterFieldName = "FiscalYear"
                spValue.Value = "Sep 30, " & FiscalYear
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AIRSNumber"
                spValue.Value = AIRSNumber
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityName"
                spValue.Value = FacilityName
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FacilityAddress"
                spValue.Value = FacilityAddress
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "ComplianceEngineer"
                spValue.Value = Engineer
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Unit"
                spValue.Value = UserUnit
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "GCode"
                spValue.Value = UserAFSGCode
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                If Mid(AirProgramCode, 1, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "SIPSource"
                    spValue.Value = "SIP Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "SIPSource"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If Mid(AirProgramCode, 5, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "PSDSource"
                    spValue.Value = "PSD Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "PSDSource"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If Mid(AirProgramCode, 7, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "MACT61Source"
                    spValue.Value = "MACT 61 Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "MACT61Source"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If Mid(AirProgramCode, 8, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "NSPSSource"
                    spValue.Value = "NSPS Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "NSPSSource"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If Mid(AirProgramCode, 12, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "MACT63Source"
                    spValue.Value = "MACT 63 Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "MACT63Source"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                If Mid(AirProgramCode, 13, 1) = "1" Then
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TitleVSource"
                    spValue.Value = "Title V Subject"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                Else
                    'Do this at the beginning of every new entry 
                    ParameterField = New CrystalDecisions.Shared.ParameterField
                    spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                    ParameterField.ParameterFieldName = "TitleVSource"
                    spValue.Value = "N/A"
                    ParameterField.CurrentValues.Add(spValue)
                    ParameterFields.Add(ParameterField)
                End If

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FCECompleteDate"
                spValue.Value = FCECompleteDate
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FCEComments"
                spValue.Value = FCEComment
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Inspections"
                spValue.Value = FCEInspections
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "PerformanceTests"
                spValue.Value = FCEPerformanceTests
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "Reports"
                spValue.Value = FCEReports
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "AnnualCertifications"
                spValue.Value = FCEAnnualCertifications
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "CTSInfo"
                spValue.Value = CTSInfo
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "FeesInfo"
                spValue.Value = FeesInfo
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EnforcementInfo"
                spValue.Value = EnforcementInfo
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)

                'Do this at the beginning of every new entry 
                ParameterField = New CrystalDecisions.Shared.ParameterField
                spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

                ParameterField.ParameterFieldName = "EISInfo"
                spValue.Value = EISInfo
                ParameterField.CurrentValues.Add(spValue)
                ParameterFields.Add(ParameterField)


                'Load Variables into the Fields
                CRViewer.ParameterFieldInfo = ParameterFields

                'Display the Report
                CRViewer.ReportSource = rpt
                TabControlFCE.SelectedTab = TabPageFCEPrint

            Else
                TabControlFCE.TabPages.Remove(TabPageFCEPrint)
                MsgBox("Unable to Print at this time.", MsgBoxStyle.Information, "Print Out")
                PrintOut.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub





#End Region

#Region "Menu and toolbar"
    Private Sub MenuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuSave.Click
        SaveFCE()
    End Sub
    Private Sub MenuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuPrint.Click
        LoadSSCPFCEReport()
    End Sub
    Private Sub MenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClose.Click
        Me.Close()
    End Sub
    Private Sub MenuOpenHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuOpenHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub TBFCE_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFCE.ButtonClick
        Select Case TBFCE.Buttons.IndexOf(e.Button)
            Case 0
                SaveFCE()
            Case 1
                LoadSSCPFCEReport()
            Case Else
        End Select
    End Sub
#End Region

End Class