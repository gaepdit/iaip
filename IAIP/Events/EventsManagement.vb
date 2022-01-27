Imports System.Collections.Generic
Imports Iaip.DAL.EventsData
Imports Iaip.Apb.Res

Public Class EventsManagement

    Private Property selectedEventId As Integer?
    Private Property selectedEvent As ResEvent
    Private Property selectedRegistrationId As Integer?

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        LoadEventContactCombo()
        LoadRegistrationStatusCombo()

        LoadEvents()
    End Sub

    Private Sub LoadEventContactCombo()
        Dim staff As DataTable = DAL.GetStaffDetailsAsDataTableByBranch()

        Dim nullRow As DataRow = staff.NewRow
        nullRow("NUMUSERID") = 0
        nullRow("AlphaName") = "Select a contact…"
        staff.Rows.InsertAt(nullRow, 0)

        If staff.Rows.Count > 0 Then
            With cboEventWebContact
                .DataSource = staff
                .DisplayMember = "AlphaName"
                .ValueMember = "NUMUSERID"
                .SelectedIndex = 0
            End With
            With txtWebPhone
                .DataBindings.Add(New Binding("Text", staff, "STRPHONE"))
            End With
        Else
            Enabled = False
        End If
    End Sub

    Private Sub LoadRegistrationStatusCombo()
        ' Get list of Registration Status types and bind that list to the combobox
        Dim statuses As SortedDictionary(Of Integer, String) = GetRegistrationStatusesAsDictionary(False, "Select a status…")
        If statuses.Count > 0 Then
            cboRegStatus.BindToSortedDictionary(statuses)
        End If
    End Sub

    Private Sub LoadEvents()
        ClearSelectedEventData()

        Dim dt As DataTable = GetResEventsAsDataTable(chkShowPast.Checked)
        dgvEvents.DataSource = dt
        dgvEvents.Columns("ID").Visible = False
        dgvEvents.SanelyResizeColumns(600)
        dgvEvents.SelectNone()

        If dt.Rows.Count > 0 Then
            AddHandler dgvEvents.SelectionChanged, AddressOf dgvEvents_SelectionChanged
        End If
    End Sub

    Private Sub chkShowPast_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPast.CheckedChanged
        LoadEvents()
    End Sub

    Private Sub btnCreateNewEvent_Click(sender As Object, e As EventArgs) Handles btnCreateNewEvent.Click
        ChangeFormToNewEventCreator(True)
    End Sub

    Private Sub ChangeFormToNewEventCreator(clearFirst As Boolean)
        If clearFirst Then
            ' clear loaded data
            ClearSelectedEventData()
        End If

        lblEventTitle.Text = "Create New Event"

        ' disable/hide unused form controls
        chkShowPast.Enabled = False
        btnCreateNewEvent.Enabled = False
        dgvEvents.Enabled = False

        If clearFirst Then
            dgvEvents.SelectNone()
        Else
            dgvRegistrants.DataSource = Nothing
            ClearRegistrationManagementForm()
            lblEventStatus.Visible = False
            lblEventStatusLabel.Visible = False
            lnkGecoLink.Visible = False
            lnkGecoLink.Links.Clear()
            btnCancelEvent.Visible = False
            btnDuplicateEvent.Visible = False
        End If

        tabsEventDetails.SelectedTab = tabEventManagement

        ' enable necessary controls
        txtEventTitle.Enabled = True
        txtEventDescription.Enabled = True
        txtWebsiteURL.Enabled = True
        DTPEventDate.Enabled = True
        DTPEventEndDate.Enabled = True
        DTPEventEndDate.Enabled = True
        txtEventTime.Enabled = True
        txtEventEndTime.Enabled = True
        txtCapacity.Enabled = True

        txtEventVenue.Enabled = True
        txtEventAddress.Enabled = True
        txtEventCity.Enabled = True
        txtState.Enabled = True
        txtZip.Enabled = True

        cboEventWebContact.Enabled = True
        txtWebPhone.Enabled = True
        txtEventNotes.Enabled = True
        chkEventPasscode.Enabled = True
        btnGeneratePasscode.Visible = False

        ' change button names/functions
        btnSave.Text = "Save New Event"
        btnSave.Enabled = True
        btnCancelCreateEvent.Visible = True
    End Sub

    Private Sub btnCancelCreateEvent_Click(sender As Object, e As EventArgs) Handles btnCancelCreateEvent.Click
        ChangeFormBackFromNewEventCreator()
    End Sub

    Private Sub ChangeFormBackFromNewEventCreator()
        ClearEventManagementForm()

        chkShowPast.Enabled = True
        btnCreateNewEvent.Enabled = True
        dgvEvents.Enabled = True
        dgvEvents.SelectNone()

        btnSave.Text = "Save Changes"
        btnSave.Enabled = False
        btnCancelCreateEvent.Visible = False

        If dgvEvents.Rows.Count > 0 Then
            AddHandler dgvEvents.SelectionChanged, AddressOf dgvEvents_SelectionChanged
        End If
    End Sub

    Private Sub dgvEvents_SelectionChanged(sender As Object, e As EventArgs)
        RemoveHandler dgvRegistrants.SelectionChanged, AddressOf dgvRegistrants_SelectionChanged

        If dgvEvents.Rows.Count = 0 OrElse dgvEvents.SelectedRows.Count <> 1 Then
            dgvRegistrants.DataSource = Nothing

            ClearEventManagementForm()
            ClearRegistrationManagementForm()
            Return
        End If

        Dim newId As Integer = CInt(dgvEvents.SelectedRows(0).Cells("ID").Value)

        If newId = selectedEventId Then
            Return
        End If

        ClearEventManagementForm()
        ClearRegistrationManagementForm()

        selectedEventId = newId
        selectedEvent = GetResEventById(newId)

        If selectedEvent Is Nothing Then
            tabsEventDetails.SelectedTab = tabEventManagement
            lblEventMessage.ShowMessage("Error loading event data.", ErrorLevel.Error)
            Return
        End If

        LoadEventManagementForm()
        LoadRegistrationManagementForm()
    End Sub

    Private Sub LoadEventManagementForm()
        btnSave.Enabled = True
        btnCancelEvent.Visible = True
        btnDuplicateEvent.Visible = True

        txtEventTitle.Enabled = True
        txtEventDescription.Enabled = True
        txtWebsiteURL.Enabled = True
        DTPEventDate.Enabled = True
        DTPEventEndDate.Enabled = True
        DTPEventEndDate.Enabled = True
        txtEventTime.Enabled = True
        txtEventEndTime.Enabled = True
        txtCapacity.Enabled = True

        txtEventVenue.Enabled = True
        txtEventAddress.Enabled = True
        txtEventCity.Enabled = True
        txtState.Enabled = True
        txtZip.Enabled = True

        cboEventWebContact.Enabled = True
        txtWebPhone.Enabled = True
        txtEventNotes.Enabled = True
        chkEventPasscode.Enabled = True

        lblEventTitle.Text = selectedEvent.Title
        txtEventTitle.Text = selectedEvent.Title
        txtEventDescription.Text = selectedEvent.Description
        txtWebsiteURL.Text = selectedEvent.WebLink
        DTPEventDate.Value = selectedEvent.StartDate

        If selectedEvent.EndDate.HasValue Then
            DTPEventEndDate.Checked = True
            DTPEventEndDate.Value = selectedEvent.EndDate.Value
        Else
            DTPEventEndDate.Checked = False
            DTPEventEndDate.Value = Today
        End If

        txtEventTime.Text = selectedEvent.StartTime
        txtEventEndTime.Text = selectedEvent.EndTime
        txtCapacity.Text = selectedEvent.Capacity.ToString
        txtEventVenue.Text = selectedEvent.Venue
        txtEventAddress.Text = selectedEvent.Street
        txtEventCity.Text = selectedEvent.City
        txtState.Text = selectedEvent.State
        txtZip.Text = selectedEvent.PostalCode

        cboEventWebContact.SelectedValue = selectedEvent.WebContactId

        txtWebPhone.Text = selectedEvent.WebContactPhone
        txtEventNotes.Text = selectedEvent.Notes

        If String.IsNullOrEmpty(selectedEvent.PassCode) OrElse selectedEvent.PassCode = "1" Then
            chkEventPasscode.Text = ""
            chkEventPasscode.Checked = False
            btnGeneratePasscode.Visible = False
        Else
            chkEventPasscode.Text = selectedEvent.PassCode
            chkEventPasscode.Checked = True
            btnGeneratePasscode.Visible = True
        End If

        lblEventStatus.Visible = True
        lblEventStatusLabel.Visible = True
        lblEventStatus.Text = selectedEvent.EventStatus.GetDescription()

        If selectedEvent.EventStatus = ResEvent.EventState.Cancelled Then
            btnCancelEvent.Text = "Uncancel Event"
        Else
            btnCancelEvent.Text = "Cancel Event"
        End If

        Dim link As New Uri(GecoUrl, $"/EventRegistration/Details.aspx?eventid={selectedEventId}")
        lnkGecoLink.Links.Add(0, lnkGecoLink.Text.Length, link)
        lnkGecoLink.Visible = True
    End Sub

    Private Sub LoadRegistrationManagementForm()
        ClearRegistrationManagementForm()

        Dim registrants As DataTable = GetRegistrantsByEventId(selectedEventId.Value)

        dgvRegistrants.DataSource = registrants
        dgvRegistrants.Columns("Id").Visible = False
        dgvRegistrants.Columns("Status Code").Visible = False
        dgvRegistrants.SelectNone()
        dgvRegistrants.SanelyResizeColumns()

        Dim numRegistered As Integer = 0
        Dim numWaiting As Integer = 0
        Dim numCancelled As Integer = 0

        For Each row As DataGridViewRow In dgvRegistrants.Rows
            Dim i As Integer = CInt(row.Cells("Status Code").Value)
            If i = 1 Then
                numRegistered += 1
            ElseIf i = 2 Then
                numWaiting += 1
            ElseIf i = 3 Then
                numCancelled += 1
            End If
        Next

        txtOvEventCapacity.Text = selectedEvent.Capacity.ToString
        txtOvNumberRegistered.Text = numRegistered.ToString
        txtOvCancelled.Text = numCancelled.ToString
        txtOvWaitingList.Text = numWaiting.ToString

        btnModifyRegistration.Enabled = False
        btnEmailAll.Enabled = True
        btnEmailConfirmed.Enabled = True
        btnEmailWaitList.Enabled = True

        If registrants.Rows.Count > 0 Then
            AddHandler dgvRegistrants.SelectionChanged, AddressOf dgvRegistrants_SelectionChanged
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        lblEventMessage.ClearMessage()

        If btnSave.Text = "Save Changes" Then
            UpdateSelectedEvent()
        Else
            SaveNewEvent()
        End If
    End Sub

    Private Sub SaveNewEvent()
        If Not ValidateEventData() Then
            Return
        End If

        Dim endDate As Date?
        If DTPEventEndDate.Checked Then
            endDate = DTPEventEndDate.Value
        End If

        Dim newEvent As New ResEvent With {
            .Capacity = CInt(txtCapacity.Text),
            .City = txtEventCity.Text,
            .Description = txtEventDescription.Text,
            .EndDate = endDate,
            .EndTime = txtEventEndTime.Text,
            .Notes = txtEventNotes.Text,
            .PassCode = If(chkEventPasscode.Checked, chkEventPasscode.Text, Nothing),
            .PostalCode = txtZip.Text,
            .StartDate = DTPEventDate.Value,
            .State = txtState.Text,
            .StartTime = txtEventTime.Text,
            .Street = txtEventAddress.Text,
            .Title = txtEventTitle.Text,
            .Venue = txtEventVenue.Text,
            .WebContactId = CInt(cboEventWebContact.SelectedValue),
            .WebContactPhone = txtWebPhone.Text,
            .WebLink = txtWebsiteURL.Text
        }

        Dim result As Boolean = CreateEvent(newEvent)

        If result Then
            ChangeFormBackFromNewEventCreator()
            LoadEvents()
            lblEventMessage.ShowMessage("New event created.", ErrorLevel.Success)
        Else
            lblStatusUpdateResult.ShowMessage("Update failed; please try again.", ErrorLevel.Error)
        End If
    End Sub

    Private Function ValidateEventData() As Boolean
        Dim valid As Boolean = True

        If String.IsNullOrEmpty(txtEventTitle.Text) Then
            MessageBox.Show("An event title is required. Please add a title and try again.", "Error")
            valid = False
        End If

        If Not String.IsNullOrEmpty(txtWebsiteURL.Text) AndAlso Not IsValidURL(txtWebsiteURL.Text) Then
            MessageBox.Show("The website address is not valid. Please fix it and try again.", "Error")
            valid = False
        End If

        If String.IsNullOrEmpty(txtCapacity.Text) OrElse Not IsNumeric(txtCapacity.Text) Then
            MessageBox.Show("Capacity must be a number. Please fix it and try again.", "Error")
            valid = False
        Else
            Dim i As Integer
            If Not Integer.TryParse(txtCapacity.Text, i) Then
                MessageBox.Show("Capacity must be a whole number. Please fix it and try again.", "Error")
                valid = False
            ElseIf i < 0 Then
                MessageBox.Show("Capacity must be greater than zero. Please fix it and try again.", "Error")
                valid = False
            End If
        End If

        If cboEventWebContact.SelectedIndex = 0 Then
            MessageBox.Show("Web contact is required. Please select a contact and try again.", "Error")
            valid = False
        End If

        Return valid
    End Function

    Private Sub UpdateSelectedEvent()
        If Not ValidateEventData() Then
            Return
        End If

        Dim endDate As Date?
        If DTPEventEndDate.Checked Then
            endDate = DTPEventEndDate.Value
        End If

        Dim updatedEvent As New ResEvent With {
            .EventId = selectedEventId.Value,
            .Capacity = CInt(txtCapacity.Text),
            .City = txtEventCity.Text,
            .Description = txtEventDescription.Text,
            .EndDate = endDate,
            .EndTime = txtEventEndTime.Text,
            .Notes = txtEventNotes.Text,
            .PassCode = If(chkEventPasscode.Checked, chkEventPasscode.Text, Nothing),
            .PostalCode = txtZip.Text,
            .StartDate = DTPEventDate.Value,
            .State = txtState.Text,
            .StartTime = txtEventTime.Text,
            .Street = txtEventAddress.Text,
            .Title = txtEventTitle.Text,
            .Venue = txtEventVenue.Text,
            .WebContactId = CInt(cboEventWebContact.SelectedValue),
            .WebContactPhone = txtWebPhone.Text,
            .WebLink = txtWebsiteURL.Text
        }

        Dim result As Boolean = UpdateEvent(updatedEvent)

        If result Then
            LoadEvents()
            lblEventMessage.ShowMessage("Event successfully updated.", ErrorLevel.Success)
        Else
            lblStatusUpdateResult.ShowMessage("Event update failed; please try again.", ErrorLevel.Error)
        End If
    End Sub

    Private Sub btnCancelEvent_Click(sender As Object, e As EventArgs) Handles btnCancelEvent.Click
        If btnCancelEvent.Text = "Cancel Event" Then
            If CancelEvent(selectedEventId.Value) Then
                LoadEvents()
                lblEventMessage.ShowMessage("Event canceled.", ErrorLevel.Success)
            Else
                lblStatusUpdateResult.ShowMessage("Event update failed; please try again.", ErrorLevel.Error)
            End If
        Else
            If UncancelEvent(selectedEventId.Value) Then
                LoadEvents()
                lblEventMessage.ShowMessage("Event uncanceled.", ErrorLevel.Success)
            Else
                lblStatusUpdateResult.ShowMessage("Event update failed; please try again.", ErrorLevel.Error)
            End If
        End If
    End Sub

    Private Sub chkEventPasscode_CheckedChanged(sender As Object, e As EventArgs) Handles chkEventPasscode.CheckedChanged
        If chkEventPasscode.Checked Then
            btnGeneratePasscode.Visible = True

            If chkEventPasscode.Text = "" Then
                chkEventPasscode.Text = GeneratePasscode()
            End If
        Else
            btnGeneratePasscode.Visible = False
        End If
    End Sub

    Private Function GeneratePasscode() As String
        Dim r As New Random(Date.Now.Millisecond)
        Dim passcode As String = "GA" & r.Next(100000, 999999)

        If PasscodeExists(passcode) Then
            Return GeneratePasscode()
        Else
            Return passcode
        End If
    End Function

    Private Sub btnGeneratePasscode_Click(sender As Object, e As EventArgs) Handles btnGeneratePasscode.Click
        chkEventPasscode.Text = GeneratePasscode()
    End Sub

    Private Sub ClearSelectedEventData()
        RemoveHandler dgvEvents.SelectionChanged, AddressOf dgvEvents_SelectionChanged
        RemoveHandler dgvRegistrants.SelectionChanged, AddressOf dgvRegistrants_SelectionChanged

        dgvEvents.SelectNone()
        ClearEventManagementForm()
        ClearRegistrationManagementForm()
    End Sub

    Private Sub ClearEventManagementForm()
        selectedEventId = Nothing
        selectedEvent = Nothing

        lblEventTitle.Text = "Select an event to view/edit"

        btnSave.Enabled = False
        btnCancelEvent.Visible = False
        btnDuplicateEvent.Visible = False

        txtEventTitle.Clear()
        txtEventDescription.Clear()
        txtWebsiteURL.Clear()
        DTPEventDate.Value = Today
        DTPEventEndDate.Value = Today
        DTPEventEndDate.Checked = False
        txtEventTime.Clear()
        txtEventEndTime.Clear()
        txtCapacity.Text = "0"

        txtEventVenue.Clear()
        txtEventAddress.Clear()
        txtEventCity.Clear()
        txtState.Clear()
        txtZip.Clear()

        cboEventWebContact.SelectedIndex = 0
        txtWebPhone.Clear()
        txtEventNotes.Clear()
        chkEventPasscode.Text = ""
        chkEventPasscode.Checked = False

        lblEventStatus.Visible = False
        lblEventStatusLabel.Visible = False
        lnkGecoLink.Visible = False
        lnkGecoLink.Links.Clear()
        btnGeneratePasscode.Visible = False

        lblEventMessage.ClearMessage()

        txtEventTitle.Enabled = False
        txtEventDescription.Enabled = False
        txtWebsiteURL.Enabled = False
        DTPEventDate.Enabled = False
        DTPEventEndDate.Enabled = False
        DTPEventEndDate.Enabled = False
        txtEventTime.Enabled = False
        txtEventEndTime.Enabled = False
        txtCapacity.Enabled = False

        txtEventVenue.Enabled = False
        txtEventAddress.Enabled = False
        txtEventCity.Enabled = False
        txtState.Enabled = False
        txtZip.Enabled = False

        cboEventWebContact.Enabled = False
        txtWebPhone.Enabled = False
        txtEventNotes.Enabled = False
        chkEventPasscode.Enabled = False
        btnGeneratePasscode.Visible = False
    End Sub

    Private Sub ClearRegistrationManagementForm()
        ClearRegistrationChangeForm()

        btnEmailAll.Enabled = False
        btnEmailConfirmed.Enabled = False
        btnEmailWaitList.Enabled = False

        lblSelectedRegistrantLabel.Text = ""
        txtOvNumberRegistered.Clear()
        txtOvEventCapacity.Clear()
        txtOvWaitingList.Clear()
        txtOvCancelled.Clear()
    End Sub

    Private Sub ClearRegistrationChangeForm()
        selectedRegistrationId = Nothing
        btnModifyRegistration.Enabled = False
        lblSelectedRegistrantLabel.Text = ""
        cboRegStatus.Enabled = False
        cboRegStatus.SelectedIndex = 0
        lblStatusUpdateResult.ClearMessage()
    End Sub

    Private Sub dgvRegistrants_SelectionChanged(sender As Object, e As EventArgs)
        If dgvRegistrants.Rows.Count = 0 OrElse dgvRegistrants.SelectedRows.Count <> 1 Then
            ClearRegistrationChangeForm()
            Return
        End If

        lblStatusUpdateResult.ClearMessage()

        Dim newId As Integer = CInt(dgvRegistrants.SelectedRows(0).Cells("ID").Value)

        If selectedRegistrationId = newId Then
            Return
        End If

        selectedRegistrationId = newId

        Dim row As DataGridViewRow = dgvRegistrants.SelectedRows(0)

        lblSelectedRegistrantLabel.Text = row.Cells("First Name").Value.ToString & " " &
            row.Cells("Last Name").Value.ToString & ", " &
            row.Cells("Email").Value.ToString

        cboRegStatus.Text = row.Cells("Registration Status").Value.ToString

        cboRegStatus.Enabled = True
        btnModifyRegistration.Enabled = True
    End Sub

    Private Sub btnModifyRegistration_Click(sender As Object, e As EventArgs) Handles btnModifyRegistration.Click
        lblStatusUpdateResult.ClearMessage()

        If Not selectedRegistrationId.HasValue Then
            btnModifyRegistration.Enabled = False
            Return
        End If

        'First check to see if event is already at capacity
        If CInt(cboRegStatus.SelectedValue) = 1 AndAlso CInt(txtOvNumberRegistered.Text) >= selectedEvent.Capacity Then

            'Give the admin a warning that they may be overbooking the event
            Dim msg As String = "Event is already at capacity. If this registrant wasn't previously confirmed, " &
                "the event will be overbooked. " & vbNewLine & vbNewLine &
                "Would you like to continue?"
            Dim result As DialogResult = MessageBox.Show(Me, msg, "Event is at capacity", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                Return
            End If
        End If

        If UpdateRegistration(selectedRegistrationId.Value, CInt(cboRegStatus.SelectedValue)) Then
            RemoveHandler dgvRegistrants.SelectionChanged, AddressOf dgvRegistrants_SelectionChanged
            LoadRegistrationManagementForm()
            lblStatusUpdateResult.ShowMessage("Status updated", ErrorLevel.Success)
        Else
            lblStatusUpdateResult.ShowMessage("Status update failed", ErrorLevel.Error)
        End If
    End Sub

    Private Sub SendEmail(Optional statusFilter As String = "")
        Dim subject As String = "EPD Event: " & selectedEvent.Title & " – " & selectedEvent.StartDate.ToString(DateFormat)

        Dim body As String = selectedEvent.Title & vbNewLine & vbNewLine &
            selectedEvent.Description & vbNewLine & vbNewLine &
            "Starts on: " & selectedEvent.StartDate.ToString(DateFormat)

        If selectedEvent.StartTime IsNot Nothing Then
            body &= ", " & selectedEvent.StartTime
        End If

        body &= vbNewLine & "Venue: " & selectedEvent.Venue & vbNewLine & vbNewLine &
            "Event link: " & selectedEvent.WebLink & vbNewLine

        Dim recipientsBCC As List(Of String) = GetCorrectRecipients(statusFilter)

        If recipientsBCC Is Nothing OrElse recipientsBCC.Count = 0 Then
            MessageBox.Show("There are no recipients to email.", "Error", MessageBoxButtons.OK)
            Return
        End If

        Cursor = Cursors.AppStarting

        Select Case CreateEmail(subject, body, recipientsBCC:=recipientsBCC.ToArray)

            Case CreateEmailResult.Failure, CreateEmailResult.FunctionError
                MessageBox.Show("There was an error sending the message. Please try again.", "Error", MessageBoxButtons.OK)

            Case CreateEmailResult.InvalidEmail
                MessageBox.Show("One or more email addresses are not valid", "Error", MessageBoxButtons.OK)

        End Select

        Cursor = Nothing
    End Sub

    Private Function GetCorrectRecipients(Optional statusFilter As String = "") As List(Of String)
        Dim recipients As New List(Of String)

        For Each row As DataGridViewRow In dgvRegistrants.Rows
            If statusFilter = "" OrElse row.Cells("Registration Status").Value.ToString() = statusFilter Then
                recipients.Add(row.Cells("Email").Value.ToString)
            End If
        Next

        Return recipients
    End Function

    Private Sub btnEmailAll_Click(sender As Object, e As EventArgs) Handles btnEmailAll.Click
        SendEmail()
    End Sub

    Private Sub btnEmailConfirmed_Click(sender As Object, e As EventArgs) Handles btnEmailConfirmed.Click
        SendEmail("Confirmed")
    End Sub

    Private Sub btnEmailWaitList_Click(sender As Object, e As EventArgs) Handles btnEmailWaitList.Click
        SendEmail("Waiting List")
    End Sub

    Private Sub lnkGecoLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkGecoLink.LinkClicked
        If e.Link.LinkData IsNot Nothing Then
            OpenUri(CType(e.Link.LinkData, Uri))
        End If
    End Sub

    Private Sub btnDuplicateEvent_Click(sender As Object, e As EventArgs) Handles btnDuplicateEvent.Click
        ChangeFormToNewEventCreator(False)
    End Sub

End Class
