﻿Imports Oracle.ManagedDataAccess.Client

Public Class PASPFeesLog
    Dim SQL As String
    Dim ds As DataSet
    Dim da As OracleDataAdapter

    Private Sub PASPFeesLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            LoadDefaults()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadDefaults()
        Try

            ' clbFeeYear.Items.Add(Now.Year)

            SQL = "Select " &
            "extract(year from sysdate) as FeeYear from dual " &
            "union " &
            "Select " &
            "distinct(numFeeYear) as FeeYear " &
            "From AIRBRANCH.FS_Admin order by FeeYear Desc "


            SQL = "Select " &
              "distinct(numFeeYear) as FeeYear " &
              "From AIRBRANCH.FS_Admin order by FeeYear Desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FeeYear")) Then
                    clbFeeYear.Items.Add("")
                Else
                    If clbFeeYear.Items.Contains(dr.Item("FeeYear")) Then
                    Else
                        clbFeeYear.Items.Add(dr.Item("FeeYear"))
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub RunSearch()
        Try
            Dim FeeYearSQL As String = " "
            Dim OpStatus As String = ""
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim CollectionStatus As String = ""
            Dim ShutDownBetween As String = ""

            For y As Integer = 0 To clbFeeYear.Items.Count - 1
                If clbFeeYear.GetItemChecked(y) = True Then
                    clbFeeYear.SelectedIndex = y
                    FeeYearSQL = FeeYearSQL & " AIRBRANCH.FS_Admin.numFeeYear = '" & clbFeeYear.Items(y).ToString & "' or "
                End If
            Next
            If FeeYearSQL <> " " Then
                FeeYearSQL = " and (" & Mid(FeeYearSQL, 1, (FeeYearSQL.Length) - 3) & " ) "
            End If
            If chbOperating.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'O' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'O' "
                End If
            End If
            If chbClosed.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'X' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'X' "
                End If
            End If
            If chbPlanned.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'P' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'P' "
                End If
            End If
            If chbConstruction.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'C' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'C' "
                End If
            End If
            If chbSeasonal.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'I' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'I' "
                End If
            End If
            If chbTempClosed.Checked = True Then
                If OpStatus = "" Then
                    OpStatus = " stroperationalstatus = 'T' "
                Else
                    OpStatus = OpStatus & " or stroperationalstatus = 'T' "
                End If
            End If
            If OpStatus <> "" Then
                OpStatus = " and ( " & OpStatus & " ) "
            End If
            If mtbSearchAirsNumber.Text <> "" Then
                AIRSNumber = " and AIRBRANCH.APBFacilityInformation.strAIRSNumber like '%" & mtbSearchAirsNumber.Text & "%' "
            End If
            If txtSearchFacilityName.Text <> "" Then
                FacilityName = " and AIRBRANCH.APBFacilityInformation.strFacilityName like '%" & txtSearchFacilityName.Text & "%' "
            End If
            If chbOwesFees.Checked = True Then
                CollectionStatus = " and numCurrentStatus < 10 "
            End If
            If chbShutdown.Checked = True Then
                ShutDownBetween = " and datShutDownDate between '" & dtpStartShutDown.Text & "' and '" & dtpEndShutDown.Text & "' "
            End If

            If txtInvoice.Text = "" Then
                SQL = "select " &
                "substr(AIRBRANCH.FS_Admin.strAIRSnumber, 5) as AIRSNumber, " &
                "AIRBRANCH.APBFacilityInformation.strFacilityName, " &
                "AIRBRANCH.FS_Admin.numFeeYear, " &
                "stroperationalstatus,  " &
                "case " &
                "when stroperationalstatus <> 'O' then datShutDownDate " &
                "else null " &
                "End ShutDownDate, strIAIPDesc " &
                "from AIRBRANCH.FS_Admin, AIRBRANCH.APBFacilityInformation, " &
                "AIRBRANCH.APBHeaderData, AIRBRANCH.FSLK_Admin_Status " &
                "where AIRBRANCH.FS_Admin.strAIRSnumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " &
                "and AIRBRANCH.APBFacilityInformation.strAIRSnumber = AIRBRANCH.APBHeaderData.strAIRSNumber " &
                "and AIRBRANCH.FS_Admin.numCurrentStatus = AIRBRANCH.FSLK_Admin_Status.ID (+) " &
                FeeYearSQL & OpStatus & AIRSNumber & FacilityName & CollectionStatus & ShutDownBetween &
                "order by AIRSnumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                da.Fill(ds, "Fee_Admin")
                dgvExistingYearAdmin.DataSource = ds
                dgvExistingYearAdmin.DataMember = "Fee_Admin"

                dgvExistingYearAdmin.RowHeadersVisible = False
                dgvExistingYearAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvExistingYearAdmin.AllowUserToResizeColumns = True
                dgvExistingYearAdmin.AllowUserToAddRows = False
                dgvExistingYearAdmin.AllowUserToDeleteRows = False
                dgvExistingYearAdmin.AllowUserToOrderColumns = True
                dgvExistingYearAdmin.AllowUserToResizeRows = True
                dgvExistingYearAdmin.Columns("numFeeYear").HeaderText = "Year"
                dgvExistingYearAdmin.Columns("numFeeYear").DisplayIndex = 0
                dgvExistingYearAdmin.Columns("numFeeYear").Width = (dgvExistingYearAdmin.Width * 0.1)
                dgvExistingYearAdmin.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvExistingYearAdmin.Columns("AIRSNumber").DisplayIndex = 1
                dgvExistingYearAdmin.Columns("AIRSNumber").Width = (dgvExistingYearAdmin.Width * 0.15)
                dgvExistingYearAdmin.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvExistingYearAdmin.Columns("strFacilityName").DisplayIndex = 2
                dgvExistingYearAdmin.Columns("strFacilityName").Width = (dgvExistingYearAdmin.Width * 0.5)
                dgvExistingYearAdmin.Columns("stroperationalstatus").HeaderText = "Op. Status"
                dgvExistingYearAdmin.Columns("stroperationalstatus").DisplayIndex = 3
                dgvExistingYearAdmin.Columns("stroperationalstatus").Width = (dgvExistingYearAdmin.Width * 0.1)
                dgvExistingYearAdmin.Columns("ShutDownDate").HeaderText = "Shut Down Date"
                dgvExistingYearAdmin.Columns("ShutDownDate").DisplayIndex = 4
                dgvExistingYearAdmin.Columns("ShutDownDate").Width = (dgvExistingYearAdmin.Width * 0.2)
                dgvExistingYearAdmin.Columns("ShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            Else
                SQL = "select " &
                    "substr(AIRBRANCH.FS_Admin.strAIRSnumber, 5) as AIRSNumber, " &
                    "AIRBRANCH.APBFacilityInformation.strFacilityName, " &
                    "AIRBRANCH.FS_Admin.numFeeYear, " &
                    "InvoiceID, " &
                    "stroperationalstatus,  " &
                    "case " &
                    "when stroperationalstatus <> 'O' then datShutDownDate " &
                    "else null " &
                    "End ShutDownDate, strIAIPDesc " &
                    "from AIRBRANCH.FS_Admin, AIRBRANCH.APBFacilityInformation, " &
                    "AIRBRANCH.APBHeaderData, AIRBRANCH.FSLK_Admin_Status, " &
                    "AIRBRANCH.FS_FeeInvoice " &
                    "where AIRBRANCH.FS_Admin.strAIRSnumber = AIRBRANCH.APBFacilityInformation.strAIRSNumber " &
                    "and AIRBRANCH.APBFacilityInformation.strAIRSnumber = AIRBRANCH.APBHeaderData.strAIRSNumber " &
                    "and AIRBRANCH.FS_Admin.strAIRSNumber = AIRBRANCH.FS_FeeInvoice.strAIRSNumber " &
                    "and AIRBRANCH.FS_Admin.numFeeYear = AIRBRANCH.FS_FeeInvoice.numFeeYear " &
                    "and AIRBRANCH.FS_Admin.numCurrentStatus = AIRBRANCH.FSLK_Admin_Status.ID (+) " &
                    FeeYearSQL & OpStatus & AIRSNumber & FacilityName & CollectionStatus & ShutDownBetween &
                    "order by AIRSnumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                da.Fill(ds, "Fee_Admin")
                dgvExistingYearAdmin.DataSource = ds
                dgvExistingYearAdmin.DataMember = "Fee_Admin"

                dgvExistingYearAdmin.RowHeadersVisible = False
                dgvExistingYearAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvExistingYearAdmin.AllowUserToResizeColumns = True
                dgvExistingYearAdmin.AllowUserToAddRows = False
                dgvExistingYearAdmin.AllowUserToDeleteRows = False
                dgvExistingYearAdmin.AllowUserToOrderColumns = True
                dgvExistingYearAdmin.AllowUserToResizeRows = True
                dgvExistingYearAdmin.Columns("numFeeYear").HeaderText = "Year"
                dgvExistingYearAdmin.Columns("numFeeYear").DisplayIndex = 0
                dgvExistingYearAdmin.Columns("numFeeYear").Width = (dgvExistingYearAdmin.Width * 0.1)
                dgvExistingYearAdmin.Columns("AIRSNumber").HeaderText = "AIRS #"
                dgvExistingYearAdmin.Columns("AIRSNumber").DisplayIndex = 1
                dgvExistingYearAdmin.Columns("AIRSNumber").Width = (dgvExistingYearAdmin.Width * 0.15)
                dgvExistingYearAdmin.Columns("InvoiceID").HeaderText = "AIRS #"
                dgvExistingYearAdmin.Columns("InvoiceID").DisplayIndex = 3
                dgvExistingYearAdmin.Columns("InvoiceID").Width = (dgvExistingYearAdmin.Width * 0.15)
                dgvExistingYearAdmin.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvExistingYearAdmin.Columns("strFacilityName").DisplayIndex = 2
                dgvExistingYearAdmin.Columns("strFacilityName").Width = (dgvExistingYearAdmin.Width * 0.5)
                dgvExistingYearAdmin.Columns("stroperationalstatus").HeaderText = "Op. Status"
                dgvExistingYearAdmin.Columns("stroperationalstatus").DisplayIndex = 4
                dgvExistingYearAdmin.Columns("stroperationalstatus").Width = (dgvExistingYearAdmin.Width * 0.1)
                dgvExistingYearAdmin.Columns("ShutDownDate").HeaderText = "Shut Down Date"
                dgvExistingYearAdmin.Columns("ShutDownDate").DisplayIndex = 5
                dgvExistingYearAdmin.Columns("ShutDownDate").Width = (dgvExistingYearAdmin.Width * 0.2)
                dgvExistingYearAdmin.Columns("ShutDownDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            End If

            txtResultsCount.Text = dgvExistingYearAdmin.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRunFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunFilter.Click
        Try
            RunSearch()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvExistingYearAdmin_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvExistingYearAdmin.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvExistingYearAdmin.HitTest(e.X, e.Y)

        Try

            If dgvExistingYearAdmin.RowCount > 0 And hti.RowIndex <> -1 Then
                mtbSelectedAIRSNumber.Text = dgvExistingYearAdmin(0, hti.RowIndex).Value
                txtSelectedFacilityName.Text = dgvExistingYearAdmin(1, hti.RowIndex).Value
                mtbSelectedFeeYear.Text = dgvExistingYearAdmin(2, hti.RowIndex).Value

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnOpenFeeWorkTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFeeWorkTool.Click
        Dim parameters As New Generic.Dictionary(Of BaseForm.FormParameter, String)
        If Apb.ApbFacilityId.IsValidAirsNumberFormat(mtbSelectedAIRSNumber.Text) Then
            parameters(FormParameter.AirsNumber) = mtbSelectedAIRSNumber.Text
        End If
        parameters(FormParameter.FeeYear) = mtbSelectedFeeYear.Text

        OpenSingleForm(PASPFeeAuditLog, parameters:=parameters, closeFirst:=True)
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        dgvExistingYearAdmin.ExportToExcel(Me)
    End Sub
End Class