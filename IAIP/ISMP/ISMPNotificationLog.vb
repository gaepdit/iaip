Imports System.Data.SqlClient

Public Class ISMPNotificationLog
    Dim query As String

    Private Sub ISMPNotificationLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LoadComboBoxes()

            DTPTestPlanReceived.Value = Today
            DTPTestNotification.Value = Today
            DTPTestDateStart.Value = Today
            DTPTestDateEnd.Value = Today

            If AccountFormAccess(66, 2) <> "1" AndAlso AccountFormAccess(66, 3) <> "1" Then
                bbtSave.Visible = False
                SaveToolStripMenuItem.Visible = False
                btnNewTestNotification.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ISMPNotificationLog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If txtTestNotificationNumber.Text <> "" Then
            LoadTestNotification()
        End If
    End Sub

    Private Sub LoadComboBoxes()
        Try
            Dim dtFacilityData As New DataTable
            Dim dtStaffResponsible As New DataTable

            query = "select " &
            "SUBSTRING(strAIRSNumber, 5,8) as AIRSNumber, " &
            "strFacilityname " &
            "from APBFacilityInformation " &
            "order by strFacilityname "

            dtFacilityData = DB.GetDataTable(query)

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

            query = "select " &
            "distinct concat(strLastName, ', ' ,strFirstName) as UserName,  " &
            "epduserprofiles.numUserID  " &
            "from EPDUserProfiles, ISMPTestNotification  " &
            "where (numProgram = '3' and numunit <> '14')  " &
            "or ISMPTestNotification.strStaffResponsible = convert(varchar(3),EPDUSerProfiles.numUserID) " &
            "order by UserName "

            dtStaffResponsible = DB.GetDataTable(query)

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
    Private Sub LoadTestNotification()
        Try
            Dim Confirm As String = ""
            Dim City As String = ""
            Dim ZipCode As String = ""

            If txtTestNotificationNumber.Text <> "" Then
                query = "select " &
                "SUBSTRING(ISMPTestNotification.strAIRSNumber, 5,8) as AIRSNumber,  " &
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
                "from ISMPTestNotification LEFT JOIN APBFacilityInformation  " &
                "on ISMPTestNotification.strAIRSNumber = APBFacilityInformation.strAIRSNumber " &
                "where ISMPTestNotification.strTestLogNumber = @log "

                Dim p As New SqlParameter("@log", txtTestNotificationNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
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
                        DTPTestDateStart.Value = Today
                    Else
                        DTPTestDateStart.Text = dr.Item("datProposedStartDate")
                    End If
                    If IsDBNull(dr.Item("datProposedEndDate")) Then
                        DTPTestDateEnd.Value = Today
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
                        DTPTestPlanReceived.Value = Today
                    Else
                        DTPTestPlanReceived.Text = dr.Item("datTestPlanReceived")
                    End If
                    If IsDBNull(dr.Item("strConfirmationNumber")) Then
                        Confirm = TodayFormatted
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
                                Confirm = TodayFormatted
                        End Select
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
                        DTPTestNotification.Value = Today
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
                    LoadReferenceNumbers()
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub LoadReferenceNumbers()
        Try

            txtReferenceNumber.Clear()

            query = "Select " &
            "strReferenceNumber " &
            "from ISMPTestLogLink " &
            "where strTestLogNumber = @log "

            Dim p As New SqlParameter("@log", txtTestNotificationNumber.Text)

            Dim dt As DataTable = DB.GetDataTable(query, p)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strReferenceNumber")) Then
                Else
                    txtReferenceNumber.Text = txtReferenceNumber.Text & dr.Item("strReferenceNumber") & vbCrLf
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveNotification()
        Try
            Dim temp As String
            If AccountFormAccess(66, 2) = "1" OrElse AccountFormAccess(66, 3) = "1" Then
                Dim UserIDNum As String = ""
                Dim TestPlan As String = ""
                Dim TimelyNotification As String = ""
                Dim TestPlanRec As Date?
                Dim TestNotificationDate As Date?

                query = "select strTestLogNumber " &
                "from ISMPTestNotification " &
                "where strTestLogNumber = @log"

                Dim p As New SqlParameter("@log", txtTestNotificationNumber.Text)

                If chbWebContact.Checked = True Then
                    UserIDNum = " "
                Else
                    UserIDNum = "numUserId = null, "
                End If
                If rdbTestPlanAvailable.Checked = True Then
                    TestPlan = "True"
                    TestPlanRec = DTPTestPlanReceived.Value
                Else
                    If rdbTestPlanNotAvailable.Checked = True Then
                        TestPlan = "False"
                        TestPlanRec = Nothing
                    Else
                        TestPlan = ""
                        TestPlanRec = Nothing
                    End If
                End If
                If rdbTimelyNotification.Checked = True Then
                    TimelyNotification = "True"
                    TestNotificationDate = DTPTestNotification.Value
                Else
                    If Me.rdbNoTimelyNotification.Checked = True Then
                        TimelyNotification = "False"
                        TestNotificationDate = DTPTestNotification.Value
                    Else
                        TimelyNotification = ""
                        TestNotificationDate = DTPTestNotification.Value
                    End If
                End If

                If DB.ValueExists(query, p) Then
                    'Update 
                    query = "Update ISMPTestNotification set " &
                    "strEmissionUnit = @strEmissionUnit, " &
                    "datProposedStartDate = @datProposedStartDate, " &
                    "datProposedEndDate = @datProposedEndDate, " &
                    "strComments = @strComments, " &
                    UserIDNum &
                    "strContactEmail = @strContactEmail, " &
                    "strAIRSNumber = @strAIRSNumber, " &
                    "strStaffresponsible  = @strStaffresponsible, " &
                    "strTestPlanAvailable = @strTestPlanAvailable, " &
                    "strTimelyNotification = @strTimelyNotification, " &
                    "strOnlineFirstName = @strOnlineFirstName, " &
                    "strOnlineLastName = @strOnlineLastName, " &
                    "strInternalComments = @strInternalComments, " &
                    "strmodifingstaff = @strmodifingstaff, " &
                    "datModifingDate = getdate(), " &
                    "datTestPlanReceived = @datTestPlanReceived, " &
                    "datTestNotification = @datTestNotification, " &
                    "strTelephone = @strTelephone, " &
                    "strFax = @strFax, " &
                    "strPollutants = @strPollutants " &
                    "where strtestlognumber = @strtestlognumber "

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@strEmissionUnit", txtEmissionUnit.Text),
                        New SqlParameter("@datProposedStartDate", DTPTestDateStart.Value),
                        New SqlParameter("@datProposedEndDate", DTPTestDateEnd.Value),
                        New SqlParameter("@strComments", txtNotificationComments.Text),
                        New SqlParameter("@strContactEmail", txtContactEmailAddress.Text),
                        New SqlParameter("@strAIRSNumber", "0413" & cboAIRSNumber.SelectedValue),
                        New SqlParameter("@strStaffresponsible", cboStaffResponsible.SelectedValue),
                        New SqlParameter("@strTestPlanAvailable", TestPlan),
                        New SqlParameter("@strTimelyNotification", TimelyNotification),
                        New SqlParameter("@strOnlineFirstName", txtContactFirstName.Text),
                        New SqlParameter("@strOnlineLastName", txtContactLastName.Text),
                        New SqlParameter("@strInternalComments", txtISMPComments.Text),
                        New SqlParameter("@strmodifingstaff", CurrentUser.UserID),
                        New SqlParameter("@datTestPlanReceived", TestPlanRec),
                        New SqlParameter("@datTestNotification", TestNotificationDate),
                        New SqlParameter("@strTelephone", mtbPhoneNumber.Text),
                        New SqlParameter("@strFax", mtbFaxNumber.Text),
                        New SqlParameter("@strPollutants", txtPollutants.Text),
                        New SqlParameter("@strtestlognumber", txtTestNotificationNumber.Text)
                    }

                    DB.RunCommand(query, p2)
                Else
                    temp = ""
                    Dim testNum As String = ""
                    Dim StartDate As String = ""

                    query = "Select " &
                    "strTestLogNumber, datProposedStartDate " &
                    "from ISMPTestNotification " &
                    "where datProposedStartDate between @startdate and @enddate " &
                    "and strAIRSNumber = @airs "

                    Dim p3 As SqlParameter() = {
                        New SqlParameter("@startdate", DTPTestDateStart.Value.AddDays(-15)),
                        New SqlParameter("@enddate", DTPTestDateStart.Value.AddDays(15)),
                        New SqlParameter("@airs", "0413" & cboAIRSNumber.Text)
                    }

                    Dim dt As DataTable = DB.GetDataTable(query, p3)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strTestLogNumber")) Then
                            temp = ""
                        Else
                            temp = dr.Item("strTestLogNumber")
                            testNum = temp
                            StartDate = Format(dr.Item("datProposedStartDate"), "dd-MMM-yyyy")
                        End If
                    Next

                    Dim result As String = "False"
                    If temp <> "" Then
                        result = MessageBox.Show("There is currently a Test Notification (" & temp & ") " &
                             "that is close to the date of this test notification." & vbCrLf &
                             "Notification # - " & testNum & "       Start Date - " & StartDate & vbCrLf &
                             "Do you still want to create this Test Notification?", "Test Notification Warning",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        Select Case result
                            Case DialogResult.No
                                Return
                            Case "7"
                                Return
                            Case Else

                        End Select
                    End If

                    'Insert 
                    query = "Insert into ISMPTestNotification " &
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
                    "(@strTestLogNumber, " &
                    "@strEmissionUnit, " &
                    "@datProposedStartDate, @datProposedEndDate, " &
                    "@strComments, null, " &
                    "@strContactEmail, null, " &
                    "@strOnlineFirstname, @strOnlineLastName, " &
                    "@strAIRSNumber, @strStaffResponsible, " &
                    "@strTestPlanAvailable, @strTimelyNotification, " &
                    "@strInternalComments, @strModifingStaff, " &
                    "getdate(), @strTelePhone, " &
                    "@strFax, @datTestPlanReceived, " &
                    "@datTestNotification, @strPollutants) "

                    Dim p4 As SqlParameter() = {
                        New SqlParameter("@strEmissionUnit", txtEmissionUnit.Text),
                        New SqlParameter("@datProposedStartDate", DTPTestDateStart.Value),
                        New SqlParameter("@datProposedEndDate", DTPTestDateEnd.Value),
                        New SqlParameter("@strComments", txtNotificationComments.Text),
                        New SqlParameter("@strContactEmail", txtContactEmailAddress.Text),
                        New SqlParameter("@strAIRSNumber", "0413" & cboAIRSNumber.SelectedValue),
                        New SqlParameter("@strStaffresponsible", cboStaffResponsible.SelectedValue),
                        New SqlParameter("@strTestPlanAvailable", TestPlan),
                        New SqlParameter("@strTimelyNotification", TimelyNotification),
                        New SqlParameter("@strOnlineFirstName", txtContactFirstName.Text),
                        New SqlParameter("@strOnlineLastName", txtContactLastName.Text),
                        New SqlParameter("@strInternalComments", txtISMPComments.Text),
                        New SqlParameter("@strmodifingstaff", CurrentUser.UserID),
                        New SqlParameter("@datTestPlanReceived", TestNotificationDate),
                        New SqlParameter("@datTestNotification", TestNotificationDate),
                        New SqlParameter("@strTelephone", mtbPhoneNumber.Text),
                        New SqlParameter("@strFax", mtbFaxNumber.Text),
                        New SqlParameter("@strPollutants", txtPollutants.Text),
                        New SqlParameter("@strtestlognumber", txtTestNotificationNumber.Text)
                    }

                    DB.RunCommand(query, p4)
                End If

                MsgBox("Information Saved.", MsgBoxStyle.Information, "Notification Log")
            Else
                MsgBox("You do not have sufficient permissions to save.", MsgBoxStyle.Information, "Notification Log")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)

        End Try
    End Sub
    Private Sub SelectNewTestNotifcationNumber()
        Try
            query = "Select max(convert(int, strTestLogNumber)) + 1 as TestNum " &
            "From ISMPTestnotification "

            txtTestNotificationNumber.Text = DB.GetInteger(query)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "form events"

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

End Class