Imports Oracle.ManagedDataAccess.Client
Imports System.IO

Public Class SSPPPublicNoticiesAndAdvisories
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean
    Dim dsPublicLetters As DataSet
    Dim daPublicLetters As OracleDataAdapter


    Private Sub DevPublicNoticiesAndAdvisories_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            Panel1.Text = "Public Advisories Letter Generator"
            Panel2.Text = CurrentUser.AlphaName
            Panel3.Text = OracleDate

            'Me.WindowState = FormWindowState.Maximized

            LoadPublicNoticesList()
            TCPublicNotices.TabPages.Remove(TPPublishDocument)
            LoadOldPDFs()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".DevPublicNoticiesAndAdvisories_Load")
        Finally

        End Try

    End Sub
#Region "Page Load"
    Sub LoadPublicNoticesList()
        Try

            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
            "strCountyName, " & _
            "case " & _
            "  when strApplicationType is Null then '' " & _
            "  else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
            "End AppType, " & _
            "case " & _
            "  when strPAReady is Null then '' " & _
            "  when strPAReady = 'True' then 'PA Ready' " & _
            "  when strPAReady = 'False' then '' " & _
            "  Else '' " & _
            "End PAReady, " & _
            "to_char(datPAExpires, 'YYYY-MM-dd') as PAExpires, " & _
            "case " & _
            "when strPAPosted is null then '' " & _
            "else strPAPosted " & _
            "end strPAPosted, " & _
            "case " & _
            "  when strPNReady is Null then '' " & _
            "  when strPNReady = 'True' then 'PN Ready' " & _
            "  when strPNReady = 'False' then '' " & _
            "  Else '' " & _
            "End PNReady, " & _
            "to_char(datPNExpires, 'YYYY-MM-dd') as PNExpires, " & _
            "case " & _
            "when strPNPosted is null then '' " & _
            "else strPNPosted " & _
            "end strPNPosted " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData, " & _
            "AIRBRANCH.SSPPApplicationTracking, AIRBRANCH.LookUpApplicationTypes, " & _
            "AIRBRANCH.LookUpCountyInformation " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
            "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+) " & _
            "and datFinalizedDate is Null " & _
            "And strPAPosted Is null And strPNPosted Is null " & _
            "and (strPAReady = 'True' or strPNReady = 'True') " & _
            "and ((strApplicationTypeCode = '2' or strApplicationTypeCode = '21' " & _
            "or strApplicationTypeCode = '14' or strApplicationTypeCode = '16' " & _
            "or strApplicationTypeCode = '22' ) " & _
            "or ((strApplicationTypeCode = '15' " & _
            " or strApplicationTypeCode = '9' or strApplicationTypeCode = '20' " & _
            "or strApplicationTypeCode = '11' " & _
            "or strApplicationTypeCode = '12')and strPublicInvolvement <> '2')) " & _
            "order by strApplicationNumber desc "

            dsPublicLetters = New DataSet

            daPublicLetters = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPublicLetters.Fill(dsPublicLetters, "PublicLetters")
            dgvPublicNotice.DataSource = dsPublicLetters
            dgvPublicNotice.DataMember = "PublicLetters"

            txtPreviewCount.Text = dgvPublicNotice.RowCount.ToString

            dgvPublicNotice.RowHeadersVisible = False
            dgvPublicNotice.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvPublicNotice.AllowUserToResizeColumns = True
            dgvPublicNotice.AllowUserToAddRows = False
            dgvPublicNotice.AllowUserToDeleteRows = False
            dgvPublicNotice.AllowUserToOrderColumns = True
            dgvPublicNotice.AllowUserToResizeRows = True

            dgvPublicNotice.Columns("strApplicationNumber").HeaderText = "APL #"
            dgvPublicNotice.Columns("strApplicationNumber").DisplayIndex = 0
            dgvPublicNotice.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvPublicNotice.Columns("strFacilityName").DisplayIndex = 1
            dgvPublicNotice.Columns("strFacilityName").Width = 250
            dgvPublicNotice.Columns("strCountyName").HeaderText = "County"
            dgvPublicNotice.Columns("strCountyName").DisplayIndex = 2
            dgvPublicNotice.Columns("strCountyName").Width = 125
            dgvPublicNotice.Columns("AppType").HeaderText = "APL Type"
            dgvPublicNotice.Columns("AppType").DisplayIndex = 3
            dgvPublicNotice.Columns("AppType").Width = 100
            dgvPublicNotice.Columns("PAReady").HeaderText = "PA Ready"
            dgvPublicNotice.Columns("PAReady").DisplayIndex = 4
            dgvPublicNotice.Columns("PAExpires").HeaderText = "PA Expires"
            dgvPublicNotice.Columns("PAExpires").DisplayIndex = 5
            dgvPublicNotice.Columns("strPAPosted").HeaderText = "PA Posted"
            dgvPublicNotice.Columns("strPAPosted").DisplayIndex = 6
            dgvPublicNotice.Columns("PNReady").HeaderText = "PN Ready"
            dgvPublicNotice.Columns("PNReady").DisplayIndex = 7
            dgvPublicNotice.Columns("PNExpires").HeaderText = "PN Expires"
            dgvPublicNotice.Columns("PNExpires").DisplayIndex = 8
            dgvPublicNotice.Columns("strPNPosted").HeaderText = "PN Posted"
            dgvPublicNotice.Columns("strPNPosted").DisplayIndex = 9

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".LoadPublicNoticesList")
        Finally

        End Try

    End Sub
    Sub LoadOldPDFs()
        Try

            cboPAPNReports.Items.Clear()

            SQL = "select strFileName " & _
            "from AIRBRANCH.SSPPPublicLetters " & _
            "order by datPublishedDate "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                Me.cboPAPNReports.Items.Add(dr.Item("strFileName"))
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".LoadOldPDFs")
        Finally

        End Try
    End Sub
#End Region
#Region "Subs and Functions"
    Sub CreatePublicNoticeList()
        Try
            Dim strObject As Object = ""
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""

            Dim AppNumbers As String = ""
            Dim SIPAppNumbers As String = ""
            Dim TVAppNumbers As String = ""

            Dim temp As String = ""
            Dim i As Integer

            For i = 0 To dgvPublicNotice.RowCount - 1
                strObject = dgvPublicNotice(0, i).Value.ToString
                AppNumbers = AppNumbers & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & strObject & "' or "
            Next

            AppNumbers = "And ( " & Mid(AppNumbers, 1, (AppNumbers.Length - 3)) & " ) "

            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
            "strCountyName, " & _
            "case " & _
            "   when strApplicationType is Null then '' " & _
            "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
            "End AppType " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
            "AIRBRANCH.SSPPApplicationTracking, " & _
            "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
             "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
            "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
            "and strPAReady = 'True' " & _
            "and strPAPosted is Null " & _
            "and datPAExpires is Null " & _
            "and strPublicInvolvement = '1' " & _
            AppNumbers

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                SIPAppNumbers = SIPAppNumbers & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & dr.Item("strApplicationNumber") & "' or "
            End While
            dr.Close()

            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
            "strCountyName, " & _
            "case " & _
            "   when strApplicationType is Null then '' " & _
            "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
            "End AppType " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
            "AIRBRANCH.SSPPApplicationTracking, " & _
            "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
            "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
            "and strPNPosted is Null " & _
            "and (strApplicationType = '14' or strApplicationType = '16' " & _
            "or strApplicationType = '21' or strApplicationType = '22') " & _
            "and (datPNExpires > (sysdate + 24) and datPNExpires < (sysdate + 37)) " & _
             AppNumbers

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                TVAppNumbers = TVAppNumbers & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & dr.Item("strApplicationNumber") & "' or "
            End While
            dr.Close()

            If SIPAppNumbers <> "" Then
                If lsbApplicationList.Items.Contains("Public Advisories") Then

                Else
                    lsbApplicationList.Items.Add("Public Advisories")
                    lsbApplicationList.Items.Add(" ")
                End If

                SIPAppNumbers = " AND (" & Mid(SIPAppNumbers, 1, (SIPAppNumbers.Length) - 3) & " ) "

                SQL = "Select " & _
                "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
                "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
                "strCountyName, " & _
                "case " & _
                "   when strApplicationType is Null then '' " & _
                "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
                "End AppType " & _
                "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
                "AIRBRANCH.SSPPApplicationTracking, " & _
                "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
                SIPAppNumbers & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = ""
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        AppNum = ""
                    Else
                        AppNum = dr.Item("strApplicationNumber")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        County = ""
                    Else
                        County = dr.Item("strCountyName")
                    End If
                    If IsDBNull(dr.Item("AppType")) Then
                        AppType = ""
                    Else
                        AppType = dr.Item("AppType")
                    End If

                    temp = AppNum & " - " & FacilityName
                    Select Case temp.Length
                        Case 10 To 15
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 16 To 23
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 24 To 31
                            temp = temp & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 32 To 39
                            temp = temp & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 40 To 47
                            temp = temp & vbTab & vbTab & " (" & County & ") "
                        Case 48 To 60
                            temp = temp & vbTab & " (" & County & ") "
                        Case 61 To 70
                            temp = temp & " (" & County & ") "
                        Case Else
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                    End Select

                    Select Case County.Length
                        Case 1 To 10
                            temp = temp & vbTab & vbTab & "App Type: " & AppType
                        Case 11 To 20
                            temp = temp & vbTab & "App Type: " & AppType
                        Case Else
                            temp = temp & vbTab & vbTab & "App Type: " & AppType
                    End Select

                    If lsbApplicationList.Items.Contains(temp) Then
                    Else
                        If lsbApplicationList.Items.Contains("Public Advisories") Then
                            If lsbApplicationList.Items.Contains("Public Noticies") Then
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Noticies") - 1, temp)
                            Else
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf(" "), temp)
                            End If
                        Else
                            If lsbApplicationList.Items.Contains("Public Noticies") Then
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Noticies") - 1, temp)
                            Else
                                lsbApplicationList.Items.Insert(0, "Public Advisories")
                                lsbApplicationList.Items.Insert(1, temp)
                                lsbApplicationList.Items.Insert(2, " ")
                            End If
                        End If
                    End If
                End While
            End If

            If TVAppNumbers <> "" Then
                If lsbApplicationList.Items.Contains("Public Noticies") Then

                Else
                    lsbApplicationList.Items.Add("Public Noticies")
                End If

                TVAppNumbers = " AND (" & Mid(TVAppNumbers, 1, (TVAppNumbers.Length) - 3) & " ) "

                SQL = "Select " & _
                "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
                "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
                "strCountyName, " & _
                "case " & _
                "   when strApplicationType is Null then '' " & _
                "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
                "End AppType " & _
                "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
                "AIRBRANCH.SSPPApplicationTracking, " & _
                "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
                TVAppNumbers & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = ""
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        AppNum = ""
                    Else
                        AppNum = dr.Item("strApplicationNumber")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        County = ""
                    Else
                        County = dr.Item("strCountyName")
                    End If
                    If IsDBNull(dr.Item("AppType")) Then
                        AppType = ""
                    Else
                        AppType = dr.Item("AppType")
                    End If

                    temp = AppNum & " - " & FacilityName
                    Select Case temp.Length
                        Case 10 To 15
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 16 To 23
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 24 To 31
                            temp = temp & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 32 To 39
                            temp = temp & vbTab & vbTab & vbTab & " (" & County & ") "
                        Case 40 To 47
                            temp = temp & vbTab & vbTab & " (" & County & ") "
                        Case 48 To 60
                            temp = temp & vbTab & " (" & County & ") "
                        Case 61 To 70
                            temp = temp & " (" & County & ") "
                        Case Else
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                    End Select

                    Select Case County.Length
                        Case 1 To 10
                            temp = temp & vbTab & vbTab & "App Type: " & AppType
                        Case 11 To 20
                            temp = temp & vbTab & "App Type: " & AppType
                        Case Else
                            temp = temp & vbTab & vbTab & "App Type: " & AppType
                    End Select

                    If lsbApplicationList.Items.Contains(temp) Then
                    Else
                        If lsbApplicationList.Items.Contains("Public Noticies") Then
                            lsbApplicationList.Items.Add(temp)
                        Else
                            lsbApplicationList.Items.Add("Public Noticies")
                            lsbApplicationList.Items.Add(temp)
                        End If
                    End If
                End While
            End If

            If SIPAppNumbers = "" And TVAppNumbers = "" Then
                lsbApplicationList.Items.Add("No Public Advisories and No Public Noticies")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".CreatePublicNoticeList")
        Finally

        End Try

    End Sub
    Sub AddToApplicationList()
        Try
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""
            Dim temp As String = ""

            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
            "strCountyName, " & _
            "case " & _
            "   when strApplicationType is Null then '' " & _
            "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
            "End AppType, " & _
            "case " & _
            "   when strPAReady is Null then '' " & _
             "   when strPAReady = 'True' then 'PA Ready' " & _
            "   when strPAReady = 'False' then '' " & _
            "   Else ''  " & _
            "End PAReady,  " & _
            "case " & _
            "   when strPNReady is Null then '' " & _
            "   when strPNReady = 'True' then 'PN Ready' " & _
            "   when strPNReady = 'False' then 'PN Ready' " & _
            "   Else ''  " & _
            "End PNReady,  " & _
            "datPNExpires  " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
            "AIRBRANCH.SSPPApplicationTracking, " & _
            "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
            "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberEditor.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                temp = ""
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = ""
                Else
                    FacilityName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = ""
                Else
                    County = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("AppType")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("AppType")
                End If
            End If

            dr.Close()

            temp = AppNum & " - " & FacilityName
            Select Case temp.Length
                Case 10 To 23
                    temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 24 To 31
                    temp = temp & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 32 To 40
                    temp = temp & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 41 To 50
                    temp = temp & vbTab & vbTab & " (" & County & ") "
                Case Else
                    temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
            End Select

            Select Case County.Length
                Case 1 To 10
                    temp = temp & vbTab & vbTab & "App Type: " & AppType
                Case 11 To 20
                    temp = temp & vbTab & "App Type: " & AppType
                Case Else
                    temp = temp & vbTab & vbTab & "App Type: " & AppType
            End Select

            If lsbApplicationList.Items.Contains(temp) Then
                MsgBox("The list already contains this application.", MsgBoxStyle.Information, "SSPP Public Noticies And Advisories")
            Else
                If rdbPublicAdvisories.Checked = True Then
                    If lsbApplicationList.Items.Contains("Public Advisories") Then
                        If lsbApplicationList.Items.Contains("Public Noticies") Then
                            lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Noticies") - 1, temp)
                        Else
                            lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf(" ") - 1, temp)
                            'lsbApplicationList.Items.Add(temp)
                        End If
                    Else
                        lsbApplicationList.Items.Insert(0, "Public Advisories")
                        lsbApplicationList.Items.Insert(1, temp)
                        lsbApplicationList.Items.Insert(2, " ")
                    End If
                Else
                    If lsbApplicationList.Items.Contains("Public Noticies") Then
                        lsbApplicationList.Items.Add(temp)
                    Else
                        lsbApplicationList.Items.Add("Public Noticies")
                        lsbApplicationList.Items.Add(temp)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".AddToApplicationList")
        Finally

        End Try

    End Sub
    Sub RemoveFromApplicationList()
        Try
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""
            Dim temp As String = ""

            SQL = "Select " & _
            "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber, " & _
            "AIRBRANCH.SSPPApplicationData.strFacilityName, " & _
            "strCountyName, " & _
            "case " & _
            "   when strApplicationType is Null then '' " & _
            "   else AIRBRANCH.LookUpApplicationTypes.strApplicationTypeDesc " & _
            "End AppType, " & _
            "case " & _
            "   when strPAReady is Null then '' " & _
             "   when strPAReady = 'True' then 'PA Ready' " & _
            "   when strPAReady = 'False' then '' " & _
            "   Else ''  " & _
            "End PAReady,  " & _
            "case " & _
            "   when strPNReady is Null then '' " & _
            "   when strPNReady = 'True' then 'PN Ready' " & _
            "   when strPNReady = 'False' then 'PN Ready' " & _
            "   Else ''  " & _
            "End PNReady,  " & _
            "datPNExpires  " & _
            "from AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.SSPPApplicationData,  " & _
            "AIRBRANCH.SSPPApplicationTracking, " & _
            "AIRBRANCH.LookUpApplicationTypes, AIRBRANCH.LookUpCountyInformation  " & _
            "where AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber  " & _
            "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationType = AIRBRANCH.LookUpApplicationTypes.strApplicationTypeCode (+)  " & _
            "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberEditor.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                temp = ""
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNum = ""
                Else
                    AppNum = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = ""
                Else
                    FacilityName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = ""
                Else
                    County = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("AppType")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("AppType")
                End If
            End If

            dr.Close()

            temp = AppNum & " - " & FacilityName
            Select Case temp.Length
                Case 10 To 23
                    temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 24 To 31
                    temp = temp & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 32 To 40
                    temp = temp & vbTab & vbTab & vbTab & " (" & County & ") "
                Case 41 To 50
                    temp = temp & vbTab & vbTab & " (" & County & ") "
                Case Else
                    temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " (" & County & ") "
            End Select

            Select Case County.Length
                Case 1 To 10
                    temp = temp & vbTab & vbTab & "App Type: " & AppType
                Case 11 To 20
                    temp = temp & vbTab & "App Type: " & AppType
                Case Else
                    temp = temp & vbTab & vbTab & "App Type: " & AppType
            End Select

            If lsbApplicationList.Items.Contains(temp) Then
                lsbApplicationList.Items.Remove(temp)
            Else
                MsgBox("The list does not contains this application.", MsgBoxStyle.Information, "SSPP Public Noticies And Advisories")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".RemoveFromApplicationList")
        Finally

        End Try

    End Sub
    Sub FillApplicationList()
        Try
            Dim temp As String
            Dim i As Integer

            Dim PublicAd As String
            Dim PublicNo As String

            lsbPublicAdvisories.Items.Clear()
            lsbPublicNoticies.Items.Clear()

            If lsbApplicationList.Items.Contains("Public Advisories") Then
                PublicAd = lsbApplicationList.Items.IndexOf("Public Advisorires")
            Else
                PublicAd = "X"
            End If
            If lsbApplicationList.Items.Contains("Public Noticies") Then
                PublicNo = lsbApplicationList.Items.IndexOf("Public Noticies")
            Else
                PublicNo = "X"
            End If
            If PublicAd <> "X" Then
                If PublicNo <> "X" Then
                    For i = 1 To PublicNo - 2
                        temp = lsbApplicationList.Items.Item(i)
                        lsbPublicAdvisories.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                    Next
                Else
                    For i = 1 To lsbApplicationList.Items.Count - 1
                        temp = lsbApplicationList.Items.Item(i)
                        If temp <> " " Then
                            lsbPublicAdvisories.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                        End If
                    Next
                End If
            End If
            If PublicNo <> "X" Then
                For i = PublicNo + 1 To lsbApplicationList.Items.Count - 1
                    temp = lsbApplicationList.Items.Item(i)
                    If temp <> " " Then
                        lsbPublicNoticies.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                    End If
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".FillApplicationList")
        Finally

        End Try

    End Sub
    Sub OpenApplication()
        OpenFormPermitApplication(txtApplicationNumber.Text)
    End Sub
    Sub PreviewReport()
        Dim SQLLine As String = ""
        Dim i As Integer

        Dim PANeeded As String = ""
        Dim PublicAdvisories As String = ""
        Dim TVAdvisories As String = ""
        Dim TVInitial As String = ""
        Dim TVRenewal As String = ""
        Dim TVSigMod As String = ""
        Dim Deadline As String = ""

        Try
            If lsbPublicAdvisories.Items.Count > 0 Then
                For i = 0 To lsbPublicAdvisories.Items.Count - 1
                    SQLLine = SQLLine & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & lsbPublicAdvisories.Items.Item(i) & " or "
                Next
                SQLLine = "and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "

                SQL = "select " & _
                "AIRBRANCH.SSPPApplicationData.strApplicationNumber, " & _
                "strPAReady, strFacilityName, " & _
                "strFacilityStreet1, strFacilityCity, " & _
                "strFacilityState, strFacilityZipCode, " & _
                "strPlantDescription,  " & _
                "strApplicationNotes, strCountyName    " & _
                "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationTracking, " & _
                "AIRBRANCH.SSPPApplicationMaster, AIRBRANCH.LookUpCountyInformation " & _
                "where AIRBRANCH.SSPPApplicationData.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationData.strApplicatioNNumber " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode " & _
                SQLLine & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strCountyName")) Then
                        PANeeded = PANeeded & "County Unknown" & vbCrLf & vbCrLf
                    Else
                        PANeeded = PANeeded & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                    End If

                    If IsDBNull(dr.Item("strFacilityName")) Then
                        PANeeded = PANeeded & "Facility Name:X Unknown" & vbCrLf
                    Else
                        PANeeded = PANeeded & "Facility Name:X " & dr.Item("strFacilityName") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        PANeeded = PANeeded & "Application No:X Unknown" & vbCrLf
                    Else
                        PANeeded = PANeeded & "Application No:X " & dr.Item("strApplicationNumber") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        PANeeded = PANeeded & "Facility Address:X Unknown, "
                    Else
                        PANeeded = PANeeded & "Facility Address:X " & dr.Item("strFacilityStreet1") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        PANeeded = PANeeded & " Unknown, "
                    Else
                        PANeeded = PANeeded & dr.Item("strFacilityCity") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        PANeeded = PANeeded & " Unknown "
                    Else
                        PANeeded = PANeeded & dr.Item("strFacilityZipCode") & " "
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        PANeeded = PANeeded & "(Unknown County) " & vbCrLf
                    Else
                        PANeeded = PANeeded & "(" & dr.Item("strCountyName") & ")" & vbCrLf
                    End If
                    PANeeded = PANeeded & "EPD Notice Type:X Permit - Application. " & vbCrLf
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        PANeeded = PANeeded & "Description of Operation:X Unknown" & vbCrLf
                    Else
                        PANeeded = PANeeded & "Description of Operation:X " & dr.Item("strPlantDescription") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNotes")) Then
                        PANeeded = PANeeded & "Reason for Application:X Unknown " & vbCrLf & vbCrLf
                    Else
                        PANeeded = PANeeded & "Reason for Application:X " & dr.Item("strApplicationNotes") & vbCrLf & vbCrLf
                    End If
                End While
                dr.Close()
            End If

            SQLLine = ""
            If lsbPublicNoticies.Items.Count > 0 Then
                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    SQLLine = SQLLine & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & lsbPublicNoticies.Items.Item(i) & " or "
                Next
                SQLLine = "and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "

                SQL = "Select  " & _
                "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                "strCountyName, strFacilityName,  " & _
                "strFacilityStreet1, strFacilityCity,  " & _
                "strFacilityState, strFacilityZipCode,  " & _
                "strPlantDescription, strApplicationNotes,  " & _
                "to_char(datPNExpires, 'Monthdd, YYYY') as datPNExpires " & _
                "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster, " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.SSPPApplicationTracking " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationnumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber   " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
                "and strApplicationType = '14'  " & _
                SQLLine & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVInitial = TVInitial & "County Unknown" & vbCrLf & vbCrLf
                    Else
                        TVInitial = TVInitial & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        TVInitial = TVInitial & "Facility Name:X Unknown" & vbCrLf
                    Else
                        TVInitial = TVInitial & "Facility Name:X " & dr.Item("strFacilityName") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        TVInitial = TVInitial & "Application No:X Unknown" & vbCrLf
                    Else
                        TVInitial = TVInitial & "Application No:X " & dr.Item("strApplicationNumber") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        TVInitial = TVInitial & "Facility Address:X Unknown, "
                    Else
                        TVInitial = TVInitial & "Facility Address:X " & dr.Item("strFacilityStreet1") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        TVInitial = TVInitial & " Unknown, "
                    Else
                        TVInitial = TVInitial & dr.Item("strFacilityCity") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        TVInitial = TVInitial & " Unknown "
                    Else
                        TVInitial = TVInitial & dr.Item("strFacilityZipCode") & " "
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVInitial = TVInitial & "(Unknown County) " & vbCrLf
                    Else
                        TVInitial = TVInitial & "(" & dr.Item("strCountyName") & ")" & vbCrLf
                    End If
                    TVInitial = TVInitial & "EPD Notice Type:X Permit - Proposed. " & vbCrLf

                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        TVInitial = TVInitial & "Description of Operation:X Unknown" & vbCrLf
                    Else
                        TVInitial = TVInitial & "Description of Operation:X " & dr.Item("strPlantDescription") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("datPNExpires")) Then
                        TVInitial = TVInitial & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                    Else
                        TVInitial = TVInitial & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires") & vbCrLf & vbCrLf
                    End If
                End While
                dr.Close()
            End If

            SQLLine = ""
            If lsbPublicNoticies.Items.Count > 0 Then
                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    SQLLine = SQLLine & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & lsbPublicNoticies.Items.Item(i) & " or "
                Next
                SQLLine = "and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "

                SQL = "Select  " & _
                "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                "strCountyName, strFacilityName,  " & _
                "strFacilityStreet1, strFacilityCity,  " & _
                "strFacilityState, strFacilityZipCode,  " & _
                "strPlantDescription, strApplicationNotes,  " & _
                "to_char(datPNExpires, 'Monthdd, YYYY') as datPNExpires " & _
                "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster,   " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.SSPPApplicationTracking " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationnumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber   " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
                SQLLine & _
                "and strApplicationType = '16'  " & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVRenewal = TVRenewal & "County Unknown" & vbCrLf & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        TVRenewal = TVRenewal & "Facility Name:X Unknown" & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "Facility Name:X " & dr.Item("strFacilityName") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        TVRenewal = TVRenewal & "Application No:X Unknown" & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "Application No:X " & dr.Item("strApplicationNumber") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        TVRenewal = TVRenewal & "Facility Address:X Unknown, "
                    Else
                        TVRenewal = TVRenewal & "Facility Address:X " & dr.Item("strFacilityStreet1") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        TVRenewal = TVRenewal & " Unknown, "
                    Else
                        TVRenewal = TVRenewal & dr.Item("strFacilityCity") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        TVRenewal = TVRenewal & " Unknown "
                    Else
                        TVRenewal = TVRenewal & dr.Item("strFacilityZipCode") & " "
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVRenewal = TVRenewal & "(Unknown County) " & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "(" & dr.Item("strCountyName") & ")" & vbCrLf
                    End If
                    TVRenewal = TVRenewal & "EPD Notice Type:X Permit - Proposed. " & vbCrLf

                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        TVRenewal = TVRenewal & "Description of Operation:X Unknown" & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "Description of Operation:X " & dr.Item("strPlantDescription") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("datPNExpires")) Then
                        TVRenewal = TVRenewal & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                    Else
                        TVRenewal = TVRenewal & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires") & vbCrLf & vbCrLf
                    End If
                End While
                dr.Close()
            End If

            SQLLine = ""
            If lsbPublicNoticies.Items.Count > 0 Then
                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    SQLLine = SQLLine & " AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = " & lsbPublicNoticies.Items.Item(i) & " or "
                Next
                SQLLine = "and ( " & Mid(SQLLine, 1, (SQLLine.Length) - 3) & " ) "

                SQL = "Select  " & _
                "AIRBRANCH.SSPPApplicationMaster.strApplicationNumber,  " & _
                "strCountyName, strFacilityName,  " & _
                "strFacilityStreet1, strFacilityCity,  " & _
                "strFacilityState, strFacilityZipCode,  " & _
                "strPlantDescription, strApplicationNotes,  " & _
                "to_char(datPNExpires, 'Monthdd, YYYY') as datPNExpires, " & _
                "strSignificantComments " & _
                "from AIRBRANCH.SSPPApplicationData, AIRBRANCH.SSPPApplicationMaster,   " & _
                "AIRBRANCH.LookUpCountyInformation, AIRBRANCH.SSPPApplicationTracking " & _
                "where AIRBRANCH.SSPPApplicationMaster.strApplicationnumber = AIRBRANCH.SSPPApplicationData.strApplicationNumber   " & _
                "and substr(AIRBRANCH.SSPPApplicationMaster.strAIRSNumber, 5, 3) = AIRBRANCH.LookUpCountyInformation.strCountyCode  " & _
                "and AIRBRANCH.SSPPApplicationMaster.strApplicationNumber = AIRBRANCH.SSPPApplicationTracking.strApplicationNumber " & _
                SQLLine & _
                "and (strApplicationType = '21' or strApplicationType = '22')  " & _
                "order by strCountyName "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVSigMod = TVSigMod & "County Unknown" & vbCrLf & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "*X" & dr.Item("strCountyName").ToString.ToUpper & "X*" & vbCrLf & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        TVSigMod = TVSigMod & "Facility Name:X Unknown" & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Facility Name:X " & dr.Item("strFacilityName") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        TVSigMod = TVSigMod & "Application No:X Unknown" & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Application No:X " & dr.Item("strApplicationNumber") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        TVSigMod = TVSigMod & "Facility Address:X Unknown, "
                    Else
                        TVSigMod = TVSigMod & "Facility Address:X " & dr.Item("strFacilityStreet1") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        TVSigMod = TVSigMod & " Unknown, "
                    Else
                        TVSigMod = TVSigMod & dr.Item("strFacilityCity") & ", "
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        TVSigMod = TVSigMod & " Unknown "
                    Else
                        TVSigMod = TVSigMod & dr.Item("strFacilityZipCode") & " "
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        TVSigMod = TVSigMod & "(Unknown County) " & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "(" & dr.Item("strCountyName") & ")" & vbCrLf
                    End If
                    TVSigMod = TVSigMod & "EPD Notice Type:X Permit - Proposed. " & vbCrLf
                    If IsDBNull(dr.Item("strPlantDescription")) Then
                        TVSigMod = TVSigMod & "Description of Operation:X Unknown" & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Description of Operation:X " & dr.Item("strPlantDescription") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strSignificantComments")) Then
                        TVSigMod = TVSigMod & "Emission Increase/Decrease:X (Unknown) " & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Emission Increase/Decrease:X " & dr.Item("strSignificantComments") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("strApplicationNotes")) Then
                        TVSigMod = TVSigMod & "Description of Requested Modification/Change:X N/A " & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Description of Requested Modification/Change:X " & dr.Item("strApplicationNotes") & vbCrLf
                    End If
                    If IsDBNull(dr.Item("datPNExpires")) Then
                        TVSigMod = TVSigMod & "Comment period/deadline for public hearing request expires on:X (Unknown Date) " & vbCrLf & vbCrLf
                    Else
                        TVSigMod = TVSigMod & "Comment period/deadline for public hearing request expires on:X " & dr.Item("datPNExpires") & vbCrLf & vbCrLf
                    End If
                End While
                dr.Close()
            End If

            If DTPPADeadline.Checked = False Then
                Deadline = "PUBLICATION DEADLINE"
            Else
                Deadline = Format(CDate(DTPPADeadline.Text), "dd-MMMM-yyyy")
            End If

            PublicAdvisories = "EPD PUBLIC ADVISORY" & _
            vbCrLf & "GEORGIA AIR PROTECTION BRANCH" & _
            vbCrLf & vbCrLf & vbCrLf & "SIP PUBLIC ADVISORIES" & _
            vbCrLf & vbCrLf & "The following applications have been received for Air Quality Permits. " & vbCrLf & _
            "These applications are presently under review. Any comments should be received by " & Deadline & vbCrLf & vbCrLf & vbCrLf

            If PANeeded <> "" Then
                PublicAdvisories = PublicAdvisories & PANeeded & vbCrLf
            Else
                PublicAdvisories = PublicAdvisories & "NO PUBLIC ADVISORIESX" & vbCrLf & vbCrLf
            End If

            PublicAdvisories = PublicAdvisories & "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf & _
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf & _
            "4244 International Parkway, Suite 120, Atlanta, Georgia 30354, " & vbCrLf & "(404) 363-7000" & vbCrLf & vbCrLf & vbCrLf

            TVAdvisories = "NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS " & vbCrLf & _
            "GEORGIA ENVIRONMENTAL PROTECTION DIVISION " & vbCrLf & _
            "AIR PROTECTION BRANCHX" & vbCrLf & _
            "4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354 " & vbCrLf & vbCrLf & _
            "The Georgia Environmental Protection Division announces its intent to " & vbCrLf & _
            "issue initial Title V Operating Permits, Title V Significant " & vbCrLf & _
            "modifications, Title V Operating Permit Renewals, and/or other Title V " & vbCrLf & _
            "Permit proceedings for the following facilities. The deadlines for " & vbCrLf & _
            "submitting comments and requesting a public hearing are specified for " & vbCrLf & "each facility. " & vbCrLf & vbCrLf

            If TVInitial <> "" Or TVRenewal <> "" Or TVSigMod <> "" Then
                If TVInitial <> "" Then
                    TVAdvisories = TVAdvisories & "INITIAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf & _
                    TVInitial & vbCrLf
                End If
                If TVRenewal <> "" Then
                    TVAdvisories = TVAdvisories & "RENEWAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf & _
                    TVRenewal & vbCrLf
                End If
                If TVSigMod <> "" Then
                    TVAdvisories = TVAdvisories & "TITLE V SIGNIFICANT MODIFICATIONSX" & vbCrLf & vbCrLf & _
                    TVSigMod & vbCrLf
                End If
            Else
                TVAdvisories = TVAdvisories & "NO TITLE V ADVISORIESX" & vbCrLf & vbCrLf
            End If

            TVAdvisories = TVAdvisories & "ADDITIONAL INFORMATIONX: The draft permits and permit amendments and " & vbCrLf & _
            "all information used to develop the draft permits and permit amendments " & vbCrLf & _
            "are available for review. This includes the application, all relevant " & vbCrLf & _
            "supporting materials and all other materials available to the permitting " & vbCrLf & _
            "authority used in the permit review process. This information is " & vbCrLf & _
            "available for review at the office of the Air Protection Branch, " & vbCrLf & _
            "4244 International Parkway, Atlanta Tradeport - Suite 120, Atlanta, Georgia 30354. " & vbCrLf & _
            "Copies of the draft permits or permit amendments, narratives, " & vbCrLf & _
            "application summaries, and (in most cases) permit applications are also " & vbCrLf & _
            "available at our Internet site, epd.georgia.gov/air . Also " & vbCrLf & _
            "available at this Internet site is a copy of the public notice, as it " & vbCrLf & _
            "will appear in the legal organ of the county where the facility is " & vbCrLf & _
            "located. " & vbCrLf & vbCrLf & _
            "If a permit application is not available at our Internet site, the " & vbCrLf & _
            "public notice will indicate where a copy of these documents will be " & vbCrLf & _
            "available at a location near the facility. " & vbCrLf & vbCrLf & _
            "Persons wishing to comment on a draft Initial Title V Operating Permit, " & vbCrLf & _
            "Title V Significant modification, Title V Operating Permit Renewal, or " & vbCrLf & _
            "other Title V Permit proceedings are required to submit their comments, " & vbCrLf & _
            "in writing, to EPD at the above Atlanta Air Protection Branch address. " & vbCrLf & _
            "Comments must be received by no later than the deadline indicated for " & vbCrLf & _
            "the particular facility. (Should the comment period end on a weekend or " & vbCrLf & _
            "holiday, comments will be accepted up until the next working day.) All " & vbCrLf & _
            "comments received on or prior to the deadline will be considered by the " & vbCrLf & _
            "Division in making its final decision to issue the Title V permit or " & vbCrLf & _
            "permit amendment." & vbCrLf & vbCrLf & _
            "Any requests for a public hearing must be made prior to the deadline " & vbCrLf & _
            "indicated for the particular facility. A request for a hearing should " & vbCrLf & _
            "be in writing and should specify, in as much detail as possible, the " & vbCrLf & _
            "portion of the Georgia Rules for Air Quality Control or the Federal " & vbCrLf & _
            "Rules which the individual making the request is concerned may not have " & vbCrLf & _
            "been adequately incorporated. A public hearing may be held if the " & vbCrLf & _
            "Director of the EPD finds that such a hearing would assist the EPD in a " & vbCrLf & _
            "proper review of the facility's ability to comply with the Federal and " & vbCrLf & _
            "State air quality regulations. " & vbCrLf & vbCrLf & _
            "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf & _
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf & _
            "4244 International Parkway, Suite 120, " & vbCrLf & _
            "Atlanta, Georgia 30354, (404) 363-7000" & vbCrLf & vbCrLf & vbCrLf & _
            "--------------------------------------------------" & vbCrLf

            txtPublicNoticeDocument.Text = PublicAdvisories & TVAdvisories

            FormatReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".PreviewReport")
        Finally

        End Try

    End Sub
    Sub FormatReport()
        Try

            Dim bfont As New Font(txtPublicNoticeDocument.Font, FontStyle.Bold)
            Dim ufont As New Font(txtPublicNoticeDocument.Font, 5)

            Dim tempStart As String
            Dim tempEnd As String
            Dim temp As String
            Dim temp2 As String

            Do While txtPublicNoticeDocument.Text.Contains("*X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("*X")
                tempStart = txtPublicNoticeDocument.Find("*X")
                tempEnd = txtPublicNoticeDocument.Find("X*")
                temp = Mid(txtPublicNoticeDocument.Text, tempStart + 1, (tempEnd - tempStart) + 2)
                temp2 = Replace(temp, "*X", "")
                temp2 = Replace(temp2, "X*", "")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find(temp)
                txtPublicNoticeDocument.SelectionFont = ufont
                ' txtPublicLetter.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = temp2
            Loop

            If txtPublicNoticeDocument.Text.Contains("EPD PUBLIC ADVISORY") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("EPD PUBLIC ADVISORY")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            If txtPublicNoticeDocument.Text.Contains("GEORGIA AIR PROTECTION BRANCH") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("GEORGIA AIR PROTECTION BRANCH")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            If txtPublicNoticeDocument.Text.Contains("SIP PUBLIC ADVISORIES") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("SIP PUBLIC ADVISORIES")
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            If txtPublicNoticeDocument.Text.Contains("Any comments should be received by") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Any comments should be received by")
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            If txtPublicNoticeDocument.Text.Contains("NO PUBLIC ADVISORIESX") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("NO PUBLIC ADVISORIESX")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "NO PUBLIC ADVISORIES"
            End If

            Do While txtPublicNoticeDocument.Text.Contains("Facility Name:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Facility Name:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Facility Name:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Application No:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Application No:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Application No:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Facility Address:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Facility Address:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Facility Address:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("EPD Notice Type:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("EPD Notice Type:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "EPD Notice Type:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Description of Operation:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Description of Operation:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Description of Operation:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Reason for Application:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Reason for Application:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Reason for Application:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Comment period/deadline for public hearing request expires on:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Comment period/deadline for public hearing request expires on:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Comment period/deadline for public hearing request expires on:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Description of Requested Modification/Change:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Description of Requested Modification/Change:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Description of Requested Modification/Change:"
            Loop

            Do While txtPublicNoticeDocument.Text.Contains("Emission Increase/Decrease:X")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("Emission Increase/Decrease:X")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "Emission Increase/Decrease:"
            Loop

            If txtPublicNoticeDocument.Text.Contains("NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            If txtPublicNoticeDocument.Text.Contains("GEORGIA ENVIRONMENTAL PROTECTION DIVISION") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("GEORGIA ENVIRONMENTAL PROTECTION DIVISION")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
            End If

            Do While txtPublicNoticeDocument.Text.Contains("AIR PROTECTION BRANCHX")
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("AIR PROTECTION BRANCHX")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "AIR PROTECTION BRANCH"
            Loop

            If txtPublicNoticeDocument.Text.Contains("4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354")
                txtPublicNoticeDocument.SelectionAlignment = HorizontalAlignment.Center
                txtPublicNoticeDocument.SelectionFont = bfont
            End If



            If txtPublicNoticeDocument.Text.Contains("INITIAL TITLE V OPERATING PERMITS") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("INITIAL TITLE V OPERATING PERMITSX")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "INITIAL TITLE V OPERATING PERMITS"
            End If

            If txtPublicNoticeDocument.Text.Contains("RENEWAL TITLE V OPERATING PERMITS") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("RENEWAL TITLE V OPERATING PERMITSX")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "RENEWAL TITLE V OPERATING PERMITS"
            End If

            If txtPublicNoticeDocument.Text.Contains("TITLE V SIGNIFICANT MODIFICATIONS") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("TITLE V SIGNIFICANT MODIFICATIONSX")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "TITLE V SIGNIFICANT MODIFICATIONS"
            End If

            If txtPublicNoticeDocument.Text.Contains("NO TITLE V ADVISORIES") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("NO TITLE V ADVISORIESX")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "NO TITLE V ADVISORIES"
            End If

            If txtPublicNoticeDocument.Text.Contains("ADDITIONAL INFORMATIONX:") Then
                txtPublicNoticeDocument.SelectionStart = txtPublicNoticeDocument.Find("ADDITIONAL INFORMATIONX:")
                txtPublicNoticeDocument.SelectionFont = bfont
                txtPublicNoticeDocument.SelectedText = "ADDITIONAL INFORMATION:"
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".FormatReport")
        Finally

        End Try

    End Sub
    Sub GenerateFileName()
        Try
            Dim FileMonth As String = ""
            Dim FileYear As String = ""
            Dim FileWeek As String = ""
            Dim FileName As String = ""
            Dim Flag As Boolean = False

            FileMonth = Date.Today.Month
            FileYear = Date.Today.Year
            FileWeek = Date.Today.Day

            If FileMonth.Length = 1 Then
                FileMonth = "0" & FileMonth
            Else
                FileMonth = FileMonth
            End If

            If FileYear.Length > 3 Then
                FileYear = Mid(FileYear, 3)
            Else
                FileYear = FileYear
            End If

            If FileWeek > 0 And FileWeek < 8 Then
                FileWeek = "1"
            Else
                If FileWeek > 7 And FileWeek < 15 Then
                    FileWeek = "2"
                Else
                    If FileWeek > 14 And FileWeek < 22 Then
                        FileWeek = "3"
                    Else
                        If FileWeek > 21 And FileWeek < 29 Then
                            FileWeek = "4"
                        Else
                            FileWeek = "5"
                        End If
                    End If
                End If
            End If

            FileName = "PA" & FileMonth & FileYear & "-" & FileWeek

            Do While Flag = False
                SQL = "select strFileName " & _
                "From AIRBRANCH.SSPPPublicLetters " & _
                "where strFileName = '" & FileName & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    If FileName.Length > 8 Then
                        Select Case Mid(FileName, 9, 1)
                            Case "a"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "b"
                            Case "b"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "c"
                            Case "c"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "d"
                            Case "d"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "e"
                            Case "e"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "f"
                            Case "f"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "g"
                            Case "g"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "h"
                            Case "h"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "i"
                            Case "i"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "j"
                            Case "j"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "k"
                            Case "k"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "l"
                            Case "l"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "m"
                            Case "m"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "n"
                            Case "n"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "o"
                            Case "o"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "p"
                            Case "p"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "q"
                            Case "q"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "r"
                            Case "r"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "s"
                            Case "s"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "t"
                            Case "t"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "u"
                            Case "u"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "v"
                            Case "v"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "w"
                            Case "w"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "x"
                            Case "x"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "y"
                            Case "y"
                                FileName = Mid(FileName, 1, (FileName.Length - 1)) & "z"
                        End Select
                    Else
                        FileName = FileName & "a"
                    End If
                Else
                    Flag = True
                End If
            Loop

            If FileName <> "" Then
                lblFileName.Text = FileName
            Else
                lblFileName.Text = "ERROR"
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".GenerateFileName")
        Finally

        End Try

    End Sub
    Sub UpdateLetter()
        Try
            Dim FileName As String = ""
            Dim DestFilePath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")
            Dim ReviewingManager As String = ""
            Dim ReviewedDate As String = ""
            Dim PublishingStaff As String = ""
            Dim PublishedDate As String = ""
            Dim CommentsDate As String = ""

            If lblFileName.Text <> "pdf File Name" And lblFileName.Text <> "ERROR" Then
                FileName = lblFileName.Text

                SQL = "Select " & _
                "strFileName, strReviewingManager, " & _
                "datReviewed, strPublishingStaff, " & _
                "datPublishedDate, datCommentsDate " & _
                "from AIRBRANCH.SSPPPublicLetters " & _
                "where strFileName = '" & FileName & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist = True Then
                    FileName = FileName
                    If IsDBNull(dr.Item("strReviewingManager")) Then
                        ReviewingManager = ""
                    Else
                        ReviewingManager = dr.Item("strReviewingManager")
                    End If
                    If IsDBNull(dr.Item("datReviewed")) Then
                        ReviewedDate = ""
                    Else
                        ReviewedDate = Format(dr.Item("datReviewed"), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(dr.Item("strPublishingStaff")) Then
                        PublishingStaff = ""
                    Else
                        PublishingStaff = dr.Item("strPublishingStaff")
                    End If
                    If IsDBNull(dr.Item("datPublishedDate")) Then
                        PublishedDate = ""
                    Else
                        PublishedDate = Format(dr.Item("datPublishedDate"), "dd-MMM-yyyy")
                    End If
                Else
                    FileName = FileName
                    ReviewingManager = CurrentUser.UserID
                    ReviewedDate = OracleDate
                    PublishingStaff = CurrentUser.UserID
                    PublishedDate = OracleDate
                    CommentsDate = Format(CDate(DTPPADeadline.Text), "dd-MMM-yyyy").ToString
                End If
                dr.Close()

                If DTPPADeadline.Checked = True Then
                    CommentsDate = Format(CDate(DTPPADeadline.Text), "dd-MMM-yyyy").ToString
                Else
                    CommentsDate = OracleDate
                End If

                If FileName <> "" Then
                    SQL = "Delete AIRBRANCH.SSPPPublicLetters " & _
                    "where strFileName = '" & FileName & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Dim Encoder As New System.Text.ASCIIEncoding
                    Dim bytedata As Byte() = Encoder.GetBytes(txtPublicNoticeDocument.Rtf)

                    Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Write(bytedata, 0, bytedata.Length)
                    fs.Close()

                    Dim da As OracleDataAdapter
                    Dim ds As DataSet

                    SQL = "Select * " & _
                    "from AIRBRANCH.SSPPPublicLetters " & _
                    "where strFileName = '" & FileName & "' "

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    Dim cmdCB As OracleCommandBuilder = New OracleCommandBuilder(da)
                    ds = New DataSet("IAIPData")
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                    da.Fill(ds, "IAIPData")
                    Dim row As DataRow = ds.Tables("IAIPData").NewRow()
                    row("strFileName") = FileName
                    row("BatchFile") = bytedata
                    row("strReviewingManager") = ReviewingManager
                    row("datReviewed") = ReviewedDate
                    row("strPublishingStaff") = PublishingStaff
                    row("datPublishedDate") = PublishedDate
                    row("datcommentsDate") = CommentsDate
                    ds.Tables("IAIPData").Rows.Add(row)
                    da.Update(ds, "IAIPData")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnGenerateReport_Click")
        Finally

        End Try

    End Sub
    Sub UpdateApplications()
        Try
            Dim i As Integer
            Dim SQLLine As String = ""

            If lsbPublicAdvisories.Items.Count > 0 Then
                SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                "strPublicInvolvement = '1', " & _
                "strPAPosted = '" & lblFileName.Text & "' " & _
                "where "

                For i = 0 To lsbPublicAdvisories.Items.Count - 1
                    SQLLine = SQLLine & " strApplicationNumber = '" & lsbPublicAdvisories.Items.Item(i) & "' or "
                Next
                If SQLLine <> "" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))
                End If
                SQL = SQL & SQLLine
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update AIRBRANCH.SSPPApplicationTracking set " & _
                "datPAExpires = '" & Me.DTPPADeadline.Text & "' " & _
                "where " & SQLLine
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            SQL = ""
            SQLLine = ""

            If lsbPublicNoticies.Items.Count > 0 Then
                SQL = "Update AIRBRANCH.SSPPApplicationData set " & _
                "strPublicInvolvement = '1', " & _
                "strPNPosted = '" & lblFileName.Text & "' " & _
                "where "

                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    SQLLine = SQLLine & " strAPplicationNumber = '" & lsbPublicNoticies.Items.Item(i) & "' or "
                Next
                If SQLLine <> "" Then
                    SQLLine = Mid(SQLLine, 1, (SQLLine.Length - 3))
                End If
                SQL = SQL & SQLLine
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".UpdateApplications")
        Finally

        End Try

    End Sub
    Sub ExportPDF()
        Try
            Dim rpt As New SSPPPublicNotice
            monitor.TrackFeature("Report." & rpt.ResourceName)
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "PublicNotice"
            spValue.Value = txtPublicNoticeDocument.Rtf
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRVPublicNotices.ParameterFieldInfo = ParameterFields

            'Display the Report
            CRVPublicNotices.ReportSource = rpt

            CRVPublicNotices.ExportReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".ExportPDF")
        Finally

        End Try

    End Sub
    Sub OpenOldPAPN()
        Try
            Dim DestFilePath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")

            SQL = "Select " & _
            "strFileName, BatchFile, " & _
            "strReviewingManager, " & _
            "datReviewed, strPublishingStaff, " & _
            "datPublishedDate, datCommentsDate " & _
            "from AIRBRANCH.SSPPPublicLetters " & _
            "where strFileName = '" & cboPAPNReports.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                lblPAPNDocumentName.Text = cboPAPNReports.Text
                lblPAPNDocumentName.Visible = True

                If IsDBNull(dr.Item("datCommentsDate")) Then
                    lblPAPNExpiresDate2.Text = OracleDate
                    lblPAPNExpiresDate2.Visible = False
                Else
                    lblPAPNExpiresDate2.Text = Format(CDate(dr.Item("datCommentsdate")), "MMMM dd, yyyy").ToString
                    lblPAPNExpiresDate2.Visible = True
                End If

                Dim byteData As Byte()
                byteData = dr.Item(1)

                Dim ArraySize As Integer = New Integer
                ArraySize = byteData.GetUpperBound(0)

                Dim fs As FileStream = New FileStream(DestFilePath, FileMode.OpenOrCreate, FileAccess.Write)
                fs.Write(byteData, 0, ArraySize)
                fs.Close()

                Dim filepath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")
                If File.Exists(filepath) Then
                    Dim reader As StreamReader = New StreamReader(filepath)
                    Do
                        rtbPAPNDocument2.Rtf = reader.ReadToEnd
                    Loop Until reader.Peek = -1
                    reader.Close()
                End If
            End While

            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".OpenOldPAPN")
        Finally

        End Try
    End Sub
#End Region
#Region "Declarations"
    Private Sub DevPublicNoticiesAndAdvisories_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.Size.Width > 200 Then
                If Me.Size.Width > 0 Then
                    If Me.Size.Width > 800 Then
                        SCGenerate.SanelySetSplitterDistance(SCGenerate.Width * 0.25)
                        SCPublicNoticeTab.SanelySetSplitterDistance(SCPublicNoticeTab.Width * 0.1)
                    Else
                        SCGenerate.SanelySetSplitterDistance(322)
                        SCPublicNoticeTab.SanelySetSplitterDistance(140)
                    End If
                    rtbPAPNDocument2.Width = (Me.Size.Width - 159)
                End If
            End If
            If Me.Size.Height > 200 Then
                If Me.Size.Height > 31 Then
                    If Me.Size.Height > 600 Then
                        SCPreviewAndGenerate.SanelySetSplitterDistance(SCPreviewAndGenerate.Height * 0.4)
                    Else
                        SCPreviewAndGenerate.SanelySetSplitterDistance(230)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".DevPublicNoticiesAndAdvisories_SizeChanged")
        Finally

        End Try
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try

            CreatePublicNoticeList()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnPreview_Click")
        Finally

        End Try

    End Sub
    Private Sub btnEditApplicationList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToApplicationList.Click
        Try

            'Do not allow in unless it is in the upper table!!!!!!!

            If txtApplicationNumberEditor.Text <> "" Then
                If Me.rdbPublicAdvisories.Checked = False And Me.rdbPublicNotice.Checked = False Then
                    MsgBox("You must select either Public Advisory or Public Notice.", MsgBoxStyle.Information, "Public Notice")
                Else
                    AddToApplicationList()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnEditApplicationList_Click")
        Finally

        End Try

    End Sub
    Private Sub btnRemoveFromApplicationList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFromApplicationList.Click
        Try

            If txtApplicationNumberEditor.Text <> "" Then
                RemoveFromApplicationList()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnRemoveFromApplicationList_Click")
        Finally

        End Try

    End Sub
    Private Sub btnGeneratePublicNotice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGeneratePublicNotice.Click
        Try
            If TCPublicNotices.TabPages.Contains(TPPublishDocument) Then
            Else
                TCPublicNotices.TabPages.Remove(Me.TPOldDocuments)
                TCPublicNotices.TabPages.Add(TPPublishDocument)
                TCPublicNotices.TabPages.Add(Me.TPOldDocuments)
            End If

            FillApplicationList()
            txtPublicNoticeDocument.Clear()
            PreviewReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnGeneratePublicNotice_Click")
        Finally

        End Try

    End Sub
    Private Sub dgvPublicNotice_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPublicNotice.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvPublicNotice.HitTest(e.X, e.Y)

            If dgvPublicNotice.RowCount > 0 And hti.RowIndex <> -1 Then
                txtApplicationNumberEditor.Text = dgvPublicNotice(0, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".dgvPublicNotice_MouseUp")
        Finally

        End Try

    End Sub
    Private Sub btnOpenApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenApplication.Click
        Try

            If txtApplicationNumber.Text <> "" Then
                OpenApplication()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnOpenApplication_Click")
        Finally

        End Try

    End Sub
    Private Sub btnGeneratePNReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGeneratePNReport.Click
        Try

            txtPublicNoticeDocument.Clear()
            PreviewReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnGeneratePNReport_Click")
        Finally

        End Try

    End Sub
    Private Sub txtApplicationList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsbPublicAdvisories.SelectedIndexChanged
        Try
            If lsbPublicAdvisories.SelectedIndex > -1 Then
                txtApplicationNumber.Text = lsbPublicAdvisories.SelectedItem.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".txtApplicationList_SelectedIndexChanged")
        Finally

        End Try

    End Sub
    Private Sub lsbPublicNoticies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsbPublicNoticies.SelectedIndexChanged
        Try
            txtApplicationNumber.Text = lsbPublicNoticies.SelectedItem.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".lsbPublicNoticies_SelectedIndexChanged")
        Finally

        End Try

    End Sub
    Private Sub btnPublishPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPublishPDF.Click
        Try
            If lblFileName.Text = "pdf File Name" Then
                GenerateFileName()
                lblFileName.Visible = True
            End If
            If lblFileName.Text <> "pdf File Name" And lblFileName.Text <> "ERROR" Then
                UpdateLetter()
                UpdateApplications()
                ExportPDF()
                LoadOldPDFs()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnPublishPDF_Click")
        Finally

        End Try

    End Sub
    Private Sub btnOpenPAPN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenPAPN.Click
        Try

            If cboPAPNReports.Text <> "" Then
                If cboPAPNReports.Items.Contains(cboPAPNReports.Text) Then
                    OpenOldPAPN()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnOpenPAPN_Click")
        Finally

        End Try
    End Sub
    Private Sub btnViewOldPDFs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewOldPDFs.Click
        Try
            Dim rpt As New SSPPPublicNotice
            monitor.TrackFeature("Report." & rpt.ResourceName)
            Dim ParameterFields As CrystalDecisions.Shared.ParameterFields
            Dim ParameterField As CrystalDecisions.Shared.ParameterField
            Dim spValue As CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterFields = New CrystalDecisions.Shared.ParameterFields

            'Do this at the beginning of every new entry 
            ParameterField = New CrystalDecisions.Shared.ParameterField
            spValue = New CrystalDecisions.Shared.ParameterDiscreteValue

            ParameterField.ParameterFieldName = "PublicNotice"
            spValue.Value = rtbPAPNDocument2.Rtf
            ParameterField.CurrentValues.Add(spValue)
            ParameterFields.Add(ParameterField)

            'Load Variables into the Fields
            CRVPublicNotices.ParameterFieldInfo = ParameterFields

            'Display the Report
            CRVPublicNotices.ReportSource = rpt

            CRVPublicNotices.ExportReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnViewOldPDFs_Click")
        Finally

        End Try
    End Sub
    Private Sub btnClearPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPreview.Click
        Try

            lsbApplicationList.Items.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnClearPreview_Click")
        Finally

        End Try
    End Sub
    Private Sub btnSavePAPNChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePAPNChanges.Click
        Try
            Dim FileName As String = ""
            Dim DestFilePath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")
            Dim ReviewingManager As String = ""
            Dim ReviewedDate As String = ""
            Dim PublishingStaff As String = ""
            Dim PublishedDate As String = ""
            Dim CommentsDate As String = ""

            If lblPAPNDocumentName.Text <> "PA/PN Doc Name" And lblPAPNDocumentName.Text <> "ERROR" Then
                FileName = lblPAPNDocumentName.Text

                SQL = "Select " & _
                "strFileName, strReviewingManager, " & _
                "datReviewed, strPublishingStaff, " & _
                "datPublishedDate, datCommentsDate " & _
                "from AIRBRANCH.SSPPPublicLetters " & _
                "where strFileName = '" & FileName & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read

                If recExist = True Then
                    FileName = FileName
                    If IsDBNull(dr.Item("strReviewingManager")) Then
                        ReviewingManager = CurrentUser.UserID
                    Else
                        ReviewingManager = dr.Item("strReviewingManager")
                    End If
                    If IsDBNull(dr.Item("datReviewed")) Then
                        ReviewedDate = OracleDate
                    Else
                        ReviewedDate = Format(dr.Item("datReviewed"), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(dr.Item("strPublishingStaff")) Then
                        PublishingStaff = CurrentUser.UserID
                    Else
                        PublishingStaff = dr.Item("strPublishingStaff")
                    End If
                    If IsDBNull(dr.Item("datPublishedDate")) Then
                        PublishedDate = OracleDate
                    Else
                        PublishedDate = Format(dr.Item("datPublishedDate"), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(dr.Item("datCommentsDate")) Then
                        CommentsDate = OracleDate
                    Else
                        CommentsDate = Format(dr.Item("datCommentsDate"), "dd-MMM-yyyy")
                    End If
                Else
                    FileName = FileName
                    ReviewingManager = CurrentUser.UserID
                    ReviewedDate = OracleDate
                    PublishingStaff = CurrentUser.UserID
                    PublishedDate = OracleDate
                    CommentsDate = OracleDate
                End If
                dr.Close()

                If FileName <> "" Then
                    SQL = "Delete AIRBRANCH.SSPPPublicLetters " & _
                    "where strFileName = '" & FileName & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    Dim Encoder As New System.Text.ASCIIEncoding
                    Dim bytedata As Byte() = Encoder.GetBytes(rtbPAPNDocument2.Rtf)

                    Dim fs As New System.IO.FileStream(DestFilePath, IO.FileMode.Create, IO.FileAccess.Write)
                    fs.Write(bytedata, 0, bytedata.Length)
                    fs.Close()

                    Dim da As OracleDataAdapter
                    Dim ds As DataSet

                    SQL = "Select * " & _
                    "from AIRBRANCH.SSPPPublicLetters " & _
                    "where strFileName = '" & FileName & "' "

                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    da = New OracleDataAdapter(SQL, CurrentConnection)
                    Dim cmdCB As OracleCommandBuilder = New OracleCommandBuilder(da)
                    ds = New DataSet("IAIPData")
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                    da.Fill(ds, "IAIPData")
                    Dim row As DataRow = ds.Tables("IAIPData").NewRow()
                    row("strFileName") = FileName
                    row("BatchFile") = bytedata
                    row("strReviewingManager") = ReviewingManager
                    row("datReviewed") = ReviewedDate
                    row("strPublishingStaff") = PublishingStaff
                    row("datPublishedDate") = PublishedDate
                    row("datcommentsDate") = CommentsDate
                    ds.Tables("IAIPData").Rows.Add(row)
                    da.Update(ds, "IAIPData")

                    MsgBox("Data updated", MsgBoxStyle.Information, "Public notices and advisories.")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & ".btnClearPreview_Click")
        Finally

        End Try
    End Sub

#End Region

    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub

End Class