Imports System.Data.SqlClient


Public Class ISMPAddTestingFirms
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim recExist As Boolean
    Dim dsTestingFirms As DataSet
    Dim daTestingFirms As SqlDataAdapter


    Private Sub ISMPAddTestingFirms_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            CreateStatusBar()
            LoadDataSet()
            FormatdgrTestingFirms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = CurrentUser.AlphaName
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
    Sub LoadDataSet()
        Try

            SQL = "Select strTestingFirmKey, strTestingFirm, " &
                 "strFirmAddress1, strFirmAddress2, " &
                 "strFirmCity, strFirmState, " &
                 "strFirmZipCode, strFirmPhoneNumber1, " &
                 "strFirmPhoneNumber2, strFirmFax, strFirmEmail " &
                 "from LookUPTestingFirms " &
                 "Order by strTestingFirm "

            dsTestingFirms = New DataSet

            daTestingFirms = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daTestingFirms.Fill(dsTestingFirms, "TestingFirms")
            dgrTestingFirms.DataSource = dsTestingFirms
            dgrTestingFirms.DataMember = "TestingFirms"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatdgrTestingFirms()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "TestingFirms"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  0
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestingFirmKey"
            objtextcol.HeaderText = "Key"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 50
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strTestingFirm"
            objtextcol.HeaderText = "Testing Firm"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 250
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmAddress1"
            objtextcol.HeaderText = "Firm Address"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 150
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    3
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmCity"
            objtextcol.HeaderText = "Firm City"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 120
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    4
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmState"
            objtextcol.HeaderText = "Firm State"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 70
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    5
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmZipCode"
            objtextcol.HeaderText = "Firm Zip Code"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 80
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    6
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmPhoneNumber1"
            objtextcol.HeaderText = "Firm Phone Number 1"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    7
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmPhoneNumber2"
            objtextcol.HeaderText = "Firm Phone Number 2"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    8
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmFax"
            objtextcol.HeaderText = "Firm Fax Number"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 130
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    9
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strFirmEmail"
            objtextcol.HeaderText = "Firm Email Address"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 200
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrTestingFirms.TableStyles.Clear()
            dgrTestingFirms.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrTestingFirms.CaptionText = "Testing Firm(s)"
            dgrTestingFirms.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region


#Region "Functions and Subs"
    Sub LoadTestingFirmInfo()
        Dim dtTestingFirms As New DataTable
        Try

            dtTestingFirms = dsTestingFirms.Tables("TestingFirms")

            Dim drTestingFirms As DataRow()
            Dim row As DataRow

            drTestingFirms = dtTestingFirms.Select("strTestingFirmKey = '" & txtTestingFirmKey.Text & "'")
            For Each row In drTestingFirms
                txtTestingFirm.Text = row("strTestingFirm")
                If row("strFirmAddress1") <> "N/A" Then
                    txtTestingFirmAddress1.Text = row("strFirmAddress1")
                Else
                    txtTestingFirmAddress1.Text = ""
                End If
                If row("strFirmAddress2") <> "N/A" Then
                    txtTestingFirmAddress2.Text = row("strFirmAddress2")
                Else
                    txtTestingFirmAddress2.Text = ""
                End If
                If row("strFirmCity") <> "N/A" Then
                    txtTestingFirmCity.Text = row("strFirmCity")
                Else
                    txtTestingFirmCity.Text = ""
                End If
                txtTestingFirmState.Text = row("strFirmState")
                If row("strFirmZipCode") <> "N/A" Then
                    txtTestingFirmZipCode.Text = row("strFirmZipCode")
                Else
                    txtTestingFirmZipCode.Text = ""
                End If
                If row("strFirmPhoneNumber1") <> "N/A" Then
                    txtTestingFirmAreaCode1.Text = Mid(row("strFirmPhoneNumber1"), 1, 3)
                    txtTestingFirmPhoneNumber1.Text = Mid(row("strFirmPhoneNumber1"), 4)
                Else
                    txtTestingFirmAreaCode1.Text = ""
                    txtTestingFirmPhoneNumber1.Text = ""
                End If
                If row("strFirmPhoneNumber2") <> "N/A" Then
                    txtTestingFirmAreaCode2.Text = Mid(row("strFirmPhoneNumber2"), 1, 3)
                    txtTestingFirmPhoneNumber2.Text = Mid(row("strFirmPhoneNumber2"), 4)
                Else
                    txtTestingFirmAreaCode2.Text = ""
                    txtTestingFirmPhoneNumber2.Text = ""
                End If
                If row("strFirmFax") <> "N/A" Then
                    txtTestingFirmAreaCode3.Text = Mid(row("strFirmFax"), 1, 3)
                    txtTestingFirmFaxNumber.Text = Mid(row("strFirmFax"), 4)
                Else
                    txtTestingFirmAreaCode3.Text = ""
                    txtTestingFirmFaxNumber.Text = ""
                End If
                If row("StrFirmEmail") <> "N/A" Then
                    txtTestingFirmEmail.Text = row("strFirmEmail")
                Else
                    txtTestingFirmEmail.Text = ""
                End If
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub GetNextKey()
        Dim TestingFirmKey As String
        Dim newTestingFirmKey As String
        Try

            TestingFirmKey = "00001"
            newTestingFirmKey = "00000"

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            Do Until newTestingFirmKey <> "00000"
                Select Case TestingFirmKey.Length
                    Case 1
                        TestingFirmKey = "0000" & TestingFirmKey
                    Case 2
                        TestingFirmKey = "000" & TestingFirmKey
                    Case 3
                        TestingFirmKey = "00" & TestingFirmKey
                    Case 4
                        TestingFirmKey = "0" & TestingFirmKey
                    Case Else
                        ' TestingFirmKey = TestingFirmKey
                End Select

                SQL = "Select strTestingfirmKey " &
                "from LookUPTestingFirms " &
                "where strTestingFirmKey = '" & TestingFirmKey & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    TestingFirmKey += 1
                Else
                    newTestingFirmKey = TestingFirmKey
                End If
            Loop



            Select Case TestingFirmKey.Length
                Case 1
                    TestingFirmKey = "0000" & TestingFirmKey
                Case 2
                    TestingFirmKey = "000" & TestingFirmKey
                Case 3
                    TestingFirmKey = "00" & TestingFirmKey
                Case 4
                    TestingFirmKey = "0" & TestingFirmKey
                Case Else
                    'TestingFirmKey = TestingFirmKey
            End Select
            txtTestingFirmKey.Text = TestingFirmKey

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub Save()
        Dim TestingFirm As String = ""
        Dim TestingFirmAddress1 As String = ""
        Dim TestingFirmAddress2 As String = ""
        Dim TestingFirmCity As String = ""
        Dim TestingFirmState As String = ""
        Dim TestingFirmZipCode As String = ""
        Dim TestingFirmPhoneNumber1 As String = ""
        Dim TestingFirmPhoneNumber2 As String = ""
        Dim TestingFirmFaxNumber As String = ""
        Dim TestingFirmEmail As String = ""
        Dim x As Integer

        Try

            If txtTestingFirm.Text <> "" Then
                TestingFirm = Replace(txtTestingFirm.Text, "'", "''")
            Else
                TestingFirm = "N/A"
            End If
            If txtTestingFirmAddress1.Text <> "" Then
                TestingFirmAddress1 = Replace(txtTestingFirmAddress1.Text, "'", "''")
            Else
                TestingFirmAddress1 = "N/A"
            End If
            If txtTestingFirmAddress2.Text <> "" Then
                TestingFirmAddress2 = Replace(txtTestingFirmAddress2.Text, "'", "''")
            Else
                TestingFirmAddress2 = "N/A"
            End If
            If txtTestingFirmCity.Text <> "" Then
                TestingFirmCity = Replace(txtTestingFirmCity.Text, "'", "''")
            Else
                TestingFirmCity = "N/A"
            End If
            If txtTestingFirmState.Text <> "" Then
                TestingFirmState = Replace(txtTestingFirmState.Text, "'", "''")
            Else
                TestingFirmState = "GA"
            End If
            If txtTestingFirmZipCode.Text <> "" Then
                For x = 1 To txtTestingFirmZipCode.Text.Length
                    If IsNumeric(Mid(txtTestingFirmZipCode.Text, x, 1)) Then TestingFirmZipCode = TestingFirmZipCode & Mid(txtTestingFirmZipCode.Text, x, 1)
                Next
                If TestingFirmZipCode = "" Then
                    TestingFirmZipCode = "00000"
                End If
            Else
                TestingFirmZipCode = "00000"
            End If
            If txtTestingFirmPhoneNumber1.Text <> "" And txtTestingFirmAreaCode1.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode1.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode1.Text, x, 1)) Then TestingFirmPhoneNumber1 = TestingFirmPhoneNumber1 & Mid(txtTestingFirmAreaCode1.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmPhoneNumber1.Text.Length
                    If IsNumeric(Mid(txtTestingFirmPhoneNumber1.Text, x, 1)) Then TestingFirmPhoneNumber1 = TestingFirmPhoneNumber1 & Mid(txtTestingFirmPhoneNumber1.Text, x, 1)
                Next
                If TestingFirmPhoneNumber1 = "" Then
                    TestingFirmPhoneNumber1 = "N/A"
                End If
            Else
                TestingFirmPhoneNumber1 = "N/A"
            End If
            If txtTestingFirmPhoneNumber2.Text <> "" And txtTestingFirmAreaCode2.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode2.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode2.Text, x, 1)) Then TestingFirmPhoneNumber2 = TestingFirmPhoneNumber2 & Mid(txtTestingFirmAreaCode2.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmPhoneNumber2.Text.Length
                    If IsNumeric(Mid(txtTestingFirmPhoneNumber2.Text, x, 1)) Then TestingFirmPhoneNumber2 = TestingFirmPhoneNumber2 & Mid(txtTestingFirmPhoneNumber2.Text, x, 1)
                Next
                If TestingFirmPhoneNumber2 = "" Then
                    TestingFirmPhoneNumber2 = "N/A"
                End If
            Else
                TestingFirmPhoneNumber2 = "N/A"
            End If
            If txtTestingFirmFaxNumber.Text <> "" And txtTestingFirmAreaCode3.Text <> "" Then
                For x = 1 To txtTestingFirmAreaCode3.Text.Length
                    If IsNumeric(Mid(txtTestingFirmAreaCode3.Text, x, 1)) Then TestingFirmFaxNumber = TestingFirmFaxNumber & Mid(txtTestingFirmAreaCode3.Text, x, 1)
                Next
                For x = 1 To txtTestingFirmFaxNumber.Text.Length
                    If IsNumeric(Mid(txtTestingFirmFaxNumber.Text, x, 1)) Then TestingFirmFaxNumber = TestingFirmFaxNumber & Mid(txtTestingFirmFaxNumber.Text, x, 1)
                Next
                If TestingFirmFaxNumber = "" Then
                    TestingFirmFaxNumber = "N/A"
                End If
            Else
                TestingFirmFaxNumber = "N/A"
            End If
            If txtTestingFirmEmail.Text <> "" Then
                TestingFirmEmail = Replace(txtTestingFirmEmail.Text, "'", "''")
            Else
                TestingFirmEmail = "N/A"
            End If

            If txtTestingFirm.Text <> "" Then
                If txtTestingFirmKey.Text = "" Then
                    GetNextKey()
                End If

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                If chbDeleteTestingFirm.Checked = True Then
                    SQL = "Delete LookUPTestingFirms " &
                    "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "'"
                Else
                    SQL = "Select strTestingFirmKey " &
                    "from LookUPTestingFirms " &
                    "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "'"
                    cmd = New SqlCommand(SQL, CurrentConnection)
                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        SQL = "Update LookUPTestingFirms set " &
                        "strTestingFirm = '" & TestingFirm & "', " &
                        "strFirmAddress1 = '" & TestingFirmAddress1 & "', " &
                        "strFirmAddress2 = '" & TestingFirmAddress2 & "', " &
                        "strFirmCity = '" & TestingFirmCity & "', " &
                        "strFirmState = '" & TestingFirmState & "', " &
                        "strFirmZipCode = '" & TestingFirmZipCode & "', " &
                        "strFirmphoneNumber1 = '" & TestingFirmPhoneNumber1 & "', " &
                        "strFirmPhoneNumber2 = '" & TestingFirmPhoneNumber2 & "', " &
                        "strFirmFax = '" & TestingFirmFaxNumber & "', " &
                        "strFirmEmail = '" & TestingFirmEmail & "' " &
                        "where strTestingFirmKey = '" & txtTestingFirmKey.Text & "' "
                    Else
                        SQL = "Insert into LookUPTestingFirms " &
                        "(strTestingFirmKey, strTestingFirm, " &
                        "strFirmAddress1, strFirmAddress2, " &
                        "strFirmCity, strFirmState, " &
                        "strFirmZipCode, strFirmPhoneNumber1, " &
                        "strFirmPhoneNumber2, strFirmFax, " &
                        "strFirmEmail) " &
                        "values " &
                        "('" & txtTestingFirmKey.Text & "', '" & TestingFirm & "', " &
                        "'" & TestingFirmAddress1 & "', '" & TestingFirmAddress2 & "', " &
                        "'" & TestingFirmCity & "', '" & TestingFirmState & "', " &
                        "'" & TestingFirmZipCode & "', '" & TestingFirmPhoneNumber1 & "', " &
                        "'" & TestingFirmPhoneNumber2 & "', '" & TestingFirmFaxNumber & "', " &
                        "'" & TestingFirmEmail & "') "
                    End If
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                LoadDataSet()


            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub Clear()
        Try

            txtTestingFirmKey.Clear()
            txtTestingFirm.Clear()
            txtTestingFirmAddress1.Clear()
            txtTestingFirmAddress2.Clear()
            txtTestingFirmCity.Clear()
            txtTestingFirmState.Clear()
            txtTestingFirmZipCode.Clear()
            txtTestingFirmAreaCode1.Clear()
            txtTestingFirmAreaCode2.Clear()
            txtTestingFirmAreaCode3.Clear()
            txtTestingFirmPhoneNumber1.Clear()
            txtTestingFirmPhoneNumber2.Clear()
            txtTestingFirmFaxNumber.Clear()
            txtTestingFirmEmail.Clear()
            chbDeleteTestingFirm.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub Back()
        Try

            ISMPAddTestingFirm = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region
    Private Sub ISMPAddTestingFirms_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPAddTestingFirm = Nothing
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrTestingFirms_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgrTestingFirms.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrTestingFirms.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrTestingFirms(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrTestingFirms(hti.Row, 1)) Then
                    Else
                        If IsDBNull(dgrTestingFirms(hti.Row, 2)) Then
                        Else
                            If IsDBNull(dgrTestingFirms(hti.Row, 3)) Then
                            Else
                                If IsDBNull(dgrTestingFirms(hti.Row, 4)) Then
                                Else
                                    If IsDBNull(dgrTestingFirms(hti.Row, 5)) Then
                                    Else
                                        If IsDBNull(dgrTestingFirms(hti.Row, 6)) Then
                                        Else
                                            If IsDBNull(dgrTestingFirms(hti.Row, 7)) Then
                                            Else
                                                If IsDBNull(dgrTestingFirms(hti.Row, 8)) Then
                                                Else
                                                    If IsDBNull(dgrTestingFirms(hti.Row, 9)) Then
                                                    Else
                                                        txtTestingFirmKey.Text = dgrTestingFirms(hti.Row, 0)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtTestingFirmKey_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestingFirmKey.TextChanged
        Try

            If txtTestingFirmKey.Text <> "" Then
                LoadTestingFirmInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Main Menu Item"
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
#End Region

    Private Sub TBAddTestingFirm_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBAddTestingFirm.ButtonClick
        Try

            Select Case TBAddTestingFirm.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


End Class
