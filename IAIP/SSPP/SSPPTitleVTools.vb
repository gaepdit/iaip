Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.Apb

Public Class SSPPTitleVTools

    Private Sub DMUTitleVTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPermissions()
    End Sub

    Private Sub LoadWebPublisherDataGrid()
        Try
            Dim query As String = "SELECT " &
            "CONVERT(int, SSPPApplicationMaster.strApplicationNumber) AS ApplicationNumber,  " &
            "strFacilityName,  " &
            "CASE   " &
            "   WHEN strPermitNumber IS NULL THEN ' '   " &
            "   ELSE concat(SUBSTRING(strPermitNumber, 1, 4), '-' ,SUBSTRING(strPermitNumber, 5, 3), '-'   " &
            "    ,SUBSTRING(strPermitNumber, 8, 4), '-' ,SUBSTRING(strPermitNumber, 12, 1), '-'         " &
            "    ,SUBSTRING(strPermitNumber, 13, 2), '-' ,SUBSTRING(strPermitNumber, 15, 1) ) " &
            "END AS strPermitNumber,  " &
            "strApplicationTypeDesc, " &
            "CASE  " &
            "   WHEN datDraftIssued IS NULL THEN ' '  " &
            "   ELSE format(datDraftIssued, 'yyyy-MM-dd') " &
            "END datDraftIssued,  " &
            "CASE  " &
            "   WHEN datPermitIssued IS NULL THEN ' '  " &
            "   ELSE format(datPermitIssued, 'yyyy-MM-dd') " &
            "END datPermitIssued,  " &
            "CASE  " &
            "   WHEN datExperationDate IS NULL THEN ' '  " &
            "   ELSE format(datExperationDate, 'yyyy-MM-dd') " &
            "END datExperationDate  " &
            "from SSPPApplicationTracking, SSPPApplicationData,  " &
            "SSPPApplicationMaster, LookUpApplicationTypes " &
            "WHERE SSPPApplicationTracking.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
            "AND LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " &
            "AND SSPPApplicationTracking.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber " &
            "AND ( " &
            "(strApplicationType = '14' OR strApplicationType = '16' OR strApplicationType = '17') " &
            "AND (datDraftOnWeb IS NULL OR datPNExpires IS NULL " &
            "OR datEPAStatesNotified IS NULL OR datFinalOnWeb IS NULL " &
            "OR datEPANotified IS NULL OR datEffective IS NULL " &
            "OR datExperationDate IS NULL) " &
            "OR " &
            "(strApplicationType = '19' OR strApplicationType = '20') " &
            "AND (datEPAStatesNotifiedAppRec IS NULL  " &
            "OR datFinalOnWeb IS NULL OR datEPANotified IS NULL " &
            "OR datEffective IS NULL OR datExperationDate IS NULL) " &
            "OR " &
            "(strApplicationType = '21' OR strApplicationType = '22') " &
            "AND (datEPAStatesNotifiedAppRec IS NULL OR datDraftOnWeb IS NULL " &
            "OR datPNExpires IS NULL OR datEPAStatesNotified IS NULL " &
            "OR datFinalOnWeb IS NULL OR datEPANotified IS NULL " &
            "OR datEffective IS NULL OR datExperationDate IS NULL) " &
            "OR " &
            "(strApplicationType = '15' OR strApplicationType = '26') " &
            "AND (datFinalOnWeb IS NULL OR datEPANotified IS NULL " &
            "OR datEffective IS NULL OR datExperationDate IS NULL) " &
            "or " &
            "strApplicationType = '2' " &
            "OR " &
            "strApplicationType = '11' " &
            "and SUBSTRING(strTrackedRules, 1, 1) = '1' " &
            ") " &
            "and datFinalizedDate is Null " &
            " order by ApplicationNumber Desc "

            Dim dt As DataTable = DB.GetDataTable(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dgrWebPublisher.DataSource = dt

                dgrWebPublisher.Columns("ApplicationNumber").HeaderText = "APL #"
                dgrWebPublisher.Columns("strFacilityName").HeaderText = "Facility Name"
                dgrWebPublisher.Columns("strPermitNumber").HeaderText = "Permit Number"
                dgrWebPublisher.Columns("strApplicationTypeDesc").HeaderText = "App Type"
                dgrWebPublisher.Columns("datDraftIssued").HeaderText = "Draft Issued"
                dgrWebPublisher.Columns("datPermitIssued").HeaderText = "Final Action"
                dgrWebPublisher.Columns("datExperationDate").HeaderText = "Expiration Date"
                dgrWebPublisher.SanelyResizeColumns()

                txtTVCount.Text = dt.Rows.Count.ToString()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub CheckForLinks()
        Try
            lbLinkApplications.Items.Clear()

            If txtWebPublisherApplicationNumber.Text <> "" Then
                Dim query As String = "Select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = @app "

                Dim p As New SqlParameter("@app", txtWebPublisherApplicationNumber.Text)

                Dim MasterApplication As String = DB.GetString(query, p)

                If MasterApplication = "" Then
                    lbLinkApplications.Items.Clear()
                    lblLinkWarning.Visible = False
                Else
                    query = "Select " &
                    "strApplicationNumber " &
                    "from SSPPApplicationLinking " &
                    "where strMasterApplication = @masterapp " &
                    "order by strApplicationNumber "

                    Dim p2 As New SqlParameter("@masterapp", MasterApplication)

                    Dim dt As DataTable = DB.GetDataTable(query, p2)

                    For Each dr As DataRow In dt.Rows
                        lbLinkApplications.Items.Add(dr.Item("strApplicationNumber"))
                    Next

                    lblLinkWarning.Visible = True
                End If
            Else
                lbLinkApplications.Items.Clear()
                lblLinkWarning.Visible = False
            End If

            If Not lbLinkApplications.Items.Contains(txtWebPublisherApplicationNumber.Text) Then
                lbLinkApplications.Items.Add(txtWebPublisherApplicationNumber.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub LoadPermissions()
        LoadWebPublisherDataGrid()

        DTPNotifiedAppReceived.Value = Today
        DTPDraftOnWeb.Value = Today
        DTPEffectiveDateofPermit.Value = Today
        DTPEPANotifiedPermitOnWeb.Value = Today
        DTPEPAStatesNotified.Value = Today
        DTPFinalOnWeb.Value = Today
        DTPPNExpires.Value = Today
        DTPExperationDate.Value = Today
    End Sub
    Private Sub LoadWebPublisherApplicationData()
        Try

            Dim query As String = "Select " &
            "datDraftOnWeb, datEPAStatesNotified, " &
            "datFinalONWeb, DatEPANotified, " &
            "datEffective, strTargeted, " &
            "datEPAStatesNotifiedAppRec, " &
            "datExperationDate, datPNExpires, " &
            "strApplicationType " &
            "from SSPPApplicationTracking, SSPPApplicationData, " &
            "SSPPApplicationMaster " &
            "where SSPPApplicationTracking.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationTracking.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber  " &
            "and SSPPApplicationTracking.strApplicationNumber = @app "

            Dim p As New SqlParameter("@app", txtWebPublisherApplicationNumber.Text)

            Dim dr As DataRow = DB.GetDataRow(query, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("datEPAStatesNotifiedAppRec")) Then
                    chbNotifiedAppReceived.Checked = False
                Else
                    chbNotifiedAppReceived.Checked = True
                    DTPNotifiedAppReceived.Value = CDate(dr.Item("datEPAStatesNotifiedAppRec"))
                End If
                If IsDBNull(dr.Item("datDraftOnWeb")) Then
                    chbDraftOnWeb.Checked = False
                Else
                    chbDraftOnWeb.Checked = True
                    DTPDraftOnWeb.Value = CDate(dr.Item("datDraftOnWEb"))
                End If
                If IsDBNull(dr.Item("datEPAStatesNotified")) Then
                    chbEPAandStatesNotified.Checked = False
                Else
                    chbEPAandStatesNotified.Checked = True
                    DTPEPAStatesNotified.Value = CDate(dr.Item("datEPAStatesNotified"))
                End If
                If IsDBNull(dr.Item("datFinalOnWeb")) Then
                    chbFinalOnWeb.Checked = False
                Else
                    chbFinalOnWeb.Checked = True
                    DTPFinalOnWeb.Value = CDate(dr.Item("datFinalOnWeb"))
                End If
                If IsDBNull(dr.Item("datEPANotified")) Then
                    chbEPANotifiedPermitOnWeb.Checked = False
                Else
                    chbEPANotifiedPermitOnWeb.Checked = True
                    DTPEPANotifiedPermitOnWeb.Value = CDate(dr.Item("datEPANotified"))
                End If
                If IsDBNull(dr.Item("datEffective")) Then
                    chbEffectiveDateOfPermit.Checked = False
                Else
                    chbEffectiveDateOfPermit.Checked = True
                    DTPEffectiveDateofPermit.Value = CDate(dr.Item("datEffective"))
                End If
                If IsDBNull(dr.Item("strTargeted")) Then
                    txtEPATargetedComments.Text = ""
                Else
                    txtEPATargetedComments.Text = dr.Item("strTargeted")
                End If
                If IsDBNull(dr.Item("datExperationDate")) Then
                    chbExpirationDate.Checked = False
                    DTPExperationDate.Value = Today
                Else
                    chbExpirationDate.Checked = True
                    DTPExperationDate.Value = CDate(dr.Item("datExperationDate"))
                End If

                ' PN is available for the following application types
                '
                ' | Code | Type       |
                ' |------|------------|
                ' | 0    | N/A        |
                ' | 2    | Acid Rain  |
                ' | 11   | SIP        |
                ' | 12   | SM         |
                ' | 14   | TV-Initial |
                ' | 16   | TV-Renewal |
                ' | 21   | SAWO       |
                ' | 22   | SAW        |

                chbPNExpires.Visible = False
                DTPPNExpires.Visible = False

                If Not IsDBNull(dr.Item("strApplicationType")) Then
                    Select Case dr.Item("strApplicationType").ToString
                        Case "0", "2", "11", "12", "14", "16", "21", "22"
                            chbPNExpires.Visible = True
                    End Select
                End If

                If IsDBNull(dr.Item("datPNExpires")) Then
                    chbPNExpires.Checked = False
                    DTPPNExpires.Value = Today
                Else
                    chbPNExpires.Checked = True
                    DTPPNExpires.Visible = chbPNExpires.Visible
                    DTPPNExpires.Value = CDate(dr.Item("datPNExpires"))
                End If

            End If
            CheckForLinks()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub SaveWebPublisherData()
        Dim EPAStatesNotifiedAppRec As Date? = Nothing
        Dim DraftOnWeb As Date? = Nothing
        Dim EPAStatesNotified As Date? = Nothing
        Dim FinalOnWeb As Date? = Nothing
        Dim EPANotifiedPermitOnWeb As Date? = Nothing
        Dim EffectiveDateOnPermit As Date? = Nothing
        Dim TargetedComments As String
        Dim ExperationDate As Date? = Nothing
        Dim PNExpires As Date? = Nothing

        Try

            If chbNotifiedAppReceived.Checked Then
                EPAStatesNotifiedAppRec = DTPNotifiedAppReceived.Value
            End If
            If chbDraftOnWeb.Checked Then
                DraftOnWeb = DTPDraftOnWeb.Value
            End If
            If chbEPAandStatesNotified.Checked Then
                EPAStatesNotified = DTPEPAStatesNotified.Value
            End If
            If chbFinalOnWeb.Checked Then
                FinalOnWeb = DTPFinalOnWeb.Value
            End If
            If chbEPANotifiedPermitOnWeb.Checked Then
                EPANotifiedPermitOnWeb = DTPEPANotifiedPermitOnWeb.Value
            End If
            If chbEffectiveDateOfPermit.Checked Then
                EffectiveDateOnPermit = DTPEffectiveDateofPermit.Value
            End If
            If chbExpirationDate.Checked Then
                ExperationDate = DTPExperationDate.Value
            End If
            TargetedComments = Mid(txtEPATargetedComments.Text, 1, 4000)
            If chbPNExpires.Checked Then
                PNExpires = DTPPNExpires.Value
            End If

            If txtWebPublisherApplicationNumber.Text = "" Then
                MessageBox.Show("Select a valid permit application number first.", "Information not saved",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Dim query As String = "Update SSPPApplicationTracking set " &
                "datDraftOnWeb = @datDraftOnWeb, " &
                "datEPAStatesNotified = @datEPAStatesNotified, " &
                "datFinalOnWeb = @datFinalOnWeb, " &
                "datEPANotified = @datEPANotified, " &
                "datEffective = @datEffective, " &
                "datEPAStatesNotifiedAppRec = @datEPAStatesNotifiedAppRec, " &
                "datExperationDate = @datExperationDate, " &
                "datPNExpires = @datPNExpires " &
                "where strApplicationNumber = @strApplicationNumber "

            Dim queryList As New List(Of String) From {query}

            Dim paramsList As New List(Of SqlParameter()) From {({
                New SqlParameter("@datDraftOnWeb", DraftOnWeb),
                New SqlParameter("@datEPAStatesNotified", EPAStatesNotified),
                New SqlParameter("@datFinalOnWeb", FinalOnWeb),
                New SqlParameter("@datEPANotified", EPANotifiedPermitOnWeb),
                New SqlParameter("@datEffective", EffectiveDateOnPermit),
                New SqlParameter("@datEPAStatesNotifiedAppRec", EPAStatesNotifiedAppRec),
                New SqlParameter("@datExperationDate", ExperationDate),
                New SqlParameter("@datPNExpires", PNExpires),
                New SqlParameter("@strApplicationNumber", txtWebPublisherApplicationNumber.Text)
            })}

            Dim query2 As String = "Update SSPPApplicationData set " &
                "strTargeted = @strTargeted " &
                "where strApplicationNumber = @strApplicationNumber "

            queryList.Add(query2)

            paramsList.Add({
                New SqlParameter("@strTargeted", TargetedComments),
                New SqlParameter("@strApplicationNumber", txtWebPublisherApplicationNumber.Text)
            })

            If lblLinkWarning.Visible Then
                Dim LinkedApplication As String
                Dim i As Integer

                For i = 0 To lbLinkApplications.Items.Count - 1
                    If lbLinkApplications.Items.Item(i).ToString <> txtWebPublisherApplicationNumber.Text Then
                        LinkedApplication = lbLinkApplications.Items.Item(i)
                    Else
                        LinkedApplication = ""
                    End If
                    If LinkedApplication <> "" Then

                        queryList.Add(query)

                        paramsList.Add({
                            New SqlParameter("@datDraftOnWeb", DraftOnWeb),
                            New SqlParameter("@datEPAStatesNotified", EPAStatesNotified),
                            New SqlParameter("@datFinalOnWeb", FinalOnWeb),
                            New SqlParameter("@datEPANotified", EPANotifiedPermitOnWeb),
                            New SqlParameter("@datEffective", EffectiveDateOnPermit),
                            New SqlParameter("@datEPAStatesNotifiedAppRec", EPAStatesNotifiedAppRec),
                            New SqlParameter("@datExperationDate", ExperationDate),
                            New SqlParameter("@datPNExpires", PNExpires),
                            New SqlParameter("@strApplicationNumber", LinkedApplication)
                        })

                        queryList.Add(query2)

                        paramsList.Add({
                            New SqlParameter("@strTargeted", TargetedComments),
                            New SqlParameter("@strApplicationNumber", LinkedApplication)
                        })

                    End If
                Next
            End If

            If DraftOnWeb.HasValue AndAlso EPAStatesNotified.HasValue Then
                queryList.Add("Update SSPPApplicationData set " &
                    "strDraftOnWebNotification = 'False' " &
                    "where strApplicationNumber = @app ")

                paramsList.Add({New SqlParameter("@app", txtWebPublisherApplicationNumber.Text)})
            End If

            If DB.RunCommand(queryList, paramsList) Then
                MsgBox("Web Information Saved", MsgBoxStyle.Information, "Application Tracking Log")
            Else
                MessageBox.Show("There was an error saving the data.", "Information not saved",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub LoadWebPublishingFacilityInformation()
        Try
            Dim FacilityName As String = ""
            Dim Street As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""

            If txtWebPublisherApplicationNumber.Text <> "" Then
                Dim query As String = "Select " &
                "strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, strFacilityState, " &
                "strFacilityZipCode " &
                "from SSPPApplicationData " &
                "Where strApplicationNumber = @app "

                Dim p As New SqlParameter("@app", txtWebPublisherApplicationNumber.Text)

                Dim dr As DataRow = DB.GetDataRow(query, p)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityStreet1")) Then
                        Street = ""
                    Else
                        Street = dr.Item("strFacilityStreet1")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        City = ""
                    Else
                        City = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strFacilityState")) Then
                        State = ""
                    Else
                        State = dr.Item("strFacilityState")
                    End If
                    If IsDBNull(dr.Item("strFacilityZipCode")) Then
                        ZipCode = ""
                    Else
                        ZipCode = dr.Item("strFacilityZipCode")
                    End If
                End If

                txtFacilityInformation.Text = FacilityName & vbCrLf &
                Street & vbCrLf &
                City & " " & State & ", " & ZipCode

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub PreviewAppReceivedEmail()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim Staff As String = ""
            Dim Unit As String = ""
            Dim temp As String = ""

            clbTitleVEmailList.Items.Clear()

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "concat(strLastName,', ',strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            " INNER JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN EPDUserProfiles " &
            "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            " LEFT JOIN LookUpEPDUnits " &
            "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
            "where (strAppReceivedNotification is Null or strAppReceivedNotification = 'False') " &
            "and strApplicationType IN ('19', '20', '21', '22', '2') " &
            "order by strFacilityName, strAPplicationNumber DESC "

            Dim dt As DataTable = DB.GetDataTable(query)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacName = ""
                Else
                    FacName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacCity = ""
                Else
                    FacCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("strApplicationTypeDesc")
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    Staff = ""
                Else
                    Staff = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = ""
                Else
                    Unit = dr.Item("strUnitDesc")
                End If

                temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                Select Case temp.Length
                    Case Is < 40
                        temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 40 To 49
                        temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 50 To 51
                        temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case Else
                        temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                End Select

                If Not clbTitleVEmailList.Items.Contains(temp) Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            Next

            txtEmailType.Text = "AppReceived"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub GenerateAppReceivedEmail()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.7(e)(2),(3), and (4) and 70.8(a)(1) and (b)(1), you are hereby notified that " &
                "Georgia EPD has received an application for the modification of an existing Part 70 permit for the " &
                "following sources: " & vbCrLf & vbCrLf

                Dim query As String = "Select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strFacilityCity,  " &
                "strApplicationTypeDesc,  " &
                "strCountyName  " &
                "from SSPPApplicationMaster, SSPPApplicationData,  " &
                "LookUpApplicationTypes, LookUpCountyInformation   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode  "

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strAppReceivedNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))
                    SQLLine = SQLLine & " SSPPApplicationMaster.strApplicationNumber = '" & temp & "' or "
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine = "And ( " & Mid(SQLLine, 1, (SQLLine.Length - 3)) & " ) "
                SQLLine2 = Mid(SQLLine2, 1, SQLLine2.Length - 3)
                query = query & SQLLine & " order by strFacilityName "
                query2 &= SQLLine2

                Dim dt As DataTable = DB.GetDataTable(query)

                For Each dr As DataRow In dt.Rows
                    If IsDBNull(dr.Item("strApplicationNumber")) Then
                        AppNumber = ""
                    Else
                        AppNumber = dr.Item("strApplicationNumber")
                    End If
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        FacName = ""
                    Else
                        FacName = dr.Item("strFacilityName")
                    End If
                    If IsDBNull(dr.Item("strFacilityCity")) Then
                        FacCity = ""
                    Else
                        FacCity = dr.Item("strFacilityCity")
                    End If
                    If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                        AppType = ""
                    Else
                        AppType = dr.Item("strApplicationTypeDesc")
                    End If
                    If IsDBNull(dr.Item("strCountyName")) Then
                        County = ""
                    Else
                        County = dr.Item("strCountyName")
                    End If
                    Select Case AppType
                        Case "MAWO"
                            AppType = "Minor modification without construction"
                        Case "MAW"
                            AppType = "Minor modification with construction"
                        Case "SAWO"
                            AppType = "Significant modification without construction"
                        Case "SAW"
                            AppType = "Significant modification with construction"
                        Case Else
                            AppType = ""
                    End Select

                    txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                    FacCity & " (" & County & " County), GA" & vbCrLf &
                    "TV-" & AppNumber & "/" & AppType & vbCrLf & vbCrLf
                Next

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. " &
                "Any questions regarding this permit application may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub PreviewDraftOnWeb()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim Staff As String = ""
            Dim Unit As String = ""
            Dim temp As String = ""
            Dim LinkedApps As String = ""
            Dim MasterApp As String = ""

            clbTitleVEmailList.Items.Clear()

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "concat(strLastName,', ',strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            " INNER JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            " INNER JOIN EPDUserProfiles " &
            "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            " LEFT JOIN LookUpEPDUnits " &
            "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
            "where (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " &
            "and (strApplicationType = '14'  or strApplicationType = '16' or strApplicationType = '21' " &
            "or strApplicationType = '22') " &
            "and strPNReady = 'True' " &
            "and datDraftOnWeb is Not Null " &
            "order by strFacilityName, strAPplicationNumber DESC "

            Dim dt As DataTable = DB.GetDataTable(query)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps &= dr.Item("strApplicationNumber").ToString & ","
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacName = ""
                Else
                    FacName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacCity = ""
                Else
                    FacCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("strApplicationTypeDesc")
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    Staff = ""
                Else
                    Staff = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = ""
                Else
                    Unit = dr.Item("strUnitDesc")
                End If

                temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                Select Case temp.Length
                    Case Is < 40
                        temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 40 To 49
                        temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 50 To 51
                        temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case Else
                        temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                End Select

                If Not clbTitleVEmailList.Items.Contains(temp) Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            Next

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                query = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = @app "

                Dim p As New SqlParameter("@app", MasterApp)

                temp = DB.GetString(query, p)

                If temp <> "" Then
                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    " concat(strLastName,', ',strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "FROM SSPPApplicationMaster " &
                    " INNER JOIN SSPPApplicationData  " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    " INNER JOIN LookUpApplicationTypes " &
                    "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    " INNER JOIN SSPPApplicationLinking " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    " INNER JOIN EPDUserProfiles " &
                    "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    " LEFT JOIN LookUpEPDUnits  " &
                    "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
                    "where SSPPApplicationLinking.strMasterApplication = @mapp "

                    Dim p2 As New SqlParameter("@mapp", temp)

                    Dim dt2 As DataTable = DB.GetDataTable(query, p2)

                    For Each dr As DataRow In dt2.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        If IsDBNull(dr.Item("StaffResponsible")) Then
                            Staff = ""
                        Else
                            Staff = dr.Item("StaffResponsible")
                        End If
                        If IsDBNull(dr.Item("strUnitDesc")) Then
                            Unit = ""
                        Else
                            Unit = dr.Item("strUnitDesc")
                        End If

                        temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                        Select Case temp.Length
                            Case Is < 40
                                temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 40 To 49
                                temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 50 To 51
                                temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case Else
                                temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                        End Select

                        If Not clbTitleVEmailList.Items.Contains(temp) Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Next
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop

            txtEmailType.Text = "DraftOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub GenerateDraftOnWebEmail()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim PNExpires As String = ""
            Dim AppLine As String = ""
            Dim LinkedApp As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached are the public notices for the " &
                "draft/proposed permits and amendments for the following sources: " & vbCrLf & vbCrLf

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    Dim query As String = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", temp)

                    LinkedApp = DB.GetString(query, p)

                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName, datPNExpires " &
                    "FROM SSPPApplicationMaster " &
                    " INNER JOIN SSPPApplicationData  " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    " INNER JOIN LookUpCountyInformation " &
                    "ON SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    " INNER JOIN LookUpApplicationTypes " &
                    "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    " LEFT JOIN SSPPApplicationTracking " &
                    "ON SSPPApplicationmaster.strAPplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                    "where SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strCountyName")) Then
                            County = ""
                        Else
                            County = dr.Item("strCountyName")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        Select Case AppType
                            Case "TV-Initial"
                                AppType = "Initial"
                            Case "TV-Renewal"
                                AppType = "Renewal"
                            Case "SAWO"
                                AppType = "Significant modification without construction"
                            Case "SAW"
                                AppType = "Significant modification with construction"
                            Case "Acid Rain"
                                AppType = "Acid Rain"
                            Case "502(b)10"
                                AppType = "502(b)10"
                            Case "MAWO"
                                AppType = "Minor modification without construction"
                            Case "MAW"
                                AppType = "Minor modification with construction"
                            Case "AA"
                                AppType = "Administrative Amendment"
                        End Select
                        If IsDBNull(dr.Item("datPNExpires")) Then
                            PNExpires = ""
                        Else
                            PNExpires = dr.Item("datPNExpires")
                        End If
                    Next

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        query = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = @app "

                        Dim p3 As New SqlParameter("@app", LinkedApp)

                        Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                        For Each dr As DataRow In dt3.Rows
                            If IsDBNull(dr.Item("strApplicationNumber")) Then
                                AppNumber = ""
                            Else
                                AppNumber = dr.Item("strApplicationNumber")
                            End If
                            If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                                AppType = ""
                            Else
                                AppType = dr.Item("strApplicationTypeDesc")
                            End If
                            Select Case AppType
                                Case "TV-Initial"
                                    AppType = "Initial"
                                Case "TV-Renewal"
                                    AppType = "Renewal"
                                Case "SAWO"
                                    AppType = "Significant modification without construction"
                                Case "SAW"
                                    AppType = "Significant modification with construction"
                                Case "Acid Rain"
                                    AppType = "Acid Rain"
                                Case "502(b)10"
                                    AppType = "502(b)10"
                                Case "MAWO"
                                    AppType = "Minor modification without construction"
                                Case "MAW"
                                    AppType = "Minor modification with construction"
                                Case "AA"
                                    AppType = "Administrative Amendment"
                            End Select
                            AppLine = AppLine & "TV-" & AppNumber & "/" & AppType & ", "
                        Next
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If Not txtEmailLetter.Text.Contains(AppLine) Then
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf &
                        "30-day expires: " & PNExpires & vbCrLf & vbCrLf
                    End If

                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                query2 &= SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " &
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " &
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf &
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " &
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf &
                "https://epd.georgia.gov/draft-title-v-permitsamendments" & vbCrLf & vbCrLf &
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " &
                "amendments may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub GenerateDraftOnWebState()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim AppLine As String = ""
            Dim LinkedApp As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached are the public notices for the draft/proposed permits and " &
                "amendments for the following sources: " & vbCrLf & vbCrLf

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    Dim query As String = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", temp)

                    LinkedApp = DB.GetString(query, p)

                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strCountyName")) Then
                            County = ""
                        Else
                            County = dr.Item("strCountyName")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        Select Case AppType
                            Case "TV-Initial"
                                AppType = "Initial"
                            Case "TV-Renewal"
                                AppType = "Renewal"
                            Case "SAWO"
                                AppType = "Significant modification without construction"
                            Case "SAW"
                                AppType = "Significant modification with construction"
                            Case "Acid Rain"
                                AppType = "Acid Rain"
                            Case "502(b)10"
                                AppType = "502(b)10"
                            Case "MAWO"
                                AppType = "Minor modification without construction"
                            Case "MAW"
                                AppType = "Minor modification with construction"
                            Case "AA"
                                AppType = "Administrative Amendment"
                            Case Else
                                'AppType = AppType
                        End Select
                    Next

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        query = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = @app "

                        Dim p3 As New SqlParameter("@app", LinkedApp)

                        Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                        For Each dr As DataRow In dt3.Rows
                            If IsDBNull(dr.Item("strApplicationNumber")) Then
                                AppNumber = ""
                            Else
                                AppNumber = dr.Item("strApplicationNumber")
                            End If
                            If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                                AppType = ""
                            Else
                                AppType = dr.Item("strApplicationTypeDesc")
                            End If
                            Select Case AppType
                                Case "TV-Initial"
                                    AppType = "Initial"
                                Case "TV-Renewal"
                                    AppType = "Renewal"
                                Case "SAWO"
                                    AppType = "Significant modification without construction"
                                Case "SAW"
                                    AppType = "Significant modification with construction"
                                Case "Acid Rain"
                                    AppType = "Acid Rain"
                                Case "502(b)10"
                                    AppType = "502(b)10"
                                Case "MAWO"
                                    AppType = "Minor modification without construction"
                                Case "MAW"
                                    AppType = "Minor modification with construction"
                                Case "AA"
                                    AppType = "Administrative Amendment"
                                Case Else
                                    'AppType = AppType
                            End Select
                            AppLine = AppLine & "TV-" & AppNumber & "/" & AppType & ", "
                        Next
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If Not txtEmailLetter.Text.Contains(AppLine) Then
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If

                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                query2 &= SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " &
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " &
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf &
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " &
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf &
                "https://epd.georgia.gov/draft-title-v-permitsamendments" & vbCrLf & vbCrLf &
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " &
                "amendments may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub PreviewMinorModOnWeb()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim Staff As String = ""
            Dim Unit As String = ""
            Dim temp As String = ""
            Dim LinkedApps As String = ""
            Dim MasterApp As String = ""

            clbTitleVEmailList.Items.Clear()

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            " concat(strLastName,', ',strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            " INNER JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN EPDUserProfiles " &
            "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            " LEFT JOIN LookUpEPDUnits " &
            "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "where datEPAStatesNotified is not Null " &
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " &
            "and (strApplicationType = '19'  or strApplicationType = '20') " &
            "order by strFacilityName, strApplicationNumber DESC "

            Dim dt As DataTable = DB.GetDataTable(query)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps &= dr.Item("strApplicationNumber").ToString & ","
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacName = ""
                Else
                    FacName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacCity = ""
                Else
                    FacCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("strApplicationTypeDesc")
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    Staff = ""
                Else
                    Staff = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = ""
                Else
                    Unit = dr.Item("strUnitDesc")
                End If

                temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                Select Case temp.Length
                    Case Is < 40
                        temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 40 To 49
                        temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 50 To 51
                        temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case Else
                        temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                End Select

                If Not clbTitleVEmailList.Items.Contains(temp) Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            Next

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                query = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = @app "

                Dim p As New SqlParameter("@app", MasterApp)

                temp = DB.GetString(query, p)

                If temp <> "" Then
                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    " concat(strLastName,', ',strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "FROM SSPPApplicationMaster " &
                    " INNER JOIN SSPPApplicationData  " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    " INNER JOIN LookUpApplicationTypes " &
                    "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    " INNER JOIN SSPPApplicationLinking " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    " INNER JOIN EPDuserProfiles " &
                    "ON SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID  " &
                    " LEFT JOIN LookUPEPDunits " &
                    "ON EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
                    "where SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt2 As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt2.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        If IsDBNull(dr.Item("StaffResponsible")) Then
                            Staff = ""
                        Else
                            Staff = dr.Item("StaffResponsible")
                        End If
                        If IsDBNull(dr.Item("strUnitDesc")) Then
                            Unit = ""
                        Else
                            Unit = dr.Item("strUnitDesc")
                        End If

                        temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                        Select Case temp.Length
                            Case Is < 40
                                temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 40 To 49
                                temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 50 To 51
                                temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case Else
                                temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                        End Select

                        If Not clbTitleVEmailList.Items.Contains(temp) Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Next
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop


            txtEmailType.Text = "MinorOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub GenerateMinorOnWebEmail()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim AppLine As String = ""
            Dim LinkedApp As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached is the proposed Part " &
                "70 permit modification and permit amendment narrative for the following: " & vbCrLf & vbCrLf

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    Dim query As String = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", temp)

                    LinkedApp = DB.GetString(query, p)

                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strCountyName")) Then
                            County = ""
                        Else
                            County = dr.Item("strCountyName")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        Select Case AppType
                            Case "TV-Initial"
                                AppType = "Initial"
                            Case "TV-Renewal"
                                AppType = "Renewal"
                            Case "SAWO"
                                AppType = "Significant modification without construction"
                            Case "SAW"
                                AppType = "Significant modification with construction"
                            Case "Acid Rain"
                                AppType = "Acid Rain"
                            Case "502(b)10"
                                AppType = "502(b)10"
                            Case "MAWO"
                                AppType = "Minor modification without construction"
                            Case "MAW"
                                AppType = "Minor modification with construction"
                            Case "AA"
                                AppType = "Administrative Amendment"
                            Case Else
                                'AppType = AppType
                        End Select
                    Next

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        query = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = @app "

                        Dim p3 As New SqlParameter("@app", LinkedApp)

                        Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                        For Each dr As DataRow In dt3.Rows
                            If IsDBNull(dr.Item("strApplicationNumber")) Then
                                AppNumber = ""
                            Else
                                AppNumber = dr.Item("strApplicationNumber")
                            End If
                            If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                                AppType = ""
                            Else
                                AppType = dr.Item("strApplicationTypeDesc")
                            End If
                            Select Case AppType
                                Case "TV-Initial"
                                    AppType = "Initial"
                                Case "TV-Renewal"
                                    AppType = "Renewal"
                                Case "SAWO"
                                    AppType = "Significant modification without construction"
                                Case "SAW"
                                    AppType = "Significant modification with construction"
                                Case "Acid Rain"
                                    AppType = "Acid Rain"
                                Case "502(b)10"
                                    AppType = "502(b)10"
                                Case "MAWO"
                                    AppType = "Minor modification without construction"
                                Case "MAW"
                                    AppType = "Minor modification with construction"
                                Case "AA"
                                    AppType = "Administrative Amendment"
                                Case Else
                                    'AppType = AppType
                            End Select
                            AppLine = AppLine & "TV-" & AppNumber & "/" & AppType & ", "
                        Next
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If Not txtEmailLetter.Text.Contains(AppLine) Then
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If

                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                query2 &= SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "EPA's review of the proposed minor amendment extends from 45 days following the date of this " &
                "message. Please reply to acknowledge receipt of this notification. Any questions regarding this " &
                "proposed permit amendment may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub GenerateMinorOnWebState()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim AppLine As String = ""
            Dim LinkedApp As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached is the proposed Part 70 permit modification and  " &
                "permit amendment narrative for the following source: " & vbCrLf & vbCrLf

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    Dim query As String = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", temp)

                    LinkedApp = DB.GetString(query, p)

                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strCountyName")) Then
                            County = ""
                        Else
                            County = dr.Item("strCountyName")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        Select Case AppType
                            Case "TV-Initial"
                                AppType = "Initial"
                            Case "TV-Renewal"
                                AppType = "Renewal"
                            Case "SAWO"
                                AppType = "Significant modification without construction"
                            Case "SAW"
                                AppType = "Significant modification with construction"
                            Case "Acid Rain"
                                AppType = "Acid Rain"
                            Case "502(b)10"
                                AppType = "502(b)10"
                            Case "MAWO"
                                AppType = "Minor modification without construction"
                            Case "MAW"
                                AppType = "Minor modification with construction"
                            Case "AA"
                                AppType = "Administrative Amendment"
                            Case Else
                                'AppType = AppType
                        End Select
                    Next

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        query = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = @app "

                        Dim p3 As New SqlParameter("@app", LinkedApp)

                        Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                        For Each dr As DataRow In dt3.Rows
                            If IsDBNull(dr.Item("strApplicationNumber")) Then
                                AppNumber = ""
                            Else
                                AppNumber = dr.Item("strApplicationNumber")
                            End If
                            If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                                AppType = ""
                            Else
                                AppType = dr.Item("strApplicationTypeDesc")
                            End If
                            Select Case AppType
                                Case "TV-Initial"
                                    AppType = "Initial"
                                Case "TV-Renewal"
                                    AppType = "Renewal"
                                Case "SAWO"
                                    AppType = "Significant modification without construction"
                                Case "SAW"
                                    AppType = "Significant modification with construction"
                                Case "Acid Rain"
                                    AppType = "Acid Rain"
                                Case "502(b)10"
                                    AppType = "502(b)10"
                                Case "MAWO"
                                    AppType = "Minor modification without construction"
                                Case "MAW"
                                    AppType = "Minor modification with construction"
                                Case "AA"
                                    AppType = "Administrative Amendment"
                                Case Else
                                    'AppType = AppType
                            End Select
                            AppLine = AppLine & "TV-" & AppNumber & "/" & AppType & ", "
                        Next
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If Not txtEmailLetter.Text.Contains(AppLine) Then
                        txtEmailLetter.Text &= FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If

                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                query2 &= SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. Any questions regarding this proposed " &
                "permit amendment may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
    Private Sub PreviewFinalOnWeb()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim Staff As String = ""
            Dim Unit As String = ""
            Dim temp As String = ""
            Dim LinkedApps As String = ""
            Dim MasterApp As String = ""

            clbTitleVEmailList.Items.Clear()

            Dim query As String = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            " concat(strLastName,', ',strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "FROM SSPPApplicationMaster " &
            " INNER JOIN SSPPApplicationData " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            " INNER JOIN LookUpApplicationTypes " &
            "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            " INNER JOIN SSPPApplicationTracking " &
            "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            " INNER JOIN EPDUserProfiles " &
            "ON SSPPApplicationMaster.strStaffResponsible = EPDuserProfiles.numUserID " &
            " LEFT JOIN LookUpEPDUnits " &
            "ON EPDuserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
            "where (strFinalOnWebNotification is Null or strFinalOnWebNotification = 'False') " &
            "and (strApplicationType = '14'  or strApplicationType = '16' " &
            "or strApplicationType = '19' or strApplicationType = '20' " &
            "or strApplicationType = '21' or strApplicationType = '22' " &
            "or strApplicationType = '26' or strApplicationType = '15' " &
            "or strApplicationType = '2') " &
            "and DatFinalOnWeb is Not Null " &
            "order by strFacilityName, strAPplicationNumber DESC "

            Dim dt As DataTable = DB.GetDataTable(query)

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps &= dr.Item("strApplicationNumber").ToString & ","
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacName = ""
                Else
                    FacName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strFacilityCity")) Then
                    FacCity = ""
                Else
                    FacCity = dr.Item("strFacilityCity")
                End If
                If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                    AppType = ""
                Else
                    AppType = dr.Item("strApplicationTypeDesc")
                End If
                If IsDBNull(dr.Item("StaffResponsible")) Then
                    Staff = ""
                Else
                    Staff = dr.Item("StaffResponsible")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = ""
                Else
                    Unit = dr.Item("strUnitDesc")
                End If

                temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                Select Case temp.Length
                    Case Is < 40
                        temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 40 To 49
                        temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case 50 To 51
                        temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                    Case Else
                        temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                End Select

                If Not clbTitleVEmailList.Items.Contains(temp) Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            Next

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                query = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = @app "

                Dim p As New SqlParameter("@app", MasterApp)

                temp = DB.GetString(query, p)

                If temp <> "" Then
                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    " concat(strLastName,', ',strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "FROM SSPPApplicationMaster " &
                    " INNER JOIN SSPPApplicationData  " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    " INNER JOIN LookUpApplicationTypes " &
                    "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    " INNER JOIN SSPPApplicationLinking " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    " INNER JOIN EPDUserProfiles " &
                    "ON SSPPApplicationMaster.strStaffResponsible = EPDuserPRofiles.numUserID  " &
                    " LEFT JOIN LookUpEPDUnits  " &
                    "ON EPDUserProfiles.numUnit = LookUpEPDunits.numUnitCode " &
                    "where SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim p2 As New SqlParameter("@app", temp)

                    Dim dt2 As DataTable = DB.GetDataTable(query, p2)

                    For Each dr As DataRow In dt2.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        If IsDBNull(dr.Item("StaffResponsible")) Then
                            Staff = ""
                        Else
                            Staff = dr.Item("StaffResponsible")
                        End If
                        If IsDBNull(dr.Item("strUnitDesc")) Then
                            Unit = ""
                        Else
                            Unit = dr.Item("strUnitDesc")
                        End If

                        temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                        Select Case temp.Length
                            Case Is < 40
                                temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 40 To 49
                                temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 50 To 51
                                temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case Else
                                temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                        End Select

                        If Not clbTitleVEmailList.Items.Contains(temp) Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Next
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop

            txtEmailType.Text = "FinalOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub GenerateFinalOnWeb()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim County As String = ""
            Dim PermitNumber As String = ""
            Dim DateIssued As String = ""
            Dim AppLine As String = ""
            Dim LinkedApp As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 AndAlso clbTitleVEmailList.CheckedItems.Count > 0 Then
                txtEmailLetter.Text = "In accordance with condition V.A.1.a of Georgia's Title V Agreement, the final Part 70 " &
                "Permits were issued to the following sources:" & vbCrLf & vbCrLf

                Dim query2 As String = "Update SSPPApplicationData set " &
                "strFinalOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    Dim query As String = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", temp)

                    LinkedApp = DB.GetString(query, p)

                    query = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strCountyName, strPermitNumber,  " &
                    "datPermitIssued, datEffective, " &
                    "strApplicationTypeDesc " &
                    "FROM SSPPApplicationMaster " &
                    " INNER JOIN SSPPApplicationData " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    " INNER JOIN LookUpCountyInformation " &
                    "ON SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    " INNER JOIN LookUpApplicationTypes " &
                    "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    " inner JOIN SSPPApplicationTracking " &
                    "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                    "where SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim dt As DataTable = DB.GetDataTable(query, p)

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strCountyName")) Then
                            County = ""
                        Else
                            County = dr.Item("strCountyName")
                        End If
                        If IsDBNull(dr.Item("strPermitNumber")) Then
                            PermitNumber = "Permit - N/A"
                        Else
                            PermitNumber = dr.Item("strPermitNumber")
                            PermitNumber = "Permit " & Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & Mid(PermitNumber, 8, 4) &
                            "-" & Mid(PermitNumber, 12, 1) & "-" & Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                        End If
                        If IsDBNull(dr.Item("datPermitIssued")) Then
                            If IsDBNull(dr.Item("datEffective")) Then
                                DateIssued = ""
                            Else
                                DateIssued = "Effective: " & dr.Item("datEffective").ToString
                            End If
                        Else
                            DateIssued = "Effective: " & dr.Item("datPermitIssued").ToString
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If
                        Select Case AppType
                            Case "TV-Initial"
                                AppType = "Initial"
                            Case "TV-Renewal"
                                AppType = "Renewal"
                            Case "SAWO"
                                AppType = "Significant modification without construction"
                            Case "SAW"
                                AppType = "Significant modification with construction"
                            Case "Acid Rain"
                                AppType = "Acid Rain"
                            Case "502(b)10"
                                AppType = "502(b)10"
                            Case "MAWO"
                                AppType = "Minor modification without construction"
                            Case "MAW"
                                AppType = "Minor modification with construction"
                            Case "AA"
                                AppType = "Administrative Amendment"
                            Case Else
                                'AppType = AppType
                        End Select
                    Next

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        query = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = @app "

                        Dim p3 As New SqlParameter("@app", LinkedApp)

                        Dim dt3 As DataTable = DB.GetDataTable(query, p3)

                        For Each dr As DataRow In dt3.Rows
                            If IsDBNull(dr.Item("strApplicationNumber")) Then
                                AppNumber = ""
                            Else
                                AppNumber = dr.Item("strApplicationNumber")
                            End If
                            If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                                AppType = ""
                            Else
                                AppType = dr.Item("strApplicationTypeDesc")
                            End If
                            Select Case AppType
                                Case "TV-Initial"
                                    AppType = "Initial"
                                Case "TV-Renewal"
                                    AppType = "Renewal"
                                Case "SAWO"
                                    AppType = "Significant modification without construction"
                                Case "SAW"
                                    AppType = "Significant modification with construction"
                                Case "Acid Rain"
                                    AppType = "Acid Rain"
                                Case "502(b)10"
                                    AppType = "502(b)10"
                                Case "MAWO"
                                    AppType = "Minor modification without construction"
                                Case "MAW"
                                    AppType = "Minor modification with construction"
                                Case "AA"
                                    AppType = "Administrative Amendment"
                                Case Else
                                    'AppType = AppType
                            End Select
                            AppLine = AppLine & "TV-" & AppNumber & "/" & AppType & ", "
                        Next
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If Not txtEmailLetter.Text.Contains(AppLine) Then
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf &
                        PermitNumber & vbCrLf & DateIssued & vbCrLf & vbCrLf
                    End If

                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                query2 &= SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The final permit, permit review narrative and in most cases the " &
                "permit application will be available from the Georgia Air Permit Search Engine web page located at: " &
                vbCrLf & vbCrLf &
                "https://permitsearch.gaepd.org/" & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the final permits " &
                "may be directed to the Air Permit Manager by calling (404) 363-7000."

                DB.RunCommand(query2)

            Else
                txtEmailLetter.Clear()
                MsgBox("Click preview button first and ensure at least one applications is selected.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub AddAppToList()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""

            Dim Staff As String = ""
            Dim Unit As String = ""

            Dim temp As String = ""

            Select Case txtEmailType.Text
                Case "AppReceived", "DraftOnWeb", "MinorOnWeb", "FinalOnWeb"
                    Dim query As String = "Select " &
                        "SSPPApplicationMaster.strApplicationNumber, " &
                        "strFacilityName, strFacilityCity, " &
                        "strApplicationTypeDesc, " &
                        " concat(strLastName,', ',strFirstName) as StaffResponsible, " &
                        "strUnitDesc " &
                        "FROM SSPPApplicationMaster " &
                        " INNER JOIN SSPPApplicationData " &
                        "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                        " INNER JOIN LookUpApplicationTypes " &
                        "ON SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        " INNER JOIN SSPPApplicationTracking " &
                        "ON SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                        " INNER JOIN EPDUserProfiles " &
                        "ON SSPPApplicationMaster.strStaffResponsible = EPDuserProfiles.numUserID " &
                        " LEFT JOIN LookUpEPDUnits " &
                        "ON EPDuserProfiles.numUnit = LookUpEPDUnits.numUnitCode " &
                        "where SSPPApplicationMaster.strApplicationNumber = @app "

                    Dim p As New SqlParameter("@app", txtApplicationNumberToAdd.Text)

                    Dim dr As DataRow = DB.GetDataRow(query, p)

                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("strApplicationNumber")) Then
                            AppNumber = ""
                        Else
                            AppNumber = dr.Item("strApplicationNumber")
                        End If
                        If IsDBNull(dr.Item("strFacilityName")) Then
                            FacName = ""
                        Else
                            FacName = dr.Item("strFacilityName")
                        End If
                        If IsDBNull(dr.Item("strFacilityCity")) Then
                            FacCity = ""
                        Else
                            FacCity = dr.Item("strFacilityCity")
                        End If
                        If IsDBNull(dr.Item("strApplicationTypeDesc")) Then
                            AppType = ""
                        Else
                            AppType = dr.Item("strApplicationTypeDesc")
                        End If

                        If IsDBNull(dr.Item("StaffResponsible")) Then
                            Staff = ""
                        Else
                            Staff = dr.Item("StaffResponsible")
                        End If
                        If IsDBNull(dr.Item("strUnitDesc")) Then
                            Unit = ""
                        Else
                            Unit = dr.Item("strUnitDesc")
                        End If

                        temp = AppNumber & " - " & FacName & " - (" & FacCity & ") - " & AppType

                        Select Case temp.Length
                            Case Is < 40
                                temp = temp & vbTab & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 40 To 49
                                temp = temp & vbTab & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case 50 To 51
                                temp = temp & vbTab & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                            Case Else
                                temp = temp & vbTab & "Staff Responsible:  " & Staff & "     -     Staff Unit: " & Unit
                        End Select

                        If Not clbTitleVEmailList.Items.Contains(temp) Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Else
                        MsgBox("Unable to add this Application to list.", MsgBoxStyle.Information, "Data Management Tools")
                    End If
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnViewApplication_Click(sender As Object, e As EventArgs) Handles btnViewApplication.Click
        OpenFormPermitApplication(txtWebPublisherApplicationNumber.Text)
    End Sub
    Private Sub btnReloadGrid_Click(sender As Object, e As EventArgs) Handles btnReloadGrid.Click

        LoadWebPublisherDataGrid()

    End Sub
    Private Sub dgrWebPublisher_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrWebPublisher.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrWebPublisher.HitTest(e.X, e.Y)
        Try

            If hti.Type = DataGridViewHitTestType.Cell AndAlso
                Not IsDBNull(dgrWebPublisher(0, hti.RowIndex)) Then

                txtWebPublisherApplicationNumber.Text = dgrWebPublisher(0, hti.RowIndex).Value.ToString

                If txtWebPublisherApplicationNumber.Text <> "" Then
                    LoadWebPublisherApplicationData()
                    LoadWebPublishingFacilityInformation()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbNotifiedAppReceived_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotifiedAppReceived.CheckedChanged
        Try

            If chbNotifiedAppReceived.Checked Then
                DTPNotifiedAppReceived.Visible = True
            Else
                DTPNotifiedAppReceived.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbDraftOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbDraftOnWeb.CheckedChanged
        Try

            If chbDraftOnWeb.Checked Then
                DTPDraftOnWeb.Visible = True
            Else
                DTPDraftOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbPNExpires_CheckedChanged(sender As Object, e As EventArgs) Handles chbPNExpires.CheckedChanged
        DTPPNExpires.Visible = chbPNExpires.Checked AndAlso chbPNExpires.Visible
    End Sub
    Private Sub chbEPAandStatesNotified_CheckedChanged(sender As Object, e As EventArgs) Handles chbEPAandStatesNotified.CheckedChanged
        Try

            If chbEPAandStatesNotified.Checked Then
                DTPEPAStatesNotified.Visible = True
            Else
                DTPEPAStatesNotified.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbFinalOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbFinalOnWeb.CheckedChanged
        Try

            If chbFinalOnWeb.Checked Then
                DTPFinalOnWeb.Visible = True
            Else
                DTPFinalOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbEPANotifiedPermitOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbEPANotifiedPermitOnWeb.CheckedChanged
        Try

            If chbEPANotifiedPermitOnWeb.Checked Then
                DTPEPANotifiedPermitOnWeb.Visible = True
            Else
                DTPEPANotifiedPermitOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbEffectiveDateOfPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbEffectiveDateOfPermit.CheckedChanged
        Try

            If chbEffectiveDateOfPermit.Checked Then
                DTPEffectiveDateofPermit.Visible = True
            Else
                DTPEffectiveDateofPermit.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub chbExperationDate_CheckedChanged(sender As Object, e As EventArgs) Handles chbExpirationDate.CheckedChanged
        Try

            If chbExpirationDate.Checked Then
                DTPExperationDate.Visible = True
            Else
                DTPExperationDate.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub btnSaveWebPublisher_Click(sender As Object, e As EventArgs) Handles btnSaveWebPublisher.Click
        SaveWebPublisherData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try

            chbDraftOnWeb.Checked = False
            DTPDraftOnWeb.Value = Today
            chbEPAandStatesNotified.Checked = False
            DTPEPAStatesNotified.Value = Today
            chbFinalOnWeb.Checked = False
            DTPFinalOnWeb.Value = Today
            chbEPANotifiedPermitOnWeb.Checked = False
            DTPEPANotifiedPermitOnWeb.Value = Today
            chbEffectiveDateOfPermit.Checked = False
            DTPEffectiveDateofPermit.Value = Today
            txtEPATargetedComments.Clear()
            txtWebPublisherApplicationNumber.Clear()
            DTPExperationDate.Value = Today
            chbExpirationDate.Checked = False
            txtFacilityInformation.Clear()
            chbNotifiedAppReceived.Checked = False
            DTPNotifiedAppReceived.Value = Today
            chbPNExpires.Checked = False
            DTPPNExpires.Value = Today


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnSearchForApplication_Click(sender As Object, e As EventArgs) Handles btnSearchForApplication.Click

        LoadWebPublisherApplicationData()
        LoadWebPublishingFacilityInformation()

    End Sub
    Private Sub txtWebPublisherApplicationNumber_Leave(sender As Object, e As EventArgs) Handles txtWebPublisherApplicationNumber.Leave
        Try

            If txtWebPublisherApplicationNumber.Text <> "" Then
                LoadWebPublisherApplicationData()
                LoadWebPublishingFacilityInformation()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtWebPublisherApplicationNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWebPublisherApplicationNumber.KeyPress
        Try

            If e.KeyChar = ChrW(13) AndAlso
                txtWebPublisherApplicationNumber.Text <> "" Then
                LoadWebPublisherApplicationData()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)

        End Try

    End Sub
    Private Sub btnPreviewESNReceived_Click(sender As Object, e As EventArgs) Handles btnPreviewESNReceived.Click
        Try

            PreviewAppReceivedEmail()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewESNReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewESNReceived.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                PreviewAppReceivedEmail()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_Click(sender As Object, e As EventArgs) Handles btnPreviewDraftOnWeb.Click
        Try

            PreviewDraftOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewDraftOnWeb.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                PreviewDraftOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewMinorMod_Click(sender As Object, e As EventArgs) Handles btnPreviewMinorMod.Click
        Try

            PreviewMinorModOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewMinorMod_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewMinorMod.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                PreviewMinorModOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_Click(sender As Object, e As EventArgs) Handles btnPreviewFinalOnWeb.Click
        Try

            PreviewFinalOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewFinalOnWeb.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                PreviewFinalOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEmailESNReceived_Click(sender As Object, e As EventArgs) Handles btnEmailESNReceived.Click

        GenerateAppReceivedEmail()

    End Sub
    Private Sub btnEmailESNReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailESNReceived.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                GenerateAppReceivedEmail()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEmailDraftOnWeb_Click(sender As Object, e As EventArgs) Handles btnEmailDraftOnWeb.Click

        GenerateDraftOnWebEmail()

    End Sub
    Private Sub btnEmailDraftOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailDraftOnWeb.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                GenerateDraftOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEmailDraftOnWebState_Click(sender As Object, e As EventArgs) Handles btnEmailDraftOnWebState.Click

        GenerateDraftOnWebState()

    End Sub
    Private Sub btnEmailDraftOnWebState_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailDraftOnWebState.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                GenerateDraftOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnMinorModOnWebEPD_Click(sender As Object, e As EventArgs) Handles btnMinorModOnWebEPD.Click

        GenerateMinorOnWebEmail()

    End Sub
    Private Sub btnMinorModOnWebEPD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnMinorModOnWebEPD.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                GenerateMinorOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnMinorModOnWebState_Click(sender As Object, e As EventArgs) Handles btnMinorModOnWebState.Click

        GenerateMinorOnWebState()

    End Sub
    Private Sub btnMinorModOnWebState_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnMinorModOnWebState.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                GenerateMinorOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnEmailFinalOnWeb_Click(sender As Object, e As EventArgs) Handles btnEmailFinalOnWeb.Click

        GenerateFinalOnWeb()

    End Sub
    Private Sub btnEmailFinalOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailFinalOnWeb.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then

                GenerateFinalOnWeb()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddApplicationToList_Click(sender As Object, e As EventArgs) Handles btnAddApplicationToList.Click
        Try

            AddAppToList()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtApplicationNumberToAdd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplicationNumberToAdd.KeyPress
        Try

            If e.KeyChar = ChrW(13) Then
                AddAppToList()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub txtEmailLetter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmailLetter.KeyPress
        Try

            If e.KeyChar = ChrW(1) Then
                txtEmailLetter.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

End Class