Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text

Public Class PASPFeeManagement

    Private Sub PASPFeeManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadFeeRates()
            LoadNSPSExemptions()
            LoadNSPSExemptions2("1")
            LoadFeeYears()
            LoadSelectedNSPSExemptions()

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
            dgvFeeRates.AllowUserToResizeRows = False

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadFeeYears()
        Dim allFeeYears As List(Of Integer) = DAL.GetAllFeeYears()
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearFeeData_Click(sender As Object, e As EventArgs) Handles btnClearFeeData.Click
        ClearFeeData()
    End Sub

    Private Sub dgvFeeRates_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFeeRates.SelectionChanged
        If dgvFeeRates.SelectedRows.Count = 1 Then
            Dim row As DataGridViewRow = dgvFeeRates.CurrentRow

            ClearFeeData()
            If IsDBNull(row.Cells(0).Value) Then
                Return
            Else
                txtFeeID.Text = row.Cells(0).Value.ToString
            End If
            If IsDBNull(row.Cells(1).Value) Then
                txtFeeYear.Clear()
            Else
                txtFeeYear.Text = row.Cells(1).Value.ToString
            End If
            If IsDBNull(row.Cells(2).Value) Then
                dtpFeePeriodStart.Value = Today
            Else
                dtpFeePeriodStart.Text = row.Cells(2).Value.ToString
            End If
            If IsDBNull(row.Cells(3).Value) Then
                dtpFeePeriodEnd.Value = Today
            Else
                dtpFeePeriodEnd.Text = row.Cells(3).Value.ToString
            End If
            If IsDBNull(row.Cells(4).Value) Then
                txtTitleVfee.Clear()
            Else
                txtTitleVfee.Text = row.Cells(4).Value.ToString
            End If
            If IsDBNull(row.Cells(5).Value) Then
                txtAnnualSMFee.Clear()
            Else
                txtAnnualSMFee.Text = row.Cells(5).Value.ToString
            End If
            If IsDBNull(row.Cells(7).Value) Then
                txtAnnualNSPSFee.Clear()
            Else
                txtAnnualNSPSFee.Text = row.Cells(7).Value.ToString
            End If
            If IsDBNull(row.Cells(6).Value) Then
                txtperTonRate.Clear()
            Else
                txtperTonRate.Text = row.Cells(6).Value.ToString
            End If
            If IsDBNull(row.Cells(16).Value) Then
                txtAttainmentThreshold.Clear()
            Else
                txtAttainmentThreshold.Text = row.Cells(16).Value.ToString
            End If
            If IsDBNull(row.Cells(17).Value) Then
                txtNonAttainmentThreshold.Clear()
            Else
                txtNonAttainmentThreshold.Text = row.Cells(17).Value.ToString
            End If

            If IsDBNull(row.Cells(8).Value) Then
                dtpFeeDueDate.Value = Today
            Else
                dtpFeeDueDate.Text = row.Cells(8).Value.ToString
            End If
            If IsDBNull(row.Cells(9).Value) Then
                txtAdminFeePercent.Clear()
            Else
                txtAdminFeePercent.Text = row.Cells(9).Value.ToString
            End If
            If IsDBNull(row.Cells(10).Value) Then
                dtpAdminApplicable.Value = Today
            Else
                dtpAdminApplicable.Text = row.Cells(10).Value.ToString
            End If

            If IsDBNull(row.Cells(11).Value) Then
                dtpFirstQrtDue.Value = Today
            Else
                dtpFirstQrtDue.Text = row.Cells(11).Value.ToString
            End If
            If IsDBNull(row.Cells(12).Value) Then
                dtpSecondQrtDue.Value = Today
            Else
                dtpSecondQrtDue.Text = row.Cells(12).Value.ToString
            End If
            If IsDBNull(row.Cells(13).Value) Then
                dtpThirdQrtDue.Value = Today
            Else
                dtpThirdQrtDue.Text = row.Cells(13).Value.ToString
            End If
            If IsDBNull(row.Cells(14).Value) Then
                dtpFourthQrtDue.Value = Today
            Else
                dtpFourthQrtDue.Text = row.Cells(14).Value.ToString
            End If

            If IsDBNull(row.Cells(15).Value) Then
                txtFeeNotes.Clear()
            Else
                txtFeeNotes.Text = row.Cells(15).Value.ToString
            End If
        End If
    End Sub

    Private Sub btnUpdateFeeData_Click(sender As Object, e As EventArgs) Handles btnUpdateFeeData.Click
        Try
            If txtFeeID.Text <> "" Then
                If Update_FS_FeeRate(txtFeeID.Text, txtFeeYear.Text, dtpFeePeriodStart.Text, dtpFeePeriodEnd.Text,
                                         txtTitleVfee.Text, txtAnnualSMFee.Text, txtperTonRate.Text, txtAnnualNSPSFee.Text,
                                         dtpFeeDueDate.Text, txtAdminFeePercent.Text, dtpAdminApplicable.Text,
                                         txtFeeNotes.Text, "1", dtpFirstQrtDue.Text, dtpSecondQrtDue.Text,
                                         dtpThirdQrtDue.Text, dtpFourthQrtDue.Text, txtAttainmentThreshold.Text,
                                         txtNonAttainmentThreshold.Text) Then

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
                NSPStemp = System.Text.RegularExpressions.Regex.Replace(NSPStemp, rgxPattern, "")

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
        If cboNSPSExemptionYear.Text <> "" Then
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
        Try
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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUpdateNSPSbyYear_Click(sender As Object, e As EventArgs) Handles btnUpdateNSPSbyYear.Click
        If cboNSPSExemptionYear.Text = "" OrElse Not IsNumeric(cboNSPSExemptionYear.Text) Then
            MessageBox.Show("Please select a Fee Year first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    New SqlParameter("@year", cint(cboNSPSExemptionYear.Text)),
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
            "datInitialEnrollment = datEnrollment " &
            "where numFeeYear = @year " &
            "and datInitialEnrollment is null " &
            "and ACTIVE = '1' "
            DB.RunCommand(SQL, p)

            Dim p3 As SqlParameter() = {
                    New SqlParameter("@FeeYear", SqlDbType.Decimal) With {.Value = cboAvailableFeeYears.Text},
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
            Dim SQL As String

            If cboAvailableFeeYears.Text = "" Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Select a fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            If CInt(cboAvailableFeeYears.Text) < (Today.Year - 1) Then
                MsgBox("NO FACILITIES ENROLLED." & vbCrLf & "Only Current and last Fee Years are eligible to be unenrolled.",
                        MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you positive you want to reset enrollment for this year?",
              Me.Text, MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case DialogResult.Yes

                Case Else
                    Return
            End Select

            SQL = "Update FS_Admin set " &
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
            dgvFeeManagementLists.AllowUserToResizeRows = False

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
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
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

            If cboAvailableFeeYears.Text = "" OrElse Not IsNumeric(cboAvailableFeeYears.Text) Then
                MsgBox("Select a valid fee year first.", MsgBoxStyle.Exclamation, Me.Text)
                Return
            End If

            Dim SQL As String = "select count(*) as ContactTotals " &
                "from FS_MailOut " &
                "where numfeeyear = @year "
            Dim p As New SqlParameter("@year", cboAvailableFeeYears.Text)
            Dim dr As DataRow = DB.GetDataRow(SQL, p)
            Dim temp As Integer = 0

            If dr IsNot Nothing AndAlso Not IsDBNull(dr.Item("ContactTotals")) Then
                temp = CInt(dr.Item("ContactTotals"))
            End If

            If temp < 1 Then
                Dim p2 As SqlParameter() = {
                    New SqlParameter("@FeeYear", SqlDbType.Decimal) With {.Value = cboAvailableFeeYears.Text},
                    New SqlParameter("@AIRSNumber", "")
                }
                DB.SPRunCommand("dbo.PD_FEE_MAILOUT", p2)
                DB.SPRunCommand("dbo.PD_FEE_DATA", p2)

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
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
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
            "Are you sure you want to proceed? ",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

        If confirm = DialogResult.No Then
            Return
        End If


        Dim SelectedYear As Integer

        If Not Integer.TryParse(cboAvailableFeeYears.Text, SelectedYear) Then
            MessageBox.Show("Invalid year selected")
            Return
        End If

        Cursor = Cursors.WaitCursor

        Dim query As String = " update FS_MAILOUT " &
            " set STRFIRSTNAME       = c.STRCONTACTFIRSTNAME, " &
            "     STRLASTNAME        = c.STRCONTACTLASTNAME, " &
            "     STRPREFIX          = c.STRCONTACTPREFIX, " &
            "     STRTITLE           = c.STRCONTACTSUFFIX, " &
            "     STRCONTACTCONAME   = left(c.STRCONTACTCOMPANYNAME, 80), " &
            "     STRCONTACTADDRESS1 = c.STRCONTACTADDRESS1, " &
            "     STRCONTACTADDRESS2 = left(c.STRCONTACTADDRESS2, 50), " &
            "     STRCONTACTCITY     = c.STRCONTACTCITY, " &
            "     STRCONTACTSTATE    = c.STRCONTACTSTATE, " &
            "     STRCONTACTZIPCODE  = c.STRCONTACTZIPCODE " &
            " from FS_MAILOUT m " &
            "     inner join APBCONTACTINFORMATION c " &
            "         on c.STRAIRSNUMBER = m.STRAIRSNUMBER " &
            "            and c.STRKEY = '40' " &
            " where m.NUMFEEYEAR = @year "

        Dim param As New SqlParameter("@year", SelectedYear)

        Dim rowsaffected As Integer = 0

        Try
            DB.RunCommand(query, param, rowsaffected)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            Cursor = Nothing
        End Try

        MessageBox.Show(rowsaffected.ToString & " facilities updated.")

        ViewMailOut()
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvFeeManagementLists.ExportToExcel(Me)
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
            dgvFeeManagementLists.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code - Current Data"
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
            Return
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

    Private Sub cboAvailableFeeYears_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAvailableFeeYears.SelectedIndexChanged
        If cboAvailableFeeYears.SelectedIndex > 0 Then
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
            If Not IsNumeric(FeeRateID) OrElse
                Not IsNumeric(FeeYear) OrElse
                Not IsNumeric(Part70Fee) OrElse
                Not IsNumeric(SMFee) OrElse
                Not IsNumeric(PerTonRate) OrElse
                Not IsNumeric(NSPSFee) OrElse
                Not IsNumeric(AdminFee) OrElse
                Not IsNumeric(AAThres) OrElse
                Not IsNumeric(NAThres) Then
                Return False
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
            Return False
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

End Class