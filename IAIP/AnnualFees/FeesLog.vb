Public Class FeesLog

    Private Sub PASPFeesLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFeeYears()
    End Sub

    Private Sub LoadFeeYears()
        clbFeeYear.DataSource = DAL.GetAllFeeYears()
        cbYear.DataSource = DAL.GetAllFeeYears()
    End Sub

    Private Sub RunSearch()
        Try
            Dim SQL As String
            Dim FeeYearSqlSb As New Text.StringBuilder()
            Dim OpStatus As String = ""
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim CollectionStatus As String = ""
            Dim ShutDownBetween As String = ""

            For y As Integer = 0 To clbFeeYear.Items.Count - 1
                If clbFeeYear.GetItemChecked(y) Then
                    clbFeeYear.SelectedIndex = y
                    FeeYearSqlSb.Append(" a.numFeeYear = '" & clbFeeYear.Items(y).ToString & "' or ")
                End If
            Next
            Dim FeeYearSQL As String = FeeYearSqlSb.ToString
            If FeeYearSQL <> "" Then
                FeeYearSQL = " (" & Mid(FeeYearSQL, 1, (FeeYearSQL.Length) - 3) & " ) "
            End If

            If chbOperating.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'O' "
                Else
                    OpStatus &= " or stroperationalstatus = 'O' "
                End If
            End If
            If chbClosed.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'X' "
                Else
                    OpStatus &= " or stroperationalstatus = 'X' "
                End If
            End If
            If chbPlanned.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'P' "
                Else
                    OpStatus &= " or stroperationalstatus = 'P' "
                End If
            End If
            If chbConstruction.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'C' "
                Else
                    OpStatus &= " or stroperationalstatus = 'C' "
                End If
            End If
            If chbSeasonal.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'I' "
                Else
                    OpStatus &= " or stroperationalstatus = 'I' "
                End If
            End If
            If chbTempClosed.Checked Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'T' "
                Else
                    OpStatus &= " or stroperationalstatus = 'T' "
                End If
            End If
            If OpStatus <> "" Then
                OpStatus = " ( " & OpStatus & " ) "
            End If
            If mtbSearchAirsNumber.Text <> "" Then
                AIRSNumber = " a.strAIRSNumber like '%" & SqlQuote(mtbSearchAirsNumber.Text) & "%' "
            End If
            If txtSearchFacilityName.Text <> "" Then
                FacilityName = " f.strFacilityName like '%" & SqlQuote(txtSearchFacilityName.Text) & "%' "
            End If
            If chbOwesFees.Checked Then
                CollectionStatus = " numCurrentStatus < 10 "
            End If
            If chbShutdown.Checked Then
                ShutDownBetween = " datShutDownDate between '" & dtpStartShutDown.Text & "' and '" & dtpEndShutDown.Text & "' "
            End If

            Dim whereClause As String = ConcatNonEmptyStrings(" and ", {FeeYearSQL, OpStatus, AIRSNumber, FacilityName, CollectionStatus, ShutDownBetween})

            If whereClause <> "" Then whereClause = " WHERE " & whereClause

            If Not chbShowInvoices.Checked Then
                SQL = "SELECT SUBSTRING(a.STRAIRSNUMBER, 5, 8) AS AIRSNumber, f.STRFACILITYNAME, a.NUMFEEYEAR, h.STROPERATIONALSTATUS, " &
                    " CASE WHEN h.STROPERATIONALSTATUS <> 'O' THEN h.DATSHUTDOWNDATE ELSE NULL END AS ShutDownDate, s.STRIAIPDESC " &
                    " FROM FS_Admin AS a " &
                    " INNER JOIN APBFacilityInformation AS f ON f.STRAIRSNUMBER = a.STRAIRSNUMBER " &
                    " INNER JOIN APBHeaderData AS h ON h.STRAIRSNUMBER = f.STRAIRSNUMBER " &
                    " LEFT JOIN FSLK_Admin_Status AS s ON a.NUMCURRENTSTATUS = s.ID " &
                    whereClause &
                    " ORDER BY AIRSNumber "

                dgvExistingYearAdmin.DataSource = DB.GetDataTable(SQL)
                dgvExistingYearAdmin.Columns("numFeeYear").HeaderText = "Year"
                dgvExistingYearAdmin.Columns("numFeeYear").DisplayIndex = 0
                dgvExistingYearAdmin.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvExistingYearAdmin.Columns("AIRSNumber").DisplayIndex = 1
                dgvExistingYearAdmin.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvExistingYearAdmin.Columns("strFacilityName").DisplayIndex = 2
                dgvExistingYearAdmin.Columns("stroperationalstatus").HeaderText = "Op. Status"
                dgvExistingYearAdmin.Columns("stroperationalstatus").DisplayIndex = 3
                dgvExistingYearAdmin.Columns("ShutDownDate").HeaderText = "Shut Down Date"
                dgvExistingYearAdmin.Columns("ShutDownDate").DisplayIndex = 4
                dgvExistingYearAdmin.Columns("ShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvExistingYearAdmin.Columns("STRIAIPDESC").HeaderText = "Description"
            Else
                SQL = "SELECT SUBSTRING(a.STRAIRSNUMBER, 5, 12) AS AIRSNumber, f.STRFACILITYNAME, a.NUMFEEYEAR, i.INVOICEID, " & "h.STROPERATIONALSTATUS, " &
                    "CASE WHEN h.STROPERATIONALSTATUS <> 'O' THEN h.DATSHUTDOWNDATE ELSE NULL END AS ShutDownDate, k.STRIAIPDESC " &
                    "FROM FS_Admin AS a " &
                    "INNER JOIN FS_FeeInvoice AS i ON i.STRAIRSNUMBER = a.STRAIRSNUMBER AND i.NUMFEEYEAR = a.NUMFEEYEAR " &
                    "LEFT JOIN FSLK_Admin_Status AS k ON a.NUMCURRENTSTATUS = k.ID " &
                    "INNER JOIN APBFacilityInformation AS f ON a.STRAIRSNUMBER = f.STRAIRSNUMBER " &
                    "INNER JOIN APBHeaderData AS h ON f.STRAIRSNUMBER = h.STRAIRSNUMBER " &
                    whereClause &
                    "ORDER BY AIRSNumber "

                dgvExistingYearAdmin.DataSource = DB.GetDataTable(SQL)
                dgvExistingYearAdmin.Columns("numFeeYear").HeaderText = "Year"
                dgvExistingYearAdmin.Columns("numFeeYear").DisplayIndex = 0
                dgvExistingYearAdmin.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvExistingYearAdmin.Columns("AIRSNumber").DisplayIndex = 1
                dgvExistingYearAdmin.Columns("InvoiceID").HeaderText = "Invoice #"
                dgvExistingYearAdmin.Columns("InvoiceID").DisplayIndex = 3
                dgvExistingYearAdmin.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvExistingYearAdmin.Columns("strFacilityName").DisplayIndex = 2
                dgvExistingYearAdmin.Columns("stroperationalstatus").HeaderText = "Op. Status"
                dgvExistingYearAdmin.Columns("stroperationalstatus").DisplayIndex = 4
                dgvExistingYearAdmin.Columns("ShutDownDate").HeaderText = "Shut Down Date"
                dgvExistingYearAdmin.Columns("ShutDownDate").DisplayIndex = 5
                dgvExistingYearAdmin.Columns("ShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvExistingYearAdmin.Columns("STRIAIPDESC").HeaderText = "Description"
            End If


            dgvExistingYearAdmin.RowHeadersVisible = False
            dgvExistingYearAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvExistingYearAdmin.AllowUserToResizeColumns = True
            dgvExistingYearAdmin.AllowUserToAddRows = False
            dgvExistingYearAdmin.AllowUserToDeleteRows = False
            dgvExistingYearAdmin.AllowUserToOrderColumns = True
            dgvExistingYearAdmin.AllowUserToResizeRows = True
            dgvExistingYearAdmin.SanelyResizeColumns()

            txtResultsCount.Text = dgvExistingYearAdmin.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunFilter_Click(sender As Object, e As EventArgs) Handles btnRunFilter.Click
        RunSearch()
    End Sub

    Private Sub dgvExistingYearAdmin_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvExistingYearAdmin.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvExistingYearAdmin.HitTest(e.X, e.Y)

        Try

            If dgvExistingYearAdmin.RowCount > 0 AndAlso hti.RowIndex <> -1 Then
                mtbSelectedAIRSNumber.Text = dgvExistingYearAdmin(0, hti.RowIndex).Value.ToString
                cbYear.Text = dgvExistingYearAdmin(2, hti.RowIndex).Value.ToString
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnOpenFeeWorkTool_Click(sender As Object, e As EventArgs) Handles btnOpenFeeWorkTool.Click
        Dim parameters As New Generic.Dictionary(Of FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbSelectedAIRSNumber.Text) Then
            parameters(FormParameter.AirsNumber) = mtbSelectedAIRSNumber.Text
        End If
        parameters(FormParameter.FeeYear) = cbYear.Text

        OpenSingleForm(FeesAudit, parameters:=parameters, closeFirst:=True)
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        dgvExistingYearAdmin.ExportToExcel(Me)
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter
        AcceptButton = btnRunFilter
    End Sub

    Private Sub GroupBox1_Leave(sender As Object, e As EventArgs) Handles GroupBox1.Leave
        AcceptButton = Nothing
    End Sub

    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter
        AcceptButton = btnOpenFeeWorkTool
    End Sub

    Private Sub GroupBox5_Leave(sender As Object, e As EventArgs) Handles GroupBox5.Leave
        AcceptButton = Nothing
    End Sub
End Class