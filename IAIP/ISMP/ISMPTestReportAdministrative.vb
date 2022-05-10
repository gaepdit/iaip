Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class ISMPTestReportAdministrative
    Dim dtGrid As DataTable

#Region "Page Load"

    Private Sub ISMPTestReportAdministrative_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            DTPDateReceived.Value = Today
            DTPTestDateStart.Value = Today
            DTPTestDateEnd.Value = Today
            DTPTestDateStart.Value = Today
            DTPTestDateEnd.Value = Today

            DTPDateClosed.Value = Today
            rdbOpenReport.Checked = False
            rdbCloseReport.Checked = False

            FillPollutantCombo()
            FillTestingFirmsCombo()

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            dtpAddTestReportDateReceived.Value = Today
            DTPAddTestReportDateCompleted.Value = Today

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub FillFacilityAndAIRSCombos()
        Try

            Dim query As String = "select strFacilityName, SUBSTRING(strAIRSNumber, 5,8) as strAIRSNumber, " &
            "strFacilityStreet1, strFacilityCity, strFacilityState, " &
            "strFacilityZipCode " &
            "from APBFacilityInformation " &
            "union select ' ', ' ', ' ', ' ', ' ', ' ' " &
            "order by strFacilityName"

            Dim dtFacility As DataTable = DB.GetDataTable(query)

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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub FillPollutantCombo()

        Dim query As String = "Select strPollutantCode, strPollutantDescription from LookUPPollutants " &
            "union select ' ', ' ' " &
            " order by strPollutantDescription"

        With cboPollutant
            .DataSource = DB.GetDataTable(query)
            .DisplayMember = "strPollutantDescription"
            .ValueMember = "strPollutantCode"
            .SelectedIndex = 0
        End With

    End Sub
    Private Sub FillTestingFirmsCombo()
        Dim query As String = "Select strTestingFirmKey, strTestingFirm from LookUPTestingFirms " &
            "union select ' ', ' ' " &
            "order by strTestingFirm"

        With cboTestingFirms
            .DataSource = DB.GetDataTable(query)
            .DisplayMember = "strTestingFirm"
            .ValueMember = "strTestingFirmKey"
            .SelectedIndex = 0
        End With
    End Sub
    Private Sub FillDateGrid()
        Dim query As String = "select ISMPMaster.strReferenceNumber, " &
        "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
        "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
        " SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as StrAIRSNumber, " &
        "strFacilityName, ISMPDocumentType.strDocumentType " &
        "from ISMPReportInformation, ISMPDocumentType, " &
        "ISMPMaster, APBFacilityInformation " &
        "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
        "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
        "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
        "and strDelete is NULL " &
        "and strClosed = 'False' " &
        "order by ISMPMaster.strReferenceNumber"

        dtGrid = DB.GetDataTable(query)

    End Sub

#End Region

    Private Sub LoadAddress()
        Dim temp As String
        Try

            If btnSearchForAIRS.Visible AndAlso
                cboAIRSNumber.Text <> "" AndAlso
                cboAIRSNumber.Text.Length = 8 Then

                cboFacilityName.Text = ""
                txtFacilityAddress.Clear()
                txtFacilityCity.Clear()
                txtFacilityState.Clear()
                txtFacilityZipCode.Clear()

                Dim query As String = "select " &
                "strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, " &
                "strFacilityState, strFacilityZipcode " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & cboAIRSNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
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

                Return
            End If

            If cboAIRSNumber.SelectedIndex <> -1 Then

                Dim query As String = "Select strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, strFacilityState, strFacilityZipCode " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & cboAIRSNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub FillTestReportList()
        Try
            clbReferenceNumbers.Items.Clear()

            If DTPDateReceived.Text <> "" AndAlso cboAIRSNumber.Text <> "" Then
                Dim query As String = "select
                    m.STRREFERENCENUMBER,
                    STREMISSIONSOURCE,
                    STRPOLLUTANTDESCRIPTION
                from ISMPMASTER m
                    inner join ISMPREPORTINFORMATION i
                        on m.STRREFERENCENUMBER = i.STRREFERENCENUMBER
                    inner join LOOKUPPOLLUTANTS l
                        on l.STRPOLLUTANTCODE = i.STRPOLLUTANT
                where STRAIRSNUMBER = @airs
                      and DATRECEIVEDDATE = @date
                      and STRCLOSED <> 'True'
                      and (STRDELETE <> 'DELETE'
                           or STRDELETE is NUll)
                ORDER BY m.STRREFERENCENUMBER "

                Dim p As SqlParameter() = {
                    New SqlParameter("@airs", "0413" & cboAIRSNumber.Text),
                    New SqlParameter("@date", DTPDateReceived.Value)
                }

                Dim dt As DataTable = DB.GetDataTable(query, p)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    Return
                End If

                For Each dr As DataRow In dt.Rows
                    clbReferenceNumbers.Items.Add(String.Concat(dr.Item("strReferenceNumber"), " - ", dr.Item("strEmissionSource"), " - ", dr.Item("strPollutantDescription")))

                    If clbReferenceNumbers.Items.Contains(String.Concat(txtReferenceNumber.Text, " - ", dr.Item("StrEmissionSource"), " - ", dr.Item("strPollutantDescription"))) Then
                        clbReferenceNumbers.SetItemCheckState(clbReferenceNumbers.FindString(txtReferenceNumber.Text), CheckState.Checked)
                    End If
                Next

            End If

        Catch ex As Exception
            ex.Data.AddAsUniqueIfExists("AIRS#", cboAIRSNumber.Text)
            ex.Data.AddAsUniqueIfExists("Date", DTPDateReceived.Value)
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub GetNextReferenceNumber()
        Try

            Dim query As String = "select " &
                "case when max(strReferenceNumber) is null then CONCAT(YEAR(GETDATE()), '00001')  " &
                "else max(convert(int, strReferenceNumber) + 1 ) " &
                "end MaxRefNum  " &
                "from ISMPMaster  " &
                "where strReferenceNumber like @ref "

            Dim p As New SqlParameter("@ref", Today.Year.ToString & "%")

            txtReferenceNumber.Text = DB.GetString(query, p)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub Save()
        Dim RecordStatus As String = "False"
        Dim AIRSNumber As String

        Try

            If rdbCloseReport.Checked Then
                MsgBox("This record is currently marked as being closed." & vbCrLf & "Click Open Record to Save information.",
                MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Return
            End If

            If btnSearchForAIRS.Visible Then
                If cboAIRSNumber.Text <> "" AndAlso cboAIRSNumber.Text.Length = 8 Then
                    AIRSNumber = cboAIRSNumber.Text
                Else
                    MsgBox("Invalid AIRS Number", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                    Return
                End If
            Else
                If cboAIRSNumber.SelectedIndex <> -1 AndAlso cboAIRSNumber.SelectedIndex <> 0 Then
                    AIRSNumber = cboAIRSNumber.SelectedValue
                Else
                    MsgBox("The Facility Name does not correspond to the AIRS Number provided." _
                     & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                    Return
                End If
            End If

            If cboPollutant.SelectedIndex = -1 OrElse cboPollutant.SelectedIndex = 0 Then
                MsgBox("The Pollutant does not match any of the provided pollutants." _
                  & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Return
            End If
            If cboTestingFirms.SelectedIndex = -1 OrElse cboTestingFirms.SelectedIndex = 0 Then
                MsgBox("The Testing Firm does not match any of the provided Testing Firms." _
                  & vbCr & "This must be corrected before moving on.", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
                Return
            End If

            If Not chbOverright.Checked AndAlso txtReferenceNumber.Text = "" Then
                GetNextReferenceNumber()
            End If

            If txtEmissionSource.Text = "" Then
                txtEmissionSource.Text = "N/A"
            End If

            If txtReferenceNumber.Text <> "" Then
                If Not rdbOpenReport.Checked AndAlso Not rdbCloseReport.Checked Then
                    rdbOpenReport.Checked = True
                End If

                Dim query As String = "Select strReferenceNumber from ISMPMaster where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtReferenceNumber.Text)

                Dim queryList As New List(Of String)

                Dim paramList As New List(Of SqlParameter())

                If DB.ValueExists(query, p) Then
                    queryList.Add("Update ISMPMaster set " &
                    "strAIRSNumber = @airs " &
                    "where strReferenceNumber = @ref ")

                    queryList.Add("Update ISMPReportInformation set " &
                    "strPollutant = @strPollutant, " &
                    "strEmissionSource = @strEmissionSource, " &
                    "strTestingFirm = @strTestingFirm, " &
                    "datTestDateStart = @datTestDateStart, " &
                    "datTestDateEnd = @datTestDateEnd, " &
                    "datReceivedDate = @datReceivedDate, " &
                    "datCompleteDate = @datCompleteDate, " &
                    "strClosed = @strClosed, " &
                    "strDelete = null " &
                    "where strReferenceNumber = @strReferenceNumber ")
                Else
                    queryList.Add("Insert into ISMPMaster " &
                                  "(STRREFERENCENUMBER, STRAIRSNUMBER, STRMODIFINGPERSON, DATMODIFINGDATE) " &
                                  "values " &
                                  "(@ref, @airs, @user, getdate()) ")

                    queryList.Add("Insert into ISMPReportInformation " &
           "(strReferenceNumber, strPollutant, strEmissionSource, " &
           "strReportType, strDocumentType, strApplicableRequirement, " &
           "strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " &
           "strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " &
           "strComplianceManager, datTestDateStart, datTestDateEnd, " &
           "datReceivedDate, datCompleteDate, mmoCommentArea, strClosed, " &
           "strDirector, strCommissioner, strProgramManager, " &
           "strComplianceStatus, strcc, strModifingPerson, datModifingDate, " &
           "strControlEquipmentData) " &
           "select " &
           "@strReferenceNumber, " &
           "@strpollutant, " &
           "@strEmissionSource, " &
           "'004', " &
           "'001', " &
           "'Incomplete', " &
           "@strTestingFirm, " &
           "'0', '0', '0', " &
           "'0', " &
           "'04-Jul-1776', " &
           "(SELECT " &
           "CASE  WHEN strDistrictResponsible <> 'False' AND strDistrictResponsible IS NOT NULL         THEN strDistrictManager  " &
           "WHEN convert(int,TABLE1.STRASSIGNINGMANAGER) <> 1 AND TABLE1.STRASSIGNINGMANAGER IS NOT NULL THEN convert(int,TABLE1.STRASSIGNINGMANAGER) " &
           " ELSE '0' " &
           "END ManagerResponsible  " &
           "from LookUPDistricts " &
           " RIGHT JOIN LOOKUPDISTRICTINFORMATION  " &
           "ON LOOKUPDISTRICTINFORMATION.strDistrictCode = LookUPDistricts.strDistrictCode " &
           " RIGHT JOIN SSCPDISTRICTRESPONSIBLE     " &
           "ON SUBSTRING(SSCPDistrictResponsible.strAIRSNumber, 5, 3) = strDistrictCounty " &
           " LEFT JOIN (select SSCPINSPECTIONSREQUIRED.STRASSIGNINGMANAGER, " &
           "SSCPINSPECTIONSREQUIRED.strAIRSNumber " &
           "from SSCPINSPECTIONSREQUIRED, " &
           "(select max(INTYEAR) as MAXYEAR, STRAIRSNUMBER " &
           "from SSCPINSPECTIONSREQUIRED " &
           "group by STRAIRSNUMBER) MAXRESULTS " &
           "where SSCPINSPECTIONSREQUIRED.STRAIRSNUMBER = MAXRESULTS.STRAIRSNUMBER " &
           "and SSCPINSPECTIONSREQUIRED.INTYEAR = MAXRESULTS.MAXYEAR) Table1 " &
           "ON SSCPDistrictResponsible.strAIRSNumber = Table1.strAIRSnumber " &
           "where  SSCPDISTRICTRESPONSIBLE.STRAIRSNUMBER = @airs), " &
           "@datTestDateStart, @datTestDateEnd, " &
           "@datReceivedDate, " &
           "'04-Jul-1776', 'N/A', @strClosed, " &
           "(select strManagementName from LookUpAPBManagementType " &
           "where strKey = @ed and strCurrentContact = 'C' ), " &
           "(select strManagementName from LookUpAPBManagementType " &
           "where strKey = @dc and strCurrentContact = 'C' ), " &
           "(select strManagementName from LookUpAPBManagementType " &
           "where strKey = @ip and strCurrentContact = 'C' ), " &
           "'01', " &
           "'0', " &
           "@user, GETDATE() , " &
           "'N/A' ")

                End If

                paramList.Add({New SqlParameter("@airs", "0413" & AIRSNumber),
                                  New SqlParameter("@ref", txtReferenceNumber.Text),
                                  New SqlParameter("@user", CurrentUser.UserID)})

                paramList.Add({
                                  New SqlParameter("@strPollutant", cboPollutant.SelectedValue),
                                  New SqlParameter("@strEmissionSource", txtEmissionSource.Text),
                                  New SqlParameter("@strTestingFirm", cboTestingFirms.SelectedValue),
                                  New SqlParameter("@datTestDateStart", DTPTestDateStart.Text),
                                  New SqlParameter("@datTestDateEnd", DTPTestDateEnd.Text),
                                  New SqlParameter("@datReceivedDate", DTPDateReceived.Text),
                                  New SqlParameter("@datCompleteDate", DTPDateClosed.Text),
                                  New SqlParameter("@strClosed", RecordStatus),
                                  New SqlParameter("@strReferenceNumber", txtReferenceNumber.Text),
                                  New SqlParameter("@airs", "0413" & cboAIRSNumber.Text),
                                  New SqlParameter("@user", CurrentUser.UserID),
                                  New SqlParameter("@ed", DAL.EpdManagementTypes.EpdDirector.ToString),
                                  New SqlParameter("@dc", DAL.EpdManagementTypes.DnrCommissioner.ToString),
                                  New SqlParameter("@ip", DAL.EpdManagementTypes.IsmpProgramManager.ToString)
                                  })

                DB.RunCommand(queryList, paramList)

                bgw1.WorkerReportsProgress = True
                bgw1.WorkerSupportsCancellation = True
                bgw1.RunWorkerAsync()

                If rdbCloseReport.Checked Then
                    SaveToolStripMenuItem.Enabled = False
                End If
                Find()
                MsgBox("Done", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
            Else
                MsgBox("You must Provide a Reference Number with a length of 9 characters or less.",
                         MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
            End If

        Catch ex As Exception
            ErrorReport(ex, txtReferenceNumber.Text, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub CloseTestReport()
        Try
            Dim AFSActionNumber As String = ""
            Dim UpdateCode As String
            Dim ComplianceStatus As String
            Dim AIRSNumber As String = ""

            If rdbCloseReport.Checked Then
                If btnSearchForAIRS.Visible Then
                    If cboAIRSNumber.Text <> "" AndAlso cboAIRSNumber.Text.Length = 8 Then
                        AIRSNumber = cboAIRSNumber.Text
                    End If
                Else
                    AIRSNumber = cboAIRSNumber.SelectedValue
                End If

                For Each RefNum As String In Me.clbReferenceNumbers.CheckedItems
                    RefNum = Mid(RefNum, 1, (RefNum.IndexOf(" -")))
                    If RefNum.Length > 9 Then
                        MsgBox("There was an issue with the Reference Number being more than 9 characters in length." & vbCrLf &
                               "Please note the Reference Numbers being closed out and contact the Data Management Unit with those numbers.",
                                MsgBoxStyle.Exclamation, "Test Report Administration")
                        Return
                    End If
                    Dim query As String = "Select strComplianceStatus " &
                    "from ISMPReportInformation " &
                    "where strReferenceNumber = @ref "

                    Dim p As New SqlParameter("@ref", RefNum)

                    Dim dr As DataRow = DB.GetDataRow(query, p)
                    If dr IsNot Nothing Then
                        ComplianceStatus = dr.Item("strComplianceStatus")
                    Else
                        ComplianceStatus = "00"
                    End If

                    Select Case ComplianceStatus
                        Case "00"
                            query = ""
                        Case "01"
                            query = ""
                        Case Else
                            query = "Select strUpdateStatus " &
                            "from AFSISMPRecords " &
                            "where strReferenceNumber = @ref "

                            Dim dr2 As DataRow = DB.GetDataRow(query, p)
                            If dr2 IsNot Nothing Then
                                UpdateCode = dr2.Item("strupdateStatus")
                            Else
                                UpdateCode = ""
                            End If
                            Select Case UpdateCode
                                Case "A"
                                    'Leave it alone
                                Case "C"
                                    'Leave it alone
                                Case "N"
                                    query = "Update AFSISMPRecords set " &
                                    "strUpDateStatus = 'C' " &
                                    "where strReferenceNumber = @ref "
                                    DB.RunCommand(query, p)

                                Case ""
                                    query = "Select strAFSActionNumber " &
                                    "from APBSupplamentalData " &
                                    "where strAIRSNumber = @airs "

                                    Dim p2 As New SqlParameter("@airs", "0413" & AIRSNumber)

                                    Dim dr3 As DataRow = DB.GetDataRow(query, p2)
                                    If dr3 IsNot Nothing Then
                                        AFSActionNumber = dr3.Item("strAFSActionNumber")
                                    End If

                                    query = "Insert into AFSISMPRecords " &
                                    "(strReferenceNumber, strAFSActionNumber, " &
                                    "strUpDateStatus, strModifingPerson, " &
                                    "datModifingDate) " &
                                    "values " &
                                    "(@strReferenceNumber, @strAFSActionNumber, " &
                                    "'A', @strModifingPerson, " &
                                    "getdate()) "

                                    Dim p3 As SqlParameter() = {
                                        New SqlParameter("@strReferenceNumber", RefNum),
                                        New SqlParameter("@strAFSActionNumber", AFSActionNumber),
                                        New SqlParameter("@strModifingPerson", CurrentUser.UserID)
                                    }

                                    DB.RunCommand(query, p3)

                                    AFSActionNumber = CInt(AFSActionNumber) + 1

                                    query = "Update APBSupplamentalData set " &
                                    "strAFSActionNumber =@afs " &
                                    "where strAIRSNumber = @airs "

                                    Dim p4 As SqlParameter() = {
                                        New SqlParameter("@afs", AFSActionNumber),
                                        New SqlParameter("@airs", "0413" & AIRSNumber)
                                    }

                                    DB.RunCommand(query, p4)
                                Case Else
                                    'Leave it alone
                            End Select
                    End Select
                    Select Case ComplianceStatus
                        Case "00"
                            MsgBox("Reference Number " & RefNum.ToString & " does not exist in the system.",
                                   MsgBoxStyle.Exclamation, "ISMP Test Report Information")
                            Return
                        Case "01"
                            MsgBox("Test Report " & RefNum.ToString & " is marked as 'File Open' in the Compliance Status." & vbCrLf &
                            "Have the engineer correct this status before trying to close out this Test Report.",
                              MsgBoxStyle.Exclamation, "ISMP Test Report Information")
                            Return
                        Case Else
                            query = "Update ISMPReportInformation set " &
                            "strClosed = 'True', " &
                            "datCompleteDate = @cd " &
                            "where strReferenceNumber = @ref "

                            Dim p5 As SqlParameter() = {
                                New SqlParameter("@cd", DTPDateClosed.Text),
                                New SqlParameter("@ref", RefNum.ToString)
                            }
                            DB.RunCommand(query, p5)
                    End Select
                    StartComplianceWork(RefNum)
                Next
            End If

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            clbReferenceNumbers.Items.Clear()
            FillTestReportList()

            MsgBox("Done", MsgBoxStyle.Exclamation, "ISMP Test Report Information")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub Find()
        Try

            If txtReferenceNumber.Text <> "" Then

                Dim query As String = "Select strAIRSNumber from ISMPMaster where strReferenceNumber = @ref "
                Dim p As New SqlParameter("@ref", txtReferenceNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
                    cboAIRSNumber.Text = Mid(dr.Item("strAirsnumber"), 5)

                    LoadAddress()

                    query = "select i.strReferenceNumber,
                               format(i.datReceivedDate, 'dd-MMM-yyyy')              as forDatReceivedDate,
                               format(i.datTestDateStart, 'dd-MMM-yyyy')             as forDatTestDateStart,
                               format(i.datTestDateEnd, 'dd-MMM-yyyy')               as forDatTestDateEnd,
                               format(i.datReviewedByUnitmanager, 'dd-MMM-yyyy')     as forDatReviewedByUnitManager,
                               format(i.datCompleteDate, 'dd-MMM-yyyy')              as forDateComplete,
                               i.strClosed,
                               t.strReportType,
                               concat(u2.strLastName, ', ', u2.strFirstName)         as ReviewingEngineer,
                               concat(u.strLastName, ', ', u.strFirstName)           as UnitManager,
                               i.strEmissionSource,
                               f.strTestingFirm,
                               p.strPollutantDescription,
                               e.strUnitDesc,
                               d.strDocumentType,
                               l.strComplianceStatus,
                               convert(bit, iif(a.STRAFSACTIONNUMBER is null, 0, 1)) as AfsActionNumberExists
                        from ISMPMASTER m
                            INNER JOIN ISMPREPORTINFORMATION i
                            ON m.strReferenceNumber = i.strReferenceNumber
                            INNER JOIN ISMPREPORTTYPE t
                            on i.STRREPORTTYPE = t.STRKEY
                            INNER JOIN LOOKUPTESTINGFIRMS f
                            ON i.strTestingFirm = f.strTestingFirmKey
                            INNER JOIN LOOKUPPOLLUTANTS p
                            ON i.strPollutant = p.strPollutantCode
                            LEFT JOIN EPDUSERPROFILES u
                            ON i.numReviewingManager = u.numUserID
                            LEFT JOIN EPDUSERPROFILES u2
                            ON i.STRREVIEWINGENGINEER = u2.numUserID
                            LEFT JOIN LOOKUPEPDUNITS e
                            ON u.numUnit = e.numUnitCode
                            INNER JOIN ISMPDOCUMENTTYPE d
                            ON i.strDocumentTYpe = d.strKEy
                            INNER JOIN LOOKUPISMPCOMPLIANCESTATUS l
                            ON i.strComplianceStatus = l.strComplianceKey
                            left join AFSISMPRECORDS a
                            on a.STRREFERENCENUMBER = m.STRREFERENCENUMBER
                        where m.strReferenceNumber = @ref"

                    Dim p1 As New SqlParameter("@ref", txtReferenceNumber.Text)

                    Dim dr2 As DataRow = DB.GetDataRow(query, p1)
                    If dr2 IsNot Nothing Then
                        If dr2.Item("AfsActionNumberExists") Then
                            cboAIRSNumber.Enabled = False
                            btnSearchForAIRS.Enabled = False
                            btnLoadCombos.Enabled = False
                        Else
                            cboAIRSNumber.Enabled = True
                            btnSearchForAIRS.Enabled = True
                            btnLoadCombos.Enabled = True
                        End If

                        DTPDateReceived.Text = dr2.Item("forDatReceivedDate")
                        txtEmissionSource.Text = dr2.Item("strEmissionSource")
                        cboTestingFirms.Text = dr2.Item("strTestingFirm")
                        cboPollutant.Text = dr2.Item("strPollutantDescription")
                        DTPTestDateStart.Text = dr2.Item("forDatTestDateStart")
                        DTPTestDateEnd.Text = dr2.Item("fordatTestDateEnd")
                        DTPTestDateStart.Text = dr2.Item("forDatTestDateStart")
                        DTPTestDateEnd.Text = dr2.Item("forDatTestDateEnd")
                        If dr2.Item("forDateComplete").ToString = "04-Jul-1776" Then
                            txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr2.Item("forDatReceivedDate")), Today)
                        Else
                            txtDaysInAPB.Text = DateDiff(DateInterval.Day, CDate(dr2.Item("forDatReceivedDate")), CDate(dr2.Item("forDateComplete")))
                        End If
                        If dr2.Item("strClosed").ToString = "True" Then
                            rdbOpenReport.Checked = False
                            rdbCloseReport.Checked = True
                            SaveToolStripMenuItem.Enabled = False
                        Else
                            rdbOpenReport.Checked = True
                            rdbCloseReport.Checked = False
                            SaveToolStripMenuItem.Enabled = True
                        End If

                        If dr2.Item("forDateComplete").ToString <> "04-Jul-1776" Then
                            DTPDateClosed.Text = dr2.Item("forDateComplete")
                        Else
                            DTPDateClosed.Value = Today
                        End If
                    End If

                    FillTestReportList()
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub Clear()
        Try

            txtReferenceNumber.Clear()

            If btnSearchForAIRS.Visible Then
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

            cboAIRSNumber.Enabled = True
            btnSearchForAIRS.Enabled = True
            btnLoadCombos.Enabled = True

            txtFacilityAddress.Clear()
            txtFacilityCity.Clear()
            txtFacilityState.Clear()
            txtFacilityZipCode.Clear()
            DTPDateReceived.Value = Today
            DTPTestDateStart.Value = Today
            DTPTestDateEnd.Value = Today
            txtEmissionSource.Clear()
            cboTestingFirms.Text = ""
            cboPollutant.Text = ""
            txtDaysInAPB.Clear()
            rdbOpenReport.Checked = True
            rdbCloseReport.Checked = False
            DTPDateClosed.Value = Today
            SaveToolStripMenuItem.Enabled = True
            If cboTestingFirms.SelectedIndex > -1 Then
                cboTestingFirms.SelectedIndex = 0
            End If
            If cboPollutant.SelectedIndex > -1 Then
                cboPollutant.SelectedIndex = 0
            End If
            clbReferenceNumbers.Items.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MoveOn()
        Try
            OpenFormTestReportEntry(txtReferenceNumber.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub OpenMemo()
        OpenFormTestMemo(Me.txtReferenceNumber.Text)
    End Sub
    Private Sub DeleteTestReport()
        Try
            If MessageBox.Show("Are you sure you want to delete these test reports?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) _
                               = DialogResult.No Then
                Return
            End If

            For Each RefNum As String In clbReferenceNumbers.CheckedItems
                Dim queryList As New List(Of String)
                Dim paramList As New List(Of SqlParameter())

                RefNum = Mid(RefNum, 1, (RefNum.IndexOf(" -")))

                If Not DAL.Ismp.StackTestExists(RefNum) Then
                    MessageBox.Show("Stack test " & RefNum & " does not exist.", "No such thing", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim parameter As New SqlParameter("@ref", RefNum)

                    queryList.Add("Update ISMPReportInformation set " &
                        " strDelete = 'DELETE' where strReferenceNumber = @ref")
                    paramList.Add({parameter})

                    queryList.Add("update a
                        set a.STRDELETE = 'True'
                        from SSCPITEMMASTER a
                            inner join SSCPTESTREPORTS r
                            on a.STRTRACKINGNUMBER = r.STRTRACKINGNUMBER
                        where r.STRREFERENCENUMBER = @ref")
                    paramList.Add({parameter})

                    DB.RunCommand(queryList, paramList)
                    MessageBox.Show("Test no. " & RefNum & " deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.None)
                End If
            Next

            bgw1.WorkerReportsProgress = True
            bgw1.WorkerSupportsCancellation = True
            bgw1.RunWorkerAsync()

            Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub StartComplianceWork(ReferenceNumber As String)
        ' AIRS number required
        If cboAIRSNumber.Text = "" Then Return

        ' Check if SSCP data already exists
        Dim query As String = "select convert(bit, count(*)) from dbo.SSCPTESTREPORTS where STRREFERENCENUMBER = @ReferenceNumber "
        Dim paramRefNum As New SqlParameter("@ReferenceNumber", ReferenceNumber)
        If DB.GetBoolean(query, paramRefNum) Then Return

        ' Best guess at SSCP staff
        query = "select [Staff ID] from iaip_facility.VW_FacilityAssignments_Compliance where AIRS = @airs"
        Dim paramAirs As New SqlParameter("@airs", "0413" & cboAIRSNumber.Text)
        Dim StaffResponsible As String = DB.GetString(query, paramAirs)
        If String.IsNullOrEmpty(StaffResponsible) Then StaffResponsible = "0"

        ' Best guess at due date of current test (seems unlikely to be correct)
        query = "select DATSSCPTESTREPORTDUE from dbo.APBSUPPLAMENTALDATA where STRAIRSNUMBER = @airs "
        Dim TestDue As Date? = DB.GetSingleValue(Of Date?)(query, paramAirs)
        If TestDue Is Nothing Then TestDue = DTPDateClosed.Value

        Dim DateReceivedBySscp As Date = DTPDateClosed.Value

        Dim params As SqlParameter() = {
            paramRefNum,
            New SqlParameter("@StaffResponsible", StaffResponsible),
            New SqlParameter("@UserId", CurrentUser.UserID),
            SqlParameterAsNull("@CompleteDate", SqlDbType.DateTime2),
            SqlParameterAsNull("@AckLetter", SqlDbType.DateTime2),
            New SqlParameter("@TestDue", TestDue),
            SqlParameterAsNull("@TestReportComments", SqlDbType.VarChar),
            New SqlParameter("@FollowUp", Boolean.FalseString),
            SqlParameterAsNull("@NextTest", SqlDbType.DateTime2),
            New SqlParameter("@DateReceivedBySscp", DateReceivedBySscp)
        }

        DB.SPRunCommand("dbo.SaveStackTestSccpData", params)
    End Sub

#Region "Main Menu"

    Private Sub MmiSave_Click(sender As Object, e As EventArgs) Handles MmiSave.Click
        Save()
    End Sub
    Private Sub MmiReferenceNumber_Click(sender As Object, e As EventArgs) Handles MmiReferenceNumber.Click
        GetNextReferenceNumber()
    End Sub
    Private Sub MmiClear_Click(sender As Object, e As EventArgs) Handles MmiClear.Click
        Clear()
    End Sub
    Private Sub MmiDelete_Click(sender As Object, e As EventArgs) Handles MmiDelete.Click
        DeleteTestReport()
    End Sub
    Private Sub MmiShowDeletedRecords_Click(sender As Object, e As EventArgs) Handles MmiShowDeletedRecords.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete = 'DELETE' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiViewByTestType_Click(sender As Object, e As EventArgs) Handles MmiViewByTestType.Click
        MsgBox("Select a Test Report Type from the drop down list.", MsgBoxStyle.MsgBoxHelp, "View by Test Report Type.")
    End Sub

#Region "View by Test Reports"

    Private Sub MmiUnassigned_Click(sender As Object, e As EventArgs) Handles MmiUnassigned.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                 "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Unassigned' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiOneStackTwoRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackTwoRun.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'One Stack (Two Runs)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiOneStackThreeRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackThreeRun.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'One Stack (Three Runs)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiOneStackFourRun_Click(sender As Object, e As EventArgs) Handles MmiOneStackFourRun.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'One Stack (Four Runs)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiTwoStackStandard_Click(sender As Object, e As EventArgs) Handles MmiTwoStackStandard.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Two Stack (Standard)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiTwoStackDRE_Click(sender As Object, e As EventArgs) Handles MmiTwoStackDRE.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Two Stack (DRE)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiLoadingRack_Click(sender As Object, e As EventArgs) Handles MmiLoadingRack.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Loading Rack' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiFlare_Click(sender As Object, e As EventArgs) Handles MmiFlare.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Flare' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiPondTreatment_Click(sender As Object, e As EventArgs) Handles MmiPondTreatment.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Pond Treatment' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiGasConcentration_Click(sender As Object, e As EventArgs) Handles MmiGasConcentration.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Gas Concentration' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiRata_Click(sender As Object, e As EventArgs) Handles MmiRata.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
            "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
            "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
            "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
            "ISMPDocumentType.strDocumentType " &
            "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
            "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
            "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
            "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            "and strDelete is NULL " &
            "and strClosed = 'False' " &
            "and ISMPDocumentType.strDocumentType = 'Rata' " &
            "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMemoStandard_Click(sender As Object, e As EventArgs) Handles MmiMemoStandard.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Memorandum (Standard)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMemoToFile_Click(sender As Object, e As EventArgs) Handles MmiMemoToFile.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Memorandum (To File)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMemoPTE_Click(sender As Object, e As EventArgs) Handles MmiMemoPTE.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'PTE (Permanent Total Enclosure)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMethod9Single_Click(sender As Object, e As EventArgs) Handles MmiMethod9Single.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Method9 (Single)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMethod9Multi_Click(sender As Object, e As EventArgs) Handles MmiMethod9Multi.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Method 9 (Multi.)' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiMethod22_Click(sender As Object, e As EventArgs) Handles MmiMethod22.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "and ISMPDocumentType.strDocumentType = 'Method 22' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiAllTestReports_Click(sender As Object, e As EventArgs) Handles MmiAllTestReports.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiOpenRecords_Click(sender As Object, e As EventArgs) Handles MmiOpenRecords.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber,  " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'False' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiClosedRecords_Click(sender As Object, e As EventArgs) Handles MmiClosedRecords.Click
        Try

            Dim query As String = "select ISMPMaster.strReferenceNumber, " &
                "format(datReceivedDate, 'dd-MMM-yyyy') as forDatReceivedDate, " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as forDatTestDateStart, " &
                "SUBSTRING(ISMPMaster.strAirsnumber, 5,8) as strAIRSNumber, strFacilityName, " &
                "ISMPDocumentType.strDocumentType " &
                "from ISMPReportInformation, ISMPDocumentType, ISMPMaster, APBFacilityInformation " &
                "where APBFacilityInformation.strairsnumber = ISMPMaster.strairsnumber " &
                "and ISMPDocumentType.strKey = ISMPReportInformation.strDocumentType " &
                "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                "and strDelete is NULL " &
                "and strClosed = 'True' " &
                "order by ISMPMaster.strReferenceNumber"

            dtGrid = DB.GetDataTable(query)
            dgvFacilityInfo.DataSource = dtGrid

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

    Private Sub OpenFacilityLookupTool()
        Try
            Using facilityLookupDialog As New IAIPFacilityLookUpTool
                If facilityLookupDialog.ShowDialog() = DialogResult.OK AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                    cboAIRSNumber.Text = facilityLookupDialog.SelectedAirsNumber.Replace("-", "")
                End If
            End Using
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub MmiViewByFacility_Click(sender As Object, e As EventArgs) Handles MmiViewByFacility.Click
        OpenFacilityLookupTool()
    End Sub

#End Region

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Save()
    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        Dim temp As String = InputBox("Enter the Reference Number you are searching for.", "Reference Number Search")

        If temp <> "" Then
            Clear()
            txtReferenceNumber.Text = temp

            If cboAIRSNumber.Text = "" Then
                txtReferenceNumber.Text = ""
                MsgBox("Reference Number does not exist in Database", MsgBoxStyle.Information, "ISMP Facility/Test Report Information")
            End If
        End If
    End Sub

    Private Sub OpenTestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenTestToolStripMenuItem.Click
        MoveOn()
    End Sub

    Private Sub OpenMemoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenMemoToolStripMenuItem.Click
        OpenMemo()
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        Clear()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        DeleteTestReport()
    End Sub

    Private Sub dgvFacilityInfo_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacilityInfo.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvFacilityInfo.RowCount AndAlso dgvFacilityInfo.Columns(0).HeaderText = "Reference #" Then
            txtReferenceNumber.Text = dgvFacilityInfo(0, e.RowIndex).Value
        End If
    End Sub

    Private Sub txtReferenceNumber_TextChanged(sender As Object, e As EventArgs) Handles txtReferenceNumber.TextChanged
        Try

            If txtReferenceNumber.Text <> "" Then
                Find()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub LLFacilityName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LLFacilityName.LinkClicked
        OpenFacilityLookupTool()
    End Sub
    Private Sub rdbCloseReport_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCloseReport.CheckedChanged
        Try

            If Not rdbCloseReport.Checked AndAlso Not SaveToolStripMenuItem.Enabled Then
                SaveToolStripMenuItem.Enabled = True
            End If
            If rdbCloseReport.Checked Then
                DTPDateClosed.Enabled = True
                btnCloseTestReport.Enabled = True

                If DTPDateClosed.Value = "09-Sep-9998" OrElse DTPDateClosed.Value = "04-Jul-1776" Then
                    DTPDateClosed.Value = Date.Today
                End If
            Else
                DTPDateClosed.Enabled = False
                btnCloseTestReport.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub mmiMemo_Click(sender As Object, e As EventArgs) Handles mmiMemo.Click
        OpenMemo()
    End Sub
    Private Sub chbOverright_CheckedChanged(sender As Object, e As EventArgs) Handles chbOverright.CheckedChanged
        Try

            If chbOverright.Checked Then
                txtReferenceNumber.ReadOnly = False
            Else
                txtReferenceNumber.ReadOnly = True
                txtReferenceNumber.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub mmiAddTestingFirm_Click(sender As Object, e As EventArgs) Handles mmiAddTestingFirm.Click
        Try
            Dim ISMPAddTestingFirm As New ISMPAddTestingFirms

            If ISMPAddTestingFirm IsNot Nothing AndAlso Not ISMPAddTestingFirm.IsDisposed Then
                ISMPAddTestingFirm.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub mmiRefreshLists_Click(sender As Object, e As EventArgs) Handles mmiRefreshLists.Click
        Try

            cboPollutant.DataSource = Nothing
            cboTestingFirms.DataSource = Nothing
            FillPollutantCombo()
            FillTestingFirmsCombo()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Save()
    End Sub
    Private Sub btnDeleteTestReport_Click(sender As Object, e As EventArgs) Handles btnDeleteTestReport.Click
        DeleteTestReport()
    End Sub
    Private Sub btnClearReferenceNumber_Click(sender As Object, e As EventArgs) Handles btnClearReferenceNumber.Click
        txtReferenceNumber.Clear()
    End Sub
    Private Sub btnCloseTestReport_Click(sender As Object, e As EventArgs) Handles btnCloseTestReport.Click
        CloseTestReport()
    End Sub

    Private Sub btnAddTestReport_Click(sender As Object, e As EventArgs) Handles btnAddTestReport.Click
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
                If mtbAddTestReportAIRSNumber.Text <> "" AndAlso Len(mtbAddTestReportAIRSNumber.Text) = 8 Then
                    AIRSNumber = mtbAddTestReportAIRSNumber.Text
                    mtbAddTestReportAIRSNumber.BackColor = Color.White
                Else
                    mtbAddTestReportAIRSNumber.BackColor = Color.Tomato
                    MsgBox("Please add a valid AIRS Number.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If
                If txtAddTestReportCommissioner.Text <> "" Then
                    Commissioner = txtAddTestReportCommissioner.Text
                    txtAddTestReportCommissioner.BackColor = Color.White
                Else
                    txtAddTestReportCommissioner.BackColor = Color.Tomato
                    MsgBox("Please add a valid Commissioner.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If
                If txtAddTestReportDirector.Text <> "" Then
                    Director = txtAddTestReportDirector.Text
                    txtAddTestReportDirector.BackColor = Color.White
                Else
                    txtAddTestReportDirector.BackColor = Color.Tomato
                    MsgBox("Please add a valid Director.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If
                If txtAddTestReportProgramManager.Text <> "" Then
                    ProgramManager = txtAddTestReportProgramManager.Text
                    txtAddTestReportProgramManager.BackColor = Color.White
                Else
                    txtAddTestReportProgramManager.BackColor = Color.Tomato
                    MsgBox("Please add a valid Program Manager.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If
                DateReceived = dtpAddTestReportDateReceived.Text
                DateCompleted = DTPAddTestReportDateCompleted.Text

                Dim query As String = "Select " &
                "strReferenceNumber " &
                "from ISMPMaster " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", RefNum)

                If DB.ValueExists(query, p) Then
                    MsgBox("This Reference Number already exists in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If

                query = "Select " &
                "strAIRSNumber " &
                "from APBMasterAIRS " &
                "where strAIRSNumber = @airs "

                Dim p2 As New SqlParameter("@airs", "0413" & AIRSNumber)

                If Not DB.ValueExists(query, p2) Then
                    MsgBox("This AIRS Number does not exist in the system.", MsgBoxStyle.Information, "Add Test Report")
                    Return
                End If

                query = "Insert into ISMPMaster " &
                    "(STRREFERENCENUMBER, STRAIRSNUMBER, STRMODIFINGPERSON, DATMODIFINGDATE) " &
                    "values " &
                    "(@ref, @airs, @user, getdate()) "

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@ref", RefNum),
                    New SqlParameter("@airs", "0413" & AIRSNumber),
                    New SqlParameter("@user", CurrentUser.UserID)
                }
                DB.RunCommand(query, p3)

                query = "Insert into ISMPReportInformation " &
                   "(strReferenceNumber, strPollutant, strEmissionSource, " &
                   "strReportType, strDocumentType, strApplicableRequirement, " &
                   "strTestingFirm, strReviewingEngineer, strWitnessingEngineer, " &
                   "strWitnessingEngineer2, strReviewingUnit, datReviewedbyUnitManager, " &
                   "strComplianceManager, datTestDateStart, datTestDateEnd, " &
                   "datReceivedDate, datCompleteDate, mmoCommentArea, " &
                   "strClosed, strDirector, strCommissioner, " &
                   "strProgramManager, strComplianceStatus, strcc, " &
                   "strModifingPerson, datModifingDate, strControlEquipmentData) " &
               "values " &
                "(@strReferenceNumber, '00001', 'N/A', " &
                "'001', '001', 'N/A', " &
                "'00001', '329', '0', " &
                "'0', '12', @DateReceived, " &
                "'0', '04-Jul-1776', '04-Jul-1776', " &
                "@DateReceived , @DateCompleted, 'Historical Test report added to IAIP', " &
                "'False', @Director, @Commissioner, " &
                "@ProgramManager, '01', '0', " &
                "@user,  GETDATE() , 'N/A') "

                Dim p4 As SqlParameter() = {
                    New SqlParameter("@strReferenceNumber", RefNum),
                    New SqlParameter("@DateReceived", DateReceived),
                    New SqlParameter("@DateCompleted", DateCompleted),
                    New SqlParameter("@Commissioner", Commissioner),
                    New SqlParameter("@Director", Director),
                    New SqlParameter("@ProgramManager", ProgramManager),
                    New SqlParameter("@user", CurrentUser.UserID)
                }

                DB.RunCommand(query, p4)

                bgw1.WorkerReportsProgress = True
                bgw1.WorkerSupportsCancellation = True
                bgw1.RunWorkerAsync()

                MsgBox("Record Added.", MsgBoxStyle.Information, "Add Test Report")

            Else
                txtAddTestReportRefNum.BackColor = Color.Tomato
                MsgBox("Please add a valid Reference Number.", MsgBoxStyle.Information, "Add Test Report")
                Return
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearAddTestReport_Click(sender As Object, e As EventArgs) Handles btnClearAddTestReport.Click
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
            dtpAddTestReportDateReceived.Value = Today
            dtpAddTestReportDateReceived.BackColor = Color.White
            DTPAddTestReportDateCompleted.Value = Today
            DTPAddTestReportDateCompleted.BackColor = Color.White


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub bgw1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw1.DoWork
        FillDateGrid()
    End Sub

    Private Sub bgw1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw1.RunWorkerCompleted
        If dtGrid IsNot Nothing Then
            dgvFacilityInfo.DataSource = dtGrid
            dgvFacilityInfo.Columns("strReferenceNumber").HeaderText = "Reference #"
            dgvFacilityInfo.Columns("strReferenceNumber").DisplayIndex = 0
            dgvFacilityInfo.Columns("StrAIRSNumber").HeaderText = "AIRS #"
            dgvFacilityInfo.Columns("StrAIRSNumber").DisplayIndex = 1
            dgvFacilityInfo.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFacilityInfo.Columns("strFacilityName").DisplayIndex = 2
            dgvFacilityInfo.Columns("forDatReceivedDate").HeaderText = "Received Date"
            dgvFacilityInfo.Columns("forDatReceivedDate").DisplayIndex = 3
            dgvFacilityInfo.Columns("forDatTestDateStart").HeaderText = "Date Test Started"
            dgvFacilityInfo.Columns("forDatTestDateStart").DisplayIndex = 4
            dgvFacilityInfo.Columns("strDocumentType").HeaderText = "Document Type"
            dgvFacilityInfo.Columns("strDocumentType").DisplayIndex = 5
        End If
    End Sub

    Private Sub btnCloseHistoricTestReport_Click(sender As Object, e As EventArgs) Handles btnCloseHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                Dim query As String = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtCloseTestReportRefNum.Text)

                If DB.ValueExists(query, p) Then
                    query = "Update ISMPReportInformation set " &
                    "strClosed = 'True' " &
                    "where strReferenceNumber = @ref "

                    DB.RunCommand(query, p)

                    bgw1.WorkerReportsProgress = True
                    bgw1.WorkerSupportsCancellation = True
                    bgw1.RunWorkerAsync()

                    MsgBox("Test Report Closed", MsgBoxStyle.Information, "Historical Test Report")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReOpenHistoricTestReport_Click(sender As Object, e As EventArgs) Handles btnReOpenHistoricTestReport.Click
        Try
            If txtCloseTestReportRefNum.Text <> "" Then
                Dim query As String = "Select " &
                "strReferenceNumber " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = @ref "

                Dim p As New SqlParameter("@ref", txtCloseTestReportRefNum.Text)

                If DB.ValueExists(query, p) Then
                    query = "Update ISMPReportInformation set " &
                    "strClosed = 'False' " &
                    "where strReferenceNumber = @ref "

                    DB.RunCommand(query, p)

                    bgw1.WorkerReportsProgress = True
                    bgw1.WorkerSupportsCancellation = True
                    bgw1.RunWorkerAsync()

                    MsgBox("Test Report Re-Opened", MsgBoxStyle.Information, "Historical Test Report")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenTestReport_Click(sender As Object, e As EventArgs) Handles btnOpenTestReport.Click
        Try
            OpenFormTestReportEntry(txtAddTestReportRefNum.Text)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSearchForAIRS_Click(sender As Object, e As EventArgs) Handles btnSearchForAIRS.Click
        Try
            If cboAIRSNumber.Text <> "" AndAlso cboAIRSNumber.Text.Length = 8 Then
                cboFacilityName.Text = ""
                txtFacilityAddress.Clear()
                txtFacilityCity.Clear()
                txtFacilityState.Clear()
                txtFacilityZipCode.Clear()

                Dim query As String = "select " &
                "strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, " &
                "strFacilityState, strFacilityZipcode " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @airs "

                Dim p As New SqlParameter("@airs", "0413" & cboAIRSNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)
                If dr IsNot Nothing Then
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
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadCombos_Click(sender As Object, e As EventArgs) Handles btnLoadCombos.Click
        btnSearchForAIRS.Visible = False
        cboAIRSNumber.Text = ""
        cboAIRSNumber.Enabled = True
        btnSearchForAIRS.Enabled = True
        btnLoadCombos.Enabled = True
        cboFacilityName.Text = ""
        txtFacilityAddress.Clear()
        txtFacilityCity.Clear()
        txtFacilityState.Clear()
        txtFacilityZipCode.Clear()

        FillFacilityAndAIRSCombos()
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If dtGrid IsNot Nothing Then dtGrid.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class