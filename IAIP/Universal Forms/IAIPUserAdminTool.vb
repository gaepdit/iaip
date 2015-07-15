Imports Oracle.ManagedDataAccess.Client


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
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            LoadDataSets()
            LoadCombos()
            LoadDataGrid("Self")
            lblUserID.Text = UserGCode

            mtbPhoneNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            mtbFaxNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals

            If AccountFormAccess(8, 4) = "1" Then
                cboPermissionBranch.Enabled = True
                cboBranch.Enabled = True
            Else
                cboPermissionBranch.Enabled = False
                cboBranch.Enabled = False
                If AccountFormAccess(8, 3) = "1" Then

                Else
                    cboPermissionProgram.Enabled = False
                    cboProgram.Enabled = False
                    If AccountFormAccess(8, 2) = "1" Then

                    Else
                        If AccountFormAccess(8, 1) = "1" Then
                            btnClearAllPermissions.Enabled = False
                            cboPermissionProgram.Enabled = False
                            cboProgram.Enabled = False
                            cboUnit.Enabled = False
                            rdbActiveStatus.Enabled = False
                            rdbInactiveStatus.Enabled = False
                            btnCreateNewUser.Visible = False
                        Else
                            txtFirstName.ReadOnly = True
                            txtLastName.ReadOnly = True
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
            If lbl9.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl9.Text & ")") Then
                    chb9.Checked = True
                Else
                    chb9.Checked = False
                End If
            End If
            If lbl10.Text <> "" Then
                If lblPermissions.Text.Contains("(" & lbl10.Text & ")") Then
                    chb10.Checked = True
                Else
                    chb10.Checked = False
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                        'temp = temp
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

#End Region

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
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            With cboSearchProgram
                .DataSource = dtProgram
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                            'temp = temp
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

            'With Me.cboPermissionAccounts
            '    .DataSource = dtAccounts
            '    .DisplayMember = "strAccountDesc"
            '    .ValueMember = "numAccountCode"
            '    .SelectedIndex = 0
            'End With


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
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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

            With cboSearchUnit
                .DataSource = dtUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
                            'temp = temp
                        End If
                    Next
                    If temp = "Add" Then
                        dtUnit.Rows.Add(drNewRow)
                    End If
                    temp = "Add"
                End If
            Next

            'With cboPermissionAccounts
            '    .DataSource = dtUnit
            '    .DisplayMember = "strUnitDesc"
            '    .ValueMember = "numUnitCode"
            '    .SelectedIndex = 0
            'End With
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            chb9.Visible = False
            chb10.Visible = False

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
                            'temp = temp
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
                            Case 9
                                chb9.Text = drDSRow("strAccountDesc")
                                lbl9.Text = drDSRow("numAccountCode")
                                chb9.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl9.Text & ")") Then
                                    chb9.Checked = True
                                Else
                                    chb9.Checked = False
                                End If
                            Case 10
                                chb10.Text = drDSRow("strAccountDesc")
                                lbl10.Text = drDSRow("numAccountCode")
                                chb10.Visible = True
                                If lblPermissions.Text.Contains("(" & lbl10.Text & ")") Then
                                    chb10.Checked = True
                                Else
                                    chb10.Checked = False
                                End If

                            Case Else
                        End Select
                        j += 1
                    End If
                    temp = "Add"
                End If
            Next
            If AccountFormAccess(8, 2) = "0" And AccountFormAccess(8, 3) = "0" And AccountFormAccess(8, 4) = "0" Then
                chb1.Enabled = False
                chb2.Enabled = False
                chb3.Enabled = False
                chb4.Enabled = False
                chb5.Enabled = False
                chb6.Enabled = False
                chb7.Enabled = False
                chb8.Enabled = False
                chb9.Enabled = False
                chb10.Enabled = False
            Else
                chb1.Enabled = True
                chb2.Enabled = True
                chb3.Enabled = True
                chb4.Enabled = True
                chb5.Enabled = True
                chb6.Enabled = True
                chb7.Enabled = True
                chb8.Enabled = True
                chb9.Enabled = True
                chb10.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub UpdatePermissions(ByVal UserId As String)
        Try
            SQL = "Select " & _
            "numUserId " & _
            "from AIRBRANCH.IAIPPermissions " & _
            "where numUserId = '" & lblUserID.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If UserUnit = "14" And AccountFormAccess(129, 3) = "1" Then
            Else
                lblPermissions.Text.Replace("(118)", "")
            End If

            If recExist = False Then
                SQL = "Insert into AIRBRANCH.IAIPPermissions " & _
                "values " & _
                "('" & lblUserID.Text & "', '" & lblPermissions.Text & "') "
            Else
                SQL = "Update AIRBRANCH.IAIPPermissions set " & _
                "strIAIPPermissions = '" & lblPermissions.Text & "' " & _
                "where numUserID = '" & lblUserID.Text & "' "
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            CheckPermissions()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub CreateNewUser()
        Try
            Dim EmployeeStatus As String = "0"
            Dim EmployeeId As String = "000"

            If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
                If txtUserName.Text <> "" And txtPassword.Text <> "" Then
                    If rdbActiveStatus.Checked = True Then
                        EmployeeStatus = "1"
                    Else
                        EmployeeStatus = "0"
                    End If

                    SQL = "Select " & _
                    "strUserName " & _
                    "from AIRBRANCH.EPDUsers " & _
                    "where strUsername = '" & txtUserName.Text & "' "

                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        MsgBox("The User Name is already in use by another user." & vbCrLf & "Please choose another User Name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                    Else
                        SQL = "Insert into AIRBRANCH.EPDUsers " & _
                        "(numUserID, strUserName, " & _
                        "strPassword) " & _
                        "values " & _
                        "((select (max(numUserID) + 1) from AIRBRANCH.EPDUsers), " & _
                        "'" & Replace(txtUserName.Text, "'", "''") & "', " & _
                        "'" & Replace(EncryptDecrypt.EncryptText(txtPassword.Text), "'", "''") & "') "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()


                        'SQL = "select AIRBRANCH.SEQ_EPD_Users.currval " & _
                        '"from dual "
                        SQL = "select max(numUserID) as maxUser " & _
                        "from AIRBRANCH.EPDUsers "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            lblUserID.Text = dr.Item("maxUser")
                        End While
                        dr.Close()

                        EmployeeId = "000"

                        SQL = "Insert into AIRBRANCH.EPDUSerProfiles " & _
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

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        UpdatePermissions(lblUserID.Text)

                        LoadUserData()

                        MsgBox("Successfully Done. Please restart the IAIP and create an identical user in the testing environment.", MsgBoxStyle.Information, "IAIP User Admin Tool")

                    End If
            Else
                MsgBox("Please enter a User Name and Password.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
            End If
            Else
                MsgBox("Please enter a First and Last name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub UpdateUser()
        Try
            Dim EmployeeStatus As String = "0"
            Dim EmployeeID As String = ""
            Dim ResultDialog As DialogResult

            If lblEmailAddress.Text <> "Email: " & txtEmailAddress.Text Then
                ResultDialog = MessageBox.Show("You are about to update a user and the email address is different." & vbCrLf & _
                        "Are you sure you want to change the email address and not create a new user?", _
                   Me.Text, MessageBoxButtons.YesNoCancel, _
                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case ResultDialog
                    Case DialogResult.Yes

                    Case Else
                        Exit Sub
                End Select
            End If

            If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
                If txtUserName.Text <> "" And txtPassword.Text <> "" Then
                    EmployeeID = "000"

                    If rdbActiveStatus.Checked = True Then
                        EmployeeStatus = "1"
                    Else
                        EmployeeStatus = "0"
                    End If
                    SQL = "Select " & _
                    "strUserName " & _
                    "from AIRBRANCH.EPDUsers " & _
                    "where strUserName = '" & txtUserName.Text & "' " & _
                    "and numUserID <> '" & lblUserID.Text & "' "
                    cmd = New OracleCommand(SQL, CurrentConnection)
                    If CurrentConnection.State = ConnectionState.Closed Then
                        CurrentConnection.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        MsgBox("The User Name is already in use by another user." & vbCrLf & "Please choose another User Name.", MsgBoxStyle.Exclamation, "IAIP User Admin Tool")
                    Else
                        SQL = "Update AIRBRANCH.EPDUsers set " & _
                        "strUserName = '" & Replace(txtUserName.Text, "'", "''") & "', " & _
                        "strPassword = '" & Replace(EncryptDecrypt.EncryptText(txtPassword.Text), "'", "''") & "' " & _
                        "where numUserId = '" & lblUserID.Text & "' "

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        SQL = "Update AIRBRANCH.EPDUserProfiles set " & _
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

                        cmd = New OracleCommand(SQL, CurrentConnection)
                        If CurrentConnection.State = ConnectionState.Closed Then
                            CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadDataGrid(ByVal SearchType As String)
        Try
            Select Case SearchType
                Case "Self"
                    SQL = "select " & _
                    "AIRBRANCH.EPDUsers.numUserID, " & _
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
                    "from AIRBRANCH.EPDUsers, AIRBRANCH.EPDUserProfiles,  " & _
                    "AIRBRANCH.LookupEPDBranches, AIRBRANCH.LookupEPDPrograms,  " & _
                    "AIRBRANCH.LookUpEPDUnits  " & _
                    "where AIRBRANCH.epdusers.numuserID = AIRBRANCH.EPDUserProfiles.numUserId  " & _
                    "and AIRBRANCH.EPDUserProfiles.numBranch = AIRBRANCH.LookupEPDBranches.numBranchcode (+)  " & _
                    "and AIRBRANCH.EPDUserProfiles.numProgram = AIRBRANCH.lookupEPDPrograms.numProgramcode (+)  " & _
                    "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitcode (+) " & _
                    "and AIRBRANCH.EPDUsers.numUserId = '" & UserGCode & "' "
                Case "Search"
                    SQL = "select " & _
                    "AIRBRANCH.EPDUsers.numUserID, " & _
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
                    "from AIRBRANCH.EPDUsers, AIRBRANCH.EPDUserProfiles,  " & _
                    "AIRBRANCH.LookupEPDBranches, AIRBRANCH.LookupEPDPrograms,  " & _
                    "AIRBRANCH.LookUpEPDUnits  " & _
                    "where AIRBRANCH.epdusers.numuserID = AIRBRANCH.EPDUserProfiles.numUserId  " & _
                    "and AIRBRANCH.EPDUserProfiles.numBranch = AIRBRANCH.LookupEPDBranches.numBranchcode (+)  " & _
                    "and AIRBRANCH.EPDUserProfiles.numProgram = AIRBRANCH.lookupEPDPrograms.numProgramcode (+)  " & _
                    "and AIRBRANCH.EPDUserProfiles.numUnit = AIRBRANCH.LookUpEPDUnits.numUnitcode (+) "

                    If cboSearchBranch.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numbranch = '" & cboSearchBranch.SelectedValue & "' "
                    End If
                    If cboSearchProgram.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numProgram = '" & cboSearchProgram.SelectedValue & "' "
                    End If
                    If cboSearchUnit.SelectedIndex > 0 Then
                        SQL = SQL & " and EPDUserProfiles.numUnit = '" & cboSearchUnit.SelectedValue & "' "
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
            daDataGrid = New OracleDataAdapter(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub LoadUserData()
        Try
            SQL = "select " & _
            "AIRBRANCH.epdusers.numuserID,  " & _
            "strUserName, strPassword,  " & _
            "strLastname,  " & _
            "strFirstName, strEmailAddress,  " & _
            "strPhone, strFax,  " & _
            "numBranch,  " & _
            "numProgram,  " & _
            "numUnit,  " & _
            "strOffice,  " & _
            "numEmployeeStatus   " & _
            "from AIRBRANCH.EPDUsers, AIRBRANCH.EPDUserProfiles  " & _
            "where AIRBRANCH.epdusers.numuserID = AIRBRANCH.EPDUserProfiles.numUserId " & _
            "and AIRBRANCH.epdusers.numuserid = '" & lblUserID.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strFirstname")) Then
                    txtFirstName.Clear()
                Else
                    txtFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtLastName.Clear()
                Else
                    txtLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strEmailAddress")) Then
                    lblEmailAddress.Text = "Email: "
                    txtEmailAddress.Clear()
                Else
                    lblEmailAddress.Text = "Email: " & dr.Item("strEmailAddress")
                    txtEmailAddress.Text = dr.Item("strEmailAddress")
                End If
                If IsDBNull(dr.Item("strPhone")) Then
                    mtbPhoneNumber.Clear()
                Else
                    mtbPhoneNumber.Text = dr.Item("strPhone")
                End If
                If IsDBNull(dr.Item("strFax")) Then
                    mtbFaxNumber.Clear()
                Else
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
                    txtPassword.Clear()
                Else
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
                    cboUnit.Text = ""
                    'cboPermissionAccounts.Text = ""
                Else
                    cboUnit.SelectedValue = dr.Item("numUnit")
                    'cboPermissionAccounts.SelectedValue = dr.Item("numUnit")
                End If
            End While

            lblPermissions.Text = ""
            txtCurrentPermissions.Clear()

            SQL = "Select " & _
            "numUserId, strIAIPPermissions " & _
            "from AIRBRANCH.IAIPPermissions " & _
            "where numUserID = '" & lblUserID.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub ClearData()
        Try
            lblUserID.Text = ""
            txtFirstName.Clear()
            txtLastName.Clear()
            lblEmailAddress.Text = "Email: "
            txtEmailAddress.Clear()
            mtbPhoneNumber.Clear()
            mtbFaxNumber.Clear()
            lblUserName.Text = "User Name: "
            txtUserName.Clear()
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub cboBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBranch.SelectedValueChanged
        Try
            If cboBranch.SelectedIndex > 0 Then
                LoadProgram(cboBranch.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboSearchBranch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearchBranch.SelectedValueChanged
        Try
            If cboSearchBranch.SelectedIndex > 0 Then
                LoadSearchProgram(cboSearchBranch.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProgram.SelectedValueChanged
        Try
            If cboProgram.SelectedIndex > 0 Then
                LoadUnit(cboProgram.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub cboSearchProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearchProgram.SelectedValueChanged
        Try
            If cboSearchProgram.SelectedIndex > 0 Then
                LoadSearchUnit(cboSearchProgram.SelectedValue)
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnCreateNewUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNewUser.Click
        Try
            CreateNewUser()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If lblUserID.Text <> "" And lblUserID.Text <> "numUserID" Then
                If AccountFormAccess(8, 2) = "0" And AccountFormAccess(8, 3) = "0" And AccountFormAccess(8, 4) = "0" Then
                    If lblUserID.Text = UserGCode Then
                        UpdateUser()
                    Else
                        MsgBox("You only have authorization to edit your own data.", MsgBoxStyle.Information, "IAIP User Admin Tool")
                    End If
                Else
                    If AccountFormAccess(8, 4) = "0" Then
                        If AccountFormAccess(8, 3) = "0" Then
                            If UserBranch.ToString = cboBranch.SelectedValue.ToString And UserProgram.ToString = cboProgram.SelectedValue.ToString Then
                                UpdateUser()
                            Else
                                MsgBox("You only have authorization to edit users in your Branch and Program.", _
                                         MsgBoxStyle.Information, "IAIP User Admin Tool")
                            End If
                        Else
                            If UserBranch.ToString = cboBranch.SelectedValue.ToString Then
                                UpdateUser()
                            Else
                                MsgBox("You only have authorization to edit users in your Branch.", _
                                         MsgBoxStyle.Information, "IAIP User Admin Tool")
                            End If
                        End If
                    Else
                        UpdateUser()
                    End If
                End If
            Else
                MsgBox("Select a user for the search tool below before making changes and saving data.", MsgBoxStyle.Information, "IAIP User Admin Tool")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            LoadDataGrid("Search")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            txtSearchFirstName.Clear()
            txtSearchLastName.Clear()
            cboSearchBranch.SelectedIndex = 0
            LoadSearchProgram(cboSearchBranch.SelectedValue)
            LoadSearchUnit(cboSearchProgram.SelectedValue)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb9_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb9.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl9.Text) And chb9.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl9.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl9.Text & ")", "")
                If chb9.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl9.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub chb10_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb10.CheckStateChanged
        Try
            If lblPermissions.Text.Contains(lbl10.Text) And chb10.Checked = False Then
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl10.Text & ")", "")
            Else
                lblPermissions.Text = Replace(lblPermissions.Text, "(" & lbl10.Text & ")", "")
                If chb10.Checked = True Then
                    lblPermissions.Text = lblPermissions.Text & "(" & lbl10.Text & ")"
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region " Accept button "

    Private Sub pnlSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlSearch.Enter
        Me.AcceptButton = btnSearch
    End Sub

    Private Sub pnlSearch_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlSearch.Leave
        Me.AcceptButton = Nothing
    End Sub

#End Region

End Class