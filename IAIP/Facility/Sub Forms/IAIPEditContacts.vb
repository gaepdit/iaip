Imports System.Data.SqlClient
Imports Iaip.DAL

Public Class IAIPEditContacts

#Region "Properties"

    Public Property AirsNumber As Apb.ApbFacilityId
    Public Property FacilityName As String
    Friend Property Key As ContactKey

#End Region

#Region "Page Load"

    Private Sub APBAddContacts_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                    AirsNumber = Parameters(FormParameter.AirsNumber)
                    lblAirsNumber.Text = AirsNumber.FormattedString
                Catch ex As Exception
                    AirsNumber = Nothing
                End Try
            End If
            If Parameters.ContainsKey(FormParameter.FacilityName) Then
                FacilityName = Parameters(FormParameter.FacilityName)
                lblFacilityName.Text = FacilityName
            End If
            If Parameters.ContainsKey(FormParameter.Key) Then
                Key = [Enum].Parse(GetType(ContactKey), Parameters(FormParameter.Key))
            End If
        End If
    End Sub

    Private Sub LoadContactsDataset()
        Try
            If AirsNumber.ToString IsNot Nothing Then

                Dim query As String = "Select " &
                "case " &
                "when strKey = '10' then 'Current Monitoring Contact'" &
                "when strKey = '20' then 'Current Compliance Contact' " &
                "when strKey = '30' then 'Current Permitting Contact' " &
                "when strKey = '40' then 'Current Fee Contact' " &
                "when strkey = '41' then 'Current EIS Contact' " &
                "when strKey = '42' then 'Current ES Contact' " &
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
                 "where strAIRSnumber = @airs " &
                 "and convert(int, STRKEY) < 50 " &
                 "order by substring(strKey, 2, 1), strKey "

                Dim p As New SqlParameter("@airs", AirsNumber.DbFormattedString)

                ContactsDataGrid.DataSource = DB.GetDataTable(query, p)

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
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Grid click"

    Private Sub NewContactDataLoad()
        Try
            If AirsNumber.ToString IsNot Nothing AndAlso Key <> ContactKey.None Then
                Dim query As String = "Select * from APBContactInformation " &
                "where strAIRSNumber = @airsnumber " &
                "and strKey = @key "

                Dim parameterArray As SqlParameter() = {
                    New SqlParameter("@airsnumber", AirsNumber.DbFormattedString),
                    New SqlParameter("@key", Key.ToString("D"))
                }

                Dim dr As DataRow = DB.GetDataRow(query, parameterArray)
                If dr IsNot Nothing Then
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
                        txtNewPhoneNumber.Clear()
                    Else
                        txtNewPhoneNumber.Text = dr.Item("STRCONTACTPHONENUMBER1")
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
                End If

                rdbNewMonitoringContact.Checked = False
                rdbNewComplianceContact.Checked = False
                rdbNewPermittingContact.Checked = False
                rdbNewFeeContact.Checked = False
                rdbNewEISContact.Checked = False
                rdbNewESContact.Checked = False

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
                End Select

            End If
        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgrContacts_MouseUp(sender As Object, e As MouseEventArgs) Handles ContactsDataGrid.MouseUp
        Dim hti As DataGridView.HitTestInfo = ContactsDataGrid.HitTest(e.X, e.Y)

        Try

            If ContactsDataGrid.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
                AirsNumber = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 5, 8)
                Key = Mid(ContactsDataGrid(1, hti.RowIndex).Value, 13)
                NewContactDataLoad()
            End If

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnNewClear_Click(sender As Object, e As EventArgs) Handles btnNewClear.Click
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
        txtNewPhoneNumber.Clear()
        mtbNewPhoneNumber2.Clear()
        mtbNewZipCode.Clear()
        Key = ContactKey.None
    End Sub

    Private Sub btnNewUpdate_Click(sender As Object, e As EventArgs) Handles btnNewUpdate.Click
        Try
            Dim newKey As String = ""

            If AirsNumber.ToString <> "" Then
                If Not rdbNewComplianceContact.Checked AndAlso
                   Not rdbNewEISContact.Checked AndAlso
                   Not rdbNewESContact.Checked AndAlso
                   Not rdbNewFeeContact.Checked AndAlso
                   Not rdbNewMonitoringContact.Checked AndAlso
                   Not rdbNewPermittingContact.Checked Then

                    MsgBox("Please select a Contact Type first" & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Text)
                    Return
                End If

                If Key <> ContactKey.None Then
                    Dim query As String = "UPDATE APBCONTACTINFORMATION " &
                        "SET STRCONTACTFIRSTNAME = @STRCONTACTFIRSTNAME " &
                        ", STRCONTACTLASTNAME = @STRCONTACTLASTNAME " &
                        ", STRCONTACTPREFIX = @STRCONTACTPREFIX " &
                        ", STRCONTACTSUFFIX = @STRCONTACTSUFFIX " &
                        ", STRCONTACTTITLE = @STRCONTACTTITLE " &
                        ", STRCONTACTCOMPANYNAME = @STRCONTACTCOMPANYNAME " &
                        ", STRCONTACTPHONENUMBER1 = @STRCONTACTPHONENUMBER1 " &
                        ", STRCONTACTPHONENUMBER2 = @STRCONTACTPHONENUMBER2 " &
                        ", STRCONTACTFAXNUMBER = @STRCONTACTFAXNUMBER " &
                        ", STRCONTACTEMAIL = @STRCONTACTEMAIL " &
                        ", STRCONTACTADDRESS1 = @STRCONTACTADDRESS1 " &
                        ", STRCONTACTCITY = @STRCONTACTCITY " &
                        ", STRCONTACTSTATE = @STRCONTACTSTATE " &
                        ", STRCONTACTZIPCODE = @STRCONTACTZIPCODE " &
                        ", STRMODIFINGPERSON = @STRMODIFINGPERSON " &
                        ", DATMODIFINGDATE = GETDATE() " &
                        ", STRCONTACTDESCRIPTION = @STRCONTACTDESCRIPTION " &
                        "WHERE  STRAIRSNUMBER = @STRAIRSNUMBER " &
                        "AND STRKEY = @STRKEY "

                    Dim p As SqlParameter() = {
                        New SqlParameter("@STRCONTACTFIRSTNAME", txtNewFirstName.Text),
                        New SqlParameter("@STRCONTACTLASTNAME", txtNewLastName.Text),
                        New SqlParameter("@STRCONTACTPREFIX", txtNewPrefix.Text),
                        New SqlParameter("@STRCONTACTSUFFIX", txtNewSuffix.Text),
                        New SqlParameter("@STRCONTACTTITLE", txtNewTitle.Text),
                        New SqlParameter("@STRCONTACTCOMPANYNAME", txtNewCompany.Text),
                        New SqlParameter("@STRCONTACTPHONENUMBER1", txtNewPhoneNumber.Text),
                        New SqlParameter("@STRCONTACTPHONENUMBER2", mtbNewPhoneNumber2.Text),
                        New SqlParameter("@STRCONTACTFAXNUMBER", mtbNewFaxNumber.Text),
                        New SqlParameter("@STRCONTACTEMAIL", txtNewEmail.Text),
                        New SqlParameter("@STRCONTACTADDRESS1", txtNewAddress.Text),
                        New SqlParameter("@STRCONTACTCITY", txtNewCity.Text),
                        New SqlParameter("@STRCONTACTSTATE", txtNewState.Text),
                        New SqlParameter("@STRCONTACTZIPCODE", mtbNewZipCode.Text),
                        New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                        New SqlParameter("@STRCONTACTDESCRIPTION", txtNewDescrption.Text),
                        New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                        New SqlParameter("@STRKEY", Key.ToString("D"))
                    }

                    DB.RunCommand(query, p)
                Else
                    If rdbNewMonitoringContact.Checked Then
                        newKey = "10"
                    ElseIf rdbNewComplianceContact.Checked Then
                        newKey = "20"
                    ElseIf rdbNewPermittingContact.Checked Then
                        newKey = "30"
                    ElseIf rdbNewFeeContact.Checked Then
                        newKey = "40"
                    ElseIf rdbNewEISContact.Checked Then
                        newKey = "41"
                    ElseIf rdbNewESContact.Checked Then
                        newKey = "42"
                    End If

                    If newKey <> "" Then
                        Dim query As String = "UPDATE APBCONTACTINFORMATION " &
                            "SET STRCONTACTFIRSTNAME = @STRCONTACTFIRSTNAME " &
                            ", STRCONTACTLASTNAME = @STRCONTACTLASTNAME " &
                            ", STRCONTACTPREFIX = @STRCONTACTPREFIX " &
                            ", STRCONTACTSUFFIX = @STRCONTACTSUFFIX " &
                            ", STRCONTACTTITLE = @STRCONTACTTITLE " &
                            ", STRCONTACTCOMPANYNAME = @STRCONTACTCOMPANYNAME " &
                            ", STRCONTACTPHONENUMBER1 = @STRCONTACTPHONENUMBER1 " &
                            ", STRCONTACTPHONENUMBER2 = @STRCONTACTPHONENUMBER2 " &
                            ", STRCONTACTFAXNUMBER = @STRCONTACTFAXNUMBER " &
                            ", STRCONTACTEMAIL = @STRCONTACTEMAIL " &
                            ", STRCONTACTADDRESS1 = @STRCONTACTADDRESS1 " &
                            ", STRCONTACTCITY = @STRCONTACTCITY " &
                            ", STRCONTACTSTATE = @STRCONTACTSTATE " &
                            ", STRCONTACTZIPCODE = @STRCONTACTZIPCODE " &
                            ", STRMODIFINGPERSON = @STRMODIFINGPERSON " &
                            ", DATMODIFINGDATE = GETDATE() " &
                            ", STRCONTACTDESCRIPTION = @STRCONTACTDESCRIPTION " &
                            "WHERE  STRAIRSNUMBER = @STRAIRSNUMBER " &
                            "AND STRKEY = @STRKEY "

                        Dim p As SqlParameter() = {
                            New SqlParameter("@STRCONTACTFIRSTNAME", txtNewFirstName.Text),
                            New SqlParameter("@STRCONTACTLASTNAME", txtNewLastName.Text),
                            New SqlParameter("@STRCONTACTPREFIX", txtNewPrefix.Text),
                            New SqlParameter("@STRCONTACTSUFFIX", txtNewSuffix.Text),
                            New SqlParameter("@STRCONTACTTITLE", txtNewTitle.Text),
                            New SqlParameter("@STRCONTACTCOMPANYNAME", txtNewCompany.Text),
                            New SqlParameter("@STRCONTACTPHONENUMBER1", txtNewPhoneNumber.Text),
                            New SqlParameter("@STRCONTACTPHONENUMBER2", mtbNewPhoneNumber2.Text),
                            New SqlParameter("@STRCONTACTFAXNUMBER", mtbNewFaxNumber.Text),
                            New SqlParameter("@STRCONTACTEMAIL", txtNewEmail.Text),
                            New SqlParameter("@STRCONTACTADDRESS1", txtNewAddress.Text),
                            New SqlParameter("@STRCONTACTCITY", txtNewCity.Text),
                            New SqlParameter("@STRCONTACTSTATE", txtNewState.Text),
                            New SqlParameter("@STRCONTACTZIPCODE", mtbNewZipCode.Text),
                            New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                            New SqlParameter("@STRCONTACTDESCRIPTION", txtNewDescrption.Text),
                            New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                            New SqlParameter("@STRKEY", newKey)
                        }

                        DB.RunCommand(query, p)
                    End If

                End If
                LoadContactsDataset()
                MsgBox("Data Updated", MsgBoxStyle.Information, Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNewSave_Click(sender As Object, e As EventArgs) Handles btnNewSave.Click
        Try
            Dim newKey As String = ""
            Dim query As String

            If AirsNumber.ToString <> "" Then
                If rdbNewMonitoringContact.Checked Then
                    newKey = "10"
                ElseIf rdbNewComplianceContact.Checked Then
                    newKey = "20"
                ElseIf rdbNewPermittingContact.Checked Then
                    newKey = "30"
                ElseIf rdbNewFeeContact.Checked Then
                    newKey = "40"
                ElseIf rdbNewEISContact.Checked Then
                    newKey = "41"
                ElseIf rdbNewESContact.Checked Then
                    newKey = "42"
                End If

                If newKey = "" Then
                    MsgBox("Please select a Contact Type first" & vbCrLf & "No Data Saved", MsgBoxStyle.Information, Text)
                    Return
                Else
                    Select Case newKey
                        Case "10", "20", "30"
                            query = "delete APBContactInformation " &
                            "where strAIRSnumber = @airs " &
                            "and strKey = @key "

                            Dim p As SqlParameter() = {
                                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                                New SqlParameter("@key", Mid(newKey, 1, 1) & "9")
                            }

                            DB.RunCommand(query, p)

                            query = "Update APBContactInformation set " &
                            "strKey = concat(substring(strKey, 1,1), substring(strKey, 2,1) + 1), " &
                            "strContactKey = concat(substring(strContactKey, 1, 13), substring(strContactKey, 14, 1) + 1) " &
                            "where strAIRSNumber = @airs " &
                            "and strKey like @key "

                            Dim p2 As SqlParameter() = {
                                New SqlParameter("@airs", AirsNumber.DbFormattedString),
                                New SqlParameter("@key", Mid(newKey, 1, 1) & "%")
                            }

                            DB.RunCommand(query, p2)

                            query = "INSERT INTO APBCONTACTINFORMATION " &
                                "(STRCONTACTKEY, STRAIRSNUMBER, STRKEY, STRCONTACTFIRSTNAME, " &
                                "STRCONTACTLASTNAME, STRCONTACTPREFIX, STRCONTACTSUFFIX, STRCONTACTTITLE, " &
                                "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1, STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER, " &
                                "STRCONTACTEMAIL, STRCONTACTADDRESS1, STRCONTACTADDRESS2, STRCONTACTCITY, " &
                                "STRCONTACTSTATE, STRCONTACTZIPCODE, STRMODIFINGPERSON, DATMODIFINGDATE, " &
                                "STRCONTACTDESCRIPTION) " &
                                "select " &
                                "@STRCONTACTKEY, @STRAIRSNUMBER, @STRKEY, @STRCONTACTFIRSTNAME, " &
                                "@STRCONTACTLASTNAME, @STRCONTACTPREFIX, @STRCONTACTSUFFIX, @STRCONTACTTITLE, " &
                                "@STRCONTACTCOMPANYNAME, @STRCONTACTPHONENUMBER1, @STRCONTACTPHONENUMBER2, @STRCONTACTFAXNUMBER, " &
                                "@STRCONTACTEMAIL, @STRCONTACTADDRESS1, @STRCONTACTADDRESS2, @STRCONTACTCITY, " &
                                "@STRCONTACTSTATE, @STRCONTACTZIPCODE, @STRMODIFINGPERSON, getdate(), " &
                                "@STRCONTACTDESCRIPTION " &
                                "WHERE NOT EXISTS " &
                                "(SELECT * FROM APBCONTACTINFORMATION " &
                                "WHERE STRKEY = @STRKEY " &
                                "AND STRAIRSNUMBER = @STRAIRSNUMBER) "
                            Dim p3 As SqlParameter() = {
                                New SqlParameter("@STRCONTACTKEY", AirsNumber.DbFormattedString & newKey),
                                New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                                New SqlParameter("@STRKEY", newKey),
                                New SqlParameter("@STRCONTACTFIRSTNAME", txtNewFirstName.Text),
                                New SqlParameter("@STRCONTACTLASTNAME", txtNewLastName.Text),
                                New SqlParameter("@STRCONTACTPREFIX", txtNewPrefix.Text),
                                New SqlParameter("@STRCONTACTSUFFIX", txtNewSuffix.Text),
                                New SqlParameter("@STRCONTACTTITLE", txtNewTitle.Text),
                                New SqlParameter("@STRCONTACTCOMPANYNAME", txtNewCompany.Text),
                                New SqlParameter("@STRCONTACTPHONENUMBER1", txtNewPhoneNumber.Text),
                                New SqlParameter("@STRCONTACTPHONENUMBER2", mtbNewPhoneNumber2.Text),
                                New SqlParameter("@STRCONTACTFAXNUMBER", mtbNewFaxNumber.Text),
                                New SqlParameter("@STRCONTACTEMAIL", txtNewEmail.Text),
                                New SqlParameter("@STRCONTACTADDRESS1", txtNewAddress.Text),
                                New SqlParameter("@STRCONTACTADDRESS2", ""),
                                New SqlParameter("@STRCONTACTCITY", txtNewCity.Text),
                                New SqlParameter("@STRCONTACTSTATE", txtNewState.Text),
                                New SqlParameter("@STRCONTACTZIPCODE", mtbNewZipCode.Text),
                                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                                New SqlParameter("@STRCONTACTDESCRIPTION", txtNewDescrption.Text)
                            }

                            DB.RunCommand(query, p3)

                        Case Else
                            query = "INSERT INTO APBCONTACTINFORMATION " &
                                "(STRCONTACTKEY, STRAIRSNUMBER, STRKEY, STRCONTACTFIRSTNAME, " &
                                "STRCONTACTLASTNAME, STRCONTACTPREFIX, STRCONTACTSUFFIX, STRCONTACTTITLE, " &
                                "STRCONTACTCOMPANYNAME, STRCONTACTPHONENUMBER1, STRCONTACTPHONENUMBER2, STRCONTACTFAXNUMBER, " &
                                "STRCONTACTEMAIL, STRCONTACTADDRESS1, STRCONTACTADDRESS2, STRCONTACTCITY, " &
                                "STRCONTACTSTATE, STRCONTACTZIPCODE, STRMODIFINGPERSON, DATMODIFINGDATE, " &
                                "STRCONTACTDESCRIPTION) " &
                                "select " &
                                "@STRCONTACTKEY, @STRAIRSNUMBER, @STRKEY, @STRCONTACTFIRSTNAME, " &
                                "@STRCONTACTLASTNAME, @STRCONTACTPREFIX, @STRCONTACTSUFFIX, @STRCONTACTTITLE, " &
                                "@STRCONTACTCOMPANYNAME, @STRCONTACTPHONENUMBER1, @STRCONTACTPHONENUMBER2, @STRCONTACTFAXNUMBER, " &
                                "@STRCONTACTEMAIL, @STRCONTACTADDRESS1, @STRCONTACTADDRESS2, @STRCONTACTCITY, " &
                                "@STRCONTACTSTATE, @STRCONTACTZIPCODE, @STRMODIFINGPERSON, getdate(), " &
                                "@STRCONTACTDESCRIPTION " &
                                "WHERE NOT EXISTS " &
                                "(SELECT * FROM APBCONTACTINFORMATION " &
                                "WHERE STRKEY = @STRKEY " &
                                "AND STRAIRSNUMBER = @STRAIRSNUMBER) "
                            Dim p3 As SqlParameter() = {
                                New SqlParameter("@STRCONTACTKEY", AirsNumber.DbFormattedString & newKey),
                                New SqlParameter("@STRAIRSNUMBER", AirsNumber.DbFormattedString),
                                New SqlParameter("@STRKEY", newKey),
                                New SqlParameter("@STRCONTACTFIRSTNAME", txtNewFirstName.Text),
                                New SqlParameter("@STRCONTACTLASTNAME", txtNewLastName.Text),
                                New SqlParameter("@STRCONTACTPREFIX", txtNewPrefix.Text),
                                New SqlParameter("@STRCONTACTSUFFIX", txtNewSuffix.Text),
                                New SqlParameter("@STRCONTACTTITLE", txtNewTitle.Text),
                                New SqlParameter("@STRCONTACTCOMPANYNAME", txtNewCompany.Text),
                                New SqlParameter("@STRCONTACTPHONENUMBER1", txtNewPhoneNumber.Text),
                                New SqlParameter("@STRCONTACTPHONENUMBER2", mtbNewPhoneNumber2.Text),
                                New SqlParameter("@STRCONTACTFAXNUMBER", mtbNewFaxNumber.Text),
                                New SqlParameter("@STRCONTACTEMAIL", txtNewEmail.Text),
                                New SqlParameter("@STRCONTACTADDRESS1", txtNewAddress.Text),
                                New SqlParameter("@STRCONTACTADDRESS2", ""),
                                New SqlParameter("@STRCONTACTCITY", txtNewCity.Text),
                                New SqlParameter("@STRCONTACTSTATE", txtNewState.Text),
                                New SqlParameter("@STRCONTACTZIPCODE", mtbNewZipCode.Text),
                                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                                New SqlParameter("@STRCONTACTDESCRIPTION", txtNewDescrption.Text)
                            }

                            DB.RunCommand(query, p3)
                    End Select

                    LoadContactsDataset()
                    MsgBox("Data Inserted", MsgBoxStyle.Information, Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Class