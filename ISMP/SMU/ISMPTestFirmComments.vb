Imports System.Data.OracleClient


Public Class ISMPTestFirmComments
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

    Dim dsTestingFirms As DataSet
    Dim daTestingFirms As OracleDataAdapter

    Private Sub ISMPTestFirmComments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ClearPage()
            lblComment.Visible = False
            cboCommentNumber.Visible = False
            btnDeleteComment.Visible = False
            btnEditComment.Visible = False

            LoadTestingFirms()

            If txtAllComments.Text = "" Then
                SplitContainer1.SplitterDistance = 465
            Else
                SplitContainer1.SplitterDistance = 242
            End If
            If txtTestNotificationNumber.Text <> "" Or txtTestReportNumber.Text <> "" Then
                LoadTestFirmComments()
            End If
            If AccountArray(67, 3) = "1" Then
                btnOpenManagerTools.Visible = True
            Else
                btnOpenManagerTools.Visible = False
            End If

            If AccountArray(67, 1) = "1" Or AccountArray(67, 2) = "1" Or AccountArray(67, 3) = "1" Or AccountArray(67, 4) = "1" Then
            Else
                tsbSave.Visible = False
                btnSavePreTest.Visible = False
                btnSaveDayOf.Visible = False
                btnSaveTestReport.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region "Page Load"
    Sub LoadTestingFirms()
        Dim dtTestingFirm As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try
            SQL = "Select " & _
            "strTestingFirmKey, strTestingFirm " & _
            "from " & connNameSpace & ".LookUpTestingFirms " & _
            "order by strTestingFirm "

            dsTestingFirms = New DataSet
            daTestingFirms = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "select " & _
            "numCommentsID, strCommentType, " & _
            "strComment, " & _
            "to_char(datCommentDate, 'dd-Mon-yyyy') as CommentDate, " & _
            "(strLastName|| ', ' ||strFirstName) as StaffResponsible " & _
            "from " & connNameSpace & ".ismptestfirmcomments, " & connNameSpace & ".EPDUSerProfiles " & _
            "where " & connNameSpace & ".ismptestfirmcomments.strStaffResponsible = " & connNameSpace & ".EPDUSerProfiles.numUserID "

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

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
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
                txtAllComments.Text = txtAllComments.Text & dr.Item("numCommentsID") & ") " & CommentLine & vbCrLf & _
                     dr.Item("strComment") & vbCrLf

                cboCommentNumber.Items.Add(dr.Item("numCommentsId"))
            End While
            dr.Close()

            If txtAllComments.Text = "" Then
                SplitContainer1.SplitterDistance = 465
            Else
                SplitContainer1.SplitterDistance = 242
            End If
            If txtTestReportNumber.Text <> "" Then
                txtTestDateEnd.Clear()
                txtTestDateEnd.Visible = False
                txtTestDateStart.Clear()

                SQL = "Select " & _
                "to_char(datTestDateStart, 'dd-Mon-yyyy') as datTestDateStart, " & _
                "to_char(datTestDateEnd, 'dd-Mon-yyyy') as datTestDateEnd " & _
                "from " & connNameSpace & ".ISMPReportInformation " & _
                "where strReferenceNumber = '" & txtTestReportNumber.Text & "' "
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub SaveCommentData(ByVal SaveType As String)
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
                    SQL = "Insert into " & connNameSpace & ".ISMPTestFirmComments " & _
                    "values " & _
                    "((select max(" & connNameSpace & ".ismptestfirmcomments.numcommentsid) + 1 " & _
                    "from " & connNameSpace & ".ismptestfirmcomments ),   " & _
                    "'" & TestFirmKey & "', " & _
                    "'" & AIRSNum & "', '" & TestLogNum & "', " & _
                    "'" & RefNum & "', '" & SaveType & "', " & _
                    "'" & UserGCode & "', sysdate, " & _
                    "'" & Replace(Comment, "'", "''") & "', '" & UserGCode & "', " & _
                    "sysdate) "
                Else
                    SQL = "Update " & connNameSpace & ".ISMPTestFirmComments set " & _
                    "strTestingFirmKey = '" & TestFirmKey & "', " & _
                    "strAIRSNumber = '" & AIRSNum & "', " & _
                    "strTestLogNumber = '" & TestLogNum & "', " & _
                    "strReferenceNumber = '" & RefNum & "', " & _
                    "strCommentType = '" & SaveType & "', " & _
                    "strStaffresponsible = '" & UserGCode & "', " & _
                    "datCommentDate = sysdate, " & _
                    "strComment = '" & Replace(Comment, "'", "''") & "', " & _
                    "strModifingPerson = '" & UserGCode & "', " & _
                    "datModifingdate = sysdate " & _
                    "where numcommentsID = '" & CommentID & "' "
                End If
                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadTestFirmComments()

                If CommentID = "" Then
                    SQL = "Select " & _
                    "max(" & connNameSpace & ".ISMPTestFirmComments.numcommentsid) " & _
                    "from " & connNameSpace & ".ISMPTestFirmComments "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    CommentID = dr.Read
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadHeaderData(ByVal RefreshType As String)
        Try
            Select Case RefreshType
                Case "AIRS Number"
                    SQL = "select " & _
                    "substr(" & connNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as strAIRSnumber, " & _
                    "strFacilityName " & _
                    "from " & connNameSpace & ".APBFacilityInformation " & _
                    "where strAIRSnumber = '0413" & txtAIRSNumber.Text & "'"

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
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
                    SQL = "select " & _
                    "substr(" & connNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as strAIRSNumber, " & _
                    "strFacilityName, " & _
                    "strTestLogNumber " & _
                    "from " & connNameSpace & ".APBFacilityInformation, " & connNameSpace & ".ISMPTestNotification " & _
                    "where " & connNameSpace & ".APBFacilityInformation.strAIRSNumber = " & connNameSpace & ".ISMPTestNotification.strAIRSNumber (+) " & _
                    "and " & connNameSpace & ".ISMPTestNotification.strTestLogNumber = '" & txtTestNotificationNumber.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
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
                    SQL = "select " & _
                    "" & connNameSpace & ".ISMPMaster.strReferenceNumber,  " & _
                    "substr(" & connNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as strAIRSNUmber, " & _
                    "strFacilityName,  strTestLogNumber,  " & _
                    "to_char(datTestDateStart, 'dd-Mon-yyyy') as datTestDateStart, " & _
                    "to_char(datTestDateEnd, 'dd-Mon-yyyy') as datTestDateEnd " & _
                    "from " & connNameSpace & ".ISMPMaster, " & connNameSpace & ".APBFacilityInformation,  " & _
                    "" & connNameSpace & ".ISMPTestNotification, " & connNameSpace & ".ISMPReportInformation " & _
                    "where " & connNameSpace & ".ISMPMaster.strAIRSNumber = " & connNameSpace & ".APBFacilityInformation.strAIRSnumber  " & _
                    "and " & connNameSpace & ".ISMPMaster.strAIRSNumber = " & connNameSpace & ".ISMpTestNotification.strAIRSNumber (+)  " & _
                    "and " & connNameSpace & ".ISMPMaster.strReferenceNumber = " & connNameSpace & ".ISMPReportInformation.strReferenceNumber (+) " & _
                    "and " & connNameSpace & ".ISMPMaster.strReferenceNumber = '" & txtTestReportNumber.Text & "'  "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                    SQL = "Update " & connNameSpace & ".ISMPTestFirmComments set " & _
                    "strTestingFirmKey = '" & TestFirmKey & "', " & _
                    "strAIRSNumber = '" & AIRSNum & "', " & _
                    "strTestLogNumber = '" & TestLogNum & "', " & _
                    "strReferenceNumber = '" & RefNum & "', " & _
                    "strModifingPerson = '" & UserGCode & "', " & _
                    "datModifingdate = sysdate " & _
                    "where numcommentsID = '" & CommentID & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    LoadTestFirmComments()

                Else
                    MsgBox("No data was saved because ")
                End If

                End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region
#Region "Declarations"
    Private Sub btnSavePreTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePreTest.Click
        Try
            SaveCommentData("1")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSaveDayOf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDayOf.Click
        Try
            SaveCommentData("2")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSaveTestReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveTestReport.Click
        Try
            SaveCommentData("3")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnOpenManagerTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenManagerTools.Click
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsmBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmBack.Click
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbLookUpAirNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbLookUpAirNumber.Click
        Try
            If FacilityLookUpTool Is Nothing Then
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            Else
                FacilityLookUpTool.Dispose()
                FacilityLookUpTool = New IAIPFacilityLookUpTool
                If FacilityLookUpTool Is Nothing Then FacilityLookUpTool = New IAIPFacilityLookUpTool
                FacilityLookUpTool.Show()
            End If
            FacilityLookUpTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Public WriteOnly Property ValueFromFacilityLookUp() As String
        Set(ByVal Value As String)
            txtAIRSNumber.Text = Value
        End Set
    End Property
    Public WriteOnly Property ValueFromFacilityLookUp2() As String
        Set(ByVal Value2 As String)
            txtFacilityTested.Text = Value2
        End Set
    End Property
    Private Sub cboCommentNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCommentNumber.TextChanged
        Try
            If cboCommentNumber.Text <> "" Then
                SQL = "Select " & _
                "strComment " & _
                "from " & connNameSpace & ".ISMPTestFirmComments " & _
                "where numCommentsID = '" & cboCommentNumber.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
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
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnEditComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditComment.Click
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

                SQL = "Update " & connNameSpace & ".ISMPTestFirmComments set " & _
                "strTestingFirmKey = '" & TestFirmKey & "', " & _
                "strAIRSNumber = '" & AIRSNum & "', " & _
                "strTestLogNumber = '" & TestLogNum & "', " & _
                "strReferenceNumber = '" & RefNum & "', " & _
                "strComment = '" & Replace(txtAddComments.Text, "'", "''") & "', " & _
                "strStaffresponsible = '" & UserGCode & "', " & _
                "datCommentDate = sysdate, " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = sysdate " & _
                "where numCommentsID = '" & CommentID & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteComment.Click
        Try
            If cboCommentNumber.Text <> "" Then
                SQL = "Delete " & connNameSpace & ".ISMPTestFirmComments " & _
                "where numCommentsId = '" & cboCommentNumber.Text & "' "

                cmd = New OracleCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtAddComments.Clear()
                txtAllComments.Clear()
                LoadTestFirmComments()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshNotifications_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshNotifications.Click
        Try
            LoadHeaderData("Notification")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshReportNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshReportNumber.Click
        Try
            LoadHeaderData("Test Report")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnRefreshAIRSNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshAIRSNumber.Click
        Try
            LoadHeaderData("AIRS Number")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try
            SaveHeaderData()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, "https://sites.google.com/a/dnr.state.ga.us/iaip-docs/")
        Catch ex As Exception
        End Try

    End Sub
End Class