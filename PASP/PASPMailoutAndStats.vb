Imports System.Data.OracleClient


Public Class DEVMailoutAndStats
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim dsViewCount As DataSet
    Dim daViewCount As OracleDataAdapter
    Dim ds As DataSet
    Dim da As OracleDataAdapter
    Dim ds2 As DataSet
    Dim da2 As OracleDataAdapter
    Dim dsWorkEntry As DataSet
    Dim daWorkEntry As OracleDataAdapter
    Dim airsno As String
    Dim recExist As Boolean
    Dim dtairs As New DataTable
    Dim dtairs2 As New DataTable


    Private Sub PASPMailoutAndStats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            pnl2.Text = UserName
            pnl3.Text = OracleDate
            loadDepositAndPayment()
            loadMailOutYear()
            loadFeeRateYear()
            LoadAdminComboBoxes()
            loadYear()
            FormatDataGridForWorkEnTry()
            FormatDataGridForWorkEnTryEmail()
            pnl1.Text = "Misc Web Tools Loading..."
            bgwAIRS.WorkerSupportsCancellation = True
            bgwEmails.WorkerSupportsCancellation = True
            bgwAIRS.RunWorkerAsync()
            bgwEmails.RunWorkerAsync()
            pnlDetails.Dock = DockStyle.None
            pnlCorrectPaymentType.Visible = False

            If AccountArray(12, 3) <> "1" Then
                TCMailoutAndStats.TabPages.Remove(TPMiscWebTools)
                TCMailoutAndStats.TabPages.Remove(TPGenerateMailOut)
                TCMailoutAndStats.TabPages.Remove(TPEnrollment)
                TCMailoutAndStats.TabPages.Remove(TPFeeRates)
                TCMailoutAndStats.TabPages.Remove(TPNonRespondersReport)
                btnCorrectPaymentType.Visible = False
            End If
            If TCMailoutAndStats.TabPages.Contains(TPNonRespondersReport) Then
                LoadFeeYear()
            End If
            dtpStartDepositDate.Text = Format(CDate(OracleDate).AddMonths(-1), "dd-MMM-yyyy")
            dtpEndDepositDate.Text = OracleDate
            dtpStartDepositDate.Enabled = False
            dtpEndDepositDate.Enabled = False
            chbDepositDateSearch.Checked = False
            btnRunDepositReport.Enabled = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region "Fee Admin Tools"
    Sub LoadAdminComboBoxes()
        Try
            cboClass.Items.Add(" ")
            cboClass.Items.Add("A")
            cboClass.Items.Add("B")
            cboClass.Items.Add("PM")
            cboClass.Items.Add("SM")
            cboClass.Items.Add("PR")
            cboOperation.Items.Add(" ")
            cboOperation.Items.Add("O")
            cboOperation.Items.Add("X")
            cboOperation.Items.Add("C")
            cboOperation.Items.Add("P")
            cboOperation.Items.Add("T")
            cboNSPS.Items.Add(" ")
            cboNSPS.Items.Add("YES")
            cboNSPS.Items.Add("NO")
            cboPart70.Items.Add(" ")
            cboPart70.Items.Add("YES")
            cboPart70.Items.Add("NO")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub loadMailOutYear()
        Try
            Dim year As Integer

            year = Now.Year - 1
            If cboMailoutYear.Items.Contains(year) Then
            Else
                cboMailoutYear.Items.Add(year)
            End If

            SQL = "Select " & _
            "distinct(FEEMAILOUT.INTYEAR) as IntYear " & _
            "from " & DBNameSpace & ".FEEMAILOUT " & _
            "order by FEEMAILOUT.INTYEAR desc"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("IntYear")) Then
                    year = Now.Year
                Else
                    year = dr.Item("IntYear")
                End If
                If cboMailoutYear.Items.Contains(year) Then

                Else
                    cboMailoutYear.Items.Add(year)
                End If
            End While
            dr.Close()
            If cboMailoutYear.Items.Count = 0 Then
                cboMailoutYear.Items.Add(Now.Year)
            End If
         

            cboMailoutYear.SelectedIndex = 0


            'While dr.Read
            '    If IsDBNull(dr.Item("IntYear")) Then
            '        year = Now.Year
            '    Else
            '        year = dr.Item("IntYear")
            '    End If
            '    cboMailoutYear.Items.Add(year)
            'End While
            'dr.Close()

            'cboMailoutYear.SelectedIndex = 0

            'SQL = "Select " & _
            '"distinct FEEMAILOUT.INTYEAR " & _
            '"from " & connNameSpace & ".FEEMAILOUT " & _
            '"order by FEEMAILOUT.INTYEAR desc"
            'cmd = New OracleCommand(SQL, conn)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            'dr.Read()
            'year = dr("INTYEAR") + 1
            'cboMailoutYear.Items.Add(year)
            'Do
            '    year = dr("INTYEAR")
            '    cboMailoutYear.Items.Add(year)
            'Loop While dr.Read
            'cboMailoutYear.SelectedIndex = 0
            'dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub BtnExportExcel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click
        Try
            ExporttoExcel2()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub ExporttoExcel2()
        Try
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            'Dim ExcelApp As New Excel.Application
            Dim i As Integer
            Dim j As Integer

            If dgvFeeDataCount2.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvFeeDataCount2.ColumnCount - 1
                        .Cells(1, i + 1) = dgvFeeDataCount2.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvFeeDataCount2.ColumnCount - 1
                        For j = 0 To dgvFeeDataCount2.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvFeeDataCount2.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub dgvFeeDataCount_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFeeDataCount.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFeeDataCount.HitTest(e.X, e.Y)
        Try
            If dgvFeeDataCount.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFeeDataCount.Columns(0).HeaderText = "Airs No." Then
                    If IsDBNull(dgvFeeDataCount(0, hti.RowIndex).Value) Then
                    Else
                        clearFeeMailout()
                        txtAirsNo.Text = dgvFeeDataCount(0, hti.RowIndex).Value
                        txtFacilityName.Text = dgvFeeDataCount(1, hti.RowIndex).Value
                        findFeeMailOut()
                    End If
                Else
                    If dgvFeeDataCount.Columns(0).HeaderText = "Airs No." Then
                        If IsDBNull(dgvFeeDataCount(0, hti.RowIndex).Value) Then
                        Else
                            clearFeeMailout()
                            txtAirsNo.Text = dgvFeeDataCount(0, hti.RowIndex).Value
                            txtFacilityName.Text = dgvFeeDataCount(1, hti.RowIndex).Value
                            findFeeMailOut()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try

    End Sub
    Private Sub clearFeeMailout()

        txtAirsNo.Text = ""
        txtFacilityName.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtCompanyName.Text = ""
        txtContactAddress1.Text = ""
        txtContactCity.Text = ""
        txtContactState.Text = ""
        txtContactZip.Text = ""
        txtMailoutEmail.Text = ""
        txtShutdowndate.Text = ""
        cboClass.SelectedItem = " "
        cboOperation.SelectedItem = " "
        cboNSPS.SelectedItem = " "
        cboPart70.SelectedItem = " "

    End Sub
    Private Sub findFeeMailOut()
        Try
            Dim AirsNo As String = txtAirsNo.Text
            Dim feeyear As Integer = CInt(cboMailoutYear.SelectedItem)
            SQL = "SELECT FeeMailOut.STRFACILITYNAME, FeeMailOut.STRCONTACTFIRSTNAME, FeeMailOut.STRCONTACTLASTNAME, " & _
              "FeeMailOut.STRCOMPANYNAME, FeeMailOut.STRCONTACTADDRESS, FeeMailOut.STRCONTACTCITY, " & _
              "FeeMailOut.STRCONTACTSTATE, FeeMailOut.STRCONTACTZIPCODE, FeeMailOut.STRCONTACTEMAIL, " & _
              "FeeMailOut.STROPERATIONALSTATUS, FeeMailOut.DATSHUTDOWNDATE, FeeMailOut.STRCLASS, " & _
              "FeeMailOut.STRAPCPART70, FeeMailOut.STRAPCNSPS, " & _
              "FeeMailOut.STRFACILITYADDRESS, " & _
              "FeeMailOut.STRFACILITYCITY, FeeMailOut.STRFACILITYSTATE, " & _
              "FeeMailOut.STRFACILITYZIPCODE " & _
              "from " & DBNameSpace & ".FeeMailOut " & _
              "where FeeMailOut.STRAIRSNUMBER = '" & AirsNo & "' " & _
              "and FeeMailOut.intYEAR = '" & feeyear & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtFacilityName.Text = ""
                Else
                    txtFacilityName.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRFACILITYADDRESS")) Then
                    txtFacilityAddress.Text = ""
                Else
                    txtFacilityAddress.Text = dr("STRFACILITYADDRESS")
                End If
                If IsDBNull(dr("STRFACILITYCITY")) Then
                    txtFacilityCity.Text = ""
                Else
                    txtFacilityCity.Text = dr("STRFACILITYCITY")
                End If
                If IsDBNull(dr("STRFACILITYSTATE")) Then
                    txtFacilityState.Text = ""
                Else
                    txtFacilityState.Text = dr("STRFACILITYSTATE")
                End If
                If IsDBNull(dr("STRFACILITYZIPCODE")) Then
                    txtFacilityZip.Text = ""
                Else
                    txtFacilityZip.Text = dr("STRFACILITYZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    txtFirstName.Text = ""
                Else
                    txtFirstName.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    txtLastName.Text = ""
                Else
                    txtLastName.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCOMPANYNAME")) Then
                    txtCompanyName.Text = ""
                Else
                    txtCompanyName.Text = dr("STRCOMPANYNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS")) Then
                    txtContactAddress1.Text = ""
                Else
                    txtContactAddress1.Text = dr("STRCONTACTADDRESS")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    txtContactCity.Text = ""
                Else
                    txtContactCity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    txtContactState.Text = ""
                Else
                    txtContactState.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                    txtContactZip.Text = ""
                Else
                    txtContactZip.Text = dr("STRCONTACTZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    txtMailoutEmail.Text = ""
                Else
                    txtMailoutEmail.Text = dr("STRCONTACTEMAIL")
                End If
                If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                    cboOperation.SelectedItem = ""
                Else
                    cboOperation.SelectedItem = dr("STROPERATIONALSTATUS")
                End If
                If IsDBNull(dr("DATSHUTDOWNDATE")) Then
                    txtShutdowndate.Text = ""
                Else
                    txtShutdowndate.Text = Format(dr("DATSHUTDOWNDATE"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr("STRCLASS")) Then
                    cboClass.SelectedItem = ""
                Else
                    cboClass.SelectedItem = dr("STRCLASS")
                End If
                If IsDBNull(dr("STRAPCPART70")) Then
                    cboPart70.SelectedItem = ""
                Else
                    cboPart70.SelectedItem = dr("STRAPCPART70")
                End If
                If IsDBNull(dr("STRAPCNSPS")) Then
                    cboNSPS.SelectedItem = ""
                Else
                    cboNSPS.SelectedItem = dr("STRAPCNSPS")
                End If
            End While
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub findSummaryMailOut()
        Try
            Dim AirsNo As String = lblAirsNo.Text
            Dim feeyear As Integer = CInt(cboYear.SelectedItem)
            SQL = "SELECT FeeMailOut.STRFACILITYNAME, FeeMailOut.STRCONTACTFIRSTNAME, FeeMailOut.STRCONTACTLASTNAME, " & _
              "FeeMailOut.STRCONTACTADDRESS, FeeMailOut.STRCONTACTCITY, " & _
              "FeeMailOut.STRCONTACTSTATE, FeeMailOut.STRCONTACTZIPCODE, FeeMailOut.STRCONTACTEMAIL, " & _
              "FeeMailOut.STROPERATIONALSTATUS, FeeMailOut.DATSHUTDOWNDATE, FeeMailOut.STRCLASS, " & _
              "FeeMailOut.STRAPCPART70, FeeMailOut.STRAPCNSPS " & _
              "from " & DBNameSpace & ".FeeMailOut " & _
              "where FeeMailOut.STRAIRSNUMBER = '" & AirsNo & "' " & _
              "and FeeMailOut.intYEAR = '" & feeyear & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    lblFacilityName.Text = ""
                Else
                    lblFacilityName.Text = dr("STRFACILITYNAME")
                End If
                If IsDBNull(dr("STRCONTACTADDRESS")) Then
                    lblcontactstreet.Text = ""
                Else
                    lblcontactstreet.Text = dr("STRCONTACTADDRESS")
                End If
                If IsDBNull(dr("STRCONTACTCITY")) Then
                    lblcity.Text = ""
                Else
                    lblcity.Text = dr("STRCONTACTCITY")
                End If
                If IsDBNull(dr("STRCONTACTSTATE")) Then
                    lblstate.Text = ""
                Else
                    lblstate.Text = dr("STRCONTACTSTATE")
                End If
                If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                    lblzip.Text = ""
                Else
                    lblzip.Text = dr("STRCONTACTZIPCODE")
                End If
                If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                    lblfirstname.Text = ""
                Else
                    lblfirstname.Text = dr("STRCONTACTFIRSTNAME")
                End If
                If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                    lblLastname.Text = ""
                Else
                    lblLastname.Text = dr("STRCONTACTLASTNAME")
                End If
                If IsDBNull(dr("STRCONTACTEMAIL")) Then
                    lblEmail.Text = ""
                Else
                    lblEmail.Text = dr("STRCONTACTEMAIL")
                End If
                If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                    lbloperationalstatus.Text = ""
                Else
                    lbloperationalstatus.Text = dr("STROPERATIONALSTATUS")
                End If
                If IsDBNull(dr("DATSHUTDOWNDATE")) Then
                    lblshutdowndate.Text = ""
                Else
                    lblshutdowndate.Text = dr("DATSHUTDOWNDATE")
                End If
                If IsDBNull(dr("STRCLASS")) Then
                    lblclass.Text = ""
                Else
                    lblclass.Text = dr("STRCLASS")
                End If
                If IsDBNull(dr("STRAPCPART70")) Then
                    lblPart70.Text = ""
                Else
                    lblPart70.Text = dr("STRAPCPART70")
                End If
                If IsDBNull(dr("STRAPCNSPS")) Then
                    lblNSPS.Text = ""
                Else
                    lblNSPS.Text = dr("STRAPCNSPS")
                End If
            End While
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

    Private Sub btnFeeDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeDelete.Click
        Try
            Dim intAnswer As Integer
            intAnswer = MsgBox("Delete the record?", MsgBoxStyle.OkCancel)
            If intAnswer = vbOK Then
                deleteFeemailout()
                MsgBox("The info has been deleted!", MsgBoxStyle.Information, "Mailout and Stats")
                txtRecordNumber.Text = CInt(txtRecordNumber.Text) - 1
                clearFeeMailout()
            Else
                MsgBox("The info is not deleted!", MsgBoxStyle.Information, "Mailout and Stats")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub deleteFeemailout()
        Try
            Dim AirsNo As String = txtAirsNo.Text
            Dim feeyear As Integer = CInt(cboMailoutYear.SelectedItem)
            SQL = "delete from " & DBNameSpace & ".feeMailOut " & _
            "where feeMailOut.STRAIRSNUMBER = '" & AirsNo & "'" & _
            " and feeMailOut.INTYEAR = '" & feeyear & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnFeeSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeSave.Click
        Try
            If txtAirsNo.Text = "" Then
                MsgBox("Please enter Airs  Number.", MsgBoxStyle.Information, "Mailout and Stats")
            Else
                SaveFeeMailOut()
                clearFeeMailout()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub SaveFeeMailOut()
        Dim AirsNo As String = txtAirsNo.Text
        Dim feeYear As Integer = CInt(cboMailoutYear.SelectedItem)
        Dim FacilityName As String = txtFacilityName.Text
        Dim FacilityAddress1 As String = ""
        Dim programcodes As String = ""
        Dim FacilityCity As String = ""
        Dim FacilityState As String = ""
        Dim FacilityZip As String = ""
        Dim FirstName As String = txtFirstName.Text
        Dim LastName As String = txtLastName.Text
        Dim CompanyName As String = txtCompanyName.Text
        Dim ContactAddress1 As String = txtContactAddress1.Text
        Dim ContactCity As String = txtContactCity.Text
        Dim contactState As String = txtContactState.Text
        Dim ContactZip As String = txtContactZip.Text
        Dim ContactEmail As String = txtMailoutEmail.Text
        Dim shutdowndate As String = txtShutdowndate.Text
        Dim operationalstatus As String = cboOperation.SelectedItem
        Dim classstatus As String = cboClass.SelectedItem
        Dim NSPSstatus As String = cboNSPS.SelectedItem
        Dim Part70Status As String = cboPart70.SelectedItem
        Try
            SQL = "Select " & _
            "FEEMAILOUT.STRAIRSNUMBER " & _
            "from " & DBNameSpace & ".FEEMAILOUT " & _
            "where FEEMAILOUT.STRAIRSNUMBER = '" & AirsNo & "' " & _
            " and FEEMAILOUT.INTYEAR = '" & feeYear & "' "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "update " & DBNameSpace & ".FEEMAILOUT set " & _
                "FEEMAILOUT.STRCONTACTFIRSTNAME = '" & FirstName & "', " & _
                "FEEMAILOUT.STRFACILITYNAME = '" & FacilityName & "', " & _
                "FEEMAILOUT.STRCONTACTLASTNAME = '" & LastName & "', " & _
                "FEEMAILOUT.STRCOMPANYNAME = '" & CompanyName & "', " & _
                "FEEMAILOUT.STRCONTACTADDRESS = '" & ContactAddress1 & "', " & _
                "FEEMAILOUT.STRCONTACTCITY = '" & ContactCity & "', " & _
                "FEEMAILOUT.STRCONTACTSTATE = '" & contactState & "', " & _
                "FEEMAILOUT.STRCONTACTZIPCODE = '" & ContactZip & "', " & _
                "FEEMAILOUT.STRCONTACTEMAIL = '" & ContactEmail & "', " & _
                "FEEMAILOUT.DATSHUTDOWNDATE = '" & shutdowndate & "', " & _
                "FEEMAILOUT.STROPERATIONALSTATUS = '" & operationalstatus & "', " & _
                "FEEMAILOUT.STRCLASS = '" & classstatus & "', " & _
                "FEEMAILOUT.STRAPCNSPS = '" & NSPSstatus & "', " & _
                "FEEMAILOUT.STRAPCPART70 = '" & Part70Status & "' " & _
                "where FEEMAILOUT.STRAIRSNUMBER = '" & AirsNo & "' " & _
                "and FEEMAILOUT.INTYEAR = '" & feeYear & "' "
                MsgBox("The info has been updated.", MsgBoxStyle.Information, "Mailout and Stats")
            Else
                SQL = "Select * " & _
                "from " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                "where APBFACILITYINFORMATION.STRAIRSNUMBER = '" & AirsNo & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("STRFACILITYNAME")) Then
                        FacilityName = ""
                    Else
                        FacilityName = dr("STRFACILITYNAME")
                    End If
                    If IsDBNull(dr("STRFACILITYSTREET1")) Then
                        FacilityAddress1 = ""
                    Else
                        FacilityAddress1 = dr("STRFACILITYSTREET1")
                    End If
                    If IsDBNull(dr("STRFACILITYCITY")) Then
                        FacilityCity = ""
                    Else
                        FacilityCity = dr("STRFACILITYCITY")
                    End If
                    If IsDBNull(dr("STRFACILITYSTATE")) Then
                        FacilityState = ""
                    Else
                        FacilityState = dr("STRFACILITYSTATE")
                    End If
                    If IsDBNull(dr("STRFACILITYZIPCODE")) Then
                        FacilityZip = ""
                    Else
                        FacilityZip = dr("STRFACILITYZIPCODE")
                    End If
                End While
                dr.Close()
                SQL = "Select * " & _
                "from " & DBNameSpace & ".APBHEADERDATA " & _
                "where APBHEADERDATA.STRAIRSNUMBER = '" & AirsNo & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("STRCLASS")) Then
                        classstatus = ""
                    Else
                        classstatus = dr("STRCLASS")
                    End If
                    If IsDBNull(dr("DATSHUTDOWNDATE")) Then
                        shutdowndate = ""
                    Else
                        shutdowndate = dr("DATSHUTDOWNDATE")
                    End If
                    If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                        operationalstatus = ""
                    Else
                        operationalstatus = dr("STROPERATIONALSTATUS")
                    End If
                    If IsDBNull(dr("strairprogramcodes")) Then
                        programcodes = " "
                    Else
                        programcodes = dr("strairprogramcodes")
                        If Mid(programcodes, 8, 1) = 1 Then
                            NSPSstatus = "YES"
                        Else
                            NSPSstatus = "NO"
                        End If

                        If Mid(programcodes, 13, 1) = 1 Then
                            Part70Status = "YES"
                        Else
                            Part70Status = "NO"
                        End If
                    End If
                End While
                dr.Close()
                SQL = "Insert into " & DBNameSpace & ".FEEMAILOUT " & _
                "(FEEMAILOUT.STRAIRSNUMBER, " & _
                "FEEMAILOUT.INTYEAR, " & _
                "FEEMAILOUT.STRFACILITYNAME, " & _
                "FEEMAILOUT.STRFACILITYADDRESS, " & _
                "FEEMAILOUT.STRFACILITYCITY, " & _
                "FEEMAILOUT.STRFACILITYSTATE, " & _
                "FEEMAILOUT.STRFACILITYZIPCODE, " & _
                "FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
                "FEEMAILOUT.STRCONTACTLASTNAME, " & _
                "FEEMAILOUT.STRCOMPANYNAME, " & _
                "FEEMAILOUT.STRCONTACTADDRESS, " & _
                "FEEMAILOUT.STRCONTACTCITY, " & _
                "FEEMAILOUT.STRCONTACTSTATE, " & _
                "FEEMAILOUT.STRCONTACTZIPCODE, " & _
                "FEEMAILOUT.STRCONTACTEMAIL, " & _
                "FEEMAILOUT.STROPERATIONALSTATUS, " & _
                "FEEMAILOUT.DATSHUTDOWNDATE, " & _
                "FEEMAILOUT.STRCLASS, " & _
                "FEEMAILOUT.STRAPCPART70, " & _
                "FEEMAILOUT.STRAPCNSPS) " & _
                "values (" & _
                "'" & Replace(AirsNo, "'", "''") & "', " & _
                "'" & Replace(feeYear, "'", "''") & "', " & _
                "'" & Replace(FacilityName, "'", "''") & "', " & _
                "'" & Replace(FacilityAddress1, "'", "''") & "', " & _
                "'" & Replace(FacilityCity, "'", "''") & "', " & _
                "'" & Replace(FacilityState, "'", "''") & "', " & _
                "'" & Replace(FacilityZip, "'", "''") & "', " & _
                "'" & Replace(FirstName, "'", "''") & "', " & _
                "'" & Replace(LastName, "'", "''") & "', " & _
                "'" & Replace(CompanyName, "'", "''") & "', " & _
                "'" & Replace(ContactAddress1, "'", "''") & "', " & _
                "'" & Replace(ContactCity, "'", "''") & "', " & _
                "'" & Replace(contactState, "'", "''") & "', " & _
                "'" & Replace(ContactZip, "'", "''") & "', " & _
                "'" & Replace(ContactEmail, "'", "''") & "', " & _
                "'" & Replace(operationalstatus, "'", "''") & "', " & _
                "'" & Replace(shutdowndate, "'", "''") & "', " & _
                "'" & Replace(classstatus, "'", "''") & "', " & _
                "'" & Replace(Part70Status, "'", "''") & "', " & _
                "'" & Replace(NSPSstatus, "'", "''") & "' " & _
                " )"
                MsgBox("The new fee contact info has been added!", MsgBoxStyle.Information, "Mailout and Stats")
            End If
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub GeneratefeeMailOut()
        Dim AirsNo As String = txtAirsNo.Text
        Dim feeYear As Integer = CInt(cboMailoutYear.SelectedItem)
        Dim FacilityName As String = txtFacilityName.Text
        Dim FacilityAddress1 As String
        Dim FacilityCity As String
        Dim FacilityState As String
        Dim FacilityZip As String
        Dim FirstName As String = txtFirstName.Text
        Dim LastName As String = txtLastName.Text
        Dim CompanyName As String = txtCompanyName.Text
        Dim ContactAddress1 As String = txtContactAddress1.Text
        Dim ContactCity As String = txtContactCity.Text
        Dim contactState As String = txtContactState.Text
        Dim ContactZip As String = txtContactZip.Text
        Dim ContactEmail As String = txtMailoutEmail.Text
        Dim shutdowndate As String = txtShutdowndate.Text
        Dim operationalstatus As String = cboOperation.SelectedItem
        Dim classstatus As String = cboClass.SelectedItem
        Dim NSPSstatus As String = cboNSPS.SelectedItem
        Dim Part70Status As String = cboPart70.SelectedItem
        Dim programcodes As String
        Try
            lblEnrollYear.Text = feeYear
            SQL = "Select * " & _
            "from " & DBNameSpace & ".FeemailOut " & _
            "where INTYEAR = '" & feeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                MsgBox("That year is already being used." & vbCrLf & "If you want to use that year," & vbCrLf & "you must first delete that year from the database.")
            Else
                If cboMailoutYear.Text <> "" Then
                    If cboMailoutYear.Text.Length = 4 Then
                        SQL = "SELECT DISTINCT dt_40contact.strairsnumber,  " & _
                        "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
                        "APBFACILITYINFORMATION.STRFACILITYCITY,APBFACILITYINFORMATION.STRFACILITYSTATE, " & _
                        "APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
                        "dt_40contact.strclass,  " & _
                        "dt_40contact.stroperationalstatus,  " & _
                        "dt_40contact.datshutdowndate,  " & _
                        "dt_40contact.strairprogramcodes,  " & _
                        "dt_40contact.strkey,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactlastname  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactlastname  " & _
                        "ELSE ' '  " & _
                        "END) strcontactlastname,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactfirstname  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactfirstname  " & _
                        "END) strcontactfirstname,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactcompanyname  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactcompanyname  " & _
                        "END) STRCOMPANYNAME,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactemail  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactemail  " & _
                        "END) strcontactemail,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactaddress1  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactaddress1  " & _
                        "END) strcontactaddress,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactcity  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactcity  " & _
                        "END) strcontactcity,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactstate  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactstate  " & _
                        "END) strcontactstate,  " & _
                        "(CASE  " & _
                        "WHEN dt_40contact.strkey = '40' THEN dt_40contact.strcontactzipcode  " & _
                        "WHEN dt_40contact.strkey IS NULL THEN dt_30contact.strcontactzipcode  " & _
                        "END) STRCONTACTZIPCODE  " & _
                        "FROM " & _
                        "(SELECT DISTINCT dt_fees.strairsnumber,  " & _
                        "dt_fees.strclass,  " & _
                        "dt_fees.stroperationalstatus,  " & _
                        "dt_fees.datshutdowndate,  " & _
                        "dt_contact.strkey,  " & _
                        "dt_fees.strairprogramcodes,  " & _
                        "dt_contact.strcontactlastname,  " & _
                        "dt_contact.strcontactfirstname,  " & _
                        "dt_contact.strcontactcompanyname,  " & _
                        "dt_contact.strcontactemail,  " & _
                        "dt_contact.strcontactaddress1,  " & _
                        "dt_contact.strcontactcity,  " & _
                        "dt_contact.strcontactstate,  " & _
                        "dt_contact.strcontactzipcode  " & _
                        "FROM (SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'O' OR stroperationalstatus = 'P' OR stroperationalstatus = 'C' " & _
                        "or stroperationalstatus = 'I' or stroperationalstatus = 'T')  " & _
                        "AND (strclass = 'A' OR strclass = 'SM')  " & _
                        "UNION  " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'O' OR stroperationalstatus = 'P' OR stroperationalstatus = 'C' " & _
                        "or stroperationalstatus = 'I' or stroperationalstatus = 'T')  " & _
                        "AND (strairprogramcodes LIKE '_______1%' OR strairprogramcodes LIKE '____________1%')  " & _
                        "UNION  " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'X'  " & _
                        "AND (datshutdowndate > to_date('31-DEC-" & CInt(cboMailoutYear.Text) - 1 & "'))) AND  " & _
                        "(strclass = 'A' OR strclass = 'SM')  " & _
                        "UNION  " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'X'  " & _
                        "AND (datshutdowndate > to_date('31-DEC-" & CInt(cboMailoutYear.Text) - 1 & "'))) AND  " & _
                        "(strairprogramcodes LIKE '_______1%' OR strairprogramcodes LIKE '____________1%')) dt_fees,  " & _
                        "(SELECT *FROM " & DBNameSpace & ".apbcontactinformation  " & _
                        "WHERE strkey = 40) dt_contact  " & _
                        "WHERE dt_fees.strairsnumber = dt_contact.strairsnumber(+))  " & _
                        "dt_40contact,  " & _
                        "(SELECT DISTINCT dt_fees.strairsnumber,  " & _
                        "dt_fees.strclass,  " & _
                        "dt_fees.stroperationalstatus,  " & _
                        "dt_fees.datshutdowndate,  " & _
                        "dt_fees.strairprogramcodes,  " & _
                        "dt_contact.strcontactlastname,  " & _
                        "dt_contact.strcontactfirstname,  " & _
                        "dt_contact.strcontactcompanyname,  " & _
                        "dt_contact.strcontactemail,  " & _
                        "dt_contact.strcontactaddress1,  " & _
                        "dt_contact.strcontactcity,  " & _
                        "dt_contact.strcontactstate,  " & _
                        "dt_contact.strcontactzipcode  " & _
                        "FROM  " & _
                        "(SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'O' OR stroperationalstatus = 'P' OR stroperationalstatus = 'C' " & _
                        "or stroperationalstatus = 'I' or stroperationalstatus = 'T')  " & _
                        "AND (strclass = 'A' OR strclass = 'SM')  " & _
                        "UNION  " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'O' OR stroperationalstatus = 'P' OR stroperationalstatus = 'C' " & _
                        "or stroperationalstatus = 'I' or stroperationalstatus = 'T')  " & _
                        "AND (strairprogramcodes LIKE '_______1%' OR strairprogramcodes LIKE '____________1%')  " & _
                        "UNION  " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'X'  " & _
                        "AND(datshutdowndate > to_date('31-DEC-" & CInt(cboMailoutYear.Text) - 1 & "'))) AND  " & _
                        "(strclass = 'A' OR strclass = 'SM')  " & _
                        "UNION " & _
                        "SELECT * FROM " & DBNameSpace & ".apbheaderdata  " & _
                        "WHERE (stroperationalstatus = 'X'  " & _
                        "AND (datshutdowndate > to_date('31-DEC-" & CInt(cboMailoutYear.Text) - 1 & "'))) AND  " & _
                        "(strairprogramcodes LIKE '_______1%' OR strairprogramcodes LIKE '____________1%')) dt_fees,  " & _
                        "(SELECT * FROM " & DBNameSpace & ".apbcontactinformation  " & _
                        "WHERE strkey = 30) dt_contact  " & _
                        "WHERE dt_fees.strairsnumber = dt_contact.strairsnumber(+))  " & _
                        "dt_30contact, " & DBNameSpace & ".APBFACILITYINFORMATION  " & _
                        "WHERE dt_40contact.strairsnumber = dt_30contact.strairsnumber(+)  " & _
                        "AND dt_40contact.strairsnumber=" & DBNameSpace & ".APBFACILITYINFORMATION.strairsnumber (+)"

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Read()
                        Do
                            AirsNo = dr("strAirsNumber")
                            feeYear = cboMailoutYear.SelectedItem

                            If IsDBNull(dr("STRFACILITYNAME")) Then
                                FacilityName = " "
                            Else
                                FacilityName = dr("STRFACILITYNAME")
                            End If
                            If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                                operationalstatus = " "
                            Else
                                operationalstatus = dr("STROPERATIONALSTATUS")
                            End If
                            If IsDBNull(dr("STRCLASS")) Then
                                classstatus = " "
                            Else
                                classstatus = dr("STRCLASS")
                            End If
                            If IsDBNull(dr("DATSHUTDOWNDATE")) Then
                                shutdowndate = ""
                            Else
                                shutdowndate = Format(dr("DATSHUTDOWNDATE"), "dd-MMM-yyyy")
                            End If
                            If IsDBNull(dr("STRCOMPANYNAME")) Then
                                CompanyName = " "
                            Else
                                CompanyName = dr("STRCOMPANYNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTADDRESS")) Then
                                ContactAddress1 = " "
                            Else
                                ContactAddress1 = dr("STRCONTACTADDRESS")
                            End If
                            If IsDBNull(dr("STRCONTACTCITY")) Then
                                ContactCity = " "
                            Else
                                ContactCity = dr("STRCONTACTCITY")
                            End If
                            If IsDBNull(dr("STRCONTACTSTATE")) Then
                                contactState = " "
                            Else
                                contactState = dr("STRCONTACTSTATE")
                            End If
                            If IsDBNull(dr("STRCONTACTZIPCODE")) Then
                                ContactZip = " "
                            Else
                                ContactZip = dr("STRCONTACTZIPCODE")
                            End If
                            If IsDBNull(dr("STRCONTACTFIRSTNAME")) Then
                                FirstName = " "
                            Else
                                FirstName = dr("STRCONTACTFIRSTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTLASTNAME")) Then
                                LastName = " "
                            Else
                                LastName = dr("STRCONTACTLASTNAME")
                            End If
                            If IsDBNull(dr("STRCONTACTEMAIL")) Then
                                ContactEmail = " "
                            Else
                                ContactEmail = dr("STRCONTACTEMAIL")
                            End If
                            If IsDBNull(dr("STRFACILITYSTREET1")) Then
                                FacilityAddress1 = " "
                            Else
                                FacilityAddress1 = dr("STRFACILITYSTREET1")
                            End If
                            If IsDBNull(dr("STRFACILITYCITY")) Then
                                FacilityCity = " "
                            Else
                                FacilityCity = dr("STRFACILITYCITY")
                            End If
                            If IsDBNull(dr("STRFACILITYSTATE")) Then
                                FacilityState = " "
                            Else
                                FacilityState = dr("STRFACILITYSTATE")
                            End If
                            If IsDBNull(dr("STRFACILITYZIPCODE")) Then
                                FacilityZip = " "
                            Else
                                FacilityZip = dr("STRFACILITYZIPCODE")
                            End If
                            If IsDBNull(dr("strairprogramcodes")) Then
                                programcodes = " "
                            Else
                                programcodes = dr("strairprogramcodes")
                                If Mid(programcodes, 8, 1) = 1 Then
                                    NSPSstatus = "YES"
                                Else
                                    NSPSstatus = "NO"
                                End If

                                If Mid(programcodes, 13, 1) = 1 Then
                                    Part70Status = "YES"
                                Else
                                    Part70Status = "NO"
                                End If
                            End If

                            SQL2 = "Insert into " & DBNameSpace & ".FEEMAILOUT " & _
                            "(FEEMAILOUT.STRAIRSNUMBER, " & _
                            "FEEMAILOUT.INTYEAR, " & _
                            "FEEMAILOUT.STRFACILITYNAME, " & _
                            "FEEMAILOUT.STRFACILITYSTREET, " & _
                            "FEEMAILOUT.STRFACILITYCITY, " & _
                            "FEEMAILOUT.STRFACILITYSTATE, " & _
                            "FEEMAILOUT.STRFACILITYZIPCODE, " & _
                            "FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
                            "FEEMAILOUT.STRCONTACTLASTNAME, " & _
                            "FEEMAILOUT.STRCOMPANYNAME, " & _
                            "FEEMAILOUT.STRCONTACTADDRESS, " & _
                            "FEEMAILOUT.STRCONTACTCITY, " & _
                            "FEEMAILOUT.STRCONTACTSTATE, " & _
                            "FEEMAILOUT.STRCONTACTZIPCODE, " & _
                            "FEEMAILOUT.STRCONTACTEMAIL, " & _
                            "FEEMAILOUT.STROPERATIONALSTATUS, " & _
                            "FEEMAILOUT.DATSHUTDOWNDATE, " & _
                            "FEEMAILOUT.STRCLASS, " & _
                            "FEEMAILOUT.STRAPCPART70, " & _
                            "FEEMAILOUT.STRAPCNSPS) " & _
                            "values " & _
                            "('" & Replace(AirsNo, "'", "''") & "', " & _
                            "'" & Replace(feeYear, "'", "''") & "', " & _
                            "'" & Replace(FacilityName, "'", "''") & "', " & _
                            "'" & Replace(FacilityAddress1, "'", "''") & "', " & _
                            "'" & Replace(FacilityCity, "'", "''") & "', " & _
                            "'" & Replace(FacilityState, "'", "''") & "', " & _
                            "'" & Replace(FacilityZip, "'", "''") & "', " & _
                            "'" & Replace(FirstName, "'", "''") & "', " & _
                            "'" & Replace(LastName, "'", "''") & "', " & _
                            "'" & Replace(CompanyName, "'", "''") & "', " & _
                            "'" & Replace(ContactAddress1, "'", "''") & "', " & _
                            "'" & Replace(ContactCity, "'", "''") & "', " & _
                            "'" & Replace(contactState, "'", "''") & "', " & _
                            "'" & Replace(ContactZip, "'", "''") & "', " & _
                            "'" & Replace(ContactEmail, "'", "''") & "', " & _
                            "'" & Replace(operationalstatus, "'", "''") & "', " & _
                            "'" & Replace(shutdowndate, "'", "''") & "', " & _
                            "'" & Replace(classstatus, "'", "''") & "', " & _
                            "'" & Replace(Part70Status, "'", "''") & "', " & _
                            "'" & Replace(NSPSstatus, "'", "''") & "') "

                            cmd2 = New OracleCommand(SQL2, DBConn)
                            If DBConn.State = ConnectionState.Closed Then
                                DBConn.Open()
                            End If
                            dr2 = cmd2.ExecuteReader
                            dr2.Close()
                        Loop While dr.Read
                    End If

                    Dim year As Integer = CInt(cboMailoutYear.SelectedItem)
                    SQL = "SELECT feeMailOut.STRAIRSNUMBER, " & _
                    "feeMailOut.STRFACILITYNAME, " & _
                    "feeMailOut.STROPERATIONALSTATUS, " & _
                    "feeMailOut.STRCLASS, " & _
                    "feeMailOut.STRAPCNSPS, " & _
                    "FEEMAILOUT.STRAPCPART70, " & _
                    "feeMailOut.DATSHUTDOWNDATE, " & _
                    "feeMailOut.STRCONTACTFIRSTNAME, " & _
                    "feeMailOut.STRCONTACTLASTNAME, " & _
                    "feeMailOut.STRCOMPANYNAME, " & _
                    "feeMailOut.STRCONTACTADDRESS, " & _
                    "feeMailOut.STRCONTACTCITY, " & _
                    "feeMailOut.STRCONTACTSTATE, " & _
                    "feeMailOut.STRCONTACTZIPCODE, " & _
                    "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYSTREET, " & _
                    "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYCITY, " & _
                    "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYZIPCODE, " & _
                    "feeMailOut.STRCONTACTEMAIL " & _
                    "from " & DBNameSpace & ".feeMailOut " & _
                    "where feeMailOut.INTYEAR = '" & year & "' " & _
                    "order by feeMailOut.STRFACILITYNAME"

                    dsViewCount = New DataSet
                    daViewCount = New OracleDataAdapter(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    daViewCount.Fill(dsViewCount, "ViewCount")
                    dgvFeeDataCount.DataSource = dsViewCount
                    dgvFeeDataCount.DataMember = "ViewCount"
                    dgvFeeDataCount.RowHeadersVisible = False
                    dgvFeeDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                    dgvFeeDataCount.AllowUserToResizeColumns = True
                    dgvFeeDataCount.AllowUserToAddRows = False
                    dgvFeeDataCount.AllowUserToDeleteRows = False
                    dgvFeeDataCount.AllowUserToOrderColumns = True
                    dgvFeeDataCount.AllowUserToResizeRows = True

                    dgvFeeDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
                    dgvFeeDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
                    dgvFeeDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
                    dgvFeeDataCount.Columns("strFacilityName").DisplayIndex = 1
                    dgvFeeDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
                    dgvFeeDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
                    dgvFeeDataCount.Columns("STRCLASS").HeaderText = "Class"
                    dgvFeeDataCount.Columns("STRCLASS").DisplayIndex = 3
                    dgvFeeDataCount.Columns("STRAPCNSPS").HeaderText = "NSPS"
                    dgvFeeDataCount.Columns("STRAPCNSPS").DisplayIndex = 4
                    dgvFeeDataCount.Columns("STRAPCPART70").HeaderText = "TV Source"
                    dgvFeeDataCount.Columns("STRAPCPART70").DisplayIndex = 5
                    dgvFeeDataCount.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
                    dgvFeeDataCount.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
                    dgvFeeDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
                    dgvFeeDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 7
                    dgvFeeDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
                    dgvFeeDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 8
                    dgvFeeDataCount.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
                    dgvFeeDataCount.Columns("STRCOMPANYNAME").DisplayIndex = 9
                    dgvFeeDataCount.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
                    dgvFeeDataCount.Columns("STRCONTACTADDRESS").DisplayIndex = 10
                    dgvFeeDataCount.Columns("STRCONTACTCITY").HeaderText = "Contact City"
                    dgvFeeDataCount.Columns("STRCONTACTCITY").DisplayIndex = 11
                    dgvFeeDataCount.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
                    dgvFeeDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 12
                    dgvFeeDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
                    dgvFeeDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
                    dgvFeeDataCount.Columns("STRFACILITYSTREET").HeaderText = "Facility Street"
                    dgvFeeDataCount.Columns("STRFACILITYSTREET").DisplayIndex = 14
                    dgvFeeDataCount.Columns("STRFACILITYCITY").HeaderText = "Facility City"
                    dgvFeeDataCount.Columns("STRFACILITYCITY").DisplayIndex = 15
                    dgvFeeDataCount.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip"
                    dgvFeeDataCount.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
                    dgvFeeDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
                    dgvFeeDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 17

                    txtRecordNumber.Text = dgvFeeDataCount.RowCount.ToString
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
        MsgBox("Mailout list generated.", MsgBoxStyle.Information, "Mailout and Stats")
    End Sub
    Private Sub deletefeemailOutbyYear()
        Dim feeyear As Integer = CInt(cboMailoutYear.SelectedItem)
        Try

            SQL = "delete from " & DBNameSpace & ".feemailout " & _
            "where feemailout.INTYEAR = '" & feeyear & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dsViewCount = New DataSet
            dgvFeeDataCount.DataSource = dsViewCount
            txtRecordNumber.Text = " "
            'MsgBox("Fee mail out is deleted!", MsgBoxStyle.Information, "Mailout and Stats")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnGenerateFeeMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateFeeMailOut.Click
        Try
            GeneratefeeMailOut()
            cboMailoutYear.Items.Clear()
            loadMailOutYear()
            cboMailoutYear.Text = ""
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeleteFeeMailOut_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFeeMailOut.Click
        Try
            Dim intAnswer As Integer
            intAnswer = MsgBox("Delete the mailout?", MsgBoxStyle.OkCancel)
            If intAnswer = vbOK Then
                deletefeemailOutbyYear()
                cboMailoutYear.Items.Clear()
                loadMailOutYear()
                cboMailoutYear.Text = ""
            Else
                MsgBox("Fee mail out is not deleted!", MsgBoxStyle.Information, "Mailout and Stats")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewselectedyearMailOutlist_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewselectedyearMailOutlist.LinkClicked
        Try
            Dim year As Integer = CInt(cboMailoutYear.SelectedItem)
            lblEnrollYear.Text = year
            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER,  " & _
            "" & DBNameSpace & ".FEEMAILOUT.strFacilityName, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STROPERATIONALSTATUS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCLASS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRAPCNSPS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRAPCPART70, " & _
            "" & DBNameSpace & ".FEEMAILOUT.DATSHUTDOWNDATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYSTREET, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FEEMAILOUT " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "' " & _
            "order by " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount.DataSource = dsViewCount
            dgvFeeDataCount.DataMember = "ViewCount"

            dgvFeeDataCount.RowHeadersVisible = False
            dgvFeeDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount.AllowUserToResizeColumns = True
            dgvFeeDataCount.AllowUserToAddRows = False
            dgvFeeDataCount.AllowUserToDeleteRows = False
            dgvFeeDataCount.AllowUserToOrderColumns = True
            dgvFeeDataCount.AllowUserToResizeRows = True

            dgvFeeDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvFeeDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeDataCount.Columns("STRCLASS").HeaderText = "Class"
            dgvFeeDataCount.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeDataCount.Columns("STRAPCNSPS").HeaderText = "NSPS"
            dgvFeeDataCount.Columns("STRAPCNSPS").DisplayIndex = 4
            dgvFeeDataCount.Columns("STRAPCPART70").HeaderText = "TV Source"
            dgvFeeDataCount.Columns("STRAPCPART70").DisplayIndex = 5
            dgvFeeDataCount.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
            dgvFeeDataCount.Columns("DATSHUTDOWNDATE").DisplayIndex = 6
            dgvFeeDataCount.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 7
            dgvFeeDataCount.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount.Columns("STRCONTACTLASTNAME").DisplayIndex = 8
            dgvFeeDataCount.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount.Columns("STRCOMPANYNAME").DisplayIndex = 9
            dgvFeeDataCount.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount.Columns("STRCONTACTADDRESS").DisplayIndex = 10
            dgvFeeDataCount.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount.Columns("STRCONTACTCITY").DisplayIndex = 11
            dgvFeeDataCount.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount.Columns("STRCONTACTSTATE").DisplayIndex = 12
            dgvFeeDataCount.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount.Columns("STRCONTACTZIPCODE").DisplayIndex = 13
            dgvFeeDataCount.Columns("STRFACILITYSTREET").HeaderText = "Facility Street"
            dgvFeeDataCount.Columns("STRFACILITYSTREET").DisplayIndex = 14
            dgvFeeDataCount.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeDataCount.Columns("STRFACILITYCITY").DisplayIndex = 15
            dgvFeeDataCount.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zipcode"
            dgvFeeDataCount.Columns("STRFACILITYZIPCODE").DisplayIndex = 16
            dgvFeeDataCount.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount.Columns("STRCONTACTEMAIL").DisplayIndex = 17

            txtRecordNumber.Text = dgvFeeDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnroll.Click
        Try
            Mailoutenrollment()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub Mailoutenrollment()
        Dim AirsNo As String
        Dim feeYear As Integer = CInt(cboMailoutYear.SelectedItem)

        Try
            SQL = "Select * " & _
            "FROM " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            "where INTYEAR = '" & feeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read

            If recExist = True Then
                MsgBox("That year is already enrolled." & vbCrLf & "If you want to enroll the" & vbCrLf & " mailout, you must first unenroll that year from the database.")
            Else
                SQL = "Select feemailout.STRAIRSNUMBER " & _
                "FROM " & DBNameSpace & ".feemailout " & _
                "where feemailout.INTYEAR = '" & feeYear & "'"

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Read()
                Do
                    AirsNo = dr("strAirsNumber")
                    SQL2 = "Insert into " & DBNameSpace & ".FSPayandSubmit " & _
                    "(FSPayandSubmit.strairsnumber, " & _
                    "FSPayandSubmit.intyear, " & _
                    "FSPayandSubmit.strpaymenttype, " & _
                    "FSPayandSubmit.strofficialname, " & _
                    "FSPayandSubmit.strofficialtitle, " & _
                    "FSPayandSubmit.INTSUBMITTAL, " & _
                    "FSPayandSubmit.datesubmit) " & _
                    "values " & _
                    "('" & Replace(AirsNo, "'", "''") & "', " & _
                    "'" & Replace(feeYear, "'", "''") & "', " & _
                    "'N/A', " & _
                    "'N/A', " & _
                    "'N/A', " & _
                    "'0', " & _
                    "sysdate)"

                    cmd2 = New OracleCommand(SQL2, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr2 = cmd2.ExecuteReader
                    dr2.Close()
                Loop While dr.Read
                MsgBox("The facilities have been enrolled", MsgBoxStyle.Information, "Mailout and Stats")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnDeEnroll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeEnroll.Click
        Dim feeyear As Integer = CInt(lblEnrollYear.Text)
        Dim sql As String
        Try

            Dim intAnswer As Integer
            intAnswer = MsgBox("Remove the enrollment?", MsgBoxStyle.OkCancel)
            If intAnswer = vbOK Then
                sql = "delete from " & DBNameSpace & ".FSPAYANDSUBMIT " & _
                "where FSPAYANDSUBMIT.INTYEAR = '" & feeyear & "'"
                cmd = New OracleCommand(sql, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
                MsgBox("Enrollment has been removed!", MsgBoxStyle.Information, "Mailout and Stats")
            Else
                MsgBox("Enrollment has not been removed!", MsgBoxStyle.Information, "Mailout and Stats")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnRefreshAirsNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshAirsNo.Click
        Try
            loadfacilityinfo()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub loadfacilityinfo()
        Try
            Dim AirsNo As String = txtAirsNo.Text
            Dim feeYear As Integer = CInt(cboMailoutYear.SelectedItem)
            Dim programcodes As String

            SQL = "Select * " & _
            "from " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            "where APBFACILITYINFORMATION.STRAIRSNUMBER = '" & AirsNo & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STRFACILITYNAME")) Then
                    txtFacilityName.Text = ""
                Else
                    txtFacilityName.Text = dr("STRFACILITYNAME")
                End If
            End While
            dr.Close()

            SQL = "Select * " & _
            "from " & DBNameSpace & ".APBHEADERDATA " & _
            "where APBHEADERDATA.STRAIRSNUMBER = '" & AirsNo & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("STROPERATIONALSTATUS")) Then
                    cboOperation.SelectedItem = " "
                Else
                    cboOperation.SelectedItem = dr("STROPERATIONALSTATUS")
                End If
                If IsDBNull(dr("STRCLASS")) Then
                    cboClass.SelectedItem = " "
                Else
                    cboClass.SelectedItem = dr("STRCLASS")
                End If
                If IsDBNull(dr("DATSHUTDOWNDATE")) Then
                    txtShutdowndate.Text = ""
                Else
                    txtShutdowndate.Text = Format(dr("DATSHUTDOWNDATE"), "dd-MMM-yyyy")
                End If
                If IsDBNull(dr("strairprogramcodes")) Then
                    programcodes = " "
                Else
                    programcodes = dr("strairprogramcodes")
                    If Mid(programcodes, 8, 1) = 1 Then
                        cboNSPS.SelectedItem = "YES"
                    Else
                        cboNSPS.SelectedItem = "NO"
                    End If

                    If Mid(programcodes, 13, 1) = 1 Then
                        cboPart70.SelectedItem = "YES"
                    Else
                        cboPart70.SelectedItem = "NO"
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub TCMailoutAndStats_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TCMailoutAndStats.SelectedIndexChanged
        Try
            lblEnrollYear.Text = cboMailoutYear.SelectedItem
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
#End Region
#Region "Fee Stats"
    Private Sub loadYear()
        Try
            Dim year As String

            SQL = "Select distinct INTYEAR from " & DBNameSpace & ".FSCALCULATIONS order by INTYEAR desc"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Read()
            Do
                year = dr("INTYEAR")
                cboYear.Items.Add(year)
            Loop While dr.Read

            cboYear.SelectedIndex = 0

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            If txtfeeYear.Text <> "" Then
                runcount()
                lblfeeYear.Text = cboYear.SelectedItem
            Else
                MsgBox("Please select a year first.", MsgBoxStyle.Information, "Mailout and Stats")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub runcount()
        Dim Nonresponsecount As Integer
        Dim Removedcount As Integer
        Dim Truecount As Integer
        Dim MailoutCount As Integer
        Dim ResponseCount As Integer
        Dim ExtraResponseCount As Integer
        Dim MailoutFinalized As Integer
        Dim MailoutInProcess As Integer
        Dim ExtraFinalized As Integer
        Dim ExtraInprocess As Integer
        Dim TotalFinalized As Integer
        Dim TotalInprocess As Integer
        Dim Ontimecount As Integer
        Dim Latecount As Integer
        Dim TotalResponsecount As Integer
        Dim ExtraFacilities As Integer
        Dim ExtraNonResponser As Integer


        txtfeeYear.Text = cboYear.SelectedItem

        Dim FeeYear As Integer = CInt(txtfeeYear.Text)
        Dim deadlineyear As Integer = FeeYear + 1
        Dim deadline As String = String.Concat("01-SEP-", deadlineyear)

        Try

            SQL = "select count(*) as MailoutCount " & _
            "from " & DBNameSpace & ".feemailout " & _
            "where feemailout.INTYEAR = '" & FeeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtMailOutCount.Text = dr.Item(MailoutCount)
            End While
            dr.Close()

            SQL = "select count(*) as ResponseCount " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSCALCULATIONS " & _
            " where FSCALCULATIONS.STRAIRSNUMBER = FEEMAILOUT.STRAIRSNUMBER " & _
            " and FEEMAILOUT.STRAIRSNUMBER is not null" & _
            " and FEEMAILOUT.INTYEAr = FSCALCULATIONS.INTYEAr " & _
            " and FSCALCULATIONS.INTYEAR = '" & FeeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtResponseCount.Text = dr.Item(ResponseCount)
            End While
            dr.Close()

            SQL = "select count(*) as MailoutFinalized " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and " & DBNameSpace & ".FEEMAILOUT.intyear = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
            " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is not null" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtMailoutFinalized.Text = dr.Item(MailoutFinalized)
            End While
            dr.Close()

            SQL = "select count(*) as MailoutInProcess " & _
            "from " & DBNameSpace & ".FSCALCULATIONS left outer join " & DBNameSpace & ".FEEMAILOUT " & _
            " on " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER " & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAr, " & DBNameSpace & ".FSPAYANDSUBMIT" & _
            " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.intyear = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
            " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is not null" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtMailOutInProcess.Text = dr.Item(MailoutInProcess)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraresponseCount " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSCALCULATIONS " & _
            " where " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & FeeYear & "' " & _
            " and FSCALCULATIONS.INTYEAr = FEEMAILOUT.INTYEAr (+) " & _
            " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtextraResponse.Text = dr.Item(ExtraResponseCount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraFinalized " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' " & _
            " and FSPAYANDSUBMIT.INTYEAr = FEEMAILOUT.INTYEAr (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
            " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtExtraFinalized.Text = dr.Item(ExtraFinalized)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraInprocess " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSCALCULATIONS, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & FeeYear & "' " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER =" & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            "and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FSPAYANDSUBMIT.intyear " & _
            " and FSCALCULATIONS.INTYEAr = FEEMAILOUT.INTYEAr (+) " & _
            " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtExtraInProcess.Text = dr.Item(ExtraInprocess)
            End While
            dr.Close()

            SQL = "select count(*) as TotalResponsecount " & _
            "from " & DBNameSpace & ".FSCALCULATIONS " & _
            "where FSCALCULATIONS.INTYEAR = '" & FeeYear & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtTotalResponse.Text = dr.Item(TotalResponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as TotalFinalized " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' " & _
            " and FSPAYANDSUBMIT.INTSUBMITTAL = 1"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtTotalFinalizedCount.Text = dr.Item(TotalFinalized)
            End While
            dr.Close()

            txtTotalInProcessCount.Text = CInt(txtExtraInProcess.Text) + CInt(txtMailOutInProcess.Text)
            TotalInprocess = CInt(txtTotalInProcessCount.Text)

            SQL = "select count(*) as Ontimecount " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' " & _
            " and to_date(FSPAYANDSUBMIT.DATESUBMIT) < = '" & deadline & "'" & _
            " AND FSPAYANDSUBMIT.INTSUBMITTAL = 1"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtTotalincompliance.Text = dr.Item(Ontimecount)
            End While
            dr.Close()

            SQL = "select count(*) as Latecount " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' " & _
            " and to_date(FSPAYANDSUBMIT.DATESUBMIT) > '" & deadline & "'" & _
            " AND FSPAYANDSUBMIT.INTSUBMITTAL = 1 "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read()
                txtTotaloutofcompliance.Text = dr.Item(Latecount)
            End While
            dr.Close()

            SQL = "select count(*) as Nonresponsecount " & _
            "from " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            "   on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR " & _
            " where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & FeeYear & "'" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtNonResponseCount.Text = dr.Item(Nonresponsecount)
            End While
            dr.Close()

            SQL = "select count(*) as Removedcount " & _
            "from " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            "   on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & FeeYear & "'" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER is null"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtRemovedFacilities.Text = dr.Item(Removedcount)
            End While
            dr.Close()

            SQL = "select count(*) as Truecount " & _
            "from " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            "   on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR, " & DBNameSpace & ".FSPAYANDSUBMIT  " & _
            " where " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER" & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & FeeYear & "'" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtTrueNonResponsers.Text = dr.Item(Truecount)
            End While
            dr.Close()

            SQL = "select count(*) as ExtraFacilities " & _
           "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT " & _
           " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' " & _
           " and FSPAYANDSUBMIT.INTYEAr = FEEMAILOUT.INTYEAr (+) " & _
           " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
           " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtextrafacilities.Text = dr.Item(ExtraFacilities)
            End While
            dr.Close()


            SQL = "select count(*) as ExtraNonResponser " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " where not exists (select * from " & DBNameSpace & ".FSCALCULATIONS " & _
            "where " & DBNameSpace & ".FSPAYANDSUBMIT.strAIRSnumber = " & DBNameSpace & ".FSCalculations.strAIRSnumber " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAr = " & DBNameSpace & ".FSCALCULATIONS.INTYEAr)" & _
            " and  not exists (select * from " & DBNameSpace & ".FEEMAILOUT " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER" & _
            " and FSPAYANDSUBMIT.INTYEAr = FEEMAILOUT.INTYEAr) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & FeeYear & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            While dr.Read()
                txtExtraNonResponse.Text = dr.Item(ExtraNonResponser)
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblViewMailOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailOut.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "' " & _
            "and APBFACILITYINFORMATION.STRAIRSNUMBER(+) = FeeMailOut. STRAIRSNUMBER " & _
            "order by " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRFACILITYCITY").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRFACILITYCITY").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").DisplayIndex = 11
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 12

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailOutCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewNonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewNonResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            " on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR " & _
            " where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null " & _
            "and APBFACILITYINFORMATION.STRAIRSNUMBER = FeeMailOut. STRAIRSNUMBER"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").HeaderText = "FACILITY STREET"
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRFACILITYCITY").HeaderText = "FACILITY CITY"
            dgvFeeDataCount2.Columns("STRFACILITYCITY").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").HeaderText = "FACILITY ZIP"
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").DisplayIndex = 11
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 12

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtNonResponseCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewRemovedFacilities_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewRemovedFacilities.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYSTREET1, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from  " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER is null " & _
            "and APBFACILITYINFORMATION.STRAIRSNUMBER = FeeMailOut. STRAIRSNUMBER"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").HeaderText = "Facility Street"
            dgvFeeDataCount2.Columns("STRFACILITYSTREET1").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRFACILITYCITY").HeaderText = "FACILITY CITY"
            dgvFeeDataCount2.Columns("STRFACILITYCITY").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").HeaderText = "FACILITY ZIP"
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").DisplayIndex = 11
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 12

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtRemovedFacilities.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewTrueNonresponsers_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTrueNonresponsers.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            'Changed the SQL statement on 2/10/2010 per B.Gregory request based on J.Capp request.

            'SQL = "SELECT " & connNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRFACILITYNAME, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRFACILITYSTREET, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRFACILITYCITY, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRFACILITYZIPCODE, " & _
            '"" & connNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            '"from " & connNameSpace & ".FEEMAILOUT left outer join " & connNameSpace & ".FSCALCULATIONS " & _
            '" on " & connNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & connNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            '" and  " & connNameSpace & ".FEEMAILOUT.INTYEAR = " & connNameSpace & ".FSCALCULATIONS.INTYEAR, " & connNameSpace & ".FSPAYANDSUBMIT  " & _
            '" where " & connNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & connNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER" & _
            '" and " & connNameSpace & ".FEEMAILOUT.INTYEAR = " & connNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            '" and " & connNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            '" and " & connNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null"


            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYSTREET, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL, " & _
            "" & DBNameSpace & ".FeeMailOut.strOperationalStatus, " & _
            "" & DBNameSpace & ".FeeMailOut.DatShutDownDate, " & _
            "" & DBNameSpace & ".FeeMailOut.strClass, " & _
            "" & DBNameSpace & ".FeeMailOut.strAPCPart70, " & _
            "" & DBNameSpace & ".FeeMailOut.strAPCNSPS " & _
            "from " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            " on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR, " & DBNameSpace & ".FSPAYANDSUBMIT  " & _
            " where " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER" & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null" & _
            " order by " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER "

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRFACILITYSTREET").HeaderText = "Facility Address"
            dgvFeeDataCount2.Columns("STRFACILITYSTREET").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRFACILITYCITY").HeaderText = "Facility City"
            dgvFeeDataCount2.Columns("STRFACILITYCITY").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").HeaderText = "Facility Zip Code"
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").DisplayIndex = 11
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 12
            dgvFeeDataCount2.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvFeeDataCount2.Columns("STROPERATIONALSTATUS").DisplayIndex = 13
            dgvFeeDataCount2.Columns("DATSHUTDOWNDATE").HeaderText = "Shut Down Date"
            dgvFeeDataCount2.Columns("DATSHUTDOWNDATE").DisplayIndex = 14
            dgvFeeDataCount2.Columns("DATSHUTDOWNDATE").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvFeeDataCount2.Columns("STRCLASS").HeaderText = "Classification"
            dgvFeeDataCount2.Columns("STRCLASS").DisplayIndex = 15
            dgvFeeDataCount2.Columns("STRAPCPART70").HeaderText = "Part 70"
            dgvFeeDataCount2.Columns("STRAPCPART70").DisplayIndex = 16
            dgvFeeDataCount2.Columns("STRAPCNSPS").HeaderText = "NSPS"
            dgvFeeDataCount2.Columns("STRAPCNSPS").DisplayIndex = 17

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTrueNonResponsers.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewMailoutFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailoutFinalized.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER  = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear = " & DBNameSpace & ".FEEMAILOUT.intyear " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Date Submit"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 10

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailoutFinalized.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewMailoutInprocess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewMailoutInprocess.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
             "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
             "from " & DBNameSpace & ".FSCALCULATIONS left outer join " & DBNameSpace & ".FEEMAILOUT " & _
              " on " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR, " & DBNameSpace & ".FSPAYANDSUBMIT " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.intyear = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is not null" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailOutInProcess.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewINCompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewINCompliance.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)
            Dim deadlineyear As Integer = year + 1
            Dim deadline As String = String.Concat("01-SEP-", deadlineyear)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = " & DBNameSpace & ".FEEMAILOUT.INTYEAR (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+) " & _
            " and to_date(" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT) <= '" & deadline & "'" & _
            " AND " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Date Submit"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalincompliance.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewOutofcompliance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewOutofcompliance.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)
            Dim deadlineyear As Integer = year + 1
            Dim deadline As String = String.Concat("01-SEP-", deadlineyear)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FSCONTACTINFO,  " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = " & DBNameSpace & ".FEEMAILOUT.INTYEAR (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+)" & _
            " and to_date(" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT) > '" & deadline & "'" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER is not null" & _
            " AND " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Date Submit"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 10
            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotaloutofcompliance.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewTotalResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FSCALCULATIONS," & DBNameSpace & ".FSCONTACTINFO, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.intyear = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER =" & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FSPAYANDSUBMIT.intyear (+) " & _
            " and " & DBNameSpace & ".FSCalculations.intyear = " & DBNameSpace & ".FSContactInfo.intyear (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Date Submit"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").HeaderText = "Contact Phone Number"
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 11

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewTotalFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalFinalized.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FSCONTACTINFO, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear = " & DBNameSpace & ".FSCONTACTINFO.intyear (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Date Submit"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").HeaderText = "Contact Phone Number"
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 11

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalFinalizedCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewTotalInProcess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewTotalInProcess.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FSCALCULATIONS," & DBNameSpace & ".FSCONTACTINFO, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.intyear = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER =" & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FSPAYANDSUBMIT.intyear (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Last Login"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Contact Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "Contact City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "Contact State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Contact Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").HeaderText = "Contact Phone Number"
            dgvFeeDataCount2.Columns("STRCONTACTPHONENUMBER").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 11

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalInProcessCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblextraResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblextraResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
              "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSCALCULATIONS, " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSCONTACTINFO " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "' " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)" & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
              " and " & DBNameSpace & ".FSCalculations.intyear = " & DBNameSpace & ".FSContactInfo.intyear (+) " & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtextraResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraFinalized.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)


            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
              "" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT " & _
              "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
               " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
               " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
               " and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
               " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
               " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1" & _
                                   " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Last log in"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtExtraFinalized.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewExtraInProcess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewExtraInProcess.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
              " " & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT " & _
              " from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSCALCULATIONS, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "' " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear = '" & year & "' " & _
              " and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER =" & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0" & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("DATESUBMIT").HeaderText = "Last log in"
            dgvFeeDataCount2.Columns("DATESUBMIT").DisplayIndex = 2

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtExtraInProcess.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub BtnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportExcel2.Click, BtnExportExcel.Click
        Try
            ExporttoExcel()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub ExporttoExcel()
        Try
            'Dim ExcelApp As New Excel.Application
            Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
            'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
            Dim i As Integer
            Dim j As Integer

            If dgvFeeDataCount.RowCount <> 0 Then

                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    For i = 0 To dgvFeeDataCount.ColumnCount - 1
                        .Cells(1, i + 1) = dgvFeeDataCount.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvFeeDataCount.ColumnCount - 1
                        For j = 0 To dgvFeeDataCount.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvFeeDataCount.Item(i, j).Value.ToString
                        Next
                    Next
                End With
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTFIRSTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTLASTNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCOMPANYNAME, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTADDRESS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTSTATE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYSTREET, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYCITY, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRFACILITYZIPCODE, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCONTACTEMAIL " & _
            "from " & DBNameSpace & ".FEEMAILOUT " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "' " & _
            "order by " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRFACILITYSTREET").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRFACILITYSTREET").DisplayIndex = 9
            dgvFeeDataCount2.Columns("STRFACILITYCITY").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRFACILITYCITY").DisplayIndex = 10
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRFACILITYZIPCODE").DisplayIndex = 11
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 12

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailOutCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblviewsumarryMailout_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewsumarryMailout.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "' " & _
            "and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER(+) = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER " & _
            "order by " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1
            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailOutCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub lblviewSumNonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumNonResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            "on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            "and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            "and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null " & _
            "and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtNonResponseCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumRemovedFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumRemovedFacility.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            " " & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
            " from  " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSPAYANDSUBMIT " & _
            " on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER is null " & _
            " and " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtRemovedFacilities.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewsumTrueNonresponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewsumTrueNonresponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
            " " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME " & _
            " from " & DBNameSpace & ".FEEMAILOUT left outer join " & DBNameSpace & ".FSCALCULATIONS " & _
            " on " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER " & _
            " and  " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR, " & DBNameSpace & ".FSPAYANDSUBMIT  " & _
            " where " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER" & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR " & _
            " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "'" & _
            " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER is null"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("strFacilityName").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTrueNonResponsers.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumFinalized.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
              "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
              " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER  = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailoutFinalized.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewSumInProcess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewSumInProcess.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER, " & _
              " " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME " & _
              " from " & DBNameSpace & ".FSCALCULATIONS left outer join " & DBNameSpace & ".FEEMAILOUT " & _
              " on " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FEEMAILOUT.INTYEAR = " & DBNameSpace & ".FSCALCULATIONS.INTYEAR, " & DBNameSpace & ".FSPAYANDSUBMIT " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = " & DBNameSpace & ".FSPAYANDSUBMIT.intyear " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is not null" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtMailOutInProcess.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewSumExtraResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewSumExtraResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
              "from " & DBNameSpace & ".FSCALCULATIONS, " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "' " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)" & _
              " and " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtextraResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumTotalResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumTotalResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".FSCALCULATIONS, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumExtraToalFinalized_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumExtraToalFinalized.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
            "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
             " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1 " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalFinalizedCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumtotalInporcess_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumtotalInporcess.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
              "from " & DBNameSpace & ".FSCALCULATIONS, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
              " where " & DBNameSpace & ".FSCALCULATIONS.STRAIRSNUMBER =" & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FSCALCULATIONS.intyear =" & DBNameSpace & ".FSPAYANDSUBMIT.intyear (+) " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 0 " & _
              " and " & DBNameSpace & ".FSCALCULATIONS.INTYEAR = '" & year & "'"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1


            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotalInProcessCount.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewSumLateresponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewSumLateresponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)
            Dim deadlineyear As Integer = year + 1
            Dim deadline As String = String.Concat("01-SEP-", deadlineyear)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
            "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
             "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".FEEMAILOUT, " & _
             "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSContactInfo " & _
            " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = " & DBNameSpace & ".FEEMAILOUT.INTYEAR (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER (+) " & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+)" & _
            " and to_date(" & DBNameSpace & ".FSPAYANDSUBMIT.DATESUBMIT) > '" & deadline & "'" & _
            " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER is not null" & _
            " AND " & DBNameSpace & ".FSPAYANDSUBMIT.INTSUBMITTAL = 1"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "FACILITY NAME"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtTotaloutofcompliance.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Web App Tools"
#Region "Web Application Users"

    Private Sub btnActivateEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateEmail.Click
        Try
            LoadComboBoxesEmail()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadComboBoxesEmail()
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Dim SQL As String

        Try


            SQL = "Select numuserid, struseremail " _
            + "from " & DBNameSpace & ".OlapUserLogin " _
            + "Order by struseremail "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Open Then
            Else
                DBConn.Open()
            End If

            da.Fill(ds, "UserEmail")

            dtAIRS.Columns.Add("numuserid", GetType(System.String))
            dtAIRS.Columns.Add("struseremail", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("numuserid") = " "
            drNewRow("struseremail") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("UserEmail").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("numuserid") = drDSRow("numuserid")
                drNewRow("struseremail") = drDSRow("struseremail")
                dtAIRS.Rows.Add(drNewRow)
            Next
            Dim temp As String

            temp = dtAIRS.Rows.Count

            With cboUserEmail
                .DataSource = dtAIRS
                .DisplayMember = "struseremail"
                .ValueMember = "numuserid"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
#End Region
#Region "Mahesh Code for Web App Users"

    Sub FormatDataGridForWorkEnTry()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            Dim objbooleancol As New DataGridBoolColumn

            dgrUsers.TableStyles.Clear()
            'objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblFacilityUser"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True

            'Formatting our DataGrid 0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "ID"
            objtextcol.HeaderText = "ID"
            objtextcol.Width = 1
            objtextcol.ReadOnly = True
            objGrid.GridColumnStyles.Add(objtextcol)

            'Formatting our DataGrid 1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "Email"
            objtextcol.HeaderText = "Email Address"
            objtextcol.ReadOnly = True
            objtextcol.Width = 300
            objGrid.GridColumnStyles.Add(objtextcol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intAdminAccess"
            objbooleancol.HeaderText = "Admin Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intFeeAccess"
            objbooleancol.HeaderText = "Fee Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intEIAccess"
            objbooleancol.HeaderText = "EI Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intESAccess"
            objbooleancol.HeaderText = "ES Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            'Applying the above formating 
            'dgrUsers.TableStyles.Clear()
            dgrUsers.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrUsers.CaptionText = "Current Users for " & lblFacilityName.Text
            dgrUsers.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatDataGridForWorkEnTryEmail()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            Dim objbooleancol As New DataGridBoolColumn

            dgrFacilities.TableStyles.Clear()
            'objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "tblUserFacility"
            objGrid.RowHeadersVisible = False
            objGrid.AllowSorting = True

            'Formatting our DataGrid 0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strairsnumber"
            objtextcol.HeaderText = "Airs Number"
            objtextcol.Width = 110
            objtextcol.ReadOnly = True
            objGrid.GridColumnStyles.Add(objtextcol)

            'Formatting our DataGrid 1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strfacilityname"
            objtextcol.HeaderText = "Facility Name"
            objtextcol.ReadOnly = True
            objtextcol.Width = 220
            objGrid.GridColumnStyles.Add(objtextcol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intAdminAccess"
            objbooleancol.HeaderText = "Admin Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intFeeAccess"
            objbooleancol.HeaderText = "Fee Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intEIAccess"
            objbooleancol.HeaderText = "EI Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            objbooleancol = New DataGridBoolColumn
            objbooleancol.MappingName = "intESAccess"
            objbooleancol.HeaderText = "ES Access"
            objbooleancol.ReadOnly = False
            objbooleancol.Width = 100
            objbooleancol.FalseValue = "False"
            objbooleancol.TrueValue = "True"
            'objbooleancol.AllowNull = False 'For a two-state checkbox
            objGrid.GridColumnStyles.Add(objbooleancol)

            'Applying the above formating 
            'dgrUsers.TableStyles.Clear()
            dgrFacilities.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrFacilities.CaptionText = "Current Facilities for " & txtWebUserEmail.Text
            dgrFacilities.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewFacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewFacility.LinkClicked
        Try
            LoadDataGridFacility(cboUserEmail.Text)
            LoadUserInfo(cboUserEmail.Text)
            FormatDataGridForWorkEnTryEmail()

            If txtWebUserID.Text = "" Then
                pnlUserInfo.Visible = False
                pnlUserFacility.Visible = False
            Else
                pnlUserInfo.Visible = True
                pnlUserFacility.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Sub LoadDataGrid()
        Dim SQL As String = ""
        Try
            dsWorkEntry = New DataSet
            If Not dsWorkEntry.Tables("tblFacilityUser") Is Nothing Then
                dsWorkEntry.Tables("tblFacilityUser").Clear()
                dsWorkEntry.Tables.Remove("tblFacilityUser")
                dsWorkEntry.AcceptChanges()
            End If

            SQL = "SELECT " & DBNameSpace & ".OlapUserAccess.NumUserID as ID, " & DBNameSpace & ".OlapUserLogin.numuserid, " & _
                    "" & DBNameSpace & ".OlapUserLogin.strUserEmail as Email, " & _
                    "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
                    "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
                    "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
                    "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
                    "FROM " & DBNameSpace & ".OlapUserAccess, " & DBNameSpace & ".OlapUserLogin " & _
                    "WHERE " & DBNameSpace & ".OLAPUserAccess.NumUserId = " & DBNameSpace & ".OlapUserLogin.NumUserID " & _
                    "AND " & DBNameSpace & ".OlapUserAccess.strAirsNumber = '0413" & airsno & "' order by email"

            daWorkEntry = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daWorkEntry.Fill(dsWorkEntry, "tblFacilityUser")

            dgrUsers.DataSource = dsWorkEntry
            dgrUsers.DataMember = "tblFacilityUser"

            cboUsers.DataSource = dsWorkEntry.Tables("tblFacilityUser")
            cboUsers.DisplayMember = "Email"
            cboUsers.ValueMember = "ID"

            'USER_ this little section removes the Append Row from
            ' the end of the datagrid
            Dim cm As CurrencyManager = Me.BindingContext(Me.dgrUsers.DataSource, Me.dgrUsers.DataMember)

            Dim dv As DataView = cm.List
            dv.AllowNew = False
            'End remove append Row

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadDataGridFacility(ByVal EmailLoc As String)
        Dim SQL As String = ""
        Try
            dsWorkEntry = New DataSet
            If Not dsWorkEntry.Tables("tblUserFacility") Is Nothing Then
                dsWorkEntry.Tables("tblUserFacility").Clear()
                dsWorkEntry.Tables.Remove("tblUserFacility")
                dsWorkEntry.AcceptChanges()
            End If

            SQL = "SELECT strairsnumber, strfacilityname, " & _
                    "Case When intAdminAccess = 0 Then 'False' When intAdminAccess = 1 Then 'True' End as intAdminAccess, " & _
                    "Case When intFeeAccess = 0 Then 'False' When intFeeAccess = 1 Then 'True' End as intFeeAccess, " & _
                    "Case When intEIAccess = 0 Then 'False' When intEIAccess = 1 Then 'True' End as intEIAccess, " & _
                    "Case When intESAccess = 0 Then 'False' When intESAccess = 1 Then 'True' End as intESAccess " & _
                    "FROM " & DBNameSpace & ".OlapUserAccess, " & DBNameSpace & ".OLAPUserLogIn  " & _
                    "WHERE " & DBNameSpace & ".OlapUserAccess.numUserId = " & DBNameSpace & ".OLAPUserLogIn.numUserId " & _
                    "and  strUserEmail = upper('" & EmailLoc & "') " & _
                    "order by strfacilityname"

            daWorkEntry = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daWorkEntry.Fill(dsWorkEntry, "tblUserFacility")

            dgrFacilities.DataSource = dsWorkEntry
            dgrFacilities.DataMember = "tblUserFacility"

            cboFacilityToDelete.DataSource = dsWorkEntry.Tables("tblUserFacility")
            cboFacilityToDelete.DisplayMember = "strfacilityname"
            cboFacilityToDelete.ValueMember = "strairsnumber"

            'USER_ this little section removes the Append Row from
            ' the end of the datagrid
            Dim cm As CurrencyManager = Me.BindingContext(Me.dgrFacilities.DataSource, Me.dgrFacilities.DataMember)

            Dim dv As DataView = cm.List
            dv.AllowNew = False
            'End remove append Row

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadUserInfo(ByVal UserData As String)
        Try
            SQL = "Select " & _
            "" & DBNameSpace & ".OLAPUserProfile.numUserID, " & _
            "strfirstname, strlastname, " & _
            "strtitle, strcompanyname, " & _
            "straddress, strcity, " & _
            "strstate, strzip, " & _
            "strphonenumber, strfaxnumber, " & _
            "datLastLogIn, strConfirm, " & _
            "strUserEmail " & _
            "from " & DBNameSpace & ".OlapUserProfile, " & DBNameSpace & ".OLAPUserLogIn " & _
            "where " & DBNameSpace & ".OLAPUserProfile.numUserID = " & DBNameSpace & ".OLAPUserLogIn.numuserid " & _
            "and strUserEmail = upper('" & UserData & "') "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                If IsDBNull(dr.Item("numUserID")) Then
                    txtWebUserID.Clear()
                Else
                    txtWebUserID.Text = dr.Item("numUserID")
                End If
                If IsDBNull(dr.Item("strfirstname")) Then
                    txtEditFirstName.Clear()
                Else
                    lblFName.Text = "First Name: " & dr.Item("strfirstname")
                    txtEditFirstName.Text = dr.Item("strFirstName")
                End If
                If IsDBNull(dr.Item("strlastname")) Then
                    txtEditLastName.Clear()
                Else
                    lblLName.Text = "Last Name: " & dr.Item("strlastname")
                    txtEditLastName.Text = dr.Item("strLastName")
                End If
                If IsDBNull(dr.Item("strtitle")) Then
                    txtEditTitle.Clear()
                Else
                    lblTitle.Text = "Title: " & dr.Item("strtitle")
                    txtEditTitle.Text = dr.Item("strTitle")
                End If
                If IsDBNull(dr.Item("strcompanyname")) Then
                    txtEditCompany.Clear()
                Else
                    lblCoName.Text = "Company Name: " & dr.Item("strcompanyname")
                    txtEditCompany.Text = dr.Item("strCompanyName")
                End If
                If IsDBNull(dr.Item("straddress")) Then
                    txtEditAddress.Clear()
                Else
                    lblAddress.Text = dr.Item("straddress")
                    txtEditAddress.Text = dr.Item("strAddress")
                End If
                If IsDBNull(dr.Item("strcity")) Then
                    txtEditCity.Clear()
                    mtbEditState.Clear()
                    mtbEditZipCode.Clear()
                Else
                    lblCityStateZip.Text = dr.Item("strcity") & ", " & dr.Item("strstate") & " " & dr.Item("strzip")
                    txtEditCity.Text = dr.Item("strCity")
                    mtbEditState.Text = dr.Item("strState")
                    mtbEditZipCode.Text = dr.Item("strZip")
                End If
                If IsDBNull(dr.Item("strphonenumber")) Then
                    mtbEditPhoneNumber.Clear()
                Else
                    lblPhoneNo.Text = "Phone Number: " & dr.Item("strphonenumber")
                    mtbEditPhoneNumber.Text = dr.Item("strPhoneNumber")
                End If
                If IsDBNull(dr.Item("strfaxnumber")) Then
                    mtbEditFaxNumber.Clear()
                Else
                    lblFaxNo.Text = "Fax Number: " & dr.Item("strfaxnumber")
                    mtbEditFaxNumber.Text = dr.Item("strFaxNumber")
                End If
                If IsDBNull(dr.Item("strConfirm")) Then
                    lblConfirmDate.Text = ""
                Else
                    lblConfirmDate.Text = "Date User Email Confirmed: " & dr.Item("strConfirm")
                End If
                If IsDBNull(dr.Item("datLastLogIn")) Then
                    lblLastLogIn.Text = ""
                Else
                    lblLastLogIn.Text = "Date User Last Logged In: " & dr.Item("datLastLogIn")
                End If
                If IsDBNull(dr.Item("strUserEmail")) Then
                    txtEditEmail.Text = ""
                Else
                    txtEditEmail.Text = dr.Item("strUserEmail")
                End If
            Else
                txtWebUserID.Clear()
                txtEditFirstName.Clear()
                txtEditLastName.Clear()
                txtEditTitle.Clear()
                txtEditCompany.Clear()
                txtEditAddress.Clear()
                txtEditCity.Clear()
                mtbEditState.Clear()
                mtbEditZipCode.Clear()
                mtbEditPhoneNumber.Clear()
                mtbEditFaxNumber.Clear()
                lblLastLogIn.Text = ""
                lblConfirmDate.Text = ""
                txtEditEmail.Clear()
            End If

            txtEditUserPassword.Clear()
            txtEditFirstName.Visible = False
            txtEditLastName.Visible = False
            txtEditTitle.Visible = False
            txtEditCompany.Visible = False
            txtEditAddress.Visible = False
            txtEditCity.Visible = False
            mtbEditState.Visible = False
            mtbEditZipCode.Visible = False
            mtbEditPhoneNumber.Visible = False
            mtbEditFaxNumber.Visible = False
            btnSaveEditedData.Visible = False
            txtEditUserPassword.Visible = False
            btnChangeEmailAddress.Visible = False
            txtEditEmail.Visible = False
            btnUpdatePassword.Visible = False

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub Back()
        Try
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim dt As DataTable
            Dim userId As Integer = -1
            Dim adminaccess As Boolean = False
            Dim feeaccess As Boolean = False
            Dim eiaccess As Boolean = False
            Dim esaccess As Boolean = False

            dt = dsWorkEntry.Tables("tblFacilityUser").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 3 To 6
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        userId = Row(0, DataRowVersion.Original)
                                        adminaccess = Row(3, DataRowVersion.Current)
                                        feeaccess = Row(4, DataRowVersion.Current)
                                        eiaccess = Row(5, DataRowVersion.Current)
                                        esaccess = Row(6, DataRowVersion.Current)
                                        If userId = -1 Then
                                            MsgBox("There was an error while saving please try to resave." & vbCrLf & _
                                                   "You may have to close the window and reopen if it does not work after a few tries.", _
                                                    MsgBoxStyle.Exclamation, "Mailout and Stats")
                                            Exit Sub
                                        End If
                                        UpdateRecords(userId, adminaccess, feeaccess, eiaccess, esaccess)
                                        'Exit For
                                    End If
                                End If
                            Next

                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub UpdateRecords(ByVal userid As Object, ByVal adminaccess As Object, ByVal feeaccess As Object, ByVal eiaccess As Object, ByVal esaccess As Object)

        Dim admin, fee, ei, es As Integer
        If adminaccess = True Then
            admin = 1
        Else
            admin = 0
        End If
        If feeaccess = True Then
            fee = 1
        Else
            fee = 0
        End If
        If eiaccess = True Then
            ei = 1
        Else
            ei = 0
        End If
        If esaccess = True Then
            es = 1
        Else
            es = 0
        End If

        Try
            SQL = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & userid & "' " & _
                      "and strAirsNumber = '0413" & airsno & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub UpdateRecordsEmail(ByVal airsnumber As Object, ByVal adminaccess As Object, ByVal feeaccess As Object, ByVal eiaccess As Object, ByVal esaccess As Object)

        Dim admin, fee, ei, es As Integer
        If adminaccess = True Then
            admin = 1
        Else
            admin = 0
        End If
        If feeaccess = True Then
            fee = 1
        Else
            fee = 0
        End If
        If eiaccess = True Then
            ei = 1
        Else
            ei = 0
        End If
        If esaccess = True Then
            es = 1
        Else
            es = 0
        End If

        Try
            SQL = "UPDATE " & DBNameSpace & ".OlapUserAccess " & _
                      "SET intadminaccess = '" & admin & "', " & _
                      "intFeeAccess = '" & fee & "', " & _
                      "intEIAccess = '" & ei & "', " & _
                      "intESAccess = '" & es & "' " & _
                      "WHERE numUserID = '" & cboUserEmail.SelectedValue & "' " & _
                      "and strAirsNumber = '0413" & airsnumber & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddFacilitytoUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacilitytoUser.Click
        Try
            If txtWebUserID.Text <> "" And mtbFacilityToAdd.Text <> "" Then
                Dim InsertString As String = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
                "(numUserId, strAirsNumber, strFacilityName) values( " & _
                "'" & txtWebUserID.Text & "', '0413" & mtbFacilityToAdd.Text & "', " & _
                "(select strFacilityName from " & DBNameSpace & ".APBFacilityInformation where strAIRSnumber = '0413" & mtbFacilityToAdd.Text & "')) "

                Dim cmd As New OracleCommand(InsertString, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                cmd.ExecuteNonQuery()

                dgrFacilities.Refresh()
                MsgBox("The facility has beed added to this user", MsgBoxStyle.Information, "Insert Success!")
                LoadDataGridFacility(txtWebUserEmail.Text)

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteFacilityUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFacilityUser.Click
        Try
            If txtWebUserID.Text <> "" And cboFacilityToDelete.Text <> "" Then
                Dim deleteString As String = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
                "WHERE numUserID = '" & txtWebUserID.Text & "' " & _
                "and strAirsNumber = '" & cboFacilityToDelete.SelectedValue & "' "

                Dim cmd As New OracleCommand(deleteString, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                cmd.ExecuteNonQuery()

                MsgBox("The facility has been removed for this user", MsgBoxStyle.Information, "Facility Removed!")
                LoadDataGridFacility(txtWebUserEmail.Text)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim userID As Integer
            Dim sql As String = "Select numUserId from " & DBNameSpace & ".olapuserlogin " & _
            "where struseremail = '" & Replace(UCase(txtEmail.Text), "'", "''") & "' "
            Dim cmd As New OracleCommand
            Dim dr As OracleDataReader
            Dim recexist As Boolean

            cmd = New OracleCommand(sql, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader

            recexist = dr.Read

            If recexist = True Then 'Email address is registered
                userID = dr.Item("numUserId")
                Dim InsertString As String = "Insert into " & DBNameSpace & ".OlapUserAccess " & _
                "(numUserId, strAirsNumber, strFacilityName) values( " & _
                "'" & userID & "', '0413" & airsno & "', '" & Replace(lblFacilityName.Text, "'", "''") & "') "

                Dim cmd1 As New OracleCommand(InsertString, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                cmd1.ExecuteNonQuery()

                dgrUsers.Refresh()
                MsgBox("The User has beed added to this facility", MsgBoxStyle.Information, "Insert Success!")
                LoadDataGrid()
            Else 'email address not registered
                MsgBox("This Email Address is not registered", MsgBoxStyle.OkOnly, "Insert Failed!")
            End If

            If dr.IsClosed = False Then dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim deleteString As String = "DELETE " & DBNameSpace & ".OlapUserAccess " & _
                                "WHERE numUserID = '" & cboUsers.SelectedValue & "' " & _
                                "and strAirsNumber = '0413" & airsno & "' "

            Dim cmd As New OracleCommand(deleteString, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            cmd.ExecuteNonQuery()

            MsgBox("The User has been removed for this facility", MsgBoxStyle.Information, "User Removed!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnActivateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivateUser.Click
        Try
            SQL = "Select strUserEmail " & _
            "from " & DBNameSpace & ".OlapUserLogIn " & _
            "where strUserEmail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                Dim updateString As String = "UPDATE " & DBNameSpace & ".OlapUserLogin " & _
                          "SET strconfirm = to_char(sysdate, 'YYYY/MM/DD HH:MI:SS') " & _
                          "WHERE struseremail = '" & Replace(UCase(txtEmailAddress.Text), "'", "''") & "' "

                cmd = New OracleCommand(updateString, DBConn)

                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                'cmd.ExecuteNonQuery()
                dr = cmd.ExecuteReader
                dr.Close()

                MsgBox("The account has been activated", MsgBoxStyle.Information, "Activated!")
            Else
                MsgBox("This user does not exist.", MsgBoxStyle.Exclamation, "Activate failed!")
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub btnAddFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFacility.Click
        Try
            SQL = "select strAIRSnumber " & _
            "from " & DBNameSpace & ".APBFacilityInformation " & _
            "where strAIRSnumber = '0413" & mtbFeeAirsNumber.Text & "' "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "Select " & _
                "strAIRSNumber " & _
                "from " & DBNameSpace & ".FSPayAndSubmit " & _
                "where strAIRSNumber = '0413" & mtbFeeAirsNumber.Text & "' " & _
                "and intYear = '" & mtbyear.Text & "' "

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MsgBox("This Facility is already in the system for the desired year.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                Else
                    SQL = "Insert into " & DBNameSpace & ".FSPayandSubmit " & _
                    "(intyear, strairsnumber, strpaymenttype, strofficialname, strofficialtitle, datesubmit) " & _
                    "values ('" & CInt(mtbyear.Text) & "', '0413" & mtbFeeAirsNumber.Text & "', 'N/A', 'N/A', 'N/A', '" & OracleDate & "')"

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    MsgBox("The facility has been added", MsgBoxStyle.Information, "Facility Added!")
                End If
            Else
                MsgBox("This is an invalid AIRS #.", MsgBoxStyle.Exclamation, "Mailout and Stats")
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString() & vbCrLf & SQL, "PASPMailoutAndStats.btnAddFacility_Click")
        End Try
    End Sub
    Private Sub btnRemoveFacility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFacility.Click
        Try
            '  mtbYear.Text = "2006"
            Dim SQL As String = "Delete from " & DBNameSpace & ".FSPayandSubmit " & _
                      "where intyear = '" & CInt(mtbyear.Text) & "' and strairsnumber = '0413" & mtbFeeAirsNumber.Text & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete from " & DBNameSpace & ".FSCalculations " & _
                      "where intyear = '" & CInt(mtbyear.Text) & "' and strairsnumber = '0413" & mtbFeeAirsNumber.Text & "'"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Delete from " & DBNameSpace & ".FSConfirmation " & _
                      "where intyear = '" & CInt(mtbyear.Text) & "' and strairsnumber = '0413" & mtbFeeAirsNumber.Text & "'"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            MsgBox("The facility has been removed", MsgBoxStyle.Information, "Facility Removed!")
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdateUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateUser.Click
        Try
            Dim dt As DataTable
            Dim airsnumber As String
            Dim adminaccess As Boolean = False
            Dim feeaccess As Boolean = False
            Dim eiaccess As Boolean = False
            Dim esaccess As Boolean = False

            dt = dsWorkEntry.Tables("tblUserFacility").GetChanges
            If dt Is Nothing Then
            Else
                Dim Row As DataRow
                Dim intColumn As Integer
                For Each Row In dt.Rows
                    Select Case Row.RowState
                        'Case DataRowState.Added
                        '    blnDataChanged = True
                        'Case DataRowState.Deleted
                        '    blnDataChanged = True
                        Case DataRowState.Modified
                            For intColumn = 2 To 5
                                If Not IsDBNull(Row(intColumn, DataRowVersion.Original)) And Not IsDBNull(Row(intColumn, DataRowVersion.Current)) Then
                                    If Row(intColumn, DataRowVersion.Original) <> Row(intColumn, DataRowVersion.Current) Then
                                        airsnumber = Row(0, DataRowVersion.Original)
                                        adminaccess = Row(2, DataRowVersion.Current)
                                        feeaccess = Row(3, DataRowVersion.Current)
                                        eiaccess = Row(4, DataRowVersion.Current)
                                        esaccess = Row(5, DataRowVersion.Current)
                                        If airsnumber = "" Then
                                            MsgBox("There was an error while saving please try to resave." & vbCrLf & _
                                                 "You may have to close the window and reopen if it does not work after a few tries.", _
                                                  MsgBoxStyle.Exclamation, "Mailout and Stats")
                                            Exit Sub
                                        End If
                                        UpdateRecordsEmail(airsnumber, adminaccess, feeaccess, eiaccess, esaccess)
                                        'Exit For
                                    End If
                                End If
                            Next
                    End Select
                Next
            End If
            MsgBox("The records have been updated", MsgBoxStyle.Information, "Update Success!")
            LoadDataGrid()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region
#End Region
    Private Sub bgwLoad_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAIRS.RunWorkerCompleted
        Try
            'If MailoutAndStats Is Nothing Then
            'Else

            '    With cboFacilityToAdd
            '        .DataSource = dtairs2
            '        .DisplayMember = "strAirsNumber"
            '        .ValueMember = "strFacilityName"
            '    End With

            '    pnl1.Text = ""
            'End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub bgwEmails_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwEmails.DoWork
        Try
            'Dim drDSRow As DataRow
            'Dim drNewRow As DataRow
            'dtEmails = New DataTable

            'SQL = "Select numuserid, struseremail " & _
            '"from " & connNameSpace & ".OlapUserLogin " & _
            '"Order by struseremail "

            'ds2 = New DataSet
            'da2 = New OracleDataAdapter(SQL, conn)

            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If

            'da2.Fill(ds2, "UserEmail")

            'dtEmails.Columns.Add("numuserid", GetType(System.String))
            'dtEmails.Columns.Add("struseremail", GetType(System.String))

            'drNewRow = dtEmails.NewRow()
            'drNewRow("numuserid") = " "
            'drNewRow("struseremail") = " "
            'dtEmails.Rows.Add(drNewRow)

            'For Each drDSRow In ds2.Tables("UserEmail").Rows()
            '    drNewRow = dtEmails.NewRow()
            '    drNewRow("numuserid") = drDSRow("numuserid")
            '    drNewRow("struseremail") = drDSRow("struseremail")
            '    dtEmails.Rows.Add(drNewRow)
            'Next
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub bgwEmails_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwEmails.RunWorkerCompleted
        Try
            'If MailoutAndStats Is Nothing Then
            'Else
            '    With cboUserEmail
            '        .DataSource = dtEmails
            '        .DisplayMember = "struseremail"
            '        .ValueMember = "numuserid"
            '        '   .SelectedIndex = 0
            '    End With

            '    cboUserEmail.Enabled = True
            '    lblViewFacility.Enabled = True
            '    btnActivateEmail.Enabled = True
            '    pnl1.Text = ""
            'End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub PASPMailoutAndStats_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            bgwAIRS.CancelAsync()
            bgwEmails.CancelAsync()
            MailoutAndStats = Nothing

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub PASPMailoutAndStats_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Try
            If Me.Size.Width > 465 Then
                SCFeeStatistics.SplitterDistance = 462
            End If
            If Me.Size.Width > 550 Then
                SCGenerateMailOut.SplitterDistance = 545
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblviewsumarrymailoutinfo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblviewsumarrymailoutinfo.LinkClicked
        Try
            Dim year As Integer = CInt(cboMailoutYear.SelectedItem)
            lblEnrollYear.Text = year
            SQL = "SELECT " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER,  " & _
            "" & DBNameSpace & ".FEEMAILOUT.strFacilityName, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STROPERATIONALSTATUS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRCLASS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRAPCNSPS, " & _
            "" & DBNameSpace & ".FEEMAILOUT.STRAPCPART70 " & _
            "from " & DBNameSpace & ".FEEMAILOUT " & _
            "where " & DBNameSpace & ".FEEMAILOUT.INTYEAR = '" & year & "' " & _
            "order by " & DBNameSpace & ".FEEMAILOUT.STRFACILITYNAME"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount.DataSource = dsViewCount
            dgvFeeDataCount.DataMember = "ViewCount"

            dgvFeeDataCount.RowHeadersVisible = False
            dgvFeeDataCount.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount.AllowUserToResizeColumns = True
            dgvFeeDataCount.AllowUserToAddRows = False
            dgvFeeDataCount.AllowUserToDeleteRows = False
            dgvFeeDataCount.AllowUserToOrderColumns = True
            dgvFeeDataCount.AllowUserToResizeRows = True

            dgvFeeDataCount.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvFeeDataCount.Columns("strFacilityName").DisplayIndex = 1
            dgvFeeDataCount.Columns("STROPERATIONALSTATUS").HeaderText = "Operational Status"
            dgvFeeDataCount.Columns("STROPERATIONALSTATUS").DisplayIndex = 2
            dgvFeeDataCount.Columns("STRCLASS").HeaderText = "Class"
            dgvFeeDataCount.Columns("STRCLASS").DisplayIndex = 3
            dgvFeeDataCount.Columns("STRAPCNSPS").HeaderText = "NSPS"
            dgvFeeDataCount.Columns("STRAPCNSPS").DisplayIndex = 4
            dgvFeeDataCount.Columns("STRAPCPART70").HeaderText = "TV Source"
            dgvFeeDataCount.Columns("STRAPCPART70").DisplayIndex = 5

            txtRecordNumber.Text = dgvFeeDataCount.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgvFeeDataCount2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFeeDataCount2.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvFeeDataCount2.HitTest(e.X, e.Y)
        Try
            If dgvFeeDataCount2.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvFeeDataCount2.Columns(0).HeaderText = "Airs No." Then
                    If IsDBNull(dgvFeeDataCount2(0, hti.RowIndex).Value) Then

                    Else
                        clearSummaryFeeMailout()
                        lblAirsNo.Text = dgvFeeDataCount2(0, hti.RowIndex).Value
                        lblFacilityName.Text = dgvFeeDataCount2(1, hti.RowIndex).Value
                        findSummaryMailOut()
                    End If
                Else
                    If dgvFeeDataCount2.Columns(0).HeaderText = "Airs No." Then
                        If IsDBNull(dgvFeeDataCount2(0, hti.RowIndex).Value) Then

                        Else
                            clearSummaryFeeMailout()
                            lblAirsNo.Text = dgvFeeDataCount2(0, hti.RowIndex).Value
                            lblFacilityName.Text = dgvFeeDataCount2(1, hti.RowIndex).Value
                            findSummaryMailOut()
                        End If
                    End If
                End If
                If dgvFeeDataCount2.Columns(0).HeaderText = "Facility Name" Then
                    If IsDBNull(dgvFeeDataCount2(0, hti.RowIndex).Value) Then

                    Else
                        clearSummaryFeeMailout()
                        lblAirsNo.Text = dgvFeeDataCount2(0, hti.RowIndex).Value
                        lblFacilityName.Text = dgvFeeDataCount2(1, hti.RowIndex).Value
                        findSummaryMailOut()
                    End If
                Else
                    If dgvFeeDataCount2.Columns(0).HeaderText = "Facility Name" Then
                        If IsDBNull(dgvFeeDataCount2(0, hti.RowIndex).Value) Then

                        Else
                            clearSummaryFeeMailout()
                            lblAirsNo.Text = dgvFeeDataCount2(0, hti.RowIndex).Value
                            lblFacilityName.Text = dgvFeeDataCount2(1, hti.RowIndex).Value
                            findSummaryMailOut()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub clearSummaryFeeMailout()

        txtAirsNo.Text = ""
        txtFacilityName.Text = ""
        lblfirstname.Text = ""
        lblLastname.Text = ""
        lblcontactstreet.Text = ""
        lblcity.Text = ""
        lblstate.Text = ""
        lblzip.Text = ""
        lblEmail.Text = ""
        lblshutdowndate.Text = ""
        lblclass.Text = " "
        lbloperationalstatus.Text = " "
        lblNSPS.Text = " "
        lblPart70.Text = " "

    End Sub
    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            Help.ShowHelp(Label1, HELP_URL)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub llbViewUserData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewUserData.LinkClicked
        Try
            If mtbAIRSNumber.Text.Length <> 8 Then
                MsgBox("Please enter a complete 8 digit AIRS #.", MsgBoxStyle.Information, "DMU Tools")
            Else
                txtEmail.Clear()
                airsno = mtbAIRSNumber.Text
                SQL = "Select strFacilityName " & _
                "from " & DBNameSpace & ".APBFacilityInformation " & _
                "where strAIRSNumber = '0413" & airsno & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("strFacilityName")) Then
                        lblFacilityName.Text = "Facility Name"
                    Else
                        lblFacilityName.Text = dr.Item("strFacilityName")
                    End If
                End While
                dr.Close()

                FormatDataGridForWorkEnTry()
                LoadDataGrid()
            End If

            If lblFacilityName.Text = "Facility Name" Then
                pnlUser.Visible = False
            Else
                pnlUser.Visible = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblViewEmailData_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewEmailData.LinkClicked
        Try
            mtbFacilityToAdd.Clear()
            LoadDataGridFacility(txtWebUserEmail.Text)
            LoadUserInfo(txtWebUserEmail.Text)
            FormatDataGridForWorkEnTryEmail()

            If txtWebUserID.Text = "" Then
                pnlUserInfo.Visible = False
                pnlUserFacility.Visible = False
            Else
                pnlUserInfo.Visible = True
                pnlUserFacility.Visible = True
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub btnEditUserData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUserData.Click
        Try
            txtEditFirstName.Visible = True
            txtEditLastName.Visible = True
            txtEditTitle.Visible = True
            txtEditCompany.Visible = True
            txtEditAddress.Visible = True
            txtEditCity.Visible = True
            mtbEditState.Visible = True
            mtbEditZipCode.Visible = True
            mtbEditPhoneNumber.Visible = True
            mtbEditFaxNumber.Visible = True
            btnSaveEditedData.Visible = True
            txtEditUserPassword.Visible = True
            btnChangeEmailAddress.Visible = True
            txtEditEmail.Visible = True
            btnUpdatePassword.Visible = True


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnSaveEditedData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEditedData.Click
        Try

            If txtWebUserID.Text <> "" Then
                UpdateWebUserData()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            Else

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUpdatePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
        Try
            If txtWebUserID.Text <> "" And txtEditUserPassword.Text <> "" Then
                'SQL = "Update " & connNameSpace & ".OLAPUserLogIN set " & _
                '"strUserPassword = '" & EncryptDecrypt.EncryptText(txtEditUserPassword.Text) & "' " & _
                '"where numUserID = '" & txtWebUserID.Text & "' "

                'New password change code 6/30/2010
                SQL = "Update " & DBNameSpace & ".OLAPUserLogIN set " & _
                "strUserPassword = '" & getMd5Hash(txtEditUserPassword.Text) & "' " & _
                "where numUserID = '" & txtWebUserID.Text & "' "

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtEditUserPassword.Clear()
                txtEditFirstName.Visible = False
                txtEditLastName.Visible = False
                txtEditTitle.Visible = False
                txtEditCompany.Visible = False
                txtEditAddress.Visible = False
                txtEditCity.Visible = False
                mtbEditState.Visible = False
                mtbEditZipCode.Visible = False
                mtbEditPhoneNumber.Visible = False
                mtbEditFaxNumber.Visible = False
                btnSaveEditedData.Visible = False
                txtEditUserPassword.Visible = False
                btnChangeEmailAddress.Visible = False
                txtEditEmail.Visible = False
                btnUpdatePassword.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Sub UpdateWebUserData()
        Try
            Dim FirstName As String = " "
            Dim LastName As String = " "
            Dim Title As String = " "
            Dim Company As String = " "
            Dim Address As String = " "
            Dim City As String = " "
            Dim State As String = " "
            Dim Zip As String = " "
            Dim PhoneNumber As String = " "
            Dim FaxNumber As String = " "

            If txtEditFirstName.Text <> "" Then
                FirstName = " strFirstName = '" & Replace(txtEditFirstName.Text, "'", "''") & "', "
            End If
            If txtEditLastName.Text <> "" Then
                LastName = " strLastName = '" & Replace(txtEditLastName.Text, "'", "''") & "', "
            End If
            If txtEditTitle.Text <> "" Then
                Title = " strTitle = '" & Replace(txtEditTitle.Text, "'", "''") & "', "
            End If
            If txtEditCompany.Text <> "" Then
                Company = " strCompanyName = '" & Replace(txtEditCompany.Text, "'", "''") & "', "
            End If
            If txtEditAddress.Text <> "" Then
                Address = " strAddress = '" & Replace(txtEditAddress.Text, "'", "''") & "', "
            End If
            If txtEditCity.Text <> "" Then
                City = " strCity = '" & Replace(txtEditCity.Text, "'", "''") & "', "
            End If
            If mtbEditState.Text <> "" Then
                State = " strState = '" & Replace(mtbEditState.Text, "'", "''") & "', "
            End If
            If mtbEditZipCode.Text <> "" Then
                Zip = " strZip = '" & Replace(mtbEditZipCode.Text, "'", "''") & "', "
            End If
            If mtbEditPhoneNumber.Text <> "" Then
                PhoneNumber = " strPhoneNumber = '" & Replace(mtbEditPhoneNumber.Text, "'", "''") & "', "
            End If
            If mtbEditFaxNumber.Text <> "" Then
                FaxNumber = " strFaxNumber = '" & Replace(mtbEditFaxNumber.Text, "'", "''") & "', "
            End If

            SQL = "Update " & DBNameSpace & ".OLAPUserProfile set " & _
            FirstName & LastName & Title & Company & Address & _
            City & State & Zip & PhoneNumber & FaxNumber & _
            "numUserID = '" & txtWebUserID.Text & "' " & _
            "where numUserID = '" & txtWebUserID.Text & "' "

            'SQL = "Update " & connNameSpace & ".OLAPUserProfile set " & _
            '"strFirstName = '" & Replace(txtEditFirstName.Text, "'", "''") & "', " & _
            '"strLastName = '" & Replace(txtEditLastName.Text, "'", "''") & "', " & _
            '"strTitle = '" & Replace(txtEditTitle.Text, "'", "''") & "', " & _
            '"strCompanyName = '" & Replace(txtEditCompany.Text, "'", "''") & "', " & _
            '"strAddress = '" & Replace(txtEditAddress.Text, "'", "''") & "', " & _
            '"strCity = '" & Replace(txtEditCity.Text, "'", "''") & "', " & _
            '"strState = '" & Replace(mtbEditState.Text, "'", "''") & "', " & _
            '"strZip= '" & mtbEditZipCode.Text & "', " & _
            '"strPhoneNumber = '" & mtbEditPhoneNumber.Text & "', " & _
            '"strFaxNumber = '" & mtbEditFaxNumber.Text & "' " & _
            '"where numUserID = '" & txtWebUserID.Text & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            lblFName.Text = "First Name: " & txtEditFirstName.Text
            lblLName.Text = "Last Name: " & txtEditLastName.Text
            lblTitle.Text = "Title: " & txtEditTitle.Text
            lblCoName.Text = "Company Name: " & txtEditCompany.Text
            lblAddress.Text = txtEditAddress.Text
            lblCityStateZip.Text = txtEditCity.Text & ", " & mtbEditState.Text & " " & mtbEditZipCode.Text
            lblPhoneNo.Text = "Phone Number: " & mtbEditPhoneNumber.Text
            lblFaxNo.Text = "Fax Number: " & mtbEditFaxNumber.Text

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub cboUserEmail_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserEmail.SelectedValueChanged
        Try
            txtWebUserEmail.Text = cboUserEmail.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnChangeEmailAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeEmailAddress.Click
        Try
            If txtWebUserID.Text <> "" Then
                If EmailAddressCheck(txtEditEmail.Text) = True Then
                    SQL = "Select " & _
                    "numUserID, strUserPassword " & _
                    "from " & DBNameSpace & ".OLAPUserLogIN " & _
                    "where upper(strUserEmail) = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("numUserID")) Then
                            Else
                                If txtWebUserID.Text <> dr.Item("numUserID") Then
                                    MsgBox("Another user already has this email address and it would violate a unique constraint if you were " & _
                                           "to add this email to this user.", MsgBoxStyle.Exclamation, "Mailout and Stats")
                                    Exit Sub
                                End If
                            End If
                        End While
                        dr.Close()
                    End If

                    SQL = "Update " & DBNameSpace & ".OLAPUserLogIn set " & _
                    "strUserEmail = '" & Replace(txtEditEmail.Text.ToUpper, "'", "''") & "' " & _
                    "where numUserID = '" & txtWebUserID.Text & "' "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    cboUserEmail.Text = ""
                    txtWebUserEmail.Text = txtEditEmail.Text

                    LoadDataGridFacility(txtWebUserEmail.Text)
                    LoadUserInfo(txtWebUserEmail.Text)
                    FormatDataGridForWorkEnTryEmail()

                    If txtWebUserID.Text = "" Then
                        pnlUserInfo.Visible = False
                        pnlUserFacility.Visible = False
                    Else
                        pnlUserInfo.Visible = True
                        pnlUserFacility.Visible = True
                    End If

                Else
                    MsgBox("Invalid Email Address", MsgBoxStyle.Exclamation, "DMU Tools")
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub lblsumextrafacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblsumextrafacility.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
              "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
              " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtextrafacilities.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblextrafacility_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblextrafacility.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
              "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
              "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
              "from " & DBNameSpace & ".FEEMAILOUT, " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSCONTACTINFO " & _
              " where " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.intyear =" & DBNameSpace & ".FEEMAILOUT.intyear (+) " & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER(+)" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER (+)" & _
              " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
              "      and " & DBNameSpace & ".FSPayAndSubmit.intyear = " & DBNameSpace & ".FSContactINfo.intyear (+)  " & _
              " and " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER is null"


            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtextrafacilities.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblsumExtraNonresponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblsumExtraNonresponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
                       "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME " & _
                       "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION " & _
                       " where not exists (select * from " & DBNameSpace & ".FSCALCULATIONS " & _
                       "where " & DBNameSpace & ".FSPAYANDSUBMIT.strAIRSnumber = " & DBNameSpace & ".FSCalculations.strAIRSnumber " & _
                       " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAr = " & DBNameSpace & ".FSCALCULATIONS.INTYEAr)" & _
                       " and  not exists (select * from " & DBNameSpace & ".FEEMAILOUT " & _
                       " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER" & _
                       " and FSPAYANDSUBMIT.INTYEAr = FEEMAILOUT.INTYEAr) " & _
                       " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
                       " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER"




            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtExtraNonResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub lblExtraNonResponse_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblExtraNonResponse.LinkClicked
        Try
            txtfeeYear.Text = cboYear.SelectedItem
            Dim year As Integer = CInt(txtfeeYear.Text)

            SQL = "SELECT " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION.STRFACILITYNAME, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTFIRSTNAME, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTLASTNAME, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCOMPANYNAME, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTADDRESS, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTCITY, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTSTATE, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTPHONENUMBER, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTZIPCODE, " & _
                     "" & DBNameSpace & ".FSCONTACTINFO.STRCONTACTEMAIL " & _
                     "from " & DBNameSpace & ".FSPAYANDSUBMIT, " & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSCONTACTINFO " & _
                     " where not exists (select * from " & DBNameSpace & ".FSCALCULATIONS " & _
                     "where " & DBNameSpace & ".FSPAYANDSUBMIT.strAIRSnumber = " & DBNameSpace & ".FSCalculations.strAIRSnumber " & _
                     " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAr = " & DBNameSpace & ".FSCALCULATIONS.INTYEAr)" & _
                     " and  not exists (select * from " & DBNameSpace & ".FEEMAILOUT " & _
                     " where " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FEEMAILOUT.STRAIRSNUMBER" & _
                     " and FSPAYANDSUBMIT.INTYEAr = FEEMAILOUT.INTYEAr) " & _
                     " and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & year & "' " & _
                     " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".FSCONTACTINFO.STRAIRSNUMBER (+) " & _
                     " and " & DBNameSpace & ".FSPAYANDSUBMIT.STRAIRSNUMBER = " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER"

            dsViewCount = New DataSet
            daViewCount = New OracleDataAdapter(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            daViewCount.Fill(dsViewCount, "ViewCount")
            dgvFeeDataCount2.DataSource = dsViewCount
            dgvFeeDataCount2.DataMember = "ViewCount"

            dgvFeeDataCount2.RowHeadersVisible = False
            dgvFeeDataCount2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvFeeDataCount2.AllowUserToResizeColumns = True
            dgvFeeDataCount2.AllowUserToAddRows = False
            dgvFeeDataCount2.AllowUserToDeleteRows = False
            dgvFeeDataCount2.AllowUserToOrderColumns = True
            dgvFeeDataCount2.AllowUserToResizeRows = True

            dgvFeeDataCount2.Columns("STRAIRSNUMBER").HeaderText = "Airs No."
            dgvFeeDataCount2.Columns("STRAIRSNUMBER").DisplayIndex = 0
            dgvFeeDataCount2.Columns("STRFACILITYNAME").HeaderText = "Facility Name"
            dgvFeeDataCount2.Columns("STRFACILITYNAME").DisplayIndex = 1
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").HeaderText = "Contact First Name"
            dgvFeeDataCount2.Columns("STRCONTACTFIRSTNAME").DisplayIndex = 2
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").HeaderText = "Contact Last Name"
            dgvFeeDataCount2.Columns("STRCONTACTLASTNAME").DisplayIndex = 3
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").HeaderText = "Contact Company"
            dgvFeeDataCount2.Columns("STRCONTACTCOMPANYNAME").DisplayIndex = 4
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").HeaderText = "Address"
            dgvFeeDataCount2.Columns("STRCONTACTADDRESS").DisplayIndex = 5
            dgvFeeDataCount2.Columns("STRCONTACTCITY").HeaderText = "City"
            dgvFeeDataCount2.Columns("STRCONTACTCITY").DisplayIndex = 6
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").HeaderText = "State"
            dgvFeeDataCount2.Columns("STRCONTACTSTATE").DisplayIndex = 7
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").HeaderText = "Zip"
            dgvFeeDataCount2.Columns("STRCONTACTZIPCODE").DisplayIndex = 8
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").HeaderText = "Contact Email"
            dgvFeeDataCount2.Columns("STRCONTACTEMAIL").DisplayIndex = 9

            txtRecordNumber2.Text = dgvFeeDataCount2.RowCount.ToString
            txtExtraNonResponse.Text = txtRecordNumber2.Text
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub
    Private Sub loadFeeRateYear()
        Try
            Dim year As Integer

            year = Now.Year - 1
            If cboFeeRateYear.Items.Contains(year) Then
            Else
                cboFeeRateYear.Items.Add(year)
            End If

            SQL = "Select " & _
            "distinct(FSFEERATES.INTYEAR) as IntYear " & _
            "from " & DBNameSpace & ".FSFEERATES " & _
            "order by FSFEERATES.INTYEAR desc"
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("IntYear")) Then
                    year = Now.Year
                Else
                    year = dr.Item("IntYear")
                End If
                If cboFeeRateYear.Items.Contains(year) Then

                Else
                    cboFeeRateYear.Items.Add(year)
                End If
            End While
            dr.Close()
            If cboFeeRateYear.Items.Count = 0 Then
                cboFeeRateYear.Items.Add(Now.Year)
            End If



        Catch ex As Exception
            ErrorReport(ex.ToString(), "PASPMailoutAndStats.loadFeeRateYear")
        Finally
        End Try
    End Sub
    Private Sub lblViewFeeRate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblViewFeeRate.LinkClicked
        Try
            Dim year As Integer = CInt(cboFeeRateYear.SelectedItem)

            SQL = "SELECT " & DBNameSpace & ".FSFEERATES.TITLEVFEE, " & _
                     "" & DBNameSpace & ".FSFEERATES.SMFEE, " & _
                     "" & DBNameSpace & ".FSFEERATES.PERTONRATE, " & _
                     "" & DBNameSpace & ".FSFEERATES.NSPSFEE, " & _
                     "" & DBNameSpace & ".FSFEERATES.PART70FEE, " & _
                     "" & DBNameSpace & ".FSFEERATES.AdminFEEPERCENT, " & _
                     "" & DBNameSpace & ".FSFEERATES.DUEDATE, " & _
                     "" & DBNameSpace & ".FSFEERATES.DATFEEDUE " & _
                        "from " & DBNameSpace & ".FSFEERATES " & _
                    " Where " & DBNameSpace & ".FSFEERATES.INTYEAR = '" & year & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader


            While dr.Read
                If IsDBNull(dr("TITLEVFEE")) Then
                    txtTitleVfee.Text = ""
                Else
                    txtTitleVfee.Text = dr("TITLEVFEE")
                End If
                If IsDBNull(dr("SMFEE")) Then
                    txtAnnualSMFee.Text = ""
                Else
                    txtAnnualSMFee.Text = dr("SMFEE")
                End If
                If IsDBNull(dr("NSPSFEE")) Then
                    txtAnnualNSPSFee.Text = ""
                Else
                    txtAnnualNSPSFee.Text = dr("NSPSFEE")
                End If
                If IsDBNull(dr("PERTONRATE")) Then
                    txtperTonRate.Text = ""
                Else
                    txtperTonRate.Text = dr("PERTONRATE")
                End If
                If IsDBNull(dr("AdminFEEPERCENT")) Then
                    txtAdminFeePercent.Text = ""
                Else
                    txtAdminFeePercent.Text = dr("AdminFEEPERCENT")
                End If
                If IsDBNull(dr("DUEDATE")) Then
                    dtpduedate.Text = OracleDate
                    dtpduedate.Checked = False
                Else
                    dtpduedate.Text = dr("DUEDATE")
                    dtpduedate.Checked = True
                End If
                If IsDBNull(dr.Item("datFeeDue")) Then
                    dtpFeeDueDate.Text = OracleDate
                    dtpFeeDueDate.Checked = False
                Else
                    dtpFeeDueDate.Text = dr("DATFEEDUE")
                    dtpFeeDueDate.Checked = True
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), "PASPMailoutAndStats.lblViewFeeRate_LinkClicked")
        Finally

        End Try
    End Sub
    Sub loadDepositAndPayment()
        Try
            Dim Year As String


            Year = Now.Year
            'cboStatYear.Items.Add(Now.Year + 1)
            'cboStatYear.Items.Add(Now.Year)
            'cboStatYear.Items.Add(Now.Year - 1)

            SQL = "Select distinct(intYear) as intYear " & _
            "from " & DBNameSpace & ".FSPayAndSubmit " & _
            "order by intyear desc "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intYear")) Then
                    Year = Now.Year
                Else
                    Year = dr.Item("intYear")
                End If
                If cboStatYear.Items.Contains(Year) Then
                Else
                    cboStatYear.Items.Add(Year)
                End If
            End While
            dr.Close()

            cboStatPayType.Items.Add("ALL")
            cboStatPayType.Items.Add("ALL QUARTERS")

            SQL = "Select paytype " & _
            "from " & DBNameSpace & ".FSPayType " & _
            "order by paytype"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                cboStatPayType.Items.Add(dr.Item("paytype"))
            End While
            dr.Close()

            cboNewPaymentType.Items.Add("")
            cboNewPaymentType.Items.Add("N/A")
            cboNewPaymentType.Items.Add("Entire Annual Year")
            cboNewPaymentType.Items.Add("Four Quarterly Payments")

            cboStatYear.Text = cboStatYear.Items.Item(0)
            cboStatPayType.Text = cboStatPayType.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewDepositsStats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewDepositsStats.Click
        Try
            Dim PayType As String
            Select Case cboStatPayType.Text
                Case "ALL"
                    PayType = "%"
                Case "ANNUAL"
                    PayType = "Entire Annual Year"
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    PayType = "Four Quarterly Payments"
                Case Else
                    PayType = "N/A"
            End Select

            If cboStatYear.Text <> "" And cboStatPayType.Text <> "" Then
                SQL = "Select " & _
                "sum(numTotalFee + numAdminFee) as TotalDue " & _
                "from " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".FSPayAndSubmit " & _
                "where " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.strPaymentType like '" & PayType & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("TotalDue")) Then
                        txtTotalPaymentDue.Text = CDec("0").ToString("c")
                    Else
                        If PayType = "Four Quarterly Payments" And cboStatPayType.Text <> "ALL QUARTERS" Then
                            txtTotalPaymentDue.Text = CDec(dr.Item("TotalDue") / 4).ToString("c")
                        Else
                            txtTotalPaymentDue.Text = CDec(dr.Item("TotalDue")).ToString("c")
                        End If
                    End If
                End While
                dr.Close()

                Select Case cboStatPayType.Text
                    Case "ALL"
                        SQL = "Select " & _
                        "sum(numPayment) as TotalPaid " & _
                        "from " & DBNameSpace & ".FSAddPaid " & _
                        "where intYear = '" & cboStatYear.Text & "' "
                    Case "ALL QUARTERS"
                        SQL = "Select " & _
                        "sum(numPayment) as TotalPaid " & _
                        "from " & DBNameSpace & ".FSAddPaid " & _
                        "where intYear = '" & cboStatYear.Text & "' " & _
                        "and strPayType like '%QUARTER%' "
                    Case Else
                        SQL = "Select " & _
                        "sum(numPayment) as TotalPaid " & _
                        "from " & DBNameSpace & ".FSAddPaid " & _
                        "where intYear = '" & cboStatYear.Text & "' " & _
                        "and strPayType = '" & cboStatPayType.Text & "' "
                End Select

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("TotalPaid")) Then
                        txtTotalPaid.Text = CDec("0").ToString("c")
                    Else
                        txtTotalPaid.Text = CDec(dr.Item("TotalPaid")).ToString("c")
                    End If
                End While
                dr.Close()

                txtBalance.Text = CDec(txtTotalPaymentDue.Text) - CDec(txtTotalPaid.Text)
                txtBalance.Text = CDec(txtBalance.Text).ToString("c")
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewPaymentDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPaymentDue.Click
        Try
            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " & _
                    "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                    "strFacilityName, strPaymentType, " & _
                    "(numTotalFee + numAdminFee) as Due, " & DBNameSpace & ".FSCalculations.intYear, " & _
                      "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, numAdminFee " & _
                    "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSCalculations, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select " & _
                    "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNUmber, " & _
                    "strFacilityName, strPaymentType, " & _
                    "(numTotalFee + numAdminFee) as Due, " & DBNameSpace & ".FSCalculations.intYear, " & _
                      "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, numAdminFee " & _
                    "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSCalculations, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and strPaymentType = 'Entire Annual Year' "
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    SQL = "select " & _
                    "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNUmber, " & _
                    "strFacilityName, strPaymentType, " & _
                    "case " & _
                    "when strPaymentType = 'Four Quarterly Payments' then (numTotalFee + numAdminFee)/4 " & _
                    "else (numTotalFee + numAdminFee) " & _
                    "END Due, " & DBNameSpace & ".FSCalculations.intYear, " & _
                      "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, numAdminFee " & _
                    "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSCalculations, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                     "and " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and strPaymentType = 'Four Quarterly Payments' "
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                    SQL = "select " & _
                    "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNUmber, " & _
                    "strFacilityName, strPaymentType, " & _
                    "case " & _
                    "when strPaymentType = 'Four Quarterly Payments' then (numTotalFee + numAdminFee)/4 " & _
                    "else (numTotalFee + numAdminFee) " & _
                    "END Due, " & DBNameSpace & ".FSCalculations.intYear, " & _
                      "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, numAdminFee " & _
                    "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSCalculations, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit  " & _
                    "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber " & _
                    "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                     "and " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and strPaymentType <> 'Four Quarterly Payments' " & _
                    "and strPaymentType <> 'Entire Annual Year' "
                Case Else
                    SQL = "select " & _
                     "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
                     "strFacilityName, strPaymentType, " & _
                     "(numTotalFee + numAdminFee) as Due, " & DBNameSpace & ".FSCalculations.intYear, " & _
                      "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, numAdminFee " & _
                     "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSCalculations, " & _
                     "" & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber " & _
                     "and " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                     "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                       "and " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' "
            End Select

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            da.Fill(ds, "PaymentDue")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPaymentType").HeaderText = "Payment Type"
            dgvDepositsAndPayments.Columns("strPaymentType").DisplayIndex = 2
            Select Case cboStatPayType.Text
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                    dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Due per Quarter"
                Case Else
                    dgvDepositsAndPayments.Columns("Due").HeaderText = "Amount Due"
            End Select
            dgvDepositsAndPayments.Columns("Due").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("Due").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 4

            dgvDepositsAndPayments.Columns("strClass1").HeaderText = "Classification"
            dgvDepositsAndPayments.Columns("strClass1").DisplayIndex = 5

            dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
            dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 8
            dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 9
            dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

            dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
            dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 10
            dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub bntViewTotalPaid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntViewTotalPaid.Click
        Try
            Dim PaymentType As String
            Dim PayType As String

            Select Case cboStatPayType.Text
                Case "ALL"
                    PaymentType = "%"
                    PayType = "%"
                Case "ANNUAL"
                    PaymentType = "Entire Annual Year"
                    PayType = "ANNUAL"
                Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR"
                    PaymentType = "Four Quarterly Payments"
                    PayType = cboStatPayType.Text
                Case "ALL QUARTERS"
                    PaymentType = "Four Quarterly Payments"
                    PayType = "%Q%"
                Case "AMENDMENT", "ONE-TIME", "REFUND"
                    PaymentType = "N/A"
                    PayType = cboStatPayType.Text
                Case Else
                    PaymentType = "N/A"
                    PayType = cboStatPayType.Text
            End Select

            SQL = "select " & _
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, " & _
            "strPayType, numPayment, " & _
            "strDepositNo, datPayDate, " & _
            "strCheckNo, strInvoiceNo, " & _
            "" & DBNameSpace & ".FSAddPaid.intYear, strClass1 " & _
            "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSAddPaid " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber " & _
            "and " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "' " & _
            "and strPayType like '" & PayType & "' "

            SQL = "select " & _
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, " & _
            "strPayType, numPayment, " & _
            "strDepositNo, datPayDate, " & _
            "strCheckNo, strInvoiceNo, " & _
            "" & DBNameSpace & ".FSAddPaid.intYear, " & _
            "numPart70Fee, numSMFee, numNSPSFee, " & _
            "numTotalFee, strClass1, " & _
            "numAdminFee, (numTotalFee + numAdminFee) as Due " & _
            "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSAddPaid, " & _
            "" & DBNameSpace & ".FSCalculations " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber " & _
            "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".FSCalculations.strAIRSNumber (+) " & _
            "and " & DBNameSpace & ".FSAddPaid.intYear = " & DBNameSpace & ".FSCalculations.intYear (+) " & _
            "and " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "' " & _
            "and strPayType like '" & PayType & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            da.Fill(ds, "PaymentDue")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
            dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("numPayment").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("numPayment").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("numPayment").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("datPayDate").HeaderText = "Pay Date"
            dgvDepositsAndPayments.Columns("datPayDate").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("datPayDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDepositsAndPayments.Columns("strCheckNo").HeaderText = "Check #"
            dgvDepositsAndPayments.Columns("strCheckNo").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("strInvoiceNo").HeaderText = "Invoice #"
            dgvDepositsAndPayments.Columns("strInvoiceNo").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 8

            dgvDepositsAndPayments.Columns("strClass1").HeaderText = "Classification"
            dgvDepositsAndPayments.Columns("strClass1").DisplayIndex = 9

            dgvDepositsAndPayments.Columns("numPart70Fee").HeaderText = "Part 70 Fee"
            dgvDepositsAndPayments.Columns("numPart70Fee").DisplayIndex = 10
            dgvDepositsAndPayments.Columns("numPart70Fee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numSMFee").HeaderText = "SM Fee"
            dgvDepositsAndPayments.Columns("numSMFee").DisplayIndex = 11
            dgvDepositsAndPayments.Columns("numSMFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numNSPSFee").HeaderText = "NSPS Fee"
            dgvDepositsAndPayments.Columns("numNSPSFee").DisplayIndex = 12
            dgvDepositsAndPayments.Columns("numNSPSFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("numTotalFee").HeaderText = "Fees Total"
            dgvDepositsAndPayments.Columns("numTotalFee").DisplayIndex = 13
            dgvDepositsAndPayments.Columns("numTotalFee").DefaultCellStyle.Format = "c"

            dgvDepositsAndPayments.Columns("numAdminFee").HeaderText = "Admin Fees"
            dgvDepositsAndPayments.Columns("numAdminFee").DisplayIndex = 14
            dgvDepositsAndPayments.Columns("numAdminFee").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Due").HeaderText = "Total Due"
            dgvDepositsAndPayments.Columns("Due").DisplayIndex = 15
            dgvDepositsAndPayments.Columns("Due").DefaultCellStyle.Format = "c"

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewBalance.Click
        Try
            Dim PaymentType As String
            Dim PayType As String

            Select Case cboStatPayType.Text
                Case "ALL"
                    SQL = "select " & _
                    "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                    "TOTALDUE, TOTALPAID, " & _
                    "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                    "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                    "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                    "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                    "(select " & _
                    "STRAIRSNUMBER, INTYEAR, " & _
                    "sum(NUMPAYMENT) as TotalPaid     " & _
                    "from " & DBNameSpace & ".FSADDPAID " & _
                    "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                    "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                    "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' "
                Case "ANNUAL"
                    SQL = "select " & _
                    "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                    "TOTALDUE, TOTALPAID, " & _
                    "(TOTALDUE - TOTALPAID) as Balance " & _
                    "from " & _
                    "(select " & _
                    "STRAIRSNUMBER, intyear, " & _
                    "NUMTOTALFEE, NUMADMINFEE, " & _
                    "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                    "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                    "(select " & _
                    "STRAIRSNUMBER, INTYEAR, " & _
                    "sum(NUMPAYMENT) as TotalPaid     " & _
                    "from " & DBNameSpace & ".FSADDPAID " & _
                    "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                    "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                    "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and strPaymentType = 'Entire Annual Year' "
                Case "ALL QUARTERS"
                    SQL = "select " & _
                    "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                    "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                    "TOTALDUE, TOTALPAID, " & _
                    "(TOTALDUE - TOTALPAID) as Balance " & _
                    "from " & _
                    "(select " & _
                    "STRAIRSNUMBER, intyear, " & _
                    "NUMTOTALFEE, NUMADMINFEE, " & _
                    "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                    "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                    "(select " & _
                    "STRAIRSNUMBER, INTYEAR, " & _
                    "sum(NUMPAYMENT) as TotalPaid     " & _
                    "from " & DBNameSpace & ".FSADDPAID " & _
                    "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                    "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                    "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                    "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                    "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                    "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                    "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                    "and strPaymentType = 'Four Quarterly Payments' "
                Case "QUARTER ONE"
                    SQL = "Select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                   "TotalDue, TotalPaid, " & _
                   "Balance,   " & _
                   "strPaymentType, " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                   "from " & _
                   "(select " & _
                   "sum(Due) as TotalDue, " & _
                   "sum(Paid) as TotalPaid, " & _
                   "AIRSNumber, strFacilityName, " & _
                   "(sum(Due) -  sum(Paid)) as Balance " & _
                   "from " & _
                   "( select  " & _
                   "0 as paid, " & _
                   "numTotalFee/4 as Due,  " & _
                   "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber, " & _
                   "strFacilityName  " & _
                   "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation, " & _
                   "" & DBNameSpace & ".FSPayAndSubmit " & _
                   "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                   "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                   "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                   "and strPaymentType = 'Four Quarterly Payments' " & _
                   "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                   "union " & _
                   "select  " & _
                   "sum (numPayment) as Paid, 0 as due, " & _
                   "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber, " & _
                   "strFacilityName    " & _
                   "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation   " & _
                   "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'  " & _
                   "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                   "and strPayType = 'QUARTER ONE' " & _
                   "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName )Table1  " & _
                   "Group by AIRSNumber, strFacilityName       " & _
                   "order by AIRSNumber) Table1,  " & _
                   "" & DBNameSpace & ".FSPayAndSubmit " & _
                   "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber (+) " & _
                   "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' "


                    SQL = "select " & _
                     "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                     "TOTALDUE, TOTALPAID, " & _
                     "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                     "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                     "(NUMTOTALFEE + NUMADMINFEE)/4 as TOTALDUE " & _
                     "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                     "(select " & _
                     "STRAIRSNUMBER, INTYEAR, " & _
                     "sum(NUMPAYMENT) as TotalPaid     " & _
                     "from " & DBNameSpace & ".FSADDPAID " & _
                     "where  strPayType = 'QUARTER ONE' " & _
                     "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                     "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                     "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and strPaymentType = 'Four Quarterly Payments' "

                Case "QUARTER TWO"
                    SQL = "Select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                    "TotalDue, TotalPaid, " & _
                    "Balance,   " & _
                    "strPaymentType, " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "from " & _
                    "(select " & _
                    "sum(Due) as TotalDue, " & _
                    "sum(Paid) as TotalPaid, " & _
                    "AIRSNumber, strFacilityName, " & _
                    "(sum(Due) -  sum(Paid)) as Balance " & _
                    "from " & _
                    "( select  " & _
                    "0 as paid, " & _
                    "numTotalFee/4 as Due,  " & _
                    "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName  " & _
                    "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPaymentType = 'Four Quarterly Payments' " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "union " & _
                    "select  " & _
                    "sum (numPayment) as Paid, 0 as due, " & _
                    "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName    " & _
                    "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation   " & _
                    "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'  " & _
                    "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPayType = 'QUARTER TWO' " & _
                    "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName )Table1  " & _
                    "Group by AIRSNumber, strFacilityName       " & _
                    "order by AIRSNumber) Table1,  " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber (+) " & _
                    "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' "
                    SQL = "select " & _
                     "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                     "TOTALDUE, TOTALPAID, " & _
                     "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                     "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                     "(NUMTOTALFEE + NUMADMINFEE)/4 as TOTALDUE " & _
                     "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                     "(select " & _
                     "STRAIRSNUMBER, INTYEAR, " & _
                     "sum(NUMPAYMENT) as TotalPaid     " & _
                     "from " & DBNameSpace & ".FSADDPAID " & _
                     "where  strPayType = 'QUARTER TWO' " & _
                     "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                     "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                     "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and strPaymentType = 'Four Quarterly Payments' "
                Case "QUARTER THREE"
                    SQL = "Select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                    "TotalDue, TotalPaid, " & _
                    "Balance,  " & _
                    "strPaymentType, " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "from " & _
                    "(select " & _
                    "sum(Due) as TotalDue, " & _
                    "sum(Paid) as TotalPaid, " & _
                    "AIRSNumber, strFacilityName, " & _
                    "(sum(Due) -  sum(Paid)) as Balance " & _
                    "from " & _
                    "( select  " & _
                    "0 as paid, " & _
                    "numTotalFee/4 as Due,  " & _
                    "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName  " & _
                    "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPaymentType = 'Four Quarterly Payments' " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "union " & _
                    "select  " & _
                    "sum (numPayment) as Paid, 0 as due, " & _
                    "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName    " & _
                    "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation   " & _
                    "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'  " & _
                    "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPayType = 'QUARTER THREE' " & _
                    "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName )Table1  " & _
                    "Group by AIRSNumber, strFacilityName       " & _
                    "order by AIRSNumber) Table1,  " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber (+) " & _
                    "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' "

                    SQL = "select " & _
                     "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                     "TOTALDUE, TOTALPAID, " & _
                     "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                     "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                     "(NUMTOTALFEE + NUMADMINFEE)/4 as TOTALDUE " & _
                     "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                     "(select " & _
                     "STRAIRSNUMBER, INTYEAR, " & _
                     "sum(NUMPAYMENT) as TotalPaid     " & _
                     "from " & DBNameSpace & ".FSADDPAID " & _
                     "where  strPayType = 'QUARTER THREE' " & _
                     "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                     "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                     "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and strPaymentType = 'Four Quarterly Payments' "
                Case "QUARTER FOUR"
                    SQL = "Select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                    "TotalDue, TotalPaid, " & _
                    "Balance,   " & _
                    "strPaymentType, " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "from " & _
                    "(select " & _
                    "sum(Due) as TotalDue, " & _
                    "sum(Paid) as TotalPaid, " & _
                    "AIRSNumber, strFacilityName, " & _
                    "(sum(Due) -  sum(Paid)) as Balance " & _
                    "from " & _
                    "( select  " & _
                    "0 as paid, " & _
                    "numTotalFee/4 as Due,  " & _
                    "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName  " & _
                    "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation, " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPaymentType = 'Four Quarterly Payments' " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "union " & _
                    "select  " & _
                    "sum (numPayment) as Paid, 0 as due, " & _
                    "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber, " & _
                    "strFacilityName    " & _
                    "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation   " & _
                    "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'  " & _
                    "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+) " & _
                    "and strPayType = 'QUARTER FOUR' " & _
                    "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName )Table1  " & _
                    "Group by AIRSNumber, strFacilityName       " & _
                    "order by AIRSNumber) Table1,  " & _
                    "" & DBNameSpace & ".FSPayAndSubmit " & _
                    "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber (+) " & _
                    "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' "

                    SQL = "select " & _
                     "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                     "TOTALDUE, TOTALPAID, " & _
                     "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                     "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                     "(NUMTOTALFEE + NUMADMINFEE)/4 as TOTALDUE " & _
                     "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                     "(select " & _
                     "STRAIRSNUMBER, INTYEAR, " & _
                     "sum(NUMPAYMENT) as TotalPaid     " & _
                     "from " & DBNameSpace & ".FSADDPAID " & _
                     "where  strPayType = 'QUARTER FOUR' " & _
                     "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                     "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                     "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and strPaymentType = 'Four Quarterly Payments' "

                Case "AMENDMENT"
                    SQL = "select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                    "TotalDue, TotalPaid,  " & _
                    "Balance,   " & _
                    "strpaymentType, strPayType, " & DBNameSpace & ".FSAddPaid.intYear    " & _
                    "from  " & _
                    "(select  " & _
                    "sum(Due) as TotalDue,  " & _
                    "sum(Paid) as TotalPaid,  " & _
                    "AIRSNumber, strFacilityName,  " & _
                    "(sum(Due) -  sum(Paid)) as Balance  " & _
                    "from  " & _
                    "( select   " & _
                    "0 as paid,  " & _
                    "numTotalFee as Due,   " & _
                    "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber,  " & _
                    "strFacilityName   " & _
                    "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation,  " & _
                    "" & DBNameSpace & ".FSPayAndSubmit  " & _
                    "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                    "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                    "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                    "and strPaymentType <> 'Four Quarterly Payments'  " & _
                    "and strPaymentType <> 'Entire Annual Year'  " & _
                    "union  " & _
                    "select   " & _
                    "sum (numPayment) as Paid, 0 as due,  " & _
                    "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber,  " & _
                    "strFacilityName      " & _
                    "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation    " & _
                    "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'   " & _
                    "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                    "and strPayType not like 'QUARTER%'  " & _
                    "and strPayType <> 'ANNUAL'  " & _
                    "and strPayType = 'AMENDMENT'  " & _
                    "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName  )Table1   " & _
                    "Group by AIRSNumber, strFacilityName       " & _
                    "order by AIRSNumber ) Table1,  " & _
                    "" & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".FSAddPaid   " & _
                    "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber (+)  " & _
                    "and Table1.AIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber (+)  " & _
                    "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' " & _
                    "and " & DBNameSpace & ".FSAddPaid.intYEar = '" & cboStatYear.Text & "'  " & _
                    "and strPayType = 'AMENDMENT'  "


                    SQL = "select " & _
                     "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                     "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                     "TOTALDUE, TOTALPAID, " & _
                     "(TOTALDUE - TOTALPAID) as Balance " & _
                     "from " & _
                     "(select " & _
                     "STRAIRSNUMBER, intyear, " & _
                     "NUMTOTALFEE, NUMADMINFEE, " & _
                     "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                     "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                     "(select " & _
                     "STRAIRSNUMBER, INTYEAR, " & _
                     "sum(NUMPAYMENT) as TotalPaid     " & _
                     "from " & DBNameSpace & ".FSADDPAID " & _
                     "where  strPayType = 'AMENDMENT' " & _
                     "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                     "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                     "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                     "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                     "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                     "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                     "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                     "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                     "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' "  

                Case "ONE-TIME"
                    SQL = "select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                  "TotalDue, TotalPaid,  " & _
                  "Balance,    " & _
                  "strpaymentType, strPayType, " & DBNameSpace & ".FSAddPaid.intYear    " & _
                  "from  " & _
                  "(select  " & _
                  "sum(Due) as TotalDue,  " & _
                  "sum(Paid) as TotalPaid,  " & _
                  "AIRSNumber, strFacilityName,  " & _
                  "(sum(Due) -  sum(Paid)) as Balance  " & _
                  "from  " & _
                  "( select   " & _
                  "0 as paid,  " & _
                  "numTotalFee as Due,   " & _
                  "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber,  " & _
                  "strFacilityName   " & _
                  "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation,  " & _
                  "" & DBNameSpace & ".FSPayAndSubmit  " & _
                  "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                  "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                  "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                  "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                  "and strPaymentType <> 'Four Quarterly Payments'  " & _
                  "and strPaymentType <> 'Entire Annual Year'  " & _
                  "union  " & _
                  "select   " & _
                  "sum (numPayment) as Paid, 0 as due,  " & _
                  "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber,  " & _
                  "strFacilityName      " & _
                  "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation    " & _
                  "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'   " & _
                  "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                  "and strPayType not like 'QUARTER%'  " & _
                  "and strPayType <> 'ANNUAL'  " & _
                  "and strPayType = 'ONE-TIME'  " & _
                  "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName  )Table1   " & _
                  "Group by AIRSNumber, strFacilityName       " & _
                  "order by AIRSNumber ) Table1,  " & _
                  "" & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".FSAddPaid   " & _
                  "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber (+)  " & _
                  "and Table1.AIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber (+)  " & _
                  "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' " & _
                  "and " & DBNameSpace & ".FSAddPaid.intYEar = '" & cboStatYear.Text & "'  " & _
                  "and strPayType = 'ONE-TIME'  "

                    SQL = "select " & _
                  "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                  "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                  "TOTALDUE, TOTALPAID, " & _
                  "(TOTALDUE - TOTALPAID) as Balance " & _
                  "from " & _
                  "(select " & _
                  "STRAIRSNUMBER, intyear, " & _
                  "NUMTOTALFEE, NUMADMINFEE, " & _
                  "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                  "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                  "(select " & _
                  "STRAIRSNUMBER, INTYEAR, " & _
                  "sum(NUMPAYMENT) as TotalPaid     " & _
                  "from " & DBNameSpace & ".FSADDPAID " & _
                  "where  strPayType = 'ONE-TIME' " & _
                  "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                  "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                  "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                  "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                  "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                  "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' "

                Case "REFUND"
                    SQL = "select " & _
                    "substr(AIRSNumber, 5) as AIRSNumber, strFacilityName, " & _
                   "TotalDue, TotalPaid,  " & _
                   "Balance,   " & _
                   "strpaymentType, strPayType, " & _
                   "" & DBNameSpace & ".FSAddPaid.intYear    " & _
                   "from  " & _
                   "(select  " & _
                   "sum(Due) as TotalDue,  " & _
                   "sum(Paid) as TotalPaid,  " & _
                   "AIRSNumber, strFacilityName,  " & _
                   "(sum(Due) -  sum(Paid)) as Balance  " & _
                   "from  " & _
                   "( select   " & _
                   "0 as paid,  " & _
                   "numTotalFee as Due,   " & _
                   "(" & DBNameSpace & ".FSCalculations.strAIRSNumber) as AIRSNumber,  " & _
                   "strFacilityName   " & _
                   "From  " & DBNameSpace & ".FSCalculations, " & DBNameSpace & ".APBFacilityInformation,  " & _
                   "" & DBNameSpace & ".FSPayAndSubmit  " & _
                   "where  " & DBNameSpace & ".FSCalculations.intYear = '" & cboStatYear.Text & "' " & _
                   "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber  " & _
                   "and " & DBNameSpace & ".FSCalculations.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                   "and " & DBNameSpace & ".FSCalculations.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear " & _
                   "and strPaymentType <> 'Four Quarterly Payments'  " & _
                   "and strPaymentType <> 'Entire Annual Year'  " & _
                   "union  " & _
                   "select   " & _
                   "sum (numPayment) as Paid, 0 as due,  " & _
                   "(" & DBNameSpace & ".FSAddPaid.strAIRSNumber) as AIRSNumber,  " & _
                   "strFacilityName      " & _
                   "From   " & DBNameSpace & ".FSAddPaid, " & DBNameSpace & ".APBFacilityInformation    " & _
                   "where  " & DBNameSpace & ".FSAddPaid.intYear = '" & cboStatYear.Text & "'   " & _
                   "and " & DBNameSpace & ".FSAddPaid.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber (+)  " & _
                   "and strPayType not like 'QUARTER%'  " & _
                   "and strPayType <> 'ANNUAL'  " & _
                   "and strPayType = 'REFUND'  " & _
                   "group by " & DBNameSpace & ".FSAddPaid.strAIRSNumber, strFacilityName  )Table1   " & _
                   "Group by AIRSNumber, strFacilityName       " & _
                   "order by AIRSNumber ) Table1,  " & _
                   "" & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".FSAddPaid   " & _
                   "where Table1.AIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber (+)  " & _
                   "and Table1.AIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber (+)  " & _
                   "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboStatYear.Text & "' " & _
                   "and " & DBNameSpace & ".FSAddPaid.intYEar = '" & cboStatYear.Text & "'  " & _
                   "and strPayType = 'REFUND'  "

                    SQL = "select " & _
                  "SUBSTR(" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER, 5) as AIRSNUMBER, " & _
                  "STRFACILITYNAME, TOTALDUE.INTYEAR, STRPAYMENTTYPE,  " & _
                  "TOTALDUE, TOTALPAID, " & _
                  "(TOTALDUE - TOTALPAID) as Balance " & _
                  "from " & _
                  "(select " & _
                  "STRAIRSNUMBER, intyear, " & _
                  "NUMTOTALFEE, NUMADMINFEE, " & _
                  "(NUMTOTALFEE + NUMADMINFEE) as TOTALDUE " & _
                  "from  " & DBNameSpace & ".FSCALCULATIONS) TOTALDUE, " & _
                  "(select " & _
                  "STRAIRSNUMBER, INTYEAR, " & _
                  "sum(NUMPAYMENT) as TotalPaid     " & _
                  "from " & DBNameSpace & ".FSADDPAID " & _
                  "where  strPayType = 'REFUND' " & _
                  "group by STRAIRSNUMBER, INTYEAR) TOTALPAID, " & _
                  "" & DBNameSpace & ".APBFACILITYINFORMATION, " & DBNameSpace & ".FSPayAndSubmit  " & _
                  "where (" & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALDUE.STRAIRSNUMBER  " & _
                  "or " & DBNameSpace & ".APBFACILITYINFORMATION.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER   ) " & _
                  "and " & DBNameSpace & ".APBFACILITYINFORMATION.strAIRSNumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber " & _
                  "and TOTALDUE.STRAIRSNUMBER = TOTALPAID.STRAIRSNUMBER " & _
                  "and TOTALDUE.INTYEAR = TOTALPAID.INTYEAR " & _
                  "and TOTALDUE.INTYEAR = '" & cboStatYear.Text & "' " & _
                  "and " & DBNameSpace & ".FSPAYANDSUBMIT.INTYEAR = '" & cboStatYear.Text & "' "
                Case Else
                    PaymentType = "N/A"
                    PayType = cboStatPayType.Text
            End Select
            If chbNonZeroBalance.Checked = True Then
                SQL = SQL & " and (TOTALDUE - TOTALPAID) <> '0'  "
            End If

            ds = New DataSet
            If SQL <> "" Then
                da = New OracleDataAdapter(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                da.Fill(ds, "PaymentDue")
            End If

            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPaymentType").HeaderText = "Payment Type"
            dgvDepositsAndPayments.Columns("strPaymentType").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Due"
            dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
            dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 6

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

            Exit Sub

            If cboStatPayType.Text = "ALL QUARTERS" Then
                dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Paid Type"
                dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 3
                dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Due per Quarter"
                dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 4
                dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
                dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 5
                dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
                dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 6
                dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
                dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 7
                Select Case cboStatPayType.Text
                    Case "ONE-TIME", "AMENDMENT", "REFUND"
                        dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
                        dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 8
                End Select

            Else
                Select Case cboStatPayType.Text
                    Case "QUARTER ONE", "QUARTER TWO", "QUARTER THREE", "QUARTER FOUR", "ALL QUARTERS"
                        dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Due per Quarter"
                    Case Else
                        dgvDepositsAndPayments.Columns("TotalDue").HeaderText = "Amount Due"
                End Select
                dgvDepositsAndPayments.Columns("TotalDue").DisplayIndex = 3
                dgvDepositsAndPayments.Columns("TotalDue").DefaultCellStyle.Format = "c"

                dgvDepositsAndPayments.Columns("TotalPaid").HeaderText = "Amount Paid"
                dgvDepositsAndPayments.Columns("TotalPaid").DisplayIndex = 4
                dgvDepositsAndPayments.Columns("TotalPaid").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("Balance").HeaderText = "Balance"
                dgvDepositsAndPayments.Columns("Balance").DisplayIndex = 5
                dgvDepositsAndPayments.Columns("Balance").DefaultCellStyle.Format = "c"
                dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
                dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 6
                Select Case cboStatPayType.Text
                    Case "ONE-TIME", "AMENDMENT", "REFUND"
                        dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
                        dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 7
                End Select

            End If


            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewSelectedFeeData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewSelectedFeeData.Click
        Try
            If pnlDetails.Dock = DockStyle.None Then
                pnlDetails.Dock = DockStyle.Top
            Else
                pnlDetails.Dock = DockStyle.None
            End If

            If txtSelectedAIRSNumber.Text <> "" And txtSelectedAIRSNumber.Text.Length = 8 And txtSelectedYear.Text <> "" Then
                LoadSelectedFeeData()
            End If
            pnlCorrectPaymentType.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSelectedFeeData()
        Try
            SQL = "Select " & _
            "strAIRSNumber, intYear, " & _
            "strPaymentType, intSubmittal, " & _
            "dateSubmit, strComments " & _
            "from " & DBNameSpace & ".FSPayAndSubmit " & _
            "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
            "and intyear = '" & txtSelectedYear.Text & "' "
            cmd = New OracleCommand(SQL, DBConn)

            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("strPaymentType")) Then
                    txtPaymentType.Text = ""
                Else
                    txtPaymentType.Text = dr.Item("strPaymentType")
                End If
                If IsDBNull(dr.Item("intSubmittal")) Then
                    txtOnlineSubmittalStatus.Text = ""
                Else
                    If dr.Item("IntSubmittal") = "0" Then
                        txtOnlineSubmittalStatus.Text = "Not Finalized"
                    Else
                        txtOnlineSubmittalStatus.Text = "Finalized"
                    End If
                End If
                If IsDBNull(dr.Item("dateSubmit")) Then
                    txtDateSubmitted.Text = ""
                Else
                    If txtOnlineSubmittalStatus.Text = "Finalized" Then
                        txtDateSubmitted.Text = Format(dr.Item("dateSubmit"), "dd-MMM-yyyy")
                    Else
                        txtDateSubmitted.Text = ""
                    End If
                End If
                If IsDBNull(dr.Item("strComments")) Then
                    txtSubmittalComments.Text = ""
                Else
                    txtSubmittalComments.Text = dr.Item("strComments")
                End If
            End While
            dr.Close()

            SQL = "Select " & _
            "intVOCTons, intPMTons, " & _
            "intSO2Tons, intNOxTons, " & _
            "numPart70Fee, numSMFee, " & _
            "numNSPSFee, numTotalFee, " & _
            "strNSPSExempt, strNSPSReason, " & _
            "strOperate, numFeeRate, " & _
            "strNSPSExemptReason, strPart70, " & _
            "strSyntheticMinor, numCalculatedFee, " & _
            "strClass1, strNSPS1, " & _
            "shutDate, varianceCheck, " & _
            "varianceComments, numAdminFee, " & _
            "(numTotalFee + numAdminFee) as AllFees " & _
            "from " & DBNameSpace & ".FSCalculations " & _
            "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
            "and intYear = '" & txtSelectedYear.Text & "' "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("intVOCTons")) Then
                    txtVOCTons.Clear()
                Else
                    txtVOCTons.Text = dr.Item("intVOCTons")
                End If
                If IsDBNull(dr.Item("intPMTons")) Then
                    txtPMTons.Clear()
                Else
                    txtPMTons.Text = dr.Item("intPMTons")
                End If
                If IsDBNull(dr.Item("intSO2Tons")) Then
                    txtSO2Tons.Clear()
                Else
                    txtSO2Tons.Text = dr.Item("intSO2Tons")
                End If
                If IsDBNull(dr.Item("intNOxTons")) Then
                    txtNOxTons.Clear()
                Else
                    txtNOxTons.Text = dr.Item("intNOxTons")
                End If
                If IsDBNull(dr.Item("numPart70Fee")) Then
                    txtPart70Fee.Clear()
                Else
                    txtPart70Fee.Text = Format(dr.Item("numPart70Fee"), "c")
                End If
                If IsDBNull(dr.Item("numSMFee")) Then
                    txtSMfee.Clear()
                Else
                    txtSMfee.Text = Format(dr.Item("numSMFee"), "c")
                End If
                If IsDBNull(dr.Item("numNSPSFee")) Then
                    txtNSPSfee.Clear()
                Else
                    txtNSPSfee.Text = Format(dr.Item("numNSPSFee"), "c")
                End If
                If IsDBNull(dr.Item("numTotalFee")) Then
                    txtTotalFee.Clear()
                Else
                    txtTotalFee.Text = Format(dr.Item("numTotalFee"), "c")
                End If
                If IsDBNull(dr.Item("strNSPSExempt")) Then
                    txtNSPSExempt.Clear()
                    txtNSPSReason.Clear()
                    txtNSPSExemptReason.Clear()
                Else
                    txtNSPSExempt.Text = dr.Item("strNSPSExempt")
                End If
                If txtNSPSExempt.Text <> "NO" Then
                    If IsDBNull(dr.Item("strNSPSReason")) Then
                        txtNSPSReason.Clear()
                    Else
                        txtNSPSReason.Text = dr.Item("strNSPSReason")
                    End If
                    If IsDBNull(dr.Item("strNSPSExemptReason")) Then
                        txtNSPSExemptReason.Clear()
                    Else
                        txtNSPSExemptReason.Text = dr.Item("strNSPSExemptReason")
                    End If
                Else
                    txtNSPSReason.Clear()
                    txtNSPSExemptReason.Clear()
                End If
                If IsDBNull(dr.Item("strOperate")) Then
                    txtOperate.Clear()
                Else
                    txtOperate.Text = dr.Item("strOperate")
                End If
                If IsDBNull(dr.Item("numFeeRate")) Then
                    txtFeeRate.Clear()
                Else
                    txtFeeRate.Text = Format(dr.Item("numFeeRate"), "c")
                End If

                If IsDBNull(dr.Item("strPart70")) Then
                    txtPart70.Clear()
                Else
                    txtPart70.Text = dr.Item("strPart70")
                End If
                If IsDBNull(dr.Item("strSyntheticMinor")) Then
                    txtSyntheticMinor.Clear()
                Else
                    txtSyntheticMinor.Text = dr.Item("strSyntheticMinor")
                End If
                If IsDBNull(dr.Item("numCalculatedFee")) Then
                    txtCalculatedFee.Clear()
                Else
                    txtCalculatedFee.Text = Format(dr.Item("numCalculatedFee"), "c")
                End If
                If IsDBNull(dr.Item("strClass1")) Then
                    txtClass.Clear()
                Else
                    txtClass.Text = dr.Item("strClass1")
                End If
                If IsDBNull(dr.Item("strNSPS1")) Then
                    txtNSPS.Clear()
                Else
                    txtNSPS.Text = dr.Item("strNSPS1")
                End If
                If IsDBNull(dr.Item("shutDate")) Then
                    txtShutdowndate.Clear()
                Else
                    txtShutdowndate.Text = dr.Item("ShutDate")
                End If
                If IsDBNull(dr.Item("VarianceCheck")) Then
                    txtVarianceCheck.Clear()
                Else
                    txtVarianceCheck.Text = dr.Item("VarianceCheck")
                End If
                If IsDBNull(dr.Item("VarianceComments")) Then
                    txtVarianceComments.Clear()
                Else
                    txtVarianceComments.Text = dr.Item("VarianceComments")
                End If
                If IsDBNull(dr.Item("numAdminFee")) Then
                    txtAdminFee.Text = "0"
                Else
                    txtAdminFee.Text = Format(dr.Item("numAdminFee"), "c")
                End If
                If IsDBNull(dr.Item("AllFees")) Then
                    txtAllFees.Text = "ERROR"
                Else
                    txtAllFees.Text = Format(dr.Item("AllFees"), "c")
                End If
            End While
            dr.Close()
            ' Format(dr.Item("numTotalFee"), "c")
            SQL = "select " & _
            "datPayDate, numPayment, " & _
            "strPayType, strDepositNo, " & _
            "strCheckNo, strBatchNo, " & _
            "intFiscalYear, strInvoiceNo, " & _
            "strComments " & _
            "from " & DBNameSpace & ".FSAddPaid " & _
            "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
            "and intYear = '" & txtSelectedYear.Text & "' " & _
            "order by datPayDate "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            da.Fill(ds, "PaymentStats")
            dgvStats.DataSource = ds
            dgvStats.DataMember = "PaymentStats"

            dgvStats.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvStats.AllowUserToResizeColumns = True
            dgvStats.AllowUserToResizeRows = True
            dgvStats.AllowUserToAddRows = False
            dgvStats.AllowUserToDeleteRows = False
            dgvStats.AllowUserToOrderColumns = True
            dgvStats.Columns("datPayDate").HeaderText = "Pay Date"
            dgvStats.Columns("datPayDate").DisplayIndex = 0
            dgvStats.Columns("datPayDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvStats.Columns("numPayment").HeaderText = "Amount Paid"
            dgvStats.Columns("numPayment").DisplayIndex = 1
            dgvStats.Columns("numPayment").DefaultCellStyle.Format = "c"
            dgvStats.Columns("strPayType").HeaderText = "Pay Type"
            dgvStats.Columns("strPayType").DisplayIndex = 2
            dgvStats.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvStats.Columns("strDepositNo").DisplayIndex = 3
            dgvStats.Columns("strCheckNo").HeaderText = "Check No"
            dgvStats.Columns("strCheckNo").DisplayIndex = 4
            dgvStats.Columns("strBatchNo").HeaderText = "Batch #"
            dgvStats.Columns("strBatchNo").DisplayIndex = 5
            dgvStats.Columns("intFiscalYear").HeaderText = "Fiscal Year"
            dgvStats.Columns("intFiscalYear").DisplayIndex = 6
            dgvStats.Columns("strInvoiceNo").HeaderText = "Invoice #"
            dgvStats.Columns("strInvoiceNo").DisplayIndex = 7
            dgvStats.Columns("strComments").HeaderText = "Comments"
            dgvStats.Columns("strComments").DisplayIndex = 8

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvDepositsAndPayments_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDepositsAndPayments.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvDepositsAndPayments.HitTest(e.X, e.Y)
        Try
            If dgvDepositsAndPayments.RowCount > 0 And hti.RowIndex <> -1 Then
                txtSelectedAIRSNumber.Text = dgvDepositsAndPayments(0, hti.RowIndex).Value
                txtSelectedFacilityName.Text = dgvDepositsAndPayments(1, hti.RowIndex).Value
                txtSelectedYear.Text = cboStatYear.Text
                pnlDetails.Dock = DockStyle.None
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnHideResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideResults.Click
        Try
            If pnlDetails.Dock = DockStyle.None Then
                pnlDetails.Dock = DockStyle.Top
            Else
                pnlDetails.Dock = DockStyle.None
            End If
            pnlCorrectPaymentType.Visible = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If dgvDepositsAndPayments.RowCount <> 0 Then
                With ExcelApp
                    .SheetsInNewWorkbook = 1
                    .Workbooks.Add()
                    .Worksheets(1).Select()

                    'For displaying the column name in the the excel file.
                    For i = 0 To dgvDepositsAndPayments.ColumnCount - 1
                        .Cells(1, i + 1) = dgvDepositsAndPayments.Columns(i).HeaderText.ToString
                    Next

                    For i = 0 To dgvDepositsAndPayments.ColumnCount - 1
                        For j = 0 To dgvDepositsAndPayments.RowCount - 1
                            .Cells(j + 2, i + 1).numberformat = "@"
                            .Cells(j + 2, i + 1).value = dgvDepositsAndPayments.Item(i, j).Value.ToString
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
        Finally
        End Try
    End Sub
    Private Sub btnCorrectPaymentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCorrectPaymentType.Click
        Try
            If pnlCorrectPaymentType.Visible = False Then
                pnlCorrectPaymentType.Visible = True
            Else
                pnlCorrectPaymentType.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUpdatePaymentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePaymentType.Click
        Try
            If txtSelectedAIRSNumber.Text <> "" And cboNewPaymentType.Text <> "" Then
                SQL = "Update " & DBNameSpace & ".FSPayAndSubmit set " & _
                "strPaymentType = '" & cboNewPaymentType.Text & "' " & _
                "where strAIRSNumber = '0413" & txtSelectedAIRSNumber.Text & "' " & _
                "and intYear = '" & txtSelectedYear.Text & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                txtPaymentType.Text = cboNewPaymentType.Text
                pnlCorrectPaymentType.Visible = False
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnsaveRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveRate.Click

        Dim feeYear As Integer = CInt(cboFeeRateYear.SelectedItem)
        Dim TitleVFee As String = txtTitleVfee.Text
        Dim SMFee As String = txtAnnualSMFee.Text
        Dim NSPSFee As String = txtAnnualNSPSFee.Text
        Dim PerTonRate As String = txtperTonRate.Text
        Dim AdminFeepercent As String = txtAdminFeePercent.Text
        Dim LateFeeDate As String
        Dim FeeDueDate As String

        If dtpduedate.Checked = True Then
            LateFeeDate = dtpduedate.Text
        Else
            LateFeeDate = ""
        End If
        If dtpFeeDueDate.Checked = True Then
            FeeDueDate = dtpFeeDueDate.Text
        Else
            FeeDueDate = ""
        End If

        Try
            SQL = "Select " & DBNameSpace & ".FSFEERATES.INTYEAR  " & _
            "from " & DBNameSpace & ".FSFEERATES " & _
            "where FSFEERATES.INTYEAR = '" & feeYear & "' "
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            recExist = dr.Read
            dr.Close()
            If recExist = True Then
                SQL = "update " & DBNameSpace & ".FSFEERATES set " & _
                "FSFEERATES.TITLEVFEE = '" & TitleVFee & "', " & _
                "FSFEERATES.SMFEE = '" & SMFee & "', " & _
                "FSFEERATES.PERTONRATE = '" & PerTonRate & "', " & _
                "FSFEERATES.NSPSFEE = '" & NSPSFee & "', " & _
                "FSFEERATES.PART70FEE = '" & TitleVFee & "', " & _
                "FSFEERATES.AdminFEEPERCENT = '" & AdminFeepercent & "', " & _
                "FSFEERATES.DUEDATE = '" & LateFeeDate & "', " & _
                "FSFEERATES.DatFeeDue = '" & FeeDueDate & "' " & _
                "where FSFEERATES.INTYEAR = '" & feeYear & "' "
                MsgBox("The fee rate info has been updated.", MsgBoxStyle.Information, "Mailout and Stats")
            Else

                SQL = "Insert into " & DBNameSpace & ".FSFEERATES " & _
                "(FSFEERATES.INTYEAR, " & _
                "FSFEERATES.TITLEVFEE, " & _
                "FSFEERATES.SMFEE, " & _
                "FSFEERATES.PERTONRATE, " & _
                "FSFEERATES.NSPSFEE, " & _
                "FSFEERATES.PART70FEE, " & _
                "FSFEERATES.AdminFEEPERCENT, " & _
                "FSFEERATES.DUEDATE, " & _
                "FSFEERATES.DATFEEDUE) " & _
                "values (" & _
                "'" & Replace(feeYear, "'", "''") & "', " & _
                "'" & Replace(TitleVFee, "'", "''") & "', " & _
                "'" & Replace(SMFee, "'", "''") & "', " & _
                "'" & Replace(PerTonRate, "'", "''") & "', " & _
                "'" & Replace(NSPSFee, "'", "''") & "', " & _
                 "'" & Replace(TitleVFee, "'", "''") & "', " & _
                "'" & Replace(AdminFeepercent, "'", "''") & "', " & _
                "'" & Replace(LateFeeDate, "'", "''") & "' " & _
                "'" & Replace(FeeDueDate, "'", "''") & "' )"
                MsgBox("The new fee rate info has been added!", MsgBoxStyle.Information, "Mailout and Stats")
            End If
            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub

#Region "Late Fee Payer Report"
    Sub LoadFeeYear()
        Try

            SQL = "Select " & _
            "distinct(intYear) as FeeYear " & _
            "from " & DBNameSpace & ".FSPayAndSubmit " & _
            "order by intYear desc "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("FeeYear")) Then
                Else
                    cboFeeYear.Items.Add(dr.Item("FeeYear"))
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

    Private Sub btnRunLateFeeReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunLateFeeReport.Click
        Try
            Dim AIRSNumber As String = ""
            Dim FacilityName As String = ""
            Dim County As String = ""
            Dim Classification As String = ""
            Dim OperationalStatus As String = ""
            Dim SIC As String = ""
            Dim NSPS As String = ""
            Dim TV As String = ""
            Dim LastApp As String = ""
            Dim AppDate As String = ""
            Dim PermitNumber As String = ""
            Dim PendingApp As String = ""
            Dim LastCompliance As String = ""
            Dim ComplianceDate As String = ""
            Dim FeeYear As String = ""

            dgvLateFeePayerReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeePayerReport.AllowUserToDeleteRows = False
            dgvLateFeePayerReport.AllowUserToResizeColumns = True
            dgvLateFeePayerReport.AllowUserToAddRows = False
            dgvLateFeePayerReport.Columns.Add("AIRSNumber", "AIRS Number")
            dgvLateFeePayerReport.Columns.Add("strFacilityName", "Facility Name")
            dgvLateFeePayerReport.Columns.Add("strCountyName", "County Name")
            dgvLateFeePayerReport.Columns.Add("strClass", "Classification")
            dgvLateFeePayerReport.Columns.Add("strOperationalStatus", "Operating Status")
            dgvLateFeePayerReport.Columns.Add("strSICCode", "SIC")
            dgvLateFeePayerReport.Columns.Add("NSPSStatus", "NSPS (Y/N)")
            dgvLateFeePayerReport.Columns.Add("TVStatus", "TV (Y/N)")
            dgvLateFeePayerReport.Columns.Add("FeeYear", "Fee Year")
            dgvLateFeePayerReport.Columns.Add("ComplianceEvent", "Compliance Event")
            dgvLateFeePayerReport.Columns.Add("ComplianceDate", "Compliance Date")
            dgvLateFeePayerReport.Columns.Add("PermittingAction", "Permitting Action")
            dgvLateFeePayerReport.Columns.Add("PermittingDate", "Permitting Date")
            dgvLateFeePayerReport.Columns("PermittingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvLateFeePayerReport.Columns.Add("PermitNumber", "Permit Number")
            dgvLateFeePayerReport.Columns.Add("PendingPermit", "Pending Permit")


            SQL = "select " & _
            "substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, strCountyName, " & _
            "strClass, " & _
            "case " & _
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
            "else strOperationalStatus " & _
            "End strOperationalStatus, " & _
            "strSICCode, " & _
            "case " & _
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end NSPSStatus, " & _
            "case " & _
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end TVStatus, " & _
            "'" & cboFeeYear.Text & "' as FeeYear " & _
            "from " & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".APBFacilityInformation, " & _
            "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".APBHeaderData " & _
            "where " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & _
            "and " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
            "and substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber,5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
            "and intYear = '" & cboFeeYear.Text & "' " & _
            "and intSubmittal = '0' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                AIRSNumber = ""
                FacilityName = ""
                County = ""
                Classification = ""
                OperationalStatus = ""
                SIC = ""
                NSPS = ""
                TV = ""
                FeeYear = ""
                LastApp = ""
                AppDate = ""
                PermitNumber = ""
                PendingApp = ""
                LastCompliance = ""
                ComplianceDate = ""

                If IsDBNull(dr.Item("AIRSNumber")) Then
                    AIRSNumber = ""
                Else
                    AIRSNumber = dr.Item("AIRSNumber")
                End If
                If IsDBNull(dr.Item("strFacilityName")) Then
                    FacilityName = ""
                Else
                    FacilityName = dr.Item("strFacilityName")
                End If
                If IsDBNull(dr.Item("strCountyName")) Then
                    County = ""
                Else
                    County = dr.Item("strCountyName")
                End If
                If IsDBNull(dr.Item("strClass")) Then
                    Classification = ""
                Else
                    Classification = dr.Item("strClass")
                End If
                If IsDBNull(dr.Item("strOperationalStatus")) Then
                    OperationalStatus = ""
                Else
                    OperationalStatus = dr.Item("strOperationalStatus")
                End If
                If IsDBNull(dr.Item("strSICCode")) Then
                    SIC = ""
                Else
                    SIC = dr.Item("strSICCode")
                End If
                If IsDBNull(dr.Item("NSPSStatus")) Then
                    NSPS = ""
                Else
                    NSPS = dr.Item("NSPSStatus")
                End If
                If IsDBNull(dr.Item("TVStatus")) Then
                    TV = ""
                Else
                    TV = dr.Item("TVStatus")
                End If
                If IsDBNull(dr.Item("FeeYear")) Then
                    FeeYear = ""
                Else
                    FeeYear = dr.Item("FeeYear")
                End If

                SQL = "select " & _
                "max(to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as LastApp " & _
                "from AirBranch.SSPPApplicationMaster " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                "and datFinalizedDate is not null"

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("LastApp")) Then
                        LastApp = ""
                    Else
                        LastApp = dr2.Item("LastApp")
                    End If
                End While
                dr2.Close()

                If LastApp <> "" Then
                    SQL = "select " & _
                    "strApplicationTypeDesc, strPermitNumber, " & _
                    "case " & _
                    "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " & _
                    "else to_char(datPermitIssued, 'dd-Mon-yyyy') " & _
                    "end FinalDate " & _
                    "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".LookUpApplicationTypes, " & _
                    "" & DBNameSpace & ".SSPPApplicationTracking, " & DBNameSpace & ".SSPPApplicationData  " & _
                    "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber (+) " & _
                    "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                            'LastApp = LastApp
                        Else
                            LastApp = LastApp & " - " & dr2.Item("strApplicationTypeDesc")
                        End If
                        If IsDBNull(dr2.Item("FinalDate")) Then
                            AppDate = ""
                        Else
                            AppDate = dr2.Item("FinalDate")
                        End If
                        If IsDBNull(dr2.Item("strPermitNumber")) Then
                            PermitNumber = ""
                        Else
                            PermitNumber = dr2.Item("strPermitNumber")
                            If PermitNumber.Length = 15 Then
                                If IsNumeric(Mid(PermitNumber, 1, 4)) Then
                                    PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & _
                                                     Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" & _
                                                      Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                                End If
                            End If
                        End If
                    End While
                    dr2.Close()
                End If

                SQL = "select " & _
                "strApplicationNumber " & _
                "from " & DBNameSpace & ".SSPPApplicationMaster " & _
                "where datfinalizedDate Is null " & _
                "and strAIRSNumber = '0413" & AIRSNumber & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr2 = cmd.ExecuteReader
                PendingApp = "No"
                While dr2.Read
                    If IsDBNull(dr2.Item("strApplicationNumber")) Then
                        PendingApp = "No"
                    Else
                        PendingApp = dr2.Item("strApplicationNumber")
                    End If
                End While
                dr2.Close()
               
                SQL = "select " & _
                "max(datReceiveddate) as MaxDate " & _
                "from AIRBranch.SSCPItemMaster " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, DBConn)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        ComplianceDate = dr2.Item("MaxDate")
                        LastCompliance = "Item"
                    End If
                End While
                dr2.Close()

                SQL = "select " & _
                "max(datFCECompleted) as MaxDate " & _
                "from " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".SSCPFCE  " & _
                "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                "and strAIRSnumber = '0413" & AIRSNumber & "' "

                cmd = New OracleCommand(SQL, DBConn)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        If ComplianceDate <> "" Then
                            If ComplianceDate < dr2.Item("maxdate") Then
                                ComplianceDate = dr2.Item("MaxDate")
                                LastCompliance = "FCE"
                            End If
                        Else
                            ComplianceDate = dr2.Item("MaxDate")
                            LastCompliance = "FCE"
                        End If
                    End If
                End While
                dr2.Close()

                SQL = "select " & _
                "max(datEnforcementFinalized) as MaxDate " & _
                "from AIRBranch.SSCP_AuditedEnforcement " & _
                "where strAIRSnumber = '0413" & AIRSNumber & "'"

                cmd = New OracleCommand(SQL, DBConn)
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("MaxDate")) Then
                        ComplianceDate = ""
                    Else
                        If ComplianceDate <> "" Then
                            If ComplianceDate < dr2.Item("maxdate") Then
                                ComplianceDate = dr2.Item("MaxDate")
                                LastCompliance = "Enforcement"
                            End If
                        Else
                            ComplianceDate = dr2.Item("MaxDate")
                            LastCompliance = "Enforcement"
                        End If
                    End If
                End While
                dr2.Close()

                Select Case LastCompliance
                    Case "Item"
                        SQL = "select strTrackingNumber, datCompleteDate, " & _
                        "strActivityName " & _
                        "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".LookupComplianceActivities  " & _
                        "where " & DBNameSpace & ".SSCPItemMaster.strEventType = " & DBNameSpace & ".LookUpComplianceActivities.strActivityType  " & _
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and datCompleteDate = (select max(datCompleteDate) from " & DBNameSpace & ".SSCPItemMaster " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strTrackingNumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strTrackingNumber")
                            End If
                            If IsDBNull(dr2.Item("strActivityName")) Then
                                'LastCompliance = LastCompliance
                            Else
                                LastCompliance = LastCompliance & " - " & dr2.Item("strActivityName")
                            End If
                            If IsDBNull(dr2.Item("datCompleteDate")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datCompleteDate")
                            End If
                        End While
                    Case "FCE"
                        SQL = "select " & _
                        "" & DBNameSpace & ".SSCPFCE.strFCENumber, datFCECompleted " & _
                        "from " & DBNameSpace & ".SSCPFCE, " & DBNameSpace & ".SSCPFCEMaster  " & _
                        "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                        "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and " & DBNameSpace & ".SSCPFCE.datFCECompleted = (select " & _
                        "max(datFCECompleted) " & _
                        "from " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".SSCPFCE  " & _
                        "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                        "and strAIRSnumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strFCENumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strFCENumber") & " - FCE "
                            End If
                            If IsDBNull(dr2.Item("datFCECompleted")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datFCECompleted")
                            End If
                        End While
                        dr2.Close()
                    Case "Enforcement"
                        SQL = "select " & _
                        "strEnforcementNumber, datEnforcementFinalized " & _
                        "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and datEnforcementFinalized = (Select " & _
                        "max(datEnforcementFinalized) " & _
                        "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                        "where strairsnumber = '0413" & AIRSNumber & "') "

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr2 = cmd.ExecuteReader
                        While dr2.Read
                            If IsDBNull(dr2.Item("strEnforcementNumber")) Then
                                LastCompliance = ""
                            Else
                                LastCompliance = dr2.Item("strEnforcementNumber") & " - Enforcement "
                            End If
                            If IsDBNull(dr2.Item("datEnforcementFinalized")) Then
                                ComplianceDate = ""
                            Else
                                ComplianceDate = dr2.Item("datEnforcementFinalized")
                            End If
                        End While
                        dr2.Close()
                    Case Else
                End Select

                dgvLateFeePayerReport.Rows.Add(AIRSNumber, FacilityName, County, Classification, _
                                               OperationalStatus, SIC, NSPS, TV, FeeYear, LastCompliance, ComplianceDate, _
                                               LastApp, AppDate, PermitNumber, PendingApp)
            End While
            dr.Close()

            txtFeeCount.Text = dgvLateFeePayerReport.RowCount.ToString
            dgvLateFeePayerReport.Columns("PermittingDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            

        Catch ex As Exception
          


            ' ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRunReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunReport.Click
        Try
            SQL = "select " & _
            "substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, strCountyName, " & _
            "strClass, " & _
            "case " & _
            "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
            "else strOperationalStatus " & _
            "End strOperationalStatus, " & _
            "strSICCode, " & _
            "case " & _
            "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end NSPSStatus, " & _
            "case " & _
            "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
            "else 'No' " & _
            "end TVStatus, " & _
            "'" & cboFeeYear.Text & "' as FeeYear " & _
            "from " & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".APBFacilityInformation, " & _
            "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".APBHeaderData " & _
            "where " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & _
            "and " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
            "and substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber,5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
            "and intYear = '" & cboFeeYear.Text & "' " & _
            "and intSubmittal = '0' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            da.Fill(ds, "LateFeeReport")

            dgvLateFeeReport.DataSource = ds
            dgvLateFeeReport.DataMember = "LateFeeReport"

            dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeeReport.AllowUserToResizeColumns = True
            dgvLateFeeReport.AllowUserToResizeRows = True
            dgvLateFeeReport.AllowUserToAddRows = False
            dgvLateFeeReport.AllowUserToDeleteRows = False
            dgvLateFeeReport.AllowUserToOrderColumns = True
            dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
            dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
            dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
            dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
            dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
            dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
            dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
            dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
            dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
            dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
            dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
            dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
            dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
            dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
            dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
            dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 8

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnCheckforFeesPaid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckforFeesPaid.Click
        Try
            If rdbHasPaidFee.Checked = True Then
                SQL = "select  " & _
                "substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber,   " & _
                "strFacilityName, strCountyName,   " & _
                "strClass,  " & _
                "case   " & _
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate   " & _
                "else strOperationalStatus   " & _
                "End strOperationalStatus,  " & _
                "strSICCode,  " & _
                "case   " & _
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes'   " & _
                "else 'No'   " & _
                "end NSPSStatus, " & _
                "case " & _
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end TVStatus, " & _
                "sum(numPayment) TotalPaid, " & _
                 "'" & cboFeeYear.Text & "' as FeeYear " & _
                "from " & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".APBFacilityInformation,   " & _
                "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".APBHeaderData,  " & _
                "" & DBNameSpace & ".FSAddPaid  " & _
                "where " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber   " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber   " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSnumber   " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.intYear = " & DBNameSpace & ".FSAddPaid.intYear  " & _
                "and substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber,5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode   " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.intYear = '" & cboFeeYear.Text & "'   " & _
                "and intSubmittal = '0'   " & _
                "group by " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber, strFacilityName, strCountyName,   " & _
                "strClass, strOperationalStatus, datShutDownDate, strSICCode, strAirProgramCodes  " & _
                "order by AIRSNumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                da.Fill(ds, "LateFeeReport")

                dgvLateFeeReport.DataSource = ds
                dgvLateFeeReport.DataMember = "LateFeeReport"

                dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvLateFeeReport.AllowUserToResizeColumns = True
                dgvLateFeeReport.AllowUserToResizeRows = True
                dgvLateFeeReport.AllowUserToAddRows = False
                dgvLateFeeReport.AllowUserToDeleteRows = False
                dgvLateFeeReport.AllowUserToOrderColumns = True
                dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
                dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
                dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
                dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
                dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
                dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
                dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
                dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
                dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
                dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
                dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
                dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
                dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
                dgvLateFeeReport.Columns("TotalPaid").HeaderText = "Total Paid"
                dgvLateFeeReport.Columns("TotalPaid").DisplayIndex = 8
                dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
                dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 9
            Else
                SQL = "select " & _
                "substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber, 5) as AIRSNumber, " & _
                "strFacilityName, strCountyName, " & _
                "strClass, " & _
                "case " & _
                "when strOperationalStatus = 'X' then 'X - ' ||datShutDownDate " & _
                "else strOperationalStatus " & _
                "End strOperationalStatus, " & _
                "strSICCode, " & _
                "case " & _
                "when substr(strAirProgramCodes, 8, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end NSPSStatus, " & _
                "case " & _
                "when substr(strAirProgramCodes, 13, 1) = '1' then 'Yes' " & _
                "else 'No' " & _
                "end TVStatus, " & _
                 "'" & cboFeeYear.Text & "' as FeeYear " & _
                "from " & DBNameSpace & ".FSPayAndSubmit, " & DBNameSpace & ".APBFacilityInformation, " & _
                "" & DBNameSpace & ".LookUpCountyInformation, " & DBNameSpace & ".APBHeaderData " & _
                "where " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber = " & DBNameSpace & ".APBHeaderData.strAIRSNumber " & _
                "and substr(" & DBNameSpace & ".FSPayAndSubmit.strAIRSNumber,5, 3) = " & DBNameSpace & ".LookUpCountyInformation.strCountyCode " & _
                "and intYear = '" & cboFeeYear.Text & "' " & _
                "and intSubmittal = '0' " & _
                "and not exists (select * from " & DBNameSpace & ".FSAddPaid " & _
                "where " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber = " & DBNameSpace & ".FSAddPaid.strAIRSnumber " & _
                "and " & DBNameSpace & ".FSPayAndSubmit.intYear = " & DBNameSpace & ".FSAddPaid.intYear) " & _
                "order by AIRSNumber "

                ds = New DataSet
                da = New OracleDataAdapter(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                da.Fill(ds, "LateFeeReport")

                dgvLateFeeReport.DataSource = ds
                dgvLateFeeReport.DataMember = "LateFeeReport"

                dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
                dgvLateFeeReport.AllowUserToResizeColumns = True
                dgvLateFeeReport.AllowUserToResizeRows = True
                dgvLateFeeReport.AllowUserToAddRows = False
                dgvLateFeeReport.AllowUserToDeleteRows = False
                dgvLateFeeReport.AllowUserToOrderColumns = True
                dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
                dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
                dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
                dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1
                dgvLateFeeReport.Columns("strCountyName").HeaderText = "County Name"
                dgvLateFeeReport.Columns("strCountyName").DisplayIndex = 2
                dgvLateFeeReport.Columns("strClass").HeaderText = "Classification"
                dgvLateFeeReport.Columns("strClass").DisplayIndex = 3
                dgvLateFeeReport.Columns("strOperationalStatus").HeaderText = "Operating Status"
                dgvLateFeeReport.Columns("strOperationalStatus").DisplayIndex = 4
                dgvLateFeeReport.Columns("strSICCode").HeaderText = "SIC"
                dgvLateFeeReport.Columns("strSICCode").DisplayIndex = 5
                dgvLateFeeReport.Columns("NSPSStatus").HeaderText = "NSPS (Y/N)"
                dgvLateFeeReport.Columns("NSPSStatus").DisplayIndex = 6
                dgvLateFeeReport.Columns("TVStatus").HeaderText = "TV (Y/N)"
                dgvLateFeeReport.Columns("TVStatus").DisplayIndex = 7
                dgvLateFeeReport.Columns("FeeYear").HeaderText = "Fee Year"
                dgvLateFeeReport.Columns("FeeYear").DisplayIndex = 8
            End If

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnRemovePaidFacilities_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePaidFacilities.Click
        Try
            Dim AIRSNumber As String = ""
            Dim i As Integer
            temp = dgvLateFeeReport.ColumnCount.ToString

            If dgvLateFeeReport.ColumnCount.ToString = 10 Then
                If dgvLateFeeReport.Columns(8).HeaderText = "Total Paid" Then
                    For i = 0 To dgvLateFeeReport.RowCount.ToString - 1
                        AIRSNumber = dgvLateFeeReport(0, i).Value

                        SQL = "update " & DBNameSpace & ".FSPayAndSubmit set " & _
                        "intSubmittal = '1' " & _
                        "where strAIRSnumber = '0413" & AIRSNumber & "' " & _
                        "and intYear = '" & cboFeeYear.Text & "' "

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Connecting Then
                            DBConn.Close()
                        End If
                        dr = cmd.ExecuteReader
                        dr.Close()

                        temp = "0"

                        SQL = "Select " & _
                        "count(*) as FSCalc " & _
                        "from " & DBNameSpace & ".FSCalculations " & _
                        "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                        "and intYear = '" & cboFeeYear.Text & "' "
                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        While dr.Read
                            If IsDBNull(dr.Item("FSCalc")) Then
                                temp = "0"
                            Else
                                temp = dr.Item("FSCalc")
                            End If
                        End While
                        dr.Close()
                        If temp = "0" Then
                            SQL = "Insert into " & DBNameSpace & ".FSCalculations " & _
                            "values " & _
                            "('0413" & AIRSNumber & "', '" & cboFeeYear.Text & "', " & _
                            "'0', '0', '0', '0', " & _
                            "'0', '0', '0', '0', " & _
                            "'NO', '0', 'YES', '33.0', " & _
                            "'', 'No', 'No', '0', " & _
                            "'', '', '', '', '', '0') "

                            cmd = New OracleCommand(SQL, DBConn)
                            If DBConn.State = ConnectionState.Closed Then
                                DBConn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        End If
                    Next

                    MessageBox.Show("Removal Complete", "Fee Stats & Mailout", MessageBoxButtons.OK)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewUnenrolled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewUnenrolled.Click
        Try
            SQL = "select " & _
            "substr(" & DBNameSpace & ".FEEMailOut.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName " & _
            "from " & DBNameSpace & ".FeeMailout " & _
            "where intYear = '" & cboFeeYear.Text & "' " & _
            "and not exists (select * from " & DBNameSpace & ".FSPayAndSubmit " & _
            "where " & DBNameSpace & ".FeeMailOut.strAIRSnumber = " & DBNameSpace & ".FSPayAndSubmit.strAIRSnumber " & _
            "and " & DBNameSpace & ".FeeMailOut.intYear = " & DBNameSpace & ".FSPayAndSubmit.intYear) "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            da.Fill(ds, "LateFeeReport")

            dgvLateFeeReport.DataSource = ds
            dgvLateFeeReport.DataMember = "LateFeeReport"

            dgvLateFeeReport.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvLateFeeReport.AllowUserToResizeColumns = True
            dgvLateFeeReport.AllowUserToResizeRows = True
            dgvLateFeeReport.AllowUserToAddRows = False
            dgvLateFeeReport.AllowUserToDeleteRows = False
            dgvLateFeeReport.AllowUserToOrderColumns = True
            dgvLateFeeReport.Columns("AIRSNumber").HeaderText = "AIRS Number"
            dgvLateFeeReport.Columns("AIRSNumber").DisplayIndex = 0
            dgvLateFeeReport.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvLateFeeReport.Columns("strFacilityName").DisplayIndex = 1

            txtFeeCount.Text = dgvLateFeeReport.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvLateFeeReport_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvLateFeeReport.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvLateFeeReport.HitTest(e.X, e.Y)
            If dgvLateFeeReport.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvLateFeeReport.Columns(0).HeaderText = "AIRS Number" Then
                    If IsDBNull(dgvLateFeeReport(0, hti.RowIndex).Value) Then
                        txtFeeAIRSNumber.Clear()
                    Else
                        txtFeeAIRSNumber.Text = dgvLateFeeReport(0, hti.RowIndex).Value
                    End If
                    If IsDBNull(dgvLateFeeReport(1, hti.RowIndex).Value) Then
                        txtFeeFacilityName.Clear()
                    Else
                        txtFeeFacilityName.Text = dgvLateFeeReport(1, hti.RowIndex).Value
                    End If

                    txtFeeComplianceEvent.Clear()
                    txtFeeComplianceEventType.Clear()
                    txtFeeLastComplianceEvent.Clear()
                    txtFeePermittingEvent.Clear()
                    txtFeePermittingEventType.Clear()
                    txtFeePermittingDate.Clear()
                    txtFeePermitNumber.Clear()
                    txtFeePendingPermit.Clear()
                    txtFeePendingPermitType.Clear()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadFeeData()
        Try
            Dim AIRSNumber As String
            Dim ComplianceDate As String = ""
            Dim ComplianceType As String = ""
            Dim LastCompliance As String = ""
            Dim LastApp As String = ""
            Dim AppDate As String = ""
            Dim AppType As String = ""
            Dim PermitNumber As String = ""
            Dim PendingApp As String = ""
            Dim PendingAppType As String = ""

            AIRSNumber = txtFeeAIRSNumber.Text

            SQL = "select " & _
            "max(datReceiveddate) as MaxDate " & _
            "from AIRBranch.SSCPItemMaster " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, DBConn)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    ComplianceDate = dr2.Item("MaxDate")
                    ComplianceType = "Item"
                End If
            End While
            dr2.Close()

            SQL = "select " & _
            "max(datFCECompleted) as MaxDate " & _
            "from " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".SSCPFCE  " & _
            "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
            "and strAIRSnumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, DBConn)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    If ComplianceDate <> "" Then
                        If ComplianceDate < dr2.Item("maxdate") Then
                            ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                            ComplianceType = "FCE"
                        End If
                    Else
                        ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                        ComplianceType = "FCE"
                    End If
                End If
            End While
            dr2.Close()

            SQL = "select " & _
            "max(datEnforcementFinalized) as MaxDate " & _
            "from AIRBranch.SSCP_AuditedEnforcement " & _
            "where strAIRSnumber = '0413" & AIRSNumber & "'"

            cmd = New OracleCommand(SQL, DBConn)
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("MaxDate")) Then
                    ComplianceDate = ""
                Else
                    If ComplianceDate <> "" Then
                        If ComplianceDate < dr2.Item("maxdate") Then
                            ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                            ComplianceType = "Enforcement"
                        End If
                    Else
                        ComplianceDate = Format(dr2.Item("MaxDate"), "dd-MMM-yyyy")
                        ComplianceType = "Enforcement"
                    End If
                End If
            End While
            dr2.Close()

            Select Case ComplianceType
                Case "Item"
                    SQL = "select strTrackingNumber, datCompleteDate, " & _
                    "strActivityName " & _
                    "from " & DBNameSpace & ".SSCPItemMaster, " & DBNameSpace & ".LookupComplianceActivities  " & _
                    "where " & DBNameSpace & ".SSCPItemMaster.strEventType = " & DBNameSpace & ".LookUpComplianceActivities.strActivityType  " & _
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and datCompleteDate = (select max(datCompleteDate) from " & DBNameSpace & ".SSCPItemMaster " & _
                    "where strAIRSNumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        If IsDBNull(dr2.Item("strTrackingNumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strTrackingNumber")
                        End If
                        If IsDBNull(dr2.Item("strActivityName")) Then
                            ComplianceType = ""
                        Else
                            ComplianceType = dr2.Item("strActivityName")
                        End If
                        If IsDBNull(dr2.Item("datCompleteDate")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datCompleteDate"), "dd-MMM-yyyy")
                        End If
                    End While
                    lblComplianceDate.Text = "Date Completed"
                Case "FCE"
                    SQL = "select " & _
                    "" & DBNameSpace & ".SSCPFCE.strFCENumber, datFCECompleted " & _
                    "from " & DBNameSpace & ".SSCPFCE, " & DBNameSpace & ".SSCPFCEMaster  " & _
                    "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                    "and strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and " & DBNameSpace & ".SSCPFCE.datFCECompleted = (select " & _
                    "max(datFCECompleted) " & _
                    "from " & DBNameSpace & ".SSCPFCEMaster, " & DBNameSpace & ".SSCPFCE  " & _
                    "where " & DBNameSpace & ".SSCPFCEMaster.strFCENumber = " & DBNameSpace & ".SSCPFCE.strFCENumber " & _
                    "and strAIRSnumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        ComplianceType = "FCE"
                        If IsDBNull(dr2.Item("strFCENumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strFCENumber")
                        End If
                        If IsDBNull(dr2.Item("datFCECompleted")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datFCECompleted"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr2.Close()
                    lblComplianceDate.Text = "Date FCE Completed"
                Case "Enforcement"
                    SQL = "select " & _
                    "strEnforcementNumber, datEnforcementFinalized " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                    "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                    "and datEnforcementFinalized = (Select " & _
                    "max(datEnforcementFinalized) " & _
                    "from " & DBNameSpace & ".SSCP_AuditedEnforcement " & _
                    "where strairsnumber = '0413" & AIRSNumber & "') "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr2 = cmd.ExecuteReader
                    While dr2.Read
                        ComplianceType = "Enforcement"
                        If IsDBNull(dr2.Item("strEnforcementNumber")) Then
                            LastCompliance = ""
                        Else
                            LastCompliance = dr2.Item("strEnforcementNumber")
                        End If
                        If IsDBNull(dr2.Item("datEnforcementFinalized")) Then
                            ComplianceDate = ""
                        Else
                            ComplianceDate = Format(dr2.Item("datEnforcementFinalized"), "dd-MMM-yyyy")
                        End If
                    End While
                    dr2.Close()
                    lblComplianceDate.Text = "Date Enforcement Finalized"
                Case Else
                    lblComplianceDate.Text = "Date"
            End Select

            txtFeeComplianceEvent.Text = LastCompliance
            txtFeeComplianceEventType.Text = ComplianceType
            txtFeeLastComplianceEvent.Text = ComplianceDate

            SQL = "select " & _
            "max(to_number(AIRBranch.SSPPApplicationMaster.strApplicationNumber)) as LastApp " & _
            "from AirBranch.SSPPApplicationMaster " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
            "and datFinalizedDate is not null"

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr2 = cmd.ExecuteReader
            While dr2.Read
                If IsDBNull(dr2.Item("LastApp")) Then
                    LastApp = ""
                Else
                    LastApp = dr2.Item("LastApp")
                End If
            End While
            dr2.Close()

            If LastApp <> "" Then
                SQL = "select " & _
                "strApplicationTypeDesc, strPermitNumber, " & _
                "case " & _
                "when datPermitIssued is null then to_char(datFinalizedDate, 'dd-Mon-yyyy') " & _
                "else to_char(datPermitIssued, 'dd-Mon-yyyy') " & _
                "end FinalDate " & _
                "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".LookUpApplicationTypes, " & _
                "" & DBNameSpace & ".SSPPApplicationTracking, " & DBNameSpace & ".SSPPApplicationData  " & _
                "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode (+) " & _
                "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationTracking.strApplicationNumber (+) " & _
                "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = " & DBNameSpace & ".SSPPApplicationData.strApplicationNumber (+) " & _
                "and " & DBNameSpace & ".SSPPApplicationMaster.strApplicationNumber = '" & LastApp & "' "

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr2 = cmd.ExecuteReader
                While dr2.Read
                    If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                        AppType = ""
                    Else
                        AppType = dr2.Item("strApplicationTypeDesc")
                    End If
                    If IsDBNull(dr2.Item("FinalDate")) Then
                        AppDate = ""
                    Else
                        AppDate = dr2.Item("FinalDate")
                    End If
                    If IsDBNull(dr2.Item("strPermitNumber")) Then
                        PermitNumber = ""
                    Else
                        PermitNumber = dr2.Item("strPermitNumber")
                        If PermitNumber.Length = 15 Then
                            If IsNumeric(Mid(PermitNumber, 1, 4)) Then
                                PermitNumber = Mid(PermitNumber, 1, 4) & "-" & Mid(PermitNumber, 5, 3) & "-" & _
                                                 Mid(PermitNumber, 8, 4) & "-" & Mid(PermitNumber, 12, 1) & "-" & _
                                                  Mid(PermitNumber, 13, 2) & "-" & Mid(PermitNumber, 15)
                            End If
                        End If
                    End If
                End While
                dr2.Close()
            End If

            txtFeePermittingEvent.Text = LastApp
            txtFeePermittingEventType.Text = AppType
            txtFeePermittingDate.Text = AppDate
            txtFeePermitNumber.Text = PermitNumber

            SQL = "select " & _
            "strApplicationNumber " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster " & _
            "where datfinalizedDate Is null " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            SQL = "select " & _
            "strApplicationNumber, strApplicationTypeDesc " & _
            "from " & DBNameSpace & ".SSPPApplicationMaster, " & DBNameSpace & ".LookUpApplicationTypes " & _
            "where " & DBNameSpace & ".SSPPApplicationMaster.strApplicationType = " & DBNameSpace & ".LookUpApplicationTypes.strApplicationTypeCode (+) " & _
            "and datfinalizedDate Is null " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr2 = cmd.ExecuteReader
            PendingApp = "No"
            While dr2.Read
                If IsDBNull(dr2.Item("strApplicationNumber")) Then
                    PendingApp = "No"
                Else
                    PendingApp = dr2.Item("strApplicationNumber")
                End If
                If IsDBNull(dr2.Item("strApplicationTypeDesc")) Then
                    PendingAppType = ""
                Else
                    PendingAppType = dr2.Item("strApplicationTypeDesc")
                End If
            End While
            dr2.Close()

            txtFeePendingPermit.Text = PendingApp
            txtFeePendingPermitType.Text = PendingAppType

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeeFacilitySummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeFacilitySummary.Click
        Try
            If txtFeeAIRSNumber.Text <> "" Then
                If FacilitySummary Is Nothing Then
                    FacilitySummary = Nothing
                    If FacilitySummary Is Nothing Then FacilitySummary = New IAIPFacilitySummary
                    FacilitySummary.mtbAIRSNumber.Text = txtFeeAIRSNumber.Text
                    FacilitySummary.Show()
                Else
                    FacilitySummary.mtbAIRSNumber.Text = txtFeeAIRSNumber.Text
                    FacilitySummary.Show()
                End If
                FacilitySummary.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)

                FacilitySummary.LoadInitialData()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeeViewComplianceEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeViewComplianceEvent.Click
        Try
            If txtFeeComplianceEvent.Text <> "" Then
                Select Case txtFeeComplianceEventType.Text
                    Case "FCE"
                        If SSCPFCE Is Nothing Then
                            If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                            SSCPFCE.txtAirsNumber.Text = txtFeeAIRSNumber.Text
                            SSCPFCE.txtFacilityInformation.Text = txtFeeAIRSNumber.Text
                            SSCPFCE.Show()
                            SSCPFCE.txtFCENumber.Text = txtFeeComplianceEvent.Text
                        Else
                            SSCPFCE.Clear()
                            SSCPFCE = Nothing
                            If SSCPFCE Is Nothing Then SSCPFCE = New SSCPFCEWork
                            SSCPFCE.txtAirsNumber.Text = Me.txtFeeAIRSNumber.Text
                            SSCPFCE.txtFacilityInformation.Text = txtFeeAIRSNumber.Text
                            SSCPFCE.Show()
                            SSCPFCE.txtFCENumber.Text = txtFeeComplianceEvent.Text
                        End If
                        SSCPFCE.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Case "Enforcement"
                        If SSCP_Enforcement Is Nothing Then
                            If SSCP_Enforcement Is Nothing Then SSCP_Enforcement = New SSCPEnforcementAudit
                            SSCP_Enforcement.txtOrigin.Text = "Nav Screen"
                            If txtFeeComplianceEvent.Text <> "" Then
                                SSCP_Enforcement.txtEnforcementNumber.Text = txtFeeComplianceEvent.Text
                            End If
                            SSCP_Enforcement.Show()
                        Else
                            SSCP_Enforcement.Close()
                            SSCP_Enforcement = Nothing
                            If SSCP_Enforcement Is Nothing Then SSCP_Enforcement = New SSCPEnforcementAudit
                            SSCP_Enforcement.txtOrigin.Text = "Nav Screen"
                            If txtFeeComplianceEvent.Text <> "" Then
                                SSCP_Enforcement.txtEnforcementNumber.Text = txtFeeComplianceEvent.Text
                            End If
                            SSCP_Enforcement.Show()
                        End If
                        SSCP_Enforcement.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                    Case Else
                        Dim RefNum As String = ""
                        Dim DocType As String = ""

                        SQL = "Select " & _
                        "" & DBNameSpace & ".ISMPReportInformation.strReferenceNumber, " & DBNameSpace & ".ISMPDocumentType.strDocumentType " & _
                        "from " & DBNameSpace & ".SSCPTestReports, " & DBNameSpace & ".ISMPDocumentType, " & _
                        "" & DBNameSpace & ".ISMPReportInformation " & _
                        "where " & DBNameSpace & ".SSCPTestReports.strReferenceNumber = " & DBNameSpace & ".ISMPReportInformation.strReferenceNumber " & _
                        "and " & DBNameSpace & ".ISMPReportInformation.strDocumentType = " & DBNameSpace & ".ISMPDocumentType.strKey " & _
                        "and strTrackingNumber = '" & txtFeeComplianceEvent.Text & "' "

                        cmd = New OracleCommand(SQL, DBConn)
                        If DBConn.State = ConnectionState.Closed Then
                            DBConn.Open()
                        End If
                        dr = cmd.ExecuteReader
                        recExist = dr.Read
                        If recExist = True Then
                            RefNum = dr.Item("strReferenceNumber")
                            DocType = dr.Item("strDocumentType")
                        Else
                            RefNum = ""
                            DocType = ""
                        End If
                        dr.Close()
                        If RefNum <> "" Then
                            ISMPTestReportsEntry = Nothing
                            If ISMPTestReportsEntry Is Nothing Then ISMPTestReportsEntry = New ISMPTestReports
                            ISMPTestReportsEntry.txtReferenceNumber.Text = RefNum
                            ISMPTestReportsEntry.Show()
                        Else
                            If SSCPREports Is Nothing Then
                                SSCPREports = Nothing
                                If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                                SSCPREports.txtTrackingNumber.Text = txtFeeComplianceEvent.Text
                                SSCPREports.Show()
                            Else
                                SSCPREports.Close()
                                SSCPREports = Nothing
                                If SSCPREports Is Nothing Then SSCPREports = New SSCPEvents
                                SSCPREports.txtTrackingNumber.Text = txtFeeComplianceEvent.Text
                                SSCPREports.Show()
                            End If
                            SSCPREports.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                        End If
                End Select
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeeViewPermittingEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeeViewPermittingEvent.Click
        Try
            If txtFeePermittingEvent.Text <> "" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = Nothing
                    If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = txtFeePermittingEvent.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
                PermitTrackingLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                PermitTrackingLog.TPTrackingLog.Focus()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnFeePendingPermittingEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeePendingPermittingEvent.Click
        Try
            If txtFeePendingPermit.Text <> "" And txtFeePendingPermit.Text <> "No" Then
                If PermitTrackingLog Is Nothing Then
                    PermitTrackingLog = Nothing
                    If PermitTrackingLog Is Nothing Then PermitTrackingLog = New SSPPApplicationTrackingLog
                    PermitTrackingLog.Show()
                Else
                    PermitTrackingLog.Show()
                End If
                PermitTrackingLog.txtApplicationNumber.Clear()
                PermitTrackingLog.txtApplicationNumber.Text = txtFeePendingPermit.Text
                PermitTrackingLog.LoadApplication()
                PermitTrackingLog.BringToFront()
                PermitTrackingLog.Location = New System.Drawing.Point(DefaultX + 25, DefaultY)
                PermitTrackingLog.TPTrackingLog.Focus()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewData.Click
        Try
            If txtFeeAIRSNumber.Text <> "" Then
                LoadFeeData()
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnExportFeeReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportFeeReport.Click
        'Dim ExcelApp As New Excel.Application
        Dim ExcelApp As New Microsoft.Office.Interop.Excel.Application
        'Dim ExcelDoc As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j As Integer

        Try
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

            If TPQuickFeeReport.Focus = True Then
                If dgvLateFeeReport.RowCount <> 0 Then
                    With ExcelApp
                        .SheetsInNewWorkbook = 1
                        .Workbooks.Add()
                        .Worksheets(1).Select()

                        'For displaying the column name in the the excel file.
                        For i = 0 To dgvLateFeeReport.ColumnCount - 1
                            .Cells(1, i + 1) = dgvLateFeeReport.Columns(i).HeaderText.ToString
                        Next

                        For i = 0 To dgvLateFeeReport.ColumnCount - 1
                            For j = 0 To dgvLateFeeReport.RowCount - 1
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = dgvLateFeeReport.Item(i, j).Value.ToString
                            Next
                        Next

                    End With
                End If
            Else
                If dgvLateFeePayerReport.RowCount <> 0 Then
                    With ExcelApp
                        .SheetsInNewWorkbook = 1
                        .Workbooks.Add()
                        .Worksheets(1).Select()

                        'For displaying the column name in the the excel file.
                        For i = 0 To dgvLateFeePayerReport.ColumnCount - 1
                            .Cells(1, i + 1) = dgvLateFeePayerReport.Columns(i).HeaderText.ToString
                        Next

                        For i = 0 To dgvLateFeePayerReport.ColumnCount - 1
                            For j = 0 To dgvLateFeePayerReport.RowCount - 1
                                .Cells(j + 2, i + 1).numberformat = "@"
                                .Cells(j + 2, i + 1).value = dgvLateFeePayerReport.Item(i, j).Value.ToString
                            Next
                        Next

                    End With
                End If
            End If
            If ExcelApp.Visible = False Then
                ExcelApp.Visible = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnLoadNSPSTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadNSPSTool.Click
        Try
            'If pnlNSPSExemptions.Enabled = False Then
            '    ' pnlNSPSExemptions.Enabled = True
            '    LoadNSPSExemptions()
            '    LoadNSPSExemptionYear()
            '    LoadSelectedNSPSExemptions()
            'Else
            '    LoadNSPSExemptions()
            '    LoadNSPSExemptionYear()
            '    LoadSelectedNSPSExemptions()
            'End If
            LoadNSPSExemptions()
            LoadNSPSExemptionYear()
            LoadSelectedNSPSExemptions()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNSPSExemptions()
        Try
            SQL = "Select " & _
            "ReasonID, Reason " & _
            "from " & DBNameSpace & ".FSNSPSReason " & _
            "order by ReasonID "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            da.Fill(ds, "NSPSExemptions")
            dgvNSPSExemptions.DataSource = ds
            dgvNSPSExemptions.DataMember = "NSPSExemptions"

            dgvNSPSExemptions.RowHeadersVisible = False
            dgvNSPSExemptions.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptions.AllowUserToResizeColumns = True
            dgvNSPSExemptions.AllowUserToResizeRows = True
            dgvNSPSExemptions.AllowUserToAddRows = False
            dgvNSPSExemptions.AllowUserToDeleteRows = False
            dgvNSPSExemptions.AllowUserToOrderColumns = True
            dgvNSPSExemptions.Columns("ReasonID").HeaderText = "ID"
            dgvNSPSExemptions.Columns("ReasonID").DisplayIndex = 0
            dgvNSPSExemptions.Columns("Reason").HeaderText = "NSPS Exemption Reason"
            dgvNSPSExemptions.Columns("Reason").DisplayIndex = 1

            dgvNSPSExemptions.AutoResizeColumns()
            dgvNSPSExemptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            Dim dtNSPS As New DataTable
            Dim drDSRow As DataRow
            Dim drNewRow As DataRow

            dtNSPS.Columns.Add("ReasonID", GetType(System.String))
            dtNSPS.Columns.Add("Reason", GetType(System.String))

            drNewRow = dtNSPS.NewRow()
            drNewRow("ReasonID") = " "
            drNewRow("Reason") = " "
            dtNSPS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("NSPSExemptions").Rows()
                drNewRow = dtNSPS.NewRow()
                drNewRow("ReasonID") = drDSRow("ReasonID")
                drNewRow("Reason") = drDSRow("Reason")
                dtNSPS.Rows.Add(drNewRow)
            Next
            Dim temp As String

            temp = dtairs.Rows.Count

            With cboNSPSExemptions
                .DataSource = dtNSPS
                .DisplayMember = "Reason"
                .ValueMember = "ReasonID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNSPSExemptionYear()
        Try
            cboNSPSExemptionYear.Items.Clear()
            cboNSPSExemptionYear.Items.Add(Today.Year)

            SQL = "Select " & _
            "distinct(intYear) as NSPSYear " & _
            "from " & DBNameSpace & ".FSNSPSReasonYear " & _
            "order by intYear desc "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("NSPSYear")) Then
                Else
                    If cboNSPSExemptionYear.Items.Contains(dr.Item("NSPSYear")) Then
                    Else
                        cboNSPSExemptionYear.Items.Add(dr.Item("NSPSYear"))
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadSelectedNSPSExemptions()
        Try
            dgvNSPSExemptionsByYear.RowHeadersVisible = False
            dgvNSPSExemptionsByYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptionsByYear.AllowUserToResizeColumns = True
            dgvNSPSExemptionsByYear.AllowUserToAddRows = False
            dgvNSPSExemptionsByYear.AllowUserToDeleteRows = False
            dgvNSPSExemptionsByYear.AllowUserToOrderColumns = True
            dgvNSPSExemptionsByYear.AllowUserToResizeRows = True
            dgvNSPSExemptionsByYear.ColumnHeadersHeight = "35"

            dgvNSPSExemptionsByYear.Columns.Add("intYear", "Year")
            dgvNSPSExemptionsByYear.Columns("intYear").DisplayIndex = 0
            dgvNSPSExemptionsByYear.Columns("intYear").Width = 50
            dgvNSPSExemptionsByYear.Columns("intYear").Visible = True

            dgvNSPSExemptionsByYear.Columns.Add("ReasonID", "NSPS ID")
            dgvNSPSExemptionsByYear.Columns("ReasonID").DisplayIndex = 1
            dgvNSPSExemptionsByYear.Columns("ReasonID").Width = 100
            dgvNSPSExemptionsByYear.Columns("ReasonID").ReadOnly = True

            dgvNSPSExemptionsByYear.Columns.Add("displayOrder", "Display Order")
            dgvNSPSExemptionsByYear.Columns("displayOrder").DisplayIndex = 2
            dgvNSPSExemptionsByYear.Columns("displayOrder").Width = 100
            dgvNSPSExemptionsByYear.Columns("displayOrder").ReadOnly = False

            dgvNSPSExemptionsByYear.Columns.Add("Reason", "NSPS Exemption Reason")
            dgvNSPSExemptionsByYear.Columns("Reason").DisplayIndex = 3
            dgvNSPSExemptionsByYear.Columns("Reason").Width = 250
            dgvNSPSExemptionsByYear.Columns("Reason").ReadOnly = True

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddNSPSExemption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNSPSExemption.Click
        Try
            SQL = "Insert into " & DBNameSpace & ".FSNSPSReason " & _
            "values " & _
            "((select (max(reasonID) + 1) from " & DBNameSpace & ".FSNSPSReason), " & _
            "'" & Replace(txtNSPSExemption.Text, "'", "''") & "') "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            txtNSPSExemption.Clear()
            LoadNSPSExemptions()

            Dim maxRow As Integer
            maxRow = dgvNSPSExemptions.RowCount - 1
            If dgvNSPSExemptions.Rows.Count >= maxRow AndAlso maxRow >= 1 Then
                dgvNSPSExemptions.FirstDisplayedScrollingRowIndex = maxRow
                dgvNSPSExemptions.Rows(maxRow).Selected = True
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub dgvNSPSExemptions_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvNSPSExemptions.MouseUp
        Try
            Dim hti As DataGridView.HitTestInfo = dgvNSPSExemptions.HitTest(e.X, e.Y)
            If dgvNSPSExemptions.RowCount > 0 And hti.RowIndex <> -1 Then
                If dgvNSPSExemptions.Columns(0).HeaderText = "ID" Then
                    If IsDBNull(dgvNSPSExemptions(0, hti.RowIndex).Value) Then
                        txtDeleteNSPSExemptions.Clear()
                    Else
                        txtDeleteNSPSExemptions.Text = dgvNSPSExemptions(0, hti.RowIndex).Value
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnDeleteNSPSExemption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNSPSExemption.Click
        Try
            If txtDeleteNSPSExemptions.Text <> "" Then
                SQL = "Select " & _
                "strNSPSReason " & _
                "from " & DBNameSpace & ".FSCalculations " & _
                "where strNSPSReason = '" & txtDeleteNSPSExemptions.Text & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MessageBox.Show("Unable to Delete because this exemption has been used.")
                Else
                    SQL = "Delete " & DBNameSpace & ".FSNSPSReasonYear " & _
                    "where ReasonID = '" & txtDeleteNSPSExemptions.Text & "' "
                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    SQL = "Delete " & DBNameSpace & ".FSNSPSReason " & _
                    "where ReasonID = '" & txtDeleteNSPSExemptions.Text & "' "
                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()

                    txtDeleteNSPSExemptions.Clear()
                    LoadNSPSExemptions()
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnViewNSPSExemptionsByYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewNSPSExemptionsByYear.Click
        Try
            If cboNSPSExemptionYear.Text <> "" Then
                LoadNSPSExemptionByYear()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Sub LoadNSPSExemptionByYear()
        Try
            Dim NSPStemp As String = ""
            Dim ReasonID As String = ""
            Dim DisplayOrder As String = ""
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 1

            SQL = "Select " & _
            "ReasonID, DisplayOrder " & _
            "from " & DBNameSpace & ".FSNSPSReasonYear " & _
            "where intYear = '" & cboNSPSExemptionYear.Text & "' " & _
            "order by ReasonID "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("ReasonID")) Then
                    'NSPStemp = NSPStemp
                Else
                    NSPStemp = NSPStemp & dr.Item("ReasonID")
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        NSPStemp = NSPStemp & "-" & i & ","
                        i += 1
                    Else
                        NSPStemp = NSPStemp & "-" & dr.Item("DisplayOrder") & ","
                        If dr.Item("DisplayOrder") >= i Then
                            i = dr.Item("DisplayOrder") + 1
                        End If
                    End If
                End If
            End While
            dr.Close()

            dgvNSPSExemptionsByYear.Rows.Clear()
            Do While NSPStemp <> ""
                ReasonID = Mid(NSPStemp, 1, InStr(NSPStemp, "-", CompareMethod.Text) - 1)

                If ReasonID.Length = 1 Then
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 3)
                Else
                    DisplayOrder = Mid(NSPStemp, InStr(NSPStemp, "-", CompareMethod.Text) + 1, InStr(NSPStemp, ",", CompareMethod.Text) - 4)
                End If


                temp = ReasonID & "-" & DisplayOrder & ","
                NSPStemp = Replace(NSPStemp, temp, "")
                ' NSPStemp = Replace(NSPStemp, (ReasonID & "-" & DisplayOrder & ","), "")

                Dim x As Integer = 0
                While x < dgvNSPSExemptions.Rows.Count
                    Dim y As Integer = 0
                    While y < dgvNSPSExemptions.Rows(x).Cells.Count
                        Dim c As DataGridViewCell = dgvNSPSExemptions.Rows(x).Cells(y)
                        If Not c.Value Is DBNull.Value Or Nothing Then
                            If CType(c.Value, String) = ReasonID Then
                                dgvRow = New DataGridViewRow
                                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, x).Value
                                dgvRow.Cells(2).Value = DisplayOrder
                                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, x).Value
                                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                            End If
                        End If
                        System.Math.Min(System.Threading.Interlocked.Increment(y), y - 1)
                    End While
                    System.Math.Min(System.Threading.Interlocked.Increment(x), x - 1)
                End While
            Loop
            MessageBox.Show("Done")
            Exit Sub


            SQL = "Select " & _
             "" & DBNameSpace & ".FSNSPSReasonYear.intYear, " & _
             "" & DBNameSpace & ".FSNSPSReasonYear.ReasonID, " & _
             "displayOrder, " & _
             "" & DBNameSpace & ".FSNSPSReason.Reason " & _
             "from " & DBNameSpace & ".FSNSPSReason, " & DBNameSpace & ".FSNSPSReasonYear " & _
             "where " & DBNameSpace & ".FSNSPSReason.ReasonID = " & DBNameSpace & ".FSNSPSReasonYear.ReasonID " & _
             "and intyear = '" & cboNSPSExemptionYear.Text & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            'Dim bsource As BindingSource = New BindingSource()
            '  Dim cmdBuilder As OracleCommandBuilder = New OracleCommandBuilder(da)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            da.Fill(ds, "NSPSExemptionsYear")
            'bsource.DataSource = ds.Tables("NSPSExemptionsYear")
            'dgvNSPSExemptionsByYear.DataSource = bsource

            dgvNSPSExemptionsByYear.DataSource = ds
            dgvNSPSExemptionsByYear.DataMember = "NSPSExemptionsYear"

            dgvNSPSExemptionsByYear.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvNSPSExemptionsByYear.AllowUserToResizeColumns = True
            dgvNSPSExemptionsByYear.AllowUserToResizeRows = True
            dgvNSPSExemptionsByYear.AllowUserToAddRows = False
            dgvNSPSExemptionsByYear.AllowUserToDeleteRows = False
            dgvNSPSExemptionsByYear.AllowUserToOrderColumns = True
            dgvNSPSExemptionsByYear.Columns("intYear").HeaderText = "Year"
            dgvNSPSExemptionsByYear.Columns("intYear").DisplayIndex = 0
            dgvNSPSExemptionsByYear.Columns("ReasonID").HeaderText = "NSPS ID"
            dgvNSPSExemptionsByYear.Columns("ReasonID").DisplayIndex = 1
            dgvNSPSExemptionsByYear.Columns("displayOrder").HeaderText = "Display Order"
            dgvNSPSExemptionsByYear.Columns("displayOrder").DisplayIndex = 2
            dgvNSPSExemptionsByYear.Columns("Reason").HeaderText = "NSPS Exemption Reason"
            dgvNSPSExemptionsByYear.Columns("Reason").DisplayIndex = 3

            dgvNSPSExemptionsByYear.AutoResizeColumns()
            dgvNSPSExemptionsByYear.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnAddExemptionToYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddExemptionToYear.Click
        Try

            If cboNSPSExemptions.Text <> "" Then
                SQL = "Select " & _
                "ReasonID " & _
                "from " & DBNameSpace & ".FSNSPSReasonYear " & _
                "where intYear = '" & cboNSPSExemptionYear.Text & "' " & _
                "and ReasonID = '" & cboNSPSExemptions.SelectedValue & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = False Then
                    SQL = "Insert into " & DBNameSpace & ".FSNSPSReasonYear " & _
                    "values " & _
                    "('" & cboNSPSExemptionYear.Text & "', '" & cboNSPSExemptions.SelectedValue & "', " & _
                    "'" & (dgvNSPSExemptionsByYear.RowCount + 1) & "') "
                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                    LoadNSPSExemptionByYear()
                End If

            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectForm.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim temp As String
            Dim temp2 As String = "Add"
            Dim i As Integer = 0

            i = dgvNSPSExemptionsByYear.Rows.Count

            If i > 0 Then
                temp = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                For i = 0 To dgvNSPSExemptionsByYear.Rows.Count - 1
                    If dgvNSPSExemptionsByYear(1, i).Value = temp Then
                        temp2 = "Ignore"
                    End If
                Next
                If temp2 <> "Ignore" Then
                    dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                    dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                    dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                    dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                    dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
                End If
            Else
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUpdateNSPSbyYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateNSPSbyYear.Click
        Try
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim ReasonID As String
            Dim Order As String
            Dim temp As String = ""
            Dim ExistingID As String = ""

            SQL = "Select " & _
            "reasonid " & _
            "from " & DBNameSpace & ".FSNSPSReasonYear " & _
            "where intyear = '" & cboNSPSExemptionYear.Text & "' "

            cmd = New OracleCommand(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("ReasonID")) Then
                    'ExistingID = ExistingID
                Else
                    ExistingID = ExistingID & "(" & dr.Item("ReasonID") & ")"
                End If
            End While
            dr.Close()

            While x < dgvNSPSExemptionsByYear.Rows.Count
                ReasonID = dgvNSPSExemptionsByYear(1, x).Value
                Order = dgvNSPSExemptionsByYear(2, x).Value
                x += 1

                SQL = "Select " & _
                "DisplayOrder " & _
                "from " & DBNameSpace & ".FSNSPSReasonYear " & _
                "where intYear = '" & cboNSPSExemptionYear.Text & "' " & _
                "and ReasonID = '" & ReasonID & "' "
                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                temp = ""
                While dr.Read
                    If IsDBNull(dr.Item("DisplayOrder")) Then
                        temp = "NULL"
                    Else
                        temp = dr.Item("DisplayOrder")
                    End If
                End While
                dr.Close()

                If temp <> Order Then
                    Select Case temp
                        Case ""
                            SQL = "Insert into " & DBNameSpace & ".FSNSPSReasonYear " & _
                            "values " & _
                            "('" & cboNSPSExemptionYear.Text & "', '" & ReasonID & "', " & _
                            "'" & Order & "') "
                            cmd = New OracleCommand(SQL, DBConn)
                            If DBConn.State = ConnectionState.Closed Then
                                DBConn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        Case "NULL"
                            SQL = "Update " & DBNameSpace & ".FSNSPSReasonYear set " & _
                            "displayorder = '" & Order & "' " & _
                            "where intYear = '" & cboNSPSExemptionYear.Text & "' " & _
                            "and ReasonID = '" & ReasonID & "' "
                            cmd = New OracleCommand(SQL, DBConn)
                            If DBConn.State = ConnectionState.Closed Then
                                DBConn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                        Case Else
                            SQL = "Update " & DBNameSpace & ".FSNSPSReasonYear set " & _
                          "displayorder = '" & Order & "' " & _
                          "where intYear = '" & cboNSPSExemptionYear.Text & "' " & _
                          "and ReasonID = '" & ReasonID & "' "
                            cmd = New OracleCommand(SQL, DBConn)
                            If DBConn.State = ConnectionState.Closed Then
                                DBConn.Open()
                            End If
                            dr = cmd.ExecuteReader
                            dr.Close()
                    End Select
                End If
                ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")
            End While

            If ExistingID <> "" Then
                Do While ExistingID <> ""
                    ReasonID = Mid(ExistingID, InStr(ExistingID, "(", CompareMethod.Text) + 1, InStr(ExistingID, ")", CompareMethod.Text) - 2)
                    ExistingID = Replace(ExistingID, ("(" & ReasonID & ")"), "")

                    SQL = "Delete " & DBNameSpace & ".FSNSPSReasonYear " & _
                    "where intyear = '" & cboNSPSExemptionYear.Text & "' " & _
                    "and ReasonID = '" & ReasonID & "' "
                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    dr.Close()
                Loop
            End If
            MessageBox.Show("Update Complete")

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnSelectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllForms.Click
        Try
            Dim dgvRow As New DataGridViewRow
            Dim i As Integer = 0
            dgvNSPSExemptionsByYear.Rows.Clear()

            For i = 0 To dgvNSPSExemptions.Rows.Count - 1
                dgvRow = New DataGridViewRow
                dgvRow.CreateCells(dgvNSPSExemptionsByYear)
                dgvRow.Cells(0).Value = cboNSPSExemptionYear.Text
                dgvRow.Cells(1).Value = dgvNSPSExemptions(0, dgvNSPSExemptions.CurrentRow.Index).Value
                dgvRow.Cells(2).Value = (dgvNSPSExemptionsByYear.RowCount.ToString + 1)
                dgvRow.Cells(3).Value = dgvNSPSExemptions(1, i).Value
                dgvNSPSExemptionsByYear.Rows.Add(dgvRow)
            Next

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
        End Try
    End Sub
    Private Sub btnUnselectForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectForm.Click
        Try
            Dim ReasonID As String = ""

            If dgvNSPSExemptionsByYear.RowCount > 0 Then
                ReasonID = dgvNSPSExemptionsByYear(1, dgvNSPSExemptionsByYear.CurrentRow.Index).Value

                SQL = "Select " & _
                "strNSPSReason " & _
                "from " & DBNameSpace & ".FSCalculations " & _
                "where intYear = '" & dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value & "' " & _
                "and (strNSPSReason like '%" & ReasonID & ",' or strNSPSReason = '" & ReasonID & "' or strNSPSReason like '%," & ReasonID & "') "

                cmd = New OracleCommand(SQL, DBConn)
                If DBConn.State = ConnectionState.Closed Then
                    DBConn.Open()
                End If
                dr = cmd.ExecuteReader
                recExist = dr.Read
                dr.Close()
                If recExist = True Then
                    MessageBox.Show("Unable to Remove this exemption from this year because this exemption has been used.")
                    Exit Sub
                Else
                    dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                End If
            End If


        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub btnUnselectAllForms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAllForms.Click
        Try
            Dim ReasonID As String = ""
            Dim i As Integer = 0
            Dim x As Integer = 0

            If dgvNSPSExemptionsByYear.RowCount > 0 Then
                'dgvNSPSExemptionsByYear.Rows.Clear()

                i = 0
                While i < dgvNSPSExemptionsByYear.RowCount
                    ' For i = 0 To dgvNSPSExemptionsByYear.RowCount - 1

                    dgvNSPSExemptionsByYear.Rows(i).Selected = True
                    ReasonID = dgvNSPSExemptionsByYear(1, i).Value
                    SQL = "Select " & _
                    "strNSPSReason " & _
                    "from " & DBNameSpace & ".FSCalculations " & _
                    "where intYear = '" & dgvNSPSExemptionsByYear(0, dgvNSPSExemptionsByYear.CurrentRow.Index).Value & "' " & _
                    "and (strNSPSReason like '%" & ReasonID & ",' or strNSPSReason = '" & ReasonID & "' or strNSPSReason like '%," & ReasonID & "') "

                    cmd = New OracleCommand(SQL, DBConn)
                    If DBConn.State = ConnectionState.Closed Then
                        DBConn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    dr.Close()
                    If recExist = True Then
                        i += 1
                    Else

                        dgvNSPSExemptionsByYear.Rows(i).Selected = True
                        dgvNSPSExemptionsByYear.Rows.Remove(dgvNSPSExemptionsByYear.CurrentRow)
                        dgvNSPSExemptionsByYear.Rows(i).Selected = False
                    End If
                End While
                'Next

            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub chbDepositDateSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDepositDateSearch.CheckedChanged
        Try
            If chbDepositDateSearch.Checked = True Then
                dtpStartDepositDate.Enabled = True
                dtpEndDepositDate.Enabled = True
                btnRunDepositReport.Enabled = True
                cboStatYear.Enabled = False
                cboStatPayType.Enabled = False
                btnViewDepositsStats.Enabled = False
                btnViewPaymentDue.Enabled = False
                bntViewTotalPaid.Enabled = False
                btnViewBalance.Enabled = False
                chbNonZeroBalance.Enabled = False
            Else
                dtpStartDepositDate.Enabled = False
                dtpEndDepositDate.Enabled = False
                btnRunDepositReport.Enabled = False
                cboStatYear.Enabled = True
                cboStatPayType.Enabled = True
                btnViewDepositsStats.Enabled = True
                btnViewPaymentDue.Enabled = True
                bntViewTotalPaid.Enabled = True
                btnViewBalance.Enabled = True
                chbNonZeroBalance.Enabled = True
            End If
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub btnRunDepositReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunDepositReport.Click
        Try
            Dim StartDate As String
            Dim EndDate As String

            StartDate = dtpStartDepositDate.Text
            EndDate = dtpEndDepositDate.Text

            SQL = "select " & _
            "substr(" & DBNameSpace & ".APBFacilityInformation.strAIRSNumber, 5) as AIRSNumber, " & _
            "strFacilityName, " & _
            "strPayType, numPayment, " & _
            "strDepositNo, datPayDate, " & _
            "strCheckNo, strInvoiceNo, " & _
            "" & DBNameSpace & ".FSAddPaid.intYear " & _
            "From " & DBNameSpace & ".APBFacilityInformation, " & DBNameSpace & ".FSAddPaid " & _
            "where " & DBNameSpace & ".APBFacilityInformation.strAIRSNumber = " & DBNameSpace & ".FSAddPaid.strAIRSNumber " & _
            "and datPaydate between '" & StartDate & "' and '" & EndDate & "' "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, DBConn)
            If DBConn.State = ConnectionState.Closed Then
                DBConn.Open()
            End If

            da.Fill(ds, "PaymentDue")
            dgvDepositsAndPayments.DataSource = ds
            dgvDepositsAndPayments.DataMember = "PaymentDue"

            dgvDepositsAndPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
            dgvDepositsAndPayments.AllowUserToResizeColumns = True
            dgvDepositsAndPayments.AllowUserToResizeRows = True
            dgvDepositsAndPayments.AllowUserToAddRows = False
            dgvDepositsAndPayments.AllowUserToDeleteRows = False
            dgvDepositsAndPayments.AllowUserToOrderColumns = True
            dgvDepositsAndPayments.Columns("AIRSNUmber").HeaderText = "AIRS Number"
            dgvDepositsAndPayments.Columns("AIRSNUmber").DisplayIndex = 0
            dgvDepositsAndPayments.Columns("strFacilityName").HeaderText = "Facility Name"
            dgvDepositsAndPayments.Columns("strFacilityName").DisplayIndex = 1
            dgvDepositsAndPayments.Columns("strFacilityName").Width = 300
            dgvDepositsAndPayments.Columns("strPayType").HeaderText = "Pay Type"
            dgvDepositsAndPayments.Columns("strPayType").DisplayIndex = 2
            dgvDepositsAndPayments.Columns("numPayment").HeaderText = "Amount Paid"
            dgvDepositsAndPayments.Columns("numPayment").DisplayIndex = 3
            dgvDepositsAndPayments.Columns("numPayment").DefaultCellStyle.Format = "c"
            dgvDepositsAndPayments.Columns("strDepositNo").HeaderText = "Deposit #"
            dgvDepositsAndPayments.Columns("strDepositNo").DisplayIndex = 4
            dgvDepositsAndPayments.Columns("datPayDate").HeaderText = "Pay Date"
            dgvDepositsAndPayments.Columns("datPayDate").DisplayIndex = 5
            dgvDepositsAndPayments.Columns("datPayDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
            dgvDepositsAndPayments.Columns("strCheckNo").HeaderText = "Check #"
            dgvDepositsAndPayments.Columns("strCheckNo").DisplayIndex = 6
            dgvDepositsAndPayments.Columns("strInvoiceNo").HeaderText = "Invoice #"
            dgvDepositsAndPayments.Columns("strInvoiceNo").DisplayIndex = 7
            dgvDepositsAndPayments.Columns("intYear").HeaderText = "Year"
            dgvDepositsAndPayments.Columns("intYear").DisplayIndex = 8

            txtCount.Text = dgvDepositsAndPayments.RowCount.ToString

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

  
End Class