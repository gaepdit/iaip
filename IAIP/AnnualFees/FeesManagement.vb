Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports GaEpd.DBUtilities
Imports Iaip.Apb
Imports Iaip.ApiCalls.EmailQueue
Imports Iaip.DAL
Imports Microsoft.Data.SqlClient

Public Class FeesManagement
    Private SelectedFeeYearIndex As Integer = -1
    Private EnableAutomatedEmailNotification As Boolean = False
    Private AnnualFeeDueDate As Date = Nothing
    Private EmailBatchId As Guid? = Nothing

    Private Sub PASPFeeManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFeeRates()
        LoadNSPSExemptions()
        LoadNSPSExemptions2("1")
        LoadFeeYears()
        FormatSelectedNSPSExemptions()

        FeeManagementListCountLabel.Text = ""
    End Sub

    Private Sub LoadFeeRates()
        Dim ind As Integer = -1

        If dgvFeeRates.Rows.Count > 0 Then
            ind = dgvFeeRates.SelectedRows(0).Index
        End If

        dgvFeeRates.DataSource = AnnualFees.GetFeeRates()
        dgvFeeRates.SanelyResizeColumns()

        If ind > -1 Then
            dgvFeeRates.SelectRow(ind)
        Else
            dgvFeeRates.SelectRow(0)
        End If
    End Sub

    Private Sub LoadNSPSExemptions()
        Dim SQL As String = "Select NSPSReasonCode,
               Description,
               strLastName + ', ' + strFirstName as UpdatingUser,
               UpdateDateTime,
               CreateDateTime,
               IIF(Active = '0', 'Flagged as deleted', 'Active') as ActiveStatus
        from FSLK_NSPSReason
            inner join EPDUserProfiles
            on FSLK_NSPSReason.UpdateUser = EPDUserProfiles.numUserID
        where Active = '1'
        order by NSPSReasonCode "

        dgvNSPSExemptions.DataSource = DB.GetDataTable(SQL)

        dgvNSPSExemptions.RowHeadersVisible = False
        dgvNSPSExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvNSPSExemptions.AllowUserToResizeColumns = True
        dgvNSPSExemptions.AllowUserToResizeRows = False
        dgvNSPSExemptions.AllowUserToAddRows = False
        dgvNSPSExemptions.AllowUserToDeleteRows = False
        dgvNSPSExemptions.AllowUserToOrderColumns = False
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
    End Sub

    Private Sub LoadNSPSExemptions2(ActiveStatus As String)
        Dim SQL As String = "select NSPSReasonCode,
                Description,
                strLastName + ', ' + strFirstName as UpdatingUser,
                UpdateDateTime,
                CreateDateTime,
                IIF(Active = '0', 'Flagged as deleted', 'Active') as ActiveStatus
        from FSLK_NSPSReason
            inner join EPDUserProfiles
            on FSLK_NSPSReason.UpdateUser = EPDUserProfiles.numUserID
        where Active = @Active
        order by NSPSReasonCode "

        Dim p As New SqlParameter("@Active", ActiveStatus)

        dgvExistingExemptions.DataSource = DB.GetDataTable(SQL, p)

        dgvExistingExemptions.RowHeadersVisible = False
        dgvExistingExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvExistingExemptions.AllowUserToResizeColumns = True
        dgvExistingExemptions.AllowUserToResizeRows = False
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
    End Sub

    Private Sub LoadFeeYears()
        Dim allFeeYears As List(Of Integer) = AnnualFees.GetAllFeeYears()
        cboNSPSExemptionYear.DataSource = allFeeYears
        cboAvailableFeeYears.DataSource = allFeeYears
    End Sub

    Private Sub FormatSelectedNSPSExemptions()
        dgvNSPSExemptionsByYear.RowHeadersVisible = False
        dgvNSPSExemptionsByYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvNSPSExemptionsByYear.AllowUserToResizeColumns = True
        dgvNSPSExemptionsByYear.AllowUserToAddRows = False
        dgvNSPSExemptionsByYear.AllowUserToDeleteRows = False
        dgvNSPSExemptionsByYear.AllowUserToOrderColumns = False
        dgvNSPSExemptionsByYear.AllowUserToResizeRows = False
        dgvNSPSExemptionsByYear.ColumnHeadersHeight = 35

        dgvNSPSExemptionsByYear.Columns.Add("numFeeYear", "Year")
        dgvNSPSExemptionsByYear.Columns("numFeeYear").DisplayIndex = 0
        dgvNSPSExemptionsByYear.Columns("numFeeYear").Width = CInt(Math.Floor(dgvNSPSExemptionsByYear.Width * 0.1))
        dgvNSPSExemptionsByYear.Columns("numFeeYear").Visible = True

        dgvNSPSExemptionsByYear.Columns.Add("NSPSReasonCode", "NSPS ID")
        dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").DisplayIndex = 1
        dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").Width = CInt(Math.Floor(dgvNSPSExemptionsByYear.Width * 0.15))
        dgvNSPSExemptionsByYear.Columns("NSPSReasonCode").ReadOnly = True

        dgvNSPSExemptionsByYear.Columns.Add("displayOrder", "Display Order")
        dgvNSPSExemptionsByYear.Columns("displayOrder").DisplayIndex = 2
        dgvNSPSExemptionsByYear.Columns("displayOrder").Width = CInt(Math.Floor(dgvNSPSExemptionsByYear.Width * 0.15))
        dgvNSPSExemptionsByYear.Columns("displayOrder").ReadOnly = False

        dgvNSPSExemptionsByYear.Columns.Add("Description", "NSPS Exemption Reason")
        dgvNSPSExemptionsByYear.Columns("Description").DisplayIndex = 3
        dgvNSPSExemptionsByYear.Columns("Description").Width = CInt(Math.Floor(dgvNSPSExemptionsByYear.Width * 0.6))
        dgvNSPSExemptionsByYear.Columns("Description").ReadOnly = True
    End Sub

    Private Sub dgvFeeRates_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFeeRates.SelectionChanged
        If dgvFeeRates.SelectedRows.Count = 1 Then
            Dim row As DataGridViewRow = dgvFeeRates.SelectedRows(0)

            txtFeeYear.Text = row.Cells("Fee Year").Value.ToString
            dtpFeePeriodStart.Value = CDate(row.Cells("Start Date").Value)
            dtpFeePeriodEnd.Value = CDate(row.Cells("End Date").Value)
            txtPart70Fee.Text = CDec(row.Cells("Part 70 Fee").Value).ToString("0.##")
            txtPart70MaintenanceFee.Text = CDec(row.Cells("Maintenance Fee").Value).ToString("0.##")
            txtAnnualSMFee.Text = CDec(row.Cells("SM Annual Fee").Value).ToString("0.##")
            txtAnnualNSPSFee.Text = CDec(row.Cells("NSPS Annual Fee").Value).ToString("0.##")
            txtPerTonRate.Text = CDec(row.Cells("Per Ton Fee Rate").Value).ToString("0.##")
            dtpFeeDueDate.Value = CDate(row.Cells("Due Date").Value)
            txtAdminFeePercent.Text = CDec(row.Cells("Admin Fee Percent").Value).ToString("0.##")
            dtpAdminApplicableDate.Value = CDate(row.Cells("Admin Fee Date").Value.ToString)
            dtpFirstQrtDue.Value = CDate(row.Cells("Q1 Due Date").Value)
            dtpSecondQrtDue.Value = CDate(row.Cells("Q2 Due Date").Value)
            dtpThirdQrtDue.Value = CDate(row.Cells("Q3 Due Date").Value)
            dtpFourthQrtDue.Value = CDate(row.Cells("Q4 Due Date").Value)
            txtAttainmentThreshold.Text = row.Cells("Attainment Area Threshold").Value.ToString
            txtNonAttainmentThreshold.Text = row.Cells("Non-attainment Area Threshold").Value.ToString
            txtFeeNotes.Text = GetNullableString(row.Cells("Notes").Value)
        End If
    End Sub

    Private Sub btnUpdateFeeData_Click(sender As Object, e As EventArgs) Handles btnUpdateFeeData.Click
        If String.IsNullOrEmpty(txtFeeYear.Text) OrElse Not IsNumeric(txtFeeYear.Text) Then
            MsgBox("Select a fee year record first.")
        End If

        If Not IsNumeric(txtPart70Fee.Text) OrElse
            Not IsNumeric(txtAnnualSMFee.Text) OrElse
            Not IsNumeric(txtAnnualNSPSFee.Text) OrElse
            Not IsNumeric(txtPerTonRate.Text) OrElse
            Not IsNumeric(txtAdminFeePercent.Text) OrElse
            Not IsNumeric(txtAttainmentThreshold.Text) OrElse
            Not IsNumeric(txtNonAttainmentThreshold.Text) OrElse
            Not IsNumeric(txtPart70MaintenanceFee.Text) Then

            MsgBox("No update: Please check for data errors.", MsgBoxStyle.Information, Me.Text)
            Return
        End If

        If AnnualFees.UpdateFeeRates(CInt(txtFeeYear.Text), dtpFeePeriodStart.Value, dtpFeePeriodEnd.Value,
                              CDec(txtPart70Fee.Text), CDec(txtAnnualSMFee.Text), CDec(txtPerTonRate.Text),
                              CDec(txtAnnualNSPSFee.Text), dtpFeeDueDate.Value, CDec(txtAdminFeePercent.Text),
                              dtpAdminApplicableDate.Value, txtFeeNotes.Text, dtpFirstQrtDue.Value, dtpSecondQrtDue.Value,
                              dtpThirdQrtDue.Value, dtpFourthQrtDue.Value, CInt(txtAttainmentThreshold.Text),
                              CInt(txtNonAttainmentThreshold.Text), CDec(txtPart70MaintenanceFee.Text)) Then

            LoadFeeRates()
            MsgBox("Update completed", MsgBoxStyle.Information, Me.Text)
        Else
            MsgBox("Error updating fee rates.", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub LoadNSPSExemptionByYear()
        Try
            Dim ReasonID As String = ""
            Dim DisplayOrder As String = ""
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
            Dim sbNSPStemp As New StringBuilder()

            For Each dr As DataRow In dt.Rows
                If Not IsDBNull(dr.Item("NSPSReasonCode")) Then
                    sbNSPStemp.Append(dr.Item("NSPSReasonCode").ToString)

                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        sbNSPStemp.Append("-" & i & ",")
                        i += 1
                    Else
                        sbNSPStemp.Append("-" & dr.Item("DisplayOrder").ToString & ",")
                        If CInt(dr.Item("DisplayOrder")) >= i Then
                            i = CInt(dr.Item("DisplayOrder")) + 1
                        End If
                    End If
                End If
            Next

            Dim NSPStemp As String = sbNSPStemp.ToString

            dgvNSPSExemptionsByYear.Rows.Clear()

            Do While NSPStemp <> ""
                ReasonID = Mid(NSPStemp, 1, InStr(NSPStemp, "-", CompareMethod.Text) - 1)

                If ReasonID.Length = 1 Then
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 3)
                Else
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 4)
                End If

                Dim rgxPattern As String = "\b" & ReasonID & "-" & DisplayOrder & ","
                NSPStemp = RegularExpressions.Regex.Replace(NSPStemp, rgxPattern, "")

                Dim x As Integer = 0
                While x < dgvNSPSExemptions.Rows.Count
                    Dim y As Integer = 0
                    While y < dgvNSPSExemptions.Rows(x).Cells.Count
                        Dim c As DataGridViewCell = dgvNSPSExemptions.Rows(x).Cells(y)
                        If c.Value IsNot DBNull.Value AndAlso CType(c.Value, String) = ReasonID Then
                            Using dgvRow As New DataGridViewRow
                                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, x).Value
                                dgvRow.Cells(2).Value = DisplayOrder
                                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, x).Value
                                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                            End Using
                        End If
                        Math.Min(Threading.Interlocked.Increment(y), y - 1)
                    End While
                    Math.Min(Threading.Interlocked.Increment(x), x - 1)
                End While
            Loop

            dgvNSPSExemptionsByYear.AutoResizeColumns()
            dgvNSPSExemptionsByYear.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectForm_Click(sender As Object, e As EventArgs) Handles btnSelectForm.Click
        Try
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvNSPSExemptionsByYear.Rows.Count

            If i > 0 Then
                temp = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value.ToString
                For i = 0 To dgvNSPSExemptionsByYear.Rows.Count - 1
                    If dgvNSPSExemptionsByYear(1, i).Value.ToString = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    Using dgvRow As New DataGridViewRow
                        dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                        dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                        dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                        dgvRow.Cells(2).Value = dgvNSPSExemptionsByYear.RowCount + 1
                        dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                        dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                    End Using
                End If
            Else
                Using dgvRow As New DataGridViewRow
                    dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                    dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                    dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvNSPSExemptionsByYear.RowCount + 1
                    dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                End Using
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewNSPSExemptionsByYear_Click(sender As Object, e As EventArgs) Handles btnViewNSPSExemptionsByYear.Click
        If Not String.IsNullOrEmpty(cboNSPSExemptionYear.Text) Then
            LoadNSPSExemptionByYear()
        End If
    End Sub

    Private Sub btnSelectAllForms_Click(sender As Object, e As EventArgs) Handles btnSelectAllForms.Click
        Try
            Dim i As Integer = 0
            dgvNSPSExemptionsByYear.Rows.Clear()

            For i = 0 To dgvNSPSExemptions.Rows.Count - 1
                Using dgvRow As New DataGridViewRow
                    dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                    dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                    dgvRow.Cells(1).Value = dgvNSPSExemptions(0, i).Value
                    dgvRow.Cells(2).Value = dgvNSPSExemptionsByYear.RowCount + 1
                    dgvRow.Cells(3).Value = dgvNSPSExemptions(1, i).Value
                    dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                End Using
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectForm_Click(sender As Object, e As EventArgs) Handles btnUnselectForm.Click
        If dgvNSPSExemptionsByYear.RowCount > 0 Then
            Dim ReasonID As String = dgvNSPSExemptionsByYear(1, dgvNSPSExemptionsByYear.CurrentRow.Index).Value.ToString

            Dim SQL As String = "SELECT COUNT(*) " &
                " FROM FS_FEEDATA " &
                " WHERE NUMFEEYEAR = @year " &
                " AND (STRNSPSEXEMPTREASON LIKE @reason1 " &
                " OR STRNSPSEXEMPTREASON = @reason2 " &
                " OR STRNSPSEXEMPTREASON LIKE @reason3 " &
                " OR STRNSPSEXEMPTREASON LIKE @reason4) "

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
    End Sub

    Private Sub btnUpdateNSPSbyYear_Click(sender As Object, e As EventArgs) Handles btnUpdateNSPSbyYear.Click
        If cboNSPSExemptionYear.Text = "" OrElse Not IsNumeric(cboNSPSExemptionYear.Text) Then
            MessageBox.Show("Select a Fee Year first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim x As Integer = 0
            Dim ReasonID As Integer
            Dim Order As Integer
            Dim temp As String = ""
            Dim sbExistingID As New StringBuilder()
            Dim SQL As String

            SQL = "Select NSPSReasonCode from FSLK_NSPSReasonYear where numFeeYear = @year "
            Dim p As New SqlParameter("@year", cboNSPSExemptionYear.Text)

            Dim dt As DataTable = DB.GetDataTable(SQL, p)

            For Each dr As DataRow In dt.Rows
                If Not IsDBNull(dr.Item("NSPSReasonCode")) Then
                    sbExistingID.Append("(" & dr.Item("NSPSReasonCode").ToString & ")")
                End If
            Next

            Dim ExistingID As String = sbExistingID.ToString

            While x < dgvNSPSExemptionsByYear.Rows.Count
                ReasonID = CInt(dgvNSPSExemptionsByYear(1, x).Value)
                Order = CInt(dgvNSPSExemptionsByYear(2, x).Value)
                x += 1

                SQL = "Select DisplayOrder from FSLK_NSPSReasonYear where numFeeYear = @year and NSPSReasonCode = @reasoncode "
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@year", CInt(cboNSPSExemptionYear.Text)),
                    New SqlParameter("@reasoncode", ReasonID)
                }
                Dim dr As DataRow = DB.GetDataRow(SQL, p2)
                temp = ""
                If dr IsNot Nothing Then
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        temp = "NULL"
                    Else
                        temp = dr.Item("DisplayOrder").ToString
                    End If
                End If

                If temp <> Order.ToString Then
                    Select Case temp
                        Case ""
                            Insert_FSLK_NSPSReasonYear(CInt(cboNSPSExemptionYear.Text), ReasonID, Order)
                        Case Else
                            Update_FSLK_NSPSReasonYear(CInt(cboNSPSExemptionYear.Text), ReasonID, Order, True)
                    End Select
                End If

                ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
            End While

            If ExistingID <> "" Then
                Do While ExistingID <> ""
                    ReasonID = CInt(Mid(ExistingID, InStr(ExistingID, "(", CompareMethod.Text) + 1, InStr(ExistingID, ")", CompareMethod.Text) - 2))
                    ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
                    Update_FSLK_NSPSReasonYear(CInt(cboNSPSExemptionYear.Text), ReasonID, 0, False)
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
            If Insert_FSLK_NSPSReason(txtNSPSExemption.Text) Then
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
            If txtDeleteNSPSExemptions.Text <> "" AndAlso
                Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, "", "0") Then

                txtDeleteNSPSExemptions.Clear()
                txtNSPSExemption.Clear()
                LoadNSPSExemptions()
                LoadNSPSExemptions2("1")
                MsgBox("Exemption Deleted", MsgBoxStyle.Information, Me.Text)
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
            If txtDeleteNSPSExemptions.Text <> "" AndAlso
                Update_FSLK_NSPSReason(txtDeleteNSPSExemptions.Text, txtNSPSExemption.Text, "1") Then

                txtDeleteNSPSExemptions.Clear()
                txtNSPSExemption.Clear()
                LoadNSPSExemptions()
                LoadNSPSExemptions2("1")

                MsgBox("Exemption Updated", MsgBoxStyle.Information, Me.Text)
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
            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "Select " &
            "count(*) as EnrollCheck " &
            "from FS_Admin " &
            "where numFeeYear = @year " &
            "and strEnrolled = '1' " &
            "and ACTIVE = '1' "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)

            Dim EnrollCheck As Integer = 0

            If dr IsNot Nothing AndAlso Not IsDBNull(dr.Item("EnrollCheck")) Then
                EnrollCheck = CInt(dr.Item("EnrollCheck"))
            End If

            If EnrollCheck > 0 Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "There are already facilities enrolled for this fee year.",
                        MsgBoxStyle.Exclamation, Me.Text)
                Return
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
            "datInitialEnrollment = getdate() " &
            "where numFeeYear = @year " &
            "and datInitialEnrollment is null " &
            "and ACTIVE = '1' "
            DB.RunCommand(SQL, p)

            Dim p3 As SqlParameter() = {
                New SqlParameter("@FeeYear", cboAvailableFeeYears.Text),
                New SqlParameter("@AIRSNumber", "")
            }
            DB.SPRunCommand("dbo.PD_FEE_DATA", p3)

            ViewEnrolledFacilities()

            MsgBox("Facilities Enrolled for the selected fee year.", MsgBoxStyle.Information, Me.Text)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnenrollFeeYear_Click(sender As Object, e As EventArgs) Handles btnUnenrollFeeYear.Click
        Try
            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If CInt(cboAvailableFeeYears.Text) < (Today.Year - 1) Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Only Current and last Fee Years are eligible to be unenrolled.",
                        MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If DialogResult.No = MessageBox.Show("Are you positive you want to reset enrollment for this year?",
                                                 Me.Text, MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) Then
                Return
            End If

            Dim SQL As String = "Update FS_Admin set " &
            "strEnrolled = '0', " &
            "datEnrollment = null, " &
            "datInitialEnrollment = null, " &
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
        Dim p As New SqlParameter("@feeYear", cboAvailableFeeYears.Text)
        dgvFeeManagementLists.DataSource = DB.SPGetDataTable("dbo.GetFeeEnrolledList", p)
        FeeManagementListCountLabel.Text = $"Viewing enrolled facilities for the {cboAvailableFeeYears.Text } fee year: " &
            $"{dgvFeeManagementLists.RowCount} result{If(dgvFeeManagementLists.RowCount = 1, "", "s") }"
    End Sub

    Private Sub btnViewMailout_Click(sender As Object, e As EventArgs) Handles btnViewMailout.Click
        ViewMailOut()
    End Sub

    Private Sub btnGenerateMailoutList_Click(sender As Object, e As EventArgs) Handles btnGenerateMailoutList.Click
        Try

            If cboAvailableFeeYears.Text = "" OrElse Not IsNumeric(cboAvailableFeeYears.Text) Then
                MsgBox("Select a valid fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim p As New SqlParameter("@FeeYear", CShort(cboAvailableFeeYears.Text))
            Dim returnValue As Integer = DB.SPReturnValue("dbo.PD_FEE_MAILOUT_ALL", p)

            If returnValue = 1 Then
                MsgBox("The mailout list has already been generated.", MsgBoxStyle.Exclamation, Me.Text)
            ElseIf returnValue = -1 Then
                MsgBox("There was an error generating the mailout list.", MsgBoxStyle.Exclamation, Me.Text)
            End If

            ViewMailOut()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewMailOut()
        Dim p As New SqlParameter("@feeYear", cboAvailableFeeYears.Text)
        dgvFeeManagementLists.DataSource = DB.SPGetDataTable("dbo.GetFeeMailoutList", p)
        lblMailoutCount.Text = dgvFeeManagementLists.RowCount

        FeeManagementListCountLabel.Text = $"Viewing facilities in the mailout list for the {cboAvailableFeeYears.Text } fee year: " &
                $"{dgvFeeManagementLists.RowCount} result{If(dgvFeeManagementLists.RowCount = 1, "", "s") }"
    End Sub

    Private Sub btnUpdateContactData_Click(sender As Object, e As EventArgs) Handles btnUpdateContactData.Click
        ' Warn user
        If DialogResult.No = MessageBox.Show("This will replace mailout contact data with the current" & vbNewLine &
                                             "fee contact for all sources in the mailout list." &
                                             vbNewLine & vbNewLine &
                                             "Are you sure you want to proceed? ",
                                             "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2) Then
            Return
        End If

        Dim SelectedYear As Integer

        If Not Integer.TryParse(cboAvailableFeeYears.Text, SelectedYear) Then
            MessageBox.Show("Invalid year selected")
            Return
        End If

        Cursor = Cursors.WaitCursor

        Dim query As String = "update FS_MAILOUT
            set STRFIRSTNAME = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.FirstName, c.STRCONTACTFIRSTNAME)),
                STRLASTNAME = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.LastName, c.STRCONTACTLASTNAME)),
                STRPREFIX = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Prefix, c.STRCONTACTPREFIX)),
                STRTITLE = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Title, c.STRCONTACTTITLE)),
                STRCONTACTCONAME = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Organization, c.STRCONTACTCOMPANYNAME)),
                STRCONTACTADDRESS1 = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Address1, c.STRCONTACTADDRESS1)),
                STRCONTACTADDRESS2 = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Address2, c.STRCONTACTADDRESS2)),
                STRCONTACTCITY = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.City, c.STRCONTACTCITY)),
                STRCONTACTSTATE = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.State, c.STRCONTACTSTATE)),
                STRCONTACTZIPCODE = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.PostalCode, c.STRCONTACTZIPCODE)),
                STRGECOUSEREMAIL = dbo.NullIfNaOrEmpty(IIF(m.Id is not null, m.Email, c.STRCONTACTEMAIL)),
                UPDATEDATETIME     = getdate(),
                UPDATEUSER         = @userId
            from FS_MAILOUT f
                left join Geco_MailContact m
                on f.STRAIRSNUMBER = m.FacilityId
                    and m.Confirmed = 1
                    and m.Category = 'Fees'
                left join dbo.APBCONTACTINFORMATION c
                on f.STRAIRSNUMBER = c.STRAIRSNUMBER
                    and c.STRKEY = '40'
            where f.NUMFEEYEAR = @year
              and (m.Id is not null or c.STRKEY is not null)"

        Dim p As SqlParameter() = {
            New SqlParameter("@year", SelectedYear),
            New SqlParameter("@userId", CurrentUser.UserID)
        }

        Dim rowsaffected As Integer = 0

        Try
            DB.RunCommand(query, p, rowsaffected)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Cursor = Nothing
        End Try

        MessageBox.Show(rowsaffected.ToString & " facilities updated.")

        ViewMailOut()
    End Sub

    Private Sub dgvExistingExemptions_SelectionChanged(sender As Object, e As EventArgs) Handles dgvExistingExemptions.SelectionChanged
        If dgvExistingExemptions.SelectedRows.Count = 1 Then
            Dim row As DataGridViewRow = dgvExistingExemptions.CurrentRow

            txtDeleteNSPSExemptions.Clear()
            txtNSPSExemption.Clear()
            If IsDBNull(row.Cells(0).Value) Then
                Return
            Else
                txtDeleteNSPSExemptions.Text = row.Cells(0).Value.ToString
            End If
            If IsDBNull(row.Cells(1).Value) Then
                txtNSPSExemption.Clear()
            Else
                txtNSPSExemption.Text = row.Cells(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub btnRefreshNSPSExemptions_Click(sender As Object, e As EventArgs) Handles btnRefreshNSPSExemptions.Click
        LoadNSPSExemptions2("1")
    End Sub

    Private Sub btnClearNSPSExemptions_Click(sender As Object, e As EventArgs) Handles btnClearNSPSExemptions.Click
        txtDeleteNSPSExemptions.Clear()
        txtNSPSExemption.Clear()
    End Sub

    Private Sub dgvFeeManagementLists_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvFeeManagementLists.CellFormatting
        If e IsNot Nothing AndAlso
            e.Value IsNot Nothing AndAlso
            Not IsDBNull(e.Value) AndAlso
            dgvFeeManagementLists.Columns(e.ColumnIndex).HeaderText = "Airs No." AndAlso
            Apb.ApbFacilityId.IsValidAirsNumberFormat(e.Value.ToString()) Then

            e.Value = New Apb.ApbFacilityId(e.Value.ToString).FormattedString

        ElseIf e IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) AndAlso
            dgvFeeManagementLists.Columns(e.ColumnIndex).HeaderText = "Sent" AndAlso TypeOf e.Value Is Date Then

            e.CellStyle.Format = "yyyy-MM-dd HH:mm:ss"

        End If
    End Sub

    Private Sub btnViewFacilitiesSubjectToFees_Click(sender As Object, e As EventArgs) Handles btnViewFacilitiesSubjectToFees.Click
        Try
            Dim p As New SqlParameter("@feeYear", cboAvailableFeeYears.Text)
            dgvFeeManagementLists.DataSource = DB.SPGetDataTable("dbo.GetFeeUniverse", p)
            FeeManagementListCountLabel.Text = $"Viewing facilities subject to fees for the {cboAvailableFeeYears.Text } fee year: " &
                $"{dgvFeeManagementLists.RowCount} result{If(dgvFeeManagementLists.RowCount = 1, "", "s") }"
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Async Sub btnSendInitialEmail_Click(sender As Object, e As EventArgs) Handles btnSendInitialEmail.Click
        Dim userResponse As DialogResult = MessageBox.Show($"Are you sure you want to send the initial e-notification for the {cboAvailableFeeYears.Text } fee year?" &
            vbNewLine & vbNewLine & "(This will send a mass email to all sources in the mailout list for which an email address is available.)",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)

        If userResponse = DialogResult.No Then Return

        Dim dv As DataView = TryCast(dgvFeeManagementLists.DataSource, DataView)

        If dv Is Nothing Then
            MessageBox.Show("There are no sources available in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim response As EmailQueueResponse = Await SendAnnualFeeNotificationAsync(dv, cboAvailableFeeYears.Text, AnnualFeeDueDate)

            If response Is Nothing Then
                MessageBox.Show("There was a problem sending the initial email notification. Please contact EPD-IT for more information.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                If response.Body Is Nothing Then
                    MessageBox.Show("There was a problem sending the initial email notification. Please contact EPD-IT for more information." &
                                    vbNewLine & vbNewLine & $"Emailer Status: {response.Status}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ElseIf response.Body.Status = "Empty" Then
                    MessageBox.Show("No emails were sent.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    Dim p As SqlParameter() = {
                        New SqlParameter("@feeYear", cboAvailableFeeYears.Text),
                        New SqlParameter("@batchId", response.Body.BatchId)
                    }
                    DB.SPRunCommand("dbo.SetInitialFeeNotificationDate", p)

                    MessageBox.Show($"The initial email notification has been processed and {response.Body.Count} emails have been queued.",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Await LoadEmailBatchDetailsAsync()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error sending the initial email notification. Please contact EPD-IT for more information.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        LoadFeeYearData()
    End Sub

    Private Sub cboAvailableFeeYears_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAvailableFeeYears.SelectedIndexChanged
        If cboAvailableFeeYears.SelectedIndex >= 0 AndAlso cboAvailableFeeYears.SelectedIndex <> SelectedFeeYearIndex Then
            LoadFeeYearData()
        End If
    End Sub

    Private Sub LoadFeeYearData()
        SelectedFeeYearIndex = cboAvailableFeeYears.SelectedIndex
        FeeManagementListCountLabel.Text = ""
        dgvFeeManagementLists.DataSource = Nothing

        lblFeeYearCount.Text = ""
        lblMailoutCount.Text = ""
        lblEnrollmentCount.Text = ""
        lblInitialMailoutDate.Text = ""
        lblInitialMailoutDate.Visible = False
        btnViewEmailBatchStatus.Visible = False

        Dim buttonsEnabled As Boolean = cboAvailableFeeYears.SelectedIndex = 0

        btnGenerateMailoutList.Enabled = buttonsEnabled
        btnFirstEnrollment.Enabled = buttonsEnabled
        btnUnenrollFeeYear.Enabled = buttonsEnabled
        btnUpdateContactData.Enabled = buttonsEnabled
        btnSendInitialEmail.Visible = False
        EnableAutomatedEmailNotification = buttonsEnabled
        btnViewEmailList.Enabled = buttonsEnabled
        btnViewPhysicalMailList.Enabled = buttonsEnabled

        Dim p As New SqlParameter("@FeeYear", cboAvailableFeeYears.Text)
        Dim row As DataRow = DB.SPGetDataRow("dbo.GetFeeYearStats", p)

        If row IsNot Nothing Then
            lblFeeYearCount.Text = GetNullable(Of Integer)(row("FeeYearCount"))
            lblEnrollmentCount.Text = GetNullable(Of Integer)(row("EnrollmentCount"))

            AnnualFeeDueDate = GetNullableDateTime(row("AnnualFeeDueDate"))
            If AnnualFeeDueDate <> Nothing Then
                lblFeeDueDate.Text = "Reporting Due Date:" & vbNewLine & Format(AnnualFeeDueDate, "dd-MMM-yyyy")
            End If

            Dim mailoutCount As Integer = GetNullable(Of Integer)(row("MailoutCount"))
            lblMailoutCount.Text = mailoutCount
            If mailoutCount > 0 Then
                btnGenerateMailoutList.Enabled = False
            Else
                btnUpdateContactData.Enabled = False
            End If

            Dim initialMailoutDate As Date? = GetNullable(Of Date?)(row("InitialMailoutDate"))
            If initialMailoutDate IsNot Nothing Then
                lblInitialMailoutDate.Text = "Initial Mailout Date:" & vbNewLine & initialMailoutDate.Value.ToString("dd-MMM-yyyy")
                lblInitialMailoutDate.Visible = True
                EnableAutomatedEmailNotification = False
            End If

            EmailBatchId = GetNullable(Of Guid?)(row("InitialMailoutEmailBatchId"))
            btnViewEmailBatchStatus.Visible = EmailBatchId IsNot Nothing

            Dim month As Integer = Date.Today.Month

            If month < 5 OrElse month > 7 Then
                EnableAutomatedEmailNotification = False
            End If
        End If
    End Sub

    Private Sub dgvFeeManagementLists_CellLinkSelected(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvFeeManagementLists.CellLinkSelected
        AIRSNumberEntry.Text = e.LinkValue.ToString
    End Sub

    Private Sub dgvFeeManagementLists_CellLinkActivated(sender As Object, e As IaipDataGridViewCellLinkEventArgs) Handles dgvFeeManagementLists.CellLinkActivated
        Dim parameters As New Dictionary(Of FormParameter, String)

        If Apb.ApbFacilityId.IsValidAirsNumberFormat(e.LinkValue.ToString) Then
            parameters(FormParameter.AirsNumber) = e.LinkValue.ToString
        End If

        parameters(FormParameter.FeeYear) = cboAvailableFeeYears.Text
        OpenSingleForm(FeesAudit, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub btnOpenFeesLog_Click(sender As Object, e As EventArgs) Handles btnOpenFeesLog.Click
        Dim parameters As New Dictionary(Of FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(AIRSNumberEntry.Text) Then
            parameters(FormParameter.AirsNumber) = AIRSNumberEntry.Text
        End If
        parameters(FormParameter.FeeYear) = cboAvailableFeeYears.Text

        OpenSingleForm(FeesAudit, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Function Insert_FSLK_NSPSReason(Description As String) As Boolean
        Try
            Dim SQL As String = "INSERT INTO FSLK_NSPSREASON (NSPSREASONCODE, DESCRIPTION, ACTIVE, UPDATEUSER, UPDATEDATETIME, CREATEDATETIME)" &
                "VALUES ((SELECT MAX(NSPSREASONCODE) + 1 FROM FSLK_NSPSREASON), @DESCRIPTION, '1', @UPDATEUSER, getdate(), getdate() )"
            Dim p As SqlParameter() = {
                New SqlParameter("@DESCRIPTION", Description),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID)
            }
            Return DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
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
                Return DB.RunCommand(SQL, p)
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
                Return DB.RunCommand(SQL, p)
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function Insert_FSLK_NSPSReasonYear(numFeeYear As Integer, NSPSReasonCode As Integer, DisplayOrder As Integer) As Boolean
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
            Return DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Function Update_FSLK_NSPSReasonYear(numFeeYear As Integer, NSPSReasonCode As Integer, DisplayOrder As Integer,
                                       ActiveStatus As Boolean) As Boolean
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
                New SqlParameter("@Active", If(ActiveStatus, "1", "0")),
                New SqlParameter("@updateUser", CurrentUser.UserID),
                New SqlParameter("@numFeeYear", numFeeYear),
                New SqlParameter("@NSPSReasonCode", NSPSReasonCode)
            }

            Return DB.RunCommand(SQL, p)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
            Return False
        End Try
    End Function

    Private Sub btnViewPhysicalMailList_Click(sender As Object, e As EventArgs) Handles btnViewPhysicalMailList.Click
        Dim p As New SqlParameter("@feeYear", cboAvailableFeeYears.Text)
        dgvFeeManagementLists.DataSource = DB.SPGetDataTable("dbo.GetFeeMailoutPhysicalMailList", p)
        FeeManagementListCountLabel.Text = $"Viewing facilities in the {cboAvailableFeeYears.Text } mailout list requiring physical mail: " &
                $"{dgvFeeManagementLists.RowCount} result{If(dgvFeeManagementLists.RowCount = 1, "", "s") }"
    End Sub

    Private Sub btnViewEmailList_Click(sender As Object, e As EventArgs) Handles btnViewEmailList.Click
        Dim p As New SqlParameter("@feeYear", cboAvailableFeeYears.Text)
        dgvFeeManagementLists.DataSource = DB.SPGetDataTable("dbo.GetFeeMailoutEmailList", p)
        FeeManagementListCountLabel.Text = $"Viewing email addresses for facilities in the {cboAvailableFeeYears.Text } mailout list: " &
                $"{dgvFeeManagementLists.RowCount} result{If(dgvFeeManagementLists.RowCount = 1, "", "s") }"

        btnSendInitialEmail.Visible = EnableAutomatedEmailNotification AndAlso dgvFeeManagementLists.RowCount > 0
    End Sub

    Private Sub dgvFeeManagementLists_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvFeeManagementLists.DataSourceChanged
        btnSendInitialEmail.Visible = False
        dgvFeeManagementLists.LinkifyFirstColumn = dgvFeeManagementLists.DataSource IsNot Nothing AndAlso
            dgvFeeManagementLists.Columns(0).HeaderText = "Airs No."
    End Sub

    Private Async Sub btnViewEmailBatchStatus_Click(sender As Object, e As EventArgs) Handles btnViewEmailBatchStatus.Click
        Await LoadEmailBatchDetailsAsync()
    End Sub

    Private Async Function LoadEmailBatchDetailsAsync() As Task
        Dim response As EmailBatchDetails = Await GetBatchDetails(EmailBatchId)

        If response Is Nothing OrElse response.Status = "Failed" OrElse response.Emails Is Nothing OrElse Not response.Emails.Any() Then
            MessageBox.Show("There was a problem retrieving the email batch status. Please contact EPD-IT for more information.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            dgvFeeManagementLists.DataSource = response.Emails.OrderBy(Function(p) p.Counter).Select(Function(p) New With {
                p.Status, .Sent = p.AttemptedAt, p.Subject, .Recipients = String.Join(", ", p.Recipients)
            }).ToList()
        End If
    End Function

    Private Async Function SendAnnualFeeNotificationAsync(dv As DataView, feeYear As Integer, deadline As Date) As Task(Of EmailQueueResponse)
        Dim emails As New List(Of NewEmailTask)
        Dim gecoUrl As String = ConfigurationManager.AppSettings("GecoUrl")
        Dim deadlineFormatted As String = deadline.ToString("MMMM d, yyyy")

        For Each rowView As DataRowView In dv
            Dim recipientsList As List(Of String) = rowView("Emails").ToString().Split(","c).Select(Function(s) s.Trim()).ToList()
            Dim airsNumber As String = rowView("Airs No.").ToString().Trim()
            Dim airsNumberFormatted As String = $"{Strings.Left(airsNumber, 3)}-{Strings.Right(airsNumber, 5)}"
            Dim facilityName As String = rowView("Facility Name (snapshot)").ToString().Trim()

            Dim newEmail As New NewEmailTask() With {
                .From = "GeorgiaAirProtectionBranch@dnr.ga.gov",
                .Recipients = recipientsList,
                .Subject = $"Data Collection for {feeYear} Calendar Year Emission Fees (AIRS #{airsNumberFormatted}: {facilityName})",
                .Body = EmailBody(feeYear, deadlineFormatted, gecoUrl)
            }

            emails.Add(newEmail)
        Next

        Return Await SendEmailsAsync(emails.ToArray()).ConfigureAwait(False)
    End Function

    Private Function EmailBody(feeYear As Integer, deadline As String, gecoUrl As String) As String
        Return $"Dear Sir/Madam:

This letter is a notification that you must complete the Georgia Air Emission Fees Reporting form for the {feeYear} calendar year. You must complete and submit the Emission Fees Reporting form, even if no fees are due. For the {feeYear} calendar year, you should report your respective fee amount via an ""online fee form."" This online form will eliminate the need to send paper forms and allow immediate reporting. Additionally, this online form has features to check the data, reducing the need to contact you for additional or corrected information.

You must complete your online Emission Fees Reporting form by {deadline}. You must still complete the online form if your facility did not operate during the calendar year. 

Please refer to the following links for information and instructions on accessing the forms:

    * GECO Registration and Information: {gecoUrl}files/Generating-an-Invoice-on-GECO.pdf
    * Generating an Invoice on GECO: {gecoUrl}files/GECO-Information-Sheet.pdf

Sincerely,
Lydia Davis
Fee Unit Manager
Financial Unit
"
    End Function

End Class