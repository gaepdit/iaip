Imports System.Data.SqlClient



Public Class ISMPTestMemoViewer
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim dsMemo As DataSet
    Dim daMemo As SqlDataAdapter


    Private Sub ISMPTestMemoViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            CreateStatusBar()

            LoadDataSet(True)
            FormatdgrTestReportViewer()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


#Region "Page Load Functions"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = CurrentUser.AlphaName
            panel3.Text = TodayFormatted

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


#End Region

    Private Sub ISMPTestMemoViewer_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#Region "Functions"
    Sub LoadDataSet(Loading As String)
        Dim SQL As String
        Dim SQLLine As String = " "
        Dim SQLLine2 As String = "AND ("
        Dim SQLLine3 As String = "AND ("
        Dim SQLLine4 As String = " "

        Try

            If Loading = True Then
                SQLLine = "and ISMPReportInformation.strReviewingEngineer = '" & CurrentUser.UserID & "' " &
                "and strClosed = 'False' "
            End If

            If txtFilterText1.Text <> "" Then
                SQLLine = "And Upper(strMemorandumField) like Upper('%" & txtFilterText1.Text & "%') "
            Else
                'SQLLine = SQLLine
            End If

            If chbOpen.Checked = True Then
                SQLLine2 = SQLLine2 & "strClosed = 'False' or "
            End If
            If chbClosed.Checked = True Then
                SQLLine2 = SQLLine2 & "strClosed = 'True' or "
            End If
            If SQLLine2 = "AND (" Then
                SQLLine2 = ""
            Else
                SQLLine2 = Mid(SQLLine2, 1, (Len(SQLLine2) - 4)) & ") "
            End If

            If chbComplianceStatus1.Checked = True Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '01' or "
            End If
            If chbComplianceStatus2.Checked = True Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '02' or "
            End If
            If chbComplianceStatus3.Checked = True Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '03' or "
            End If
            If chbComplianceStatus4.Checked = True Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '04' or "
            End If
            If chbComplianceStatus5.Checked = True Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '05' or "
            End If
            If SQLLine3 = "AND (" Then
                SQLLine3 = ""
            Else
                SQLLine3 = Mid(SQLLine3, 1, (Len(SQLLine3) - 4)) & ") "
            End If
            If chbDelete.Checked = True Then
                SQLLine4 = "And ISMPReportInformation.strDelete = 'DELETE' "
            Else
                SQLLine4 = "And ISMPReportInformation.strDelete is NULL "
            End If

            SQL = "select ISMPTestREportMemo.strReferenceNumber, strMemorandumField " &
            "from ISMPTestREportMemo, ISMPReportInformation " &
            "where ISMPTestREportMemo.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            SQLLine & SQLLine2 & SQLLine3 & SQLLine4

            dsMemo = New DataSet

            Dim cmd As New SqlCommand(SQL, CurrentConnection)

            daMemo = New SqlDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daMemo.Fill(dsMemo, "TestMemoViewer")
            dgrMemoViewer.DataSource = dsMemo
            dgrMemoViewer.DataMember = "TestMemoViewer"

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Sub FormatdgrTestReportViewer()
        Try

            'Formatting our DataGrid
            Dim objGrid As New DataGridTableStyle
            Dim objtextcol As New DataGridTextBoxColumn
            'Dim objDateCol As New DataGridTimePickerColumn

            objGrid.AlternatingBackColor = Color.WhiteSmoke
            objGrid.MappingName = "TestMemoViewer"
            objGrid.AllowSorting = True
            objGrid.ReadOnly = True
            objGrid.RowHeadersVisible = False

            'Setting the Column Headings  1
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strReferenceNumber"
            objtextcol.HeaderText = "Reference Number"
            objtextcol.Alignment = HorizontalAlignment.Center
            objtextcol.Width = 110
            objGrid.GridColumnStyles.Add(objtextcol)

            'Setting the Column Headings   2
            objtextcol = New DataGridTextBoxColumn
            objtextcol.MappingName = "strMemorandumField"
            objtextcol.HeaderText = "Memo Text Body"
            objtextcol.Alignment = HorizontalAlignment.Left
            objtextcol.Width = 400
            objGrid.GridColumnStyles.Add(objtextcol)

            'Applying the above formating 
            dgrMemoViewer.TableStyles.Clear()
            dgrMemoViewer.TableStyles.Add(objGrid)

            'Setting the DataGrid Caption, which defines the table title
            dgrMemoViewer.CaptionText = "Test Memo Viewer"
            dgrMemoViewer.ColumnHeadersVisible = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub ResetOptions()
        Try

            txtFilterText1.Clear()
            chbOpen.Checked = False
            chbClosed.Checked = False
            chbDelete.Checked = False
            chbComplianceStatus1.Checked = False
            chbComplianceStatus2.Checked = False
            chbComplianceStatus3.Checked = False
            chbComplianceStatus4.Checked = False
            chbComplianceStatus5.Checked = False
            txtReferenceNumber.Clear()
            txtReferenceNumber2.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Sub SelectTestReport()
        Try
            Dim id As String = txtReferenceNumber.Text
            If DAL.Ismp.StackTestExists(id) Then OpenMultiForm(ISMPTestReports, id)
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
#End Region

    Private Sub TBTestMemoViewer_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles TBTestMemoViewer.ButtonClick
        Try

            Select Case TBTestMemoViewer.Buttons.IndexOf(e.Button)
                Case 0
                    ResetOptions()
                Case 1
                    Me.Close()
                Case 2
                    Me.Close()
                Case Else
                    MsgBox("try clicking again")
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiBack_Click(sender As Object, e As EventArgs) Handles MmiBack.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiExit_Click(sender As Object, e As EventArgs) Handles MmiExit.Click
        Try

            Me.Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiCut_Click(sender As Object, e As EventArgs) Handles mmiCut.Click
        Try

            SendKeys.Send("^(x)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiCopy_Click(sender As Object, e As EventArgs) Handles MmiCopy.Click
        Try

            SendKeys.Send("^(c)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiPaste_Click(sender As Object, e As EventArgs) Handles MmiPaste.Click
        Try

            SendKeys.Send("^(v)")
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiReset_Click(sender As Object, e As EventArgs) Handles MmiReset.Click
        Try

            ResetOptions()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub mmiShowToolbar_Click(sender As Object, e As EventArgs) Handles mmiShowToolbar.Click
        Try

            If TBTestMemoViewer.Visible = True Then
                TBTestMemoViewer.Visible = False
                mmiShowToolbar.Checked = True
            Else
                TBTestMemoViewer.Visible = True
                mmiShowToolbar.Checked = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub MmiHelp_Click(sender As Object, e As EventArgs) Handles MmiHelp.Click
        OpenDocumentationUrl(Me)
    End Sub
    Private Sub LLSelectReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LLSelectReport.LinkClicked
        Try

            SelectTestReport()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub LLViewMemo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LLViewMemo.LinkClicked
        OpenFormTestMemo(Me.txtReferenceNumber2.Text)
    End Sub
    Private Sub LLRunSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LLRunSearch.LinkClicked
        Try

            dsMemo.Clear()
            LoadDataSet(False)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub
    Private Sub dgrMemoViewer_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrMemoViewer.MouseUp
        Dim hti As DataGrid.HitTestInfo = dgrMemoViewer.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(dgrMemoViewer(hti.Row, 0)) Then
                Else
                    If IsDBNull(dgrMemoViewer(hti.Row, 1)) Then
                    Else
                        txtReferenceNumber.Text = dgrMemoViewer(hti.Row, 0)
                        txtReferenceNumber2.Text = dgrMemoViewer(hti.Row, 0)
                    End If
                End If
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub


End Class
