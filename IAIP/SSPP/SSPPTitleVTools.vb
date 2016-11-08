'Imports System.DateTime
Imports System.Data.SqlClient
'Imports System.IO
Imports System.Data.OleDb
'Imports System.Data.Odbc

Public Class SSPPTitleVTools
    Dim SQL, SQL2 As String
    Dim dsWebPublisher As DataSet
    Dim daWebPublisher As SqlDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As New SqlDataAdapter
    Dim ds As DataSet
    Dim da As SqlDataAdapter
    Dim airsno As String
    Dim Startdate As String
    Dim EndDate As String
    Dim recExist As Boolean
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub DMUTitleVTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            LoadPermissions()

            If IO.Directory.Exists(New System.IO.FileInfo("S:\Permit\GATV\Warehouse\GATVWHSE.mdb").DirectoryName) = True Then
                btnLoadFromWarehouse.Visible = True
            Else
                btnLoadFromWarehouse.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadWebPublisherDataGrid(AppNum As String)
        Dim SQLLine As String

        Try



            If AppNum = "Load" Then
                SQLLine = ""
            Else
                SQLLine = " and SSPPApplicationMaster.strApplicationNumber = '" & AppNum & "' "
            End If

            SQL = "select " &
            "CONVERT(int, SSPPApplicationMaster.strApplicationNumber) as ApplicationNumber, " &
            "case " &
            "When datDraftIssued is Null then ' ' " &
            "ELSE format(datDraftIssued, 'yyyy-MM-dd') " &
            "END datDraftIssued, " &
            "case " &
            "When datPermitIssued is Null then ' ' " &
            "ELSE format(datPermitIssued, 'yyyy-MM-dd') " &
            "END datPermitIssued, " &
            "case " &
            "When datExperationDate is Null then ' ' " &
            "ELSE format(datExperationDate, 'yyyy-MM-dd') " &
            "END datExperationDate, " &
            "strFacilityName, " &
            "case  " &
            "        when strPermitNumber is NULL then ' '  " &
            "         else SUBSTRING(strPermitNumber, 1, 4)|| '-' ||SUBSTRING(strPermitNumber, 5, 3)|| '-'  " &
            "        ||SUBSTRING(strPermitNumber, 8, 4)|| '-' ||SUBSTRING(strPermitNumber, 12, 1)|| '-'           " &
            "        ||SUBSTRING(strPermitNumber, 13, 2)|| '-' ||SUBSTRING(strPermitNumber, 15, 1) " &
            "end As strPermitNumber, " &
            "Case " &
            "when datFinalizedDate is Null then ' ' " &
            "Else format(datFinalizedDate, 'yyyy-MM-dd') " &
            "End datFinalizedDate, " &
            "strApplicationTypeDesc " &
            "from SSPPApplicationTracking, SSPPApplicationData, " &
            "SSPPApplicationMaster, LookUpApplicationTypes " &
            "where SSPPApplicationTracking.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and LookUpApplicationTypes.strApplicationTypeCode = strApplicationType " &
            "and SSPPApplicationTracking.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber " &
            "and (datDraftIssued is Not Null or datPermitIssued is Not Null or datEPAStatesNotified is Not Null) " &
            "and datFinalOnWeb is Null " &
            "and datFinalizedDate is Null " &
            "and (strApplicationType = '17' or strApplicationType = '14' or strApplicationType = '16' " &
            " or strApplicationType = '15' or strApplicationType = '26' or strApplicationType = '19' " &
            " or strApplicationType = '20' or strApplicationType = '22' or strApplicationType = '21')" & SQLLine

            SQL = "SELECT " &
            "CONVERT(int, SSPPApplicationMaster.strApplicationNumber) AS ApplicationNumber,  " &
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
            "END datExperationDate,  " &
            "strFacilityName,  " &
            "CASE   " &
            "   WHEN strPermitNumber IS NULL THEN ' '   " &
            "   ELSE SUBSTRING(strPermitNumber, 1, 4)|| '-' ||SUBSTRING(strPermitNumber, 5, 3)|| '-'   " &
            "    ||SUBSTRING(strPermitNumber, 8, 4)|| '-' ||SUBSTRING(strPermitNumber, 12, 1)|| '-'         " &
            "    ||SUBSTRING(strPermitNumber, 13, 2)|| '-' ||SUBSTRING(strPermitNumber, 15, 1)  " &
            "END AS strPermitNumber,  " &
            "CASE  " &
            "   WHEN datFinalizedDate IS NULL THEN ' '  " &
            "   ELSE format(datFinalizedDate, 'yyyy-MM-dd') " &
            "END datFinalizedDate,  " &
            "strApplicationTypeDesc  " &
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
            "and datFinalizedDate is Null " & SQLLine &
            " order by ApplicationNumber Desc "

            dsWebPublisher = New DataSet

            daWebPublisher = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            Try


                daWebPublisher.Fill(dsWebPublisher, "WebPublisher")
                dgrWebPublisher.DataSource = dsWebPublisher
                dgrWebPublisher.DataMember = "WebPublisher"

            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try

            txtTVCount.Text = dsWebPublisher.Tables(0).Rows.Count


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatWebPublisherDataGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "WebPublisher"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ApplicationNumber"
            objtextcol.HeaderText = "APL #"
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFacilityName"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.Width = 250
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPermitNumber"
            objtextcol.HeaderText = "Permit Number"
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strApplicationTypeDesc"
            objtextcol.HeaderText = "App Type"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datDraftIssued"
            objtextcol.HeaderText = "Draft Issued"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datPermitIssued"
            objtextcol.HeaderText = "Final Action"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "datExpirationDate"
            objtextcol.HeaderText = "Experation Date"
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrWebPublisher.TableStyles.Clear()
            dgrWebPublisher.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrWebPublisher.CaptionText = "Web Publisher Active Title V Applications"
            dgrWebPublisher.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub CheckForLinks()
        Dim MasterApplication As String
        Dim ApplicationCount As String = 0

        Try

            MasterApplication = ""
            lbLinkApplications.Items.Clear()

            If txtWebPublisherApplicationNumber.Text <> "" Then
                SQL = "Select " &
                "strMasterApplication, strApplicationNumber " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    MasterApplication = dr.Item("strMasterApplication")
                Else
                    MasterApplication = ""
                    lbLinkApplications.Items.Clear()
                    lblLinkWarning.Visible = False
                End If
                If MasterApplication <> "" Then
                    SQL = "Select " &
                    "strMasterApplication, strApplicationNumber " &
                    "from SSPPApplicationLinking " &
                    "where strMasterApplication = '" & MasterApplication & "' " &
                    "order by strApplicationNumber "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        lbLinkApplications.Items.Add(dr.Item("strApplicationNumber"))
                        ApplicationCount += 1
                    End While
                    lblLinkWarning.Visible = True
                End If
            Else
                lbLinkApplications.Items.Clear()
                lblLinkWarning.Visible = False
            End If
            If lbLinkApplications.Items.Contains(txtWebPublisherApplicationNumber.Text) Then
            Else
                lbLinkApplications.Items.Add(txtWebPublisherApplicationNumber.Text)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadPermissions()
        Try

            TCDMUTools.TabPages.Remove(TPWebPublishing)
            TCDMUTools.TabPages.Remove(TPTVEmails)
            TCDMUTools.TabPages.Remove(TPTitleVRenewals)
            TCDMUTools.TabPages.Remove(TPPermittingContact)

            'Web Publishers
            If AccountFormAccess(131, 2) = "1" Then
                TCDMUTools.TabPages.Add(TPWebPublishing)
                TCDMUTools.TabPages.Add(TPTVEmails)
                TCDMUTools.TabPages.Add(TPTitleVRenewals)
                TCDMUTools.TabPages.Add(TPPermittingContact)

                LoadWebPublisherDataGrid("Load")
                FormatWebPublisherDataGrid()

                DTPNotifiedAppReceived.Value = Today
                DTPDraftOnWeb.Value = Today
                DTPEffectiveDateofPermit.Value = Today
                DTPEPANotifiedPermitOnWeb.Value = Today
                DTPEPAStatesNotified.Value = Today
                DTPFinalOnWeb.Value = Today
                DTPTitleVRenewalStart.Value = Today
                DTPTitleVRenewalEnd.Value = Today
                DTPTitleVRenewalStart.Value = Today
                DTPPNExpires.Value = Today
                DTPExperationDate.Value = Today
                DTPTitleVRenewalEnd.Text = Format(Today.AddMonths(1), "dd-MMM-yyyy")

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub LoadDataSetInformation()
        Try
            SQL = "select " &
            "(strLastName||', '||strFirstName) as UserName,  " &
            "numUserID  " &
            "from EPDUserProfiles  " &
            "order by strLastName  "

            dsStaff = New DataSet

            daStaff = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Subs and Functions"
    Sub LoadWebPublisherApplicationData()
        Try

            Dim AppType As String = ""

            SQL = "Select " &
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
            "and SSPPApplicationTracking.strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("datEPAStatesNotifiedAppRec")) Then
                    chbNotifiedAppReceived.Checked = False
                Else
                    chbNotifiedAppReceived.Checked = True
                    DTPNotifiedAppReceived.Text = dr.Item("datEPAStatesNotifiedAppRec")
                End If
                If IsDBNull(dr.Item("datDraftOnWeb")) Then
                    chbDraftOnWeb.Checked = False
                Else
                    chbDraftOnWeb.Checked = True
                    DTPDraftOnWeb.Text = dr.Item("datDraftOnWEb")
                End If
                If IsDBNull(dr.Item("datEPAStatesNotified")) Then
                    chbEPAandStatesNotified.Checked = False
                Else
                    chbEPAandStatesNotified.Checked = True
                    DTPEPAStatesNotified.Text = dr.Item("datEPAStatesNotified")
                End If
                If IsDBNull(dr.Item("datFinalOnWeb")) Then
                    chbFinalOnWeb.Checked = False
                Else
                    chbFinalOnWeb.Checked = True
                    DTPFinalOnWeb.Text = dr.Item("datFinalOnWeb")
                End If
                If IsDBNull(dr.Item("datEPANotified")) Then
                    chbEPANotifiedPermitOnWeb.Checked = False
                Else
                    chbEPANotifiedPermitOnWeb.Checked = True
                    DTPEPANotifiedPermitOnWeb.Text = dr.Item("datEPANotified")
                End If
                If IsDBNull(dr.Item("datEffective")) Then
                    chbEffectiveDateOfPermit.Checked = False
                Else
                    chbEffectiveDateOfPermit.Checked = True
                    DTPEffectiveDateofPermit.Text = dr.Item("datEffective")
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
                    DTPExperationDate.Text = dr.Item("datExperationDate")
                End If
                If IsDBNull(dr.Item("strApplicationType")) Then
                    chbPNExpires.Visible = False
                    DTPPNExpires.Visible = False
                Else
                    AppType = dr.Item("strApplicationType")
                    Select Case AppType
                        Case 22
                            chbPNExpires.Visible = True
                        Case 21
                            chbPNExpires.Visible = True
                        Case 14
                            chbPNExpires.Visible = True
                        Case 16
                            chbPNExpires.Visible = True
                        Case Else
                            chbPNExpires.Visible = False
                            DTPPNExpires.Visible = False
                    End Select
                End If
                If IsDBNull(dr.Item("datPNExpires")) Then
                    chbPNExpires.Checked = False
                    DTPPNExpires.Value = Today
                Else
                    chbPNExpires.Checked = True
                    DTPPNExpires.Text = dr.Item("datPNExpires")
                End If


            End If
            CheckForLinks()

            If txtWebPublisherApplicationNumber.Text <> "" Then
                'LoadWebPublisherDataGrid(txtWebPublisherApplicationNumber.Text)
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SaveWebPublisherData()
        Dim EPAStatesNotifiedAppRec As String
        Dim DraftOnWeb As String
        Dim EPAStatesNotified As String
        Dim FinalOnWeb As String
        Dim EPANotifiedPermitOnWeb As String
        Dim EffectiveDateOnPermit As String
        Dim TargetedComments As String
        Dim ExperationDate As String
        Dim PNExpires As String

        Try

            If chbNotifiedAppReceived.Checked = True Then
                EPAStatesNotifiedAppRec = DTPNotifiedAppReceived.Text
            Else
                EPAStatesNotifiedAppRec = ""
            End If
            If chbDraftOnWeb.Checked = True Then
                DraftOnWeb = DTPDraftOnWeb.Text
            Else
                DraftOnWeb = ""
            End If
            If chbEPAandStatesNotified.Checked = True Then
                EPAStatesNotified = DTPEPAStatesNotified.Text
            Else
                EPAStatesNotified = ""
            End If
            If chbFinalOnWeb.Checked = True Then
                FinalOnWeb = DTPFinalOnWeb.Text
            Else
                FinalOnWeb = ""
            End If
            If chbEPANotifiedPermitOnWeb.Checked = True Then
                EPANotifiedPermitOnWeb = DTPEPANotifiedPermitOnWeb.Text
            Else
                EPANotifiedPermitOnWeb = ""
            End If
            If chbEffectiveDateOfPermit.Checked = True Then
                EffectiveDateOnPermit = DTPEffectiveDateofPermit.Text
            Else
                EffectiveDateOnPermit = ""
            End If
            If chbExpirationDate.Checked = True Then
                ExperationDate = DTPExperationDate.Text
            Else
                ExperationDate = ""
            End If
            If txtEPATargetedComments.Text <> "" Then
                TargetedComments = Replace(txtEPATargetedComments.Text, "'", "''")
                TargetedComments = Mid(TargetedComments, 1, 4000)
            Else
                TargetedComments = ""
            End If
            If chbPNExpires.Checked = True Then
                PNExpires = DTPPNExpires.Text
            Else
                PNExpires = ""
            End If

            If txtWebPublisherApplicationNumber.Text <> "" Then
                SQL = "Update SSPPApplicationTracking set " &
                "datDraftOnWeb = '" & DraftOnWeb & "', " &
                "datEPAStatesNotified = '" & EPAStatesNotified & "', " &
                "datFinalOnWeb = '" & FinalOnWeb & "', " &
                "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " &
                "datEffective = '" & EffectiveDateOnPermit & "', " &
                "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "', " &
                "datExperationDate = '" & ExperationDate & "', " &
                "datPNExpires = '" & PNExpires & "' " &
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update SSPPApplicationData set " &
                "strTargeted = '" & TargetedComments & "' " &
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                If lblLinkWarning.Visible = True Then
                    Dim LinkedApplication As String
                    Dim i As Integer

                    For i = 0 To lbLinkApplications.Items.Count - 1
                        If lbLinkApplications.Items.Item(i) <> txtWebPublisherApplicationNumber.Text Then
                            LinkedApplication = lbLinkApplications.Items.Item(i)
                        Else
                            LinkedApplication = ""
                        End If
                        If LinkedApplication <> "" Then
                            SQL = "Update SSPPApplicationTracking set " &
                            "datDraftOnWeb = '" & DraftOnWeb & "', " &
                            "datEPAStatesNotified = '" & EPAStatesNotified & "', " &
                            "datFinalOnWeb = '" & FinalOnWeb & "', " &
                            "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " &
                            "datEffective = '" & EffectiveDateOnPermit & "', " &
                            "datExperationDate = '" & ExperationDate & "', " &
                            "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "',  " &
                            "datPNExpires = '" & PNExpires & "' " &
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Update SSPPApplicationData set " &
                            "strTargeted = '" & TargetedComments & "' " &
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New SqlCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()
                        End If
                    Next
                End If

                If DraftOnWeb <> "" And EPAStatesNotified = "" Then
                    SQL = "Update SSPPApplicationData set " &
                    "strDraftOnWebNotification = 'False' " &
                    "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                MsgBox("Web Information Saved", MsgBoxStyle.Information, "Application Tracking Log")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub RunTitleVRenewalReport()
        Dim ApplicationNumber As String
        Dim AIRSNumber As String
        Dim FacilityName As String
        Dim PermitNumber As String
        Dim DateIssued As String
        Dim EffectiveDate As String
        Dim temp As String

        Try

            Startdate = DTPTitleVRenewalStart.Text
            EndDate = DTPTitleVRenewalEnd.Text

            Startdate = Format(CDate(Startdate).AddMonths(-51), "dd-MMM-yyyy")
            EndDate = Format(CDate(EndDate).AddMonths(-51), "dd-MMM-yyyy")


            'EndDate = CDate(EndDate).AddMonths(-50)
            'EndDate = CDate(EndDate).Month & "-01-" & CDate(EndDate).Year
            'EndDate = Format(CDate(EndDate), "dd-MMM-yyyy")

            lblStartDate.Text = Startdate
            lblEndDate.Text = EndDate

            clbTitleVRenewals.Items.Clear()

            temp = "App #   Airs #   Facility Name                             (     Permit Number    )  Issued Date  Effective Date"
            If clbTitleVRenewals.Items.Contains(temp) Then
            Else
                clbTitleVRenewals.Items.Add(temp)
            End If


            SQL =
            "SELECT am.STRAPPLICATIONNUMBER , SUBSTRING( fi.STRAIRSNUMBER, 5,8 ) " &
            "  AS AIRSNumber , fi.STRFACILITYNAME ,( " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 1, 4 ) || '-' ||  " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 5, 3 ) || '-' ||  " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 8, 4 ) || '-' ||  " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 12, 1 ) || '-' ||  " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 13, 2 ) || '-' ||  " &
            "  SUBSTRING( ad.STRPERMITNUMBER, 15 ) ) AS PermitNumber , 'dd-MMM-yyyy' " &
            "  at.DATPERMITISSUED, 'dd-MMM-yyyy' ) AS PermitIssued , format " &
            "  ( at.DATEFFECTIVE, 'dd-MMM-yyyy' ) AS EffectiveDate " &
            "FROM SSPPApplicationMaster am " &
            "INNER JOIN SSPPApplicationData ad " &
            "ON ad.STRAPPLICATIONNUMBER = am.STRAPPLICATIONNUMBER " &
            "INNER JOIN SSPPApplicationTracking at " &
            "ON am.STRAPPLICATIONNUMBER = at.STRAPPLICATIONNUMBER " &
            "INNER JOIN APBHeaderData hd " &
            "ON am.STRAIRSNUMBER = hd.STRAIRSNUMBER " &
            "INNER JOIN APBFacilityInformation fi " &
            "ON fi.STRAIRSNUMBER = am.STRAIRSNUMBER " &
            "WHERE ad.STRPERMITNUMBER LIKE '%V__0' AND " &
            "  hd.STROPERATIONALSTATUS <> 'X' AND " &
            "  SUBSTRING( hd.STRAIRPROGRAMCODES, 13, 1 ) = '1' AND at.DATEFFECTIVE " &
            "  BETWEEN @Startdate AND @EndDate AND( am.STRAPPLICATIONTYPE = " &
            "  '14' OR am.STRAPPLICATIONTYPE = '16' OR am.STRAPPLICATIONTYPE " &
            "  = '27' )"

            cmd = New SqlCommand(SQL, CurrentConnection)
            cmd.Parameters.Clear()
            Dim param1 As SqlParameter = New SqlParameter("@Startdate", Startdate)
            cmd.Parameters.Add(param1)
            Dim param2 As SqlParameter = New SqlParameter("@EndDate", EndDate)
            cmd.Parameters.Add(param2)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    ApplicationNumber = ""
                Else
                    ApplicationNumber = dr.Item("strApplicationNumber")
                End If
                If IsDBNull(dr.Item("AIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("AIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = "N/A"
                Else
                    FacilityName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("PermitNumber")) Then
                    PermitNumber = "PermitNumber"
                Else
                    PermitNumber = dr.Item("PermitNumber")
                End If
                If IsDBNull(dr.Item("PermitIssued")) Then
                    DateIssued = "N/A"
                Else
                    DateIssued = dr.Item("PermitIssued")
                End If
                If IsDBNull(dr.Item("EffectiveDate")) Then
                    EffectiveDate = "N/A"
                Else
                    EffectiveDate = dr.Item("EffectiveDate")
                End If

                If AIRSNumber <> "" Then
                    temp = ApplicationNumber & "  " & AIRSNumber & "  " & FacilityName.Substring(0, Math.Min(FacilityName.Length, 40)).PadRight(40)
                    temp = temp & "  ( " & PermitNumber & " ) " & " " & DateIssued & "  " & EffectiveDate

                    If clbTitleVRenewals.Items.Contains(temp) Then
                    Else
                        clbTitleVRenewals.Items.Add(temp)
                        clbTitleVRenewals.SetItemChecked(clbTitleVRenewals.Items.IndexOf(temp), True)
                    End If

                End If

                txtRenewalCount.Text = clbTitleVRenewals.Items.Count - 1

            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub LoadWebPublishingFacilityInformation()
        Try
            Dim FacilityName As String = ""
            Dim Street As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""

            If txtWebPublisherApplicationNumber.Text <> "" Then
                SQL = "Select " &
                "strFacilityName, strFacilityStreet1, " &
                "strFacilityCity, strFacilityState, " &
                "strFacilityZipCode " &
                "from SSPPApplicationData " &
                "Where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
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
                dr.Close()

                txtFacilityInformation.Text = FacilityName & vbCrLf &
                Street & vbCrLf &
                City & " " & State & ", " & ZipCode

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub PreviewAppReceivedEmail()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""
            Dim Staff As String = ""
            Dim Unit As String = ""
            Dim temp As String = ""

            clbTitleVEmailList.Items.Clear()

            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "(strLastName||', '||strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "from SSPPApplicationMaster, SSPPApplicationData, " &
            "LookUpApplicationTypes, EPDUserProfiles, " &
            "LookUpEPDUnits " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and (strAppReceivedNotification is Null or strAppReceivedNotification = 'False') " &
            "and (strApplicationType = '19'  or strApplicationType = '20' or strApplicationType = '21' " &
            "or strApplicationType = '22') " &
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
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

                If clbTitleVEmailList.Items.Contains(temp) = False Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            End While
            dr.Close()

            txtEmailType.Text = "AppReceived"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GenerateAppReceivedEmail()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.7(e)(2),(3), and (4) and 70.8(a)(1) and (b)(1), you are hereby notified that " &
                "Georgia EPD has received an application for the modification of an existing Part 70 permit for the " &
                "following source(s): " & vbCrLf & vbCrLf

                SQL = "Select " &
                "SSPPApplicationMaster.strApplicationNumber,  " &
                "strFacilityName, strFacilityCity,  " &
                "strApplicationTypeDesc,  " &
                "strCountyName  " &
                "from SSPPApplicationMaster, SSPPApplicationData,  " &
                "LookUpApplicationTypes, LookUpCountyInformation   " &
                "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode  "

                SQL2 = "Update SSPPApplicationData set " &
                "strAppReceivedNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))
                    SQLLine = SQLLine & " SSPPApplicationMaster.strApplicationNumber = '" & temp & "' or "
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine = "And ( " & Mid(SQLLine, 1, (SQLLine.Length - 3)) & " ) "
                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL = SQL & SQLLine & " order by strFacilityName "
                SQL2 = SQL2 & SQLLine2

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
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
                End While
                dr.Close()

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. " &
                "Any questions regarding this permit application may be directed to: " & vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub PreviewDraftOnWeb()
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

            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "(strLastName||', '||strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "from SSPPApplicationMaster, SSPPApplicationData, " &
            "LookUpApplicationTypes, SSPPApplicationTracking, " &
            "EPDUserProfiles, LookUpEPDUnits " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " &
            "and (strApplicationType = '14'  or strApplicationType = '16' or strApplicationType = '21' " &
            "or strApplicationType = '22') " &
            "and strPNReady = 'True' " &
            "and datDraftOnWeb is Not Null " &
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps = LinkedApps & dr.Item("strApplicationNumber") & ","
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

                If clbTitleVEmailList.Items.Contains(temp) = False Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            End While
            dr.Close()

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                SQL = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpApplicationTypes, SSPPApplicationLinking, " &
                    "EPDUserProfiles, LookUpEPDUnits  " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                    "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    "and SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    End While
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop

            txtEmailType.Text = "DraftOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub GenerateDraftOnWebEmail()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached are the public notices for the " &
                "draft/proposed permits and amendments for the following sources: " & vbCrLf & vbCrLf

                SQL2 = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        LinkedApp = dr.Item("strMasterApplication")
                    Else
                        LinkedApp = ""
                    End If
                    dr.Close()

                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName, datPNExpires " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes, " &
                    "SSPPApplicationTracking " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SSPPApplicationmaster.strAPplicationNumber = SSPPApplicationTracking.strApplicationNumber (+) " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                        If IsDBNull(dr.Item("datPNExpires")) Then
                            PNExpires = ""
                        Else
                            PNExpires = dr.Item("datPNExpires")
                        End If
                    End While
                    dr.Close()

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        SQL = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
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
                        End While
                        dr.Close()
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If txtEmailLetter.Text.Contains(AppLine) Then
                    Else
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf &
                        "30-day expires: " & PNExpires & vbCrLf & vbCrLf

                    End If
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " &
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " &
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf &
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " &
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf &
                "http://epd.georgia.gov/air/draft-title-v-permit" & vbCrLf & vbCrLf &
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " &
                "amendments may be directed to: " & vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GenerateDraftOnWebState()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached are the public notices for the draft/proposed permits and " &
                "amendments for the following sources: " & vbCrLf & vbCrLf

                SQL2 = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        LinkedApp = dr.Item("strMasterApplication")
                    Else
                        LinkedApp = ""
                    End If
                    dr.Close()

                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                    End While
                    dr.Close()

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        SQL = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
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
                        End While
                        dr.Close()
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If txtEmailLetter.Text.Contains(AppLine) Then
                    Else
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " &
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " &
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf &
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " &
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf &
                "http://epd.georgia.gov/air/draft-title-v-permit" & vbCrLf & vbCrLf &
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " &
                "amendments may be directed to: " & vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub PreviewMinorModOnWeb()
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

            'This is the old code that was changed on May 13, 2009 
            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "(strLastName||', '||strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "from SSPPApplicationMaster, SSPPApplicationData, " &
            "LookUpApplicationTypes, EPDUserProfiles, " &
            "LookUpEPDUnits " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " &
            "and (strApplicationType = '19'  or strApplicationType = '20') " &
            "order by strFacilityName, strApplicationNumber DESC "


            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "(strLastName||', '||strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "from SSPPApplicationMaster, SSPPApplicationData, " &
            "LookUpApplicationTypes, EPDUserProfiles, " &
            "LookUpEPDUnits, SSPPApplicationTracking " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
            "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and datEPAStatesNotified is not Null " &
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " &
            "and (strApplicationType = '19'  or strApplicationType = '20') " &
            "order by strFacilityName, strApplicationNumber DESC "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps = LinkedApps & dr.Item("strApplicationNumber") & ","
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

                If clbTitleVEmailList.Items.Contains(temp) = False Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            End While
            dr.Close()

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                SQL = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpApplicationTypes, SSPPApplicationLinking, " &
                    "EPDuserProfiles, LookUPEPDunits " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID  " &
                    "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    "and SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    End While
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop


            txtEmailType.Text = "MinorOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GenerateMinorOnWebEmail()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached is the proposed Part " &
                "70 permit modification and permit amendment narrative for the following: " & vbCrLf & vbCrLf

                SQL2 = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        LinkedApp = dr.Item("strMasterApplication")
                    Else
                        LinkedApp = ""
                    End If
                    dr.Close()

                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                    End While
                    dr.Close()

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        SQL = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
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
                        End While
                        dr.Close()
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If txtEmailLetter.Text.Contains(AppLine) Then
                    Else
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "EPA's review of the proposed minor amendment extends from 45 days following the date of this " &
                "message. Please reply to acknowledge receipt of this notification. Any questions regarding this " &
                "proposed permit amendment may be directed to: " &
                vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GenerateMinorOnWebState()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached is the proposed Part 70 permit modification and  " &
                "permit amendment narrative for the following source: " & vbCrLf & vbCrLf

                SQL2 = "Update SSPPApplicationData set " &
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        LinkedApp = dr.Item("strMasterApplication")
                    Else
                        LinkedApp = ""
                    End If
                    dr.Close()

                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc, " &
                    "strCountyName " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                    End While
                    dr.Close()

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        SQL = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
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
                        End While
                        dr.Close()
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If txtEmailLetter.Text.Contains(AppLine) Then
                    Else
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. Any questions regarding this proposed " &
                "permit amendment may be directed to: " & vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub PreviewFinalOnWeb()
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

            SQL = "Select " &
            "SSPPApplicationMaster.strApplicationNumber, " &
            "strFacilityName, strFacilityCity, " &
            "strApplicationTypeDesc, " &
            "(strLastName||', '||strFirstName) as StaffResponsible, " &
            "strUnitDesc " &
            "from SSPPApplicationMaster, SSPPApplicationData, " &
            "LookUpApplicationTypes, SSPPApplicationTracking, " &
            "EPDUserProfiles, LookUpEPDUnits " &
            "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
            "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
            "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
            "and SSPPApplicationMaster.strStaffResponsible = EPDuserProfiles.numUserID " &
            "and EPDuserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
            "and (strFinalOnWebNotification is Null or strFinalOnWebNotification = 'False') " &
            "and (strApplicationType = '14'  or strApplicationType = '16' " &
            "or strApplicationType = '19' or strApplicationType = '20' " &
            "or strApplicationType = '21' or strApplicationType = '22' " &
            "or strApplicationType = '26' or strApplicationType = '15' " &
            "or strApplicationType = '2') " &
            "and DatFinalOnWeb is Not Null " &
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strApplicationNumber")) Then
                    AppNumber = ""
                Else
                    AppNumber = dr.Item("strApplicationNumber")
                    LinkedApps = LinkedApps & dr.Item("strApplicationNumber") & ","
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

                If clbTitleVEmailList.Items.Contains(temp) = False Then
                    clbTitleVEmailList.Items.Add(temp)
                    clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                End If

            End While
            dr.Close()

            Do While LinkedApps <> ""
                MasterApp = Mid(LinkedApps, 1, (InStr(LinkedApps, ",", CompareMethod.Text) - 1))
                SQL = "select " &
                "strMasterApplication " &
                "from SSPPApplicationLinking " &
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber,  " &
                    "strFacilityName, strFacilityCity,  " &
                    "strApplicationTypeDesc,  " &
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " &
                    "strUnitDesc  " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpApplicationTypes, SSPPApplicationLinking, " &
                    "EPDUserProfiles, LookUpEPDUnits  " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode   " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDuserPRofiles.numUserID  " &
                    "and EPDUserProfiles.numUnit = LookUpEPDunits.numUnitCode (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                    "and SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    End While
                End If

                LinkedApps = Replace(LinkedApps, (MasterApp & ","), "")
            Loop

            txtEmailType.Text = "FinalOnWeb"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub GenerateFinalOnWeb()
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

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with condition V.A.1.a of Georgia's Title V Agreement, the final Part 70 " &
                "Permits were issued to the following sources:" & vbCrLf & vbCrLf

                SQL2 = "Update SSPPApplicationData set " &
                "strFinalOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " &
                    "from SSPPApplicationLinking " &
                    "where strApplicationNumber = '" & temp & "' "
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        LinkedApp = dr.Item("strMasterApplication")
                    Else
                        LinkedApp = ""
                    End If
                    dr.Close()

                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity,  " &
                    "strCountyName, strPermitNumber,  " &
                    "datPermitIssued, datEffective, " &
                    "strApplicationTypeDesc " &
                    "from SSPPApplicationMaster, SSPPApplicationData,  " &
                    "LookUpCountyInformation, LookUpApplicationTypes, " &
                    "SSPPApplicationTracking " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber  " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode  " &
                    "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber (+) " &
                    "and SUBSTRING(strAIRSNumber, 5, 3) = strCountyCode " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
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
                                DateIssued = "Effective: " & dr.Item("datEffective")
                            End If
                        Else
                            DateIssued = "Effective: " & dr.Item("datPermitIssued")
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
                    End While
                    dr.Close()

                    If LinkedApp = "" Then
                        AppLine = "TV-" & AppNumber & "/" & AppType
                    Else
                        AppLine = ""

                        SQL = "select " &
                        "SSPPApplicationLinking.strApplicationNumber, " &
                        "strApplicationTypeDesc " &
                        "from SSPPApplicationLinking, SSPPApplicationMaster, " &
                        "LookUpApplicationTypes " &
                        "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationLinking.strApplicationNumber " &
                        "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New SqlCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
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
                        End While
                        dr.Close()
                        AppLine = Mid(AppLine, 1, (AppLine.Length - 2))
                    End If

                    If txtEmailLetter.Text.Contains(AppLine) Then
                    Else
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf &
                        FacCity & " (" & County & " County), GA" & vbCrLf &
                        AppLine & vbCrLf &
                        PermitNumber & vbCrLf & DateIssued & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The final permit, permit review narrative and in most cases the " &
                "permit application will be available from the Georgia Air Permit Search Engine web page located at: " &
                vbCrLf & vbCrLf &
                "http://search.georgiaair.org/" & vbCrLf & vbCrLf &
                "Please reply to acknowledge receipt of this notification. Any questions regarding the final permits " &
                "may be directed to: " & vbCrLf & vbCrLf &
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf &
                "Stationary Source Permitting Program " & vbCrLf &
                "404/363-7020"

                cmd = New SqlCommand(SQL2, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            Else
                txtEmailLetter.Clear()
                MsgBox("Click Preview button first.", MsgBoxStyle.Information, "Title V Emails.")
            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub AddAppToList()
        Try
            Dim AppNumber As String = ""
            Dim FacName As String = ""
            Dim FacCity As String = ""
            Dim AppType As String = ""

            Dim Staff As String = ""
            Dim Unit As String = ""

            Dim temp As String = ""

            Select Case txtEmailType.Text
                Case "AppReceived"
                    SQL = "Select " &
                   "SSPPApplicationMaster.strApplicationNumber, " &
                   "strFacilityName, strFacilityCity, " &
                   "strApplicationTypeDesc, " &
                   "(strLastName||', '||strFirstName) as StaffResponsible, " &
                   "strUnitDesc " &
                   "from SSPPApplicationMaster, SSPPApplicationData, " &
                   "LookUpApplicationTypes, SSPPApplicationTracking, " &
                   "EPDUserProfiles, LookUpEPDUnits " &
                   "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                   "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                   "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                   "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                   "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                   "and SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Else
                        MsgBox("Unable to add this Applition to list.", MsgBoxStyle.Information, "Data Management Tools")
                    End If
                    dr.Close()
                Case "DraftOnWeb"
                    SQL = "Select " &
                   "SSPPApplicationMaster.strApplicationNumber, " &
                   "strFacilityName, strFacilityCity, " &
                   "strApplicationTypeDesc, " &
                   "(strLastName||', '||strFirstName) as StaffResponsible, " &
                   "strUnitDesc " &
                   "from SSPPApplicationMaster, SSPPApplicationData, " &
                   "LookUpApplicationTypes, SSPPApplicationTracking, " &
                   "EPDUserProfiles, LookUpEPDUnits " &
                   "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                   "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                   "and SSPPApplicationMaster.strApplicationNumber = SSPPApplicationTracking.strApplicationNumber " &
                   "and SSPPApplicationMaster.strStaffResponsible = EPDUserProfiles.numUserID " &
                   "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                   "and SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "


                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If

                    Else
                        MsgBox("Unable to add this Applition to list.", MsgBoxStyle.Information, "Data Management Tools")
                    End If
                    dr.Close()
                Case "MinorOnWeb"
                    SQL = "Select " &
                   "SSPPApplicationMaster.strApplicationNumber, " &
                   "strFacilityName, strFacilityCity, " &
                   "strApplicationTypeDesc, " &
                   "(strLastName||', '||strFirstName) as StaffResponsible, " &
                   "strUnitDesc " &
                   "from SSPPApplicationMaster, SSPPApplicationData, " &
                   "LookUpApplicationTypes,  " &
                   "EPDUserProfiles, LookUpEPDUnits " &
                   "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                   "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                   "and SSPPApplicationMaster.strStaffResponsible = EPDuserProfiles.numUserID " &
                   "and EPDuserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                   "and SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If

                    Else
                        MsgBox("Unable to add this Applition to list.", MsgBoxStyle.Information, "Data Management Tools")
                    End If
                    dr.Close()
                Case "FinalOnWeb"
                    SQL = "Select " &
                    "SSPPApplicationMaster.strApplicationNumber, " &
                    "strFacilityName, strFacilityCity, " &
                    "strApplicationTypeDesc, " &
                    "(strLastName||', '||strFirstName) as StaffResponsible, " &
                    "strUnitDesc " &
                    "from SSPPApplicationMaster, SSPPApplicationData, " &
                    "LookUpApplicationTypes, " &
                    "EPDuserProfiles, LookUpEPDUnits " &
                    "where SSPPApplicationMaster.strApplicationNumber = SSPPApplicationData.strApplicationNumber " &
                    "and SSPPApplicationMaster.strApplicationType = LookUpApplicationTypes.strApplicationTypeCode " &
                    "and SSPPApplicationMaster.strStaffResponsible = EPDuserProfiles.numUserID " &
                    "and EPDUserProfiles.numUnit = LookUpEPDUnits.numUnitCode (+) " &
                    "and SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
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

                        If clbTitleVEmailList.Items.Contains(temp) = False Then
                            clbTitleVEmailList.Items.Add(temp)
                            clbTitleVEmailList.SetItemChecked(clbTitleVEmailList.Items.IndexOf(temp), True)
                        End If
                    Else
                        MsgBox("Unable to add this Applition to list.", MsgBoxStyle.Information, "Data Management Tools")
                    End If
                    dr.Close()
            End Select

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub DEVDataManagementTools_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewApplication_Click(sender As Object, e As EventArgs) Handles btnViewApplication.Click
        OpenFormPermitApplication(txtWebPublisherApplicationNumber.Text)
    End Sub
    Private Sub btnReloadGrid_Click(sender As Object, e As EventArgs) Handles btnReloadGrid.Click
        Try

            LoadWebPublisherDataGrid("Load")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrWebPublisher_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrWebPublisher.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrWebPublisher.HitTest(e.X, e.Y)
        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrWebPublisher(hti.Row, 0)) Then
                Else
                    txtWebPublisherApplicationNumber.Text = dgrWebPublisher(hti.Row, 0)
                    If txtWebPublisherApplicationNumber.Text <> "" Then
                        LoadWebPublisherApplicationData()
                        LoadWebPublishingFacilityInformation()
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtWebPublisherApplicationNumber_TextChanged(sender As Object, e As EventArgs) Handles txtWebPublisherApplicationNumber.TextChanged
        Try

            'If txtWebPublisherApplicationNumber.Text <> "" Then
            '    LoadWebPublisherApplicationData()
            'End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Checkbox Changes"
    Private Sub chbNotifiedAppReceived_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotifiedAppReceived.CheckedChanged
        Try

            If chbNotifiedAppReceived.Checked = True Then
                DTPNotifiedAppReceived.Visible = True
            Else
                DTPNotifiedAppReceived.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbDraftOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbDraftOnWeb.CheckedChanged
        Try

            If chbDraftOnWeb.Checked = True Then
                DTPDraftOnWeb.Visible = True
            Else
                DTPDraftOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPNExpires_CheckedChanged(sender As Object, e As EventArgs) Handles chbPNExpires.CheckedChanged
        Try

            If chbPNExpires.Checked = True Then
                DTPPNExpires.Visible = True
            Else
                DTPPNExpires.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEPAandStatesNotified_CheckedChanged(sender As Object, e As EventArgs) Handles chbEPAandStatesNotified.CheckedChanged
        Try

            If chbEPAandStatesNotified.Checked = True Then
                DTPEPAStatesNotified.Visible = True
            Else
                DTPEPAStatesNotified.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbFinalOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbFinalOnWeb.CheckedChanged
        Try

            If chbFinalOnWeb.Checked = True Then
                DTPFinalOnWeb.Visible = True
            Else
                DTPFinalOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEPANotifiedPermitOnWeb_CheckedChanged(sender As Object, e As EventArgs) Handles chbEPANotifiedPermitOnWeb.CheckedChanged
        Try

            If chbEPANotifiedPermitOnWeb.Checked = True Then
                DTPEPANotifiedPermitOnWeb.Visible = True
            Else
                DTPEPANotifiedPermitOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEffectiveDateOfPermit_CheckedChanged(sender As Object, e As EventArgs) Handles chbEffectiveDateOfPermit.CheckedChanged
        Try

            If chbEffectiveDateOfPermit.Checked = True Then
                DTPEffectiveDateofPermit.Visible = True
            Else
                DTPEffectiveDateofPermit.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbExperationDate_CheckedChanged(sender As Object, e As EventArgs) Handles chbExpirationDate.CheckedChanged
        Try

            If chbExpirationDate.Checked = True Then
                DTPExperationDate.Visible = True
            Else
                DTPExperationDate.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub btnSaveWebPublisher_Click(sender As Object, e As EventArgs) Handles btnSaveWebPublisher.Click
        Try

            SaveWebPublisherData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

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
        Finally

        End Try

    End Sub
    Private Sub btnSearchForApplication_Click(sender As Object, e As EventArgs) Handles btnSearchForApplication.Click
        Try


            LoadWebPublisherApplicationData()
            LoadWebPublishingFacilityInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Mahesh Code for Web App Users"
    Function LoadComboBoxes() As DataTable
        Dim dtairs As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Dim SQL As String

        Try


            SQL = "Select DISTINCT SUBSTRING(strairsnumber, 5,8) as strairsnumber, " _
            + "strfacilityname " _
            + "from APBFacilityInformation " _
            + "Order by strAIRSNumber "

            ds = New DataSet
            da = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            da.Fill(ds, "facilityInfo")

            dtairs.Columns.Add("strairsnumber", GetType(System.String))
            dtairs.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtairs.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtairs.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtairs.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtairs.Rows.Add(drNewRow)
            Next

            Return dtairs

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return Nothing
        Finally

        End Try

    End Function
    Private Sub Back()
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub UpdateRecords(userid As Object, adminaccess As Object, feeaccess As Object, eiaccess As Object, esaccess As Object)

        Dim admin, fee, ei, es As Integer
        If adminaccess = True Then
            admin = 1
        Else
            admin = 0
        End If
        If feeaccess = True Then
            fee = 1
        Else
            fee = 0
        End If
        If eiaccess = True Then
            ei = 1
        Else
            ei = 0
        End If
        If esaccess = True Then
            es = 1
        Else
            es = 0
        End If

        Try
            Dim updateString As String = "UPDATE OlapUserAccess " &
                      "SET intadminaccess = '" & admin & "', " &
                      "intFeeAccess = '" & fee & "', " &
                      "intEIAccess = '" & ei & "', " &
                      "intESAccess = '" & es & "' " &
                      "WHERE numUserID = '" & userid & "' " &
                      "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New SqlCommand(updateString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region
#Region "Fee Password Reset"
    Private Sub SetPassword_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
    Private Sub txtWebPublisherApplicationNumber_Leave(sender As Object, e As EventArgs) Handles txtWebPublisherApplicationNumber.Leave
        Try

            If txtWebPublisherApplicationNumber.Text <> "" Then
                LoadWebPublisherApplicationData()
                LoadWebPublishingFacilityInformation()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtWebPublisherApplicationNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWebPublisherApplicationNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                If txtWebPublisherApplicationNumber.Text <> "" Then
                    LoadWebPublisherApplicationData()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunTitleVReport_Click(sender As Object, e As EventArgs) Handles btnRunTitleVReport.Click
        Try

            RunTitleVRenewalReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPrintRenewalLetters_Click(sender As Object, e As EventArgs) Handles btnPrintRenewalLetters.Click
        Try
            If Me.txtRenewalCount.Text <> "" And txtRenewalCount.Text <> "0" Then
                Dim PrintOut As New IAIPPrintOut
                PrintOut.PrintoutType = IAIPPrintOut.PrintType.TitleVRenewal
                PrintOut.ReferenceValue = "*"
                PrintOut.StartDate = DTPTitleVRenewalStart.Value.AddMonths(-51)
                PrintOut.EndDate = DTPTitleVRenewalEnd.Value.AddMonths(-51)
                PrintOut.Show()
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
        Finally

        End Try

    End Sub
    Private Sub btnPreviewESNReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewESNReceived.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewAppReceivedEmail()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_Click(sender As Object, e As EventArgs) Handles btnPreviewDraftOnWeb.Click
        Try


            PreviewDraftOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewDraftOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewDraftOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewMinorMod_Click(sender As Object, e As EventArgs) Handles btnPreviewMinorMod.Click
        Try


            PreviewMinorModOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewMinorMod_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewMinorMod.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewMinorModOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_Click(sender As Object, e As EventArgs) Handles btnPreviewFinalOnWeb.Click
        Try


            PreviewFinalOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnPreviewFinalOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewFinalOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailESNReceived_Click(sender As Object, e As EventArgs) Handles btnEmailESNReceived.Click
        Try


            GenerateAppReceivedEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailESNReceived_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailESNReceived.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateAppReceivedEmail()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWeb_Click(sender As Object, e As EventArgs) Handles btnEmailDraftOnWeb.Click
        Try


            GenerateDraftOnWebEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailDraftOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateDraftOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWebState_Click(sender As Object, e As EventArgs) Handles btnEmailDraftOnWebState.Click
        Try

            GenerateDraftOnWebState()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWebState_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailDraftOnWebState.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateDraftOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebEPD_Click(sender As Object, e As EventArgs) Handles btnMinorModOnWebEPD.Click
        Try


            GenerateMinorOnWebEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebEPD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnMinorModOnWebEPD.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateMinorOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebState_Click(sender As Object, e As EventArgs) Handles btnMinorModOnWebState.Click
        Try


            GenerateMinorOnWebState()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebState_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnMinorModOnWebState.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateMinorOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailFinalOnWeb_Click(sender As Object, e As EventArgs) Handles btnEmailFinalOnWeb.Click
        Try

            GenerateFinalOnWeb()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailFinalOnWeb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnEmailFinalOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then

                GenerateFinalOnWeb()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnAddApplicationToList_Click(sender As Object, e As EventArgs) Handles btnAddApplicationToList.Click
        Try

            AddAppToList()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtApplicationNumberToAdd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApplicationNumberToAdd.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                AddAppToList()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtEmailLetter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmailLetter.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtEmailLetter.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPrintSingleTitleVRenewal_Click(sender As Object, e As EventArgs) Handles btnPrintSingleTitleVRenewal.Click
        Try
            Dim AppNumber As String = "*"

            If txtTitleVSingleLetter.Text <> "" Then
                AppNumber = txtTitleVSingleLetter.Text
            Else
                AppNumber = "*"
            End If

            If (Me.txtRenewalCount.Text <> "" And txtRenewalCount.Text <> "0") Or txtTitleVSingleLetter.Text <> "" Then
                Dim PrintOut As New IAIPPrintOut
                PrintOut.PrintoutType = IAIPPrintOut.PrintType.TitleVRenewal
                PrintOut.ReferenceValue = AppNumber
                PrintOut.StartDate = New Date(1990, 1, 1)
                PrintOut.EndDate = New Date(2099, 1, 1)
                PrintOut.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnLoadAppContact_Click(sender As Object, e As EventArgs) Handles btnLoadAppContact.Click
        Try
            If txtApplicationNumber.Text <> "" Then
                LoadContactData()
            Else
                MessageBox.Show("Please enter an application number first.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadContactData()
        Try
            SQL = "Select strApplicationNumber " &
            "From SSPPApplicationContact " &
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " &
                "SUBSTRING(SSPPApplicationMaster.strAIRSNumber, 5,8) as AIRSNumber, " &
                "strContactFirstName, " &
                "strContactLastName, " &
                "strContactPrefix, " &
                "strContactSuffix, " &
                "strContactTitle, " &
                "strContactCompanyName, " &
                "strContactPhoneNumber1, " &
                "strContactFaxNumber, " &
                "strContactEmail, " &
                "strContactAddress1, " &
                "strContactCity, " &
                "strContactState, " &
                "strContactZipCode, " &
                "strContactDescription " &
                "from SSPPApplicationContact, SSPPApplicationMaster " &
                "where SSPPApplicationContact.strApplicationNumber = SSPPApplicationMaster.strApplicationNumber " &
                "and SSPPApplicationContact.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("AIRSNumber")) Then
                        txtAIRSNumber.Clear()
                    Else
                        txtAIRSNumber.Text = dr.Item("AIRSNumber")
                    End If
                    If IsDBNull(dr.Item("strContactFirstname")) Then
                        txtContactFirstName.Clear()
                    Else
                        txtContactFirstName.Text = dr.Item("strContactFirstName")
                    End If
                    If IsDBNull(dr.Item("strContactLastName")) Then
                        txtContactLastName.Clear()
                    Else
                        txtContactLastName.Text = dr.Item("strContactLastName")
                    End If
                    If IsDBNull(dr.Item("strContactPrefix")) Then
                        txtContactSocialTitle.Clear()
                    Else
                        txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                    End If
                    If IsDBNull(dr.Item("strContactSuffix")) Then
                        txtContactPedigree.Clear()
                    Else
                        txtContactPedigree.Text = dr.Item("strContactSuffix")
                    End If
                    If IsDBNull(dr.Item("strContactTitle")) Then
                        txtContactTitle.Clear()
                    Else
                        txtContactTitle.Text = dr.Item("strContactTitle")
                    End If
                    If IsDBNull(dr.Item("strContactCompanyName")) Then
                        txtContactCompanyName.Clear()
                    Else
                        txtContactCompanyName.Text = dr.Item("strContactCompanyName")
                    End If
                    If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                        mtbContactPhoneNumber.Clear()
                    Else
                        mtbContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
                    End If
                    If IsDBNull(dr.Item("strContactFaxNumber")) Then
                        mtbContactFaxNumber.Clear()
                    Else
                        mtbContactFaxNumber.Text = dr.Item("strContactFaxNumber")
                    End If
                    If IsDBNull(dr.Item("strContactEmail")) Then
                        txtContactEmailAddress.Clear()
                    Else
                        txtContactEmailAddress.Text = dr.Item("strContactEmail")
                    End If
                    If IsDBNull(dr.Item("strContactAddress1")) Then
                        txtContactStreetAddress.Clear()
                    Else
                        txtContactStreetAddress.Text = dr.Item("strContactAddress1")
                    End If
                    If IsDBNull(dr.Item("strContactCity")) Then
                        txtContactCity.Clear()
                    Else
                        txtContactCity.Text = dr.Item("strContactCity")
                    End If
                    If IsDBNull(dr.Item("strContactState")) Then
                        txtContactState.Clear()
                    Else
                        txtContactState.Text = dr.Item("strContactState")
                    End If
                    If IsDBNull(dr.Item("strContactZipCode")) Then
                        mtbContactZipCode.Clear()
                    Else
                        mtbContactZipCode.Text = dr.Item("strContactZipCode")
                    End If
                    If IsDBNull(dr.Item("strContactDescription")) Then
                        txtContactDescription.Clear()
                    Else
                        txtContactDescription.Text = dr.Item("strContactDescription") & vbCrLf &
                           "Added from App # - " & txtApplicationNumber.Text
                    End If
                End While
            Else
                '30
                SQL = "Select " &
                "strContactFirstName, " &
                "strContactLastName, " &
                "strContactPrefix, " &
                "strContactSuffix, " &
                "strContactTitle, " &
                "strContactCompanyName, " &
                "strContactPhoneNumber1, " &
                "strContactFaxNumber, " &
                "strContactEmail, " &
                "strContactAddress1, " &
                "strContactCity, " &
                "strContactState, " &
                "strContactZipCode, " &
                "strContactDescription " &
                "from APBContactInformation " &
                "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGetCurrentPermittingContact_Click(sender As Object, e As EventArgs) Handles btnGetCurrentPermittingContact.Click
        Try
            SQL = "Select " &
            "strContactFirstName, " &
             "strContactLastName, " &
             "strContactPrefix, " &
             "strContactSuffix, " &
             "strContactTitle, " &
             "strContactCompanyName, " &
             "strContactPhoneNumber1, " &
             "strContactFaxNumber, " &
             "strContactEmail, " &
             "strContactAddress1, " &
             "strContactCity, " &
             "strContactState, " &
             "strContactZipCode, " &
             "strContactDescription " &
             "from APBContactInformation " &
             "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strContactFirstname")) Then
                    txtContactFirstName.Clear()
                Else
                    txtContactFirstName.Text = dr.Item("strContactFirstName")
                End If
                If IsDBNull(dr.Item("strContactLastName")) Then
                    txtContactLastName.Clear()
                Else
                    txtContactLastName.Text = dr.Item("strContactLastName")
                End If
                If IsDBNull(dr.Item("strContactPrefix")) Then
                    txtContactSocialTitle.Clear()
                Else
                    txtContactSocialTitle.Text = dr.Item("strContactPrefix")
                End If
                If IsDBNull(dr.Item("strContactSuffix")) Then
                    txtContactPedigree.Clear()
                Else
                    txtContactPedigree.Text = dr.Item("strContactSuffix")
                End If
                If IsDBNull(dr.Item("strContactTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = dr.Item("strContactTitle")
                End If
                If IsDBNull(dr.Item("strContactCompanyName")) Then
                    txtContactCompanyName.Clear()
                Else
                    txtContactCompanyName.Text = dr.Item("strContactCompanyName")
                End If
                If IsDBNull(dr.Item("strContactPhoneNumber1")) Then
                    mtbContactPhoneNumber.Clear()
                Else
                    mtbContactPhoneNumber.Text = dr.Item("strContactPhoneNumber1")
                End If
                If IsDBNull(dr.Item("strContactFaxNumber")) Then
                    mtbContactFaxNumber.Clear()
                Else
                    mtbContactFaxNumber.Text = dr.Item("strContactFaxNumber")
                End If
                If IsDBNull(dr.Item("strContactEmail")) Then
                    txtContactEmailAddress.Clear()
                Else
                    txtContactEmailAddress.Text = dr.Item("strContactEmail")
                End If
                If IsDBNull(dr.Item("strContactAddress1")) Then
                    txtContactStreetAddress.Clear()
                Else
                    txtContactStreetAddress.Text = dr.Item("strContactAddress1")
                End If
                If IsDBNull(dr.Item("strContactCity")) Then
                    txtContactCity.Clear()
                Else
                    txtContactCity.Text = dr.Item("strContactCity")
                End If
                If IsDBNull(dr.Item("strContactState")) Then
                    txtContactState.Clear()
                Else
                    txtContactState.Text = dr.Item("strContactState")
                End If
                If IsDBNull(dr.Item("strContactZipCode")) Then
                    mtbContactZipCode.Clear()
                Else
                    mtbContactZipCode.Text = dr.Item("strContactZipCode")
                End If
                If IsDBNull(dr.Item("strContactDescription")) Then
                    txtContactDescription.Clear()
                Else
                    txtContactDescription.Text = dr.Item("strContactDescription") & vbCrLf &
                            "Added from Facility Summary"
                End If
                txtContactDescription.Text = "From App #- " & txtApplicationNumber.Text & vbCrLf & txtContactDescription.Text
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSaveContactApp_Click(sender As Object, e As EventArgs) Handles btnSaveContactApp.Click
        Try
            If txtApplicationNumber.Text <> "" And txtContactFirstName.Text <> "" And txtContactLastName.Text <> "" Then
                SaveApplicationContact()
                SaveComplianceContact()

                MessageBox.Show("Contact Information Saved")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub SaveApplicationContact()
        Try
            Dim ContactFirstName As String = " "
            Dim ContactLastname As String = " "
            Dim ContactPrefix As String = " "
            Dim ContactSuffix As String = " "
            Dim ContactTitle As String = " "
            Dim ContactCompany As String = " "
            Dim ContactPhone As String = " "
            Dim ContactFax As String = " "
            Dim ContactEmail As String = " "
            Dim ContactAddress As String = " "
            Dim ContactCity As String = " "
            Dim ContactState As String = " "
            Dim ContactZipCode As String = " "
            Dim ContactDescription As String = " "

            If txtContactFirstName.Text <> "" Then
                ContactFirstName = txtContactFirstName.Text
            Else
                ContactFirstName = " "
            End If
            If txtContactLastName.Text <> "" Then
                ContactLastname = txtContactLastName.Text
            Else
                ContactLastname = " "
            End If
            If txtContactSocialTitle.Text <> "" Then
                ContactPrefix = txtContactSocialTitle.Text
            Else
                ContactPrefix = " "
            End If
            If txtContactPedigree.Text <> "" Then
                ContactSuffix = txtContactPedigree.Text
            Else
                ContactSuffix = " "
            End If
            If txtContactTitle.Text <> "" Then
                ContactTitle = txtContactTitle.Text
            Else
                ContactTitle = " "
            End If
            If txtContactCompanyName.Text <> "" Then
                ContactCompany = txtContactCompanyName.Text
            Else
                ContactCompany = " "
            End If
            If mtbContactPhoneNumber.Text <> "" Then
                ContactPhone = mtbContactPhoneNumber.Text
            Else
                ContactPhone = "0000000000"
            End If
            If mtbContactFaxNumber.Text <> "" Then
                ContactFax = mtbContactFaxNumber.Text
            Else
                ContactFax = "0000000000"
            End If
            If txtContactEmailAddress.Text <> "" Then
                ContactEmail = txtContactEmailAddress.Text
            Else
                ContactEmail = " "
            End If
            If txtContactStreetAddress.Text <> "" Then
                ContactAddress = txtContactStreetAddress.Text
            Else
                ContactAddress = " "
            End If
            If txtContactCity.Text <> "" Then
                ContactCity = txtContactCity.Text
            Else
                ContactCity = " "
            End If
            If txtContactState.Text <> "" Then
                ContactState = txtContactState.Text
            Else
                ContactState = " "
            End If
            If mtbContactZipCode.Text <> "" Then
                ContactZipCode = mtbContactZipCode.Text
            Else
                ContactZipCode = "00000"
            End If
            If txtContactDescription.Text <> "" Then
                ContactDescription = txtContactDescription.Text
            Else
                If txtApplicationNumber.Text <> "" Then
                    ContactDescription = "Added by Title V Tool"
                Else
                    ContactDescription = " "
                End If
            End If

            SQL = "Select strApplicationNumber " &
            "from SSPPApplicationContact " &
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                'update
                SQL = "Update SSPPApplicationContact set " &
                "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " &
                "strContactLastName = '" & Replace(ContactLastname, "'", "''") & "', " &
                "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " &
                "strContactSuffix = '" & Replace(ContactSuffix, "'", "''") & "', " &
                "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " &
                "strContactCompanyName = '" & Replace(ContactCompany, "'", "''") & "', " &
                "strContactPhoneNumber1 = '" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " &
                "strContactfaxnumber = '" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " &
                "strContactemail = '" & Replace(ContactEmail, "'", "''") & "', " &
                "strContactAddress1 = '" & Replace(ContactAddress, "'", "''") & "', " &
                "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " &
                "strContactState = '" & Replace(ContactState, "'", "''") & "', " &
                "strContactZipCode = '" & Replace(ContactZipCode, "-", "") & "', " &
                "strContactDescription = '" & Replace(ContactDescription, "'", "''") & "' " &
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            Else
                'insert 
                SQL = "Insert into SSPPApplicationContact " &
                "values " &
                "('" & txtApplicationNumber.Text & "', " &
                "'" & Replace(ContactFirstName, "'", "''") & "', " &
                "'" & Replace(ContactLastname, "'", "''") & "', " &
                "'" & Replace(ContactPrefix, "'", "''") & "', " &
                "'" & Replace(ContactSuffix, "'", "''") & "', " &
                "'" & Replace(ContactTitle, "'", "''") & "', " &
                "'" & Replace(ContactCompany, "'", "''") & "', " &
                "'" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " &
                "'" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " &
                "'" & Replace(ContactEmail, "'", "''") & "', " &
                "'" & Replace(ContactAddress, "'", "''") & "', " &
                "'" & Replace(ContactCity, "'", "''") & "', " &
                "'" & Replace(ContactState, "'", "''") & "', " &
                "'" & Replace(ContactZipCode, "-", "") & "', " &
                "'" & Replace(ContactDescription, "'", "''") & "') "
            End If

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub SaveComplianceContact()
        Try
            Dim temp As String = ""
            Dim ContactFirstName As String = " "
            Dim ContactLastname As String = " "
            Dim ContactPrefix As String = " "
            Dim ContactSuffix As String = " "
            Dim ContactTitle As String = " "
            Dim ContactCompany As String = " "
            Dim ContactPhone As String = " "
            Dim ContactFax As String = " "
            Dim ContactEmail As String = " "
            Dim ContactAddress As String = " "
            Dim ContactCity As String = " "
            Dim ContactState As String = " "
            Dim ContactZipCode As String = " "

            If txtContactFirstNameCompliance.Text <> "" Then
                ContactFirstName = txtContactFirstNameCompliance.Text
            Else
                ContactFirstName = " "
            End If
            If txtContactLastNameCompliance.Text <> "" Then
                ContactLastname = txtContactLastNameCompliance.Text
            Else
                ContactLastname = " "
            End If
            If txtContactSocialTitleCompliance.Text <> "" Then
                ContactPrefix = txtContactSocialTitleCompliance.Text
            Else
                ContactPrefix = " "
            End If
            If txtContactPedigreeCompliance.Text <> "" Then
                ContactSuffix = txtContactPedigreeCompliance.Text
            Else
                ContactSuffix = " "
            End If
            If txtContactTitleCompliance.Text <> "" Then
                ContactTitle = txtContactTitleCompliance.Text
            Else
                ContactTitle = " "
            End If
            If txtContactCompanyNameCompliance.Text <> "" Then
                ContactCompany = txtContactCompanyNameCompliance.Text
            Else
                ContactCompany = " "
            End If
            If mtbContactPhoneNumberCompliance.Text <> "" Then
                ContactPhone = mtbContactPhoneNumberCompliance.Text
            Else
                ContactPhone = "0000000000"
            End If
            If mtbContactFaxNumberCompliance.Text <> "" Then
                ContactFax = mtbContactFaxNumberCompliance.Text
            Else
                ContactFax = "0000000000"
            End If
            If txtContactEmailAddressCompliance.Text <> "" Then
                ContactEmail = txtContactEmailAddressCompliance.Text
            Else
                ContactEmail = " "
            End If
            If txtContactStreetAddressCompliance.Text <> "" Then
                ContactAddress = txtContactStreetAddressCompliance.Text
            Else
                ContactAddress = " "
            End If
            If txtContactCityCompliance.Text <> "" Then
                ContactCity = txtContactCityCompliance.Text
            Else
                ContactCity = " "
            End If
            If txtContactStateCompliance.Text <> "" Then
                ContactState = txtContactStateCompliance.Text
            Else
                ContactState = " "
            End If
            If mtbContactZipCodeCompliance.Text <> "" Then
                ContactZipCode = mtbContactZipCodeCompliance.Text
            Else
                ContactZipCode = "00000"
            End If

            SQL = "Select count(*) as SSCPContact " &
            "From APBContactInformation " &
            "where strContactKey = '0413" & txtAIRSNumber.Text & "20' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("SSCPContact")) Then
                    temp = "0"
                Else
                    temp = dr.Item("SSCPContact")
                End If
            End While
            dr.Close()

            If temp = "0" Then
                Insert_APBContactInformation(txtAIRSNumber.Text, "20",
                                              ContactFirstName, ContactLastname,
                                              ContactPrefix, ContactSuffix,
                                              ContactTitle, ContactCompany,
                                              ContactPhone,
                                              ContactFax, ContactEmail,
                                              ContactAddress,
                                              ContactCity, ContactState,
                                              ContactZipCode, "Contact Added from Title V Warehouse from Enforcement Contact")
            Else
                Update_APBContactInformation(txtAIRSNumber.Text, "20",
                                              ContactFirstName, ContactLastname,
                                              ContactPrefix, ContactSuffix,
                                              ContactTitle, ContactCompany,
                                              ContactPhone, "",
                                              ContactFax, ContactEmail,
                                              ContactAddress, "",
                                              ContactCity, ContactState,
                                              ContactZipCode, "Contact Added from Title V Warehouse for Enforcement Contact")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnLoadFromWarehouse_Click(sender As Object, e As EventArgs) Handles btnLoadFromWarehouse.Click
        Try
            Dim GATVConn As Object = ""
            Dim GATVcmd As Object = ""
            Dim GATVdr As Object = ""

            Dim AIRSNumber As String = ""
            Dim AppNumber As String = ""
            Dim ContactName As String = ""

            If txtApplicationNumber.Text <> "" Then
                AppNumber = txtApplicationNumber.Text
            Else
                AppNumber = ""
            End If
            If txtAIRSNumber.Text <> "" Then
                AIRSNumber = "13" & Mid(txtAIRSNumber.Text, 1, 3) & Mid(txtAIRSNumber.Text, 5, 4)
            Else
                AIRSNumber = ""
            End If

            SQL = "SELECT " &
            "tbl_ProjectManagement.ProjectIdentifier, " &
            "tblFacilityInformation_1_10_Contacts.ContactName, " &
            "tblFacilityInformation_1_10_Contacts.ContactTitle, " &
            "tblFacilityInformation_1_10_Contacts.ContactPhone, " &
            "tblFacilityInformation_1_10_Contacts.ContactPhoneExt, " &
            "tblFacilityInformation_1_10_Contacts.ContactFax, " &
            "tblFacilityInformation_1_10_Contacts.ContactEMail, " &
            "tblFacilityInformation_1_10_MailAddress.MailAddressCompany, " &
            "tblFacilityInformation_1_10_MailAddress.MailAddressStreet, " &
            "tblFacilityInformation_1_10_MailAddress.MailAddressCity, " &
            "tblFacilityInformation_1_10_MailAddress.MailAddressState, " &
            "tblFacilityInformation_1_10_MailAddress.MailingAddressZip " &
            "FROM (tblFacilityInformation_1_10_MailAddress INNER JOIN (tblFacilityInformation_1_10_Contacts " &
            "INNER JOIN tblFacilityInformation_1_10 " &
            "ON (tblFacilityInformation_1_10_Contacts.ContactID = tblFacilityInformation_1_10.ContactForPermits) " &
            "AND (tblFacilityInformation_1_10_Contacts.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " &
            "ON (tblFacilityInformation_1_10_MailAddress.MailAddressID = tblFacilityInformation_1_10.MailContactForPermits) " &
            "AND (tblFacilityInformation_1_10_MailAddress.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " &
            "INNER JOIN tbl_ProjectManagement " &
            "ON tblFacilityInformation_1_10.ProjectIdentifier = tbl_ProjectManagement.ProjectIdentifier " &
            "WHERE (((tbl_ProjectManagement.ProjectIdentifier)=[tblFacilityInformation_1_10].[ProjectIdentifier]) " &
            "AND ((tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_Contacts].[ProjectIdentifier] " &
            "And (tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_MailAddress].[ProjectIdentifier]) " &
            "AND ((tblFacilityInformation_1_10.ContactForPermits)=[tblFacilityInformation_1_10_Contacts].[ContactID]) " &
            "AND ((tblFacilityInformation_1_10.MailContactForPermits)=[tblFacilityInformation_1_10_MailAddress].[MailAddressID]) " &
            "AND ((tbl_ProjectManagement.ApplicationNumber)='" & AppNumber & "') or tbl_ProjectManagement.FacilityId = '" & AIRSNumber & "') " &
            "ORDER BY tbl_ProjectManagement.ProjectIdentifier "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader

            While GATVdr.Read
                If IsDBNull(GATVdr.Item("ContactName")) Then
                    ContactName = ""
                Else
                    ContactName = GATVdr.Item("ContactName")
                End If
                txtContactSocialTitle.Clear()
                txtContactPedigree.Clear()
                If ContactName <> "" Then
                    If ContactName.Contains("Mr.") Then
                        txtContactSocialTitle.Text = "Mr."
                        ContactName = ContactName.Replace("Mr. ", "")
                    End If
                    If ContactName.Contains(" ") Then
                        txtContactFirstName.Text = Mid(ContactName, 1, ContactName.IndexOf(" "))
                        txtContactLastName.Text = Mid(ContactName, ContactName.IndexOf(" ") + 2)
                    Else
                        txtContactFirstName.Text = ContactName
                        txtContactLastName.Clear()
                    End If
                Else
                    txtContactFirstName.Clear()
                    txtContactLastName.Clear()
                End If
                If IsDBNull(GATVdr.Item("MailAddressCompany")) Then
                    txtContactCompanyName.Clear()
                Else
                    txtContactCompanyName.Text = GATVdr.Item("MailAddressCompany")
                End If
                If IsDBNull(GATVdr.Item("ContactTitle")) Then
                    txtContactTitle.Clear()
                Else
                    txtContactTitle.Text = GATVdr.Item("ContactTitle")
                End If
                If IsDBNull(GATVdr.Item("MailAddressStreet")) Then
                    txtContactStreetAddress.Clear()
                Else
                    txtContactStreetAddress.Text = GATVdr.Item("MailAddressStreet")
                End If
                If IsDBNull(GATVdr.Item("MailAddressCity")) Then
                    txtContactCity.Clear()
                Else
                    txtContactCity.Text = GATVdr.Item("MailAddressCity")
                End If
                If IsDBNull(GATVdr.Item("MailAddressState")) Then
                    txtContactState.Clear()
                Else
                    txtContactState.Text = GATVdr.Item("MailAddressState")
                End If
                If IsDBNull(GATVdr.Item("MailingAddressZip")) Then
                    mtbContactZipCode.Clear()
                Else
                    mtbContactZipCode.Text = GATVdr.Item("MailingAddressZip")
                End If
                If IsDBNull(GATVdr.Item("ContactPhone")) Then
                    mtbContactPhoneNumber.Clear()
                Else
                    mtbContactPhoneNumber.Text = GATVdr.Item("ContactPhone")
                End If
                If IsDBNull(GATVdr.Item("ContactFax")) Then
                    mtbContactFaxNumber.Clear()
                Else
                    mtbContactFaxNumber.Text = GATVdr.Item("ContactFax")
                End If
                If IsDBNull(GATVdr.Item("ContactEMail")) Then
                    txtContactEmailAddress.Clear()
                Else
                    txtContactEmailAddress.Text = GATVdr.Item("ContactEMail")
                End If
                txtContactDescription.Clear()
                If IsDBNull(GATVdr.Item("ContactPhoneExt")) Then
                Else
                    txtContactDescription.Text = "Contact extension - " & GATVdr.Item("ContactPhoneExt")
                End If
                If txtContactDescription.Text <> "" Then
                    txtContactDescription.Text = txtContactDescription.Text & vbCrLf &
                        "Added from GATV Warehouse contact information"
                Else
                    txtContactDescription.Text = "Added from GATV Warehouse contact information"
                End If

            End While
            GATVdr.Close()


            SQL = "SELECT " &
             "tbl_ProjectManagement.ProjectIdentifier, " &
             "tblFacilityInformation_1_10_Contacts.ContactName, " &
             "tblFacilityInformation_1_10_Contacts.ContactTitle, " &
             "tblFacilityInformation_1_10_Contacts.ContactPhone, " &
             "tblFacilityInformation_1_10_Contacts.ContactPhoneExt, " &
             "tblFacilityInformation_1_10_Contacts.ContactFax, " &
             "tblFacilityInformation_1_10_Contacts.ContactEMail, " &
             "tblFacilityInformation_1_10_MailAddress.MailAddressCompany, " &
             "tblFacilityInformation_1_10_MailAddress.MailAddressStreet, " &
             "tblFacilityInformation_1_10_MailAddress.MailAddressCity, " &
             "tblFacilityInformation_1_10_MailAddress.MailAddressState, " &
             "tblFacilityInformation_1_10_MailAddress.MailingAddressZip " &
             "FROM (tblFacilityInformation_1_10_MailAddress INNER JOIN (tblFacilityInformation_1_10_Contacts " &
             "INNER JOIN tblFacilityInformation_1_10 " &
             "ON (tblFacilityInformation_1_10_Contacts.ContactID = tblFacilityInformation_1_10.ContactEnforcement) " &
             "AND (tblFacilityInformation_1_10_Contacts.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " &
             "ON (tblFacilityInformation_1_10_MailAddress.MailAddressID = tblFacilityInformation_1_10.MailContactEnforcement) " &
             "AND (tblFacilityInformation_1_10_MailAddress.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " &
             "INNER JOIN tbl_ProjectManagement " &
             "ON tblFacilityInformation_1_10.ProjectIdentifier = tbl_ProjectManagement.ProjectIdentifier " &
             "WHERE (((tbl_ProjectManagement.ProjectIdentifier)=[tblFacilityInformation_1_10].[ProjectIdentifier]) " &
             "AND ((tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_Contacts].[ProjectIdentifier] " &
             "And (tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_MailAddress].[ProjectIdentifier]) " &
             "AND ((tblFacilityInformation_1_10.ContactEnforcement)=[tblFacilityInformation_1_10_Contacts].[ContactID]) " &
             "AND ((tblFacilityInformation_1_10.MailContactEnforcement)=[tblFacilityInformation_1_10_MailAddress].[MailAddressID]) " &
             "AND ((tbl_ProjectManagement.ApplicationNumber)='" & AppNumber & "') or tbl_ProjectManagement.FacilityId = '" & AIRSNumber & "') " &
             "ORDER BY tbl_ProjectManagement.ProjectIdentifier "

            GATVConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\Permit\GATV\Warehouse\GATVWHSE.mdb;User Id=admin;Password=;")
            GATVcmd = New OleDbCommand(SQL, GATVConn)
            If GATVConn.State = ConnectionState.Closed Then
                GATVConn.Open()
            End If
            GATVdr = GATVcmd.ExecuteReader

            While GATVdr.Read
                If IsDBNull(GATVdr.Item("ContactName")) Then
                    ContactName = ""
                Else
                    ContactName = GATVdr.Item("ContactName")
                End If
                txtContactSocialTitleCompliance.Clear()
                txtContactPedigreeCompliance.Clear()
                If ContactName <> "" Then
                    If ContactName.Contains("Mr.") Then
                        txtContactSocialTitleCompliance.Text = "Mr."
                        ContactName = ContactName.Replace("Mr. ", "")
                    End If
                    If ContactName.Contains(" ") Then
                        txtContactFirstNameCompliance.Text = Mid(ContactName, 1, ContactName.IndexOf(" "))
                        txtContactLastNameCompliance.Text = Mid(ContactName, ContactName.IndexOf(" ") + 2)
                    Else
                        txtContactFirstNameCompliance.Text = ContactName
                        txtContactLastNameCompliance.Clear()
                    End If
                Else
                    txtContactFirstNameCompliance.Clear()
                    txtContactLastNameCompliance.Clear()
                End If
                If IsDBNull(GATVdr.Item("MailAddressCompany")) Then
                    txtContactCompanyNameCompliance.Clear()
                Else
                    txtContactCompanyNameCompliance.Text = GATVdr.Item("MailAddressCompany")
                End If
                If IsDBNull(GATVdr.Item("ContactTitle")) Then
                    txtContactTitleCompliance.Clear()
                Else
                    txtContactTitleCompliance.Text = GATVdr.Item("ContactTitle")
                End If
                If IsDBNull(GATVdr.Item("MailAddressStreet")) Then
                    txtContactStreetAddressCompliance.Clear()
                Else
                    txtContactStreetAddressCompliance.Text = GATVdr.Item("MailAddressStreet")
                End If
                If IsDBNull(GATVdr.Item("MailAddressCity")) Then
                    txtContactCityCompliance.Clear()
                Else
                    txtContactCityCompliance.Text = GATVdr.Item("MailAddressCity")
                End If
                If IsDBNull(GATVdr.Item("MailAddressState")) Then
                    txtContactStateCompliance.Clear()
                Else
                    txtContactStateCompliance.Text = GATVdr.Item("MailAddressState")
                End If
                If IsDBNull(GATVdr.Item("MailingAddressZip")) Then
                    mtbContactZipCodeCompliance.Clear()
                Else
                    mtbContactZipCodeCompliance.Text = GATVdr.Item("MailingAddressZip")
                End If
                If IsDBNull(GATVdr.Item("ContactPhone")) Then
                    mtbContactPhoneNumberCompliance.Clear()
                Else
                    mtbContactPhoneNumberCompliance.Text = GATVdr.Item("ContactPhone")
                End If
                If IsDBNull(GATVdr.Item("ContactFax")) Then
                    mtbContactFaxNumberCompliance.Clear()
                Else
                    mtbContactFaxNumberCompliance.Text = GATVdr.Item("ContactFax")
                End If
                If IsDBNull(GATVdr.Item("ContactEMail")) Then
                    txtContactEmailAddressCompliance.Clear()
                Else
                    txtContactEmailAddressCompliance.Text = GATVdr.Item("ContactEMail")
                End If
                txtContactDescriptionCompliance.Clear()
                If IsDBNull(GATVdr.Item("ContactPhoneExt")) Then
                Else
                    txtContactDescriptionCompliance.Text = "Contact extension - " & GATVdr.Item("ContactPhoneExt")
                End If
                If txtContactDescriptionCompliance.Text <> "" Then
                    txtContactDescriptionCompliance.Text = txtContactDescription.Text & vbCrLf &
                        "Added from GATV Warehouse contact information"
                Else
                    txtContactDescriptionCompliance.Text = "Added from GATV Warehouse contact information"
                End If

            End While
            GATVdr.Close()

        Catch ex As Exception
            txtContactDescription.Text = ex.ToString
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region " CodeFile "
    ' Code that was formerly in CodeFile.vb but is only used in this form anyway

    Function Insert_APBContactInformation(AIRSNumber As String, Key As String,
                                      ContactFirstName As String, ContactLastName As String,
                                      ContactPrefix As String, ContactSuffix As String,
                                      ContactTitle As String, ContactCompanyName As String,
                                      ContactPhoneNumber1 As String,
                                      ContactFaxNumber As String, ContactEmail As String,
                                      ContactAddress1 As String,
                                      ContactCity As String, ContactState As String,
                                      ContactZipCode As String, ContactDescription As String) As Boolean
        Try
            If ContactState.Length > 2 Then
                ContactState = "GA"
            End If
            If AIRSNumber = "" Then
                Return False
            End If
            Dim SQL As String = "Insert into APBContactInformation " &
             "values " &
             "('0413" & AIRSNumber & Key & "', '0413" & AIRSNumber & "', " &
             "" & Key & " , '" & Replace(ContactFirstName, "'", "''") & "', " &
             "'" & Replace(ContactLastName, "'", "''") & "', '" & Replace(ContactPrefix, "'", "''") & "', " &
             "'" & Replace(ContactSuffix, "'", "''") & "', '" & Replace(ContactTitle, "'", "''") & "', " &
             "'" & Replace(ContactCompanyName, "'", "''") & "', '" & Replace(ContactPhoneNumber1, "'", "''") & "', " &
             "'', '" & Replace(ContactFaxNumber, "'", "''") & "', " &
             "'" & Replace(ContactEmail, "'", "''") & "', '" & Replace(ContactAddress1, "'", "''") & "', " &
             "'', '" & Replace(ContactCity, "'", "''") & "', " &
             "'" & Replace(ContactState, "'", "''") & "', '" & Replace(ContactZipCode, "'", "''") & "', " &
             "'" & CurrentUser.UserID & "',  GETDATE() , " &
             "'" & Replace(ContactDescription, "'", "''") & "') "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Function

    Function Update_APBContactInformation(AIRSNumber As String, Key As String,
                                         ContactFirstName As String, ContactLastName As String,
                                         ContactPrefix As String, ContactSuffix As String,
                                         ContactTitle As String, ContactCompanyName As String,
                                         ContactPhoneNumber1 As String, ContactPhoneNumber2 As String,
                                         ContactFaxNumber As String, ContactEmail As String,
                                         ContactAddress1 As String, ContactAddress2 As String,
                                         ContactCity As String, ContactState As String,
                                         ContactZipCode As String, ContactDescription As String) As Boolean
        Try
            Dim NewKey As Integer = 0
            If ContactState.Length > 2 Then
                ContactState = "GA"
            End If
            If AIRSNumber = "" Then
                Return False
            End If

            Dim SQL As String = "Select " &
            "SUBSTRING(max(strKey) + 1, 2, 1) as NewKey " &
            "from APBContactInformation " &
            "where strAIRSNumber = '0413" & AIRSNumber & "' " &
            "and strKey like '" & Mid(Key, 1, 1) & "%' "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("newKey")) Then
                    NewKey = 0
                Else
                    NewKey = dr.Item("newKey")
                End If
            End While
            dr.Close()

            If NewKey = 0 Then
                NewKey = 9
                SQL = "Delete APBContactInformation " &
                "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                "and strKey = '" & Mid(Key, 1, 1) & "9'"
                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            Do Until NewKey = 0
                ' MsgBox(NewKey.ToString)

                SQL = "Update APBContactInformation set " &
                "strKey = '" & Mid(Key, 1, 1) & NewKey & "', " &
                "strContactKey = '0413" & AIRSNumber & Mid(Key, 1, 1) & NewKey & "' " &
                "where strAIRSNumber = '0413" & AIRSNumber & "' " &
                "and strKey = '" & Mid(Key, 1, 1) & (NewKey - 1) & "' "

                cmd = New SqlCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                NewKey -= 1
            Loop

            SQL = "Insert into APBContactInformation " &
            "values " &
            "('0413" & AIRSNumber & Mid(Key, 1, 1) & NewKey & "', " &
            "'0413" & AIRSNumber & "', '" & Key & "', " &
            "'" & Replace(ContactFirstName, "'", "''") & "', " &
            "'" & Replace(ContactLastName, "'", "''") & "', " &
            "'" & Replace(ContactPrefix, "'", "''") & "', " &
            "'" & Replace(ContactSuffix, "'", "''") & "', " &
            "'" & Replace(ContactTitle, "'", "''") & "', " &
            "'" & Replace(ContactCompanyName, "'", "''") & "', " &
            "'" & Replace(ContactPhoneNumber1, "'", "''") & "', " &
            "'" & Replace(ContactPhoneNumber2, "'", "''") & "', " &
            "'" & Replace(ContactFaxNumber, "'", "''") & "', " &
            "'" & Replace(ContactEmail, "'", "''") & "', " &
            "'" & Replace(ContactAddress1, "'", "''") & "', " &
            "'" & Replace(ContactAddress2, "'", "''") & "', " &
            "'" & Replace(ContactCity, "'", "''") & "', " &
            "'" & Replace(ContactState, "'", "''") & "', " &
            "'" & Replace(ContactZipCode, "'", "''") & "', " &
            "'" & CurrentUser.UserID & "', " &
            " GETDATE() , " &
            "'" & Replace(ContactDescription, "'", "''") & "') "

            cmd = New SqlCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Return True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Function

#End Region

End Class