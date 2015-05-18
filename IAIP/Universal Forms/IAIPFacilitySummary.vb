﻿Imports Oracle.DataAccess.Client ' TODO: remove
Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.Apb.Facilities
Imports Iaip.DAL.FacilitySummary

Public Class IAIPFacilitySummary

#Region " Deprecated fields "

    Dim SQL As String
    Dim dsFacilityWideData As DataSet
    Dim daFacilityWideData As OracleDataAdapter
    Dim dsISMP As DataSet
    Dim daISMP As OracleDataAdapter

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

    Private ThisFacility As Facility
    Private FacilitySummaryDataSet As DataSet

    Private Enum FacilityDataTables
        ComplianceWork
        ComplianceEnforcement
        ComplianceFCE
        Fees
        ContactsState
        ContactsWebSite
        ContactsPermitting
        ContactsTesting
        ContactsCompliance
        ContactsGeco
    End Enum

#End Region

#Region " Form Load "

    Private Sub IAIPFacilitySummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)
        LoadPermissions()
        InitializeDataTables()
        InitializeAcceptButtonDictionary()
        InitializeGridSelectionDictionary()
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

    Private Sub InitializeDataTables()
        FacilitySummaryDataSet = New DataSet
        With FacilitySummaryDataSet.Tables
            .Add(FacilityDataTables.ComplianceWork.ToString)
            .Add(FacilityDataTables.ComplianceEnforcement.ToString)
            .Add(FacilityDataTables.ComplianceFCE.ToString)
            .Add(FacilityDataTables.Fees.ToString)
            .Add(FacilityDataTables.ContactsCompliance.ToString)
            .Add(FacilityDataTables.ContactsGeco.ToString)
            .Add(FacilityDataTables.ContactsPermitting.ToString)
            .Add(FacilityDataTables.ContactsState.ToString)
            .Add(FacilityDataTables.ContactsTesting.ToString)
            .Add(FacilityDataTables.ContactsWebSite.ToString)
        End With
    End Sub

#End Region

#Region " Clear all data "

    Private Sub ClearAllData()
        ThisFacility = Nothing
        FacilitySummaryDataSet.Clear()

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

#Region " Basic Info data "

    Private Sub ClearBasicFacilityData()

        'Navigation Panel
        AirsNumberEntry.BackColor = SystemColors.ControlLightLight
        FacilityNameDisplay.Text = ""

        'Location
        LocationDisplay.Text = ""
        MapAddressLink.Enabled = False
        LatLonDisplay.Text = ""
        MapLatLonLink.Enabled = False

        'Description
        InfoDescDisplay.Text = ""

        'Status
        InfoClassDisplay.Text = ""
        InfoOperStatusDisplay.Text = ""
        CmsDisplay.Text = ""
        CmsDisplay.BackColor = SystemColors.ControlLightLight
        ComplianceStatusDisplay.Text = ""
        ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight

        'Offices
        DistrictOfficeDisplay.Text = ""
        ResponsibleOfficeDisplay.Text = ""

        'Facility Dates
        InfoStartupDateDisplay.Text = ""
        InfoPermitRevocationDateDisplay.Text = ""
        CreatedDateDisplay.Text = ""

        'Data Dates
        FisDateDisplay.Text = ""
        EpaDateDisplay.Text = ""
        DataUpdateDateDisplay.Text = ""

    End Sub

    Private Sub LoadBasicFacilityAndHeaderData()
        ThisFacility = DAL.FacilityModule.GetFacility(Me.AirsNumber)

        If ThisFacility Is Nothing Then
            FacilityNameDisplay.Text = "Facility does not exist"
            AirsNumberEntry.BackColor = Color.Bisque
            AirsNumberEntry.Focus()
        Else
            EnableFacilityTools()
            ThisFacility.RetrieveHeaderData()
            ThisFacility.RetrieveComplianceStatusList()
            DisplayBasicFacilityData()
            DisplayHeaderData()
        End If
    End Sub

    Private Sub DisplayBasicFacilityData()

        'Navigation Panel
        AirsNumberEntry.Text = Me.AirsNumber.FormattedString

        With ThisFacility

            FacilityNameDisplay.Text = .FacilityName
            FacilityApprovalLinkLabel.Visible = Not .ApprovedByApb

            With .FacilityLocation
                'Location
                LocationDisplay.Text = .Address.ToString
                CountyDisplay.Text = .County.ToString
                If .Address IsNot Nothing Then
                    MapAddressLink.Enabled = True
                End If
                LatLonDisplay.Text = .Latitude.ToString & _
                    ", " & _
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
            EpaDateDisplay.Text = String.Format(DateStringFormat, dataDates("EpaExchangeDate"))
            DataUpdateDateDisplay.Text = String.Format(DateStringFormat, dataDates("DataModifiedOn"))
        End If

    End Sub

    Private Sub ColorCodeComplianceStatusDisplay()
        If ThisFacility.ControllingComplianceStatus > 20 Then
            ComplianceStatusDisplay.BackColor = Color.Pink
        ElseIf ThisFacility.ControllingComplianceStatus > 10 Then
            ComplianceStatusDisplay.BackColor = Color.LemonChiffon
        Else
            ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight
        End If
    End Sub

    Private Sub ColorCodeCmsDisplay()
        With ThisFacility.HeaderData
            If (.CmsMember = FacilityCmsMember.A And .Classification <> FacilityClassification.A) _
            OrElse (.CmsMember = FacilityCmsMember.S And .Classification <> FacilityClassification.SM) Then
                CmsDisplay.BackColor = Color.Pink
            Else
                CmsDisplay.BackColor = SystemColors.ControlLightLight
            End If
        End With
    End Sub

#End Region

#Region " Basic Info tab functionality "

    Private Sub MapAddressLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MapAddressLink.LinkClicked
        OpenMapUrl(ThisFacility.FacilityLocation.Address.ToLinearString, Me)
    End Sub

    Private Sub MapLatLonLink_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles MapLatLonLink.LinkClicked
        OpenMapUrl(ThisFacility.FacilityLocation.Latitude.ToString & "," & _
                   ThisFacility.FacilityLocation.Longitude.ToString, Me)
    End Sub

#End Region

#Region " Header data "

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
        With ThisFacility.HeaderData
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

#Region " Generic data procedures "

    Private Sub LoadDataTable(ByVal whichTable As FacilityDataTables)
        If Me.AirsNumber Is Nothing OrElse Me.ThisFacility Is Nothing Then Exit Sub
        If TableDataExists(whichTable) Then Exit Sub

        Dim table As DataTable = GetData(whichTable)
        If table IsNot Nothing AndAlso table.Rows.Count > 0 Then
            FacilitySummaryDataSet.Tables(whichTable.ToString).Merge(table, False, MissingSchemaAction.Add)
            SetUpData(whichTable)
        End If
    End Sub

    Private Function TableDataExists(ByVal whichTable As FacilityDataTables) As Boolean
        Return FacilitySummaryDataSet.Tables(whichTable.ToString).Rows.Count > 0
    End Function

    Private Function GetData(ByVal table As FacilityDataTables) As DataTable
        Select Case table
            Case FacilityDataTables.ComplianceWork
                Return GetComplianceWorkData(Me.AirsNumber)
            Case FacilityDataTables.ComplianceFCE
                Return GetComplianceFceData(Me.AirsNumber)
            Case FacilityDataTables.ComplianceEnforcement
                Return GetComplianceEnforcementData(Me.AirsNumber)
            Case FacilityDataTables.Fees
                Return GetFeesData(Me.AirsNumber)
            Case FacilityDataTables.ContactsState
                Return GetContactsStateData(Me.AirsNumber)
            Case FacilityDataTables.ContactsWebSite
                Return GetContactsWebSiteData(Me.AirsNumber)
            Case FacilityDataTables.ContactsPermitting
                Return GetContactsPermittingData(Me.AirsNumber)
            Case FacilityDataTables.ContactsTesting
                Return GetContactsTestingData(Me.AirsNumber)
            Case FacilityDataTables.ContactsCompliance
                Return GetContactsComplianceData(Me.AirsNumber)
            Case FacilityDataTables.ContactsGeco
                Return GetContactsGecoData(Me.AirsNumber)

            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub SetUpData(ByVal table As FacilityDataTables)
        Select Case table
            Case FacilityDataTables.ComplianceWork
                SetUpComplianceWorkGrid()
            Case FacilityDataTables.ComplianceFCE
                SetUpComplianceFceGrid()
            Case FacilityDataTables.ComplianceEnforcement
                SetUpComplianceEnforcementGrid()
            Case FacilityDataTables.Fees
                SetUpFeesTab()
            Case FacilityDataTables.ContactsState
                SetUpContactsStateGrid()
            Case FacilityDataTables.ContactsWebSite
                SetUpContactsWebSiteGrid()
            Case FacilityDataTables.ContactsPermitting
                SetUpContactsPermittingGrid()
            Case FacilityDataTables.ContactsTesting
                SetUpContactsTestingGrid()
            Case FacilityDataTables.ContactsCompliance
                SetUpContactsComplianceGrid()
            Case FacilityDataTables.ContactsGeco
                SetUpContactsGecoGrid()

        End Select
    End Sub

#End Region

#Region " AcceptButton "

    Private AcceptButtonDictionary As New Dictionary(Of TextBox, Button)
    Private Sub InitializeAcceptButtonDictionary()
        ' Navigation Panel
        AcceptButtonDictionary.Add(AirsNumberEntry, ViewDataButton)

        'Compliance tab
        AcceptButtonDictionary.Add(ComplianceWorkEntry, OpenComplianceWorkButton)
        AcceptButtonDictionary.Add(ComplianceFceEntry, OpenComplianceFceButton)
        AcceptButtonDictionary.Add(ComplianceEnforcementEntry, OpenComplianceEnforcementButton)

    End Sub

    Private Sub AddAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles AirsNumberEntry.Enter, _
    ComplianceWorkEntry.Enter, ComplianceFceEntry.Enter, ComplianceEnforcementEntry.Enter
        If TypeOf (sender) Is TextBox Then
            Me.AcceptButton = AcceptButtonDictionary(sender)
        End If
    End Sub

    Private Sub RemoveAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles AirsNumberEntry.Leave, _
    ComplianceWorkEntry.Leave, ComplianceFceEntry.Leave, ComplianceEnforcementEntry.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

#Region " Grid Item Selection "

    Private GridSelectionDictionary As New Dictionary(Of DataGridView, TextBox)
    Private Sub InitializeGridSelectionDictionary()
        'Compliance tab
        GridSelectionDictionary.Add(ComplianceEnforcementGrid, ComplianceEnforcementEntry)
        GridSelectionDictionary.Add(ComplianceFceGrid, ComplianceFceEntry)
        GridSelectionDictionary.Add(ComplianceWorkGrid, ComplianceWorkEntry)
    End Sub

    Private Sub HandleGridSelection(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    Handles ComplianceWorkGrid.CellEnter, ComplianceFceGrid.CellEnter, ComplianceEnforcementGrid.CellEnter
        If TypeOf (sender) Is DataGridView Then
            Dim dgv As DataGridView = CType(sender, DataGridView)
            If e.RowIndex <> -1 AndAlso e.RowIndex < dgv.RowCount Then
                GridSelectionDictionary(dgv).Text = dgv(0, e.RowIndex).FormattedValue
            End If
        End If
    End Sub

#End Region

#Region " Specific data procedures "

#Region " Compliance data "

    Private Sub LoadComplianceData()
        LoadDataTable(FacilityDataTables.ComplianceWork)
        LoadDataTable(FacilityDataTables.ComplianceFCE)
        LoadDataTable(FacilityDataTables.ComplianceEnforcement)
    End Sub

    Private Sub SetUpComplianceWorkGrid()
        With ComplianceWorkGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ComplianceWork.ToString)
                .Columns("STRTRACKINGNUMBER").HeaderText = "Tracking Number"
                .Columns("STRTRACKINGNUMBER").DisplayIndex = 0
                .Columns("STRACTIVITYNAME").HeaderText = "Event Type"
                .Columns("RECEIVEDDATE").HeaderText = "Date Received"
                .Columns("RECEIVEDDATE").DefaultCellStyle.Format = DateFormat
            End If
        End With
    End Sub

    Private Sub SetUpComplianceEnforcementGrid()
        With ComplianceEnforcementGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ComplianceEnforcement.ToString)
                .Columns("STRENFORCEMENTNUMBER").HeaderText = "Enforcement Number"
                .Columns("STRENFORCEMENTNUMBER").DisplayIndex = 0
                .Columns("ViolationDate").HeaderText = "Discovery Date"
                .Columns("ViolationDate").DefaultCellStyle.Format = DateFormat
                .Columns("HPVStatus").HeaderText = "HPV Status"
                .Columns("Status").HeaderText = "Status"
            End If
        End With
    End Sub

    Private Sub SetUpComplianceFceGrid()
        With ComplianceFceGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ComplianceFCE.ToString)
                .Columns("FCEYear").HeaderText = "FCE Year"
                .Columns("FCEYear").DisplayIndex = 0
                .Columns("FCECompleted").HeaderText = "Date Completed"
                .Columns("FCECompleted").DefaultCellStyle.Format = DateFormat
                .Columns("FCECompleted").DisplayIndex = 1
                .Columns("ReviewingEngineer").HeaderText = "Reviewing Engineer"
                .Columns("ReviewingEngineer").DisplayIndex = 2
                .Columns("STRFCECOMMENTS").HeaderText = "Comments"
                .Columns("STRFCECOMMENTS").DisplayIndex = 3
                .Columns("STRFCENUMBER").Visible = False
                .Columns("STRFCENUMBER").DisplayIndex = 4
            End If
        End With
    End Sub

#End Region

#Region " Compliance Tab events "

    Private Sub OpenComplianceWorkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenComplianceWorkButton.Click
        OpenFormSscpWorkItem(ComplianceWorkEntry.Text)
    End Sub

    Private Sub OpenComplianceFceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenComplianceFceButton.Click
        OpenFormFceByYear(Me.AirsNumber, ComplianceFceEntry.Text)
    End Sub

    Private Sub OpenComplianceEnforcementButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenComplianceEnforcementButton.Click
        OpenFormEnforcement(ComplianceEnforcementEntry.Text)
    End Sub

#End Region

#Region " Fees data "

    Private Sub LoadFeesData()
        LoadDataTable(FacilityDataTables.Fees)
    End Sub

    Private Sub SetUpFeesTab()
        If FeeYearSelect.DataSource Is Nothing Then
            With FeeYearSelect
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString)
                .DisplayMember = "FeesData"
                .ValueMember = "intYear"
                .SelectedIndex = 0
            End With

            Dim textBoxDataBindings As New Dictionary(Of TextBox, String)
            textBoxDataBindings.Add(FeeFacilityClassDisplay, "strClass")
            textBoxDataBindings.Add(FeeDateSubmitDisplay, "DateSubmit")
            textBoxDataBindings.Add(FeeStatusDisplay, "strIAIPDesc")
            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString), item.Value))
            Next

            Dim textBoxDataBindingsTons As New Dictionary(Of TextBox, String)
            textBoxDataBindingsTons.Add(FeeVocDisplay, "intVOCTons")
            textBoxDataBindingsTons.Add(FeePmDisplay, "intPMTons")
            textBoxDataBindingsTons.Add(FeeSO2Display, "intSO2Tons")
            textBoxDataBindingsTons.Add(FeeNOxDisplay, "intNOXtons")
            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsTons
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatTons
                item.Key.DataBindings.Add(b)
            Next

            Dim textBoxDataBindingsDollars As New Dictionary(Of TextBox, String)
            textBoxDataBindingsDollars.Add(FeeTotalDisplay, "NumTotalFee")
            textBoxDataBindingsDollars.Add(FeePaidDisplay, "TotalPaid")
            textBoxDataBindingsDollars.Add(FeePart70Display, "NumPart70Fee")
            textBoxDataBindingsDollars.Add(FeeSmDisplay, "NumSMFee")
            textBoxDataBindingsDollars.Add(FeeNspsDisplay, "NumNSPSFee")
            textBoxDataBindingsDollars.Add(FeeAdminDisplay, "NumAdminFee")
            textBoxDataBindingsDollars.Add(FeePollutantTotalDisplay, "numCalculatedFee")
            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsDollars
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatDollars
                item.Key.DataBindings.Add(b)
            Next

            Dim binding As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString), "NumFeeRate")
            AddHandler binding.Format, AddressOf BindingFormatDollarsPerTon
            FeeRateDisplay.DataBindings.Add(binding)

            Dim checkBoxDataBindings As New Dictionary(Of CheckBox, String)
            checkBoxDataBindings.Add(FeeFacilityOperatingDisplay, "strOperate")
            checkBoxDataBindings.Add(FeeFacilityPart70Display, "strPart70")
            checkBoxDataBindings.Add(FeeFacilityNspsExemptDisplay, "strNSPSExempt")
            For Each item As KeyValuePair(Of CheckBox, String) In checkBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Checked", FacilitySummaryDataSet.Tables(FacilityDataTables.Fees.ToString), item.Value))
            Next

        End If
    End Sub

    Private Sub BindingFormatTons(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = num.ToString("N0") & " ton"
            If num <> 1 Then cevent.Value = cevent.Value & "s"
        End If
    End Sub

    Private Sub BindingFormatDollars(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = "$" & num.ToString("N0")
        End If
    End Sub

    Private Sub BindingFormatDollarsPerTon(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = "$" & num.ToString("N2") & " /ton"
        End If
    End Sub

#End Region

#Region " Contacts data "

    Private Sub LoadContactsData()
        LoadDataTable(FacilityDataTables.ContactsState)
        LoadDataTable(FacilityDataTables.ContactsWebSite)
        LoadDataTable(FacilityDataTables.ContactsPermitting)
        LoadDataTable(FacilityDataTables.ContactsTesting)
        LoadDataTable(FacilityDataTables.ContactsCompliance)
        LoadDataTable(FacilityDataTables.ContactsGeco)
    End Sub

    Private Sub SetUpContactsStateGrid()
        With ContactsStateGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsState.ToString)
                .Columns("AirProgram").HeaderText = "Program"
                .Columns("Staff").HeaderText = "Staff"
                .Columns("Unit").HeaderText = "Unit"
            End If
        End With
    End Sub

    Private Sub SetUpContactsWebSiteGrid()
        With ContactsWebSiteGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsWebSite.ToString)
                .Columns("strContactKey").Visible = False
                .Columns("ContactName").HeaderText = "Name"
                .Columns("strContactTitle").HeaderText = "Title"
                .Columns("strContactCompanyName").HeaderText = "Company"
                .Columns("strContactPhoneNumber1").HeaderText = "Phone"
                .Columns("strContactFaxNumber").HeaderText = "Fax"
                .Columns("strContactEmail").HeaderText = "Email"
                .Columns("Address").HeaderText = "Address"
                .Columns("strContactDescription").HeaderText = "User Type"
            End If
        End With
    End Sub

    Private Sub SetUpContactsPermittingGrid()
        With ContactsPermittingGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsPermitting.ToString)
                .Columns("strContactKey").Visible = False
                .Columns("ContactName").HeaderText = "Name"
                .Columns("strContactTitle").HeaderText = "Title"
                .Columns("strContactCompanyName").HeaderText = "Company"
                .Columns("strContactPhoneNumber1").HeaderText = "Phone"
                .Columns("strContactFaxNumber").HeaderText = "Fax"
                .Columns("strContactEmail").HeaderText = "Email"
                .Columns("Address").HeaderText = "Address"
                .Columns("strContactDescription").HeaderText = "User Type"
            End If
        End With
    End Sub

    Private Sub SetUpContactsTestingGrid()
        With ContactsTestingGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsTesting.ToString)
                .Columns("strContactKey").Visible = False
                .Columns("ContactName").HeaderText = "Name"
                .Columns("strContactTitle").HeaderText = "Title"
                .Columns("strContactCompanyName").HeaderText = "Company"
                .Columns("strContactPhoneNumber1").HeaderText = "Phone"
                .Columns("strContactFaxNumber").HeaderText = "Fax"
                .Columns("strContactEmail").HeaderText = "Email"
                .Columns("Address").HeaderText = "Address"
                .Columns("strContactDescription").HeaderText = "User Type"
            End If
        End With
    End Sub

    Private Sub SetUpContactsComplianceGrid()
        With ContactsComplianceGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsCompliance.ToString)
                .Columns("strContactKey").Visible = False
                .Columns("ContactName").HeaderText = "Name"
                .Columns("strContactTitle").HeaderText = "Title"
                .Columns("strContactCompanyName").HeaderText = "Company"
                .Columns("strContactPhoneNumber1").HeaderText = "Phone"
                .Columns("strContactFaxNumber").HeaderText = "Fax"
                .Columns("strContactEmail").HeaderText = "Email"
                .Columns("Address").HeaderText = "Address"
                .Columns("strContactDescription").HeaderText = "User Type"
            End If
        End With
    End Sub

    Private Sub SetUpContactsGecoGrid()
        With ContactsGecoGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTables.ContactsGeco.ToString)
                .Columns("NUMUSERID").Visible = False
                .Columns("STRUSERTYPE").HeaderText = "User Type"
                .Columns("GECOContact").HeaderText = "Name"
                .Columns("STRTITLE").HeaderText = "Title"
                .Columns("STRUSEREMAIL").HeaderText = "Email"
                .Columns("STRPHONENUMBER").HeaderText = "Phone"
                .Columns("STRFAXNUMBER").HeaderText = "Fax"
                .Columns("STRCOMPANYNAME").HeaderText = "Company"
                .Columns("Address").HeaderText = "Address"
            End If
        End With
    End Sub

#End Region

#Region "... Emission Inventory data "

    Private Sub LoadEmissionInventoryData()

    End Sub

#End Region

#Region "... Financial "

    Private Sub LoadFinancialData()

    End Sub

#End Region

#Region "... Permitting data "

    Private Sub LoadPermittingData()

    End Sub

#End Region

#Region "... Testing data "

    Private Sub LoadTestingData()

    End Sub

#End Region

#End Region

#Region " ... open other forms"

    Private Sub OpenEditContactInformationTool()
        If Me.AirsNumber IsNot Nothing Then
            Dim parameters As New Dictionary(Of String, String)
            parameters("airsnumber") = Me.AirsNumber.ShortString
            parameters("facilityname") = Me.ThisFacility.FacilityName
            OpenMultiForm("IAIPEditContacts", Me.AirsNumber.ShortString, parameters)
        End If
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
            FacilityPrintOut.FacilityName.Text = Me.ThisFacility.FacilityName

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
            editHeaderDataDialog.FacilityName = Me.ThisFacility.FacilityName

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

    Private Sub EditContactsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditContactsButton.Click
        OpenEditContactInformationTool()
    End Sub

#End Region

#Region " ... main Load Work"

    Private Sub llbViewAirPermitsOnline_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAirPermitsOnline.LinkClicked
        OpenPermitSearchUrl(Me.AirsNumber, Me)
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
                    ISMPCloseAndPrint.txtFacilityName.Text = ThisFacility.FacilityName
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
        If ThisFacility IsNot Nothing Then
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

    Private Sub ViewData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewDataButton.Click
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

    Private Sub FacilitySearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FacilitySearchButton.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub OpenFacilityLookupTool()
        Dim facilityLookupDialog As New IAIPFacilityLookUpTool
        facilityLookupDialog.ShowDialog()
        If facilityLookupDialog.DialogResult = Windows.Forms.DialogResult.OK _
        AndAlso Apb.ApbFacilityId.IsValidAirsNumberFormat(facilityLookupDialog.SelectedAirsNumber) Then
            Me.AirsNumber = facilityLookupDialog.SelectedAirsNumber
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

#Region " Form-level events "

    Private Sub FacilitySummaryTabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FSMainTabControl.SelectedIndexChanged
        Select Case FSMainTabControl.SelectedTab.Name

            Case FSCompliance.Name
                LoadComplianceData()

            Case FSFees.Name
                LoadFeesData()

            Case FSContacts.Name
                LoadContactsData()

            Case FSEmissionInventory.Name
                LoadEmissionInventoryData()

            Case FSFinancial.Name
                LoadFinancialData()

            Case FSPermitting.Name
                LoadPermittingData()

            Case FSTesting.Name
                LoadTestingData()

        End Select
    End Sub

    Private Sub IAIPFacilitySummary_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.A AndAlso e.Alt Then
            AirsNumberEntry.Focus()
        End If
    End Sub

    Private Sub DisplayEmptyTextBoxAsNA(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles InfoDescDisplay.TextChanged, LocationDisplay.TextChanged, LatLonDisplay.TextChanged, _
        InfoDescDisplay.TextChanged, InfoClassDisplay.TextChanged, InfoOperStatusDisplay.TextChanged, _
        CmsDisplay.TextChanged, ComplianceStatusDisplay.TextChanged, DistrictOfficeDisplay.TextChanged, _
        ResponsibleOfficeDisplay.TextChanged, InfoStartupDateDisplay.TextChanged, _
        InfoPermitRevocationDateDisplay.TextChanged, CreatedDateDisplay.TextChanged, FisDateDisplay.TextChanged, _
        EpaDateDisplay.TextChanged, DataUpdateDateDisplay.TextChanged, _
        HeaderClassDisplay.TextChanged, HeaderOperStatusDisplay.TextChanged, SicDisplay.TextChanged, _
        NaicsDisplay.TextChanged, RmpIdDisplay.TextChanged, HeaderStartupDisplay.TextChanged, _
        HeaderRevocationDateDisplay.TextChanged, HeaderDescDisplay.TextChanged

        Dim t As TextBox = CType(sender, TextBox)
        If t.Text = "" Then
            t.Text = "N/A"
        End If
    End Sub

#End Region


End Class