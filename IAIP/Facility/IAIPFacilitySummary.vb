Imports System.Collections.Generic
Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports Iaip.DAL.FacilitySummaryData
Imports EpdIt
Imports System.ComponentModel

Public Class IAIPFacilitySummary

#Region " Properties and fields "

    Private _airsNumber As ApbFacilityId = Nothing
    Public Property AirsNumber() As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As ApbFacilityId)
            If _airsNumber Is Nothing AndAlso value Is Nothing Then Return
            'If _airsNumber IsNot Nothing AndAlso _airsNumber.Equals(value) Then Return
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
        Fees
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
        FinancialFees
        FinancialInvoices
        FinancialDeposits
        EiPost2009
        EiPre2009
    End Enum

#End Region

#Region " Form Load "

    Private Sub IAIPFacilitySummary_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadPermissions()
        InitializeDataTables()
        InitializeGridEvents()
    End Sub

    Private Sub IAIPFacilitySummary_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Me.AirsNumber Is Nothing Then
            ClearAllData()
            AirsNumberEntry.Focus()
        End If
    End Sub

    Private Sub LoadPermissions()
        ' TODO DWW: Better permissions definition

        ' Menu items
        UpdateEpaMenuItem.Available = (CurrentUser.UserID = "1" Or CurrentUser.UserID = "345")
        CreateFacilityMenuItem.Available = (AccountFormAccess(138, 0) IsNot Nothing _
                                          AndAlso AccountFormAccess(138, 0) = "138" _
                                          AndAlso (AccountFormAccess(138, 1) = "1" _
                                                   Or AccountFormAccess(138, 2) = "1" _
                                                   Or AccountFormAccess(138, 3) = "1" _
                                                   Or AccountFormAccess(138, 4) = "1"))
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
        AddDataTable(FacilityDataTable.Fees)
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
        AddDataTable(FacilityDataTable.FinancialDeposits)
        AddDataTable(FacilityDataTable.FinancialFees)
        AddDataTable(FacilityDataTable.FinancialInvoices)
        AddDataTable(FacilityDataTable.EiPost2009)
        AddDataTable(FacilityDataTable.EiPre2009)
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

        ' Don't track tab index changes when clearing form
        RemoveHandler FSMainTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler ContactsTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler TestingTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler PermittingTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler ComplianceTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler FinancialTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        RemoveHandler EiTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged

        FSMainTabControl.SelectedTab = FSInfo
        ContactsTabControl.SelectedTab = TPStateContacts
        TestingTabControl.SelectedTab = TPTestReport
        ComplianceTabControl.SelectedTab = TPComplianceWork
        PermittingTabControl.SelectedTab = TPAppLog
        FinancialTabControl.SelectedTab = TPFeeData
        EiTabControl.SelectedTab = TPEiPost2009

        AddHandler FSMainTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler ContactsTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler TestingTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler PermittingTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler ComplianceTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler FinancialTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler EiTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
    End Sub

    Private Sub EnableFacilityTools()
        FSMainTabControl.Enabled = True
        UpdateEpaMenuItem.Enabled = True
        PrintFacilitySummaryMenuItem.Enabled = True
    End Sub

#End Region

#Region " Basic Info data "

    Private Sub EditFacilityLocationButton_Click(sender As Object, e As EventArgs) Handles EditFacilityLocationButton.Click
        Dim parameters As New Dictionary(Of FormParameter, String) From {{FormParameter.AirsNumber, Me.AirsNumber.ToString}}
        OpenMultiForm(IAIPEditFacilityLocation, Me.AirsNumber.GetHashCode, parameters)
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
        ThisFacility = DAL.FacilityData.GetFacility(Me.AirsNumber)

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
        AirsNumberEntry.Text = Me.AirsNumber.FormattedString

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

        bgw = New BackgroundWorker
        bgw.WorkerSupportsCancellation = True
        AddHandler bgw.DoWork, AddressOf DataDatesBackgroundWorker_DoWork
        AddHandler bgw.RunWorkerCompleted, AddressOf DataDatesBackgroundWorker_RunWorkerCompleted
        bgw.RunWorkerAsync()
    End Sub

    Private Sub DataDatesBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim dt As DataRow = DAL.GetDataExchangeDates(Me.AirsNumber)
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
            If (.CmsMember = FacilityCmsMember.A And .Classification <> FacilityClassification.A) _
            OrElse (.CmsMember = FacilityCmsMember.S And .Classification <> FacilityClassification.SM) _
            OrElse (.CmsMember = FacilityCmsMember.M And .Classification <> FacilityClassification.A) Then
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
        Dim editSubParts As IAIPEditSubParts = OpenMultiForm(IAIPEditSubParts, Me.AirsNumber.GetHashCode)
        editSubParts.AirsNumber = AirsNumber
    End Sub

    Private Sub EditHeaderDataButton_Click(sender As Object, e As EventArgs) Handles EditHeaderDataButton.Click
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(Me.AirsNumber.ToString) Then

            Dim editHeaderDataDialog As New IAIPEditHeaderData
            editHeaderDataDialog.AirsNumber = Me.AirsNumber.ToString
            editHeaderDataDialog.FacilityName = Me.ThisFacility.FacilityName

            editHeaderDataDialog.ShowDialog()

            If editHeaderDataDialog.SomethingWasSaved Then
                ReloadAllData()
                FSMainTabControl.SelectedTab = FSHeaderData
                FSMainTabControl.Focus()
            End If

            editHeaderDataDialog.Dispose()
        Else
            MessageBox.Show("AIRS number is not valid.", "Invalid AIRS number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.AirsNumber = Nothing
        End If
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
                .Clear()
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
            ProgramClassificationsListBox.Items.Clear()
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

#Region " Generic data table procedures "

    Private Sub LoadDataTable(whichTable As FacilityDataTable)
        If Me.AirsNumber Is Nothing OrElse Me.ThisFacility Is Nothing Then Exit Sub
        If TableDataExists(whichTable) Then Exit Sub

        Dim table As DataTable = GetFSDataTable(whichTable, Me.AirsNumber)
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
                SetUpComplianceWorkGrid()
            Case FacilityDataTable.ComplianceFCE
                SetUpComplianceFceGrid()
            Case FacilityDataTable.ComplianceEnforcement
                SetUpComplianceEnforcementGrid()

                ' Fees
            Case FacilityDataTable.Fees
                SetUpFeesTab()

                ' Contacts
            Case FacilityDataTable.ContactsState
                SetUpContactsStateGrid()
            Case FacilityDataTable.ContactsWebSite
                SetUpContactsWebSiteGrid()
            Case FacilityDataTable.ContactsPermitting
                SetUpContactsPermittingGrid()
            Case FacilityDataTable.ContactsTesting
                SetUpContactsTestingGrid()
            Case FacilityDataTable.ContactsCompliance
                SetUpContactsComplianceGrid()
            Case FacilityDataTable.ContactsGeco
                SetUpContactsGecoGrid()

                ' Testing
            Case FacilityDataTable.TestReports
                SetUpTestReportsGrid()
            Case FacilityDataTable.TestNotifications
                SetUpTestNotificationsGrid()
            Case FacilityDataTable.TestMemos
                SetUpTestMemosGrid()

                ' Permitting
            Case FacilityDataTable.PermitApplications
                SetUpPermitApplicationsGrid()
            Case FacilityDataTable.PermitRuleHistory
                SetUpPermitRuleHistoryGrid()
            Case FacilityDataTable.PermitRules
                SetUpPermitRulesGrid()
            Case FacilityDataTable.Permits
                SetUpPermitsGrid()

                ' Financial
            Case FacilityDataTable.FinancialDeposits
                SetUpFinancialDepositsGrid()
            Case FacilityDataTable.FinancialFees
                SetUpFinancialFeesGrid()
            Case FacilityDataTable.FinancialInvoices
                SetUpFinancialInvoicesGrid()

                ' Emission Inventory
            Case FacilityDataTable.EiPost2009
                SetUpEiPost2009Grid()
            Case FacilityDataTable.EiPre2009
                SetUpEiPre2009Grid()

        End Select
    End Sub

#End Region

#Region " AcceptButton "

    Private Sub AddAcceptButton(sender As Object, e As EventArgs) _
    Handles AirsNumberEntry.Enter
        Me.AcceptButton = ViewDataButton
    End Sub

    Private Sub RemoveAcceptButton(sender As Object, e As EventArgs) _
    Handles AirsNumberEntry.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

#Region " Grid Item events "

    Private Sub OpenItem(dgv As DataGridView, id As String)
        Select Case dgv.Name

            ' Compliance
            Case ComplianceEnforcementGrid.Name
                OpenFormEnforcement(id)
            Case ComplianceFceGrid.Name
                OpenFormFce(Me.AirsNumber, id)
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

        End Select
    End Sub

    Private Sub InitializeGridEvents()
        Dim GridsWithEvents As New List(Of DataGridView)

        ' Compliance 
        GridsWithEvents.Add(ComplianceEnforcementGrid)
        GridsWithEvents.Add(ComplianceFceGrid)
        GridsWithEvents.Add(ComplianceWorkGrid)

        ' Testing
        GridsWithEvents.Add(TestReportsGrid)
        GridsWithEvents.Add(TestNotificationsGrid)
        GridsWithEvents.Add(TestMemosGrid)

        ' Permitting
        GridsWithEvents.Add(PermitApplicationGrid)

        For Each dgv As DataGridView In GridsWithEvents
            AddHandler dgv.CellClick, AddressOf HandleGrid_CellClick
            AddHandler dgv.CellDoubleClick, AddressOf HandleGrid_CellDoubleClick
            AddHandler dgv.KeyDown, AddressOf HandleGrid_KeyDown
            AddHandler dgv.KeyUp, AddressOf HandleGrid_KeyUp
            AddHandler dgv.CellMouseEnter, AddressOf HandleGrid_MouseEnter
            AddHandler dgv.CellMouseLeave, AddressOf HandleGrid_MouseLeave
        Next

    End Sub

    Private Sub HandleGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Only within the cell content of first column
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If e.RowIndex <> -1 And e.RowIndex < dgv.RowCount And e.ColumnIndex = 0 Then
            OpenItem(dgv, dgv.Rows(e.RowIndex).Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub HandleGrid_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        'Double-click within the cell content (but exclude first column to avoid double-firing)
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If e.RowIndex <> -1 And e.RowIndex < dgv.RowCount And e.ColumnIndex <> 0 Then
            OpenItem(dgv, dgv.Rows(e.RowIndex).Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub HandleGrid_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub HandleGrid_KeyUp(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim dgv As DataGridView = CType(sender, DataGridView)
            If dgv.RowCount > 0 Then
                OpenItem(dgv, dgv.CurrentRow.Cells(0).Value.ToString)
            End If
        End If
    End Sub

    Private Sub HandleGrid_MouseEnter(sender As Object, e As DataGridViewCellEventArgs)
        ' Change cursor and text color when hovering over first column (treats text like a hyperlink)
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If e.RowIndex <> -1 And e.RowIndex < dgv.RowCount And e.ColumnIndex = 0 Then
            dgv.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, True)
        End If
    End Sub

    Private Sub HandleGrid_MouseLeave(sender As Object, e As DataGridViewCellEventArgs)
        ' Reset cursor and text color when mouse leaves (un-hovers) a cell
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If e.RowIndex <> -1 And e.RowIndex < dgv.RowCount And e.ColumnIndex = 0 Then
            dgv.MakeCellLookLikeHoveredLink(e.RowIndex, e.ColumnIndex, False)
        End If
    End Sub

#End Region

#Region " Compliance data "

    Private Sub LoadComplianceData()
        LoadDataTable(FacilityDataTable.ComplianceWork)
        LoadDataTable(FacilityDataTable.ComplianceFCE)
        LoadDataTable(FacilityDataTable.ComplianceEnforcement)
    End Sub

    Private Sub SetUpComplianceWorkGrid()
        With ComplianceWorkGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ComplianceWork.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpComplianceEnforcementGrid()
        With ComplianceEnforcementGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ComplianceEnforcement.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpComplianceFceGrid()
        With ComplianceFceGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ComplianceFCE.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

#End Region

#Region " Fees data "

    Private Sub LoadFeesData()
        LoadDataTable(FacilityDataTable.Fees)
    End Sub

    Private Sub SetUpFeesTab()
        If FeeYearSelect.DataSource Is Nothing Then
            With FeeYearSelect
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString)
                .DisplayMember = "FeesData"
                .ValueMember = "intYear"
                .SelectedIndex = 0
            End With

            Dim textBoxDataBindings As New Dictionary(Of TextBox, String)
            textBoxDataBindings.Add(FeeFacilityClassDisplay, "strClass")
            textBoxDataBindings.Add(FeeDateSubmitDisplay, "DateSubmit")
            textBoxDataBindings.Add(FeeStatusDisplay, "strIAIPDesc")
            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString), item.Value))
            Next

            Dim textBoxDataBindingsTons As New Dictionary(Of TextBox, String)
            textBoxDataBindingsTons.Add(FeeVocDisplay, "intVOCTons")
            textBoxDataBindingsTons.Add(FeePmDisplay, "intPMTons")
            textBoxDataBindingsTons.Add(FeeSO2Display, "intSO2Tons")
            textBoxDataBindingsTons.Add(FeeNOxDisplay, "intNOXtons")
            For Each item As KeyValuePair(Of TextBox, String) In textBoxDataBindingsTons
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString), item.Value)
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
                Dim b As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString), item.Value)
                AddHandler b.Format, AddressOf BindingFormatDollars
                item.Key.DataBindings.Add(b)
            Next

            Dim binding As Binding = New Binding("Text", FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString), "NumFeeRate")
            AddHandler binding.Format, AddressOf BindingFormatDollarsPerTon
            FeeRateDisplay.DataBindings.Add(binding)

            Dim checkBoxDataBindings As New Dictionary(Of CheckBox, String)
            checkBoxDataBindings.Add(FeeFacilityOperatingDisplay, "strOperate")
            checkBoxDataBindings.Add(FeeFacilityPart70Display, "strPart70")
            checkBoxDataBindings.Add(FeeFacilityNspsExemptDisplay, "strNSPSExempt")
            For Each item As KeyValuePair(Of CheckBox, String) In checkBoxDataBindings
                item.Key.DataBindings.Add(New Binding("Checked", FacilitySummaryDataSet.Tables(FacilityDataTable.Fees.ToString), item.Value))
            Next

        End If
    End Sub

    Private Sub BindingFormatTons(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = num.ToString("N0") & " ton"
            If num <> 1 Then cevent.Value = cevent.Value & "s"
        End If
    End Sub

    Private Sub BindingFormatDollars(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = "$" & num.ToString("N0")
        End If
    End Sub

    Private Sub BindingFormatDollarsPerTon(sender As Object, cevent As ConvertEventArgs)
        Dim num As Decimal = 0
        cevent.Value = DBUtilities.GetNullable(Of String)(cevent.Value)

        If Decimal.TryParse(cevent.Value, num) Then
            cevent.Value = "$" & num.ToString("N2") & " /ton"
        End If
    End Sub

#End Region

#Region " Contacts data "

    Private Sub EditContactsButton_Click(sender As Object, e As EventArgs) Handles EditContactsButton.Click
        Dim parameters As New Dictionary(Of FormParameter, String)
        parameters(FormParameter.AirsNumber) = Me.AirsNumber.ShortString
        parameters(FormParameter.FacilityName) = Me.ThisFacility.FacilityName
        OpenMultiForm(IAIPEditContacts, Me.AirsNumber.ShortString, parameters)
    End Sub

    Private Sub LoadContactsData()
        LoadDataTable(FacilityDataTable.ContactsState)
        LoadDataTable(FacilityDataTable.ContactsWebSite)
        LoadDataTable(FacilityDataTable.ContactsPermitting)
        LoadDataTable(FacilityDataTable.ContactsTesting)
        LoadDataTable(FacilityDataTable.ContactsCompliance)
        LoadDataTable(FacilityDataTable.ContactsGeco)
    End Sub

    Private Sub SetUpContactsStateGrid()
        With ContactsStateGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsState.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

    Private Sub SetUpContactsWebSiteGrid()
        With ContactsWebSiteGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsWebSite.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

    Private Sub SetUpContactsPermittingGrid()
        With ContactsPermittingGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsPermitting.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

    Private Sub SetUpContactsTestingGrid()
        With ContactsTestingGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsTesting.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

    Private Sub SetUpContactsComplianceGrid()
        With ContactsComplianceGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsCompliance.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

    Private Sub SetUpContactsGecoGrid()
        With ContactsGecoGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.ContactsGeco.ToString)
                .SanelyResizeColumns
            End If
        End With
    End Sub

#End Region

#Region " Emission Inventory data "

    Private Sub LoadEmissionInventoryData()
        LoadDataTable(FacilityDataTable.EiPost2009)
        LoadDataTable(FacilityDataTable.EiPre2009)
    End Sub

    Private Sub SetUpEiPost2009Grid()
        With EiPost2009Grid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.EiPost2009.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpEiPre2009Grid()
        With EiPre2009Grid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.EiPre2009.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

#End Region

#Region " Financial "

    Private Sub LoadFinancialData()
        LoadDataTable(FacilityDataTable.FinancialDeposits)
        LoadDataTable(FacilityDataTable.FinancialFees)
        LoadDataTable(FacilityDataTable.FinancialInvoices)
    End Sub

    Private Sub SetUpFinancialFeesGrid()
        With FinancialFeeGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.FinancialFees.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpFinancialDepositsGrid()
        With FinancialDepositsGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.FinancialDeposits.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpFinancialInvoicesGrid()
        With FinancialInvoicesGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.FinancialInvoices.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

#End Region

#Region " Permitting data "

    Private Sub PermitsLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles PermitsLink.LinkClicked
        OpenPermitSearchUrl(Me.AirsNumber, Me)
    End Sub

    Private Sub LoadPermittingData()
        LoadDataTable(FacilityDataTable.PermitApplications)
        LoadDataTable(FacilityDataTable.PermitRuleHistory)
        LoadDataTable(FacilityDataTable.PermitRules)
        LoadDataTable(FacilityDataTable.Permits)
    End Sub

    Private Sub SetUpPermitApplicationsGrid()
        With PermitApplicationGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.PermitApplications.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpPermitRulesGrid()
        With PermitRulesGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.PermitRules.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpPermitRuleHistoryGrid()
        With PermitRuleHistoryGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.PermitRuleHistory.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpPermitsGrid()
        With PermitsGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.Permits.ToString)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

#End Region

#Region " Testing data "

    Private Sub LoadTestingData()
        LoadDataTable(FacilityDataTable.TestReports)
        LoadDataTable(FacilityDataTable.TestNotifications)
        LoadDataTable(FacilityDataTable.TestMemos)
    End Sub

    Private Sub SetUpTestReportsGrid()
        With TestReportsGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.TestReports.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With

    End Sub

    Private Sub SetUpTestNotificationsGrid()
        With TestNotificationsGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.TestNotifications.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

    Private Sub SetUpTestMemosGrid()
        With TestMemosGrid
            If .DataSource Is Nothing Then
                .DataSource = FacilitySummaryDataSet.Tables(FacilityDataTable.TestMemos.ToString)
                .MakeColumnsLookLikeLinks(0)
                .SanelyResizeColumns()
            End If
        End With
    End Sub

#End Region

#Region " ICIS-Air Update "

    Private Sub UpdateEpaData()
        If ThisFacility IsNot Nothing Then
            If DAL.FacilityData.TriggerDataUpdateAtEPA(Me.AirsNumber) Then
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

    Private Sub ViewData_Click(sender As Object, e As EventArgs) Handles ViewDataButton.Click
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

    Private Sub FacilitySearchButton_Click(sender As Object, e As EventArgs) Handles FacilitySearchButton.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub OpenFacilityLookupTool()
        Dim facilityLookupDialog As New IAIPFacilityLookUpTool
        facilityLookupDialog.ShowDialog()
        If facilityLookupDialog.DialogResult = DialogResult.OK _
        AndAlso Apb.ApbFacilityId.IsValidAirsNumberFormat(facilityLookupDialog.SelectedAirsNumber) Then
            Me.AirsNumber = facilityLookupDialog.SelectedAirsNumber
        End If
        facilityLookupDialog.Dispose()
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
        Me.AirsNumber = Nothing
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMenuItem.Click
        Me.Close()
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
        monitor.TrackFeature("FacilitySummaryTab." & FSMainTabControl.SelectedTab.Name)

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

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) _
    Handles ContactsTabControl.SelectedIndexChanged, TestingTabControl.SelectedIndexChanged,
    PermittingTabControl.SelectedIndexChanged, ComplianceTabControl.SelectedIndexChanged,
    FinancialTabControl.SelectedIndexChanged, EiTabControl.SelectedIndexChanged

        Dim tabcontrol As TabControl = CType(sender, TabControl)
        monitor.TrackFeature("FacilitySummaryTab." & tabcontrol.SelectedTab.Name)

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
        HeaderRevocationDateDisplay.TextChanged, HeaderDescDisplay.TextChanged, EpaFacilityIdDisplay.TextChanged

        Dim t As TextBox = CType(sender, TextBox)
        If t.Text = "" Then
            t.Text = "N/A"
        End If
    End Sub

    Private Sub OpenFacilitySummaryPrintTool()
        Dim facilityPrintOut As New IaipFacilitySummaryPrint
        facilityPrintOut.AirsNumber = Me.AirsNumber
        facilityPrintOut.FacilityName = Me.ThisFacility.FacilityName
        facilityPrintOut.Show()
    End Sub

#End Region

End Class