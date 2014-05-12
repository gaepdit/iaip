Imports Oracle.DataAccess.Client

Public Class SBEAPPhoneLog
    Dim SQL, SQL2 As String
    Dim dsStaff As DataSet
    Dim daStaff As OracleDataAdapter

    Public WriteOnly Property ValueFromClientLookUp() As String
        Set(ByVal Value As String)
            txtClientID.Text = Value
        End Set
    End Property

    Private Sub SBEAPPhoneLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            lbl1.Text = "Enter Phone Log Info..."
            lbl2.Text = UserName
            lbl3.Text = OracleDate

            pnlClientInfo.Visible = False
            pnlNewClient.Visible = False
            pnlExistingClient.Visible = False
            pnlDetails.Visible = False

            ClearForm()
            LoadComboBoxes()

            cboStaffResponsible.SelectedValue = UserGCode
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Page Load Functions"
    Sub ClearForm()
        Try
            txtCaseID.Clear()
            txtActionID.Clear()
            rdbExistingClient.Checked = False
            rdbNewClient.Checked = False
            txtClientID.Clear()
            txtClientInformation.Clear()
            txtOutstandingCases.Clear()
            txtCallName.Clear()
            txtDescription.Clear()
            mtbPhoneNumber.Clear()

            chbFrontDeskCall.Checked = False
            cboStaffResponsible.Text = ""
            DTPCaseOpened.Text = OracleDate
            DTPCaseClosed.Text = OracleDate
            chbOnetimeAssist.Checked = False
            DTPCaseClosed.Checked = False
            txtCaseSummary.Clear()
            txtReferralInformation.Clear()
            txtPhoneCallNotes.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadComboBoxes()
        Try
            Dim dtStaff As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dsStaff = New DataSet

            SQL = "select " & _
            "NumUserID, " & _
            "(strLastName||', '||strFirstName) as UserName " & _
            "from " & DBNameSpace & ".EPDUserProfiles " & _
            "where numBranch = '5' " & _
            "and numProgram = '35' " & _
            "union " & _
            "select " & _
            "distinct(NumUserID) as NumUserID, " & _
            "(strLastName||', '||strFirstName) as UserName " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".SBEAPCaseLog " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".SBEAPCaseLog.numStaffResponsible " & _
            "Order by UserName "

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daStaff.Fill(dsStaff, "Staff")

            dtStaff.Columns.Add("NumUserID", GetType(System.String))
            dtStaff.Columns.Add("UserName", GetType(System.String))

            drNewRow = dtStaff.NewRow
            drNewRow("NumUserID") = ""
            drNewRow("UserName") = ""
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow In dsStaff.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow
                drNewRow("NumUserID") = drDSRow("NumUserID")
                drNewRow("UserName") = drDSRow("UserName")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboStaffResponsible
                .DataSource = dtStaff
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub LoadClientInfo()
        Try
            Dim ClientID As String = ""
            Dim CompanyName As String = ""
            Dim CompanyAddress As String = ""
            Dim County As String = ""

            SQL = "select " & _
            "clientID, " & _
            "strCompanyName, " & _
            "strCompanyAddress, " & _
            "strCompanyCity, " & _
            "strCompanyState, " & _
            "strCompanyZipCode, " & _
            "strCountyName " & _
            "from " & DBNameSpace & ".SBEAPClients, " & DBNameSpace & ".LookUpCountyInformation " & _
            "where " & DBNameSpace & ".SBEAPClients.strCompanyCounty = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode (+) " & _
            "and ClientId = '" & txtClientID.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtClientID.BackColor = Color.White
                If IsDBNull(dr.Item("ClientID")) Then
                    ClientID = "Client ID: " & vbCrLf & vbCrLf
                Else
                    ClientID = "Client ID - " & dr.Item("ClientID") & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyName")) Then
                    CompanyName = ""
                Else
                    CompanyName = dr.Item("strCompanyName") & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyAddress")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyAddress") & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyCity")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyCity")
                End If
                If IsDBNull(dr.Item("strCompanyState")) Then
                    CompanyAddress = CompanyAddress
                Else
                    CompanyAddress = CompanyAddress & ", " & dr.Item("strCompanyState")
                End If
                If IsDBNull(dr.Item("strCompanyZipCode")) Then
                    CompanyAddress = CompanyAddress & vbCrLf & vbCrLf
                Else
                    CompanyAddress = CompanyAddress & " " & dr.Item("strCompanyZipCode") & vbCrLf & vbCrLf
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = "County- " & CompanyAddress
                Else
                    County = "County- " & dr.Item("strCountyName")
                End If
            End While
            dr.Close()
            txtClientInformation.Text = ClientID & CompanyName & CompanyAddress & County

            SQL = "select " & _
            "count(*) as Outstanding " & _
            "from " & DBNameSpace & ".SBEAPCaseLog " & _
            "where ClientID = '" & txtClientID.Text & "' " & _
            "and datCaseClosed is null"
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("Outstanding")) Then
                    txtOutstandingCases.Text = "0"
                Else
                    txtOutstandingCases.Text = dr.Item("Outstanding")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SavePhoneLog()
        Try
            Dim Result As DialogResult = Windows.Forms.DialogResult.Yes
            Dim Staff As String = ""
            Dim ClientID As String = ""
            Dim CloseDate As String = ""
            Dim CallerInfo As String = ""
            Dim CallerPhone As String = ""
            Dim ReferralInformation As String = ""
            Dim PhoneCallNotes As String = ""
            Dim OneTimeAssist As String = ""
            Dim FrontDeskCall As String = ""


            If txtCaseID.Text <> "" Then
                Result = MessageBox.Show("Are you sure you want to update this Case?", _
                                 "Case Update", MessageBoxButtons.YesNoCancel, _
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            End If
            If Result = Windows.Forms.DialogResult.Yes Then
                If cboStaffResponsible.Text <> "" Then
                    Staff = cboStaffResponsible.SelectedValue
                Else
                    Staff = ""
                End If
                If DTPCaseClosed.Checked = True Then
                    CloseDate = DTPCaseClosed.Text
                Else
                    CloseDate = ""
                End If
                If txtCallName.Text <> "" Then
                    CallerInfo = txtCallName.Text
                Else
                    CallerInfo = ""
                End If
                If mtbPhoneNumber.Text <> "" Then
                    CallerPhone = mtbPhoneNumber.Text
                Else
                    CallerPhone = ""
                End If
                If txtReferralInformation.Text <> "" Then
                    ReferralInformation = txtReferralInformation.Text
                Else
                    ReferralInformation = ""
                End If
                If txtPhoneCallNotes.Text <> "" Then
                    PhoneCallNotes = txtPhoneCallNotes.Text
                Else
                    PhoneCallNotes = ""
                End If
                If chbOnetimeAssist.Checked = True Then
                    OneTimeAssist = "True"
                Else
                    OneTimeAssist = "False"
                End If
                If chbFrontDeskCall.Checked = True Then
                    FrontDeskCall = "True"
                Else
                    FrontDeskCall = "False"
                End If


                If rdbExistingClient.Checked = True Then
                    If txtClientID.Text <> "" And txtClientInformation.Text <> "" Then
                        If txtClientID.Text <> "" Then
                            ClientID = txtClientID.Text
                        Else
                            ClientID = ""
                        End If
                    Else
                        MsgBox("Please enter valid Client ID and validate it.", MsgBoxStyle.Information, "Phone Log")
                    End If
                End If
                If rdbNewClient.Checked = True Then
                    ClientID = ""
                End If

                If txtCaseID.Text = "" Then
                    SQL = "Insert into " & DBNameSpace & ".SBEAPCaseLog " & _
                    "values " & _
                    "((Select " & _
                    "case " & _
                    "when (select max(numCaseID) from " & DBNameSpace & ".SBEAPCaseLog) is Null then 1 " & _
                    "else (select max(numCaseID) + 1 from " & DBNameSpace & ".SBEAPCaseLog) " & _
                    "End CaseID " & _
                    "from dual), " & _
                    "'" & Staff & "', '" & DTPCaseOpened.Text & "', " & _
                    "'" & Replace(txtCaseSummary.Text, "'", "''") & "', " & _
                    "'" & Replace(ClientID, "'", "''") & "', '" & CloseDate & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "', '', " & _
                    "'" & Replace(ReferralInformation, "'", "''") & "', '', '', '') "

                    SQL2 = "Select max(numCaseID) as CaseID from " & DBNameSpace & ".SBEAPCaseLog "
                Else
                    SQL = "Update " & DBNameSpace & ".SBEAPCaseLog set " & _
                    "numStaffResponsible = '" & Staff & "', " & _
                    "datCaseOpened = '" & DTPCaseOpened.Text & "', " & _
                    "strCaseSummary = '" & Replace(txtCaseSummary.Text, "'", "''") & "', " & _
                    "ClientID = '" & Replace(ClientID, "'", "''") & "', " & _
                    "datCaseClosed = '" & CloseDate & "', " & _
                    "numModifingStaff = '" & UserGCode & "', " & _
                    "datModifingDate = '" & OracleDate & "', " & _
                    "strReferralComments = '" & Replace(ReferralInformation, "'", "''") & "' " & _
                    "where numCaseID = '" & txtCaseID.Text & "' "

                    SQL2 = ""
                End If

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If SQL2 <> "" Then
                    cmd = New OracleCommand(SQL2, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("CaseID")) Then
                            txtCaseID.Text = ""
                        Else
                            txtCaseID.Text = dr.Item("CaseID")
                        End If
                    End While
                    dr.Close()
                End If

                If txtActionID.Text = "" Then
                    SQL = "select " & _
                    "case " & _
                    "when (select max(numActionID) from " & DBNameSpace & ".SBEAPActionLog) is Null then 1 " & _
                    "else (select max(numActionID) + 1 from " & DBNameSpace & ".SBEAPActionLog)   " & _
                    "end ActionNumber " & _
                    "from dual  "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("ActionNumber")) Then
                            txtActionID.Text = "1"
                        Else
                            txtActionID.Text = dr.Item("ActionNumber")
                        End If
                    End While
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".SBEAPActionLog " & _
                    "values " & _
                    "('" & txtActionID.Text & "', '" & txtCaseID.Text & "', " & _
                    "'6', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '" & UserGCode & "', " & _
                    "'" & OracleDate & "', '" & OracleDate & "') "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Insert into " & DBNameSpace & ".SBEAPPhoneLog " & _
                    "values " & _
                    "('" & txtActionID.Text & "', '" & Replace(CallerInfo, "'", "''") & "', " & _
                    "'" & Replace(CallerPhone, "'", "''") & "', " & _
                    "'" & Replace(PhoneCallNotes, "'", "''") & "', " & _
                    "'" & OneTimeAssist & "', '" & FrontDeskCall & "', " & _
                    "'" & UserGCode & "', '" & OracleDate & "') "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Else
                    SQL = "Update " & DBNameSpace & ".SBEAPPhoneLog set " & _
                    "strCallerInformation = '" & Replace(CallerInfo, "'", "''") & "', " & _
                    "numCallerPhoneNumber = '" & CallerPhone & "', " & _
                    "strPhoneLogNotes = '" & Replace(PhoneCallNotes, "'", "''") & "', " & _
                    "strOneTimeAssist = '" & OneTimeAssist & "', " & _
                    "strFrontDeskCall = '" & FrontDeskCall & "', " & _
                    "strModifingStaff = '" & UserGCode & "', " & _
                    "datModifingDate = '" & OracleDate & "' " & _
                    "where numActionID = '" & txtActionID.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                MsgBox("Data Saved", MsgBoxStyle.Information, "Phone Log")
            Else
                MsgBox("Data Not Updated.", MsgBoxStyle.Information, "Phone Log")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
    Private Sub rdbExistingClient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExistingClient.CheckedChanged
        Try
            pnlClientInfo.Visible = True
            pnlExistingClient.Visible = True

            pnlNewClient.Visible = False
            pnlDetails.Visible = True
            pnlDetails.BringToFront()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub rdbNewClient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNewClient.CheckedChanged
        Try
            pnlClientInfo.Visible = True
            pnlNewClient.Visible = True
            
            pnlExistingClient.Visible = False
            pnlDetails.Visible = True
            pnlDetails.BringToFront()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNewCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCall.Click
        Try
            rdbNewClient.Checked = False
            rdbExistingClient.Checked = False
            ClearForm()

            pnlDetails.Visible = False
            pnlClientInfo.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    
    Private Sub btnRefreshClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshClient.Click
        Try
            If txtClientID.Text <> "" Then
                LoadClientInfo()
                txtClientInformation.BackColor = Color.White
            Else
                txtClientID.BackColor = Color.Tomato
                txtClientInformation.Clear()
                txtOutstandingCases.Clear()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCreateNewClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNewClient.Click
        Try
            If ClientSummary Is Nothing Then

            Else
                ClientSummary.Dispose()
            End If
            ClientSummary = New SBEAPClientSummary
            ClientSummary.Show()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try
            SavePhoneLog()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbClientSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClientSearch.Click
        Try
            Dim clientSearchDialog As New SBEAPClientSearchTool
            clientSearchDialog.ShowDialog()
            If clientSearchDialog.DialogResult = Windows.Forms.DialogResult.OK Then
                Me.ValueFromClientLookUp = clientSearchDialog.SelectedClientID
                LoadClientInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub mmiClearCaseID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClearCaseID.Click
        Try
            txtCaseID.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub chbOnetimeAssist_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbOnetimeAssist.CheckStateChanged
        Try
            If chbOnetimeAssist.Checked = True Then
                btnCreateNewClient.Visible = False
            Else
                btnCreateNewClient.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class