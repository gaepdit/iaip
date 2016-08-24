Imports System.Data.SqlClient


Public Class ISMPNotificationLog
    Dim SQL, SQL2 As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim recExist As Boolean
    Dim dsFacilityData As DataSet
    Dim daFacilityData As SqlDataAdapter
    Dim dsStaffResponsible As DataSet
    Dim daStaffResponsible As SqlDataAdapter

    Private Sub DevNotificationLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = CurrentUser.AlphaName
            Panel3.Text = OracleDate

            LoadComboBoxes()
            If txtTestNotificationNumber.Text <> "" Then
                LoadTestNotification()
            End If
            DTPTestPlanReceived.Text = OracleDate
            DTPTestNotification.Text = OracleDate
            DTPTestDateStart.Text = OracleDate
            DTPTestDateEnd.Text = OracleDate

            If AccountFormAccess(66, 2) <> "1" And AccountFormAccess(66, 3) <> "1" Then
                bbtSave.Visible = False
                SaveToolStripMenuItem.Visible = False
                btnNewTestNotification.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Page Load"
    Sub LoadComboBoxes()
        Try
            Dim dtFacilityData As New DataTable
            Dim dtStaffResponsible As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "select " &
            "substr(strAIRSNumber, 5) as AIRSNumber, " &
            "strFacilityname " &
            "from APBFacilityInformation " &
            "order by strFacilityname "

            SQL2 = "select " &
            "distinct(strLastName|| ', ' ||strFirstName) as UserName,  " &
            "epduserprofiles.numUserID  " &
            "from EPDUserProfiles, ISMPTestNotification  " &
            "where (numProgram = '3' and numunit <> '14')  " &
            "or ISMPTestNotification.strStaffResponsible = to_char(EPDUSerProfiles.numUserID) " &
            "order by UserName "

            dsFacilityData = New DataSet
            daFacilityData = New SqlDataAdapter(SQL, CurrentConnection)

            dsStaffResponsible = New DataSet
            daStaffResponsible = New SqlDataAdapter(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daFacilityData.Fill(dsFacilityData, "FacilityData")
            daStaffResponsible.Fill(dsStaffResponsible, "StaffResponsible")

            dtFacilityData.Columns.Add("strFacilityName", GetType(System.String))
            dtFacilityData.Columns.Add("AIRSNumber", GetType(System.String))

            drNewRow = dtFacilityData.NewRow()
            drNewRow("strFacilityName") = " "
            drNewRow("AIRSNumber") = ""
            dtFacilityData.Rows.Add(drNewRow)

            For Each drDSRow In dsFacilityData.Tables("FacilityData").Rows()
                drNewRow = dtFacilityData.NewRow()
                drNewRow("strFacilityName") = drDSRow("strFacilityName")
                drNewRow("AIRSNumber") = drDSRow("AIRSNumber")
                dtFacilityData.Rows.Add(drNewRow)
            Next

            With cboAIRSNumber
                .DataSource = dtFacilityData
                .DisplayMember = "AIRSNumber"
                .ValueMember = "AIRSNumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName
                .DataSource = dtFacilityData
                .DisplayMember = "strFacilityName"
                .ValueMember = "AIRSNumber"
                .SelectedIndex = 0
            End With

            dtStaffResponsible.Columns.Add("UserName", GetType(System.String))
            dtStaffResponsible.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtStaffResponsible.NewRow()
            drNewRow("UserName") = " "
            drNewRow("numUserID") = ""
            dtStaffResponsible.Rows.Add(drNewRow)

            For Each drDSRow In dsStaffResponsible.Tables("StaffResponsible").Rows()
                drNewRow = dtStaffResponsible.NewRow()
                drNewRow("UserName") = drDSRow("UserName")
                drNewRow("numUserID") = drDSRow("numUserID")
                dtStaffResponsible.Rows.Add(drNewRow)
            Next

            With cboStaffResponsible
                .DataSource = dtStaffResponsible
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            cboStaffResponsible.SelectedValue = CurrentUser.UserID

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)

        End Try
    End Sub
    Sub LoadTestNotification()
        Try
            Dim Confirm As String = ""
            Dim City As String = ""
            Dim ZipCode As String = ""

            If txtTestNotificationNumber.Text <> "" Then
                SQL = "select " &
                "substr(ISMPTestNotification.strAIRSNumber, 5) as AIRSNumber,  " &
                "strFacilityStreet1, " &
                "strFacilityCity, strFacilityZipCode,  " &
                "strEmissionUnit,  " &
                "datProposedStartDate, datProposedEndDate,  " &
                "ISMPTestNotification.strComments,  " &
                "strTestPlanAvailable, strTimelyNotification,  " &
                "strInternalComments,  " &
                "strStaffresponsible,  " &
                "strOnlineFirstName, strOnlineLastName,  " &
                "strContactEmail, numUserID,  " &
                "strConfirmationNumber,  " &
                "strTelePhone, strFax, " &
                "datTestPlanReceived, datTestNotification, " &
                "strPollutants " &
                "from ISMPTestNotification, APBFacilityInformation  " &
                "where ISMPTestNotification.strAIRSNumber = APBFacilityInformation.strAIRSNumber (+)  " &
                "and ISMPTestNotification.strTestLogNumber = '" & txtTestNotificationNumber.Text & "'  "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("AIRSNumber")) Then
                        cboAIRSNumber.Text = " "
                    Else
                        cboAIRSNumber.Text = dr.Item("AIRSnumber")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        lblStreetAddress.Text = ""
                    Else
                        lblStreetAddress.Text = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        City = ""
                    Else
                        City = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        ZipCode = ""
                    Else
                        ZipCode = dr.Item("strFacilityZipCode")
                    End If
                    lblCityStateZipCode.Text = City & " GA, " & ZipCode

                    If IsDBNull(dr.Item("strEmissionUnit")) Then
                        txtEmissionUnit.Text = ""
                    Else
                        txtEmissionUnit.Text = dr.Item("strEmissionUnit")
                    End If
                    If IsDBNull(dr.Item("datProposedStartDate")) Then
                        DTPTestDateStart.Text = OracleDate
                    Else
                        DTPTestDateStart.Text = dr.Item("datProposedStartDate")
                    End If
                    If IsDBNull(dr.Item("datProposedEndDate")) Then
                        DTPTestDateEnd.Text = OracleDate
                    Else
                        DTPTestDateEnd.Text = dr.Item("datProposedEndDate")
                    End If
                    If IsDBNull(dr.Item("strComments")) Then
                        txtNotificationComments.Text = ""
                    Else
                        txtNotificationComments.Text = dr.Item("strComments")
                    End If
                    If IsDBNull(dr.Item("strTestPlanAvailable")) Then
                        rdbTestPlanAvailable.Checked = False
                        rdbTestPlanNotAvailable.Checked = False
                    Else
                        If dr.Item("strTestPlanAvailable") = "True" Then
                            rdbTestPlanAvailable.Checked = True
                        Else
                            rdbTestPlanNotAvailable.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("datTestPlanReceived")) Then
                        DTPTestPlanReceived.Text = OracleDate
                    Else
                        DTPTestPlanReceived.Text = dr.Item("datTestPlanReceived")
                    End If
                    If IsDBNull(dr.Item("strConfirmationNumber")) Then
                        Confirm = OracleDate
                        DTPTestNotification.Text = Confirm
                    Else
                        Confirm = dr.Item("strConfirmationNumber")
                        Select Case Confirm.Length
                            Case 14
                                Confirm = Mid(Confirm, 6)
                            Case 23
                                Confirm = Mid(Confirm, Confirm.Length - 14, Confirm.Length - 14)
                            Case 22
                                Confirm = Mid(Confirm, Confirm.Length - 14, Confirm.Length - 13)
                            Case Else
                                Confirm = OracleDate
                        End Select
                        'If Confirm.Length >= 9 Then
                        '    If Confirm.Length > 13 And Confirm.Length > 15 Then
                        '        Confirm = Mid(Confirm, Confirm.Length - 14, Confirm.Length - 14)
                        '    Else
                        '        Confirm = Mid(Confirm, Confirm.Length - 8)
                        '    End If
                        'End If
                        DTPTestNotification.Text = Confirm
                    End If

                    If IsDBNull(dr.Item("strTimelyNotification")) Then
                        rdbTimelyNotification.Checked = False
                        rdbNoTimelyNotification.Checked = False
                    Else
                        If dr.Item("strTimelyNotification") = "True" Then
                            rdbTimelyNotification.Checked = True
                        Else
                            rdbNoTimelyNotification.Checked = True
                        End If
                    End If
                    If IsDBNull(dr.Item("datTestNotification")) Then
                        DTPTestNotification.Text = OracleDate
                    Else
                        DTPTestNotification.Text = dr.Item("datTestNotification")
                    End If

                    If IsDBNull(dr.Item("strInternalcomments")) Then
                        txtISMPComments.Text = ""
                    Else
                        txtISMPComments.Text = dr.Item("strInternalComments")
                    End If
                    If IsDBNull(dr.Item("strOnlineFirstName")) Then
                        txtContactFirstName.Text = ""
                    Else
                        txtContactFirstName.Text = dr.Item("strOnlineFirstName")
                    End If
                    If IsDBNull(dr.Item("strOnlineLastName")) Then
                        txtContactLastName.Text = ""
                    Else
                        txtContactLastName.Text = dr.Item("strOnlineLastName")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtContactEmailAddress.Text = ""
                    Else
                        txtContactEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("numUserID")) Then
                        chbWebContact.Checked = False
                    Else
                        chbWebContact.Checked = True
                    End If
                    If IsDBNull(dr.Item("strTelePhone")) Then
                        mtbPhoneNumber.Clear()
                    Else
                        mtbPhoneNumber.Text = dr.Item("strTelePhone")
                    End If
                    If IsDBNull(dr.Item("strFax")) Then
                        mtbFaxNumber.Clear()
                    Else
                        mtbFaxNumber.Text = dr.Item("strFax")
                    End If

                    If IsDBNull(dr.Item("strStaffResponsible")) Then
                        cboStaffResponsible.SelectedValue = CurrentUser.UserID
                    Else
                        cboStaffResponsible.SelectedValue = dr.Item("strStaffResponsible")
                    End If
                    If IsDBNull(dr.Item("strPollutants")) Then
                        txtPollutants.Text = ""
                    Else
                        txtPollutants.Text = dr.Item("strPollutants")
                    End If
                    dr.Close()
                    LoadReferenceNumbers()
                Else

                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try



    End Sub
    Sub LoadReferenceNumbers()
        Try

            txtReferenceNumber.Clear()
            SQL = "Select " &
            "strReferenceNumber " &
            "from ISMPTestLogLink " &
            "where strTestLogNumber = '" & txtTestNotificationNumber.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strReferenceNumber")) Then
                Else
                    txtReferenceNumber.Text = txtReferenceNumber.Text & dr.Item("strReferenceNumber") & vbCrLf
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub SaveNotification()
        Try
            If AccountFormAccess(66, 2) = "1" Or AccountFormAccess(66, 3) = "1" Then
                Dim UserIDNum As String = ""
                Dim TestPlan As String = ""
                Dim TimelyNotification As String = ""
                Dim TestPlanRec As String = ""
                Dim TestNotificationDate As String = ""

                SQL = "select strTestLogNumber " &
                "from ISMPTestNotification " &
                "where strTestLogNumber = '" & txtTestNotificationNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If chbWebContact.Checked = True Then
                    UserIDNum = " "
                Else
                    UserIDNum = "numUserId = '', "
                End If
                If rdbTestPlanAvailable.Checked = True Then
                    TestPlan = "True"
                    TestPlanRec = DTPTestPlanReceived.Text
                Else
                    If rdbTestPlanNotAvailable.Checked = True Then
                        TestPlan = "False"
                        TestPlanRec = ""
                    Else
                        TestPlan = ""
                        TestPlanRec = ""
                    End If
                End If
                If rdbTimelyNotification.Checked = True Then
                    TimelyNotification = "True"
                    TestNotificationDate = Me.DTPTestNotification.Text
                Else
                    If Me.rdbNoTimelyNotification.Checked = True Then
                        TimelyNotification = "False"
                        TestNotificationDate = DTPTestNotification.Text
                    Else
                        TimelyNotification = ""
                        TestNotificationDate = Me.DTPTestNotification.Text
                    End If
                End If

                If recExist = True Then
                    'Update 
                    SQL = "Update ISMPTestNotification set " &
                    "strEmissionUnit = '" & Replace(txtEmissionUnit.Text, "'", "''") & "', " &
                    "datProposedStartDate = '" & Me.DTPTestDateStart.Text & "', " &
                    "datProposedEndDate = '" & DTPTestDateEnd.Text & "', " &
                    "strComments = '" & Replace(Me.txtNotificationComments.Text, "'", "''") & "', " &
                    UserIDNum &
                    "strContactEmail = '" & Replace(txtContactEmailAddress.Text, "'", "''") & "', " &
                    "strAIRSNumber = '0413" & cboAIRSNumber.SelectedValue & "', " &
                    "strStaffresponsible  = '" & cboStaffResponsible.SelectedValue & "', " &
                    "strTestPlanAvailable = '" & TestPlan & "', " &
                    "strTimelyNotification = '" & TimelyNotification & "', " &
                    "strOnlineFirstName = '" & Me.txtContactFirstName.Text & "', " &
                    "strOnlineLastName = '" & txtContactLastName.Text & "', " &
                    "strInternalComments = '" & Replace(txtISMPComments.Text, "'", "''") & "', " &
                    "strmodifingstaff = '" & CurrentUser.UserID & "', " &
                    "datModifingDate = '" & OracleDate & "', " &
                    "datTestPlanReceived = '" & TestPlanRec & "', " &
                    "datTestNotification = '" & TestNotificationDate & "', " &
                    "strTelephone = '" & mtbPhoneNumber.Text & "', " &
                    "strFax = '" & mtbFaxNumber.Text & "', " &
                    "strPollutants = '" & Replace(txtPollutants.Text, "'", "''") & "' " &
                    "where strtestlognumber = '" & txtTestNotificationNumber.Text & "' "
                Else
                    temp = ""
                    Dim testNum As String = ""
                    Dim StartDate As String = ""

                    SQL = "Select " &
                    "strTestLogNumber, datProposedStartDate " &
                    "from ISMPTestNotification " &
                    "where datProposedStartDate between " &
                    "'" & Format(DTPTestDateStart.Value.AddDays(-15), "dd-MMM-yyyy") & "' " &
                    "and '" & Format(DTPTestDateStart.Value.AddDays(15), "dd-MMM-yyyy") & "' " &
                    "and strAIRSNumber = '0413" & cboAIRSNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        If IsDBNull(dr.Item("strTestLogNumber")) Then
                            temp = ""
                        Else
                            temp = dr.Item("strTestLogNumber")
                            testNum = temp
                            StartDate = Format(dr.Item("datProposedStartDate"), "dd-MMM-yyyy")
                        End If
                    End If
                    dr.Close()

                    Dim result As String = "False"
                    If temp <> "" Then
                        result = MessageBox.Show("There is currently a Test Notification (" & temp & ") " &
                             "that is close to the date of this test notification." & vbCrLf &
                             "Notification # - " & testNum & "       Start Date - " & StartDate & vbCrLf &
                             "Do you still want to create this Test Notification?", "Test Notification Warning",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        Select Case result
                            Case DialogResult.No
                                Exit Sub
                            Case "7"
                                Exit Sub
                            Case Else

                        End Select
                    End If

                    'Insert 
                    SQL = "Insert into ISMPTestNotification " &
                    "(strTestLogNumber, " &
                    "strEmissionUnit, " &
                    "datProposedStartDate, datProposedEndDate, " &
                    "strComments, numUserID, " &
                    "strContactEmail, strConfirmationNumber, " &
                    "strOnlineFirstname, strOnlineLastName, " &
                    "strAIRSNumber, strStaffResponsible, " &
                    "strTestPlanAvailable, strTimelyNotification, " &
                    "strInternalComments, strModifingStaff, " &
                    "datModifingDate, strTelePhone, " &
                    "strFax, datTestPlanReceived, " &
                    "datTestNotification, strPollutants) " &
                    "values " &
                    "('" & txtTestNotificationNumber.Text & "', " &
                    "'" & Replace(txtEmissionUnit.Text, "'", "''") & "', " &
                    "'" & DTPTestDateStart.Text & "', '" & DTPTestDateEnd.Text & "', " &
                    "'" & Replace(txtNotificationComments.Text, "'", "''") & "', '', " &
                    "'" & Replace(txtContactEmailAddress.Text, "'", "''") & "', '', " &
                    "'" & Replace(txtContactFirstName.Text, "'", "''") & "', " &
                    "'" & Replace(txtContactLastName.Text, "'", "''") & "', " &
                    "'0413" & cboAIRSNumber.SelectedValue & "', " &
                    "'" & cboStaffResponsible.SelectedValue & "', " &
                    "'" & TestPlan & "', '" & TimelyNotification & "', " &
                    "'" & Replace(txtISMPComments.Text, "'", "''") & "', " &
                    "'" & CurrentUser.UserID & "', '" & OracleDate & "', " &
                    "'" & mtbPhoneNumber.Text & "', '" & mtbFaxNumber.Text & "', " &
                    "'" & TestNotificationDate & "', '" & TestNotificationDate & "', " &
                    "'" & Replace(txtPollutants.Text, "'", "''") & "') "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                MsgBox("Information Saved.", MsgBoxStyle.Information, "Notification Log")
            Else
                MsgBox("You do not have sufficent permissions to save.", MsgBoxStyle.Information, "Notification Log")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)

        End Try
    End Sub
    Sub SelectNewTestNotifcationNumber()
        Try
            SQL = "Select max(to_number(strTestLogNumber)) + 1 as TestNum " &
            "From ISMPTestnotification "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("TestNum")) Then
                    txtTestNotificationNumber.Text = "1"
                Else
                    txtTestNotificationNumber.Text = dr.Item("TestNum")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Declaration"
    Private Sub DevNotificationLog_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPNotificationLogForm = Nothing
            Me.Dispose()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub bbtSave_Click(sender As Object, e As EventArgs) Handles bbtSave.Click
        Try
            If txtTestNotificationNumber.Text <> "" Then
                SaveNotification()
            Else
                MsgBox("First Select New Test Notification.", MsgBoxStyle.Exclamation, "Notification Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbBack_Click(sender As Object, e As EventArgs) Handles tsbBack.Click
        Try

            ISMPNotificationLogForm = Nothing
            Me.Dispose()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnNewTestNotification_Click(sender As Object, e As EventArgs) Handles btnNewTestNotification.Click
        Try
            SelectNewTestNotifcationNumber()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If txtTestNotificationNumber.Text <> "" Then
                SaveNotification()
            Else
                MsgBox("First Select New Test Notification.", MsgBoxStyle.Exclamation, "Notification Log")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Try

            ISMPNotificationLogForm = Nothing
            Me.Dispose()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub rdbTestPlanAvailable_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTestPlanAvailable.CheckedChanged
        Try
            If rdbTestPlanAvailable.Checked = True Then
                DTPTestPlanReceived.Visible = True
                lblReceivedDate.Visible = True
            Else
                DTPTestPlanReceived.Visible = False
                lblReceivedDate.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)

        End Try
    End Sub

#End Region




    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class