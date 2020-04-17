Imports System.Data.SqlClient

Public Class ISMPAddTestingFirms
    Dim query As String

#Region "Page Load"

    Private Sub ISMPAddTestingFirms_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            LoadDataSet()
            FormatdgrTestingFirms()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub LoadDataSet()
        Try

            query = "Select strTestingFirmKey, strTestingFirm, " &
                 "strFirmAddress1, strFirmAddress2, " &
                 "strFirmCity, strFirmState, " &
                 "strFirmZipCode, strFirmPhoneNumber1, " &
                 "strFirmPhoneNumber2, strFirmFax, strFirmEmail " &
                 "from LookUPTestingFirms " &
                 "Order by strTestingFirm "

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.TableName = "TestingFirms"
            dgrTestingFirms.DataSource = dt

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub FormatdgrTestingFirms()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As DataGridTextBoxColumn

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
            dgrTestingFirms.CaptionText = "Testing Firms"
            dgrTestingFirms.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub LoadTestingFirmInfo()
        Try
            Dim dtTestingFirms As DataTable = CType(dgrTestingFirms.DataSource, DataTable)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub GetNextKey()
        Dim TestingFirmKey As String
        Dim newTestingFirmKey As String
        Try

            TestingFirmKey = "00001"
            newTestingFirmKey = "00000"

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

                query = "Select strTestingfirmKey " &
                "from LookUPTestingFirms " &
                "where strTestingFirmKey = @key "

                Dim p As New SqlParameter("@key", TestingFirmKey)

                If DB.ValueExists(query, p) Then
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub Save()
        Dim TestingFirm As String
        Dim TestingFirmAddress1 As String
        Dim TestingFirmAddress2 As String
        Dim TestingFirmCity As String
        Dim TestingFirmState As String
        Dim TestingFirmZipCode As String = ""
        Dim TestingFirmPhoneNumber1 As String = ""
        Dim TestingFirmPhoneNumber2 As String = ""
        Dim TestingFirmFaxNumber As String = ""
        Dim TestingFirmEmail As String
        Dim x As Integer

        Try

            If txtTestingFirm.Text <> "" Then
                TestingFirm = txtTestingFirm.Text
            Else
                TestingFirm = "N/A"
            End If
            If txtTestingFirmAddress1.Text <> "" Then
                TestingFirmAddress1 = txtTestingFirmAddress1.Text
            Else
                TestingFirmAddress1 = "N/A"
            End If
            If txtTestingFirmAddress2.Text <> "" Then
                TestingFirmAddress2 = txtTestingFirmAddress2.Text
            Else
                TestingFirmAddress2 = "N/A"
            End If
            If txtTestingFirmCity.Text <> "" Then
                TestingFirmCity = txtTestingFirmCity.Text
            Else
                TestingFirmCity = "N/A"
            End If
            If txtTestingFirmState.Text <> "" Then
                TestingFirmState = txtTestingFirmState.Text
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
            If txtTestingFirmPhoneNumber1.Text <> "" AndAlso txtTestingFirmAreaCode1.Text <> "" Then
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
            If txtTestingFirmPhoneNumber2.Text <> "" AndAlso txtTestingFirmAreaCode2.Text <> "" Then
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
            If txtTestingFirmFaxNumber.Text <> "" AndAlso txtTestingFirmAreaCode3.Text <> "" Then
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
                TestingFirmEmail = txtTestingFirmEmail.Text
            Else
                TestingFirmEmail = "N/A"
            End If

            If txtTestingFirm.Text <> "" Then
                If txtTestingFirmKey.Text = "" Then
                    GetNextKey()
                End If

                query = "Select strTestingFirmKey " &
                "from LookUPTestingFirms " &
                "where strTestingFirmKey = @key "

                Dim p As New SqlParameter("@key", txtTestingFirmKey.Text)

                If DB.ValueExists(query, p) Then
                    query = "Update LookUPTestingFirms set " &
                        "strTestingFirm = @strTestingFirm, " &
                        "strFirmAddress1 = @strFirmAddress1, " &
                        "strFirmAddress2 = @strFirmAddress2, " &
                        "strFirmCity = @strFirmCity, " &
                        "strFirmState = @strFirmState, " &
                        "strFirmZipCode = @strFirmZipCode, " &
                        "strFirmphoneNumber1 = @strFirmphoneNumber1, " &
                        "strFirmPhoneNumber2 = @strFirmPhoneNumber2, " &
                        "strFirmFax = @strFirmFax, " &
                        "strFirmEmail = @strFirmEmail " &
                        "where strTestingFirmKey = @strTestingFirmKey "
                Else
                    query = "Insert into LookUPTestingFirms " &
                        "(strTestingFirmKey, strTestingFirm, " &
                        "strFirmAddress1, strFirmAddress2, " &
                        "strFirmCity, strFirmState, " &
                        "strFirmZipCode, strFirmPhoneNumber1, " &
                        "strFirmPhoneNumber2, strFirmFax, " &
                        "strFirmEmail) " &
                        "values " &
                        "(@strTestingFirmKey, @strTestingFirm, " &
                        "@strFirmAddress1, @strFirmAddress2, " &
                        "@strFirmCity, @strFirmState, " &
                        "@strFirmZipCode, @strFirmPhoneNumber1, " &
                        "@strFirmPhoneNumber2, @strFirmFax, " &
                        "@strFirmEmail) "
                End If

                Dim p2 As SqlParameter() = {
                    New SqlParameter("@strTestingFirm", TestingFirm),
                    New SqlParameter("@strFirmAddress1", TestingFirmAddress1),
                    New SqlParameter("@strFirmAddress2", TestingFirmAddress2),
                    New SqlParameter("@strFirmCity", TestingFirmCity),
                    New SqlParameter("@strFirmState", TestingFirmState),
                    New SqlParameter("@strFirmZipCode", TestingFirmZipCode),
                    New SqlParameter("@strFirmphoneNumber1", TestingFirmPhoneNumber1),
                    New SqlParameter("@strFirmPhoneNumber2", TestingFirmPhoneNumber2),
                    New SqlParameter("@strFirmFax", TestingFirmFaxNumber),
                    New SqlParameter("@strFirmEmail", TestingFirmEmail),
                    New SqlParameter("@strTestingFirmKey", txtTestingFirmKey.Text)
                }

                DB.RunCommand(query, p2)

                LoadDataSet()

            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub Clear()
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
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub dgrTestingFirms_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrTestingFirms.MouseUp
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub txtTestingFirmKey_TextChanged(sender As Object, e As EventArgs) Handles txtTestingFirmKey.TextChanged
        Try

            If txtTestingFirmKey.Text <> "" Then
                LoadTestingFirmInfo()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region "Main Menu Item"
    Private Sub MmiSave_Click(sender As Object, e As EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiClear_Click(sender As Object, e As EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region

    Private Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click
        Save()
    End Sub

    Private Sub bClear_Click(sender As Object, e As EventArgs) Handles bClear.Click
        Clear()
    End Sub

End Class
