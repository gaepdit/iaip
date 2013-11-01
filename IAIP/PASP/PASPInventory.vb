Imports System.Data.OracleClient
'Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class PASPInventory
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim AssetWhere As String
    Dim TransactionWhere As String
    Dim ReportWhere As String
    Dim Program As String
    Dim Unit As String
    Dim rpt As ReportClass
    Dim dsGAITInventory As DataSet
    Dim dsGAITLookUps As DataSet
    Dim dsGAITCategory As DataSet
    Dim dsGAITManufacturer As DataSet
    Dim dsGAITModel As DataSet
    Dim dsGAITModelNumber As DataSet
    Dim dsGAITQuality As DataSet
    Dim daGAITInventory As OracleDataAdapter
    Dim daGAITLookUps As OracleDataAdapter
    Dim daGAITCategory As OracleDataAdapter
    Dim daGAITManufacturer As OracleDataAdapter
    Dim daGAITModel As OracleDataAdapter
    Dim daGAITModelNumber As OracleDataAdapter
    Dim daGAITQuality As OracleDataAdapter

    Private Sub PASPComputerInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try
            'LoadInventoryTypes()
            'LoadTransactionTypes()
            'LoadStaffData()
            'LoadProgramUnit()

            'txtInventoryID.Clear()
            'txtNewInventoryType.Clear()
            'txtTransactionIDValue.Clear()
            'txtNewTransactionType.Clear()
            'DTPDateAssetAcquired.Text = OracleDate
            'DTPDatAcquiredStart.Text = OracleDate
            'DTPDatAcquiredEnd.Text = OracleDate

            'DTPDatAcquiredStart.Checked = False
            'DTPDatAcquiredEnd.Enabled = False

            gbGAITCategory.Enabled = False
            gbGAITManufacturer.Enabled = False
            gbGAITModel.Enabled = False
            gbGAITModelNumber.Enabled = False
            gbGAITQuality.Enabled = False
            dtpGAITDateStart.Text = OracleDate
            dtpGAITDateEnd.Text = OracleDate
            pnlGAITDateSearch.Enabled = False
            rdbGAITDatePurchased.Checked = False
            rdbGAITDatePurchased.Enabled = False
            rdbGAITDateAquired.Checked = False
            rdbGAITDateAquired.Enabled = False
            dtpGAITDateStart.Enabled = False
            dtpGAITDateEnd.Enabled = False

            TCComputerInventory.TabPages.Remove(TPInvenotry)
            TCComputerInventory.TabPages.Remove(TPTransactions)
            TCComputerInventory.TabPages.Remove(TPReports)
            TCComputerInventory.TabPages.Remove(TPListTools)

            LoadGAITLookUps()
            ' LoadGAITInventory()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#Region "Page Load Functions"
    Sub LoadInventoryTypes()
        Try
            Dim dtInventoryTypes As New DataTable
            Dim dtExistingInventoryTypes As New DataTable
            Dim dtInventorySearch As New DataTable
            Dim dtReportInventoryType As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drDSRow4 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numInventoryID, strInventoryType " & _
            "from " & DBNameSpace & ".LookUpPASPInventoryType " & _
            "order by strInventoryType "
            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "InventoryType")

            dtInventoryTypes.Columns.Add("numInventoryID", GetType(System.String))
            dtInventoryTypes.Columns.Add("strInventoryType", GetType(System.String))

            drNewRow = dtInventoryTypes.NewRow()
            drNewRow("numInventoryID") = " "
            drNewRow("strInventoryType") = " "
            dtInventoryTypes.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("InventoryType").Rows()
                drNewRow = dtInventoryTypes.NewRow()
                drNewRow("numInventoryID") = drDSRow("numInventoryID")
                drNewRow("strInventoryType") = drDSRow("strInventoryType")
                dtInventoryTypes.Rows.Add(drNewRow)
            Next

            With cboInventoryType
                .DataSource = dtInventoryTypes
                .DisplayMember = "strInventoryType"
                .ValueMember = "numInventoryID"
                .SelectedIndex = 0
            End With

            dtExistingInventoryTypes.Columns.Add("numInventoryID", GetType(System.String))
            dtExistingInventoryTypes.Columns.Add("strInventoryType", GetType(System.String))

            drNewRow = dtExistingInventoryTypes.NewRow()
            drNewRow("numInventoryID") = " "
            drNewRow("strInventoryType") = " "
            dtExistingInventoryTypes.Rows.Add(drNewRow)

            For Each drDSRow2 In ds.Tables("InventoryType").Rows()
                drNewRow = dtExistingInventoryTypes.NewRow()
                drNewRow("numInventoryID") = drDSRow2("numInventoryID")
                drNewRow("strInventoryType") = drDSRow2("strInventoryType")
                dtExistingInventoryTypes.Rows.Add(drNewRow)
            Next

            With cboExistingInventoryTypes
                .DataSource = dtExistingInventoryTypes
                .DisplayMember = "strInventoryType"
                .ValueMember = "numInventoryID"
                .SelectedIndex = 0
            End With

            dtInventorySearch.Columns.Add("numInventoryID", GetType(System.String))
            dtInventorySearch.Columns.Add("strInventoryType", GetType(System.String))

            drNewRow = dtInventorySearch.NewRow()
            drNewRow("numInventoryID") = " "
            drNewRow("strInventoryType") = " "
            dtInventorySearch.Rows.Add(drNewRow)

            For Each drDSRow3 In ds.Tables("InventoryType").Rows()
                drNewRow = dtInventorySearch.NewRow()
                drNewRow("numInventoryID") = drDSRow3("numInventoryID")
                drNewRow("strInventoryType") = drDSRow3("strInventoryType")
                dtInventorySearch.Rows.Add(drNewRow)
            Next

            With cboInventorySearch
                .DataSource = dtInventorySearch
                .DisplayMember = "strInventoryType"
                .ValueMember = "numInventoryID"
                .SelectedIndex = 0
            End With

            dtReportInventoryType.Columns.Add("numInventoryID", GetType(System.String))
            dtReportInventoryType.Columns.Add("strInventoryType", GetType(System.String))

            drNewRow = dtReportInventoryType.NewRow()
            drNewRow("numInventoryID") = ""
            drNewRow("strInventoryType") = "-All Types-"
            dtReportInventoryType.Rows.Add(drNewRow)

            For Each drDSRow4 In ds.Tables("InventoryType").Rows()
                drNewRow = dtReportInventoryType.NewRow()
                drNewRow("numInventoryID") = drDSRow4("numInventoryID")
                drNewRow("strInventoryType") = drDSRow4("strInventoryType")
                dtReportInventoryType.Rows.Add(drNewRow)
            Next

            With cboReportInventoryType
                .DataSource = dtReportInventoryType
                .DisplayMember = "strInventoryType"
                .ValueMember = "numInventoryID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadTransactionTypes()
        Try
            Dim dtTransactionTypes As New DataTable
            Dim dtExistingTransactionTypes As New DataTable
            Dim dtTransactionTypeSearch As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numTransactionType, strTransactionType " & _
            "from " & DBNameSpace & ".LookUpPASPTransactionType " & _
            "order by strTransactionType "
            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "TransactionType")

            dtTransactionTypes.Columns.Add("numTransactionType", GetType(System.String))
            dtTransactionTypes.Columns.Add("strTransactionType", GetType(System.String))

            drNewRow = dtTransactionTypes.NewRow()
            drNewRow("numTransactionType") = " "
            drNewRow("strTransactionType") = " "
            dtTransactionTypes.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("TransactionType").Rows()
                drNewRow = dtTransactionTypes.NewRow()
                drNewRow("numTransactionType") = drDSRow("numTransactionType")
                drNewRow("strTransactionType") = drDSRow("strTransactionType")
                dtTransactionTypes.Rows.Add(drNewRow)
            Next

            With cboTransactionType
                .DataSource = dtTransactionTypes
                .DisplayMember = "strTransactionType"
                .ValueMember = "numTransactionType"
                .SelectedIndex = 0
            End With

            dtExistingTransactionTypes.Columns.Add("numTransactionType", GetType(System.String))
            dtExistingTransactionTypes.Columns.Add("strTransactionType", GetType(System.String))

            drNewRow = dtExistingTransactionTypes.NewRow()
            drNewRow("numTransactionType") = " "
            drNewRow("strTransactionType") = " "
            dtExistingTransactionTypes.Rows.Add(drNewRow)

            For Each drDSRow2 In ds.Tables("TransactionType").Rows()
                drNewRow = dtExistingTransactionTypes.NewRow()
                drNewRow("numTransactionType") = drDSRow2("numTransactionType")
                drNewRow("strTransactionType") = drDSRow2("strTransactionType")
                dtExistingTransactionTypes.Rows.Add(drNewRow)
            Next

            With cboExistingTransactionTypes
                .DataSource = dtExistingTransactionTypes
                .DisplayMember = "strTransactionType"
                .ValueMember = "numTransactionType"
                .SelectedIndex = 0
            End With

            dtTransactionTypeSearch.Columns.Add("numTransactionType", GetType(System.String))
            dtTransactionTypeSearch.Columns.Add("strTransactionType", GetType(System.String))

            drNewRow = dtTransactionTypeSearch.NewRow()
            drNewRow("numTransactionType") = " "
            drNewRow("strTransactionType") = " "
            dtTransactionTypeSearch.Rows.Add(drNewRow)

            For Each drDSRow3 In ds.Tables("TransactionType").Rows()
                drNewRow = dtTransactionTypeSearch.NewRow()
                drNewRow("numTransactionType") = drDSRow3("numTransactionType")
                drNewRow("strTransactionType") = drDSRow3("strTransactionType")
                dtTransactionTypeSearch.Rows.Add(drNewRow)
            Next

            With cboTransactionTypeSearch
                .DataSource = dtTransactionTypeSearch
                .DisplayMember = "strTransactionType"
                .ValueMember = "numTransactionType"
                .SelectedIndex = 0
            End With



        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadStaffData()
        Try
            Dim dtStaff As New DataTable
            Dim dtStaffSearch As New DataTable
            Dim dtTransactionStaffSearch As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drNewRow As DataRow

            SQL = "select " & _
            "(strLastName|| ', '||strFirstName) as Staff, numUserID  " & _
            "from AIRBranch.EPDUserProfiles " & _
            "where numBranch = '1'  " & _
            "and numEmployeeStatus = '1'  " & _
            "order by strLastName "

            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "Staff")

            dtStaff.Columns.Add("numUserID", GetType(System.String))
            dtStaff.Columns.Add("Staff", GetType(System.String))

            drNewRow = dtStaff.NewRow()
            drNewRow("numUserID") = ""
            drNewRow("Staff") = " "
            dtStaff.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("Staff").Rows()
                drNewRow = dtStaff.NewRow()
                drNewRow("numUserID") = drDSRow("numUserID")
                drNewRow("Staff") = drDSRow("Staff")
                dtStaff.Rows.Add(drNewRow)
            Next

            With cboStaff
                .DataSource = dtStaff
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtStaffSearch.Columns.Add("numUserID", GetType(System.String))
            dtStaffSearch.Columns.Add("Staff", GetType(System.String))

            drNewRow = dtStaffSearch.NewRow()
            drNewRow("numUserID") = ""
            drNewRow("Staff") = " "
            dtStaffSearch.Rows.Add(drNewRow)

            For Each drDSRow2 In ds.Tables("Staff").Rows()
                drNewRow = dtStaffSearch.NewRow()
                drNewRow("numUserID") = drDSRow2("numUserID")
                drNewRow("Staff") = drDSRow2("Staff")
                dtStaffSearch.Rows.Add(drNewRow)
            Next

            With cboSearchByStaff
                .DataSource = dtStaffSearch
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

            dtTransactionStaffSearch.Columns.Add("numUserID", GetType(System.String))
            dtTransactionStaffSearch.Columns.Add("Staff", GetType(System.String))

            drNewRow = dtTransactionStaffSearch.NewRow()
            drNewRow("numUserID") = ""
            drNewRow("Staff") = " "
            dtTransactionStaffSearch.Rows.Add(drNewRow)

            For Each drDSRow3 In ds.Tables("Staff").Rows()
                drNewRow = dtTransactionStaffSearch.NewRow()
                drNewRow("numUserID") = drDSRow3("numUserID")
                drNewRow("Staff") = drDSRow3("Staff")
                dtTransactionStaffSearch.Rows.Add(drNewRow)
            Next

            With cboTransactionStaffSearch
                .DataSource = dtTransactionStaffSearch
                .DisplayMember = "Staff"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadProgramUnit()
        Try
            Dim dtProgram As New DataTable
            Dim dtUnit As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numProgramCode, strProgramDesc " & _
            "from " & DBNameSpace & ".LookUpEPDPrograms " & _
            "where " & DBNameSpace & ".LookUpEPDPrograms.numBranchCode = '1' " & _
            "order by strProgramDesc "

            SQL2 = "Select " & _
            "numUnitCode, strUnitDesc " & _
            "from " & DBNameSpace & ".LookUpEPDUnits, " & DBNameSpace & ".LookUpEPDPrograms " & _
            "where " & DBNameSpace & ".LookUpEPDUnits.numProgramCode = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode " & _
            "and " & DBNameSpace & ".LookUpEPDPrograms.numBranchCode = '1' " & _
            "order by strUnitDesc "

            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "Program")

            da = New OracleDataAdapter(SQL2, Conn)
            da.Fill(ds, "Unit")

            dtProgram.Columns.Add("numProgramCode", GetType(System.String))
            dtProgram.Columns.Add("strProgramDesc", GetType(System.String))

            drNewRow = dtProgram.NewRow()
            drNewRow("numProgramCode") = "%"
            drNewRow("strProgramDesc") = "-All Programs-"
            dtProgram.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("Program").Rows()
                drNewRow = dtProgram.NewRow()
                drNewRow("numProgramCode") = drDSRow("numProgramCode")
                drNewRow("strProgramDesc") = drDSRow("strProgramDesc")
                dtProgram.Rows.Add(drNewRow)
            Next

            With cboReportProgram
                .DataSource = dtProgram
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .SelectedIndex = 0
            End With

            dtUnit.Columns.Add("numUnitCode", GetType(System.String))
            dtUnit.Columns.Add("strUnitDesc", GetType(System.String))

            drNewRow = dtUnit.NewRow()
            drNewRow("numUnitCode") = "%"
            drNewRow("strUnitDesc") = "-All Units-"
            dtUnit.Rows.Add(drNewRow)

            For Each drDSRow2 In ds.Tables("Unit").Rows()
                drNewRow = dtUnit.NewRow()
                drNewRow("numUnitCode") = drDSRow2("numUnitCode")
                drNewRow("strUnitDesc") = drDSRow2("strUnitDesc")
                dtUnit.Rows.Add(drNewRow)
            Next

            With cboReportUnit
                .DataSource = dtUnit
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ReturnProgramUnit(ByVal StaffCode As String)
        Try
            Program = ""
            Unit = ""

            SQL = "select " & _
            "strProgramDesc, strUnitDesc  " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDPrograms,  " & _
            "" & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+)  " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+)  " & _
            "and (strLastName||', '||strFirstName) = '" & StaffCode & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strProgramDesc")) Then
                    Program = ""
                Else
                    Program = dr.Item("strProgramDesc")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = ""
                Else
                    Unit = dr.Item("strUnitDesc")
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "List Tool"
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            txtNewInventoryType.Clear()
            txtInventoryID.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewType.Click
        Try
            SQL = "Select " & _
            "numInventoryID " & _
            "from " & DBNameSpace & ".LookUpPASPInventoryType " & _
            "where Upper(strInventoryType) = '" & txtNewInventoryType.Text.ToUpper & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                MsgBox("The Inventory Description currently exists.", MsgBoxStyle.Information, "PASP Inventory")
            Else
                SQL = "Insert into " & DBNameSpace & ".LookUpPASPInventoryType " & _
                "values " & _
                "((Select max(numInventoryID) +1 from " & DBNameSpace & ".LookUpPASPInventoryType), " & _
                "'" & Replace(txtNewInventoryType.Text, "'", "''") & "') "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadInventoryTypes()
                cboExistingInventoryTypes.Text = txtNewInventoryType.Text
                txtNewInventoryType.Clear()
                txtInventoryID.Clear()
                MsgBox("Inventory Type added.", MsgBoxStyle.Information, "PASP Inventory")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditType.Click
        Try
            If txtInventoryID.Text <> "" And txtInventoryID.Text <> " " Then
                SQL = "Select " & _
                "numInventoryID " & _
                "from " & DBNameSpace & ".LookUpPASPInventoryType " & _
                "where numInventoryID = '" & txtInventoryID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpPASPInventoryType set " & _
                    "strInventoryType = '" & Replace(txtNewInventoryType.Text, "'", "''") & "' " & _
                    "where numInventoryID = '" & txtInventoryID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    LoadInventoryTypes()
                    MsgBox("List Updated", MsgBoxStyle.Information, "PASP Inventory")
                Else
                    MsgBox("Please select an existing inventory type from the dropdown below first.", MsgBoxStyle.Information, "PASP Inventory")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteInventory.Click
        Try
            If txtInventoryID.Text <> "" Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Are you sure you want to remove this inventory item?", _
                  "PASP Inventory", MessageBoxButtons.YesNoCancel, _
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case Windows.Forms.DialogResult.Yes
                        SQL = "select count(*) as ResultCount " & _
                        "from " & DBNameSpace & ".PASPInventory " & _
                        "where numInventoryID = '" & txtInventoryID.Text & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("ResultCount")) Then
                                temp = "0"
                            Else
                                temp = dr.Item("ResultCount")
                            End If
                        End While
                        dr.Close()

                        If temp > 0 Then
                            MsgBox("This inventory item can not be deleted because assets currently use this item." & vbCrLf & _
                                   "Please reclassify any assets that use this item before deleting this item.", MsgBoxStyle.Information, "PASP Inventory")
                        Else
                            SQL = "Delete " & DBNameSpace & ".LookUpPASPInventoryType  " & _
                            "where numInventoryID = '" & txtInventoryID.Text & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                            cboExistingInventoryTypes.Text = ""
                            LoadInventoryTypes()
                            MsgBox("Inventory Item Deleted", MsgBoxStyle.Information, "PASP Inventory")
                        End If

                    Case Windows.Forms.DialogResult.No

                    Case Windows.Forms.DialogResult.Cancel

                    Case Else

                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingInventoryTypes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingInventoryTypes.TextChanged
        Try

            txtInventoryID.Text = cboExistingInventoryTypes.SelectedValue.ToString
            If txtInventoryID.Text <> "" And txtInventoryID.Text <> " " Then
                txtNewInventoryType.Text = cboExistingInventoryTypes.Text
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearTransactionType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTransactionType.Click
        Try
            txtNewTransactionType.Clear()
            txtTransactionIDValue.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddTransactionType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTransactionType.Click
        Try
            SQL = "Select " & _
            "numTransactionType " & _
            "from " & DBNameSpace & ".LookUpPASPTransactionType " & _
            "where Upper(strTransactionType) = '" & txtNewTransactionType.Text.ToUpper & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                MsgBox("The Transaction Description currently exists.", MsgBoxStyle.Information, "PASP Inventory")
            Else
                SQL = "Insert into " & DBNameSpace & ".LookUpPASPTransactionType " & _
                "values " & _
                "((Select max(numTransactionType) +1 from " & DBNameSpace & ".LookUpPASPTransactionType), " & _
                "'" & Replace(txtNewTransactionType.Text, "'", "''") & "') "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadTransactionTypes()
                cboExistingTransactionTypes.Text = txtNewTransactionType.Text
                txtNewTransactionType.Clear()
                txtTransactionIDValue.Clear()
                MsgBox("Transaction Type added.", MsgBoxStyle.Information, "PASP Inventory")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditTransactionType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTransactionType.Click
        Try
            If txtTransactionIDValue.Text <> "" And txtTransactionIDValue.Text <> " " Then
                SQL = "Select " & _
                "numTransactionType " & _
                "from " & DBNameSpace & ".LookUpPASPTransactionType " & _
                "where numTransactionID = '" & txtTransactionIDValue.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".LookUpPASPTransactionType set " & _
                    "strTransactionType = '" & Replace(txtNewTransactionType.Text, "'", "''") & "' " & _
                    "where numTransactionType = '" & txtTransactionIDValue.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    LoadTransactionTypes()
                    MsgBox("List Updated", MsgBoxStyle.Information, "PASP Inventory")
                Else
                    MsgBox("Please select an existing Transaction type from the dropdown below first.", MsgBoxStyle.Information, "PASP Inventory")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTransactionType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTransactionType.Click
        Try
            If txtTransactionIDValue.Text <> "" Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Are you sure you want to remove this Transaction item?", _
                  "PASP Inventory", MessageBoxButtons.YesNoCancel, _
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                Select Case Result
                    Case Windows.Forms.DialogResult.Yes
                        SQL = "select count(*) as ResultCount " & _
                        "from " & DBNameSpace & ".PASPInventoryTransactions " & _
                        "where numTransactionType = '" & txtTransactionIDValue.Text & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("ResultCount")) Then
                                temp = "0"
                            Else
                                temp = dr.Item("ResultCount")
                            End If
                        End While
                        dr.Close()

                        If temp > 0 Then
                            MsgBox("This transaction item can not be deleted because transactions currently use this item." & vbCrLf & _
                                   "Please reclassify any transactions that use this item before deleting this item.", _
                                   MsgBoxStyle.Information, "PASP Inventory")
                        Else
                            SQL = "Delete " & DBNameSpace & ".LookUPPASPTransactionType " & _
                            "where numTransactionType = '" & txtTransactionIDValue.Text & "' "
                            cmd = New OracleCommand(SQL, Conn)
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()

                            cboExistingTransactionTypes.Text = ""
                            LoadTransactionTypes()
                            MsgBox("Transactions List Deleted", MsgBoxStyle.Information, "PASP Inventory")
                        End If

                    Case Windows.Forms.DialogResult.No

                    Case Windows.Forms.DialogResult.Cancel

                    Case Else

                End Select
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingTranasctionTypes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingTransactionTypes.TextChanged
        Try

            txtTransactionIDValue.Text = cboExistingTransactionTypes.SelectedValue.ToString
            If txtTransactionIDValue.Text <> "" And txtTransactionIDValue.Text <> " " Then
                txtNewTransactionType.Text = cboExistingTransactionTypes.Text
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Asset List"
    Sub AddNewAsset()
        Try
            Dim Status As String
            If rdbInactiveInventory.Checked = True Then
                Status = "False"
            Else
                Status = "True"
            End If

            SQL = "Insert into " & DBNameSpace & ".PASPInventory " & _
            "values " & _
            "((Select max(strAssetid) + 1 " & _
            "from " & DBNameSpace & ".PASPInventory " & _
            "where length(trim(Translate(strAssetid, '+-.0123456789',' '))) is null ),  " & _
            "'" & Replace(txtDNRDecal.Text, "'", "''") & "', " & _
            "'" & cboInventoryType.SelectedValue & "', '" & Replace(txtDescription.Text, "'", "''") & "', " & _
            "'" & DTPDateAssetAcquired.Text & "', '" & Replace(Replace(txtCost.Text, "$", ""), ",", "") & "', " & _
            "'" & Replace(txtSericalID.Text, "'", "''") & "', " & _
            "'" & Replace(txtPOID.Text, "'", "''") & "', '" & mtbReplacementDate.Text & "', " & _
            "'" & Status & "', '') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(strAssetid) as AssetID " & _
            "from " & DBNameSpace & ".PASPInventory " & _
            "where length(trim(Translate(strAssetid, '+-.0123456789',' '))) is null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("AssetID")) Then
                    txtAssetID.Text = "ERROR"
                Else
                    txtAssetID.Text = dr.Item("AssetID")
                End If
            End While
            dr.Close()

            SQL = "Insert into " & DBNameSpace & ".PASPInventoryTransactions " & _
            "values " & _
            "((Select max(numTransactionId) + 1 " & _
            "from " & DBNameSpace & ".PASPInventoryTransactions " & _
            "where length(trim(Translate(strAssetid, '+-.0123456789',' '))) is null ),  " & _
            "'" & txtAssetID.Text & "', '2', " & _
            "'', '" & OracleDate & "', " & _
            "'', " & _
            "'Initial Transaction created when Asset was created by " & UserName & " on " & OracleDate & "', '') "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If AssetWhere = "" Then
                txtDecalSearch.Text = txtDNRDecal.Text
                AssetWhere = AssetWhere & " and " & DBNameSpace & ".PASPInventory.strDecalNumber = '" & txtDecalSearch.Text & "' "
            End If
            btnSearchForAsset.Enabled = False
            btnExportInventory.Enabled = False
            btnAddNewAsset.Enabled = False
            btnUpdateAsset.Enabled = False
            btnDeleteAsset.Enabled = False

            bgwInventory.WorkerReportsProgress = True
            bgwInventory.WorkerSupportsCancellation = True
            bgwInventory.RunWorkerAsync()

            MsgBox("Asset Added", MsgBoxStyle.Information, "PASP Inventory")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub UpdateAsset()
        Try
            Dim Status As String
            If rdbInactiveInventory.Checked = True Then
                Status = "False"
            Else
                Status = "True"
            End If

            If txtAssetID.Text <> "" Then
                SQL = "Select strAssetID " & _
                "From " & DBNameSpace & ".PASPInventory " & _
                "where strAssetID = '" & txtAssetID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".PASPInventory set " & _
                    "strDecalNumber = '" & Replace(txtDNRDecal.Text, "'", "''") & "', " & _
                    "numInventoryID = '" & cboInventoryType.SelectedValue & "', " & _
                    "strDescription = '" & Replace(txtDescription.Text, "'", "''") & "', " & _
                    "numCost = '" & Replace(Replace(txtCost.Text, "$", ""), ",", "") & "', " & _
                    "strSerialID = '" & Replace(txtSericalID.Text, "'", "''") & "', " & _
                    "strPurchaseOrder = '" & Replace(txtPOID.Text, "'", "''") & "', " & _
                    "strReplacementYear = '" & mtbReplacementDate.Text & "', " & _
                    "strActivityStatus = '" & Status & "' " & _
                    "where strAssetID = '" & txtAssetID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    If AssetWhere = "" Then
                        txtDecalSearch.Text = txtDNRDecal.Text
                        AssetWhere = AssetWhere & " and " & DBNameSpace & ".PASPInventory.strDecalNumber = '" & txtDecalSearch.Text & "' "
                    End If
                    btnSearchForAsset.Enabled = False
                    btnExportInventory.Enabled = False
                    btnAddNewAsset.Enabled = False
                    btnUpdateAsset.Enabled = False
                    btnDeleteAsset.Enabled = False

                    bgwInventory.WorkerReportsProgress = True
                    bgwInventory.WorkerSupportsCancellation = True
                    bgwInventory.RunWorkerAsync()

                    MsgBox("Asset Updated", MsgBoxStyle.Information, "PASP Inventory")
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DeleteAsset()
        Try
            Dim Result As DialogResult
            Result = MessageBox.Show("Are you sure you want to remove this Asset?", _
              "PASP Inventory", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case Windows.Forms.DialogResult.Yes
                    SQL = "select " & _
                    "numTransactionID  " & _
                    "from AIRBranch.PASPInventoryTransactions  " & _
                    "where strAssetID = '" & txtAssetID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        Result = MessageBox.Show("There are existing Transactions for this Asset." & vbCrLf & _
                                                 "Do you want to delete these Transactions? " & vbCrLf & _
                                                 "If not then this Asset will not be deleted.", "PASP Inventory", _
                                                 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        Select Case Result
                            Case Windows.Forms.DialogResult.Yes
                                SQL = "Delete " & DBNameSpace & ".PASPInventoryTransactions " & _
                                "where strAssetID = '" & txtAssetID.Text & "' "
                                cmd = New OracleCommand(SQL, Conn)
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                dr = cmd.ExecuteReader
                                dr.Close()
                            Case Else
                                Exit Sub
                        End Select
                    End If
                    SQL = "Delete " & DBNameSpace & ".PASPInventory " & _
                          "where strAssetID = '" & txtAssetID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ClearAsset()
                    btnSearchForAsset.Enabled = False
                    btnExportInventory.Enabled = False
                    btnAddNewAsset.Enabled = False
                    btnUpdateAsset.Enabled = False
                    btnDeleteAsset.Enabled = False

                    bgwInventory.WorkerReportsProgress = True
                    bgwInventory.WorkerSupportsCancellation = True
                    bgwInventory.RunWorkerAsync()

                    MsgBox("Asset Deleted", MsgBoxStyle.Information, "PASP Inventory")
                Case Windows.Forms.DialogResult.No

                Case Windows.Forms.DialogResult.Cancel

                Case Else

            End Select

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearAsset()
        Try
            txtAssetID.Clear()
            txtDNRDecal.Clear()
            txtPOID.Clear()
            mtbReplacementDate.Clear()
            cboInventoryType.Text = ""
            DTPDateAssetAcquired.Text = OracleDate
            txtCost.Clear()
            txtSericalID.Clear()
            txtDescription.Clear()
            txtCurrentStaff.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNewAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewAsset.Click
        Try
            If txtAssetID.Text = "" Then
                AddNewAsset()
            Else
                MsgBox("Asset ID must be blank to add new asset.", MsgBoxStyle.Information, "PASP Inventory")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateAsset.Click
        Try
            If txtAssetID.Text <> "" Then
                UpdateAsset()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAssetID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAssetID.Click
        Try
            txtAssetID.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAsset.Click
        Try
            If txtAssetID.Text <> "" Then
                DeleteAsset()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtCost_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCost.Validated
        Try
            Me.txtCost.Text = Decimal.Parse(Me.txtCost.Text).ToString("c")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtCost_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Try
            Dim currency As Decimal
            'Convert the current value to currency, with or without a currency symbol.

            If Not Decimal.TryParse(Me.txtCost.Text, _
                                  Globalization.NumberStyles.Currency, _
                                                    Nothing, _
                                                   currency) Then

                'Don't let the user leave the field if the value is invalid.

                With Me.txtCost
                    .HideSelection = False
                    .SelectAll()
                    MessageBox.Show("Please enter a valid currency amount.", _
                                   "Invalid Value", _
                                    MessageBoxButtons.OK, _
                                    MessageBoxIcon.Error)
                    .HideSelection = True
                End With
                e.Cancel = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvInventory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvInventory.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvInventory.HitTest(e.X, e.Y)

            If hti.Type = DataGrid.HitTestType.Cell Then
                If dgvInventory.RowCount > 0 And hti.RowIndex <> -1 Then
                    If dgvInventory.Columns(0).HeaderText = "Asset ID" Then
                        txtAssetID.Text = dgvInventory(0, hti.RowIndex).Value

                        If IsDBNull(dgvInventory(1, hti.RowIndex).Value) Then
                            txtDNRDecal.Clear()
                        Else
                            txtDNRDecal.Text = dgvInventory(1, hti.RowIndex).Value
                            txtDecalSearch2.Text = txtDNRDecal.Text
                        End If
                        If IsDBNull(dgvInventory(2, hti.RowIndex).Value) Then
                            cboInventoryType.SelectedValue = 0
                        Else
                            cboInventoryType.Text = dgvInventory(2, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(3, hti.RowIndex).Value) Then
                            txtDescription.Clear()
                        Else
                            txtDescription.Text = dgvInventory(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(4, hti.RowIndex).Value) Then
                            DTPDateAssetAcquired.Text = OracleDate
                        Else
                            DTPDateAssetAcquired.Text = dgvInventory(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(5, hti.RowIndex).Value) Then
                            txtCost.Clear()
                        Else
                            txtCost.Text = dgvInventory(5, hti.RowIndex).Value
                            txtCost.Text = Decimal.Parse(txtCost.Text).ToString("c")
                        End If
                        If IsDBNull(dgvInventory(6, hti.RowIndex).Value) Then
                            txtSericalID.Clear()
                        Else
                            txtSericalID.Text = dgvInventory(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(7, hti.RowIndex).Value) Then
                            txtPOID.Clear()
                        Else
                            txtPOID.Text = dgvInventory(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(8, hti.RowIndex).Value) Then
                            mtbReplacementDate.Clear()
                        Else
                            mtbReplacementDate.Text = dgvInventory(8, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvInventory(9, hti.RowIndex).Value) Then
                            rdbActiveInventory.Checked = False
                            rdbInactiveInventory.Checked = False
                        Else
                            If dgvInventory(9, hti.RowIndex).Value = True Then
                                rdbActiveInventory.Checked = True
                            Else
                                rdbInactiveInventory.Checked = True
                            End If
                        End If
                        If IsDBNull(dgvInventory(10, hti.RowIndex).Value) Then
                            txtCurrentStaff.Clear()
                        Else
                            txtCurrentStaff.Text = dgvInventory(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoaddgvInventory(ByVal WhereClause As String)
        Try
            SQL = "Select " & _
            "distinct(" & DBNameSpace & ".PASPInventory.strAssetId) as AssetID, " & _
            "strDecalnumber, " & _
            "strInventoryType, strDescription, " & _
            "datAquired, numCost, " & _
            "strSerialID, " & _
            "strPurchaseOrder, strReplacementYear, " & _
            "strActivityStatus, " & _
            "case " & _
            "when numCurrentStaff is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "end CurrentStaff, " & _
            "strProgramDesc, strUnitDesc " & _
            "from " & DBNameSpace & ".PASPInventory, " & DBNameSpace & ".LookUpPASPInventoryType, " & _
            "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDPrograms, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".PASPInventory.numInventoryID = " & DBNameSpace & ".LookUpPASPInventoryType.numInventoryID " & _
            "and " & DBNameSpace & ".PASPInventory.numCurrentStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and " & DBNameSpace & ".PASPInventory.strAssetID is not null " & _
            WhereClause

            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "Inventory")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchForAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForAsset.Click
        Try
            AssetWhere = ""
            If txtDecalSearch.Text <> "" Then
                AssetWhere = AssetWhere & " and " & DBNameSpace & ".PASPInventory.strDecalNumber like '%" & txtDecalSearch.Text & "%' "
            End If
            If cboInventorySearch.Text <> "" And cboInventorySearch.Text <> " " Then
                AssetWhere = AssetWhere & " and " & DBNameSpace & ".PASPInventory.numInventoryID = '" & cboInventorySearch.SelectedValue & "' "
            End If
            If DTPDatAcquiredStart.Checked = True Then
                AssetWhere = AssetWhere & " and datAquired between '" & DTPDatAcquiredStart.Text & "' and '" & DTPDatAcquiredEnd.Text & "' "
            End If
            If cboSearchByStaff.SelectedValue <> "" And cboSearchByStaff.SelectedValue <> " " Then
                AssetWhere = AssetWhere & " and numCurrentStaff = '" & cboSearchByStaff.SelectedValue & "' "
            End If
            If rdbActiveSearch.Checked = True Or rdbInactiveSearch.Checked = True Then
                If rdbActiveSearch.Checked = True Then
                    AssetWhere = AssetWhere & " and strActivityStatus = 'True' "
                Else
                    AssetWhere = AssetWhere & " and strActivityStatus = 'False' "
                End If
            End If

            btnSearchForAsset.Enabled = False
            btnExportInventory.Enabled = False
            btnAddNewAsset.Enabled = False
            btnUpdateAsset.Enabled = False
            btnDeleteAsset.Enabled = False

            bgwInventory.WorkerReportsProgress = True
            bgwInventory.WorkerSupportsCancellation = True
            bgwInventory.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub DTPDatAcquiredStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPDatAcquiredStart.ValueChanged
        Try
            If DTPDatAcquiredStart.Checked = True Then
                DTPDatAcquiredEnd.Enabled = True
            Else
                DTPDatAcquiredEnd.Enabled = False
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAsset.Click
        Try
            ClearAsset()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub txtCurrentStaff_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCurrentStaff.TextChanged
        Try
            If txtCurrentStaff.Text <> "" And txtCurrentStaff.Text <> "None" Then
                ReturnProgramUnit(txtCurrentStaff.Text)
            Else
                Program = ""
                Unit = ""
            End If

            lblAssetProgramUnit.Text = "Program = " & Program & " / Unit = " & Unit

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportInventory.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvInventory.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvInventory.ColumnCount - 1
                        .Cells(1, i + 1) = dgvInventory.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvInventory.ColumnCount - 1
                        For j = 0 To dgvInventory.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvInventory.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Private Sub bgwInventory_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwInventory.DoWork
        Try

            LoaddgvInventory(AssetWhere)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgw1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInventory.RunWorkerCompleted
        Try
            dgvInventory.DataSource = ds
            dgvInventory.DataMember = "Inventory"

            dgvInventory.RowHeadersVisible = False
            dgvInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvInventory.AllowUserToResizeColumns = True
            dgvInventory.AllowUserToAddRows = False
            dgvInventory.AllowUserToDeleteRows = False
            dgvInventory.AllowUserToOrderColumns = True
            dgvInventory.AllowUserToResizeRows = True
            dgvInventory.ColumnHeadersHeight = "35"
            dgvInventory.Columns("AssetID").HeaderText = "Asset ID"
            dgvInventory.Columns("AssetID").DisplayIndex = 0
            dgvInventory.Columns("strDecalnumber").HeaderText = "Decal Number"
            dgvInventory.Columns("strDecalnumber").DisplayIndex = 1
            dgvInventory.Columns("strInventoryType").HeaderText = "Inventory Type"
            dgvInventory.Columns("strInventoryType").DisplayIndex = 2
            dgvInventory.Columns("strDescription").HeaderText = "Description"
            dgvInventory.Columns("strDescription").DisplayIndex = 3
            dgvInventory.Columns("datAquired").HeaderText = "Date Aquired"
            dgvInventory.Columns("datAquired").DisplayIndex = 4
            dgvInventory.Columns("datAquired").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvInventory.Columns("numCost").HeaderText = "Asset Cost"
            dgvInventory.Columns("numCost").DisplayIndex = 5
            dgvInventory.Columns("numCost").DefaultCellStyle.Format = "c"
            dgvInventory.Columns("strSerialID").HeaderText = "Serial ID"
            dgvInventory.Columns("strSerialID").DisplayIndex = 6
            dgvInventory.Columns("strPurchaseOrder").HeaderText = "Purchase Order"
            dgvInventory.Columns("strPurchaseOrder").DisplayIndex = 7
            dgvInventory.Columns("strReplacementYear").HeaderText = "Replacement Year"
            dgvInventory.Columns("strReplacementYear").DisplayIndex = 8
            dgvInventory.Columns("strActivityStatus").HeaderText = "Activity Status"
            dgvInventory.Columns("strActivityStatus").DisplayIndex = 9
            dgvInventory.Columns("CurrentStaff").HeaderText = "Current Staff"
            dgvInventory.Columns("CurrentStaff").DisplayIndex = 10
            dgvInventory.Columns("strProgramDesc").HeaderText = "Program"
            dgvInventory.Columns("strProgramDesc").DisplayIndex = 11
            dgvInventory.Columns("strUnitDesc").HeaderText = "Unit"
            dgvInventory.Columns("strUnitDesc").DisplayIndex = 12

            txtInventoryCount.Text = dgvInventory.RowCount.ToString

            btnSearchForAsset.Enabled = True
            btnExportInventory.Enabled = True
            btnAddNewAsset.Enabled = True
            btnUpdateAsset.Enabled = True
            btnDeleteAsset.Enabled = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Transactions"
    Sub AddNewTransaction()
        Try
            Dim Replacement As String
            Dim AssetRole As String

            If chbReplacementOrdered.Checked = True Then
                Replacement = True
            Else
                Replacement = False
            End If
            If rdbPrimaryUse.Checked = True Then
                AssetRole = "1"
            Else
                AssetRole = "0"
            End If

            If txtAssetTransaction.Text <> "" Then
                SQL = "Select strAssetID " & _
                "from " & DBNameSpace & ".PASPInventory " & _
                "where strAssetID = '" & txtAssetTransaction.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    MsgBox("The Asset ID is invalid." & vbCrLf & _
                            "Please enter a valid Asset ID before saving can occur.", MsgBoxStyle.Exclamation, "PASP Inventory")
                    Exit Sub
                End If
            End If

            SQL = "Insert into " & DBNameSpace & ".PASPInventoryTransactions " & _
            "values " & _
            "((Select max(numTransactionId) + 1 " & _
            "from " & DBNameSpace & ".PASPInventoryTransactions " & _
            "where length(trim(Translate(strAssetid, '+-.0123456789',' '))) is null ),  " & _
            "'" & txtAssetTransaction.Text & "', '" & cboTransactionType.SelectedValue & "', " & _
            "'" & cboStaff.SelectedValue & "', '" & DTPTransactionDate.Text & "', " & _
            "'" & Replacement & "', " & _
            "'" & txtTransactionComments.Text & "', '" & AssetRole & "') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Select max(numTransactionID) as TransactionID " & _
            "from " & DBNameSpace & ".PASPInventoryTransactions " & _
            "where length(trim(Translate(numTransactionID, '+-.0123456789',' '))) is null "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("TransactionID")) Then
                    txtTransactionID.Text = "ERROR"
                Else
                    txtTransactionID.Text = dr.Item("TransactionID")
                End If
            End While
            dr.Close()

            If txtAssetTransaction.Text <> "" Then
                SQL = "Update " & DBNameSpace & ".PASPInventory set " & _
                "numCurrentStaff = '" & cboStaff.SelectedValue & "' " & _
                "where strAssetID = '" & txtAssetTransaction.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            If cboTransactionType.SelectedValue = "4" Then
                SQL = "Update " & DBNameSpace & ".PASPInventory set " & _
                "strActivityStatus = 'False' " & _
                "where strAssetID = '" & txtAssetTransaction.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            btnAddTransaction.Enabled = False
            btnEditTransaction.Enabled = False
            btnDeleteTransaction.Enabled = False
            btnTransactionSearch.Enabled = False

            bgwTransactions.WorkerReportsProgress = True
            bgwTransactions.WorkerSupportsCancellation = True
            bgwTransactions.RunWorkerAsync()
            MsgBox("Transaction Added", MsgBoxStyle.Information, "PASP Inventory")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub UpdateTransaction()
        Try
            Dim Replacement As String
            Dim AssetRole As String

            If txtTransactionID.Text <> "" Then
                If chbReplacementOrdered.Checked = True Then
                    Replacement = True
                Else
                    Replacement = False
                End If
                If rdbPrimaryUse.Checked = True Then
                    AssetRole = "1"
                Else
                    AssetRole = "0"
                End If

                SQL = "Select numTransactionID " & _
                "From " & DBNameSpace & ".PASPInventoryTransactions " & _
                "where numTransactionID = '" & txtTransactionID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()

                If recExist = True Then
                    SQL = "Update " & DBNameSpace & ".PASPInventoryTransactions set " & _
                    "strAssetID = '" & Replace(txtAssetTransaction.Text, "'", "''") & "', " & _
                    "numTransactionType = '" & cboTransactionType.SelectedValue & "', " & _
                    "numStaff = '" & cboStaff.SelectedValue & "', " & _
                    "datTransactionDate = '" & DTPTransactionDate.Text & "', " & _
                    "strReplacementOrdered = '" & Replacement & "', " & _
                    "strTransactionComments = '" & Replace(txtTransactionComments.Text, "'", "''") & "', " & _
                    "strAssetRole = '" & AssetRole & "' " & _
                    "where numTransactionID = '" & txtTransactionID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    If txtAssetTransaction.Text <> "" Then
                        SQL = "Update " & DBNameSpace & ".PASPInventory set " & _
                        "numCurrentStaff = '" & cboStaff.SelectedValue & "' " & _
                        "where strAssetID = '" & txtAssetTransaction.Text & "' "
                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                    If cboTransactionType.SelectedValue = "4" Then
                        SQL = "Update " & DBNameSpace & ".PASPInventory set " & _
                        "strActivityStatus = 'False' " & _
                        "where strAssetID = '" & txtAssetTransaction.Text & "' "

                        cmd = New OracleCommand(SQL, Conn)
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()
                    End If

                    btnAddTransaction.Enabled = False
                    btnEditTransaction.Enabled = False
                    btnDeleteTransaction.Enabled = False
                    btnTransactionSearch.Enabled = False

                    bgwTransactions.WorkerReportsProgress = True
                    bgwTransactions.WorkerSupportsCancellation = True
                    bgwTransactions.RunWorkerAsync()
                    MsgBox("Transaction Updated", MsgBoxStyle.Information, "PASP Inventory")

                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub DeleteTransaction()
        Try

            Dim Result As DialogResult
            Result = MessageBox.Show("Are you sure you want to remove this Transaction?", _
              "PASP Inventory", MessageBoxButtons.YesNoCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            Select Case Result
                Case Windows.Forms.DialogResult.Yes
                    SQL = "Delete " & DBNameSpace & ".PASPInventoryTransactions " & _
                          "where numTransactionID = '" & txtTransactionID.Text & "' "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    ClearTransaction()

                    btnAddTransaction.Enabled = False
                    btnEditTransaction.Enabled = False
                    btnDeleteTransaction.Enabled = False
                    btnTransactionSearch.Enabled = False

                    bgwTransactions.WorkerReportsProgress = True
                    bgwTransactions.WorkerSupportsCancellation = True
                    bgwTransactions.RunWorkerAsync()
                    MsgBox("Trasnaction Deleted", MsgBoxStyle.Information, "PASP Inventory")
                Case Windows.Forms.DialogResult.No

                Case Windows.Forms.DialogResult.Cancel

                Case Else

            End Select


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearTransaction()
        Try
            txtTransactionID.Clear()
            txtAssetTransaction.Clear()
            cboTransactionType.SelectedValue = 0
            DTPTransactionDate.Text = OracleDate
            cboStaff.SelectedValue = 0
            rdbPrimaryUse.Checked = False
            rdbSecondaryUse.Checked = False
            chbReplacementOrdered.Checked = False
            txtTransactionComments.Clear()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTransaction.Click
        Try
            If txtTransactionID.Text = "" Then
                AddNewTransaction()
            Else
                MsgBox("Transaction ID must be blank to add new Transaction.", MsgBoxStyle.Information, "PASP Inventory")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTransaction.Click
        Try
            If txtTransactionID.Text <> "" Then
                UpdateTransaction()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTransaction.Click
        Try
            If txtTransactionID.Text <> "" Then
                DeleteTransaction()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoaddgvTransactions(ByVal WhereClause As String)
        Try
            SQL = "select " & _
            "numTransactionID, " & _
            "" & DBNameSpace & ".PASPInventoryTransactions.strAssetID,  " & _
            "strTransactionType, " & _
            "case " & _
            "when numStaff is Null then ' ' " & _
            "else (strLastName||', '||strFirstName) " & _
            "end Staff, " & _
            "datTransactionDate, " & _
            "case " & _
            "when strReplacementOrdered = 'True' then 'Yes' " & _
            "else 'No' " & _
            "end strReplacementOrdered, " & _
            "strTransactionComments,  " & _
            "case " & _
            "when strAssetRole is null then 'Primary' " & _
            "when strAssetRole = '1' then 'Primary' " & _
            "when strAssetRole = '0' then 'Secondary' " & _
            "else 'Primary' " & _
            "End AssetRole, " & _
            "numStaff,  " & _
            "strProgramDesc, strUnitDesc, " & _
            "strDecalNumber " & _
            "from " & DBNameSpace & ".PASPInventoryTransactions, " & DBNameSpace & ".LookUpPASPTransactionType, " & _
            "" & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookUpEPDPrograms, " & _
            "" & DBNameSpace & ".LookUpEPDUnits, " & DBNameSpace & ".PASPInventory " & _
            "where " & DBNameSpace & ".PASPInventoryTransactions.numTransactionType = " & _
            "" & DBNameSpace & ".LookUpPASPTransactionType.numTransactiontype  " & _
            "and " & DBNameSpace & ".PASPInventoryTransactions.strAssetID = " & DBNameSpace & ".PASPInventory.strAssetID (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and " & DBNameSpace & ".PASPInventoryTransactions.numStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            WhereClause

            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "Transactions")


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvTransactions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTransactions.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvTransactions.HitTest(e.X, e.Y)

            If hti.Type = DataGrid.HitTestType.Cell Then
                If dgvTransactions.RowCount > 0 And hti.RowIndex <> -1 Then
                    If dgvTransactions.Columns(0).HeaderText = "Transaction ID" Then
                        txtTransactionID.Text = dgvTransactions(0, hti.RowIndex).Value
                        If IsDBNull(dgvTransactions(1, hti.RowIndex).Value) Then
                            txtAssetTransaction.Clear()
                        Else
                            txtAssetTransaction.Text = dgvTransactions(1, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvTransactions(11, hti.RowIndex).Value) Then
                            txtDNRDecalTransaction.Clear()
                        Else
                            txtDNRDecalTransaction.Text = dgvTransactions(11, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvTransactions(2, hti.RowIndex).Value) Then
                            cboTransactionType.SelectedValue = 0
                        Else
                            cboTransactionType.Text = dgvTransactions(2, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvTransactions(3, hti.RowIndex).Value) Then
                            cboStaff.SelectedValue = 0
                        Else
                            temp = dgvTransactions(3, hti.RowIndex).Value
                            cboStaff.Text = dgvTransactions(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvTransactions(4, hti.RowIndex).Value) Then
                            DTPTransactionDate.Text = OracleDate
                        Else
                            temp = dgvTransactions(4, hti.RowIndex).Value
                            DTPTransactionDate.Text = dgvTransactions(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvTransactions(5, hti.RowIndex).Value) Then
                            chbReplacementOrdered.Checked = False
                        Else
                            If dgvTransactions(5, hti.RowIndex).Value = "Yes" Then
                                chbReplacementOrdered.Checked = True
                            Else
                                chbReplacementOrdered.Checked = False
                            End If
                        End If
                        If IsDBNull(dgvTransactions(6, hti.RowIndex).Value) Then
                            txtTransactionComments.Clear()
                        Else
                            txtTransactionComments.Text = dgvTransactions(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvTransactions(7, hti.RowIndex).Value) Then
                            rdbPrimaryUse.Checked = False
                            rdbSecondaryUse.Checked = False
                        Else
                            If dgvTransactions(7, hti.RowIndex).Value = "Primary" Then
                                rdbPrimaryUse.Checked = True
                            Else
                                rdbSecondaryUse.Checked = True
                            End If
                        End If

                    End If
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnTransactionSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransactionSearch.Click
        Try
            TransactionWhere = ""
            If txtTransactionIDSearch.Text <> "" Then
                TransactionWhere = TransactionWhere & " and numTransactionID like '%" & txtTransactionIDSearch.Text & "%' "
            End If
            If txtDecalSearch2.Text <> "" Then
                TransactionWhere = TransactionWhere & " and strDecalNumber like '%" & txtDecalSearch2.Text & "%' "
            End If
            If cboTransactionTypeSearch.Text <> "" And cboTransactionTypeSearch.Text <> " " Then
                TransactionWhere = TransactionWhere & " and " & DBNameSpace & ".PASPInventoryTransactions.numTransactionType = " & _
                      "'" & cboTransactionTypeSearch.SelectedValue & "' "
            End If
            If cboTransactionStaffSearch.Text <> "" And cboTransactionStaffSearch.Text <> " " Then
                TransactionWhere = TransactionWhere & " and " & DBNameSpace & ".PASPInventoryTransactions.numStaff = " & _
                      "'" & cboTransactionStaffSearch.SelectedValue & "' "
            End If

            btnAddTransaction.Enabled = False
            btnEditTransaction.Enabled = False
            btnDeleteTransaction.Enabled = False
            btnTransactionSearch.Enabled = False

            bgwTransactions.WorkerReportsProgress = True
            bgwTransactions.WorkerSupportsCancellation = True
            bgwTransactions.RunWorkerAsync()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearTransactionID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTransactionID.Click
        Try
            txtTransactionID.Clear()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTransaction.Click
        Try
            ClearTransaction()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboStaff_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStaff.TextChanged
        Try
            If cboStaff.Text <> "" And cboStaff.Text <> " " Then
                ReturnProgramUnit(cboStaff.Text)
            Else
                Program = ""
                Unit = ""
            End If

            lblTransactionProgramUnit.Text = "Program = " & Program & " / Unit = " & Unit
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportTransactions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportTransactions.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvTransactions.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvTransactions.ColumnCount - 1
                        .Cells(1, i + 1) = dgvTransactions.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvTransactions.ColumnCount - 1
                        For j = 0 To dgvTransactions.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvTransactions.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub
    Private Sub bgwTransactions_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwTransactions.DoWork
        Try

            LoaddgvTransactions(TransactionWhere)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwTransactions_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTransactions.RunWorkerCompleted
        Try

            dgvTransactions.DataSource = ds
            dgvTransactions.DataMember = "Transactions"

            dgvTransactions.RowHeadersVisible = False
            dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvTransactions.AllowUserToResizeColumns = True
            dgvTransactions.AllowUserToAddRows = False
            dgvTransactions.AllowUserToDeleteRows = False
            dgvTransactions.AllowUserToOrderColumns = True
            dgvTransactions.AllowUserToResizeRows = True
            dgvTransactions.ColumnHeadersHeight = "35"
            dgvTransactions.Columns("numTransactionID").HeaderText = "Transaction ID"
            dgvTransactions.Columns("numTransactionID").DisplayIndex = 0
            dgvTransactions.Columns("strAssetID").HeaderText = "Asset ID"
            dgvTransactions.Columns("strAssetID").DisplayIndex = 1
            dgvTransactions.Columns("strTransactionType").HeaderText = "Transaction Type"
            dgvTransactions.Columns("strTransactionType").DisplayIndex = 3
            dgvTransactions.Columns("Staff").HeaderText = "Staff"
            dgvTransactions.Columns("Staff").DisplayIndex = 4
            dgvTransactions.Columns("datTransactionDate").HeaderText = "Date Transaction"
            dgvTransactions.Columns("datTransactionDate").DisplayIndex = 5
            dgvTransactions.Columns("datTransactionDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvTransactions.Columns("strReplacementOrdered").HeaderText = "Replacement Ordered"
            dgvTransactions.Columns("strReplacementOrdered").DisplayIndex = 6
            dgvTransactions.Columns("strTransactionComments").HeaderText = "Transaction Comments"
            dgvTransactions.Columns("strTransactionComments").DisplayIndex = 7
            dgvTransactions.Columns("AssetRole").HeaderText = "Asset Role"
            dgvTransactions.Columns("AssetRole").DisplayIndex = 8
            dgvTransactions.Columns("numStaff").HeaderText = "StaffID"
            dgvTransactions.Columns("numStaff").DisplayIndex = 9
            dgvTransactions.Columns("numStaff").Visible = False
            dgvTransactions.Columns("strProgramDesc").HeaderText = "Program"
            dgvTransactions.Columns("strProgramDesc").DisplayIndex = 10
            dgvTransactions.Columns("strUnitDesc").HeaderText = "Unit"
            dgvTransactions.Columns("strUnitDesc").DisplayIndex = 11
            dgvTransactions.Columns("strDecalNumber").HeaderText = "DNR Decal #"
            dgvTransactions.Columns("strDecalNumber").DisplayIndex = 2

            txtTransactionCount.Text = dgvTransactions.RowCount.ToString

            btnAddTransaction.Enabled = True
            btnEditTransaction.Enabled = True
            btnDeleteTransaction.Enabled = True
            btnTransactionSearch.Enabled = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region
#Region "Reports"
    Private Sub btnViewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewReport.Click
        Try
            ReportWhere = " "
            If cboReportProgram.Text = "-All Programs-" Then
                ReportWhere = " "
            Else
                ReportWhere = "and " & DBNameSpace & ".EPDUserProfiles.numProgram = '" & cboReportProgram.SelectedValue & "' "
            End If
            If cboReportUnit.Text = "-All Units-" Then
                ReportWhere = ReportWhere & " "
            Else
                ReportWhere = ReportWhere & "and " & DBNameSpace & ".EPDUserProfiles.numUnit = '" & cboReportUnit.SelectedValue & "' "
            End If
            If cboReportInventoryType.Text = "-All Types-" Then
                ReportWhere = ReportWhere & " "
            Else
                ReportWhere = ReportWhere & "and " & DBNameSpace & ".PASPInventory.numInventoryID = '" & cboReportInventoryType.SelectedValue & "' "
            End If
            If rdbAssignedInventory.Checked = True Then
                ReportWhere = ReportWhere & " and " & DBNameSpace & ".EPDUserProfiles.numBranch = '1' "
            Else
                ReportWhere = ReportWhere & " and " & DBNameSpace & ".PASPInventory.numCurrentStaff is null "
            End If

            btnViewReport.Enabled = False

            bgwReports.WorkerReportsProgress = True
            bgwReports.WorkerSupportsCancellation = True
            bgwReports.RunWorkerAsync()

            TCReport.SelectedIndex = 0
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwReports_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwReports.DoWork
        Try

            SQL = "select " & _
            "case " & _
            "when strLastName is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "End Staff, " & _
            "strProgramDesc, strUnitDesc, " & _
            "strOffice, strDecalNumber, " & _
            "strDescription, strReplacementYear " & _
            "from " & DBNameSpace & ".PASPInventory, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDPrograms, " & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".PASPInventory.numCurrentStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numBranch = '1' " & _
            "and strActivityStatus = 'True' " & _
            ReportWhere & _
            "order by strProgramDesc, strUnitDesc, Staff  "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da.Fill(ds, "InventoryReport")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bgwReports_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwReports.RunWorkerCompleted
        Try

            dgvReport.DataSource = ds
            dgvReport.DataMember = "InventoryReport"

            dgvReport.RowHeadersVisible = False
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvReport.AllowUserToResizeColumns = True
            dgvReport.AllowUserToAddRows = False
            dgvReport.AllowUserToDeleteRows = False
            dgvReport.AllowUserToOrderColumns = True
            dgvReport.AllowUserToResizeRows = True
            dgvReport.ColumnHeadersHeight = "35"
            dgvReport.Columns("strProgramDesc").HeaderText = "Program"
            dgvReport.Columns("strProgramDesc").DisplayIndex = 0
            dgvReport.Columns("strProgramDesc").Width = 150
            dgvReport.Columns("strUnitDesc").HeaderText = "Unit"
            dgvReport.Columns("strUnitDesc").DisplayIndex = 1
            dgvReport.Columns("strUnitDesc").Width = 150
            dgvReport.Columns("Staff").HeaderText = "Staff"
            dgvReport.Columns("Staff").DisplayIndex = 2
            dgvReport.Columns("Staff").Width = 150
            dgvReport.Columns("strOffice").HeaderText = "Office"
            dgvReport.Columns("strOffice").DisplayIndex = 3
            dgvReport.Columns("strDecalNumber").HeaderText = "Decal Number"
            dgvReport.Columns("strDecalNumber").DisplayIndex = 4
            dgvReport.Columns("strDescription").HeaderText = "Description"
            dgvReport.Columns("strDescription").DisplayIndex = 5
            dgvReport.Columns("strReplacementYear").HeaderText = "Replacement Year"
            dgvReport.Columns("strReplacementYear").DisplayIndex = 6

            btnViewReport.Enabled = True
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



#End Region
    Private Sub btnViewInventoryReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewInventoryReport.Click
        Try
            Dim Staff As String
            Dim Program As String
            Dim Unit As String
            Dim Office As String
            Dim Decal As String
            Dim Description As String
            Dim ReplacementYear As String
            Dim InventoryType As String

            ReportWhere = " "
            If cboReportProgram.Text = "-All Programs-" Then
                ReportWhere = " "
            Else
                ReportWhere = "and " & DBNameSpace & ".EPDUserProfiles.numProgram = '" & cboReportProgram.SelectedValue & "' "
            End If
            If cboReportUnit.Text = "-All Units-" Then
                ReportWhere = ReportWhere & " "
            Else
                ReportWhere = ReportWhere & "and " & DBNameSpace & ".EPDUserProfiles.numUnit = '" & cboReportUnit.SelectedValue & "' "
            End If
            If cboReportInventoryType.Text = "-All Types-" Then
                ReportWhere = ReportWhere & " "
            Else
                ReportWhere = ReportWhere & "and " & DBNameSpace & ".PASPInventory.numInventoryID = '" & cboReportInventoryType.SelectedValue & "' "
            End If
            If rdbAssignedInventory.Checked = True Then
                ReportWhere = ReportWhere & " and " & DBNameSpace & ".EPDUserProfiles.numBranch = '1' "
            Else
                ReportWhere = ReportWhere & " and " & DBNameSpace & ".PASPInventory.numCurrentStaff is null "
            End If

            Dim ds2 As dsInventoryReport = New dsInventoryReport
            ds2.EnforceConstraints = False
            SQL = "select " & _
            "case " & _
            "when strLastName is null then '' " & _
            "else (strLastName||', '||strFirstName) " & _
            "End Staff, " & _
            "strProgramDesc, strUnitDesc, " & _
            "strOffice, strDecalNumber, " & _
            "strDescription, strReplacementYear, " & _
            "" & DBNameSpace & ".LookUpPASPInventoryType.strInventoryType " & _
            "from " & DBNameSpace & ".PASPInventory, " & DBNameSpace & ".EPDUserProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDPrograms, " & DBNameSpace & ".LookUpEPDUnits,  " & _
            "" & DBNameSpace & ".LookUpPASPInventoryType " & _
            "where " & DBNameSpace & ".PASPInventory.numCurrentStaff = " & DBNameSpace & ".EPDUserProfiles.numUserID (+) " & _
            "and " & DBNameSpace & ".PASPInventory.NumInventoryID = " & DBNameSpace & ".LookUpPASPInventoryType.numInventoryID (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and strActivityStatus = 'True' " & _
            ReportWhere & _
            "order by strProgramDesc, strUnitDesc, Staff  "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("Staff")) Then
                    Staff = ""
                Else
                    Staff = dr.Item("Staff")
                End If
                If IsDBNull(dr.Item("strProgramDesc")) Then
                    Program = "-"
                Else
                    Program = dr.Item("strProgramDesc")
                End If
                If IsDBNull(dr.Item("strUnitDesc")) Then
                    Unit = "-"
                Else
                    Unit = dr.Item("strUnitDesc")
                End If
                If IsDBNull(dr.Item("strOffice")) Then
                    Office = ""
                Else
                    Office = dr.Item("strOffice")
                End If
                If IsDBNull(dr.Item("strDecalNumber")) Then
                    Decal = ""
                Else
                    Decal = dr.Item("strDecalNumber")
                End If
                If IsDBNull(dr.Item("strDescription")) Then
                    Description = ""
                Else
                    Description = dr.Item("strDescription")
                End If
                If IsDBNull(dr.Item("strReplacementYear")) Then
                    ReplacementYear = ""
                Else
                    ReplacementYear = dr.Item("strReplacementYear")
                End If
                If IsDBNull(dr.Item("strInventoryType")) Then
                    InventoryType = ""
                Else
                    InventoryType = dr.Item("strInventoryType")
                End If

                ds2.InventoryReport.AddInventoryReportRow(Staff, Program, Unit, Office, Decal, Description, ReplacementYear, InventoryType)
            End While
            dr.Close()

            rpt = New CRInventoryReport
            monitor.TrackFeature("Report." & rpt.ResourceName)

            rpt.SetDataSource(ds2)

            CRInventoryReport.ReportSource = rpt
            TCReport.SelectedIndex = 1

        Catch ex As Exception

        End Try
    End Sub

#Region "GAIT Look Up Tables"
    Private Sub LoadGAITCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadGAITCategory.Click
        Try

            If gbGAITCategory.Enabled = True Then
                gbGAITCategory.Enabled = False
            Else
                gbGAITCategory.Enabled = True
                LoadGAITCategoryEdit()
                txtGAITCategoryID.Clear()
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub LoadGAITCategoryEdit()
        Try
            Dim dtGAITCategory As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numCategoryID, strCategoryDesc, " & _
            "Active " & _
            "from " & DBNameSpace & ".LookUpGAITCategory " & _
            "order by numCategoryID "

            dsGAITCategory = New DataSet
            daGAITCategory = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITCategory.Fill(dsGAITCategory, "GAITCategoryEdit")

            dtGAITCategory.Columns.Add("numCategoryID", GetType(System.String))
            dtGAITCategory.Columns.Add("strCategoryDesc", GetType(System.String))

            drNewRow = dtGAITCategory.NewRow()
            drNewRow("numCategoryID") = " "
            drNewRow("strCategoryDesc") = " "
            dtGAITCategory.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITCategory.Tables("GAITCategoryEdit").Rows()
                drNewRow = dtGAITCategory.NewRow()
                drNewRow("numCategoryID") = drDSRow("numCategoryID")
                drNewRow("strCategoryDesc") = drDSRow("strCategoryDesc")
                dtGAITCategory.Rows.Add(drNewRow)
            Next

            With cboExistingGAITCategory
                .DataSource = dtGAITCategory
                .DisplayMember = "strCategoryDesc"
                .ValueMember = "numCategoryID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingGAITCategory_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingGAITCategory.TextChanged
        Try
            Dim GAITID As Integer
            Dim GAITActive As String

            txtAddEditGAITCategory.Clear()
            rdbGAITCategoryActive.Checked = False

            txtGAITCategoryID.Text = cboExistingGAITCategory.SelectedValue.ToString
            If txtGAITCategoryID.Text <> "" And txtGAITCategoryID.Text <> " " And txtGAITCategoryID.Text <> "System.Data.DataRowView" Then
                GAITID = txtGAITCategoryID.Text
                If IsDBNull(dsGAITCategory.Tables("GAITCategoryEdit").Rows(txtGAITCategoryID.Text - 1).Item(1)) Then
                    txtAddEditGAITCategory.Clear()
                Else
                    txtAddEditGAITCategory.Text = dsGAITCategory.Tables("GAITCategoryEdit").Rows(txtGAITCategoryID.Text - 1).Item(1)
                End If
                If IsDBNull(dsGAITCategory.Tables("GAITCategoryEdit").Rows(txtGAITCategoryID.Text - 1).Item(2)) Then
                    GAITActive = "0"
                Else
                    GAITActive = dsGAITCategory.Tables("GAITCategoryEdit").Rows(txtGAITCategoryID.Text - 1).Item(2)
                End If
                If GAITActive <> "1" Then
                    rdbGAITCategoryActive.Checked = False
                Else
                    rdbGAITCategoryActive.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGaitCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearGaitCategory.Click
        Try
            txtGAITCategoryID.Clear()
            cboExistingGAITCategory.SelectedValue = " "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddGAITCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGAITCategory.Click
        Try
            Dim GAITID As String = ""
            If IsDBNull(dsGAITCategory.Tables("GAITCategoryEdit").Rows.Count) Then
                GAITID = "1"
            Else
                GAITID = dsGAITCategory.Tables("GAITCategoryEdit").Rows.Count + 1
            End If

            If txtAddEditGAITCategory.Text <> "" Then
                SQL = "Select " & _
                "numCategoryID " & _
                "from " & DBNameSpace & ".LookupGAITCategory " & _
                "where Upper(strCategoryDesc) = '" & Replace(txtAddEditGAITCategory.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("Category already exists in the database." & vbCrLf & _
                           "Use the Edit button.", _
                           MsgBoxStyle.Exclamation, "GAIT Inventory")
                    Exit Sub
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpGAITCategory " & _
                    "values " & _
                    "((select max(numCategoryID) + 1 from " & DBNameSpace & ".LookUpGAITCategory), " & _
                    "'" & txtAddEditGAITCategory.Text & "', " & _
                    "'1') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadGAITCategoryEdit()
                cboExistingGAITCategory.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditGAITCategory.Click
        Try
            Dim GAITID As String = ""
            If txtGAITCategoryID.Text <> "" And txtGAITCategoryID.Text <> " " Then
                GAITID = txtGAITCategoryID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitCategory set " & _
                "strCategoryDESC = '" & Replace(txtAddEditGAITCategory.Text, "'", "''") & "', " & _
                "Active = '1' " & _
                "where numCategoryID = '" & txtGAITCategoryID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITCategoryEdit()
                cboExistingGAITCategory.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteGAITCategory.Click
        Try
            Dim GAITID As String = ""

            If txtGAITCategoryID.Text <> "" And txtGAITCategoryID.Text <> " " Then
                GAITID = txtGAITCategoryID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitCategory set " & _
                "Active = '0' " & _
                "where numCategoryID = '" & txtGAITCategoryID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITCategoryEdit()
                cboExistingGAITCategory.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadGAITManufacturer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadGAITManufacturer.Click
        Try
            If gbGAITManufacturer.Enabled = True Then
                gbGAITManufacturer.Enabled = False
            Else
                gbGAITManufacturer.Enabled = True
                LoadGAITManufacturerEdit()
                txtGAITManufacturerID.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadGAITManufacturerEdit()
        Try
            Dim dtGAITManufacturer As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numManufactureID, strManufactureDesc, " & _
            "Active " & _
            "from " & DBNameSpace & ".LookUpGAITManufacture " & _
            "order by numManufactureID "

            dsGAITManufacturer = New DataSet
            daGAITManufacturer = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITManufacturer.Fill(dsGAITManufacturer, "GAITManufacturerEdit")

            dtGAITManufacturer.Columns.Add("numManufactureID", GetType(System.String))
            dtGAITManufacturer.Columns.Add("strManufactureDesc", GetType(System.String))

            drNewRow = dtGAITManufacturer.NewRow()
            drNewRow("numManufactureID") = " "
            drNewRow("strManufactureDesc") = " "
            dtGAITManufacturer.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITManufacturer.Tables("GAITManufacturerEdit").Rows()
                drNewRow = dtGAITManufacturer.NewRow()
                drNewRow("numManufactureID") = drDSRow("numManufactureID")
                drNewRow("strManufactureDesc") = drDSRow("strManufactureDesc")
                dtGAITManufacturer.Rows.Add(drNewRow)
            Next

            With cboExistingGAITManufacturer
                .DataSource = dtGAITManufacturer
                .DisplayMember = "strManufactureDesc"
                .ValueMember = "numManufactureID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingGAITManufacturer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingGAITManufacturer.TextChanged
        Try
            Dim GAITID As Integer
            Dim GAITActive As String

            txtAddEditGAITManufacturer.Clear()
            rdbGAITManufacturerActive.Checked = False

            txtGAITManufacturerID.Text = cboExistingGAITManufacturer.SelectedValue.ToString
            If txtGAITManufacturerID.Text <> "" And txtGAITManufacturerID.Text <> " " And txtGAITManufacturerID.Text <> "System.Data.DataRowView" Then
                GAITID = txtGAITManufacturerID.Text
                If IsDBNull(dsGAITManufacturer.Tables("GAITManufacturerEdit").Rows(txtGAITManufacturerID.Text - 1).Item(1)) Then
                    txtAddEditGAITManufacturer.Clear()
                Else
                    txtAddEditGAITManufacturer.Text = dsGAITManufacturer.Tables("GAITManufacturerEdit").Rows(txtGAITManufacturerID.Text - 1).Item(1)
                End If
                If IsDBNull(dsGAITManufacturer.Tables("GAITManufacturerEdit").Rows(txtGAITManufacturerID.Text - 1).Item(2)) Then
                    GAITActive = "0"
                Else
                    GAITActive = dsGAITManufacturer.Tables("GAITManufacturerEdit").Rows(txtGAITManufacturerID.Text - 1).Item(2)
                End If
                If GAITActive <> "1" Then
                    rdbGAITManufacturerActive.Checked = False
                Else
                    rdbGAITManufacturerActive.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGaitManufacturer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearGaitManufacturer.Click
        Try
            txtGAITManufacturerID.Clear()
            cboExistingGAITManufacturer.SelectedValue = " "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddGAITManufaturer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGAITManufaturer.Click
        Try
            Dim GAITID As String = ""
            If IsDBNull(dsGAITManufacturer.Tables("GAITmanufacturerEdit").Rows.Count) Then
                GAITID = "1"
            Else
                GAITID = dsGAITManufacturer.Tables("GAITmanufacturerEdit").Rows.Count + 1
            End If

            If txtAddEditGAITManufacturer.Text <> "" Then
                SQL = "Select " & _
                "nummanufactureID " & _
                "from " & DBNameSpace & ".LookupGAITmanufacture " & _
                "where Upper(strmanufactureDesc) = '" & Replace(txtAddEditGAITManufacturer.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("Manufacturer already exists in the database." & vbCrLf & _
                           "Use the Edit button.", _
                           MsgBoxStyle.Exclamation, "GAIT Inventory")
                    Exit Sub
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpGAITmanufacture " & _
                    "values " & _
                    "((select max(nummanufactureID) + 1 from " & DBNameSpace & ".LookUpGAITmanufacture), " & _
                    "'" & txtAddEditGAITManufacturer.Text & "', " & _
                    "'1') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadGAITManufacturerEdit()
                cboExistingGAITManufacturer.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITManufacturer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditGAITManufacturer.Click
        Try
            Dim GAITID As String = ""
            If txtGAITManufacturerID.Text <> "" And txtGAITManufacturerID.Text <> " " Then
                GAITID = txtGAITManufacturerID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitManufacture set " & _
                "strManufactureDESC = '" & Replace(txtAddEditGAITManufacturer.Text, "'", "''") & "', " & _
                "Active = '1' " & _
                "where numManufactureID = '" & txtGAITManufacturerID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITManufacturerEdit()
                cboExistingGAITManufacturer.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITManufaturer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGAITManufaturer.Click
        Try
            Dim GAITID As String = ""

            If txtGAITManufacturerID.Text <> "" And txtGAITManufacturerID.Text <> " " Then
                GAITID = txtGAITManufacturerID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitManufacture set " & _
                "Active = '0' " & _
                "where numManufactureID = '" & txtGAITManufacturerID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITManufacturerEdit()
                cboExistingGAITManufacturer.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadGAITModel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadGAITModel.Click
        Try

            If gbGAITModel.Enabled = True Then
                gbGAITModel.Enabled = False
            Else
                gbGAITModel.Enabled = True
                LoadGAITModelEdit()
                txtGAITModelID.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadGAITModelEdit()
        Try
            Dim dtGAITModel As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numModelID, strModelDesc, " & _
            "Active " & _
            "from " & DBNameSpace & ".LookUpGAITModel " & _
            "order by numModelID "

            dsGAITModel = New DataSet
            daGAITModel = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITModel.Fill(dsGAITModel, "GAITModelEdit")

            dtGAITModel.Columns.Add("numModelID", GetType(System.String))
            dtGAITModel.Columns.Add("strModelDesc", GetType(System.String))

            drNewRow = dtGAITModel.NewRow()
            drNewRow("numModelID") = " "
            drNewRow("strModelDesc") = " "
            dtGAITModel.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITModel.Tables("GAITModelEdit").Rows()
                drNewRow = dtGAITModel.NewRow()
                drNewRow("numModelID") = drDSRow("numModelID")
                drNewRow("strModelDesc") = drDSRow("strModelDesc")
                dtGAITModel.Rows.Add(drNewRow)
            Next

            With cboExistingGAITModel
                .DataSource = dtGAITModel
                .DisplayMember = "strModelDesc"
                .ValueMember = "numModelID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingGAITModel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingGAITModel.TextChanged
        Try
            Dim GAITID As Integer
            Dim GAITActive As String

            txtAddEditGAITModel.Clear()
            rdbGAITModelActive.Checked = False

            txtGAITModelID.Text = cboExistingGAITModel.SelectedValue.ToString
            If txtGAITModelID.Text <> "" And txtGAITModelID.Text <> " " And txtGAITModelID.Text <> "System.Data.DataRowView" Then
                GAITID = txtGAITModelID.Text
                If IsDBNull(dsGAITModel.Tables("GAITModelEdit").Rows(txtGAITModelID.Text - 1).Item(1)) Then
                    txtAddEditGAITModel.Clear()
                Else
                    txtAddEditGAITModel.Text = dsGAITModel.Tables("GAITModelEdit").Rows(txtGAITModelID.Text - 1).Item(1)
                End If
                If IsDBNull(dsGAITModel.Tables("GAITModelEdit").Rows(txtGAITModelID.Text - 1).Item(2)) Then
                    GAITActive = "0"
                Else
                    GAITActive = dsGAITModel.Tables("GAITModelEdit").Rows(txtGAITModelID.Text - 1).Item(2)
                End If
                If GAITActive <> "1" Then
                    rdbGAITModelActive.Checked = False
                Else
                    rdbGAITModelActive.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGaitModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearGaitModel.Click
        Try
            txtGAITModelID.Clear()
            cboExistingGAITModel.SelectedValue = " "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddGAITModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddGAITModel.Click
        Try
            Dim GAITID As String = ""
            If IsDBNull(dsGAITModel.Tables("GAITModelEdit").Rows.Count) Then
                GAITID = "1"
            Else
                GAITID = dsGAITModel.Tables("GAITModelEdit").Rows.Count + 1
            End If

            If txtAddEditGAITModel.Text <> "" Then
                SQL = "Select " & _
                "numModelID " & _
                "from " & DBNameSpace & ".LookupGAITModel " & _
                "where Upper(strModelDesc) = '" & Replace(txtAddEditGAITModel.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("Model already exists in the database." & vbCrLf & _
                           "Use the Edit button.", _
                           MsgBoxStyle.Exclamation, "GAIT Inventory")
                    Exit Sub
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpGAITModel " & _
                    "values " & _
                    "((select max(numModelID) + 1 from " & DBNameSpace & ".LookUpGAITModel), " & _
                    "'" & txtAddEditGAITModel.Text & "', " & _
                    "'1') "

                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadGAITModelEdit()
                cboExistingGAITModel.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditGAITModel.Click
        Try
            Dim GAITID As String = ""
            If txtGAITModelID.Text <> "" And txtGAITModelID.Text <> " " Then
                GAITID = txtGAITModelID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitModel set " & _
                "strModelDESC = '" & Replace(txtAddEditGAITModel.Text, "'", "''") & "', " & _
                "Active = '1' " & _
                "where numModelID = '" & txtGAITModelID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITModelEdit()
                cboExistingGAITModel.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGAITModel.Click
        Try
            Dim GAITID As String = ""

            If txtGAITModelID.Text <> "" And txtGAITModelID.Text <> " " Then
                GAITID = txtGAITModelID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitModel set " & _
                "Active = '0' " & _
                "where numModelID = '" & txtGAITModelID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITModelEdit()
                cboExistingGAITModel.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadGAITModelNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadGAITModelNumber.Click
        Try
            If gbGAITModelNumber.Enabled = True Then
                gbGAITModel.Enabled = False
            Else
                gbGAITModelNumber.Enabled = True
                LoadGAITModelNumberEdit()
                txtGAITModelNumberID.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadGAITModelNumberEdit()
        Try
            Dim dtGAITModelNumber As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numModelNumberID, strModelNumberDesc, " & _
            "Active " & _
            "from " & DBNameSpace & ".LookUpGAITModelNumber " & _
            "order by numModelNumberID "

            dsGAITModelNumber = New DataSet
            daGAITModelNumber = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITModelNumber.Fill(dsGAITModelNumber, "GAITModelNumberEdit")

            dtGAITModelNumber.Columns.Add("numModelNumberID", GetType(System.String))
            dtGAITModelNumber.Columns.Add("strModelNumberDesc", GetType(System.String))

            drNewRow = dtGAITModelNumber.NewRow()
            drNewRow("numModelNumberID") = " "
            drNewRow("strModelNumberDesc") = " "
            dtGAITModelNumber.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows()
                drNewRow = dtGAITModelNumber.NewRow()
                drNewRow("numModelNumberID") = drDSRow("numModelNumberID")
                drNewRow("strModelNumberDesc") = drDSRow("strModelNumberDesc")
                dtGAITModelNumber.Rows.Add(drNewRow)
            Next

            With cboExistingGAITModelNumber
                .DataSource = dtGAITModelNumber
                .DisplayMember = "strModelNumberDesc"
                .ValueMember = "numModelNumberID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingGAITModelNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingGAITModelNumber.TextChanged
        Try
            Dim GAITID As Integer
            Dim GAITActive As String

            txtAddEditGAITModelNumber.Clear()
            rdbGAITModelNumberActive.Checked = False

            txtGAITModelNumberID.Text = cboExistingGAITModelNumber.SelectedValue.ToString
            If txtGAITModelNumberID.Text <> "" And txtGAITModelNumberID.Text <> " " And txtGAITModelNumberID.Text <> "System.Data.DataRowView" Then
                GAITID = txtGAITModelNumberID.Text
                If IsDBNull(dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows(txtGAITModelNumberID.Text - 1).Item(1)) Then
                    txtAddEditGAITModelNumber.Clear()
                Else
                    txtAddEditGAITModelNumber.Text = dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows(txtGAITModelNumberID.Text - 1).Item(1)
                End If
                If IsDBNull(dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows(txtGAITModelNumberID.Text - 1).Item(2)) Then
                    GAITActive = "0"
                Else
                    GAITActive = dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows(txtGAITModelNumberID.Text - 1).Item(2)
                End If
                If GAITActive <> "1" Then
                    rdbGAITModelNumberActive.Checked = False
                Else
                    rdbGAITModelNumberActive.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGaitModelNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearGaitModelNumber.Click
        Try
            txtGAITModelNumberID.Clear()
            cboExistingGAITModelNumber.SelectedValue = " "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddGAITModelNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddGAITModelNumber.Click
        Try
            Dim GAITID As String = ""
            If IsDBNull(dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows.Count) Then
                GAITID = "1"
            Else
                GAITID = dsGAITModelNumber.Tables("GAITModelNumberEdit").Rows.Count + 1
            End If

            If txtAddEditGAITModelNumber.Text <> "" Then
                SQL = "Select " & _
                "numModelNumberID " & _
                "from " & DBNameSpace & ".LookupGAITModelNumber " & _
                "where Upper(strModelNumberDesc) = '" & Replace(txtAddEditGAITModelNumber.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("ModelNumber already exists in the database." & vbCrLf & _
                           "Use the Edit button.", _
                           MsgBoxStyle.Exclamation, "GAIT Inventory")
                    Exit Sub
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpGAITModelNumber " & _
                    "values " & _
                    "((select max(numModelNumberID) + 1 from " & DBNameSpace & ".LookUpGAITModelNumber), " & _
                    "'" & txtAddEditGAITModelNumber.Text & "', " & _
                    "'1') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadGAITModelNumberEdit()
                cboExistingGAITModelNumber.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITModelNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditGAITModelNumber.Click
        Try
            Dim GAITID As String = ""
            If txtGAITModelNumberID.Text <> "" And txtGAITModelNumberID.Text <> " " Then
                GAITID = txtGAITModelNumberID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitModelNumber set " & _
                "strModelNumberDESC = '" & Replace(txtAddEditGAITModelNumber.Text, "'", "''") & "', " & _
                "Active = '1' " & _
                "where numModelNumberID = '" & txtGAITModelNumberID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITModelNumberEdit()
                cboExistingGAITModelNumber.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITModelNumber_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGAITModelNumber.Click
        Try
            Dim GAITID As String = ""

            If txtGAITModelNumberID.Text <> "" And txtGAITModelNumberID.Text <> " " Then
                GAITID = txtGAITModelNumberID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitModelNumber set " & _
                "Active = '0' " & _
                "where numModelNumberID = '" & txtGAITModelNumberID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITModelNumberEdit()
                cboExistingGAITModelNumber.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnLoadGAITQuality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadGAITQuality.Click
        Try

            If gbGAITQuality.Enabled = True Then
                gbGAITQuality.Enabled = False
            Else
                gbGAITQuality.Enabled = True
                LoadGAITQualityEdit()
                txtGAITQualityID.Clear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadGAITQualityEdit()
        Try
            Dim dtGAITQuality As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            SQL = "Select " & _
            "numQualityID, strQualityDesc, " & _
            "Active " & _
            "from " & DBNameSpace & ".LookUpGAITQuality " & _
            "order by numQualityID "

            dsGAITQuality = New DataSet
            daGAITQuality = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITQuality.Fill(dsGAITQuality, "GAITQualityEdit")

            dtGAITQuality.Columns.Add("numQualityID", GetType(System.String))
            dtGAITQuality.Columns.Add("strQualityDesc", GetType(System.String))

            drNewRow = dtGAITQuality.NewRow()
            drNewRow("numQualityID") = " "
            drNewRow("strQualityDesc") = " "
            dtGAITQuality.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITQuality.Tables("GAITQualityEdit").Rows()
                drNewRow = dtGAITQuality.NewRow()
                drNewRow("numQualityID") = drDSRow("numQualityID")
                drNewRow("strQualityDesc") = drDSRow("strQualityDesc")
                dtGAITQuality.Rows.Add(drNewRow)
            Next

            With cboExistingGAITQuality
                .DataSource = dtGAITQuality
                .DisplayMember = "strQualityDesc"
                .ValueMember = "numQualityID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub cboExistingGAITQuality_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExistingGAITQuality.TextChanged
        Try
            Dim GAITID As Integer
            Dim GAITActive As String

            txtAddEditGAITQuality.Clear()
            rdbGAITQualityActive.Checked = False

            txtGAITQualityID.Text = cboExistingGAITQuality.SelectedValue.ToString
            If txtGAITQualityID.Text <> "" And txtGAITQualityID.Text <> " " And txtGAITQualityID.Text <> "System.Data.DataRowView" Then
                GAITID = txtGAITQualityID.Text
                If IsDBNull(dsGAITQuality.Tables("GAITQualityEdit").Rows(txtGAITQualityID.Text - 1).Item(1)) Then
                    txtAddEditGAITQuality.Clear()
                Else
                    txtAddEditGAITQuality.Text = dsGAITQuality.Tables("GAITQualityEdit").Rows(txtGAITQualityID.Text - 1).Item(1)
                End If
                If IsDBNull(dsGAITQuality.Tables("GAITQualityEdit").Rows(txtGAITQualityID.Text - 1).Item(2)) Then
                    GAITActive = "0"
                Else
                    GAITActive = dsGAITQuality.Tables("GAITQualityEdit").Rows(txtGAITQualityID.Text - 1).Item(2)
                End If
                If GAITActive <> "1" Then
                    rdbGAITQualityActive.Checked = False
                Else
                    rdbGAITQualityActive.Checked = True
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGaitQuality_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearGaitQuality.Click
        Try
            txtGAITQualityID.Clear()
            cboExistingGAITQuality.SelectedValue = " "

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddGAITQuality_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddGAITQuality.Click
        Try
            Dim GAITID As String = ""
            If IsDBNull(dsGAITQuality.Tables("GAITQualityEdit").Rows.Count) Then
                GAITID = "1"
            Else
                GAITID = dsGAITQuality.Tables("GAITQualityEdit").Rows.Count + 1
            End If

            If txtAddEditGAITQuality.Text <> "" Then
                SQL = "Select " & _
                "numQualityID " & _
                "from " & DBNameSpace & ".LookupGAITQuality " & _
                "where Upper(strQualityDesc) = '" & Replace(txtAddEditGAITQuality.Text.ToUpper, "'", "''") & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("Quality already exists in the database." & vbCrLf & _
                           "Use the Edit button.", _
                           MsgBoxStyle.Exclamation, "GAIT Inventory")
                    Exit Sub
                Else
                    SQL = "Insert into " & DBNameSpace & ".LookUpGAITQuality " & _
                    "values " & _
                    "((select max(numQualityID) + 1 from " & DBNameSpace & ".LookUpGAITQuality), " & _
                    "'" & txtAddEditGAITQuality.Text & "', " & _
                    "'1') "
                    cmd = New OracleCommand(SQL, Conn)
                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                End If
                LoadGAITQualityEdit()
                cboExistingGAITQuality.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITQuality_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditGAITQuality.Click
        Try
            Dim GAITID As String = ""
            If txtGAITQualityID.Text <> "" And txtGAITQualityID.Text <> " " Then
                GAITID = txtGAITQualityID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitQuality set " & _
                "strQualityDESC = '" & Replace(txtAddEditGAITQuality.Text, "'", "''") & "', " & _
                "Active = '1' " & _
                "where numQualityID = '" & txtGAITQualityID.Text & "' "

                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITQualityEdit()
                cboExistingGAITQuality.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITQuality_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteGAITQuality.Click
        Try
            Dim GAITID As String = ""

            If txtGAITQualityID.Text <> "" And txtGAITQualityID.Text <> " " Then
                GAITID = txtGAITQualityID.Text

                SQL = "Update " & DBNameSpace & ".LookUpGaitQuality set " & _
                "Active = '0' " & _
                "where numQualityID = '" & txtGAITQualityID.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITQualityEdit()
                cboExistingGAITQuality.SelectedValue = GAITID
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    Sub LoadGAITLookUps()
        Try
            Dim dtGAITCategory As New DataTable
            Dim dtGAITCategory2 As New DataTable
            Dim dtGAITManufacturer As New DataTable
            Dim dtGAITModel As New DataTable
            Dim dtGAITModelNumber As New DataTable
            Dim dtGAITQuality As New DataTable
            Dim dtIAIPStaff As New DataTable
            Dim dtIAIPPrograms As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drDSRow3 As DataRow
            Dim drDSRow4 As DataRow
            Dim drDSRow5 As DataRow
            Dim drDSRow6 As DataRow
            Dim drDSRow7 As DataRow
            Dim drDSRow8 As DataRow
            Dim drNewRow As DataRow

            dsGAITLookUps = New DataSet

            SQL = "Select " & _
            "numCategoryID, strCategoryDesc " & _
            "from " & DBNameSpace & ".LookUpGAITCategory " & _
            "where Active = '1' " & _
            "order by strCategoryDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "GAITCategory")

            SQL = "Select " & _
            "numManufactureID, strManufactureDesc " & _
            "from " & DBNameSpace & ".LookUpGAITManufacture " & _
            "where Active = '1' " & _
            "order by strManufactureDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "GAITManufacture")

            SQL = "Select " & _
            "numModelID, strModelDesc " & _
            "from " & DBNameSpace & ".LookUpGAITModel " & _
            "where Active = '1' " & _
            "order by strModelDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "GAITModel")

            SQL = "Select " & _
            "numModelNumberID, strModelNumberDesc " & _
            "from " & DBNameSpace & ".LookUpGAITModelNumber " & _
            "where Active = '1' " & _
            "order by strModelNumberDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "GAITModelNumber")

            SQL = "Select " & _
            "numQualityID, strQualityDesc " & _
           "from " & DBNameSpace & ".LookUpGAITQuality " & _
            "order by strQualityDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "GAITQuality")

            'SQL = "select  " & _
            '"numUserID,  " & _
            '"(strLastName||', '||strFirstname) as IAIPUser " & _
            '"from " & DBNameSpace & ".EPDUSerProfiles  " & _
            '"where numBranch = '1' " & _
            '"order by strLastName "

            SQL = "select * " & _
            "from " & _
            "(select  " & _
            "numUserID,  " & _
            "(strLastName||', '||strFirstname) as IAIPUser " & _
            "from " & DBNameSpace & ".EPDUSerProfiles  " & _
            "where numbranch = '1' " & _
            "union " & _
            "select " & _
            "distinct " & _
            "" & DBNameSpace & ".GAITInventory.numUserID, " & _
            "(strLastName||', '||strFirstName) as IAIPUSER " & _
            "from " & DBNameSpace & ".GAITInventory, " & DBNameSpace & ".EPDUserProfiles " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numUserID = " & DBNameSpace & ".GAITInventory.numUserID)  " & _
            "order by IAIPUser "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "IAIPStaff")

            SQL = "Select " & _
            "numProgramCode, strProgramDesc " & _
            "from " & DBNameSpace & ".LookUpEPDPrograms " & _
            "where numBranchCode = '1' " & _
            "order by strProgramDesc "

            daGAITLookUps = New OracleDataAdapter(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            daGAITLookUps.Fill(dsGAITLookUps, "IAIPPrograms")


            dtGAITCategory.Columns.Add("numCategoryID", GetType(System.String))
            dtGAITCategory.Columns.Add("strCategoryDesc", GetType(System.String))

            drNewRow = dtGAITCategory.NewRow()
            drNewRow("numCategoryID") = " "
            drNewRow("strCategoryDesc") = " "
            dtGAITCategory.Rows.Add(drNewRow)

            For Each drDSRow In dsGAITLookUps.Tables("GAITCategory").Rows()
                drNewRow = dtGAITCategory.NewRow()
                drNewRow("numCategoryID") = drDSRow("numCategoryID")
                drNewRow("strCategoryDesc") = drDSRow("strCategoryDesc")
                dtGAITCategory.Rows.Add(drNewRow)
            Next

            With cboGAITCategory
                .DataSource = dtGAITCategory
                .DisplayMember = "strCategoryDesc"
                .ValueMember = "numCategoryID"
                .SelectedIndex = 0
            End With


            dtGAITCategory2.Columns.Add("numCategoryID", GetType(System.String))
            dtGAITCategory2.Columns.Add("strCategoryDesc", GetType(System.String))

            'drNewRow = dtGAITCategory2.NewRow()
            'drNewRow("numCategoryID") = " "
            'drNewRow("strCategoryDesc") = " "
            'dtGAITCategory2.Rows.Add(drNewRow)

            For Each drDSRow7 In dsGAITLookUps.Tables("GAITCategory").Rows()
                drNewRow = dtGAITCategory2.NewRow()
                drNewRow("numCategoryID") = drDSRow7("numCategoryID")
                drNewRow("strCategoryDesc") = drDSRow7("strCategoryDesc")
                dtGAITCategory2.Rows.Add(drNewRow)
            Next

            With clbAssestCategory
                .DataSource = dtGAITCategory2
                .DisplayMember = "strCategoryDesc"
                .ValueMember = "numCategoryID"
                .SelectedIndex = 0
            End With

            dtGAITManufacturer.Columns.Add("numManufactureID", GetType(System.String))
            dtGAITManufacturer.Columns.Add("strManufactureDesc", GetType(System.String))

            drNewRow = dtGAITManufacturer.NewRow()
            drNewRow("numManufactureID") = " "
            drNewRow("strManufactureDesc") = " "
            dtGAITManufacturer.Rows.Add(drNewRow)

            For Each drDSRow2 In dsGAITLookUps.Tables("GAITManufacture").Rows()
                drNewRow = dtGAITManufacturer.NewRow()
                drNewRow("numManufactureID") = drDSRow2("numManufactureID")
                drNewRow("strManufactureDesc") = drDSRow2("strManufactureDesc")
                dtGAITManufacturer.Rows.Add(drNewRow)
            Next

            With cboGAITManufacturer
                .DataSource = dtGAITManufacturer
                .DisplayMember = "strManufactureDesc"
                .ValueMember = "numManufactureID"
                .SelectedIndex = 0
            End With




            dtGAITModel.Columns.Add("numModelID", GetType(System.String))
            dtGAITModel.Columns.Add("strModelDesc", GetType(System.String))

            drNewRow = dtGAITModel.NewRow()
            drNewRow("numModelID") = " "
            drNewRow("strModelDesc") = " "
            dtGAITModel.Rows.Add(drNewRow)

            For Each drDSRow3 In dsGAITLookUps.Tables("GAITModel").Rows()
                drNewRow = dtGAITModel.NewRow()
                drNewRow("numModelID") = drDSRow3("numModelID")
                drNewRow("strModelDesc") = drDSRow3("strModelDesc")
                dtGAITModel.Rows.Add(drNewRow)
            Next

            With cboGAITModel
                .DataSource = dtGAITModel
                .DisplayMember = "strModelDesc"
                .ValueMember = "numModelID"
                .SelectedIndex = 0
            End With



            dtGAITModelNumber.Columns.Add("numModelNumberID", GetType(System.String))
            dtGAITModelNumber.Columns.Add("strModelNumberDesc", GetType(System.String))

            drNewRow = dtGAITModelNumber.NewRow()
            drNewRow("numModelNumberID") = " "
            drNewRow("strModelNumberDesc") = " "
            dtGAITModelNumber.Rows.Add(drNewRow)

            For Each drDSRow4 In dsGAITLookUps.Tables("GAITModelNumber").Rows()
                drNewRow = dtGAITModelNumber.NewRow()
                drNewRow("numModelNumberID") = drDSRow4("numModelNumberID")
                drNewRow("strModelNumberDesc") = drDSRow4("strModelNumberDesc")
                dtGAITModelNumber.Rows.Add(drNewRow)
            Next

            With cboGAITModelNumber
                .DataSource = dtGAITModelNumber
                .DisplayMember = "strModelNumberDesc"
                .ValueMember = "numModelNumberID"
                .SelectedIndex = 0
            End With

            dtGAITQuality.Columns.Add("numQualityID", GetType(System.String))
            dtGAITQuality.Columns.Add("strQualityDesc", GetType(System.String))

            drNewRow = dtGAITQuality.NewRow()
            drNewRow("numQualityID") = " "
            drNewRow("strQualityDesc") = " "
            dtGAITQuality.Rows.Add(drNewRow)

            For Each drDSRow5 In dsGAITLookUps.Tables("GAITQuality").Rows()
                drNewRow = dtGAITQuality.NewRow()
                drNewRow("numQualityID") = drDSRow5("numQualityID")
                drNewRow("strQualityDesc") = drDSRow5("strQualityDesc")
                dtGAITQuality.Rows.Add(drNewRow)
            Next

            With cboGAITQuality
                .DataSource = dtGAITQuality
                .DisplayMember = "strQualityDesc"
                .ValueMember = "numQualityID"
                .SelectedIndex = 0
            End With

            dtIAIPStaff.Columns.Add("numUserID", GetType(System.String))
            dtIAIPStaff.Columns.Add("IAIPUser", GetType(System.String))

            drNewRow = dtIAIPStaff.NewRow()
            drNewRow("numUserID") = " "
            drNewRow("IAIPUser") = " "
            dtIAIPStaff.Rows.Add(drNewRow)

            For Each drDSRow6 In dsGAITLookUps.Tables("IAIPStaff").Rows()
                drNewRow = dtIAIPStaff.NewRow()
                drNewRow("numUserID") = drDSRow6("numUserID")
                drNewRow("IAIPUser") = drDSRow6("IAIPUser")
                dtIAIPStaff.Rows.Add(drNewRow)
            Next

            With cboGAITIAIPUser
                .DataSource = dtIAIPStaff
                .DisplayMember = "IAIPUser"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With



            dtIAIPPrograms.Columns.Add("numProgramCode", GetType(System.String))
            dtIAIPPrograms.Columns.Add("strProgramDesc", GetType(System.String))

            drNewRow = dtIAIPPrograms.NewRow()
            drNewRow("numProgramCode") = " "
            drNewRow("strProgramDesc") = " "
            dtIAIPPrograms.Rows.Add(drNewRow)

            For Each drDSRow8 In dsGAITLookUps.Tables("IAIPPrograms").Rows()
                drNewRow = dtIAIPPrograms.NewRow()
                drNewRow("numProgramCode") = drDSRow8("numProgramCode")
                drNewRow("strProgramDesc") = drDSRow8("strProgramDesc")
                dtIAIPPrograms.Rows.Add(drNewRow)
            Next

            With cboGAITProgram
                .DataSource = dtIAIPPrograms
                .DisplayMember = "strProgramDesc"
                .ValueMember = "numProgramCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadGAITInventory()
        Try
            Dim ActiveStatus As String = ""
            Dim AssetTag As String = ""
            Dim APBProgram As String = ""
            Dim AssetCategory As String = ""
            Dim GAITDates As String = ""
            Dim GAITComments As String = ""

            If rdbGAITActive.Checked = True Then
                ActiveStatus = "1"
            End If
            If rdbGAITInactive.Checked = True Then
                ActiveStatus = "0"
            End If
            If rdbGAITBoth.Checked = True Then
                ActiveStatus = "%"
            End If
            If txtGAITAssetSearch.Text <> "" Then
                AssetTag = " and " & DBNameSpace & ".GAITInventory.strAssetTag like '%" & txtGAITAssetSearch.Text & "%' "
            Else
                AssetTag = " "
            End If
            If cboGAITProgram.SelectedValue <> "" And cboGAITProgram.SelectedValue <> " " _
                      And cboGAITProgram.SelectedValue <> "System.Data.DataRowView" Then
                APBProgram = " and numProgram = '" & cboGAITProgram.SelectedValue & "' "
            Else
                APBProgram = " "
            End If
            If txtGAITCommentSearch.Text <> "" Then
                GAITComments = " and upper(" & DBNameSpace & ".GAITInventory.strComments) like '%" & txtGAITCommentSearch.Text.ToUpper & "%' "
            Else
                GAITComments = " "
            End If

            AssetCategory = ""
            For i As Integer = 0 To clbAssestCategory.Items.Count - 1
                Dim chkstate As CheckState
                chkstate = clbAssestCategory.GetItemCheckState(i)
                If (chkstate = CheckState.Checked) Then
                    clbAssestCategory.SetSelected(i, True)
                    AssetCategory = AssetCategory & " numCategoryID = '" & clbAssestCategory.SelectedValue.ToString & "' or "
                End If
            Next
            If AssetCategory <> "" Then
                AssetCategory = Mid(AssetCategory, 1, AssetCategory.Length - 3)
                AssetCategory = " and ( " & AssetCategory & " ) "
            End If

            GAITDates = " "
            If rdbGAITDatePurchased.Checked = True Then
                GAITDates = " and datPurchased between '" & dtpGAITDateStart.Text & "' and '" & dtpGAITDateEnd.Text & "' "
            End If
            If rdbGAITDateAquired.Checked = True Then
                GAITDates = " and datAquired between '" & dtpGAITDateStart.Text & "' and '" & dtpGAITDateEnd.Text & "' "
            End If

            'SQL = "select " & _
            '"numGAITID, strAssetTag, " & _
            '"strCategoryDesc, " & _
            '"strManufactureDesc, " & _
            '"strModelDesc, " & _
            '"strModelNumberDesc, " & _
            '"strSerial, " & _
            '"strQualityDesc, " & _
            '"strPurchaseOrder, " & _
            '"datPurchased, " & _
            '"datAquired, " & _
            '"case " & _
            '"when " & DBNameSpace & ".GAITInventory.numUserID is null then '' " & _
            '"when " & DBNameSpace & ".GAITInventory.numUserID is not null then  (strLastName||', '||strFirstName) " & _
            '"end IAIPStaff, " & _
            '"strProgramDesc, strUnitDesc, " & _
            '"to_number(to_char(abs(trunc(months_between(datAquired, sysdate)/12, 1))))  as TimeInService, " & _
            '"strComments " & _
            '"from " & DBNameSpace & ".GAITInventory, " & _
            '"" & DBNameSpace & ".LookupGAITCategory, " & _
            '"" & DBNameSpace & ".LookUpGAITManufacture, " & _
            '"" & DBNameSpace & ".LookupGAITModel, " & _
            '"" & DBNameSpace & ".LookUpGAITModelNumber, " & _
            '"" & DBNameSpace & ".LookUpGAITQuality, " & _
            '"" & DBNameSpace & ".EPDUSERProfiles, " & _
            '"" & DBNameSpace & ".LookUpEPDPrograms, " & _
            '"" & DBNameSpace & ".LookUpEPDUnits " & _
            '"where " & DBNameSpace & ".GAITInventory.numCategory = " & DBNameSpace & ".LookupGAITCategory.numCategoryID (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.numManufacturer = " & DBNameSpace & ".LookUpGAITManufacture.numManufactureID  (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.numModel = " & DBNameSpace & ".LookUpGAITModel.numModelID   (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.numModelNumber = " & DBNameSpace & ".LookUpGAITModelNumber.numModelNumberID (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.numQuality = " & DBNameSpace & ".LookUpGAITQuality.numQualityID  (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.numUserID = " & DBNameSpace & ".EPDUserProfiles.numUserID  (+) " & _
            '"and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            '"and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            '"and " & DBNameSpace & ".GAITInventory.ACTIVE like '" & ActiveStatus & "' " & _
            'AssetTag & APBProgram & AssetCategory & GAITDates & GAITComments & _
            '"order by numGAITID desc "



            SQL = "select " & _
            "numGAITID, strAssetTag, " & _
            "strCategoryDesc, " & _
            "datAquired, " & _
            "strComments, " & _
            "case " & _
            "when " & DBNameSpace & ".GAITInventory.numUserID is null then '' " & _
            "when " & DBNameSpace & ".GAITInventory.numUserID is not null then  (strLastName||', '||strFirstName) " & _
            "end IAIPStaff, strOffice, " & _
            "strProgramDesc, strUnitDesc, " & _
            "to_number(to_char(abs(trunc(months_between(datAquired, sysdate)/12, 1))))  as TimeInService, " & _
            "strManufactureDesc, " & _
            "strModelDesc, " & _
            "strModelNumberDesc, " & _
            "strSerial, " & _
            "strQualityDesc, " & _
            "strPurchaseOrder, " & _
            "datPurchased, " & _
            "case " & _
            "when GAITInventory.Active = '1' then 'Active' " & _
            "when GAITInventory.Active = '0' then 'Deleted' " & _
            "End Active " & _
            "from " & DBNameSpace & ".GAITInventory, " & _
            "" & DBNameSpace & ".LookupGAITCategory, " & _
            "" & DBNameSpace & ".LookUpGAITManufacture, " & _
            "" & DBNameSpace & ".LookupGAITModel, " & _
            "" & DBNameSpace & ".LookUpGAITModelNumber, " & _
            "" & DBNameSpace & ".LookUpGAITQuality, " & _
            "" & DBNameSpace & ".EPDUSERProfiles, " & _
            "" & DBNameSpace & ".LookUpEPDPrograms, " & _
            "" & DBNameSpace & ".LookUpEPDUnits " & _
            "where " & DBNameSpace & ".GAITInventory.numCategory = " & DBNameSpace & ".LookupGAITCategory.numCategoryID (+) " & _
            "and " & DBNameSpace & ".GAITInventory.numManufacturer = " & DBNameSpace & ".LookUpGAITManufacture.numManufactureID  (+) " & _
            "and " & DBNameSpace & ".GAITInventory.numModel = " & DBNameSpace & ".LookUpGAITModel.numModelID   (+) " & _
            "and " & DBNameSpace & ".GAITInventory.numModelNumber = " & DBNameSpace & ".LookUpGAITModelNumber.numModelNumberID (+) " & _
            "and " & DBNameSpace & ".GAITInventory.numQuality = " & DBNameSpace & ".LookUpGAITQuality.numQualityID  (+) " & _
            "and " & DBNameSpace & ".GAITInventory.numUserID = " & DBNameSpace & ".EPDUserProfiles.numUserID  (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode (+) " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode (+) " & _
            "and " & DBNameSpace & ".GAITInventory.ACTIVE like '" & ActiveStatus & "' " & _
            AssetTag & APBProgram & AssetCategory & GAITDates & GAITComments & _
            "order by numGAITID desc "

            ds = New DataSet
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            da = New OracleDataAdapter(SQL, Conn)
            da.Fill(ds, "GAITInventory")

            dgvGAITInventory.DataSource = ds
            dgvGAITInventory.DataMember = "GAITInventory"

            dgvGAITInventory.RowHeadersVisible = False
            dgvGAITInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvGAITInventory.AllowUserToResizeColumns = True
            dgvGAITInventory.AllowUserToAddRows = False
            dgvGAITInventory.AllowUserToDeleteRows = False
            dgvGAITInventory.AllowUserToOrderColumns = True
            dgvGAITInventory.AllowUserToResizeRows = True
            dgvGAITInventory.ColumnHeadersHeight = "35"
            dgvGAITInventory.Columns("numGAITID").HeaderText = "ID"
            dgvGAITInventory.Columns("numGAITID").DisplayIndex = 0
            dgvGAITInventory.Columns("numGAITID").Visible = False
            dgvGAITInventory.Columns("strAssetTag").HeaderText = "Asset Tag"
            dgvGAITInventory.Columns("strAssetTag").DisplayIndex = 1
            dgvGAITInventory.Columns("strCategoryDesc").HeaderText = "Category"
            dgvGAITInventory.Columns("strCategoryDesc").DisplayIndex = 2
            dgvGAITInventory.Columns("datAquired").HeaderText = "Date Aquired"
            dgvGAITInventory.Columns("datAquired").DisplayIndex = 3
            dgvGAITInventory.Columns("datAquired").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvGAITInventory.Columns("strComments").HeaderText = "Comments"
            dgvGAITInventory.Columns("strComments").DisplayIndex = 4
            dgvGAITInventory.Columns("IAIPStaff").HeaderText = "IAIP Staff"
            dgvGAITInventory.Columns("IAIPStaff").DisplayIndex = 5
            dgvGAITInventory.Columns("strOffice").HeaderText = "Office #"
            dgvGAITInventory.Columns("strOffice").DisplayIndex = 6
            dgvGAITInventory.Columns("strProgramDesc").HeaderText = "Program"
            dgvGAITInventory.Columns("strProgramDesc").DisplayIndex = 7
            dgvGAITInventory.Columns("strUnitDesc").HeaderText = "Unit"
            dgvGAITInventory.Columns("strUnitDesc").DisplayIndex = 8
            dgvGAITInventory.Columns("TimeInService").HeaderText = "Time In Service(Years)"
            dgvGAITInventory.Columns("TimeInService").DisplayIndex = 9
            dgvGAITInventory.Columns("TimeInService").DefaultCellStyle.Format = "G"
            dgvGAITInventory.Columns("strManufactureDesc").HeaderText = "Manufacturer"
            dgvGAITInventory.Columns("strManufactureDesc").DisplayIndex = 10
            dgvGAITInventory.Columns("strModelDesc").HeaderText = "Model"
            dgvGAITInventory.Columns("strModelDesc").DisplayIndex = 11
            dgvGAITInventory.Columns("strModelNumberDesc").HeaderText = "Model No."
            dgvGAITInventory.Columns("strModelNumberDesc").DisplayIndex = 12
            dgvGAITInventory.Columns("strSerial").HeaderText = "Serial"
            dgvGAITInventory.Columns("strSerial").DisplayIndex = 13
            dgvGAITInventory.Columns("strQualityDesc").HeaderText = "Quality"
            dgvGAITInventory.Columns("strQualityDesc").DisplayIndex = 14
            dgvGAITInventory.Columns("strPurchaseOrder").HeaderText = "Purchase Order"
            dgvGAITInventory.Columns("strPurchaseOrder").DisplayIndex = 15
            dgvGAITInventory.Columns("datPurchased").HeaderText = "Date Purchased"
            dgvGAITInventory.Columns("datPurchased").DisplayIndex = 16
            dgvGAITInventory.Columns("datPurchased").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvGAITInventory.Columns("Active").DisplayIndex = 17
            dgvGAITInventory.Columns("Active").DefaultCellStyle.Format = "Active Status"

            txtGAITCount.Text = dgvGAITInventory.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvGAITInventory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvGAITInventory.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvGAITInventory.HitTest(e.X, e.Y)

            If hti.Type = DataGrid.HitTestType.Cell Then
                If dgvGAITInventory.RowCount > 0 And hti.RowIndex <> -1 Then
                    If dgvGAITInventory.Columns(0).HeaderText = "ID" Then
                        txtGAITID.Text = dgvGAITInventory(0, hti.RowIndex).Value
                        If IsDBNull(dgvGAITInventory(1, hti.RowIndex).Value) Then
                            txtGAITAssetTag.Clear()
                        Else
                            txtGAITAssetTag.Text = dgvGAITInventory(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(2, hti.RowIndex).Value) Then
                            cboGAITCategory.SelectedIndex = 0
                        Else
                            cboGAITCategory.Text = dgvGAITInventory(2, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(3, hti.RowIndex).Value) Then
                            dtpGAITAquired.Text = OracleDate
                        Else
                            temp = dgvGAITInventory(3, hti.RowIndex).Value
                            dtpGAITAquired.Text = dgvGAITInventory(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(4, hti.RowIndex).Value) Then
                            txtGAITComment.Clear()
                        Else
                            txtGAITComment.Text = dgvGAITInventory(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(5, hti.RowIndex).Value) Then
                            cboGAITIAIPUser.SelectedIndex = 0
                        Else
                            cboGAITIAIPUser.Text = dgvGAITInventory(5, hti.RowIndex).Value
                        End If

                        If IsDBNull(dgvGAITInventory(10, hti.RowIndex).Value) Then
                            cboGAITManufacturer.SelectedIndex = 0
                        Else
                            cboGAITManufacturer.Text = dgvGAITInventory(10, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(11, hti.RowIndex).Value) Then
                            cboGAITModel.SelectedIndex = 0
                        Else
                            cboGAITModel.Text = dgvGAITInventory(11, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(12, hti.RowIndex).Value) Then
                            cboGAITModelNumber.SelectedIndex = 0
                        Else
                            cboGAITModelNumber.Text = dgvGAITInventory(12, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(13, hti.RowIndex).Value) Then
                            txtGAITSerial.Clear()
                        Else
                            txtGAITSerial.Text = dgvGAITInventory(13, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(14, hti.RowIndex).Value) Then
                            cboGAITQuality.SelectedIndex = 0
                        Else
                            cboGAITQuality.Text = dgvGAITInventory(14, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(15, hti.RowIndex).Value) Then
                            txtGAITPurchaseOrder.Clear()
                        Else
                            txtGAITPurchaseOrder.Text = dgvGAITInventory(15, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(16, hti.RowIndex).Value) Then
                            dtpGAITPurchased.Text = OracleDate
                        Else
                            temp = dgvGAITInventory(16, hti.RowIndex).Value
                            dtpGAITPurchased.Text = dgvGAITInventory(16, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvGAITInventory(17, hti.RowIndex).Value) Then
                            temp = "1"
                        Else
                            temp = dgvGAITInventory(17, hti.RowIndex).Value
                        End If
                        If temp = "0" Then
                            rdbDeleted.Checked = True
                        Else
                            rdbActive.Checked = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSaveGAITInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewGAITAsset.Click
        Try
            Dim AssetTag As String = ""
            Dim Category As String = ""
            Dim Manufacturer As String = ""
            Dim Model As String = ""
            Dim ModelNumber As String = ""
            Dim Serial As String = ""
            Dim Quality As String = ""
            Dim PurchaseOrder As String = ""
            Dim DatePurchased As String = ""
            Dim DateAquired As String = ""
            Dim IAIPStaff As String = ""
            Dim Comments As String = ""
            Dim ActiveStatus As String = ""

            If txtGAITAssetTag.Text <> "" Then
                SQL = "Select " & _
                "numGAITID " & _
                "from " & DBNameSpace & ".GAITInventory " & _
                "where strAssetTag = '" & txtGAITAssetTag.Text & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("There is already an Asset with this Asset Tag." & vbCrLf & _
                           "You cannot dublicate Asset Tags in the inventory.", MsgBoxStyle.Information, "GAIT Inventory")
                    Exit Sub
                End If
            End If
            If txtGAITAssetTag.Text <> "" Then
                AssetTag = txtGAITAssetTag.Text
            End If
            If cboGAITCategory.SelectedValue <> "" And cboGAITCategory.SelectedValue <> " " _
                      And cboGAITCategory.SelectedValue <> "System.Data.DataRowView" Then
                Category = cboGAITCategory.SelectedValue
            End If
            If cboGAITManufacturer.SelectedValue <> "" And cboGAITManufacturer.SelectedValue <> " " _
                      And cboGAITManufacturer.SelectedValue <> "System.Data.DataRowView" Then
                Manufacturer = cboGAITManufacturer.SelectedValue
            End If
            If cboGAITModel.SelectedValue <> "" And cboGAITModel.SelectedValue <> " " _
                      And cboGAITModel.SelectedValue <> "System.Data.DataRowView" Then
                Model = cboGAITModel.SelectedValue
            End If
            If cboGAITModelNumber.SelectedValue <> "" And cboGAITModelNumber.SelectedValue <> " " _
                     And cboGAITModelNumber.SelectedValue <> "System.Data.DataRowView" Then
                ModelNumber = cboGAITModelNumber.SelectedValue
            End If
            If txtGAITSerial.Text <> "" Then
                Serial = txtGAITSerial.Text
            End If
            If cboGAITQuality.SelectedValue <> "" And cboGAITQuality.SelectedValue <> " " _
                     And cboGAITQuality.SelectedValue <> "System.Data.DataRowView" Then
                Quality = cboGAITQuality.SelectedValue
            End If
            If txtGAITPurchaseOrder.Text <> "" Then
                PurchaseOrder = txtGAITPurchaseOrder.Text
            End If
            DatePurchased = dtpGAITPurchased.Text
            DateAquired = dtpGAITAquired.Text

            If DateAquired > DatePurchased Then
                DatePurchased = DateAquired
            End If

            If cboGAITIAIPUser.SelectedValue <> "" And cboGAITIAIPUser.SelectedValue <> " " _
                     And cboGAITIAIPUser.SelectedValue <> "System.Data.DataRowView" Then
                IAIPStaff = cboGAITIAIPUser.SelectedValue
            End If
            If txtGAITComment.Text <> "" Then
                Comments = txtGAITComment.Text
            End If
            If rdbDeleted.Checked = True Then
                ActiveStatus = "0"
            Else
                ActiveStatus = "1"
            End If

            SQL = "Insert into " & DBNameSpace & ".GAITInventory " & _
            "values " & _
            "((Select max(numGAITid) + 1 from " & DBNameSpace & ".GAITInventory), " & _
            "'" & Replace(AssetTag, "'", "''") & "', '" & Replace(Category, "'", "''") & "', " & _
            "'" & Replace(Manufacturer, "'", "''") & "', '" & Replace(Model, "'", "''") & "', " & _
            "'" & Replace(ModelNumber, "'", "''") & "', '" & Serial & "', " & _
            "'" & Quality & "', '" & Replace(PurchaseOrder, "'", "''") & "', " & _
            "'" & DatePurchased & "', '" & DateAquired & "', " & _
            "'" & IAIPStaff & "', '" & UserGCode & "', " & _
            "'" & OracleDate & "', '" & ActiveStatus & "', '" & Replace(Comments, "'", "''") & "') "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadGAITInventory()

            txtGAITAssetTag.Clear()
            txtGAITSerial.Clear()
            cboGAITIAIPUser.Text = ""

            MsgBox("Asset Added to Inventory.", MsgBoxStyle.Information, "GAIT Inventory")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnClearGAIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearGAIT.Click
        Try
            ClearGAITInventory()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnEditGAITInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGAITAsset.Click
        Try
            Dim GAITID As String = ""
            Dim AssetTag As String = ""
            Dim Category As String = ""
            Dim Manufacturer As String = ""
            Dim Model As String = ""
            Dim ModelNumber As String = ""
            Dim Serial As String = ""
            Dim Quality As String = ""
            Dim PurchaseOrder As String = ""
            Dim DatePurchased As String = ""
            Dim DateAquired As String = ""
            Dim IAIPStaff As String = ""
            Dim Comments As String = ""
            Dim ActiveStatus As String = ""

            If txtGAITID.Text <> "" Then
                GAITID = txtGAITID.Text
            Else
                MsgBox("Please use Save Asset for new assets.", MsgBoxStyle.Information, "GAIT Inventory")
                Exit Sub
            End If
            If txtGAITAssetTag.Text <> "" Then
                AssetTag = txtGAITAssetTag.Text
            End If
            If cboGAITCategory.SelectedValue <> "" And cboGAITCategory.SelectedValue <> " " _
                      And cboGAITCategory.SelectedValue <> "System.Data.DataRowView" Then
                Category = cboGAITCategory.SelectedValue
            End If
            If cboGAITManufacturer.SelectedValue <> "" And cboGAITManufacturer.SelectedValue <> " " _
                      And cboGAITManufacturer.SelectedValue <> "System.Data.DataRowView" Then
                Manufacturer = cboGAITManufacturer.SelectedValue
            End If
            If cboGAITModel.SelectedValue <> "" And cboGAITModel.SelectedValue <> " " _
                      And cboGAITModel.SelectedValue <> "System.Data.DataRowView" Then
                Model = cboGAITModel.SelectedValue
            End If
            If cboGAITModelNumber.SelectedValue <> "" And cboGAITModelNumber.SelectedValue <> " " _
                     And cboGAITModelNumber.SelectedValue <> "System.Data.DataRowView" Then
                ModelNumber = cboGAITModelNumber.SelectedValue
            End If
            If txtGAITSerial.Text <> "" Then
                Serial = txtGAITSerial.Text
            End If
            If cboGAITQuality.SelectedValue <> "" And cboGAITQuality.SelectedValue <> " " _
                     And cboGAITQuality.SelectedValue <> "System.Data.DataRowView" Then
                Quality = cboGAITQuality.SelectedValue
            End If
            If txtGAITPurchaseOrder.Text <> "" Then
                PurchaseOrder = txtGAITPurchaseOrder.Text
            End If
            DatePurchased = dtpGAITPurchased.Text
            DateAquired = dtpGAITAquired.Text
            If DateAquired < DatePurchased Then
                DatePurchased = DateAquired
            End If
            If cboGAITIAIPUser.SelectedValue <> "" And cboGAITIAIPUser.SelectedValue <> " " _
                     And cboGAITIAIPUser.SelectedValue <> "System.Data.DataRowView" Then
                IAIPStaff = cboGAITIAIPUser.SelectedValue
            End If
            If txtGAITComment.Text <> "" Then
                Comments = txtGAITComment.Text
            Else
                Comments = ""
            End If
            If rdbDeleted.Checked = True Then
                ActiveStatus = "0"
            Else
                ActiveStatus = "1"
            End If

            SQL = "Select numGAITID " & _
            "from " & DBNameSpace & ".GAITInventory " & _
            "where nuMGAITID = '" & GAITID & "' "
            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()

            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".GAITInventory set " & _
                "strAssetTag = '" & Replace(AssetTag, "'", "''") & "', " & _
                "numCategory = '" & Category & "', " & _
                "numManufacturer = '" & Manufacturer & "', " & _
                "numModel = '" & Model & "', " & _
                "numModelNumber = '" & ModelNumber & "', " & _
                "strSerial = '" & Replace(Serial, "'", "''") & "', " & _
                "numQuality = '" & Quality & "', " & _
                "strPurchaseOrder = '" & Replace(PurchaseOrder, "'", "''") & "', " & _
                "datPurchased = '" & DatePurchased & "', " & _
                "datAquired = '" & DateAquired & "', " & _
                "numUserID = '" & IAIPStaff & "', " & _
                "strModifingPerson = '" & UserGCode & "', " & _
                "datModifingDate = '" & OracleDate & "', " & _
                "Active = '" & ActiveStatus & "', " & _
                "strComments = '" & Replace(Comments, "'", "''") & "' " & _
                "where numGAITid = '" & GAITID & "' "
                cmd = New OracleCommand(SQL, Conn)
                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                LoadGAITInventory()

                MsgBox("Asset Saved to Inventory.", MsgBoxStyle.Information, "GAIT Inventory")
            Else
                txtGAITID.Clear()
                MsgBox("Please Save Asset.", MsgBoxStyle.Information, "GAIT Inventory")
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub GAITYearsInService()
        Try
            Dim tsTimeSpan As TimeSpan
            Dim iNumberOfDays As Decimal

            tsTimeSpan = Now.Subtract(dtpGAITAquired.Text)
            iNumberOfDays = (tsTimeSpan.Days / 365)
            iNumberOfDays = Decimal.Round(iNumberOfDays, 1)

            txtGAITTimeInService.Text = iNumberOfDays.ToString
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub ClearGAITInventory()
        Try
            txtGAITID.Clear()
            txtGAITAssetTag.Clear()
            cboGAITCategory.SelectedIndex = 0
            cboGAITManufacturer.SelectedIndex = 0
            cboGAITModel.SelectedIndex = 0
            cboGAITModelNumber.SelectedIndex = 0
            txtGAITSerial.Clear()
            cboGAITQuality.SelectedIndex = 0
            txtGAITPurchaseOrder.Clear()
            dtpGAITPurchased.Text = OracleDate
            dtpGAITAquired.Text = OracleDate
            cboGAITIAIPUser.SelectedIndex = 0
            rdbActive.Checked = False
            rdbDeleted.Checked = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteGAITInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteGAITAsset.Click
        Try
            Dim ActiveStatus As String = "1"

            If rdbDeleted.Checked = True Then
                ActiveStatus = "0"
            Else
                ActiveStatus = "1"
            End If

            SQL = "Update " & DBNameSpace & ".GAITInventory set " & _
            "Active = '" & ActiveStatus & "' " & _
            "where numGAITID = '" & txtGAITID.Text & "' "

            cmd = New OracleCommand(SQL, Conn)
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            LoadGAITInventory()
            ClearGAITInventory()
            MsgBox("Asset Removed from Inventory.", MsgBoxStyle.Information, "GAIT Inventory")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dtpGAITAquired_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpGAITAquired.TextChanged
        Try
            GAITYearsInService()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRefreshGAITDropdowns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshGAITDropdowns.Click
        Try
            ClearGAITInventory()
            LoadGAITLookUps()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbGAITUseDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbGAITUseDate.CheckedChanged
        Try
            If chbGAITUseDate.Checked = False Then
                pnlGAITDateSearch.Enabled = False
                rdbGAITDatePurchased.Checked = False
                rdbGAITDatePurchased.Enabled = False
                rdbGAITDateAquired.Checked = False
                rdbGAITDateAquired.Enabled = False
                dtpGAITDateStart.Enabled = False
                dtpGAITDateEnd.Enabled = False
            Else
                pnlGAITDateSearch.Enabled = True
                rdbGAITDatePurchased.Enabled = True
                rdbGAITDateAquired.Enabled = True
                dtpGAITDateStart.Enabled = True
                dtpGAITDateEnd.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSearchGAITAssets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchGAITAssets.Click
        Try

            LoadGAITInventory()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportGAIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportGAIT.Click
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i, j As Integer

            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvGAITInventory.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvGAITInventory.ColumnCount - 1
                        .Cells(1, i + 1) = dgvGAITInventory.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvGAITInventory.ColumnCount - 1
                        For j = 0 To dgvGAITInventory.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvGAITInventory.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                If ExcelApp.Visible = False Then
                    ExcelApp.Visible = True
                End If
            End If

        Catch ex As Exception
            If ex.ToString.Contains("RPC_E_CALL_REJECTED") Then
                MsgBox("Error in exporting data." & vbCrLf & "Please run the export again.")
            Else
                ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If
        End Try
    End Sub

End Class