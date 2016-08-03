Imports System.Data.SqlClient
Imports Iaip.Apb.Facilities
Imports Iaip.DAL

Public Class IAIPEditContacts

#Region "Properties"

    Private _airsNumber As Apb.ApbFacilityId
    Public Property AirsNumber() As Apb.ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As Apb.ApbFacilityId)
            _airsNumber = value
        End Set
    End Property

    Private _facilityName As String
    Public Property FacilityName() As String
        Get
            Return _facilityName
        End Get
        Set(value As String)
            _facilityName = value
        End Set
    End Property

    Private _key As ContactKey
    Friend Property Key() As ContactKey
        Get
            Return _key
        End Get
        Set(value As ContactKey)
            _key = value
        End Set
    End Property

#End Region

    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dsContacts As DataSet
    Dim daContacts As SqlDataAdapter

#Region "Page Load"

    Private Sub APBAddContacts_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load

        ParseParameters()
        LoadContactsDataset()
        If Key <> ContactKey.None AndAlso [Enum].IsDefined(GetType(ContactKey), Key) Then
            NewContactDataLoad()
        End If
    End Sub

    Private Sub ParseParameters()
        If Parameters IsNot Nothing Then
            If Parameters.ContainsKey(FormParameter.AirsNumber) Then
                Try
                    Me.AirsNumber = Parameters(FormParameter.AirsNumber)
                    lblAirsNumber.Text = Me.AirsNumber.FormattedString
                Catch ex As Exception
                    Me.AirsNumber = Nothing
                End Try
            End If
            If Parameters.ContainsKey(FormParameter.FacilityName) Then
                Me.FacilityName = Parameters(FormParameter.FacilityName)
                lblFacilityName.Text = FacilityName
            End If
            If Parameters.ContainsKey(FormParameter.Key) Then
                Me.Key = [Enum].Parse(GetType(ContactKey), Parameters(FormParameter.Key))
            End If
        End If
    End Sub

    Private Sub LoadContactsDataset()
        Try
            If AirsNumber.ToString IsNot Nothing Then

                SQL = "Select " &
                "case " &
                "when strKey = '10' then 'Current Monitoring Contact'" &
                "when strKey = '20' then 'Current Compliance Contact' " &
                "when strKey = '30' then 'Current Permitting Contact' " &
                "when strKey = '40' then 'Current Fee Contact' " &
                "when strkey = '41' then 'Current EIS Contact' " &
                "when strKey = '42' then 'Current ES Contact' " &
                "when strKey = '50' then 'Current Ambient Contact' " &
                "when strKey = '60' then 'Current Planning Contact' " &
                "when strKey = '70' then 'Current District Contact' " &
                "Else 'Past Contact' " &
                "end ContactType, " &
                 "strContactKey, " &
                 "strContactFirstName, strContactLastname, " &
                 "strContactPrefix, strContactSuffix, strContactTitle, " &
                 "strContactCompanyName, strContactPhoneNumber1, " &
                 "Case  " &
                 "    when strContactPhoneNumber2 is NULL then '' " &
                 "    Else strContactPhoneNumber2 " &
                 "END as ContactPhoneNumber2,  " &
                 "case " &
                 "    when strContactFaxNumber is Null then '' " &
                 "    else strContactFaxNumber " &
                 "END as strContactFaxNumber, " &
                 "Case " &
                 "    when strContactEmail is Null then '' " &
                 "    ELSE strContactEmail " &
                 "END as ContactEmail, " &
                 "strContactAddress1, strContactAddress2, " &
                 "strContactCity, strContactState, strContactZipCode, " &
                 "Case " &
                 "    when strContactDescription is Null then '' " &
                 "    ELSE strContactDescription " &
                 "END as ContactDescription " &
                 "from APBContactInformation " &
                 "where strAIRSnumber = '" & AirsNumber.DbFormattedString & "' " &
                 "order by substr(strKey, 2), strKey "

                dsContacts = New DataSet
                daContacts = New SqlDataAdapter(SQL, CurrentConnection)

                daContacts.Fill(dsContacts, "Contacts")
                ContactsDataGrid.DataSource = dsContacts
                ContactsDataGrid.DataMember = "Contacts"

                ContactsDataGrid.RowHeadersVisible = False
                ContactsDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                ContactsDataGrid.AllowUserToResizeColumns = True
                ContactsDataGrid.AllowUserToAddRows = False
                ContactsDataGrid.AllowUserToDeleteRows = False
                ContactsDataGrid.AllowUserToOrderColumns = True
                ContactsDataGrid.AllowUserToResizeRows = True
                ContactsDataGrid.Columns("ContactType").HeaderText = "Contact Type"
                ContactsDataGrid.Columns("strContactKey").HeaderText = "Key"
                ContactsDataGrid.Columns("strContactKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactKey").DisplayIndex = 17
                ContactsDataGrid.Columns("strContactKey").Visible = False
                ContactsDataGrid.Columns("strContactPrefix").HeaderText = "Social Title"
                ContactsDataGrid.Columns("strContactPrefix").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactPrefix").DisplayIndex = 2
                ContactsDataGrid.Columns("strContactFirstName").HeaderText = "First Name"
                ContactsDataGrid.Columns("strContactFirstName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactFirstName").DisplayIndex = 3
                ContactsDataGrid.Columns("strContactLastName").HeaderText = "Last Name"
                ContactsDataGrid.Columns("strContactLastName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactLastName").DisplayIndex = 4
                ContactsDataGrid.Columns("strContactSuffix").HeaderText = "Suffix"
                ContactsDataGrid.Columns("strContactSuffix").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactSuffix").DisplayIndex = 5
                ContactsDataGrid.Columns("strContactTitle").HeaderText = "Title"
                ContactsDataGrid.Columns("strContactTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactTitle").DisplayIndex = 6
                ContactsDataGrid.Columns("strContactCompanyName").HeaderText = "Company Name"
                ContactsDataGrid.Columns("strContactCompanyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactCompanyName").DisplayIndex = 7
                ContactsDataGrid.Columns("strContactPhoneNumber1").HeaderText = "Phone Number 1"
                ContactsDataGrid.Columns("strContactPhoneNumber1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactPhoneNumber1").DisplayIndex = 8
                ContactsDataGrid.Columns("ContactPhoneNumber2").HeaderText = "Phone Number 2"
                ContactsDataGrid.Columns("ContactPhoneNumber2").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("ContactPhoneNumber2").DisplayIndex = 9
                ContactsDataGrid.Columns("strContactFaxNumber").HeaderText = "Fax Number"
                ContactsDataGrid.Columns("strContactFaxNumber").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactFaxNumber").DisplayIndex = 10
                ContactsDataGrid.Columns("ContactEmail").HeaderText = "Email Address"
                ContactsDataGrid.Columns("ContactEmail").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("ContactEmail").DisplayIndex = 11
                ContactsDataGrid.Columns("strContactAddress1").HeaderText = "Address Line 1"
                ContactsDataGrid.Columns("strContactAddress1").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactAddress1").DisplayIndex = 12
                ContactsDataGrid.Columns("strContactAddress2").HeaderText = "Address Line 2"
                ContactsDataGrid.Columns("strContactAddress2").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactAddress2").DisplayIndex = 13
                ContactsDataGrid.Columns("strContactCity").HeaderText = "City"
                ContactsDataGrid.Columns("strContactCity").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactCity").DisplayIndex = 14
                ContactsDataGrid.Columns("strContactState").HeaderText = "State"
                ContactsDataGrid.Columns("strContactState").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactState").DisplayIndex = 15
                ContactsDataGrid.Columns("strContactZipCode").HeaderText = "Zip Code"
                ContactsDataGrid.Columns("strContactZipCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactZipCode").DisplayIndex = 16
                ContactsDataGrid.Columns("ContactDescription").HeaderText = "Description"
                ContactsDataGrid.Columns("ContactDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("ContactDescription").DisplayIndex = 1

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

#Region "Grid click"

    Sub NewContactDataLoad()
        Try
            If Me.AirsNumber.ToString IsNot Nothing And Key <> ContactKey.None Then
                Dim query As String = "Select * from APBContactInformation " &
                "where strAIRSNumber = @airsnumber " &
                "and strKey = @key "

                Dim parameters As SqlParameter() = New SqlParameter() {
                    New SqlParameter("@airsnumber", Me.AirsNumber.DbFormattedString),
                    New SqlParameter("@key", Key.ToString("D"))
                }

                Using connection As New SqlConnection(CurrentConnectionString)
                    Using command As New SqlCommand(query, connection)
                        command.CommandType = CommandType.Text
                        command.Parameters.AddRange(parameters)
                        command.Connection.Open()

                        Dim dr As SqlDataReader = command.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("strContactFirstName")) Then
                                txtNewFirstName.Clear()
                            Else
                                txtNewFirstName.Text = dr.Item("strContactFirstName")
                            End If
                            If IsDBNull(dr.Item("strContactLastName")) Then
                                txtNewLastName.Clear()
                            Else
                                txtNewLastName.Text = dr.Item("strContactLastName")
                            End If
                            If IsDBNull(dr.Item("strContactPrefix")) Then
                                txtNewPrefix.Clear()
                            Else
                                txtNewPrefix.Text = dr.Item("strContactPrefix")
                            End If
                            If IsDBNull(dr.Item("strContactSuffix")) Then
                                txtNewSuffix.Clear()
                            Else
                                txtNewSuffix.Text = dr.Item("strContactSuffix")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTTITLE")) Then
                                txtNewTitle.Clear()
                            Else
                                txtNewTitle.Text = dr.Item("STRCONTACTTITLE")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTCOMPANYNAME")) Then
                                txtNewCompany.Clear()
                            Else
                                txtNewCompany.Text = dr.Item("STRCONTACTCOMPANYNAME")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTPHONENUMBER1")) Then
                                mtbNewPhoneNumber.Clear()
                            Else
                                mtbNewPhoneNumber.Text = dr.Item("STRCONTACTPHONENUMBER1")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTPHONENUMBER2")) Then
                                mtbNewPhoneNumber2.Clear()
                            Else
                                mtbNewPhoneNumber2.Text = dr.Item("STRCONTACTPHONENUMBER2")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTFAXNUMBER")) Then
                                mtbNewFaxNumber.Clear()
                            Else
                                mtbNewFaxNumber.Text = dr.Item("STRCONTACTFAXNUMBER")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTEMAIL")) Then
                                txtNewEmail.Clear()
                            Else
                                txtNewEmail.Text = dr.Item("STRCONTACTEMAIL")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTADDRESS1")) Then
                                txtNewAddress.Clear()
                            Else
                                txtNewAddress.Text = dr.Item("STRCONTACTADDRESS1")
                            End If
                            'If IsDBNull(dr.Item("STRCONTACTADDRESS2")) Then
                            '    txtNewFirstName.Clear()
                            'Else
                            '    txtNewFirstName.Text = dr.Item("STRCONTACTADDRESS2")
                            'End If
                            If IsDBNull(dr.Item("STRCONTACTCITY")) Then
                                txtNewCity.Clear()
                            Else
                                txtNewCity.Text = dr.Item("STRCONTACTCITY")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTSTATE")) Then
                                txtNewState.Clear()
                            Else
                                txtNewState.Text = dr.Item("STRCONTACTSTATE")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTZIPCODE")) Then
                                mtbNewZipCode.Clear()
                            Else
                                mtbNewZipCode.Text = dr.Item("STRCONTACTZIPCODE")
                            End If
                            If IsDBNull(dr.Item("STRCONTACTDESCRIPTION")) Then
                                txtNewDescrption.Clear()
                            Else
                                txtNewDescrption.Text = dr.Item("STRCONTACTDESCRIPTION")
                            End If
                        End While
                        dr.Close()

                        command.Connection.Close()
                    End Using
                End Using

                rdbNewMonitoringContact.Checked = False
                rdbNewComplianceContact.Checked = False
                rdbNewPermittingContact.Checked = False
                rdbNewFeeContact.Checked = False
                rdbNewEISContact.Checked = False
                rdbNewESContact.Checked = False
                rdbNewAmbientContact.Checked = False
                rdbNewPlanningContact.Checked = False
                rdbNewDistrictContact.Checked = False

                Select Case Key
                    Case ContactKey.IndustrialSourceMonitoring
                        rdbNewMonitoringContact.Checked = True
                    Case ContactKey.StationarySourceCompliance
                        rdbNewComplianceContact.Checked = True
                    Case ContactKey.StationarySourcePermitting
                        rdbNewPermittingContact.Checked = True
                    Case ContactKey.Fees
                        rdbNewFeeContact.Checked = True
                    Case ContactKey.EmissionInventory
                        rdbNewEISContact.Checked = True
                    Case ContactKey.EmissionStatement
                        rdbNewESContact.Checked = True
                    Case ContactKey.AmbientMonitoring
                        rdbNewAmbientContact.Checked = True
                    Case ContactKey.PlanningAndSupport
                        rdbNewPlanningContact.Checked = True
                    Case ContactKey.DistrictOffices
                        rdbNewDistrictContact.Checked = True
                End Select

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrContacts_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ContactsDataGrid.MouseUp
        Dim hti As DataGridView.HitTestInfo = ContactsDataGrid.HitTest(e.X, e.Y)

        Try

            If ContactsDataGrid.RowCount > 0 And hti.RowIndex <> -1 Then
                AirsNumber = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 5, 8)
                Key = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 13)
                NewContactDataLoad()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region

#Region "Buttons"

    Private Sub btnNewClear_Click(sender As System.Object, e As System.EventArgs) Handles btnNewClear.Click
        txtNewAddress.Clear()
        txtNewCity.Clear()
        txtNewCompany.Clear()
        txtNewDescrption.Clear()
        txtNewEmail.Clear()
        txtNewFirstName.Clear()
        txtNewLastName.Clear()
        txtNewPrefix.Clear()
        txtNewState.Clear()
        txtNewSuffix.Clear()
        txtNewTitle.Clear()
        mtbNewFaxNumber.Clear()
        mtbNewPhoneNumber.Clear()
        mtbNewPhoneNumber2.Clear()
        mtbNewZipCode.Clear()
        Key = ContactKey.None
    End Sub

    Private Sub btnNewUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnNewUpdate.Click
        Try
            Dim newKey As String = ""

            If AirsNumber.ToString <> "" Then
                If rdbNewAmbientContact.Checked = False And rdbNewComplianceContact.Checked = False _
                And rdbNewDistrictContact.Checked = False And rdbNewEISContact.Checked = False _
                And rdbNewESContact.Checked = False And rdbNewFeeContact.Checked = False _
                And rdbNewMonitoringContact.Checked = False And rdbNewPermittingContact.Checked = False _
                And rdbNewPlanningContact.Checked = False Then
                    MsgBox("Please select a Contact Type first" & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                If Key <> ContactKey.None Then
                    SQL = "Update APBContactInformation set " &
                    "STRCONTACTFIRSTNAME = '" & Replace(txtNewFirstName.Text, "'", "''") & "', " &
                    "STRCONTACTLASTNAME = '" & Replace(txtNewLastName.Text, "'", "''") & "', " &
                    "STRCONTACTPREFIX = '" & Replace(txtNewPrefix.Text, "'", "''") & "', " &
                    "STRCONTACTSUFFIX = '" & Replace(txtNewSuffix.Text, "'", "''") & "', " &
                    "STRCONTACTTITLE = '" & Replace(txtNewTitle.Text, "'", "''") & "', " &
                    "STRCONTACTCOMPANYNAME = '" & Replace(txtNewCompany.Text, "'", "''") & "', " &
                    "STRCONTACTPHONENUMBER1 = '" & mtbNewPhoneNumber.Text & "', " &
                    "STRCONTACTPHONENUMBER2 = '" & mtbNewPhoneNumber2.Text & "', " &
                    "STRCONTACTFAXNUMBER = '" & mtbNewFaxNumber.Text & "'," &
                    "STRCONTACTEMAIL = '" & Replace(txtNewEmail.Text, "'", "''") & "', " &
                    "STRCONTACTADDRESS1 = '" & Replace(txtNewAddress.Text, "'", "''") & "', " &
                    "STRCONTACTCITY = '" & Replace(txtNewCity.Text, "'", "''") & "', " &
                    "STRCONTACTSTATE = '" & Replace(txtNewState.Text, "'", "''") & "', " &
                    "STRCONTACTZIPCODE = '" & mtbNewZipCode.Text & "', " &
                    "STRMODIFINGPERSON = '" & CurrentUser.UserID & "', " &
                    "DATMODIFINGDATE = sysdate,  " &
                    "STRCONTACTDESCRIPTION = '" & Replace(txtNewDescrption.Text, "'", "''") & "' " &
                    "where strAIRSnumber = '" & AirsNumber.DbFormattedString & "' " &
                    "and strKey = '" & Key.ToString("D") & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    cmd.ExecuteReader()
                Else
                    If rdbNewMonitoringContact.Checked = True Then
                        newKey = "10"
                    ElseIf rdbNewComplianceContact.Checked = True Then
                        newKey = "20"
                    ElseIf rdbNewPermittingContact.Checked = True Then
                        newKey = "30"
                    ElseIf rdbNewFeeContact.Checked = True Then
                        newKey = "40"
                    ElseIf rdbNewEISContact.Checked = True Then
                        newKey = "41"
                    ElseIf rdbNewESContact.Checked = True Then
                        newKey = "42"
                    ElseIf rdbNewAmbientContact.Checked = True Then
                        newKey = "50"
                    ElseIf rdbNewPlanningContact.Checked = True Then
                        newKey = "60"
                    ElseIf rdbNewDistrictContact.Checked = True Then
                        newKey = "70"
                    End If

                    If newKey <> "" Then
                        SQL = "Update APBContactInformation set " &
                       "STRCONTACTFIRSTNAME = '" & Replace(txtNewFirstName.Text, "'", "''") & "', " &
                       "STRCONTACTLASTNAME = '" & Replace(txtNewLastName.Text, "'", "''") & "', " &
                       "STRCONTACTPREFIX = '" & Replace(txtNewPrefix.Text, "'", "''") & "', " &
                       "STRCONTACTSUFFIX = '" & Replace(txtNewSuffix.Text, "'", "''") & "', " &
                       "STRCONTACTTITLE = '" & Replace(txtNewTitle.Text, "'", "''") & "', " &
                       "STRCONTACTCOMPANYNAME = '" & Replace(txtNewCompany.Text, "'", "''") & "', " &
                       "STRCONTACTPHONENUMBER1 = '" & mtbNewPhoneNumber.Text & "', " &
                       "STRCONTACTPHONENUMBER2 = '" & mtbNewPhoneNumber2.Text & "', " &
                       "STRCONTACTFAXNUMBER = '" & mtbNewFaxNumber.Text & "'," &
                       "STRCONTACTEMAIL = '" & Replace(txtNewEmail.Text, "'", "''") & "', " &
                       "STRCONTACTADDRESS1 = '" & Replace(txtNewAddress.Text, "'", "''") & "', " &
                       "STRCONTACTCITY = '" & Replace(txtNewCity.Text, "'", "''") & "', " &
                       "STRCONTACTSTATE = '" & Replace(txtNewState.Text, "'", "''") & "', " &
                       "STRCONTACTZIPCODE = '" & mtbNewZipCode.Text & "', " &
                       "STRMODIFINGPERSON = '" & CurrentUser.UserID & "', " &
                       "DATMODIFINGDATE = sysdate,  " &
                       "STRCONTACTDESCRIPTION = '" & Replace(txtNewDescrption.Text, "'", "''") & "' " &
                       "where strAIRSnumber = '" & AirsNumber.DbFormattedString & "' " &
                       "and strKey = '" & newKey & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        cmd.ExecuteReader()
                    End If

                End If
                LoadContactsDataset()
                MsgBox("Data Updated", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNewSave_Click(sender As System.Object, e As System.EventArgs) Handles btnNewSave.Click
        Try
            Dim newKey As String = ""

            If AirsNumber.ToString <> "" Then
                If rdbNewMonitoringContact.Checked = True Then
                    newKey = "10"
                ElseIf rdbNewComplianceContact.Checked = True Then
                    newKey = "20"
                ElseIf rdbNewPermittingContact.Checked = True Then
                    newKey = "30"
                ElseIf rdbNewFeeContact.Checked = True Then
                    newKey = "40"
                ElseIf rdbNewEISContact.Checked = True Then
                    newKey = "41"
                ElseIf rdbNewESContact.Checked = True Then
                    newKey = "42"
                ElseIf rdbNewAmbientContact.Checked = True Then
                    newKey = "50"
                ElseIf rdbNewPlanningContact.Checked = True Then
                    newKey = "60"
                ElseIf rdbNewDistrictContact.Checked = True Then
                    newKey = "70"
                End If

                If newKey = "" Then
                    MsgBox("Please select a Contact Type first" & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                Else
                    Select Case newKey
                        Case "10", "20", "30", "50", "60", "70"
                            SQL = "delete APBContactInformation " &
                            "where strAIRSnumber = '" & AirsNumber.DbFormattedString & "' " &
                            "and strKey = '" & Mid(newKey, 1, 1) & "9' "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                            SQL = "Update APBContactInformation set " &
                            "strKey = substr(strKey, 1,1) || (substr(strKey, 2,1) + 1), " &
                            "strContactKey = substr(strContactKey, 1, 13) || (substr(strContactKey, 14, 1) + 1) " &
                            "where strAIRSNumber = '" & AirsNumber.DbFormattedString & "' " &
                            "and strKey like '" & Mid(newKey, 1, 1) & "%' "
                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                            SQL = "Insert into APBContactInformation " &
                            "(STRCONTACTKEY, STRAIRSNUMBER,  " &
                            "STRKEY, STRCONTACTFIRSTNAME,  " &
                            "STRCONTACTLASTNAME, STRCONTACTPREFIX,  " &
                            "STRCONTACTSUFFIX, STRCONTACTTITLE,  " &
                            "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1,  " &
                            "STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER,  " &
                            "STRCONTACTEMAIL, STRCONTACTADDRESS1,  " &
                            "STRCONTACTADDRESS2, STRCONTACTCITY,  " &
                            "STRCONTACTSTATE, STRCONTACTZIPCODE,  " &
                            "STRMODIFINGPERSON, DATMODIFINGDATE,  " &
                            "STRCONTACTDESCRIPTION)  " &
                            "(Select  " &
                            "'" & AirsNumber.DbFormattedString & newKey & "', '" & AirsNumber.DbFormattedString & "', " &
                            "'" & newKey & "', '" & Replace(txtNewFirstName.Text, "'", "''") & "', " &
                            "'" & Replace(txtNewLastName.Text, "'", "''") & "',  '" & Replace(txtNewPrefix.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewSuffix.Text, "'", "''") & "', '" & Replace(txtNewTitle.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewCompany.Text, "'", "''") & "', '" & mtbNewPhoneNumber.Text & "', " &
                            " '" & mtbNewPhoneNumber2.Text & "',  '" & mtbNewFaxNumber.Text & "', " &
                            " '" & Replace(txtNewEmail.Text, "'", "''") & "', '" & Replace(txtNewAddress.Text, "'", "''") & "', " &
                            " '', '" & Replace(txtNewCity.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewState.Text, "'", "''") & "',  '" & mtbNewZipCode.Text & "', " &
                            " '" & CurrentUser.UserID & "',  sysdate, " &
                            " '" & Replace(txtNewDescrption.Text, "'", "''") & "' " &
                            "from dual  " &
                            "where not exists (select * from APBContactInformation  " &
                            "where strKey = '" & newKey & "' " &
                            "and strAIRSNumber = '" & AirsNumber.DbFormattedString & "')) "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                        Case Else
                            SQL = "Insert into APBContactInformation " &
                            "(STRCONTACTKEY, STRAIRSNUMBER,  " &
                            "STRKEY, STRCONTACTFIRSTNAME,  " &
                            "STRCONTACTLASTNAME, STRCONTACTPREFIX,  " &
                            "STRCONTACTSUFFIX, STRCONTACTTITLE,  " &
                            "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1,  " &
                            "STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER,  " &
                            "STRCONTACTEMAIL, STRCONTACTADDRESS1,  " &
                            "STRCONTACTADDRESS2, STRCONTACTCITY,  " &
                            "STRCONTACTSTATE, STRCONTACTZIPCODE,  " &
                            "STRMODIFINGPERSON, DATMODIFINGDATE,  " &
                            "STRCONTACTDESCRIPTION)  " &
                            "(Select  " &
                            "'" & AirsNumber.DbFormattedString & newKey & "', '" & AirsNumber.DbFormattedString & "', " &
                            "'" & newKey & "', '" & Replace(txtNewFirstName.Text, "'", "''") & "', " &
                            "'" & Replace(txtNewLastName.Text, "'", "''") & "',  '" & Replace(txtNewPrefix.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewSuffix.Text, "'", "''") & "', '" & Replace(txtNewTitle.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewCompany.Text, "'", "''") & "', '" & mtbNewPhoneNumber.Text & "', " &
                            " '" & mtbNewPhoneNumber2.Text & "',  '" & mtbNewFaxNumber.Text & "', " &
                            " '" & Replace(txtNewEmail.Text, "'", "''") & "', '" & Replace(txtNewAddress.Text, "'", "''") & "', " &
                            " '', '" & Replace(txtNewCity.Text, "'", "''") & "', " &
                            " '" & Replace(txtNewState.Text, "'", "''") & "',  '" & mtbNewZipCode.Text & "', " &
                            " '" & CurrentUser.UserID & "',  sysdate, " &
                            " '" & Replace(txtNewDescrption.Text, "'", "''") & "' " &
                            "from dual  " &
                            "where not exists (select * from APBContactInformation  " &
                            "where strKey = '" & newKey & "' " &
                            "and strAIRSNumber = '" & AirsNumber.DbFormattedString & "')) "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()
                    End Select

                    LoadContactsDataset()
                    MsgBox("Data Inserted", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class