Imports System.Data.SqlClient

Public Class SSPPAttainmentStatus

    Private Sub SSPPAttainmentStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAttainmentStatusComboBoxes()
        LoadCountyComboBox()
    End Sub

    Private Sub LoadAttainmentStatusComboBoxes()
        Try
            cboNonAttainmentStatus.Items.Clear()
            cboNonAttainmentStatus.Items.Add("-Select an Attainment Status-")
            cboNonAttainmentStatus.Items.Add("-View All-")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (Yes)")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (No)")
            cboNonAttainmentStatus.Items.Add("1 hr Maintenance (Contribute)")
            cboNonAttainmentStatus.Items.Add("8 hr Atlanta")
            cboNonAttainmentStatus.Items.Add("8 hr Macon")
            cboNonAttainmentStatus.Items.Add("8 hr (No)")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Atlanta")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Chattanooga")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Floyd")
            cboNonAttainmentStatus.Items.Add("PM 2.5 Macon")
            cboNonAttainmentStatus.Items.Add("PM 2.5 (No)")

            cboNonAttainmentStatus.Text = cboNonAttainmentStatus.Items.Item(0)

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try
    End Sub

    Private Sub LoadCountyComboBox()
        Try
            Dim query As String = "select strCountyCode, strCountyname " &
            "from LookUpCountyInformation " &
            "order by strcountyName"

            With cboCounty
                .DataSource = DB.GetDataTable(query)
                .DisplayMember = "strCountyName"
                .ValueMember = "strCountyCode"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub ViewSelectedData()
        Dim SQLClause As String

        Select Case cboNonAttainmentStatus.Text
            Case ("-View All-")
                SQLClause = "Where strNonAttainment like '_____'"
            Case ("1 hr Maintenance (Yes)")
                SQLClause = "Where strNonAttainment like '_1___'"
            Case ("1 hr Maintenance (No)")
                SQLClause = "Where strNonAttainment like '_0___'"
            Case ("1 hr Maintenance (Contribute)")
                SQLClause = "Where strNonAttainment like '_2___'"
            Case ("8 hr Atlanta")
                SQLClause = "Where strNonAttainment like '__1__'"
            Case ("8 hr Macon")
                SQLClause = "Where strNonAttainment like '__2__'"
            Case ("8 hr (No)")
                SQLClause = "Where strNonAttainment like '__0__'"
            Case ("PM 2.5 Atlanta")
                SQLClause = "Where strNonAttainment like '___1_'"
            Case ("PM 2.5 Chattanooga")
                SQLClause = "Where strNonAttainment like '___2_'"
            Case ("PM 2.5 Floyd")
                SQLClause = "Where strNonAttainment like '___3_'"
            Case ("PM 2.5 Macon")
                SQLClause = "Where strNonAttainment like '___4_'"
            Case ("PM 2.5 (No)")
                SQLClause = "Where strNonAttainment like '___0_'"
            Case Else
                SQLClause = "Where strNonAttainment like '____1'"
        End Select

        Dim query As String = "Select " &
        "strCountyName, strCountyCode, strNonAttainment  " &
        "from LookUpCountyInformation  " &
        SQLClause

        dgvAttainmentStatus.DataSource = DB.GetDataTable(query)

        dgvAttainmentStatus.RowHeadersVisible = False
        dgvAttainmentStatus.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvAttainmentStatus.AllowUserToResizeColumns = True
        dgvAttainmentStatus.AllowUserToAddRows = False
        dgvAttainmentStatus.AllowUserToDeleteRows = False
        dgvAttainmentStatus.AllowUserToOrderColumns = True
        dgvAttainmentStatus.AllowUserToResizeRows = True

        dgvAttainmentStatus.Columns("strCountyCode").HeaderText = "County Code"
        dgvAttainmentStatus.Columns("strCountyCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvAttainmentStatus.Columns("strCountyCode").DisplayIndex = 0
        dgvAttainmentStatus.Columns("strCountyName").HeaderText = "County Name"
        dgvAttainmentStatus.Columns("strCountyName").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvAttainmentStatus.Columns("strCountyName").DisplayIndex = 1
        dgvAttainmentStatus.Columns("strNonAttainment").HeaderText = "Attainment status"
        dgvAttainmentStatus.Columns("strNonAttainment").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvAttainmentStatus.Columns("strNonAttainment").DisplayIndex = 2
        dgvAttainmentStatus.Columns("strNonAttainment").Visible = False

    End Sub

    Private Sub ViewCountyInformation()
        Try
            Dim Attainment As String = ""

            Dim query As String = "select " &
            "strNonAttainment " &
            "from LookUpCountyInformation " &
            "where strCountyCode = @cty "

            Dim p As New SqlParameter("@cty", cboCounty.SelectedValue)

            Attainment = DB.GetString(query, p)

            If Attainment <> "" Then
                Select Case Mid(Attainment, 2, 1)
                    Case 0
                        rdbOneHourNo.Checked = True
                    Case 1
                        rdbOneHourYes.Checked = True
                    Case 2
                        rdbOneHourCont.Checked = True
                    Case Else
                        rdbOneHourNo.Checked = True
                End Select
                Select Case Mid(Attainment, 3, 1)
                    Case 0
                        rdbEightHourNo.Checked = True
                    Case 1
                        rdbEightHourAtlanta.Checked = True
                    Case 2
                        rdbEightHourMacon.Checked = True
                    Case Else
                        rdbEightHourNo.Checked = True
                End Select
                Select Case Mid(Attainment, 4, 1)
                    Case 0
                        rdbPMFineNo.Checked = True
                    Case 1
                        rdbPMFineAtlanta.Checked = True
                    Case 2
                        rdbPMFineChattanooga.Checked = True
                    Case 3
                        rdbPMFineFloyd.Checked = True
                    Case 4
                        rdbPMFineMacon.Checked = True
                    Case Else
                        rdbPMFineNo.Checked = True
                End Select

            End If


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub

    Private Sub SaveOneHourChanges()
        Try
            Dim AttainmentStatus As String = "00000"

            Dim query As String = "select strNonAttainment " &
            "from LookUpCountyInformation " &
            "where strCountyCode = @cty "

            Dim p As New SqlParameter("@cty", cboCounty.SelectedValue)

            AttainmentStatus = DB.GetString(query, p)

            If rdbOneHourNo.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "0" & Mid(AttainmentStatus, 3)
            End If
            If rdbOneHourYes.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "1" & Mid(AttainmentStatus, 3)
            End If
            If rdbOneHourCont.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "2" & Mid(AttainmentStatus, 3)
            End If

            query = "Update LookUpCountyInformation set " &
            "strNonAttainment = @att " &
            "where strCountyCode = @cty "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@att", AttainmentStatus),
                p
            }

            DB.RunCommand(query, p2)

            query = "Select strAttainmentStatus, strAIRSNumber " &
            "from APBHeaderData " &
            "where SUBSTRING(strAIRSNumber, 5, 3) = @cty "

            Dim dt As DataTable = DB.GetDataTable(query, p)

            Dim query2 As String = "Update APBHeaderData set " &
                "strAttainmentStatus = @st " &
                "where strAIRSNumber = @airs "

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strAttainmentStatus")
                End If

                If rdbOneHourNo.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "0" & Mid(AttainmentStatus, 3)
                End If

                If rdbOneHourYes.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "1" & Mid(AttainmentStatus, 3)
                End If

                If rdbOneHourCont.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 1) & "2" & Mid(AttainmentStatus, 3)
                End If

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@st", AttainmentStatus),
                    New SqlParameter("@airs", dr.Item("strAirsNumber"))
                }

                DB.RunCommand(query2, p3)
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub SaveEightHourChanges()
        Try
            Dim AttainmentStatus As String = "00000"

            Dim query As String = "select strNonAttainment " &
            "from LookUpCountyInformation " &
            "where strCountyCode = @cty "

            Dim p As New SqlParameter("@cty", cboCounty.SelectedValue)

            AttainmentStatus = DB.GetString(query, p)

            If rdbEightHourNo.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
            End If
            If rdbEightHourAtlanta.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
            End If
            If rdbEightHourMacon.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
            End If

            query = "Update LookUpCountyInformation set " &
            "strNonAttainment = @att " &
            "where strCountyCode = @cty "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@att", AttainmentStatus),
                p
            }

            DB.RunCommand(query, p2)

            query = "Select strAttainmentStatus, strAIRSNumber " &
            "from APBHeaderData " &
            "where SUBSTRING(strAIRSNumber, 5, 3) = @cty "

            Dim dt As DataTable = DB.GetDataTable(query, p)

            Dim query2 As String = "Update APBHeaderData set " &
                "strAttainmentStatus = @st " &
                "where strAIRSNumber = @airs "

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strAttainmentStatus")
                End If

                If rdbEightHourNo.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "0" & Mid(AttainmentStatus, 4)
                End If

                If rdbEightHourAtlanta.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "1" & Mid(AttainmentStatus, 4)
                End If

                If rdbEightHourMacon.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 2) & "2" & Mid(AttainmentStatus, 4)
                End If

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@st", AttainmentStatus),
                    New SqlParameter("@airs", dr.Item("strAirsNumber"))
                }

                DB.RunCommand(query2, p3)
            Next

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub SavePMFineChanges()
        Try
            Dim AttainmentStatus As String = "00000"

            Dim query As String = "select strNonAttainment " &
            "from LookUpCountyInformation " &
            "where strCountyCode = @cty "

            Dim p As New SqlParameter("@cty", cboCounty.SelectedValue)

            AttainmentStatus = DB.GetString(query, p)

            If rdbPMFineNo.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineAtlanta.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineChattanooga.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineFloyd.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
            End If
            If rdbPMFineMacon.Checked Then
                AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
            End If

            query = "Update LookUpCountyInformation set " &
            "strNonAttainment = @att " &
            "where strCountyCode = @cty "

            Dim p2 As SqlParameter() = {
                New SqlParameter("@att", AttainmentStatus),
                p
            }

            DB.RunCommand(query, p2)

            query = "Select strAttainmentStatus, strAIRSNumber " &
            "from APBHeaderData " &
            "where SUBSTRING(strAIRSNumber, 5, 3) = @cty "

            Dim dt As DataTable = DB.GetDataTable(query, p)

            Dim query2 As String = "Update APBHeaderData set " &
                "strAttainmentStatus = @st " &
                "where strAIRSNumber = @airs "

            For Each dr As DataRow In dt.Rows
                If IsDBNull(dr.Item("strAttainmentStatus")) Then
                    AttainmentStatus = "00000"
                Else
                    AttainmentStatus = dr.Item("strAttainmentStatus")
                End If

                If rdbPMFineNo.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "0" & Mid(AttainmentStatus, 5)
                End If

                If rdbPMFineAtlanta.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "1" & Mid(AttainmentStatus, 5)
                End If

                If rdbPMFineChattanooga.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "2" & Mid(AttainmentStatus, 5)
                End If

                If rdbPMFineFloyd.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "3" & Mid(AttainmentStatus, 5)
                End If

                If rdbPMFineMacon.Checked Then
                    AttainmentStatus = Mid(AttainmentStatus, 1, 3) & "4" & Mid(AttainmentStatus, 5)
                End If

                Dim p3 As SqlParameter() = {
                    New SqlParameter("@st", AttainmentStatus),
                    New SqlParameter("@airs", dr.Item("strAirsNumber"))
                }

                DB.RunCommand(query2, p3)
            Next
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#Region " Button and DGV click events "

    Private Sub btnViewSelectedData_Click(sender As Object, e As EventArgs) Handles btnViewSelectedData.Click
        Try


            ViewSelectedData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnViewCountyInfo_Click(sender As Object, e As EventArgs) Handles btnViewCountyInfo.Click
        Try


            ViewCountyInformation()


        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub dgvAttainmentStatus_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvAttainmentStatus.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgvAttainmentStatus.HitTest(e.X, e.Y)

        Try
            If dgvAttainmentStatus.RowCount > 0 AndAlso hti.RowIndex <> -1 AndAlso
                dgvAttainmentStatus.Columns(0).HeaderText = "County Name" Then

                cboCounty.Text = dgvAttainmentStatus(0, hti.RowIndex).Value
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try



    End Sub

    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        Try


            SaveOneHourChanges()
            SaveEightHourChanges()
            SavePMFineChanges()

            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnOneHourChanges_Click(sender As Object, e As EventArgs) Handles btnOneHourChanges.Click
        Try

            SaveOneHourChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnEightHourChanges_Click(sender As Object, e As EventArgs) Handles btnEightHourChanges.Click
        Try

            SaveEightHourChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub btnPMFineChanges_Click(sender As Object, e As EventArgs) Handles btnPMFineChanges.Click
        Try

            SavePMFineChanges()
            MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click
        SaveOneHourChanges()
        SaveEightHourChanges()
        SavePMFineChanges()
        MsgBox("Done", MsgBoxStyle.Information, "Attainment Status Update")
    End Sub

#End Region

End Class