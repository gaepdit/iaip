Imports System.Data.SqlClient


Public Class ISMPAddPollutants
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim recExist As Boolean
    Dim dsPollutant As DataSet
    Dim daPollutant As SqlDataAdapter


    Private Sub ISMPAddPollutants_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            CreateStatusBar()
            FormatdgrPollutants()
            LoadDataSet()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub LoadDataSet()
        Try

            SQL = "Select strPollutantCode, strPOllutantDescription " &
                 "from LookUPPollutants " &
                 "Order by strPollutantDescription"

            dsPollutant = New DataSet

            daPollutant = New SqlDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daPollutant.Fill(dsPollutant, "Pollutant")
            dgrPollutants.DataSource = dsPollutant
            dgrPollutants.DataMember = "Pollutant"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub FormatdgrPollutants()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "Pollutant"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPollutantCode"
            objtextcol.HeaderText = "Pollutant Code"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 100
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings    2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strPOllutantDescription"
            objtextcol.HeaderText = "Pollutant Description"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrPollutants.TableStyles.Clear()
            dgrPollutants.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrPollutants.CaptionText = "Pollutant(s)"
            dgrPollutants.ColumnHeadersVisible = True

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

#End Region
#Region "Functions and Subs"
    Sub Save()
        Try

            If txtPollutant.Text <> "" Then
                If txtPollutantCode.Text = "" Then
                    GetNextPollutantCode()
                End If
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                If chbDeletePollutant.Checked = True Then
                    SQL = "Delete LookUPPollutants " &
                    "where strPollutantCode = '" & txtPollutantCode.Text & "' "
                Else
                    SQL = "Select strPollutantDescription " &
                    "from LookUPPollutants " &
                    "where strPollutantcode = '" & txtPollutantCode.Text & "' "

                    cmd = New SqlCommand(SQL, CurrentConnection)

                    dr = cmd.ExecuteReader
                    recExist = dr.Read
                    If recExist = True Then
                        SQL = "Update LookUPPollutants set " &
                        "strPollutantDescription = '" & txtPollutant.Text & "', " &
                        "strPollutantCode = '" & txtPollutantCode.Text & "' " &
                        "where strPollutantCode = '" & txtPollutantCode.Text & "' "
                    Else
                        SQL = "Insert into LookUPPollutants " &
                        "(strPollutantCode, strPollutantDescription) " &
                        "values " &
                        "('" & txtPollutantCode.Text & "', '" & txtPollutant.Text & "') "
                    End If
                End If
                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader

                LoadDataSet()


            Else
                MsgBox("You must add a pollutant", MsgBoxStyle.Information, "ISMP Add Pollutant")
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub Clear()
        Try

            txtPollutant.Clear()
            txtPollutantCode.Clear()
            chbDeletePollutant.Checked = False
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub Back()
        Try

            ISMPAddPollutant = Nothing
            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub GetNextPollutantCode()
        Dim PollutantCode As String
        Dim newPollutantCode As String
        Try

            PollutantCode = "00001"
            newPollutantCode = "00000"

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            Do Until newPollutantCode <> "00000"
                Select Case PollutantCode.Length
                    Case 1
                        PollutantCode = "0000" & PollutantCode
                    Case 2
                        PollutantCode = "000" & PollutantCode
                    Case 3
                        PollutantCode = "00" & PollutantCode
                    Case 4
                        PollutantCode = "0" & PollutantCode
                    Case Else
                        'PollutantCode = PollutantCode
                End Select

                SQL = "Select strPollutantCode " &
                "from LookUPPollutants " &
                "where strPollutantCode = '" & PollutantCode & "' "
                cmd = New SqlCommand(SQL, CurrentConnection)
                dr = cmd.ExecuteReader
                recExist = dr.Read
                If recExist = True Then
                    PollutantCode += 1
                Else
                    newPollutantCode = PollutantCode
                End If
            Loop



            Select Case PollutantCode.Length
                Case 1
                    PollutantCode = "0000" & PollutantCode
                Case 2
                    PollutantCode = "000" & PollutantCode
                Case 3
                    PollutantCode = "00" & PollutantCode
                Case 4
                    PollutantCode = "0" & PollutantCode
                Case Else
                    'PollutantCode = PollutantCode
            End Select
            txtPollutantCode.Text = PollutantCode

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
#End Region

    Private Sub TBAddPollutant_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBAddPollutant.ButtonClick
        Try

            Select Case TBAddPollutant.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrPollutants_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrPollutants.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrPollutants.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrPollutants(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrPollutants(hti.Row, 1)) Then
                    Else
                        txtPollutantCode.Text = dgrPollutants(hti.Row, 0)
                        txtPollutant.Text = dgrPollutants(hti.Row, 1)
                    End If
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub ISMPAddPollutants_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            ISMPAddPollutant = Nothing
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
    Private Sub MmiBack_Click(sender As Object, e As EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(sender As Object, e As EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("(^X)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCopy_Click(sender As Object, e As EventArgs) Handles mmiCopy.Click
        Try

            SendKeys.Send("(^C)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiPaste_Click(sender As Object, e As EventArgs) Handles mmiPaste.Click
        Try

            SendKeys.Send("(^V)")
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
    Private Sub mmiHelp_Click(sender As Object, e As EventArgs) Handles mmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
#End Region


    Private Sub llbGetNextValue_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbGetNextValue.LinkClicked
        Try

            GetNextPollutantCode()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


End Class
