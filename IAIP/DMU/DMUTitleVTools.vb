'Imports System.DateTime
Imports Oracle.DataAccess.Client
'Imports System.IO
Imports System.Data.OleDb
'Imports System.Data.Odbc

Public Class DMUTitleVTools
    Dim dsWebPublisher As DataSet
    Dim daWebPublisher As OracleDataAdapter
    Dim dsStaff As DataSet
    Dim daStaff As New OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim airsno As String
    Dim Startdate As String
    Dim EndDate As String

    Private Sub DMUTitleVTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            Panel1.Text = "Select a Function..."
            Panel2.Text = UserName
            Panel3.Text = OracleDate

            LoadPermissions()

            If IO.Directory.Exists(New System.IO.FileInfo("S:\Permit\GATV\Warehouse\GATVWHSE.mdb").DirectoryName) = True Then
                btnLoadFromWarehouse.Visible = True
            Else
                btnLoadFromWarehouse.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadWebPublisherDataGrid(ByVal AppNum As String)
        Dim SQLLine As String

        Try



            If AppNum = "Load" Then
                SQLLine = ""
            Else
                SQLLine = " and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & AppNum & "' "
            End If

            SQL = "select " & _
            "to_Number(" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber) as ApplicationNumber, " & _
            "case " & _
            "When datDraftIssued is Null then ' ' " & _
            "ELSE to_char(datDraftIssued, 'RRRR-MM-dd') " & _
            "END datDraftIssued, " & _
            "case " & _
            "When datPermitIssued is Null then ' ' " & _
            "ELSE to_Char(datPermitIssued, 'RRRR-MM-dd') " & _
            "END datPermitIssued, " & _
            "case " & _
            "When datExperationDate is Null then ' ' " & _
            "ELSE to_char(datExperationDate, 'RRRR-MM-dd') " & _
            "END datExperationDate, " & _
            "strFacilityName, " & _
            "case  " & _
            "        when strPermitNumber is NULL then ' '  " & _
            "         else substr(strPermitNumber, 1, 4)|| '-' ||substr(strPermitNumber, 5, 3)|| '-'  " & _
            "        ||substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)|| '-'           " & _
            "        ||substr(strPermitNumber, 13, 2)|| '-' ||substr(strPermitNumber, 15, 1) " & _
            "end As strPermitNumber, " & _
            "Case " & _
            "when datFinalizedDate is Null then ' ' " & _
            "Else to_char(datFinalizedDate, 'RRRR-MM-dd') " & _
            "End datFinalizedDate, " & _
            "strApplicationTypeDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationTracking, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".LookUpApplicationTypes " & _
            "where " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode = strApplicationType " & _
            "and " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
            "and (datDraftIssued is Not Null or datPermitIssued is Not Null or datEPAStatesNotified is Not Null) " & _
            "and datFinalOnWeb is Null " & _
            "and datFinalizedDate is Null " & _
            "and (strApplicationType = '17' or strApplicationType = '14' or strApplicationType = '16' " & _
            " or strApplicationType = '15' or strApplicationType = '26' or strApplicationType = '19' " & _
            " or strApplicationType = '20' or strApplicationType = '22' or strApplicationType = '21')" & SQLLine

            SQL = "SELECT " & _
            "TO_NUMBER(" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber) AS ApplicationNumber,  " & _
            "CASE  " & _
            "   WHEN datDraftIssued IS NULL THEN ' '  " & _
            "   ELSE TO_CHAR(datDraftIssued, 'RRRR-MM-dd') " & _
            "END datDraftIssued,  " & _
            "CASE  " & _
            "   WHEN datPermitIssued IS NULL THEN ' '  " & _
            "   ELSE TO_CHAR(datPermitIssued, 'RRRR-MM-dd') " & _
            "END datPermitIssued,  " & _
            "CASE  " & _
            "   WHEN datExperationDate IS NULL THEN ' '  " & _
            "   ELSE TO_CHAR(datExperationDate, 'RRRR-MM-dd') " & _
            "END datExperationDate,  " & _
            "strFacilityName,  " & _
            "CASE   " & _
            "   WHEN strPermitNumber IS NULL THEN ' '   " & _
            "   ELSE SUBSTR(strPermitNumber, 1, 4)|| '-' ||SUBSTR(strPermitNumber, 5, 3)|| '-'   " & _
            "    ||SUBSTR(strPermitNumber, 8, 4)|| '-' ||SUBSTR(strPermitNumber, 12, 1)|| '-'         " & _
            "    ||SUBSTR(strPermitNumber, 13, 2)|| '-' ||SUBSTR(strPermitNumber, 15, 1)  " & _
            "END AS strPermitNumber,  " & _
            "CASE  " & _
            "   WHEN datFinalizedDate IS NULL THEN ' '  " & _
            "   ELSE TO_CHAR(datFinalizedDate, 'RRRR-MM-dd') " & _
            "END datFinalizedDate,  " & _
            "strApplicationTypeDesc  " & _
            "from " & DBNameSpace & ".SSPPApplicationTracking, " & DBNameSpace & ".SSPPApplicationData,  " & _
            "" & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".LookUpApplicationTypes " & _
            "WHERE " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
            "AND " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode = strApplicationType  " & _
            "AND " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
            "AND ( " & _
            "(strApplicationType = '14' OR strApplicationType = '16' OR strApplicationType = '17') " & _
            "AND (datDraftOnWeb IS NULL OR datPNExpires IS NULL " & _
            "OR datEPAStatesNotified IS NULL OR datFinalOnWeb IS NULL " & _
            "OR datEPANotified IS NULL OR datEffective IS NULL " & _
            "OR datExperationDate IS NULL) " & _
            "OR " & _
            "(strApplicationType = '19' OR strApplicationType = '20') " & _
            "AND (datEPAStatesNotifiedAppRec IS NULL  " & _
            "OR datFinalOnWeb IS NULL OR datEPANotified IS NULL " & _
            "OR datEffective IS NULL OR datExperationDate IS NULL) " & _
            "OR " & _
            "(strApplicationType = '21' OR strApplicationType = '22') " & _
            "AND (datEPAStatesNotifiedAppRec IS NULL OR datDraftOnWeb IS NULL " & _
            "OR datPNExpires IS NULL OR datEPAStatesNotified IS NULL " & _
            "OR datFinalOnWeb IS NULL OR datEPANotified IS NULL " & _
            "OR datEffective IS NULL OR datExperationDate IS NULL) " & _
            "OR " & _
            "(strApplicationType = '15' OR strApplicationType = '26') " & _
            "AND (datFinalOnWeb IS NULL OR datEPANotified IS NULL " & _
            "OR datEffective IS NULL OR datExperationDate IS NULL) " & _
            "or " & _
            "strApplicationType = '2' " & _
            "OR " & _
            "strApplicationType = '11' " & _
            "and substr(strTrackedRules, 1, 1) = '1' " & _
            ") " & _
            "and datFinalizedDate is Null " & SQLLine & _
            " order by ApplicationNumber Desc "

            dsWebPublisher = New DataSet

            daWebPublisher = New OracleDataAdapter(SQL, CurrentConnection)

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatWebPublisherDataGrid()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            Dim objbooleancol As New DataGridBoolColumn

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Select " & _
                "strMasterApplication, strApplicationNumber " & _
                "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select " & _
                    "strMasterApplication, strApplicationNumber " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strMasterApplication = '" & MasterApplication & "' " & _
                    "order by strApplicationNumber "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadPermissions()
        Try

            TCDMUTools.TabPages.Remove(TPWebPublishing)
            TCDMUTools.TabPages.Remove(TPTVEmails)
            TCDMUTools.TabPages.Remove(TPTitleVRenewals)
            TCDMUTools.TabPages.Remove(TPPermittingContact)

            'AFS Users
            If AccountArray(131, 1) = "1" Then
                TitleVTools.Width = 800
                TitleVTools.Height = 600
            End If
            'Web Publishers
            If AccountArray(131, 2) = "1" Then
                TCDMUTools.TabPages.Add(TPWebPublishing)
                TCDMUTools.TabPages.Add(TPTVEmails)
                TCDMUTools.TabPages.Add(TPTitleVRenewals)
                TCDMUTools.TabPages.Add(TPPermittingContact)

                LoadWebPublisherDataGrid("Load")
                FormatWebPublisherDataGrid()

                DTPNotifiedAppReceived.Text = OracleDate
                DTPDraftOnWeb.Text = OracleDate
                DTPEffectiveDateofPermit.Text = OracleDate
                DTPEPANotifiedPermitOnWeb.Text = OracleDate
                DTPEPAStatesNotified.Text = OracleDate
                DTPFinalOnWeb.Text = OracleDate
                DTPTitleVRenewalStart.Text = OracleDate
                DTPTitleVRenewalEnd.Text = OracleDate
                DTPTitleVRenewalStart.Text = OracleDate
                DTPPNExpires.Text = OracleDate
                DTPExperationDate.Text = OracleDate
                DTPTitleVRenewalEnd.Text = Format(CDate(OracleDate).AddMonths(1), "dd-MMM-yyyy")

                TitleVTools.Width = 800
                TitleVTools.Height = 600

            End If
            If AccountArray(131, 3) = "1" Or AccountArray(131, 4) = "1" Then
                TitleVTools.Width = 800
                'If SystemInformation.PrimaryMonitorSize.Width > 1200 Then
                '    '  TitleVTools.Width = (SystemInformation.PrimaryMonitorSize.Width - 400)
                '    TitleVTools.Width = 800
                'Else
                '    TitleVTools.Width = 800
                'End If
                TitleVTools.Height = 600
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub LoadDataSetInformation()
        Try
            SQL = "select " & _
            "(strLastName||', '||strFirstName) as UserName,  " & _
            "numUserID  " & _
            "from AIRBranch.EPDUserProfiles  " & _
            "order by strLastName  "

            dsStaff = New DataSet

            daStaff = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daStaff.Fill(dsStaff, "Staff")


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

#Region "Subs and Functions"
    Sub LoadWebPublisherApplicationData()
        Try

            Dim AppType As String = ""

            SQL = "Select " & _
            "datDraftOnWeb, datEPAStatesNotified, " & _
            "datFinalONWeb, DatEPANotified, " & _
            "datEffective, strTargeted, " & _
            "datEPAStatesNotifiedAppRec, " & _
            "datExperationDate, datPNExpires, " & _
            "strApplicationType " & _
            "from " & DBNameSpace & ".SSPPApplicationTracking, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".SSPPApplicationMaster " & _
            "where " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber  " & _
            "and " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    DTPExperationDate.Text = OracleDate
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
                    DTPPNExpires.Text = OracleDate
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Update " & DBNameSpace & ".SSPPApplicationTracking set " & _
                "datDraftOnWeb = '" & DraftOnWeb & "', " & _
                "datEPAStatesNotified = '" & EPAStatesNotified & "', " & _
                "datFinalOnWeb = '" & FinalOnWeb & "', " & _
                "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " & _
                "datEffective = '" & EffectiveDateOnPermit & "', " & _
                "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "', " & _
                "datExperationDate = '" & ExperationDate & "', " & _
                "datPNExpires = '" & PNExpires & "' " & _
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strTargeted = '" & TargetedComments & "' " & _
                "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
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
                            SQL = "Update " & DBNameSpace & ".SSPPApplicationTracking set " & _
                            "datDraftOnWeb = '" & DraftOnWeb & "', " & _
                            "datEPAStatesNotified = '" & EPAStatesNotified & "', " & _
                            "datFinalOnWeb = '" & FinalOnWeb & "', " & _
                            "datEPANotified = '" & EPANotifiedPermitOnWeb & "', " & _
                            "datEffective = '" & EffectiveDateOnPermit & "', " & _
                            "datExperationDate = '" & ExperationDate & "', " & _
                            "datEPAStatesNotifiedAppRec = '" & EPAStatesNotifiedAppRec & "',  " & _
                            "datPNExpires = '" & PNExpires & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
                            If CurrentConnection.State = ConnectionState.Closed Then
                                CurrentConnection.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Read()
                            dr.Close()

                            SQL = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                            "strTargeted = '" & TargetedComments & "' " & _
                            "where strApplicationNumber = '" & LinkedApplication & "' "

                            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                    "strDraftOnWebNotification = 'False' " & _
                    "where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' " '& _

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If

                MsgBox("Web Information Saved", MsgBoxStyle.Information, "Application Tracking Log")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            temp = "App #  -  Airs #   - Facility Name " & vbTab & vbTab & vbTab & vbTab & vbTab & " (     Permit Number    ) " & vbTab & "Issued Date" & vbTab & "Effective Date"
            If clbTitleVRenewals.Items.Contains(temp) Then
            Else
                clbTitleVRenewals.Items.Add(temp)
            End If

            'This SQL statement was changed on Feb 3, 2010 to minimize the number of results based on effective and issued dates. 

            'SQL = "select " & _
            '"" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            '"substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
            '"" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
            '"(substr(strPermitNumber, 1, 4)|| '-' || substr(strPermitNumber, 5, 3)  " & _
            '"   || '-' || substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)  " & _
            '"     || '-' ||substr(strPermitNumber, 13,2) || '-' ||substr(strPermitNumber, 15)) as PermitNumber,  " & _
            '"to_char(datPermitIssued, 'dd-Mon-yyyy') as PermitIssued, " & _
            '"to_char(datEffective, 'dd-Mon-yyyy') as EffectiveDate  " & _
            '"from " & DBNameSpace & ".SSPPApplicationMaster,  " & _
            '"" & DBNameSpace & ".SSPPApplicationData, " & DBNameSpace & ".SSPPApplicationTracking,  " & _
            '"" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation  " & _
            '"where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
            '"and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber  " & _
            '"and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
            '"and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber  " & _
            '"and strPermitNumber Like '%V__0'  " & _
            '"and " & DBNameSpace & ".APBHeaderData.strOperationalStatus <> 'X'  " & _
            '"and substr(" & DBNameSpace & ".apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
            '"and (datPermitIssued between '" & Startdate & "' and '" & EndDate & "' " & _
            '"   or datEffective between '" & Startdate & "' and '" & EndDate & "') " & _
            '"and (strApplicationType = '14' or strApplicationType = '16' or strApplicationType = '27') "









            SQL = "select " & _
           "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
           "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
           "(substr(strPermitNumber, 1, 4)|| '-' || substr(strPermitNumber, 5, 3)  " & _
           "   || '-' || substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)  " & _
           "     || '-' ||substr(strPermitNumber, 13,2) || '-' ||substr(strPermitNumber, 15)) as PermitNumber,  " & _
           "to_char(datPermitIssued, 'dd-Mon-yyyy') as PermitIssued, " & _
           "to_char(datEffective, 'dd-Mon-yyyy') as EffectiveDate  " & _
           "from " & DBNameSpace & ".SSPPApplicationMaster,  " & _
           "" & DBNameSpace & ".SSPPApplicationData, " & DBNameSpace & ".SSPPApplicationTracking,  " & _
           "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation  " & _
           "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber  " & _
           "and strPermitNumber Like '%V__0'  " & _
           "and " & DBNameSpace & ".APBHeaderData.strOperationalStatus <> 'X'  " & _
           "and substr(" & DBNameSpace & ".apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
           "and (datPermitIssued not between '" & Startdate & "' and '" & EndDate & "' " & _
           "   and datEffective between '" & Startdate & "' and '" & EndDate & "') " & _
           "and (strApplicationType = '14' or strApplicationType = '16' or strApplicationType = '27') " & _
           "union  " & _
           "select " & _
           "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
           "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSnumber, 5) as AIRSNumber,  " & _
           "" & DBNameSpace & ".APBFacilityInformation.strFacilityName,  " & _
           "(substr(strPermitNumber, 1, 4)|| '-' || substr(strPermitNumber, 5, 3)  " & _
           "   || '-' || substr(strPermitNumber, 8, 4)|| '-' ||substr(strPermitNumber, 12, 1)  " & _
           "     || '-' ||substr(strPermitNumber, 13,2) || '-' ||substr(strPermitNumber, 15)) as PermitNumber,  " & _
           "to_char(datPermitIssued, 'dd-Mon-yyyy') as PermitIssued, " & _
           "to_char(datEffective, 'dd-Mon-yyyy') as EffectiveDate  " & _
           "from " & DBNameSpace & ".SSPPApplicationMaster,  " & _
           "" & DBNameSpace & ".SSPPApplicationData, " & DBNameSpace & ".SSPPApplicationTracking,  " & _
           "" & DBNameSpace & ".APBHeaderData, " & DBNameSpace & ".APBFacilityInformation  " & _
           "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber  " & _
           "and " & DBNameSpace & ".SSPPApplicationMaster.strAIRSnumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber  " & _
           "and strPermitNumber Like '%V__0'  " & _
           "and " & DBNameSpace & ".APBHeaderData.strOperationalStatus <> 'X'  " & _
           "and substr(" & DBNameSpace & ".apbheaderdata.strairprogramcodes, 13, 1) = '1' " & _
           "and (datPermitIssued between '" & Startdate & "' and '" & EndDate & "' " & _
           "   and datEffective between '" & Startdate & "' and '" & EndDate & "') " & _
           "and (strApplicationType = '14' or strApplicationType = '16' or strApplicationType = '27') "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    temp = ApplicationNumber & " - " & AIRSNumber & " - " & FacilityName
                    Select Case temp.Length
                        Case 10 To 24
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 25 To 32
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 33 To 39
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 40 To 45
                            temp = temp & vbTab & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 46 To 52
                            temp = temp & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 53 To 59
                            temp = temp & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 60 To 66
                            temp = temp & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case 67 To 100
                            temp = temp & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                        Case Else
                            temp = temp & vbTab & vbTab & vbTab & vbTab & vbTab & " ( " & PermitNumber & " ) " & vbTab & DateIssued & vbTab & EffectiveDate
                    End Select

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                SQL = "Select " & _
                "strFacilityName, strFacilityStreet1, " & _
                "strFacilityCity, strFacilityState, " & _
                "strFacilityZipCode " & _
                "from " & DBNameSpace & ".SSPPApplicationData " & _
                "Where strApplicationNumber = '" & txtWebPublisherApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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

                txtFacilityInformation.Text = FacilityName & vbCrLf & _
                Street & vbCrLf & _
                City & " " & State & ", " & ZipCode

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select " & _
            "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            "strFacilityName, strFacilityCity, " & _
            "strApplicationTypeDesc, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible, " & _
            "strUnitDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and (strAppReceivedNotification is Null or strAppReceivedNotification = 'False') " & _
            "and (strApplicationType = '19'  or strApplicationType = '20' or strApplicationType = '21' " & _
            "or strApplicationType = '22') " & _
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                txtEmailLetter.Text = "In accordance with 40 CFR 70.7(e)(2),(3), and (4) and 70.8(a)(1) and (b)(1), you are hereby notified that " & _
                "Georgia EPD has received an application for the modification of an existing Part 70 permit for the " & _
                "following source(s): " & vbCrLf & vbCrLf

                SQL = "Select " & _
                "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber,  " & _
                "strFacilityName, strFacilityCity,  " & _
                "strApplicationTypeDesc,  " & _
                "strCountyName  " & _
                "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".LookUpCountyInformation   " & _
                "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                "and substr(strAIRSNumber, 5, 3) = strCountyCode  "

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strAppReceivedNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))
                    SQLLine = SQLLine & " " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' or "
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine = "And ( " & Mid(SQLLine, 1, (SQLLine.Length - 3)) & " ) "
                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL = SQL & SQLLine & " order by strFacilityName "
                SQL2 = SQL2 & SQLLine2

                cmd = New OracleCommand(SQL, CurrentConnection)
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

                    txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                    FacCity & " (" & County & " County), GA" & vbCrLf & _
                    "TV-" & AppNumber & "/" & AppType & vbCrLf & vbCrLf
                End While
                dr.Close()

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. " & _
                "Any questions regarding this permit application may be directed to: " & vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select " & _
            "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            "strFacilityName, strFacilityCity, " & _
            "strApplicationTypeDesc, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible, " & _
            "strUnitDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationTracking, " & _
            "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " & _
            "and (strApplicationType = '14'  or strApplicationType = '16' or strApplicationType = '21' " & _
            "or strApplicationType = '22') " & _
            "and strPNReady = 'True' " & _
            "and datDraftOnWeb is Not Null " & _
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "select " & _
                "strMasterApplication " & _
                "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc,  " & _
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                    "strUnitDesc  " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationLinking, " & _
                    "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits  " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode   " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
                    "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                    "and " & DBNameSpace & ".SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached are the public notices for the " & _
                "draft/proposed permits and amendments for the following sources: " & vbCrLf & vbCrLf

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc, " & _
                    "strCountyName, datPNExpires " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpApplicationTypes, " & _
                    "" & DBNameSpace & ".SSPPApplicationTracking " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                    "and " & DBNameSpace & ".SSPPApplicationmaster.strAPplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                        SQL = "select " & _
                        "" & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber, " & _
                        "strApplicationTypeDesc " & _
                        "from " & DBNameSpace & ".SSPPApplicationLinking, " & DBNameSpace & ".SSPPApplicationMaster, " & _
                        "" & DBNameSpace & ".LookUpApplicationTypes " & _
                        "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                        "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                        FacCity & " (" & County & " County), GA" & vbCrLf & _
                        AppLine & vbCrLf & _
                        "30-day expires: " & PNExpires & vbCrLf & vbCrLf

                    End If
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " & _
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " & _
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf & _
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " & _
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf & _
                "http://www.georgiaair.org/airpermit/html/permits/draft.html" & vbCrLf & vbCrLf & _
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf & _
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " & _
                "amendments may be directed to: " & vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached are the public notices for the draft/proposed permits and " & _
                "amendments for the following sources: " & vbCrLf & vbCrLf

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc, " & _
                    "strCountyName " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpApplicationTypes " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                    "and substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                        SQL = "select " & _
                        "" & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber, " & _
                        "strApplicationTypeDesc " & _
                        "from " & DBNameSpace & ".SSPPApplicationLinking, " & DBNameSpace & ".SSPPApplicationMaster, " & _
                        "" & DBNameSpace & ".LookUpApplicationTypes " & _
                        "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                        "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                        FacCity & " (" & County & " County), GA" & vbCrLf & _
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The public notices are to be published by each facility in a newspaper of general " & _
                "circulation in the area where the facility is located within 14/30 days following their receipt of the draft permit and/or " & _
                "amendment and public notice. A 30-day comment period will follow the public notification. " & vbCrLf & vbCrLf & _
                "The draft permit, permit review narrative and in most cases the permit application will be available from the " & _
                "Georgia EPD - Air Protection Branch Title V Draft permit web page located at: " & vbCrLf & vbCrLf & _
                "http://www.georgiaair.org/airpermit/html/permits/draft.html" & vbCrLf & vbCrLf & _
                "The public comment deadline is posted on the Title V web page. " & vbCrLf & vbCrLf & _
                "Please reply to acknowledge receipt of this notification. Any questions regarding the draft permits and " & _
                "amendments may be directed to: " & vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            SQL = "Select " & _
            "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            "strFacilityName, strFacilityCity, " & _
            "strApplicationTypeDesc, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible, " & _
            "strUnitDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " & _
            "and (strApplicationType = '19'  or strApplicationType = '20') " & _
            "order by strFacilityName, strApplicationNumber DESC "


            SQL = "Select " & _
            "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            "strFacilityName, strFacilityCity, " & _
            "strApplicationTypeDesc, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible, " & _
            "strUnitDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDUnits, " & DBNameSpace & ".SSPPApplicationTracking " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and datEPAStatesNotified is not Null " & _
            "and (strDraftOnWebNotification is Null or strDraftOnWebNotification = 'False') " & _
            "and (strApplicationType = '19'  or strApplicationType = '20') " & _
            "order by strFacilityName, strApplicationNumber DESC "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "select " & _
                "strMasterApplication " & _
                "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc,  " & _
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                    "strUnitDesc  " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationLinking, " & _
                    "" & DBNameSpace & ".EPDuserProfiles, " & DBNameSpace & ".LookUPEPDunits " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode   " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID  " & _
                    "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                    "and " & DBNameSpace & ".SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with Georgia's Title V Implementation Agreement, attached is the proposed Part " & _
                "70 permit modification and permit amendment narrative for the following: " & vbCrLf & vbCrLf

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc, " & _
                    "strCountyName " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpApplicationTypes " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                    "and substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                        SQL = "select " & _
                        "" & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber, " & _
                        "strApplicationTypeDesc " & _
                        "from " & DBNameSpace & ".SSPPApplicationLinking, " & DBNameSpace & ".SSPPApplicationMaster, " & _
                        "" & DBNameSpace & ".LookUpApplicationTypes " & _
                        "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                        "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                        FacCity & " (" & County & " County), GA" & vbCrLf & _
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "EPA's review of the proposed minor amendment extends from 45 days following the date of this " & _
                "message. Please reply to acknowledge receipt of this notification. Any questions regarding this " & _
                "proposed permit amendment may be directed to: " & _
                vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with 40 CFR 70.8(b)(1), attached is the proposed Part 70 permit modification and  " & _
                "permit amendment narrative for the following source: " & vbCrLf & vbCrLf

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strDraftOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc, " & _
                    "strCountyName " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpApplicationTypes " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                    "and substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                        SQL = "select " & _
                        "" & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber, " & _
                        "strApplicationTypeDesc " & _
                        "from " & DBNameSpace & ".SSPPApplicationLinking, " & DBNameSpace & ".SSPPApplicationMaster, " & _
                        "" & DBNameSpace & ".LookUpApplicationTypes " & _
                        "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                        "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                        FacCity & " (" & County & " County), GA" & vbCrLf & _
                        AppLine & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "Please reply to acknowledge receipt of this notification. Any questions regarding this proposed " & _
                "permit amendment may be directed to: " & vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            SQL = "Select " & _
            "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
            "strFacilityName, strFacilityCity, " & _
            "strApplicationTypeDesc, " & _
            "(strLastName||', '||strFirstName) as StaffResponsible, " & _
            "strUnitDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
            "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationTracking, " & _
            "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber " & _
            "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDuserProfiles.numUserID " & _
            "and " & DBNameSpace & ".EPDuserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and (strFinalOnWebNotification is Null or strFinalOnWebNotification = 'False') " & _
            "and (strApplicationType = '14'  or strApplicationType = '16' " & _
            "or strApplicationType = '19' or strApplicationType = '20' " & _
            "or strApplicationType = '21' or strApplicationType = '22' " & _
            "or strApplicationType = '26' or strApplicationType = '15' " & _
            "or strApplicationType = '2') " & _
            "and DatFinalOnWeb is Not Null " & _
            "order by strFacilityName, strAPplicationNumber DESC "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                SQL = "select " & _
                "strMasterApplication " & _
                "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                "where strApplicationNumber = '" & MasterApp & "' "

                temp = ""
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    temp = dr.Item("strMasterApplication")
                End While
                dr.Close()

                If temp <> "" Then
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber,  " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strApplicationTypeDesc,  " & _
                    "(strLastName||', '||strFirstName) as StaffResponsible,  " & _
                    "strUnitDesc  " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationLinking, " & _
                    "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits  " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode   " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDuserPRofiles.numUserID  " & _
                    "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDunits.numUnitCode (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                    "and " & DBNameSpace & ".SSPPApplicationLinking.strMasterApplication = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            Dim SQLLine As String = ""
            Dim SQLLine2 As String = ""
            Dim temp As String = ""
            Dim strObject As Object

            If clbTitleVEmailList.Items.Count > 0 Then
                txtEmailLetter.Text = "In accordance with condition V.A.1.a of Georgia's Title V Agreement, the final Part 70 " & _
                "Permits were issued to the following sources:" & vbCrLf & vbCrLf

                SQL2 = "Update " & DBNameSpace & ".SSPPApplicationData set " & _
                "strFinalOnWebNotification = 'True' where "

                For Each strObject In clbTitleVEmailList.CheckedItems
                    temp = strObject
                    temp = Mid(temp, 1, (InStr(temp, " -", CompareMethod.Text) - 1))

                    SQL = "Select strMasterApplication " & _
                    "from " & DBNameSpace & ".SSPPApplicationLinking " & _
                    "where strApplicationNumber = '" & temp & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
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

                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity,  " & _
                    "strCountyName, strPermitNumber,  " & _
                    "datPermitIssued, datEffective, " & _
                    "strApplicationTypeDesc " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData,  " & _
                    "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".LookUpApplicationTypes, " & _
                    "" & DBNameSpace & ".SSPPApplicationTracking " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode  " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and substr(strAIRSNumber, 5, 3) = strCountyCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & temp & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                            PermitNumber = "Permit " & Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & Mid(PermitNumber, 8, 4) & _
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

                        SQL = "select " & _
                        "" & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber, " & _
                        "strApplicationTypeDesc " & _
                        "from " & DBNameSpace & ".SSPPApplicationLinking, " & DBNameSpace & ".SSPPApplicationMaster, " & _
                        "" & DBNameSpace & ".LookUpApplicationTypes " & _
                        "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationLinking.strApplicationNumber " & _
                        "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                        "and strMasterApplication = '" & LinkedApp & "' "
                        cmd = New OracleCommand(SQL, CurrentConnection)
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
                        txtEmailLetter.Text = txtEmailLetter.Text & FacName & vbCrLf & _
                        FacCity & " (" & County & " County), GA" & vbCrLf & _
                        AppLine & vbCrLf & _
                        PermitNumber & vbCrLf & DateIssued & vbCrLf & vbCrLf
                    End If
                    SQLLine2 = SQLLine2 & " " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber = '" & temp & "' or "
                Next

                SQLLine2 = Mid(SQLLine2, 1, (SQLLine2.Length - 3))
                SQL2 = SQL2 & SQLLine2

                txtEmailLetter.Text = txtEmailLetter.Text & "The final permit, permit review narrative and in most cases the " & _
                "permit application will be available from the Georgia Air Permit Search Engine web page located at: " & _
                vbCrLf & vbCrLf & _
                "http://airpermit.dnr.state.ga.us/gaairpermits/" & vbCrLf & vbCrLf & _
                "Please reply to acknowledge receipt of this notification. Any questions regarding the final permits " & _
                "may be directed to: " & vbCrLf & vbCrLf & _
                "Eric Cornwell " & vbCrLf & "Program Manager" & vbCrLf & _
                "Stationary Source Permitting Program " & vbCrLf & _
                "404/363-7020"

                cmd = New OracleCommand(SQL2, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                    SQL = "Select " & _
                   "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                   "strFacilityName, strFacilityCity, " & _
                   "strApplicationTypeDesc, " & _
                   "(strLastName||', '||strFirstName) as StaffResponsible, " & _
                   "strUnitDesc " & _
                   "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
                   "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationTracking, " & _
                   "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
                   "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
                   "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select " & _
                   "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                   "strFacilityName, strFacilityCity, " & _
                   "strApplicationTypeDesc, " & _
                   "(strLastName||', '||strFirstName) as StaffResponsible, " & _
                   "strUnitDesc " & _
                   "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
                   "" & DBNameSpace & ".LookUpApplicationTypes, " & DBNameSpace & ".SSPPApplicationTracking, " & _
                   "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
                   "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDUserProfiles.numUserID " & _
                   "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "


                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select " & _
                   "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                   "strFacilityName, strFacilityCity, " & _
                   "strApplicationTypeDesc, " & _
                   "(strLastName||', '||strFirstName) as StaffResponsible, " & _
                   "strUnitDesc " & _
                   "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
                   "" & DBNameSpace & ".LookUpApplicationTypes,  " & _
                   "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
                   "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDuserProfiles.numUserID " & _
                   "and " & DBNameSpace & ".EPDuserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                   "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
                    SQL = "Select " & _
                    "" & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber, " & _
                    "strFacilityName, strFacilityCity, " & _
                    "strApplicationTypeDesc, " & _
                    "(strLastName||', '||strFirstName) as StaffResponsible, " & _
                    "strUnitDesc " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".SSPPApplicationData, " & _
                    "" & DBNameSpace & ".LookUpApplicationTypes, " & _
                    "" & DBNameSpace & ".EPDuserProfiles, " & DBNameSpace & ".LookUpEPDUnits " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strStaffResponsible = " & DBNameSpace & ".EPDuserProfiles.numUserID " & _
                    "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & txtApplicationNumberToAdd.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub DEVDataManagementTools_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            TitleVTools = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnViewApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewApplication.Click
        Try

            If txtWebPublisherApplicationNumber.Text <> "" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = Nothing
                    If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = txtWebPublisherApplicationNumber.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnReloadGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadGrid.Click
        Try

            LoadWebPublisherDataGrid("Load")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrWebPublisher_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrWebPublisher.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtWebPublisherApplicationNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWebPublisherApplicationNumber.TextChanged
        Try

            'If txtWebPublisherApplicationNumber.Text <> "" Then
            '    LoadWebPublisherApplicationData()
            'End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#Region "Checkbox Changes"
    Private Sub chbNotifiedAppReceived_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbNotifiedAppReceived.CheckedChanged
        Try

            If chbNotifiedAppReceived.Checked = True Then
                DTPNotifiedAppReceived.Visible = True
            Else
                DTPNotifiedAppReceived.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbDraftOnWeb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDraftOnWeb.CheckedChanged
        Try

            If chbDraftOnWeb.Checked = True Then
                DTPDraftOnWeb.Visible = True
            Else
                DTPDraftOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbPNExpires_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbPNExpires.CheckedChanged
        Try

            If chbPNExpires.Checked = True Then
                DTPPNExpires.Visible = True
            Else
                DTPPNExpires.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEPAandStatesNotified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbEPAandStatesNotified.CheckedChanged
        Try

            If chbEPAandStatesNotified.Checked = True Then
                DTPEPAStatesNotified.Visible = True
            Else
                DTPEPAStatesNotified.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbFinalOnWeb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbFinalOnWeb.CheckedChanged
        Try

            If chbFinalOnWeb.Checked = True Then
                DTPFinalOnWeb.Visible = True
            Else
                DTPFinalOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEPANotifiedPermitOnWeb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbEPANotifiedPermitOnWeb.CheckedChanged
        Try

            If chbEPANotifiedPermitOnWeb.Checked = True Then
                DTPEPANotifiedPermitOnWeb.Visible = True
            Else
                DTPEPANotifiedPermitOnWeb.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbEffectiveDateOfPermit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbEffectiveDateOfPermit.CheckedChanged
        Try

            If chbEffectiveDateOfPermit.Checked = True Then
                DTPEffectiveDateofPermit.Visible = True
            Else
                DTPEffectiveDateofPermit.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub chbExperationDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbExpirationDate.CheckedChanged
        Try

            If chbExpirationDate.Checked = True Then
                DTPExperationDate.Visible = True
            Else
                DTPExperationDate.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub btnSaveWebPublisher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWebPublisher.Click
        Try

            SaveWebPublisherData()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try

            chbDraftOnWeb.Checked = False
            DTPDraftOnWeb.Text = OracleDate
            chbEPAandStatesNotified.Checked = False
            DTPEPAStatesNotified.Text = OracleDate
            chbFinalOnWeb.Checked = False
            DTPFinalOnWeb.Text = OracleDate
            chbEPANotifiedPermitOnWeb.Checked = False
            DTPEPANotifiedPermitOnWeb.Text = OracleDate
            chbEffectiveDateOfPermit.Checked = False
            DTPEffectiveDateofPermit.Text = OracleDate
            txtEPATargetedComments.Clear()
            txtWebPublisherApplicationNumber.Clear()
            DTPExperationDate.Text = OracleDate
            chbExpirationDate.Checked = False
            txtFacilityInformation.Clear()
            chbNotifiedAppReceived.Checked = False
            DTPNotifiedAppReceived.Text = OracleDate
            chbPNExpires.Checked = False
            DTPPNExpires.Text = OracleDate


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnSearchForApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForApplication.Click
        Try


            LoadWebPublisherApplicationData()
            LoadWebPublishingFacilityInformation()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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


            SQL = "Select DISTINCT substr(strairsnumber, 5) as strairsnumber, " _
            + "strfacilityname " _
            + "from " & DBNameSpace & ".APBFacilityInformation " _
            + "Order by strAIRSNumber "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, CurrentConnection)

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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            Return Nothing
        Finally

        End Try

    End Function
    Private Sub Back()
        Try

            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub UpdateRecords(ByVal userid As Object, ByVal adminaccess As Object, ByVal feeaccess As Object, ByVal eiaccess As Object, ByVal esaccess As Object)

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
            Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & userid & "' " & _
                      "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New OracleCommand(updateString, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

#End Region
#Region "Fee Password Reset"
    Private Sub SetPassword_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
    Private Sub txtWebPublisherApplicationNumber_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWebPublisherApplicationNumber.Leave
        Try

            If txtWebPublisherApplicationNumber.Text <> "" Then
                LoadWebPublisherApplicationData()
                LoadWebPublishingFacilityInformation()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtWebPublisherApplicationNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWebPublisherApplicationNumber.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                If txtWebPublisherApplicationNumber.Text <> "" Then
                    LoadWebPublisherApplicationData()
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnRunTitleVReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunTitleVReport.Click
        Try

            RunTitleVRenewalReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPrintRenewalLetters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintRenewalLetters.Click
        Try

            Dim SQLLine As String = "*"
            Dim temp As String = ""

            If Me.txtRenewalCount.Text <> "" And txtRenewalCount.Text <> "0" Then
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtPrintType.Text = "TitleVRenewal"
                PrintOut.txtSQLLine.Text = SQLLine
                PrintOut.txtStartDate.Text = Startdate
                PrintOut.txtEndDate.Text = EndDate
                PrintOut.Show()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewESNReceived_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewESNReceived.Click
        Try


            PreviewAppReceivedEmail()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewESNReceived_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnPreviewESNReceived.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewAppReceivedEmail()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewDraftOnWeb.Click
        Try


            PreviewDraftOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewDraftOnWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnPreviewDraftOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewDraftOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewMinorMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewMinorMod.Click
        Try


            PreviewMinorModOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewMinorMod_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnPreviewMinorMod.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewMinorModOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewFinalOnWeb.Click
        Try


            PreviewFinalOnWeb()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPreviewFinalOnWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnPreviewFinalOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                PreviewFinalOnWeb()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailESNReceived_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailESNReceived.Click
        Try


            GenerateAppReceivedEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailESNReceived_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnEmailESNReceived.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateAppReceivedEmail()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailDraftOnWeb.Click
        Try


            GenerateDraftOnWebEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnEmailDraftOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateDraftOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWebState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailDraftOnWebState.Click
        Try

            GenerateDraftOnWebState()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailDraftOnWebState_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnEmailDraftOnWebState.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateDraftOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebEPD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinorModOnWebEPD.Click
        Try


            GenerateMinorOnWebEmail()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebEPD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnMinorModOnWebEPD.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateMinorOnWebEmail()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinorModOnWebState.Click
        Try


            GenerateMinorOnWebState()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnMinorModOnWebState_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnMinorModOnWebState.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                GenerateMinorOnWebState()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailFinalOnWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailFinalOnWeb.Click
        Try

            GenerateFinalOnWeb()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnEmailFinalOnWeb_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnEmailFinalOnWeb.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then

                GenerateFinalOnWeb()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnAddApplicationToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddApplicationToList.Click
        Try

            AddAppToList()

            txtApplicationCount.Text = clbTitleVEmailList.Items.Count

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtApplicationNumberToAdd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicationNumberToAdd.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                AddAppToList()

                txtApplicationCount.Text = clbTitleVEmailList.Items.Count
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtEmailLetter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmailLetter.KeyPress
        Try

            If e.KeyChar = Microsoft.VisualBasic.ChrW(1) Then
                txtEmailLetter.SelectAll()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnPrintSingleTitleVRenewal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSingleTitleVRenewal.Click
        Try

            Dim SQLLine As String = "*"
            Dim temp As String = ""

            If txtTitleVSingleLetter.Text <> "" Then
                SQLLine = txtTitleVSingleLetter.Text
            Else
                SQLLine = "*"
            End If

            If (Me.txtRenewalCount.Text <> "" And txtRenewalCount.Text <> "0") Or txtTitleVSingleLetter.Text <> "" Then
                PrintOut = Nothing
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
                PrintOut.txtPrintType.Text = "TitleVRenewal"
                PrintOut.txtSQLLine.Text = SQLLine
                PrintOut.txtStartDate.Text = "01-Jan-1990"
                PrintOut.txtEndDate.Text = "01-Jan-2099"
                PrintOut.Show()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub btnLoadAppContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAppContact.Click
        Try
            If txtApplicationNumber.Text <> "" Then
                LoadContactData()
            Else
                MessageBox.Show("Please enter an application number first.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadContactData()
        Try
            Dim temp As String = ""
            SQL = "Select strApplicationNumber " & _
            "From " & DBNameSpace & ".SSPPApplicationContact " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " & _
                "substr(" & DBNameSpace & ".SSPPApplicationMaster.strAIRSNumber, 5) as AIRSNumber, " & _
                "strContactFirstName, " & _
                "strContactLastName, " & _
                "strContactPrefix, " & _
                "strContactSuffix, " & _
                "strContactTitle, " & _
                "strContactCompanyName, " & _
                "strContactPhoneNumber1, " & _
                "strContactFaxNumber, " & _
                "strContactEmail, " & _
                "strContactAddress1, " & _
                "strContactCity, " & _
                "strContactState, " & _
                "strContactZipCode, " & _
                "strContactDescription " & _
                "from " & DBNameSpace & ".SSPPApplicationContact, " & DBNameSpace & ".SSPPApplicationMaster " & _
                "where " & DBNameSpace & ".SSPPApplicationContact.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber " & _
                "and " & DBNameSpace & ".SSPPApplicationContact.strApplicationNumber = '" & txtApplicationNumber.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
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
                        temp = dr.Item("strContactPhoneNumber1")
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
                        txtContactDescription.Text = dr.Item("strContactDescription") & vbCrLf & _
                           "Added from App # - " & txtApplicationNumber.Text
                    End If
                End While
            Else
                '30
                SQL = "Select " & _
                "strContactFirstName, " & _
                "strContactLastName, " & _
                "strContactPrefix, " & _
                "strContactSuffix, " & _
                "strContactTitle, " & _
                "strContactCompanyName, " & _
                "strContactPhoneNumber1, " & _
                "strContactFaxNumber, " & _
                "strContactEmail, " & _
                "strContactAddress1, " & _
                "strContactCity, " & _
                "strContactState, " & _
                "strContactZipCode, " & _
                "strContactDescription " & _
                "from " & DBNameSpace & ".APBContactInformation " & _
                "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnGetCurrentPermittingContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCurrentPermittingContact.Click
        Try
            Dim temp As String = ""

            SQL = "Select " & _
            "strContactFirstName, " & _
             "strContactLastName, " & _
             "strContactPrefix, " & _
             "strContactSuffix, " & _
             "strContactTitle, " & _
             "strContactCompanyName, " & _
             "strContactPhoneNumber1, " & _
             "strContactFaxNumber, " & _
             "strContactEmail, " & _
             "strContactAddress1, " & _
             "strContactCity, " & _
             "strContactState, " & _
             "strContactZipCode, " & _
             "strContactDescription " & _
             "from " & DBNameSpace & ".APBContactInformation " & _
             "where strContactKey = '0413" & txtAIRSNumber.Text & "30' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                    temp = dr.Item("strContactPhoneNumber1")
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
                    txtContactDescription.Text = dr.Item("strContactDescription") & vbCrLf & _
                            "Added from Facility Summary"
                End If
                txtContactDescription.Text = "From App #- " & txtApplicationNumber.Text & vbCrLf & txtContactDescription.Text
            End While

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnSaveContactApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveContactApp.Click
        Try
            If txtApplicationNumber.Text <> "" And txtContactFirstName.Text <> "" And txtContactLastName.Text <> "" Then
                SaveApplicationContact()
                SaveComplianceContact()

                MessageBox.Show("Contact Information Saved")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                contactSuffix = txtContactPedigree.Text
            Else
                contactSuffix = " "
            End If
            If txtContactTitle.Text <> "" Then
                contactTitle = txtContactTitle.Text
            Else
                contactTitle = " "
            End If
            If txtContactCompanyName.Text <> "" Then
                contactCompany = txtContactCompanyName.Text
            Else
                contactCompany = " "
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

            SQL = "Select strApplicationNumber " & _
            "from " & DBNameSpace & ".SSPPApplicationContact " & _
            "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                'update
                SQL = "Update " & DBNameSpace & ".SSPPApplicationContact set " & _
                "strContactFirstName = '" & Replace(ContactFirstName, "'", "''") & "', " & _
                "strContactLastName = '" & Replace(ContactLastname, "'", "''") & "', " & _
                "strContactPrefix = '" & Replace(ContactPrefix, "'", "''") & "', " & _
                "strContactSuffix = '" & Replace(ContactSuffix, "'", "''") & "', " & _
                "strContactTitle = '" & Replace(ContactTitle, "'", "''") & "', " & _
                "strContactCompanyName = '" & Replace(ContactCompany, "'", "''") & "', " & _
                "strContactPhoneNumber1 = '" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "strContactfaxnumber = '" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "strContactemail = '" & Replace(ContactEmail, "'", "''") & "', " & _
                "strContactAddress1 = '" & Replace(ContactAddress, "'", "''") & "', " & _
                "strContactCity = '" & Replace(ContactCity, "'", "''") & "', " & _
                "strContactState = '" & Replace(ContactState, "'", "''") & "', " & _
                "strContactZipCode = '" & Replace(ContactZipCode, "-", "") & "', " & _
                "strContactDescription = '" & Replace(ContactDescription, "'", "''") & "' " & _
                "where strApplicationNumber = '" & txtApplicationNumber.Text & "' "
            Else
                'insert 
                SQL = "Insert into " & DBNameSpace & ".SSPPApplicationContact " & _
                "values " & _
                "('" & txtApplicationNumber.Text & "', " & _
                "'" & Replace(ContactFirstName, "'", "''") & "', " & _
                "'" & Replace(ContactLastname, "'", "''") & "', " & _
                "'" & Replace(ContactPrefix, "'", "''") & "', " & _
                "'" & Replace(ContactSuffix, "'", "''") & "', " & _
                "'" & Replace(ContactTitle, "'", "''") & "', " & _
                "'" & Replace(ContactCompany, "'", "''") & "', " & _
                "'" & Replace(Replace(Replace(Replace(ContactPhone, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "'" & Replace(Replace(Replace(Replace(ContactFax, "(", ""), ")", ""), "-", ""), " ", "") & "', " & _
                "'" & Replace(ContactEmail, "'", "''") & "', " & _
                "'" & Replace(ContactAddress, "'", "''") & "', " & _
                "'" & Replace(ContactCity, "'", "''") & "', " & _
                "'" & Replace(ContactState, "'", "''") & "', " & _
                "'" & Replace(ContactZipCode, "-", "") & "', " & _
                "'" & Replace(ContactDescription, "'", "''") & "') "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub SaveComplianceContact()
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
            If txtContactDescriptionCompliance.Text <> "" Then
                ContactDescription = txtContactDescriptionCompliance.Text
            Else
                If txtApplicationNumber.Text <> "" Then
                    ContactDescription = "Added by Title V Tool"
                Else
                    ContactDescription = " "
                End If
            End If

            SQL = "Select count(*) as SSCPContact " & _
            "From " & DBNameSpace & ".APBContactInformation " & _
            "where strContactKey = '0413" & txtAIRSNumber.Text & "20' "

            cmd = New OracleCommand(SQL, CurrentConnection)
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
                Insert_APBContactInformation(txtAIRSNumber.Text, "20", _
                                              ContactFirstName, ContactLastname, _
                                              ContactPrefix, ContactSuffix, _
                                              ContactTitle, ContactCompany, _
                                              ContactPhone, "", _
                                              ContactFax, ContactEmail, _
                                              ContactAddress, "", _
                                              ContactCity, ContactState, _
                                              ContactZipCode, "Contact Added from Title V Warehouse from Enforcement Contact")
            Else
                Update_APBContactInformation(txtAIRSNumber.Text, "20", _
                                              ContactFirstName, ContactLastname, _
                                              ContactPrefix, ContactSuffix, _
                                              ContactTitle, ContactCompany, _
                                              ContactPhone, "", _
                                              ContactFax, ContactEmail, _
                                              ContactAddress, "", _
                                              ContactCity, ContactState, _
                                              ContactZipCode, "Contact Added from Title V Warehouse for Enforcement Contact")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnLoadFromWarehouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadFromWarehouse.Click
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

            SQL = "SELECT " & _
            "tbl_ProjectManagement.ProjectIdentifier, " & _
            "tblFacilityInformation_1_10_Contacts.ContactName, " & _
            "tblFacilityInformation_1_10_Contacts.ContactTitle, " & _
            "tblFacilityInformation_1_10_Contacts.ContactPhone, " & _
            "tblFacilityInformation_1_10_Contacts.ContactPhoneExt, " & _
            "tblFacilityInformation_1_10_Contacts.ContactFax, " & _
            "tblFacilityInformation_1_10_Contacts.ContactEMail, " & _
            "tblFacilityInformation_1_10_MailAddress.MailAddressCompany, " & _
            "tblFacilityInformation_1_10_MailAddress.MailAddressStreet, " & _
            "tblFacilityInformation_1_10_MailAddress.MailAddressCity, " & _
            "tblFacilityInformation_1_10_MailAddress.MailAddressState, " & _
            "tblFacilityInformation_1_10_MailAddress.MailingAddressZip " & _
            "FROM (tblFacilityInformation_1_10_MailAddress INNER JOIN (tblFacilityInformation_1_10_Contacts " & _
            "INNER JOIN tblFacilityInformation_1_10 " & _
            "ON (tblFacilityInformation_1_10_Contacts.ContactID = tblFacilityInformation_1_10.ContactForPermits) " & _
            "AND (tblFacilityInformation_1_10_Contacts.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
            "ON (tblFacilityInformation_1_10_MailAddress.MailAddressID = tblFacilityInformation_1_10.MailContactForPermits) " & _
            "AND (tblFacilityInformation_1_10_MailAddress.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
            "INNER JOIN tbl_ProjectManagement " & _
            "ON tblFacilityInformation_1_10.ProjectIdentifier = tbl_ProjectManagement.ProjectIdentifier " & _
            "WHERE (((tbl_ProjectManagement.ProjectIdentifier)=[tblFacilityInformation_1_10].[ProjectIdentifier]) " & _
            "AND ((tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_Contacts].[ProjectIdentifier] " & _
            "And (tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_MailAddress].[ProjectIdentifier]) " & _
            "AND ((tblFacilityInformation_1_10.ContactForPermits)=[tblFacilityInformation_1_10_Contacts].[ContactID]) " & _
            "AND ((tblFacilityInformation_1_10.MailContactForPermits)=[tblFacilityInformation_1_10_MailAddress].[MailAddressID]) " & _
            "AND ((tbl_ProjectManagement.ApplicationNumber)='" & AppNumber & "') or tbl_ProjectManagement.FacilityId = '" & AIRSNumber & "') " & _
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
                    txtContactDescription.Text = txtContactDescription.Text & vbCrLf & _
                        "Added from GATV Warehouse contact information"
                Else
                    txtContactDescription.Text = "Added from GATV Warehouse contact information"
                End If

            End While
            GATVdr.Close()


            SQL = "SELECT " & _
             "tbl_ProjectManagement.ProjectIdentifier, " & _
             "tblFacilityInformation_1_10_Contacts.ContactName, " & _
             "tblFacilityInformation_1_10_Contacts.ContactTitle, " & _
             "tblFacilityInformation_1_10_Contacts.ContactPhone, " & _
             "tblFacilityInformation_1_10_Contacts.ContactPhoneExt, " & _
             "tblFacilityInformation_1_10_Contacts.ContactFax, " & _
             "tblFacilityInformation_1_10_Contacts.ContactEMail, " & _
             "tblFacilityInformation_1_10_MailAddress.MailAddressCompany, " & _
             "tblFacilityInformation_1_10_MailAddress.MailAddressStreet, " & _
             "tblFacilityInformation_1_10_MailAddress.MailAddressCity, " & _
             "tblFacilityInformation_1_10_MailAddress.MailAddressState, " & _
             "tblFacilityInformation_1_10_MailAddress.MailingAddressZip " & _
             "FROM (tblFacilityInformation_1_10_MailAddress INNER JOIN (tblFacilityInformation_1_10_Contacts " & _
             "INNER JOIN tblFacilityInformation_1_10 " & _
             "ON (tblFacilityInformation_1_10_Contacts.ContactID = tblFacilityInformation_1_10.ContactEnforcement) " & _
             "AND (tblFacilityInformation_1_10_Contacts.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
             "ON (tblFacilityInformation_1_10_MailAddress.MailAddressID = tblFacilityInformation_1_10.MailContactEnforcement) " & _
             "AND (tblFacilityInformation_1_10_MailAddress.ProjectIdentifier = tblFacilityInformation_1_10.ProjectIdentifier)) " & _
             "INNER JOIN tbl_ProjectManagement " & _
             "ON tblFacilityInformation_1_10.ProjectIdentifier = tbl_ProjectManagement.ProjectIdentifier " & _
             "WHERE (((tbl_ProjectManagement.ProjectIdentifier)=[tblFacilityInformation_1_10].[ProjectIdentifier]) " & _
             "AND ((tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_Contacts].[ProjectIdentifier] " & _
             "And (tblFacilityInformation_1_10.ProjectIdentifier)=[tblFacilityInformation_1_10_MailAddress].[ProjectIdentifier]) " & _
             "AND ((tblFacilityInformation_1_10.ContactEnforcement)=[tblFacilityInformation_1_10_Contacts].[ContactID]) " & _
             "AND ((tblFacilityInformation_1_10.MailContactEnforcement)=[tblFacilityInformation_1_10_MailAddress].[MailAddressID]) " & _
             "AND ((tbl_ProjectManagement.ApplicationNumber)='" & AppNumber & "') or tbl_ProjectManagement.FacilityId = '" & AIRSNumber & "') " & _
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
                    txtContactDescriptionCompliance.Text = txtContactDescription.Text & vbCrLf & _
                        "Added from GATV Warehouse contact information"
                Else
                    txtContactDescriptionCompliance.Text = "Added from GATV Warehouse contact information"
                End If

            End While
            GATVdr.Close()

        Catch ex As Exception
            txtContactDescription.Text = ex.ToString
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
 
End Class