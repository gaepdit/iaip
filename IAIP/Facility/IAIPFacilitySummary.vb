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

    Private _airsNumber As ApbFacilityId
    Public Property AirsNumber As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set
            _airsNumber = Value
            ReloadAllData()
        End Set
    End Property

    Private Property ThisFacility As Facility
    Private Property FacilitySummaryDataSet As DataSet
    Private Property DataDates As DataRow
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
        DisableFacilityTools()

        MyBase.OnLoad(e)
    End Sub

    Private Sub LoadPermissions()
        ' Menu items
        UpdateEpaMenuItem.Available = CurrentUser.HasRole({19, 118})
        CreateFacilityMenuItem.Available = CurrentUser.HasPermission(UserCan.CreateFacility)
        ToolsMenuSeparator.Visible = (CreateFacilityMenuItem.Available AndAlso UpdateEpaMenuItem.Available)

        ' Edit location/header data
        If CurrentUser.UnitId = 0 OrElse AccountFormAccess(22, 3) = "1" OrElse AccountFormAccess(1, 3) = "1" Then
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
        If AirsNumber IsNot Nothing Then
            OpenMultiForm(IAIPEditFacilityLocation, AirsNumber.ToInt, New Dictionary(Of FormParameter, String) From {{FormParameter.AirsNumber, AirsNumber.ToString}})
        End If
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
                LatLonDisplay.Text = .Latitude.ToString & ", " & .Longitude.ToString
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
            Dim enforcementCount As Integer = DAL.Sscp.GetOpenEnforcementCountForFacility(AirsNumber)
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
            If (.CmsMember = FacilityCmsMember.A AndAlso .Classification <> FacilityClassification.A) OrElse
                (.CmsMember = FacilityCmsMember.S AndAlso .Classification <> FacilityClassification.SM) OrElse
                (.CmsMember = FacilityCmsMember.M AndAlso .Classification <> FacilityClassification.A) Then
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

        Dim staticMapsUrl As New Text.StringBuilder("https://maps.googleapis.com/maps/api/staticmap?")
        staticMapsUrl.Append("key=" & GOOGLE_MAPS_API_KEY)
        staticMapsUrl.Append("&size=" & MapPictureBox.Width.ToString & "x" & MapPictureBox.Height.ToString)
        staticMapsUrl.Append("&zoom=6&center=32.9,-83.3")

        With ThisFacility.FacilityLocation
            If .Latitude.HasValue AndAlso .Longitude.HasValue Then
                staticMapsUrl.Append("&markers=" & Math.Round(.Latitude.Value, 6).ToString & "," & Math.Round(.Longitude.Value, 6).ToString)
            ElseIf Not String.IsNullOrWhiteSpace(.Address.ToLinearString) Then
                staticMapsUrl.Append("&markers=" & .Address.ToLinearString)
            Else
                Return
            End If
        End With

        MapPictureBox.Visible = True

        Console.WriteLine(staticMapsUrl.ToString)
        Try
            MapPictureBox.LoadAsync(staticMapsUrl.ToString)
        Catch ex As Exception
            ' Log error but don't display error to user
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name, False)
        End Try
    End Sub

#End Region

#Region " Header data "

    Private Sub EditPollutantsButton_Click(sender As Object, e As EventArgs) Handles EditPollutantsButton.Click
        If AirsNumber IsNot Nothing Then
            Using editProgPollDialog As New IAIPEditAirProgramPollutants
                With editProgPollDialog
                    .AirsNumber = AirsNumber
                    .FacilityName = ThisFacility.FacilityName & ", " & ThisFacility.DisplayCity
                    .ShowDialog()
                End With
            End Using
        End If
    End Sub

    Private Sub EditSubpartsButton_Click(sender As Object, e As EventArgs) Handles EditSubpartsButton.Click
        If AirsNumber IsNot Nothing Then
            Dim editSubParts As IAIPEditSubParts = CType(OpenMultiForm(IAIPEditSubParts, AirsNumber.ToInt), IAIPEditSubParts)
            editSubParts.AirsNumber = AirsNumber
        End If
    End Sub

    Private Sub EditHeaderDataButton_Click(sender As Object, e As EventArgs) Handles EditHeaderDataButton.Click
        If AirsNumber IsNot Nothing Then

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
            Dim tempAp As AirPrograms = .AirPrograms
            With AirProgramsListBox.Items
                .Clear()
                If tempAp = AirPrograms.None Then
                    .Add(AirPrograms.None.GetDescription)
                Else
                    If CBool(tempAp And AirPrograms.SIP) Then .Add(AirPrograms.SIP.GetDescription)
                    If CBool(tempAp And AirPrograms.FederalSIP) Then AirProgramsListBox.Items.Add(AirPrograms.FederalSIP.GetDescription)
                    If CBool(tempAp And AirPrograms.NonFederalSIP) Then AirProgramsListBox.Items.Add(AirPrograms.NonFederalSIP.GetDescription)
                    If CBool(tempAp And AirPrograms.CfcTracking) Then AirProgramsListBox.Items.Add(AirPrograms.CfcTracking.GetDescription)
                    If CBool(tempAp And AirPrograms.PSD) Then AirProgramsListBox.Items.Add(AirPrograms.PSD.GetDescription)
                    If CBool(tempAp And AirPrograms.NSR) Then AirProgramsListBox.Items.Add(AirPrograms.NSR.GetDescription)
                    If CBool(tempAp And AirPrograms.TitleV) Then AirProgramsListBox.Items.Add(AirPrograms.TitleV.GetDescription)
                    If CBool(tempAp And AirPrograms.MACT) Then AirProgramsListBox.Items.Add(AirPrograms.MACT.GetDescription)
                    If CBool(tempAp And AirPrograms.NESHAP) Then AirProgramsListBox.Items.Add(AirPrograms.NESHAP.GetDescription)
                    If CBool(tempAp And AirPrograms.NSPS) Then AirProgramsListBox.Items.Add(AirPrograms.NSPS.GetDescription)
                    If CBool(tempAp And AirPrograms.AcidPrecipitation) Then AirProgramsListBox.Items.Add(AirPrograms.AcidPrecipitation.GetDescription)
                    If CBool(tempAp And AirPrograms.FESOP) Then AirProgramsListBox.Items.Add(AirPrograms.FESOP.GetDescription)
                    If CBool(tempAp And AirPrograms.NativeAmerican) Then AirProgramsListBox.Items.Add(AirPrograms.NativeAmerican.GetDescription)
                    If CBool(tempAp And AirPrograms.RMP) Then AirProgramsListBox.Items.Add(AirPrograms.RMP.GetDescription)
                End If
            End With

            'Buttons for Air Program Subparts
            EditSubpartsButton.Visible = CBool(.AirPrograms And
                (AirPrograms.MACT Or AirPrograms.NESHAP Or AirPrograms.NSPS Or AirPrograms.SIP))
            EditPollutantsButton.Enabled = True

            'Classifications
            ProgramClassificationsListBox.Items.Clear()
            If (.AirProgramClassifications = AirProgramClassifications.None) Then
                ProgramClassificationsListBox.Items.Add(AirProgramClassifications.None.GetDescription)
            Else
                If CBool(.AirProgramClassifications And AirProgramClassifications.NsrMajor) Then
                    ProgramClassificationsListBox.Items.Add(AirProgramClassifications.NsrMajor.GetDescription)
                End If
                If CBool(.AirProgramClassifications And AirProgramClassifications.HapMajor) Then
                    ProgramClassificationsListBox.Items.Add(AirProgramClassifications.HapMajor.GetDescription)
                End If
            End If

            'Other Header Data
            OtherHeaderDataListBox.Items.Clear()
            If .NspsFeeExempt Then
                OtherHeaderDataListBox.Items.Add(FacilityHeaderData.NspsFeeExemptDesc)
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
            Return
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
                SetUpDataGridSource(FinancialFeeGrid, FacilityDataTable.EmissionsFeesSummary)

            Case FacilityDataTable.EmissionsFeesDeposits
                SetUpDataGridSource(FinancialDepositsGrid, FacilityDataTable.EmissionsFeesDeposits)

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

    Private Sub OpenItem(dgv As IaipDataGridView, itemId As String)
        Select Case dgv.Name

            ' Compliance
            Case ComplianceEnforcementGrid.Name
                OpenFormEnforcement(itemId)
            Case ComplianceFceGrid.Name
                OpenFormFce(AirsNumber, itemId)
            Case ComplianceWorkGrid.Name
                OpenFormSscpWorkItem(itemId)

                ' Testing
            Case TestReportsGrid.Name
                OpenFormTestReport(itemId)
            Case TestNotificationsGrid.Name
                OpenFormTestNotification(itemId)
            Case TestMemosGrid.Name
                OpenFormTestMemo(itemId)

                ' Permitting
            Case PermitApplicationGrid.Name
                OpenFormPermitApplication(itemId)
            Case PermitApplicationInvoicesGrid.Name
                OpenInvoiceView(CInt(itemId))

        End Select
    End Sub

    Private Sub InitializeGridEvents()
        Dim gridsWithEvents As New List(Of IaipDataGridView) From {
            ComplianceEnforcementGrid,
            ComplianceFceGrid,
            ComplianceWorkGrid,
            TestReportsGrid,
            TestNotificationsGrid,
            TestMemosGrid,
            PermitApplicationGrid,
            PermitApplicationInvoicesGrid
        }

        For Each dgv As IaipDataGridView In gridsWithEvents
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
                .ValueMember = "Fee Year"
                .SelectedIndex = 0
            End With

            Dim textBoxDataBindings As New Dictionary(Of TextBox, String) From {
                {FeeFacilityClassDisplay, "Classification"},
                {FeeStatusDisplay, "Status"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value))
            Next

            Dim feeDateBinding As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "Date submitted")
            AddHandler feeDateBinding.Format, AddressOf BindingShortDate
            FeeDateSubmitDisplay.DataBindings.Add(feeDateBinding)

            Dim textBoxDataBindingsTons As New Dictionary(Of TextBox, String) From {
                {FeeVocDisplay, "VOC tons"},
                {FeePmDisplay, "PM tons"},
                {FeeSO2Display, "SO2 tons"},
                {FeeNOxDisplay, "NOx tons"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsTons
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatTons
                item.Key.DataBindings.Add(b)
            Next

            Dim textBoxDataBindingsDollars As New Dictionary(Of TextBox, String) From {
                {FeeTotalDisplay, "Total fee"},
                {FeePaidDisplay, "Total paid"},
                {FeePart70Display, "Part 70 fee"},
                {FeePart70MaintDisplay, "Part 70 maintenance fee"},
                {FeeSmDisplay, "SM fee"},
                {FeeNspsDisplay, "NSPS fee"},
                {FeeAdminDisplay, "Admin fee"},
                {FeePollutantTotalDisplay, "Calculated emission fee"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsDollars
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatDollars
                item.Key.DataBindings.Add(b)
            Next

            Dim feeRateBinding As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "Fee rate")
            AddHandler feeRateBinding.Format, AddressOf BindingFormatDollarsPerTon
            FeeRateDisplay.DataBindings.Add(feeRateBinding)

            Dim checkBoxDataBindings As New Dictionary(Of CheckBox, String) From {
                {FeeFacilityOperatingDisplay, "Operating"},
                {FeeFacilityPart70Display, "Part 70"},
                {FeeFacilityNspsDisplay, "NSPS"},
                {FeeFacilityNspsExemptDisplay, "NSPS fee exempt"}
            }

            For Each item As KeyValuePair(Of CheckBox, String) In checkBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Checked", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value, True))
            Next

        End If
    End Sub

    Private Sub BindingShortDate(sender As Object, e As ConvertEventArgs)
        Dim d As Date? = DBUtilities.GetNullableDateTime(e.Value)

        If d.HasValue Then
            e.Value = d.Value.ToString(DateFormat)
        Else
            e.Value = "Not Submitted"
        End If
    End Sub

    Private Sub BindingFormatTons(sender As Object, e As ConvertEventArgs)
        Dim num As Decimal = 0
        e.Value = DBUtilities.GetNullable(Of String)(e.Value)

        If Decimal.TryParse(e.Value.ToString, num) Then
            e.Value = num.ToString("N0") & " ton"
            If num <> 1 Then e.Value = e.Value.ToString & "s"
        End If
    End Sub

    Private Sub BindingFormatDollars(sender As Object, e As ConvertEventArgs)
        Dim num As Decimal = 0
        e.Value = DBUtilities.GetNullable(Of String)(e.Value)

        If Decimal.TryParse(e.Value.ToString, num) Then
            e.Value = "$" & num.ToString("N0")
        End If
    End Sub

    Private Sub BindingFormatDollarsPerTon(sender As Object, e As ConvertEventArgs)
        Dim num As Decimal = 0
        e.Value = DBUtilities.GetNullable(Of String)(e.Value)

        If Decimal.TryParse(e.Value.ToString, num) Then
            e.Value = "$" & num.ToString("N2") & "/ton"
        End If
    End Sub

    Private Sub EditContactsButton_Click(sender As Object, e As EventArgs) Handles EditContactsButton.Click
        If AirsNumber IsNot Nothing Then
            Dim params As New Dictionary(Of FormParameter, String)
            params(FormParameter.AirsNumber) = AirsNumber.ShortString
            params(FormParameter.FacilityName) = ThisFacility.FacilityName
            OpenMultiForm(IAIPEditContacts, AirsNumber.ToInt, params)
        End If
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
        If AirsNumber IsNot Nothing Then
            Dim facilityPrintOut As New IaipFacilitySummaryPrint With {
                .AirsNumber = AirsNumber,
                .FacilityName = ThisFacility.FacilityName
            }

            If facilityPrintOut IsNot Nothing AndAlso Not facilityPrintOut.IsDisposed Then
                facilityPrintOut.Show()
            End If
        End If
    End Sub

#End Region

    'Form overrides dispose to clean up the component list.
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                If FacilitySummaryDataSet IsNot Nothing Then FacilitySummaryDataSet.Dispose()
                If bgw IsNot Nothing Then bgw.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class