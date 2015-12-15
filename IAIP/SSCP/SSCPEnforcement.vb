Imports System.Collections.Generic
Imports System.Linq
Imports Iaip.Apb.Sscp
Imports Iaip.DAL.DocumentData
Imports Oracle.ManagedDataAccess.Client

Public Class SscpEnforcement

#Region " Properties and Fields "

    Public Property EnforcementId() As String
    Public Property EnforcementCase() As New EnforcementCase
    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property Facility As Apb.Facilities.Facility
    Public Property LinkedEventId As String

    Dim SscpStaff As DataTable
    Dim ViolationTypes As DataTable
    Private ExistingFiles As List(Of EnforcementDocument)

    Dim nullableDates As New Dictionary(Of Date?, DateTimePicker) From {
        {EnforcementCase.DateFinalized, ResolvedDate},
        {EnforcementCase.AoAppealed, AOAppealed},
        {EnforcementCase.AoExecuted, AOExecuted},
        {EnforcementCase.AoResolved, AOResolved},
        {EnforcementCase.CoExecuted, COExecuted},
        {EnforcementCase.CoProposed, COProposed},
        {EnforcementCase.CoReceivedFromCompany, COReceivedfromCompany},
        {EnforcementCase.CoReceivedFromDirector, COReceivedFromDirector},
        {EnforcementCase.CoResolved, COResolved},
        {EnforcementCase.CoToPm, COToPM},
        {EnforcementCase.CoToUc, COToUC},
        {EnforcementCase.DiscoveryDate, DiscoveryDate},
        {EnforcementCase.LonResolved, LonResolved},
        {EnforcementCase.LonSent, LonSent},
        {EnforcementCase.LonToUc, LonToUC},
        {EnforcementCase.NfaSent, NfaSent},
        {EnforcementCase.NfaToPm, NfaToPM},
        {EnforcementCase.NfaToUc, NfaToUC},
        {EnforcementCase.NovResponseReceived, NovResponseReceived},
        {EnforcementCase.NovSent, NovSent},
        {EnforcementCase.NovToPm, NovToPM},
        {EnforcementCase.NovToUc, NovToUC}
    }

    Dim SelectedStipulatedPenaltyItem As Int16 = 0

#End Region

#Region " Form load event "

    Private Sub SscpEnforcement_Load(sender As Object, e As EventArgs) Handles Me.Load
        monitor.TrackFeature("Forms." & Me.Name)

        ' Set up form/defaults/permissions
        GetLookupTables()
        LoadComboBoxes()
        SetUpForm()
        SetUserPermissions()

        ' Parse parameters load initial data
        ParseParameters()
        LoadCurrentEnforcement()
        LoadCurrentFacility()

        ' Display initial data
        DisplayFacility()
        DisplayEnforcementCase()
        DisplayLinkedEvent()

    End Sub

#End Region

#Region " Form setup "

    Private Sub GetLookupTables()
        SscpStaff = SharedData.GetTable(SharedData.Tables.AllComplianceStaff)
        ViolationTypes = SharedData.GetTable(SharedData.Tables.ViolationTypes)
    End Sub

    Private Sub LoadComboBoxes()
        LoadStaffComboBox()
    End Sub

    Private Sub LoadStaffComboBox()
        With StaffResponsible
            .DataSource = SscpStaff
            .DisplayMember = "Staff"
            .ValueMember = "numUserID"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub SetUpForm()
        ' Tabs
        EnforcementTabs.TabPages.Remove(LonTabPage)
        EnforcementTabs.TabPages.Remove(NovTabPage)
        EnforcementTabs.TabPages.Remove(COTabPage)
        EnforcementTabs.TabPages.Remove(AOTabPage)
        EnforcementTabs.TabPages.Remove(PollutantsTabPage)
        EnforcementTabs.TabPages.Remove(DocumentsTabPage)
        EnforcementTabs.TabPages.Remove(AuditHistoryTabPage)
        EnforcementTabs.TabPages.Remove(EpaValuesTabPage)

        ' Header
        StaffResponsible.SelectedValue = UserGCode

        ' Dates
        SetDtpMaxDates(Today, New List(Of DateTimePicker) From {
                       ResolvedDate, DiscoveryDate,
                       LonToUC, LonSent, LonResolved,
                       NovToUC, NovToPM, NovSent, NovResponseReceived,
                       NfaToUC, NfaToPM, NfaSent,
                       COToUC, COToPM, COProposed,
                       COReceivedfromCompany, COReceivedFromDirector,
                       COExecuted, COResolved,
                       AOExecuted, AOAppealed, AOResolved
                       })
    End Sub

    Private Sub SetDtpMaxDates(maxDate As DateTime, dateControls As List(Of DateTimePicker))
        For Each dateControl As DateTimePicker In dateControls
            dateControl.MaxDate = maxDate
        Next
    End Sub

    Private Sub SetUserPermissions()

        If UserAccounts.Contains("(19)") OrElse ' SSCP Program Manager
            UserAccounts.Contains("(114)") OrElse ' SSCP Unit Manager
            UserAccounts.Contains("(118)") Then ' DMU Management

            ' Enable full access to resolve/delete/submit to EPA
            ResolvedCheckBox.Enabled = True
            DeleteEnforcementMenuItem.Enabled = True
            SubmitToEpa.Enabled = True

        ElseIf Not (UserBranch = "5") AndAlso  ' (not) District offices
            Not (UserProgram = "3" Or UserProgram = "4") Then ' (not) DMU or SSCP

            ' Disable any save/write access
            SaveButton.Enabled = False
            SaveMenuItem.Enabled = False
            SubmitToUC.Enabled = False
            EditAirProgramPollutantsButton.Enabled = False
            StipulatedPenaltyControls.Enabled = False
            DocumentUpdateButton.Enabled = False
            LinkToEvent.Enabled = False
            ClearLinkedEvent.Enabled = False

        End If

    End Sub

#End Region

#Region " Load initial data "

    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey(FormParameter.EnforcementId) Then EnforcementId = Parameters(FormParameter.EnforcementId)
            If Parameters.ContainsKey(FormParameter.AirsNumber) Then AirsNumber = Parameters(FormParameter.AirsNumber)
            If Parameters.ContainsKey(FormParameter.TrackingNumber) Then LinkedEventId = Parameters(FormParameter.TrackingNumber)
        End If
    End Sub

    Private Sub LoadCurrentEnforcement()
        If EnforcementId Is Nothing Then Exit Sub
        EnforcementCase = DAL.Sscp.GetEnforcementCase(EnforcementId)
        If EnforcementCase Is Nothing Then
            MessageBox.Show("Invalid enforcement number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        AirsNumber = EnforcementCase.AirsNumber
        LinkedEventId = EnforcementCase.LinkedWorkItemId
        LoadStipulatedPenalties()
        LoadPollutants()
        LoadAirPrograms()
        LoadDocuments()
    End Sub

    Private Sub LoadCurrentFacility()
        Facility = DAL.GetFacility(AirsNumber)
        If Facility Is Nothing Then
            MessageBox.Show("Invalid AIRS number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub

#End Region

#Region " Display data "

    Private Sub DisplayFacility()
        AirsNumberDisplay.Text = AirsNumber.FormattedString

        FacilityNameDisplay.Text = Facility.FacilityName
        If Facility.FacilityLocation.Address.City IsNot Nothing Then
            FacilityNameDisplay.Text &= ", " & Facility.FacilityLocation.Address.City
        End If

        FacilityNotApprovedDisplay.Visible = Not Facility.ApprovedByApb
    End Sub

    Private Sub DisplayEnforcementCase()
        If EnforcementCase IsNot Nothing Then
            With EnforcementCase
                Me.Text = "Enforcement # " & .EnforcementId

                ' Header
                EnforcementIdDisplay.Text = "Enforcement # " & .EnforcementId
                ComplianceStatusDisplay.Visible = True
                ComplianceStatusDisplay.Text = .ComplianceStatus
                ResolvedCheckBox.Visible = True
                ResolvedDate.Visible = True
                ResolvedCheckBox.Checked = Not .Open
                StaffResponsible.SelectedValue = .StaffResponsible.StaffId

                ' General tab
                LastEditedDateDisplay.Visible = True
                LastEditedDateDisplay.Text = "Last edited on " & .DateModified.ToString

                LoadViolationType()
                GeneralComments.Text = .Comment
                If .DayZeroDate.HasValue Then DayZeroDisplay.Text = "Day Zero: " & .DayZeroDate.Value.ToString
                SubmitToEpa.Visible = Not .SubmittedToEpa
                SubmitToUC.Visible = Not .SubmittedToUc

                ' LON
                If .EnforcementActions.Contains(EnforcementActionType.LON) Then
                    LonCheckBox.Checked = True
                    LonComments.Text = .LonComment
                End If

                ' NOV
                If .EnforcementActions.Contains(EnforcementActionType.NOV) Then
                    NovCheckBox.Checked = True
                    NovComments.Text = .NovComment
                End If

                ' CO
                If .EnforcementActions.Contains(EnforcementActionType.CO) Then
                    COCheckBox.Checked = True
                    COComments.Text = .CoComment
                    Dim parts As String() = .CoNumber.Split("-"c)
                    CoNumber.Value = Math.Min(Convert.ToInt32(parts(parts.Length - 1)), 999999)
                    COPenaltyAmount.Text = .CoPenaltyAmount
                    COPenaltyComments.Text = .CoPenaltyAmountComment
                    StipulatedPenaltiesGroupBox.Enabled = True
                    LoadStipulatedPenalties()
                End If

                ' AO
                If .EnforcementActions.Contains(EnforcementActionType.AO) Then
                    AOCheckBox.Checked = True
                    AOComments.Text = .AoComment
                End If

                ' All nullable Dates
                DisplayNullableDates()
            End With
        End If
    End Sub

    Private Sub DisplayNullableDates()
        For Each kvp As KeyValuePair(Of Date?, DateTimePicker) In nullableDates
            If kvp.Key.HasValue Then
                kvp.Value.Checked = True
                kvp.Value.Value = kvp.Key.Value
            End If
        Next
    End Sub

#End Region

#Region " Header tab: Resolving "

    Private Sub ResolvedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ResolvedCheckBox.CheckedChanged
        If ResolvedCheckBox.Checked Then
            ResolvedDate.Enabled = True
            EnableDisableChanges(EnableOrDisable.Disable)
        Else
            ResolvedDate.Enabled = False
            EnableDisableChanges(EnableOrDisable.Enable)
        End If
    End Sub

    Private Sub EnableDisableChanges(enabler As EnableOrDisable)
        Dim enabled As Boolean = (enabler = EnableOrDisable.Enable)
        InfoTabPage.Enabled = enabled
        PollutantsTabPage.Enabled = enabled
        LonTabPage.Enabled = enabled
        NovTabPage.Enabled = enabled
        COTabPage.Enabled = enabled
        AOTabPage.Enabled = enabled
        DocumentsTabPage.Enabled = enabled
        AuditHistoryTabPage.Enabled = enabled
        EpaValuesTabPage.Enabled = enabled
        StaffResponsible.Enabled = enabled
    End Sub

#End Region

#Region " General tab: Violation Types "

    Private Sub LoadViolationType()
        Dim severityCode As String
        Dim rowFilter As String

        If String.IsNullOrEmpty(EnforcementCase.ViolationType) Then
            severityCode = "BLANK"
        Else
            Dim dr As DataRow = ViolationTypes.Rows.Find(EnforcementCase.ViolationType)
            severityCode = dr("SEVERITYCODE").ToString
        End If

        Select Case severityCode
            Case "BLANK"
                ViolationTypeNone.Checked = True
                rowFilter = "AIRVIOLATIONTYPECODE = 'BLANK'"
            Case "HPV"
                ViolationTypeHpv.Checked = True
                rowFilter = "(SEVERITYCODE='HPV' AND DEPRECATED = 'FALSE') OR " &
                    "(AIRVIOLATIONTYPECODE = 'BLANK') OR " &
                    "(AIRVIOLATIONTYPECODE = '" & EnforcementCase.ViolationType & "')"
            Case "FRV"
                ViolationTypeFrv.Checked = True
                rowFilter = "(SEVERITYCODE='FRV' AND DEPRECATED = 'FALSE') OR " &
                    "(AIRVIOLATIONTYPECODE = 'BLANK') OR " &
                    "(AIRVIOLATIONTYPECODE = '" & EnforcementCase.ViolationType & "')"
            Case Else
                ViolationTypeNonFrv.Checked = True
                rowFilter = "(SEVERITYCODE<>'FRV' AND SEVERITYCODE<>'FRV' AND DEPRECATED = 'FALSE') OR " &
                    "(AIRVIOLATIONTYPECODE = 'BLANK')"
        End Select

        ApplyViolationSelectFilter(rowFilter)
        ViolationTypeSelect.SelectedValue = EnforcementCase.ViolationType
    End Sub

    Private Sub ApplyViolationSelectFilter(rowFilter As String)
        Dim dv As New DataView(ViolationTypes)
        dv.RowFilter = rowFilter
        dv.Sort = "VIOLATIONTYPEDESC ASC"

        With ViolationTypeSelect
            .DataSource = dv
            .ValueMember = "AIRVIOLATIONTYPECODE"
            .DisplayMember = "VIOLATIONTYPEDESC"
        End With
    End Sub

    Private Sub ViolationType_CheckedChanged(sender As Object, e As EventArgs) _
        Handles ViolationTypeFrv.CheckedChanged, ViolationTypeHpv.CheckedChanged,
        ViolationTypeNonFrv.CheckedChanged, ViolationTypeNone.CheckedChanged

        Dim rowFilter As String

        ViolationTypeSelect.SelectedValue = "BLANK"

        If ViolationTypeNone.Checked Then
            ViolationTypeSelect.Enabled = False
            DayZeroDisplay.Visible = False
        ElseIf ViolationTypeFrv.Checked Then
            rowFilter = "(SEVERITYCODE='FRV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
            ViolationTypeSelect.Enabled = True
            DayZeroDisplay.Visible = False
            ApplyViolationSelectFilter(rowFilter)
        ElseIf ViolationTypeHpv.Checked Then
            rowFilter = "(SEVERITYCODE='HPV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
            ViolationTypeSelect.Enabled = True
            DayZeroDisplay.Visible = True
            ApplyViolationSelectFilter(rowFilter)
        ElseIf ViolationTypeNonFrv.Checked Then
            rowFilter = "(SEVERITYCODE<>'FRV' AND SEVERITYCODE<>'HPV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
            ViolationTypeSelect.Enabled = True
            DayZeroDisplay.Visible = False
            ApplyViolationSelectFilter(rowFilter)
        End If

    End Sub

#End Region

#Region " General tab: Link work item "

    Private Sub LinkToEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkToEvent.Click
        OpenEventLinker()
    End Sub

    Private Sub ClearLinkedEvent_Click(sender As Object, e As EventArgs) Handles ClearLinkedEvent.Click
        LinkedEventId = 0
        DisplayLinkedEvent()
    End Sub

    Private Sub DisplayLinkedEvent()
        If LinkedEventId = 0 Then
            LinkedEventDisplay.Visible = False
            ClearLinkedEvent.Visible = False
            LinkToEvent.Visible = True
        Else
            LinkedEventDisplay.Visible = False
            ClearLinkedEvent.Visible = False
            LinkToEvent.Visible = True
            LinkedEventDisplay.Text = "Discovery Event: " & LinkedEventId.ToString
            LinkedEventDisplay.LinkArea = New LinkArea(17, LinkedEventDisplay.Text.Length)
        End If
    End Sub

    Private Sub OpenEventLinker()
        If EnforcementId Is Nothing Then
            MessageBox.Show("Current enforcement must be saved before linking a discovery event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim discoveryEventDialog As New SSCPEnforcementChecklist
        With discoveryEventDialog
            .AirsNumber = AirsNumber
            .EnforcementNumber = EnforcementId
            .SelectedDiscoveryEvent = LinkedEventId
            .ShowDialog()
        End With

        If discoveryEventDialog.DialogResult = DialogResult.OK Then
            LinkedEventId = discoveryEventDialog.SelectedDiscoveryEvent
            DisplayLinkedEvent()
        End If

        discoveryEventDialog.Dispose()
    End Sub

    Private Sub LinkedEventDisplay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkedEventDisplay.LinkClicked
        OpenFormSscpWorkItem(LinkedEventId)
    End Sub

#End Region

#Region " General tab: Enforcement action checkboxes "

    Private Sub LonCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LonCheckBox.CheckedChanged
        If LonCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(LonTabPage)
            AOCheckBox.Visible = False
            COCheckBox.Visible = False
            NovCheckBox.Visible = False
            ViolationTypeNone.Checked = True
            ViolationTypeGroupbox.Visible = False
        Else
            EnforcementTabs.TabPages.Remove(LonTabPage)
            AOCheckBox.Visible = True
            COCheckBox.Visible = True
            NovCheckBox.Visible = True
        End If
    End Sub

    Private Sub NovCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles NovCheckBox.CheckedChanged
        If NovCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(NovTabPage)
            EnableMajorEnforcementTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(NovTabPage)
            If Not (COCheckBox.Checked Or AOCheckBox.Checked) Then
                EnableMajorEnforcementTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub COCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles COCheckBox.CheckedChanged
        If COCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(COTabPage)
            EnableMajorEnforcementTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(COTabPage)
            If Not (NovCheckBox.Checked Or AOCheckBox.Checked) Then
                EnableMajorEnforcementTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub AOCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AOCheckBox.CheckedChanged
        If AOCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(AOTabPage)
            EnableMajorEnforcementTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(AOTabPage)
            If Not (COCheckBox.Checked Or NovCheckBox.Checked) Then
                EnableMajorEnforcementTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub EnableMajorEnforcementTools(enabler As EnableOrDisable)
        If enabler = EnableOrDisable.Enable Then
            LonCheckBox.Visible = False
            DayZeroDisplay.Visible = True
            ViolationTypeGroupbox.Visible = True
            If Not EnforcementCase.SubmittedToUc And EnforcementId IsNot Nothing Then
                SubmitToUC.Visible = True
            End If
            If Not EnforcementCase.SubmittedToEpa And EnforcementId IsNot Nothing Then
                SubmitToEpa.Visible = True
            End If
            If Not EnforcementTabs.TabPages.Contains(PollutantsTabPage) Then EnforcementTabs.TabPages.Add(PollutantsTabPage)
        Else
            DayZeroDisplay.Visible = False
            ViolationTypeGroupbox.Visible = False
            SubmitToUC.Visible = False
            SubmitToEpa.Visible = False
            LonCheckBox.Visible = True
            If EnforcementTabs.TabPages.Contains(PollutantsTabPage) Then EnforcementTabs.TabPages.Remove(PollutantsTabPage)
        End If
    End Sub

#End Region

#Region " Pollutants/Programs tab "

    Public Sub LoadPollutants()
        Dim dt As DataTable = DAL.GetFacilityPollutants(AirsNumber)

        PollutantsListView.Items.Clear()
        For Each row As DataRow In dt.Rows
            PollutantsListView.Items.Add(New ListViewItem({row(0), row(1)}))
        Next

        Dim poll As String() = DAL.Sscp.GetEnforcementPollutants(EnforcementId)

        For i As Integer = 0 To PollutantsListView.Items.Count - 1
            If poll.Contains(PollutantsListView.Items.Item(i).SubItems(0).Text) Then
                PollutantsListView.Items.Item(i).Checked = True
            End If
        Next
    End Sub

    Private Sub LoadAirPrograms()
        Throw New NotImplementedException()
        Dim dt As DataTable = DAL.GetFacilityAirProgramsAsDataTable(AirsNumber)

        ProgramsListView.Items.Clear()
        For Each row As DataRow In dt.Rows
            ProgramsListView.Items.Add(New ListViewItem({row(0), row(1)}))
        Next

        Dim poll As String() = DAL.Sscp.GetEnforcementPollutants(EnforcementId)

        For i As Integer = 0 To ProgramsListView.Items.Count - 1
            If poll.Contains(ProgramsListView.Items.Item(i).SubItems(0).Text) Then
                ProgramsListView.Items.Item(i).Checked = True
            End If
        Next
    End Sub

    Private Sub EditAirProgramPollutantsButton_Click(sender As Object, e As EventArgs) Handles EditAirProgramPollutantsButton.Click
        If EnforcementId Is Nothing Then
            MessageBox.Show("Current enforcement must be saved before adding pollutants.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim EditAirProgramPollutants As IAIPEditAirProgramPollutants = OpenSingleForm(IAIPEditAirProgramPollutants)
            EditAirProgramPollutants.AirsNumberDisplay.Text = AirsNumber.ShortString
        End If
    End Sub

#End Region

#Region " CO tab: Stipulated Penalties "

    Private Sub LoadStipulatedPenalties()
        Dim dt As DataTable = DAL.Sscp.GetStipulatedPenalties(EnforcementId)

        StipulatedPenalties.DataSource = dt

        StipulatedPenalties.Columns("STRENFORCEMENTKEY").Visible = False
        StipulatedPenalties.Columns("STRSTIPULATEDPENALTY").HeaderText = "Penalty Amount"
        StipulatedPenalties.Columns("STRSTIPULATEDPENALTY").DisplayIndex = 0
        StipulatedPenalties.Columns("STRSTIPULATEDPENALTYCOMMENTS").HeaderText = "Comments"
        StipulatedPenalties.Columns("STRSTIPULATEDPENALTYCOMMENTS").DisplayIndex = 1
        StipulatedPenalties.Columns("STRAFSSTIPULATEDPENALTYNUMBER").Visible = False

        If EnforcementTabs.TabPages.Contains(EpaValuesTabPage) Then DisplayEpaValues()
    End Sub

    Private Sub SaveNewStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles SaveNewStipulatedPenaltyButton.Click
        If EnforcementId Is Nothing Then
            MessageBox.Show("Current enforcement must be saved before saving stipulated penalties.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf String.IsNullOrEmpty(StipulatedPenaltyAmount.Text) Then
            MessageBox.Show("Enter a stipulated penalty amount first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Not Decimal.TryParse(StipulatedPenaltyAmount.Text, Nothing) Then
            MessageBox.Show("Stipulated penalty amount must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            SaveNewStipulatedPenalty()
        End If
    End Sub

    Private Sub SaveNewStipulatedPenalty()
        Dim enfKey As Integer
        Dim afsKey As Integer
        Dim result As Boolean = DAL.Sscp.SaveNewStipulatedPenalty(EnforcementId, AirsNumber, StipulatedPenaltyAmount.Text, StipulatedPenaltyComments.Text, enfKey, afsKey)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
        Else
            MessageBox.Show("There was an error saving the new stipulated penalty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub UpdateStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles UpdateStipulatedPenaltyButton.Click
        If EnforcementId Is Nothing Then
            MessageBox.Show("Current enforcement must be saved before modifying stipulated penalties.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf SelectedStipulatedPenaltyItem = 0 Then
            MessageBox.Show("Select an existing stipulated penalty first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf String.IsNullOrEmpty(StipulatedPenaltyAmount.Text) Then
            MessageBox.Show("Enter a stipulated penalty amount first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Not Decimal.TryParse(StipulatedPenaltyAmount.Text, Nothing) Then
            MessageBox.Show("Stipulated penalty amount must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            UpdateStipulatedPenalty()
        End If
    End Sub

    Private Sub UpdateStipulatedPenalty()
        Dim result As Boolean = DAL.Sscp.UpdateStipulatedPenalty(EnforcementId, StipulatedPenaltyAmount.Text, StipulatedPenaltyComments.Text, SelectedStipulatedPenaltyItem)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
        Else
            MessageBox.Show("There was an error updating the stipulated penalty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub DeleteStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles DeleteStipulatedPenaltyButton.Click
        If EnforcementId Is Nothing Then
            MessageBox.Show("Current enforcement must be saved before modifying stipulated penalties.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf SelectedStipulatedPenaltyItem = 0 Then
            MessageBox.Show("Select an existing stipulated penalty first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            DeleteSelectedStipulatedPenalty()
        End If
    End Sub

    Private Sub DeleteSelectedStipulatedPenalty()
        Dim result As Boolean = DAL.Sscp.DeleteStipulatedPenalty(EnforcementId, SelectedStipulatedPenaltyItem)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
        Else
            MessageBox.Show("There was an error deleting the stipulated penalty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ClearStipulatedPenaltyFormButton_Click(sender As Object, e As EventArgs) Handles ClearStipulatedPenaltyFormButton.Click
        ClearStipulatedPenaltyForm()
    End Sub

    Private Sub StipulatedPenalties_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles StipulatedPenalties.CellContentClick
        If e.RowIndex <> -1 And e.RowIndex < StipulatedPenalties.RowCount Then
            SelectedStipulatedPenaltyItem = StipulatedPenalties.Rows(e.RowIndex).Cells(0).Value.ToString
            StipulatedPenaltyAmount.Text = StipulatedPenalties.Rows(e.RowIndex).Cells(1).Value.ToString
            StipulatedPenaltyComments.Text = StipulatedPenalties.Rows(e.RowIndex).Cells(3).Value.ToString
            UpdateStipulatedPenaltyButton.Visible = True
            DeleteStipulatedPenaltyButton.Visible = True
            SaveNewStipulatedPenaltyButton.Visible = False
        End If
    End Sub

    Private Sub ClearStipulatedPenaltyForm()
        StipulatedPenaltyAmount.Text = ""
        StipulatedPenaltyComments.Text = ""
        UpdateStipulatedPenaltyButton.Visible = False
        DeleteStipulatedPenaltyButton.Visible = False
        SaveNewStipulatedPenaltyButton.Visible = True
        SelectedStipulatedPenaltyItem = 0
    End Sub

#End Region

#Region " Documents tab "

#Region " Display Document files "

    Private Sub LoadDocuments()
        EnableOrDisableDocuments(EnableOrDisable.Disable)
        DocumentList.DataSource = Nothing
        ExistingFiles = GetEnforcementDocumentsAsList(EnforcementCase.EnforcementId)
        If ExistingFiles.Count > 0 Then
            With DocumentList
                .DataSource = New BindingSource(ExistingFiles, Nothing)
                .Enabled = True
                .ClearSelection()
            End With
        End If
    End Sub

    Private Sub EnableOrDisableDocuments(ByVal enabler As EnableOrDisable)
        Dim enabled As Boolean = (enabler = EnableOrDisable.Enable)
        With pnlDocument
            .Enabled = enabled
            .Visible = enabled
        End With
        If enabled Then
            txtDocumentDescription.Text = DocumentList.CurrentRow.Cells("Comment").Value
            lblDocumentName.Text = DocumentList.CurrentRow.Cells("FileName").Value
        End If
    End Sub

    Private Sub FormatDocumentList()
        With DocumentList
            .Columns("EnforcementNumber").Visible = False
            .Columns("BinaryFileId").Visible = False
            With .Columns("Comment")
                .HeaderText = "Description"
                .DisplayIndex = 4
            End With
            .Columns("DocumentId").Visible = False
            With .Columns("DocumentType")
                .HeaderText = "Document Type"
                .DisplayIndex = 0
            End With
            .Columns("DocumentTypeId").Visible = False
            .Columns("FileExtension").Visible = False
            With .Columns("FileName")
                .HeaderText = "File Name"
                .DisplayIndex = 1
            End With
            With .Columns("FileSize")
                .HeaderText = "File Size"
                .DefaultCellStyle.Format = "fs:1"
                .DisplayIndex = 3
                .DefaultCellStyle.FormatProvider = New FileSizeFormatProvider
            End With
            With .Columns("UploadDate")
                .HeaderText = "Uploaded On"
                .DefaultCellStyle.Format = DateFormat
                .DisplayIndex = 2
            End With
        End With
    End Sub

    Private Sub dataGridView_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DocumentList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvDocumentList_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles DocumentList.DataBindingComplete
        FormatDocumentList()
        DocumentList.SanelyResizeColumns()
        DocumentList.ClearSelection()
    End Sub

#End Region

#Region " Document update/download/delete "

    Private Sub dgvDocumentList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentList.SelectionChanged
        If DocumentList.SelectedRows.Count = 1 Then
            EnableOrDisableDocuments(EnableOrDisable.Enable)
        Else
            EnableOrDisableDocuments(EnableOrDisable.Disable)
        End If
    End Sub

    Private Sub btnDocumentDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumentDownload.Click
        ClearMessage(lblMessage, EP)

        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(DocumentList.CurrentRow)
        DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadingFile), doc.FileName))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded Or canceled Then
            ClearMessage(lblMessage, EP)
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.DownloadFailure), lblDocumentName), True, EP, lblMessage)
        End If
    End Sub

    Private Sub btnDocumentUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentUpdateButton.Click
        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(DocumentList.CurrentRow)
        doc.Comment = txtDocumentDescription.Text
        Dim updated As Boolean = UpdateEnforcementDocument(doc, Me)
        If updated Then
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateSuccess), doc.FileName))
            LoadDocuments()
        Else
            DisplayMessage(lblMessage, String.Format(GetDocumentMessage(DocumentMessageType.UpdateFailure), lblDocumentName), True, EP)
        End If
    End Sub

    Private Function EnforcementDocumentFromFileListRow(ByVal row As DataGridViewRow) As EnforcementDocument
        Dim doc As New EnforcementDocument
        With doc
            .EnforcementNumber = row.Cells("EnforcementNumber").Value
            .BinaryFileId = row.Cells("BinaryFileId").Value
            .Comment = row.Cells("Comment").Value
            .DocumentId = row.Cells("DocumentId").Value
            .DocumentType = row.Cells("DocumentType").Value
            .DocumentTypeId = row.Cells("DocumentTypeId").Value
            .FileName = row.Cells("FileName").Value
            .FileSize = row.Cells("FileSize").Value
            .UploadDate = DateTime.Parse(row.Cells("UploadDate").Value)
        End With
        Return doc
    End Function

#End Region

#Region " Accept Button (Documents) "

    Private Sub NoAcceptButton(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtDocumentDescription.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub FileProperties_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtDocumentDescription.Enter
        Me.AcceptButton = DocumentUpdateButton
    End Sub

#End Region

#End Region

#Region " Audit History tab "

    Private Sub RefreshAuditHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshAuditHistory.Click
        LoadAuditData()
    End Sub

    Private Sub ExportAuditHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAuditHistory.Click
        AuditHistory.ExportToExcel(Me)
    End Sub

    Private Sub ShowAuditHistory()
        If Not EnforcementTabs.TabPages.Contains(AuditHistoryTabPage) Then EnforcementTabs.TabPages.Add(AuditHistoryTabPage)
        EnforcementTabs.SelectedTab = AuditHistoryTabPage
        LoadAuditData()
    End Sub

    Private Sub LoadAuditData()
        AuditHistory.DataSource = DAL.Sscp.GetEnforcementAuditHistory(EnforcementId)

        AuditHistory.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        AuditHistory.Columns("STRTRACKINGNUMBER").HeaderText = "Linked event"
        AuditHistory.Columns("DATENFORCEMENTFINALIZED").HeaderText = "Date Closed"
        AuditHistory.Columns("DATENFORCEMENTFINALIZED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("Status").HeaderText = "Submitted to UC"
        AuditHistory.Columns("STRACTIONTYPE").HeaderText = "Action Type"
        AuditHistory.Columns("STRGENERALCOMMENTS").HeaderText = "Comments"
        AuditHistory.Columns("DATDISCOVERYDATE").HeaderText = "Date Discovered"
        AuditHistory.Columns("DATDISCOVERYDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATDAYZERO").HeaderText = "Day Zero"
        AuditHistory.Columns("DATDAYZERO").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRHPV").HeaderText = "Violation Type"
        AuditHistory.Columns("STRPOLLUTANTS").HeaderText = "Pollutants"
        AuditHistory.Columns("STRPOLLUTANTSTATUS").HeaderText = "Compliance Status"
        AuditHistory.Columns("DATLONTOUC").HeaderText = "Date LON to UC"
        AuditHistory.Columns("DATLONTOUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATLONSENT").HeaderText = "Date LON Sent"
        AuditHistory.Columns("DATLONSENT").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATLONRESOLVED").HeaderText = "Date LON Resolved"
        AuditHistory.Columns("DATLONRESOLVED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRLONCOMMENTS").HeaderText = "LON Comments"
        AuditHistory.Columns("DATNOVTOUC").HeaderText = "Date NOV to UC"
        AuditHistory.Columns("DATNOVTOUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNOVTOPM").HeaderText = "Date NOV to PM"
        AuditHistory.Columns("DATNOVTOPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNOVSENT").HeaderText = "Date NOV Sent"
        AuditHistory.Columns("DATNOVSENT").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNOVRESPONSERECEIVED").HeaderText = "Date NOV Response Recieved"
        AuditHistory.Columns("DATNOVRESPONSERECEIVED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNFATOUC").HeaderText = "Date NFA to UC"
        AuditHistory.Columns("DATNFATOUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNFATOPM").HeaderText = "Date NFA to PM"
        AuditHistory.Columns("DATNFATOPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATNFALETTERSENT").HeaderText = "Date NFA Letter Sent"
        AuditHistory.Columns("DATNFALETTERSENT").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRNOVCOMMENT").HeaderText = "NOV Comment"
        AuditHistory.Columns("DATCOTOUC").HeaderText = "Date CO to UC"
        AuditHistory.Columns("DATCOTOUC").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATCOTOPM").HeaderText = "Date CO to PM"
        AuditHistory.Columns("DATCOTOPM").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATCOPROPOSED").HeaderText = "Date CO Proposed"
        AuditHistory.Columns("DATCOPROPOSED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATCORECEIVEDFROMCOMPANY").HeaderText = "Date CO Recieved From Company"
        AuditHistory.Columns("DATCORECEIVEDFROMCOMPANY").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATCORECEIVEDFROMDIRECTOR").HeaderText = "Date CO Received From Director"
        AuditHistory.Columns("DATCORECEIVEDFROMDIRECTOR").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATCOEXECUTED").HeaderText = "Date CO Executed"
        AuditHistory.Columns("DATCOEXECUTED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRCONUMBER").HeaderText = "CO Number"
        AuditHistory.Columns("DATCORESOLVED").HeaderText = "Date CO Resolved"
        AuditHistory.Columns("DATCORESOLVED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRCOCOMMENT").HeaderText = "CO Comment"
        AuditHistory.Columns("STRCOPENALTYAMOUNT").HeaderText = "CO Penalty Amount"
        AuditHistory.Columns("STRCOPENALTYAMOUNTCOMMENTS").HeaderText = "CO Penalty Amount Comments"
        AuditHistory.Columns("DATAOEXECUTED").HeaderText = "Date AO Executed"
        AuditHistory.Columns("DATAOEXECUTED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATAOAPPEALED").HeaderText = "Date AO Appealed"
        AuditHistory.Columns("DATAOAPPEALED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATAORESOLVED").HeaderText = "Date AO Resolved"
        AuditHistory.Columns("DATAORESOLVED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRAOCOMMENT").HeaderText = "AO Comments"
        AuditHistory.Columns("ModifiedBy").HeaderText = "Modified By"
        AuditHistory.Columns("DATMODIFINGDATE").HeaderText = "Date Modified"
        AuditHistory.Columns("DATMODIFINGDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"

        AuditHistory.SanelyResizeColumns()
    End Sub

#End Region

#Region " EPA values tab "

    Private Sub ShowEpaValues()
        If Not EnforcementTabs.TabPages.Contains(EpaValuesTabPage) Then EnforcementTabs.TabPages.Add(EpaValuesTabPage)
        EnforcementTabs.SelectedTab = EpaValuesTabPage
        DisplayEpaValues()
    End Sub

    Private Sub DisplayEpaValues()
        With EnforcementCase
            AfsKeyActionNumber.Text = .AfsKeyActionNumber
            AfsNovActionNumber.Text = .AfsNovActionNumber
            AfsNfaActionNumber.Text = .AfsNfaActionNumber
            AfsCoProposedActionNumber.Text = .AfsCoProposedNumber
            AfsCoExecutedActionNumber.Text = .AfsCoActionNumber
            AfsCoResolvedActionNumber.Text = .AfsCoResolvedActionNumber
            AfsAoCivilCourtActionNumber.Text = .AfsCivilCourtActionNumber
            AfsAoToAgActionNumber.Text = .AfsAoToAGActionNumber
            AfsAoResolvedActionNumber.Text = .AfsAoResolvedActionNumber
            Dim sp As New List(Of String)
            For Each row As DataRow In StipulatedPenalties.Rows
                sp.Add(row("STRAFSSTIPULATEDPENALTYNUMBER").ToString)
            Next
            AfsStipulatedPenalitiesActionNumbers.Text = String.Join(", ", sp)
        End With
    End Sub

#End Region











#Region " Save data "

    Private Sub SaveClick()
        If ValidateFormData() Then
            If SaveEnforcement() Then
                MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")
            Else
                MsgBox("Current data not saved.", MsgBoxStyle.Information, "SSCP Enforcement")
            End If
        End If
    End Sub

    Private Function ValidateFormData() As Boolean
        Throw New NotImplementedException()
    End Function

    Private Function ViolationTypeCheck() As Boolean
        If (NovCheckBox.Checked Or COCheckBox.Checked Or AOCheckBox.Checked) AndAlso
            (ViolationTypeNone.Checked Or ViolationTypeSelect.SelectedValue = "BLANK") Then
            MessageBox.Show("Choose a Violation Type before proceeding.",
                            "No Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            Return True
        End If
    End Function

    Private Function SaveEnforcement() As Boolean
        Dim TrackingNumber As String = ""
        Dim EnforcementFinalizedCheck As String = ""
        Dim EnforcementFinalized As String = ""
        Dim StaffResponsible As String = ""
        Dim EnforcementStatus As String = ""
        Dim ActionType As String = ""
        Dim GeneralComments As String = ""
        Dim DiscoveryDateCheck As String = ""
        Dim DiscoveryDate As String = ""
        Dim DayZeroCheck As String = ""
        Dim DayZero As String = ""
        Dim ViolationType As String = ""
        Dim Pollutants As String = ""
        Dim PollutantStatus As String = ""
        Dim LONtoUCCheck As String = ""
        Dim LONToUC As String = ""
        Dim LONSentCheck As String = ""
        Dim LonSent As String = ""
        Dim LONResolvedCheck As String = ""
        Dim LONResolved As String = ""
        Dim LONComments As String = ""
        Dim LONResolvedEnforcement As String = ""
        Dim NOVtoUCCheck As String = ""
        Dim NOVToUC As String = ""
        Dim NOVtoPMCheck As String = ""
        Dim NOVToPM As String = ""
        Dim NOVSentCheck As String = ""
        Dim NOVSent As String = ""
        Dim NOVResponseRecieveCheck As String = ""
        Dim NOVResponseReceived As String = ""
        Dim NFAtoUCCheck As String = ""
        Dim NFAToUC As String = ""
        Dim NFAtoPMCheck As String = ""
        Dim NFAToPM As String = ""
        Dim NFALetterSentCheck As String = ""
        Dim NFALetterSent As String = ""
        Dim NOVCommetns As String = ""
        Dim NOVResolvedEnforcement As String = ""
        Dim COtoUCCheck As String = ""
        Dim COToUC As String = ""
        Dim COtoPMCheck As String = ""
        Dim COToPM As String = ""
        Dim CoProposedCheck As String = ""
        Dim COProposed As String = ""
        Dim COReceivedCompanyCheck As String = ""
        Dim COReceivedCompany As String = ""
        Dim CORecievedDirectorCheck As String = ""
        Dim COReceivedDirector As String = ""
        Dim COExecutedCheck As String = ""
        Dim COExecuted As String = ""
        Dim CONumber As String = ""
        Dim COResolvedCheck As String = ""
        Dim COResolved As String = ""
        Dim COPenaltyAmount As String = ""
        Dim COPenaltyAmountComments As String = ""
        Dim COComment As String = ""
        Dim COResolvedEnforcement As String = ""
        Dim StipulatedPenalty As String = ""
        Dim AOExecutedCheck As String = ""
        Dim AOExecuted As String = ""
        Dim AOAppealedCheck As String = ""
        Dim AOAppealed As String = ""
        Dim AOResolvedCheck As String = ""
        Dim AOResolved As String = ""
        Dim AOComment As String = ""
        Dim AFSKeyActionNumber As String = ""
        Dim AFSNOVSentNumber As String = ""
        Dim AFSNOVResolvedNumber As String = ""
        Dim AFSCOProposedNumber As String = ""
        Dim AFSCOExecutedNumber As String = ""
        Dim AFSCOResolvedNumber As String = ""
        Dim AFSAOtoAGNumber As String = ""
        Dim AFSCivilCourtNumber As String = ""
        Dim AFSAOResolvedNumber As String = ""

        If AccountFormAccess(48, 2) = "0" And AccountFormAccess(48, 3) = "0" And AccountFormAccess(48, 4) = "0" Then
            MsgBox("You do not have sufficent permission to save Compliance Events.", MsgBoxStyle.Information, "Compliance Events")
            Exit Sub
        Else
            If LinkedEvent.Text <> "" Then
                TrackingNumber = LinkedEvent.Text
            Else
                TrackingNumber = ""
            End If
            If Me.AirsNumber Is Nothing Then
                MsgBox("There is no AIRS #. An Enforcement Action cannot be saved without an AIRS #." &
                           "No Data Saved", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If ResolvedDate.Checked = True Then
                EnforcementFinalized = Format(ResolvedDate.Value, "dd-MMM-yyyy")
                EnforcementFinalizedCheck = "True"
            Else
                EnforcementFinalized = ""
                EnforcementFinalizedCheck = "False"
            End If
            StaffResponsible = Me.StaffResponsible.SelectedValue
            If StaffResponsible = "" Then
                StaffResponsible = UserGCode
            End If
            If txtSubmitToUC.Text <> "" Then
                EnforcementStatus = "UC"
            Else
                EnforcementStatus = ""
            End If

            If LonCheckBox.Checked And Not (NovCheckBox.Checked Or COCheckBox.Checked Or AOCheckBox.Checked) Then
                ActionType = "LON"
            ElseIf NovCheckBox.Checked And Not (COCheckBox.Checked Or AOCheckBox.Checked Or ViolationTypeHpv.Checked) Then
                ActionType = "NOV"
            ElseIf COCheckBox.Checked And Not (AOCheckBox.Checked Or ViolationTypeHpv.Checked Or Me.COExecuted.Checked) Then
                ActionType = "NOVCOP"
            ElseIf COCheckBox.Checked And Not (AOCheckBox.Checked Or ViolationTypeHpv.Checked) And Me.COExecuted.Checked Then
                ActionType = "NOVCO"
            ElseIf AOCheckBox.Checked And Not ViolationTypeHpv.Checked Then
                ActionType = "NOVAO"
            ElseIf COCheckBox.Checked And ViolationTypeHpv.Checked And Not (AOCheckBox.Checked Or Me.COExecuted.Checked) Then
                ActionType = "HPVCOP"
            ElseIf ViolationTypeHpv.Checked And COCheckBox.Checked And Not AOCheckBox.Checked And Me.COExecuted.Checked Then
                ActionType = "HPVCO"
            ElseIf ViolationTypeHpv.Checked And AOCheckBox.Checked Then
                ActionType = "HPVAO"
            ElseIf ViolationTypeHpv.Checked And Not (COCheckBox.Checked Or AOCheckBox.Checked) Then
                ActionType = "HPV"
            End If

            If Me.GeneralComments.Text <> "" Then
                GeneralComments = Me.GeneralComments.Text
            Else
                GeneralComments = ""
            End If
            If Me.DiscoveryDate.Checked = True Then
                DiscoveryDate = Format(Me.DiscoveryDate.Value, "dd-MMM-yyyy")
                DiscoveryDateCheck = "True"
            Else
                DiscoveryDate = ""
                DiscoveryDateCheck = "False"
            End If
            If DTPDayZero.Checked = True Then
                DayZero = Format(DTPDayZero.Value, "dd-MMM-yyyy")
                DayZeroCheck = "True"
            Else
                DayZero = ""
                DayZeroCheck = "False"
            End If

            If ViolationTypeSelect.SelectedValue = "BLANK" Then
                ViolationType = ""
            Else
                ViolationType = ViolationTypeSelect.SelectedValue
            End If

            Dim i As Integer
            For i = 0 To PollutantsListView.Items.Count - 1
                If PollutantsListView.Items.Item(i).Checked = True Then
                    Pollutants = Pollutants & Mid(PollutantsListView.Items.Item(i).SubItems(1).Text, 1, 1) & PollutantsListView.Items.Item(i).SubItems(2).Text & ","
                End If
            Next

            PollutantStatus = cboPollutantStatus.SelectedValue
            If Me.LonToUC.Checked = True Then
                LONToUC = Format(Me.LonToUC.Value, "dd-MMM-yyyy")
                LONtoUCCheck = "True"
            Else
                LONToUC = ""
                LONtoUCCheck = "False"
            End If
            If Me.LonSent.Checked = True Then
                LonSent = Format(Me.LonSent.Value, "dd-MMM-yyyy")
                LONSentCheck = "True"
            Else
                LonSent = ""
                LONSentCheck = "False"
            End If
            If Me.LonResolved.Checked = True Then
                LONResolved = Format(Me.LonResolved.Value, "dd-MMM-yyyy")
                LONResolvedCheck = "True"
            Else
                LONResolved = ""
                LONResolvedCheck = "False"
            End If
            If Me.LonComments.Text <> "" Then
                LONComments = Me.LonComments.Text
            Else
                LONComments = ""
            End If
            LONResolvedEnforcement = ""
            If Me.NovToUC.Checked = True Then
                NOVToUC = Format(Me.NovToUC.Value, "dd-MMM-yyyy")
                NOVtoUCCheck = "True"
            Else
                NOVToUC = ""
                NOVtoUCCheck = "False"
            End If
            If Me.NovToPM.Checked = True Then
                NOVToPM = Format(Me.NovToPM.Value, "dd-MMM-yyyy")
                NOVtoPMCheck = "True"
            Else
                NOVToPM = ""
                NOVtoPMCheck = "False"
            End If
            If Me.NovSent.Checked = True Then
                NOVSent = Format(Me.NovSent.Value, "dd-MMM-yyyy")
                NOVSentCheck = "True"
            Else
                NOVSent = ""
                NOVSentCheck = "False"
            End If
            If Me.NovResponseReceived.Checked = True Then
                NOVResponseReceived = Format(Me.NovResponseReceived.Value, "dd-MMM-yyyy")
                NOVResponseRecieveCheck = "True"
            Else
                NOVResponseReceived = ""
                NOVResponseRecieveCheck = "False"
            End If
            If Me.NfaToUC.Checked = True Then
                NFAToUC = Format(Me.NfaToUC.Value, "dd-MMM-yyyy")
                NFAtoUCCheck = "True"
            Else
                NFAToUC = ""
                NFAtoUCCheck = "False"
            End If
            If Me.NfaToPM.Checked = True Then
                NFAToPM = Format(Me.NfaToPM.Value, "dd-MMM-yyyy")
                NFAtoPMCheck = "True"
            Else
                NFAToPM = ""
                NFAtoPMCheck = "False"
            End If
            If NfaSent.Checked = True Then
                NFALetterSent = Format(NfaSent.Value, "dd-MMM-yyyy")
                NFALetterSentCheck = "True"
            Else
                NFALetterSent = ""
                NFALetterSentCheck = "False"
            End If
            If NovComments.Text <> "" Then
                NOVCommetns = NovComments.Text
            Else
                NOVCommetns = ""
            End If
            If Me.COToUC.Checked = True Then
                COToUC = Format(Me.COToUC.Value, "dd-MMM-yyyy")
                COtoUCCheck = "True"
            Else
                COToUC = ""
                COtoUCCheck = "False"
            End If
            If Me.COToPM.Checked = True Then
                COToPM = Format(Me.COToPM.Value, "dd-MMM-yyyy")
                COtoPMCheck = "True"
            Else
                COToPM = ""
                COtoPMCheck = "False"
            End If
            If Me.COProposed.Checked = True Then
                COProposed = Format(Me.COProposed.Value, "dd-MMM-yyyy")
                CoProposedCheck = "True"
            Else
                COProposed = ""
                CoProposedCheck = "False"
            End If
            If COReceivedfromCompany.Checked = True Then
                COReceivedCompany = Format(COReceivedfromCompany.Value, "dd-MMM-yyyy")
                COReceivedCompanyCheck = "True"
            Else
                COReceivedCompany = ""
                COReceivedCompanyCheck = "False"
            End If
            If COReceivedFromDirector.Checked = True Then
                COReceivedDirector = Format(COReceivedFromDirector.Value, "dd-MMM-yyyy")
                CORecievedDirectorCheck = "True"
            Else
                COReceivedDirector = ""
                CORecievedDirectorCheck = "False"
            End If
            If Me.COExecuted.Checked = True Then
                COExecuted = Format(Me.COExecuted.Value, "dd-MMM-yyyy")
                COExecutedCheck = "True"
            Else
                COExecuted = ""
                COExecutedCheck = "False"
            End If
            If Me.CoNumber.Value = 0 Then
                CONumber = ""
            Else
                CONumber = "EPD-AQC-" & Me.CoNumber.Value.ToString
            End If
            If Me.COResolved.Checked = True Then
                COResolved = Format(Me.COResolved.Value, "dd-MMM-yyyy")
                COResolvedCheck = "True"
            Else
                COResolved = ""
                COResolvedCheck = "False"
            End If
            If Me.COPenaltyAmount.Text <> "" Then
                COPenaltyAmount = Me.COPenaltyAmount.Text
            Else
                COPenaltyAmount = ""
            End If
            If COPenaltyComments.Text <> "" Then
                COPenaltyAmountComments = COPenaltyComments.Text
            Else
                COPenaltyAmountComments = ""
            End If
            If COComments.Text <> "" Then
                COComment = COComments.Text
            Else
                COComment = ""
            End If
            If Me.AOExecuted.Checked = True Then
                AOExecuted = Format(Me.AOExecuted.Value, "dd-MMM-yyyy")
                AOExecutedCheck = "True"
            Else
                AOExecuted = ""
                AOExecutedCheck = "False"
            End If
            If Me.AOAppealed.Checked = True Then
                AOAppealed = Format(Me.AOAppealed.Value, "dd-MMM-yyyy")
                AOAppealedCheck = "True"
            Else
                AOAppealed = ""
                AOAppealedCheck = "False"
            End If
            If Me.AOResolved.Checked = True Then
                AOResolved = Format(Me.AOResolved.Value, "dd-MMM-yyyy")
                AOResolvedCheck = "True"
            Else
                AOResolved = ""
                AOResolvedCheck = "False"
            End If
            If AOComments.Text <> "" Then
                AOComment = AOComments.Text
            Else
                AOComment = ""
            End If

            'For Each row As DataGridViewRow In dgvStipulatedPenalties.Rows
            '    StipulatedPenalty = StipulatedPenalty + CDec(row.Cells(0).Value)
            'Next
            If IsDBNull(StipulatedPenalties.RowCount.ToString) Then
                StipulatedPenalty = ""
            Else
                StipulatedPenalty = StipulatedPenalties.RowCount.ToString
            End If
            'If txtStipulatedKey.Text <> "" Then
            '    StipulatedPenalty = txtStipulatedKey.Text
            'Else
            '    StipulatedPenalty = ""
            'End If
            If Me.AfsKeyActionNumber.Text <> "" Then
                AFSKeyActionNumber = Me.AfsKeyActionNumber.Text
            Else
                AFSKeyActionNumber = ""
            End If
            If AfsNovActionNumber.Text <> "" Then
                AFSNOVSentNumber = AfsNovActionNumber.Text
            Else
                AFSNOVSentNumber = ""
            End If
            If AfsNfaActionNumber.Text <> "" Then
                AFSNOVResolvedNumber = AfsNfaActionNumber.Text
            Else
                AFSNOVResolvedNumber = ""
            End If
            If AfsCoProposedActionNumber.Text <> "" Then
                AFSCOProposedNumber = AfsCoProposedActionNumber.Text
            Else
                AFSCOProposedNumber = ""
            End If
            If AfsCoExecutedActionNumber.Text <> "" Then
                AFSCOExecutedNumber = AfsCoExecutedActionNumber.Text
            Else
                AFSCOExecutedNumber = ""
            End If
            If AfsCoResolvedActionNumber.Text <> "" Then
                AFSCOResolvedNumber = AfsCoResolvedActionNumber.Text
            Else
                AFSCOResolvedNumber = ""
            End If
            If AfsAoToAgActionNumber.Text <> "" Then
                AFSAOtoAGNumber = AfsAoToAgActionNumber.Text
            Else
                AFSAOtoAGNumber = ""
            End If
            If AfsAoCivilCourtActionNumber.Text <> "" Then
                AFSCivilCourtNumber = AfsAoCivilCourtActionNumber.Text
            Else
                AFSCivilCourtNumber = ""
            End If
            If AfsAoResolvedActionNumber.Text <> "" Then
                AFSAOResolvedNumber = AfsAoResolvedActionNumber.Text
            Else
                AFSAOResolvedNumber = ""
            End If

            If EnforcementIdDisplay.Text = "" Or EnforcementIdDisplay.Text = "N/A" Then
                Sql = "Insert into AIRBRANCH.SSCP_Enforcement " &
                    "values " &
                    "((select max(ID) + 1 from AIRBRANCH.SSCP_Enforcement), " &
                    "AIRBRANCH.SSCPEnforcementNumber.nextval, " &
                    "'" & TrackingNumber & "', '" & AirsNumber.DbFormattedString & "', " &
                    "'" & EnforcementFinalizedCheck & "', '" & EnforcementFinalized & "', " &
                    "'" & StaffResponsible & "', '" & EnforcementStatus & "', " &
                    "'" & ActionType & "', '" & Replace(GeneralComments, "'", "''") & "', " &
                    "'" & DiscoveryDateCheck & "', '" & DiscoveryDate & "', " &
                    "'" & DayZeroCheck & "', '" & DayZero & "', " &
                    "'" & ViolationType & "', '" & Pollutants & "', " &
                    "'" & PollutantStatus & "',  " &
                    "'" & LONtoUCCheck & "', '" & LONToUC & "', " &
                    "'" & LONSentCheck & "', '" & LonSent & "', " &
                    "'" & LONResolvedCheck & "', '" & LONResolved & "', " &
                    "'" & Replace(LONComments, "'", "''") & "', '" & LONResolvedEnforcement & "', " &
                    "'" & NOVtoUCCheck & "', '" & NOVToUC & "', " &
                    "'" & NOVtoPMCheck & "', '" & NOVToPM & "', " &
                    "'" & NOVSentCheck & "', '" & NOVSent & "', " &
                    "'" & NOVResponseRecieveCheck & "', '" & NOVResponseReceived & "', " &
                    "'" & NFAtoUCCheck & "', '" & NFAToUC & "', " &
                    "'" & NFAtoPMCheck & "', '" & NFAToPM & "', " &
                    "'" & NFALetterSentCheck & "', '" & NFALetterSent & "', " &
                    "'" & Replace(NOVCommetns, "'", "''") & "', '" & NOVResolvedEnforcement & "', " &
                    "'" & COtoUCCheck & "', '" & COToUC & "', " &
                    "'" & COtoPMCheck & "', '" & COToPM & "', " &
                    "'" & CoProposedCheck & "', '" & COProposed & "', " &
                    "'" & COReceivedCompanyCheck & "', '" & COReceivedCompany & "', " &
                    "'" & CORecievedDirectorCheck & "', '" & COReceivedDirector & "', " &
                    "'" & COExecutedCheck & "', '" & COExecuted & "', " &
                    "'" & CONumber & "', " &
                    "'" & COResolvedCheck & "', '" & COResolved & "', " &
                    "'" & COPenaltyAmount & "', '" & Replace(COPenaltyAmountComments, "'", "''") & "', " &
                    "'" & Replace(COComment, "'", "''") & "', '" & StipulatedPenalty.ToString & "', " &
                    "'" & COResolvedEnforcement & "', " &
                    "'" & AOExecutedCheck & "', '" & AOExecuted & "', " &
                    "'" & AOAppealedCheck & "', '" & AOAppealed & "', " &
                    "'" & AOResolvedCheck & "', '" & AOResolved & "', " &
                    "'" & Replace(AOComment, "'", "''") & "', " &
                    "'" & AFSKeyActionNumber & "', '" & AFSNOVSentNumber & "', " &
                    "'" & AFSNOVResolvedNumber & "', '" & AFSCOProposedNumber & "', " &
                    "'" & AFSCOExecutedNumber & "', '" & AFSCOResolvedNumber & "', " &
                    "'" & AFSAOtoAGNumber & "', '" & AFSCivilCourtNumber & "', " &
                    "'" & AFSAOResolvedNumber & "', " &
                    "'" & UserGCode & "', sysdate ) "

                cmd = New OracleCommand(Sql, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                Sql = "Select AIRBRANCH.SSCPEnforcementnumber.currval from dual "
                cmd = New OracleCommand(Sql, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    EnforcementIdDisplay.Text = dr.Item(0)
                    If Me.ID = -1 Then
                        Me.ID = CInt(EnforcementIdDisplay.Text)
                        MultiForm(Me.Name).ChangeKey(-1, CInt(EnforcementIdDisplay.Text))
                    End If
                End While

                dr.Close()
            Else
                Sql = "Insert into AIRBRANCH.SSCP_Enforcement " &
                    "values " &
                    "((select max(ID) + 1 from AIRBRANCH.SSCP_Enforcement), " &
                    "'" & EnforcementIdDisplay.Text & "', " &
                    "'" & TrackingNumber & "', '" & AirsNumber.DbFormattedString & "', " &
                    "'" & EnforcementFinalizedCheck & "', '" & EnforcementFinalized & "', " &
                    "'" & StaffResponsible & "', '" & EnforcementStatus & "', " &
                    "'" & ActionType & "', '" & Replace(GeneralComments, "'", "''") & "', " &
                    "'" & DiscoveryDateCheck & "', '" & DiscoveryDate & "', " &
                    "'" & DayZeroCheck & "', '" & DayZero & "', " &
                    "'" & ViolationType & "', '" & Pollutants & "', " &
                    "'" & PollutantStatus & "',  " &
                    "'" & LONtoUCCheck & "', '" & LONToUC & "', " &
                    "'" & LONSentCheck & "', '" & LonSent & "', " &
                    "'" & LONResolvedCheck & "', '" & LONResolved & "', " &
                    "'" & Replace(LONComments, "'", "''") & "', '" & LONResolvedEnforcement & "', " &
                    "'" & NOVtoUCCheck & "', '" & NOVToUC & "', " &
                    "'" & NOVtoPMCheck & "', '" & NOVToPM & "', " &
                    "'" & NOVSentCheck & "', '" & NOVSent & "', " &
                    "'" & NOVResponseRecieveCheck & "', '" & NOVResponseReceived & "', " &
                    "'" & NFAtoUCCheck & "', '" & NFAToUC & "', " &
                    "'" & NFAtoPMCheck & "', '" & NFAToPM & "', " &
                    "'" & NFALetterSentCheck & "', '" & NFALetterSent & "', " &
                    "'" & Replace(NOVCommetns, "'", "''") & "', '" & NOVResolvedEnforcement & "', " &
                    "'" & COtoUCCheck & "', '" & COToUC & "', " &
                    "'" & COtoPMCheck & "', '" & COToPM & "', " &
                    "'" & CoProposedCheck & "', '" & COProposed & "', " &
                    "'" & COReceivedCompanyCheck & "', '" & COReceivedCompany & "', " &
                    "'" & CORecievedDirectorCheck & "', '" & COReceivedDirector & "', " &
                    "'" & COExecutedCheck & "', '" & COExecuted & "', " &
                    "'" & CONumber & "', " &
                    "'" & COResolvedCheck & "', '" & COResolved & "', " &
                    "'" & COPenaltyAmount & "', '" & Replace(COPenaltyAmountComments, "'", "''") & "', " &
                    "'" & Replace(COComment, "'", "''") & "', '" & StipulatedPenalty & "', " &
                    "'" & COResolvedEnforcement & "', " &
                    "'" & AOExecutedCheck & "', '" & AOExecuted & "', " &
                    "'" & AOAppealedCheck & "', '" & AOAppealed & "', " &
                    "'" & AOResolvedCheck & "', '" & AOResolved & "', " &
                    "'" & Replace(AOComment, "'", "''") & "', " &
                    "'" & AFSKeyActionNumber & "', '" & AFSNOVSentNumber & "', " &
                    "'" & AFSNOVResolvedNumber & "', '" & AFSCOProposedNumber & "', " &
                    "'" & AFSCOExecutedNumber & "', '" & AFSCOResolvedNumber & "', " &
                    "'" & AFSAOtoAGNumber & "', '" & AFSCivilCourtNumber & "', " &
                    "'" & AFSAOResolvedNumber & "', " &
                    "'" & UserGCode & "', sysdate ) "

                cmd = New OracleCommand(Sql, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_SSCPEnforcement", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("ENFORCEMENT", OracleDbType.Varchar2)).Value = EnforcementIdDisplay.Text
            cmd.ExecuteNonQuery()

            If cboPollutantStatus.SelectedValue = "" Then
                cboPollutantStatus.SelectedValue = "0"
            End If
            'Update Pollutant Status in Header Tables 
            i = 0
            For i = 0 To PollutantsListView.Items.Count - 1
                If PollutantsListView.Items.Item(i).Checked = True Then

                    Sql = "Update AIRBRANCH.APBAirProgramPollutants set " &
                        "strComplianceStatus = '" & cboPollutantStatus.SelectedValue & "', " &
                        "strModifingPerson = '" & UserGCode & "', " &
                        "datModifingDate = '" & OracleDate & "' " &
                        "where strAirPollutantKey = '" & PollutantsListView.Items.Item(i).SubItems(5).Text & "' " &
                        "and strPollutantkey = '" & PollutantsListView.Items.Item(i).SubItems(2).Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Next
        End If
    End Function

    Private Sub SaveAFSInformation()
        Dim KeyActionNumber As String = ""
        Dim NOVActionNumber As String = ""
        Dim NFAActionNumber As String = ""
        Dim COProposedActionNumber As String = ""
        Dim COExecutedActionNumber As String = ""
        Dim COResolvedActionNumber As String = ""
        Dim AOtoAGActionNumber As String = ""
        Dim AOtoCivilCourtActionNumber As String = ""
        Dim AOResolvedActionNumber As String = ""

        Try
            If NovCheckBox.Checked Or COCheckBox.Checked Or AOCheckBox.Checked Then

                If AfsKeyActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        KeyActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsKeyActionNumber.Text = KeyActionNumber

                    KeyActionNumber = CStr(CInt(KeyActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNUmber = '" & KeyActionNumber & "' " &
                    "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If NovSent.Checked AndAlso AfsNovActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        NOVActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsNovActionNumber.Text = NOVActionNumber

                    NOVActionNumber = CStr(CInt(NOVActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNUmber = '" & NOVActionNumber & "' " &
                    "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If NfaSent.Checked AndAlso AfsNfaActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '" & Me.AirsNumber.DbFormattedString & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        NFAActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsNfaActionNumber.Text = NFAActionNumber

                    NFAActionNumber = CStr(CInt(NFAActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & NFAActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If COProposed.Checked AndAlso AfsCoProposedActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        COProposedActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsCoProposedActionNumber.Text = COProposedActionNumber

                    COProposedActionNumber = CStr(CInt(COProposedActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & COProposedActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If COExecuted.Checked AndAlso AfsCoExecutedActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        COExecutedActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsCoExecutedActionNumber.Text = COExecutedActionNumber

                    COExecutedActionNumber = CStr(CInt(COExecutedActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & COExecutedActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If COResolved.Checked AndAlso AfsCoResolvedActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        COResolvedActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsCoResolvedActionNumber.Text = COResolvedActionNumber

                    COResolvedActionNumber = CStr(CInt(COResolvedActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & COResolvedActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If AOExecuted.Checked AndAlso AfsAoToAgActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AOtoAGActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsAoToAgActionNumber.Text = AOtoAGActionNumber

                    AOtoAGActionNumber = CStr(CInt(AOtoAGActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & AOtoAGActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If AOAppealed.Checked AndAlso AfsAoCivilCourtActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AOtoCivilCourtActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsAoCivilCourtActionNumber.Text = AOtoCivilCourtActionNumber

                    AOtoCivilCourtActionNumber = CStr(CInt(AOtoCivilCourtActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & AOtoCivilCourtActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                If AOResolved.Checked AndAlso AfsAoResolvedActionNumber.Text = "" Then
                    Sql = "Select strAFSActionNumber " &
                    "from AIRBRANCH.APBSupplamentalData " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        AOResolvedActionNumber = dr.Item("strAFSActionNumber")
                    End While
                    dr.Close()

                    AfsAoResolvedActionNumber.Text = AOResolvedActionNumber

                    AOResolvedActionNumber = CStr(CInt(AOResolvedActionNumber) + 1)

                    Sql = "Update AIRBRANCH.APBSupplamentalData set " &
                    "strAFSActionNumber = '" & AOResolvedActionNumber & "' " &
                    "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
                    cmd = New OracleCommand(Sql, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Delete enforcement "

    Private Sub DeleteEnforcement()
        Try
            Dim AFSStatus As String = ""
            Dim tempAIRS As String = ""

            Sql = "Select strUpDateStatus " &
            "from AIRBRANCH.AFSSSCPEnforcementRecords " &
            "where strEnforcementNumber = '" & EnforcementIdDisplay.Text & "' "

            cmd = New OracleCommand(Sql, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                AFSStatus = dr.Item("strUpDateStatus")
            Else
                AFSStatus = "X"
            End If
            dr.Close()

            If AFSStatus = "C" Or AFSStatus = "N" Then
                MsgBox("This Enforcement has already been submitted to the EPA." & vbCrLf &
                "Please contact your manager and Michael Floyd to have this enforcement Deleted.",
                MsgBoxStyle.Exclamation, "Enforcement")
            Else
                Dim Result As DialogResult

                Result = MessageBox.Show("Are you certain that you want to delete this enforcement?",
                  "Enforcement", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case DialogResult.Yes
                        Sql = "Select strAIRSNumber " &
                        "from AIRBRANCH.SSCP_AuditedEnforcement " &
                        "where strEnforcementNumber = '" & EnforcementIdDisplay.Text & "' "

                        cmd = New OracleCommand(Sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strAIRSNumber")) Then
                                tempAIRS = ""
                            Else
                                tempAIRS = dr.Item("strAIRSNumber")
                            End If
                        End While
                        dr.Close()

                        Sql = "Delete AIRBRANCH.AFSSSCPEnforcementRecords " &
                        "where strEnforcementNumber = '" & EnforcementIdDisplay.Text & "' "
                        cmd = New OracleCommand(Sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AIRBRANCH.AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(Sql, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        Sql = "Delete AIRBRANCH.SSCPENforcementStipulated " &
                        "where strEnforcementNumber = '" & EnforcementIdDisplay.Text & "' "
                        cmd = New OracleCommand(Sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AIRBRANCH.AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(Sql, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        Sql = "Delete AIRBRANCH.SSCP_AuditedEnforcement " &
                        "where strEnforcementNumber = '" & EnforcementIdDisplay.Text & "' "
                        cmd = New OracleCommand(Sql, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL2 = "Insert into AIRBRANCH.AFSDeletions " &
                               "values " &
                               "(" &
                               "(select " &
                               "case when max(numCounter) is null then 1 " &
                               "else max(numCounter) + 1 " &
                               "end numCounter " &
                               "from AIRBRANCH.AFSDeletions), " &
                               "'" & tempAIRS & "', " &
                               "'" & Replace(Sql, "'", "''") & "', 'True', " &
                               "'" & OracleDate & "', '', " &
                               "'') "

                        cmd = New OracleCommand(SQL2, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        MsgBox("Enforcement Deleted.", MsgBoxStyle.Information, "Enforcement")
                        Me.Close()

                    Case DialogResult.No
                        Sql = ""
                    Case DialogResult.Cancel
                        Sql = ""
                    Case Else
                        Sql = ""
                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

#Region " General tab: Submit to UC/EPA "

    Private Sub btnSubmitToUC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubmitToUC.Click
        Try
            If Not ViolationTypeCheck() Then Exit Sub

            txtSubmitToUC.Text = "UC"
            SubmitToUC.Visible = False
            SaveEnforcement()
            MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSubmitEnforcementToEPA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubmitToEpa.Click
        Try
            If Not ViolationTypeCheck() Then Exit Sub

            If LinkedEvent.Text = "" Then
                Dim result As DialogResult

                result = MessageBox.Show("There is no linked event for this enforcement action." & vbCrLf &
                "Do you want to submit this enforcement to EPA without an initiating action?", "Enforcement",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                Select Case result
                    Case DialogResult.No
                        Exit Sub
                End Select
            End If

            SaveAFSInformation()
            SaveEnforcement()
            MsgBox("Current data saved.", MsgBoxStyle.Information, "SSCP Enforcement")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region





#Region " Menu and Toolbar "

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        SaveClick()
    End Sub

    Private Sub mmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveMenuItem.Click
        SaveClick()
    End Sub

    Private Sub mmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClose.Click
        Me.Close()
    End Sub

    Private Sub mmiShowAuditHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiShowAuditHistory.Click
        ShowAuditHistory()
    End Sub

    Private Sub mmiShowEpaActionNumbersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mmiShowEpaActionNumbersToolStripMenuItem.Click
        ShowEpaValues()
    End Sub

    Private Sub mmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteEnforcementMenuItem.Click
        DeleteEnforcement()
    End Sub



#End Region

End Class