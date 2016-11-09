Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Iaip.DAL.EventRegistrationData
Imports Iaip.Apb.Res

Public Class MASPRegistrationTool
    Dim query As String

#Region "Properties"

    Dim selectedEventId As Integer?
    Dim selectedEvent As ResEvent

#End Region

#Region "Form events"

    Private Sub MASPRegistrationTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadComboBoxes()
        LoadEvent()

        lblEventTitle.Text = ""
        lblEventDate.Text = ""
        btnViewDetails.Enabled = False

        btnGeneratePasscode.Visible = False
        chbEventPasscode.Text = ""

    End Sub

    Private Sub MASPRegistrationTool_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        AddHandler rdbEventsFilterFuture.CheckedChanged, AddressOf rdbEventsFilter_CheckedChanged
        AddHandler rdbEventsFilterPast.CheckedChanged, AddressOf rdbEventsFilter_CheckedChanged
        AddHandler rdbEventsFilterAll.CheckedChanged, AddressOf rdbEventsFilter_CheckedChanged
    End Sub

#End Region

#Region "Form combo boxes"

    Private Sub LoadComboBoxes()
        LoadEventContactCombos()
        LoadEventStatusCombo()
        LoadRegistrationStatusCombo()
    End Sub

    Private Sub LoadEventContactCombos()
        Dim staff As DataTable = DAL.GetStaffDetailsAsDataTableByBranch()

        Dim nullRow As DataRow = staff.NewRow
        nullRow("NUMUSERID") = 0
        nullRow("AlphaName") = "Select a contact…"
        staff.Rows.InsertAt(nullRow, 0)

        If staff.Rows.Count > 0 Then
            With cboEventContact
                .DataSource = staff
                .DisplayMember = "AlphaName"
                .ValueMember = "NUMUSERID"
                .SelectedIndex = 0
            End With
            With mtbEventPhoneNumber
                .DataBindings.Add(New Binding("Text", staff, "STRPHONE"))
            End With

            Dim webStaff As DataTable = staff.Copy
            With cboEventWebContact
                .DataSource = webStaff
                .DisplayMember = "AlphaName"
                .ValueMember = "NUMUSERID"
                .SelectedIndex = 0
            End With
            With mtbEventWebPhoneNumber
                .DataBindings.Add(New Binding("Text", webStaff, "STRPHONE"))
            End With
        Else
            Me.Enabled = False
        End If
    End Sub

    Private Sub LoadEventStatusCombo()
        ' Get list of Event Status types and bind that list to the combobox
        Dim statuses As SortedDictionary(Of Integer, String) = GetResEventStatusesAsDictionary(True, "Select a status…")
        If statuses.Count > 0 Then
            cboEventStatus.BindToSortedDictionary(statuses)
        End If
    End Sub

    Private Sub LoadRegistrationStatusCombo()
        ' Get list of Registration Status types and bind that list to the combobox
        Dim statuses As SortedDictionary(Of Integer, String) = GetRegistrationStatusesAsDictionary(True, "Select a status…")
        If statuses.Count > 0 Then
            cboRegStatus.BindToSortedDictionary(statuses)
        End If
    End Sub

#End Region

#Region "Events List"

    Private Sub LoadEvent()
        Try
            Dim toDate As Date? = If(rdbEventsFilterPast.Checked, Today, CType(Nothing, Date?))
            Dim fromDate As Date? = If(rdbEventsFilterFuture.Checked, Today, CType(Nothing, Date?))
            Dim events As DataTable = GetResEventsAsDataTable(toDate, fromDate)

            dgvEvents.DataSource = events
            FormatEventsList()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatEventsList()
        With dgvEvents
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            With .Columns("NUMRES_EVENTID")
                .HeaderText = "ID"
                .Visible = False
            End With
            With .Columns("STRTITLE")
                .HeaderText = "Event"
                .DisplayIndex = 1
            End With
            With .Columns("STRDESCRIPTION")
                .HeaderText = "Description"
                .DisplayIndex = 2
            End With
            With .Columns("DATSTARTDATE")
                .HeaderText = "Start Date"
                .DisplayIndex = 3
            End With
            With .Columns("STREVENTSTARTTIME")
                .HeaderText = "Start Time"
                .DisplayIndex = 4
            End With
            With .Columns("STRVENUE")
                .HeaderText = "Venue"
                .DisplayIndex = 5
            End With
            With .Columns("STRNOTES")
                .HeaderText = "Notes"
                .DisplayIndex = 6
            End With
        End With
    End Sub

    Private Sub rdbEventsFilter_CheckedChanged(sender As Object, e As EventArgs)
        RemoveHandler dgvEvents.SelectionChanged, AddressOf dgvEvents_SelectionChanged
        LoadEvent()
    End Sub

    Private Sub dgvEvents_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvEvents.DataBindingComplete
        With dgvEvents
            .SanelyResizeColumns()
            .ClearSelection()
        End With
        AddHandler dgvEvents.SelectionChanged, AddressOf dgvEvents_SelectionChanged
    End Sub

    Private Sub dgvEvents_SelectionChanged(sender As Object, e As EventArgs)
        Try
            If dgvEvents.SelectedCells.Count > 0 Then
                Dim selectedRow As DataGridViewRow = dgvEvents.Rows(dgvEvents.CurrentCell.RowIndex)
                selectedEventId = selectedRow.Cells("NUMRES_EVENTID").Value
                lblEventTitle.Text = selectedRow.Cells("STRTITLE").Value
                lblEventDate.Text = CType(selectedRow.Cells("DATSTARTDATE").Value, Date).ToString(DateFormat)
                btnViewDetails.Enabled = True
            Else
                selectedEventId = Nothing
                ClearEventSelection()
                lblEventTitle.Text = ""
                lblEventDate.Text = ""
                btnViewDetails.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Load Individual Event Data"

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        If selectedEventId IsNot Nothing Then
            selectedEvent = New ResEvent(selectedEventId)
            LoadEventOverview()
            LoadEventManagement()
            LoadRegistrationManagement()
        End If
    End Sub

#Region "Event Overview Tab"

    Private Sub LoadEventOverview()
        LoadEventOverviewDetails()
        LoadEventOverviewRegistrants()
    End Sub

    Private Sub LoadEventOverviewDetails()

        If selectedEvent IsNot Nothing Then
            txtOvEvent.Text = selectedEvent.Title
            txtOvDescription.Text = selectedEvent.Description
            txtOvEventDateTime.Text = selectedEvent.StartDate.Value.ToString(DateFormat)
            If selectedEvent.StartTime IsNot Nothing Then
                txtOvEventDateTime.Text &= ", " & selectedEvent.StartTime
            End If
            chbOvLoginRequired.Checked = selectedEvent.LoginRequired
            txtOvPassCode.Text = selectedEvent.PassCode
            txtOvEventStatus.Text = selectedEvent.EventStatus
            txtOvEventCapacity.Text = selectedEvent.Capacity.ToString
            txtOvVenue.Text = selectedEvent.Venue & vbCrLf & selectedEvent.Address.ToString
            txtOvNotes.Text = selectedEvent.Notes
            txtOvWebContact.Text = selectedEvent.WebContact.ToString
            txtOvAPBContact.Text = selectedEvent.Contact.ToString
        End If
    End Sub

    Private Sub LoadEventOverviewRegistrants()
        Try
            Dim registrants As DataTable = GetRegistrantsByEventId(selectedEventId)

            dgvOverviewRegistrants.DataSource = registrants
            FormatEventOverviewRegistrants()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatEventOverviewRegistrants()
        With dgvOverviewRegistrants
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            With .Columns("NUMRES_REGISTRATIONID")
                .HeaderText = "ID"
                .Visible = False
            End With
            With .Columns("DATREGISTRATIONDATETIME")
                .HeaderText = "Registration Date"
                .DisplayIndex = 0
            End With
            With .Columns("STRFIRSTNAME")
                .HeaderText = "First Name"
                .DisplayIndex = 1
            End With
            With .Columns("STRLASTNAME")
                .HeaderText = "Last Name"
                .DisplayIndex = 2
            End With
            With .Columns("STRCOMMENTS")
                .HeaderText = "Comments"
                .DisplayIndex = 3
            End With
            With .Columns("STRREGISTRATIONSTATUS")
                .HeaderText = "Registration Status"
                .DisplayIndex = 4
            End With
            With .Columns("STRUSEREMAIL")
                .HeaderText = "Email"
                .DisplayIndex = 5
            End With
            With .Columns("STRPHONENUMBER")
                .HeaderText = "Phone"
                .DisplayIndex = 6
            End With
            With .Columns("STRCOMPANYNAME")
                .HeaderText = "Company Name"
                .DisplayIndex = 7
            End With
            With .Columns("NUMREGISTRATIONSTATUSCODE")
                .Visible = False
                .HeaderText = "Status Code"
            End With

        End With

        Dim numRegistered As Integer = 0
        Dim numWaiting As Integer = 0
        Dim numCancelled As Integer = 0
        For Each row As DataGridViewRow In dgvOverviewRegistrants.Rows
            If row.Cells("NUMREGISTRATIONSTATUSCODE").Value = 1 Then numRegistered += 1
            If row.Cells("NUMREGISTRATIONSTATUSCODE").Value = 2 Then numWaiting += 1
            If row.Cells("NUMREGISTRATIONSTATUSCODE").Value = 3 Then numCancelled += 1
        Next
        txtOvNumberRegistered.Text = numRegistered.ToString
        txtOvCancelled.Text = numCancelled.ToString
        txtOvWaitingList.Text = numWaiting.ToString
    End Sub

    Private Sub dgvOverviewRegistrants_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOverviewRegistrants.DataBindingComplete
        With dgvOverviewRegistrants
            .SanelyResizeColumns()
            .ClearSelection()
        End With
    End Sub

#End Region

    Private Sub LoadEventManagement()
        Try
            query = "Select " &
            "numEventStatusCode, strUserGCode, " &
            "strTitle, strDescription, " &
            "datStartDate, datEndDate, " &
            "strVenue, " &
            "numCapacity, strNotes, " &
            "strLoginRequired, strPassCode, " &
            "strAddress, strCity, " &
            "strState, numZipCode, " &
            "numAPBContact, numWebPhoneNumber, " &
            "strEventStartTime, strEventEndTime, " &
            "strWebURL " &
            "From RES_Event " &
            "where convert(int,NUMRES_EVENTID) = @eventid "

            Dim p As New SqlParameter("@eventid", selectedEventId)

            Dim dr As DataRow = DB.GetDataRow(query, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numEventStatusCode")) Then
                    cboEventStatus.SelectedValue = 0
                Else
                    cboEventStatus.SelectedValue = Convert.ToInt32(dr.Item("numEventStatusCode"))
                End If
                If IsDBNull(dr.Item("strUserGCode")) Then
                    cboEventWebContact.Text = ""
                Else
                    cboEventWebContact.SelectedValue = dr.Item("strUserGCode")
                End If
                If IsDBNull(dr.Item("strTitle")) Then
                    txtEventTitle.Clear()
                Else
                    txtEventTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    txtEventDescription.Clear()
                Else
                    txtEventDescription.Text = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("datStartDate")) Then
                    DTPEventDate.Value = Today
                Else
                    DTPEventDate.Text = dr.Item("datStartDate")
                End If
                If IsDBNull(dr.Item("datEndDate")) Then
                    DTPEventEndDate.Value = Today
                    DTPEventEndDate.Checked = False
                Else
                    DTPEventEndDate.Text = dr.Item("datEndDate")
                    DTPEventEndDate.Checked = True
                End If
                If IsDBNull(dr.Item("strVenue")) Then
                    txtEventVenue.Clear()
                Else
                    txtEventVenue.Text = dr.Item("strVenue")
                End If
                If IsDBNull(dr.Item("numCapacity")) Then
                    mtbEventCapacity.Clear()
                Else
                    mtbEventCapacity.Text = dr.Item("numCapacity")
                End If
                If IsDBNull(dr.Item("strNotes")) Then
                    txtEventNotes.Clear()
                Else
                    txtEventNotes.Text = dr.Item("strNotes")
                End If
                If IsDBNull(dr.Item("strPassCode")) Then
                    chbEventPasscode.Text = ""
                    chbEventPasscode.Checked = False
                Else
                    chbEventPasscode.Text = dr.Item("strPassCode")
                    chbEventPasscode.Checked = True
                End If
                If IsDBNull(dr.Item("strLoginRequired")) Then
                    chbEventPasscode.Checked = False
                Else
                    If dr.Item("strLogInRequired") = "1" Then
                        chbEventPasscode.Checked = True
                    Else
                        chbEventPasscode.Checked = False
                    End If
                End If
                If IsDBNull(dr.Item("strAddress")) Then
                    txtEventAddress.Clear()
                Else
                    txtEventAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strCity")) Then
                    txtEventCity.Clear()
                Else
                    txtEventCity.Text = dr.Item("strCity")
                End If
                If IsDBNull(dr.Item("strState")) Then
                    mtbEventState.Clear()
                Else
                    mtbEventState.Text = dr.Item("strState")
                End If
                If IsDBNull(dr.Item("numZipCode")) Then
                    mtbEventZipCode.Clear()
                Else
                    mtbEventZipCode.Text = dr.Item("numZipCode")
                End If
                If IsDBNull(dr.Item("numAPBContact")) Then
                    cboEventContact.Text = ""
                Else
                    cboEventContact.SelectedValue = dr.Item("numApbcontact")
                End If
                If IsDBNull(dr.Item("numWebPhoneNumber")) Then
                    mtbEventWebPhoneNumber.Text = ""
                Else
                    mtbEventWebPhoneNumber.Text = dr.Item("numWebPhoneNumber")
                End If
                If IsDBNull(dr.Item("strEventStartTime")) Then
                    txtEventTime.Clear()
                Else
                    txtEventTime.Text = dr.Item("strEventStartTime")
                End If
                If IsDBNull(dr.Item("strEventendTime")) Then
                    txtEventEndTime.Clear()
                Else
                    txtEventEndTime.Text = dr.Item("strEventendTime")
                End If
                If IsDBNull(dr.Item("strWebURL")) Then
                    txtWebsiteURL.Clear()
                Else
                    txtWebsiteURL.Text = dr.Item("strWebURL")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub LoadRegistrationManagement()
        Try
            query = "select " &
            "Res_Registration.numRes_registrationID, " &
            "Res_Event.strTitle as eventTitle,  " &
            "datRegistrationDateTime, " &
            "strConfirmationNumber, strComments, " &
            "STRREGISTRATIONSTATUS, Res_Registration.numGECouserID, " &
            "strSalutation, strFirstName, " &
            "strLastName, strUserEmail, " &
            "OlapUserProfile.strAddress, OlapUserProfile.strCity, " &
            "OlapUserProfile.strState, strZip, " &
            "strCompanyName, strPhonenumber, " &
            "strUserType, " &
            "OLAPUserProfile.strTitle as UserTitle " &
            "from Res_Registration, OLAPUSERProfile, " &
            "res_event, OLAPUserLogIn,  " &
            "RESLK_RegistrationStatus " &
            "where Res_Registration.numGECouserID = OlapUserProfile.numUserID " &
            "and Res_registration.numRes_eventid = Res_Event.numRes_EventId  " &
            "and Res_registration.numRegistrationStatusCode = " &
            "RESLK_RegistrationStatus.NUMRESLK_REGISTRATIONSTATUSID " &
            "and Res_Registration.numGECouserID = OLAPUserLogIn.numuserid " &
            "and convert(int,Res_registration.numRes_EventID) = @eventid "

            Dim p As New SqlParameter("@eventid", selectedEventId)

            dgvRegistrationManagement.DataSource = DB.GetDataTable(query, p)

            dgvRegistrationManagement.RowHeadersVisible = False
            dgvRegistrationManagement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvRegistrationManagement.AllowUserToResizeColumns = True
            dgvRegistrationManagement.AllowUserToAddRows = False
            dgvRegistrationManagement.AllowUserToDeleteRows = False
            dgvRegistrationManagement.AllowUserToOrderColumns = True
            dgvRegistrationManagement.AllowUserToResizeRows = True


            dgvRegistrationManagement.Columns("numRes_registrationID").HeaderText = "ID"
            dgvRegistrationManagement.Columns("numRes_registrationID").DisplayIndex = 0
            dgvRegistrationManagement.Columns("numRes_registrationID").Width = 40
            dgvRegistrationManagement.Columns("numRes_registrationID").Visible = False
            dgvRegistrationManagement.Columns("EventTitle").HeaderText = "Event Title"
            dgvRegistrationManagement.Columns("EventTitle").DisplayIndex = 1
            dgvRegistrationManagement.Columns("EventTitle").Visible = False
            dgvRegistrationManagement.Columns("datRegistrationDateTime").HeaderText = "Reg. Date"
            dgvRegistrationManagement.Columns("datRegistrationDateTime").DisplayIndex = 2
            dgvRegistrationManagement.Columns("strConfirmationNumber").HeaderText = "Confirmation"
            dgvRegistrationManagement.Columns("strConfirmationNumber").DisplayIndex = 3
            dgvRegistrationManagement.Columns("strConfirmationNumber").Visible = False
            dgvRegistrationManagement.Columns("strComments").HeaderText = "Comments"
            dgvRegistrationManagement.Columns("strComments").DisplayIndex = 4
            dgvRegistrationManagement.Columns("STRREGISTRATIONSTATUS").HeaderText = "Registration Status"
            dgvRegistrationManagement.Columns("STRREGISTRATIONSTATUS").DisplayIndex = 5
            dgvRegistrationManagement.Columns("numGECouserID").HeaderText = "GEID"
            dgvRegistrationManagement.Columns("numGECouserID").DisplayIndex = 6
            dgvRegistrationManagement.Columns("numGECouserID").Visible = False
            dgvRegistrationManagement.Columns("strSalutation").HeaderText = "Salutation"
            dgvRegistrationManagement.Columns("strSalutation").DisplayIndex = 7
            dgvRegistrationManagement.Columns("strSalutation").Visible = False
            dgvRegistrationManagement.Columns("strFirstName").HeaderText = "First Name"
            dgvRegistrationManagement.Columns("strFirstName").DisplayIndex = 8
            dgvRegistrationManagement.Columns("strLastName").HeaderText = "Last Name"
            dgvRegistrationManagement.Columns("strLastName").DisplayIndex = 9
            dgvRegistrationManagement.Columns("UserTitle").HeaderText = "Title"
            dgvRegistrationManagement.Columns("UserTitle").DisplayIndex = 10
            dgvRegistrationManagement.Columns("strUserEmail").HeaderText = "User Email"
            dgvRegistrationManagement.Columns("strUserEmail").DisplayIndex = 11
            dgvRegistrationManagement.Columns("strAddress").HeaderText = "Address"
            dgvRegistrationManagement.Columns("strAddress").DisplayIndex = 12
            dgvRegistrationManagement.Columns("strCity").HeaderText = "City"
            dgvRegistrationManagement.Columns("strCity").DisplayIndex = 13
            dgvRegistrationManagement.Columns("strState").HeaderText = "State"
            dgvRegistrationManagement.Columns("strState").DisplayIndex = 14
            dgvRegistrationManagement.Columns("strZip").HeaderText = "Zip"
            dgvRegistrationManagement.Columns("strZip").DisplayIndex = 15
            dgvRegistrationManagement.Columns("strCompanyName").HeaderText = "Company Name"
            dgvRegistrationManagement.Columns("strCompanyName").DisplayIndex = 16

            dgvRegistrationManagement.Columns("strPhonenumber").HeaderText = "Phone #"
            dgvRegistrationManagement.Columns("strPhonenumber").DisplayIndex = 17
            dgvRegistrationManagement.Columns("strUserType").HeaderText = "User Type"
            dgvRegistrationManagement.Columns("strUserType").DisplayIndex = 18
            dgvRegistrationManagement.Columns("strUserType").Visible = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Events Management"
    Private Sub btnSaveNewEvent_Click(sender As Object, e As EventArgs) Handles btnSaveNewEvent.Click
        Try
            If chbEventPasscode.Checked AndAlso chbEventPasscode.Text = "Error" Then
                MsgBox("Passcode is invalid; please fix.", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim EndDate As String = ""
            If DTPEventEndDate.Checked = True Then
                EndDate = DTPEventDate.Text
            End If

            Dim resultcode As DialogResult

            resultcode = 1

            resultcode = MessageBox.Show("This will create a new Event." & vbCrLf &
                  "Click Ok to create a new event.", Me.Text,
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If resultcode = DialogResult.OK Then
                Insert_RES_Event(cboEventStatus.SelectedValue, txtEventTitle.Text, txtEventDescription.Text,
                                 DTPEventDate.Text, EndDate, txtEventVenue.Text,
                                 txtEventAddress.Text, txtEventCity.Text, mtbEventState.Text,
                                 mtbEventZipCode.Text, mtbEventCapacity.Text, txtEventNotes.Text,
                                 cboEventContact.SelectedValue, cboEventWebContact.SelectedValue, mtbEventWebPhoneNumber.Text,
                                 chbGECOlogInRequired.CheckState, chbEventPasscode.CheckState, chbEventPasscode.Text, "1", txtEventTime.Text,
                                 txtEventEndTime.Text, txtWebsiteURL.Text)
                LoadEvent()

                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateEvent_Click(sender As Object, e As EventArgs) Handles btnUpdateEvent.Click
        Try
            If chbEventPasscode.Checked AndAlso chbEventPasscode.Text = "Error" Then
                MsgBox("Passcode is invalid; please fix.", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim EndDate As String = ""
            If DTPEventEndDate.Checked = True Then
                EndDate = DTPEventDate.Text
            End If
            If Update_RES_Event(selectedEventId,
                             cboEventStatus.SelectedValue, txtEventTitle.Text, txtEventDescription.Text,
                             DTPEventDate.Text, EndDate, txtEventVenue.Text,
                             txtEventAddress.Text, txtEventCity.Text, mtbEventState.Text,
                             mtbEventZipCode.Text, mtbEventCapacity.Text, txtEventNotes.Text,
                             cboEventContact.SelectedValue, cboEventWebContact.SelectedValue,
                             chbGECOlogInRequired.CheckState, chbEventPasscode.CheckState, chbEventPasscode.Text, "1", txtEventTime.Text,
                             txtEventEndTime.Text, txtWebsiteURL.Text) = True Then
                LoadEvent()

                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteEvent_Click(sender As Object, e As EventArgs) Handles btnDeleteEvent.Click
        Try
            If Update_RES_Event(selectedEventId,
                                "", "", "",
                             "", "", "",
                             "", "", "",
                             "", "", "",
                             "", "", "",
                             "", "", "0", "", "", "") = True Then
                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbEventPasscode_CheckedChanged(sender As Object, e As EventArgs) Handles chbEventPasscode.CheckedChanged
        Try
            If chbEventPasscode.Checked = True Then
                btnGeneratePasscode.Visible = True
            Else
                btnGeneratePasscode.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function GeneratePasscode() As String
        Try
            Dim r As New Random(Date.Now.Millisecond)
            Dim passcode As String = "GA" & r.Next(100000, 999999)
            If PasscodeExists(passcode) Then
                Return GeneratePasscode()
            Else
                Return passcode
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return "Error"
    End Function

    Private Sub btnGeneratePasscode_Click(sender As Object, e As EventArgs) Handles btnGeneratePasscode.Click
        chbEventPasscode.Text = GeneratePasscode()
    End Sub

    Private Sub btnClearEventManagement_Click(sender As Object, e As EventArgs) Handles btnClearEventManagement.Click
        ClearEventSelection()
    End Sub
    Private Sub ClearEventSelection()
        dgvEvents.ClearSelection()
        ClearEventManagementForm()
    End Sub
    Private Sub ClearEventManagementForm()
        txtEventTitle.Clear()
        txtEventDescription.Clear()
        DTPEventDate.Value = Today
        DTPEventEndDate.Value = Today
        DTPEventEndDate.Checked = False
        txtEventTime.Clear()
        txtEventEndTime.Clear()
        chbEventPasscode.Text = ""
        chbEventPasscode.Checked = False
        cboEventStatus.SelectedIndex = 0
        cboEventContact.SelectedIndex = 0
        cboEventWebContact.SelectedIndex = 0
        txtEventVenue.Clear()
        txtEventAddress.Clear()
        txtEventCity.Clear()
        mtbEventState.Clear()
        mtbEventZipCode.Clear()
        mtbEventCapacity.Clear()
        txtEventNotes.Clear()
        txtWebsiteURL.Clear()
    End Sub

#End Region

#Region "Registration Management"
    Private Sub dgvRegistrationManagement_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvRegistrationManagement.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvRegistrationManagement.HitTest(e.X, e.Y)

            If dgvRegistrationManagement.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvRegistrationManagement(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtRegID.Text = dgvRegistrationManagement(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(1, hti.RowIndex).Value) Then
                    txtRegEventTitle.Text = ""
                Else
                    txtRegEventTitle.Text = dgvRegistrationManagement(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(2, hti.RowIndex).Value) Then
                    DTPRegDateRegistered.Text = ""
                Else
                    DTPRegDateRegistered.Value = dgvRegistrationManagement(2, hti.RowIndex).Value
                End If

                If IsDBNull(dgvRegistrationManagement(3, hti.RowIndex).Value) Then
                    txtRegConfirmationNum.Text = ""
                Else
                    txtRegConfirmationNum.Text = dgvRegistrationManagement(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(4, hti.RowIndex).Value) Then
                    txtRegComments.Text = ""
                Else
                    txtRegComments.Text = dgvRegistrationManagement(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(5, hti.RowIndex).Value) Then
                    cboRegStatus.Text = ""
                Else
                    cboRegStatus.Text = dgvRegistrationManagement(5, hti.RowIndex).Value
                End If

                If IsDBNull(dgvRegistrationManagement(6, hti.RowIndex).Value) Then
                    txtGECOUserID.Text = ""
                Else
                    txtGECOUserID.Text = dgvRegistrationManagement(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(7, hti.RowIndex).Value) Then
                    txtRegSalutation.Text = ""
                Else
                    txtRegSalutation.Text = dgvRegistrationManagement(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(8, hti.RowIndex).Value) Then
                    txtRegFirstName.Text = ""
                Else
                    txtRegFirstName.Text = dgvRegistrationManagement(8, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(9, hti.RowIndex).Value) Then
                    txtRegLastName.Text = ""
                Else
                    txtRegLastName.Text = dgvRegistrationManagement(9, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(10, hti.RowIndex).Value) Then
                    txtRegEmail.Text = ""
                Else
                    txtRegEmail.Text = dgvRegistrationManagement(10, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(11, hti.RowIndex).Value) Then
                    txtRegAddress.Text = ""
                Else
                    txtRegAddress.Text = dgvRegistrationManagement(11, hti.RowIndex).Value
                End If

                If IsDBNull(dgvRegistrationManagement(12, hti.RowIndex).Value) Then
                    txtRegCity.Text = ""
                Else
                    txtRegCity.Text = dgvRegistrationManagement(12, hti.RowIndex).Value
                End If

                If IsDBNull(dgvRegistrationManagement(13, hti.RowIndex).Value) Then
                    mtbRegState.Text = ""
                Else
                    mtbRegState.Text = dgvRegistrationManagement(13, hti.RowIndex).Value
                End If

                If IsDBNull(dgvRegistrationManagement(14, hti.RowIndex).Value) Then
                    mtbRegZipCode.Text = ""
                Else
                    mtbRegZipCode.Text = dgvRegistrationManagement(14, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(16, hti.RowIndex).Value) Then
                    mtbRegPhoneNo.Text = ""
                    mtbRegPhoneExt.Text = ""
                Else
                    mtbRegPhoneNo.Text = Mid(dgvRegistrationManagement(16, hti.RowIndex).Value, 1, 10)
                    If dgvRegistrationManagement(16, hti.RowIndex).Value.ToString.Length > 10 Then
                        mtbRegPhoneExt.Text = Mid(dgvRegistrationManagement(16, hti.RowIndex).Value, 11)
                    Else
                        mtbRegPhoneExt.Text = ""
                    End If

                End If
                If IsDBNull(dgvRegistrationManagement(17, hti.RowIndex).Value) Then
                    cboRegUserType.Text = ""
                Else
                    cboRegUserType.Text = dgvRegistrationManagement(17, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationManagement(15, hti.RowIndex).Value) Then
                    txtRegTitle.Text = ""
                Else
                    txtRegTitle.Text = dgvRegistrationManagement(15, hti.RowIndex).Value
                End If


            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub btnModifyRegistration_Click(sender As Object, e As EventArgs) Handles btnModifyRegistration.Click
        Try

            If Update_RES_Registration(txtRegID.Text, txtRegConfirmationNum.Text,
                                       cboRegStatus.SelectedValue, DTPRegDateRegistered.Text) = True Then

                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
                LoadRegistrationManagement()
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMapEventLocation_Click(sender As Object, e As EventArgs) Handles btnMapEventLocation.Click
        Try


            Dim StreetAddress As String = "4244 International Parkway"
            Dim City As String = "Atlanta"
            Dim State As String = "GA"
            Dim ZipCode As String = "30354"

            If txtEventAddress.Text <> "" Then
                StreetAddress = txtEventAddress.Text
            End If
            If txtEventCity.Text <> "" Then
                City = txtEventCity.Text
            End If
            If mtbEventState.Text <> "" Then
                State = mtbEventState.Text
            End If
            If mtbEventZipCode.Text <> "" Then
                ZipCode = mtbEventZipCode.Text
            End If

            Dim mapString As String = StreetAddress & "+" & City & "+" & State & "+" & ZipCode
            OpenMapUrl(mapString, Me)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub

    Private Sub btnExportRegistrantsToExcel_Click(sender As Object, e As EventArgs) Handles btnExportRegistrantsToExcel.Click
        dgvOverviewRegistrants.ExportToExcel()
    End Sub

#Region "Emails"

    Private Sub SendEmail(Optional whichSet As String = "")
        Dim subject As String = selectedEvent.Title & " – " & selectedEvent.StartDate
        Dim body As String = selectedEvent.Title & vbNewLine & vbNewLine &
            selectedEvent.Description & vbNewLine & vbNewLine &
            "Starts on: " & selectedEvent.StartDate.Value.ToString(DateFormat)
        If selectedEvent.StartTime IsNot Nothing Then
            body &= ", " & selectedEvent.StartTime
        End If
        body &= vbNewLine & "Venue: " & selectedEvent.Venue & vbNewLine & vbNewLine &
            selectedEvent.Address.ToString

        Dim recipientsBCC As List(Of String) = GetCorrectRecipients(whichSet)

        Me.Cursor = Cursors.AppStarting
        If Not CreateEmail(subject, body, recipientsBCC:=recipientsBCC.ToArray) Then
            MsgBox("There was an error sending the message. Please try again.", MsgBoxStyle.OkOnly, "Error")
        End If
        Me.Cursor = Nothing
    End Sub

    Private Function GetCorrectRecipients(Optional statusFilter As String = "") As List(Of String)
        Dim recipients As New List(Of String)

        For Each row As DataGridViewRow In dgvOverviewRegistrants.Rows
            If statusFilter = "" OrElse row.Cells("STRREGISTRATIONSTATUS").Value = statusFilter Then
                recipients.Add(row.Cells("STRUSEREMAIL").Value.ToString)
            End If
        Next

        Return recipients
    End Function

    Private Sub btnEmailAll_Click(sender As Object, e As EventArgs) Handles btnEmailAll.Click
        SendEmail()
    End Sub

    Private Sub btnEmailRegistrants_Click(sender As Object, e As EventArgs) Handles btnEmailRegistrants.Click
        SendEmail("Confirmed")
    End Sub

    Private Sub btnEmailWaitList_Click(sender As Object, e As EventArgs) Handles btnEmailWaitList.Click
        SendEmail("Waiting List")
    End Sub

#End Region

#Region "Insert/Update event/registration"

    Private Sub Insert_RES_Event(EventStatusCode As String, Title As String,
                            Description As String, StartDateTime As String,
                            EndDateTime As String, Venue As String,
                            Address As String, City As String,
                            State As String, ZipCode As String,
                            Capacity As String, Notes As String,
                            APBContact As String,
                            WebContact As String, WebPhoneNumber As String,
                            LogInRequired As String,
                            PassCodeRequired As String, PassCode As String,
                            Active As String, EventTime As String,
                            EventEndTime As String, WebURL As String)

        Try
            If LogInRequired = True Then
                LogInRequired = "1"
            Else
                LogInRequired = "0"
            End If
            If PassCodeRequired = "" Then
                PassCode = "1"
            Else
                If PassCodeRequired = False Then
                    PassCode = "1"
                Else
                    PassCode = PassCode
                End If
            End If
            If LogInRequired = True Then
                LogInRequired = "1"
            Else
                LogInRequired = "0"
            End If

            query = "Insert into RES_Event " &
                     "(numRes_EventID, numEventStatusCode, " &
                     "strUserGCode, strTitle, " &
                     "strDescription, datStartDate, " &
                     "datEndDate, strVenue, " &
                     "numCapacity, strNotes, " &
                     "strMultipleregistrations, Active, " &
                     "createDateTime, UpdateUser, " &
                     "UpdateDateTime, strLogInRequired, " &
                     "strPassCode, strAddress, " &
                     "strCity, strState, " &
                     "numZipCode, numAPBContact, " &
                     "numWebPhoneNumber, strEventStartTime, " &
                     "strEventEndTime, strWebURL) " &
                     "values " &
                     "((select " &
                     "case when max(convert(int,numres_eventID)) is null then 1 " &
                     "else max(convert(int,NUMRES_EVENTID)) + 1 End  " &
                     "from Res_event), @numEventStatusCode, " &
                     "@strUserGCode, @strTitle, " &
                     "@strDescription, @datStartDate, " &
                     "@datEndDate, @strVenue, " &
                     "@numCapacity, @strNotes, " &
                     "null, @Active, " &
                     "getdate(), @UpdateUser, " &
                     "getdate(), @strLogInRequired, " &
                     "@strPassCode, @strAddress, " &
                     "@strCity, @strState, " &
                     "@numZipCode, @numAPBContact, " &
                     "@numWebPhoneNumber, @strEventStartTime, " &
                     "@strEventEndTime, @strWebURL) "

            Dim p As SqlParameter() = {
                New SqlParameter("@numEventStatusCode", EventStatusCode),
                New SqlParameter("@strUserGCode", WebContact),
                New SqlParameter("@strTitle", Title),
                New SqlParameter("@strDescription", Description),
                New SqlParameter("@datStartDate", StartDateTime),
                New SqlParameter("@datEndDate", EndDateTime),
                New SqlParameter("@strVenue", Venue),
                New SqlParameter("@numCapacity", Capacity),
                New SqlParameter("@strNotes", Notes),
                New SqlParameter("@Active", Active),
                New SqlParameter("@UpdateUser", CurrentUser.UserID),
                New SqlParameter("@strLogInRequired", LogInRequired),
                New SqlParameter("@strPassCode", PassCode),
                New SqlParameter("@strAddress", Address),
                New SqlParameter("@strCity", City),
                New SqlParameter("@strState", State),
                New SqlParameter("@numZipCode", ZipCode),
                New SqlParameter("@numAPBContact", APBContact),
                New SqlParameter("@numWebPhoneNumber", WebPhoneNumber),
                New SqlParameter("@strEventStartTime", EventTime),
                New SqlParameter("@strEventEndTime", EventEndTime),
                New SqlParameter("@strWebURL", WebURL)
            }

            DB.RunCommand(query, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Function Update_RES_Event(Res_EventID As String,
                           EventStatusCode As String, Title As String,
                           Description As String, StartDateTime As String,
                           EndDateTime As String, Venue As String,
                           Address As String, City As String,
                           State As String, ZipCode As String,
                           Capacity As String, Notes As String,
                           APBContact As String,
                           WebContact As String,
                           LogInRequired As String,
                           PassCodeRequired As String, PassCode As String,
                           Active As String, EventTime As String,
                           EventEndTime As String, WebURL As String) As Boolean
        Try
            Dim SQL As String = ""
            If IsDBNull(EventStatusCode) Then
            Else
                If EventStatusCode <> "" Then
                    SQL = "numEventStatusCode = @EventStatusCode, "
                End If
            End If
            If IsDBNull(Title) Then
            Else
                If Title <> "" Then
                    SQL = SQL & "strTitle = @Title, "
                End If
            End If
            If IsDBNull(Description) Then
            Else
                If Description <> "" Then
                    SQL = SQL & "strDescription = @Description, "
                End If
            End If
            If IsDBNull(StartDateTime) Then
            Else
                If StartDateTime <> "" Then
                    SQL = SQL & "datStartDate = @StartDateTime, "
                End If
            End If
            If IsDBNull(EndDateTime) Then
            Else
                If EndDateTime <> "" Then
                    SQL = SQL & "datEndDate = @EndDateTime, "
                End If
            End If
            If IsDBNull(Venue) Then
            Else
                If Venue <> "" Then
                    SQL = SQL & "strVenue = @Venue, "
                End If
            End If
            If IsDBNull(Address) Then
            Else
                If Address <> "" Then
                    SQL = SQL & "strAddress = @Address, "
                End If
            End If
            If IsDBNull(City) Then
            Else
                If City <> "" Then
                    SQL = SQL & "strCity = @City, "
                End If
            End If
            If IsDBNull(State) Then
            Else
                If State <> "" Then
                    SQL = SQL & "strState = @State, "
                End If
            End If
            If IsDBNull(ZipCode) Then
            Else
                If ZipCode <> "" Then
                    SQL = SQL & "numZipCode = @ZipCode, "
                End If
            End If
            If IsDBNull(Capacity) Then
            Else
                If Capacity <> "" Then
                    SQL = SQL & "numCapacity = @Capacity, "
                End If
            End If
            If IsDBNull(Notes) Then
            Else
                If Notes <> "" Then
                    SQL = SQL & "strNotes = @Notes, "
                End If
            End If
            If IsDBNull(APBContact) Then
            Else
                If APBContact <> "" Then
                    SQL = SQL & "numAPBContact = @APBContact, "
                End If
            End If
            If IsDBNull(WebContact) Then
            Else
                If WebContact <> "" Then
                    SQL = SQL & "strUserGCode = @WebContact, "
                End If
            End If
            If IsDBNull(LogInRequired) Then
                LogInRequired = "0"
            Else
                If LogInRequired <> "" Then
                    If LogInRequired = True Then
                        LogInRequired = "1"
                    Else
                        LogInRequired = "0"
                    End If
                    SQL = SQL & "strLogInRequired = @LogInRequired, "
                Else
                    LogInRequired = "0"
                End If
            End If
            If IsDBNull(PassCodeRequired) Then
                SQL = SQL & "strPasscode = '1', "
            Else
                If PassCodeRequired = "0" Or PassCode = "" Then
                    SQL = SQL & "strPasscode = '1', "
                Else
                    SQL = SQL & "strPassCode = @PassCode, "
                End If
            End If
            If IsDBNull(EventTime) Then
            Else
                If EventTime <> "" Then
                    SQL = SQL & "strEventStartTime = @EventTime, "
                End If
            End If
            If IsDBNull(EventEndTime) Then
            Else
                If EventEndTime <> "" Then
                    SQL = SQL & "strEventEndTime = @EventEndTime, "
                End If
            End If
            If IsDBNull(WebURL) Then
            Else
                If WebURL <> "" Then
                    SQL = SQL & "strWebURL = @WebURL, "
                End If
            End If

            If IsDBNull(Active) Then
            Else
                SQL = SQL & "active = @Active, "
            End If
            If SQL <> "" Then
                SQL = "Update Res_Event set " &
                SQL & "updateUser = @user , " &
                "updateDateTime =  GETDATE()  " &
                "where convert(int,NUMRES_EVENTID) = @Res_EventID "

                Dim p As SqlParameter() = {
                    New SqlParameter("@Res_EventID", Res_EventID),
                    New SqlParameter("@EventStatusCode", EventStatusCode),
                    New SqlParameter("@Title", Title),
                    New SqlParameter("@Description", Description),
                    New SqlParameter("@StartDateTime", StartDateTime),
                    New SqlParameter("@EndDateTime", EndDateTime),
                    New SqlParameter("@Venue", Venue),
                    New SqlParameter("@Address", Address),
                    New SqlParameter("@City", City),
                    New SqlParameter("@State", State),
                    New SqlParameter("@ZipCode", ZipCode),
                    New SqlParameter("@Capacity", Capacity),
                    New SqlParameter("@Notes", Notes),
                    New SqlParameter("@APBContact", APBContact),
                    New SqlParameter("@WebContact", WebContact),
                    New SqlParameter("@LogInRequired", LogInRequired),
                    New SqlParameter("@PassCodeRequired", PassCodeRequired),
                    New SqlParameter("@PassCode", PassCode),
                    New SqlParameter("@Active", Active),
                    New SqlParameter("@EventTime", EventTime),
                    New SqlParameter("@EventEndTime", EventEndTime),
                    New SqlParameter("@WebURL", WebURL),
                    New SqlParameter("@user", CurrentUser.UserID)
                }

                DB.RunCommand(SQL, p)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Update_RES_Registration(RegistrationID As String, Confirmation As String,
                                     RegStatusCode As String, RegDate As String) As Boolean
        Try

            query = "Update Res_Registration set " &
            "numREgistrationStatusCode = @numREgistrationStatusCode, " &
            "datRegistrationDateTime = @datRegistrationDateTime " &
            "where numRes_RegistrationID = @numRes_RegistrationID " &
            "and strConfirmationNumber = @strConfirmationNumber "

            Dim p As SqlParameter() = {
                New SqlParameter("@numREgistrationStatusCode", RegStatusCode),
                New SqlParameter("@datRegistrationDateTime", RegDate),
                New SqlParameter("@numRes_RegistrationID", RegistrationID),
                New SqlParameter("@strConfirmationNumber", Confirmation)
            }

            DB.RunCommand(query, p)
            Return True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

#End Region

#Region "DAL"

    Private Function PasscodeExists(id As String) As Boolean
        Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM RES_EVENT WHERE STRPASSCODE = @id"
        Dim parameter As New SqlParameter("@id", id)
        Return DB.GetBoolean(query, parameter)
    End Function

#End Region

End Class