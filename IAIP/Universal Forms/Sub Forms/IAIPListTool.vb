Imports Oracle.ManagedDataAccess.Client


Public Class IAIPListTool
    Dim SQL As String
    Dim dr As OracleDataReader
    Dim cmd As OracleCommand
    Dim dsOrginizations As DataSet
    Dim daOrginizations As OracleDataAdapter
    Dim dsBranch As DataSet
    Dim daBranch As OracleDataAdapter
    Dim dsProgram As DataSet
    Dim daProgram As OracleDataAdapter
    Dim dsUnit As DataSet
    Dim daUnit As OracleDataAdapter
    Dim dsAccount As DataSet
    Dim daAccount As OracleDataAdapter
    Dim dsAccounts As DataSet
    Dim daAccounts As OracleDataAdapter
    Dim dsForms As DataSet
    Dim daForms As OracleDataAdapter

    Private Sub IAIPListTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            btnAddBranch.Enabled = False
            btnEditBranch.Enabled = False
            btnDeleteBranch.Enabled = False
            btnAddProgram.Enabled = False
            btnEditProgram.Enabled = False
            btnDeleteProgram.Enabled = False
            btnAddUnit.Enabled = False
            btnEditUnit.Enabled = False
            btnDeleteUnit.Enabled = False
            btnAddAccount.Enabled = False
            btnEditAccount.Enabled = False
            btnDeleteAccount.Enabled = False

            LoadDataSets()
            LoadBranchGrid()
            LoadForms()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region "Page Load"
    Sub LoadDataSets()
        Try
            dsOrginizations = New DataSet
            dsAccounts = New DataSet

            SQL = "select " & _
            "AIRBRANCH.lookupepdbranches.numBranchCode, strBranchDesc,  " & _
            "AIRBRANCH.lookupepdprograms.numProgramCode, strProgramDesc,  " & _
            "AIRBRANCH.lookupepdunits.numUnitCode, strUnitdesc " & _
            "from AIRBRANCH.Lookupepdbranches, AIRBRANCH.lookupepdprograms,  " & _
            "AIRBRANCH.lookupepdunits " & _
            "where AIRBRANCH.lookupepdbranches.numbranchcode = AIRBRANCH.lookupepdprograms.numbranchcode (+) " & _
            "and AIRBRANCH.lookupepdprograms.numprogramcode = AIRBRANCH.lookupepdunits.numprogramcode (+) " & _
            "order by strbranchdesc, strProgramDesc, strUnitDesc "

            daOrginizations = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "numAccountCode, strAccountDesc, " & _
            "numBranchCode, numProgramCode, " & _
            "numUnitCode " & _
            "from AIRBRANCH.LookUpIAIPAccounts " & _
            "order by strAccountDesc "

            daAccounts = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daOrginizations.Fill(dsOrginizations, "Orginization")
            daAccounts.Fill(dsAccounts, "Accounts")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadBranchGrid()
        Try
            SQL = "Select " & _
            "numBranchCode, strBranchDesc " & _
            "from AIRBRANCH.LookUpEPDBranches " & _
            "order by strBranchDesc "

            dsBranch = New DataSet
            daBranch = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daBranch.Fill(dsBranch, "Branches")

            dgvBranch.DataSource = dsBranch
            dgvBranch.DataMember = "Branches"

            dgvBranch.RowHeadersVisible = False
            dgvBranch.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvBranch.AllowUserToResizeColumns = True
            dgvBranch.AllowUserToAddRows = False
            dgvBranch.AllowUserToDeleteRows = False
            dgvBranch.AllowUserToOrderColumns = True
            dgvBranch.AllowUserToResizeRows = True
            dgvBranch.ReadOnly = True
            dgvBranch.ColumnHeadersHeight = "35"
            dgvBranch.Columns("numBranchCode").HeaderText = "ID #"
            dgvBranch.Columns("numBranchCode").DisplayIndex = 0
            dgvBranch.Columns("numBranchCode").Width = 60
            dgvBranch.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvBranch.Columns("strBranchDesc").DisplayIndex = 1
            dgvBranch.Columns("strBranchDesc").Width = 300

            btnAddBranch.Enabled = True

            Dim dtBranch As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtBranch.Columns.Add("numBranchCode", GetType(System.String))
            dtBranch.Columns.Add("strBranchDesc", GetType(System.String))

            drNewRow = dtBranch.NewRow()
            drNewRow("numBranchCode") = ""
            drNewRow("strBranchDesc") = " "
            dtBranch.Rows.Add(drNewRow)

            For Each drDSRow In dsBranch.Tables("Branches").Rows()
                drNewRow = dtBranch.NewRow()
                drNewRow("numBranchCode") = drDSRow("numBranchCode")
                drNewRow("strBranchDesc") = drDSRow("strBranchDesc")

                For i = 0 To dtBranch.Rows.Count - 1
                    If drDSRow("strBranchDesc") = dtBranch.Rows(i).Item(1) Then
                        temp = "No"
                    Else
                        'temp = temp
                    End If
                Next
                If temp = "Add" Then
                    dtBranch.Rows.Add(drNewRow)
                End If
                temp = "Add"
            Next

            With cboBranch
                .DataSource = dtBranch
                .DisplayMember = "strBranchDesc"
                .ValueMember = "numBranchCode"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadForms()
        Try
            SQL = "Select " & _
            "numFormCode, strForm, " & _
            "strFormDesc " & _
            "from AIRBRANCH.LookUpIAIPForms "

            dsForms = New DataSet
            daForms = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daForms.Fill(dsForms, "Forms")

            dgvAvailableForms.DataSource = dsForms
            dgvAvailableForms.DataMember = "Forms"

            dgvAvailableForms.RowHeadersVisible = False
            dgvAvailableForms.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAvailableForms.AllowUserToResizeColumns = True
            dgvAvailableForms.AllowUserToAddRows = False
            dgvAvailableForms.AllowUserToDeleteRows = False
            dgvAvailableForms.AllowUserToOrderColumns = True
            dgvAvailableForms.AllowUserToResizeRows = True
            dgvAvailableForms.ReadOnly = True
            dgvAvailableForms.ColumnHeadersHeight = "35"
            dgvAvailableForms.Columns("numFormCode").HeaderText = "Form #"
            dgvAvailableForms.Columns("numFormCode").DisplayIndex = 0
            dgvAvailableForms.Columns("numFormCode").Width = 60
            dgvAvailableForms.Columns("numFormCode").Visible = False

            dgvAvailableForms.Columns("strForm").HeaderText = "Form"
            dgvAvailableForms.Columns("strForm").DisplayIndex = 1
            dgvAvailableForms.Columns("strForm").Width = 150

            dgvAvailableForms.Columns("strFormDesc").HeaderText = "Form Description"
            dgvAvailableForms.Columns("strFormDesc").DisplayIndex = 2
            dgvAvailableForms.Columns("strFormDesc").Width = 250

            dgvSelectedForms.RowHeadersVisible = False
            dgvSelectedForms.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvSelectedForms.AllowUserToResizeColumns = True
            dgvSelectedForms.AllowUserToAddRows = False
            dgvSelectedForms.AllowUserToDeleteRows = False
            dgvSelectedForms.AllowUserToOrderColumns = True
            dgvSelectedForms.AllowUserToResizeRows = True
            dgvSelectedForms.ColumnHeadersHeight = "35"

            dgvSelectedForms.Columns.Add("numFormCode", "Form #")
            dgvSelectedForms.Columns("numFormCode").DisplayIndex = 0
            dgvSelectedForms.Columns("numFormCode").Width = 60
            dgvSelectedForms.Columns("numFormCode").Visible = False

            dgvSelectedForms.Columns.Add("strForm", "Form")
            dgvSelectedForms.Columns("strForm").DisplayIndex = 1
            dgvSelectedForms.Columns("strForm").Width = 150
            dgvSelectedForms.Columns("strForm").ReadOnly = True

            dgvSelectedForms.Columns.Add("strFormDesc", "Form Description")
            dgvSelectedForms.Columns("strFormDesc").DisplayIndex = 2
            dgvSelectedForms.Columns("strFormDesc").Width = 250
            dgvSelectedForms.Columns("strFormDesc").ReadOnly = True

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

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub


#End Region
#Region "Subs and Functions"
    Sub CheckButtons()
        Try
            If txtAccountCode.Text = "" Then
                btnEditAccount.Enabled = False
                btnDeleteAccount.Enabled = False
            Else
                btnEditAccount.Enabled = True
                btnDeleteAccount.Enabled = True
            End If
            If txtUnitCode.Text = "" Then
                btnEditUnit.Enabled = False
                btnDeleteUnit.Enabled = False
                btnAddAccount.Enabled = False
            Else
                btnEditUnit.Enabled = True
                btnDeleteUnit.Enabled = True
                btnAddAccount.Enabled = True
            End If
            If txtProgramCode.Text = "" Then
                btnEditProgram.Enabled = False
                btnDeleteProgram.Enabled = False
                btnAddUnit.Enabled = False
                btnAddAccount.Enabled = False
            Else
                btnEditProgram.Enabled = True
                btnDeleteProgram.Enabled = True
                btnAddUnit.Enabled = True
                btnAddAccount.Enabled = True
            End If
            If txtBranchCode.Text = "" Then
                btnEditBranch.Enabled = False
                btnDeleteBranch.Enabled = False
                btnAddProgram.Enabled = False
                btnAddAccount.Enabled = False
            Else
                btnEditBranch.Enabled = True
                btnDeleteBranch.Enabled = True
                btnAddProgram.Enabled = True
                btnAddAccount.Enabled = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadProgramGrid()
        Try
            dsUnit = New DataSet
            dsAccount = New DataSet
            dgvUnit.DataSource = dsUnit
            dgvAccounts.DataSource = dsAccount

            SQL = "Select " & _
            "numProgramCode, strProgramDesc, " & _
            "numBranchCode " & _
            "from AIRBRANCH.LookUpEPDprograms " & _
            "where numBranchCode = '" & txtBranchCode.Text & "' " & _
            "order by strProgramDesc "

            dsProgram = New DataSet
            daProgram = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daProgram.Fill(dsProgram, "Programs")

            dgvProgram.DataSource = dsProgram
            dgvProgram.DataMember = "Programs"

            dgvProgram.RowHeadersVisible = False
            dgvProgram.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvProgram.AllowUserToResizeColumns = True
            dgvProgram.AllowUserToAddRows = False
            dgvProgram.AllowUserToDeleteRows = False
            dgvProgram.AllowUserToOrderColumns = True
            dgvProgram.AllowUserToResizeRows = True
            dgvProgram.ColumnHeadersHeight = "35"
            dgvProgram.Columns("numProgramCode").HeaderText = "ID #"
            dgvProgram.Columns("numProgramCode").DisplayIndex = 0
            dgvProgram.Columns("numProgramCode").Width = 60
            dgvProgram.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvProgram.Columns("strProgramDesc").DisplayIndex = 1
            dgvProgram.Columns("strProgramDesc").Width = 300
            dgvProgram.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvProgram.Columns("numBranchCode").DisplayIndex = 2
            dgvProgram.Columns("numBranchCode").Width = 50
            dgvProgram.Columns("numBranchCode").Visible = False

            SQL = "Select " & _
            "numAccountCode, strAccountDesc,  " & _
            "AIRBRANCH.LookUpEPDBranches.numBranchCode, strBranchDesc, " & _
            "AIRBRANCH.LookUpEPDPrograms.numProgramCode, strProgramDesc,  " & _
            "AIRBRANCH.LookUpEPDUnits.numUnitCode, strUnitDesc " & _
            "from AIRBRANCH.LookUpIAIPAccounts, AIRBRANCH.LookupEPDBranches,  " & _
            "AIRBRANCH.LookUpEPDPrograms, AIRBRANCH.LookUpEPDUnits   " & _
            "where AIRBRANCH.LookUpIAIPAccounts.numBranchCode = AIRBRANCH.LookUpEPDBranches.numBranchCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numProgramCode = AIRBRANCH.LookUpEPDPrograms.numProgramCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numUnitCode = AIRBRANCH.LookUpEPDUnits.numUnitCode (+)  " & _
            "and AIRBRANCH.lookupiaipaccounts.numBranchCode = '" & txtBranchCode.Text & "'  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numProgramCode is Null " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numUnitCode is Null " & _
            "order by strAccountDesc "

            dsAccount = New DataSet
            daAccount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAccount.Fill(dsAccount, "Accounts")

            dgvAccounts.DataSource = dsAccount
            dgvAccounts.DataMember = "Accounts"

            dgvAccounts.RowHeadersVisible = False
            dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAccounts.AllowUserToResizeColumns = True
            dgvAccounts.AllowUserToAddRows = False
            dgvAccounts.AllowUserToDeleteRows = False
            dgvAccounts.AllowUserToOrderColumns = True
            dgvAccounts.AllowUserToResizeRows = True
            dgvAccounts.ColumnHeadersHeight = "35"
            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("numAccountCode").DisplayIndex = 0
            dgvAccounts.Columns("numAccountCode").Width = 60
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("strAccountDesc").DisplayIndex = 1
            dgvAccounts.Columns("strAccountDesc").Width = 300
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").DisplayIndex = 2
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("strBranchDesc").DisplayIndex = 3
            dgvAccounts.Columns("strBranchDesc").Width = 250
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("numProgramCode").DisplayIndex = 4
            dgvAccounts.Columns("numProgramCode").Visible = False
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("strProgramDesc").DisplayIndex = 5
            dgvAccounts.Columns("strProgramDesc").Width = 250
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").DisplayIndex = 6
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.Columns("strUnitDesc").DisplayIndex = 7
            dgvAccounts.Columns("strUnitDesc").Width = 250

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadUnitGrid()
        Try
            dsAccount = New DataSet
            dgvAccounts.DataSource = dsAccount

            SQL = "Select " & _
            "numUnitCode, strUnitDesc, " & _
            "numProgramCode " & _
            "from AIRBRANCH.LookUpEPDUnits " & _
            "where numProgramCode = '" & txtProgramCode.Text & "' " & _
            "order by strUnitDesc "

            dsUnit = New DataSet
            daUnit = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daUnit.Fill(dsUnit, "Units")

            dgvUnit.DataSource = dsUnit
            dgvUnit.DataMember = "Units"

            dgvUnit.RowHeadersVisible = False
            dgvUnit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUnit.AllowUserToResizeColumns = True
            dgvUnit.AllowUserToAddRows = False
            dgvUnit.AllowUserToDeleteRows = False
            dgvUnit.AllowUserToOrderColumns = True
            dgvUnit.AllowUserToResizeRows = True
            dgvUnit.ColumnHeadersHeight = "35"
            dgvUnit.Columns("numUnitCode").HeaderText = "ID #"
            dgvUnit.Columns("numUnitCode").DisplayIndex = 0
            dgvUnit.Columns("numUnitCode").Width = 60
            dgvUnit.Columns("strUnitDesc").HeaderText = "Unit Desc."
            dgvUnit.Columns("strUnitDesc").DisplayIndex = 1
            dgvUnit.Columns("strUnitDesc").Width = 300
            dgvUnit.Columns("numProgramCode").HeaderText = "Program Code"
            dgvUnit.Columns("numProgramCode").DisplayIndex = 2
            dgvUnit.Columns("numProgramCode").Width = 50
            dgvUnit.Columns("numProgramCode").Visible = False

            SQL = "Select " & _
            "numAccountCode, strAccountDesc,  " & _
            "AIRBRANCH.LookUpEPDBranches.numBranchCode, strBranchDesc, " & _
            "AIRBRANCH.LookUpEPDPrograms.numProgramCode, strProgramDesc,  " & _
            "AIRBRANCH.LookUpEPDUnits.numUnitCode, strUnitDesc " & _
            "from AIRBRANCH.LookUpIAIPAccounts, AIRBRANCH.LookupEPDBranches,  " & _
            "AIRBRANCH.LookUpEPDPrograms, AIRBRANCH.LookUpEPDUnits   " & _
            "where AIRBRANCH.LookUpIAIPAccounts.numBranchCode = AIRBRANCH.LookUpEPDBranches.numBranchCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numProgramCode = AIRBRANCH.LookUpEPDPrograms.numProgramCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numUnitCode = AIRBRANCH.LookUpEPDUnits.numUnitCode (+)  " & _
            "and AIRBRANCH.lookupiaipaccounts.numProgramCode = '" & txtProgramCode.Text & "'  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numUnitCode is Null " & _
            "order by strAccountDesc "

            dsAccount = New DataSet
            daAccount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAccount.Fill(dsAccount, "Accounts")

            dgvAccounts.DataSource = dsAccount
            dgvAccounts.DataMember = "Accounts"

            dgvAccounts.RowHeadersVisible = False
            dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAccounts.AllowUserToResizeColumns = True
            dgvAccounts.AllowUserToAddRows = False
            dgvAccounts.AllowUserToDeleteRows = False
            dgvAccounts.AllowUserToOrderColumns = True
            dgvAccounts.AllowUserToResizeRows = True
            dgvAccounts.ColumnHeadersHeight = "35"
            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("numAccountCode").DisplayIndex = 0
            dgvAccounts.Columns("numAccountCode").Width = 60
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("strAccountDesc").DisplayIndex = 1
            dgvAccounts.Columns("strAccountDesc").Width = 300
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").DisplayIndex = 2
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("strBranchDesc").DisplayIndex = 3
            dgvAccounts.Columns("strBranchDesc").Width = 250
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("numProgramCode").DisplayIndex = 4
            dgvAccounts.Columns("numProgramCode").Visible = False
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("strProgramDesc").DisplayIndex = 5
            dgvAccounts.Columns("strProgramDesc").Width = 250
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").DisplayIndex = 6
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.Columns("strUnitDesc").DisplayIndex = 7
            dgvAccounts.Columns("strUnitDesc").Width = 250
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadAccountGrid()
        Try
            SQL = "Select " & _
            "numAccountCode, strAccountDesc,  " & _
            "AIRBRANCH.LookUpEPDBranches.numBranchCode, strBranchDesc, " & _
            "AIRBRANCH.LookUpEPDPrograms.numProgramCode, strProgramDesc,  " & _
            "AIRBRANCH.LookUpEPDUnits.numUnitCode, strUnitDesc " & _
            "from AIRBRANCH.LookUpIAIPAccounts, AIRBRANCH.LookupEPDBranches,  " & _
            "AIRBRANCH.LookUpEPDPrograms, AIRBRANCH.LookUpEPDUnits   " & _
            "where AIRBRANCH.LookUpIAIPAccounts.numBranchCode = AIRBRANCH.LookUpEPDBranches.numBranchCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numProgramCode = AIRBRANCH.LookUpEPDPrograms.numProgramCode (+)  " & _
            "and AIRBRANCH.LookUpIAIPAccounts.numUnitCode = AIRBRANCH.LookUpEPDUnits.numUnitCode (+)  " & _
            "and AIRBRANCH.lookupiaipaccounts.numUnitCode = '" & txtUnitCode.Text & "'  " & _
            "order by strAccountDesc "

            dsAccount = New DataSet
            daAccount = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            daAccount.Fill(dsAccount, "Accounts")

            dgvAccounts.DataSource = dsAccount
            dgvAccounts.DataMember = "Accounts"

            dgvAccounts.RowHeadersVisible = False
            dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvAccounts.AllowUserToResizeColumns = True
            dgvAccounts.AllowUserToAddRows = False
            dgvAccounts.AllowUserToDeleteRows = False
            dgvAccounts.AllowUserToOrderColumns = True
            dgvAccounts.AllowUserToResizeRows = True
            dgvAccounts.ColumnHeadersHeight = "35"
            dgvAccounts.Columns("numAccountCode").HeaderText = "ID #"
            dgvAccounts.Columns("numAccountCode").DisplayIndex = 0
            dgvAccounts.Columns("numAccountCode").Width = 60
            dgvAccounts.Columns("strAccountDesc").HeaderText = "Account Desc."
            dgvAccounts.Columns("strAccountDesc").DisplayIndex = 1
            dgvAccounts.Columns("strAccountDesc").Width = 300
            dgvAccounts.Columns("numBranchCode").HeaderText = "Branch Code"
            dgvAccounts.Columns("numBranchCode").DisplayIndex = 2
            dgvAccounts.Columns("numBranchCode").Visible = False
            dgvAccounts.Columns("strBranchDesc").HeaderText = "Branch Desc."
            dgvAccounts.Columns("strBranchDesc").DisplayIndex = 3
            dgvAccounts.Columns("strBranchDesc").Width = 250
            dgvAccounts.Columns("numProgramCode").HeaderText = "Program Code"
            dgvAccounts.Columns("numProgramCode").DisplayIndex = 4
            dgvAccounts.Columns("numProgramCode").Visible = False
            dgvAccounts.Columns("strProgramDesc").HeaderText = "Program Desc."
            dgvAccounts.Columns("strProgramDesc").DisplayIndex = 5
            dgvAccounts.Columns("strProgramDesc").Width = 250
            dgvAccounts.Columns("numUnitCode").HeaderText = "Unit Code"
            dgvAccounts.Columns("numUnitCode").DisplayIndex = 6
            dgvAccounts.Columns("numUnitCode").Visible = False
            dgvAccounts.Columns("strUnitDesc").HeaderText = "unit Desc."
            dgvAccounts.Columns("strUnitDesc").DisplayIndex = 7
            dgvAccounts.Columns("strUnitDesc").Width = 250

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadProgram(ByVal BranchCode As String)
        Try
            Dim dtProgram As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtProgram.Columns.Add("numProgramCode", GetType(System.String))
            dtProgram.Columns.Add("strProgramDesc", GetType(System.String))

            drNewRow = dtProgram.NewRow()
            drNewRow("numProgramCode") = ""
            drNewRow("strProgramDesc") = " "
            dtProgram.Rows.Add(drNewRow)

            If BranchCode = " " Or BranchCode = "" Then

            Else
                For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numBranchCode = " & BranchCode, "strProgramDesc")
                    drNewRow = dtProgram.NewRow()
                    drNewRow("numProgramCode") = drDSRow("numProgramCode")
                    drNewRow("strProgramDesc") = drDSRow("strProgramDesc")

                    If Not IsDBNull(drDSRow("strProgramDesc")) Then
                        For i = 0 To dtProgram.Rows.Count - 1
                            If drDSRow("strProgramDesc") = dtProgram.Rows(i).Item(1) Then
                                temp = "No"
                            Else
                                'temp = temp
                            End If
                        Next
                        If temp = "Add" Then
                            dtProgram.Rows.Add(drNewRow)
                        End If
                        temp = "Add"
                    End If
                Next
            End If

            With cboProgram
                .DataSource = dtProgram
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadUnit(ByVal ProgramCode As String)
        Try
            Dim dtUnit As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtUnit.Columns.Add("numUnitCode", GetType(System.String))
            dtUnit.Columns.Add("strUnitDesc", GetType(System.String))

            drNewRow = dtUnit.NewRow()
            drNewRow("numUnitCode") = ""
            drNewRow("strUnitDesc") = " "
            dtUnit.Rows.Add(drNewRow)

            If ProgramCode = " " Or ProgramCode = "" Then

            Else
                For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numProgramCode = " & ProgramCode, "strUnitDesc")
                    drNewRow = dtUnit.NewRow()
                    drNewRow("numUnitCode") = drDSRow("numUnitCode")
                    drNewRow("strUnitDesc") = drDSRow("strUnitDesc")

                    If Not IsDBNull(drDSRow("strUnitDesc")) Then
                        For i = 0 To dtUnit.Rows.Count - 1
                            If drDSRow("strUnitDesc") = dtUnit.Rows(i).Item(1) Then
                                temp = "No"
                            Else
                                'temp = temp
                            End If
                        Next
                        If temp = "Add" Then
                            dtUnit.Rows.Add(drNewRow)
                        End If
                        temp = "Add"
                    End If
                Next
            End If
            With cboUnit
                .DataSource = dtUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadAccounts(ByVal UnitCode As String, ByVal ProgramCode As String, ByVal BranchCode As String)
        Try
            Dim dtAccount As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dtAccount.Columns.Add("numAccountCode", GetType(System.String))
            dtAccount.Columns.Add("strAccountDesc", GetType(System.String))


            If BranchCode = "" And ProgramCode = "" And UnitCode = "" Then
                With clbAccounts
                    .DataSource = dtAccount
                    .DisplayMember = "strAccountDesc"
                    .ValueMember = "numAccountCode"
                End With
            Else
                If UnitCode = "" Or UnitCode = " " Then
                    UnitCode = " numUnitCode is null "
                Else
                    UnitCode = " numUnitCode = " & UnitCode
                End If
                If ProgramCode = "" Or ProgramCode = " " Then
                    ProgramCode = " numProgramCode is null "
                Else
                    ProgramCode = " numProgramCode = " & ProgramCode
                End If
                If BranchCode = "" Or BranchCode = " " Then
                    BranchCode = " numBranchCode is Null "
                Else
                    BranchCode = " numBranchCode = " & BranchCode
                End If

                For Each drDSRow In dsAccounts.Tables("Accounts").Select(BranchCode & " and " & ProgramCode & " and " & UnitCode, "strAccountdesc")
                    drNewRow = dtAccount.NewRow()
                    drNewRow("numAccountCode") = drDSRow("numAccountCode")
                    drNewRow("strAccountDesc") = drDSRow("strAccountDesc")
                    dtAccount.Rows.Add(drNewRow)
                Next

                With clbAccounts
                    .DataSource = dtAccount
                    .DisplayMember = "strAccountDesc"
                    .ValueMember = "numAccountCode"
                End With
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub UpdateAccount()
        Try
            Dim i As Integer = 0
            Dim temp As String = ""
            Dim SQLLine As String = ""

            lblFormAccess.Text = ""

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
                lblFormAccess.Text = lblFormAccess.Text & temp
            Next

            SQL = "Update AIRBRANCH.LookUpIAIPAccounts set " & _
            "strFormAccess = '" & lblFormAccess.Text & "' " & _
            "where "

            If chbCascadeBranch.Checked = True Then
                SQLLine = " numBranchcode = '" & cboBranch.SelectedValue & "' "
            Else
                If chbCascadeProgram.Checked = True Then
                    SQLLine = " numProgramcode = '" & cboProgram.SelectedValue & "' "
                Else
                    SQLLine = ""
                    Dim myStringIndex As String
                    i = 0
                    Do While i < Me.clbAccounts.CheckedItems.Count
                        myStringIndex = Me.clbAccounts.CheckedIndices(i).ToString()
                        clbAccounts.SetSelected(myStringIndex, True)
                        SQLLine = SQLLine & " numaccountCode = '" & clbAccounts.SelectedValue & "' and "
                        i += 1
                    Loop
                    If SQLLine <> "" Then
                        SQLLine = Mid(SQLLine, 1, (SQLLine.Length) - 4)
                    End If
                End If
            End If

            If SQLLine <> "" Then
                SQL = SQL & SQLLine
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                MsgBox("Successfully  Done.", MsgBoxStyle.Information, "IAIP List Tool")
            Else
                MsgBox("No data was save. " & vbCrLf & "Select atleast one Account or cascade through Branch/Program", MsgBoxStyle.Exclamation, "IAIP List Tool")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub ViewForms()
        Try
            Dim i As Integer = 0
            Dim myStringIndex As String
            Dim temp As String = ""
            Dim temp2 As String = ""
            Dim temp3 As String = ""
            Dim tempForm As String = ""
            Dim dgvRow As New DataGridViewRow

            Do While i < Me.clbAccounts.CheckedItems.Count
                myStringIndex = Me.clbAccounts.CheckedIndices(i).ToString()
                clbAccounts.SetSelected(myStringIndex, True)
                temp = clbAccounts.SelectedValue
                i += 1
            Loop

            SQL = "Select " & _
            "strFormAccess " & _
            "from AIRBRANCH.LookUpIAIPAccounts " & _
            "where numAccountCode = '" & temp & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFormAccess")) Then
                    lblFormAccess.Text = ""
                Else
                    lblFormAccess.Text = dr.Item("strFormAccess")
                End If
            End While
            dr.Close()

            dgvSelectedForms.Rows.Clear()
            If lblFormAccess.Text <> "" Then
                temp = lblFormAccess.Text
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
                                    temp3 = x.ToString()
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
                            System.Math.Min(System.Threading.Interlocked.Increment(y), y - 1)
                        End While
                        System.Math.Min(System.Threading.Interlocked.Increment(x), x - 1)
                    End While
                Loop

                lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub


#End Region
#Region "Declaration"
    Private Sub dgvBranch_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvBranch.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvProgram_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvProgram.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvUnit_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvUnit.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvAccounts_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvAccounts.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClear.Click
        Try
            dsProgram = New DataSet
            dsUnit = New DataSet
            dsAccount = New DataSet

            dgvProgram.DataSource = dsProgram
            dgvUnit.DataSource = dsUnit
            dgvAccounts.DataSource = dsAccount

            txtAccount.Clear()
            txtAccountCode.Clear()
            txtUnit.Clear()
            txtUnitCode.Clear()
            txtProgram.Clear()
            txtProgramCode.Clear()
            txtBranch.Clear()
            txtBranchCode.Clear()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtBranchCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBranchCode.TextChanged
        Try
            CheckButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtProgramCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProgramCode.TextChanged
        Try
            CheckButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtUnitCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUnitCode.TextChanged
        Try
            CheckButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub txtAccountCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccountCode.TextChanged
        Try
            CheckButtons()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnAddBranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBranch.Click
        Try
            SQL = "Insert into AIRBRANCH.LookUpEPDBranches " & _
            "values " & _
            "((select max(numBranchCode) + 1 from AIRBRANCH.LookUpEPDBranches), " & _
            "'" & Replace(txtBranch.Text, "'", "''") & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(numBranchCode) from AIRBRANCH.LookUpEPDBranches "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtBranchCode.Text = dr.Item(0)
            End While
            dr.Close()

            LoadBranchGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnEditBranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditBranch.Click
        Try
            If txtBranchCode.Text <> "" Then
                SQL = "Update AIRBRANCH.LookUpEPDBranches set " & _
                "strBranchDesc = '" & Replace(txtBranch.Text, "'", "''") & "' " & _
                "where numBranchCode = '" & txtBranchCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadBranchGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteBranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteBranch.Click
        Try
            If txtBranchCode.Text <> "" Then
                SQL = "Delete AIRBRANCH.LookUpEPDBranches " & _
                "where numBranchCode = '" & txtBranchCode.Text & "' "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtBranch.Clear()
                txtBranchCode.Clear()

                LoadBranchGrid()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnAddProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddProgram.Click
        Try
            If txtBranchCode.Text <> "" Then
                SQL = "Insert into AIRBRANCH.LookUpEPDPrograms " & _
                "values " & _
                "((select max(numProgramCode) + 1 from AIRBRANCH.LookUpEPDPrograms where numProgramCode < 99), " & _
                "'" & Replace(txtProgram.Text, "'", "''") & "', " & _
                "'" & txtBranchCode.Text & "') "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select max(numProgramCode) from AIRBRANCH.LookUpEPDPrograms where numProgramCode < 99 "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    txtProgramCode.Text = dr.Item(0)
                End While
                dr.Close()

                LoadProgramGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnEditProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditProgram.Click
        Try
            If txtProgramCode.Text <> "" And txtBranchCode.Text <> "" Then

                SQL = "Update AIRBRANCH.LookUpEPDPrograms set " & _
                "strProgramDesc = '" & Replace(txtProgram.Text, "'", "''") & "', " & _
                "numBranchCode = '" & txtBranchCode.Text & "' " & _
                "where numProgramCode = '" & txtProgramCode.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadProgramGrid()

            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteProgram.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpEPDPrograms " & _
            "where numProgramCode = '" & txtProgramCode.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtProgram.Clear()
            txtProgramCode.Clear()

            LoadProgramGrid()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnAddUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUnit.Click
        Try
            If txtProgramCode.Text <> "" Then
                SQL = "Insert into AIRBranch.LookUpEPDUnits " & _
                "(numUnitCode, strUnitDesc, numProgramCode)  " & _
                "values  " & _
                "((select max(numUnitCode) + 1 From AIRBranch.LookUpEPDUnits), '" & Replace(txtUnit.Text, "'", "''") & "',  " & _
                "'" & Replace(txtProgramCode.Text, "'", "''") & "') "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                SQL = "select max(numUnitCode) from AIRBRANCH.LookUpEPDUnits "
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    txtUnitCode.Text = dr.Item(0)
                End While
                dr.Close()

                LoadUnitGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnEditUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUnit.Click
        Try
            If txtUnitCode.Text <> "" And txtProgramCode.Text <> "" Then
                SQL = "Update AIRBRANCH.LookUpEPDUnits set " & _
                "strUnitDesc = '" & txtUnit.Text & "' " & _
                "where numUnitCode = '" & txtUnitCode.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadUnitGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteUnit.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpEPDUnits " & _
            "where numUnitCode = '" & txtUnitCode.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtUnit.Clear()
            txtUnitCode.Clear()

            LoadUnitGrid()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnAddAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAccount.Click
        Try
            SQL = "Insert into AIRBRANCH.LookUpIAIPAccounts " & _
            "values " & _
            "((Select (max(numAccountCode) + 1) from AIRBRANCH.LookUpIAIPAccounts), " & _
            "'" & txtAccount.Text & "', " & _
            "'" & txtBranchCode.Text & "', '" & txtProgramCode.Text & "', " & _
            "'" & txtUnitCode.Text & "', '') "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(numAccountCode) as MaxAccount " & _
            "from AIRBRANCH.LookUpIAIPAccounts "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                txtAccountCode.Text = dr.Item(0)
            End While
            dr.Close()

            LoadAccountGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnEditAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditAccount.Click
        Try
            If txtAccountCode.Text <> "" Then
                SQL = "Update AIRBRANCH.LookUpIAIPAccounts set " & _
                "strAccountDesc = '" & Replace(txtAccount.Text, "'", "''") & "', " & _
                "numBranchCode = '" & txtBranchCode.Text & "', " & _
                "numProgramCode = '" & txtProgramCode.Text & "', " & _
                "numUnitCode = '" & txtUnitCode.Text & "' " & _
                "where numAccountCode = '" & txtAccountCode.Text & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                LoadAccountGrid()
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnDeleteAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAccount.Click
        Try
            SQL = "Delete AIRBRANCH.LookUpIAIPAccounts " & _
            "where numAccountCode = '" & txtAccountCode.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            txtAccount.Clear()
            txtAccountCode.Clear()

            LoadAccountGrid()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnClearBranch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearBranch.Click
        Try
            txtBranch.Clear()
            txtBranchCode.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnClearProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearProgram.Click
        Try
            txtProgram.Clear()
            txtProgramCode.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnClearUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearUnit.Click
        Try
            txtUnit.Clear()
            txtUnitCode.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnClearAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAccount.Click
        Try
            txtAccount.Clear()
            txtAccountCode.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub cboBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBranch.SelectedValueChanged
        Try
            Dim Unit As String
            Dim Program As String
            Dim Branch As String

            If cboBranch.SelectedIndex > 0 Then
                LoadProgram(cboBranch.SelectedValue)
            End If

            If cboBranch.SelectedIndex > 0 Then
                Branch = cboBranch.SelectedValue
            Else
                Branch = ""
            End If
            If cboProgram.SelectedIndex > 0 Then
                Program = cboProgram.SelectedValue
            Else
                Program = ""
            End If
            If cboUnit.SelectedIndex > 0 Then
                Unit = cboUnit.SelectedValue
            Else
                Unit = ""
            End If

            LoadAccounts(Unit, Program, Branch)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProgram.SelectedValueChanged
        Try
            Dim Unit As String
            Dim Program As String
            Dim Branch As String

            If cboProgram.SelectedIndex > 0 Then
                LoadUnit(cboProgram.SelectedValue)
            End If

            If cboBranch.SelectedIndex > 0 Then
                Branch = cboBranch.SelectedValue
            Else
                Branch = ""
            End If
            If cboProgram.SelectedIndex > 0 Then
                Program = cboProgram.SelectedValue
            Else
                Program = ""
            End If
            If cboUnit.SelectedIndex > 0 Then
                Unit = cboUnit.SelectedValue
            Else
                Unit = ""
            End If

            LoadAccounts(Unit, Program, Branch)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboUnit_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUnit.SelectedValueChanged
        Try
            Dim Unit As String
            Dim Program As String
            Dim Branch As String

            If cboBranch.SelectedIndex > 0 Then
                Branch = cboBranch.SelectedValue
            Else
                Branch = ""
            End If
            If cboProgram.SelectedIndex > 0 Then
                Program = cboProgram.SelectedValue
            Else
                Program = ""
            End If
            If cboUnit.SelectedIndex > 0 Then
                Unit = cboUnit.SelectedValue
            Else
                Unit = ""
            End If

            LoadAccounts(Unit, Program, Branch)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSelectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectForm.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSelectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllForms.Click
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUnselectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectForm.Click
        Try
            dgvSelectedForms.Rows.Remove(dgvSelectedForms.CurrentRow)

            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUnselectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAllForms.Click
        Try

            dgvSelectedForms.Rows.Clear()

            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbCascadeBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCascadeBranch.CheckedChanged
        Try

            If chbCascadeBranch.Checked = True Or chbCascadeProgram.Checked = True Then
                clbAccounts.Enabled = False
            Else
                clbAccounts.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbCascadeProgram_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCascadeProgram.CheckedChanged
        Try
            If chbCascadeBranch.Checked = True Or chbCascadeProgram.Checked = True Then
                clbAccounts.Enabled = False
            Else
                clbAccounts.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chbCascadeUnit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If chbCascadeBranch.Checked = True Or chbCascadeProgram.Checked = True Then
                clbAccounts.Enabled = False
            Else
                clbAccounts.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUpdateAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAccount.Click
        Try

            UpdateAccount()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvSelectedForms_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvSelectedForms.MouseUp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnViewAccountForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAccountForms.Click
        Try
            ViewForms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbRefreshForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRefreshForm.Click
        Try
            txtBranch.Clear()
            txtBranchCode.Clear()
            txtProgram.Clear()
            txtProgramCode.Clear()
            txtUnit.Clear()
            txtUnitCode.Clear()
            txtAccount.Clear()
            txtAccountCode.Clear()
            dsProgram = New DataSet
            dgvProgram.DataSource = dsProgram
            dsUnit = New DataSet
            dgvUnit.DataSource = dsUnit
            dsAccount = New DataSet
            dgvAccounts.DataSource = dsAccount

            btnAddBranch.Enabled = False
            btnEditBranch.Enabled = False
            btnDeleteBranch.Enabled = False
            btnAddProgram.Enabled = False
            btnEditProgram.Enabled = False
            btnDeleteProgram.Enabled = False
            btnAddUnit.Enabled = False
            btnEditUnit.Enabled = False
            btnDeleteUnit.Enabled = False
            btnAddAccount.Enabled = False
            btnEditAccount.Enabled = False
            btnDeleteAccount.Enabled = False

            If dsUnit.Tables.Count > 0 Then
                cboUnit.SelectedIndex = 0
            End If
            If dsProgram.Tables.Count > 0 Then
                cboProgram.SelectedIndex = 0
            End If
            If dsBranch.Tables.Count > 0 Then
                cboBranch.SelectedIndex = 0
            End If

            chbCascadeBranch.Checked = False
            chbCascadeProgram.Checked = False
            dgvSelectedForms.Rows.Clear()
            lblSelectedFormCount.Text = "Count: " & dgvSelectedForms.Rows.Count.ToString

            LoadDataSets()
            LoadBranchGrid()
            LoadForms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub


#End Region



    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class