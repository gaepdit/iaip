Imports System.Data.SqlClient

Public Class ISMPTestFirmComments
    Dim query As String

    Private Sub ISMPTestFirmComments_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            lblComment.Visible = False
            cboCommentNumber.Visible = False
            btnDeleteComment.Visible = False
            btnEditComment.Visible = False

            LoadTestingFirms()

            If txtAllComments.Text = "" Then
                SplitContainer1.SanelySetSplitterDistance(465)
            Else
                SplitContainer1.SanelySetSplitterDistance(242)
            End If
            If AccountFormAccess(67, 3) = "1" Then
                btnOpenManagerTools.Visible = True
            Else
                btnOpenManagerTools.Visible = False
            End If

            If AccountFormAccess(67, 1) <> "1" AndAlso
                AccountFormAccess(67, 2) <> "1" AndAlso
                AccountFormAccess(67, 3) <> "1" AndAlso
                AccountFormAccess(67, 4) <> "1" Then

                tsbSave.Visible = False
                btnSavePreTest.Visible = False
                btnSaveDayOf.Visible = False
                btnSaveTestReport.Visible = False
            End If

            LoadTestFirmComments()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadTestingFirms()
        Try
            query = "Select " &
            "strTestingFirmKey, strTestingFirm " &
            "from LookUpTestingFirms " &
            "order by strTestingFirm "

            With cboTestingFirm
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "strTestingFirm"
                .ValueMember = "strTestingFirmKey"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub LoadTestFirmComments()
        Try
            Dim CommentType As String = ""
            Dim CommentLine As String = ""
            cboCommentNumber.Items.Clear()
            cboCommentNumber.Text = ""
            txtAllComments.Clear()
            txtAddComments.Clear()

            query = "select " &
            "numCommentsID, strCommentType, " &
            "strComment, " &
            "format(datCommentDate, 'dd-MMM-yyyy') as CommentDate, " &
            "concat(strLastName, ', ' ,strFirstName) as StaffResponsible " &
            "from ismptestfirmcomments, EPDUSerProfiles " &
            "where ismptestfirmcomments.strStaffResponsible = EPDUSerProfiles.numUserID "

            If txtTestNotificationNumber.Text <> "" Then
                query = query & " and strTestLogNumber = @log "
            End If
            If Me.txtTestReportNumber.Text <> "" Then
                query = query & " and strReferenceNumber = @ref "
            End If
            If txtAIRSNumber.Text <> "" Then
                query = query & " and strAIRSNumber = @airs "
            End If
            If Me.cboTestingFirm.SelectedIndex > 1 Then
                query = query & " and strTestingFirmkey = @key "
            End If

            query = query & " order by numCommentsID desc "

            Dim p As SqlParameter() = {
                New SqlParameter("@log", txtTestNotificationNumber.Text),
                New SqlParameter("@ref", txtTestReportNumber.Text),
                New SqlParameter("@airs", "0413" & txtAIRSNumber.Text),
                New SqlParameter("@key", cboTestingFirm.SelectedValue)
            }

            Dim dt As DataTable = DB.GetDataTable(query, p)

            For Each dr As DataRow In dt.Rows
                If txtAllComments.Text <> "" Then
                    txtAllComments.Text &= "-------------" & vbCrLf
                End If
                If IsDBNull(dr.Item("strCommentType")) Then
                    CommentType = ""
                Else
                    CommentType = dr.Item("strCommentType").ToString
                End If
                Select Case CommentType
                    Case "1"
                        CommentLine = "This comment was made before the actual test event by " & dr.Item("staffresponsible").ToString & " on " & dr.Item("CommentDate").ToString
                    Case "2"
                        CommentLine = "This comment was made in relation to information on the day of the test " _
                                           & dr.Item("staffresponsible").ToString & " on " & dr.Item("CommentDate").ToString
                    Case "3"
                        CommentLine = "This comment was made after the actual test event by " & dr.Item("staffresponsible").ToString & " on " & dr.Item("CommentDate").ToString
                    Case Else
                        CommentLine = "Unknown Comment Type by " & dr.Item("StaffResponsible").ToString & " on " & dr.Item("CommentDate").ToString
                End Select
                txtAllComments.Text &= dr.Item("numCommentsID").ToString & ") " & CommentLine & vbCrLf & dr.Item("strComment").ToString & vbCrLf

                cboCommentNumber.Items.Add(dr.Item("numCommentsId"))
            Next

            If txtAllComments.Text = "" Then
                SplitContainer1.SanelySetSplitterDistance(465)
            Else
                SplitContainer1.SanelySetSplitterDistance(242)
            End If
            If txtTestReportNumber.Text <> "" Then
                txtTestDateEnd.Clear()
                txtTestDateEnd.Visible = False
                txtTestDateStart.Clear()

                query = "Select " &
                "format(datTestDateStart, 'dd-MMM-yyyy') as datTestDateStart, " &
                "format(datTestDateEnd, 'dd-MMM-yyyy') as datTestDateEnd " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = @ref "

                Dim p2 As New SqlParameter("@ref", txtTestReportNumber.Text)

                Dim dr2 As DataRow = DB.GetDataRow(query, p2)
                If dr2 IsNot Nothing Then
                    If IsDBNull(dr2.Item("datTestDateEnd")) Then
                        If Not IsDBNull(dr2.Item("datTestDateStart")) Then
                            txtTestDateStart.Text = dr2.Item("datTestDateStart").ToString
                        End If
                    Else
                        If dr2.Item("datTestDateEnd").ToString = dr2.Item("datTestDateStart").ToString Then
                            txtTestDateStart.Text = dr2.Item("datTestDateStart").ToString
                        Else
                            txtTestDateStart.Text = dr2.Item("datTestDateStart").ToString
                            txtTestDateEnd.Text = dr2.Item("datTestDateEnd").ToString
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SaveCommentData(SaveType As String)
        Try
            Dim CommentID As String = ""
            Dim TestFirmKey As String = ""
            Dim AIRSNum As String = ""
            Dim TestLogNum As String = ""
            Dim RefNum As String = ""
            Dim Comment As String = ""

            If cboTestingFirm.SelectedIndex < 1 Then
                MsgBox("Please Select a Testing Firm before saving comments.", MsgBoxStyle.Exclamation, "ISMP Test Firm Comments")
            Else
                CommentID = ""
                TestFirmKey = cboTestingFirm.SelectedValue
                If txtAIRSNumber.Text <> "" Then
                    AIRSNum = "0413" & txtAIRSNumber.Text
                Else
                    AIRSNum = ""
                End If
                If txtTestNotificationNumber.Text <> "" Then
                    TestLogNum = txtTestNotificationNumber.Text
                Else
                    TestLogNum = ""
                End If
                If txtTestReportNumber.Text <> "" Then
                    RefNum = txtTestReportNumber.Text
                Else
                    RefNum = ""
                End If
                If txtAddComments.Text <> "" Then
                    Comment = txtAddComments.Text
                Else
                    Comment = ""
                End If

                If CommentID = "" Then
                    query = "Insert into ISMPTestFirmComments " &
                        "(NUMCOMMENTSID, STRTESTINGFIRMKEY, STRAIRSNUMBER, STRTESTLOGNUMBER, " &
                        "STRREFERENCENUMBER, STRCOMMENTTYPE, STRSTAFFRESPONSIBLE, DATCOMMENTDATE, " &
                        "STRCOMMENT, STRMODIFINGPERSON, DATMODIFINGDATE) " &
                    "values " &
                    "((select max(ismptestfirmcomments.numcommentsid) + 1 " &
                    "from ismptestfirmcomments ),   " &
                        "@STRTESTINGFIRMKEY, @STRAIRSNUMBER, @STRTESTLOGNUMBER, " &
                        "@STRREFERENCENUMBER, @STRCOMMENTTYPE, @STRSTAFFRESPONSIBLE, getdate(), " &
                        "@STRCOMMENT, @STRMODIFINGPERSON, getdate()) "
                Else
                    query = "Update ISMPTestFirmComments set " &
                    "strTestingFirmKey = @strTestingFirmKey, " &
                    "strAIRSNumber = @strAIRSNumber, " &
                    "strTestLogNumber = @strTestLogNumber, " &
                    "strReferenceNumber = @strReferenceNumber, " &
                    "strCommentType = @strCommentType, " &
                    "strStaffresponsible = @strStaffresponsible, " &
                    "datCommentDate = GETDATE(), " &
                    "strComment = @strComment, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingdate = GETDATE() " &
                    "where numcommentsID = @numcommentsID "
                End If

                Dim p As SqlParameter() = {
                    New SqlParameter("@strTestingFirmKey", TestFirmKey),
                    New SqlParameter("@strAIRSNumber", AIRSNum),
                    New SqlParameter("@strTestLogNumber", TestLogNum),
                    New SqlParameter("@strReferenceNumber", RefNum),
                    New SqlParameter("@strCommentType", SaveType),
                    New SqlParameter("@strStaffresponsible", CurrentUser.UserID),
                    New SqlParameter("@strComment", Comment),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@numcommentsID", CommentID)
                }

                DB.RunCommand(query, p)

                LoadTestFirmComments()

                If CommentID = "" Then
                    query = "Select " &
                    "max(ISMPTestFirmComments.numcommentsid) " &
                    "from ISMPTestFirmComments "

                    CommentID = DB.GetInteger(query)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub LoadHeaderData(RefreshType As String)
        Try
            Select Case RefreshType
                Case "AIRS Number"
                    query = "select " &
                    "strFacilityName " &
                    "from APBFacilityInformation " &
                    "where strAIRSnumber = @airs "

                    Dim p As New SqlParameter("@airs", "0413" & txtAIRSNumber.Text)

                    txtFacilityTested.Text = DB.GetString(query, p)
                Case "Notification"
                    query = "select " &
                    "SUBSTRING(APBFacilityInformation.strAIRSNumber, 5,8) as strAIRSNumber, " &
                    "strFacilityName, " &
                    "strTestLogNumber " &
                    "from APBFacilityInformation LEFT JOIN ISMPTestNotification " &
                    "ON APBFacilityInformation.strAIRSNumber = ISMPTestNotification.strAIRSNumber " &
                    "WHERE ISMPTestNotification.strTestLogNumber = @log "

                    Dim p As New SqlParameter("@log", txtTestNotificationNumber.Text)

                    Dim dr As DataRow = DB.GetDataRow(query, p)

                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("strAIRSnumber")) Then
                            txtAIRSNumber.Clear()
                        Else
                            txtAIRSNumber.Text = dr.Item("strAIRSNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            txtFacilityTested.Clear()
                        Else
                            txtFacilityTested.Text = dr.Item("strFacilityName")
                        End If
                    Else
                        txtAIRSNumber.Clear()
                        txtFacilityTested.Clear()
                    End If
                Case "Test Report"
                    query = "select " &
                    "ISMPMaster.strReferenceNumber,  " &
                    "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as strAIRSNUmber, " &
                    "strFacilityName,  strTestLogNumber,  " &
                    "format(datTestDateStart, 'dd-MMM-yyyy') as datTestDateStart, " &
                    "format(datTestDateEnd, 'dd-MMM-yyyy') as datTestDateEnd " &
                    "from ISMPMaster " &
                    " INNER JOIN APBFacilityInformation  " &
                    "ON ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSnumber  " &
                    " LEFT JOIN ISMPTestNotification " &
                    "ON ISMPMaster.strAIRSNumber = ISMpTestNotification.strAIRSNumber " &
                    " LEFT JOIN ISMPReportInformation " &
                    "ON ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
                    "where ISMPMaster.strReferenceNumber = @ref "

                    Dim p As New SqlParameter("@ref", txtTestReportNumber.Text)

                    Dim dr As DataRow = DB.GetDataRow(query, p)

                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("strAIRSNumber")) Then
                            txtAIRSNumber.Clear()
                        Else
                            txtAIRSNumber.Text = dr.Item("strAIRSnumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityname")) Then
                            txtFacilityTested.Clear()
                        Else
                            txtFacilityTested.Text = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strTestLogNumber")) Then
                            txtTestNotificationNumber.Clear()
                        Else
                            txtTestNotificationNumber.Text = dr.Item("strTestLogNumber")
                        End If

                        If IsDBNull(dr.Item("datTestDateEnd")) Then
                            If Not IsDBNull(dr.Item("datTestDateStart")) Then
                                txtTestDateStart.Text = dr.Item("datTestDateStart")
                            End If
                        Else
                            If dr.Item("datTestDateEnd") = dr.Item("datTestDateStart") Then
                                txtTestDateStart.Text = dr.Item("datTestDateStart")
                            Else
                                txtTestDateStart.Text = dr.Item("datTestDateStart")
                                txtTestDateEnd.Text = dr.Item("datTestDateEnd")
                            End If
                        End If
                    Else
                        txtAIRSNumber.Clear()
                        txtFacilityTested.Clear()
                        txtTestNotificationNumber.Clear()
                        txtTestDateStart.Clear()
                        txtTestDateEnd.Clear()
                    End If
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub SaveHeaderData()
        Try
            Dim CommentID As String = ""
            Dim TestFirmKey As String = ""
            Dim AIRSNum As String = ""
            Dim TestLogNum As String = ""
            Dim RefNum As String = ""

            If cboTestingFirm.SelectedIndex < 1 Then
                MsgBox("Please Select a Testing Firm before saving comments.", MsgBoxStyle.Exclamation, "ISMP Test Firm Comments")
            Else
                CommentID = txtCommentID.Text
                TestFirmKey = cboTestingFirm.SelectedValue
                If txtAIRSNumber.Text <> "" Then
                    AIRSNum = "0413" & txtAIRSNumber.Text
                Else
                    AIRSNum = ""
                End If
                If txtTestNotificationNumber.Text <> "" Then
                    TestLogNum = txtTestNotificationNumber.Text
                Else
                    TestLogNum = ""
                End If
                If txtTestReportNumber.Text <> "" Then
                    RefNum = txtTestReportNumber.Text
                Else
                    RefNum = ""
                End If

                If CommentID <> "" Then
                    query = "Update ISMPTestFirmComments set " &
                    "strTestingFirmKey = @strTestingFirmKey, " &
                    "strAIRSNumber = @strAIRSNumber, " &
                    "strTestLogNumber = @strTestLogNumber, " &
                    "strReferenceNumber = @strReferenceNumber, " &
                    "strModifingPerson = @strModifingPerson, " &
                    "datModifingdate = GETDATE() " &
                    "where numcommentsID = @numcommentsID "

                    Dim p As SqlParameter() = {
                        New SqlParameter("@strTestingFirmKey", TestFirmKey),
                        New SqlParameter("@strAIRSNumber", AIRSNum),
                        New SqlParameter("@strTestLogNumber", TestLogNum),
                        New SqlParameter("@strReferenceNumber", RefNum),
                        New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                        New SqlParameter("@numcommentsID", CommentID)
                    }

                    DB.RunCommand(query, p)

                    LoadTestFirmComments()

                Else
                    MsgBox("No data was saved because ")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSavePreTest_Click(sender As Object, e As EventArgs) Handles btnSavePreTest.Click
        Try
            SaveCommentData("1")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveDayOf_Click(sender As Object, e As EventArgs) Handles btnSaveDayOf.Click
        Try
            SaveCommentData("2")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveTestReport_Click(sender As Object, e As EventArgs) Handles btnSaveTestReport.Click
        Try
            SaveCommentData("3")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnOpenManagerTools_Click(sender As Object, e As EventArgs) Handles btnOpenManagerTools.Click
        Try

            If txtAddComments.Size.Width > (Me.Size.Width) - 212 Then
                txtAddComments.Size = New Size(Me.Size.Width - 212, txtAddComments.Size.Height)
                lblComment.Visible = True
                cboCommentNumber.Visible = True
                btnDeleteComment.Visible = True
                btnEditComment.Visible = True
            Else
                txtAddComments.Size = New Size(Me.Size.Width - 32, txtAddComments.Size.Height)
                lblComment.Visible = False
                cboCommentNumber.Visible = False
                btnDeleteComment.Visible = False
                btnEditComment.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub OpenFacilityLookupTool()
        Try
            Using facilityLookupDialog As New IAIPFacilityLookUpTool
                If facilityLookupDialog.ShowDialog() = DialogResult.OK AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                    txtAIRSNumber.Text = facilityLookupDialog.SelectedAirsNumber
                    txtFacilityTested.Text = facilityLookupDialog.SelectedFacilityName
                End If
            End Using
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbLookUpAirNumber_Click(sender As Object, e As EventArgs) Handles tsbLookUpAirNumber.Click
        OpenFacilityLookupTool()
    End Sub
    Private Sub cboCommentNumber_TextChanged(sender As Object, e As EventArgs) Handles cboCommentNumber.TextChanged
        Try
            If cboCommentNumber.Text <> "" Then
                query = "Select " &
                "strComment " &
                "from ISMPTestFirmComments " &
                "where numCommentsID = @co "

                Dim p As New SqlParameter("@co", cboCommentNumber.Text)

                txtAddComments.Text = DB.GetString(query, p)
            Else
                txtAddComments.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditComment_Click(sender As Object, e As EventArgs) Handles btnEditComment.Click
        Try
            Dim CommentID As String = ""
            Dim TestFirmKey As String = ""
            Dim AIRSNum As String = ""
            Dim TestLogNum As String = ""
            Dim RefNum As String = ""

            If cboCommentNumber.Text <> "" Then
                CommentID = cboCommentNumber.Text
                TestFirmKey = cboTestingFirm.SelectedValue
                If txtAIRSNumber.Text <> "" Then
                    AIRSNum = "0413" & txtAIRSNumber.Text
                Else
                    AIRSNum = ""
                End If
                If txtTestNotificationNumber.Text <> "" Then
                    TestLogNum = txtTestNotificationNumber.Text
                Else
                    TestLogNum = ""
                End If
                If txtTestReportNumber.Text <> "" Then
                    RefNum = txtTestReportNumber.Text
                Else
                    RefNum = ""
                End If

                query = "Update ISMPTestFirmComments set " &
                "strTestingFirmKey = @strTestingFirmKey, " &
                "strAIRSNumber = @strAIRSNumber, " &
                "strTestLogNumber = @strTestLogNumber, " &
                "strReferenceNumber = @strReferenceNumber, " &
                "strComment = @strComment, " &
                "strStaffresponsible = @strStaffresponsible, " &
                "datCommentDate = GETDATE(), " &
                "strModifingPerson = @strModifingPerson, " &
                "datModifingDate = GETDATE() " &
                "where numCommentsID = @numCommentsID "

                Dim p As SqlParameter() = {
                    New SqlParameter("@strTestingFirmKey", TestFirmKey),
                    New SqlParameter("@strAIRSNumber", AIRSNum),
                    New SqlParameter("@strTestLogNumber", TestLogNum),
                    New SqlParameter("@strReferenceNumber", RefNum),
                    New SqlParameter("@strComment", txtAddComments.Text),
                    New SqlParameter("@strStaffresponsible", CurrentUser.UserID),
                    New SqlParameter("@strModifingPerson", CurrentUser.UserID),
                    New SqlParameter("@numCommentsID", CommentID)
                }

                DB.RunCommand(query, p)

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteComment_Click(sender As Object, e As EventArgs) Handles btnDeleteComment.Click
        Try
            If cboCommentNumber.Text <> "" Then
                query = "Delete ISMPTestFirmComments " &
                "where numCommentsId = @co "

                Dim p As New SqlParameter("@co", cboCommentNumber.Text)

                DB.RunCommand(query, p)

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshNotifications_Click(sender As Object, e As EventArgs) Handles btnRefreshNotifications.Click
        Try
            LoadHeaderData("Notification")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshReportNumber_Click(sender As Object, e As EventArgs) Handles btnRefreshReportNumber.Click
        Try
            LoadHeaderData("Test Report")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshAIRSNumber_Click(sender As Object, e As EventArgs) Handles btnRefreshAIRSNumber.Click
        Try
            LoadHeaderData("AIRS Number")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Try
            SaveHeaderData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class