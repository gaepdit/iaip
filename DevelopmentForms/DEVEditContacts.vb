Imports Oracle.DataAccess.Client

Public Class DEVEditContacts
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub DEVEditContacts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        monitor.TrackFeature("Dev." & Me.Name)
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim strContactkey As String = ""
            Dim strFirstName As String = ""
            Dim strLastName As String = ""
            Dim strPrefix As String = ""
            Dim strSuffix As String = ""
            Dim strTitle As String = ""
            Dim strCompanyName As String = ""
            Dim strPhoneNumber1 As String = ""
            Dim strPhoneNumber2 As String = ""
            Dim strFaxNumber As String = ""
            Dim strEmail As String = ""
            Dim strAddress As String = ""
            Dim strCity As String = ""
            Dim strState As String = ""
            Dim strZipCode As String = ""
            Dim strDescription As String = ""
            Dim ModPerson As String = ""
            Dim modDate As String = ""

            Dim ContactID As String = ""
            Dim AIRSNumber As String = ""
            Dim ContactType As String = ""
            Dim ContactID2 As String = ""
            Dim OLAPid As String = ""

            SQL = "select * " & _
            "From " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber like '0413%' " & _
            "order by datModifingDate asc "

            cmd = New OracleCommand(SQL, Conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstName")) Then
                    strFirstName = ""
                Else
                    strFirstName = Mid(dr.Item("strContactFirstName"), 1, 50)
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    strLastName = ""
                Else
                    strLastName = Mid(dr.Item("strContactLastName"), 1, 50)
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    strPrefix = ""
                Else
                    strPrefix = Mid(dr.Item("strContactPrefix"), 1, 5)
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    strSuffix = ""
                Else
                    strSuffix = Mid(dr.Item("strContactSuffix"), 1, 5)
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    strTitle = ""
                Else
                    strTitle = Mid(dr.Item("strContactTitle"), 1, 100)
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    strCompanyName = ""
                Else
                    strCompanyName = Mid(dr.Item("strContactCompanyName"), 1, 200)
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    strPhoneNumber1 = ""
                Else
                    strPhoneNumber1 = Mid(dr.Item("strCOntactPhoneNumber1"), 1, 15)
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber2")) Then
                    strPhoneNumber2 = ""
                Else
                    strPhoneNumber2 = Mid(dr.Item("strCOntactPhoneNumber2"), 1, 15)
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    strFaxNumber = ""
                Else
                    strFaxNumber = Mid(dr.Item("strContactFaxNumber"), 1, 15)
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    strEmail = ""
                Else
                    strEmail = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    strAddress = ""
                Else
                    strAddress = Mid(dr.Item("strContactAddress1"), 1, 250)
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    strCity = ""
                Else
                    strCity = Mid(dr.Item("strContactCity"), 1, 50)
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    strState = ""
                Else
                    strState = Mid(dr.Item("strContactState"), 1, 2)
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    strZipCode = ""
                Else
                    strZipCode = Mid(dr.Item("strContactZipCode"), 1, 9)
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    strDescription = ""
                Else
                    strDescription = Mid(dr.Item("strContactDescription"), 1, 4000)
                End If
                If IsDBNull(dr.Item("strContactKey")) Then
                    strContactkey = ""
                Else
                    strContactkey = dr.Item("strContactKey")
                End If
                If IsDBNull(dr.Item("strModifingPerson")) Then
                    ModPerson = "1"
                Else
                    ModPerson = dr.Item("strModifingPerson")
                End If
                If IsDBNull(dr.Item("datModifingDate")) Then
                    modDate = OracleDate
                Else
                    modDate = dr.Item("datModifingDate")
                End If

                If strContactkey <> "" Then
                    SQL2 = "Select numContactID " & _
                    "from " & DBNameSpace & ".APBContactData " & _
                    "where upper(strFirstName) = '" & Replace(strFirstName.ToUpper, "'", "''") & "' " & _
                    "and upper(strLastName) = '" & Replace(strLastName.ToUpper, "'", "''") & "' "
                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    recExist = dr2.Read
                    dr2.Close()
                    If recExist = False Then
                        AIRSNumber = Mid(strContactkey, 1, 12)
                        ContactType = Mid(strContactkey, 13)

                        SQL2 = "Select (max(numContactID) + 1) as ConID " & _
                        "From " & DBNameSpace & ".APBContactData "
                        cmd2 = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("ConID")) Then
                                ContactID = "1"
                            Else
                                ContactID = dr2.Item("ConID")
                            End If
                        End While
                        dr2.Close()

                        SQL2 = "Insert into " & DBNameSpace & ".APBContactData " & _
                        "values " & _
                        "('" & ContactID & "', '" & Replace(strFirstName, "'", "''") & "', " & _
                        "'" & Replace(strLastName, "'", "''") & "', '" & Replace(strPrefix, "'", "''") & "', " & _
                        "'" & Replace(strSuffix, "'", "''") & "', '" & Replace(strTitle, "'", "''") & "', " & _
                        "'" & Replace(strCompanyName, "'", "''") & "', '" & Replace(strPhoneNumber1, "'", "''") & "', " & _
                        "'" & Replace(strPhoneNumber2, "'", "''") & "', '" & Replace(strFaxNumber, "'", "''") & "', " & _
                        "'" & Replace(strEmail, "'", "''") & "', '" & Replace(strAddress, "'", "''") & "', " & _
                        "'" & Replace(strCity, "'", "''") & "', '" & Replace(strState, "'", "''") & "', " & _
                        "'" & Replace(strZipCode, "'", "''") & "', '" & Replace(strDescription, "'", "''") & "', " & _
                        "'" & Replace(ModPerson, "'", "''") & "', '" & OracleDate & "', " & _
                        "'" & Replace(ModPerson, "'", "''") & "', '" & OracleDate & "', " & _
                        "'' ) "
                        cmd2 = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        dr2.Close()
                    Else
                        cmd2 = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("numContactID")) Then
                                ContactID = ""
                            Else
                                ContactID = dr2.Item("numContactID")
                            End If
                        End While
                        dr2.Close()
                    End If

                    If ContactID <> "" Then
                        SQL2 = "Select " & _
                        "numID " & _
                        "from " & DBNameSpace & ".APBContacts " & _
                        "where strAIRSNumber = '" & AIRSNumber & "' " & _
                        "and strkey = '" & ContactType & "' "

                        cmd2 = New OracleCommand(SQL2, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr2 = cmd2.ExecuteReader
                        recExist = dr2.Read
                        dr2.Close()
                        If recExist = False Then
                            SQL2 = "Select " & _
                            "(max(numID) + 1) as numID " & _
                            "from " & DBNameSpace & ".APBContacts "
                            cmd2 = New OracleCommand(SQL2, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            While dr2.Read
                                If IsDBNull(dr2.Item("numID")) Then
                                    ContactID2 = "1"
                                Else
                                    ContactID2 = dr2.Item("numID")
                                End If
                            End While
                            dr2.Close()

                            SQL2 = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ContactID2 & "', '" & AIRSNumber & "', " & _
                            "'" & ContactType & "', '" & ContactID & "') "
                            cmd2 = New OracleCommand(SQL2, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        Else
                            dr2 = cmd2.ExecuteReader
                            While dr2.Read
                                If IsDBNull(dr2.Item("numID")) Then
                                    ContactID2 = ""
                                Else
                                    ContactID2 = dr2.Item("numID")
                                End If
                            End While
                            dr2.Close()

                            SQL2 = "Update " & DBNameSpace & ".APBContacts set " & _
                            "numContactID = '" & ContactID & "' " & _
                            "where numID = '" & ContactID2 & "' "
                            cmd2 = New OracleCommand(SQL2, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        End If
                    End If
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "numContactId, strEmail " & _
            "From " & DBNameSpace & ".APBContactData "  

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                ContactID = ""
                strEmail = ""

                If IsDBNull(dr.Item("numContactID")) Then
                    ContactID = ""
                Else
                    ContactID = dr.Item("numContactID")
                End If
                If IsDBNull(dr.Item("strEmail")) Then
                    strEmail = ""
                Else
                    strEmail = dr.Item("strEmail")
                End If

                SQL2 = "Select numUserID " & _
                "from " & DBNameSpace & ".OlapUserLogIn " & _
                "where upper(strUserEmail) = '" & Replace(strEmail.ToUpper, "'", "''") & "' "

                cmd2 = New OracleCommand(SQL2, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr2 = cmd2.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("numUserID")) Then
                        OLAPid = ""
                    Else
                        OLAPid = dr2.Item("numUserID")
                    End If
                End While
                dr2.Close()
                If OLAPid <> "" Then
                    SQL2 = "update " & DBNameSpace & ".APBContactData set " & _
                    "numOlapID = '" & OLAPid & "' " & _
                    "where numContactID = '" & ContactID & "' "

                    cmd2 = New OracleCommand(SQL2, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                End If
            End While
            dr.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnAddNewContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewContact.Click
        Try

            SaveNewContact()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveNewContact()
        Try
            Dim ResultDoc As DialogResult
            Dim OLAPid As String = ""
        

            SQL = "select " & _
            "numContactID " & _
            "from " & DBNameSpace & ".APBContactData " & _
            "where upper(strEmail) = '" & Replace(txtEmailAddress.Text.ToUpper, "'", "''") & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                ResultDoc = MessageBox.Show("There is already a user with this email addrss in the system." & vbCrLf & _
                    "Do you still want to add this user as a new contact?", Me.Text, _
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                Select Case ResultDoc
                    Case Windows.Forms.DialogResult.Yes

                    Case Else
                        Exit Sub
                End Select
            End If

            SQL = "Insert into " & DBNameSpace & ".APBContactData " & _
            "values " & _
            "((select " & _
            "case " & _
            "when max(numContactID) is null then 1 " & _
            "else max(numContactID) + 1 " & _
            "End ConID " & _
            "from " & DBNameSpace & ".APBContactData), " & _
            "'" & Replace(txtContactFirstName.Text, "'", "''") & "', '" & Replace(txtContactLastName.Text, "'", "''") & "', " & _
            "'" & Replace(txtContactSocialTitle.Text, "'", "''") & "', '" & Replace(txtContactPedigree.Text, "'", "''") & "', " & _
            "'" & Replace(txtContactTitle.Text, "'", "''") & "', '" & Replace(txtContactCompanyName.Text, "'", "''") & "', " & _
            "'" & Replace(mtbPhoneNumber.Text, "'", "''") & "', '" & Replace(mtbPhoneNumber2.Text, "'", "''") & "', " & _
            "'" & Replace(mtbFaxNumber.Text, "'", "''") & "', '" & Replace(txtEmailAddress.Text, "'", "''") & "', " & _
            "'" & Replace(txtContactAddress.Text, "'", "''") & "', '" & Replace(txtContactCity.Text, "'", "''") & "', " & _
            "'" & Replace(txtContactState.Text, "'", "''") & "', '" & Replace(txtContactZipCode.Text, "'", "''") & "', " & _
            "'" & Replace(txtContactNotes.Text, "'", "''") & "', " & _
            "'" & UserGCode & "', '" & OracleDate & "', " & _
            "'" & UserGCode & "', '" & OracleDate & "', " & _
            "'') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select " & _
            "numContactID " & _
            "from " & DBNameSpace & ".APBContactData " & _
            "where strEmail = '" & txtEmailAddress.Text & "' " & _
            "and strFirstName = '" & txtContactFirstName.Text & "' " & _
            "and strLastName = '" & txtContactLastName.Text & "' " & _
            "and numCreator = '" & UserGCode & "' " & _
            "and numLastModified = '" & UserGCode & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numContactID")) Then
                    txtContactId.Clear()
                Else
                    txtContactId.Text = dr.Item("numContactID")
                End If
            End While

            OLAPid = ""
            SQL = "select " & _
            "numUserID " & _
            "from " & DBNameSpace & ".OLAPUserLogIn " & _
            "where upper(strUserEmail) = '" & Replace(txtEmailAddress.Text.ToUpper, "'", "''") & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("numUserID")) Then
                    OLAPid = ""
                Else
                    OLAPid = dr.Item("numUserID")
                End If
            End While
            dr.Close()

            If OLAPid <> "" Then
                SQL = "Update " & DBNameSpace & ".APBContactData set " & _
                "numOLAPid = '" & OLAPid & "' " & _
                "where numContactID = '" & txtContactId.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            SaveContactTypes()

            MsgBox("Contact Data Created.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SaveContactTypes()
        Try
            Dim ID(1, 9) As String
            Dim ID_0 As String = ""

            If mtbAIRSNumber.Text = "" Then
                Exit Sub
            End If

            '10
            If chbMonitoringContact.Checked = True Then
                SQL = "Select " & _
                "numID " & _
                "from " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and strKey = '10' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '10', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '1%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '1%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'10', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'1" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop

                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '1%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '20 
            If chbComplianceContact.Checked = True Then
                SQL = "Select " & _
                "numID " & _
                "from " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and strKey = '20' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '20', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '2%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '2%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'20', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'2" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '2%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            End If
            '30 
            If chbPermittingContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '30' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '30', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '3%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '3%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'30', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'3" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '3%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '40
            If chbFeeContact.Checked = True Then
                SQL = "Select " & _
                "numID " & _
                "from " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and strKey = '40' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '40', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Update " & DBNameSpace & ".APBContacts set " & _
                    "numContactID = '" & txtContactId.Text & "' " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey = '40' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey = '40' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '41
            If chbGECOContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '41' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '41', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Update " & DBNameSpace & ".APBContacts set " & _
                    "numContactID = '" & txtContactId.Text & "' " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey = '41' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey = '41' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '42
            If chbEIContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '42' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '42', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Update " & DBNameSpace & ".APBContacts set " & _
                    "numContactID = '" & txtContactId.Text & "' " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey = '42' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            End If
            '43
            If chbESContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '43' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '43', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Update " & DBNameSpace & ".APBContacts set " & _
                    "numContactID = '" & txtContactId.Text & "' " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey = '43' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey = '43' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '50
            If chbAmbientContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '50' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '50', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '5%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '5%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'50', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'5" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '5%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '60
            If chbPlanningContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '60' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '60', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '6%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '6%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'60', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'6" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '6%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If
            '70 
            If chbDistrictContact.Checked = True Then
                SQL = "Select " & _
               "numID " & _
               "from " & DBNameSpace & ".APBContacts " & _
               "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
               "and strKey = '70' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when max(numID) is null then 1 " & _
                    "else max(numID) + 1 " & _
                    "end numID " & _
                    "from " & DBNameSpace & ".APBContacts), " & _
                    "'0413" & mtbAIRSNumber.Text & "', '70', " & _
                    "'" & txtContactId.Text & "') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    Dim NumID As String = ""
                    Dim ContactID As String = ""

                    i = 0
                    ID(0, i) = txtContactId.Text
                    temp = txtContactId.Text & ","
                    i += 1

                    SQL = "Select " & _
                    "numID, numContactID " & _
                    "from " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '7%' " & _
                    "order by strKey asc "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If IsDBNull(dr.Item("numContactID")) Then
                            ContactID = ""
                        Else
                            ContactID = dr.Item("numContactID")
                        End If
                        If IsDBNull(dr.Item("numID")) Then
                            NumID = ""
                        Else
                            NumID = dr.Item("numID")
                        End If
                        If temp.Contains((ContactID & ",")) = True Then
                        Else
                            If i = 10 Then
                            Else
                                ID(0, i) = ContactID
                                ID(1, i) = NumID
                                i += 1
                                temp = temp & ContactID & ","
                            End If
                        End If
                    End While
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                    "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                    "and strKey like '7%' "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Do Until i = 0
                        If ID(1, i - 1) Is Nothing Then
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "((Select " & _
                            "case " & _
                            "when max(numID) is null then 1 " & _
                            "else max(numID) + 1 " & _
                            "end numID " & _
                            "from " & DBNameSpace & ".APBContacts), " & _
                            "'0413" & mtbAIRSNumber.Text & "', " & _
                            "'70', '" & ID(0, i - 1).ToString & "') "
                        Else
                            SQL = "Insert into " & DBNameSpace & ".APBContacts " & _
                            "values " & _
                            "('" & ID(1, i - 1).ToString & "', '0413" & mtbAIRSNumber.Text & "', " & _
                            "'7" & (i - 1) & "', '" & ID(0, i - 1).ToString & "') "
                        End If
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                        i -= 1
                    Loop
                End If
            Else
                SQL = "Delete " & DBNameSpace & ".APBContacts " & _
                "where strAIRSNumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactID = '" & txtContactId.Text & "' " & _
                "and strKey like '7%' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchContacts.Click
        Try
            SearchContacts()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub SearchContacts()
        Try
            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim Email As String = ""
            Dim AIRSNum As String = ""

            If txtSearchFirstName.Text <> "" Then
                FirstName = txtSearchFirstName.Text.ToUpper
            Else
                FirstName = ""
            End If
            If txtSearchLastName.Text <> "" Then
                LastName = txtSearchLastName.Text.ToUpper
            Else
                LastName = ""
            End If
            If txtSearchEmail.Text <> "" Then
                Email = txtSearchEmail.Text.ToUpper
            Else
                Email = ""
            End If
            If mtbAIRSNumber.Text <> "" Then
                AIRSNum = mtbSearchAIRS.Text
            Else
                AIRSNum = ""
            End If

            If mtbSearchAIRS.Text = "" Then
                SQL = "select " & _
                "distinct " & _
                "" & DBNameSpace & ".APBContactData.numContactID, " & _
                "strFirstName, strLastName, " & _
                "strPrefix, strSuffix, " & _
                "strTitle, strCompanyName, " & _
                "strPhoneNumber1, strPhoneNumber2, " & _
                "strFaxNumber, strEmail, " & _
                "strAddress, strCity, " & _
                "strState, strZipCode, " & _
                "strDescription " & _
                "from " & DBNameSpace & ".APBContactData " & _
                "where numcontactid is not null " & _
                "and (Upper(strFirstName) like '%" & FirstName & "%' or strFirstName is null)  " & _
                "and (Upper(strLastName) like '%" & LastName & "%' or strLastName is null) " & _
                "and (Upper(strEmail) like '%" & Email & "%' or strEmail is null) "
            Else
                SQL = "select " & _
                "distinct " & _
                "" & DBNameSpace & ".APBContactData.numContactID, " & _
                "strFirstName, strLastName, " & _
                "strPrefix, strSuffix, " & _
                "strTitle, strCompanyName, " & _
                "strPhoneNumber1, strPhoneNumber2, " & _
                "strFaxNumber, strEmail, " & _
                "strAddress, strCity, " & _
                "strState, strZipCode, " & _
                 "strDescription " & _
                "from " & DBNameSpace & ".APBContactData " & _
                "where numcontactid is not null " & _
                "and (Upper(strFirstName) like '%" & FirstName & "%' or strFirstName is null)  " & _
                "and (Upper(strLastName) like '%" & LastName & "%' or strLastName is null) " & _
                "and (Upper(strEmail) like '%" & Email & "%' or strEmail is null) " & _
                "and Exists " & _
                "(select * from " & DBNameSpace & ".APBContacts " & _
                "where " & DBNameSpace & ".APBContactData.numContactID = " & DBNameSpace & ".APBContacts.numContactID " & _
                "and strAIRSnumber like '0413%" & AIRSNum & "%' ) "
            End If

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            da.Fill(ds, "EditContacts")

            dgvContacts.DataSource = ds
            dgvContacts.DataMember = "EditContacts"

            dgvContacts.RowHeadersVisible = False
            dgvContacts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvContacts.AllowUserToResizeColumns = True
            dgvContacts.AllowUserToAddRows = False
            dgvContacts.AllowUserToDeleteRows = False
            dgvContacts.AllowUserToOrderColumns = True
            dgvContacts.AllowUserToResizeRows = True

            dgvContacts.Columns("numContactID").HeaderText = "Contact ID"
            dgvContacts.Columns("numContactID").DisplayIndex = 0
            dgvContacts.Columns("numContactID").Visible = False
            dgvContacts.Columns("strFirstName").HeaderText = "First Name"
            dgvContacts.Columns("strFirstName").DisplayIndex = 1
            dgvContacts.Columns("strFirstName").Width = 100
            dgvContacts.Columns("strLastName").HeaderText = "Last Name"
            dgvContacts.Columns("strLastName").DisplayIndex = 2
            dgvContacts.Columns("strLastName").Width = 100
            dgvContacts.Columns("strPrefix").HeaderText = "Prefix"
            dgvContacts.Columns("strPrefix").DisplayIndex = 3
            dgvContacts.Columns("strPrefix").Width = 75
            dgvContacts.Columns("strSuffix").HeaderText = "Suffix"
            dgvContacts.Columns("strSuffix").DisplayIndex = 4
            dgvContacts.Columns("strSuffix").Width = 75
            dgvContacts.Columns("strTitle").HeaderText = "Title"
            dgvContacts.Columns("strTitle").DisplayIndex = 5
            dgvContacts.Columns("strTitle").Width = 100
            dgvContacts.Columns("strCompanyName").HeaderText = "Company Name"
            dgvContacts.Columns("strCompanyName").DisplayIndex = 6
            dgvContacts.Columns("strCompanyName").Width = 200
            dgvContacts.Columns("strPhoneNumber1").HeaderText = "Phone Number"
            dgvContacts.Columns("strPhoneNumber1").DisplayIndex = 7
            dgvContacts.Columns("strPhoneNumber1").Width = 85
            dgvContacts.Columns("strPhoneNumber2").HeaderText = "Phone Number 2"
            dgvContacts.Columns("strPhoneNumber2").DisplayIndex = 8
            dgvContacts.Columns("strPhoneNumber2").Width = 85
            dgvContacts.Columns("strFaxNumber").HeaderText = "Fax Number"

            dgvContacts.Columns("strFaxNumber").DisplayIndex = 9
            dgvContacts.Columns("strFaxNumber").Width = 85
            dgvContacts.Columns("strEmail").HeaderText = "Email Address"
            dgvContacts.Columns("strEmail").DisplayIndex = 10
            dgvContacts.Columns("strEmail").Width = 100
            dgvContacts.Columns("strAddress").HeaderText = "Address"
            dgvContacts.Columns("strAddress").DisplayIndex = 11
            dgvContacts.Columns("strAddress").Width = 100
            dgvContacts.Columns("strCity").HeaderText = "City"
            dgvContacts.Columns("strCity").DisplayIndex = 12
            dgvContacts.Columns("strCity").Width = 100
            dgvContacts.Columns("strState").HeaderText = "State"
            dgvContacts.Columns("strState").DisplayIndex = 13
            dgvContacts.Columns("strState").Width = 50
            dgvContacts.Columns("strZipCode").HeaderText = "Zip Code"
            dgvContacts.Columns("strZipCode").DisplayIndex = 14
            dgvContacts.Columns("strZipCode").Width = 50
            dgvContacts.Columns("strDescription").HeaderText = "Description"
            dgvContacts.Columns("strDescription").DisplayIndex = 15
            dgvContacts.Columns("strDescription").Width = 200

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvContacts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvContacts.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvContacts.HitTest(e.X, e.Y)
        Try
            If dgvContacts.RowCount > 0 And hti.RowIndex <> -1 Then
                If IsDBNull(dgvContacts(0, hti.RowIndex).Value) Then
                    txtContactId.Text = ""
                Else
                    txtContactId.Text = dgvContacts(0, hti.RowIndex).Value
                End If
            End If

            If txtContactId.Text <> "" Then
                LoadContactData()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadContactData()
        Try
            SQL = "Select * " & _
            "from " & DBNameSpace & ".APBContactData " & _
            "where numContactID = '" & txtContactId.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFirstName")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strPrefix")) Then
                    txtContactSocialTitle.Clear()
                Else
                    txtContactSocialTitle.Text = dr.Item("strPrefix")
                End If
                If IsDBNull(dr.Item("strSuffix")) Then
                    txtContactPedigree.Clear()
                Else
                    txtContactPedigree.Text = dr.Item("strSuffix")
                End If
                If IsDBNull(dr.Item("strTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strCompanyName")) Then
                    txtContactCompanyName.Clear()
                Else
                    txtContactCompanyName.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("strPhoneNumber1")) Then
                    mtbPhoneNumber.Clear()
                Else
                    mtbPhoneNumber.Text = dr.Item("strPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strPhoneNumber2")) Then
                    mtbPhoneNumber2.Clear()
                Else
                    mtbPhoneNumber2.Text = dr.Item("strPhoneNumber2")
                End If
                If IsDBNull(dr.Item("strFaxNumber")) Then
                    mtbFaxNumber.Clear()
                Else
                    mtbFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strEmail")) Then
                    txtEmailAddress.Clear()
                Else
                    txtEmailAddress.Text = dr.Item("strEmail")
                End If
                If IsDBNull(dr.Item("strAddress")) Then
                    txtContactAddress.Clear()
                Else
                    txtContactAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strCity")) Then
                    txtContactCity.Clear()
                Else
                    txtContactCity.Text = dr.Item("strCity")
                End If
                If IsDBNull(dr.Item("strState")) Then
                    txtContactState.Clear()
                Else
                    txtContactState.Text = dr.Item("strState")
                End If
                If IsDBNull(dr.Item("strZipCode")) Then
                    txtContactZipCode.Clear()
                Else
                    txtContactZipCode.Text = dr.Item("strZipCode")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    txtContactNotes.Clear()
                Else
                    txtContactNotes.Text = dr.Item("strDescription")
                End If
            End While
            dr.Close()

            chbMonitoringContact.Checked = False
            chbComplianceContact.Checked = False
            chbPermittingContact.Checked = False
            chbFeeContact.Checked = False
            chbGECOContact.Checked = False
            chbEIContact.Checked = False
            chbESContact.Checked = False
            chbAmbientContact.Checked = False
            chbPlanningContact.Checked = False
            chbDistrictContact.Checked = False

            If mtbAIRSNumber.Text <> "" Then
                SQL = "Select " & _
                "strKey " & _
                "from " & DBNameSpace & ".APBContacts " & _
                "where strAIRSnumber = '0413" & mtbAIRSNumber.Text & "' " & _
                "and numContactId = '" & txtContactId.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strKey")) Then
                        Else
                            temp = dr.Item("strKey")
                        End If
                        Select Case Mid(temp, 1, 1)
                            Case "1"
                                chbMonitoringContact.Checked = True
                            Case "2"
                                chbComplianceContact.Checked = True
                            Case "3"
                                chbPermittingContact.Checked = True
                            Case "4"
                                Select Case temp
                                    Case "40"
                                        chbFeeContact.Checked = True
                                    Case "41"
                                        chbGECOContact.Checked = True
                                    Case "42"
                                        chbEIContact.Checked = True
                                    Case "43"
                                        chbESContact.Checked = True
                                End Select
                            Case "5"
                                chbAmbientContact.Checked = True
                            Case "6"
                                chbPlanningContact.Checked = True
                            Case "7"
                                chbDistrictContact.Checked = True
                        End Select
                    End While
                    dr.Close()
                Else
                    chbMonitoringContact.Checked = False
                    chbComplianceContact.Checked = False
                    chbPermittingContact.Checked = False
                    chbFeeContact.Checked = False
                    chbGECOContact.Checked = False
                    chbEIContact.Checked = False
                    chbESContact.Checked = False
                    chbAmbientContact.Checked = False
                    chbPlanningContact.Checked = False
                    chbDistrictContact.Checked = False
                End If
            Else
                chbMonitoringContact.Checked = False
                chbComplianceContact.Checked = False
                chbPermittingContact.Checked = False
                chbFeeContact.Checked = False
                chbGECOContact.Checked = False
                chbEIContact.Checked = False
                chbESContact.Checked = False
                chbAmbientContact.Checked = False
                chbPlanningContact.Checked = False
                chbDistrictContact.Checked = False
            End If

            txtAssociatedFacilites.Clear()
            SQL = "Select distinct " & _
            "substr(" & DBNameSpace & ".APBContacts.strAIRSnumber,5) as AIRSNumber, " & _
            "strFacilityName " & _
            "from " & DBNameSpace & ".APBContacts, " & DBNameSpace & ".APBFacilityInformation " & _
            "where " & DBNameSpace & ".APBContacts.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSnumber " & _
            "and numContactID = '" & txtContactId.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                temp = ""
                If IsDBNull(dr.Item("AIRSNumber")) Then
                    temp = ""
                Else
                    temp = dr.Item("AIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    temp = temp
                Else
                    temp = temp & " - " & dr.Item("strFacilityName")
                End If
                If temp <> "" Then
                    txtAssociatedFacilites.Text = txtAssociatedFacilites.Text & temp & vbCrLf
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveContactInformation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveContactInformation.Click
        Try
            If txtContactId.Text <> "" Then
                SQL = "Update " & DBNameSpace & ".APBContactData set " & _
                "strFirstName = '" & Replace(txtContactFirstName.Text, "'", "''") & "', " & _
                "strLastname = '" & Replace(txtContactLastName.Text, "'", "''") & "', " & _
                "strPrefix = '" & Replace(txtContactSocialTitle.Text, "'", "''") & "', " & _
                "strSuffix = '" & Replace(txtContactPedigree.Text, "'", "''") & "', " & _
                "strTitle = '" & Replace(txtContactTitle.Text, "'", "''") & "', " & _
                "strCompanyName = '" & Replace(txtContactCompanyName.Text, "'", "''") & "', " & _
                "strPhoneNumber1 = '" & Replace(mtbPhoneNumber.Text, "'", "''") & "', " & _
                "strPhoneNumber2 = '" & Replace(mtbPhoneNumber2.Text, "'", "''") & "', " & _
                "strFaxNumber = '" & Replace(mtbFaxNumber.Text, "'", "''") & "', " & _
                "strEmail = '" & Replace(txtEmailAddress.Text, "'", "''") & "', " & _
                "strAddress = '" & Replace(txtContactAddress.Text, "'", "''") & "', " & _
                "strCity = '" & Replace(txtContactCity.Text, "'", "''") & "', " & _
                "strState = '" & Replace(txtContactState.Text, "'", "''") & "', " & _
                "strZipCode = '" & Replace(txtContactZipCode.Text, "'", "''") & "', " & _
                "strDescription = '" & Replace(txtContactNotes.Text, "'", "''") & "', " & _
                "numLastModified = '" & UserGCode & "', " & _
                "datLastModified = '" & OracleDate & "' " & _
                "where numContactID = '" & txtContactId.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SaveContactTypes()

                MsgBox("Contact Data Updated.", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Please select an existing contact first", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class