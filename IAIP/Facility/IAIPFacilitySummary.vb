Imports System.Collections.Generic
Imports System.ComponentModel
Imports EpdIt
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.DAL
Imports Iaip.DAL.FacilitySummaryData

Public Class IAIPFacilitySummary

#Region " Properties and fields "

    Private _airsNumber As ApbFacilityId = Nothing
    Public Property AirsNumber() As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As ApbFacilityId)
            _airsNumber = value
            ReloadAllData()
        End Set
    End Property

    Private ThisFacility As Facility
    Private FacilitySummaryDataSet As DataSet
    Private DataDates As DataRow
    Private bgw As BackgroundWorker

    Friend Enum FacilityDataTable
        ComplianceWork
        ComplianceEnforcement
        ComplianceFCE
        ContactsState
        ContactsWebSite
        ContactsPermitting
        ContactsTesting
        ContactsCompliance
        ContactsGeco
        TestReports
        TestNotifications
        TestMemos
        PermitApplications
        PermitRules
        PermitRuleHistory
        Permits
        PermitApplicationFees
        EmissionsFeesSummary
        EmissionsFeesData
        EmissionsFeesInvoices
        EmissionsFeesDeposits
        EIPost2009
        EIPre2009
    End Enum

#End Region

#Region " Form Load "

    Protected Overrides Sub OnLoad(e As EventArgs)
        LoadPermissions()
        InitializeDataTables()
        InitializeGridEvents()

        MyBase.OnLoad(e)
    End Sub

    Private Sub IAIPFacilitySummary_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'If AirsNumber Is Nothing Then
        '    AirsNumberEntry.Focus()
        'End If
    End Sub

    Private Sub LoadPermissions()
        ' TODO DWW: Better permissions definition

        ' Menu items
        UpdateEpaMenuItem.Available = CurrentUser.HasRole({19, 118})

        CreateFacilityMenuItem.Available = (
            AccountFormAccess(138, 0) IsNot Nothing AndAlso
            AccountFormAccess(138, 0) = "138" AndAlso
            (AccountFormAccess(138, 1) = "1" Or
            AccountFormAccess(138, 2) = "1" Or
            AccountFormAccess(138, 3) = "1" Or
            AccountFormAccess(138, 4) = "1"))

        ToolsMenuSeparator.Visible = (CreateFacilityMenuItem.Available And UpdateEpaMenuItem.Available)

        ' Edit location/header data
        If CurrentUser.UnitId = 0 Or AccountFormAccess(22, 3) = "1" Or AccountFormAccess(1, 3) = "1" Then
            EditFacilityLocationButton.Visible = True
            EditHeaderDataButton.Visible = True
        Else
            EditFacilityLocationButton.Visible = False
            EditHeaderDataButton.Visible = False
        End If
    End Sub

    Private Sub InitializeDataTables()
        FacilitySummaryDataSet = New DataSet

        AddDataTable(FacilityDataTable.ComplianceWork)
        AddDataTable(FacilityDataTable.ComplianceEnforcement)
        AddDataTable(FacilityDataTable.ComplianceFCE)
        AddDataTable(FacilityDataTable.EmissionsFeesSummary)
        AddDataTable(FacilityDataTable.ContactsCompliance)
        AddDataTable(FacilityDataTable.ContactsGeco)
        AddDataTable(FacilityDataTable.ContactsPermitting)
        AddDataTable(FacilityDataTable.ContactsState)
        AddDataTable(FacilityDataTable.ContactsTesting)
        AddDataTable(FacilityDataTable.ContactsWebSite)
        AddDataTable(FacilityDataTable.TestReports)
        AddDataTable(FacilityDataTable.TestNotifications)
        AddDataTable(FacilityDataTable.TestMemos)
        AddDataTable(FacilityDataTable.PermitApplications)
        AddDataTable(FacilityDataTable.PermitRuleHistory)
        AddDataTable(FacilityDataTable.PermitRules)
        AddDataTable(FacilityDataTable.Permits)
        AddDataTable(FacilityDataTable.PermitApplicationFees)
        AddDataTable(FacilityDataTable.EmissionsFeesDeposits)
        AddDataTable(FacilityDataTable.EmissionsFeesData)
        AddDataTable(FacilityDataTable.EmissionsFeesInvoices)
        AddDataTable(FacilityDataTable.EIPost2009)
        AddDataTable(FacilityDataTable.EIPre2009)
    End Sub

    Private Sub AddDataTable(whichTable As FacilityDataTable)
        FacilitySummaryDataSet.Tables.Add(whichTable.ToString)
    End Sub

    Private Sub ReloadAllData()
        ClearAllData()

        If _airsNumber Is Nothing Then
            AirsNumberEntry.Focus()
        Else
            LoadBasicFacilityAndHeaderData()
            FSMainTabControl.Focus()
        End If
    End Sub

#End Region

#Region " Clear all data "

    Private Sub ClearAllData()
        ThisFacility = Nothing
        FacilitySummaryDataSet.Clear()
        DisableFacilityTools()
        ClearBasicFacilityData()
    End Sub

    Private Sub DisableFacilityTools()
        FSMainTabControl.Enabled = False
        UpdateEpaMenuItem.Enabled = False
        PrintFacilitySummaryMenuItem.Enabled = False

        FSMainTabControl.SelectedTab = FSInfo
        ContactsTabControl.SelectedTab = TPStateContacts
        TestingTabControl.SelectedTab = TPTestReport
        ComplianceTabControl.SelectedTab = TPComplianceWork
        PermittingTabControl.SelectedTab = TPAppTrackingLog
        EmissionsFeesTabControl.SelectedTab = TPEmissionsAnnual
        EiTabControl.SelectedTab = TPEiPost2009
    End Sub

    Private Sub EnableFacilityTools()
        FSMainTabControl.Enabled = True
        UpdateEpaMenuItem.Enabled = True
        PrintFacilitySummaryMenuItem.Enabled = True
    End Sub

#End Region

#Region " Basic Info data "

    Private Sub EditFacilityLocationButton_Click(sender As Object, e As EventArgs) Handles EditFacilityLocationButton.Click
        OpenMultiForm(IAIPEditFacilityLocation, AirsNumber.ToInt, New Dictionary(Of FormParameter, String) From {{FormParameter.AirsNumber, AirsNumber.ToString}})
    End Sub

    Private Sub ClearBasicFacilityData()

        'Navigation Panel
        AirsNumberEntry.BackColor = Color.Empty
        FacilityNameDisplay.Text = ""

        'Location
        LocationDisplay.Text = ""
        MapAddressLink.Enabled = False
        CountyDisplay.Text = ""
        MapCountyLink.Enabled = False
        LatLonDisplay.Text = ""
        MapLatLonLink.Enabled = False
        MapPictureBox.Visible = False

        'Description
        InfoDescDisplay.Text = ""
        EpaDateDisplay.Text = ""

        'Status
        InfoClassDisplay.Text = ""
        InfoOperStatusDisplay.Text = ""
        CmsDisplay.Text = ""
        CmsDisplay.BackColor = Color.Empty
        ComplianceStatusDisplay.Text = ""
        ComplianceStatusDisplay.BackColor = SystemColors.ControlLightLight
        ComplianceStatusDisplay.BorderStyle = BorderStyle.None

        'Offices
        DistrictOfficeDisplay.Text = ""
        ResponsibleOfficeDisplay.Text = ""

        'Facility Dates
        InfoStartupDateDisplay.Text = ""
        InfoPermitRevocationDateDisplay.Text = ""
        CreatedDateDisplay.Text = ""

        'Data Dates
        FisDateDisplay.Text = ""
        EpaFacilityIdDisplay.Text = ""
        DataUpdateDateDisplay.Text = ""

    End Sub

    Private Sub LoadBasicFacilityAndHeaderData()
        ThisFacility = GetFacility(AirsNumber)

        If ThisFacility Is Nothing Then
            FacilityNameDisplay.Text = "Facility does not exist"
            AirsNumberEntry.BackColor = Color.Bisque
            AirsNumberEntry.Focus()
        Else
            EnableFacilityTools()
            ThisFacility.RetrieveHeaderData()
            DisplayBasicFacilityData()
            DisplayHeaderData()
        End If
    End Sub

    Private Sub DisplayBasicFacilityData()

        DisplayMap()

        'Navigation Panel
        AirsNumberEntry.Text = AirsNumber.FormattedString

        With ThisFacility

            FacilityNameDisplay.Text = .FacilityName
            FacilityApprovalLinkLabel.Visible = Not .ApprovedByApb

            With .FacilityLocation
                'Location
                LocationDisplay.Text = .Address.ToString
                If .Address IsNot Nothing Then
                    MapAddressLink.Enabled = True
                End If
                CountyDisplay.Text = .County.ToString
                If .County IsNot Nothing Then
                    MapCountyLink.Enabled = True
                End If
                LatLonDisplay.Text = .Latitude.ToString &
                    ", " &
                    .Longitude.ToString
                If .Latitude.HasValue AndAlso .Longitude.HasValue Then
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
            Dim enforcementCount As Integer = Sscp.GetOpenEnforcementCountForFacility(AirsNumber)
            If enforcementCount = 0 Then
                ComplianceStatusDisplay.Text = "No open enforcement cases"
            ElseIf enforcementCount = 1 Then
                ComplianceStatusDisplay.Text = "One open enforcement case"
            ElseIf enforcementCount > 1 Then
                ComplianceStatusDisplay.Text = enforcementCount & " open enforcement cases"
            End If
            ColorCodeComplianceStatusDisplay(enforcementCount)

            'Offices
            DistrictOfficeDisplay.Text = .DistrictOfficeLocation
            ResponsibleOfficeDisplay.Text = If(.DistrictResponsible, "District Office", "Air Branch")

            EpaFacilityIdDisplay.Text = .AirsNumber.EpaFacilityIdentifier
        End With

        DataDates = Nothing
        SpinUpDataDatesBackgroundWorker()
    End Sub

    Private Sub SpinUpDataDatesBackgroundWorker()
        If bgw IsNot Nothing Then
            If bgw.IsBusy Then bgw.CancelAsync()
            bgw.Dispose()
        End If

        CreatedDateDisplay.Text = "..."
        FisDateDisplay.Text = "..."
        EpaDateDisplay.Text = "..."
        DataUpdateDateDisplay.Text = "..."

        bgw = New BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        AddHandler bgw.DoWork, AddressOf DataDatesBackgroundWorker_DoWork
        AddHandler bgw.RunWorkerCompleted, AddressOf DataDatesBackgroundWorker_RunWorkerCompleted
        bgw.RunWorkerAsync()
    End Sub

    Private Sub DataDatesBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim dt As DataRow = GetDataExchangeDates(AirsNumber)
        If Not CType(sender, BackgroundWorker).CancellationPending Then DataDates = dt
    End Sub

    Private Sub DataDatesBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If Not e.Cancelled Then DisplayDataDates()
    End Sub

    Private Sub DisplayDataDates()
        If DataDates IsNot Nothing Then
            CreatedDateDisplay.Text = String.Format(DateStringFormat, DataDates("DbRecordCreated"))
            FisDateDisplay.Text = String.Format(DateStringFormat, DataDates("FisExchangeDate"))
            EpaDateDisplay.Text = String.Format(DateStringFormat, DataDates("EpaExchangeDate"))
            DataUpdateDateDisplay.Text = String.Format(DateStringFormat, DataDates("DataModifiedOn"))
        Else
            CreatedDateDisplay.Text = Nothing
            FisDateDisplay.Text = Nothing
            EpaDateDisplay.Text = Nothing
            DataUpdateDateDisplay.Text = Nothing
        End If
    End Sub

    Private Sub ColorCodeComplianceStatusDisplay(num As Integer)
        With ComplianceStatusDisplay
            Select Case num
                Case > 0
                    .BackColor = IaipColors.WarningBackColor
                    .ForeColor = IaipColors.WarningForeColor
                    .BorderStyle = BorderStyle.None
                Case Else
                    .BackColor = SystemColors.ControlLightLight
                    .ForeColor = Color.Empty
                    .BorderStyle = BorderStyle.None
            End Select
        End With
    End Sub

    Private Sub ColorCodeCmsDisplay()
        With ThisFacility.HeaderData
            If (.CmsMember = FacilityCmsMember.A And .Classification <> FacilityClassification.A) OrElse
                (.CmsMember = FacilityCmsMember.S And .Classification <> FacilityClassification.SM) OrElse
                (.CmsMember = FacilityCmsMember.M And .Classification <> FacilityClassification.A) Then
                CmsDisplay.BackColor = IaipColors.WarningBackColor
                CmsDisplay.ForeColor = IaipColors.WarningForeColor
            Else
                CmsDisplay.BackColor = SystemColors.ControlLightLight
                CmsDisplay.ForeColor = Color.Empty
            End If
        End With
    End Sub

    Private Sub MapAddressLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MapAddressLink.LinkClicked
        OpenMapUrl(ThisFacility.FacilityLocation.Address.ToLinearString, Me)
    End Sub

    Private Sub MapCountyLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MapCountyLink.LinkClicked
        OpenMapUrl(ThisFacility.FacilityLocation.County & " County", Me)
    End Sub

    Private Sub MapLatLonLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MapLatLonLink.LinkClicked
        OpenMapUrl(ThisFacility.FacilityLocation.Latitude.ToString & "," &
                   ThisFacility.FacilityLocation.Longitude.ToString, Me)
    End Sub

    Private Sub DisplayMap()
        ' Blank map: https://maps.googleapis.com/maps/api/staticmap?key=AIzaSyAOMeyIrtZeEJb1Pci5jgtn_Uh3wr0NP14&size=230x280&zoom=6&center=32.9,-83.3&style=feature:all|element:labels|visibility:off&style=feature:road|visibility:off

        MapPictureBox.Visible = False

        Dim StaticMapsUrl As New Text.StringBuilder("https://maps.googleapis.com/maps/api/staticmap?")
        StaticMapsUrl.Append("key=" & GOOGLE_MAPS_API_KEY)
        StaticMapsUrl.Append("&size=" & MapPictureBox.Width.ToString & "x" & MapPictureBox.Height.ToString)
        StaticMapsUrl.Append("&zoom=6&center=32.9,-83.3")

        With ThisFacility.FacilityLocation
            If .Latitude.HasValue AndAlso .Longitude.HasValue Then
                StaticMapsUrl.Append("&markers=" & Math.Round(.Latitude.Value, 6).ToString & "," & Math.Round(.Longitude.Value, 6).ToString)
            ElseIf Not String.IsNullOrWhiteSpace(.Address.ToLinearString) Then
                StaticMapsUrl.Append("&markers=" & .Address.ToLinearString)
            Else
                Exit Sub
            End If
        End With

        MapPictureBox.Visible = True

        Console.WriteLine(StaticMapsUrl.ToString)
        Try
            MapPictureBox.LoadAsync(StaticMapsUrl.ToString)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region " Header data "

    Private Sub EditPollutantsButton_Click(sender As Object, e As EventArgs) Handles EditPollutantsButton.Click
        Using editProgPollDialog As New IAIPEditAirProgramPollutants
            With editProgPollDialog
                .AirsNumber = AirsNumber
                .FacilityName = ThisFacility.FacilityName & ", " & ThisFacility.DisplayCity
                .ShowDialog()
            End With
        End Using
    End Sub

    Private Sub EditSubpartsButton_Click(sender As Object, e As EventArgs) Handles EditSubpartsButton.Click
        Dim editSubParts As IAIPEditSubParts = CType(OpenMultiForm(IAIPEditSubParts, AirsNumber.ToInt), IAIPEditSubParts)
        editSubParts.AirsNumber = AirsNumber
    End Sub

    Private Sub EditHeaderDataButton_Click(sender As Object, e As EventArgs) Handles EditHeaderDataButton.Click
        If IsValidAirsNumberFormat(AirsNumber.ToString) Then

            Dim editHeaderDataDialog As New IAIPEditHeaderData With {
                .AirsNumber = AirsNumber,
                .FacilityName = ThisFacility.FacilityName
            }

            editHeaderDataDialog.ShowDialog()

            If editHeaderDataDialog.SomethingWasSaved Then
                ReloadAllData()
                FSMainTabControl.SelectedTab = FSHeaderData
                FSMainTabControl.Focus()
            End If

            editHeaderDataDialog.Dispose()
        Else
            MessageBox.Show("AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            AirsNumber = Nothing
        End If
    End Sub

    Private Sub DisplayHeaderData()
        With ThisFacility.HeaderData
            'Status
            HeaderClassDisplay.Text = .ClassificationDescription
            HeaderOperStatusDisplay.Text = .OperationalStatusDescription
            OwnershipDisplay.Text = .OwnershipType
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
                .Clear()
                If tempAP = AirProgram.None Then
                    .Add(AirProgram.None.GetDescription)
                Else
                    If CBool(tempAP And AirProgram.SIP) Then .Add(AirProgram.SIP.GetDescription)
                    If CBool(tempAP And AirProgram.FederalSIP) Then AirProgramsListBox.Items.Add(AirProgram.FederalSIP.GetDescription)
                    If CBool(tempAP And AirProgram.NonFederalSIP) Then AirProgramsListBox.Items.Add(AirProgram.NonFederalSIP.GetDescription)
                    If CBool(tempAP And AirProgram.CfcTracking) Then AirProgramsListBox.Items.Add(AirProgram.CfcTracking.GetDescription)
                    If CBool(tempAP And AirProgram.PSD) Then AirProgramsListBox.Items.Add(AirProgram.PSD.GetDescription)
                    If CBool(tempAP And AirProgram.NSR) Then AirProgramsListBox.Items.Add(AirProgram.NSR.GetDescription)
                    If CBool(tempAP And AirProgram.TitleV) Then AirProgramsListBox.Items.Add(AirProgram.TitleV.GetDescription)
                    If CBool(tempAP And AirProgram.MACT) Then AirProgramsListBox.Items.Add(AirProgram.MACT.GetDescription)
                    If CBool(tempAP And AirProgram.NESHAP) Then AirProgramsListBox.Items.Add(AirProgram.NESHAP.GetDescription)
                    If CBool(tempAP And AirProgram.NSPS) Then AirProgramsListBox.Items.Add(AirProgram.NSPS.GetDescription)
                    If CBool(tempAP And AirProgram.AcidPrecipitation) Then AirProgramsListBox.Items.Add(AirProgram.AcidPrecipitation.GetDescription)
                    If CBool(tempAP And AirProgram.FESOP) Then AirProgramsListBox.Items.Add(AirProgram.FESOP.GetDescription)
                    If CBool(tempAP And AirProgram.NativeAmerican) Then AirProgramsListBox.Items.Add(AirProgram.NativeAmerican.GetDescription)
                    If CBool(tempAP And AirProgram.RMP) Then AirProgramsListBox.Items.Add(AirProgram.RMP.GetDescription)
                End If
            End With

            'Buttons for Air Program Subparts
            EditSubpartsButton.Visible = CBool(.AirPrograms And
                (AirProgram.MACT Or AirProgram.NESHAP Or AirProgram.NSPS Or AirProgram.SIP))
            EditPollutantsButton.Enabled = True

            'Classifications
            ProgramClassificationsListBox.Items.Clear()
            If (.AirProgramClassifications = AirProgramClassification.None) Then
                ProgramClassificationsListBox.Items.Add(AirProgramClassification.None.GetDescription)
            Else
                If CBool(.AirProgramClassifications And AirProgramClassification.NsrMajor) Then
                    ProgramClassificationsListBox.Items.Add(AirProgramClassification.NsrMajor.GetDescription)
                End If
                If CBool(.AirProgramClassifications And AirProgramClassification.HapMajor) Then
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

#Region " Generic data table procedures "

    Private Sub LoadDataTable(whichTable As FacilityDataTable)
        If AirsNumber Is Nothing OrElse ThisFacility Is Nothing OrElse TableDataExists(whichTable) Then
            Exit Sub
        End If

        Dim table As DataTable = GetFSDataTable(whichTable, AirsNumber)

        If table IsNot Nothing AndAlso table.Rows.Count > 0 Then
            FacilitySummaryDataSet.Tables(whichTable.ToString).Merge(table, False, MissingSchemaAction.Add)
            SetUpData(whichTable)
        End If
    End Sub

    Private Function TableDataExists(whichTable As FacilityDataTable) As Boolean
        Return FacilitySummaryDataSet.Tables(whichTable.ToString).Rows.Count > 0
    End Function

    Private Sub SetUpData(table As FacilityDataTable)
        Select Case table

            ' Compliance
            Case FacilityDataTable.ComplianceWork
                SetUpDataGridSource(ComplianceWorkGrid, FacilityDataTable.ComplianceWork)

            Case FacilityDataTable.ComplianceFCE
                SetUpDataGridSource(ComplianceFceGrid, FacilityDataTable.ComplianceFCE)

            Case FacilityDataTable.ComplianceEnforcement
                SetUpDataGridSource(ComplianceEnforcementGrid, FacilityDataTable.ComplianceEnforcement)

                ' Contacts
            Case FacilityDataTable.ContactsState
                SetUpDataGridSource(ContactsStateGrid, FacilityDataTable.ContactsState)

            Case FacilityDataTable.ContactsWebSite
                SetUpDataGridSource(ContactsWebSiteGrid, FacilityDataTable.ContactsWebSite)

            Case FacilityDataTable.ContactsPermitting
                SetUpDataGridSource(ContactsPermittingGrid, FacilityDataTable.ContactsPermitting)

            Case FacilityDataTable.ContactsTesting
                SetUpDataGridSource(ContactsTestingGrid, FacilityDataTable.ContactsTesting)

            Case FacilityDataTable.ContactsCompliance
                SetUpDataGridSource(ContactsComplianceGrid, FacilityDataTable.ContactsCompliance)

            Case FacilityDataTable.ContactsGeco
                SetUpDataGridSource(ContactsGecoGrid, FacilityDataTable.ContactsGeco)

                ' Testing
            Case FacilityDataTable.TestReports
                SetUpDataGridSource(TestReportsGrid, FacilityDataTable.TestReports)

            Case FacilityDataTable.TestNotifications
                SetUpDataGridSource(TestNotificationsGrid, FacilityDataTable.TestNotifications)

            Case FacilityDataTable.TestMemos
                SetUpDataGridSource(TestMemosGrid, FacilityDataTable.TestMemos)

                ' Permitting
            Case FacilityDataTable.PermitApplications
                SetUpDataGridSource(PermitApplicationGrid, FacilityDataTable.PermitApplications)

            Case FacilityDataTable.PermitRuleHistory
                SetUpDataGridSource(PermitRuleHistoryGrid, FacilityDataTable.PermitRuleHistory)

            Case FacilityDataTable.PermitRules
                SetUpDataGridSource(PermitRulesGrid, FacilityDataTable.PermitRules)

            Case FacilityDataTable.Permits
                SetUpDataGridSource(PermitsGrid, FacilityDataTable.Permits)

            Case FacilityDataTable.PermitApplicationFees
                SetUpDataGridSource(PermitApplicationInvoicesGrid, FacilityDataTable.PermitApplicationFees)

                ' Emissions Fees
            Case FacilityDataTable.EmissionsFeesSummary
                SetUpFeesTab()

            Case FacilityDataTable.EmissionsFeesDeposits
                SetUpDataGridSource(FinancialDepositsGrid, FacilityDataTable.EmissionsFeesDeposits)

            Case FacilityDataTable.EmissionsFeesData
                SetUpDataGridSource(FinancialFeeGrid, FacilityDataTable.EmissionsFeesData)

            Case FacilityDataTable.EmissionsFeesInvoices
                SetUpDataGridSource(FinancialInvoicesGrid, FacilityDataTable.EmissionsFeesInvoices)

                ' Emission Inventory
            Case FacilityDataTable.EIPost2009
                SetUpDataGridSource(EiPost2009Grid, FacilityDataTable.EIPost2009)

            Case FacilityDataTable.EIPre2009
                SetUpDataGridSource(EiPre2009Grid, FacilityDataTable.EIPre2009)

        End Select
    End Sub

#End Region

#Region " AcceptButton "

    Private Sub AddAcceptButton(sender As Object, e As EventArgs) Handles AirsNumberEntry.Enter
        AcceptButton = ViewDataButton
    End Sub

    Private Sub RemoveAcceptButton(sender As Object, e As EventArgs) Handles AirsNumberEntry.Leave
        AcceptButton = Nothing
    End Sub

#End Region

#Region " Grid Item events "

    Private Sub OpenItem(dgv As IaipDataGridView, id As String)
        Select Case dgv.Name

            ' Compliance
            Case ComplianceEnforcementGrid.Name
                OpenFormEnforcement(id)
            Case ComplianceFceGrid.Name
                OpenFormFce(AirsNumber, id)
            Case ComplianceWorkGrid.Name
                OpenFormSscpWorkItem(id)

                ' Testing
            Case TestReportsGrid.Name
                OpenFormTestReport(id)
            Case TestNotificationsGrid.Name
                OpenFormTestNotification(id)
            Case TestMemosGrid.Name
                OpenFormTestMemo(id)

                ' Permitting
            Case PermitApplicationGrid.Name
                OpenFormPermitApplication(id)
            Case PermitApplicationInvoicesGrid.Name
                OpenInvoiceView(CInt(id))

        End Select
    End Sub

    Private Sub InitializeGridEvents()
        Dim GridsWithEvents As New List(Of IaipDataGridView) From {
            ComplianceEnforcementGrid,
            ComplianceFceGrid,
            ComplianceWorkGrid,
            TestReportsGrid,
            TestNotificationsGrid,
            TestMemosGrid,
            PermitApplicationGrid,
            PermitApplicationInvoicesGrid
        }

        For Each dgv As IaipDataGridView In GridsWithEvents
            dgv.LinkifyFirstColumn = True
            AddHandler dgv.CellLinkActivated, AddressOf HandleGrid_CellLinkActivated
        Next
    End Sub

    Private Sub HandleGrid_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs)
        Dim dgv As IaipDataGridView = CType(sender, IaipDataGridView)
        OpenItem(dgv, e.LinkValue.ToString)
    End Sub

#End Region

#Region " Data Sources "

    Private Sub SetUpDataGridSource(dgv As IaipDataGridView, table As FacilityDataTable)
        If dgv.DataSource Is Nothing Then
            dgv.DataSource = FacilitySummaryDataSet.Tables(table.ToString)
        End If
    End Sub

    Private Sub LoadComplianceData()
        LoadDataTable(FacilityDataTable.ComplianceWork)
        LoadDataTable(FacilityDataTable.ComplianceFCE)
        LoadDataTable(FacilityDataTable.ComplianceEnforcement)
    End Sub

    Private Sub LoadContactsData()
        LoadDataTable(FacilityDataTable.ContactsState)
        LoadDataTable(FacilityDataTable.ContactsWebSite)
        LoadDataTable(FacilityDataTable.ContactsPermitting)
        LoadDataTable(FacilityDataTable.ContactsTesting)
        LoadDataTable(FacilityDataTable.ContactsCompliance)
        LoadDataTable(FacilityDataTable.ContactsGeco)
    End Sub

    Private Sub LoadEmissionInventoryData()
        LoadDataTable(FacilityDataTable.EIPost2009)
        LoadDataTable(FacilityDataTable.EIPre2009)
    End Sub

    Private Sub LoadEmissionsFeesData()
        LoadDataTable(FacilityDataTable.EmissionsFeesSummary)
        LoadDataTable(FacilityDataTable.EmissionsFeesDeposits)
        LoadDataTable(FacilityDataTable.EmissionsFeesData)
        LoadDataTable(FacilityDataTable.EmissionsFeesInvoices)
    End Sub

    Private Sub LoadPermittingData()
        LoadDataTable(FacilityDataTable.PermitApplications)
        LoadDataTable(FacilityDataTable.PermitApplicationFees)
        LoadDataTable(FacilityDataTable.PermitRuleHistory)
        LoadDataTable(FacilityDataTable.PermitRules)
        LoadDataTable(FacilityDataTable.Permits)
    End Sub

    Private Sub LoadTestingData()
        LoadDataTable(FacilityDataTable.TestReports)
        LoadDataTable(FacilityDataTable.TestNotifications)
        LoadDataTable(FacilityDataTable.TestMemos)
    End Sub

    Private Sub SetUpFeesTab()
        If FeeYearSelect.DataSource Is Nothing Then
            With FeeYearSelect
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString)
                .DisplayMember = "FeesData"
                .ValueMember = "intYear"
                .SelectedIndex = 0
            End With

            Dim textBoxDataBindings As New Dictionary(Of TextBox, String) From {
                {FeeFacilityClassDisplay, "strClass"},
                {FeeDateSubmitDisplay, "DateSubmit"},
                {FeeStatusDisplay, "strIAIPDesc"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value))
            Next

            Dim textBoxDataBindingsTons As New Dictionary(Of TextBox, String) From {
                {FeeVocDisplay, "intVOCTons"},
                {FeePmDisplay, "intPMTons"},
                {FeeSO2Display, "intSO2Tons"},
                {FeeNOxDisplay, "intNOXtons"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsTons
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatTons
                item.Key.DataBindings.Add(b)
            Next

            Dim textBoxDataBindingsDollars As New Dictionary(Of TextBox, String) From {
                {FeeTotalDisplay, "NumTotalFee"},
                {FeePaidDisplay, "TotalPaid"},
                {FeePart70Display, "NumPart70Fee"},
                {FeeSmDisplay, "NumSMFee"},
                {FeeNspsDisplay, "NumNSPSFee"},
                {FeeAdminDisplay, "NumAdminFee"},
                {FeePollutantTotalDisplay, "numCalculatedFee"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsDollars
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatDollars
                item.Key.DataBindings.Add(b)
            Next

            Dim binding As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "NumFeeRate")
            AddHandler binding.Format, AddressOf BindingFormatDollarsPerTon
            FeeRateDisplay.DataBindings.Add(binding)

            Dim checkBoxDataBindings As New Dictionary(Of CheckBox, String) From {
                {FeeFacilityOperatingDisplay, "strOperate"},
                {FeeFacilityPart70Display, "strPart70"},
                {FeeFacilityNspsExemptDisplay, "strNSPSExempt"}
            }

            For Each item As KeyValuePair(Of CheckBox, String) In checkBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Checked", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value))
            Next

        End If
    End Sub

    Private Sub BindingFormatTons(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value.ToString, num) Then
            cevent.Value = num.ToString("N0") & " ton"
            If num <> 1 Then cevent.Value = cevent.Value.ToString & "s"
        End If
    End Sub

    Private Sub BindingFormatDollars(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value.ToString, num) Then
            cevent.Value = "$" & num.ToString("N0")
        End If
    End Sub

    Private Sub BindingFormatDollarsPerTon(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value.ToString, num) Then
            cevent.Value = "$" & num.ToString("N2") & " /ton"
        End If
    End Sub

    Private Sub EditContactsButton_Click(sender As Object, e As EventArgs) Handles EditContactsButton.Click
        Dim parameters As New Dictionary(Of FormParameter, String)
        parameters(FormParameter.AirsNumber) = AirsNumber.ShortString
        parameters(FormParameter.FacilityName) = ThisFacility.FacilityName
        OpenMultiForm(IAIPEditContacts, AirsNumber.ToInt, parameters)
    End Sub

    Private Sub PermitsLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles PermitsLink.LinkClicked
        OpenPermitSearchUrl(AirsNumber, Me)
    End Sub

#End Region

#Region " ICIS-Air Update "

    Private Sub UpdateEpaData()
        If ThisFacility IsNot Nothing Then
            If TriggerDataUpdateAtEPA(AirsNumber) Then
                MessageBox.Show("Data for this facility will be sent to EPA the next time the database update procedures run.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("There was an error attempting to flag this facility to update. Contact EPD IT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("The AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

#End Region

#Region " Navigation Panel "

    Private Sub FacilityApprovalLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles FacilityApprovalLinkLabel.LinkClicked
        OpenSingleForm(IAIPFacilityCreator)
    End Sub

    Private Sub ViewDataButton_Click(sender As Object, e As EventArgs) Handles ViewDataButton.Click
        If AirsNumberEntry.Text = "" Then
            ClearAllData()
        Else
            Try
                AirsNumber = CType(AirsNumberEntry.Text, ApbFacilityId)
            Catch ex As ArgumentException
                ClearAllData()
                FacilityNameDisplay.Text = "Invalid AIRS number"
                AirsNumberEntry.BackColor = Color.Bisque
                AirsNumberEntry.Focus()
            End Try
        End If
    End Sub

    Private Sub FacilitySearchButton_Click(sender As Object, e As EventArgs) Handles FacilitySearchButton.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub OpenFacilityLookupTool()
        Using facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()

            If facilityLookupDialog.DialogResult = DialogResult.OK AndAlso
                IsValidAirsNumberFormat(facilityLookupDialog.SelectedAirsNumber) Then
                AirsNumber = CType(facilityLookupDialog.SelectedAirsNumber, ApbFacilityId)
            End If
        End Using
    End Sub

#End Region

#Region " Menu Strip "

    Private Sub LookUpFacilityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LookUpFacilityMenuItem.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub PrintFacilitySummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintFacilitySummaryMenuItem.Click
        OpenFacilitySummaryPrintTool()
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearFormMenuItem.Click
        AirsNumber = Nothing
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMenuItem.Click
        Close()
    End Sub

    Private Sub FacilityCreatorToolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateFacilityMenuItem.Click
        OpenSingleForm(IAIPFacilityCreator)
    End Sub

    Private Sub UpdateAllDataSentToEPAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateEpaMenuItem.Click
        UpdateEpaData()
    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub

#End Region

#Region " Form-level events "

    Private Sub FSMainTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FSMainTabControl.SelectedIndexChanged
        Cursor = Cursors.WaitCursor

        Select Case FSMainTabControl.SelectedTab.Name
            Case FSCompliance.Name
                LoadComplianceData()
            Case FSContacts.Name
                LoadContactsData()
            Case FSEmissionInventory.Name
                LoadEmissionInventoryData()
            Case FSEmissionsFees.Name
                LoadEmissionsFeesData()
            Case FSPermitting.Name
                LoadPermittingData()
            Case FSTesting.Name
                LoadTestingData()
        End Select

        Cursor = Cursors.Default
    End Sub

    Private Sub IAIPFacilitySummary_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.A AndAlso e.Alt Then
            AirsNumberEntry.Focus()
        End If
    End Sub

    Private Sub DisplayEmptyTextBoxAsNA(sender As Object, e As EventArgs) _
    Handles InfoDescDisplay.TextChanged, LocationDisplay.TextChanged, LatLonDisplay.TextChanged,
        InfoDescDisplay.TextChanged, InfoClassDisplay.TextChanged, InfoOperStatusDisplay.TextChanged,
        CmsDisplay.TextChanged, ComplianceStatusDisplay.TextChanged, DistrictOfficeDisplay.TextChanged,
        ResponsibleOfficeDisplay.TextChanged, InfoStartupDateDisplay.TextChanged,
        InfoPermitRevocationDateDisplay.TextChanged, CreatedDateDisplay.TextChanged, FisDateDisplay.TextChanged,
        EpaDateDisplay.TextChanged, DataUpdateDateDisplay.TextChanged,
        HeaderClassDisplay.TextChanged, HeaderOperStatusDisplay.TextChanged, SicDisplay.TextChanged,
        NaicsDisplay.TextChanged, RmpIdDisplay.TextChanged, HeaderStartupDisplay.TextChanged,
        HeaderRevocationDateDisplay.TextChanged, HeaderDescDisplay.TextChanged, EpaFacilityIdDisplay.TextChanged,
        OwnershipDisplay.TextChanged

        Dim t As TextBox = CType(sender, TextBox)

        If t.Text = "" Then
            t.Text = "N/A"
        End If
    End Sub

    Private Sub OpenFacilitySummaryPrintTool()
        Dim facilityPrintOut As New IaipFacilitySummaryPrint With {
            .AirsNumber = AirsNumber,
            .FacilityName = ThisFacility.FacilityName
        }

        If facilityPrintOut IsNot Nothing AndAlso Not facilityPrintOut.IsDisposed Then
            facilityPrintOut.Show()
        End If
    End Sub

#End Region

End Class