Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class PASPFeeManagement

    Private Sub PASPFeeManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadFeeRates()
            LoadNSPSExemptions()
            LoadNSPSExemptions2("1")
            LoadFeeYears()
            LoadSelectedNSPSExemptions()

            FormatWebUsers()

            btnGenerateMailoutList.Enabled = False
            btnFirstEnrollment.Enabled = False
            btnUnenrollFeeYear.Enabled = False
            btnUpdateContactData.Enabled = False
            btnSetMailoutDate.Enabled = False
            dtpDateMailoutSent.Enabled = False

            FeeManagementListCountLabel.Text = ""
            btnExportToExcel.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFeeRates()
        Try
            Dim SQL As String

            SQL = "Select " &
            "numFeeRateID, " &
            "numFeeYear, " &
            "datFeePeriodStart, datFeePeriodEnd, " &
            "numPart70Fee, numSMFee, " &
            "numPerTonRate, numNSPSFee, " &
            "datFeeDueDate, " &
            "numAdminFeeRate, datAdminApplicable, " &
            "datFirstQrtDue, datSecondQrtDue, " &
            "datThirdQrtDue, datFourthQrtDue, " &
            "strComments, " &
            "numAAThres, numNAThres " &
            "from FS_FeeRate " &
            "where Active = '1' " &
            "order by numFeeYear desc "

            dgvFeeRates.DataSource = DB.GetDataTable(SQL)

            dgvFeeRates.RowHeadersVisible = False
            dgvFeeRates.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeRates.AllowUserToResizeColumns = True
            dgvFeeRates.AllowUserToAddRows = False
            dgvFeeRates.AllowUserToDeleteRows = False
            dgvFeeRates.AllowUserToOrderColumns = True
            dgvFeeRates.AllowUserToResizeRows = True

            dgvFeeRates.Columns("numFeeRateID").HeaderText = "ID"
            dgvFeeRates.Columns("numFeeRateID").DisplayIndex = 0
            dgvFeeRates.Columns("numFeeRateID").Visible = False
            dgvFeeRates.Columns("numFeeYear").HeaderText = "Fee Year"
            dgvFeeRates.Columns("numFeeYear").DisplayIndex = 1
            dgvFeeRates.Columns("numFeeYear").Width = 40
            dgvFeeRates.Columns("datFeePeriodStart").HeaderText = "Start Date"
            dgvFeeRates.Columns("datFeePeriodStart").DisplayIndex = 2
            dgvFeeRates.Columns("datFeePeriodStart").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datFeePeriodStart").Width = 75
            dgvFeeRates.Columns("datFeePeriodEnd").HeaderText = "End Date"
            dgvFeeRates.Columns("datFeePeriodEnd").DisplayIndex = 3
            dgvFeeRates.Columns("datFeePeriodEnd").Width = 75
            dgvFeeRates.Columns("datFeePeriodEnd").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvFeeRates.Columns("numPart70Fee").DisplayIndex = 4
            dgvFeeRates.Columns("numPart70Fee").Width = 75
            dgvFeeRates.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numSMFee").HeaderText = "SM Fee"
            dgvFeeRates.Columns("numSMFee").DisplayIndex = 5
            dgvFeeRates.Columns("numSMFee").Width = 75
            dgvFeeRates.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numPerTonRate").HeaderText = "Per Ton Rate"
            dgvFeeRates.Columns("numPerTonRate").DisplayIndex = 6
            dgvFeeRates.Columns("numPerTonRate").Width = 75
            dgvFeeRates.Columns("numPerTonRate").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvFeeRates.Columns("numNSPSFee").DisplayIndex = 7
            dgvFeeRates.Columns("numNSPSFee").Width = 75
            dgvFeeRates.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvFeeRates.Columns("numAAThres").HeaderText = "Attainment Threshold"
            dgvFeeRates.Columns("numAAThres").DisplayIndex = 8
            dgvFeeRates.Columns("numAAThres").Width = 75
            dgvFeeRates.Columns("numNAThres").HeaderText = "NonAttainment Threshold"
            dgvFeeRates.Columns("numNAThres").DisplayIndex = 9
            dgvFeeRates.Columns("numNAThres").Width = 75

            dgvFeeRates.Columns("datFeeDueDate").HeaderText = "Due Date"
            dgvFeeRates.Columns("datFeeDueDate").DisplayIndex = 10
            dgvFeeRates.Columns("datFeeDueDate").Width = 75
            dgvFeeRates.Columns("datFeeDueDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("numAdminFeeRate").HeaderText = "Admin Fee"
            dgvFeeRates.Columns("numAdminFeeRate").DisplayIndex = 11
            dgvFeeRates.Columns("numAdminFeeRate").Width = 75
            dgvFeeRates.Columns("datAdminApplicable").HeaderText = "Admin. Fee Applicable"
            dgvFeeRates.Columns("datAdminApplicable").DisplayIndex = 12
            dgvFeeRates.Columns("datAdminApplicable").Width = 75
            dgvFeeRates.Columns("datAdminApplicable").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeRates.Columns("datFirstQrtDue").HeaderText = "1st Qrt Due Date"
            dgvFeeRates.Columns("datFirstQrtDue").DisplayIndex = 13
            dgvFeeRates.Columns("datFirstQrtDue").Width = 125
            dgvFeeRates.Columns("datFirstQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datSecondQrtDue").HeaderText = "2nd Qrt Due Date"
            dgvFeeRates.Columns("datSecondQrtDue").DisplayIndex = 14
            dgvFeeRates.Columns("datSecondQrtDue").Width = 125
            dgvFeeRates.Columns("datSecondQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datThirdQrtDue").HeaderText = "3rd Qrt Due Date"
            dgvFeeRates.Columns("datThirdQrtDue").DisplayIndex = 15
            dgvFeeRates.Columns("datThirdQrtDue").Width = 125
            dgvFeeRates.Columns("datThirdQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeRates.Columns("datFourthQrtDue").HeaderText = "4th Qrt Due Date"
            dgvFeeRates.Columns("datFourthQrtDue").DisplayIndex = 16
            dgvFeeRates.Columns("datFourthQrtDue").Width = 125
            dgvFeeRates.Columns("datFourthQrtDue").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeRates.Columns("strComments").HeaderText = "Notes"
            dgvFeeRates.Columns("strComments").DisplayIndex = 17
            dgvFeeRates.Columns("strComments").Width = 200

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ClearFeeData()
        txtFeeID.Clear()
        txtFeeYear.Clear()
        dtpFeePeriodStart.Value = Today
        dtpFeePeriodEnd.Value = Today
        txtTitleVfee.Clear()
        txtAnnualSMFee.Clear()
        txtAnnualNSPSFee.Clear()
        txtperTonRate.Clear()
        dtpFeeDueDate.Value = Today
        txtAdminFeePercent.Clear()
        dtpAdminApplicable.Value = Today
        txtFeeNotes.Clear()
    End Sub

    Private Sub LoadNSPSExemptions()
        Try
            Dim SQL As String

            SQL = "Select " &
            "NSPSReasonCode, Description, " &
            "(strLastName+', '+strFirstName) as UpdatingUser, " &
            "UpdateDateTime, CreateDateTime, " &
            "case " &
            "when Active = '0' then 'Flagged as deleted' " &
            "else 'Active' " &
            "end ActiveStatus " &
            "from FSLK_NSPSReason inner join EPDUserProfiles " &
            "on FSLK_NSPSReason.UpdateUser = EPDUserProfiles.numUserID " &
            "where Active = '1' " &
            "order by NSPSReasonCode "

            dgvNSPSExemptions.DataSource = DB.GetDataTable(SQL)

            dgvNSPSExemptions.RowHeadersVisible = False
            dgvNSPSExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptions.AllowUserToResizeColumns = True
            dgvNSPSExemptions.AllowUserToResizeRows = True
            dgvNSPSExemptions.AllowUserToAddRows = False
            dgvNSPSExemptions.AllowUserToDeleteRows = False
            dgvNSPSExemptions.AllowUserToOrderColumns = True
            dgvNSPSExemptions.Columns("NSPSReasonCode").HeaderText = "ID"
            dgvNSPSExemptions.Columns("NSPSReasonCode").DisplayIndex = 0
            dgvNSPSExemptions.Columns("Description").HeaderText = "NSPS Exemption Reason"
            dgvNSPSExemptions.Columns("Description").DisplayIndex = 1
            dgvNSPSExemptions.Columns("UpdatingUser").HeaderText = "Updating User"
            dgvNSPSExemptions.Columns("UpdatingUser").DisplayIndex = 2
            dgvNSPSExemptions.Columns("UpdateDateTime").HeaderText = "Updated Date"
            dgvNSPSExemptions.Columns("UpdateDateTime").DisplayIndex = 3
            dgvNSPSExemptions.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvNSPSExemptions.Columns("CreateDateTime").HeaderText = "Created Date"
            dgvNSPSExemptions.Columns("CreateDateTime").DisplayIndex = 4
            dgvNSPSExemptions.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvNSPSExemptions.Columns("ActiveStatus").HeaderText = "Active Status"
            dgvNSPSExemptions.Columns("ActiveStatus").DisplayIndex = 5

            dgvNSPSExemptions.AutoResizeColumns()
            dgvNSPSExemptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadNSPSExemptions2(ActiveStatus As String)
        Try
            Dim SQL As String

            SQL = "Select " &
            "NSPSReasonCode, Description, " &
            "(strLastName+', '+strFirstName) as UpdatingUser, " &
            "UpdateDateTime, CreateDateTime, " &
            "case " &
            "when Active = '0' then 'Flagged as deleted' " &
            "else 'Active' " &
            "end ActiveStatus " &
            "from FSLK_NSPSReason inner join EPDUserProfiles " &
            "on FSLK_NSPSReason.UpdateUser = EPDUserProfiles.numUserID " &
            "where Active = @Active " &
            "order by NSPSReasonCode "

            Dim p As New SqlParameter("@Active", ActiveStatus)

            dgvExistingExemptions.DataSource = DB.GetDataTable(SQL, p)

            dgvExistingExemptions.RowHeadersVisible = False
            dgvExistingExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvExistingExemptions.AllowUserToResizeColumns = True
            dgvExistingExemptions.AllowUserToResizeRows = True
            dgvExistingExemptions.AllowUserToAddRows = False
            dgvExistingExemptions.AllowUserToDeleteRows = False
            dgvExistingExemptions.AllowUserToOrderColumns = True
            dgvExistingExemptions.Columns("NSPSReasonCode").HeaderText = "ID"
            dgvExistingExemptions.Columns("NSPSReasonCode").DisplayIndex = 0
            dgvExistingExemptions.Columns("Description").HeaderText = "NSPS Exemption Reason"
            dgvExistingExemptions.Columns("Description").DisplayIndex = 1
            dgvExistingExemptions.Columns("UpdatingUser").HeaderText = "Updating User"
            dgvExistingExemptions.Columns("UpdatingUser").DisplayIndex = 2
            dgvExistingExemptions.Columns("UpdateDateTime").HeaderText = "Updated Date"
            dgvExistingExemptions.Columns("UpdateDateTime").DisplayIndex = 3
            dgvExistingExemptions.Columns("UpdateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvExistingExemptions.Columns("CreateDateTime").HeaderText = "Created Date"
            dgvExistingExemptions.Columns("CreateDateTime").DisplayIndex = 4
            dgvExistingExemptions.Columns("CreateDateTime").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvExistingExemptions.Columns("ActiveStatus").HeaderText = "Active Status"
            dgvExistingExemptions.Columns("ActiveStatus").DisplayIndex = 5

            dgvExistingExemptions.AutoResizeColumns()
            dgvExistingExemptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFeeYears()
        Dim allFeeYears As List(Of String) = DAL.GetAllFeeYears().AddBlankRowToList()
        cboNSPSExemptionYear.DataSource = allFeeYears
        cboAvailableFeeYears.DataSource = allFeeYears
    End Sub

    Private Sub LoadSelectedNSPSExemptions()
        Try
            dgvNSPSExemptionsByYear.RowHeadersVisible = False
            dgvNSPSExemptionsByYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptionsByYear.AllowUserToResizeColumns = True
            dgvNSPSExemptionsByYear.AllowUserToAddRows = False
            dgvNSPSExemptionsByYear.AllowUserToDeleteRows = False
            dgvNSPSExemptionsByYear.AllowUserToOrderColumns = True
            dgvNSPSExemptionsByYear.AllowUserToResizeRows = True
            dgvNSPSExemptionsByYear.ColumnHeadersHeight = "35"

            dgvNSPSExemptionsByYear.Columns.Add("numFeeYear", "Year")
            dgvNSPSExemptionsByYear.Columns("numFeeYear").DisplayIndex = 0
            dgvNSPSExemptionsByYear.Columns("numFeeYear").Width = (dgvNSPSExemptionsByYear.Width * 0.1)
            dgvNSPSExemptionsByYear.Columns("numFeeYear").Visible = True

            dgvNSPSExemptionsByYear.Columns.Add("NSPSReasonCode", "NSPS ID")
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").DisplayIndex = 1
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").Width = (dgvNSPSExemptionsByYear.Width * 0.15)
            dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").ReadOnly = True

            dgvNSPSExemptionsByYear.Columns.Add("displayOrder", "Display Order")
            dgvNSPSExemptionsByYear.Columns("displayOrder").DisplayIndex = 2
            dgvNSPSExemptionsByYear.Columns("displayOrder").Width = (dgvNSPSExemptionsByYear.Width * 0.15)
            dgvNSPSExemptionsByYear.Columns("displayOrder").ReadOnly = False

            dgvNSPSExemptionsByYear.Columns.Add("Description", "NSPS Exemption Reason")
            dgvNSPSExemptionsByYear.Columns("Description").DisplayIndex = 3
            dgvNSPSExemptionsByYear.Columns("Description").Width = (dgvNSPSExemptionsByYear.Width * 0.6)
            dgvNSPSExemptionsByYear.Columns("Description").ReadOnly = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearFeeData_Click(sender As Object, e As EventArgs) Handles btnClearFeeData.Click
        ClearFeeData()
    End Sub

    Private Sub dgvFeeRates_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvFeeRates.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvFeeRates.HitTest(e.X, e.Y)

            If dgvFeeRates.RowCount > 0 And hti.RowIndex <> -1 Then

                ClearFeeData()
                If IsDBNull(dgvFeeRates(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtFeeID.Text = dgvFeeRates(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(1, hti.RowIndex).Value) Then
                    txtFeeYear.Clear()
                Else
                    txtFeeYear.Text = dgvFeeRates(1, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(2, hti.RowIndex).Value) Then
                    dtpFeePeriodStart.Value = Today
                Else
                    dtpFeePeriodStart.Text = dgvFeeRates(2, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(3, hti.RowIndex).Value) Then
                    dtpFeePeriodEnd.Value = Today
                Else
                    dtpFeePeriodEnd.Text = dgvFeeRates(3, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(4, hti.RowIndex).Value) Then
                    txtTitleVfee.Clear()
                Else
                    txtTitleVfee.Text = dgvFeeRates(4, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(5, hti.RowIndex).Value) Then
                    txtAnnualSMFee.Clear()
                Else
                    txtAnnualSMFee.Text = dgvFeeRates(5, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(7, hti.RowIndex).Value) Then
                    txtAnnualNSPSFee.Clear()
                Else
                    txtAnnualNSPSFee.Text = dgvFeeRates(7, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(6, hti.RowIndex).Value) Then
                    txtperTonRate.Clear()
                Else
                    txtperTonRate.Text = dgvFeeRates(6, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(16, hti.RowIndex).Value) Then
                    txtAttainmentThreshold.Clear()
                Else
                    txtAttainmentThreshold.Text = dgvFeeRates(16, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(17, hti.RowIndex).Value) Then
                    txtNonAttainmentThreshold.Clear()
                Else
                    txtNonAttainmentThreshold.Text = dgvFeeRates(17, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(8, hti.RowIndex).Value) Then
                    dtpFeeDueDate.Value = Today
                Else
                    dtpFeeDueDate.Text = dgvFeeRates(8, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(9, hti.RowIndex).Value) Then
                    txtAdminFeePercent.Clear()
                Else
                    txtAdminFeePercent.Text = dgvFeeRates(9, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(10, hti.RowIndex).Value) Then
                    dtpAdminApplicable.Value = Today
                Else
                    dtpAdminApplicable.Text = dgvFeeRates(10, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(11, hti.RowIndex).Value) Then
                    dtpFirstQrtDue.Value = Today
                Else
                    dtpFirstQrtDue.Text = dgvFeeRates(11, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(12, hti.RowIndex).Value) Then
                    dtpSecondQrtDue.Value = Today
                Else
                    dtpSecondQrtDue.Text = dgvFeeRates(12, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(13, hti.RowIndex).Value) Then
                    dtpThirdQrtDue.Value = Today
                Else
                    dtpThirdQrtDue.Text = dgvFeeRates(13, hti.RowIndex).Value
                End If
                If IsDBNull(dgvFeeRates(14, hti.RowIndex).Value) Then
                    dtpFourthQrtDue.Value = Today
                Else
                    dtpFourthQrtDue.Text = dgvFeeRates(14, hti.RowIndex).Value
                End If

                If IsDBNull(dgvFeeRates(15, hti.RowIndex).Value) Then
                    txtFeeNotes.Clear()
                Else
                    txtFeeNotes.Text = dgvFeeRates(15, hti.RowIndex).Value
                End If

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateFeeData_Click(sender As Object, e As EventArgs) Handles btnUpdateFeeData.Click
        Try
            If txtFeeID.Text <> "" Then
                If Update_FS_FeeRate(txtFeeID.Text, txtFeeYear.Text, dtpFeePeriodStart.Text, dtpFeePeriodEnd.Text,
                                         txtTitleVfee.Text, txtAnnualSMFee.Text, txtperTonRate.Text, txtAnnualNSPSFee.Text,
                                         dtpFeeDueDate.Text, txtAdminFeePercent.Text, dtpAdminApplicable.Text,
                                         txtFeeNotes.Text, "1", dtpFirstQrtDue.Text, dtpSecondQrtDue.Text,
                                         dtpThirdQrtDue.Text, dtpFourthQrtDue.Text, txtAttainmentThreshold.Text,
                                         txtNonAttainmentThreshold.Text) = True Then

                    LoadFeeRates()
                    ClearFeeData()
                    MsgBox("Update completed", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Did not update", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadNSPSExemptionByYear()
        Try
            Dim NSPStemp As String = ""
            Dim ReasonID As String = ""
            Dim DisplayOrder As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 1
            Dim SQL As String

            SQL = "Select " &
            "NSPSReasonCode, DisplayOrder " &
            "from FSLK_NSPSReasonYear " &
            "where numFeeYear = @year " &
            " and active = '1' " &
            "order by NSPSReasonCode "
            Dim p As New SqlParameter("@year", cboNSPSExemptionYear.Text)

            Dim dt As DataTable = DB.GetDataTable(SQL, p)
            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("NSPSReasonCode")) Then
                Else
                    NSPStemp = NSPStemp & dr.Item("NSPSReasonCode")
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        NSPStemp = NSPStemp & "-" & i & ","
                        i += 1
                    Else
                        NSPStemp = NSPStemp & "-" & dr.Item("DisplayOrder") & ","
                        If dr.Item("DisplayOrder") >= i Then
                            i = dr.Item("DisplayOrder") + 1
                        End If
                    End If
                End If
            Next

            dgvNSPSExemptionsByYear.Rows.Clear()

            Do While NSPStemp <> ""
                ReasonID = Mid(NSPStemp, 1, InStr(NSPStemp, "-", CompareMethod.Text) - 1)

                If ReasonID.Length = 1 Then
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 3)
                Else
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 4)
                End If

                Dim rgxPattern As String = "\b" & ReasonID & "-" & DisplayOrder & ","
                NSPStemp = System.Text.RegularExpressions.Regex.Replace(NSPStemp, rgxPattern, "")

                Dim x As Integer = 0
                While x < dgvNSPSExemptions.Rows.Count
                    Dim y As Integer = 0
                    While y < dgvNSPSExemptions.Rows(x).Cells.Count
                        Dim c As DataGridViewCell = dgvNSPSExemptions.Rows(x).Cells(y)
                        If Not c.Value Is DBNull.Value Or Nothing Then
                            If CType(c.Value, String) = ReasonID Then
                                dgvRow = New DataGridViewRow
                                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, x).Value
                                dgvRow.Cells(2).Value = DisplayOrder
                                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, x).Value
                                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                            End If
                        End If
                        Math.Min(Threading.Interlocked.Increment(y), y - 1)
                    End While
                    Math.Min(Threading.Interlocked.Increment(x), x - 1)
                End While
            Loop

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectForm_Click(sender As Object, e As EventArgs) Handles btnSelectForm.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvNSPSExemptionsByYear.Rows.Count

            If i > 0 Then
                temp = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                For i = 0 To dgvNSPSExemptionsByYear.Rows.Count - 1
                    If dgvNSPSExemptionsByYear(1, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                    dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                    dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                    dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                End If
            Else
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewNSPSExemptionsByYear_Click(sender As Object, e As EventArgs) Handles btnViewNSPSExemptionsByYear.Click
        If cboNSPSExemptionYear.Text <> "" Then
            LoadNSPSExemptionByYear()
        End If
    End Sub

    Private Sub btnSelectAllForms_Click(sender As Object, e As EventArgs) Handles btnSelectAllForms.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            dgvNSPSExemptionsByYear.Rows.Clear()

            For i = 0 To dgvNSPSExemptions.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, i).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, i).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectForm_Click(sender As Object, e As EventArgs) Handles btnUnselectForm.Click
        Try
            If dgvNSPSExemptionsByYear.RowCount > 0 Then
                Dim ReasonID As String = dgvNSPSExemptionsByYear(1, dgvNSPSExemptionsByYear.CurrentRow.Index).Value

                Dim SQL As String = "SELECT COUNT(*) " &
                    "FROM FSCALCULATIONS " &
                    "WHERE INTYEAR = @year " &
                    "AND (STRNSPSREASON LIKE @reason1 " &
                    "OR STRNSPSREASON = @reason2 " &
                    "OR STRNSPSREASON LIKE @reason3 " &
                    "OR STRNSPSREASON LIKE @reason4) "
                Dim p As SqlParameter() = {
                    New SqlParameter("@year", dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value),
                    New SqlParameter("@reason1", ReasonID & ",%"),
                    New SqlParameter("@reason2", ReasonID),
                    New SqlParameter("@reason3", "%," & ReasonID),
                    New SqlParameter("@reason4", "%," & ReasonID & ",%")
                }

                If DB.GetInteger(SQL, p) > 0 Then
                    MessageBox.Show("Unable to Remove this exemption from this year because this exemption has been used.")
                Else
                    dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectAllForms_Click(sender As Object, e As EventArgs) Handles btnUnselectAllForms.Click
        Try
            Dim ReasonID As String = ""
            Dim i As Integer = 0
            Dim SQL As String = "SELECT COUNT(*) " &
                "FROM FSCALCULATIONS " &
                "WHERE INTYEAR = @year " &
                "AND (STRNSPSREASON LIKE @reason1 " &
                "OR STRNSPSREASON = @reason2 " &
                "OR STRNSPSREASON LIKE @reason3 " &
                "OR STRNSPSREASON LIKE @reason4) "

            If dgvNSPSExemptionsByYear.RowCount > 0 Then

                i = 0
                While i < dgvNSPSExemptionsByYear.RowCount

                    dgvNSPSExemptionsByYear.Rows(i).Selected = True
                    ReasonID = dgvNSPSExemptionsByYear(1, i).Value

                    Dim p As SqlParameter() = {
                        New SqlParameter("@year", dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value),
                        New SqlParameter("@reason1", ReasonID & ",%"),
                        New SqlParameter("@reason2", ReasonID),
                        New SqlParameter("@reason3", "%," & ReasonID),
                        New SqlParameter("@reason4", "%," & ReasonID & ",%")
                    }

                    If DB.GetInteger(SQL, p) > 0 Then
                        i += 1
                    Else
                        dgvNSPSExemptionsByYear.Rows(i).Selected = True
                        dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                        dgvNSPSExemptionsByYear.Rows(i).Selected = False
                    End If
                End While

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateNSPSbyYear_Click(sender As Object, e As EventArgs) Handles btnUpdateNSPSbyYear.Click
        If cboNSPSExemptionYear.Text = "" OrElse Not IsNumeric(cboNSPSExemptionYear.Text) Then
            MessageBox.Show("Please select a Fee Year first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Dim x As Integer = 0
            Dim ReasonID As String
            Dim Order As String
            Dim temp As String = ""
            Dim ExistingID As String = ""
            Dim SQL As String

            SQL = "Select " &
            "NSPSReasonCode " &
            "from FSLK_NSPSReasonYear " &
            "where numFeeYear = @year "
            Dim p As New SqlParameter("@year", cboNSPSExemptionYear.Text)

            Dim dt As DataTable = DB.GetDataTable(SQL, p)
            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("NSPSReasonCode")) Then
                Else
                    ExistingID = ExistingID & "(" & dr.Item("NSPSReasonCode") & ")"
                End If
            Next

            While x < dgvNSPSExemptionsByYear.Rows.Count
                ReasonID = dgvNSPSExemptionsByYear(1, x).Value
                Order = dgvNSPSExemptionsByYear(2, x).Value
                x += 1

                SQL = "Select " &
                "DisplayOrder " &
                "from FSLK_NSPSReasonYear " &
                "where numFeeYear = @year " &
                "and NSPSReasonCode = @reasoncode "
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@year", cboNSPSExemptionYear.Text),
                    New SqlParameter("@reasoncode", ReasonID)
                }
                Dim dr As DataRow = DB.GetDataRow(SQL, p2)
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        temp = "NULL"
                    Else
                        temp = dr.Item("DisplayOrder")
                    End If
                End If

                If temp <> Order Then
                    Select Case temp
                        Case ""
                            If Insert_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order) = True Then

                            End If

                        Case "NULL"
                            If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order, "1") = True Then

                            End If

                        Case Else
                            If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, Order, "1") = True Then

                            End If
                    End Select
                End If
                ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
            End While

            If ExistingID <> "" Then
                Do While ExistingID <> ""
                    ReasonID = Mid(ExistingID, InStr(ExistingID, "(", CompareMethod.Text) + 1, InStr(ExistingID, ")", CompareMethod.Text) - 2)
                    ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
                    If Update_FSLK_NSPSReasonYear(cboNSPSExemptionYear.Text, ReasonID, "0", "0") = True Then

                    End If
                Loop
            End If
            LoadFeeYears()
            MessageBox.Show("Update Complete", Me.Text, MessageBoxButtons.OK)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddNSPSExemption_Click(sender As Object, e As EventArgs) Handles btnAddNSPSExemption.Click
        Try
            If Insert_FSLK_NSPSReason(txtNSPSExemption.Text) = True Then
                txtNSPSExemption.Clear()
                LoadNSPSExemptions()
                MsgBox("Save completed", MsgBoxStyle.Information, Me.Text)

                Dim maxRow As Integer = dgvNSPSExemptions.RowCount - 1
                If dgvNSPSExemptions.Rows.Count >= maxRow AndAlso maxRow >= 1 Then
                    dgvNSPSExemptions.FirstDisplayedScrollingRowIndex = maxRow
                    dgvNSPSExemptions.Rows(maxRow).Selected = True
                End If
                LoadNSPSExemptions()
                LoadNSPSExemptions2("1")
            Else
                MsgBox("Did not Save", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteNSPSExemption_Click(sender As Object, e As EventArgs) Handles btnDeleteNSPSExemption.Click
        Try
            If txtDeleteNSPSExemptions.Text <> "" Then
                If Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, "", "0") = True Then
                    txtDeleteNSPSExemptions.Clear()
                    txtNSPSExemption.Clear()
                    LoadNSPSExemptions()
                    LoadNSPSExemptions2("1")
                    MsgBox("Exemption Deleted", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewDeletedNSPS_Click(sender As Object, e As EventArgs) Handles btnViewDeletedNSPS.Click
        LoadNSPSExemptions2("0")
    End Sub

    Private Sub btnUpdateNSPSExemption_Click(sender As Object, e As EventArgs) Handles btnUpdateNSPSExemption.Click
        Try
            If txtDeleteNSPSExemptions.Text <> "" Then
                If Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, txtNSPSExemption.Text, "1") = True Then
                    txtDeleteNSPSExemptions.Clear()
                    txtNSPSExemption.Clear()
                    LoadNSPSExemptions()
                    LoadNSPSExemptions2("1")

                    MsgBox("Exemption Updated", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnReloadFeeRate_Click(sender As Object, e As EventArgs) Handles btnReloadFeeRate.Click
        LoadFeeRates()
    End Sub

    Private Sub btnViewEnrolledFacilities_Click(sender As Object, e As EventArgs) Handles btnViewEnrolledFacilities.Click
        ViewEnrolledFacilities()
    End Sub

    Private Sub btnFirstEnrollment_Click(sender As Object, e As EventArgs) Handles btnFirstEnrollment.Click
        Try
            Dim EnrollCheck As String = ""
            Dim SQL As String

            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Select " &
            "count(*) as EnrollCheck " &
            "from FS_Admin " &
            "where numFeeYear = @year " &
            "and strEnrolled = '1' " &
            "and ACTIVE = '1' "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("EnrollCheck")) Then
                    EnrollCheck = "0"
                Else
                    EnrollCheck = dr.Item("EnrollCheck")
                End If
            End If
            If EnrollCheck > 0 Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "There are already facilities enrolled for this fee year.",
                        MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "Update FS_Admin set " &
            "strEnrolled = '1', " &
            "datEnrollment = getdate(), " &
            "updateUser = @user , " &
            "UpdateDateTime = getdate(), " &
             "numCurrentStatus = 3 " &
            "where numFeeYear = @year " &
            "and ACTIVE = '1' "
            Dim p2 As SqlParameter() = {
                New SqlParameter("@user", "IAIP||" & CurrentUser.AlphaName),
                New SqlParameter("@year", cboAvailableFeeYears.Text)
            }
            DB.RunCommand(SQL, p2)

            SQL = "Update FS_Admin set " &
            "datInitialEnrollment = datEnrollment " &
            "where numFeeYear = @year " &
            "and datInitialEnrollment is null " &
            "and ACTIVE = '1' "
            DB.RunCommand(SQL, p)

            Dim p3 As SqlParameter() = {
                    New SqlParameter("@FeeYear", SqlDbType.Decimal) With {.Value = cboAvailableFeeYears.Text},
                    New SqlParameter("@AIRSNumber", "")
                }
            DB.SPRunCommand("PD_FEE_DATA", p3)

            ViewEnrolledFacilities()

            MsgBox("Facilities Enrolled for the selected fee year.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnenrollFeeYear_Click(sender As Object, e As EventArgs) Handles btnUnenrollFeeYear.Click
        Try
            Dim SQL As String

            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If cboAvailableFeeYears.Text < (Today.Year - 1) Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Only Current and last Fee Years are elegible to be unenrolled.",
                        MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you positive you want to reset enrollment for this year?",
              Me.Text, MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case DialogResult.Yes

                Case Else
                    Exit Sub
            End Select

            SQL = "Update FS_Admin set " &
            "strEnrolled = '0', " &
            "datEnrollment = '', " &
            "datInitialEnrollment = '', " &
            "updateUser = @user, " &
            "UpdateDateTime = getdate() " &
            "where numFeeYear = @year " &
            "and ACTIVE = '1' "
            Dim p2 As SqlParameter() = {
                New SqlParameter("@user", "IAIP||" & CurrentUser.AlphaName),
                New SqlParameter("@year", cboAvailableFeeYears.Text)
            }
            DB.RunCommand(SQL, p2)

            ViewEnrolledFacilities()

            MsgBox("Facilities Unenrolled.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewEnrolledFacilities()
        Try
            Dim SQL As String = "Select " &
                "substring(fS_Admin.strAIRSnumber, 5, 8) as AIRSNumber,  " &
                "strFacilityname,  " &
                "strFacilityAddress1, strFacilityCity,  " &
                "strFacilityZipCode,  " &
                "strFirstName, strLastName, " &
                "strContactCoName, strContactAddress1,  " &
                "strContactCity, strcontactState,  " &
                "strContactZipCode, strGECOUserEmail,  " &
                "case " &
                "when strOperationalStatus = '1' then 'Yes' " &
                "when strOperationalStatus = '0' then 'No' " &
                "else 'No' " &
                "end strOperationalStatus, " &
                "strClass,  " &
                "case " &
                "when strNSPS = '1' then 'Yes' " &
                "when strNSPS = '0' then 'No' " &
                "else 'No' " &
                "end strNSPS, " &
                "case " &
                "when strPart70 = '1' then 'Yes' " &
                "when strPart70 = '0' then 'No' " &
                "else 'No' " &
                "end strPart70, " &
                "datShutdowndate  " &
                "From FS_Admin inner join FS_MailOut  " &
                "on FS_Admin.strAIRSnumber = FS_MailOut.strAIRSnumber  " &
                "and FS_Admin.numFeeYear = FS_MailOut.numFeeYear  " &
                "where FS_Admin.numFeeYear = @year " &
                "and (strEnrolled = '1')  " &
                "AND FS_Admin.Active = '1' "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)

            dgvFeeManagementLists.DataSource = DB.GetDataTable(SQL, p)

            dgvFeeManagementLists.RowHeadersVisible = False
            dgvFeeManagementLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagementLists.AllowUserToResizeColumns = True
            dgvFeeManagementLists.AllowUserToAddRows = False
            dgvFeeManagementLists.AllowUserToDeleteRows = False
            dgvFeeManagementLists.AllowUserToOrderColumns = True
            dgvFeeManagementLists.AllowUserToResizeRows = True

            dgvFeeManagementLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagementLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagementLists.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeManagementLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeManagementLists.Columns("STRCLASS").HeaderText = "Class"
            dgvFeeManagementLists.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeManagementLists.Columns("strNSPS").HeaderText = "NSPS"
            dgvFeeManagementLists.Columns("STRNSPS").DisplayIndex = 4
            dgvFeeManagementLists.Columns("STRPART70").HeaderText = "TV Source"
            dgvFeeManagementLists.Columns("STRPART70").DisplayIndex = 5
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
            dgvFeeManagementLists.Columns("strFirstName").HeaderText = "Contact First Name"
            dgvFeeManagementLists.Columns("strFirstName").DisplayIndex = 7
            dgvFeeManagementLists.Columns("STRlASTNAME").HeaderText = "Contact Last Name"
            dgvFeeManagementLists.Columns("STRLASTNAME").DisplayIndex = 8
            dgvFeeManagementLists.Columns("strContactCoName").HeaderText = "Contact Company"
            dgvFeeManagementLists.Columns("strContactCoName").DisplayIndex = 9
            dgvFeeManagementLists.Columns("strContactAddress1").HeaderText = "Address"
            dgvFeeManagementLists.Columns("strContactAddress1").DisplayIndex = 10
            dgvFeeManagementLists.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeManagementLists.Columns("STRCONTACTCITY").DisplayIndex = 11
            dgvFeeManagementLists.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeManagementLists.Columns("STRCONTACTSTATE").DisplayIndex = 12
            dgvFeeManagementLists.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeManagementLists.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
            dgvFeeManagementLists.Columns("strFacilityAddress1").HeaderText = "Facility Street"
            dgvFeeManagementLists.Columns("strFacilityAddress1").DisplayIndex = 14
            dgvFeeManagementLists.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeManagementLists.Columns("STRFACILITYCITY").DisplayIndex = 15
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode"
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
            dgvFeeManagementLists.Columns("strGECOUserEmail").HeaderText = "Contact Email"
            dgvFeeManagementLists.Columns("strGECOUserEmail").DisplayIndex = 17

            FeeManagementListCountLabel.Text = String.Format("Viewing {0} Fee Year: {1} result{2}", cboAvailableFeeYears.Text, dgvFeeManagementLists.RowCount.ToString, If(dgvFeeManagementLists.RowCount = 1, "", "s"))
            If dgvFeeManagementLists.RowCount > 0 Then
                btnExportToExcel.Visible = True
            Else
                btnExportToExcel.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewMailout_Click(sender As Object, e As EventArgs) Handles btnViewMailout.Click
        ViewMailOut()
    End Sub

    Private Sub btnGenerateMailoutList_Click(sender As Object, e As EventArgs) Handles btnGenerateMailoutList.Click
        Try
            Dim temp As String = ""
            Dim SQL As String

            If cboAvailableFeeYears.Text = "" OrElse Not IsNumeric(cboAvailableFeeYears.Text) Then
                MsgBox("Select a valid fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            SQL = "select count(*) as ContactTotals " &
                "from FS_MailOut " &
                "where numfeeyear = @year "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("ContactTotals")) Then
                    temp = "0"
                Else
                    temp = dr.Item("ContactTotals")
                End If
            End If

            If temp < 1 Then
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@FeeYear", SqlDbType.Decimal) With {.Value = cboAvailableFeeYears.Text},
                    New SqlParameter("@AIRSNumber", "")
                }
                DB.SPRunCommand("PD_FEE_MAILOUT", p2)
                DB.SPRunCommand("PD_FEE_DATA", p2)

                SQL = "Update FS_Admin set " &
                    "numCurrentStatus = 2, " &
                    "strInitialMailout = '1'  " &
                    "where numFeeYear = @year " &
                    "and strInitialMailout ='0' " &
                    "and strMailoutSent <> '0' " &
                    "and numCurrentStatus < 5 "
                DB.RunCommand(SQL, p)
            End If

            ViewMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewMailOut()
        Try
            Dim SQL As String = "Select " &
                "substring(fS_Admin.strAIRSnumber, 5, 8) as AIRSNumber,  " &
                "strFacilityname,  " &
                "strFacilityAddress1, strFacilityCity,  " &
                "strFacilityZipCode,  " &
                "strFirstName, strLastName, " &
                "strContactCoName, strContactAddress1,  " &
                "strContactCity, strcontactState,  " &
                "strContactZipCode, strGECOUserEmail,  " &
                "case " &
                "when strOperationalStatus = '1' then 'Yes' " &
                "when strOperationalStatus = 'X' then 'No' " &
                "else 'Yes' " &
                "end strOperationalStatus, " &
                "strClass, " &
                "case " &
                "when strNSPS = '1' then 'Yes' " &
                "when strNSPS = '0' then 'No' " &
                "else 'No' " &
                "end strNSPS, " &
                "case " &
                "when strPart70 = '1' then 'Yes'" &
                "when strPart70 = '0' then 'No' " &
                "else 'No' " &
                "end strPart70, " &
                "datShutdowndate  " &
                "From FS_Admin inner join FS_MailOut " &
                "on FS_Admin.strAIRSnumber = FS_MailOut.strAIRSnumber " &
                "and FS_Admin.numFeeYear = FS_MailOut.numFeeYear  " &
                "where FS_Admin.numFeeYear = @year " &
                "AND FS_Admin.Active = '1' "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)

            dgvFeeManagementLists.DataSource = DB.GetDataTable(SQL, p)

            dgvFeeManagementLists.RowHeadersVisible = False
            dgvFeeManagementLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagementLists.AllowUserToResizeColumns = True
            dgvFeeManagementLists.AllowUserToAddRows = False
            dgvFeeManagementLists.AllowUserToDeleteRows = False
            dgvFeeManagementLists.AllowUserToOrderColumns = True
            dgvFeeManagementLists.AllowUserToResizeRows = True

            dgvFeeManagementLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagementLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagementLists.Columns("strFacilityName").HeaderText = "Facility Name - Snapshot"
            dgvFeeManagementLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status - Snapshot"
            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeManagementLists.Columns("STRCLASS").HeaderText = "Class - Snapshot"
            dgvFeeManagementLists.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeManagementLists.Columns("strNSPS").HeaderText = "NSPS - Snapshot"
            dgvFeeManagementLists.Columns("STRNSPS").DisplayIndex = 4
            dgvFeeManagementLists.Columns("STRPART70").HeaderText = "TV Source - Snapshot"
            dgvFeeManagementLists.Columns("STRPART70").DisplayIndex = 5
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date - Snapshot"
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
            dgvFeeManagementLists.Columns("strFirstName").HeaderText = "Contact First Name "
            dgvFeeManagementLists.Columns("strFirstName").DisplayIndex = 7
            dgvFeeManagementLists.Columns("STRlASTNAME").HeaderText = "Contact Last Name"
            dgvFeeManagementLists.Columns("STRLASTNAME").DisplayIndex = 8
            dgvFeeManagementLists.Columns("strContactCoName").HeaderText = "Contact Company "
            dgvFeeManagementLists.Columns("strContactCoName").DisplayIndex = 9
            dgvFeeManagementLists.Columns("strContactAddress1").HeaderText = "Address"
            dgvFeeManagementLists.Columns("strContactAddress1").DisplayIndex = 10
            dgvFeeManagementLists.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeManagementLists.Columns("STRCONTACTCITY").DisplayIndex = 11
            dgvFeeManagementLists.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeManagementLists.Columns("STRCONTACTSTATE").DisplayIndex = 12
            dgvFeeManagementLists.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeManagementLists.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
            dgvFeeManagementLists.Columns("strFacilityAddress1").HeaderText = "Facility Street"
            dgvFeeManagementLists.Columns("strFacilityAddress1").DisplayIndex = 14
            dgvFeeManagementLists.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeManagementLists.Columns("STRFACILITYCITY").DisplayIndex = 15
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode"
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
            dgvFeeManagementLists.Columns("strGECOUserEmail").HeaderText = "Contact Email"
            dgvFeeManagementLists.Columns("strGECOUserEmail").DisplayIndex = 17

            FeeManagementListCountLabel.Text = String.Format("Viewing {0} Fee Year: {1} result{2}", cboAvailableFeeYears.Text, dgvFeeManagementLists.RowCount.ToString, If(dgvFeeManagementLists.RowCount = 1, "", "s"))
            If dgvFeeManagementLists.RowCount > 0 Then
                btnExportToExcel.Visible = True
            Else
                btnExportToExcel.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateContactData_Click(sender As Object, e As EventArgs) Handles btnUpdateContactData.Click
        ' Warn user
        Dim confirm As DialogResult = MessageBox.Show("This will replace mailout contact data with the current " & vbNewLine &
            "fee contact for all sources in the mailout list. " &
            vbNewLine & vbNewLine &
            "Are you sure you want to proceed?",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        If confirm = DialogResult.No Then
            Exit Sub
        End If

        Try
            Dim AIRSNumber As String = ""
            Dim ContactFirstName As String = ""
            Dim ContactLastName As String = ""
            Dim ContactPrefix As String = ""
            Dim ContactSuffix As String = ""
            Dim ContactCompanyName As String = ""
            Dim ContactAddress1 As String = ""
            Dim ContactAddress2 As String = ""
            Dim ContactCity As String = ""
            Dim ContactState As String = ""
            Dim ContactZipCode As String = ""

            Dim SQL As String = "Select " &
                "strAIRSNumber " &
                "from FS_Admin " &
                "where numFeeYear = @year "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("strAIRSNumber")
                End If
                If AIRSNumber <> "" Then
                    SQL = "Select * " &
                        "from APBContactInformation " &
                        "where strAIRSNumber = @airs " &
                        "and strKey = '40' "
                    Dim p2 As New SqlParameter("@airs", AIRSNumber)
                    Dim dr2 As DataRow = DB.GetDataRow(SQL, p2)

                    If dr2 IsNot Nothing Then
                        If IsDBNull(dr2.Item("strContactFirstname")) Then
                            ContactFirstName = ""
                        Else
                            ContactFirstName = dr2.Item("strContactFirstName")
                        End If
                        If IsDBNull(dr2.Item("strContactLastname")) Then
                            ContactLastName = ""
                        Else
                            ContactLastName = dr2.Item("strContactLastName")
                        End If
                        If IsDBNull(dr2.Item("strContactprefix")) Then
                            ContactPrefix = ""
                        Else
                            ContactPrefix = dr2.Item("strContactPrefix")
                        End If
                        If IsDBNull(dr2.Item("strContactSuffix")) Then
                            ContactSuffix = ""
                        Else
                            ContactSuffix = dr2.Item("strContactSuffix")
                        End If
                        If IsDBNull(dr2.Item("strContactCompanyName")) Then
                            ContactCompanyName = ""
                        Else
                            ContactCompanyName = dr2.Item("strContactCompanyName")
                        End If
                        If IsDBNull(dr2.Item("strContactAddress1")) Then
                            ContactAddress1 = ""
                        Else
                            ContactAddress1 = dr2.Item("strContactAddress1")
                        End If
                        If IsDBNull(dr2.Item("strContactAddress2")) Then
                            ContactAddress2 = ""
                        Else
                            ContactAddress2 = dr2.Item("strContactAddress2")
                        End If
                        If IsDBNull(dr2.Item("strContactcity")) Then
                            ContactCity = ""
                        Else
                            ContactCity = dr2.Item("strcontactCity")
                        End If
                        If IsDBNull(dr2.Item("strContactState")) Then
                            ContactState = ""
                        Else
                            ContactState = dr2.Item("strContactState")
                        End If
                        If IsDBNull(dr2.Item("strContactZipCode")) Then
                            ContactZipCode = ""
                        Else
                            ContactZipCode = dr2.Item("strContactZipCode")
                        End If
                    End If

                    SQL = "Update FS_MailOut set " &
                        "strFirstName = @ContactFirstName, " &
                        "strLastName = @ContactLastName, " &
                        "strPrefix = @ContactPrefix,  " &
                        "strTitle = @ContactSuffix, " &
                        "strContactCoName = @ContactCompanyName, " &
                        "strContactAddress1 = @ContactAddress1, " &
                        "strContactAddress2 = @ContactAddress2, " &
                        "strContactCity = @ContactCity, " &
                        "strContactState = @ContactState, " &
                        "strcontactZipCode = @ContactZipCode " &
                        "where strAIRSNumber = @AIRSNumber " &
                        "and numFeeYear = @AvailableFeeYears "

                    Dim parameters As SqlParameter()
                    parameters = New SqlParameter() {
                        New SqlParameter("@ContactFirstName", ContactFirstName),
                        New SqlParameter("@ContactLastName", ContactLastName),
                        New SqlParameter("@ContactPrefix", ContactPrefix),
                        New SqlParameter("@ContactSuffix", ContactSuffix),
                        New SqlParameter("@ContactCompanyName", ContactCompanyName),
                        New SqlParameter("@ContactAddress1", ContactAddress1),
                        New SqlParameter("@ContactAddress2", ContactAddress2),
                        New SqlParameter("@ContactCity", ContactCity),
                        New SqlParameter("@ContactState", ContactState),
                        New SqlParameter("@ContactZipCode", ContactZipCode),
                        New SqlParameter("@AIRSNumber", AIRSNumber),
                        New SqlParameter("@AvailableFeeYears", cboAvailableFeeYears.Text)
                    }
                    DB.RunCommand(SQL, parameters)

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvFeeManagementLists.ExportToExcel(Me)
    End Sub

    Private Sub dgvExistingExemptions_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvExistingExemptions.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvExistingExemptions.HitTest(e.X, e.Y)

            If dgvExistingExemptions.RowCount > 0 And hti.RowIndex <> -1 Then

                txtDeleteNSPSExemptions.Clear()
                txtNSPSExemption.Clear()
                If IsDBNull(dgvExistingExemptions(0, hti.RowIndex).Value) Then
                    Exit Sub
                Else
                    txtDeleteNSPSExemptions.Text = dgvExistingExemptions(0, hti.RowIndex).Value
                End If
                If IsDBNull(dgvExistingExemptions(1, hti.RowIndex).Value) Then
                    txtNSPSExemption.Clear()
                Else
                    txtNSPSExemption.Text = dgvExistingExemptions(1, hti.RowIndex).Value
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRefreshNSPSExemptions_Click(sender As Object, e As EventArgs) Handles btnRefreshNSPSExemptions.Click
        Try
            LoadNSPSExemptions2("1")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearNSPSExemptions_Click(sender As Object, e As EventArgs) Handles btnClearNSPSExemptions.Click
        txtDeleteNSPSExemptions.Clear()
        txtNSPSExemption.Clear()
    End Sub

    Private Sub btnViewFacilitiesSubjectToFees_Click(sender As Object, e As EventArgs) Handles btnViewFacilitiesSubjectToFees.Click
        Try
            Dim SQL As String = "Select " &
                "substring(fS_Admin.strAIRSnumber, 5, 8) as AIRSNumber,  " &
                "strFacilityname,  " &
                "strFacilityStreet1, strFacilityCity,  " &
                "strFacilityZipCode,  " &
                "strOperationalStatus, strClass,  " &
                "case " &
                "when substring(strAIRProgramCodes, 8, 1) = '1' then 'Yes' " &
                "else 'No' " &
                "end strNSPS, " &
                "case " &
                "when substring(strAIRProgramCodes, 13, 1) = '1' then 'Yes' " &
                "else 'No' " &
                "end strPArt70, " &
                "case " &
                "when strOperationalStatus = 'X' then datShutDownDate " &
                "end datShutdowndate,  " &
                "case " &
                "when strEnrolled = '1' then 'Yes' " &
                "else 'No' " &
                "end strEnrolled, " &
                "case " &
                "when strInitialMailout = '1' then 'Yes' " &
                "else 'No' " &
                "end strInitialMailout, " &
                "case " &
                "when strMailoutSent = '1' then 'Yes' " &
                "else 'No' " &
                "end strMailoutSent " &
                "From FS_Admin inner join APBFacilityInformation " &
                "on FS_Admin.strAIRSnumber = APBFacilityInformation.strAIRSnumber " &
                "inner join APBHeaderData " &
                "on FS_admin.strAIRSNumber = APBheaderData.strAIRSNumber " &
                "where FS_Admin.numFeeYear = @year " &
                "AND FS_Admin.Active = '1' "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)

            dgvFeeManagementLists.DataSource = DB.GetDataTable(SQL, p)

            dgvFeeManagementLists.RowHeadersVisible = False
            dgvFeeManagementLists.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeManagementLists.AllowUserToResizeColumns = True
            dgvFeeManagementLists.AllowUserToAddRows = False
            dgvFeeManagementLists.AllowUserToDeleteRows = False
            dgvFeeManagementLists.AllowUserToOrderColumns = True
            dgvFeeManagementLists.AllowUserToResizeRows = True

            dgvFeeManagementLists.Columns("AIRSNumber").HeaderText = "Airs No."
            dgvFeeManagementLists.Columns("AIRSNumber").DisplayIndex = 0
            dgvFeeManagementLists.Columns("strFacilityName").HeaderText = "Facility Name - Current Data"
            dgvFeeManagementLists.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeManagementLists.Columns("strFacilityStreet1").HeaderText = "Facility Street - Current Data"
            dgvFeeManagementLists.Columns("strFacilityStreet1").DisplayIndex = 2
            dgvFeeManagementLists.Columns("STRFACILITYCITY").HeaderText = "Facility City - Current Data"
            dgvFeeManagementLists.Columns("STRFACILITYCITY").DisplayIndex = 3
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode - Current Data"
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").DisplayIndex = 4

            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status - Current Data"
            dgvFeeManagementLists.Columns("STROPERATIONALSTATUS").DisplayIndex = 5
            dgvFeeManagementLists.Columns("STRCLASS").HeaderText = "Class - Current Data"
            dgvFeeManagementLists.Columns("STRCLASS").DisplayIndex = 6
            dgvFeeManagementLists.Columns("strNSPS").HeaderText = "NSPS - Current Data"
            dgvFeeManagementLists.Columns("STRNSPS").DisplayIndex = 7
            dgvFeeManagementLists.Columns("STRPART70").HeaderText = "TV Source - Current Data"
            dgvFeeManagementLists.Columns("STRPART70").DisplayIndex = 8
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date - Current Data"
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").DisplayIndex = 9
            dgvFeeManagementLists.Columns("DATSHUTDOWNDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"

            dgvFeeManagementLists.Columns("strEnrolled").HeaderText = "Enrolled Facility"
            dgvFeeManagementLists.Columns("strEnrolled").DisplayIndex = 10
            dgvFeeManagementLists.Columns("strInitialMailout").HeaderText = "In Initial Mailout"
            dgvFeeManagementLists.Columns("strInitialMailout").DisplayIndex = 11
            dgvFeeManagementLists.Columns("strMailoutSent").HeaderText = "In Mailout"
            dgvFeeManagementLists.Columns("strMailoutSent").DisplayIndex = 12

            FeeManagementListCountLabel.Text = String.Format("Viewing {0} Fee Year: {1} result{2}", cboAvailableFeeYears.Text, dgvFeeManagementLists.RowCount.ToString, If(dgvFeeManagementLists.RowCount = 1, "", "s"))
            If dgvFeeManagementLists.RowCount > 0 Then
                btnExportToExcel.Visible = True
            Else
                btnExportToExcel.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSetMailoutDate_Click(sender As Object, e As EventArgs) Handles btnSetMailoutDate.Click
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to set the initial mailout date for all sources in the mailout list?",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        If confirm = DialogResult.No Then
            Exit Sub
        End If

        Try
            Dim SQL As String = "Update FS_Admin set " &
            " datMailoutSent = @date, " &
            " numcurrentstatus = 4, " &
            " STRINITIALMAILOUT = '1' , " &
            " strMailOutSent = '1' " &
            " where numFeeYear = @year " &
            " and datMailoutSent is null " &
            " and strEnrolled = '1' " &
            " and active = '1' "
            Dim p As SqlParameter() = {
                New SqlParameter("@date", dtpDateMailoutSent.Value),
                New SqlParameter("@year", cboAvailableFeeYears.Text)
            }
            DB.RunCommand(SQL, p)

            MsgBox("Mailout Sent date set.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatWebUsers()
        Try
            dgvUsers.RowHeadersVisible = False
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUsers.AllowUserToResizeColumns = True
            dgvUsers.AllowUserToAddRows = False
            dgvUsers.AllowUserToDeleteRows = False
            dgvUsers.AllowUserToOrderColumns = True
            dgvUsers.AllowUserToResizeRows = True
            dgvUsers.ColumnHeadersHeight = "35"

            dgvUsers.Columns.Add("ID", "ID")
            dgvUsers.Columns("ID").DisplayIndex = 0
            dgvUsers.Columns("ID").Visible = False

            dgvUsers.Columns.Add("numuserid", "UserID")
            dgvUsers.Columns("numuserid").DisplayIndex = 1
            dgvUsers.Columns("numuserid").Visible = False

            dgvUsers.Columns.Add("Email", "Email Address")
            dgvUsers.Columns("Email").DisplayIndex = 2
            dgvUsers.Columns("Email").Width = 250
            dgvUsers.Columns("Email").ReadOnly = True

            Dim colReadOnly As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colReadOnly)
            dgvUsers.Columns(3).HeaderText = "Admin Access"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colWrite)
            dgvUsers.Columns(4).HeaderText = "Fee Access"

            Dim colFullAccess As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colFullAccess)
            dgvUsers.Columns(5).HeaderText = "EI Access"

            Dim colSpecialPermissions As New DataGridViewCheckBoxColumn
            dgvUsers.Columns.Add(colSpecialPermissions)
            dgvUsers.Columns(6).HeaderText = "ES Access"


            dgvUserFacilities.RowHeadersVisible = False
            dgvUserFacilities.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUserFacilities.AllowUserToResizeColumns = True
            dgvUserFacilities.AllowUserToAddRows = False
            dgvUserFacilities.AllowUserToDeleteRows = False
            dgvUserFacilities.AllowUserToOrderColumns = True
            dgvUserFacilities.AllowUserToResizeRows = True
            dgvUserFacilities.ColumnHeadersHeight = "35"

            dgvUserFacilities.Columns.Add("strAIRSNumber", "AIRS Number")
            dgvUserFacilities.Columns("strAIRSNumber").DisplayIndex = 0
            dgvUserFacilities.Columns("strAIRSNumber").Visible = True

            dgvUserFacilities.Columns.Add("strFacilityName", "Facility Name")
            dgvUserFacilities.Columns("strFacilityName").DisplayIndex = 1
            dgvUserFacilities.Columns("strFacilityName").Width = 250

            Dim colReadOnly2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colReadOnly2)
            dgvUserFacilities.Columns(2).HeaderText = "Admin Access"

            Dim colWrite2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colWrite2)
            dgvUserFacilities.Columns(3).HeaderText = "Fee Access"

            Dim colFullAccess2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colFullAccess2)
            dgvUserFacilities.Columns(4).HeaderText = "EI Access"

            Dim colSpecialPermissions2 As New DataGridViewCheckBoxColumn
            dgvUserFacilities.Columns.Add(colSpecialPermissions2)
            dgvUserFacilities.Columns(5).HeaderText = "ES Access"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub llbViewUserData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        ViewFacilitySpecificUsers()
    End Sub

    Private Sub ViewFacilitySpecificUsers()
        Try
            Dim dgvRow As New DataGridViewRow
            Dim SQL As String

            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "Error")
            Else
                txtEmail.Clear()

                SQL = "Select strFacilityName " &
                "from APBFacilityInformation " &
                "where strAIRSNumber = @airs "
                Dim p As New SqlParameter("@airs", "0413" & mtbAIRSNumber.Text)
                Dim dr2 As DataRow = DB.GetDataRow(SQL, p)

                If dr2 IsNot Nothing Then
                    If IsDBNull(dr2.Item("strFacilityName")) Then
                        lblFaciltyName.Text = " - "
                    Else
                        lblFaciltyName.Text = Apb.Facilities.Facility.SanitizeFacilityNameForDb(dr2.Item("strFacilityName"))
                    End If
                End If

                SQL = "SELECT " &
                "OlapUserAccess.NumUserID as ID, OlapUserLogin.numuserid, " &
                "OlapUserLogin.strUserEmail as Email, " &
                "Case " &
                "When intAdminAccess = 0 Then 'False' " &
                "When intAdminAccess = 1 Then 'True' " &
                "End as intAdminAccess, " &
                "Case " &
                "When intFeeAccess = 0 Then 'False' " &
                "When intFeeAccess = 1 Then 'True' " &
                "End as intFeeAccess, " &
                "Case " &
                "When intEIAccess = 0 Then 'False' " &
                "When intEIAccess = 1 Then 'True' " &
                "End as intEIAccess, " &
                "Case " &
                "When intESAccess = 0 Then 'False' " &
                "When intESAccess = 1 Then 'True' " &
                "End as intESAccess " &
                "FROM OlapUserAccess inner join OlapUserLogin " &
                "on OLAPUserAccess.NumUserId = OlapUserLogin.NumUserID " &
                "where OlapUserAccess.strAirsNumber = @airs order by email"

                dgvUsers.Rows.Clear()
                Dim dt As DataTable = DB.GetDataTable(SQL, p)
                For Each dr As DataRow In dt.Rows
                    dgvRow = New DataGridViewRow
                    dgvRow.CreateCells(dgvUsers)
                    If IsDBNull(dr.Item("ID")) Then
                        dgvRow.Cells(0).Value = ""
                    Else
                        dgvRow.Cells(0).Value = dr.Item("ID")
                    End If
                    If IsDBNull(dr.Item("numuserid")) Then
                        dgvRow.Cells(1).Value = ""
                    Else
                        dgvRow.Cells(1).Value = dr.Item("numuserid")
                    End If
                    If IsDBNull(dr.Item("Email")) Then
                        dgvRow.Cells(2).Value = ""
                    Else
                        dgvRow.Cells(2).Value = dr.Item("Email")
                    End If
                    If IsDBNull(dr.Item("intAdminAccess")) Then
                        dgvRow.Cells(3).Value = False
                    Else
                        dgvRow.Cells(3).Value = dr.Item("intAdminAccess")
                    End If
                    If IsDBNull(dr.Item("intFeeAccess")) Then
                        dgvRow.Cells(4).Value = False
                    Else
                        dgvRow.Cells(4).Value = dr.Item("intFeeAccess")
                    End If

                    If IsDBNull(dr.Item("intEIAccess")) Then
                        dgvRow.Cells(5).Value = False
                    Else
                        dgvRow.Cells(5).Value = dr.Item("intEIAccess")
                    End If
                    If IsDBNull(dr.Item("intESAccess")) Then
                        dgvRow.Cells(6).Value = False
                    Else
                        dgvRow.Cells(6).Value = dr.Item("intESAccess")
                    End If
                    dgvUsers.Rows.Add(dgvRow)
                Next

                cboUsers.DataSource = dt
                cboUsers.DisplayMember = "Email"
                cboUsers.ValueMember = "ID"
                cboUsers.Text = ""
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer
            Dim SQL As String = "Select numUserId " &
            "from olapuserlogin " &
            "where struseremail = @email "
            Dim p As New SqlParameter("@email", txtEmail.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            If dr IsNot Nothing Then 'Email address is registered
                userID = dr.Item("numUserId")

                Dim InsertString As String = "Insert into OlapUserAccess " &
                "(numUserId, strAirsNumber, strFacilityName) values " &
                "(@numUserId, @strAirsNumber, @strFacilityName) "

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@numUserId", userID),
                    New SqlParameter("@strAirsNumber", "0413" & mtbAIRSNumber.Text),
                    New SqlParameter("@strFacilityName", lblFaciltyName.Text)
                }
                DB.RunCommand(InsertString, p2)

                ViewFacilitySpecificUsers()

                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim SQL As String = "DELETE OlapUserAccess " &
            "WHERE numUserID = @user " &
            "and strAirsNumber = @airs "
            Dim p As SqlParameter() = {
                New SqlParameter("@user", cboUsers.SelectedValue),
                New SqlParameter("@airs", "0413" & mtbAIRSNumber.Text)
            }
            DB.RunCommand(SQL, p)

            ViewFacilitySpecificUsers()
            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String
            Dim SQL As String

            For i As Integer = 0 To dgvUsers.Rows.Count - 1
                If dgvUsers(3, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUsers(4, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUsers(5, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUsers(6, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE OlapUserAccess " &
                    "SET intadminaccess = @intadminaccess, " &
                    "intFeeAccess = @intFeeAccess, " &
                    "intEIAccess = @intEIAccess, " &
                    "intESAccess = @intESAccess " &
                    "WHERE numUserID = @numUserID " &
                    "and strAirsNumber = @strAirsNumber "
                Dim p As SqlParameter() = {
                    New SqlParameter("@intadminaccess", adminaccess),
                    New SqlParameter("@intFeeAccess", feeaccess),
                    New SqlParameter("@intEIAccess", eiaccess),
                    New SqlParameter("@intESAccess", esaccess),
                    New SqlParameter("@numUserID", dgvUsers(1, i).Value),
                    New SqlParameter("@strAirsNumber", "0413" & mtbAIRSNumber.Text)
                }
                DB.RunCommand(SQL, p)
            Next

            ViewFacilitySpecificUsers()
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub lblViewEmailData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        LoadUserInfo(txtWebUserEmail.Text)
        LoadUserFacilityInfo(txtWebUserEmail.Text)
    End Sub

    Private Sub LoadUserInfo(UserData As String)
        Try
            Dim SQL As String = "Select " &
            "OLAPUserProfile.numUserID, " &
            "strfirstname, strlastname, " &
            "strtitle, strcompanyname, " &
            "straddress, strcity, " &
            "strstate, strzip, " &
            "strphonenumber, strfaxnumber, " &
            "datLastLogIn, strConfirm, " &
            "strUserEmail " &
            "from OlapUserProfile inner join OLAPUserLogIn " &
            "on OLAPUserProfile.numUserID = OLAPUserLogIn.numuserid " &
            "where strUserEmail = @email "
            Dim p As New SqlParameter("@email", UserData)

            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            If dr IsNot Nothing Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    txtEditFirstName.Clear()
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                    txtEditFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtEditLastName.Clear()
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                    txtEditLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    txtEditTitle.Clear()
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                    txtEditTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    txtEditCompany.Clear()
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                    txtEditCompany.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    txtEditAddress.Clear()
                Else
                    lblAddress.Text = dr.Item("straddress")
                    txtEditAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    txtEditCity.Clear()
                    mtbEditState.Clear()
                    mtbEditZipCode.Clear()
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                    txtEditCity.Text = dr.Item("strCity")
                    mtbEditState.Text = dr.Item("strState")
                    mtbEditZipCode.Text = dr.Item("strZip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    mtbEditPhoneNumber.Clear()
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                    mtbEditPhoneNumber.Text = dr.Item("strPhoneNumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    mtbEditFaxNumber.Clear()
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                    mtbEditFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strConfirm")) Then
                    lblConfirmDate.Text = ""
                Else
                    lblConfirmDate.Text = "Date User Email Confirmed: " & dr.Item("strConfirm")
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date User Last Logged In: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("strUserEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("strUserEmail")
                End If
            Else
                txtWebUserID.Clear()
                txtEditFirstName.Clear()
                txtEditLastName.Clear()
                txtEditTitle.Clear()
                txtEditCompany.Clear()
                txtEditAddress.Clear()
                txtEditCity.Clear()
                mtbEditState.Clear()
                mtbEditZipCode.Clear()
                mtbEditPhoneNumber.Clear()
                mtbEditFaxNumber.Clear()
                lblLastLogIn.Text = ""
                lblConfirmDate.Text = ""
                txtEditEmail.Clear()
            End If

            txtEditUserPassword.Clear()
            txtEditFirstName.Visible = False
            txtEditLastName.Visible = False
            txtEditTitle.Visible = False
            txtEditCompany.Visible = False
            txtEditAddress.Visible = False
            txtEditCity.Visible = False
            mtbEditState.Visible = False
            mtbEditZipCode.Visible = False
            mtbEditPhoneNumber.Visible = False
            mtbEditFaxNumber.Visible = False
            btnSaveEditedData.Visible = False
            txtEditUserPassword.Visible = False
            btnChangeEmailAddress.Visible = False
            txtEditEmail.Visible = False
            btnUpdatePassword.Visible = False

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub

    Private Sub LoadUserFacilityInfo(EmailLoc As String)
        Try
            Dim dgvRow As New DataGridViewRow
            Dim SQL As String

            SQL = "SELECT substring(strairsnumber, 5, 8) as strAIRSNumber, strfacilityname, " &
             "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " &
             "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " &
             "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " &
             "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " &
             "FROM OlapUserAccess, OLAPUserLogIn  " &
             "WHERE OlapUserAccess.numUserId = OLAPUserLogIn.numUserId " &
             "and  strUserEmail = @email " &
             "order by strfacilityname"
            Dim p As New SqlParameter("@email", EmailLoc)
            Dim dt As DataTable = DB.GetDataTable(SQL, p)

            dgvUserFacilities.Rows.Clear()

            For Each dr As DataRow In dt.Rows
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvUserFacilities)
                If IsDBNull(dr.Item("strAIRSNumber")) Then
                    dgvRow.Cells(0).Value = ""
                Else
                    dgvRow.Cells(0).Value = dr.Item("strAIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    dgvRow.Cells(1).Value = ""
                Else
                    dgvRow.Cells(1).Value = dr.Item("strFacilityName")
                End If

                If IsDBNull(dr.Item("intAdminAccess")) Then
                    dgvRow.Cells(2).Value = False
                Else
                    dgvRow.Cells(2).Value = dr.Item("intAdminAccess")
                End If
                If IsDBNull(dr.Item("intFeeAccess")) Then
                    dgvRow.Cells(3).Value = False
                Else
                    dgvRow.Cells(3).Value = dr.Item("intFeeAccess")
                End If

                If IsDBNull(dr.Item("intEIAccess")) Then
                    dgvRow.Cells(4).Value = False
                Else
                    dgvRow.Cells(4).Value = dr.Item("intEIAccess")
                End If
                If IsDBNull(dr.Item("intESAccess")) Then
                    dgvRow.Cells(5).Value = False
                Else
                    dgvRow.Cells(5).Value = dr.Item("intESAccess")
                End If
                dgvUserFacilities.Rows.Add(dgvRow)
            Next

            cboFacilityToDelete.DataSource = dt
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"
            cboFacilityToDelete.Text = ""

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditUserData_Click(sender As Object, e As EventArgs) Handles btnEditUserData.Click
        Try
            txtEditFirstName.Visible = True
            txtEditLastName.Visible = True
            txtEditTitle.Visible = True
            txtEditCompany.Visible = True
            txtEditAddress.Visible = True
            txtEditCity.Visible = True
            mtbEditState.Visible = True
            mtbEditZipCode.Visible = True
            mtbEditPhoneNumber.Visible = True
            mtbEditFaxNumber.Visible = True
            btnSaveEditedData.Visible = True
            txtEditUserPassword.Visible = True
            btnChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
            btnUpdatePassword.Visible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSaveEditedData_Click(sender As Object, e As EventArgs) Handles btnSaveEditedData.Click
        Try
            Dim FirstName As String = " "
            Dim LastName As String = " "
            Dim Title As String = " "
            Dim Company As String = " "
            Dim Address As String = " "
            Dim City As String = " "
            Dim State As String = " "
            Dim Zip As String = " "
            Dim PhoneNumber As String = " "
            Dim FaxNumber As String = " "
            Dim SQL As String

            If txtWebUserID.Text <> "" Then
                If txtEditFirstName.Text <> "" Then
                    FirstName = " strFirstName = @strFirstName, "
                End If
                If txtEditLastName.Text <> "" Then
                    LastName = " strLastName = @strLastName, "
                End If
                If txtEditTitle.Text <> "" Then
                    Title = " strTitle = @strTitle, "
                End If
                If txtEditCompany.Text <> "" Then
                    Company = " strCompanyName = @strCompanyName, "
                End If
                If txtEditAddress.Text <> "" Then
                    Address = " strAddress = @strAddress, "
                End If
                If txtEditCity.Text <> "" Then
                    City = " strCity = @strCity, "
                End If
                If mtbEditState.Text <> "" Then
                    State = " strState = @strState, "
                End If
                If mtbEditZipCode.Text <> "" Then
                    Zip = " strZip = @strZip, "
                End If
                If mtbEditPhoneNumber.Text <> "" Then
                    PhoneNumber = " strPhoneNumber = @strPhoneNumber, "
                End If
                If mtbEditFaxNumber.Text <> "" Then
                    FaxNumber = " strFaxNumber = @strFaxNumber, "
                End If

                SQL = "Update OLAPUserProfile set " &
                    FirstName & LastName & Title & Company & Address &
                    City & State & Zip & PhoneNumber & FaxNumber &
                    "numUserID = @user " &
                    "where numUserID = @user "
                Dim p As SqlParameter() = {
                    New SqlParameter("@strFirstName", txtEditFirstName.Text),
                    New SqlParameter("@strLastName", txtEditLastName.Text),
                    New SqlParameter("@strTitle", txtEditTitle.Text),
                    New SqlParameter("@strCompanyName", txtEditCompany.Text),
                    New SqlParameter("@strAddress", txtEditAddress.Text),
                    New SqlParameter("@strCity", txtEditCity.Text),
                    New SqlParameter("@strState", mtbEditState.Text),
                    New SqlParameter("@strZip", mtbEditZipCode.Text),
                    New SqlParameter("@strPhoneNumber", mtbEditPhoneNumber.Text),
                    New SqlParameter("@strFaxNumber", mtbEditFaxNumber.Text),
                    New SqlParameter("@user", txtWebUserID.Text)
                }
                DB.RunCommand(SQL, p)

                lblFName.Text = "First Name: " & txtEditFirstName.Text
                lblLName.Text = "Last Name: " & txtEditLastName.Text
                lblTitle.Text = "Title: " & txtEditTitle.Text
                lblCoName.Text = "Company Name: " & txtEditCompany.Text
                lblAddress.Text = txtEditAddress.Text
                lblCityStateZip.Text = txtEditCity.Text & ", " & mtbEditState.Text & " " & mtbEditZipCode.Text
                lblPhoneNo.Text = "Phone Number: " & mtbEditPhoneNumber.Text
                lblFaxNo.Text = "Fax Number: " & mtbEditFaxNumber.Text

                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdatePassword_Click(sender As Object, e As EventArgs) Handles btnUpdatePassword.Click
        Try
            Dim SQL As String

            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                SQL = "Update OLAPUserLogIN set " &
                "strUserPassword = @pass " &
                "where numUserID = @user "
                Dim p As SqlParameter() = {
                    New SqlParameter("@pass", getMd5Hash(txtEditUserPassword.Text)),
                    New SqlParameter("@user", txtWebUserID.Text)
                }
                DB.RunCommand(SQL, p)

                txtEditUserPassword.Clear()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnChangeEmailAddress_Click(sender As Object, e As EventArgs) Handles btnChangeEmailAddress.Click
        Try
            Dim SQL As String

            If txtWebUserID.Text <> "" Then
                If IsValidEmailAddress(txtEditEmail.Text) Then
                    SQL = "Select " &
                        "numUserID " &
                        "from OLAPUserLogIN " &
                        "where strUserEmail = @email "
                    Dim p As New SqlParameter("@email", txtEditEmail.Text)
                    Dim dr As DataRow = DB.GetDataRow(SQL, p)

                    If dr IsNot Nothing Then
                        If IsDBNull(dr.Item("numUserID")) Then
                        Else
                            If txtWebUserID.Text <> dr.Item("numUserID") Then
                                MsgBox("Another user already has this email address and it would violate a unique constraint if you were " &
                                           "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                                Exit Sub
                            End If
                        End If
                    End If

                    SQL = "Update OLAPUserLogIn set " &
                        "strUserEmail = @email " &
                        "where numUserID = @user "
                    Dim p2 As SqlParameter() = {
                        p,
                        New SqlParameter("@user", txtWebUserID.Text)
                    }
                    DB.RunCommand(SQL, p2)

                    txtWebUserEmail.Text = txtEditEmail.Text

                    LoadUserInfo(txtWebUserEmail.Text)

                    If txtWebUserID.Text = "" Then
                        pnlUserInfo.Visible = False
                        pnlUserFacility.Visible = False
                    Else
                        pnlUserInfo.Visible = True
                        pnlUserFacility.Visible = True
                    End If

                Else
                    MsgBox("Invalid Email Address", MsgBoxStyle.Exclamation, "Error")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddFacilitytoUser_Click(sender As Object, e As EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            Dim SQL As String

            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                SQL = "Select " &
                "numUserId " &
                "from OlapUserAccess " &
                "where numUserId = @user " &
                "and strAirsNumber = @airs "
                Dim p As SqlParameter() = {
                    New SqlParameter("@user", txtWebUserID.Text),
                    New SqlParameter("@airs", "0413" & mtbFacilityToAdd.Text)
                }
                Dim recExist As Boolean = DB.ValueExists(SQL, p)

                If Not recExist Then
                    SQL = "Insert into OlapUserAccess " &
                        "(numUserId, strAirsNumber, strFacilityName) " &
                        "values " &
                        "(@user, @airs, " &
                        " (select strFacilityName " &
                        " from APBFacilityInformation " &
                        " where strAIRSnumber = @airs)) "
                    DB.RunCommand(SQL, p)

                    LoadUserFacilityInfo(txtWebUserEmail.Text)
                    MsgBox("The facility has beed added to this user", MsgBoxStyle.Information, "Insert Success!")
                Else
                    MsgBox("The facility already exists for this user." & vbCrLf & "NO DATA SAVED", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnDeleteFacilityUser_Click(sender As Object, e As EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            Dim SQL As String

            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                SQL = "DELETE OlapUserAccess " &
                    "WHERE numUserID = @user " &
                    "and strAirsNumber = @airs "
                Dim p As SqlParameter() = {
                    New SqlParameter("@user", txtWebUserID.Text),
                    New SqlParameter("@airs", "0413" & cboFacilityToDelete.SelectedValue)
                }
                DB.RunCommand(SQL, p)

                LoadUserFacilityInfo(txtWebUserEmail.Text)
                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click
        Try
            Dim adminaccess As String
            Dim feeaccess As String
            Dim eiaccess As String
            Dim esaccess As String
            Dim SQL As String

            For i As Integer = 0 To dgvUserFacilities.Rows.Count - 1
                If dgvUserFacilities(2, i).Value = True Then
                    adminaccess = "1"
                Else
                    adminaccess = "0"
                End If
                If dgvUserFacilities(3, i).Value = True Then
                    feeaccess = "1"
                Else
                    feeaccess = "0"
                End If
                If dgvUserFacilities(4, i).Value = True Then
                    eiaccess = "1"
                Else
                    eiaccess = "0"
                End If
                If dgvUserFacilities(5, i).Value = True Then
                    esaccess = "1"
                Else
                    esaccess = "0"
                End If

                SQL = "UPDATE OlapUserAccess " &
                    "SET intadminaccess = @intadminaccess, " &
                    "intFeeAccess = @intFeeAccess, " &
                    "intEIAccess = @intEIAccess, " &
                    "intESAccess = @intESAccess " &
                    "WHERE numUserID = @numUserID " &
                    "and strAirsNumber = @strAirsNumber "

                Dim p As SqlParameter() = {
                    New SqlParameter("@intadminaccess", adminaccess),
                    New SqlParameter("@intFeeAccess", feeaccess),
                    New SqlParameter("@intEIAccess", eiaccess),
                    New SqlParameter("@intESAccess", esaccess),
                    New SqlParameter("@numUserID", txtWebUserID.Text),
                    New SqlParameter("@strAirsNumber", "0413" & dgvUserFacilities(0, i).Value)
                }
                DB.RunCommand(SQL, p)
            Next

            LoadUserFacilityInfo(txtWebUserEmail.Text)
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub cboAvailableFeeYears_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAvailableFeeYears.SelectedIndexChanged
        If cboAvailableFeeYears.SelectedIndex > 1 Then
            btnGenerateMailoutList.Enabled = False
            btnFirstEnrollment.Enabled = False
            btnUnenrollFeeYear.Enabled = False
            btnUpdateContactData.Enabled = False
            btnSetMailoutDate.Enabled = False
            dtpDateMailoutSent.Enabled = False
        Else
            btnGenerateMailoutList.Enabled = True
            btnFirstEnrollment.Enabled = True
            btnUnenrollFeeYear.Enabled = True
            btnUpdateContactData.Enabled = True
            btnSetMailoutDate.Enabled = True
            dtpDateMailoutSent.Enabled = True
        End If
    End Sub

    Private Sub btnOpenFeesLog_Click(sender As Object, e As EventArgs) Handles btnOpenFeesLog.Click
        Dim parameters As New Dictionary(Of FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbCheckAIRSNumber.Text) Then
            parameters(FormParameter.AirsNumber) = mtbCheckAIRSNumber.Text
        End If
        parameters(FormParameter.FeeYear) = cboAvailableFeeYears.Text

        OpenSingleForm(PASPFeeAuditLog, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub dgvFeeManagementLists_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFeeManagementLists.SelectionChanged
        mtbCheckAIRSNumber.Clear()
        If dgvFeeManagementLists.SelectedRows.Count = 1 Then
            mtbCheckAIRSNumber.Text = dgvFeeManagementLists.CurrentRow.Cells("AIRSNumber").Value.ToString
        End If
    End Sub

    Private Function Update_FS_FeeRate(FeeRateID As String, FeeYear As String, PeriodStart As String,
                          PeriodEnd As String, Part70Fee As String, SMFee As String,
                          PerTonRate As String, NSPSFee As String, FeeDueDate As String,
                          AdminFee As String, AdminApplicable As String, Comments As String,
                          Active As String, FirstQrtDue As String, SecondQrtDue As String,
                          ThridQrtDue As String, FourthQrtDue As String, AAThres As String,
                          NAThres As String) As Boolean
        Try
            If IsNumeric(FeeRateID) Then
            Else
                Return False
            End If

            If IsDBNull(FeeYear) Or FeeYear = "" Then
                Return False
            Else
                If IsNumeric(FeeYear) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(Part70Fee) Or Part70Fee = "" Then
            Else
                If IsNumeric(Part70Fee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(SMFee) Or SMFee = "" Then
            Else
                If IsNumeric(SMFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(PerTonRate) Or PerTonRate = "" Then
            Else
                If IsNumeric(PerTonRate) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(NSPSFee) Or NSPSFee = "" Then
            Else
                If IsNumeric(NSPSFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(AdminFee) Or AdminFee = "" Then
            Else
                If IsNumeric(AdminFee) Then
                Else
                    Return False
                End If
            End If
            If IsDBNull(AAThres) Or AAThres = "" Then
            Else
                If IsNumeric(AAThres) Then
                Else
                    Return False
                End If
            End If
            If IsDBNull(NAThres) Or NAThres = "" Then
            Else
                If IsNumeric(NAThres) Then
                Else
                    Return False
                End If
            End If

            Dim SQL As String = "Update FS_FeeRate set " &
            "numFeeYear = @numFeeYear, " &
            "datFeePeriodStart = @datFeePeriodStart, " &
            "datFeePeriodEnd = @datFeePeriodEnd, " &
            "numPart70Fee = @numPart70Fee, " &
            "numSMFee = @numSMFee, " &
            "numPerTonRate = @numPerTonRate, " &
            "numNSPSFee = @numNSPSFee, " &
            "datFeeDueDate = @datFeeDueDate, " &
            "numAdminFeeRate = @numAdminFeeRate, " &
            "datAdminApplicable = @datAdminApplicable, " &
            "strComments = @strComments, " &
            "Active = @Active, " &
            "UpdateUser = @UpdateUser, " &
            "upDateDateTime = getdate(), " &
            "datFirstQrtDue = @datFirstQrtDue, " &
            "datSecondQrtDue = @datSecondQrtDue, " &
            "datThirdQrtDue = @datThirdQrtDue, " &
            "datFourthQrtDue = @datFourthQrtDue, " &
            "numAAThres = @numAAThres, " &
            "numNAThres = @numNAThres " &
            "where numFeeRateID = @numFeeRateID "

            Dim p As SqlParameter() = {
                New SqlParameter("@numFeeYear", FeeYear),
                New SqlParameter("@datFeePeriodStart", PeriodStart),
                New SqlParameter("@datFeePeriodEnd", PeriodEnd),
                New SqlParameter("@numPart70Fee", Part70Fee),
                New SqlParameter("@numSMFee", SMFee),
                New SqlParameter("@numPerTonRate", PerTonRate),
                New SqlParameter("@numNSPSFee", NSPSFee),
                New SqlParameter("@datFeeDueDate", FeeDueDate),
                New SqlParameter("@numAdminFeeRate", AdminFee),
                New SqlParameter("@datAdminApplicable", AdminApplicable),
                New SqlParameter("@strComments", Comments),
                New SqlParameter("@Active", Active),
                New SqlParameter("@UpdateUser", CurrentUser.UserID),
                New SqlParameter("@datFirstQrtDue", FirstQrtDue),
                New SqlParameter("@datSecondQrtDue", SecondQrtDue),
                New SqlParameter("@datThirdQrtDue", ThridQrtDue),
                New SqlParameter("@datFourthQrtDue", FourthQrtDue),
                New SqlParameter("@numAAThres", AAThres),
                New SqlParameter("@numNAThres", NAThres),
                New SqlParameter("@numFeeRateID", FeeRateID)
            }

            Return DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Insert_FSLK_NSPSReason(Description As String) As Boolean
        Try
            Dim SQL As String = "INSERT INTO FSLK_NSPSREASON (NSPSREASONCODE, DESCRIPTION, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME)" &
                "VALUES ((SELECT MAX(NSPSREASONCODE) + 1 FROM FSLK_NSPSREASON), @DESCRIPTION, '1', @UPDATEUSER, getdate(), getdate() )"
            Dim p As SqlParameter() = {
                New SqlParameter("@DESCRIPTION", Description),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID)
            }
            DB.RunCommand(SQL, p)
            Return True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Update_FSLK_NSPSReason(NSPSReasonCode As String, Description As String, ActiveStatus As String) As Boolean
        Try
            Dim SQL As String

            If Description = "" Then
                SQL = "Update FSLK_NSPSReason set " &
                "Active = @active , " &
                "updateUser = @user , " &
                "UpdateDateTime = getdate() " &
                "where NSPSReasonCode = @reason "
                Dim p As SqlParameter() = {
                    New SqlParameter("@active", ActiveStatus),
                    New SqlParameter("@user", CurrentUser.UserID),
                    New SqlParameter("@reason", NSPSReasonCode)
                }
                DB.RunCommand(SQL, p)
            Else
                SQL = "Update FSLK_NSPSReason set " &
                "Description = @description , " &
                "Active = @active , " &
                "updateUser = @user , " &
                "UpdateDateTime = getdate() " &
                "where NSPSReasonCode = @reason "
                Dim p As SqlParameter() = {
                    New SqlParameter("@description", Description),
                    New SqlParameter("@active", ActiveStatus),
                    New SqlParameter("@user", CurrentUser.UserID),
                    New SqlParameter("@reason", NSPSReasonCode)
                }
                DB.RunCommand(SQL, p)
            End If
            Return True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Insert_FSLK_NSPSReasonYear(numFeeYear As String, NSPSReasonCode As String, DisplayOrder As String) As Boolean
        Try
            Dim SQL As String = "Insert into FSLK_NSPSReasonYear " &
                " ( NUMFEEYEAR, NSPSREASONCODE, DISPLAYORDER, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME ) " &
                " values " &
                " ( @NUMFEEYEAR, @NSPSREASONCODE, @DISPLAYORDER, 1, @UPDATEUSER, getdate(), getdate() ) "
            Dim p As SqlParameter() = {
                New SqlParameter("NUMFEEYEAR", numFeeYear),
                New SqlParameter("NSPSREASONCODE", NSPSReasonCode),
                New SqlParameter("DISPLAYORDER", DisplayOrder),
                New SqlParameter("UPDATEUSER", CurrentUser.UserID)
            }
            DB.RunCommand(SQL, p)
            Return True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Private Function Update_FSLK_NSPSReasonYear(numFeeYear As String, NSPSReasonCode As String, DisplayOrder As String,
                                       ActiveStatus As String) As Boolean
        Try
            Dim SQL As String = "Update FSLK_NSPSReasonYear set " &
                "DisplayOrder = @DisplayOrder, " &
                "Active = @Active, " &
                "updateUser = @updateUser, " &
                "updateDateTime = getdate() " &
                "where numFeeYear = @numFeeYear " &
                "and NSPSReasonCode = @NSPSReasonCode "
            Dim p As SqlParameter() = {
                New SqlParameter("@DisplayOrder", DisplayOrder),
                New SqlParameter("@Active", ActiveStatus),
                New SqlParameter("@updateUser", CurrentUser.UserID),
                New SqlParameter("@numFeeYear", numFeeYear),
                New SqlParameter("@NSPSReasonCode", NSPSReasonCode)
            }

            DB.RunCommand(SQL, p)
            Return True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

End Class