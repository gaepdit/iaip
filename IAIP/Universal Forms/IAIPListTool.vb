Imports System.Data.SqlClient

Public Class IAIPListTool
    Private formAccessText As String = ""
    Private dtOrganization As DataTable
    Private loading As Boolean = False

#Region "Page Load"

    Private Sub IAIPListTool_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnAddBranch.Enabled = False
        btnEditBranch.Enabled = False
        btnAddProgram.Enabled = False
        btnEditProgram.Enabled = False
        btnAddUnit.Enabled = False
        btnEditUnit.Enabled = False
        btnAddAccount.Enabled = False
        btnEditAccount.Enabled = False

        FormatDataGridView(dgvBranch)
        FormatDataGridView(dgvProgram)
        FormatDataGridView(dgvUnit)
        FormatDataGridView(dgvAccounts)
        FormatDataGridView(dgvAvailableForms)
        FormatDataGridView(dgvSelectedForms, False)

        LoadDataSets()
        LoadBranchGrid()
        LoadForms()
    End Sub

    Private Sub LoadDataSets()
        Dim SQL As String = "select " &
            "lookupepdbranches.numBranchCode, strBranchDesc,  " &
            "lookupepdprograms.numProgramCode, strProgramDesc,  " &
            "lookupepdunits.numUnitCode, strUnitdesc " &
            "from Lookupepdbranches " &
            "left join lookupepdprograms  " &
            "on lookupepdbranches.numbranchcode = lookupepdprograms.numbranchcode " &
            "inner join lookupepdunits " &
            "on lookupepdprograms.numprogramcode = lookupepdunits.numprogramcode " &
            "order by strbranchdesc, strProgramDesc, strUnitDesc "

        dtOrganization = DB.GetDataTable(SQL)
    End Sub

    Private Sub LoadBranchGrid()
        Try
            Dim SQL As String = "Select " &
            "numBranchCode, strBranchDesc " &
            "from LookUpEPDBranches " &
            "order by strBranchDesc "

            Dim dt As DataTable = DB.GetDataTable(SQL)
            dgvBranch.DataSource = dt
            dgvBranch.Columns("numBranchCode").HeaderText = "ID #"
            dgvBranch.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvBranch.SanelyResizeColumns

            btnAddBranch.Enabled = True

            With cboBranch
                .DisplayMember = "strBranchDesc"
                .ValueMember = "numBranchCode"
                .DataSource = dt.Copy
                .SelectedIndex = -1
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadForms()
        Try
            Dim SQL As String = "Select " &
            "numFormCode, strForm, " &
            "strFormDesc " &
            "from LookUpIAIPForms "

            dgvAvailableForms.DataSource = DB.GetDataTable(SQL)
            dgvAvailableForms.Columns("numFormCode").HeaderText = "Form #"
            dgvAvailableForms.Columns("numFormCode").Visible = False
            dgvAvailableForms.Columns("strForm").HeaderText = "Form"
            dgvAvailableForms.Columns("strForm").ReadOnly = True
            dgvAvailableForms.Columns("strFormDesc").HeaderText = "Form Description"
            dgvAvailableForms.Columns("strFormDesc").ReadOnly = True
            dgvAvailableForms.SanelyResizeColumns

            dgvSelectedForms.Columns.Add("numFormCode", "Form #")
            dgvSelectedForms.Columns("numFormCode").Visible = False
            dgvSelectedForms.Columns.Add("strForm", "Form")
            dgvSelectedForms.Columns.Add("strFormDesc", "Form Description")

            Dim colReadOnly As New DataGridViewCheckBoxColumn
            dgvSelectedForms.Columns.Add(colReadOnly)
            dgvSelectedForms.Columns(3).HeaderText = "Read Only"

            Dim colWrite As New DataGridViewCheckBoxColumn
            dgvSelectedForms.Columns.Add(colWrite)
            dgvSelectedForms.Columns(4).HeaderText = "Write"

            Dim colFullAccess As New DataGridViewCheckBoxColumn
            dgvSelectedForms.Columns.Add(colFullAccess)
            dgvSelectedForms.Columns(5).HeaderText = "Full Access"

            Dim colSpecialPermissions As New DataGridViewCheckBoxColumn
            dgvSelectedForms.Columns.Add(colSpecialPermissions)
            dgvSelectedForms.Columns(6).HeaderText = "Special Permissions"

            dgvSelectedForms.SanelyResizeColumns
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub FormatDataGridView(dgv As DataGridView, Optional makeReadOnly As Boolean = True)
        dgv.RowHeadersVisible = False
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgv.AllowUserToResizeColumns = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToOrderColumns = True
        dgv.AllowUserToResizeRows = True
        dgv.ReadOnly = makeReadOnly
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
    End Sub

#End Region

    Private Sub CheckButtons()
        If txtAccountCode.Text = "" Then
            btnEditAccount.Enabled = False
        Else
            btnEditAccount.Enabled = True
        End If

        If txtUnitCode.Text = "" Then
            btnEditUnit.Enabled = False
            btnAddAccount.Enabled = False
        Else
            btnEditUnit.Enabled = True
            btnAddAccount.Enabled = True
        End If

        If txtProgramCode.Text = "" Then
            btnEditProgram.Enabled = False
            btnAddUnit.Enabled = False
            btnAddAccount.Enabled = False
        Else
            btnEditProgram.Enabled = True
            btnAddUnit.Enabled = True
            btnAddAccount.Enabled = True
        End If

        If txtBranchCode.Text = "" Then
            btnEditBranch.Enabled = False
            btnAddProgram.Enabled = False
            btnAddAccount.Enabled = False
        Else
            btnEditBranch.Enabled = True
            btnAddProgram.Enabled = True
            btnAddAccount.Enabled = True
        End If
    End Sub

    Private Sub LoadProgramGrid()
        Try
            dgvUnit.DataSource = Nothing
            dgvAccounts.DataSource = Nothing

            Dim SQL As String = "Select " &
            "numProgramCode, strProgramDesc, " &
            "numBranchCode " &
            "from LookUpEPDprograms " &
            "where numBranchCode = @branchcode " &
            "order by strProgramDesc "

            Dim p As New SqlParameter("@branchcode", txtBranchCode.Text)

            dgvProgram.DataSource = DB.GetDataTable(SQL, p)

            dgvProgram.Columns("numProgramCode").HeaderText = "ID #"
            dgvProgram.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvProgram.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvProgram.Columns("numBranchCode").Visible = False
            dgvProgram.SanelyResizeColumns

            SQL = "Select " &
            "numAccountCode, strAccountDesc,  " &
            "LookUpEPDBranches.numBranchCode, strBranchDesc, " &
            "LookUpEPDPrograms.numProgramCode, strProgramDesc,  " &
            "LookUpEPDUnits.numUnitCode, strUnitDesc " &
            "from LookUpIAIPAccounts " &
            "left join LookupEPDBranches " &
            "on LookUpIAIPAccounts.numBranchCode = LookUpEPDBranches.numBranchCode " &
            "left join LookUpEPDPrograms " &
            "on LookUpIAIPAccounts.numProgramCode = LookUpEPDPrograms.numProgramCode " &
            "left join LookUpEPDUnits   " &
            "on LookUpIAIPAccounts.numUnitCode = LookUpEPDUnits.numUnitCode " &
            "where lookupiaipaccounts.numBranchCode = @branchcode " &
            "and LookUpIAIPAccounts.numProgramCode is Null " &
            "and LookUpIAIPAccounts.numUnitCode is Null " &
            "order by strAccountDesc "

            dgvAccounts.DataSource = DB.GetDataTable(SQL, p)

            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("numProgramCode").Visible = False
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.SanelyResizeColumns

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadUnitGrid()
        Try
            dgvAccounts.DataSource = Nothing

            Dim SQL As String = "Select " &
            "numUnitCode, strUnitDesc, " &
            "numProgramCode " &
            "from LookUpEPDUnits " &
            "where numProgramCode = @programcode " &
            "order by strUnitDesc "

            Dim p As New SqlParameter("@programcode", txtProgramCode.Text)

            dgvUnit.DataSource = DB.GetDataTable(SQL, p)
            dgvUnit.Columns("numUnitCode").HeaderText = "ID #"
            dgvUnit.Columns("strUnitDesc").HeaderText = "Unit Desc."
            dgvUnit.Columns("numProgramCode").HeaderText = "Program Code"
            dgvUnit.Columns("numProgramCode").Visible = False
            dgvUnit.SanelyResizeColumns

            SQL = "Select " &
            "numAccountCode, strAccountDesc,  " &
            "LookUpEPDBranches.numBranchCode, strBranchDesc, " &
            "LookUpEPDPrograms.numProgramCode, strProgramDesc,  " &
            "LookUpEPDUnits.numUnitCode, strUnitDesc " &
            "from LookUpIAIPAccounts " &
            "left join LookupEPDBranches " &
            "on LookUpIAIPAccounts.numBranchCode = LookUpEPDBranches.numBranchCode " &
            "left join LookUpEPDPrograms " &
            "on LookUpIAIPAccounts.numProgramCode = LookUpEPDPrograms.numProgramCode " &
            "left join LookUpEPDUnits " &
            "on LookUpIAIPAccounts.numUnitCode = LookUpEPDUnits.numUnitCode " &
            "where lookupiaipaccounts.numProgramCode = @programcode " &
            "and LookUpIAIPAccounts.numUnitCode is Null " &
            "order by strAccountDesc "

            dgvAccounts.DataSource = DB.GetDataTable(SQL, p)
            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.SanelyResizeColumns
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAccountGrid()
        Try
            Dim SQL As String = "Select " &
            "numAccountCode, strAccountDesc,  " &
            "LookUpEPDBranches.numBranchCode, strBranchDesc, " &
            "LookUpEPDPrograms.numProgramCode, strProgramDesc,  " &
            "LookUpEPDUnits.numUnitCode, strUnitDesc " &
            "from LookUpIAIPAccounts " &
            "left join LookupEPDBranches " &
            "on LookUpIAIPAccounts.numBranchCode = LookUpEPDBranches.numBranchCode " &
            "left join LookUpEPDPrograms " &
            "on LookUpIAIPAccounts.numProgramCode = LookUpEPDPrograms.numProgramCode " &
            "left join LookUpEPDUnits   " &
            "on LookUpIAIPAccounts.numUnitCode = LookUpEPDUnits.numUnitCode " &
            "where lookupiaipaccounts.numUnitCode = @unitcode " &
            "order by strAccountDesc "

            Dim p As New SqlParameter("@unitcode", txtUnitCode.Text)

            dgvAccounts.DataSource = DB.GetDataTable(SQL, p)

            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("numProgramCode").Visible = False
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.SanelyResizeColumns
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadProgram(BranchCode As Integer)
        Try
            Dim dvProgram As New DataView(dtOrganization, "numBranchCode = " & BranchCode.ToString, "strProgramDesc", DataViewRowState.CurrentRows)
            Dim dtProgram As DataTable = dvProgram.ToTable(True, {"numProgramCode", "strProgramDesc"})

            With cboProgram
                .Enabled = True
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .DataSource = dtProgram
                .SelectedIndex = -1
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadAccounts(ProgramCode As Integer, BranchCode As Integer)
        Try
            Dim filterString As String = " BranchID = " & BranchCode.ToString

            If ProgramCode > 0 Then
                filterString &= " and ProgramID = " & ProgramCode.ToString
            Else
                filterString &= " and ProgramID is null "
            End If

            Dim dvAccount As New DataView(GetSharedData(SharedTable.IaipAccountRoles), filterString, "Role", DataViewRowState.CurrentRows)
            Dim dtAccount As DataTable = dvAccount.ToTable(True, {"RoleCode", "Role"})

            With lbAccounts
                .DisplayMember = "Role"
                .ValueMember = "RoleCode"
                .DataSource = dtAccount
            End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub UpdateAccount()
        Try
            Dim i As Integer = 0
            Dim temp As String = ""
            Dim SQLLine As String = ""

            formAccessText = ""

            For i = 0 To dgvSelectedForms.Rows.Count - 1
                temp = "(" & dgvSelectedForms(0, i).Value & "-"
                If dgvSelectedForms(3, i).Value = True Then
                    temp = temp & "1,"
                Else
                    temp = temp & "0,"
                End If
                If dgvSelectedForms(4, i).Value = True Then
                    temp = temp & "1,"
                Else
                    temp = temp & "0,"
                End If
                If dgvSelectedForms(5, i).Value = True Then
                    temp = temp & "1,"
                Else
                    temp = temp & "0,"
                End If
                If dgvSelectedForms(6, i).Value = True Then
                    temp = temp & "1"
                Else
                    temp = temp & "0"
                End If
                temp = temp & ")"
                formAccessText = formAccessText & temp
            Next

            Dim SQL As String = "Update LookUpIAIPAccounts set " &
            "strFormAccess = @formAccessText " &
            "where "

            Dim code As Integer

            If chbCascadeBranch.Checked = True Then
                SQLLine = " numBranchcode = @code "
                code = CInt(cboBranch.SelectedValue)
            ElseIf chbCascadeProgram.Checked = True Then
                SQLLine = " numProgramcode = @code "
                code = CInt(cboProgram.SelectedValue)
            ElseIf lbAccounts.SelectedItems.Count = 1 Then
                SQLLine = " numaccountCode = @code "
                code = CInt(lbAccounts.SelectedValue)
            End If

            If SQLLine <> "" Then
                SQL = SQL & SQLLine

                Dim p As SqlParameter() = {
                    New SqlParameter("@formAccessText", formAccessText),
                    New SqlParameter("@code", code)
                }

                DB.RunCommand(SQL, p)

                SharedData.ClearSharedData(SharedTable.IaipAccountRoles)

                MsgBox("Successfully  Done.", MsgBoxStyle.Information, "IAIP List Tool")
            Else
                MsgBox("No data was save. " & vbCrLf & "Select at least one Account or cascade through Branch/Program", MsgBoxStyle.Exclamation, "IAIP List Tool")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub ViewForms()
        Try
            If lbAccounts.SelectedItems.Count <> 1 Then
                Exit Sub
            End If

            Dim temp As String = lbAccounts.SelectedValue
            Dim temp2 As String = ""
            Dim tempForm As String = ""
            Dim dgvRow As New DataGridViewRow

            Dim SQL As String = "Select " &
            "strFormAccess " &
            "from LookUpIAIPAccounts " &
            "where numAccountCode = @code "

            Dim p As New SqlParameter("@code", temp)

            formAccessText = DB.GetString(SQL, p)

            dgvSelectedForms.Rows.Clear()

            If formAccessText <> "" Then
                temp = formAccessText
                Do While temp <> ""
                    tempForm = Mid(temp, 1, InStr(temp, ")", CompareMethod.Text))
                    temp = Replace(temp, tempForm, "")
                    temp2 = Mid(tempForm, 2, InStr(tempForm, "-", CompareMethod.Text) - 2)
                    tempForm = Replace(tempForm, "(" & temp2 & "-", "")


                    Dim x As Integer = 0
                    While x < dgvAvailableForms.Rows.Count
                        Dim y As Integer = 0
                        While y < dgvAvailableForms.Rows(x).Cells.Count
                            Dim c As DataGridViewCell = dgvAvailableForms.Rows(x).Cells(y)
                            If Not c.Value Is DBNull.Value Or Nothing Then
                                If CType(c.Value, String) = temp2 Then
                                    dgvRow = New DataGridViewRow
                                    dgvRow.CreateCells(dgvSelectedForms)
                                    dgvRow.Cells(0).Value = dgvAvailableForms(0, x).Value
                                    dgvRow.Cells(1).Value = dgvAvailableForms(1, x).Value
                                    dgvRow.Cells(2).Value = dgvAvailableForms(2, x).Value
                                    If Mid(tempForm, 1, 1) = "1" Then
                                        dgvRow.Cells(3).Value = True
                                    Else
                                        dgvRow.Cells(3).Value = False
                                    End If
                                    If Mid(tempForm, 3, 1) = "1" Then
                                        dgvRow.Cells(4).Value = True
                                    Else
                                        dgvRow.Cells(4).Value = False
                                    End If
                                    If Mid(tempForm, 5, 1) = "1" Then
                                        dgvRow.Cells(5).Value = True
                                    Else
                                        dgvRow.Cells(5).Value = False
                                    End If
                                    If Mid(tempForm, 7, 1) = "1" Then
                                        dgvRow.Cells(6).Value = True
                                    Else
                                        dgvRow.Cells(6).Value = False
                                    End If
                                    dgvSelectedForms.Rows.Add(dgvRow)
                                End If
                            End If
                            Math.Min(Threading.Interlocked.Increment(y), y - 1)
                        End While
                        Math.Min(Threading.Interlocked.Increment(x), x - 1)
                    End While
                Loop

                lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString
                dgvSelectedForms.SanelyResizeColumns
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvBranch_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvBranch.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvBranch.HitTest(e.X, e.Y)
            If dgvBranch.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvBranch.Columns(0).HeaderText = "ID #" Then
                    If IsDBNull(dgvBranch(0, hti.RowIndex).Value) Then
                        txtBranch.Text = ""
                        txtBranchCode.Text = ""
                    Else
                        txtBranch.Text = dgvBranch(1, hti.RowIndex).Value
                        txtBranchCode.Text = dgvBranch(0, hti.RowIndex).Value
                    End If
                    If txtBranchCode.Text <> "" Then
                        LoadProgramGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvProgram_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvProgram.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvProgram.HitTest(e.X, e.Y)
            If dgvProgram.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvProgram.Columns(0).HeaderText = "ID #" Then
                    If IsDBNull(dgvProgram(0, hti.RowIndex).Value) Then
                        txtProgramCode.Text = ""
                        txtProgram.Text = ""
                    Else
                        txtProgram.Text = dgvProgram(1, hti.RowIndex).Value
                        txtProgramCode.Text = dgvProgram(0, hti.RowIndex).Value
                    End If
                    If txtProgramCode.Text <> "" Then
                        LoadUnitGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvUnit_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvUnit.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvUnit.HitTest(e.X, e.Y)
            If dgvUnit.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvUnit.Columns(0).HeaderText = "ID #" Then
                    If IsDBNull(dgvUnit(0, hti.RowIndex).Value) Then
                        txtUnit.Text = ""
                        txtUnitCode.Text = ""
                    Else
                        txtUnit.Text = dgvUnit(1, hti.RowIndex).Value
                        txtUnitCode.Text = dgvUnit(0, hti.RowIndex).Value
                    End If
                    If txtUnitCode.Text <> "" Then
                        LoadAccountGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub dgvAccounts_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvAccounts.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvAccounts.HitTest(e.X, e.Y)
            If dgvAccounts.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvAccounts.Columns(0).HeaderText = "ID #" Then
                    txtAccount.Text = dgvAccounts(1, hti.RowIndex).Value
                    txtAccountCode.Text = dgvAccounts(0, hti.RowIndex).Value

                    If IsDBNull(dgvAccounts(2, hti.RowIndex).Value) Then
                        txtBranch.Text = ""
                        txtBranchCode.Text = ""
                    Else
                        txtBranch.Text = dgvAccounts(3, hti.RowIndex).Value
                        txtBranchCode.Text = dgvAccounts(2, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvAccounts(4, hti.RowIndex).Value) Then
                        txtProgram.Text = ""
                        txtProgramCode.Text = ""
                    Else
                        txtProgram.Text = dgvAccounts(5, hti.RowIndex).Value
                        txtProgramCode.Text = dgvAccounts(4, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvAccounts(6, hti.RowIndex).Value) Then
                        txtUnit.Text = ""
                        txtUnitCode.Text = ""
                    Else
                        txtUnit.Text = dgvAccounts(7, hti.RowIndex).Value
                        txtUnitCode.Text = dgvAccounts(6, hti.RowIndex).Value
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub txtBranchCode_TextChanged(sender As Object, e As EventArgs) Handles txtBranchCode.TextChanged,
        txtProgramCode.TextChanged, txtUnitCode.TextChanged, txtAccountCode.TextChanged

        CheckButtons()
    End Sub

    Private Sub btnAddBranch_Click(sender As Object, e As EventArgs) Handles btnAddBranch.Click
        Try
            Dim SQL As String = "Insert into LookUpEPDBranches " &
            "(NUMBRANCHCODE, STRBRANCHDESC) " &
            "values " &
            "((select max(numBranchCode) + 1 from LookUpEPDBranches), " &
            "@branch ) "

            Dim p As New SqlParameter("@branch", txtBranch.Text)

            DB.RunCommand(SQL, p)

            SQL = "Select max(numBranchCode) from LookUpEPDBranches "

            txtBranchCode.Text = DB.GetInteger(SQL)

            LoadBranchGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditBranch_Click(sender As Object, e As EventArgs) Handles btnEditBranch.Click
        Try
            If txtBranchCode.Text <> "" Then
                Dim SQL As String = "Update LookUpEPDBranches set " &
                "strBranchDesc = @branch " &
                "where numBranchCode = @branchcode "

                Dim p As SqlParameter() = {
                    New SqlParameter("@branch", txtBranch.Text),
                    New SqlParameter("@branchcode", txtBranchCode.Text)
                }

                DB.RunCommand(SQL, p)

                LoadBranchGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddProgram_Click(sender As Object, e As EventArgs) Handles btnAddProgram.Click
        Try
            If txtBranchCode.Text <> "" Then
                Dim SQL As String = "Insert into LookUpEPDPrograms " &
                "(NUMPROGRAMCODE, STRPROGRAMDESC, NUMBRANCHCODE) " &
                "values " &
                "((select max(numProgramCode) + 1 from LookUpEPDPrograms), " &
                " @program, @branchcode) "

                Dim p As SqlParameter() = {
                    New SqlParameter("@program", txtProgram.Text),
                    New SqlParameter("@branchcode", txtBranchCode.Text)
                }

                DB.RunCommand(SQL, p)

                SQL = "select max(numProgramCode) from LookUpEPDPrograms"

                txtProgramCode.Text = DB.GetInteger(SQL)

                LoadProgramGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditProgram_Click(sender As Object, e As EventArgs) Handles btnEditProgram.Click
        Try
            If txtProgramCode.Text <> "" And txtBranchCode.Text <> "" Then
                Dim SQL As String = "Update LookUpEPDPrograms set " &
                "strProgramDesc = @program, " &
                "numBranchCode = @branchcode " &
                "where numProgramCode = @programcode "

                Dim p As SqlParameter() = {
                    New SqlParameter("@program", txtProgram.Text),
                    New SqlParameter("@branchcode", txtBranchCode.Text),
                    New SqlParameter("@programcode", txtProgramCode.Text)
                }

                DB.RunCommand(SQL, p)

                LoadProgramGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddUnit_Click(sender As Object, e As EventArgs) Handles btnAddUnit.Click
        Try
            If txtProgramCode.Text <> "" Then
                Dim SQL As String = "Insert into LookUpEPDUnits " &
                "(numUnitCode, strUnitDesc, numProgramCode)  " &
                "values  " &
                "((select max(numUnitCode) + 1 From LookUpEPDUnits), " &
                " @unit, @programcode ) "

                Dim p As SqlParameter() = {
                    New SqlParameter("@unit", txtUnit.Text),
                    New SqlParameter("@programcode", txtProgramCode.Text)
                }

                DB.RunCommand(SQL, p)

                SQL = "select max(numUnitCode) from LookUpEPDUnits "

                txtUnitCode.Text = DB.GetInteger(SQL)

                LoadUnitGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditUnit_Click(sender As Object, e As EventArgs) Handles btnEditUnit.Click
        Try
            If txtUnitCode.Text <> "" And txtProgramCode.Text <> "" Then
                Dim SQL As String = "Update LookUpEPDUnits set " &
                "strUnitDesc = @unit " &
                "where numUnitCode = @unitcode "

                Dim p As SqlParameter() = {
                    New SqlParameter("@unit", txtUnit.Text),
                    New SqlParameter("@unitcode", txtUnitCode.Text)
                }

                DB.RunCommand(SQL, p)

                LoadUnitGrid()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnAddAccount_Click(sender As Object, e As EventArgs) Handles btnAddAccount.Click
        Try
            Dim SQL As String = "Insert into LookUpIAIPAccounts " &
            "(NUMACCOUNTCODE, STRACCOUNTDESC, NUMBRANCHCODE, NUMPROGRAMCODE, NUMUNITCODE, STRFORMACCESS) " &
            "values " &
            "((Select (max(numAccountCode) + 1) from LookUpIAIPAccounts), " &
            " @desc, @branchcode, @programcode, @unitcode, null) "

            Dim p As SqlParameter() = {
                New SqlParameter("@desc", txtAccount.Text),
                New SqlParameter("@branchcode", txtBranchCode.Text),
                New SqlParameter("@programcode", txtProgramCode.Text),
                New SqlParameter("@unitcode", txtUnitCode.Text),
                New SqlParameter("@formaccess", txtUnitCode.Text)
            }

            DB.RunCommand(SQL, p)

            SQL = "Select max(numAccountCode) as MaxAccount " &
            "from LookUpIAIPAccounts "
            txtAccountCode.Text = DB.GetInteger(SQL)

            LoadAccountGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnEditAccount_Click(sender As Object, e As EventArgs) Handles btnEditAccount.Click
        Try
            If txtAccountCode.Text <> "" Then
                Dim SQL As String = "Update LookUpIAIPAccounts set " &
                "strAccountDesc = @desc, " &
                "numBranchCode = @branchcode, " &
                "numProgramCode = @programcode, " &
                "numUnitCode = @unitcode " &
                "where numAccountCode = @accountcode "

                Dim p As SqlParameter() = {
                    New SqlParameter("@desc", txtAccount.Text),
                    New SqlParameter("@branchcode", txtBranchCode.Text),
                    New SqlParameter("@programcode", txtProgramCode.Text),
                    New SqlParameter("@unitcode", txtUnitCode.Text),
                    New SqlParameter("@accountcode", txtAccountCode.Text)
                }

                DB.RunCommand(SQL, p)

                LoadAccountGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnClearBranch_Click(sender As Object, e As EventArgs) Handles btnClearBranch.Click
        txtBranch.Clear()
        txtBranchCode.Clear()
    End Sub

    Private Sub btnClearProgram_Click(sender As Object, e As EventArgs) Handles btnClearProgram.Click
        txtProgram.Clear()
        txtProgramCode.Clear()
    End Sub

    Private Sub btnClearUnit_Click(sender As Object, e As EventArgs) Handles btnClearUnit.Click
        txtUnit.Clear()
        txtUnitCode.Clear()
    End Sub

    Private Sub btnClearAccount_Click(sender As Object, e As EventArgs) Handles btnClearAccount.Click
        txtAccount.Clear()
        txtAccountCode.Clear()
    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        loading = True
        If cboBranch.SelectedIndex > -1 Then
            LoadProgram(CInt(cboBranch.SelectedValue))
            LoadAccounts(Nothing, CInt(cboBranch.SelectedValue))
        Else
            cboProgram.Enabled = False
            cboProgram.DataSource = Nothing
        End If
        loading = False
    End Sub

    Private Sub cboProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProgram.SelectedIndexChanged
        If Not loading And cboProgram.SelectedIndex > -1 Then
            LoadAccounts(CInt(cboProgram.SelectedValue), CInt(cboBranch.SelectedValue))
        End If
    End Sub

    Private Sub btnSelectForm_Click(sender As Object, e As EventArgs) Handles btnSelectForm.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvSelectedForms.Rows.Count

            If i > 0 Then
                temp = dgvAvailableForms(0, dgvAvailableForms.CurrentRow.Index).Value
                For i = 0 To dgvSelectedForms.Rows.Count - 1
                    If dgvSelectedForms(0, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvSelectedForms)
                    dgvRow.Cells(0).Value = dgvAvailableForms(0, dgvAvailableForms.CurrentRow.Index).Value
                    dgvRow.Cells(1).Value = dgvAvailableForms(1, dgvAvailableForms.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = dgvAvailableForms(2, dgvAvailableForms.CurrentRow.Index).Value
                    dgvSelectedForms.Rows.Add(dgvRow)
                End If
            Else
                dgvRow.CreateCells(dgvSelectedForms)
                dgvRow.Cells(0).Value = dgvAvailableForms(0, dgvAvailableForms.CurrentRow.Index).Value
                dgvRow.Cells(1).Value = dgvAvailableForms(1, dgvAvailableForms.CurrentRow.Index).Value
                dgvRow.Cells(2).Value = dgvAvailableForms(2, dgvAvailableForms.CurrentRow.Index).Value
                dgvSelectedForms.Rows.Add(dgvRow)
            End If

            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnSelectAllForms_Click(sender As Object, e As EventArgs) Handles btnSelectAllForms.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            dgvSelectedForms.Rows.Clear()

            For i = 0 To dgvAvailableForms.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvSelectedForms)
                dgvRow.Cells(0).Value = dgvAvailableForms(0, i).Value
                dgvRow.Cells(1).Value = dgvAvailableForms(1, i).Value
                dgvRow.Cells(2).Value = dgvAvailableForms(2, i).Value
                dgvSelectedForms.Rows.Add(dgvRow)
            Next

            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnUnselectForm_Click(sender As Object, e As EventArgs) Handles btnUnselectForm.Click
        dgvSelectedForms.Rows.Remove(dgvSelectedForms.CurrentRow)
        lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString
    End Sub

    Private Sub btnUnselectAllForms_Click(sender As Object, e As EventArgs) Handles btnUnselectAllForms.Click
        dgvSelectedForms.Rows.Clear()
        lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString
    End Sub

    Private Sub chbCascadeBranch_CheckedChanged(sender As Object, e As EventArgs) Handles chbCascadeBranch.CheckedChanged, chbCascadeProgram.CheckedChanged
        lbAccounts.Enabled = Not chbCascadeBranch.Checked And Not chbCascadeProgram.Checked
    End Sub

    Private Sub btnUpdateAccount_Click(sender As Object, e As EventArgs) Handles btnUpdateAccount.Click
        UpdateAccount()
    End Sub

    Private Sub dgvSelectedForms_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvSelectedForms.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvSelectedForms.HitTest(e.X, e.Y)
            Dim i As Integer = 0

            If hti.RowIndex = -1 And hti.ColumnIndex <> -1 Then
                If dgvSelectedForms.Columns(hti.ColumnIndex).HeaderText = "Read Only" Then
                    If dgvSelectedForms(3, 0).Value = True Then
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(3, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(3, i).Value = True
                        Next
                    End If
                End If
                If dgvSelectedForms.Columns(hti.ColumnIndex).HeaderText = "Write" Then
                    If dgvSelectedForms(4, 0).Value = True Then
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(4, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(4, i).Value = True
                        Next
                    End If
                End If
                If dgvSelectedForms.Columns(hti.ColumnIndex).HeaderText = "Full Access" Then
                    If dgvSelectedForms(5, 0).Value = True Then
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(5, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(5, i).Value = True
                        Next
                    End If
                End If
                If dgvSelectedForms.Columns(hti.ColumnIndex).HeaderText = "Special Permissions" Then
                    If dgvSelectedForms(6, 0).Value = True Then
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(6, i).Value = False
                        Next
                    Else
                        For i = 0 To dgvSelectedForms.Rows.Count - 1
                            dgvSelectedForms(6, i).Value = True
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnViewAccountForms_Click(sender As Object, e As EventArgs) Handles btnViewAccountForms.Click
        ViewForms()
    End Sub

    Private Sub tsbRefreshForm_Click(sender As Object, e As EventArgs) Handles tsbRefreshForm.Click
        Try
            txtBranch.Clear()
            txtBranchCode.Clear()
            txtProgram.Clear()
            txtProgramCode.Clear()
            txtUnit.Clear()
            txtUnitCode.Clear()
            txtAccount.Clear()
            txtAccountCode.Clear()
            dgvProgram.DataSource = Nothing
            dgvUnit.DataSource = Nothing
            dgvAccounts.DataSource = Nothing

            btnAddBranch.Enabled = False
            btnEditBranch.Enabled = False
            btnAddProgram.Enabled = False
            btnEditProgram.Enabled = False
            btnAddUnit.Enabled = False
            btnEditUnit.Enabled = False
            btnAddAccount.Enabled = False
            btnEditAccount.Enabled = False

            cboProgram.SelectedIndex = -1
            cboBranch.SelectedIndex = -1

            chbCascadeBranch.Checked = False
            chbCascadeProgram.Checked = False
            dgvSelectedForms.Rows.Clear()
            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

            LoadDataSets()
            LoadBranchGrid()
            LoadForms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    'Form overrides dispose to clean up the component list. 
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If dtOrganization IsNot Nothing Then dtOrganization.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class