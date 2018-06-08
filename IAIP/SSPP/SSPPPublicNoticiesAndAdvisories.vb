Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports Iaip.DAL

Public Class SSPPPublicNoticiesAndAdvisories

    Private Sub DevPublicNoticiesAndAdvisories_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            LoadPublicNoticesList()
            TCPublicNotices.TabPages.Remove(TPPublishDocument)
            LoadOldPDFs()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub LoadPublicNoticesList()
        Try

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "SSPPApplicationData.strFacilityName, " &
            "strCountyName, " &
            "case " &
            "  when strApplicationType is Null then '' " &
            "  else LookUpApplicationTypes.strApplicationTypeDesc " &
            "End AppType, " &
            "case " &
            "  when strPAReady is Null then '' " &
            "  when strPAReady = 'True' then 'PA Ready' " &
            "  when strPAReady = 'False' then '' " &
            "  Else '' " &
            "End PAReady, " &
            "format(datPAExpires, 'yyyy-MM-dd') as PAExpires, " &
            "case " &
            "when strPAPosted is null then '' " &
            "else strPAPosted " &
            "end strPAPosted, " &
            "case " &
            "  when strPNReady is Null then '' " &
            "  when strPNReady = 'True' then 'PN Ready' " &
            "  when strPNReady = 'False' then '' " &
            "  Else '' " &
            "End PNReady, " &
            "format(datPNExpires, 'yyyy-MM-dd') as PNExpires, " &
            "case " &
            "when strPNPosted is null then '' " &
            "else strPNPosted " &
            "end strPNPosted " &
            "from SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            " LEFT JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN LookUpCountyInformation " &
            "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode " &
            "where datFinalizedDate is Null " &
            "And strPAPosted Is null And strPNPosted Is null " &
            "and (strPAReady = 'True' or strPNReady = 'True') " &
            "and ((strApplicationTypeCode = '2' or strApplicationTypeCode = '21' " &
            "or strApplicationTypeCode = '14' or strApplicationTypeCode = '16' " &
            "or strApplicationTypeCode = '22' ) " &
            "or ((strApplicationTypeCode = '15' " &
            " or strApplicationTypeCode = '9' or strApplicationTypeCode = '20' " &
            "or strApplicationTypeCode = '11' " &
            "or strApplicationTypeCode = '12')and strPublicInvolvement <> '2')) " &
            "order by strApplicationNumber desc "

            dgvPublicNotice.DataSource = DB.GetDataTable(query)

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

            dgvPublicNotice.SanelyResizeColumns

            txtPreviewCount.Text = dgvPublicNotice.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LoadOldPDFs()
        Try

            cboPAPNReports.Items.Clear()

            Dim query As String = "select strFileName " &
            "from SSPPPublicLetters " &
            "order by datPublishedDate "

            Dim dt As DataTable = DB.GetDataTable(query)
            For Each dr As DataRow In dt.Rows
                Me.cboPAPNReports.Items.Add(dr.Item("strFileName"))
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub CreatePublicNoticeList()
        Try
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""

            Dim AppNumbers As New List(Of Integer)
            Dim SIPAppNumbers As New List(Of Integer)
            Dim TVAppNumbers As New List(Of Integer)

            Dim temp As String = ""
            Dim i As Integer

            For i = 0 To dgvPublicNotice.RowCount - 1
                AppNumbers.Add(CInt(dgvPublicNotice(0, i).Value))
            Next

            Dim p As SqlParameter = AppNumbers.AsTvpSqlParameter("@appnumbers")

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData  " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            " LEFT JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN LookUpCountyInformation  " &
            "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
            "where strPAReady = 'True' " &
            "and strPAPosted is Null " &
            "and datPAExpires is Null " &
            "and strPublicInvolvement = '1' " &
            " and SSPPApplicationMaster.strApplicationNumber in " &
            "(select * from @appnumbers) "

            Dim dt As DataTable = DB.GetDataTable(query, p)

            For Each dr As DataRow In dt.Rows
                SIPAppNumbers.Add(CInt(dr.Item("strApplicationNumber")))
            Next

            query = "Select " &
            "SSPPApplicationMaster.strApplicationNumber " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData  " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            " LEFT JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN LookUpCountyInformation  " &
            "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
            "where strPNPosted is Null " &
            "and (strApplicationType = '14' or strApplicationType = '16' " &
            "or strApplicationType = '21' or strApplicationType = '22') " &
            "and (datPNExpires > (GETDATE() + 24) and datPNExpires < (GETDATE() + 37)) " &
            " and SSPPApplicationMaster.strApplicationNumber in " &
            "(select * from @appnumbers) "

            Dim dt2 As DataTable = DB.GetDataTable(query, p)

            For Each dr2 As DataRow In dt2.Rows
                TVAppNumbers.Add(CInt(dr2.Item("strApplicationNumber")))
            Next

            If SIPAppNumbers.Count > 0 Then
                If lsbApplicationList.Items.Contains("Public Advisories") Then

                Else
                    lsbApplicationList.Items.Add("Public Advisories")
                    lsbApplicationList.Items.Add(" ")
                End If


                query = "Select " &
                "SSPPApplicationMaster.strApplicationNumber, " &
                "SSPPApplicationData.strFacilityName, " &
                "strCountyName, " &
                "case " &
                "   when strApplicationType is Null then '' " &
                "   else LookUpApplicationTypes.strApplicationTypeDesc " &
                "End AppType " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationData  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationTracking " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN LookUpCountyInformation  " &
                "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
                " where SSPPApplicationMaster.strApplicationNumber in " &
                "(select * from @sipappnumbers)  " &
                "order by strCountyName "

                Dim p3 As SqlParameter = SIPAppNumbers.AsTvpSqlParameter("@sipappnumbers")

                Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                For Each dr3 As DataRow In dt3.Rows
                    temp = ""
                    If IsDBNull(dr3.Item("strApplicationNumber")) Then
                        AppNum = ""
                    Else
                        AppNum = dr3.Item("strApplicationNumber")
                    End If
                    If IsDBNull(dr3.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr3.Item("strFacilityName")
                    End If
                    If IsDBNull(dr3.Item("strCountyName")) Then
                        County = ""
                    Else
                        County = dr3.Item("strCountyName")
                    End If
                    If IsDBNull(dr3.Item("AppType")) Then
                        AppType = ""
                    Else
                        AppType = dr3.Item("AppType")
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
                            If lsbApplicationList.Items.Contains("Public Notices") Then
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Notices") - 1, temp)
                            Else
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf(" "), temp)
                            End If
                        Else
                            If lsbApplicationList.Items.Contains("Public Notices") Then
                                lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Notices") - 1, temp)
                            Else
                                lsbApplicationList.Items.Insert(0, "Public Advisories")
                                lsbApplicationList.Items.Insert(1, temp)
                                lsbApplicationList.Items.Insert(2, " ")
                            End If
                        End If
                    End If
                Next
            End If

            If TVAppNumbers.Count > 0 Then
                If lsbApplicationList.Items.Contains("Public Notices") Then

                Else
                    lsbApplicationList.Items.Add("Public Notices")
                End If

                query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "SSPPApplicationData.strFacilityName, " &
                    "strCountyName, " &
                    "case " &
                    "   when strApplicationType is Null then '' " &
                    "   else LookUpApplicationTypes.strApplicationTypeDesc " &
                    "End AppType " &
                "FROM SSPPApplicationMaster " &
                " INNER JOIN SSPPApplicationData  " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                " INNER JOIN SSPPApplicationTracking " &
                "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
                " LEFT JOIN LookUpApplicationTypes " &
                "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                " INNER JOIN LookUpCountyInformation  " &
                "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
                " where SSPPApplicationMaster.strApplicationNumber in " &
                "(select * from @tvappnumbers)  " &
                    "order by strCountyName "

                Dim p4 As SqlParameter = TVAppNumbers.AsTvpSqlParameter("@tvappnumbers")

                Dim dt4 As DataTable = DB.GetDataTable(query, p4)

                For Each dr4 As DataRow In dt4.Rows
                    temp = ""
                    If IsDBNull(dr4.Item("strApplicationNumber")) Then
                        AppNum = ""
                    Else
                        AppNum = dr4.Item("strApplicationNumber")
                    End If
                    If IsDBNull(dr4.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr4.Item("strFacilityName")
                    End If
                    If IsDBNull(dr4.Item("strCountyName")) Then
                        County = ""
                    Else
                        County = dr4.Item("strCountyName")
                    End If
                    If IsDBNull(dr4.Item("AppType")) Then
                        AppType = ""
                    Else
                        AppType = dr4.Item("AppType")
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
                        If lsbApplicationList.Items.Contains("Public Notices") Then
                            lsbApplicationList.Items.Add(temp)
                        Else
                            lsbApplicationList.Items.Add("Public Notices")
                            lsbApplicationList.Items.Add(temp)
                        End If
                    End If
                Next
            End If

            If SIPAppNumbers.Count = 0 And TVAppNumbers.Count = 0 Then
                lsbApplicationList.Items.Add("No Public Advisories and No Public Notices")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub AddToApplicationList()
        Try
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""
            Dim temp As String = ""

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "SSPPApplicationData.strFacilityName, " &
            "strCountyName, " &
            "case " &
            "   when strApplicationType is Null then '' " &
            "   else LookUpApplicationTypes.strApplicationTypeDesc " &
            "End AppType, " &
            "case " &
            "   when strPAReady is Null then '' " &
             "   when strPAReady = 'True' then 'PA Ready' " &
            "   when strPAReady = 'False' then '' " &
            "   Else ''  " &
            "End PAReady,  " &
            "case " &
            "   when strPNReady is Null then '' " &
            "   when strPNReady = 'True' then 'PN Ready' " &
            "   when strPNReady = 'False' then 'PN Ready' " &
            "   Else ''  " &
            "End PNReady,  " &
            "datPNExpires  " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData  " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            " LEFT JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN LookUpCountyInformation  " &
            "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
            "where SSPPApplicationMaster.strApplicationNumber = @appnum "

            Dim p As New SqlParameter("@appnum", txtApplicationNumberEditor.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
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
                MsgBox("The list already contains this application.", MsgBoxStyle.Information, "SSPP Public Notices And Advisories")
            Else
                If rdbPublicAdvisories.Checked = True Then
                    If lsbApplicationList.Items.Contains("Public Advisories") Then
                        If lsbApplicationList.Items.Contains("Public Notices") Then
                            lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf("Public Notices") - 1, temp)
                        Else
                            lsbApplicationList.Items.Insert(lsbApplicationList.Items.IndexOf(" ") - 1, temp)
                        End If
                    Else
                        lsbApplicationList.Items.Insert(0, "Public Advisories")
                        lsbApplicationList.Items.Insert(1, temp)
                        lsbApplicationList.Items.Insert(2, " ")
                    End If
                Else
                    If lsbApplicationList.Items.Contains("Public Notices") Then
                        lsbApplicationList.Items.Add(temp)
                    Else
                        lsbApplicationList.Items.Add("Public Notices")
                        lsbApplicationList.Items.Add(temp)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub RemoveFromApplicationList()
        Try
            Dim AppNum As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim AppType As String = ""
            Dim temp As String = ""

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "SSPPApplicationData.strFacilityName, " &
            "strCountyName, " &
            "case " &
            "   when strApplicationType is Null then '' " &
            "   else LookUpApplicationTypes.strApplicationTypeDesc " &
            "End AppType, " &
            "case " &
            "   when strPAReady is Null then '' " &
             "   when strPAReady = 'True' then 'PA Ready' " &
            "   when strPAReady = 'False' then '' " &
            "   Else ''  " &
            "End PAReady,  " &
            "case " &
            "   when strPNReady is Null then '' " &
            "   when strPNReady = 'True' then 'PN Ready' " &
            "   when strPNReady = 'False' then 'PN Ready' " &
            "   Else ''  " &
            "End PNReady,  " &
            "datPNExpires  " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData  " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber  " &
            " LEFT JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN LookUpCountyInformation  " &
            "ON SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
            "where SSPPApplicationMaster.strApplicationNumber = @appnum "

            Dim p As New SqlParameter("@appnum", txtApplicationNumberEditor.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
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
                MsgBox("The list does not contains this application.", MsgBoxStyle.Information, "SSPP Public Notices And Advisories")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub FillApplicationList()
        Try
            Dim temp As String
            Dim PublicNo As Integer = -1

            lsbPublicAdvisories.Items.Clear()
            lsbPublicNoticies.Items.Clear()

            If lsbApplicationList.Items.Contains("Public Notices") Then
                PublicNo = lsbApplicationList.Items.IndexOf("Public Notices")
            End If

            If lsbApplicationList.Items.Contains("Public Advisories") Then
                If PublicNo > -1 Then
                    For i As Integer = 1 To PublicNo - 2
                        temp = lsbApplicationList.Items.Item(i).ToString
                        lsbPublicAdvisories.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                    Next
                Else
                    For i As Integer = 1 To lsbApplicationList.Items.Count - 1
                        temp = lsbApplicationList.Items.Item(i).ToString
                        If temp <> " " Then
                            lsbPublicAdvisories.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                        End If
                    Next
                End If
            End If

            If PublicNo > -1 Then
                For i As Integer = PublicNo + 1 To lsbApplicationList.Items.Count - 1
                    temp = lsbApplicationList.Items.Item(i).ToString
                    If temp <> " " Then
                        lsbPublicNoticies.Items.Add(Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text)) - 1))
                    End If
                Next
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub OpenApplication()
        OpenFormPermitApplication(txtApplicationNumber.Text)
    End Sub
    Private Sub PreviewReport()
        Dim i As Integer
        Dim query As String

        Dim PANeeded As String = ""
        Dim PublicAdvisories As String = ""
        Dim TVAdvisories As String = ""
        Dim TVInitial As String = ""
        Dim TVRenewal As String = ""
        Dim TVSigMod As String = ""
        Dim Deadline As String = ""

        Dim AdvAppNumbers As New List(Of Integer)
        Dim NotAppNumbers As New List(Of Integer)

        Try
            If lsbPublicAdvisories.Items.Count > 0 Then
                For i = 0 To lsbPublicAdvisories.Items.Count - 1
                    AdvAppNumbers.Add(CInt(lsbPublicAdvisories.Items.Item(i)))
                Next

                query = "select " &
                "SSPPApplicationData.strApplicationNumber, " &
                "strPAReady, strFacilityName, " &
                "strFacilityStreet1, strFacilityCity, " &
                "strFacilityState, strFacilityZipCode, " &
                "strPlantDescription,  " &
                "strApplicationNotes, strCountyName    " &
                "from SSPPApplicationData, SSPPApplicationTracking, " &
                "SSPPApplicationMaster, LookUpCountyInformation " &
                "where SSPPApplicationData.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicatioNNumber " &
                "and SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode " &
                " and SSPPApplicationMaster.strApplicationNumber in " &
                " (select * from @appnumbers)  " &
                "order by strCountyName "

                Dim p As SqlParameter = AdvAppNumbers.AsTvpSqlParameter("@appnumbers")

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
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
                Next
            End If

            If lsbPublicNoticies.Items.Count > 0 Then
                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    NotAppNumbers.Add(CInt(lsbPublicNoticies.Items.Item(i)))
                Next

                query = "Select  " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strCountyName, strFacilityName,  " &
                "strFacilityStreet1, strFacilityCity,  " &
                "strFacilityState, strFacilityZipCode,  " &
                "strPlantDescription, strApplicationNotes,  " &
                "format(datPNExpires, 'MMMM dd, yyyy') as datPNExpires " &
                "from SSPPApplicationData, SSPPApplicationMaster, " &
                "LookUpCountyInformation, SSPPApplicationTracking " &
                "where SSPPApplicationMaster.strApplicationnumber = SSPPApplicationData.strApplicationNumber   " &
                "and SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                "and strApplicationType = '14'  " &
                " and SSPPApplicationMaster.strApplicationNumber in " &
                " (select * from @appnumbers)  " &
                "order by strCountyName "

                Dim p As SqlParameter = NotAppNumbers.AsTvpSqlParameter("@appnumbers")

                Dim dt As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt.Rows
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
                Next

                query = "Select  " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strCountyName, strFacilityName,  " &
                "strFacilityStreet1, strFacilityCity,  " &
                "strFacilityState, strFacilityZipCode,  " &
                "strPlantDescription, strApplicationNotes,  " &
                "format(datPNExpires, 'MMMM dd, yyyy') as datPNExpires " &
                "from SSPPApplicationData, SSPPApplicationMaster,   " &
                "LookUpCountyInformation, SSPPApplicationTracking " &
                "where SSPPApplicationMaster.strApplicationnumber = SSPPApplicationData.strApplicationNumber   " &
                "and SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                " and SSPPApplicationMaster.strApplicationNumber in " &
                " (select * from @appnumbers)  " &
                "and strApplicationType = '16'  " &
                "order by strCountyName "

                Dim dt2 As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt2.Rows
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
                Next

                query = "Select  " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strCountyName, strFacilityName,  " &
                "strFacilityStreet1, strFacilityCity,  " &
                "strFacilityState, strFacilityZipCode,  " &
                "strPlantDescription, strApplicationNotes,  " &
                "format(datPNExpires, 'MMMM dd, yyyy') as datPNExpires, " &
                "strSignificantComments " &
                "from SSPPApplicationData, SSPPApplicationMaster,   " &
                "LookUpCountyInformation, SSPPApplicationTracking " &
                "where SSPPApplicationMaster.strApplicationnumber = SSPPApplicationData.strApplicationNumber   " &
                "and SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5, 3) = LookUpCountyInformation.strCountyCode  " &
                "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                " and SSPPApplicationMaster.strApplicationNumber in " &
                " (select * from @appnumbers)  " &
                "and (strApplicationType = '21' or strApplicationType = '22')  " &
                "order by strCountyName "


                Dim dt3 As DataTable = DB.GetDataTable(query, p)

                For Each dr As DataRow In dt3.Rows
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
                Next
            End If

            If DTPPADeadline.Checked = False Then
                Deadline = "PUBLICATION DEADLINE"
            Else
                Deadline = Format(CDate(DTPPADeadline.Text), "dd-MMMM-yyyy")
            End If

            PublicAdvisories = "EPD PUBLIC ADVISORY" &
            vbCrLf & "GEORGIA AIR PROTECTION BRANCH" &
            vbCrLf & vbCrLf & vbCrLf & "SIP PUBLIC ADVISORIES" &
            vbCrLf & vbCrLf & "The following applications have been received for Air Quality Permits. " & vbCrLf &
            "These applications are presently under review. Any comments should be received by " & Deadline & vbCrLf & vbCrLf & vbCrLf

            If PANeeded <> "" Then
                PublicAdvisories = PublicAdvisories & PANeeded & vbCrLf
            Else
                PublicAdvisories = PublicAdvisories & "NO PUBLIC ADVISORIESX" & vbCrLf & vbCrLf
            End If

            PublicAdvisories = PublicAdvisories & "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf &
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Suite 120, Atlanta, Georgia 30354, " & vbCrLf & "(404) 363-7000" & vbCrLf & vbCrLf & vbCrLf

            TVAdvisories = "NOTICE OF DRAFT TITLE V OPERATING PERMITS AND PERMIT MODIFICATIONS " & vbCrLf &
            "GEORGIA ENVIRONMENTAL PROTECTION DIVISION " & vbCrLf &
            "AIR PROTECTION BRANCHX" & vbCrLf &
            "4244 INTERNATIONAL PARKWAY, SUITE 120, ATLANTA, GA 30354 " & vbCrLf & vbCrLf &
            "The Georgia Environmental Protection Division announces its intent to " & vbCrLf &
            "issue initial Title V Operating Permits, Title V Significant " & vbCrLf &
            "modifications, Title V Operating Permit Renewals, and/or other Title V " & vbCrLf &
            "Permit proceedings for the following facilities. The deadlines for " & vbCrLf &
            "submitting comments and requesting a public hearing are specified for " & vbCrLf & "each facility. " & vbCrLf & vbCrLf

            If TVInitial <> "" Or TVRenewal <> "" Or TVSigMod <> "" Then
                If TVInitial <> "" Then
                    TVAdvisories = TVAdvisories & "INITIAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf &
                    TVInitial & vbCrLf
                End If
                If TVRenewal <> "" Then
                    TVAdvisories = TVAdvisories & "RENEWAL TITLE V OPERATING PERMITSX" & vbCrLf & vbCrLf &
                    TVRenewal & vbCrLf
                End If
                If TVSigMod <> "" Then
                    TVAdvisories = TVAdvisories & "TITLE V SIGNIFICANT MODIFICATIONSX" & vbCrLf & vbCrLf &
                    TVSigMod & vbCrLf
                End If
            Else
                TVAdvisories = TVAdvisories & "NO TITLE V ADVISORIESX" & vbCrLf & vbCrLf
            End If

            TVAdvisories = TVAdvisories & "ADDITIONAL INFORMATIONX: The draft permits and permit amendments and " & vbCrLf &
            "all information used to develop the draft permits and permit amendments " & vbCrLf &
            "are available for review. This includes the application, all relevant " & vbCrLf &
            "supporting materials and all other materials available to the permitting " & vbCrLf &
            "authority used in the permit review process. This information is " & vbCrLf &
            "available for review at the office of the Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Atlanta Tradeport - Suite 120, Atlanta, Georgia 30354. " & vbCrLf &
            "Copies of the draft permits or permit amendments, narratives, " & vbCrLf &
            "application summaries, and (in most cases) permit applications are also " & vbCrLf &
            "available at our Internet site, epd.georgia.gov/air . Also " & vbCrLf &
            "available at this Internet site is a copy of the public notice, as it " & vbCrLf &
            "will appear in the legal organ of the county where the facility is " & vbCrLf &
            "located. " & vbCrLf & vbCrLf &
            "If a permit application is not available at our Internet site, the " & vbCrLf &
            "public notice will indicate where a copy of these documents will be " & vbCrLf &
            "available at a location near the facility. " & vbCrLf & vbCrLf &
            "Persons wishing to comment on a draft Initial Title V Operating Permit, " & vbCrLf &
            "Title V Significant modification, Title V Operating Permit Renewal, or " & vbCrLf &
            "other Title V Permit proceedings are required to submit their comments, " & vbCrLf &
            "in writing, to EPD at the above Atlanta Air Protection Branch address. " & vbCrLf &
            "Comments must be received by no later than the deadline indicated for " & vbCrLf &
            "the particular facility. (Should the comment period end on a weekend or " & vbCrLf &
            "holiday, comments will be accepted up until the next working day.) All " & vbCrLf &
            "comments received on or prior to the deadline will be considered by the " & vbCrLf &
            "Division in making its final decision to issue the Title V permit or " & vbCrLf &
            "permit amendment." & vbCrLf & vbCrLf &
            "Any requests for a public hearing must be made prior to the deadline " & vbCrLf &
            "indicated for the particular facility. A request for a hearing should " & vbCrLf &
            "be in writing and should specify, in as much detail as possible, the " & vbCrLf &
            "portion of the Georgia Rules for Air Quality Control or the Federal " & vbCrLf &
            "Rules which the individual making the request is concerned may not have " & vbCrLf &
            "been adequately incorporated. A public hearing may be held if the " & vbCrLf &
            "Director of the EPD finds that such a hearing would assist the EPD in a " & vbCrLf &
            "proper review of the facility's ability to comply with the Federal and " & vbCrLf &
            "State air quality regulations. " & vbCrLf & vbCrLf &
            "For additional information, contact Eric Cornwell, Program Manager, " & vbCrLf &
            "Stationary Source Permitting Program, Air Protection Branch, " & vbCrLf &
            "4244 International Parkway, Suite 120, " & vbCrLf &
            "Atlanta, Georgia 30354, (404) 363-7000" & vbCrLf & vbCrLf & vbCrLf &
            "--------------------------------------------------" & vbCrLf

            txtPublicNoticeDocument.Text = PublicAdvisories & TVAdvisories

            FormatReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub FormatReport()
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub GenerateFileName()
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
                Dim query As String = "select strFileName " &
                "From SSPPPublicLetters " &
                "where strFileName = @FileName "

                Dim p As New SqlParameter("@FileName", FileName)

                If DB.ValueExists(query, p) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub UpdateLetter()
        Try
            Dim FileName As String = ""
            Dim DestFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")
            Dim ReviewingManager As String = ""
            Dim ReviewedDate As String = ""
            Dim PublishingStaff As String = ""
            Dim PublishedDate As String = ""
            Dim CommentsDate As String = ""

            If lblFileName.Text <> "pdf File Name" And lblFileName.Text <> "ERROR" Then
                FileName = lblFileName.Text

                Dim query As String = "Select " &
                "strFileName, strReviewingManager, " &
                "datReviewed, strPublishingStaff, " &
                "datPublishedDate, datCommentsDate " &
                "from SSPPPublicLetters " &
                "where strFileName = @FileName "

                Dim p As New SqlParameter("@FileName", FileName)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
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
                    ReviewedDate = TodayFormatted
                    PublishingStaff = CurrentUser.UserID
                    PublishedDate = TodayFormatted
                    CommentsDate = Format(CDate(DTPPADeadline.Text), "dd-MMM-yyyy").ToString
                End If

                If DTPPADeadline.Checked = True Then
                    CommentsDate = Format(CDate(DTPPADeadline.Text), "dd-MMM-yyyy").ToString
                Else
                    CommentsDate = TodayFormatted
                End If

                If FileName <> "" Then
                    query = "Delete SSPPPublicLetters " &
                    "where strFileName = @FileName "

                    DB.RunCommand(query, p)

                    Dim Encoder As New Text.ASCIIEncoding
                    Dim bytedata As Byte() = Encoder.GetBytes(txtPublicNoticeDocument.Rtf)

                    Dim fs As New FileStream(DestFilePath, FileMode.Create, FileAccess.Write)
                    fs.Write(bytedata, 0, bytedata.Length)
                    fs.Close()

                    query = "INSERT INTO SSPPPUBLICLETTERS " &
                        "(STRFILENAME, BATCHFILE, STRREVIEWINGMANAGER, DATREVIEWED, " &
                        " STRPUBLISHINGSTAFF, DATPUBLISHEDDATE, DATCOMMENTSDATE) " &
                        "VALUES " &
                        "(@STRFILENAME, @BATCHFILE, @STRREVIEWINGMANAGER, @DATREVIEWED, " &
                        " @STRPUBLISHINGSTAFF, @DATPUBLISHEDDATE, @DATCOMMENTSDATE) "

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@strFileName", FileName),
                        New SqlParameter("@BatchFile", bytedata),
                        New SqlParameter("@strReviewingManager", ReviewingManager),
                        New SqlParameter("@datReviewed", ReviewedDate),
                        New SqlParameter("@strPublishingStaff", PublishingStaff),
                        New SqlParameter("@datPublishedDate", PublishedDate),
                        New SqlParameter("@datcommentsDate", CommentsDate)
                    }

                    DB.RunCommand(query, p2)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub UpdateApplications()
        Try
            Dim i As Integer
            Dim query As String

            If lsbPublicAdvisories.Items.Count > 0 Then
                query = "Update SSPPApplicationData set " &
                "strPublicInvolvement = '1', " &
                "strPAPosted = @filename " &
                "where strApplicationNumber in " &
                "(select * from @appnums)  "

                Dim AppNums As New List(Of Integer)

                For i = 0 To lsbPublicAdvisories.Items.Count - 1
                    AppNums.Add(CInt(lsbPublicAdvisories.Items.Item(i)))
                Next

                Dim p As SqlParameter() = {
                    New SqlParameter("@filename", lblFileName.Text),
                    AppNums.AsTvpSqlParameter("@appnums")
                }

                DB.RunCommand(query, p)

                query = "Update SSPPApplicationTracking set " &
                "datPAExpires = @dat " &
                "where strApplicationNumber in " &
                "(select * from @appnums)  "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@dat", DTPPADeadline.Value),
                    AppNums.AsTvpSqlParameter("@appnums")
                }

                DB.RunCommand(query, p2)
            End If

            If lsbPublicNoticies.Items.Count > 0 Then
                query = "Update SSPPApplicationData set " &
                "strPublicInvolvement = '1', " &
                "strPNPosted = @filename " &
                "where strApplicationNumber in " &
                "(select * from @appnums)  "

                Dim AppNums As New List(Of Integer)

                For i = 0 To lsbPublicNoticies.Items.Count - 1
                    AppNums.Add(CInt(lsbPublicNoticies.Items.Item(i)))
                Next

                Dim p As SqlParameter() = {
                    New SqlParameter("@filename", lblFileName.Text),
                    AppNums.AsTvpSqlParameter("@appnums")
                }

                DB.RunCommand(query, p)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub ExportPDF()
        Cursor = Cursors.WaitCursor

        Try
            Dim rpt As New SSPPPublicNotice
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

        Catch ex As TypeInitializationException
            ShowCrystalReportsSupportMessage()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Cursor = Nothing
        End Try
    End Sub

    Private Sub OpenOldPAPN()
        Try
            Dim DestFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")

            Dim query As String = "Select " &
            "strFileName, BatchFile, " &
            "strReviewingManager, " &
            "datReviewed, strPublishingStaff, " &
            "datPublishedDate, datCommentsDate " &
            "from SSPPPublicLetters " &
            "where strFileName = @filename "

            Dim p As New SqlParameter("@filename", cboPAPNReports.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)
            If dr IsNot Nothing Then
                lblPAPNDocumentName.Text = cboPAPNReports.Text
                lblPAPNDocumentName.Visible = True

                If IsDBNull(dr.Item("datCommentsDate")) Then
                    lblPAPNExpiresDate2.Text = TodayFormatted
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

                Dim filepath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "temp.rtf")
                If File.Exists(filepath) Then
                    Dim reader As StreamReader = New StreamReader(filepath)
                    Do
                        rtbPAPNDocument2.Rtf = reader.ReadToEnd
                    Loop Until reader.Peek = -1
                    reader.Close()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub DevPublicNoticiesAndAdvisories_SizeChanged(sender As Object, e As EventArgs) 'Handles Me.SizeChanged
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Try

            CreatePublicNoticeList()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEditApplicationList_Click(sender As Object, e As EventArgs) Handles btnAddToApplicationList.Click
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRemoveFromApplicationList_Click(sender As Object, e As EventArgs) Handles btnRemoveFromApplicationList.Click
        Try

            If txtApplicationNumberEditor.Text <> "" Then
                RemoveFromApplicationList()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGeneratePublicNotice_Click(sender As Object, e As EventArgs) Handles btnGeneratePublicNotice.Click
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvPublicNotice_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvPublicNotice.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvPublicNotice.HitTest(e.X, e.Y)

            If dgvPublicNotice.RowCount > 0 And hti.RowIndex <> -1 Then
                txtApplicationNumberEditor.Text = dgvPublicNotice(0, hti.RowIndex).Value
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnOpenApplication_Click(sender As Object, e As EventArgs) Handles btnOpenApplication.Click
        Try

            If txtApplicationNumber.Text <> "" Then
                OpenApplication()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGeneratePNReport_Click(sender As Object, e As EventArgs) Handles btnGeneratePNReport.Click
        Try

            txtPublicNoticeDocument.Clear()
            PreviewReport()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtApplicationList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsbPublicAdvisories.SelectedIndexChanged
        Try
            If lsbPublicAdvisories.SelectedIndex > -1 Then
                txtApplicationNumber.Text = lsbPublicAdvisories.SelectedItem.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lsbPublicNoticies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsbPublicNoticies.SelectedIndexChanged
        Try
            txtApplicationNumber.Text = lsbPublicNoticies.SelectedItem.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPublishPDF_Click(sender As Object, e As EventArgs) Handles btnPublishPDF.Click
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnOpenPAPN_Click(sender As Object, e As EventArgs) Handles btnOpenPAPN.Click
        Try

            If cboPAPNReports.Text <> "" Then
                If cboPAPNReports.Items.Contains(cboPAPNReports.Text) Then
                    OpenOldPAPN()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnViewOldPDFs_Click(sender As Object, e As EventArgs) Handles btnViewOldPDFs.Click
        Cursor = Cursors.WaitCursor

        Try
            Dim rpt As New SSPPPublicNotice
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

        Catch ex As TypeInitializationException
            ShowCrystalReportsSupportMessage()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Cursor = Nothing
        End Try
    End Sub

    Private Sub btnClearPreview_Click(sender As Object, e As EventArgs) Handles btnClearPreview.Click
        Try

            lsbApplicationList.Items.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub btnSavePAPNChanges_Click(sender As Object, e As EventArgs) Handles btnSavePAPNChanges.Click
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

                Dim query As String = "Select " &
                "strFileName, strReviewingManager, " &
                "datReviewed, strPublishingStaff, " &
                "datPublishedDate, datCommentsDate " &
                "from SSPPPublicLetters " &
                "where strFileName = @filename "

                Dim p As New SqlParameter("@filename", FileName)

                Dim dr As DataRow = DB.GetDataRow(query, p)

                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strReviewingManager")) Then
                        ReviewingManager = CurrentUser.UserID
                    Else
                        ReviewingManager = dr.Item("strReviewingManager")
                    End If
                    If IsDBNull(dr.Item("datReviewed")) Then
                        ReviewedDate = TodayFormatted
                    Else
                        ReviewedDate = Format(dr.Item("datReviewed"), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(dr.Item("strPublishingStaff")) Then
                        PublishingStaff = CurrentUser.UserID
                    Else
                        PublishingStaff = dr.Item("strPublishingStaff")
                    End If
                    If IsDBNull(dr.Item("datPublishedDate")) Then
                        PublishedDate = TodayFormatted
                    Else
                        PublishedDate = Format(dr.Item("datPublishedDate"), "dd-MMM-yyyy")
                    End If
                    If IsDBNull(dr.Item("datCommentsDate")) Then
                        CommentsDate = TodayFormatted
                    Else
                        CommentsDate = Format(dr.Item("datCommentsDate"), "dd-MMM-yyyy")
                    End If
                Else
                    ReviewingManager = CurrentUser.UserID
                    ReviewedDate = TodayFormatted
                    PublishingStaff = CurrentUser.UserID
                    PublishedDate = TodayFormatted
                    CommentsDate = TodayFormatted
                End If

                If FileName <> "" Then
                    query = "Delete SSPPPublicLetters " &
                    "where strFileName = @FileName "

                    DB.RunCommand(query, p)

                    Dim Encoder As New Text.ASCIIEncoding
                    Dim bytedata As Byte() = Encoder.GetBytes(rtbPAPNDocument2.Rtf)

                    Dim fs As New FileStream(DestFilePath, FileMode.Create, FileAccess.Write)
                    fs.Write(bytedata, 0, bytedata.Length)
                    fs.Close()

                    query = "INSERT INTO SSPPPUBLICLETTERS " &
                        "(STRFILENAME, BATCHFILE, STRREVIEWINGMANAGER, DATREVIEWED, " &
                        " STRPUBLISHINGSTAFF, DATPUBLISHEDDATE, DATCOMMENTSDATE) " &
                        "VALUES " &
                        "(@STRFILENAME, @BATCHFILE, @STRREVIEWINGMANAGER, @DATREVIEWED, " &
                        " @STRPUBLISHINGSTAFF, @DATPUBLISHEDDATE, @DATCOMMENTSDATE) "

                    Dim p2 As SqlParameter() = {
                        New SqlParameter("@strFileName", FileName),
                        New SqlParameter("@BatchFile", bytedata),
                        New SqlParameter("@strReviewingManager", ReviewingManager),
                        New SqlParameter("@datReviewed", ReviewedDate),
                        New SqlParameter("@strPublishingStaff", PublishingStaff),
                        New SqlParameter("@datPublishedDate", PublishedDate),
                        New SqlParameter("@datcommentsDate", CommentsDate)
                    }

                    DB.RunCommand(query, p2)

                    MsgBox("Data updated", MsgBoxStyle.Information, "Public notices and advisories.")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

End Class