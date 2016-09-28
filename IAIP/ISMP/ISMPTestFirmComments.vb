Imports System.Data.SqlClient


Public Class ISMPTestFirmComments
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim recExist As Boolean

    Dim dsTestingFirms As DataSet
    Dim daTestingFirms As SqlDataAdapter

    Private Sub ISMPTestFirmComments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ClearPage()
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
            If txtTestNotificationNumber.Text <> "" Or txtTestReportNumber.Text <> "" Then
                LoadTestFirmComments()
            End If
            If AccountFormAccess(67, 3) = "1" Then
                btnOpenManagerTools.Visible = True
            Else
                btnOpenManagerTools.Visible = False
            End If

            If AccountFormAccess(67, 1) = "1" Or AccountFormAccess(67, 2) = "1" Or AccountFormAccess(67, 3) = "1" Or AccountFormAccess(67, 4) = "1" Then
            Else
                tsbSave.Visible = False
                btnSavePreTest.Visible = False
                btnSaveDayOf.Visible = False
                btnSaveTestReport.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region "Page Load"
    Sub LoadTestingFirms()
        Dim dtTestingFirm As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try
            SQL = "Select " &
            "strTestingFirmKey, strTestingFirm " &
            "from LookUpTestingFirms " &
            "order by strTestingFirm "

            dsTestingFirms = New DataSet
            daTestingFirms = New SqlDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daTestingFirms.Fill(dsTestingFirms, "TestingFirms")

            dtTestingFirm.Columns.Add("strTestingFirmKey", GetType(System.String))
            dtTestingFirm.Columns.Add("strTestingFirm", GetType(System.String))

            drNewRow = dtTestingFirm.NewRow()
            drNewRow("strTestingFirmKey") = " "
            drNewRow("strTestingFirm") = " "
            dtTestingFirm.Rows.Add(drNewRow)

            For Each drDSRow In dsTestingFirms.Tables("TestingFirms").Rows()
                drNewRow = dtTestingFirm.NewRow()
                drNewRow("strTestingFirmKey") = drDSRow("strTestingFirmKey")
                drNewRow("strTestingFirm") = drDSRow("strTestingFirm")
                dtTestingFirm.Rows.Add(drNewRow)
            Next

            With cboTestingFirm
                .DataSource = dtTestingFirm
                .DisplayMember = "strTestingFirm"
                .ValueMember = "strTestingFirmKey"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadTestFirmComments()
        Try
            Dim CommentType As String = ""
            Dim CommentLine As String = ""
            cboCommentNumber.Items.Clear()
            cboCommentNumber.Text = ""
            txtAllComments.Clear()
            txtAddComments.Clear()

            SQL = "select " &
            "numCommentsID, strCommentType, " &
            "strComment, " &
            "to_char(datCommentDate, 'dd-Mon-yyyy') as CommentDate, " &
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible " &
            "from ismptestfirmcomments, EPDUSerProfiles " &
            "where ismptestfirmcomments.strStaffResponsible = EPDUSerProfiles.numUserID "

            If txtTestNotificationNumber.Text <> "" Then
                SQL = SQL & " and strTestLogNumber = '" & txtTestNotificationNumber.Text & "' "
            End If
            If Me.txtTestReportNumber.Text <> "" Then
                SQL = SQL & " and strReferenceNumber = '" & txtTestReportNumber.Text & "' "
            End If
            If txtAIRSNumber.Text <> "" Then
                SQL = SQL & " and strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
            End If
            If Me.cboTestingFirm.SelectedIndex > 1 Then
                SQL = SQL & " and strTestingFirmkey = '" & cboTestingFirm.SelectedValue & "' "
            End If

            SQL = SQL & " order by numCommentsID desc "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If txtAllComments.Text <> "" Then
                    txtAllComments.Text = txtAllComments.Text & "-------------" & vbCrLf
                End If
                If IsDBNull(dr.Item("strCommentType")) Then
                    CommentType = ""
                Else
                    CommentType = dr.Item("strCommentType")
                End If
                Select Case CommentType
                    Case "1"
                        CommentLine = "This comment was made before the actual test event by " & dr.Item("staffresponsible") & " on " & dr.Item("CommentDate")
                    Case "2"
                        CommentLine = "This comment was made in relation to information on the day of the test " _
                                           & dr.Item("staffresponsible") & " on " & dr.Item("CommentDate")
                    Case "3"
                        CommentLine = "This comment was made after the actual test event by " & dr.Item("staffresponsible") & " on " & dr.Item("CommentDate")
                    Case Else
                        CommentLine = "Unknown Comment Type by " & dr.Item("StaffResponsible") & " on " & dr.Item("CommentDate")
                End Select
                txtAllComments.Text = txtAllComments.Text & dr.Item("numCommentsID") & ") " & CommentLine & vbCrLf &
                     dr.Item("strComment") & vbCrLf

                cboCommentNumber.Items.Add(dr.Item("numCommentsId"))
            End While
            dr.Close()

            If txtAllComments.Text = "" Then
                SplitContainer1.SanelySetSplitterDistance(465)
            Else
                SplitContainer1.SanelySetSplitterDistance(242)
            End If
            If txtTestReportNumber.Text <> "" Then
                txtTestDateEnd.Clear()
                txtTestDateEnd.Visible = False
                txtTestDateStart.Clear()

                SQL = "Select " &
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as datTestDateStart, " &
                "to_char(datTestDateEnd, 'dd-Mon-yyyy') as datTestDateEnd " &
                "from ISMPReportInformation " &
                "where strReferenceNumber = '" & txtTestReportNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("datTestDateEnd")) Then
                        If IsDBNull(dr.Item("datTestDateStart")) Then

                        Else
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

                End If
                dr.Close()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region
#Region "Subs and Functions"
    Sub ClearPage()
        Try
            txtFacilityTested.Clear()
            cboTestingFirm.Text = ""
            txtTestNotificationNumber.Clear()
            txtTestReportNumber.Clear()
            txtTestDateStart.Clear()
            txtTestDateEnd.Clear()
            txtTestDateEnd.Visible = False
            txtAddComments.Clear()
            txtAllComments.Clear()
            cboCommentNumber.Items.Clear()
            cboCommentNumber.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub SaveCommentData(SaveType As String)
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
                'If cboCommentNumber.Text <> "" Then
                '    CommentID = ""    'cboCommentNumber.Text
                'Else
                '    CommentID = ""
                'End If
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
                    SQL = "Insert into ISMPTestFirmComments " &
                    "values " &
                    "((select max(ismptestfirmcomments.numcommentsid) + 1 " &
                    "from ismptestfirmcomments ),   " &
                    "'" & TestFirmKey & "', " &
                    "'" & AIRSNum & "', '" & TestLogNum & "', " &
                    "'" & RefNum & "', '" & SaveType & "', " &
                    "'" & CurrentUser.UserID & "', GETDATE(), " &
                    "'" & Replace(Comment, "'", "''") & "', '" & CurrentUser.UserID & "', " &
                    "GETDATE()) "
                Else
                    SQL = "Update ISMPTestFirmComments set " &
                    "strTestingFirmKey = '" & TestFirmKey & "', " &
                    "strAIRSNumber = '" & AIRSNum & "', " &
                    "strTestLogNumber = '" & TestLogNum & "', " &
                    "strReferenceNumber = '" & RefNum & "', " &
                    "strCommentType = '" & SaveType & "', " &
                    "strStaffresponsible = '" & CurrentUser.UserID & "', " &
                    "datCommentDate = GETDATE(), " &
                    "strComment = '" & Replace(Comment, "'", "''") & "', " &
                    "strModifingPerson = '" & CurrentUser.UserID & "', " &
                    "datModifingdate = GETDATE() " &
                    "where numcommentsID = '" & CommentID & "' "
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadTestFirmComments()

                If CommentID = "" Then
                    SQL = "Select " &
                    "max(ISMPTestFirmComments.numcommentsid) " &
                    "from ISMPTestFirmComments "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    CommentID = dr.Read
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadHeaderData(RefreshType As String)
        Try
            Select Case RefreshType
                Case "AIRS Number"
                    SQL = "select " &
                    "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as strAIRSnumber, " &
                    "strFacilityName " &
                    "from APBFacilityInformation " &
                    "where strAIRSnumber = '0413" & txtAIRSNumber.Text & "'"

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("strFacilityname")) Then
                            txtFacilityTested.Clear()
                        Else
                            txtFacilityTested.Text = dr.Item("strFacilityName")
                        End If
                    End While
                    dr.Close()
                Case "Notification"
                    SQL = "select " &
                    "SUBSTRING(APBFacilityInformation.strAIRSNumber, 5,8) as strAIRSNumber, " &
                    "strFacilityName, " &
                    "strTestLogNumber " &
                    "from APBFacilityInformation, ISMPTestNotification " &
                    "where APBFacilityInformation.strAIRSNumber = ISMPTestNotification.strAIRSNumber (+) " &
                    "and ISMPTestNotification.strTestLogNumber = '" & txtTestNotificationNumber.Text & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                    End While
                    dr.Close()
                Case "Test Report"
                    SQL = "select " &
                    "ISMPMaster.strReferenceNumber,  " &
                    "SUBSTRING(APBFacilityInformation.strAIRSnumber, 5,8) as strAIRSNUmber, " &
                    "strFacilityName,  strTestLogNumber,  " &
                    "to_char(datTestDateStart, 'dd-Mon-yyyy') as datTestDateStart, " &
                    "to_char(datTestDateEnd, 'dd-Mon-yyyy') as datTestDateEnd " &
                    "from ISMPMaster, APBFacilityInformation,  " &
                    "ISMPTestNotification, ISMPReportInformation " &
                    "where ISMPMaster.strAIRSNumber = APBFacilityInformation.strAIRSnumber  " &
                    "and ISMPMaster.strAIRSNumber = ISMpTestNotification.strAIRSNumber (+)  " &
                    "and ISMPMaster.strReferenceNumber = ISMPReportInformation.strReferenceNumber (+) " &
                    "and ISMPMaster.strReferenceNumber = '" & txtTestReportNumber.Text & "'  "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                            If IsDBNull(dr.Item("datTestDateStart")) Then

                            Else
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

                    End While
                    dr.Close()
                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub SaveHeaderData()
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
                    SQL = "Update ISMPTestFirmComments set " &
                    "strTestingFirmKey = '" & TestFirmKey & "', " &
                    "strAIRSNumber = '" & AIRSNum & "', " &
                    "strTestLogNumber = '" & TestLogNum & "', " &
                    "strReferenceNumber = '" & RefNum & "', " &
                    "strModifingPerson = '" & CurrentUser.UserID & "', " &
                    "datModifingdate = GETDATE() " &
                    "where numcommentsID = '" & CommentID & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    LoadTestFirmComments()

                Else
                    MsgBox("No data was saved because ")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region
#Region "Declarations"
    Private Sub btnSavePreTest_Click(sender As Object, e As EventArgs) Handles btnSavePreTest.Click
        Try
            SaveCommentData("1")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSaveDayOf_Click(sender As Object, e As EventArgs) Handles btnSaveDayOf.Click
        Try
            SaveCommentData("2")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSaveTestReport_Click(sender As Object, e As EventArgs) Handles btnSaveTestReport.Click
        Try
            SaveCommentData("3")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnOpenManagerTools_Click(sender As Object, e As EventArgs) Handles btnOpenManagerTools.Click
        Try

            If txtAddComments.Size.Width > (Me.Size.Width) - 212 Then
                txtAddComments.Size = New Drawing.Size(Me.Size.Width - 212, txtAddComments.Size.Height)
                lblComment.Visible = True
                cboCommentNumber.Visible = True
                btnDeleteComment.Visible = True
                btnEditComment.Visible = True
            Else
                txtAddComments.Size = New Drawing.Size(Me.Size.Width - 32, txtAddComments.Size.Height)
                lblComment.Visible = False
                cboCommentNumber.Visible = False
                btnDeleteComment.Visible = False
                btnEditComment.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbBack_Click(sender As Object, e As EventArgs) Handles tsbBack.Click
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsmBack_Click(sender As Object, e As EventArgs) Handles tsmBack.Click
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub OpenFacilityLookupTool()
        Try
            Dim facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK _
            AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                txtAIRSNumber.Text = facilityLookupDialog.SelectedAirsNumber
                txtFacilityTested.Text = facilityLookupDialog.SelectedFacilityName
            End If
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
                SQL = "Select " &
                "strComment " &
                "from ISMPTestFirmComments " &
                "where numCommentsID = '" & cboCommentNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    If IsDBNull(dr.Item("strComment")) Then
                        txtAddComments.Clear()
                    Else
                        txtAddComments.Text = dr.Item("strComment")
                    End If
                Else
                    txtAddComments.Clear()
                End If
                dr.Close()
            Else
                txtAddComments.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
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

                SQL = "Update ISMPTestFirmComments set " &
                "strTestingFirmKey = '" & TestFirmKey & "', " &
                "strAIRSNumber = '" & AIRSNum & "', " &
                "strTestLogNumber = '" & TestLogNum & "', " &
                "strReferenceNumber = '" & RefNum & "', " &
                "strComment = '" & Replace(txtAddComments.Text, "'", "''") & "', " &
                "strStaffresponsible = '" & CurrentUser.UserID & "', " &
                "datCommentDate = GETDATE(), " &
                "strModifingPerson = '" & CurrentUser.UserID & "', " &
                "datModifingDate = GETDATE() " &
                "where numCommentsID = '" & CommentID & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteComment_Click(sender As Object, e As EventArgs) Handles btnDeleteComment.Click
        Try
            If cboCommentNumber.Text <> "" Then
                SQL = "Delete ISMPTestFirmComments " &
                "where numCommentsId = '" & cboCommentNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshNotifications_Click(sender As Object, e As EventArgs) Handles btnRefreshNotifications.Click
        Try
            LoadHeaderData("Notification")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshReportNumber_Click(sender As Object, e As EventArgs) Handles btnRefreshReportNumber.Click
        Try
            LoadHeaderData("Test Report")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshAIRSNumber_Click(sender As Object, e As EventArgs) Handles btnRefreshAIRSNumber.Click
        Try
            LoadHeaderData("AIRS Number")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Try
            SaveHeaderData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class