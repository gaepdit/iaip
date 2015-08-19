Imports Oracle.ManagedDataAccess.Client


Public Class PASPFeeDeposits
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Panel1temp As String

    Dim dsWorkEnTry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Dim recExist As Boolean
    Dim feeyear As String
    Dim SQL, SQL2, SQL3 As String
    Dim cmd, cmd2, cmd3 As OracleCommand
    Dim dr, dr2, dr3 As OracleDataReader

    Private Sub PASPFeeDeposits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()

            'If Now.Date < "09/01/" & Now.Year Then
            '    feeyear = Now.Year - 2
            'Else
            '    feeyear = Now.Year - 1
            'End If
            feeyear = 2005
            txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")

            FillComboBoxes()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Deposit Number or AIRS Number..."
            panel2.Text = UserName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub FillComboBoxes()
        Try

            cboDepositNo.Items.Add("")
            cboAirsNo.Items.Add("")
            cboFeeYear.Items.Add("")

            SQL = "Select distinct strdepositno from AIRBRANCH.FSAddPaid " _
           + "order by strdepositno"

            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            dr.Read()
            Do
                cboDepositNo.Items.Add(dr("strdepositno"))

            Loop While dr.Read
            dr.Close()

            SQL2 = "Select distinct substr(strairsnumber, 5) as strairsnumber " _
           + "from AIRBRANCH.FSAddPaid order by strairsnumber"

            cmd2 = New OracleCommand(SQL2, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr2 = cmd2.ExecuteReader

            dr2.Read()
            Do
                cboAirsNo.Items.Add(dr2("strairsnumber"))

            Loop While dr2.Read
            dr2.Close()

            SQL3 = "Select paytype " & _
            "from AIRBRANCH.FSPayType " & _
            "order by paytype"

            cmd3 = New OracleCommand(SQL3, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr3 = cmd3.ExecuteReader

            dr3.Read()
            Do
                cboPayType.Items.Add(dr3("paytype"))

            Loop While dr3.Read
            dr3.Close()

            SQL = "Select " & _
            "distinct(intYear) as intYear " & _
            "from AIRBRANCH.FSAddPaid " & _
            "order by intyear desc "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboFeeYear.Items.Add(dr.Item("intYear"))
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll.LinkClicked
        Try

            LoadDataGrid()
            txtCount.Text = dgvDeposit.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadDataGrid()

        Dim SQL As String = ""
        Try

            dsWorkEnTry = New DataSet

            If cboDepositNo.Text <> "" Then
                SQL = "Select " & _
                "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                "strDepositNo, datPayDate, " & _
                "numPayment, intYear, " & _
                "strCheckNo, strBatchNo, " & _
                "strPayType, intPayId, " & _
                "strComments, strInvoiceNo " & _
                "from AIRBRANCH.FSAddPaid " & _
                "where strDepositNo = '" & cboDepositNo.Text & "' " & _
                "order by strBatchNo "

            Else
                If cboAirsNo.Text <> "" Then
                    SQL = "Select " & _
                   "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                   "strDepositNo, datPayDate, " & _
                   "numPayment, intYear, " & _
                   "strCheckNo, strBatchNo, " & _
                   "strPayType, intPayId, " & _
                   "strComments, strInvoiceNo " & _
                   "from AIRBRANCH.FSAddPaid " & _
                   "where strairsnumber = '0413" & cboAirsNo.Text & "' " & _
                   "order by strBatchNo "
                Else
                    If cboFeeYear.Text <> "" Then
                        If rdbDeposited.Checked = True Then
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from AIRBRANCH.FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "and strDepositNo is Not Null " & _
                           "order by strBatchNo "
                        Else
                            SQL = "Select " & _
                           "substr(strAIRSNumber, 5) as strAIRSnumber, " & _
                           "strDepositNo, datPayDate, " & _
                           "numPayment, intYear, " & _
                           "strCheckNo, strBatchNo, " & _
                           "strPayType, intPayId, " & _
                           "strComments, strInvoiceNo " & _
                           "from AIRBRANCH.FSAddPaid " & _
                           "where intYear = '" & cboFeeYear.Text & "' " & _
                           "order by strBatchNo "
                        End If
                    Else
                        MsgBox("Please select a Deposit Number or AIRS Number or Fee Year first", MsgBoxStyle.OkOnly, "Incorrect Selection")
                    End If
                End If
            End If

            If SQL <> "" Then
                daWorkEnTry = New OracleDataAdapter(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                daWorkEnTry.Fill(dsWorkEnTry, "tblWorkEnTry")
                dgvDeposit.DataSource = dsWorkEnTry
                dgvDeposit.DataMember = "tblWorkEntry"

                dgvDeposit.RowHeadersVisible = False
                dgvDeposit.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvDeposit.AllowUserToResizeColumns = True
                dgvDeposit.AllowUserToAddRows = False
                dgvDeposit.AllowUserToDeleteRows = False
                dgvDeposit.AllowUserToOrderColumns = True
                dgvDeposit.AllowUserToResizeRows = True
                dgvDeposit.ColumnHeadersHeight = "35"
                dgvDeposit.Columns("strairsnumber").HeaderText = "AIRS Number"
                dgvDeposit.Columns("strairsnumber").DisplayIndex = 0
                dgvDeposit.Columns("strdepositno").HeaderText = "Deposit Number"
                dgvDeposit.Columns("strdepositno").DisplayIndex = 1
                dgvDeposit.Columns("datpaydate").HeaderText = "Deposit Date"
                dgvDeposit.Columns("datpaydate").DisplayIndex = 2
                dgvDeposit.Columns("datpaydate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                dgvDeposit.Columns("numpayment").HeaderText = "Payment"
                dgvDeposit.Columns("numpayment").DisplayIndex = 3
                dgvDeposit.Columns("intyear").HeaderText = "Year"
                dgvDeposit.Columns("intyear").DisplayIndex = 4
                dgvDeposit.Columns("strcheckno").HeaderText = "Check Number"
                dgvDeposit.Columns("strcheckno").DisplayIndex = 5
                dgvDeposit.Columns("strbatchno").HeaderText = "Batch Number"
                dgvDeposit.Columns("strbatchno").DisplayIndex = 6
                dgvDeposit.Columns("strpaytype").HeaderText = "Payment Type"
                dgvDeposit.Columns("strpaytype").DisplayIndex = 7
                dgvDeposit.Columns("intpayid").HeaderText = "Pay ID"
                dgvDeposit.Columns("intpayid").DisplayIndex = 8
                dgvDeposit.Columns("intpayid").Visible = False
                dgvDeposit.Columns("strcomments").HeaderText = "Comments"
                dgvDeposit.Columns("strcomments").DisplayIndex = 9
                dgvDeposit.Columns("strcomments").Visible = False
                dgvDeposit.Columns("strinvoiceno").HeaderText = "Invoce Number"
                dgvDeposit.Columns("strinvoiceno").DisplayIndex = 10
                dgvDeposit.Columns("strinvoiceno").Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ExportToExcel()
        dgvDeposit.ExportToExcel(Me)
    End Sub
    Private Sub dgvDeposit_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDeposit.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDeposit.HitTest(e.X, e.Y)
        Dim temp As String

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then

                If dgvDeposit.RowCount > 0 And hti.RowIndex <> -1 Then
                    temp = dgvDeposit.Columns(1).HeaderText

                    If dgvDeposit.Columns(0).HeaderText = "AIRS Number" Then
                        mtbAirsNo.Text = dgvDeposit(0, hti.RowIndex).Value
                        txtPayId.Text = dgvDeposit(8, hti.RowIndex).Value
                        If IsDBNull(dgvDeposit(1, hti.RowIndex).Value) Then
                            txtDepositNo.Clear()
                        Else
                            txtDepositNo.Text = dgvDeposit(1, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(2, hti.RowIndex).Value) Then
                            txtDepositdate.Clear()
                        Else
                            txtDepositdate.Text = dgvDeposit(2, hti.RowIndex).FormattedValue
                        End If
                        If IsDBNull(dgvDeposit(3, hti.RowIndex).Value) Then
                            txtPayment.Clear()
                        Else
                            txtPayment.Text = dgvDeposit(3, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(4, hti.RowIndex).Value) Then
                            txtYear.Clear()
                        Else
                            txtYear.Text = dgvDeposit(4, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(5, hti.RowIndex).Value) Then
                            txtCheckNo.Clear()
                        Else
                            txtCheckNo.Text = dgvDeposit(5, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(6, hti.RowIndex).Value) Then
                            txtbatchNo.Clear()
                        Else
                            txtbatchNo.Text = dgvDeposit(6, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(7, hti.RowIndex).Value) Then
                            cboPayType.Text = ""
                        Else
                            cboPayType.Text = ""
                            cboPayType.Text = dgvDeposit(7, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(9, hti.RowIndex).Value) Then
                            txtComments.Clear()
                        Else
                            txtComments.Text = dgvDeposit(9, hti.RowIndex).Value
                        End If
                        If IsDBNull(dgvDeposit(10, hti.RowIndex).Value) Then
                            txtInvoiceNo.Clear()
                        Else
                            txtInvoiceNo.Text = dgvDeposit(10, hti.RowIndex).Value
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try


    End Sub
    Private Sub btnAddNewEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewEnTry.Click
        Try


            ValidateEntry()

            SQL = "Insert into AIRBRANCH.FSAddPaid (strairsnumber, intyear, numpayment, " _
             + "datpaydate, strcheckno, strdepositno, strpaytype, strbatchno, " _
             + "strentryperson, strcomments, intpayid) values('0413" & mtbAirsNo.Text & "', " _
             + " " & CInt(txtYear.Text) & " ,  " & CDec(txtPayment.Text) & " , '" & txtDepositdate.Text & "', " _
             + "'" & txtCheckNo.Text & "', '" & txtDepositNo.Text & "', " _
             + "'" & cboPayType.Text & "', '" & txtbatchNo.Text & "', " _
             + "'" & UserGCode & "', '" & Replace(txtComments.Text, "'", "''") & "', AIRBRANCH.seqfsdeposit.nextval)"

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record was added successfully", MsgBoxStyle.Information, "Entry Success!")
            ClearPage()
            LoadDataGrid()
            FillComboBoxes()

        Catch ex As Exception
            ErrorReport(ex, SQL, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ValidateEntry()
        Try

            If mtbAirsNo.Text.Length <> 8 Then
                MsgBox("Please enter a valid AIRS Number.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                Exit Sub
            End If
            If cboAirsNo.ValueMember.Contains(mtbAirsNo.Text) = False Then
                MsgBox("This AIRS # is not a valid AIRS # please verify that the value entered it correct." & vbCrLf & _
                       "If you get this message in error contact Data Management Unit for help.", MsgBoxStyle.OkOnly, "Incorrect AIRS Number")
                Exit Sub
            Else
                MsgBox("Valid AIRS #")
                Exit Sub
            End If
            If txtYear.Text = "" Then
                If IsNumeric(txtYear.Text) Then
                Else
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    Exit Sub
                End If
            Else
                If txtYear.Text > feeyear + 1 Then
                    MsgBox("Please enter a valid Reporting Year", MsgBoxStyle.OkOnly, "Incorrect Year")
                    Exit Sub
                End If
            End If

            If txtDepositdate.Text = "" Then
                MsgBox("Please enter a Deposit Date", MsgBoxStyle.OkOnly, "Incorrect Date")
                Exit Sub
            End If

            If txtPayment.Text = "" Then
                MsgBox("Please enter Amount Paid", MsgBoxStyle.OkOnly, "Incorrect Payment")
                Exit Sub
            End If

            If txtDepositNo.Text = "" Then
                MsgBox("Please enter a Deposit Number", MsgBoxStyle.OkOnly, "Incorrect Deposit No.")
                Exit Sub
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnUpdateEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateEnTry.Click

        Try

            SQL = "Update AIRBRANCH.FSAddPaid set strairsnumber = '0413" & mtbAirsNo.Text & "', " _
            + "datpaydate = '" & txtDepositdate.Text & "', " _
            + "numpayment = '" & CDec(txtPayment.Text) & "', " _
            + "strcheckno = '" & txtCheckNo.Text & "', " _
            + "strbatchno = '" & txtbatchNo.Text & "', " _
            + "strpaytype = '" & cboPayType.Text & "', " _
            + "strdepositno = '" & txtDepositNo.Text & "', " _
            + "intyear = '" & CInt(txtYear.Text) & "', " _
            + "strcomments = '" & Replace(txtComments.Text, "'", "''") & "', " _
            + "strentryperson = '" & UserGCode & "' " _
            + "where intpayid = '" & txtPayId.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record has been updated successfully", MsgBoxStyle.Information, "Update Success!")
            ClearPage()
            LoadDataGrid()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub btnDeleteEnTry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEnTry.Click

        Try

            SQL = "Delete from AIRBRANCH.FSAddPaid " _
            + "where intpayid = '" & txtPayId.Text & "'"

            cmd = New OracleCommand(SQL, CurrentConnection)
            cmd.CommandType = CommandType.Text

            If CurrentConnection.State = ConnectionState.Open Then
            Else
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader

            MsgBox("The record has been deleted successfully", MsgBoxStyle.Information, "Delete Success!")
            ClearPage()
            LoadDataGrid()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub ClearPage()
        Try

            txtPayId.Text = ""
            txtDepositdate.Text = Format$(Now, "dd-MMM-yyyy")
            txtDepositNo.Text = ""
            txtPayment.Text = ""
            txtCheckNo.Text = ""
            txtbatchNo.Text = ""
            txtComments.Text = ""
            txtYear.Text = ""
            cboPayType.Text = ""
            mtbAirsNo.Text = ""
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
            Case 0
                ClearPage()
            Case 1
                ExportToExcel()
            Case Else
        End Select
    End Sub
    Private Sub PASPFeeDeposits_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Dispose()
    End Sub

End Class
