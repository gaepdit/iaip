Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Iaip.Apb
Imports Iaip.Apb.Sscp
Imports Iaip.DAL.DocumentData

Public Class SscpEnforcement

#Region " Properties and Fields "

    Public Property EnforcementId As Integer = 0
    Public Property EnforcementCase As New EnforcementCase
    Public Property AirsNumber As ApbFacilityId
    Public Property Facility As Facilities.Facility

    ''' <summary>
    ''' Only used when creating enforcement for a work item not 
    ''' currently associated with enforcement
    ''' </summary>
    Public Property InitialLinkedEventId As Integer

    Private violationTypes As DataTable
    Private existingFiles As List(Of EnforcementDocument)
    Private selectedStipulatedPenaltyItem As Integer = 0
    Private validationErrors As Dictionary(Of Control, String)
    Private nextAfsKey As Integer

    Private _generalMessage As IaipMessage
    Private Property GeneralMessage As IaipMessage
        Get
            Return _generalMessage
        End Get
        Set(value As IaipMessage)
            If value Is Nothing And Message IsNot Nothing Then
                ClearGeneralMessage()
            End If
            _generalMessage = value
            If value IsNot Nothing Then
                DisplayGeneralMessage()
            End If
        End Set
    End Property

    Private _message As IaipMessage
    Private Property Message As IaipMessage
        Get
            Return _message
        End Get
        Set(value As IaipMessage)
            If value Is Nothing And Message IsNot Nothing Then
                Message.Clear()
            End If
            _message = value
            If value IsNot Nothing Then
                If Message.WarningLevel = IaipMessage.WarningLevels.ErrorReport Then
                    Message.Display(DocumentMessageDisplay, DocumentsErrorProvider)
                Else
                    Message.Display(DocumentMessageDisplay)
                End If
            End If
        End Set
    End Property

#End Region

#Region " Form load/closing events "

    Private Sub SscpEnforcement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Set up form/defaults/permissions
        GetLookupTables()
        LoadComboBoxes()
        SetUpForm()
        SetUserPermissions()

        ' Parse parameters load initial data
        ParseParameters()
        If EnforcementId > 0 Then
            LoadCurrentEnforcement()
        End If
        LoadLinkedEvents()
        If InitialLinkedEventId > 0 Then
            LoadInitialLinkedEvent()
        End If
        LoadCurrentFacility()

        ' Display initial data
        DisplayFacility()
        DisplayEnforcementCase()

        ' Programs/Pollutants
        LoadFacilityPollutants()
        LoadFacilityAirPrograms()
        DisplayEnforcementPollutants()
        DisplayEnforcementAirPrograms()

        DisableAllIfDeleted()
    End Sub

    Private Sub SscpEnforcement_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ClearGeneralMessage()
    End Sub

#End Region

#Region " Form setup "

    Private Sub GetLookupTables()
        violationTypes = GetSharedData(SharedTable.ViolationTypes)
    End Sub

    Private Sub LoadComboBoxes()
        LoadStaffComboBox()
    End Sub

    Private Sub LoadStaffComboBox()
        With StaffResponsible
            .DataSource = GetSharedData(SharedTable.AllComplianceStaff)
            .DisplayMember = "StaffName"
            .ValueMember = "UserID"
            .SelectedIndex = -1
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
        StaffResponsible.SelectedValue = CurrentUser.UserID

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

#End Region

#Region " Permissions "

    Private Sub SetUserPermissions()
        If CurrentUser.HasPermission(UserCan.ResolveEnforcement) Then
            ' Enable full access to resolve/delete/submit to EPA
            ResolvedCheckBox.Enabled = True
            DeleteEnforcementMenuItem.Enabled = True
            SubmitToEpa.Enabled = True
            SubmitToEpa2.Enabled = True

        ElseIf Not CurrentUser.HasPermission(UserCan.SaveEnforcement) Then
            ' Disable any save/write access
            DisableWriteAccess()
        End If
    End Sub

    Private Sub DisableAllIfDeleted()
        If EnforcementCase.IsDeleted Then
            With EnforcementStatusDisplay
                .Text = "DELETED"
                .BackColor = IaipColors.WarningBackColor
                .ForeColor = IaipColors.WarningForeColor
            End With

            EnableDisableChanges(EnableOrDisable.Disable)

            DisableWriteAccess()
        End If
    End Sub

    Private Sub DisableWriteAccess()
        ' Disable any save/write access
        SaveButton.Enabled = False
        SaveMenuItem.Enabled = False
        SubmitToUC.Enabled = False
        AddPollutantsButton.Enabled = False
        StipulatedPenaltyControls.Enabled = False
        DocumentUpdateButton.Enabled = False
        LinkToEvent.Enabled = False
        RemoveLinkedEvent.Enabled = False
        ResolvedCheckBox.Enabled = False
        DeleteEnforcementMenuItem.Enabled = False
        SubmitToEpa.Enabled = False
        SubmitToEpa2.Enabled = False
    End Sub

#End Region

#Region " Load initial data "

    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey(FormParameter.EnforcementId) Then EnforcementId = CInt(Parameters(FormParameter.EnforcementId))
            If Parameters.ContainsKey(FormParameter.AirsNumber) Then AirsNumber = New ApbFacilityId(Parameters(FormParameter.AirsNumber))
            If Parameters.ContainsKey(FormParameter.TrackingNumber) Then InitialLinkedEventId = CInt(Parameters(FormParameter.TrackingNumber))
        End If
    End Sub

    Private Sub LoadCurrentEnforcement()
        If EnforcementId = 0 Then Exit Sub
        EnforcementCase = DAL.Sscp.GetEnforcementCase(EnforcementId)
        If EnforcementCase Is Nothing Then
            MessageBox.Show("Invalid enforcement number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End If
        AirsNumber = EnforcementCase.AirsNumber
        ShowCorrectTabs()
        LoadDocuments()
    End Sub

    Private Sub LoadInitialLinkedEvent()
        Dim workItem As DataRow = DAL.Sscp.GetWorkItemBasics(InitialLinkedEventId)
        AddLinkedEventToList(workItem)
    End Sub

    Private Sub ShowCorrectTabs()
        With EnforcementCase
            LonCheckBox.Checked = .EnforcementActions.Contains(EnforcementActionType.LON)
            NovCheckBox.Checked = .EnforcementActions.Contains(EnforcementActionType.NOV)
            COCheckBox.Checked = .EnforcementActions.Contains(EnforcementActionType.CO)
            AOCheckBox.Checked = .EnforcementActions.Contains(EnforcementActionType.AO)
        End With
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
        FacilityNotApprovedDisplay.Visible = CBool(Not Facility.ApprovedByApb)
    End Sub

    Private Sub DisplayEnforcementCase()
        If EnforcementCase Is Nothing OrElse EnforcementCase.EnforcementId = 0 Then
            Text = "New Enforcement for " & AirsNumber.FormattedString & ", " & Facility.FacilityName
            EnforcementIdDisplay.Text = "New enforcement (unsaved)"
        Else
            With EnforcementCase
                ' Form
                Text = "Enforcement #" & .EnforcementId.ToString() & " — " & AirsNumber.FormattedString & ", " & Facility.FacilityName
                ShowAuditHistoryMenuItem.Visible = True
                ShowEpaActionNumbersMenuItem.Visible = True
                EnforcementToolStripSeparator.Visible = True
                DeleteEnforcementMenuItem.Visible = True
                DeleteEnforcementToolStripSeparator.Visible = True

                ' Header
                EnforcementIdDisplay.Text = "Enforcement #" & .EnforcementId.ToString()
                EnforcementStatusDisplay.Visible = True
                EnforcementStatusDisplay.Text = .EnforcementStatus.GetDescription
                ColorCodeEnforcementStatusDisplay()
                ResolvedCheckBox.Visible = True
                ResolvedDate.Visible = True
                ResolvedCheckBox.Checked = CBool(Not .Open)
                ResolvedDate.Checked = CBool(Not .Open)
                StaffResponsible.SelectedValue = .StaffResponsibleId

                ' General tab
                If .DateModified.HasValue Then
                    LastEditedDateDisplay.Visible = True
                    LastEditedDateDisplay.Text = "Last edited on " & .DateModified.Value.ToString(DateFormat)
                End If

                If Not EnforcementCase.SubmittedToUc Then
                    SubmitToUC.Visible = True
                End If

                LoadViolationType()
                GeneralComments.Text = .Comment

                ' LON
                If .EnforcementActions.Contains(EnforcementActionType.LON) Then LonComments.Text = .LonComment

                ' NOV
                If .EnforcementActions.Contains(EnforcementActionType.NOV) Then NovComments.Text = .NovComment

                ' CO
                If .EnforcementActions.Contains(EnforcementActionType.CO) Then
                    COComments.Text = .CoComment
                    If .CoNumber <> "" AndAlso .CoNumber.Contains("-"c) Then
                        Dim parts As String() = .CoNumber.Split("-"c)
                        If IsNumeric(parts(parts.Length - 1)) Then
                            CoNumber.Value = Math.Min(Convert.ToInt32(parts(parts.Length - 1)), 999999)
                        End If
                    Else
                        If IsNumeric(.CoNumber) Then
                            CoNumber.Value = Math.Min(Convert.ToInt32(.CoNumber), 999999)
                        End If
                    End If

                    COPenaltyAmount.Text = .CoPenaltyAmount.ToString("C")
                    COPenaltyComments.Text = .CoPenaltyAmountComment
                    StipulatedPenaltiesGroupBox.Enabled = True
                    LoadStipulatedPenalties()
                End If

                ' AO
                If .EnforcementActions.Contains(EnforcementActionType.AO) Then AOComments.Text = .AoComment

                ' All nullable Dates
                DisplayNullableDates()

                If EnforcementTabs.TabPages.Contains(EpaValuesTabPage) Then
                    DisplayEpaValues()
                End If

            End With
        End If
    End Sub

    Private Sub DisplayNullableDates()

        Dim nullableDates As New Dictionary(Of DateTimePicker, Date?) From {
            {ResolvedDate, EnforcementCase.DateFinalized},
            {AOAppealed, EnforcementCase.AoAppealed},
            {AOExecuted, EnforcementCase.AoExecuted},
            {AOResolved, EnforcementCase.AoResolved},
            {COExecuted, EnforcementCase.CoExecuted},
            {COProposed, EnforcementCase.CoProposed},
            {COReceivedfromCompany, EnforcementCase.CoReceivedFromCompany},
            {COReceivedFromDirector, EnforcementCase.CoReceivedFromDirector},
            {COResolved, EnforcementCase.CoResolved},
            {COToPM, EnforcementCase.CoToPm},
            {COToUC, EnforcementCase.CoToUc},
            {DiscoveryDate, EnforcementCase.DiscoveryDate},
            {LonResolved, EnforcementCase.LonResolved},
            {LonSent, EnforcementCase.LonSent},
            {LonToUC, EnforcementCase.LonToUc},
            {NfaSent, EnforcementCase.NfaSent},
            {NfaToPM, EnforcementCase.NfaToPm},
            {NfaToUC, EnforcementCase.NfaToUc},
            {NovResponseReceived, EnforcementCase.NovResponseReceived},
            {NovSent, EnforcementCase.NovSent},
            {NovToPM, EnforcementCase.NovToPm},
            {NovToUC, EnforcementCase.NovToUc}
        }

        For Each kvp As KeyValuePair(Of DateTimePicker, Date?) In nullableDates
            If kvp.Value.HasValue Then
                kvp.Key.Checked = True
                kvp.Key.Value = kvp.Value.Value
            End If
        Next
    End Sub

    Private Sub ColorCodeEnforcementStatusDisplay()
        With EnforcementStatusDisplay
            Select Case EnforcementCase.EnforcementStatus
                Case EnforcementStatus.CaseClosed
                    .BackColor = Color.Empty
                    .ForeColor = Color.Empty
                Case EnforcementStatus.CaseOpen, EnforcementStatus.CaseResolved, EnforcementStatus.SubjectToComplianceSchedule
                    .BackColor = IaipColors.InfoBackColor
                    .ForeColor = IaipColors.InfoForeColor
            End Select
        End With
    End Sub

#End Region

#Region " Header panel "

    Private Sub ResolvedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ResolvedCheckBox.CheckedChanged
        If ResolvedCheckBox.Checked Then
            ResolvedDate.Enabled = True
            ResolvedDate.Checked = True
            EnforcementCase.Open = OpenOrClosed.Closed
            EnableDisableChanges(EnableOrDisable.Disable)
        Else
            ResolvedDate.Enabled = False
            ResolvedDate.Checked = False
            EnforcementCase.Open = OpenOrClosed.Open
            EnableDisableChanges(EnableOrDisable.Enable)
        End If
    End Sub

    Private Sub EnableDisableChanges(enabler As EnableOrDisable)
        Dim enabled As Boolean = (enabler = EnableOrDisable.Enable)

        'InfoTabPage.Enabled = enabled
        DiscoveryDate.Enabled = enabled
        StaffResponsible.Enabled = enabled
        GeneralComments.Enabled = enabled
        LinkToEvent.Enabled = enabled
        RemoveLinkedEvent.Enabled = enabled
        ViolationTypeGroupbox.Enabled = enabled
        SubmitToEpa.Enabled = enabled
        SubmitToUC.Enabled = enabled

        PollutantsTabPage.Enabled = enabled
        LonTabPage.Enabled = enabled
        NovTabPage.Enabled = enabled
        COTabPage.Enabled = enabled
        AOTabPage.Enabled = enabled
        DocumentsTabPage.Enabled = enabled
        EpaValuesTabPage.Enabled = enabled
        EnforcementTypePanel.Enabled = enabled
    End Sub

    Private Sub AirsNumberDisplay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles AirsNumberDisplay.LinkClicked
        OpenFormFacilitySummary(AirsNumber)
    End Sub

#End Region

#Region " Message panel "

    Private Sub DisplayGeneralMessage()
        GeneralMessage.Display(GeneralMessageDisplay)
        DismissMessageButton.Visible = True
        DismissMessageButton.BringToFront()
        ClearMessageMenuItem.Enabled = True

        Dim oldFormHeight As Integer = Me.Height

        Dim oldMessageHeight As Integer = 0
        If GeneralMessagePanel.Visible Then
            oldMessageHeight = GeneralMessagePanel.Height
        End If

        Dim numLines As Integer = GeneralMessage.MessageText.LineCount()
        GeneralMessagePanel.Height = (numLines * 15) + 28

        Me.MinimumSize = New Size(747, 580 + GeneralMessagePanel.Height)
        If Me.WindowState = FormWindowState.Normal Then
            Me.Height = oldFormHeight + GeneralMessagePanel.Height - oldMessageHeight
        End If

        GeneralMessagePanel.Visible = True
    End Sub

    Private Sub ClearGeneralMessage()
        If GeneralMessage IsNot Nothing Then GeneralMessage.Clear()
        'DismissMessageButton.Visible = False
        ClearMessageMenuItem.Enabled = False

        Me.MinimumSize = New Size(747, 580)
        If GeneralMessagePanel.Visible And Me.WindowState = FormWindowState.Normal Then
            Me.Height = Me.Height - GeneralMessagePanel.Height
        End If

        GeneralMessagePanel.Visible = False
    End Sub

#End Region

#Region " General tab: Violation Types "

    Private Sub LoadViolationType()
        Dim severityCode As String
        Dim rowFilter As String

        If String.IsNullOrEmpty(EnforcementCase.ViolationType) Then
            severityCode = "BLANK"
            EnforcementCase.ViolationType = "BLANK"
        Else
            Dim dr As DataRow = violationTypes.Rows.Find(EnforcementCase.ViolationType)
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
        Dim dv As New DataView(violationTypes) With {
            .RowFilter = rowFilter,
            .Sort = "VIOLATIONTYPEDESC ASC"
        }

        With ViolationTypeSelect
            .DataSource = dv
            .ValueMember = "AIRVIOLATIONTYPECODE"
            .DisplayMember = "VIOLATIONTYPEDESC"
        End With
    End Sub

    Private Sub ViolationType_CheckedChanged(sender As Object, e As EventArgs) _
        Handles ViolationTypeFrv.CheckedChanged, ViolationTypeHpv.CheckedChanged,
        ViolationTypeNonFrv.CheckedChanged, ViolationTypeNone.CheckedChanged

        If Not CType(sender, RadioButton).Checked Then
            ' Prevents sub from firing twice (once for unchecking one button and once for checking another)
            Exit Sub
        End If

        Dim rowFilter As String = Nothing

        ViolationTypeSelect.SelectedValue = "BLANK"

        If ViolationTypeNone.Checked Then
            ViolationTypeSelect.Enabled = False
        End If

        If ViolationTypeFrv.Checked Then
            rowFilter = "(SEVERITYCODE='FRV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
        End If

        If ViolationTypeHpv.Checked Then
            rowFilter = "(SEVERITYCODE='HPV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
        End If

        If ViolationTypeNonFrv.Checked Then
            rowFilter = "(SEVERITYCODE<>'FRV' AND SEVERITYCODE<>'HPV' AND DEPRECATED = 'FALSE') OR " &
                "(AIRVIOLATIONTYPECODE = 'BLANK')"
        End If

        If rowFilter IsNot Nothing Then
            ViolationTypeSelect.Enabled = True
            ApplyViolationSelectFilter(rowFilter)
        End If
    End Sub

#End Region

#Region " General tab: Linked compliance events "

    Private Sub LoadLinkedEvents()
        Dim dt As DataTable = DAL.Sscp.GetLinkedComplianceEvents(EnforcementId)
        With LinkedEvents
            .DataSource = dt
            .SanelyResizeColumns
            .ClearSelection()
        End With
    End Sub

    Private Sub LinkedEvents_SelectionChanged(sender As Object, e As EventArgs) Handles LinkedEvents.SelectionChanged
        If LinkedEvents.SelectedRows.Count = 1 Then
            OpenLinkedEvent.Visible = True
            RemoveLinkedEvent.Visible = True
        Else
            OpenLinkedEvent.Visible = False
            RemoveLinkedEvent.Visible = False
        End If
    End Sub

    Private Sub LinkToEvent_Click(sender As Object, e As EventArgs) Handles LinkToEvent.Click
        Dim discoveryEventDialog As New SSCPEnforcementChecklist
        With discoveryEventDialog
            .AirsNumber = AirsNumber
            .EnforcementNumber = EnforcementId
            .ShowDialog()
        End With

        If discoveryEventDialog.DialogResult = DialogResult.OK Then
            Dim linkedEventId As Integer = discoveryEventDialog.SelectedDiscoveryEvent
            Dim workItem As DataRow = DAL.Sscp.GetWorkItemBasics(linkedEventId)
            If workItem Is Nothing Then
                GeneralMessage = New IaipMessage("No compliance discovery event was selected.", IaipMessage.WarningLevels.Warning)
            Else
                If EnforcementId = 0 Then
                    AddLinkedEventToList(workItem)
                    GeneralMessage = New IaipMessage("The compliance discovery event was linked. All linked events will be saved when the current enforcement is saved.", IaipMessage.WarningLevels.Success)
                Else
                    If SaveLinkedEvent(CInt(workItem("Tracking #"))) Then
                        AddLinkedEventToList(workItem)
                        GeneralMessage = New IaipMessage("The compliance discovery event was linked.", IaipMessage.WarningLevels.Success)
                    Else
                        GeneralMessage = New IaipMessage("There was an error linking the compliance discovery event.", IaipMessage.WarningLevels.ErrorReport)
                    End If
                End If
            End If
        End If

        discoveryEventDialog.Dispose()
    End Sub

    Private Sub AddLinkedEventToList(workItem As DataRow)
        Dim dt As DataTable = CType(LinkedEvents.DataSource, DataTable)
        Dim dr As DataRow = dt.NewRow()
        dr("Tracking #") = workItem("Tracking #")
        dr("Type") = workItem("Type")
        dr("Event Date") = workItem("Event Date")
        dr("AIRS #") = workItem("AIRS #")
        dr("Date Linked") = Today
        dt.Rows.Add(dr)
    End Sub

    Private Sub OpenLinkedEvent_Click(sender As Object, e As EventArgs) Handles OpenLinkedEvent.Click
        If LinkedEvents.SelectedRows.Count = 1 Then
            OpenFormSscpWorkItem(LinkedEvents.SelectedRows.Item(0).Cells("Tracking #").Value.ToString)
        End If
    End Sub

    Private Sub RemoveLinkedEvent_Click(sender As Object, e As EventArgs) Handles RemoveLinkedEvent.Click
        If LinkedEvents.SelectedRows.Count = 1 Then
            If EnforcementId = 0 Then
                LinkedEvents.Rows.RemoveAt(LinkedEvents.CurrentRow.Index)
                GeneralMessage = New IaipMessage("The linked compliance event was removed.", IaipMessage.WarningLevels.Success)
            Else
                If DAL.Sscp.DeleteLinkedComplianceEvent(EnforcementId, CInt(LinkedEvents.SelectedRows.Item(0).Cells("Tracking #").Value)) Then
                    LinkedEvents.Rows.RemoveAt(LinkedEvents.CurrentRow.Index)
                    GeneralMessage = New IaipMessage("The linked compliance event was removed.", IaipMessage.WarningLevels.Success)
                Else
                    LoadLinkedEvents()
                    GeneralMessage = New IaipMessage("An error occurred removing the linked compliance event. Please check the list and try again.", IaipMessage.WarningLevels.ErrorReport)
                End If
            End If
        Else
            OpenLinkedEvent.Visible = False
            RemoveLinkedEvent.Visible = False
        End If
    End Sub

    Private Function SaveAllLinkedEvents() As Boolean
        Dim success As Boolean = True
        For Each row As DataGridViewRow In LinkedEvents.Rows
            success = success And SaveLinkedEvent(CInt(row.Cells("Tracking #").Value))
        Next
        Return success
    End Function

    Private Function SaveLinkedEvent(trackingNumber As Integer) As Boolean
        Return DAL.Sscp.SaveLinkedComplianceEvent(EnforcementId, trackingNumber)
    End Function

    Private Sub LinkedEvents_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles LinkedEvents.CellFormatting
        If e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
            If LinkedEvents.Columns(e.ColumnIndex).HeaderText.ToUpper = "AIRS #" AndAlso ApbFacilityId.IsValidAirsNumberFormat(e.Value.ToString) Then
                e.Value = New ApbFacilityId(e.Value.ToString).FormattedString
            ElseIf TypeOf e.Value Is Date Then
                e.CellStyle.Format = DateFormat
            End If
        End If
    End Sub

    Private Sub LinkedEvents_MouseUp(sender As Object, e As MouseEventArgs) Handles LinkedEvents.MouseUp
        ' See if the left mouse button was clicked
        If e.Button = MouseButtons.Left Then
            If LinkedEvents.HitTest(e.X, e.Y).Type = DataGridViewHitTestType.None Then
                LinkedEvents.ClearSelection()
            End If
        End If
    End Sub

#End Region

#Region " General tab: Enforcement action checkboxes "

    Private Sub LonCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LonCheckBox.CheckedChanged
        If LonCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(LonTabPage)
            AOCheckBox.Enabled = False
            COCheckBox.Enabled = False
            NovCheckBox.Enabled = False
            ViolationTypeNone.Checked = True
            ViolationTypeGroupbox.Visible = False
            If Not EnforcementCase.SubmittedToUc And EnforcementId > 0 Then
                SubmitToUC.Visible = True
            End If
        Else
            EnforcementTabs.TabPages.Remove(LonTabPage)
            AOCheckBox.Enabled = True
            COCheckBox.Enabled = True
            NovCheckBox.Enabled = True
            SubmitToUC.Visible = False
        End If
    End Sub

    Private Sub NovCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles NovCheckBox.CheckedChanged
        If NovCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(NovTabPage)
            EnableCaseFileTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(NovTabPage)
            If Not (COCheckBox.Checked Or AOCheckBox.Checked) Then
                EnableCaseFileTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub COCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles COCheckBox.CheckedChanged
        If COCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(COTabPage)
            EnableCaseFileTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(COTabPage)
            If Not (NovCheckBox.Checked Or AOCheckBox.Checked) Then
                EnableCaseFileTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub AOCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AOCheckBox.CheckedChanged
        If AOCheckBox.Checked Then
            EnforcementTabs.TabPages.Add(AOTabPage)
            EnableCaseFileTools(EnableOrDisable.Enable)
        Else
            EnforcementTabs.TabPages.Remove(AOTabPage)
            If Not (COCheckBox.Checked Or NovCheckBox.Checked) Then
                EnableCaseFileTools(EnableOrDisable.Disable)
            End If
        End If
    End Sub

    Private Sub EnableCaseFileTools(enabler As EnableOrDisable)
        If enabler = EnableOrDisable.Enable Then
            LonCheckBox.Enabled = False
            ViolationTypeGroupbox.Visible = True
            If Not EnforcementCase.SubmittedToUc And EnforcementId > 0 Then
                SubmitToUC.Visible = True
            End If
            If Not EnforcementCase.SubmittedToEpa And EnforcementId > 0 Then
                SubmitToEpa.Visible = True
                SubmitToEpa2.Visible = True
                NotSubmittedToEpaLabel.Visible = True
            End If
            If Not EnforcementTabs.TabPages.Contains(PollutantsTabPage) Then EnforcementTabs.TabPages.Insert(1, PollutantsTabPage)
        Else
            ViolationTypeGroupbox.Visible = False
            SubmitToUC.Visible = False
            SubmitToEpa.Visible = False
            SubmitToEpa2.Visible = False
            NotSubmittedToEpaLabel.Visible = False
            LonCheckBox.Enabled = True
            If EnforcementTabs.TabPages.Contains(PollutantsTabPage) Then EnforcementTabs.TabPages.Remove(PollutantsTabPage)
        End If
    End Sub

#End Region

#Region " General tab: Submit to UC/EPA buttons "

    Private Sub SubmitToUC_Click(sender As Object, e As EventArgs) Handles SubmitToUC.Click
        If EnforcementCase.SubmittedToUc = True Then Exit Sub

        EnforcementCase.SubmittedToUc = True
        If ValidateAndSave() Then
            SubmitToUC.Visible = False
        Else
            EnforcementCase.SubmittedToUc = False
        End If
    End Sub

    Private Sub SubmitToEpa_Click(sender As Object, e As EventArgs) _
        Handles SubmitToEpa.Click, SubmitToEpa2.Click

        If EnforcementCase.SubmittedToEpa = True Then
            SubmitToEpa.Visible = False
            SubmitToEpa2.Visible = False
            ShowEpaValues()
            Exit Sub
        End If

        If Not CurrentUser.HasPermission(UserCan.ResolveEnforcement) Then
            GeneralMessage = New IaipMessage("You do not have sufficent permission to submit enforcement case to EPA.", IaipMessage.WarningLevels.ErrorReport)
            Exit Sub
        End If

        EnforcementCase.SubmittedToEpa = True

        If ValidateAndSave() Then
            SubmitToEpa.Visible = False
            SubmitToEpa2.Visible = False
            ShowEpaValues()
        Else
            EnforcementCase.SubmittedToEpa = False
        End If
    End Sub

#End Region

#Region " Pollutants/Programs tab "

    Public Sub LoadFacilityPollutants()
        ' All available pollutants for facility
        Dim dt As DataTable = DAL.GetFacilityPollutants(AirsNumber)
        PollutantsListView.Items.Clear()
        For Each row As DataRow In dt.Rows
            PollutantsListView.Items.Add(New ListViewItem({row(1).ToString, row(0).ToString}))
        Next
    End Sub

    Public Sub DisplayEnforcementPollutants()
        ' Pollutants associated with this case
        If EnforcementId = 0 OrElse EnforcementCase Is Nothing Then Exit Sub
        If EnforcementCase.Pollutants IsNot Nothing Then
            For i As Integer = 0 To PollutantsListView.Items.Count - 1
                If EnforcementCase.Pollutants.Contains(PollutantsListView.Items(i).SubItems(1).Text) Then
                    PollutantsListView.Items.Item(i).Checked = True
                End If
            Next
        End If
    End Sub

    Private Sub LoadFacilityAirPrograms()
        ' All available air programs
        Dim dt As DataTable = DAL.GetFacilityAirProgramsAsDataTable(AirsNumber, True)
        ProgramsListView.Items.Clear()
        For Each row As DataRow In dt.Rows
            ProgramsListView.Items.Add(New ListViewItem({row(1).ToString, row(0).ToString}))
        Next
    End Sub

    Private Sub DisplayEnforcementAirPrograms()
        ' Programs associated with this case
        If EnforcementId = 0 OrElse EnforcementCase Is Nothing Then Exit Sub
        If EnforcementCase.AirPrograms IsNot Nothing Then
            For i As Integer = 0 To ProgramsListView.Items.Count - 1
                If EnforcementCase.AirPrograms.Contains(ProgramsListView.Items(i).SubItems(1).Text) Then
                    ProgramsListView.Items.Item(i).Checked = True
                End If
            Next
        End If
    End Sub

    Private Sub EditAirProgramPollutantsButton_Click(sender As Object, e As EventArgs) Handles AddPollutantsButton.Click
        Using editProgPollDialog As New IAIPEditAirProgramPollutants
            With editProgPollDialog

                .AirsNumber = AirsNumber
                .FacilityName = Facility.FacilityName & ", " & Facility.DisplayCity
                .ShowDialog()

                If .SomethingChanged Then
                    ' Get sets of existing/checked pollutants
                    Dim existingPollutantsSet As New HashSet(Of String)
                    For Each pi As ListViewItem In PollutantsListView.Items
                        existingPollutantsSet.Add(pi.SubItems(1).Text)
                    Next

                    Dim checkedPollutantsSet As New HashSet(Of String)
                    For Each pi As ListViewItem In PollutantsListView.CheckedItems
                        checkedPollutantsSet.Add(pi.SubItems(1).Text)
                    Next

                    ' Reload facility pollutants list
                    LoadFacilityPollutants()

                    ' Uncheck all 
                    For Each lvi As ListViewItem In PollutantsListView.Items
                        lvi.Checked = False
                    Next

                    ' Remove previously existing pollutants (existingPollutantsSet) from new set of pollutants (.FacilityPollutantsSet)
                    .FacilityPollutantsSet.ExceptWith(existingPollutantsSet)

                    ' Add new pollutants to set of checked pollutants
                    checkedPollutantsSet.UnionWith(.FacilityPollutantsSet)

                    ' Check all pollutants in new set of checked pollutants
                    If checkedPollutantsSet.Count > 0 Then
                        For i As Integer = 0 To PollutantsListView.Items.Count - 1
                            If checkedPollutantsSet.Contains(PollutantsListView.Items(i).SubItems(1).Text) Then
                                PollutantsListView.Items.Item(i).Checked = True
                            End If
                        Next
                    End If

                End If

            End With
        End Using
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
        StipulatedPenalties.SanelyResizeColumns

        If EnforcementTabs.TabPages.Contains(EpaValuesTabPage) Then DisplayEpaValues()
    End Sub

    Private Sub SaveNewStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles SaveNewStipulatedPenaltyButton.Click
        If EnforcementId = 0 Then
            GeneralMessage = New IaipMessage("Current enforcement must be saved before saving stipulated penalties.", IaipMessage.WarningLevels.Warning)
        ElseIf String.IsNullOrEmpty(StipulatedPenaltyAmount.Text) Then
            GeneralMessage = New IaipMessage("Enter a stipulated penalty amount first.", IaipMessage.WarningLevels.Warning)
        ElseIf Not StringValidatesAsCurrency(StipulatedPenaltyAmount.Text) Then
            GeneralMessage = New IaipMessage("Stipulated penalty amount must be a number.", IaipMessage.WarningLevels.ErrorReport)
        Else
            SaveNewStipulatedPenalty()
        End If
    End Sub

    Private Sub SaveNewStipulatedPenalty()
        Dim result As Boolean = DAL.Sscp.SaveNewStipulatedPenalty(EnforcementId, AirsNumber, ConvertCurrencyStringToDecimal(StipulatedPenaltyAmount.Text), StipulatedPenaltyComments.Text)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
            GeneralMessage = New IaipMessage("Stipulated penalty saved.", IaipMessage.WarningLevels.Success)
        Else
            GeneralMessage = New IaipMessage("Error: There was an error saving the new stipulated penalty.", IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Sub UpdateStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles UpdateStipulatedPenaltyButton.Click
        If EnforcementId = 0 Then
            GeneralMessage = New IaipMessage("Current enforcement must be saved before modifying stipulated penalties.", IaipMessage.WarningLevels.Warning)
        ElseIf selectedStipulatedPenaltyItem = 0 Then
            GeneralMessage = New IaipMessage("Select an existing stipulated penalty first.", IaipMessage.WarningLevels.Warning)
        ElseIf String.IsNullOrEmpty(StipulatedPenaltyAmount.Text) Then
            GeneralMessage = New IaipMessage("Enter a stipulated penalty amount first.", IaipMessage.WarningLevels.Warning)
        ElseIf Not StringValidatesAsCurrency(StipulatedPenaltyAmount.Text) Then
            GeneralMessage = New IaipMessage("Stipulated penalty amount must be a number.", IaipMessage.WarningLevels.Warning)
        Else
            UpdateStipulatedPenalty()
        End If
    End Sub

    Private Sub UpdateStipulatedPenalty()
        Dim result As Boolean = DAL.Sscp.UpdateStipulatedPenalty(EnforcementId, ConvertCurrencyStringToDecimal(StipulatedPenaltyAmount.Text), StipulatedPenaltyComments.Text, selectedStipulatedPenaltyItem)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
            GeneralMessage = New IaipMessage("Stipulated penalty saved.", IaipMessage.WarningLevels.Success)
        Else
            GeneralMessage = New IaipMessage("Error: There was an error updating the stipulated penalty.", IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Sub DeleteStipulatedPenalty_Click(sender As Object, e As EventArgs) Handles DeleteStipulatedPenaltyButton.Click
        If EnforcementId = 0 Then
            GeneralMessage = New IaipMessage("Current enforcement must be saved before modifying stipulated penalties.", IaipMessage.WarningLevels.Warning)
        ElseIf selectedStipulatedPenaltyItem = 0 Then
            GeneralMessage = New IaipMessage("Select an existing stipulated penalty first.", IaipMessage.WarningLevels.Warning)
        Else
            DeleteSelectedStipulatedPenalty()
        End If
    End Sub

    Private Sub DeleteSelectedStipulatedPenalty()
        Dim result As Boolean = DAL.Sscp.DeleteStipulatedPenalty(EnforcementId, selectedStipulatedPenaltyItem)

        If result Then
            LoadStipulatedPenalties()
            ClearStipulatedPenaltyForm()
            GeneralMessage = New IaipMessage("Stipulated penalty deleted.", IaipMessage.WarningLevels.Success)
        Else
            GeneralMessage = New IaipMessage("Error: There was an error deleting the stipulated penalty.", IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Sub ClearStipulatedPenaltyFormButton_Click(sender As Object, e As EventArgs) Handles ClearStipulatedPenaltyFormButton.Click
        ClearStipulatedPenaltyForm()
    End Sub

    Private Sub StipulatedPenalties_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles StipulatedPenalties.CellClick

        If e.RowIndex <> -1 And e.RowIndex < StipulatedPenalties.RowCount Then
            selectedStipulatedPenaltyItem = CInt(StipulatedPenalties.Rows(e.RowIndex).Cells("STRENFORCEMENTKEY").Value)
            Dim sp As String = StipulatedPenalties.Rows(e.RowIndex).Cells("STRSTIPULATEDPENALTY").Value.ToString
            If StringValidatesAsCurrency(sp) Then
                StipulatedPenaltyAmount.Text = ConvertCurrencyStringToDecimal(sp).ToString("C")
            End If
            StipulatedPenaltyComments.Text = StipulatedPenalties.Rows(e.RowIndex).Cells("STRSTIPULATEDPENALTYCOMMENTS").Value.ToString
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
        selectedStipulatedPenaltyItem = 0
    End Sub

#End Region

#Region " Documents tab "

#Region " Display Document files "

    Private Sub LoadDocuments()
        EnableOrDisableDocuments(EnableOrDisable.Disable)
        DocumentList.DataSource = Nothing
        existingFiles = GetEnforcementDocumentsAsList(EnforcementCase.EnforcementId)
        If existingFiles.Count > 0 Then
            lblCurrentFiles.Text = "Current Documents"
            With DocumentList
                .DataSource = New BindingSource(existingFiles, Nothing)
                .Enabled = True
                .ClearSelection()
            End With
        Else
            DocumentList.Visible = False
            lblCurrentFiles.Text = "No documents are attached to this enforcement case."
        End If
    End Sub

    Private Sub EnableOrDisableDocuments(enabler As EnableOrDisable)
        Dim enabled As Boolean = (enabler = EnableOrDisable.Enable)
        With pnlDocument
            .Enabled = enabled
            .Visible = enabled
        End With
        If enabled Then
            txtDocumentDescription.Text = DocumentList.CurrentRow.Cells("Comment").Value.ToString
            lblDocumentName.Text = DocumentList.CurrentRow.Cells("FileName").Value.ToString
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

    Private Sub dataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DocumentList.CellFormatting
        If TypeOf e.CellStyle.FormatProvider Is ICustomFormatter Then
            e.Value = TryCast(e.CellStyle.FormatProvider.GetFormat(GetType(ICustomFormatter)), ICustomFormatter).Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvDocumentList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DocumentList.DataBindingComplete
        FormatDocumentList()
        DocumentList.SanelyResizeColumns()
        DocumentList.ClearSelection()
    End Sub

#End Region

#Region " Document update/download/delete "

    Private Sub dgvDocumentList_SelectionChanged(sender As Object, e As EventArgs) Handles DocumentList.SelectionChanged
        If DocumentList.SelectedRows.Count = 1 Then
            EnableOrDisableDocuments(EnableOrDisable.Enable)
        Else
            EnableOrDisableDocuments(EnableOrDisable.Disable)
        End If
    End Sub

    Private Sub btnDocumentDownload_Click(sender As Object, e As EventArgs) Handles btnDocumentDownload.Click
        If Message IsNot Nothing Then Message.Clear()

        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(DocumentList.CurrentRow)
        Me.Message = New IaipMessage(GetDocumentMessage(DocumentMessageType.DownloadingFile))

        Dim canceled As Boolean = False
        Dim downloaded As Boolean = DownloadDocument(doc, canceled, Me)
        If downloaded Or canceled Then
            If Message IsNot Nothing Then Message.Clear()
        Else
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.DownloadFailure), lblDocumentName), IaipMessage.WarningLevels.ErrorReport)
        End If
    End Sub

    Private Sub btnDocumentUpdate_Click(sender As Object, e As EventArgs) Handles DocumentUpdateButton.Click
        Dim doc As EnforcementDocument = EnforcementDocumentFromFileListRow(DocumentList.CurrentRow)
        doc.Comment = txtDocumentDescription.Text
        Dim updated As Boolean = UpdateEnforcementDocument(doc, Me)
        If updated Then
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.UpdateSuccess), doc.FileName))
            LoadDocuments()
        Else
            Me.Message = New IaipMessage(String.Format(GetDocumentMessage(DocumentMessageType.UpdateFailure), lblDocumentName))
        End If
    End Sub

    Private Function EnforcementDocumentFromFileListRow(row As DataGridViewRow) As EnforcementDocument
        Dim doc As New EnforcementDocument
        With doc
            .EnforcementNumber = CInt(row.Cells("EnforcementNumber").Value)
            .BinaryFileId = CInt(row.Cells("BinaryFileId").Value)
            .Comment = row.Cells("Comment").Value.ToString
            .DocumentId = CInt(row.Cells("DocumentId").Value)
            .DocumentType = row.Cells("DocumentType").Value.ToString
            .DocumentTypeId = CInt(row.Cells("DocumentTypeId").Value)
            .FileName = row.Cells("FileName").Value.ToString
            .FileSize = CInt(row.Cells("FileSize").Value)
            .UploadDate = DateTime.Parse(row.Cells("UploadDate").Value.ToString)
        End With
        Return doc
    End Function

#End Region

#Region " Accept Button (Documents) "

    Private Sub NoAcceptButton(sender As Object, e As EventArgs) _
    Handles txtDocumentDescription.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub FileProperties_Enter(sender As Object, e As EventArgs) _
    Handles txtDocumentDescription.Enter
        Me.AcceptButton = DocumentUpdateButton
    End Sub

#End Region

#End Region

#Region " Audit History tab "

    Private Sub RefreshAuditHistory_Click(sender As Object, e As EventArgs) Handles RefreshAuditHistory.Click
        LoadAuditData()
    End Sub

    Private Sub ExportAuditHistory_Click(sender As Object, e As EventArgs) Handles ExportAuditHistory.Click
        AuditHistory.ExportToExcel(Me)
    End Sub

    Private Sub ShowAuditHistory()
        If Not EnforcementTabs.TabPages.Contains(AuditHistoryTabPage) Then EnforcementTabs.TabPages.Add(AuditHistoryTabPage)
        EnforcementTabs.SelectTab(AuditHistoryTabPage)
        LoadAuditData()
    End Sub

    Private Sub LoadAuditData()
        AuditHistory.DataSource = DAL.Sscp.GetEnforcementAuditHistory(EnforcementId)

        AuditHistory.Columns("StaffResponsible").HeaderText = "Staff Responsible"
        AuditHistory.Columns("STRTRACKINGNUMBER").HeaderText = "Linked event"
        AuditHistory.Columns("DATENFORCEMENTFINALIZED").HeaderText = "Date Closed"
        AuditHistory.Columns("DATENFORCEMENTFINALIZED").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRSTATUS").HeaderText = "Submitted to UC"
        AuditHistory.Columns("STRACTIONTYPE").HeaderText = "Action Type"
        AuditHistory.Columns("STRGENERALCOMMENTS").HeaderText = "Comments"
        AuditHistory.Columns("DATDISCOVERYDATE").HeaderText = "Date Discovered"
        AuditHistory.Columns("DATDISCOVERYDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("DATDAYZERO").HeaderText = "Day Zero"
        AuditHistory.Columns("DATDAYZERO").DefaultCellStyle.Format = "dd-MMM-yyyy"
        AuditHistory.Columns("STRHPV").HeaderText = "Violation Type"
        AuditHistory.Columns("STRPOLLUTANTS").HeaderText = "Pollutants"
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
        AuditHistory.Columns("DATCORECEIVEDFROMCOMPANY").HeaderText = "Date CO Received From Company"
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
        AuditHistory.Columns("DATMODIFINGDATE").DefaultCellStyle.Format = "dd-MMM-yyyy HH:mm:ss"
        AuditHistory.Columns("IsDeleted").HeaderText = "Deleted"

        AuditHistory.SanelyResizeColumns()
    End Sub

#End Region

#Region " EPA values tab "

    Private Sub ShowEpaValues()
        If Not EnforcementTabs.TabPages.Contains(EpaValuesTabPage) Then EnforcementTabs.TabPages.Add(EpaValuesTabPage)
        EnforcementTabs.SelectTab(EpaValuesTabPage)
        DisplayEpaValues()
    End Sub

    Private Sub DisplayEpaValues()
        With EnforcementCase
            If .SubmittedToEpa Then
                NotSubmittedToEpaLabel.Visible = False

                ' AFS action numbers
                AfsKeyActionNumber.Text = .AfsKeyActionNumber.ToString(DisplayZeroAsNA)
                AfsNovActionNumber.Text = .AfsNovActionNumber.ToString(DisplayZeroAsNA)
                AfsNfaActionNumber.Text = .AfsNfaActionNumber.ToString(DisplayZeroAsNA)
                AfsCoProposedActionNumber.Text = .AfsCoProposedNumber.ToString(DisplayZeroAsNA)
                AfsCoExecutedActionNumber.Text = .AfsCoActionNumber.ToString(DisplayZeroAsNA)
                AfsCoResolvedActionNumber.Text = .AfsCoResolvedActionNumber.ToString(DisplayZeroAsNA)
                AfsAoCivilCourtActionNumber.Text = .AfsCivilCourtActionNumber.ToString(DisplayZeroAsNA)
                AfsAoToAgActionNumber.Text = .AfsAoToAGActionNumber.ToString(DisplayZeroAsNA)
                AfsAoResolvedActionNumber.Text = .AfsAoResolvedActionNumber.ToString(DisplayZeroAsNA)

                ' Stipulated penalties
                If StipulatedPenalties.Rows.Count = 0 Then
                    AfsStipulatedPenalitiesActionNumbers.Text = "N/A"
                Else
                    Dim sp As New List(Of String)
                    For Each row As DataGridViewRow In StipulatedPenalties.Rows
                        sp.Add(row.Cells("STRAFSSTIPULATEDPENALTYNUMBER").Value.ToString)
                    Next
                    AfsStipulatedPenalitiesActionNumbers.Text = String.Join(", ", sp)
                End If

                ' EPA IDs
                EpaCaseFileId.Text = .CaseFileId
                EpaNovId.Text = .NovEnforcementActionId
                EpaCoId.Text = .CoEnforcementActionId
                EpaAoId.Text = .AoEnforcementActionId

                If .DayZeroDate.HasValue Then
                    EpaDayZero.Text = .DayZeroDate.Value.ToString(DateFormat)
                Else
                    EpaDayZero.Text = "N/A"
                End If
            End If
        End With
    End Sub

#End Region

#Region " Menu and Toolbar "

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        ValidateAndSave()
    End Sub

    Private Sub SaveMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMenuItem.Click
        ValidateAndSave()
    End Sub

    Private Sub CloseMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ShowAuditHistoryMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAuditHistoryMenuItem.Click
        ShowAuditHistory()
    End Sub

    Private Sub ShowEpaActionNumbersMenuItem_Click(sender As Object, e As EventArgs) Handles ShowEpaActionNumbersMenuItem.Click
        ShowEpaValues()
    End Sub

    Private Sub DeleteEnforcement_Click(sender As Object, e As EventArgs) Handles DeleteEnforcementMenuItem.Click
        DeleteEnforcement()
    End Sub

    Private Sub ClearMessageMenuItem_Click(sender As Object, e As EventArgs) Handles ClearMessageMenuItem.Click
        ClearGeneralMessage()
    End Sub

    Private Sub ClearErrorsMenuItem_Click(sender As Object, e As EventArgs) Handles ClearErrorsMenuItem.Click
        ClearErrors()
    End Sub

    Private Sub DismissMessageButton_Click(sender As Object, e As EventArgs) Handles DismissMessageButton.Click
        ClearGeneralMessage()
    End Sub

#End Region

#Region " Save enforcement data "

    Private Function ValidateAndSave() As Boolean
        If Not CurrentUser.HasPermission(UserCan.SaveEnforcement) Then
            GeneralMessage = New IaipMessage("You do not have sufficent permission to save changes to enforcement cases.", IaipMessage.WarningLevels.ErrorReport)
            Return False
        End If

        If Not ValidateFormData() Then
            DisplayValidationErrors()
            Return False
        End If

        Return SaveEnforcement()
    End Function

    Private Function SaveEnforcement() As Boolean
        ReadEnforcementCaseFromForm()
        DetermineAfsActionNumbers()

        Dim enforcementIsNew As Boolean = (EnforcementCase.EnforcementId = 0)

        Dim result As Integer = DAL.Sscp.SaveEnforcement(EnforcementCase)
        If result = 0 Then
            GeneralMessage = New IaipMessage("There was an error saving the current data.", IaipMessage.WarningLevels.ErrorReport)
            Return False
        Else
            EnforcementCase.DateModified = Today
            EnforcementId = result
            EnforcementCase.EnforcementId = result

            Dim linkedEventSuccess As Boolean = SaveAllLinkedEvents()

            Dim message As String = String.Empty

            If enforcementIsNew Then
                message = vbNewLine & "New enforcement ID: " & EnforcementCase.EnforcementId.ToString()
            End If

            If linkedEventSuccess Then
                message = "Current data saved." & message
                GeneralMessage = New IaipMessage(message, IaipMessage.WarningLevels.Success)
            Else
                message = "Current enforcement data saved, but there was an error saving the linked compliance discovery events." & message
                GeneralMessage = New IaipMessage(message, IaipMessage.WarningLevels.Warning)
            End If

            DisplayEnforcementCase()
            Return True
        End If
    End Function

    Private Sub DetermineAfsActionNumbers()
        With EnforcementCase
            If EnforcementCase.SubmittedToEpa Then
                nextAfsKey = DAL.GetNextAfsActionNumber(AirsNumber)
                Dim prevAfsKey As Integer = nextAfsKey

                AssignAfsKey(.AfsKeyActionNumber)
                AssignAfsKey(.AfsNovActionNumber, .NovSent Is Nothing)
                AssignAfsKey(.AfsNfaActionNumber, .NfaSent Is Nothing)
                AssignAfsKey(.AfsCoProposedNumber, .CoProposed Is Nothing)
                AssignAfsKey(.AfsCoActionNumber, .CoExecuted Is Nothing)
                AssignAfsKey(.AfsCoResolvedActionNumber, .CoResolved Is Nothing)
                AssignAfsKey(.AfsAoToAGActionNumber, .AoExecuted Is Nothing)
                AssignAfsKey(.AfsCivilCourtActionNumber, .AoAppealed Is Nothing)
                AssignAfsKey(.AfsAoResolvedActionNumber, .AoResolved Is Nothing)

                If nextAfsKey > prevAfsKey Then
                    DAL.AfsData.SaveNextAfsActionNumber(AirsNumber, nextAfsKey)
                End If
            Else
                .AfsKeyActionNumber = 0
                .AfsNovActionNumber = 0
                .AfsNfaActionNumber = 0
                .AfsCoProposedNumber = 0
                .AfsCoActionNumber = 0
                .AfsCoResolvedActionNumber = 0
                .AfsAoToAGActionNumber = 0
                .AfsCivilCourtActionNumber = 0
                .AfsAoResolvedActionNumber = 0
            End If
        End With
    End Sub

    Private Sub AssignAfsKey(ByRef assignee As Integer, Optional remove As Boolean = False)
        ' Assigns or deletes (sets to zero) an AFS action number. 
        ' If remove is true, AFS key is set to zero.
        ' Otherwise, if key as already set, then it is left as is.
        ' Otherwise, key is assigned the next sequential key
        If remove Then
            assignee = 0
        ElseIf assignee = 0 Then
            assignee = nextAfsKey
            nextAfsKey += 1
        End If
    End Sub

#End Region

#Region " Validation "

    Private Sub DisplayValidationErrors()
        ClearErrorsMenuItem.Enabled = True
        Dim messageText As New StringBuilder("Please correct the following issues before saving:")
        Dim lines As Integer = 1

        For Each kvp As KeyValuePair(Of Control, String) In validationErrors
            If lines < 4 Then
                messageText.AppendLine()
                messageText.Append("* " & kvp.Value)
                lines += 1
            ElseIf lines = 4 Then
                messageText.AppendLine()
                messageText.Append("* More...")
                lines += 1
            End If
            GeneralErrorProvider.SetError(kvp.Key, kvp.Value)
            GeneralErrorProvider.SetIconAlignment(kvp.Key, ErrorIconAlignment.MiddleLeft)
        Next

        GeneralMessage = New IaipMessage(messageText.ToString, IaipMessage.WarningLevels.Warning)
    End Sub

    Private Sub ClearErrors()
        ClearErrorsMenuItem.Enabled = False
        If validationErrors IsNot Nothing Then
            For Each kvp As KeyValuePair(Of Control, String) In validationErrors
                GeneralErrorProvider.SetError(kvp.Key, String.Empty)
            Next
        End If
        validationErrors = New Dictionary(Of Control, String)
        ClearGeneralMessage()
    End Sub

    Private Function ValidateFormData() As Boolean
        ClearErrors()
        Return ValidateDates() And
            ValidateViolationType() And
            ValidatePrograms() And
            ValidatePollutants() And
            ValidateLinkedEvents() And
            ValidateFacility() And
            ValidatePenaltyAmount()
    End Function

    Private Function ValidatePenaltyAmount() As Boolean
        If COCheckBox.Checked AndAlso
            COPenaltyAmount.Text <> "" AndAlso
            Not StringValidatesAsCurrency(COPenaltyAmount.Text) Then
            validationErrors.Add(COPenaltyAmount, "Penalty amount must be a number.")
            Return False
        End If
        Return True
    End Function

    Private Function ValidateViolationType() As Boolean
        If FormIsCaseFile() AndAlso
            (ViolationTypeNone.Checked Or ViolationTypeSelect.SelectedValue?.ToString = "BLANK") Then

            validationErrors.Add(ViolationTypeGroupbox, "Choose a Violation Type")
            Return False
        End If
        Return True
    End Function

    Private Function FormIsCaseFile() As Boolean
        Return NovCheckBox.Checked OrElse COCheckBox.Checked OrElse AOCheckBox.Checked
    End Function

    Private Function ValidateFacility() As Boolean
        If AirsNumber Is Nothing OrElse Not DAL.FacilityData.AirsNumberExists(AirsNumber) Then

            validationErrors.Add(FacilityNameDisplay, "Invalid or missing AIRS number.")
            Return False
        End If
        Return True
    End Function

    Private Function ValidateLinkedEvents() As Boolean
        If EnforcementCase.SubmittedToEpa Then
            If LinkedEvents.Rows.Count = 0 Then
                Dim dr As DialogResult = MessageBox.Show(
                "There are no compliance discovery events linked to this enforcement case. Do you want to submit to EPA without an initiating action?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If dr = DialogResult.No Then
                    validationErrors.Add(LinkToEvent, "Missing discovery event.")
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Function ValidatePollutants() As Boolean
        If FormIsCaseFile() Then
            If PollutantsListView.CheckedIndices.Count = 0 Then
                validationErrors.Add(PollutantsListLabel, "At least one pollutant must be selected.")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidatePrograms() As Boolean
        If FormIsCaseFile() Then
            If ProgramsListView.CheckedIndices.Count = 0 Then
                validationErrors.Add(ProgramsListLabel, "At least one air program must be selected.")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function ValidateDates() As Boolean
        Dim result As Boolean = True

        result = ValidateDiscoveryDate()

        If LonCheckBox.Checked Then
            result = result And ValidateLonDates()
        Else
            If NovCheckBox.Checked Then result = result And ValidateNovDates()
            If NovCheckBox.Checked Then result = result And ValidateNfaDates()
            If COCheckBox.Checked Then result = result And ValidateCODates()
            If AOCheckBox.Checked Then result = result And ValidateAODates()
        End If

        Return result
    End Function

    Private Function ValidateDiscoveryDate() As Boolean
        Dim result As Boolean = CheckTheseDates(
            DiscoveryDate.Value,
            subsequents:=New List(Of DateTimePicker) From
            {LonSent, LonResolved,
            NovSent, NovResponseReceived, NfaSent,
            COProposed, COReceivedfromCompany, COReceivedFromDirector, COExecuted, COResolved,
            AOAppealed, AOExecuted, AOResolved})

        If Not result Then
            validationErrors.Add(DiscoveryDate, "Discovery date is invalid.")
        End If

        Return result
    End Function

    Private Function ValidateLonDates() As Boolean
        Dim result As Boolean = True

        If LonToUC.Checked Then
            result = result And CheckTheseDates(
                LonToUC.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate},
                subsequents:=New List(Of DateTimePicker) From {LonSent, LonResolved})
        End If

        If LonSent.Checked Then
            result = result And CheckTheseDates(
                LonSent.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, LonToUC},
                subsequents:=New List(Of DateTimePicker) From {LonResolved})
        End If

        If LonResolved.Checked Then
            result = result And CheckTheseDates(
                LonResolved.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, LonToUC},
                requiredAntecedents:=New List(Of DateTimePicker) From {LonSent})
        End If

        If Not result Then
            validationErrors.Add(LonToUC, "LON dates are invalid.")
        End If

        Return result
    End Function

    Private Function ValidateNovDates() As Boolean
        Dim result As Boolean = True

        If NovToUC.Checked Then
            result = result And CheckTheseDates(
                NovToUC.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate},
                subsequents:=New List(Of DateTimePicker) From
                {NovToPM, NovSent, NovResponseReceived, NfaToUC, NfaToPM, NfaSent,
                COToUC, COToPM, COProposed, COReceivedfromCompany, COReceivedFromDirector,
                COExecuted, COResolved})
        End If

        If NovToPM.Checked Then
            result = result And CheckTheseDates(
                NovToPM.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, NovToUC},
                subsequents:=New List(Of DateTimePicker) From
                {NovSent, NovResponseReceived, NfaToPM, NfaSent,
                COToUC, COToPM, COProposed, COReceivedfromCompany, COReceivedFromDirector,
                COExecuted, COResolved})
        End If

        If NovSent.Checked Then
            result = result And CheckTheseDates(
                NovSent.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, NovToUC, NovToPM},
                subsequents:=New List(Of DateTimePicker) From
                {NovResponseReceived, NfaSent,
                COToUC, COToPM, COProposed, COReceivedfromCompany, COReceivedFromDirector,
                COExecuted, COResolved})
        End If

        If NovResponseReceived.Checked Then
            result = result And CheckTheseDates(
                NovResponseReceived.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, NovToUC, NovToPM},
                subsequents:=New List(Of DateTimePicker) From
                {COToUC, COToPM, COProposed, COReceivedfromCompany, COReceivedFromDirector,
                COExecuted, COResolved},
                requiredAntecedents:=New List(Of DateTimePicker) From {NovSent})
        End If

        If Not result Then
            validationErrors.Add(NovToUC, "NOV dates are invalid.")
        End If

        Return result
    End Function

    Private Function ValidateNfaDates() As Boolean
        Dim result As Boolean = True

        If NfaToUC.Checked Then
            result = result And CheckTheseDates(
                NfaToUC.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, NovToUC},
                subsequents:=New List(Of DateTimePicker) From {NfaToPM, NfaSent})
        End If

        If NfaToPM.Checked Then
            result = result And CheckTheseDates(
                NfaToPM.Value,
                antecedents:=New List(Of DateTimePicker) From
                {DiscoveryDate, NovToUC, NovToPM, NfaToUC},
                subsequents:=New List(Of DateTimePicker) From {NfaSent})
        End If

        If NfaSent.Checked Then
            result = result And CheckTheseDates(
                NfaSent.Value,
                antecedents:=New List(Of DateTimePicker) From
                {DiscoveryDate, NovToUC, NovToPM, NovSent,
                NfaToUC, NfaToPM},
                requiredAntecedents:=New List(Of DateTimePicker) From {NovSent})
        End If

        If Not result Then
            validationErrors.Add(NfaToUC, "NFA dates are invalid.")
        End If

        Return result
    End Function

    Private Function ValidateCODates() As Boolean
        Dim result As Boolean = True

        If COToUC.Checked Then
            result = result And CheckTheseDates(
                COToUC.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate},
                subsequents:=New List(Of DateTimePicker) From
                {COToPM, COProposed, COExecuted, COResolved})
        End If

        If COToPM.Checked Then
            result = result And CheckTheseDates(
                COToPM.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, COToUC},
                subsequents:=New List(Of DateTimePicker) From {COProposed, COExecuted, COResolved})
        End If

        If COProposed.Checked Then
            result = result And CheckTheseDates(
                COProposed.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, COToUC, COToPM},
                subsequents:=New List(Of DateTimePicker) From {COExecuted, COResolved})
        End If

        If COExecuted.Checked Then
            result = result And CheckTheseDates(
                COExecuted.Value,
                antecedents:=New List(Of DateTimePicker) From
                {DiscoveryDate, COToUC, COToPM, COProposed},
                subsequents:=New List(Of DateTimePicker) From {COResolved})
        End If

        If COResolved.Checked Then
            result = result And CheckTheseDates(
                COResolved.Value,
                antecedents:=New List(Of DateTimePicker) From
                {DiscoveryDate, COToUC, COToPM, COProposed},
                requiredAntecedents:=New List(Of DateTimePicker) From {COExecuted})
        End If

        If Not result Then
            validationErrors.Add(COToUC, "CO dates are invalid.")
        End If

        Return result
    End Function

    Private Function ValidateAODates() As Boolean
        Dim result As Boolean = True

        If AOExecuted.Checked Then
            result = result And CheckTheseDates(
                AOExecuted.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate},
                subsequents:=New List(Of DateTimePicker) From {AOAppealed, AOResolved})
        End If

        If AOAppealed.Checked Then
            result = result And CheckTheseDates(
                AOAppealed.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate},
                subsequents:=New List(Of DateTimePicker) From {AOResolved},
                requiredAntecedents:=New List(Of DateTimePicker) From {AOExecuted})
        End If

        If AOResolved.Checked Then
            result = result And CheckTheseDates(
                AOResolved.Value,
                antecedents:=New List(Of DateTimePicker) From {DiscoveryDate, AOAppealed},
                requiredAntecedents:=New List(Of DateTimePicker) From {AOExecuted})
        End If

        If Not result Then
            validationErrors.Add(AOExecuted, "AO dates are invalid.")
        End If

        Return result
    End Function

    Private Shared Function CheckTheseDates(dateToCheck As Date,
                               Optional antecedents As List(Of DateTimePicker) = Nothing,
                               Optional subsequents As List(Of DateTimePicker) = Nothing,
                               Optional requiredAntecedents As List(Of DateTimePicker) = Nothing,
                               Optional requiredSubsequents As List(Of DateTimePicker) = Nothing
                               ) As Boolean

        If antecedents IsNot Nothing Then
            For Each dtp As DateTimePicker In antecedents
                If dtp.Checked AndAlso dtp.Value > dateToCheck Then Return False
            Next
        End If

        If subsequents IsNot Nothing Then
            For Each dtp As DateTimePicker In subsequents
                If dtp.Checked AndAlso dtp.Value < dateToCheck Then Return False
            Next
        End If

        If requiredAntecedents IsNot Nothing Then
            For Each dtp As DateTimePicker In requiredAntecedents
                If Not dtp.Checked OrElse dtp.Value > dateToCheck Then Return False
            Next
        End If

        If requiredSubsequents IsNot Nothing Then
            For Each dtp As DateTimePicker In requiredSubsequents
                If Not dtp.Checked OrElse dtp.Value < dateToCheck Then Return False
            Next
        End If

        Return True
    End Function

#End Region

#Region " Delete enforcement data "

    Private Sub DeleteEnforcement()
        If EnforcementId = 0 Then
            GeneralMessage = New IaipMessage("Current enforcement must be saved before you can delete it. I know, sounds weird, right?", IaipMessage.WarningLevels.ErrorReport)
            Exit Sub
        End If

        If Not CurrentUser.HasPermission(UserCan.ResolveEnforcement) Then
            GeneralMessage = New IaipMessage("You do not have sufficent permission to delete enforcement cases.", IaipMessage.WarningLevels.ErrorReport)
            Exit Sub
        End If

        Dim dr As DialogResult = MessageBox.Show(
            "Are you sure you want to delete enforcement case #" &
            EnforcementId.ToString & "? " &
            "This cannot be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If dr = DialogResult.Yes Then
            If DAL.Sscp.DeleteEnforcement(EnforcementId) Then
                MessageBox.Show(
                    "Enforcement case #" & EnforcementId.ToString &
                    " was successfully deleted." & vbNewLine & vbNewLine &
                    "If it has already been sent to EPA, it will deleted from EPA's " &
                    "database during the next batch update.",
                    "Success", MessageBoxButtons.OK)

                EnforcementCase.IsDeleted = True
                DisableAllIfDeleted()
            Else
                GeneralMessage = New IaipMessage("There was an error deleting the enforcement case.", IaipMessage.WarningLevels.ErrorReport)
            End If
        End If
    End Sub

#End Region

#Region " Gather data from form "

    Private Sub ReadEnforcementCaseFromForm()
        ReadEnforcementActionDataFromForm()

        With EnforcementCase
            .AirsNumber = AirsNumber
            .Comment = GeneralComments.Text
            .DayZeroDate = DetermineDayZeroFromForm()
            .EnforcementId = EnforcementId
            .Open = CType(Not ResolvedCheckBox.Checked, OpenOrClosed)
            .Pollutants = ReadPollutantsFromForm()
            .LegacyAirPrograms = ReadProgramsFromForm()
            .StaffResponsibleId = CInt(StaffResponsible.SelectedValue)
            .ViolationType = ViolationTypeSelect.SelectedValue?.ToString
            .DateFinalized = GetNullableDateFromDateTimePicker(ResolvedDate)
            .DiscoveryDate = GetNullableDateFromDateTimePicker(DiscoveryDate)
        End With
    End Sub

    Private Sub ReadEnforcementActionDataFromForm()
        If Not (FormIsCaseFile()) Then EnforcementCase.SubmittedToEpa = False
        ' No ELSE: We're just removing the flag here if it might have been unset during editing
        ' The flag is only added on the "Send to EPA" button click.

        EnforcementCase.EnforcementActions = New List(Of EnforcementActionType)
        ReadLonDataFromForm()
        ReadNovDataFromForm()
        ReadCoDataFromForm()
        ReadAoDataFromForm()
    End Sub

    Private Sub ReadLonDataFromForm()
        With EnforcementCase
            If LonCheckBox.Checked AndAlso
                (LonResolved.Checked Or LonSent.Checked Or LonToUC.Checked) Then

                .EnforcementActions.Add(EnforcementActionType.LON)
                .LonResolved = GetNullableDateFromDateTimePicker(LonResolved)
                .LonSent = GetNullableDateFromDateTimePicker(LonSent)
                .LonToUc = GetNullableDateFromDateTimePicker(LonToUC)
                .LonComment = LonComments.Text
            Else
                .LonResolved = Nothing
                .LonSent = Nothing
                .LonToUc = Nothing
                .LonComment = Nothing
            End If
        End With
    End Sub

    Private Sub ReadNovDataFromForm()
        With EnforcementCase
            If NovCheckBox.Checked AndAlso
                (NovSent.Checked Or NovToPM.Checked Or NovToUC.Checked Or
                NfaSent.Checked Or NfaToPM.Checked Or NfaToUC.Checked) Then

                .EnforcementActions.Add(EnforcementActionType.NOV)
                .NovResponseReceived = GetNullableDateFromDateTimePicker(NovResponseReceived)
                .NovSent = GetNullableDateFromDateTimePicker(NovSent)
                .NovToPm = GetNullableDateFromDateTimePicker(NovToPM)
                .NovToUc = GetNullableDateFromDateTimePicker(NovToUC)
                .NfaSent = GetNullableDateFromDateTimePicker(NfaSent)
                .NfaToPm = GetNullableDateFromDateTimePicker(NfaToPM)
                .NfaToUc = GetNullableDateFromDateTimePicker(NfaToUC)
                .NovComment = NovComments.Text
            Else
                .NovResponseReceived = Nothing
                .NovSent = Nothing
                .NovToPm = Nothing
                .NovToUc = Nothing
                .NfaSent = Nothing
                .NfaToPm = Nothing
                .NfaToUc = Nothing
                .NovComment = Nothing
            End If
        End With
    End Sub

    Private Sub ReadCoDataFromForm()
        With EnforcementCase
            If COCheckBox.Checked AndAlso
                (COExecuted.Checked Or COProposed.Checked Or COToPM.Checked Or COToUC.Checked) Then

                .EnforcementActions.Add(EnforcementActionType.CO)
                .CoExecuted = GetNullableDateFromDateTimePicker(COExecuted)
                .CoProposed = GetNullableDateFromDateTimePicker(COProposed)
                .CoReceivedFromCompany = GetNullableDateFromDateTimePicker(COReceivedfromCompany)
                .CoReceivedFromDirector = GetNullableDateFromDateTimePicker(COReceivedFromDirector)
                .CoResolved = GetNullableDateFromDateTimePicker(COResolved)
                .CoToPm = GetNullableDateFromDateTimePicker(COToPM)
                .CoToUc = GetNullableDateFromDateTimePicker(COToUC)
                .CoComment = COComments.Text
                .CoNumber = If(CoNumber.Value = 0, "", "EPD-AQC-" & CoNumber.Value.ToString)
                .CoPenaltyAmount = ConvertCurrencyStringToDecimal(COPenaltyAmount.Text)
                .CoPenaltyAmountComment = COPenaltyComments.Text
            Else
                .CoExecuted = Nothing
                .CoProposed = Nothing
                .CoReceivedFromCompany = Nothing
                .CoReceivedFromDirector = Nothing
                .CoResolved = Nothing
                .CoToPm = Nothing
                .CoToUc = Nothing
                .CoComment = Nothing
                .CoNumber = Nothing
                .CoPenaltyAmount = Nothing
                .CoPenaltyAmountComment = Nothing
            End If
        End With
    End Sub

    Private Sub ReadAoDataFromForm()
        With EnforcementCase
            If AOCheckBox.Checked AndAlso
                (AOAppealed.Checked Or AOExecuted.Checked Or AOResolved.Checked) Then

                .EnforcementActions.Add(EnforcementActionType.AO)
                .AoAppealed = GetNullableDateFromDateTimePicker(AOAppealed)
                .AoExecuted = GetNullableDateFromDateTimePicker(AOExecuted)
                .AoResolved = GetNullableDateFromDateTimePicker(AOResolved)
            Else
                .AoAppealed = Nothing
                .AoExecuted = Nothing
                .AoResolved = Nothing
            End If
        End With
    End Sub

    Private Function ReadProgramsFromForm() As List(Of String)
        Dim pList As New List(Of String)
        For Each item As ListViewItem In ProgramsListView.CheckedItems
            pList.Add(Facilities.FacilityHeaderData.ConvertAirProgramToLegacyCode(item.SubItems(1).Text))
        Next
        Return pList
    End Function

    Private Function ReadPollutantsFromForm() As List(Of String)
        Dim pList As New List(Of String)
        For Each item As ListViewItem In PollutantsListView.CheckedItems
            pList.Add(item.SubItems(1).Text)
        Next
        Return pList
    End Function

    Private Function DetermineDayZeroFromForm() As Date?
        If FormIsCaseFile() Then
            Dim dl As New List(Of Date)

            If DiscoveryDate.Checked Then dl.Add(DiscoveryDate.Value.AddDays(90))
            If NovCheckBox.Checked AndAlso NovSent.Checked Then dl.Add(NovSent.Value)
            If COCheckBox.Checked AndAlso COProposed.Checked Then dl.Add(COProposed.Value)
            If COCheckBox.Checked AndAlso COExecuted.Checked Then dl.Add(COExecuted.Value)
            If AOCheckBox.Checked AndAlso AOExecuted.Checked Then dl.Add(AOExecuted.Value)

            If dl.Count > 0 Then Return dl.Min
        End If
        Return Nothing
    End Function

#End Region

End Class