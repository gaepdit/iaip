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

    Private _airsNumber As ApbFacilityId = Nothing
    Public Property AirsNumber() As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(ByVal value As ApbFacilityId)
            If _airsNumber Is Nothing AndAlso value Is Nothing Then Return
            If _airsNumber IsNot Nothing AndAlso _airsNumber.Equals(value) Then Return
            _airsNumber = value
            ClearAllData()
            If _airsNumber Is Nothing Then
                AirsNumberEntry.Focus()
            Else
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
        ' TODO DWW: Better permissions defn 

        ' Menu items
        UpdateEpaMenuItem.Available = (UserGCode = "1" Or UserGCode = "345")
        CreateFacilityMenuItem.Available = (AccountFormAccess(138, 0) IsNot Nothing _
                                          AndAlso AccountFormAccess(138, 0) = "138" _
                                          AndAlso (AccountFormAccess(138, 1) = "1" _
                                                   Or AccountFormAccess(138, 2) = "1" _
                                                   Or AccountFormAccess(138, 3) = "1" _
                                                   Or AccountFormAccess(138, 4) = "1"))
        ToolsMenuSeparator.Visible = (CreateFacilityMenuItem.Available And UpdateEpaMenuItem.Available)

        ' Close/Print Test Reports
        llbClosePrintTestReport.Visible = UserAccounts.Contains("(118)")
        
        ' Edit location/header data
        If UserUnit = "---" Or AccountFormAccess(22, 3) = "1" Or AccountFormAccess(1, 3) = "1" Then
            EditFacilityLocationButton.Visible = True
            EditHeaderDataButton.Visible = True
        Else
            EditFacilityLocationButton.Visible = False
            EditHeaderDataButton.Visible = False
        End If
    End Sub

#End Region

#Region " Clear all data "

    Private Sub ClearAllData()
        selectedFacility = Nothing
        selectedFacilityDataSet = Nothing

        DisableFacilityTools()

        'TODO: Fill this out as more data is configured
        ClearBasicFacilityData()
        ClearHeaderData()


    End Sub

    Private Sub DisableFacilityTools()
        FSMainTabControl.SelectedTab = FSInfo
        FSMainTabControl.Enabled = False
        UpdateEpaMenuItem.Enabled = False
        PrintFacilitySummaryMenuItem.Enabled = False
    End Sub

    Private Sub EnableFacilityTools()
        FSMainTabControl.Enabled = True
        UpdateEpaMenuItem.Enabled = True
        PrintFacilitySummaryMenuItem.Enabled = True
    End Sub

#End Region

#Region " Basic Info Data "

    Private Sub ClearBasicFacilityData()

        'Navigation Panel
        AirsNumberEntry.BackColor = SystemColors.ControlLightLight
        FacilityNameDisplay.Text = ""

        'Location
        LocationDisplay.Text = "N/A"
        MapAddressLink.Enabled = False
        LatLonDisplay.Text = "N/A"
        MapLatLonLink.Enabled = False

        'Description
        InfoDescDisplay.Text = "N/A"

        'Status
        InfoClassDisplay.Text = "N/A"
        InfoOperStatusDisplay.Text = "N/A"
        CmsDisplay.Text = "N/A"
        CmsDisplay.BackColor = SystemColors.ControlLightLight
        ComplianceStatusDisplay.Text = "N/A"
        ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight

        'Offices
        DistrictOfficeDisplay.Text = "N/A"
        ResponsibleOfficeDisplay.Text = "N/A"

        'Facility Dates
        InfoStartupDateDisplay.Text = "N/A"
        InfoPermitRevocationDateDisplay.Text = "N/A"
        CreatedDateDisplay.Text = "N/A"

        'Data Dates
        FisDateDisplay.Text = "N/A"
        EpaDateDisplay.Text = "N/A"
        DataUpdateDateDisplay.Text = "N/A"

    End Sub

    Private Sub LoadBasicFacilityAndHeaderData()
        selectedFacility = DAL.FacilityModule.GetFacility(Me.AirsNumber)

        If selectedFacility Is Nothing Then
            FacilityNameDisplay.Text = "Facility does not exist"
            AirsNumberEntry.BackColor = Color.Bisque
            AirsNumberEntry.Focus()
        Else
            EnableFacilityTools()
            selectedFacility.RetrieveHeaderData()
            selectedFacility.RetrieveComplianceStatusList()
            DisplayBasicFacilityData()
            DisplayHeaderData()
        End If
    End Sub

    Private Sub DisplayBasicFacilityData()

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
                InfoClassDisplay.Text = .ClassificationCode & ", " & .ClassificationDescription
                InfoOperStatusDisplay.Text = .OperationalStatusDescription
                CmsDisplay.Text = .CmsMemberDescription
                ColorCodeCmsDisplay()

                'Description
                InfoDescDisplay.Text = .FacilityDescription

                'Facility Dates
                InfoStartupDateDisplay.Text = String.Format(DateStringFormat, .StartupDate)
                InfoPermitRevocationDateDisplay.Text = String.Format(DateStringFormat, .ShutdownDate)
            End With

            'Compliance Status
            ComplianceStatusDisplay.Text = .ControllingComplianceStatus.GetDescription
            ColorCodeComplianceStatusDisplay()

            'Offices
            DistrictOfficeDisplay.Text = .DistrictOfficeLocation
            ResponsibleOfficeDisplay.Text = If(.DistrictResponsible, "District Office", "Air Branch")

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
        Else
            ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight
        End If
    End Sub

    Private Sub ColorCodeCmsDisplay()
        With selectedFacility.HeaderData
            If (.CmsMember = FacilityCmsMember.A And .Classification <> FacilityClassification.A) _
            OrElse (.CmsMember = FacilityCmsMember.S And .Classification <> FacilityClassification.SM) Then
                CmsDisplay.BackColor = Color.Pink
            Else
                CmsDisplay.BackColor = SystemColors.ControlLightLight
            End If
        End With
    End Sub

#End Region

#Region " Basic Info TabPage functionality "

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
        'Status
        HeaderClassDisplay.Text = "N/A"
        HeaderOperStatusDisplay.Text = "N/A"
        'Codes
        SicDisplay.Text = "N/A"
        NaicsDisplay.Text = "N/A"
        RmpIdDisplay.Text = "N/A"
        'Facility Dates
        HeaderStartupDisplay.Text = "N/A"
        HeaderRevocationDateDisplay.Text = "N/A"
        'Desc
        HeaderDescDisplay.Text = "N/A"
        'Air Programs
        AirProgramsListBox.Items.Clear()
        EditPollutantsButton.Enabled = False
        EditSubpartsButton.Visible = False
        'Program Classifications
        ProgramClassificationsListBox.Items.Clear()
        'Nonattainment
        OneHourOzoneDisplay.Text = "N/A"
        EightHourOzoneDisplay.Text = "N/A"
        PmNonattainmentDisplay.Text = "N/A"
    End Sub

    Private Sub DisplayHeaderData()
        With selectedFacility.HeaderData
            'Status
            HeaderClassDisplay.Text = .ClassificationDescription
            HeaderOperStatusDisplay.Text = .OperationalStatusDescription
            'Codes
            SicDisplay.Text = .SicCode
            NaicsDisplay.Text = .Naics
            RmpIdDisplay.Text = .RmpId
            'Facility Dates
            HeaderStartupDisplay.Text = String.Format(DateStringFormat, .StartupDate)
            HeaderRevocationDateDisplay.Text = String.Format(DateStringFormat, .ShutdownDate)
            'Desc
            HeaderDescDisplay.Text = .FacilityDescription

            'Air Programs
            Dim tempAP As AirProgram = .AirPrograms
            With AirProgramsListBox.Items
                If tempAP = AirProgram.None Then
                    .Add(AirProgram.None.GetDescription)
                Else
                    If (tempAP And AirProgram.SIP) Then .Add(AirProgram.SIP.GetDescription)
                    If (tempAP And AirProgram.FederalSIP) Then AirProgramsListBox.Items.Add(AirProgram.FederalSIP.GetDescription)
                    If (tempAP And AirProgram.NonFederalSIP) Then AirProgramsListBox.Items.Add(AirProgram.NonFederalSIP.GetDescription)
                    If (tempAP And AirProgram.CfcTracking) Then AirProgramsListBox.Items.Add(AirProgram.CfcTracking.GetDescription)
                    If (tempAP And AirProgram.PSD) Then AirProgramsListBox.Items.Add(AirProgram.PSD.GetDescription)
                    If (tempAP And AirProgram.NSR) Then AirProgramsListBox.Items.Add(AirProgram.NSR.GetDescription)
                    If (tempAP And AirProgram.TitleV) Then AirProgramsListBox.Items.Add(AirProgram.TitleV.GetDescription)
                    If (tempAP And AirProgram.MACT) Then AirProgramsListBox.Items.Add(AirProgram.MACT.GetDescription)
                    If (tempAP And AirProgram.NESHAP) Then AirProgramsListBox.Items.Add(AirProgram.NESHAP.GetDescription)
                    If (tempAP And AirProgram.NSPS) Then AirProgramsListBox.Items.Add(AirProgram.NSPS.GetDescription)
                    If (tempAP And AirProgram.AcidPrecipitation) Then AirProgramsListBox.Items.Add(AirProgram.AcidPrecipitation.GetDescription)
                    If (tempAP And AirProgram.FESOP) Then AirProgramsListBox.Items.Add(AirProgram.FESOP.GetDescription)
                    If (tempAP And AirProgram.NativeAmerican) Then AirProgramsListBox.Items.Add(AirProgram.NativeAmerican.GetDescription)
                    If (tempAP And AirProgram.RMP) Then AirProgramsListBox.Items.Add(AirProgram.RMP.GetDescription)
                End If
            End With

            'Buttons for Air Program Subparts
            EditSubpartsButton.Visible = .AirPrograms And (AirProgram.MACT _
                                                           Or AirProgram.NESHAP _
                                                           Or AirProgram.NSPS _
                                                           Or AirProgram.SIP)
            EditPollutantsButton.Enabled = True

            'Classifications
            If (.AirProgramClassifications = AirProgramClassification.None) Then
                ProgramClassificationsListBox.Items.Add(AirProgramClassification.None.GetDescription)
            Else
                If (.AirProgramClassifications And AirProgramClassification.NsrMajor) Then
                    ProgramClassificationsListBox.Items.Add(AirProgramClassification.NsrMajor.GetDescription)
                End If
                If (.AirProgramClassifications And AirProgramClassification.HapMajor) Then
                    ProgramClassificationsListBox.Items.Add(AirProgramClassification.HapMajor.GetDescription)
                End If
            End If

            'Nonattainment
            OneHourOzoneDisplay.Text = .OneHourOzoneNonAttainment.ToString
            EightHourOzoneDisplay.Text = .EightHourOzoneNonAttainment.ToString
            PmNonattainmentDisplay.Text = .PMFineNonAttainmentState.ToString

        End With
    End Sub

#End Region

#Region " Fees Data "

    Private Sub LoadFeesData()
        Try
            Dim PollutantStatus As String = ""
            Dim dtFacilityWideData As New DataTable
            Dim drDSRow As DataRow
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub ClearForm()
        'If FSTabControl.TabPages.Contains(FSContacts) = True Then
        '    txtSSCPContact.Clear()
        '    txtSSCPUnit.Clear()
        '    txtSSPPContact.Clear()
        '    txtSSPPUnit.Clear()
        '    txtISMPContact.Clear()
        '    txtISMPUnit.Clear()
        '    txtDistrictEngineer.Clear()
        '    txtDistrictUnit.Clear()
        '    FSTabControl.TabPages.Remove(FSContacts)
        'End If
        'If FSTabControl.TabPages.Contains(FSEmissionInventory) = True Then
        '    FSTabControl.TabPages.Remove(FSEmissionInventory)
        'End If
        'If FSTabControl.TabPages.Contains(FSTesting) = True Then
        '    FSTabControl.TabPages.Remove(FSTesting)
        'End If
        'If FSTabControl.TabPages.Contains(FSCompliance) = True Then
        '    FSTabControl.TabPages.Remove(FSCompliance)
        'End If
        'If FSTabControl.TabPages.Contains(FSPermitting) = True Then
        '    FSTabControl.TabPages.Remove(FSPermitting)
        'End If
        'If FSTabControl.TabPages.Contains(FSFinancial) = True Then
        '    FSTabControl.TabPages.Remove(FSFinancial)
        'End If

        'txtReferenceNumber.Clear()
        'txtTestingNumber.Clear()
        'txtReferenceNumber2.Clear()
        'txtTrackingNumber.Clear()
        'txtFCEYear.Clear()
        'txtEnforcementNumber.Clear()
        'txtApplicationNumber.Clear()
        'EditSubpartsButton.Visible = False

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
                LoadFeesData()
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

    Private Sub btnOpenFacilityLocationEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditFacilityLocationButton.Click
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

    Private Sub btnEditHeaderData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditHeaderDataButton.Click
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(Me.AirsNumber.ToString) Then

            Dim editHeaderDataDialog As New IAIPEditHeaderData
            editHeaderDataDialog.AirsNumber = Me.AirsNumber.ToString
            editHeaderDataDialog.FacilityName = Me.selectedFacility.FacilityName

            editHeaderDataDialog.ShowDialog()

            If editHeaderDataDialog.SomethingWasSaved Then
                LoadFeesData()
            End If

            editHeaderDataDialog.Dispose()
        Else
            MessageBox.Show("AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.AirsNumber = Nothing
        End If
    End Sub

    Private Sub btnEditContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditContacts.Click
        OpenEditContactInformationTool()
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
    Private Sub btnEditAirProgramPollutants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditPollutantsButton.Click
        Dim EditAirProgramPollutants As IAIPEditAirProgramPollutants = OpenSingleForm(IAIPEditAirProgramPollutants)
        EditAirProgramPollutants.AirsNumberDisplay.Text = Me.AirsNumber.ToString
    End Sub
    Private Sub btnOpenSubpartEditior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditSubpartsButton.Click
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

    Private Sub LookUpFacilityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LookUpFacilityMenuItem.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub PrintFacilitySummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintFacilitySummaryMenuItem.Click
        OpenFacilitySummaryPrintTool()
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearFormMenuItem.Click
        Me.AirsNumber = Nothing
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseMenuItem.Click
        Me.Close()
    End Sub

    Private Sub FacilityCreatorToolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateFacilityMenuItem.Click
        OpenSingleForm("IAIPFacilityCreator")
    End Sub

    Private Sub UpdateAllDataSentToEPAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateEpaMenuItem.Click
        UpdateEpaData()
    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

    Private Sub FacilitySummaryTabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FSMainTabControl.SelectedIndexChanged
        Select Case FSMainTabControl.SelectedTab.Name
            Case FSCompliance.Name

            Case FSContacts.Name

            Case FSEmissionInventory.Name

            Case FSFees.Name

            Case FSFinancial.Name

            Case FSPermitting.Name

            Case FSTesting.Name

        End Select
    End Sub

End Class