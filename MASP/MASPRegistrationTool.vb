Imports Oracle.DataAccess.Client
Imports System.Collections.Generic
Imports JohnGaltProject.DAL.EventRegistration

Public Class MASPRegistrationTool
    Dim ds As DataSet
    Dim da As OracleDataAdapter

#Region "Properties"

    Dim selectedEventId As Decimal

#End Region


    Private Sub MASPRegistrationTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)

        LoadComboBoxes()
        LoadForm()
        LoadEventList()

        btnGeneratePasscode.Visible = False
        chbEventPasscode.Text = ""

    End Sub

#Region "Form combo boxes"

    Private Sub LoadComboBoxes()
        LoadEventContactCombos()
        LoadEventStatusCombo()
        LoadRegistrationStatusCombo()
    End Sub

    Private Sub LoadEventContactCombos()
        Dim staff As DataTable = DAL.GetAllActiveStaffAsDataTable

        Dim nullRow As DataRow = staff.NewRow
        nullRow("NUMUSERID") = DBNull.Value
        nullRow("AlphaName") = "Select a contact…"
        staff.Rows.InsertAt(nullRow, 0)

        Dim webStaff As DataTable = staff.Copy

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
        Dim statuses As Dictionary(Of Integer, String) = GetEventStatusesAsDictionary(True, "Select a status…")
        If statuses.Count > 0 Then DB.BindDictionaryToComboBox(statuses, cboEventStatus)
    End Sub

    Private Sub LoadRegistrationStatusCombo()
        ' Get list of Registration Status types and bind that list to the combobox
        Dim statuses As Dictionary(Of Integer, String) = GetRegistrationStatusesAsDictionary(True, "Select a status…")
        If statuses.Count > 0 Then DB.BindDictionaryToComboBox(statuses, cboRegStatus)
    End Sub

#End Region

    Sub LoadForm()
        ' CONTINUE separating this Sub into individual Combobox Subs
        Try
            Dim dtAPBContact As New DataTable
            Dim dtWebContact As New DataTable
            Dim dtEventStatus As New DataTable
            Dim dtRegistrationStatus As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            ds = New DataSet



            SQL = "Select " & _
            "numResLK_RegistrationStatusID, strRegistrationStatus " & _
            "from " & DBNameSpace & ".RESLK_RegistrationStatus " & _
            "where Active = '1' " & _
            "order by strRegistrationStatus "

            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "RegistrationStatus")

            dtRegistrationStatus.Columns.Add("strRegistrationStatus", GetType(System.String))
            dtRegistrationStatus.Columns.Add("numResLK_RegistrationStatusID", GetType(System.String))

            drNewRow = dtRegistrationStatus.NewRow()
            drNewRow("strRegistrationStatus") = " "
            drNewRow("numResLK_RegistrationStatusID") = ""
            dtRegistrationStatus.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("RegistrationStatus").Rows()
                drNewRow = dtRegistrationStatus.NewRow()
                drNewRow("strRegistrationStatus") = drDSRow("strRegistrationStatus")
                drNewRow("numResLK_RegistrationStatusID") = drDSRow("numResLK_RegistrationStatusID")
                dtRegistrationStatus.Rows.Add(drNewRow)
            Next

            With cboRegStatus
                .DataSource = dtRegistrationStatus
                .DisplayMember = "strRegistrationStatus"
                .ValueMember = "numResLK_RegistrationStatusID"
                .SelectedValue = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadEventList()
        Try
            SQL = ""
            If rdbAllEvents.Checked = True Then
                SQL = "and datStartDate is not null "
            End If
            If rdbUpcomingEvents.Checked = True Then
                SQL = "and datStartDate > sysdate - 1"
            End If
            If rdbPastEvents.Checked = True Then
                SQL = "and datStartDate <= sysdate "
            End If

            SQL = "Select " & _
            "numRes_EventID,  " & _
            "strTitle, strDescription, " & _
            "datStartDate, strEventStartTime, " & _
            "strVenue, strNotes " & _
            "from " & DBNameSpace & ".RES_EVENT " & _
            "where Active = '1' " & _
            SQL

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "Event")

            dgvRegistrationEvent.DataSource = ds
            dgvRegistrationEvent.DataMember = "Event"

            dgvRegistrationEvent.RowHeadersVisible = False
            dgvRegistrationEvent.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvRegistrationEvent.AllowUserToResizeColumns = True
            dgvRegistrationEvent.AllowUserToAddRows = False
            dgvRegistrationEvent.AllowUserToDeleteRows = False
            dgvRegistrationEvent.AllowUserToOrderColumns = True
            dgvRegistrationEvent.AllowUserToResizeRows = True

            dgvRegistrationEvent.Columns("numRes_EventID").HeaderText = "ID"
            dgvRegistrationEvent.Columns("numRes_EventID").DisplayIndex = 0
            dgvRegistrationEvent.Columns("numRes_EventID").Width = 40
            dgvRegistrationEvent.Columns("numRes_EventID").Visible = False
            dgvRegistrationEvent.Columns("strTitle").HeaderText = "Event Title"
            dgvRegistrationEvent.Columns("strTitle").DisplayIndex = 1
            dgvRegistrationEvent.Columns("strDescription").HeaderText = "Description"
            dgvRegistrationEvent.Columns("strDescription").DisplayIndex = 2
            dgvRegistrationEvent.Columns("datStartDate").HeaderText = "Event Date"
            dgvRegistrationEvent.Columns("datStartDate").DisplayIndex = 3
            dgvRegistrationEvent.Columns("strEventStartTime").HeaderText = "Event Time"
            dgvRegistrationEvent.Columns("strEventStartTime").DisplayIndex = 4
            dgvRegistrationEvent.Columns("strVenue").HeaderText = "Venue"
            dgvRegistrationEvent.Columns("strVenue").DisplayIndex = 5
            dgvRegistrationEvent.Columns("strNotes").HeaderText = "Notes"
            dgvRegistrationEvent.Columns("strNotes").DisplayIndex = 6
            dgvRegistrationEvent.Columns("strNotes").Width = 200

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub


    Private Sub btnFilterEvents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterEvents.Click
        Try

            LoadEventList()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvRegistrationEvent_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvRegistrationEvent.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvRegistrationEvent.HitTest(e.X, e.Y)

            If dgvRegistrationEvent.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvRegistrationEvent(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtSelectedEventID.Text = dgvRegistrationEvent(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationEvent(1, hti.RowIndex).Value) Then
                    lblEventTitle.Text = "Event Title "
                Else
                    lblEventTitle.Text = "Event Title: " & dgvRegistrationEvent(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvRegistrationEvent(3, hti.RowIndex).Value) Then
                    lblEVentDate.Text = "Event Date: "
                Else
                    lblEVentDate.Text = "Event Date: " & Format(dgvRegistrationEvent(3, hti.RowIndex).Value, "dd-MMM-yyyy")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDetails.Click
        Try

            LoadEventOverview()
            txtEventID.Text = txtSelectedEventID.Text
            If txtEventID.Text <> "" Then
                LoadEventManagement()
            End If
            If txtSelectedEventID.Text <> "" Then
                LoadRegistrationManagement()
            End If



        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#Region "Load Event Data"
    Sub LoadEventOverview()
        Try

            SQL = "Select " & _
               "numRes_EventID, " & _
               "strEventStatus, strUserGCode, " & _
               "strTitle, " & _
               "" & DBNameSpace & ".Res_Event.strDescription, " & _
               "datStartDate, datEndDate, " & _
               "strVenue, " & _
               "numCapacity, strNotes, " & _
               "strLoginRequired, strPassCode, " & _
               "strAddress, strCity, " & _
               "strState, numZipCode, " & _
               "numAPBContact, numWebPhoneNumber, " & _
               "strEventStartTime, strEventEndTime " & _
               "From " & DBNameSpace & ".RES_Event, " & DBNameSpace & ".RESLK_EVENTStatus " & _
               "where " & DBNameSpace & ".Res_Event.numEventStatusCode = " & DBNameSpace & ".ResLK_EventStatus.numResLK_EventStatusID " & _
               "and nuMRes_EventID = '" & txtSelectedEventID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strTitle")) Then
                    lblEvent.Text = ""
                Else
                    lblEvent.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    lblDescription.Text = ""
                Else
                    lblDescription.Text = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("datStartDate")) Then
                    lblEventDateTime.Text = ""
                Else
                    lblEventDateTime.Text = dr.Item("datStartDate")
                End If
                If IsDBNull(dr.Item("strEventStartTime")) Then
                Else
                    lblEventDateTime.Text = lblEventDateTime.Text & " - " & dr.Item("strEventStartTime")
                End If
                If IsDBNull(dr.Item("strLogInRequired")) Then
                    lblLogInRequired.Text = ""
                Else
                    If dr.Item("strLogInRequired") = "1" Then
                        lblLogInRequired.Text = "True"
                    Else
                        lblLogInRequired.Text = "False"
                    End If
                End If
                If IsDBNull(dr.Item("strPassCode")) Then
                    lblPassCode.Text = ""
                Else
                    lblPassCode.Text = dr.Item("strPasscode")
                End If
                If IsDBNull(dr.Item("strEventStatus")) Then
                    lblEventStatus.Text = ""
                Else
                    lblEventStatus.Text = dr.Item("strEventStatus")
                End If
                If IsDBNull(dr.Item("numCapacity")) Then
                    lblEventCapacity.Text = ""
                Else
                    lblEventCapacity.Text = dr.Item("numCapacity")
                End If
                If IsDBNull(dr.Item("strVenue")) Then
                    lblVenue.Text = ""
                Else
                    lblVenue.Text = dr.Item("strVenue")
                End If
                If IsDBNull(dr.Item("strAddress")) Then
                    lblVenue.Text = lblVenue.Text
                Else
                    lblVenue.Text = lblVenue.Text & vbCrLf & dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strCity")) Then
                    lblVenue.Text = lblVenue.Text
                Else
                    lblVenue.Text = lblVenue.Text & vbCrLf & dr.Item("strCity")
                End If
                If IsDBNull(dr.Item("strState")) Then
                    lblVenue.Text = lblVenue.Text
                Else
                    lblVenue.Text = lblVenue.Text & ", " & dr.Item("strState")
                End If
                If IsDBNull(dr.Item("numZipCode")) Then
                    lblVenue.Text = lblVenue.Text
                Else
                    lblVenue.Text = lblVenue.Text & " " & dr.Item("numZipCode")
                End If
                If IsDBNull(dr.Item("strNotes")) Then
                    lblNotes.Text = ""
                Else
                    lblNotes.Text = dr.Item("strNotes")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "count(*) as RegNum " & _
            "from " & DBNameSpace & ".Res_registration " & _
            "where numRes_EventID = '" & txtSelectedEventID.Text & "' " & _
            "and numRegistrationStatusCode = '1' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("RegNum")) Then
                    lblNumberRegistered.Text = "0"
                Else
                    lblNumberRegistered.Text = dr.Item("RegNum")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "count(*) as RegNum " & _
            "from " & DBNameSpace & ".Res_registration " & _
            "where numRes_EventID = '" & txtSelectedEventID.Text & "' " & _
            "and numRegistrationStatusCode = '2' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("RegNum")) Then
                    lblWaitingList.Text = "0"
                Else
                    lblWaitingList.Text = dr.Item("RegNum")
                End If
            End While
            dr.Close()

            SQL = "select " & _
         "" & DBNameSpace & ".Res_Registration.numRes_registrationID, " & _
         "" & DBNameSpace & ".Res_Event.strTitle as eventTitle,  " & _
         "datRegistrationDateTime, " & _
         " strComments, " & _
         "STRREGISTRATIONSTATUS,  " & _
         "  strFirstName, " & _
         "strLastName, strUserEmail, " & _
         "  " & _
         "    " & _
         "strCompanyName, strPhonenumber  " & _
         " " & _
         "  " & _
         "from " & DBNameSpace & ".Res_Registration, " & DBNameSpace & ".OLAPUSERProfile, " & _
         "" & DBNameSpace & ".res_event, " & DBNameSpace & ".OLAPUserLogIn,  " & _
         "" & DBNameSpace & ".RESLK_RegistrationStatus " & _
         "where " & DBNameSpace & ".Res_Registration.numGECouserID = " & DBNameSpace & ".OlapUserProfile.numUserID " & _
         "and " & DBNameSpace & ".Res_registration.numRes_eventid = " & DBNameSpace & ".Res_Event.numRes_EventId  " & _
         "and " & DBNameSpace & ".Res_registration.numRegistrationStatusCode = " & _
         "" & DBNameSpace & ".RESLK_RegistrationStatus.NUMRESLK_REGISTRATIONSTATUSID " & _
         "and " & DBNameSpace & ".Res_Registration.numGECouserID = " & DBNameSpace & ".OLAPUserLogIn.numuserid " & _
         "and " & DBNameSpace & ".Res_registration.numRes_EventID = '" & txtSelectedEventID.Text & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "Registered")
            dgvOverviewRegistrants.DataSource = ds
            dgvOverviewRegistrants.DataMember = "Registered"

            dgvOverviewRegistrants.RowHeadersVisible = False
            dgvRegistrationManagement.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvOverviewRegistrants.AllowUserToResizeColumns = True
            dgvOverviewRegistrants.AllowUserToAddRows = False
            dgvOverviewRegistrants.AllowUserToDeleteRows = False
            dgvOverviewRegistrants.AllowUserToOrderColumns = True
            dgvOverviewRegistrants.AllowUserToResizeRows = True


            dgvOverviewRegistrants.Columns("numRes_registrationID").HeaderText = "ID"
            dgvOverviewRegistrants.Columns("numRes_registrationID").DisplayIndex = 0
            dgvOverviewRegistrants.Columns("numRes_registrationID").Width = 40
            dgvOverviewRegistrants.Columns("numRes_registrationID").Visible = False

            dgvOverviewRegistrants.Columns("EventTitle").HeaderText = "Event Title"
            dgvOverviewRegistrants.Columns("EventTitle").DisplayIndex = 1
            dgvOverviewRegistrants.Columns("EventTitle").Visible = False
            dgvOverviewRegistrants.Columns("datRegistrationDateTime").HeaderText = "Reg. Date"
            dgvOverviewRegistrants.Columns("datRegistrationDateTime").DisplayIndex = 2
            dgvOverviewRegistrants.Columns("strFirstName").HeaderText = "First Name"
            dgvOverviewRegistrants.Columns("strFirstName").DisplayIndex = 3
            dgvOverviewRegistrants.Columns("strLastName").HeaderText = "Last Name"
            dgvOverviewRegistrants.Columns("strLastName").DisplayIndex = 4
            dgvOverviewRegistrants.Columns("strComments").HeaderText = "Comments"
            dgvOverviewRegistrants.Columns("strComments").DisplayIndex = 5
            dgvOverviewRegistrants.Columns("STRREGISTRATIONSTATUS").HeaderText = "Registration Status"
            dgvOverviewRegistrants.Columns("STRREGISTRATIONSTATUS").DisplayIndex = 6
            dgvOverviewRegistrants.Columns("strUserEmail").HeaderText = "User Email"
            dgvOverviewRegistrants.Columns("strUserEmail").DisplayIndex = 7
            dgvOverviewRegistrants.Columns("strPhonenumber").HeaderText = "Phone #"
            dgvOverviewRegistrants.Columns("strPhonenumber").DisplayIndex = 8
            dgvOverviewRegistrants.Columns("strCompanyName").HeaderText = "Company Name"
            dgvOverviewRegistrants.Columns("strCompanyName").DisplayIndex = 9

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadEventManagement()
        Try
            SQL = "Select " & _
            "numRes_EventID, " & _
            "numEventStatusCode, strUserGCode, " & _
            "strTitle, strDescription, " & _
            "datStartDate, datEndDate, " & _
            "strVenue, " & _
            "numCapacity, strNotes, " & _
            "strLoginRequired, strPassCode, " & _
            "strAddress, strCity, " & _
            "strState, numZipCode, " & _
            "numAPBContact, numWebPhoneNumber, " & _
            "strEventStartTime, strEventEndTime, " & _
            "strWebURL " & _
            "From " & DBNameSpace & ".RES_Event " & _
            "where nuMRes_EventID = '" & txtEventID.Text & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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
                    DTPEventDate.Text = OracleDate
                Else
                    DTPEventDate.Text = dr.Item("datStartDate")
                End If
                If IsDBNull(dr.Item("datEndDate")) Then
                    DTPEventEndDate.Text = OracleDate
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
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadRegistrationManagement()
        Try
            SQL = "select " & _
            "" & DBNameSpace & ".Res_Registration.numRes_registrationID, " & _
            "" & DBNameSpace & ".Res_Event.strTitle as eventTitle,  " & _
            "datRegistrationDateTime, " & _
            "strConfirmationNumber, strComments, " & _
            "STRREGISTRATIONSTATUS, " & DBNameSpace & ".Res_Registration.numGECouserID, " & _
            "strSalutation, strFirstName, " & _
            "strLastName, strUserEmail, " & _
            "" & DBNameSpace & ".OlapUserProfile.strAddress, " & DBNameSpace & ".OlapUserProfile.strCity, " & _
            "" & DBNameSpace & ".OlapUserProfile.strState, strZip, " & _
            "strCompanyName, strPhonenumber, " & _
            "strUserType, " & _
            "" & DBNameSpace & ".OLAPUserProfile.strTitle as UserTitle " & _
            "from " & DBNameSpace & ".Res_Registration, " & DBNameSpace & ".OLAPUSERProfile, " & _
            "" & DBNameSpace & ".res_event, " & DBNameSpace & ".OLAPUserLogIn,  " & _
            "" & DBNameSpace & ".RESLK_RegistrationStatus " & _
            "where " & DBNameSpace & ".Res_Registration.numGECouserID = " & DBNameSpace & ".OlapUserProfile.numUserID " & _
            "and " & DBNameSpace & ".Res_registration.numRes_eventid = " & DBNameSpace & ".Res_Event.numRes_EventId  " & _
            "and " & DBNameSpace & ".Res_registration.numRegistrationStatusCode = " & _
            "" & DBNameSpace & ".RESLK_RegistrationStatus.NUMRESLK_REGISTRATIONSTATUSID " & _
            "and " & DBNameSpace & ".Res_Registration.numGECouserID = " & DBNameSpace & ".OLAPUserLogIn.numuserid " & _
            "and " & DBNameSpace & ".Res_registration.numRes_EventID = '" & txtSelectedEventID.Text & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "Registered")
            dgvRegistrationManagement.DataSource = ds
            dgvRegistrationManagement.DataMember = "Registered"

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
            ' dgvRegistrationManagement.Columns("datRegistrationDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy HH:MM"
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
            'dgvRegistrationManagement.Columns("strComments").HeaderText = "Comments"
            'dgvRegistrationManagement.Columns("strComments").DisplayIndex = 19
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadReports()
        Try


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Events Management"
    Private Sub btnSaveNewEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNewEvent.Click
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

            resultcode = MessageBox.Show("This will create a new Event." & vbCrLf & _
                  "Click Yes to create a new event.", Me.Text, _
                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            Select Case resultcode
                Case Windows.Forms.DialogResult.Yes
                    txtEventID.Text = Insert_RES_Event(cboEventStatus.SelectedValue, txtEventTitle.Text, txtEventDescription.Text, _
                                     DTPEventDate.Text, EndDate, txtEventVenue.Text, _
                                     txtEventAddress.Text, txtEventCity.Text, mtbEventState.Text, _
                                     mtbEventZipCode.Text, mtbEventCapacity.Text, txtEventNotes.Text, _
                                     cboEventContact.SelectedValue, cboEventWebContact.SelectedValue, mtbEventWebPhoneNumber.Text, _
                                     chbGECOlogInRequired.CheckState, chbEventPasscode.CheckState, chbEventPasscode.Text, "1", txtEventTime.Text, _
                                     txtEventEndTime.Text, txtWebsiteURL.Text)
                    LoadEventList()

                    MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
                Case Else
                    Exit Sub
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEvent.Click
        Try
            If chbEventPasscode.Checked AndAlso chbEventPasscode.Text = "Error" Then
                MsgBox("Passcode is invalid; please fix.", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim EndDate As String = ""
            If DTPEventEndDate.Checked = True Then
                EndDate = DTPEventDate.Text
            End If
            If Update_RES_Event(txtEventID.Text, _
                             cboEventStatus.SelectedValue, txtEventTitle.Text, txtEventDescription.Text, _
                             DTPEventDate.Text, EndDate, txtEventVenue.Text, _
                             txtEventAddress.Text, txtEventCity.Text, mtbEventState.Text, _
                             mtbEventZipCode.Text, mtbEventCapacity.Text, txtEventNotes.Text, _
                             cboEventContact.SelectedValue, cboEventWebContact.SelectedValue, mtbEventWebPhoneNumber.Text, _
                             chbGECOlogInRequired.CheckState, chbEventPasscode.CheckState, chbEventPasscode.Text, "1", txtEventTime.Text, _
                             txtEventEndTime.Text, txtWebsiteURL.Text) = True Then
                LoadEventList()

                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEvent.Click
        Try
            If Update_RES_Event(txtEventID.Text, _
                                "", "", "", _
                             "", "", "", _
                             "", "", "", _
                             "", "", "", _
                             "", "", "", "", _
                             "", "", "0", "", "", "") = True Then
                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub chbEventPasscode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbEventPasscode.CheckedChanged
        Try
            If chbEventPasscode.Checked = True Then
                btnGeneratePasscode.Visible = True
            Else
                btnGeneratePasscode.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Function GeneratePasscode() As String
        Try
            Dim r As New Random(System.DateTime.Now.Millisecond)
            Dim passcode As String = "GA" & r.Next(100000, 999999)
            If PasscodeExists(passcode) Then
                Return GeneratePasscode()
            Else
                Return passcode
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return "Error"
    End Function

    Private Sub btnGeneratePasscode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGeneratePasscode.Click
        chbEventPasscode.Text = GeneratePasscode()
    End Sub

    Private Sub btnClearEventManagement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearEventManagement.Click
        Try

            txtEventID.Clear()
            txtEventTitle.Clear()
            txtEventDescription.Clear()
            DTPEventDate.Text = OracleDate
            DTPEventEndDate.Text = OracleDate
            DTPEventEndDate.Checked = False
            txtEventTime.Clear()
            chbEventPasscode.Text = ""
            chbEventPasscode.Checked = False
            cboEventStatus.Text = ""
            cboEventContact.Text = ""
            mtbEventPhoneNumber.Text = ""
            cboEventWebContact.Text = ""
            mtbEventWebPhoneNumber.Text = ""
            txtEventVenue.Clear()
            txtEventAddress.Clear()
            txtEventCity.Clear()
            mtbEventState.Clear()
            mtbEventZipCode.Clear()
            mtbEventCapacity.Clear()
            txtEventNotes.Clear()
            txtEventEndTime.Clear()
            txtWebsiteURL.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Registration Management"
    Private Sub dgvRegistrationManagement_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvRegistrationManagement.MouseUp
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
                    temp = dgvRegistrationManagement(2, hti.RowIndex).Value
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



#End Region



    Private Sub cboEventWebContact_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEventWebContact.Leave
        Try

            If cboEventWebContact.Items.Contains(cboEventWebContact.Text) = False Then
                cboEventWebContact.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnModifyRegistration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyRegistration.Click
        Try

            Dim EndDate As String = ""
            If DTPEventEndDate.Checked = True Then
                EndDate = DTPEventDate.Text
            End If
            If Update_RES_Registration(txtRegID.Text, txtRegConfirmationNum.Text, _
                                       cboRegStatus.SelectedValue, DTPRegDateRegistered.Text) = True Then

                MsgBox("Data Saved/Updated", MsgBoxStyle.Information, Me.Text)
                LoadRegistrationManagement()
            Else
                MsgBox("Data NOT Saved/Updated", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMapEventLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMapEventLocation.Click
        Try


            Dim StreetAddress As String = "4244 International Parkway"
            Dim City As String = "Atlanta"
            Dim State As String = "GA"
            Dim ZipCode As String = "30354"
            Dim URL As String = ""

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

            URL = "http://maps.google.com/maps?q=" & StreetAddress & "+" & _
                      City & "+" & State & "+" & ZipCode & "&z=14"

            System.Diagnostics.Process.Start(URL)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub


    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            ' Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvOverviewRegistrants.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvOverviewRegistrants.ColumnCount - 1
                        .Cells(1, i + 1) = dgvOverviewRegistrants.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvOverviewRegistrants.ColumnCount - 1
                        For j = 0 To dgvOverviewRegistrants.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvOverviewRegistrants.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.", MsgBoxStyle.Exclamation, Me.Text)
            Else
                ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub

    Private Sub btnEmailAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailAll.Click
        Try
            Dim j As Integer
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim EmailAddress As String = ""

            For j = 0 To dgvOverviewRegistrants.RowCount - 1
                EmailAddress = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
                txtEmails.Text = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
            Next
            MsgBox("Emails will saved to the clipboard." & vbCrLf & "Paste them into the bbc box by hitting ctrl -V", _
                   MsgBoxStyle.Information, Me.Text)

            Subject = txtEventTitle.Text & " - " & DTPEventDate.Text
            Body = txtEventTitle.Text & "%0D%0A" & txtEventDescription.Text & "%0D%0A" & _
            DTPEventDate.Value & " - " & txtEventTime.Text & "%0D%0A" & _
            vbCrLf & txtEventVenue.Text & "%0D%0A" & _
            txtEventAddress.Text & "%0D%0A" & txtEventCity.Text & ", " & mtbEventState.Text & " " & mtbEventZipCode.Text

            System.Diagnostics.Process.Start("mailto: ?subject=" & Subject & "&body=" & _
                                             Body)

            Clipboard.SetDataObject(EmailAddress, True)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEmailRegistrants_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailRegistrants.Click
        'Confirmed
        'Cancelled
        'Waiting List
        Try
            Dim j As Integer
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim EmailAddress As String = ""

            For j = 0 To dgvOverviewRegistrants.RowCount - 1
                If dgvOverviewRegistrants(4, j).Value.ToString = "Confirmed" Then
                    EmailAddress = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
                    txtEmails.Text = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
                End If
            Next
            MsgBox("Emails will saved to the clipboard." & vbCrLf & "Paste them into the bbc box by hitting ctrl -V", _
                           MsgBoxStyle.Information, Me.Text)

            Subject = txtEventTitle.Text & " - " & DTPEventDate.Text
            Body = txtEventTitle.Text & "%0D%0A" & txtEventDescription.Text & "%0D%0A" & _
            DTPEventDate.Value & " - " & txtEventTime.Text & "%0D%0A" & _
            vbCrLf & txtEventVenue.Text & "%0D%0A" & _
            txtEventAddress.Text & "%0D%0A" & txtEventCity.Text & ", " & mtbEventState.Text & " " & mtbEventZipCode.Text

            System.Diagnostics.Process.Start("mailto: ?subject=" & Subject & "&body=" & _
                                              Body)

            Clipboard.SetDataObject(EmailAddress, True)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEmailWaitList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailWaitList.Click
        Try

            Dim j As Integer
            Dim Subject As String = ""
            Dim Body As String = ""
            Dim EmailAddress As String = ""

            For j = 0 To dgvOverviewRegistrants.RowCount - 1
                If dgvOverviewRegistrants(4, j).Value.ToString = "Waiting List" Then
                    EmailAddress = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
                    txtEmails.Text = txtEmails.Text & dgvOverviewRegistrants.Item(7, j).Value.ToString & ", "
                End If
            Next

            MsgBox("Emails will saved to the clipboard." & vbCrLf & "Paste them into the bbc box by hitting ctrl -V", _
               MsgBoxStyle.Information, Me.Text)

            Subject = txtEventTitle.Text & " - " & DTPEventDate.Text
            Body = txtEventTitle.Text & "%0D%0A" & txtEventDescription.Text & "%0D%0A" & _
            DTPEventDate.Value & " - " & txtEventTime.Text & "%0D%0A" & _
            vbCrLf & txtEventVenue.Text & "%0D%0A" & _
            txtEventAddress.Text & "%0D%0A" & txtEventCity.Text & ", " & mtbEventState.Text & " " & mtbEventZipCode.Text

            System.Diagnostics.Process.Start("mailto: ?subject=" & Subject & "&body=" & _
                                           Body)

            Clipboard.SetDataObject(EmailAddress, True)


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Insert/Update event/registration"

    Function Insert_RES_Event(ByVal EventStatusCode As String, ByVal Title As String, _
                            ByVal Description As String, ByVal StartDateTime As String, _
                            ByVal EndDateTime As String, ByVal Venue As String, _
                            ByVal Address As String, ByVal City As String, _
                            ByVal State As String, ByVal ZipCode As String, _
                            ByVal Capacity As String, ByVal Notes As String, _
                            ByVal APBContact As String, _
                            ByVal WebContact As String, ByVal WebPhoneNumber As String, _
                            ByVal LogInRequired As String, _
                            ByVal PassCodeRequired As String, ByVal PassCode As String, _
                            ByVal Active As String, ByVal EventTime As String, _
                            ByVal EventEndTime As String, ByVal WebURL As String) As String
        Try
            Dim EventID As String = "0"

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

            SQL = "Insert into " & DBNameSpace & ".RES_Event " & _
                     "(numRes_EventID, numEventStatusCode, " & _
                     "strUserGCode, strTitle, " & _
                     "strDescription, datStartDate, " & _
                     "datEndDate, strVenue, " & _
                     "numCapacity, strNotes, " & _
                     "strMultipleregistrations, Active, " & _
                     "createDateTime, UpdateUser, " & _
                     "UpdateDateTime, strLogInRequired, " & _
                     "strPassCode, strAddress, " & _
                     "strCity, strState, " & _
                     "numZipCode, numAPBContact, " & _
                     "numWebPhoneNumber, strEventStartTime, " & _
                     "strEventEndTime, strWebURL) " & _
                     "values " & _
                     "((select " & _
                     "case when max(numres_eventID) is null then 1 " & _
                     "else max(numRes_EventID) + 1 End  " & _
                     "from " & DBNameSpace & ".Res_event), " & _
                     "'" & Replace(EventStatusCode, "'", "''") & "',  '" & Replace(WebContact, "'", "''") & "', " & _
                     "'" & Replace(Title, "'", "''") & "', " & _
                     "'" & Replace(Description, "'", "''") & "', '" & Replace(StartDateTime, "'", "''") & "', " & _
                     "'" & Replace(EndDateTime, "'", "''") & "', '" & Replace(Venue, "'", "''") & "', " & _
                     "'" & Replace(Capacity, "'", "''") & "', '" & Replace(Notes, "'", "''") & "', " & _
                     "'', " & _
                     "'" & Active & "', " & _
                     "sysdate, '" & UserGCode & "', " & _
                     "sysdate, '" & LogInRequired & "', " & _
                     "'" & Replace(PassCode, "'", "''") & "', '" & Replace(Address, "'", "''") & "', " & _
                     "'" & Replace(City, "'", "''") & "', '" & Replace(State, "'", "''") & "',  " & _
                     "'" & ZipCode & "', '" & APBContact & "', " & _
                     "'" & WebPhoneNumber & "', '" & Replace(EventTime, "'", "''") & "', " & _
                     "'" & Replace(EventEndTime, "'", "''") & "', '" & Replace(WebURL, "'", "''") & "') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select " & _
            "max(numRes_eventID) as EventID " & _
            "from " & DBNameSpace & ".RES_Event "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("EventID")) Then
                    EventID = "0"
                Else
                    EventID = dr.Item("EventID")
                End If
            End While
            dr.Close()

            Return EventID

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return ""
    End Function

    Function Update_RES_Event(ByVal Res_EventID As String, _
                           ByVal EventStatusCode As String, ByVal Title As String, _
                           ByVal Description As String, ByVal StartDateTime As String, _
                           ByVal EndDateTime As String, ByVal Venue As String, _
                           ByVal Address As String, ByVal City As String, _
                           ByVal State As String, ByVal ZipCode As String, _
                           ByVal Capacity As String, ByVal Notes As String, _
                           ByVal APBContact As String, _
                           ByVal WebContact As String, ByVal WebPhoneNumber As String, _
                           ByVal LogInRequired As String, _
                           ByVal PassCodeRequired As String, ByVal PassCode As String, _
                           ByVal Active As String, ByVal EventTime As String, _
                           ByVal EventEndTime As String, ByVal WebURL As String) As Boolean
        Try
            SQL = ""
            If IsDBNull(EventStatusCode) Then
            Else
                If EventStatusCode <> "" Then
                    SQL = "numEventStatusCode = '" & Replace(EventStatusCode, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Title) Then
            Else
                If Title <> "" Then
                    SQL = SQL & "strTitle = '" & Replace(Title, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Description) Then
            Else
                If Description <> "" Then
                    SQL = SQL & "strDescription = '" & Replace(Description, "'", "''") & "', "
                End If
            End If
            If IsDBNull(StartDateTime) Then
            Else
                If StartDateTime <> "" Then
                    SQL = SQL & "datStartDate = '" & Replace(StartDateTime, "'", "''") & "', "
                End If
            End If
            If IsDBNull(EndDateTime) Then
            Else
                If EndDateTime <> "" Then
                    SQL = SQL & "datEndDate = '" & Replace(EndDateTime, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Venue) Then
            Else
                If Venue <> "" Then
                    SQL = SQL & "strVenue = '" & Replace(Venue, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Address) Then
            Else
                If Address <> "" Then
                    SQL = SQL & "strAddress = '" & Replace(Address, "'", "''") & "', "
                End If
            End If
            If IsDBNull(City) Then
            Else
                If City <> "" Then
                    SQL = SQL & "strCity = '" & Replace(City, "'", "''") & "', "
                End If
            End If
            If IsDBNull(State) Then
            Else
                If State <> "" Then
                    SQL = SQL & "strState = '" & Replace(State, "'", "''") & "', "
                End If
            End If
            If IsDBNull(ZipCode) Then
            Else
                If ZipCode <> "" Then
                    SQL = SQL & "numZipCode = '" & Replace(ZipCode, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Capacity) Then
            Else
                If Capacity <> "" Then
                    SQL = SQL & "numCapacity = '" & Replace(Capacity, "'", "''") & "', "
                End If
            End If
            If IsDBNull(Notes) Then
            Else
                If Notes <> "" Then
                    SQL = SQL & "strNotes = '" & Replace(Notes, "'", "''") & "', "
                End If
            End If
            If IsDBNull(APBContact) Then
            Else
                If APBContact <> "" Then
                    SQL = SQL & "numAPBContact = '" & Replace(APBContact, "'", "''") & "', "
                End If
            End If
            If IsDBNull(WebContact) Then
            Else
                If WebContact <> "" Then
                    SQL = SQL & "strUserGCode = '" & Replace(WebContact, "'", "''") & "', "
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
                    SQL = SQL & "strLogInRequired = '" & Replace(LogInRequired, "'", "''") & "', "
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
                    SQL = SQL & "strPassCode = '" & Replace(PassCode, "'", "''") & "', "
                End If
            End If
            If IsDBNull(EventTime) Then
            Else
                If EventTime <> "" Then
                    SQL = SQL & "strEventStartTime = '" & Replace(EventTime, "'", "''") & "', "
                End If
            End If
            If IsDBNull(EventEndTime) Then
            Else
                If EventEndTime <> "" Then
                    SQL = SQL & "strEventEndTime = '" & Replace(EventEndTime, "'", "''") & "', "
                End If
            End If
            If IsDBNull(WebURL) Then
            Else
                If WebURL <> "" Then
                    SQL = SQL & "strWebURL = '" & Replace(WebURL, "'", "''") & "', "
                End If
            End If

            If IsDBNull(Active) Then
            Else
                SQL = SQL & "active = '" & Active & "', "
            End If
            If SQL <> "" Then
                SQL = "Update " & DBNameSpace & ".Res_Event set " & _
                SQL & "updateUser = '" & UserGCode & "', " & _
                "updateDateTime = '" & OracleDate & "' " & _
                "where numRes_EventID = '" & Res_EventID & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                Return True
            End If

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function Update_RES_Registration(ByVal RegistrationID As String, ByVal Confirmation As String, _
                                     ByVal RegStatusCode As String, ByVal RegDate As String) As Boolean
        Try

            SQL = "Update " & DBNameSpace & ".Res_Registration set " & _
            "numREgistrationStatusCode = '" & RegStatusCode & "', " & _
            "datRegistrationDateTime = '" & RegDate & "' " & _
            "where numRes_RegistrationID = '" & RegistrationID & "' " & _
            "and strConfirmationNumber = '" & Confirmation & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            Return True
        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

#End Region

#Region "DAL"

    Public Function PasscodeExists(ByVal id As String) As Boolean
        Dim query As String = "SELECT 'True' FROM AIRBRANCH.RES_EVENT WHERE ROWNUM=1 AND STRPASSCODE = :pId"
        Dim parameter As New OracleParameter("pId", id)

        Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
        Return Convert.ToBoolean(result)
    End Function

#End Region
End Class