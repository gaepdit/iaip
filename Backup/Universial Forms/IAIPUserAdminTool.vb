Imports System.Data.OracleClient


Public Class IAIPUserAdminTool
    Dim dsOrginizations As DataSet
    Dim daOrginizations As OracleDataAdapter
    Dim dsDataGrid As DataSet
    Dim daDataGrid As OracleDataAdapter
    Dim dsAccounts As DataSet
    Dim daAccounts As OracleDataAdapter
    Dim dsPermissions As DataSet
    Dim daPermissions As OracleDataAdapter
    Dim SQL As String
    Dim cmd As OracleCommand
    Dim dr As OracleDataReader
    Dim recExist As Boolean

    Private Sub IAIPUserAdminTool_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            pnl1.Text = "   "
            pnl2.Text = UserName
            pnl3.Text = OracleDate

            LoadDataSets()
            LoadCombos()
            LoadDataGrid("Self")
            lblUserID.Text = UserGCode
            ' LoadUserData()

            mtbPhoneNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            mtbFaxNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals

            If AccountArray(8, 4) = "1" Then
                tsmOpenMaintenanceTool.Visible = True
                cboPermissionBranch.Enabled = True
                cboBranch.Enabled = True
            Else
                tsmOpenMaintenanceTool.Visible = False
                cboPermissionBranch.Enabled = False
                cboBranch.Enabled = False
                If AccountArray(8, 3) = "1" Then

                Else
                    cboPermissionProgram.Enabled = False
                    cboProgram.Enabled = False
                    If AccountArray(8, 2) = "1" Then

                    Else
                        If AccountArray(8, 1) = "1" Then
                            btnClearAllPermissions.Enabled = False
                            cboPermissionProgram.Enabled = False
                            cboPermissionAccounts.Enabled = False
                            cboProgram.Enabled = False
                            cboUnit.Enabled = False
                            rdbActiveStatus.Enabled = False
                            rdbInactiveStatus.Enabled = False
                            btnCreateNewUser.Visible = False
                        Else
                            txtFirstName.ReadOnly = True
                            txtLastName.ReadOnly = True
                            txtEmployeeID.ReadOnly = True
                            txtEmailAddress.ReadOnly = True
                            mtbPhoneNumber.ReadOnly = True
                            mtbFaxNumber.ReadOnly = True
                            btnSave.Enabled = False
                            txtUserName.ReadOnly = True
                            txtPassword.ReadOnly = True
                            btnClearAllPermissions.Enabled = False
                        End If
                    End If
                End If
            End If
            cboBranch.SelectedValue = UserBranch

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Sub LoadDataSets()
        Try
            dsOrginizations = New DataSet
            dsAccounts = New DataSet

            SQL = "select " & _
            "" & connNameSpace & ".lookupepdbranches.numBranchCode, strBranchDesc,  " & _
            "" & connNameSpace & ".lookupepdprograms.numProgramCode, strProgramDesc,  " & _
            "" & connNameSpace & ".lookupepdunits.numUnitCode, strUnitdesc " & _
            "from " & connNameSpace & ".Lookupepdbranches, " & connNameSpace & ".lookupepdprograms,  " & _
            "" & connNameSpace & ".lookupepdunits " & _
            "where " & connNameSpace & ".lookupepdbranches.numbranchcode = " & connNameSpace & ".lookupepdprograms.numbranchcode (+) " & _
            "and " & connNameSpace & ".lookupepdprograms.numprogramcode = " & connNameSpace & ".lookupepdunits.numprogramcode (+) " & _
            "order by strbranchdesc, strProgramDesc, strUnitDesc "

            daOrginizations = New OracleDataAdapter(SQL, conn)

            SQL = "Select " & _
            "numAccountCode, strAccountDesc, " & _
            "numBranchCode, numProgramCode, " & _
            "numUnitCode " & _
            "from " & connNameSpace & ".LookUpIAIPAccounts " & _
            "order by strAccountDesc "

            daAccounts = New OracleDataAdapter(SQL, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daOrginizations.Fill(dsOrginizations, "Orginization")
            daAccounts.Fill(dsAccounts, "Accounts")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub CheckPermissions()
        Try
            If lbl1.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl1.Text & ")") Then
                    chb1.Checked = True
                Else
                    chb1.Checked = False
                End If
            End If
            If lbl2.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl2.Text & ")") Then
                    chb2.Checked = True
                Else
                    chb2.Checked = False
                End If
            End If
            If lbl3.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl3.Text & ")") Then
                    chb3.Checked = True
                Else
                    chb3.Checked = False
                End If
            End If
            If lbl4.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl4.Text & ")") Then
                    chb4.Checked = True
                Else
                    chb4.Checked = False
                End If
            End If
            If lbl5.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl5.Text & ")") Then
                    chb5.Checked = True
                Else
                    chb5.Checked = False
                End If
            End If
            If lbl6.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl6.Text & ")") Then
                    chb6.Checked = True
                Else
                    chb6.Checked = False
                End If
            End If
            If lbl7.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl7.Text & ")") Then
                    chb7.Checked = True
                Else
                    chb7.Checked = False
                End If
            End If
            If lbl8.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl8.Text & ")") Then
                    chb8.Checked = True
                Else
                    chb8.Checked = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadCombos()
        Try
            Dim dtBranch As New DataTable
            Dim dtBranch2 As New DataTable
            Dim dtBranch3 As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim drNewRow2 As DataRow
            Dim drNewRow3 As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtBranch.Columns.Add("numBranchCode", GetType(System.String))
            dtBranch.Columns.Add("strBranchDesc", GetType(System.String))
            dtBranch2.Columns.Add("numBranchCode", GetType(System.String))
            dtBranch2.Columns.Add("strBranchDesc", GetType(System.String))
            dtBranch3.Columns.Add("numBranchCode", GetType(System.String))
            dtBranch3.Columns.Add("strBranchDesc", GetType(System.String))

            drNewRow = dtBranch.NewRow()
            drNewRow("numBranchCode") = ""
            drNewRow("strBranchDesc") = " "
            dtBranch.Rows.Add(drNewRow)

            drNewRow2 = dtBranch2.NewRow()
            drNewRow2("numBranchCode") = " "
            drNewRow2("strBranchDesc") = " "
            dtBranch2.Rows.Add(drNewRow2)

            drNewRow3 = dtBranch3.NewRow()
            drNewRow3("numBranchCode") = " "
            drNewRow3("strBranchDesc") = " "
            dtBranch3.Rows.Add(drNewRow3)

            For Each drDSRow In dsOrginizations.Tables("Orginization").Rows()
                drNewRow = dtBranch.NewRow()
                drNewRow("numBranchCode") = drDSRow("numBranchCode")
                drNewRow("strBranchDesc") = drDSRow("strBranchDesc")

                drNewRow2 = dtBranch2.NewRow()
                drNewRow2("numBranchCode") = drDSRow("numBranchCode")
                drNewRow2("strBranchDesc") = drDSRow("strBranchDesc")

                drNewRow3 = dtBranch3.NewRow()
                drNewRow3("numBranchCode") = drDSRow("numBranchCode")
                drNewRow3("strBranchDesc") = drDSRow("strBranchDesc")

                For i = 0 To dtBranch.Rows.Count - 1
                    If drDSRow("strBranchDesc") = dtBranch.Rows(i).Item(1) Then
                        temp = "No"
                    Else
                        temp = temp
                    End If
                Next
                If temp = "Add" Then
                    dtBranch.Rows.Add(drNewRow)
                    dtBranch2.Rows.Add(drNewRow2)
                    dtBranch3.Rows.Add(drNewRow3)
                End If
                temp = "Add"
            Next

            With cboBranch
                .DataSource = dtBranch
                .DisplayMember = "strBranchDesc"
                .ValueMember = "numBranchCode"
                .SelectedIndex = 0
            End With

            With cboSearchBranch
                .DataSource = dtBranch2
                .DisplayMember = "strBranchDesc"
                .ValueMember = "numBranchCode"
                .SelectedIndex = 0
            End With

            With cboPermissionBranch
                .DataSource = dtBranch3
                .DisplayMember = "strBranchDesc"
                .ValueMember = "numBranchCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region
#Region "Subs and Functions"
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
                                temp = temp
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
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadSearchProgram(ByVal SearchBranchCode As String)
        Try
            Dim dtProgram As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtProgram.Columns.Add("numProgramCode", GetType(System.String))
            dtProgram.Columns.Add("strProgramDesc", GetType(System.String))

            drNewRow = dtProgram.NewRow()
            drNewRow("numProgramCode") = " "
            drNewRow("strProgramDesc") = " "
            dtProgram.Rows.Add(drNewRow)

            If SearchBranchCode = " " Or SearchBranchCode = "" Then

            Else
                For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numBranchCode = " & SearchBranchCode, "strProgramDesc")
                    drNewRow = dtProgram.NewRow()
                    drNewRow("numProgramCode") = drDSRow("numProgramCode")
                    drNewRow("strProgramDesc") = drDSRow("strProgramDesc")

                    If Not IsDBNull(drDSRow("strProgramDesc")) Then
                        For i = 0 To dtProgram.Rows.Count - 1
                            If drDSRow("strProgramDesc") = dtProgram.Rows(i).Item(1) Then
                                temp = "No"
                            Else
                                temp = temp
                            End If
                        Next
                        If temp = "Add" Then
                            dtProgram.Rows.Add(drNewRow)
                        End If
                        temp = "Add"
                    End If
                Next
            End If
            With cboSearchProgram
                .DataSource = dtProgram
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadPermissionProgram(ByVal PermissionBranchcode As String)
        Try
            Dim dtUnit As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtUnit.Columns.Add("numProgramCode", GetType(System.String))
            dtUnit.Columns.Add("strProgramDesc", GetType(System.String))

            drNewRow = dtUnit.NewRow()
            drNewRow("numProgramCode") = " "
            drNewRow("strProgramDesc") = " "
            dtUnit.Rows.Add(drNewRow)

            For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numBranchCode = " & PermissionBranchcode, "strProgramDesc")
                drNewRow = dtUnit.NewRow()
                drNewRow("numProgramCode") = drDSRow("numProgramCode")
                drNewRow("strProgramDesc") = drDSRow("strProgramDesc")

                If Not IsDBNull(drDSRow("strProgramDesc")) Then
                    For i = 0 To dtUnit.Rows.Count - 1
                        If drDSRow("strProgramDesc") = dtUnit.Rows(i).Item(1) Then
                            temp = "No"
                        Else
                            temp = temp
                        End If
                    Next
                    If temp = "Add" Then
                        dtUnit.Rows.Add(drNewRow)
                    End If
                    temp = "Add"
                End If
            Next

            With cboPermissionProgram
                .DataSource = dtUnit
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .SelectedIndex = 0
            End With

            Dim dtAccounts As New DataTable
            Dim drNewRow2 As DataRow

            dtAccounts.Columns.Add("numAccountCode", GetType(System.String))
            dtAccounts.Columns.Add("strAccountDesc", GetType(System.String))

            drNewRow2 = dtAccounts.NewRow()
            drNewRow2("numAccountCode") = " "
            drNewRow2("strAccountDesc") = " "
            dtAccounts.Rows.Add(drNewRow2)

            With Me.cboPermissionAccounts
                .DataSource = dtAccounts
                .DisplayMember = "strAccountDesc"
                .ValueMember = "numAccountCode"
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                                temp = temp
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
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadSearchUnit(ByVal SearchProgram As String)
        Try
            Dim dtUnit As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtUnit.Columns.Add("numUnitCode", GetType(System.String))
            dtUnit.Columns.Add("strUnitDesc", GetType(System.String))

            drNewRow = dtUnit.NewRow()
            drNewRow("numUnitCode") = " "
            drNewRow("strUnitDesc") = " "
            dtUnit.Rows.Add(drNewRow)
            If SearchProgram = " " Or SearchProgram = "" Then

            Else
                For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numProgramCode = " & SearchProgram, "strUnitDesc")
                    drNewRow = dtUnit.NewRow()
                    drNewRow("numUnitCode") = drDSRow("numUnitCode")
                    drNewRow("strUnitDesc") = drDSRow("strUnitDesc")

                    If Not IsDBNull(drDSRow("strUnitDesc")) Then
                        For i = 0 To dtUnit.Rows.Count - 1
                            If drDSRow("strUnitDesc") = dtUnit.Rows(i).Item(1) Then
                                temp = "No"
                            Else
                                temp = temp
                            End If
                        Next
                        If temp = "Add" Then
                            dtUnit.Rows.Add(drNewRow)
                        End If
                        temp = "Add"
                    End If
                Next
            End If

            With cboSearchUnit
                .DataSource = dtUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadPermissionAccounts(ByVal PermissionProgramCode As String)
        Try
            Dim dtUnit As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i As Integer

            dtUnit.Columns.Add("numUnitCode", GetType(System.String))
            dtUnit.Columns.Add("strUnitDesc", GetType(System.String))

            drNewRow = dtUnit.NewRow()
            drNewRow("numUnitCode") = " "
            drNewRow("strUnitDesc") = " "
            dtUnit.Rows.Add(drNewRow)

            For Each drDSRow In dsOrginizations.Tables("Orginization").Select("numProgramCode = " & PermissionProgramCode, "strUnitDesc")
                drNewRow = dtUnit.NewRow()
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")

                If Not IsDBNull(drDSRow("strUnitDesc")) Then
                    For i = 0 To dtUnit.Rows.Count - 1
                        If drDSRow("strUnitDesc") = dtUnit.Rows(i).Item(1) Then
                            temp = "No"
                        Else
                            temp = temp
                        End If
                    Next
                    If temp = "Add" Then
                        dtUnit.Rows.Add(drNewRow)
                    End If
                    temp = "Add"
                End If
            Next

            With cboPermissionAccounts
                .DataSource = dtUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadUserTypes(ByVal UnitCode As String, ByVal ProgramCode As String, ByVal BranchCode As String)
        Try
            Dim dtAccount As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow
            Dim temp As String = "Add"
            Dim i, j As Integer

            dtAccount.Columns.Add("numAccountCode", GetType(System.String))
            dtAccount.Columns.Add("strAccountDesc", GetType(System.String))
            j = 1
            chb1.Visible = False
            chb2.Visible = False
            chb3.Visible = False
            chb4.Visible = False
            chb5.Visible = False
            chb6.Visible = False
            chb7.Visible = False
            chb8.Visible = False
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

                If Not IsDBNull(drDSRow("strAccountDesc")) Then
                    For i = 0 To dtAccount.Rows.Count - 1
                        If drDSRow("strAccountDesc") = dtAccount.Rows(i).Item(1) Then
                            temp = "No"
                        Else
                            temp = temp
                        End If
                    Next
                    If temp = "Add" Then
                        Select Case j
                            Case 1
                                chb1.Text = drDSRow("strAccountDesc")
                                lbl1.Text = drDSRow("numAccountCode")
                                chb1.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl1.Text & ")") Then
                                    chb1.Checked = True
                                Else
                                    chb1.Checked = False
                                End If
                            Case 2
                                chb2.Text = drDSRow("strAccountDesc")
                                lbl2.Text = drDSRow("numAccountCode")
                                chb2.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl2.Text & ")") Then
                                    chb2.Checked = True
                                Else
                                    chb2.Checked = False
                                End If
                            Case 3
                                chb3.Text = drDSRow("strAccountDesc")
                                lbl3.Text = drDSRow("numAccountCode")
                                chb3.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl3.Text & ")") Then
                                    chb3.Checked = True
                                Else
                                    chb3.Checked = False
                                End If
                            Case 4
                                chb4.Text = drDSRow("strAccountDesc")
                                lbl4.Text = drDSRow("numAccountCode")
                                chb4.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl4.Text & ")") Then
                                    chb4.Checked = True
                                Else
                                    chb4.Checked = False
                                End If
                            Case 5
                                chb5.Text = drDSRow("strAccountDesc")
                                lbl5.Text = drDSRow("numAccountCode")
                                chb5.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl5.Text & ")") Then
                                    chb5.Checked = True
                                Else
                                    chb5.Checked = False
                                End If
                            Case 6
                                chb6.Text = drDSRow("strAccountDesc")
                                lbl6.Text = drDSRow("numAccountCode")
                                chb6.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl6.Text & ")") Then
                                    chb6.Checked = True
                                Else
                                    chb6.Checked = False
                                End If
                            Case 7
                                chb7.Text = drDSRow("strAccountDesc")
                                lbl7.Text = drDSRow("numAccountCode")
                                chb7.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl7.Text & ")") Then
                                    chb7.Checked = True
                                Else
                                    chb7.Checked = False
                                End If
                            Case 8
                                chb8.Text = drDSRow("strAccountDesc")
                                lbl8.Text = drDSRow("numAccountCode")
                                chb8.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl8.Text & ")") Then
                                    chb8.Checked = True
                                Else
                                    chb8.Checked = False
                                End If
                            Case Else
                        End Select
                        j += 1
                    End If
                    temp = "Add"
                End If
            Next
            If AccountArray(8, 2) = "0" And AccountArray(8, 3) = "0" And AccountArray(8, 4) = "0" Then
                chb1.Enabled = False
                chb2.Enabled = False
                chb3.Enabled = False
                chb4.Enabled = False
                chb5.Enabled = False
                chb6.Enabled = False
                chb7.Enabled = False
                chb8.Enabled = False
            Else
                chb1.Enabled = True
                chb2.Enabled = True
                chb3.Enabled = True
                chb4.Enabled = True
                chb5.Enabled = True
                chb6.Enabled = True
                chb7.Enabled = True
                chb8.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub UpdatePermissions(ByVal UserId As String)
        Try
            SQL = "Select " & _
            "numUserId " & _
            "from " & connNameSpace & ".IAIPPermissions " & _
            "where numUserId = '" & lblUserID.Text & "' "
            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = False Then
                SQL = "Insert into " & connNameSpace & ".IAIPPermissions " & _
                "values " & _
                "('" & lblUserID.Text & "', '" & lblPermissions.Text & "') "
            Else
                SQL = "Update " & connNameSpace & ".IAIPPermissions set " & _
                "strIAIPPermissions = '" & lblPermissions.Text & "' " & _
                "where numUserID = '" & lblUserID.Text & "' "
            End If

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            CheckPermissions()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub CreateNewUser()
        Try
            Dim EmployeeStatus As String = "0"
            Dim EmployeeId As String = "0"

            If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
                If txtUserName.Text <> "" And txtPassword.Text <> "" Then
                    If rdbActiveStatus.Checked = True Then
                        EmployeeStatus = "1"
                    Else
                        EmployeeStatus = "0"
                    End If

                    SQL = "Select " & _
                    "strUserName " & _
                    "from " & connNameSpace & ".EPDUsers " & _
                    "where strUsername = '" & txtUserName.Text & "' "

                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        MsgBox("The User Name is already in use by another user." & vbCrLf & "Please choose another User Name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                    Else
                        SQL = "Insert into " & connNameSpace & ".EPDUsers " & _
                        "(numUserID, strUserName, " & _
                        "strPassword) " & _
                        "values " & _
                        "((select (max(numUserID) + 1) from " & connNameSpace & ".EPDUsers), " & _
                        "'" & Replace(txtUserName.Text, "'", "''") & "', " & _
                        "'" & Replace(EncryptDecrypt.EncryptText(txtPassword.Text), "'", "''") & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()


                        'SQL = "select " & connNameSpace & ".SEQ_EPD_Users.currval " & _
                        '"from dual "
                        SQL = "select max(numUserID) as maxUser " & _
                        "from " & connNameSpace & ".EPDUsers "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            lblUserID.Text = dr.Item("maxUser")
                        End While
                        dr.Close()

                        If txtEmployeeID.Text <> "" Then
                            EmployeeId = txtEmployeeID.Text
                        Else
                            EmployeeId = "000"
                        End If

                        SQL = "Insert into " & connNameSpace & ".EPDUSerProfiles " & _
                        "(numUserID, strEmployeeID, " & _
                        "strLastName, strFirstName, " & _
                        "strEmailAddress, strPhone, " & _
                        "strFax, numBranch, " & _
                        "numProgram, numUnit, " & _
                        "strOffice, numEmployeeStatus) " & _
                        "values " & _
                        "('" & lblUserID.Text & "', '" & Replace(EmployeeId, "'", "''") & "', " & _
                        "'" & Replace(txtLastName.Text, "'", "''") & "', '" & Replace(txtFirstName.Text, "'", "''") & "', " & _
                        "'" & Replace(txtEmailAddress.Text, "'", "''") & "', " & _
                        "'" & mtbPhoneNumber.Text & "', " & _
                        "'" & mtbFaxNumber.Text & "', " & _
                        "'" & cboBranch.SelectedValue & "', " & _
                        "'" & cboProgram.SelectedValue & "', '" & cboUnit.SelectedValue & "', " & _
                        "'" & Replace(txtOfficeNumber.Text, "'", "''") & "', '" & EmployeeStatus & "') "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        UpdatePermissions(lblUserID.Text)

                        LoadUserData()

                        MsgBox("Successfully Done", MsgBoxStyle.Information, "IAIP User Admin Tool")

                    End If
                Else
                    MsgBox("Please enter a User Name and Password.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                End If
            Else
                MsgBox("Please enter a First and Last name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub UpdateUser()
        Try
            Dim EmployeeStatus As String = "0"
            Dim EmployeeID As String = ""
            Dim ResultDialog As DialogResult

            If lblEmailAddress.Text <> "Email: " & txtEmailAddress.Text Then
                ResultDialog = MessageBox.Show("You are about to update a user and the email address are different." & vbCrLf & _
                        "Are you sure you want to update the informaiton and not create a new user?", _
                   Me.Text, MessageBoxButtons.YesNoCancel, _
                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case ResultDialog
                    Case Windows.Forms.DialogResult.Yes

                    Case Else
                        Exit Sub
                End Select
            End If

            If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
                If txtUserName.Text <> "" And txtPassword.Text <> "" Then
                    If txtEmployeeID.Text = "" Then
                        EmployeeID = lblUserID.Text
                    Else
                        EmployeeID = txtEmployeeID.Text
                    End If

                    If rdbActiveStatus.Checked = True Then
                        EmployeeStatus = "1"
                    Else
                        EmployeeStatus = "0"
                    End If
                    SQL = "Select " & _
                    "strUserName " & _
                    "from " & connNameSpace & ".EPDUsers " & _
                    "where strUserName = '" & txtUserName.Text & "' " & _
                    "and numUserID <> '" & lblUserID.Text & "' "
                    cmd = New OracleCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        MsgBox("The User Name is already in use by another user." & vbCrLf & "Please choose another User Name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                    Else
                        SQL = "Update " & connNameSpace & ".EPDUsers set " & _
                        "strUserName = '" & Replace(txtUserName.Text, "'", "''") & "', " & _
                        "strPassword = '" & Replace(EncryptDecrypt.EncryptText(txtPassword.Text), "'", "''") & "' " & _
                        "where numUserId = '" & lblUserID.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Update " & connNameSpace & ".EPDUserProfiles set " & _
                        "strEmployeeID = '" & Replace(EmployeeID, "'", "''") & "', " & _
                        "strLastName = '" & Replace(txtLastName.Text, "'", "''") & "', " & _
                        "strFirstName = '" & Replace(txtFirstName.Text, "'", "''") & "', " & _
                        "strEmailAddress = '" & Replace(txtEmailAddress.Text, "'", "''") & "', " & _
                        "strPhone = '" & mtbPhoneNumber.Text & "', " & _
                        "strFax = '" & mtbFaxNumber.Text & "', " & _
                        "numBranch = '" & cboBranch.SelectedValue & "', " & _
                        "numProgram = '" & cboProgram.SelectedValue & "', " & _
                        "numUnit = '" & cboUnit.SelectedValue & "', " & _
                        "strOffice = '" & Replace(txtOfficeNumber.Text, "'", "''") & "', " & _
                        "numEmployeeStatus = '" & EmployeeStatus & "' " & _
                        "where numUserId = '" & lblUserID.Text & "' "

                        cmd = New OracleCommand(SQL, conn)
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        UpdatePermissions(lblUserID.Text)

                        LoadUserData()

                        MsgBox("Successfully Done", MsgBoxStyle.Information, "IAIP User Admin Tool")
                    End If
                Else
                    MsgBox("Please enter a User Name and Password.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                End If
            Else
                MsgBox("Please enter a First and Last name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadDataGrid(ByVal SearchType As String)
        Try
            Select Case SearchType
                Case "All"
                    SQL = "select " & _
                    "" & connNameSpace & ".EPDUsers.numUserID, " & _
                    "strUserName, strPassword,  " & _
                    "strEmployeeId, strLastname,  " & _
                    "strFirstName, strEmailAddress,  " & _
                    "strPhone, strFax,  " & _
                    "strBranchDesc,  " & _
                    "strProgramDesc,  " & _
                    "strUnitDesc,  " & _
                    "strOffice,  " & _
                    "case  " & _
                    "when numEmployeeStatus = '1' then 'Active'  " & _
                    "when numEmployeeStatus = '0' then 'Inactive'  " & _
                    "else 'Inactive' " & _
                    "End EmployeeStatus  " & _
                    "from " & connNameSpace & ".EPDUsers, " & connNameSpace & ".EPDUserProfiles,  " & _
                    "" & connNameSpace & ".LookupEPDBranches, " & connNameSpace & ".LookupEPDPrograms,  " & _
                    "" & connNameSpace & ".LookUpEPDUnits  " & _
                    "where " & connNameSpace & ".epdusers.numuserID = " & connNameSpace & ".EPDUserProfiles.numUserId  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numBranch = " & connNameSpace & ".LookupEPDBranches.numBranchcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numProgram = " & connNameSpace & ".lookupEPDPrograms.numProgramcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitcode (+) "
                Case "Self"
                    SQL = "select " & _
                    "" & connNameSpace & ".EPDUsers.numUserID, " & _
                    "strUserName, strPassword,  " & _
                    "strEmployeeId, strLastname,  " & _
                    "strFirstName, strEmailAddress,  " & _
                    "strPhone, strFax,  " & _
                    "strBranchDesc,  " & _
                    "strProgramDesc,  " & _
                    "strUnitDesc,  " & _
                    "strOffice,  " & _
                    "case  " & _
                    "when numEmployeeStatus = '1' then 'Active'  " & _
                    "when numEmployeeStatus = '0' then 'Inactive'  " & _
                    "else 'Inactive' " & _
                    "End EmployeeStatus  " & _
                    "from " & connNameSpace & ".EPDUsers, " & connNameSpace & ".EPDUserProfiles,  " & _
                    "" & connNameSpace & ".LookupEPDBranches, " & connNameSpace & ".LookupEPDPrograms,  " & _
                    "" & connNameSpace & ".LookUpEPDUnits  " & _
                    "where " & connNameSpace & ".epdusers.numuserID = " & connNameSpace & ".EPDUserProfiles.numUserId  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numBranch = " & connNameSpace & ".LookupEPDBranches.numBranchcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numProgram = " & connNameSpace & ".lookupEPDPrograms.numProgramcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitcode (+) " & _
                    "and " & connNameSpace & ".EPDUsers.numUserId = '" & UserGCode & "' "
                Case "Search"
                    SQL = "select " & _
                    "" & connNameSpace & ".EPDUsers.numUserID, " & _
                    "strUserName, strPassword,  " & _
                    "strEmployeeId, strLastname,  " & _
                    "strFirstName, strEmailAddress,  " & _
                    "strPhone, strFax,  " & _
                    "strBranchDesc,  " & _
                    "strProgramDesc,  " & _
                    "strUnitDesc,  " & _
                    "strOffice,  " & _
                    "case  " & _
                    "when numEmployeeStatus = '1' then 'Active'  " & _
                    "when numEmployeeStatus = '0' then 'Inactive'  " & _
                    "else 'Inactive' " & _
                    "End EmployeeStatus  " & _
                    "from " & connNameSpace & ".EPDUsers, " & connNameSpace & ".EPDUserProfiles,  " & _
                    "" & connNameSpace & ".LookupEPDBranches, " & connNameSpace & ".LookupEPDPrograms,  " & _
                    "" & connNameSpace & ".LookUpEPDUnits  " & _
                    "where " & connNameSpace & ".epdusers.numuserID = " & connNameSpace & ".EPDUserProfiles.numUserId  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numBranch = " & connNameSpace & ".LookupEPDBranches.numBranchcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numProgram = " & connNameSpace & ".lookupEPDPrograms.numProgramcode (+)  " & _
                    "and " & connNameSpace & ".EPDUserProfiles.numUnit = " & connNameSpace & ".LookUpEPDUnits.numUnitcode (+) "

                    If cboSearchBranch.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numbranch = '" & cboSearchBranch.SelectedValue & "' "
                    End If
                    If cboSearchProgram.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numProgram = '" & cboSearchProgram.SelectedValue & "' "
                    End If
                    If cboSearchUnit.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numUnit = '" & cboSearchUnit.SelectedValue & "' "
                    End If
                    If txtSearchEmployeeID.Text <> "" Then
                        SQL = SQL & " and Upper(strEmployeeID) like '%" & Replace(txtSearchEmployeeID.Text.ToUpper, "'", "''") & "%' "
                    End If
                    If txtSearchFirstName.Text <> "" Then
                        SQL = SQL & " and Upper(strFirstname) like '%" & Replace(txtSearchFirstName.Text.ToUpper, "'", "''") & "%' "
                    End If
                    If txtSearchLastName.Text <> "" Then
                        SQL = SQL & " and Upper(strLastName) like '%" & Replace(txtSearchLastName.Text.ToUpper, "'", "''") & "%' "
                    End If
                Case Else

            End Select

            dsDataGrid = New DataSet
            daDataGrid = New OracleDataAdapter(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            daDataGrid.Fill(dsDataGrid, "DataGrid")

            dgvUserAdminTool.DataSource = dsDataGrid
            dgvUserAdminTool.DataMember = "DataGrid"

            dgvUserAdminTool.RowHeadersVisible = False
            dgvUserAdminTool.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvUserAdminTool.AllowUserToResizeColumns = True
            dgvUserAdminTool.AllowUserToAddRows = False
            dgvUserAdminTool.AllowUserToDeleteRows = False
            dgvUserAdminTool.AllowUserToOrderColumns = True
            dgvUserAdminTool.AllowUserToResizeRows = True
            dgvUserAdminTool.ColumnHeadersHeight = "35"
            dgvUserAdminTool.Columns("numUserID").HeaderText = "User ID"
            dgvUserAdminTool.Columns("numUserID").DisplayIndex = 0
            dgvUserAdminTool.Columns("numUserID").Visible = False
            dgvUserAdminTool.Columns("strFirstName").HeaderText = "First Name"
            dgvUserAdminTool.Columns("strFirstName").DisplayIndex = 1
            dgvUserAdminTool.Columns("strLastname").HeaderText = "Last Name"
            dgvUserAdminTool.Columns("strLastname").DisplayIndex = 2
            dgvUserAdminTool.Columns("strEmployeeId").HeaderText = "Employee ID"
            dgvUserAdminTool.Columns("strEmployeeId").DisplayIndex = 3
            dgvUserAdminTool.Columns("strBranchDesc").HeaderText = "Branch"
            dgvUserAdminTool.Columns("strBranchDesc").DisplayIndex = 4
            dgvUserAdminTool.Columns("strBranchDesc").Width = "125"
            dgvUserAdminTool.Columns("strProgramDesc").HeaderText = "Program"
            dgvUserAdminTool.Columns("strProgramDesc").DisplayIndex = 5
            dgvUserAdminTool.Columns("strProgramDesc").Width = "175"
            dgvUserAdminTool.Columns("strUnitDesc").HeaderText = "Unit"
            dgvUserAdminTool.Columns("strUnitDesc").DisplayIndex = 6
            dgvUserAdminTool.Columns("strUnitDesc").Width = "175"
            dgvUserAdminTool.Columns("EmployeeStatus").HeaderText = "Employee Status"
            dgvUserAdminTool.Columns("EmployeeStatus").DisplayIndex = 7
            dgvUserAdminTool.Columns("strEmailAddress").HeaderText = "Email Address"
            dgvUserAdminTool.Columns("strEmailAddress").DisplayIndex = 8
            dgvUserAdminTool.Columns("strPhone").HeaderText = "Phone Number"
            dgvUserAdminTool.Columns("strPhone").DisplayIndex = 9
            dgvUserAdminTool.Columns("strFax").HeaderText = "Fax Number"
            dgvUserAdminTool.Columns("strFax").DisplayIndex = 10
            dgvUserAdminTool.Columns("strOffice").HeaderText = "Office"
            dgvUserAdminTool.Columns("strOffice").DisplayIndex = 11
            dgvUserAdminTool.Columns("strUserName").HeaderText = "User name"
            dgvUserAdminTool.Columns("strUserName").DisplayIndex = 12
            dgvUserAdminTool.Columns("strPassword").HeaderText = "Password"
            dgvUserAdminTool.Columns("strPassword").DisplayIndex = 13
            dgvUserAdminTool.Columns("strPassword").Visible = False

            lblCount.Text = "Count: " & dgvUserAdminTool.Rows.Count

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadCurrentPermissions()
        Try
            Dim drDSRow As DataRow
            Dim temp As String = ""
            Dim Permissions As String = ""

            If lblPermissions.Text <> "" Then
                Permissions = lblPermissions.Text
                txtCurrentPermissions.Clear()
                Do While Permissions <> ""
                    temp = Mid(Permissions, InStr(Permissions, "(", CompareMethod.Text) + 1, InStr(Permissions, ")", CompareMethod.Text) - 2)
                    Permissions = Replace(Permissions, "(" & temp & ")", "")
                    For Each drDSRow In dsAccounts.Tables("Accounts").Select("numAccountCode = " & temp, "strAccountdesc")
                        txtCurrentPermissions.Text = txtCurrentPermissions.Text & drDSRow("strAccountdesc") & vbCrLf
                    Next
                Loop
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadUserData()
        Try
            SQL = "select " & _
            "" & connNameSpace & ".epdusers.numuserID,  " & _
            "strUserName, strPassword,  " & _
            "strEmployeeId, strLastname,  " & _
            "strFirstName, strEmailAddress,  " & _
            "strPhone, strFax,  " & _
            "numBranch,  " & _
            "numProgram,  " & _
            "numUnit,  " & _
            "strOffice,  " & _
            "numEmployeeStatus   " & _
            "from " & connNameSpace & ".EPDUsers, " & connNameSpace & ".EPDUserProfiles  " & _
            "where " & connNameSpace & ".epdusers.numuserID = " & connNameSpace & ".EPDUserProfiles.numUserId " & _
            "and " & connNameSpace & ".epdusers.numuserid = '" & lblUserID.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFirstname")) Then
                    lblFirstName.Text = "First Name: "
                    txtFirstName.Clear()
                Else
                    lblFirstName.Text = "First Name: " & dr.Item("strFirstName")
                    txtFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    lblLastName.Text = "Last Name: "
                    txtLastName.Clear()
                Else
                    lblLastName.Text = "Last Name: " & dr.Item("strlastName")
                    txtLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strEmployeeID")) Then
                    lblEmployeeID.Text = "Employee ID: "
                    txtEmployeeID.Clear()
                Else
                    lblEmployeeID.Text = "Employee ID: " & dr.Item("strEmployeeID")
                    txtEmployeeID.Text = dr.Item("strEmployeeID")
                End If
                If IsDBNull(dr.Item("strEmailAddress")) Then
                    lblEmailAddress.Text = "Email: "
                    txtEmailAddress.Clear()
                Else
                    lblEmailAddress.Text = "Email: " & dr.Item("strEmailAddress")
                    txtEmailAddress.Text = dr.Item("strEmailAddress")
                End If
                If IsDBNull(dr.Item("strPhone")) Then
                    lblPhoneNumber.Text = "Phone: "
                    mtbPhoneNumber.Clear()
                Else
                    lblPhoneNumber.Text = "Phone: (" & Mid(dr.Item("strPhone"), 1, 3) & ") " & Mid(dr.Item("strPhone"), 4, 3) & "-" & Mid(dr.Item("strPhone"), 7)
                    mtbPhoneNumber.Text = dr.Item("strPhone")
                End If
                If IsDBNull(dr.Item("strFax")) Then
                    lblFaxNumber.Text = "Fax: "
                    mtbFaxNumber.Clear()
                Else
                    lblFaxNumber.Text = "Fax: (" & Mid(dr.Item("strFax"), 1, 3) & ") " & Mid(dr.Item("strFax"), 4, 3) & "-" & Mid(dr.Item("strFax"), 7)
                    mtbFaxNumber.Text = dr.Item("strFax")
                End If
                If IsDBNull(dr.Item("strUserName")) Then
                    lblUserName.Text = "User Name: "
                    txtUserName.Clear()
                Else
                    lblUserName.Text = "User Name: " & dr.Item("strUserName")
                    txtUserName.Text = dr.Item("strUserName")
                End If
                If IsDBNull(dr.Item("strPassword")) Then
                    lblPassword.Text = "Password: "
                    txtPassword.Clear()
                Else
                    lblPassword.Text = "Password: *****"
                    txtPassword.Text = EncryptDecrypt.DecryptText(dr.Item("strPassword"))
                End If
                If IsDBNull(dr.Item("strOffice")) Then
                    txtOfficeNumber.Text = ""
                Else
                    txtOfficeNumber.Text = dr.Item("strOffice")
                End If
                If IsDBNull(dr.Item("numEmployeeStatus")) Then
                    rdbActiveStatus.Checked = False
                    rdbInactiveStatus.Checked = True
                Else
                    If dr.Item("numEmployeeStatus") = "1" Then
                        rdbActiveStatus.Checked = True
                    Else
                        rdbInactiveStatus.Checked = True
                    End If
                End If
                If IsDBNull(dr.Item("numBranch")) Then
                    If cboBranch.SelectedIndex > -1 Then
                        cboBranch.SelectedIndex = 0
                    End If
                    If cboPermissionBranch.SelectedIndex > -1 Then
                        cboPermissionBranch.SelectedIndex = 0
                    End If
                Else
                    cboBranch.SelectedValue = dr.Item("numBranch")
                    cboPermissionBranch.SelectedValue = dr.Item("numBranch")
                End If
                If IsDBNull(dr.Item("numProgram")) Then
                    If cboProgram.SelectedIndex > -1 Then
                        cboProgram.SelectedIndex = 0
                    End If
                    If cboPermissionProgram.SelectedIndex > -1 Then
                        cboPermissionProgram.SelectedIndex = 0
                    End If
                Else
                    cboProgram.SelectedValue = dr.Item("numProgram")
                    cboPermissionProgram.SelectedValue = dr.Item("numProgram")
                End If
                If IsDBNull(dr.Item("numUnit")) Then
                    'cboUnit.SelectedIndex = 0
                    'cboPermissionAccounts.SelectedIndex = 0
                    cboUnit.Text = ""
                    cboPermissionAccounts.Text = ""
                Else
                    cboUnit.SelectedValue = dr.Item("numUnit")
                    cboPermissionAccounts.SelectedValue = dr.Item("numUnit")
                End If
            End While

            lblPermissions.Text = ""

            SQL = "Select " & _
            "numUserId, strIAIPPermissions " & _
            "from " & connNameSpace & ".IAIPPermissions " & _
            "where numUserID = '" & lblUserID.Text & "' "

            cmd = New OracleCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strIAIPPermissions")) Then
                    lblPermissions.Text = ""
                Else
                    lblPermissions.Text = dr.Item("strIAIPPermissions")
                End If
            End While
            dr.Close()
            LoadCurrentPermissions()

            CheckPermissions()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub ClearData()
        Try
            lblUserID.Text = ""
            lblFirstName.Text = "First Name: "
            txtFirstName.Clear()
            lblLastName.Text = "Last Name: "
            txtLastName.Clear()
            lblEmployeeID.Text = "Employee ID: "
            txtEmployeeID.Clear()
            lblEmailAddress.Text = "Email: "
            txtEmailAddress.Clear()
            lblPhoneNumber.Text = "Phone: "
            mtbPhoneNumber.Clear()
            lblFaxNumber.Text = "Fax: "
            mtbFaxNumber.Clear()
            lblUserName.Text = "User Name: "
            txtUserName.Clear()
            lblPassword.Text = "Password: "
            txtPassword.Clear()
            txtOfficeNumber.Text = ""
            rdbActiveStatus.Checked = False
            rdbInactiveStatus.Checked = True
            rdbInactiveStatus.Checked = True
            cboBranch.SelectedIndex = 0
            cboPermissionBranch.SelectedIndex = 0
            cboProgram.SelectedIndex = 0
            cboPermissionProgram.SelectedIndex = 0
            cboUnit.SelectedIndex = 0
            cboPermissionAccounts.SelectedIndex = 0
            txtCurrentPermissions.Clear()

            If cboBranch.Enabled = False Then
                cboBranch.SelectedValue = UserBranch
                cboPermissionBranch.SelectedValue = UserBranch
            End If
            If cboProgram.Enabled = False Then
                cboProgram.SelectedValue = UserProgram
                cboPermissionProgram.SelectedValue = UserProgram
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region
#Region "Declarations"
    Private Sub cboBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBranch.SelectedValueChanged
        Try
            If cboBranch.SelectedIndex > 0 Then
                LoadProgram(cboBranch.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboSearchBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearchBranch.SelectedValueChanged
        Try
            If cboSearchBranch.SelectedIndex > 0 Then
                LoadSearchProgram(cboSearchBranch.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboPermissionBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPermissionBranch.SelectedValueChanged
        Try
            If cboPermissionBranch.SelectedIndex > 0 Then
                LoadPermissionProgram(cboPermissionBranch.SelectedValue)
                LoadUserTypes("", "", cboPermissionBranch.SelectedValue)
            Else
                LoadUserTypes("", "", "")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProgram.SelectedValueChanged
        Try
            If cboProgram.SelectedIndex > 0 Then
                LoadUnit(cboProgram.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboSearchProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearchProgram.SelectedValueChanged
        Try
            If cboSearchProgram.SelectedIndex > 0 Then
                LoadSearchUnit(cboSearchProgram.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboPermissionProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPermissionProgram.SelectedValueChanged
        Try
            If cboPermissionProgram.SelectedIndex > 0 Then
                LoadPermissionAccounts(cboPermissionProgram.SelectedValue)
                LoadUserTypes("", cboPermissionProgram.SelectedValue, cboPermissionBranch.SelectedValue)
            Else
                LoadUserTypes("", "", cboPermissionBranch.SelectedValue)
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboPermissionAccounts_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPermissionAccounts.SelectedValueChanged
        Try
            If cboPermissionAccounts.SelectedIndex > 0 Then
                LoadUserTypes(cboPermissionAccounts.SelectedValue, cboPermissionProgram.SelectedValue, cboPermissionBranch.SelectedValue)
            Else
                LoadUserTypes("", cboPermissionProgram.SelectedValue, cboPermissionBranch.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnCreateNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNewUser.Click
        Try
            CreateNewUser()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If lblUserID.Text <> "" And lblUserID.Text <> "numUserID" Then
                If AccountArray(8, 2) = "0" And AccountArray(8, 3) = "0" And AccountArray(8, 4) = "0" Then
                    If lblUserID.Text = UserGCode Then
                        UpdateUser()
                    Else
                        MsgBox("Sorry but you only have attorization to edit your own data.", MsgBoxStyle.Information, "IAIP User Admin Tool")
                    End If
                Else
                    If AccountArray(8, 4) = "0" Then
                        If AccountArray(8, 3) = "0" Then
                            If UserBranch.ToString = cboBranch.SelectedValue.ToString And UserProgram.ToString = cboProgram.SelectedValue.ToString Then
                                UpdateUser()
                            Else
                                MsgBox("Sorry but you only have attorization to edit users in your Branch and Program.", _
                                         MsgBoxStyle.Information, "IAIP User Admin Tool")
                            End If
                        Else
                            If UserBranch.ToString = cboBranch.SelectedValue.ToString Then
                                UpdateUser()
                            Else
                                MsgBox("Sorry but you only have attorization to edit users in your Branch.", _
                                         MsgBoxStyle.Information, "IAIP User Admin Tool")
                            End If
                        End If
                    Else
                        UpdateUser()
                    End If
                End If
            Else
                MsgBox("Select a user for the search tool below before making and changes and saving data.", MsgBoxStyle.Information, "IAIP User Admin Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        Try
            If lblUserID.Text <> "" And lblUserID.Text <> "numUserID" Then
                UpdateUser()
            Else
                MsgBox("Select a user for the search tool below before making and changes and saving data.", MsgBoxStyle.Information, "IAIP User Admin Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            LoadDataGrid("Search")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click
        Try
            LoadDataGrid("All")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub dgvUserAdminTool_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvUserAdminTool.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvUserAdminTool.HitTest(e.X, e.Y)
            If dgvUserAdminTool.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvUserAdminTool.Columns(0).HeaderText = "User ID" Then
                    lblUserID.Text = dgvUserAdminTool(0, hti.RowIndex).Value
                    If lblUserID.Text <> "" Then
                        LoadUserData()
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            txtSearchEmployeeID.Clear()
            txtSearchFirstName.Clear()
            txtSearchLastName.Clear()
            cboSearchBranch.SelectedIndex = 0
            LoadSearchProgram(cboSearchBranch.SelectedValue)
            LoadSearchUnit(cboSearchProgram.SelectedValue)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsmOpenMaintenanceTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOpenMaintenanceTool.Click
        Try
            If ListTool Is Nothing Then
                If ListTool Is Nothing Then ListTool = New IAIPListTool
            Else
                ListTool.Dispose()
                ListTool = New IAIPListTool
            End If
            ListTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            ListTool.Show()
            ListTool.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb1_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb1.CheckStateChanged
        Try

            If lblPermissions.Text.Contains(lbl1.Text) And chb1.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl1.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl1.Text & ")", "")
                If chb1.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl1.Text & ")"
                End If
            End If

            'lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl1.Text & ")", "")
            'If chb1.Checked = True Then
            '    lblPermissions.Text = lblPermissions.Text & "(" & lbl1.Text & ")"
            'End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb2_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb2.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl2.Text) And chb2.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl2.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl2.Text & ")", "")
                If chb2.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl2.Text & ")"
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb3_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb3.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl3.Text) And chb3.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl3.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl3.Text & ")", "")
                If chb3.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl3.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb4_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb4.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl4.Text) And chb4.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl4.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl4.Text & ")", "")
                If chb4.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl4.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb5_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb5.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl5.Text) And chb5.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl5.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl5.Text & ")", "")
                If chb5.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl5.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb6_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb6.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl6.Text) And chb6.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl6.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl6.Text & ")", "")
                If chb6.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl6.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb7_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb7.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl7.Text) And chb7.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl7.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl7.Text & ")", "")
                If chb7.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl7.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb8_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb8.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl8.Text) And chb8.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl8.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl8.Text & ")", "")
                If chb8.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl8.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnClearAllPermissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllPermissions.Click
        Try
            lblPermissions.Text = ""

            If lblUserID.Text <> "" Then
                UpdatePermissions(lblUserID.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClear.Click
        Try
            ClearData()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub tsbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBack.Click
        Try
            Me.Hide()
            UserAdminTool.Dispose()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#End Region

    Private Sub tsbViewOrgChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbViewOrgChart.Click
        Try
            If PrintOut Is Nothing Then
                If PrintOut Is Nothing Then PrintOut = New IAIPPrintOut
            Else
                PrintOut.Dispose()
                PrintOut = New IAIPPrintOut
            End If
            PrintOut.txtPrintType.Text = "OrgChart"

            PrintOut.Show()
            PrintOut.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub HelpOnlineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpOnlineToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, "http://airpermit.dnr.state.ga.us/helpdocs/IAIP_help/")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub


    Private Sub mmiViewPhoneList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiViewPhoneList.Click
        Try
            If PhoneList Is Nothing Then
                If PhoneList Is Nothing Then PhoneList = New IAIPPhoneList
            Else
                PhoneList.Dispose()
                PhoneList = New IAIPPhoneList
            End If
            PhoneList.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
            PhoneList.Show()
            PhoneList.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class