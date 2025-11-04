Imports System.Collections.Generic
Imports System.ComponentModel
Imports Microsoft.Data.SqlClient
Imports System.Linq
Imports GaEpd
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports Iaip.DAL
Imports Iaip.DAL.FacilitySummaryData
Imports Iaip.UrlHelpers

Public Class IAIPFacilitySummary

    ' " Properties and fields "

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
    Private ReadOnly Property AirsParam As SqlParameter
        Get
            Return New SqlParameter("@airsNumber", AirsNumber.DbFormattedString)
        End Get
    End Property
    Private Shared ReadOnly Property UserParam As SqlParameter
        Get
            Return New SqlParameter("@modifiedBy", CurrentUser.UserID)
        End Get
    End Property
    Private Shared ReadOnly Property LocationParam As SqlParameter
        Get
            Return New SqlParameter("@fromLocation", Convert.ToInt32(HeaderDataModificationLocation.FacilityColocationEditor))
        End Get
    End Property

    Private bgw As BackgroundWorker

    Friend Enum FacilityDataTable
        ColocatedFacilities
        ContactsStaff
        ContactsGecoFacility
        ContactsGecoEmails
        ContactsGecoUsers
        ContactsCaersUsers
        ContactsIaipFacility
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

    ' " Form Load "

    Protected Overrides Sub OnLoad(e As EventArgs)
        LoadPermissions()
        InitializeDataTables()
        InitializeGridEvents()
        DisableFacilityTools()

        MyBase.OnLoad(e)
    End Sub

    Private Sub LoadPermissions()
        ' Menu items
        UpdateEpaMenuItem.Available = CurrentUser.HasPermission(UserCan.ResetEpaIcisAirData)
        CreateFacilityMenuItem.Available = CurrentUser.HasPermission(UserCan.CreateFacility)
        ToolsMenuSeparator.Visible = CreateFacilityMenuItem.Available AndAlso UpdateEpaMenuItem.Available

        ' Edit location/header data/facility colocation
        EditFacilityLocationButton.Visible = CurrentUser.HasPermission(UserCan.EditFacilityAddress)
        AddColocatedFacility.Visible = CurrentUser.HasPermission(UserCan.EditFacilityColocationGroups)
        RemoveColocatedFacilities.Visible = CurrentUser.HasPermission(UserCan.EditFacilityColocationGroups)

        ' Delete Facility notes
        btnDeleteNote.Visible = CurrentUser.HasPermission(UserCan.DeleteFacilityNote)
    End Sub

    Private Sub InitializeDataTables()
        FacilitySummaryDataSet = New DataSet

        AddDataTable(FacilityDataTable.ColocatedFacilities)
        AddDataTable(FacilityDataTable.EmissionsFeesSummary)
        AddDataTable(FacilityDataTable.ContactsGecoFacility)
        AddDataTable(FacilityDataTable.ContactsGecoEmails)
        AddDataTable(FacilityDataTable.ContactsGecoUsers)
        AddDataTable(FacilityDataTable.ContactsCaersUsers)
        AddDataTable(FacilityDataTable.ContactsIaipFacility)
        AddDataTable(FacilityDataTable.ContactsStaff)
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
            AddBreadcrumb("Facility Summary: load facility", "AIRS #", AirsNumber.FormattedString, Me)
            LoadBasicFacilityAndHeaderData()
            FSMainTabControl.Focus()
        End If
    End Sub

    ' " Clear all data "

    Private Sub ClearAllData()
        ThisFacility = Nothing
        FacilitySummaryDataSet.Clear()
        FacilityNotes = New List(Of FacilityNote)
        DisableFacilityTools()
        ClearBasicFacilityData()
    End Sub

    Private Sub DisableFacilityTools()
        FSMainTabControl.Enabled = False
        UpdateEpaMenuItem.Enabled = False

        FSMainTabControl.SelectedTab = FSInfo
        ContactsTabControl.SelectedTab = TPContactsFacility
        GecoContactsTabControl.SelectedTab = TPGecoUsers
        TestingTabControl.SelectedTab = TPTestReport
        PermittingTabControl.SelectedTab = TPAppTrackingLog
        EmissionsFeesTabControl.SelectedTab = TPEmissionsAnnual
        EiTabControl.SelectedTab = TPEiPost2009
    End Sub

    Private Sub EnableFacilityTools()
        FSMainTabControl.Enabled = True
        UpdateEpaMenuItem.Enabled = True
    End Sub

    ' " Basic Info data "

    Private Sub EditFacilityLocationButton_Click(sender As Object, e As EventArgs) Handles EditFacilityLocationButton.Click
        If AirsNumber IsNot Nothing Then
            OpenMultiForm(IAIPEditFacilityLocation, AirsNumber.ToInt, New Dictionary(Of FormParameter, String) From {{FormParameter.AirsNumber, AirsNumber.ShortString}})
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
            LoadFacilityColocationTable()
        End If
    End Sub

    Private Sub DisplayBasicFacilityData()

        DisplayMap()

        'Navigation Panel
        AirsNumberEntry.Text = AirsNumber.FormattedString

        With ThisFacility

            FacilityNameDisplay.Text = .FacilityName

            If .ApprovedByApb Then
                FacilityApprovalLinkLabel.Visible = False
            Else
                FacilityApprovalLinkLabel.Visible = True
                FacilityApprovalLinkLabel.Text = "Facility not approved in the Facility Creator Tool"
                FacilityApprovalLinkLabel.LinkArea = New LinkArea(29, 63)
            End If

            If .DeactivatedByApb Then
                FacilityApprovalLinkLabel.Visible = True
                FacilityApprovalLinkLabel.Text = "Warning: Facility has been deactivated by APB"
                FacilityApprovalLinkLabel.LinkArea = New LinkArea(27, 63)
            End If

            If Not CurrentUser.HasPermission(UserCan.CreateFacility) Then
                FacilityApprovalLinkLabel.LinkArea = New LinkArea()
            End If

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

        bgw = New BackgroundWorker With {.WorkerSupportsCancellation = True}
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
        OpenGoogleMapUrl(ThisFacility.FacilityLocation.Address.ToLinearString, Me)
    End Sub

    Private Sub MapCountyLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MapCountyLink.LinkClicked
        OpenGoogleMapUrl(ThisFacility.FacilityLocation.County & " County", Me)
    End Sub

    Private Sub MapLatLonLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles MapLatLonLink.LinkClicked
        OpenGoogleMapUrl(ThisFacility.FacilityLocation.Latitude.ToString & "," &
                   ThisFacility.FacilityLocation.Longitude.ToString, Me)
    End Sub

    Private Sub DisplayMap()
        ' Blank map: https://maps.googleapis.com/maps/api/staticmap?key=AIzaSyAOMeyIrtZeEJb1Pci5jgtn_Uh3wr0NP14&size=230x280&zoom=6&center=32.9,-83.3&style=feature:all|element:labels|visibility:off&style=feature:road|visibility:off

        MapPictureBox.Visible = False

        Dim staticMapsUrl As New Text.StringBuilder("https://maps.googleapis.com/maps/api/staticmap?")
        staticMapsUrl.Append("key=" & CurrentAppConfig.GoogleMapsApiKey)
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

    ' " Header data "

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
        If AirsNumber Is Nothing OrElse Not AirsNumberExists(AirsNumber) Then
            MessageBox.Show("AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            AirsNumber = Nothing
        End If

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

    ' " Generic data table procedures "

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

            ' Facility
            Case FacilityDataTable.ColocatedFacilities
                SetUpDataGridSource(ColocatedFacilitiesGrid, table)
                SetUpColocatedFacilityUi()

            ' Contacts
            Case FacilityDataTable.ContactsStaff
                SetUpDataGridSource(ContactsStaffGrid, table)

            Case FacilityDataTable.ContactsIaipFacility
                SetUpDataGridSource(ContactsIaipFacilityGrid, table)

            ' GECO Contacts
            Case FacilityDataTable.ContactsGecoUsers
                SetUpDataGridSource(GecoUsersGrid, table)

            Case FacilityDataTable.ContactsGecoFacility
                SetUpDataGridSource(GecoContactsGrid, table)

            Case FacilityDataTable.ContactsGecoEmails
                SetUpDataGridSource(GecoEmailContactsGrid, table)

            Case FacilityDataTable.ContactsCaersUsers
                SetUpDataGridSource(GecoCaersUsersGrid, table)

            ' Testing
            Case FacilityDataTable.TestReports
                SetUpDataGridSource(TestReportsGrid, table)

            Case FacilityDataTable.TestNotifications
                SetUpDataGridSource(TestNotificationsGrid, table)

            Case FacilityDataTable.TestMemos
                SetUpDataGridSource(TestMemosGrid, table)

            ' Permitting
            Case FacilityDataTable.PermitApplications
                SetUpDataGridSource(PermitApplicationGrid, table)

            Case FacilityDataTable.PermitRuleHistory
                SetUpDataGridSource(PermitRuleHistoryGrid, table)

            Case FacilityDataTable.PermitRules
                SetUpDataGridSource(PermitRulesGrid, table)

            Case FacilityDataTable.Permits
                SetUpDataGridSource(PermitsGrid, table)

            Case FacilityDataTable.PermitApplicationFees
                SetUpDataGridSource(PermitApplicationInvoicesGrid, table)

            ' Emissions Fees
            Case FacilityDataTable.EmissionsFeesSummary
                SetUpFeesTab()
                SetUpDataGridSource(FinancialFeeGrid, table)

            Case FacilityDataTable.EmissionsFeesDeposits
                SetUpDataGridSource(FinancialDepositsGrid, table)

            Case FacilityDataTable.EmissionsFeesInvoices
                SetUpDataGridSource(FinancialInvoicesGrid, table)

            ' Emission Inventory
            Case FacilityDataTable.EIPost2009
                SetUpDataGridSource(EiPost2009Grid, table)

            Case FacilityDataTable.EIPre2009
                SetUpDataGridSource(EiPre2009Grid, table)

        End Select
    End Sub

    ' " AcceptButton "

    Private Sub AddAcceptButton(sender As Object, e As EventArgs) Handles AirsNumberEntry.Enter
        AcceptButton = ViewDataButton
    End Sub

    Private Sub RemoveAcceptButton(sender As Object, e As EventArgs) Handles AirsNumberEntry.Leave
        AcceptButton = Nothing
    End Sub

    ' " Grid Item events "

    Private Sub OpenItem(dgv As IaipDataGridView, itemId As String)
        Select Case dgv.Name

                ' Testing
            Case TestReportsGrid.Name
                OpenFormTestReport(itemId, Me)
            Case TestNotificationsGrid.Name
                OpenFormTestNotification(itemId)
            Case TestMemosGrid.Name
                OpenFormTestMemo(itemId)

                ' Permitting
            Case PermitApplicationGrid.Name
                OpenFormPermitApplication(itemId)
            Case PermitApplicationInvoicesGrid.Name
                OpenInvoiceView(CInt(itemId))

                ' Emissions Fees
            Case FinancialInvoicesGrid.Name
                OpenEmissionFeeInvoiceUrl(AirsNumber, CInt(itemId))

        End Select
    End Sub

    Private Sub InitializeGridEvents()
        Dim gridsWithEvents As New List(Of IaipDataGridView) From {
            TestReportsGrid,
            TestNotificationsGrid,
            TestMemosGrid,
            PermitApplicationGrid,
            PermitApplicationInvoicesGrid,
            FinancialInvoicesGrid
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

    ' " Data Sources "

    Private Sub SetUpDataGridSource(dgv As IaipDataGridView, table As FacilityDataTable)
        If dgv.DataSource Is Nothing Then
            dgv.DataSource = FacilitySummaryDataSet.Tables(table.ToString)
        End If
    End Sub

    Private Sub LoadFacilityColocationTable()
        LoadDataTable(FacilityDataTable.ColocatedFacilities)
    End Sub

    Private Sub LoadContactsData()
        LoadDataTable(FacilityDataTable.ContactsStaff)
        LoadDataTable(FacilityDataTable.ContactsIaipFacility)
    End Sub

    Private Sub LoadGecoContactsData()
        LoadDataTable(FacilityDataTable.ContactsGecoFacility)
        LoadDataTable(FacilityDataTable.ContactsGecoEmails)
        LoadDataTable(FacilityDataTable.ContactsGecoUsers)
        LoadDataTable(FacilityDataTable.ContactsCaersUsers)
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

            Dim feeDateBinding As New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "Date submitted")
            AddHandler feeDateBinding.Format, AddressOf BindingShortDate
            FeeDateSubmitDisplay.DataBindings.Add(feeDateBinding)

            Dim textBoxDataBindingsTons As New Dictionary(Of TextBox, String) From {
                {FeeVocDisplay, "VOC tons"},
                {FeePmDisplay, "PM tons"},
                {FeeSO2Display, "SO2 tons"},
                {FeeNOxDisplay, "NOx tons"}
            }

            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsTons
                Dim b As New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
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
                Dim b As New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatDollars
                item.Key.DataBindings.Add(b)
            Next

            Dim feeRateBinding As New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "Fee rate")
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

            Dim feeInvoiceButtonVisibleBinding As New Binding("Enabled", FacilitySummaryDataSet.Tables(FacilityDataTable.EmissionsFeesSummary.ToString), "Invoice generated")
            btnViewInvoice.DataBindings.Add(feeInvoiceButtonVisibleBinding)

        End If
    End Sub

    Private Sub btnViewInvoice_Click(sender As Object, e As EventArgs) Handles btnViewInvoice.Click
        OpenEmissionFeeInvoiceUrl(AirsNumber, FeeYearSelect.SelectedValue)
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

    ' " ICIS-Air Update "

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

    ' " Navigation Panel "

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
            If facilityLookupDialog.ShowDialog() = DialogResult.OK AndAlso
                facilityLookupDialog.SelectedAirsNumber IsNot Nothing Then
                AirsNumber = facilityLookupDialog.SelectedAirsNumber
            End If
        End Using
    End Sub

    ' " Menu Strip "

    Private Sub LookUpFacilityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LookUpFacilityMenuItem.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearFormMenuItem.Click
        AddBreadcrumb("Facility Summary: clear form", Me)
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

    ' " Form-level events "

    Private Sub FSMainTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FSMainTabControl.SelectedIndexChanged
        If AirsNumber Is Nothing Then Return

        Cursor = Cursors.WaitCursor

        Dim data As New Dictionary(Of String, Object) From {
            {"Name", Name},
            {"AIRS #", AirsNumber.FormattedString},
            {"Tab", FSMainTabControl.SelectedTab.Name}}
        AddBreadcrumb("Facility Summary: tab changed", data, Me)

        Select Case FSMainTabControl.SelectedTab.Name
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
            Case FSNotes.Name
                InitializeFacilityNotes()
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
        CmsDisplay.TextChanged, DistrictOfficeDisplay.TextChanged,
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

    ' Facility Notes

    Dim FacilityNotes As New List(Of FacilityNote)

    Private Sub InitializeFacilityNotes()
        If FacilityNotes Is Nothing OrElse FacilityNotes.Count = 0 Then LoadFacilityNotes()
    End Sub

    Private Sub LoadFacilityNotes()
        FacilityNotes = GetFacilityNotes(AirsNumber)
        DisplayFacilityNotes()
    End Sub

    Private Sub DisplayFacilityNotes()
        If chkShowArchivedNotes.Checked Then
            dgvFacilityNotes.DataSource = FacilityNotes
            dgvFacilityNotes.Columns("Archived").Visible = True
        Else
            dgvFacilityNotes.DataSource = FacilityNotes.Where(Function(n) Not n.Archived).ToList()
            dgvFacilityNotes.Columns("Archived").Visible = False
        End If

        dgvFacilityNotes.Columns("Id").Visible = False
        dgvFacilityNotes.Columns("FacilityId").Visible = False
        dgvFacilityNotes.Columns("FacilityId").Tag = "NotHidden"

        dgvFacilityNotes.SelectNone()

        CloseEditNotePanel()
    End Sub

    Private Sub chkShowArchivedNotes_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowArchivedNotes.CheckedChanged
        DisplayFacilityNotes()
    End Sub

    Private Sub btnCloseNoteView_Click(sender As Object, e As EventArgs) Handles btnCloseNoteView.Click
        CloseEditNotePanel()
    End Sub

    Private Sub CloseEditNotePanel()
        pnlEditNote.Visible = False
        pnlAddNote.Visible = True
        dgvFacilityNotes.SelectNone()
    End Sub

    Private Sub btnSaveNote_Click(sender As Object, e As EventArgs) Handles btnSaveNote.Click
        If txtNewNote.Text.Length = 0 Then
            MessageBox.Show("Note cannot be empty.", "No data saved")
            Return
        End If

        If SaveFacilityNote(AirsNumber, txtNewNote.Text) Then
            txtNewNote.Clear()
        Else
            MessageBox.Show("There was an error saving the note. Please try again.", "Error")
        End If

        LoadFacilityNotes()
    End Sub

    Private Sub btnArchiveNote_Click(sender As Object, e As EventArgs) Handles btnArchiveNote.Click
        If Not ArchiveFacilityNote(SelectedFacilityNoteId, btnArchiveNote.Text) Then
            MessageBox.Show("There was an error archiving the note. Please try again.", "Error")
        End If

        CloseEditNotePanel()
        LoadFacilityNotes()
    End Sub

    Private Sub btnDeleteNote_Click(sender As Object, e As EventArgs) Handles btnDeleteNote.Click
        If Not CurrentUser.HasPermission(UserCan.DeleteFacilityNote) Then Return

        If Not DeleteFacilityNote(SelectedFacilityNoteId) Then
            MessageBox.Show("There was an error deleting the note. Please try again.", "Error")
        End If

        CloseEditNotePanel()
        LoadFacilityNotes()
    End Sub

    Private Sub dgvFacilityNotes_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFacilityNotes.CellEnter
        If e.RowIndex <> -1 AndAlso e.RowIndex < dgvFacilityNotes.RowCount AndAlso dgvFacilityNotes.SelectedRows.Count = 1 Then
            DisplaySelectedFacilityNote(dgvFacilityNotes.Rows(e.RowIndex).Cells("Id").Value)
        End If
    End Sub

    Private SelectedFacilityNoteId As Guid
    Private Sub DisplaySelectedFacilityNote(id As Guid)
        Dim selectedNote As FacilityNote = FacilityNotes.SingleOrDefault(Function(n) n.Id = id)

        If selectedNote Is Nothing Then
            CloseEditNotePanel()
            Return
        End If

        Dim intro As String

        If selectedNote.Archived Then
            intro = "Archived note"
            btnArchiveNote.Text = "Unarchive"
        Else
            intro = "Note"
            btnArchiveNote.Text = "Archive"
        End If

        lblNoteLabel.Text = $"{intro} from {selectedNote.By.FullName} on {selectedNote.Dated.ToString(DateFormatReadable)}"
        txtDisplayNote.Text = selectedNote.Note
        SelectedFacilityNoteId = id

        OpenEditNotePanel()
    End Sub

    Private Sub OpenEditNotePanel()
        pnlEditNote.Visible = True
        pnlAddNote.Visible = False
    End Sub

    ' Co-located facilities

    Private Sub dgvColocatedFacilities_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) _
        Handles ColocatedFacilitiesGrid.CellLinkActivated
        OpenFormFacilitySummary(CStr(e.LinkValue))
    End Sub

    Private Sub ColocatedFacilitiesGrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
        Handles ColocatedFacilitiesGrid.CellFormatting

        If e IsNot Nothing AndAlso e.ColumnIndex = 0 AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
        End If
    End Sub

    Private Sub AddColocatedFacility_Click(sender As Object, e As EventArgs) Handles AddColocatedFacility.Click
        Dim newAirsParam As SqlParameter

        Using lookup As New IAIPFacilityLookUpTool
            If lookup.ShowDialog() <> DialogResult.OK OrElse
                    lookup.SelectedAirsNumber Is Nothing OrElse
                    lookup.SelectedAirsNumber = AirsNumber Then
                Return
            End If

            newAirsParam = New SqlParameter("@airsToAdd", lookup.SelectedAirsNumber.DbFormattedString)
        End Using

        If Not DB.SPRunCommand("iaip_facility.AddFacilityToColocation", {AirsParam, newAirsParam, UserParam, LocationParam}) Then
            MessageBox.Show("An unknown error occurred while trying to create the co-location group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ReloadFacilityColocationTable()
    End Sub

    Private Sub RemoveColocatedFacilities_Click(sender As Object, e As EventArgs) Handles RemoveColocatedFacilities.Click
        RemoveSelectedColocatedFacilities()
        ReloadFacilityColocationTable()
    End Sub

    Private Sub RemoveSelectedColocatedFacilities()
        For Each row As DataGridViewRow In ColocatedFacilitiesGrid.SelectedRows
            Dim removeAirsParam As New SqlParameter("@airsNumber", New ApbFacilityId(row.Cells(0).Value).DbFormattedString)

            If Not DB.SPRunCommand("iaip_facility.RemoveFacilityFromColocation", {removeAirsParam, UserParam, LocationParam}) Then
                MessageBox.Show("An unknown error occurred while trying to remove a co-located facility.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        Next
    End Sub

    Private Sub ReloadFacilityColocationTable()
        FacilitySummaryDataSet.Tables(FacilityDataTable.ColocatedFacilities.ToString).Clear()
        LoadFacilityColocationTable()
        SetUpColocatedFacilityUi()
    End Sub

    Private Sub SetUpColocatedFacilityUi()
        RemoveColocatedFacilities.Enabled = TableDataExists(FacilityDataTable.ColocatedFacilities)
        ColocatedFacilitiesGrid.SanelyResizeColumns(180)
    End Sub

    Private Sub ContactsTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ContactsTabControl.SelectedIndexChanged
        If AirsNumber Is Nothing OrElse ContactsTabControl.SelectedTab Is Nothing Then Return

        Cursor = Cursors.WaitCursor

        Dim data As New Dictionary(Of String, Object) From {
            {"Name", Name},
            {"AIRS #", AirsNumber.FormattedString},
            {"Tab", ContactsTabControl.SelectedTab.Name}}
        AddBreadcrumb("Facility Summary: tab changed", data, Me)

        If ContactsTabControl.SelectedTab.Name = TPContactsGeco.Name Then
            LoadGecoContactsData()
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub lnkWebFacility_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebFacility.LinkClicked
        If AirsNumber IsNot Nothing Then OpenFacilityOnWeb(AirsNumber, Me)
    End Sub

    Private Sub lnkWebComplianceWork_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebComplianceWork.LinkClicked
        If AirsNumber IsNot Nothing Then OpenComplianceWorkOnWeb(AirsNumber, Me)
    End Sub

    Private Sub lnkWebFce_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebFce.LinkClicked
        If AirsNumber IsNot Nothing Then OpenFceOnWeb(AirsNumber, Me)
    End Sub

    Private Sub lnkWebEnforcement_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWebEnforcement.LinkClicked
        If AirsNumber IsNot Nothing Then OpenEnforcementOnWeb(AirsNumber, Me)
    End Sub
End Class
