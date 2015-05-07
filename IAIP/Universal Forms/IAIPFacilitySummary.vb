Imports Oracle.DataAccess.Client
Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.Apb.Facilities

Public Class IAIPFacilitySummary

#Region " Deprecated fields "

    Dim SQL As String
    Dim dsFacilityWideData As DataSet
    Dim daFacilityWideData As OracleDataAdapter
    Dim dsISMP As DataSet
    Dim daISMP As OracleDataAdapter
    Dim dsSSCP As DataSet
    Dim daSSCP As OracleDataAdapter
    Dim dsSSPP As DataSet
    Dim daSSPP As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim SQLLine As String

#End Region

#Region " Properties and fields "

    Private _airsNumber As ApbFacilityId
    Public Property AirsNumber() As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(ByVal value As ApbFacilityId)
            If _airsNumber Is Nothing AndAlso value Is Nothing Then Return
            If _airsNumber IsNot Nothing AndAlso _airsNumber.Equals(value) Then Return
            _airsNumber = value
            ClearAllData()
            If _airsNumber IsNot Nothing Then
                LoadBasicFacilityAndHeaderData()
            End If
        End Set
    End Property

    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            AirsNumberEntry.Text = Value
        End Set
    End Property

    Private selectedFacility As New Facility
    Private selectedFacilityDataSet As New DataSet

#End Region

#Region " Form Load "

    Private Sub IAIPFacilitySummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
        LoadPermissions()
    End Sub

    Private Sub IAIPFacilitySummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Me.AirsNumber Is Nothing Then
            ClearAllData()
            AirsNumberEntry.Focus()
        End If
    End Sub

    Private Sub LoadPermissions()
        ' Update EPA
        If (UserGCode = "1" Or UserGCode = "345") Then ' TODO DWW: Better permissions defn for EPA Update
            UpdateAllDataSentToEPAToolStripMenuItem.Visible = True
            UpdateEpaMenuItemSeparator.Visible = True
        Else
            UpdateAllDataSentToEPAToolStripMenuItem.Visible = False
            UpdateEpaMenuItemSeparator.Visible = False
        End If

        ' Facility Creator
        If AccountFormAccess(138, 0) IsNot Nothing AndAlso AccountFormAccess(138, 0) = "138" AndAlso (AccountFormAccess(138, 1) = "1" Or AccountFormAccess(138, 2) = "1" Or AccountFormAccess(138, 3) = "1" Or AccountFormAccess(138, 4) = "1") Then
            FacilityCreatorToolToolStripMenuItem.Visible = True
            FacilityCreatorToolMenuItemSeparator.Visible = True
        Else
            FacilityCreatorToolToolStripMenuItem.Visible = False
            FacilityCreatorToolMenuItemSeparator.Visible = False
        End If

        ' Close/Print Test Reports
        If UserAccounts.Contains("(118)") Then
            llbClosePrintTestReport.Visible = True
        Else
            llbClosePrintTestReport.Visible = False
        End If

        ' Edit location/header data
        If UserUnit = "---" Or AccountFormAccess(22, 3) = "1" Or AccountFormAccess(1, 3) = "1" Then
            btnOpenFacilityLocationEditor.Enabled = True
            btnOpenFacilityLocationEditor.Visible = True
            btnEditHeaderData.Visible = True
        Else
            btnOpenFacilityLocationEditor.Enabled = False
            btnOpenFacilityLocationEditor.Visible = False
            btnEditHeaderData.Visible = False
        End If
    End Sub

#End Region

#Region " Clear all data "

    Private Sub ClearAllData()
        selectedFacility = Nothing
        selectedFacilityDataSet = Nothing

        FacilitySummaryTabControl.SelectedTab = TPBasicInfo
        FacilitySummaryTabControl.Enabled = False

        'TODO: Fill this out as more data is configured
        ClearBasicFacilityData()
        ClearHeaderData()


    End Sub

#End Region

#Region " Basic facility data "

    Private Sub ClearBasicFacilityData()

        'Navigation Panel
        AirsNumberEntry.Text = ""
        AirsNumberEntry.BackColor = SystemColors.ControlLightLight
        FacilityNameDisplay.Text = ""

        'Location
        LocationDisplay.Text = ""
        MapAddressLink.Enabled = False
        LatLonDisplay.Text = ""
        MapLatLonLink.Enabled = False

        'Description
        DescriptionDisplay.Text = ""

        'Status
        ClassificationDisplay.Text = ""
        OperatingStatusDisplay.Text = ""
        ComplianceStatusDisplay.Text = ""
        ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight

        'Offices
        DistrictOfficeDisplay.Text = ""
        ResponsibleOfficeDisplay.Text = ""

        'Facility Dates
        StartupDateDisplay.Text = ""
        PermitRevocationDateDisplay.Text = ""
        CreatedDateDisplay.Text = ""

        'Data Dates
        FisDateDisplay.Text = ""
        EpaDateDisplay.Text = ""
        DataUpdateDateDisplay.Text = ""

    End Sub

    Private Sub LoadBasicFacilityAndHeaderData()
        selectedFacility = DAL.FacilityModule.GetFacility(Me.AirsNumber)

        If selectedFacility Is Nothing Then
            FacilityNameDisplay.Text = "Facility does not exist"
            AirsNumberEntry.BackColor = Color.Bisque
            AirsNumberEntry.Focus()
        Else
            selectedFacility.RetrieveHeaderData()
            selectedFacility.RetrieveComplianceStatusList()
            DisplayBasicFacilityData()
            DisplayHeaderData()
        End If
    End Sub

    Private Sub DisplayBasicFacilityData()

        'Tab Control
        FacilitySummaryTabControl.Enabled = True

        'Navigation Panel
        AirsNumberEntry.Text = Me.AirsNumber.FormattedString

        With selectedFacility

            FacilityNameDisplay.Text = .FacilityName
            FacilityApprovalLinkLabel.Visible = Not .ApprovedByApb

            With .FacilityLocation
                'Location
                LocationDisplay.Text = .Address.ToString & _
                    vbNewLine & vbNewLine & _
                    .County.ToString & " County"
                If .Address IsNot Nothing Then
                    MapAddressLink.Enabled = True
                End If
                LatLonDisplay.Text = .Latitude.ToString & _
                    " / " & _
                    .Longitude.ToString
                If .Latitude IsNot Nothing _
                AndAlso .Longitude IsNot Nothing Then
                    MapLatLonLink.Enabled = True
                End If
            End With

            With .HeaderData
                'Status
                ClassificationDisplay.Text = .ClassificationCode & ", " & .ClassificationDescription
                OperatingStatusDisplay.Text = .OperationalStatusDescription

                'Description
                DescriptionDisplay.Text = .FacilityDescription

                'Facility Dates
                StartupDateDisplay.Text = String.Format(DateStringFormat, .StartupDate)
                PermitRevocationDateDisplay.Text = String.Format(DateStringFormat, .ShutdownDate)
            End With

            'Compliance Status
            ComplianceStatusDisplay.Text = .ControllingComplianceStatus.GetDescription
            ColorCodeComplianceStatusDisplay()

            'Offices
            DistrictOfficeDisplay.Text = .DistrictOfficeLocation
            ResponsibleOfficeDisplay.Text = .DistrictResponsible

        End With


        'Data Dates
        Dim dataDates As DataRow = DAL.FacilityModule.GetDataExchangeDates(Me.AirsNumber)
        If dataDates IsNot Nothing Then
            CreatedDateDisplay.Text = String.Format(DateStringFormat, dataDates("DbRecordCreated"))
            FisDateDisplay.Text = String.Format(DateStringFormat, dataDates("FisExchangeDate"))
            EpaDateDisplay.Text = String.Format(DateTimeStringFormat, dataDates("EpaExchangeDate"))
            DataUpdateDateDisplay.Text = String.Format(DateStringFormat, dataDates("DataModifiedOn"))
        End If
    End Sub

    Private Sub ColorCodeComplianceStatusDisplay()
        If selectedFacility.ControllingComplianceStatus > 20 Then
            ComplianceStatusDisplay.BackColor = Color.Pink
        ElseIf selectedFacility.ControllingComplianceStatus > 10 Then
            ComplianceStatusDisplay.BackColor = Color.LemonChiffon
        ElseIf selectedFacility.ControllingComplianceStatus > 0 Then
            ComplianceStatusDisplay.BackColor = Color.PaleGreen
        Else
            ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight
        End If
    End Sub

#End Region

#Region " Basic Data TabPage functionality "

    Private Sub MapAddressLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MapAddressLink.LinkClicked
        OpenMapUrl(selectedFacility.FacilityLocation.Address.ToLinearString, Me)
    End Sub

    Private Sub MapLatLonLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MapLatLonLink.LinkClicked
        OpenMapUrl(selectedFacility.FacilityLocation.Latitude.ToString & "," & _
                   selectedFacility.FacilityLocation.Longitude.ToString, Me)
    End Sub

#End Region

#Region " Header Data "

    Private Sub ClearHeaderData()

    End Sub

    Private Sub DisplayHeaderData()


        btnOpenSubpartEditor.Visible = False
        btnEditAirProgramPollutants.Enabled = False

    End Sub

#End Region

    Private Sub OldLoadInitialData()
        Try
            Dim PollutantStatus As String = ""
            Dim dtFacilityWideData As New DataTable
            Dim drDSRow As DataRow

            SQL = "select " & _
            "AIRBRANCH.VW_APBFacilityLocation.strAIRSnumber, " & _
            "AIRBRANCH.VW_APBFacilityLocation.strFacilityName, " & _
            "strFacilityStreet1, strFacilityStreet2, " & _
            "strFacilityCity, strFacilityState, " & _
            "strFacilityZipCode, " & _
            "numFacilityLongitude, numFacilityLatitude, " & _
            "strCountyName, strDistrictName, " & _
            "strOperationalStatus, " & _
            "strClass, strAirProgramCodes, " & _
            "strSICCode, strAttainmentStatus, " & _
            "datStartUpDate, datShutDownDate, " & _
            "strCMSMember, strPlantDescription, " & _
            "strStateProgramCodes, strNAICSCode, " & _
            "STRRMPID " & _
            "from " & _
            "AIRBRANCH.VW_APBFacilityLocation, " & _
            "AIRBRANCH.VW_APBFacilityHeader " & _
            "where AIRBRANCH.VW_APBFacilityLocation.strAIRSNumber = AIRBRANCH.VW_APBFacilityHeader.strAIRSNumber " & _
            "and AIRBRANCH.VW_APBFacilityLocation.strAIRSnumber = '" & Me.AirsNumber.DbFormattedString & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = False Then
                MsgBox("No data ", MsgBoxStyle.Exclamation, "Facility Summary")
                Exit Sub
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                'If IsDBNull(dr.Item("strFacilityName")) Then
                '    txtFacilityName.Clear()
                'Else
                '    txtFacilityName.Text = dr.Item("strFacilityName")
                'End If
                'If IsDBNull(dr.Item("strFacilityStreet1")) Then
                '    txtStreetAddress.Clear()
                'Else
                '    txtStreetAddress.Text = dr.Item("strFacilityStreet1")
                'End If
                'If IsDBNull(dr.Item("strFacilityStreet2")) Then
                '    txtStreetAddress2.Clear()
                'Else
                '    txtStreetAddress2.Text = dr.Item("strFacilityStreet2")
                'End If
                'If IsDBNull(dr.Item("strFacilityCity")) Then
                '    txtFacilityCity.Clear()
                'Else
                '    txtFacilityCity.Text = dr.Item("strFacilityCity")
                'End If
                'If IsDBNull(dr.Item("strFacilityState")) Then
                '    txtFacilityState.Clear()
                'Else
                '    txtFacilityState.Text = dr.Item("strFacilityState")
                'End If
                'If IsDBNull(dr.Item("strFacilityZipCode")) Then
                '    txtFacilityZipCode.Clear()
                'Else
                '    txtFacilityZipCode.Text = dr.Item("strFacilityZipCode")
                'End If
                'If IsDBNull(dr.Item("numFacilityLongitude")) Then
                '    txtFacilityLongitude.Clear()
                'Else
                '    txtFacilityLongitude.Text = dr.Item("numFacilityLongitude")
                'End If
                'If IsDBNull(dr.Item("numFacilityLatitude")) Then
                '    txtFacilityLatitude.Clear()
                'Else
                '    txtFacilityLatitude.Text = dr.Item("numFacilityLatitude")
                'End If
                'If IsDBNull(dr.Item("strCountyName")) Then
                '    txtFacilityCounty.Clear()
                'Else
                '    txtFacilityCounty.Text = dr.Item("strCountyName")
                'End If
                'If IsDBNull(dr.Item("strDistrictName")) Then
                '    txtDistrict.Clear()
                'Else
                '    txtDistrict.Text = dr.Item("strDistrictName")
                'End If
                'If IsDBNull(dr.Item("strOperationalStatus")) Then
                '    txtOperationalStatus.Clear()
                'Else
                '    temp = dr.Item("strOperationalStatus")
                '    Select Case temp
                '        Case "O"
                '            txtOperationalStatus.Text = "O - Operational"
                '        Case "P"
                '            txtOperationalStatus.Text = "P - Planned"
                '        Case "C"
                '            txtOperationalStatus.Text = "C - Under Construction"
                '        Case "T"
                '            txtOperationalStatus.Text = "T - Temporarily Closed"
                '        Case "X"
                '            txtOperationalStatus.Text = "X - Closed/Dismantled"
                '        Case "I"
                '            txtOperationalStatus.Text = "I - Seasonal Operation"
                '        Case Else
                '            txtOperationalStatus.Text = "Unknown - Please Fix"
                '    End Select
                'End If
                'If IsDBNull(dr.Item("strClass")) Then
                '    txtClassification.Clear()
                'Else
                '    txtClassification.Text = dr.Item("strClass")
                'End If
                'If IsDBNull(dr.Item("strAirProgramCodes")) Then
                '    chbAPC0.Checked = False
                '    chbAPC1.Checked = False
                '    chbAPC3.Checked = False
                '    chbAPC4.Checked = False
                '    chbAPC6.Checked = False
                '    chbAPC7.Checked = False
                '    chbAPC8.Checked = False
                '    chbAPC9.Checked = False
                '    chbAPCA.Checked = False
                '    chbAPCF.Checked = False
                '    chbAPCI.Checked = False
                '    chbAPCM.Checked = False
                '    chbAPCV.Checked = False
                '    chbAPCRMP.Checked = False
                'Else
                '    temp = dr.Item("strAirProgramCodes")
                '    If Mid(temp, 1, 1) = 1 Then
                '        chbAPC0.Checked = True
                '    End If
                '    If Mid(temp, 2, 1) = 1 Then
                '        chbAPC1.Checked = True
                '    End If
                '    If Mid(temp, 3, 1) = 1 Then
                '        chbAPC3.Checked = True
                '    End If
                '    If Mid(temp, 4, 1) = 1 Then
                '        chbAPC4.Checked = True
                '    End If
                '    If Mid(temp, 5, 1) = 1 Then
                '        chbAPC6.Checked = True
                '    End If
                '    If Mid(temp, 6, 1) = 1 Then
                '        chbAPC7.Checked = True
                '    End If
                '    If Mid(temp, 7, 1) = 1 Then
                '        chbAPC8.Checked = True
                '    End If
                '    If Mid(temp, 8, 1) = 1 Then
                '        chbAPC9.Checked = True
                '    End If
                '    If Mid(temp, 9, 1) = 1 Then
                '        chbAPCF.Checked = True
                '    End If
                '    If Mid(temp, 10, 1) = 1 Then
                '        chbAPCA.Checked = True
                '    End If
                '    If Mid(temp, 11, 1) = 1 Then
                '        chbAPCI.Checked = True
                '    End If
                '    If Mid(temp, 12, 1) = 1 Then
                '        chbAPCM.Checked = True
                '    End If
                '    If Mid(temp, 13, 1) = 1 Then
                '        chbAPCV.Checked = True
                '    End If
                '    If Mid(temp, 14, 1) = 1 Then
                '        chbAPCRMP.Checked = True
                '    End If
                'End If
                'If IsDBNull(dr.Item("strSICCode")) Then
                '    txtSICCode.Clear()
                'Else
                '    txtSICCode.Text = dr.Item("strSICCode")
                'End If
                'If IsDBNull(dr.Item("strAttainmentStatus")) Then
                '    txt1hour.Text = "No"
                '    txt8HROzone.Text = "No"
                '    txtPM.Text = "No"
                'Else
                '    temp = dr.Item("strAttainmentStatus")

                '    Select Case Mid(temp, 2, 1)
                '        Case 0
                '            txt1hour.Text = "No"
                '        Case 1
                '            txt1hour.Text = "Yes"
                '        Case 2
                '            txt1hour.Text = "Contributing"
                '        Case Else
                '            txt1hour.Text = "No"
                '    End Select
                '    Select Case Mid(temp, 3, 1)
                '        Case 0
                '            txt8HROzone.Text = "No"
                '        Case 1
                '            txt8HROzone.Text = "Atlanta"
                '        Case 2
                '            txt8HROzone.Text = "Macon"
                '        Case Else
                '            txt8HROzone.Text = "No"
                '    End Select
                '    Select Case Mid(temp, 4, 1)
                '        Case 0
                '            txtPM.Text = "No"
                '        Case 1
                '            txtPM.Text = "Atlanta"
                '        Case 2
                '            txtPM.Text = "Chattanooga"
                '        Case 3
                '            txtPM.Text = "Floyd"
                '        Case 4
                '            txtPM.Text = "Macon"
                '        Case Else
                '            txtPM.Text = "No"
                '    End Select
                'End If
                'If IsDBNull(dr.Item("datStartUpDate")) Then
                '    txtStartUpDate.Clear()
                'Else
                '    txtStartUpDate.Text = Format(dr.Item("datStartUpDate"), "dd-MMM-yyyy")
                'End If
                'If IsDBNull(dr.Item("datShutDownDate")) Then
                '    txtDateClosed.Clear()
                'Else
                '    txtDateClosed.Text = Format(dr.Item("datShutDownDate"), "dd-MMM-yyyy")
                'End If
                'If IsDBNull(dr.Item("strCMSMember")) Then
                '    txtCMSState.Clear()
                'Else
                '    txtCMSState.Text = dr.Item("strCMSMember")
                'End If
                'If IsDBNull(dr.Item("strPlantDescription")) Then
                '    txtPlantDescription.Clear()
                'Else
                '    txtPlantDescription.Text = dr.Item("strPlantDescription")
                'End If
                'If IsDBNull(dr.Item("strStateProgramCodes")) Then
                '    chbNSRMajor.Checked = False
                '    chbHAPsMajor.Checked = False
                'Else
                '    temp = dr.Item("strStateProgramCodes")
                '    If Mid(temp, 1, 1) = "1" Then
                '        chbNSRMajor.Checked = True
                '    Else
                '        chbNSRMajor.Checked = False
                '    End If
                '    If Mid(temp, 2, 1) = "1" Then
                '        chbHAPsMajor.Checked = True
                '    Else
                '        chbHAPsMajor.Checked = False
                '    End If
                'End If
                'If IsDBNull(dr.Item("strNAICSCode")) Then
                '    txtNAICSCode.Clear()
                'Else
                '    txtNAICSCode.Text = dr.Item("strNAICSCode")
                'End If
                'If IsDBNull(dr.Item("STRRMPID")) Then
                '    txtRMPID.Clear()
                'Else
                '    txtRMPID.Text = dr.Item("strRMPID")
                'End If
            End While
            dr.Close()

            SQL = "select distinct(strComplianceStatus) as PollutantStatus " & _
            "from AIRBranch.APBAirProgramPollutants  " & _
            "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("PollutantStatus")) Then
                    PollutantStatus = PollutantStatus
                Else
                    PollutantStatus = PollutantStatus & dr.Item("PollutantStatus")
                End If
            End While
            dr.Close()

            'If PollutantStatus.Contains("B") Then
            '    txtPollutantStatus.Text = "B - In violation, Procedural  Emissions"
            '    txtPollutantStatus.BackColor = Color.Pink
            'Else
            '    If PollutantStatus.Contains("1") Then
            '        txtPollutantStatus.Text = "1 - In violation, No Schedule"
            '        txtPollutantStatus.BackColor = Color.Pink
            '    Else
            '        If PollutantStatus.Contains("6") Then
            '            txtPollutantStatus.Text = "6 - In violation, Not Meeting Schedule"
            '            txtPollutantStatus.BackColor = Color.Pink
            '        Else
            '            If PollutantStatus.Contains("W") Then
            '                txtPollutantStatus.Text = "W - In violation, procedural"
            '                txtPollutantStatus.BackColor = Color.Pink
            '            Else
            '                If PollutantStatus.Contains("0") Then
            '                    txtPollutantStatus.Text = "0 - Unknown Compliance Status (SCAP)"
            '                    txtPollutantStatus.BackColor = Color.Pink
            '                Else
            '                    If PollutantStatus.Contains("5") Then
            '                        txtPollutantStatus.Text = "5 - Meeting Compliance Schedule"
            '                        txtPollutantStatus.BackColor = Color.LightGreen
            '                    Else
            '                        If PollutantStatus.Contains("8") Then
            '                            txtPollutantStatus.Text = "8 - No Applicable State Reg."
            '                            txtPollutantStatus.BackColor = Color.Pink
            '                        Else
            '                            If PollutantStatus.Contains("2") Then
            '                                txtPollutantStatus.Text = "2 - In Compliance, Source Test"
            '                                txtPollutantStatus.BackColor = Color.LightGreen
            '                            Else
            '                                If PollutantStatus.Contains("3") Then
            '                                    txtPollutantStatus.Text = "3 - In Compliance, Inspection"
            '                                    txtPollutantStatus.BackColor = Color.LightGreen
            '                                Else
            '                                    If PollutantStatus.Contains("4") Then
            '                                        txtPollutantStatus.Text = "4 - In Compliance, Certification"
            '                                        txtPollutantStatus.BackColor = Color.LightGreen
            '                                    Else
            '                                        If PollutantStatus.Contains("9") Then
            '                                            txtPollutantStatus.Text = "9 - In Compliance, Shut Down"
            '                                            txtPollutantStatus.BackColor = Color.LightGreen
            '                                        Else
            '                                            If PollutantStatus.Contains("C") Then
            '                                                txtPollutantStatus.Text = "C - In Compliance, Procedural"
            '                                                txtPollutantStatus.BackColor = Color.LightGreen
            '                                            Else
            '                                                If PollutantStatus.Contains("M") Then
            '                                                    txtPollutantStatus.Text = "M - In Complinace, CEMS Data"
            '                                                    txtPollutantStatus.BackColor = Color.LightGreen
            '                                                Else
            '                                                    txtPollutantStatus.Text = ""
            '                                                    txtPollutantStatus.BackColor = Color.White
            '                                                End If
            '                                            End If
            '                                        End If
            '                                    End If
            '                                End If
            '                            End If
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If
            'End If

            dsFacilityWideData = New DataSet

            SQL = "Select " & _
            "AIRBRANCH.VW_APBFacilityFees.*, " & _
            "(numTotalFee - TotalPaid) as Balance " & _
            "from AIRBRANCH.VW_APBFacilityFees " & _
            "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "'  " & _
            "order by intYear DESC "

            daFacilityWideData = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daFacilityWideData.Fill(dsFacilityWideData, "Fees")

            cboFeeYear.DataBindings.Clear()
            txtFeesClassification.DataBindings.Clear()
            txtFeesTotal.DataBindings.Clear()
            txtTotalFeesPaid.DataBindings.Clear()
            txtDateSubmitted.DataBindings.Clear()
            txtFeesPart70.DataBindings.Clear()
            txtFeesSM.DataBindings.Clear()
            txtFeesNSPS.DataBindings.Clear()
            txtAdminFee.DataBindings.Clear()
            txtFeesVOC.DataBindings.Clear()
            txtFeesPM.DataBindings.Clear()
            txtFeesSO2.DataBindings.Clear()
            txtFeesNOx.DataBindings.Clear()
            txtFeesRate.DataBindings.Clear()
            txtFeesPollutantFee.DataBindings.Clear()
            chbFeesOperating.DataBindings.Clear()
            chbFeesPart70.DataBindings.Clear()
            chbNSPSExempt.DataBindings.Clear()
            txtBalance.DataBindings.Clear()
            lblFeeStatus.DataBindings.Clear()

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
                txtAdminFee.Text = ""
                txtFeesVOC.Text = ""
                txtFeesPM.Text = ""
                txtFeesSO2.Text = ""
                txtFeesNOx.Text = ""
                txtFeesRate.Text = ""
                txtFeesPollutantFee.Text = ""
                chbFeesOperating.Checked = False
                chbFeesPart70.Checked = False
                chbNSPSExempt.Checked = False
                txtBalance.Text = ""
                lblFeeStatus.Text = ""
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
                dtFees.Columns.Add("NumAdminFee", GetType(System.String))
                dtFees.Columns.Add("NumTotalFee", GetType(System.String))
                dtFees.Columns.Add("strNSPSExempt", GetType(System.String))
                dtFees.Columns.Add("strOperate", GetType(System.String))
                dtFees.Columns.Add("NumFeeRate", GetType(System.String))
                dtFees.Columns.Add("numCalculatedFee", GetType(System.String))
                dtFees.Columns.Add("strPart70", GetType(System.String))
                dtFees.Columns.Add("TotalPaid", GetType(System.String))
                dtFees.Columns.Add("DateSubmit", GetType(System.String))
                dtFees.Columns.Add("Balance", GetType(System.String))
                dtFees.Columns.Add("strIAIPDesc", GetType(System.String))

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
                    drNewRow("NumAdminFee") = drDSRow("NumAdminFee")
                    drNewRow("NumTotalFee") = drDSRow("NumTotalFee")
                    drNewRow("strNSPSExempt") = drDSRow("strNSPSExempt")
                    drNewRow("strOperate") = drDSRow("strOperate")
                    drNewRow("NumFeeRate") = drDSRow("NumFeeRate")
                    drNewRow("numCalculatedFee") = drDSRow("numCalculatedFee")
                    drNewRow("strPart70") = drDSRow("strPart70")
                    drNewRow("TotalPaid") = drDSRow("TotalPaid")
                    drNewRow("DateSubmit") = drDSRow("DateSubmit")
                    drNewRow("Balance") = drDSRow("Balance")
                    drNewRow("strIAIPDesc") = drDSRow("strIAIPDesc")
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

                With txtAdminFee
                    .DataBindings.Add(New Binding("Text", dtFees, "NumAdminFee"))
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

                With txtBalance
                    .DataBindings.Add(New Binding("Text", dtFees, "Balance"))
                End With

                With lblFeeStatus
                    .DataBindings.Add(New Binding("Text", dtFees, "strIAIPDesc"))
                End With

            End If

            SQL = "select strDistrictResponsible " & _
            "from AIRBRANCH.SSCPDistrictResponsible " & _
            "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                '    If IsDBNull(dr.Item("strDistrictResponsible")) Then
                '        lblDistrictSource.Visible = False
                '    Else
                '        If dr.Item("strDistrictResponsible") = "True" Then
                '            lblDistrictSource.Visible = True
                '        Else
                '            lblDistrictSource.Visible = False
                '        End If
                '    End If
                'Else
                '    lblDistrictSource.Visible = False
            End If
            dr.Close()


            MakeEditSubpartButtonVisible()
            btnEditAirProgramPollutants.Enabled = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearForm()
        Try
            txtClassification.Clear()
            txtSICCode.Clear()
            txtOperationalStatus.Clear()
            txtCMSState.Clear()
            txtStartUpDate.Clear()
            txtDateClosed.Clear()
            txt1hour.Clear()
            txt8HROzone.Clear()
            txtPM.Clear()
            txtPlantDescription.Clear()
            'txtPollutantStatus.Clear()
            'txtPollutantStatus.BackColor = Color.Gray
            txtNAICSCode.Clear()

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
            chbAPCRMP.Checked = False
            chbHAPsMajor.Checked = False
            chbNSRMajor.Checked = False

            btnEditAirProgramPollutants.Enabled = False
            If FacilitySummaryTabControl.TabPages.Contains(TPContacts) = True Then
                txtSSCPContact.Clear()
                txtSSCPUnit.Clear()
                txtSSPPContact.Clear()
                txtSSPPUnit.Clear()
                txtISMPContact.Clear()
                txtISMPUnit.Clear()
                txtDistrictEngineer.Clear()
                txtDistrictUnit.Clear()
                FacilitySummaryTabControl.TabPages.Remove(TPContacts)
            End If
            If FacilitySummaryTabControl.TabPages.Contains(TPEmissionInventory) = True Then
                FacilitySummaryTabControl.TabPages.Remove(TPEmissionInventory)
            End If
            If FacilitySummaryTabControl.TabPages.Contains(TPTesting) = True Then
                FacilitySummaryTabControl.TabPages.Remove(TPTesting)
            End If
            If FacilitySummaryTabControl.TabPages.Contains(TPCompliance) = True Then
                FacilitySummaryTabControl.TabPages.Remove(TPCompliance)
            End If
            If FacilitySummaryTabControl.TabPages.Contains(TPPermitting) = True Then
                FacilitySummaryTabControl.TabPages.Remove(TPPermitting)
            End If
            If FacilitySummaryTabControl.TabPages.Contains(TPFinancial) = True Then
                FacilitySummaryTabControl.TabPages.Remove(TPFinancial)
            End If

            txtReferenceNumber.Clear()
            txtTestingNumber.Clear()
            txtReferenceNumber2.Clear()
            txtTrackingNumber.Clear()
            txtFCEYear.Clear()
            txtEnforcementNumber.Clear()
            txtApplicationNumber.Clear()
            btnOpenSubpartEditor.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region " ... open other forms"

    Private Sub OpenEditContactInformationTool()
        If Me.AirsNumber IsNot Nothing Then
            Dim parameters As New Dictionary(Of String, String)
            parameters("airsnumber") = Me.AirsNumber.ShortString
            parameters("facilityname") = Me.selectedFacility.FacilityName
            OpenMultiForm("IAIPEditContacts", Me.AirsNumber.ShortString, parameters)
        End If
    End Sub

    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = Windows.Forms.DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                Me.ValueFromFacilityLookUp = facilityLookupDialog.SelectedAirsNumber
                ClearForm()
                OldLoadInitialData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenFacilitySummaryPrintTool()
        Try
            If Me.AirsNumber Is Nothing Then
                MsgBox("Enter a valid AIRS # first", MsgBoxStyle.Information, "Facility Summary")
                Exit Sub
            End If
            If FacilityPrintOut Is Nothing Then
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IaipFacilitySummaryPrint
                FacilityPrintOut.Show()
            Else
                FacilityPrintOut.Dispose()
                FacilityPrintOut = New IaipFacilitySummaryPrint
                If FacilityPrintOut Is Nothing Then FacilityPrintOut = New IaipFacilitySummaryPrint
                FacilityPrintOut.Show()
            End If
            FacilityPrintOut.AirsNumber.Text = Me.AirsNumber.ShortString
            FacilityPrintOut.FacilityName.Text = Me.selectedFacility.FacilityName

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenFacilityLocationEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFacilityLocationEditor.Click
        Try
            If EditFacilityLocation Is Nothing Then
                EditFacilityLocation = New IAIPEditFacilityLocation
                EditFacilityLocation.txtAirsNumber.Text = Me.AirsNumber.ToString
                EditFacilityLocation.Show()
            Else
                EditFacilityLocation.txtAirsNumber.Text = Me.AirsNumber.ToString
                EditFacilityLocation.Show()
                EditFacilityLocation.BringToFront()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditHeaderData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditHeaderData.Click
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(Me.AirsNumber.ToString) Then

            Dim editHeaderDataDialog As New IAIPEditHeaderData
            editHeaderDataDialog.AirsNumber = Me.AirsNumber.ToString
            editHeaderDataDialog.FacilityName = Me.selectedFacility.FacilityName

            editHeaderDataDialog.ShowDialog()

            If editHeaderDataDialog.SomethingWasSaved Then
                OldLoadInitialData()
            End If

            editHeaderDataDialog.Dispose()
        Else
            MessageBox.Show("AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnEditContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContacts.Click
        OpenEditContactInformationTool()
    End Sub

#End Region

#Region " ... air program check boxes "

    Private Sub chbAPC0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC0.CheckedChanged
        Try

            MakeEditSubpartButtonVisible()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC8.CheckedChanged
        Try

            MakeEditSubpartButtonVisible()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub chbAPC9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPC9.CheckedChanged
        Try

            MakeEditSubpartButtonVisible()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbAPCM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbAPCM.CheckedChanged
        Try

            MakeEditSubpartButtonVisible()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region " ... contact copy buttons "

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

#Region " ... main Load Work"

    Private Sub llbViewAirPermitsOnline_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAirPermitsOnline.LinkClicked
        OpenPermitSearchUrl(Me.AirsNumber, Me)
    End Sub

    Private Sub txtDateSubmitted_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateSubmitted.TextChanged
        Try
            If txtBalance.Text <> "" Then
                If CInt(txtBalance.Text) > 0 Then
                    txtTotalFeesPaid.BackColor = Color.Tomato
                Else
                    txtTotalFeesPaid.BackColor = Color.LightGray
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAirProgramPollutants.Click
        Dim EditAirProgramPollutants As IAIPEditAirProgramPollutants = OpenSingleForm(IAIPEditAirProgramPollutants)
        EditAirProgramPollutants.AirsNumberDisplay.Text = Me.AirsNumber.ToString
    End Sub
    Private Sub btnOpenSubpartEditior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenSubpartEditor.Click
        Try


            If EditSubParts Is Nothing Then
                If EditSubParts Is Nothing Then EditSubParts = New IAIPEditSubParts
            Else
                EditSubParts.Dispose()
                EditSubParts = New IAIPEditSubParts
            End If
            EditSubParts.Show()
            If Me.AirsNumber IsNot Nothing Then
                EditSubParts.txtAIRSNumber.Text = Me.AirsNumber.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub MakeEditSubpartButtonVisible()
        btnOpenSubpartEditor.Visible = (chbAPC8.Checked Or chbAPC9.Checked Or chbAPCM.Checked Or chbAPC0.Checked)
    End Sub

#End Region

#Region " ... ISMP Monitoring Work"
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
    Private Sub llbISMPTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbISMPTestReport.LinkClicked
        Try
            Dim id As String = txtReferenceNumber.Text
            If id = "" Then Exit Sub

            If DAL.ISMP.StackTestExists(id) Then
                If UserProgram = "3" Then
                    OpenMultiForm("ISMPTestReports", id)
                Else
                    If DAL.ISMP.StackTestIsClosedOut(id) Then
                        If PrintOut IsNot Nothing AndAlso Not PrintOut.IsDisposed Then
                            PrintOut.Dispose()
                        End If
                        PrintOut = New IAIPPrintOut
                        PrintOut.txtReferenceNumber.Text = txtReferenceNumber.Text
                        PrintOut.txtPrintType.Text = "SSCP"
                        PrintOut.Show()
                    Else
                        MsgBox("This test has not been completely reviewed by ISMP.", MsgBoxStyle.Information, "Facility Summary")
                    End If
                End If
            Else
                MsgBox("Reference number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbClosePrintTestReport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbClosePrintTestReport.LinkClicked
        Try

            If txtReferenceNumber.Text <> "" Then
                SQL = "Select AIRBRANCH.ISMPDocumentType.strDocumentType " & _
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
                    ISMPCloseAndPrint = Nothing
                    If ISMPCloseAndPrint Is Nothing Then ISMPCloseAndPrint = New ISMPClosePrint
                    ISMPCloseAndPrint.txtTestReportType.Text = dr.Item("strDocumentType")
                    ISMPCloseAndPrint.txtReferenceNumber.Text = txtReferenceNumber.Text
                    ISMPCloseAndPrint.txtAIRSNumber.Text = Me.AirsNumber.ToString
                    ISMPCloseAndPrint.txtFacilityName.Text = selectedFacility.FacilityName
                    ISMPCloseAndPrint.txtOrigin.Text = "Facility Summary"
                    ISMPCloseAndPrint.Show()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewTestNotification_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewTestNotification.LinkClicked
        Try

            If txtTestingNumber.Text <> "" Then
                ISMPNotificationLogForm = Nothing
                If ISMPNotificationLogForm Is Nothing Then ISMPNotificationLogForm = New ISMPNotificationLog
                ISMPNotificationLogForm.txtTestNotificationNumber.Text = Me.txtTestingNumber.Text
                ISMPNotificationLogForm.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region " ... SSCP Compliance Work"
    Private Sub dgvSSCPEvents_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSSCPEvents.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvSSCPEvents.HitTest(e.X, e.Y)

        Try
            If dgvSSCPEvents.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvSSCPEvents.Columns(1).HeaderText = "Tracking Number" Then
                    txtTrackingNumber.Text = dgvSSCPEvents(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewComplianceEvent_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewComplianceEvent.LinkClicked
        Try

            If txtTrackingNumber.Text <> "" Then
                SSCPReports = Nothing
                If SSCPReports Is Nothing Then SSCPReports = New SSCPEvents
                SSCPReports.txtTrackingNumber.Text = txtTrackingNumber.Text
                SSCPReports.txtOrigin.Text = "Facility Summary"
                SSCPReports.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub llbViewSSCPEnforcement_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewSSCPEnforcement.LinkClicked
        Try

            Dim enfNum As String = txtEnforcementNumber.Text
            If enfNum = "" Then Exit Sub
            If DAL.SSCP.EnforcementExists(enfNum) Then
                OpenMultiForm("SscpEnforcement", enfNum)
            Else
                MsgBox("Enforcement number is not in the system.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub llbViewFCE_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewFCE.LinkClicked
        Try
            If txtFCEYear.Text <> "" Then
                ViewFCE()
                SSCPFCE.cboFCEYear.Text = txtFCEYear.Text
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub ViewFCE()
        Try
            If Me.AirsNumber Is Nothing Then
                MsgBox("Please Enter a valid AIRS Number.", MsgBoxStyle.Information, "Facility Summary Warning")
            Else
                SSCPFCE = Nothing
                If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                SSCPFCE.txtAirsNumber.Text = Me.AirsNumber.ToString
                SSCPFCE.txtOrigin.Text = "Facility Summary"
                SSCPFCE.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " ... SSPP Permitting Work"

    Private Sub LoadPermitsData()
        Dim allPermits As DataTable = DAL.SSPP.GetPermitsAsTable(Me.AirsNumber.ToString)

        dgvPermits.DataSource = allPermits

        dgvPermits.Columns("ISSUEDPERMITID").Visible = False
        dgvPermits.Columns("STRAIRSNUMBER").Visible = False
        dgvPermits.Columns("STRPERMITNUMBER").HeaderText = "Permit Number"
        dgvPermits.Columns("STRPERMITNUMBER").Width = 150
        dgvPermits.Columns("DATISSUED").HeaderText = "Date Issued"
        dgvPermits.Columns("DATISSUED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvPermits.Columns("DATREVOKED").HeaderText = "Date Revoked"
        dgvPermits.Columns("DATREVOKED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        dgvPermits.Columns("ACTIVE").HeaderText = "Active"
        dgvPermits.Columns("ACTIVE").DefaultCellStyle.FormatProvider = New BooleanFormatProvider
        dgvPermits.Columns("ACTIVE").DefaultCellStyle.Format = BooleanFormatProvider.BooleanFormatProviderFormat.YesNo.ToString
        dgvPermits.Columns("PERMITTYPECODE").Visible = False

        dgvPermits.SanelyResizeColumns()
    End Sub

    Private Sub dgvPermits_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) _
    Handles dgvPermits.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If

        'If dgvPermits.Rows(e.RowIndex).Cells("ACTIVE").Value = 0 Then
        '    e.CellStyle.BackColor = System.Drawing.SystemColors.ControlLight
        'End If

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
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
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

#Region " ICIS-Air Update "

    Private Sub UpdateEpaData()
        If selectedFacility IsNot Nothing Then
            If DAL.FacilityModule.TriggerDataUpdateAtEPA(Me.AirsNumber.ToString) Then
                MessageBox.Show("Data for this facility will be sent to EPA the next time the database update procedures run.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("There was an error attempting to flag this facility to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("The AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

#End Region

#Region " Navigation Panel "

    Private Sub FacilityApprovalLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles FacilityApprovalLinkLabel.LinkClicked
        OpenSingleForm("IAIPFacilityCreator")
    End Sub

    Private Sub AirsNumberEntry_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AirsNumberEntry.Enter
        Me.AcceptButton = ViewData
    End Sub

    Private Sub AirsNumberEntry_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AirsNumberEntry.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub ViewData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewData.Click
        If AirsNumberEntry.Text = "" Then
            ClearAllData()
        Else
            Try
                Me.AirsNumber = AirsNumberEntry.Text
            Catch ex As InvalidAirsNumberException
                ClearAllData()
                FacilityNameDisplay.Text = "Invalid AIRS number"
                AirsNumberEntry.BackColor = Color.Bisque
                AirsNumberEntry.Focus()
            End Try
        End If
    End Sub

#End Region

#Region " Menu Strip "

    Private Sub LookUpFacilityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LookUpFacilityToolStripMenuItem.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub PrintFacilitySummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintFacilitySummaryToolStripMenuItem.Click
        OpenFacilitySummaryPrintTool()
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearFormToolStripMenuItem.Click
        Me.AirsNumber = Nothing
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EditContactInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditContactInformationToolStripMenuItem.Click
        OpenEditContactInformationTool()
    End Sub

    Private Sub FacilityCreatorToolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FacilityCreatorToolToolStripMenuItem.Click
        OpenSingleForm("IAIPFacilityCreator")
    End Sub

    Private Sub UpdateAllDataSentToEPAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateAllDataSentToEPAToolStripMenuItem.Click
        UpdateEpaData()
    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineHelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

End Class