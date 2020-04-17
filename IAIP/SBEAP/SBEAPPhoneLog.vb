Imports System.Data.SqlClient

Public Class SBEAPPhoneLog

#Region " Page Load "

    Private Sub SBEAPPhoneLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        pnlClientInfo.Visible = False
        pnlNewClient.Visible = False
        pnlExistingClient.Visible = False
        pnlDetails.Visible = False

        ClearForm()
        LoadComboBoxes()

        cboStaffResponsible.SelectedValue = CurrentUser.UserID
    End Sub

    Private Sub ClearForm()
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
        DTPCaseOpened.Value = Today
        DTPCaseClosed.Value = Today
        chbOnetimeAssist.Checked = False
        DTPCaseClosed.Checked = False
        txtCaseSummary.Clear()
        txtReferralInformation.Clear()
        txtPhoneCallNotes.Clear()
    End Sub

    Private Sub LoadComboBoxes()
        Try
            Dim query As String = "select '' as NumUserID, '' as UserName " &
            "union select " &
            "convert(varchar(max),NumUserID), " &
            "concat(strLastName,', ',strFirstName) as UserName " &
            "from EPDUserProfiles " &
            "where numBranch = '5' " &
            "and numProgram = '35' " &
            "union " &
            "select " &
            "convert(varchar(max),NumUserID) as NumUserID, " &
            "concat(strLastName,', ',strFirstName) as UserName " &
            "from EPDUserProfiles, SBEAPCaseLog " &
            "where EPDUserProfiles.numUserID = SBEAPCaseLog.numStaffResponsible " &
            "Order by UserName "

            With cboStaffResponsible
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "UserName"
                .ValueMember = "NumUserID"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub LoadClientInfo()
        Try
            Dim ClientID As String = ""
            Dim CompanyName As String = ""
            Dim CompanyAddress As String = ""
            Dim County As String = ""

            Dim query As String = "select " &
            "clientID, " &
            "strCompanyName, " &
            "strCompanyAddress, " &
            "strCompanyCity, " &
            "strCompanyState, " &
            "strCompanyZipCode, " &
            "strCountyName " &
            "from SBEAPClients left join LookUpCountyInformation " &
            "on SBEAPClients.strCompanyCounty = LookUpCountyInformation.strCountyCode " &
            "where ClientId = @ClientId "

            Dim dr As DataRow = DB.GetDataRow(query, New SqlParameter("@ClientId", txtClientID.Text))

            If dr IsNot Nothing Then
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
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyAddress") & vbCrLf
                End If
                If IsDBNull(dr.Item("strCompanyCity")) Then
                Else
                    CompanyAddress = CompanyAddress & dr.Item("strCompanyCity")
                End If
                If IsDBNull(dr.Item("strCompanyState")) Then
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
            End If

            txtClientInformation.Text = ClientID & CompanyName & CompanyAddress & County

            query = "select " &
            "count(*) as Outstanding " &
            "from SBEAPCaseLog " &
            "where datCaseClosed is null " &
            "and ClientId = @ClientId "

            Dim dr2 As DataRow = DB.GetDataRow(query, New SqlParameter("@ClientId", txtClientID.Text))

            If dr2 IsNot Nothing Then
                If IsDBNull(dr2.Item("Outstanding")) Then
                    txtOutstandingCases.Text = "0"
                Else
                    txtOutstandingCases.Text = dr2.Item("Outstanding")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SavePhoneLog()
        Try
            Dim Result As DialogResult = DialogResult.Yes
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
                Result = MessageBox.Show("Are you sure you want to update this Case?",
                                 "Case Update", MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            End If
            If Result = DialogResult.Yes Then
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

                Dim query As String
                Dim query2 As String

                If txtCaseID.Text = "" Then
                    query = "INSERT INTO sbeapcaselog
                        ( NUMCASEID
                        , NUMSTAFFRESPONSIBLE
                        , DATCASEOPENED
                        , STRCASESUMMARY
                        , CLIENTID
                        , DATCASECLOSED
                        , NUMMODIFINGSTAFF
                        , DATMODIFINGDATE
                        , STRREFERRALCOMMENTS
                        )
                        VALUES
                        ( (Select case when (select max(numCaseID) from SBEAPCaseLog) is Null then 1 
                          Else (Select max(numCaseID) + 1 from SBEAPCaseLog) End CaseID)
                        , @NUMSTAFFRESPONSIBLE
                        , @DATCASEOPENED
                        , @STRCASESUMMARY
                        , @CLIENTID
                        , @DATCASECLOSED
                        , @NUMMODIFINGSTAFF
                        , getdate()
                        , @STRREFERRALCOMMENTS
                        )"

                    query2 = "Select max(numCaseID) as CaseID from SBEAPCaseLog "
                Else
                    query = "Update SBEAPCaseLog set " &
                    "numStaffResponsible = @NUMSTAFFRESPONSIBLE, " &
                    "datCaseOpened = @DATCASEOPENED, " &
                    "strCaseSummary = @STRCASESUMMARY, " &
                    "ClientID = @CLIENTID, " &
                    "datCaseClosed = @DATCASECLOSED, " &
                    "numModifingStaff = @NUMMODIFINGSTAFF, " &
                    "strReferralComments = @STRREFERRALCOMMENTS, " &
                    "datModifingDate =  GETDATE() " &
                    "where numCaseID = @NUMCASEID "

                    query2 = ""
                End If

                Dim p As SqlParameter() = {
                    New SqlParameter("@NUMCASEID", txtCaseID.Text),
                    New SqlParameter("@NUMSTAFFRESPONSIBLE", Staff),
                    New SqlParameter("@DATCASEOPENED", DTPCaseOpened.Text),
                    New SqlParameter("@STRCASESUMMARY", txtCaseSummary.Text),
                    New SqlParameter("@CLIENTID", ClientID),
                    New SqlParameter("@DATCASECLOSED", CloseDate),
                    New SqlParameter("@NUMMODIFINGSTAFF", CurrentUser.UserID),
                    New SqlParameter("@STRREFERRALCOMMENTS", ReferralInformation)
                }

                DB.RunCommand(query, p)

                If query2 <> "" Then
                    txtCaseID.Text = DB.GetString(query2)
                End If

                If txtActionID.Text = "" Then
                    query = "select " &
                    "case " &
                    "when (select max(numActionID) from SBEAPActionLog) is Null then 1 " &
                    "else (select max(numActionID) + 1 from SBEAPActionLog)   " &
                    "end ActionNumber "
                    Dim dr As DataRow = DB.GetDataRow(query)
                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("ActionNumber")) Then
                            txtActionID.Text = "1"
                        Else
                            txtActionID.Text = dr.Item("ActionNumber")
                        End If
                    Else
                        txtActionID.Text = "1"
                    End If

                    query = "INSERT INTO SBEAPACTIONLOG
                        ( NUMACTIONID
                        , NUMCASEID
                        , NUMACTIONTYPE
                        , NUMMODIFINGSTAFF
                        , DATMODIFINGDATE
                        , STRCREATINGSTAFF
                        , DATCREATIONDATE
                        , DATACTIONOCCURED
                        )
                        VALUES
                        ( @NUMACTIONID
                        , @NUMCASEID
                        , @NUMACTIONTYPE
                        , @NUMMODIFINGSTAFF
                        , getdate()
                        , @STRCREATINGSTAFF
                        , getdate()
                        , getdate()
                        )"

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@NUMACTIONID", txtActionID.Text),
                        New SqlParameter("@NUMCASEID", txtCaseID.Text),
                        New SqlParameter("@NUMACTIONTYPE", 6),
                        New SqlParameter("@NUMMODIFINGSTAFF", CurrentUser.UserID),
                        New SqlParameter("@STRCREATINGSTAFF", CurrentUser.UserID)
                    }

                    DB.RunCommand(query, p2)

                    query = "INSERT INTO SBEAPPHONELOG
                        ( NUMACTIONID
                        , STRCALLERINFORMATION
                        , NUMCALLERPHONENUMBER
                        , STRPHONELOGNOTES
                        , STRONETIMEASSIST
                        , STRFRONTDESKCALL
                        , STRMODIFINGSTAFF
                        , DATMODIFINGDATE
                        )
                        VALUES
                        ( @NUMACTIONID
                        , @STRCALLERINFORMATION
                        , @NUMCALLERPHONENUMBER
                        , @STRPHONELOGNOTES
                        , @STRONETIMEASSIST
                        , @STRFRONTDESKCALL
                        , @STRMODIFINGSTAFF
                        , getdate()
                        )"
                Else
                    query = "Update SBEAPPhoneLog set " &
                        "strCallerInformation = @STRCALLERINFORMATION, " &
                        "numCallerPhoneNumber = @NUMCALLERPHONENUMBER, " &
                        "strPhoneLogNotes = @STRPHONELOGNOTES, " &
                        "strOneTimeAssist = @STRONETIMEASSIST, " &
                        "strFrontDeskCall = @STRFRONTDESKCALL, " &
                        "strModifingStaff = @STRMODIFINGSTAFF, " &
                        "datModifingDate =  GETDATE()  " &
                        "where numActionID = @NUMACTIONID "
                End If

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@NUMACTIONID", txtActionID.Text),
                    New SqlParameter("@STRCALLERINFORMATION", CallerInfo),
                    New SqlParameter("@NUMCALLERPHONENUMBER", CallerPhone),
                    New SqlParameter("@STRPHONELOGNOTES", PhoneCallNotes),
                    New SqlParameter("@STRONETIMEASSIST", OneTimeAssist),
                    New SqlParameter("@STRFRONTDESKCALL", FrontDeskCall),
                    New SqlParameter("@STRMODIFINGSTAFF", CurrentUser.UserID)
                }

                DB.RunCommand(query, p3)

                MsgBox("Data Saved", MsgBoxStyle.Information, "Phone Log")
            Else
                MsgBox("Data Not Updated.", MsgBoxStyle.Information, "Phone Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbExistingClient_CheckedChanged(sender As Object, e As EventArgs) Handles rdbExistingClient.CheckedChanged
        Try
            pnlClientInfo.Visible = True
            pnlExistingClient.Visible = True

            pnlNewClient.Visible = False
            pnlDetails.Visible = True
            pnlDetails.BringToFront()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub rdbNewClient_CheckedChanged(sender As Object, e As EventArgs) Handles rdbNewClient.CheckedChanged
        Try
            pnlClientInfo.Visible = True
            pnlNewClient.Visible = True

            pnlExistingClient.Visible = False
            pnlDetails.Visible = True
            pnlDetails.BringToFront()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnNewCall_Click(sender As Object, e As EventArgs) Handles btnNewCall.Click
        Try
            rdbNewClient.Checked = False
            rdbExistingClient.Checked = False
            ClearForm()

            pnlDetails.Visible = False
            pnlClientInfo.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshClient_Click(sender As Object, e As EventArgs) Handles btnRefreshClient.Click
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
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnCreateNewClient_Click(sender As Object, e As EventArgs) Handles btnCreateNewClient.Click
        Try
            If ClientSummary IsNot Nothing Then
                ClientSummary.Dispose()
            End If

            ClientSummary = New SBEAPClientSummary

            If ClientSummary IsNot Nothing AndAlso Not ClientSummary.IsDisposed Then
                ClientSummary.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        SavePhoneLog()
    End Sub

    Private Sub tsbClientSearch_Click(sender As Object, e As EventArgs) Handles tsbClientSearch.Click
        Using clientSearchDialog As New SBEAPClientSearchTool
            If clientSearchDialog.ShowDialog() = DialogResult.OK Then
                txtClientID.Text = clientSearchDialog.SelectedClientID
                LoadClientInfo()
            End If
        End Using
    End Sub

    Private Sub chbOnetimeAssist_CheckStateChanged(sender As Object, e As EventArgs) Handles chbOnetimeAssist.CheckStateChanged
        If chbOnetimeAssist.Checked = True Then
            btnCreateNewClient.Visible = False
        Else
            btnCreateNewClient.Visible = True
        End If
    End Sub

End Class