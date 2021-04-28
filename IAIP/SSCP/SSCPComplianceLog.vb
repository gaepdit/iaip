Imports System.Collections.Generic
Imports System.Data.SqlClient


Public Class SSCPComplianceLog

#Region "Page Load"

    Private Sub SSCPComplianceLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DTPDateReceived.Value = Today

            chbEngineer.Text = CurrentUser.AlphaName

            DTPFilterStart.Enabled = False
            DTPFilterEnd.Enabled = False

            LoadDefaultSettings()
            LoadComboBoxes()
            LoadDgvWork()

            clbEngineer.SelectedItems.Clear()
            clbAirBranchUnits.SelectedItems.Clear()
            clbDistrictOffices.SelectedItems.Clear()

            If AccountFormAccess(4, 1) = "1" AndAlso Not CurrentUser.HasRole({19, 113, 114, 141, 118}) Then
                btnAddNewEntry.Visible = False
                btnDeleteWork.Visible = False
                btnUndeleteWork.Visible = False
                rdbEnforcementAction.Enabled = False
                rdbFCE.Enabled = False
                TCComplianceLog.TabPages.Remove(TPStartNewWork)
            End If

            If AccountFormAccess(4, 2) = "1" Then
                rdbEnforcementAction.Enabled = True
                rdbFCE.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadComboBoxes()
        Dim SQL As String = "select strActivityType, strActivityName, strActivityDescription " &
            "from LookUPComplianceActivities " &
            "where strActivityName <> 'Performance Tests' " &
            "order by strActivityName"
        Dim dtEvent As DataTable = DB.GetDataTable(SQL)

        dtEvent.PrimaryKey = {dtEvent.Columns("strActivityType")}

        With cboEvent
            .DataSource = dtEvent
            .DisplayMember = "strActivityName"
            .ValueMember = "strActivityType"
            If cboEvent.SelectedIndex > -1 Then
                .SelectedIndex = 0
            End If
        End With

        With clbEngineer
            .DataSource = GetSharedData(SharedTable.AllComplianceStaff)
            .DisplayMember = "StaffName"
            .ValueMember = "UserID"
        End With

        With clbNotifications
            .DataSource = GetSharedData(SharedTable.SscpNotificationTypes)
            .DisplayMember = "STRNOTIFICATIONDESC"
            .ValueMember = "STRNOTIFICATIONKEY"
        End With

        With clbAirBranchUnits
            .DataSource = DAL.GetEpdUnitsAsDataTable(4)
            .DisplayMember = "Description"
            .ValueMember = "UnitCode"
        End With

        With clbDistrictOffices
            .DataSource = DAL.GetEpdProgramsAsDataTable(5)
            .DisplayMember = "Description"
            .ValueMember = "ProgramCode"
        End With
    End Sub

    Private Sub LoadDefaultSettings()
        If AccountFormAccess(4, 3) = "1" OrElse AccountFormAccess(4, 4) = "1" Then 'Full Access in unit or District Liaison
            chbEngineer.Checked = False
        Else
            chbEngineer.Checked = True
        End If

        chbAllWork.Checked = True
        chbACCs.Checked = False
        chbEnforcement.Checked = False
        chbFCE.Checked = False
        chbInspections.Checked = False
        chbNotifications.Checked = False
        chbPerformanceTests.Checked = False
        chbReports.Checked = False

        For x As Integer = 0 To clbEngineer.Items.Count - 1
            clbEngineer.SetItemCheckState(x, CheckState.Unchecked)
        Next

        txtAIRSNumberFilter.Clear()
        txtTrackingNumberFilter.Clear()
        txtEnforcementNumberFilter.Clear()
        txtFCENumberFilter.Clear()
        txtFacilityNameFilter.Clear()

        chbOpenWork.Checked = True
        chbCompletedWork.Checked = False
        chbDeletedWork.Checked = False

        txtWorkNumber.Clear()
        DTPFilterStart.Value = Today.AddMonths(-1)
        DTPFilterEnd.Value = Today
        chbFilterDates.Checked = False

        txtAIRSNumber.Clear()
        txtFacilityName.Clear()
        txtTestType.Clear()
        txtFacilityCity.Clear()

        txtNewAIRSNumber.Clear()
        rdbEnforcementAction.Checked = False
        rdbFCE.Checked = False
        rdbOther.Checked = False
        txtFacilityInformation.Clear()
    End Sub

#End Region

#Region " Search "

    Private Sub LoadDgvWork()
        Try

            Dim SQL As String = "SELECT *
                FROM (
                    SELECT SUBSTRING(m.STRAIRSNUMBER, 5, 8)            AS [AIRS #], f.STRFACILITYNAME AS [Facility Name],
                           h.STRCLASS                                  AS Class,
                           CASE
                               WHEN m.STREVENTTYPE = '05' THEN CONCAT(c.STRACTIVITYNAME, '-', l.STRNOTIFICATIONDESC)
                               ELSE c.STRACTIVITYNAME
                           END                                         AS [Work Type],
                           CONCAT(u.STRLASTNAME, ', ', u.STRFIRSTNAME) AS Staff, m.STRTRACKINGNUMBER AS [Unique #],
                           CASE
                               WHEN m.STRDELETE IS NOT NULL THEN 'Deleted'
                               WHEN m.DATCOMPLETEDATE IS NOT NULL THEN 'Closed'
                               ELSE 'Open'
                           END                                         AS Status,
                           m
                               .DATRECEIVEDDATE                        AS [Received Date],
                           i.DATINSPECTIONDATESTART                    AS [Inspection Date], NULL AS [FCE Date],
                           NULL                                        AS [Enf Discovery Date],
                           t.STRREFERENCENUMBER                        as [Stack Test Ref], m.DATCOMPLETEDATE AS [Date Completed],
                           l.STRNOTIFICATIONDESC                       AS [Notification Type],
                           CASE
                               WHEN m.STREVENTTYPE = '01' THEN r.DATMODIFINGDATE
                               WHEN m.STREVENTTYPE = '02' THEN i.DATMODIFINGDATE
                               WHEN m.STREVENTTYPE = '03' THEN t.DATMODIFINGDATE
                               WHEN m.STREVENTTYPE = '04' THEN a.DATMODIFINGDATE
                               WHEN m.STREVENTTYPE = '05' THEN n.DATMODIFINGDATE
                               WHEN m.STREVENTTYPE = '07' THEN i.DATMODIFINGDATE
                           END                                         AS [Last Modified],
                           'IT'                                        AS Flag, u.NUMUSERID as [User ID]
                    FROM SSCPITEMMASTER AS m
                        INNER JOIN LOOKUPCOMPLIANCEACTIVITIES AS c
                        ON m.STREVENTTYPE = c.STRACTIVITYTYPE
                        INNER JOIN APBFACILITYINFORMATION AS f
                        ON m.STRAIRSNUMBER = f.STRAIRSNUMBER
                        INNER JOIN APBHEADERDATA AS h
                        ON f.STRAIRSNUMBER = h.STRAIRSNUMBER
                        INNER JOIN EPDUSERPROFILES AS u
                        ON m.STRRESPONSIBLESTAFF = u.NUMUSERID
                        LEFT JOIN SSCPTESTREPORTS AS t
                        ON m.STRTRACKINGNUMBER = t.STRTRACKINGNUMBER
                        LEFT JOIN SSCPINSPECTIONS AS i
                        ON m.STRTRACKINGNUMBER = i.STRTRACKINGNUMBER
                        LEFT JOIN SSCPACCS AS a
                        ON m.STRTRACKINGNUMBER = a.STRTRACKINGNUMBER
                        LEFT JOIN SSCPREPORTS AS r
                        ON m.STRTRACKINGNUMBER = r.STRTRACKINGNUMBER
                        LEFT JOIN SSCPNOTIFICATIONS AS n
                        ON m.STRTRACKINGNUMBER = n.STRTRACKINGNUMBER
                        LEFT JOIN LOOKUPSSCPNOTIFICATIONS AS l
                        ON n.STRNOTIFICATIONTYPE = l.STRNOTIFICATIONKEY
                    UNION
                    SELECT SUBSTRING(i.STRAIRSNUMBER, 5, 8), i.STRFACILITYNAME, h.STRCLASS, 'Full Compliance Evaluation',
                           CONCAT(u.STRLASTNAME, ', ', u.STRFIRSTNAME), fm.STRFCENUMBER,
                           CASE when fm.IsDeleted = 1 then 'Deleted' WHEN f.DATFCECOMPLETED IS NOT NULL THEN 'Closed' ELSE 'Open' END,
                           NULL, NULL, f.DATFCECOMPLETED, NULL, null, f.DATFCECOMPLETED, NULL, f.DATMODIFINGDATE, 'FC',
                           u.NUMUSERID as [User ID]
                    FROM APBFACILITYINFORMATION AS i
                        INNER JOIN APBHeaderData AS h
                        ON h.STRAIRSNUMBER = i.STRAIRSNUMBER
                        INNER JOIN SSCPFCEMASTER AS fm
                        ON i.STRAIRSNUMBER = fm.STRAIRSNUMBER
                        INNER JOIN SSCPFCE AS f
                        ON fm.STRFCENUMBER = f.STRFCENUMBER
                        INNER JOIN EPDUSERPROFILES AS u
                        ON f.STRREVIEWER = u.NUMUSERID
                    UNION
                    SELECT SUBSTRING(i.STRAIRSNUMBER, 5, 8), i.STRFACILITYNAME, h.STRCLASS, CONCAT('Enforcement-', e.STRACTIONTYPE),
                           CONCAT(u.STRLASTNAME, ', ', u.STRFIRSTNAME), e.STRENFORCEMENTNUMBER,
                           CASE
                               when e.IsDeleted = 1 then 'Deleted'
                               WHEN e.DATENFORCEMENTFINALIZED IS NOT NULL THEN 'Closed'
                               ELSE 'Open'
                           END,
                           NULL, NULL, NULL, e.DATDISCOVERYDATE, null, e.DATENFORCEMENTFINALIZED, NULL, e.DATMODIFINGDATE, 'EN',
                           u.NUMUSERID as [User ID]
                    FROM APBFACILITYINFORMATION AS i
                        INNER JOIN SSCP_AUDITEDENFORCEMENT AS e
                        ON i.STRAIRSNUMBER = e.STRAIRSNUMBER
                        INNER JOIN EPDUSERPROFILES AS u
                        ON e.NUMSTAFFRESPONSIBLE = u.NUMUSERID
                        INNER JOIN APBHEADERDATA AS h
                        ON h.STRAIRSNUMBER = i.STRAIRSNUMBER) AS AllData
                WHERE 1 = 1 "

            Dim SqlFilter As String = ""

            If Not chbAllWork.Checked Then
                If chbACCs.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'Annual Compliance Certification' or "
                End If
                If chbEnforcement.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] like 'Enforcement%' or "
                End If
                If chbFCE.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'Full Compliance Evaluation' or "
                End If
                If chbInspections.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'Inspection' or "
                End If
                If chbRMPInspections.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'RMP Inspection' or "
                End If
                If chbNotifications.Checked Then
                    SqlFilter = SqlFilter & " ([Work Type] like 'Notification%'"
                    Dim NotificationData As String = " and ("
                    For Each ind As Integer In clbNotifications.CheckedIndices
                        clbNotifications.SelectedIndex = ind
                        NotificationData = NotificationData & " [Notification Type] = '" & clbNotifications.SelectedValue & "' or "
                    Next
                    If NotificationData.Length > 6 Then
                        SqlFilter = SqlFilter & Mid(NotificationData, 1, NotificationData.Length - 3) & ") "
                    End If
                    SqlFilter = SqlFilter & ") or "
                End If
                If chbPerformanceTests.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'Performance Tests' or "
                End If
                If chbReports.Checked Then
                    SqlFilter = SqlFilter & " [Work Type] = 'Report' or "
                End If

                If SqlFilter <> "" Then
                    SQL = SQL & " and ( " & Mid(SqlFilter, 1, (SqlFilter.Length) - 3) & " ) "
                    SqlFilter = ""
                End If
            End If

            If chbFilterDates.Checked Then
                If chbAllWork.Checked Then
                    SqlFilter = SqlFilter & " ([Received Date] between @datestart and @dateend " &
                    "or [Inspection Date] between @datestart and @dateend " &
                    "or [FCE Date] between @datestart and @dateend " &
                    "or [Enf Discovery Date] between @datestart and @dateend " &
                    "or [Last Modified] between @datestart and @dateend) or "
                Else
                    If chbACCs.Checked Then
                        SqlFilter = SqlFilter & " [Received Date] between @datestart and @dateend or "
                    End If
                    If chbEnforcement.Checked Then
                        SqlFilter = SqlFilter & " [Enf Discovery Date] between @datestart and @dateend or "
                    End If
                    If chbFCE.Checked Then
                        SqlFilter = SqlFilter & " [FCE Date] between @datestart and @dateend or "
                    End If
                    If chbInspections.Checked Then
                        SqlFilter = SqlFilter & " [Inspection Date] between @datestart and @dateend or "
                    End If
                    If chbNotifications.Checked Then
                        SqlFilter = SqlFilter & " [Received Date] between @datestart and @dateend or "
                    End If
                    If chbPerformanceTests.Checked Then
                        SqlFilter = SqlFilter & " [Received Date] between @datestart and @dateend or "
                    End If
                    If chbReports.Checked Then
                        SqlFilter = SqlFilter & " [Received Date] between @datestart and @dateend or "
                    End If
                    If chbRMPInspections.Checked Then
                        SqlFilter = SqlFilter & " [Received Date] between @datestart and @dateend or "
                    End If
                    If chbLastModifiedDate.Checked Then
                        SqlFilter = SqlFilter & " [Last Modified] between @datestart and @dateend or "
                    End If
                End If

                If SqlFilter <> "" Then
                    SQL = SQL & " and ( " & Mid(SqlFilter, 1, (SqlFilter.Length) - 3) & " ) "
                    SqlFilter = ""
                End If
            End If

            If rdbUseEngineer.Checked Then
                If chbEngineer.Checked Then
                    SqlFilter = SqlFilter & " [User ID] = '" & CurrentUser.UserID & "' Or "
                Else
                    For Each ind As Integer In clbEngineer.CheckedIndices
                        clbEngineer.SelectedIndex = ind
                        SqlFilter = SqlFilter & " [User ID] = '" & clbEngineer.SelectedValue & "' Or "
                    Next
                End If
            End If

            If rdbUseUnits.Checked Then
                Dim idSetUnits As New HashSet(Of Integer)
                For Each ind As Integer In clbAirBranchUnits.CheckedIndices
                    clbAirBranchUnits.SelectedIndex = ind
                    idSetUnits.Add(clbAirBranchUnits.SelectedValue)
                Next
                If idSetUnits.Count > 0 Then
                    SqlFilter = " [User ID] IN (select numuserid from epduserprofiles where numunit in (" &
                        String.Join(",", idSetUnits) &
                        ") and numProgram = 4) or "
                End If

                Dim idSetDistricts As New HashSet(Of Integer)
                For Each ind As Integer In clbDistrictOffices.CheckedIndices
                    clbDistrictOffices.SelectedIndex = ind
                    idSetDistricts.Add(clbDistrictOffices.SelectedValue)
                Next
                If idSetDistricts.Count > 0 Then
                    SqlFilter = SqlFilter & " [User ID] IN (select numuserid from epduserprofiles where numProgram in (" &
                        String.Join(",", idSetDistricts) &
                        ") and numBranch = 5) or "
                End If
            End If

            If SqlFilter <> "" Then
                SQL = SQL & " and ( " & Mid(SqlFilter, 1, (SqlFilter.Length) - 3) & " ) "
                SqlFilter = ""
            End If

            If chbOpenWork.Checked Then
                SqlFilter = SqlFilter & " Status = 'Open' or "
            End If
            If chbCompletedWork.Checked Then
                SqlFilter = SqlFilter & " Status = 'Closed' or "
            End If
            If chbDeletedWork.Checked Then
                SqlFilter = SqlFilter & " Status = 'Deleted' or "
            End If

            If SqlFilter <> "" Then
                SQL = SQL & " and ( " & Mid(SqlFilter, 1, (SqlFilter.Length) - 3) & " ) "
                SqlFilter = ""
            End If

            If txtTrackingNumberFilter.Text <> "" Then
                SqlFilter = SqlFilter & " (Flag = 'IT' and [Unique #] like @trk) or "
            End If
            If txtEnforcementNumberFilter.Text <> "" Then
                SqlFilter = SqlFilter & " (Flag = 'EN' and [Unique #] like @enf) or "
            End If
            If txtFCENumberFilter.Text <> "" Then
                SqlFilter = SqlFilter & " (Flag = 'FC' and [Unique #] like @fce) or "
            End If

            If SqlFilter <> "" Then
                SQL = SQL & " and ( " & Mid(SqlFilter, 1, (SqlFilter.Length) - 3) & " ) "
            End If

            If txtAIRSNumberFilter.Text <> "" Then
                SQL = SQL & " and [AIRS #] like @airs "
            End If
            If txtFacilityNameFilter.Text <> "" Then
                SQL = SQL & " and [Facility Name] like @fac "
            End If

            Dim p As SqlParameter() = {
                New SqlParameter("@datestart", DTPFilterStart.Value),
                New SqlParameter("@dateend", DTPFilterEnd.Value.AddDays(1)),
                New SqlParameter("@airs", "%" & txtAIRSNumberFilter.Text & "%"),
                New SqlParameter("@trk", "%" & txtTrackingNumberFilter.Text & "%"),
                New SqlParameter("@enf", "%" & txtEnforcementNumberFilter.Text & "%"),
                New SqlParameter("@fce", "%" & txtFCENumberFilter.Text & "%"),
                New SqlParameter("@fac", "%" & txtFacilityNameFilter.Text & "%")
            }

            dgvWork.DataSource = DB.GetDataTable(SQL, p)

            dgvWork.Columns("Received Date").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("Inspection Date").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("FCE Date").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("Enf Discovery Date").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("Date Completed").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("Last Modified").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvWork.Columns("Flag").Visible = False
            dgvWork.SanelyResizeColumns

            txtWorkCount.Text = dgvWork.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

#Region " Add/Delete events "

    Private Sub AddNewEvent()
        Try
            If rdbEnforcementAction.Checked Then
                OpenFormEnforcement(New Apb.ApbFacilityId(txtNewAIRSNumber.Text))

            ElseIf rdbFCE.Checked Then
                OpenFormFce(New Apb.ApbFacilityId(txtNewAIRSNumber.Text))

            ElseIf rdbPerformanceTest.Checked Then
                OpenFormTestReportEntry(txtTrackingNumber.Text)

            ElseIf rdbOther.Checked Then
                If cboEvent.SelectedIndex > -1 Then
                    Dim SQL As String = "SELECT NEXT VALUE FOR SSCPTrackingNumber"
                    Dim trk As Integer = DB.GetInteger(SQL)

                    Dim sqlList As New List(Of String)
                    Dim paramList As New List(Of SqlParameter())

                    sqlList.Add("Insert into SSCPItemMaster " &
                                "(strTrackingNumber, strAIRSNumber, " &
                                "datReceivedDate, strEventType, " &
                                "strResponsibleStaff, strModifingPerson, " &
                                "datModifingDate) " &
                                "values " &
                                "(@trk, @airs, " &
                                " @daterec, @event, " &
                                " @user, @user, " &
                                "GETDATE() )")

                    Dim p As SqlParameter() = {
                        New SqlParameter("@trk", trk),
                        New SqlParameter("@airs", "0413" & txtNewAIRSNumber.Text),
                        New SqlParameter("@daterec", DTPDateReceived.Value),
                        New SqlParameter("@event", cboEvent.SelectedValue),
                        New SqlParameter("@user", CurrentUser.UserID)
                    }

                    paramList.Add(p)

                    If cboEvent.SelectedValue = "04" Then
                        sqlList.Add("Insert into SSCPItemMaster " &
                                    "(strTrackingNumber, strAIRSNumber, " &
                                    "datReceivedDate, strEventType, " &
                                    "strResponsibleStaff, datCompleteDate, " &
                                    "strModifingPerson, datModifingDate) " &
                                    "values " &
                                    "( NEXT VALUE FOR SSCPTrackingNumber, @airs, " &
                                    " @daterec, '06', " &
                                    " @user, @daterec, " &
                                    " @user, GETDATE())")

                        paramList.Add(p)
                    End If

                    DB.RunCommand(sqlList, paramList)

                    txtTrackingNumber.Text = trk

                    OpenFormSscpWorkItem(trk)
                Else
                    MsgBox("Select a work type and event type if needed.", MsgBoxStyle.Information, "Work Entry")
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DeleteWork()
        Dim response As DialogResult
        Dim SQL As String
        Dim p As New SqlParameter("@num", txtWorkNumber.Text)

        Select Case txtTestType.Text
            Case "Annual Compliance Certification"
                response = MessageBox.Show("Are you sure you want to delete this ACC?",
                                           "Delete ACC", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If response = DialogResult.Yes Then
                    SQL = "Update SSCPItemMaster set strDelete = 'True' where strTrackingNumber = @num"
                    DB.RunCommand(SQL, p)

                    LoadDgvWork()
                    MessageBox.Show("Done")
                End If

            Case "Enforcement-" To "Enforcement-z"
                MessageBox.Show("Enforcement cases must be deleted from within the enforcement screen.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            Case "Full Compliance Evaluation"
                response = MessageBox.Show("Are you sure you want to delete this FCE?",
                                           "Delete FCE", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If response = DialogResult.Yes Then
                    SQL = "update SSCPFCEMASTER set IsDeleted = 1 where STRFCENUMBER = @num"
                    DB.RunCommand(SQL, p)

                    LoadDgvWork()
                    MessageBox.Show("Done")
                End If

            Case "Inspection", "Report", "Notification" To "Notification-z"
                response = MessageBox.Show("Are you sure you want to delete this item?",
                                           "Delete Work Entry", MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If response = DialogResult.Yes Then
                    SQL = "Update SSCPItemMaster set strDelete = 'True' where strTrackingNumber = @num"
                    DB.RunCommand(SQL, p)

                    LoadDgvWork()
                    MessageBox.Show("Done")
                End If

            Case "Performance Tests"
                MessageBox.Show("Performance tests must be deleted by ISMP.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End Select

    End Sub

    Private Sub UnDeleteWork()
        Dim p As New SqlParameter("@num", txtWorkNumber.Text)

        Select Case txtTestType.Text
            Case "Annual Compliance Certification", "Report", "Inspection", "Notification" To "Notification-z"

                Dim SQL As String = "Update SSCPItemMaster set strDelete = NULL where strTrackingNumber = @num"
                DB.RunCommand(SQL, p)
                LoadDgvWork()
                MessageBox.Show("Done")

            Case "Enforcement-" To "Enforcement-z"
                MessageBox.Show("Deleted enforcement cases cannot be restored.", "Can't Undelete", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            Case "Full Compliance Evaluation"
                Dim SQL As String = "update SSCPFCEMASTER set IsDeleted = 0 where STRFCENUMBER = @num"
                DB.RunCommand(SQL, p)
                LoadDgvWork()
                MessageBox.Show("Done")

            Case "Performance Tests"
                MessageBox.Show("Deleted performance tests cannot be restored.", "Can't Undelete", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End Select

    End Sub

#End Region

#Region " Search options, etc. "

    Private Sub cboEvent_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboEvent.SelectedValueChanged
        Dim dr As DataRow = CType(cboEvent.DataSource, DataTable).Rows.Find(cboEvent.SelectedValue)

        If dr IsNot Nothing Then
            LabelEventDescription.Text = dr("strActivityDescription").ToString
        End If

        If cboEvent.Text = "Inspection" Then
            lblDateField.Text = "Inspection Date:"
        Else
            lblDateField.Text = "Received by GEPD:"
        End If

        LabelEventDescription.Visible = Not String.IsNullOrEmpty(LabelEventDescription.Text)
    End Sub

    Private Sub chbEnforcementDates_CheckedChanged(sender As Object, e As EventArgs) Handles chbFilterDates.CheckedChanged
        DTPFilterStart.Enabled = chbFilterDates.Checked
        DTPFilterEnd.Enabled = chbFilterDates.Checked
    End Sub

    Private Sub btnRunFilter_Click(sender As Object, e As EventArgs) Handles btnRunFilter.Click
        LoadDgvWork()
    End Sub

    Private Sub dgvWork_SelectionChanged(sender As Object, e As EventArgs) Handles dgvWork.SelectionChanged
        If dgvWork.SelectedRows.Count = 1 Then
            txtAIRSNumber.Text = dgvWork.CurrentRow.Cells(0).Value.ToString
            txtNewAIRSNumber.Text = dgvWork.CurrentRow.Cells(0).Value.ToString
            txtFacilityName.Text = dgvWork.CurrentRow.Cells(1).Value.ToString
            txtWorkNumber.Text = dgvWork.CurrentRow.Cells(5).Value.ToString
            txtTestType.Text = dgvWork.CurrentRow.Cells(3).Value.ToString
        End If
    End Sub

    Private Sub txtAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtAIRSNumber.TextChanged
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtAIRSNumber.Text) Then
            Dim SQL As String = "Select strFacilityCity from APBFacilityInformation " &
                "where strAIRSnumber = @airs "
            Dim p As New SqlParameter("@airs", "0413" & txtAIRSNumber.Text)
            txtFacilityCity.Text = DB.GetString(SQL, p)
        End If
    End Sub

    Private Sub btnSelectWork_Click(sender As Object, e As EventArgs) Handles btnSelectWork.Click
        Dim workNumber As Integer
        If txtTestType.Text <> "" AndAlso Integer.TryParse(txtWorkNumber.Text, workNumber) Then
            If InStr(txtTestType.Text, "Enforcement") > 0 Then
                OpenFormEnforcement(workNumber)
            ElseIf InStr(txtTestType.Text, "Full Compliance Evaluation") > 0 Then
                OpenFormFce(workNumber)
            Else
                OpenFormSscpWorkItem(workNumber)
            End If
        End If
    End Sub

    Private Sub txtNewAIRSNumber_TextChanged(sender As Object, e As EventArgs) Handles txtNewAIRSNumber.TextChanged
        txtFacilityInformation.Text = ""
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) Then
            Dim fac As Apb.Facilities.Facility = DAL.FacilityData.GetFacility(txtNewAIRSNumber.Text)
            If fac IsNot Nothing Then txtFacilityInformation.Text = fac.LongDisplay
        End If
    End Sub

    Private Sub rdbPerformanceTest_CheckedChanged(sender As Object, e As EventArgs) Handles rdbPerformanceTest.CheckedChanged
        If rdbPerformanceTest.Checked Then
            pnlOtherEvents.Visible = True
            lblDateField.Visible = False
            DTPDateReceived.Visible = False
            Label8.Visible = False
            cboEvent.Visible = False
            LabelEventDescription.Visible = False
            lblOtherNumber.Text = "ISMP Reference Number"
            txtTrackingNumber.ReadOnly = False
            txtTrackingNumber.Clear()
        Else
            pnlOtherEvents.Visible = False
            lblDateField.Visible = True
            DTPDateReceived.Visible = True
            Label8.Visible = True
            cboEvent.Visible = True
            LabelEventDescription.Visible = True
            lblOtherNumber.Text = "Tracking Number"
            txtTrackingNumber.ReadOnly = True
            txtTrackingNumber.Clear()
        End If
    End Sub

    Private Sub rdbOther_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOther.CheckedChanged
        pnlOtherEvents.Visible = rdbOther.Checked
    End Sub

    Private Sub btnAddNewEnTry_Click(sender As Object, e As EventArgs) Handles btnAddNewEntry.Click
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(txtNewAIRSNumber.Text) _
            AndAlso DAL.AirsNumberExists(txtNewAIRSNumber.Text) Then
            AddNewEvent()
        Else
            MsgBox("Invalid AIRS Number.", MsgBoxStyle.Information, "Work Entry")
        End If
    End Sub

    Private Sub OpenFacilityLookupTool()
        Using facilityLookupDialog As New IAIPFacilityLookUpTool
            facilityLookupDialog.ShowDialog()
            If facilityLookupDialog.DialogResult = DialogResult.OK AndAlso facilityLookupDialog.SelectedAirsNumber <> "" Then
                txtAIRSNumberFilter.Text = facilityLookupDialog.SelectedAirsNumber
            End If
        End Using
    End Sub

    Private Sub btnDeleteWork_Click(sender As Object, e As EventArgs) Handles btnDeleteWork.Click
        DeleteWork()
    End Sub

    Private Sub btnUndeleteWork_Click(sender As Object, e As EventArgs) Handles btnUndeleteWork.Click
        UnDeleteWork()
    End Sub

    Private Sub btnOpenSummary_Click(sender As Object, e As EventArgs) Handles btnOpenSummary.Click
        OpenFormFacilitySummary(txtAIRSNumber.Text)
    End Sub

    Private Sub chbNotifications_CheckedChanged(sender As Object, e As EventArgs) Handles chbNotifications.CheckedChanged
        GBNotifications.Enabled = chbNotifications.Checked
    End Sub

    Private Sub chbAllWork_CheckedChanged(sender As Object, e As EventArgs) Handles chbAllWork.CheckedChanged
        If chbAllWork.Checked Then
            chbACCs.Enabled = False
            chbEnforcement.Enabled = False
            chbFCE.Enabled = False
            chbInspections.Enabled = False
            chbPerformanceTests.Enabled = False
            chbReports.Enabled = False
            chbRMPInspections.Enabled = False
            chbNotifications.Enabled = False
        Else
            chbACCs.Enabled = True
            chbEnforcement.Enabled = True
            chbFCE.Enabled = True
            chbInspections.Enabled = True
            chbPerformanceTests.Enabled = True
            chbReports.Enabled = True
            chbRMPInspections.Enabled = True
            chbNotifications.Enabled = True
        End If
    End Sub

    Private Sub rdbIgnoreEngineer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbIgnoreEngineer.CheckedChanged
        If rdbIgnoreEngineer.Checked Then
            chbEngineer.Enabled = False
            clbEngineer.Enabled = False
            clbDistrictOffices.Enabled = False
            clbAirBranchUnits.Enabled = False
        End If
    End Sub

    Private Sub rdbUseEngineer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUseEngineer.CheckedChanged
        If rdbUseEngineer.Checked Then
            chbEngineer.Enabled = True
            clbEngineer.Enabled = True
            clbDistrictOffices.Enabled = False
            clbAirBranchUnits.Enabled = False
        End If
    End Sub

    Private Sub rdbUseUnits_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUseUnits.CheckedChanged
        If rdbUseUnits.Checked Then
            chbEngineer.Enabled = False
            clbEngineer.Enabled = False
            clbDistrictOffices.Enabled = True
            clbAirBranchUnits.Enabled = True
        End If
    End Sub

    Private Sub chbEngineer_CheckedChanged(sender As Object, e As EventArgs) Handles chbEngineer.CheckedChanged
        clbEngineer.Enabled = Not chbEngineer.Checked
    End Sub

#End Region

#Region " Toolbar and Menu "

    Private Sub FacilitySearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FacilitySearchToolStripMenuItem.Click
        OpenFacilityLookupTool()
    End Sub

    Private Sub ClearFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearFormToolStripMenuItem.Click
        LoadDefaultSettings()
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        dgvWork.ExportToExcel()
    End Sub

#End Region

#Region " Accept Button "

    Private Sub pnlFilterPanel_Enter(sender As Object, e As EventArgs) Handles pnlFilterPanel.Enter
        Me.AcceptButton = btnRunFilter
    End Sub

    Private Sub btnRunFilter_Leave(sender As Object, e As EventArgs) Handles btnRunFilter.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class