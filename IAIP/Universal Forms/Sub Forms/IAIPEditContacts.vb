Imports Oracle.DataAccess.Client


Public Class IAIPEditContacts

#Region "Properties"

    Private _airsNumber As String
    Public Property AirsNumber() As String
        Get
            Return _airsNumber
        End Get
        Set(ByVal value As String)
            _airsNumber = value
        End Set
    End Property

    Private _facilityName As String
    Public Property FacilityName() As String
        Get
            Return _facilityName
        End Get
        Set(ByVal value As String)
            _facilityName = value
        End Set
    End Property

    Private _key As String
    Public Property Key() As String
        Get
            Return _key
        End Get
        Set(ByVal value As String)
            _key = value
        End Set
    End Property

#End Region

    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsContacts As DataSet
    Dim daContacts As OracleDataAdapter

#Region "Page Load"

    Private Sub APBAddContacts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        SetHeaderLabels()
        LoadContactsDataset()
    End Sub

    Private Sub SetHeaderLabels()
        lblAirsNumber.Text = CInt(AirsNumber).ToString("000-00000")
        lblFacilityName.Text = FacilityName
    End Sub

    Private Sub LoadContactsDataset()
        Try
            If AirsNumber <> "" Then

                SQL = "Select " & _
                "case " & _
                "when strKey = '10' then 'Current Monitoring Contact'" & _
                "when strKey = '20' then 'Current Compliance Contact' " & _
                "when strKey = '30' then 'Current Permitting Contact' " & _
                "when strKey = '40' then 'Current Fee Contact' " & _
                "when strkey = '41' then 'Current EIS Contact' " & _
                "when strKey = '42' then 'Current ES Contact' " & _
                "when strKey = '50' then 'Current Ambient Contact' " & _
                "when strKey = '60' then 'Current Planning Contact' " & _
                "when strKey = '70' then 'Current District Contact' " & _
                "Else 'Past Contact' " & _
                "end ContactType, " & _
                 "strContactKey, " & _
                 "strContactFirstName, strContactLastname, " & _
                 "strContactPrefix, strContactSuffix, strContactTitle, " & _
                 "strContactCompanyName, strContactPhoneNumber1, " & _
                 "Case  " & _
                 "    when strContactPhoneNumber2 is NULL then '' " & _
                 "    Else strContactPhoneNumber2 " & _
                 "END as ContactPhoneNumber2,  " & _
                 "case " & _
                 "    when strContactFaxNumber is Null then '' " & _
                 "    else strContactFaxNumber " & _
                 "END as strContactFaxNumber, " & _
                 "Case " & _
                 "    when strContactEmail is Null then '' " & _
                 "    ELSE strContactEmail " & _
                 "END as ContactEmail, " & _
                 "strContactAddress1, strContactAddress2, " & _
                 "strContactCity, strContactState, strContactZipCode, " & _
                 "Case " & _
                 "    when strContactDescription is Null then '' " & _
                 "    ELSE strContactDescription " & _
                 "END as ContactDescription " & _
                 "from " & DBNameSpace & ".APBContactInformation " & _
                 "where strAIRSnumber = '0413" & AirsNumber & "' " & _
                 "order by substr(strKey, 2), strKey "

                dsContacts = New DataSet
                daContacts = New OracleDataAdapter(SQL, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daContacts.Fill(dsContacts, "Contacts")
                ContactsDataGrid.DataSource = dsContacts
                ContactsDataGrid.DataMember = "Contacts"

                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If

                ContactsDataGrid.RowHeadersVisible = False
                ContactsDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                ContactsDataGrid.AllowUserToResizeColumns = True
                ContactsDataGrid.AllowUserToAddRows = False
                ContactsDataGrid.AllowUserToDeleteRows = False
                ContactsDataGrid.AllowUserToOrderColumns = True
                ContactsDataGrid.AllowUserToResizeRows = True
                ContactsDataGrid.Columns("ContactType").HeaderText = "Contact Type"
                ContactsDataGrid.Columns("strContactKey").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ContactsDataGrid.Columns("strContactKey").DisplayIndex = 0
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
                ContactsDataGrid.Columns("strContactSuffix").HeaderText = "Pedigree"
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
            If AirsNumber <> "" And txtNewKey.Text <> "" Then
                SQL = "Select * from AIRBranch.APBContactInformation " & _
                "where strAIRSNumber = '0413" & AirsNumber & "' " & _
                "and strKey = '" & txtNewKey.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
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

                Select Case txtNewKey.Text
                    Case "10"
                        rdbNewMonitoringContact.Checked = True
                    Case "20"
                        rdbNewComplianceContact.Checked = True
                    Case "30"
                        rdbNewPermittingContact.Checked = True
                    Case "40"
                        rdbNewFeeContact.Checked = True
                    Case "41"
                        rdbNewEISContact.Checked = True
                    Case "42"
                        rdbNewESContact.Checked = True
                    Case "50"
                        rdbNewAmbientContact.Checked = True
                    Case "60"
                        rdbNewPlanningContact.Checked = True
                    Case "70"
                        rdbNewDistrictContact.Checked = True
                    Case Else
                        rdbNewMonitoringContact.Checked = False
                        rdbNewComplianceContact.Checked = False
                        rdbNewPermittingContact.Checked = False
                        rdbNewFeeContact.Checked = False
                        rdbNewEISContact.Checked = False
                        rdbNewESContact.Checked = False
                        rdbNewAmbientContact.Checked = False
                        rdbNewPlanningContact.Checked = False
                        rdbNewDistrictContact.Checked = False
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrContacts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ContactsDataGrid.MouseUp
        Dim hti As DataGridView.HitTestInfo = ContactsDataGrid.HitTest(e.X, e.Y)

        Try

            If ContactsDataGrid.RowCount > 0 And hti.RowIndex <> -1 Then
                AirsNumber = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 5, 8)
                txtNewKey.Text = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 13)
                NewContactDataLoad()
                'txtContactKey.Text = dgvContacts(1, hti.RowIndex).Value
                'ContactKeyChange(True)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#End Region

#Region "Buttons"

    Private Sub btnNewClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewClear.Click
        Try
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
            txtNewKey.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNewUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewUpdate.Click
        Try
            Dim newKey As String = ""

            If AirsNumber <> "" Then
                If rdbNewAmbientContact.Checked = False And rdbNewComplianceContact.Checked = False _
                And rdbNewDistrictContact.Checked = False And rdbNewEISContact.Checked = False _
                And rdbNewESContact.Checked = False And rdbNewFeeContact.Checked = False _
                And rdbNewMonitoringContact.Checked = False And rdbNewPermittingContact.Checked = False _
                And rdbNewPlanningContact.Checked = False Then
                    MsgBox("Please select a Contact Type first" & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If
                If txtNewKey.Text <> "" Then
                    SQL = "Update airbranch.APBContactInformation set " & _
                    "STRCONTACTFIRSTNAME = '" & Replace(txtNewFirstName.Text, "'", "''") & "', " & _
                    "STRCONTACTLASTNAME = '" & Replace(txtNewLastName.Text, "'", "''") & "', " & _
                    "STRCONTACTPREFIX = '" & Replace(txtNewPrefix.Text, "'", "''") & "', " & _
                    "STRCONTACTSUFFIX = '" & Replace(txtNewSuffix.Text, "'", "''") & "', " & _
                    "STRCONTACTTITLE = '" & Replace(txtNewTitle.Text, "'", "''") & "', " & _
                    "STRCONTACTCOMPANYNAME = '" & Replace(txtNewCompany.Text, "'", "''") & "', " & _
                    "STRCONTACTPHONENUMBER1 = '" & mtbNewPhoneNumber.Text & "', " & _
                    "STRCONTACTPHONENUMBER2 = '" & mtbNewPhoneNumber2.Text & "', " & _
                    "STRCONTACTFAXNUMBER = '" & mtbNewFaxNumber.Text & "'," & _
                    "STRCONTACTEMAIL = '" & Replace(txtNewEmail.Text, "'", "''") & "', " & _
                    "STRCONTACTADDRESS1 = '" & Replace(txtNewAddress.Text, "'", "''") & "', " & _
                    "STRCONTACTCITY = '" & Replace(txtNewCity.Text, "'", "''") & "', " & _
                    "STRCONTACTSTATE = '" & Replace(txtNewState.Text, "'", "''") & "', " & _
                    "STRCONTACTZIPCODE = '" & mtbNewZipCode.Text & "', " & _
                    "STRMODIFINGPERSON = '" & UserGCode & "', " & _
                    "DATMODIFINGDATE = sysdate,  " & _
                    "STRCONTACTDESCRIPTION = '" & Replace(txtNewDescrption.Text, "'", "''") & "' " & _
                    "where strAIRSnumber = '0413" & AirsNumber & "' " & _
                    "and strKey = '" & txtNewKey.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                        SQL = "Update airbranch.APBContactInformation set " & _
                       "STRCONTACTFIRSTNAME = '" & Replace(txtNewFirstName.Text, "'", "''") & "', " & _
                       "STRCONTACTLASTNAME = '" & Replace(txtNewLastName.Text, "'", "''") & "', " & _
                       "STRCONTACTPREFIX = '" & Replace(txtNewPrefix.Text, "'", "''") & "', " & _
                       "STRCONTACTSUFFIX = '" & Replace(txtNewSuffix.Text, "'", "''") & "', " & _
                       "STRCONTACTTITLE = '" & Replace(txtNewTitle.Text, "'", "''") & "', " & _
                       "STRCONTACTCOMPANYNAME = '" & Replace(txtNewCompany.Text, "'", "''") & "', " & _
                       "STRCONTACTPHONENUMBER1 = '" & mtbNewPhoneNumber.Text & "', " & _
                       "STRCONTACTPHONENUMBER2 = '" & mtbNewPhoneNumber2.Text & "', " & _
                       "STRCONTACTFAXNUMBER = '" & mtbNewFaxNumber.Text & "'," & _
                       "STRCONTACTEMAIL = '" & Replace(txtNewEmail.Text, "'", "''") & "', " & _
                       "STRCONTACTADDRESS1 = '" & Replace(txtNewAddress.Text, "'", "''") & "', " & _
                       "STRCONTACTCITY = '" & Replace(txtNewCity.Text, "'", "''") & "', " & _
                       "STRCONTACTSTATE = '" & Replace(txtNewState.Text, "'", "''") & "', " & _
                       "STRCONTACTZIPCODE = '" & mtbNewZipCode.Text & "', " & _
                       "STRMODIFINGPERSON = '" & UserGCode & "', " & _
                       "DATMODIFINGDATE = sysdate,  " & _
                       "STRCONTACTDESCRIPTION = '" & Replace(txtNewDescrption.Text, "'", "''") & "' " & _
                       "where strAIRSnumber = '0413" & AirsNumber & "' " & _
                       "and strKey = '" & newKey & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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

    Private Sub btnNewSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSave.Click
        Try
            Dim newKey As String = ""

            If AirsNumber <> "" Then
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
                            SQL = "delete airbranch.APBContactInformation " & _
                            "where strAIRSnumber = '0413" & AirsNumber & "' " & _
                            "and strKey = '" & Mid(newKey, 1, 1) & "9' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                            SQL = "Update AIRBranch.APBContactInformation set " & _
                            "strKey = substr(strKey, 1,1) || (substr(strKey, 2,1) + 1), " & _
                            "strContactKey = substr(strContactKey, 1, 13) || (substr(strContactKey, 14, 1) + 1) " & _
                            "where strAIRSNumber = '0413" & AirsNumber & "' " & _
                            "and strKey like '" & Mid(newKey, 1, 1) & "%' "
                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                            SQL = "Insert into airbranch.APBContactInformation " & _
                            "(STRCONTACTKEY, STRAIRSNUMBER,  " & _
                            "STRKEY, STRCONTACTFIRSTNAME,  " & _
                            "STRCONTACTLASTNAME, STRCONTACTPREFIX,  " & _
                            "STRCONTACTSUFFIX, STRCONTACTTITLE,  " & _
                            "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1,  " & _
                            "STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER,  " & _
                            "STRCONTACTEMAIL, STRCONTACTADDRESS1,  " & _
                            "STRCONTACTADDRESS2, STRCONTACTCITY,  " & _
                            "STRCONTACTSTATE, STRCONTACTZIPCODE,  " & _
                            "STRMODIFINGPERSON, DATMODIFINGDATE,  " & _
                            "STRCONTACTDESCRIPTION)  " & _
                            "(Select  " & _
                            "'0413" & AirsNumber & newKey & "', '0413" & AirsNumber & "', " & _
                            "'" & newKey & "', '" & Replace(txtNewFirstName.Text, "'", "''") & "', " & _
                            "'" & Replace(txtNewLastName.Text, "'", "''") & "',  '" & Replace(txtNewPrefix.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewSuffix.Text, "'", "''") & "', '" & Replace(txtNewTitle.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewCompany.Text, "'", "''") & "', '" & mtbNewPhoneNumber.Text & "', " & _
                            " '" & mtbNewPhoneNumber2.Text & "',  '" & mtbNewFaxNumber.Text & "', " & _
                            " '" & Replace(txtNewEmail.Text, "'", "''") & "', '" & Replace(txtNewAddress.Text, "'", "''") & "', " & _
                            " '', '" & Replace(txtNewCity.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewState.Text, "'", "''") & "',  '" & mtbNewZipCode.Text & "', " & _
                            " '" & UserGCode & "',  sysdate, " & _
                            " '" & Replace(txtNewDescrption.Text, "'", "''") & "' " & _
                            "from dual  " & _
                            "where not exists (select * from AIRBranch.APBContactInformation  " & _
                            "where strKey = '" & newKey & "' " & _
                            "and strAIRSNumber = '0413" & AirsNumber & "')) "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            cmd.ExecuteReader()

                        Case Else
                            SQL = "Insert into airbranch.APBContactInformation " & _
                            "(STRCONTACTKEY, STRAIRSNUMBER,  " & _
                            "STRKEY, STRCONTACTFIRSTNAME,  " & _
                            "STRCONTACTLASTNAME, STRCONTACTPREFIX,  " & _
                            "STRCONTACTSUFFIX, STRCONTACTTITLE,  " & _
                            "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1,  " & _
                            "STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER,  " & _
                            "STRCONTACTEMAIL, STRCONTACTADDRESS1,  " & _
                            "STRCONTACTADDRESS2, STRCONTACTCITY,  " & _
                            "STRCONTACTSTATE, STRCONTACTZIPCODE,  " & _
                            "STRMODIFINGPERSON, DATMODIFINGDATE,  " & _
                            "STRCONTACTDESCRIPTION)  " & _
                            "(Select  " & _
                            "'0413" & AirsNumber & newKey & "', '0413" & AirsNumber & "', " & _
                            "'" & newKey & "', '" & Replace(txtNewFirstName.Text, "'", "''") & "', " & _
                            "'" & Replace(txtNewLastName.Text, "'", "''") & "',  '" & Replace(txtNewPrefix.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewSuffix.Text, "'", "''") & "', '" & Replace(txtNewTitle.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewCompany.Text, "'", "''") & "', '" & mtbNewPhoneNumber.Text & "', " & _
                            " '" & mtbNewPhoneNumber2.Text & "',  '" & mtbNewFaxNumber.Text & "', " & _
                            " '" & Replace(txtNewEmail.Text, "'", "''") & "', '" & Replace(txtNewAddress.Text, "'", "''") & "', " & _
                            " '', '" & Replace(txtNewCity.Text, "'", "''") & "', " & _
                            " '" & Replace(txtNewState.Text, "'", "''") & "',  '" & mtbNewZipCode.Text & "', " & _
                            " '" & UserGCode & "',  sysdate, " & _
                            " '" & Replace(txtNewDescrption.Text, "'", "''") & "' " & _
                            "from dual  " & _
                            "where not exists (select * from AIRBranch.APBContactInformation  " & _
                            "where strKey = '" & newKey & "' " & _
                            "and strAIRSNumber = '0413" & AirsNumber & "')) "

                            cmd = New OracleCommand(SQL, CurrentConnection)
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