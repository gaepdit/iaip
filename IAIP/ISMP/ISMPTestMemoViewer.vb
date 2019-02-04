Imports System.Data.SqlClient

Public Class ISMPTestMemoViewer

    Private Sub ISMPTestMemoViewer_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            LoadDataSet(True)
            FormatdgrTestReportViewer()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try

    End Sub

    Private Sub LoadDataSet(Loading As Boolean)
        Dim query As String
        Dim SQLLine As String = " "
        Dim SQLLine2 As String = "AND ("
        Dim SQLLine3 As String = "AND ("
        Dim SQLLine4 As String = " "

        Try

            If Loading = True Then
                SQLLine = "and ISMPReportInformation.strReviewingEngineer = @user " &
                "and strClosed = 'False' "
            End If

            If txtFilterText1.Text <> "" Then
                SQLLine = "And strMemorandumField like @text1 "
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

            query = "select ISMPTestREportMemo.strReferenceNumber, strMemorandumField " &
            "from ISMPTestREportMemo, ISMPReportInformation " &
            "where ISMPTestREportMemo.strReferenceNumber = ISMPReportInformation.strReferenceNumber " &
            SQLLine & SQLLine2 & SQLLine3 & SQLLine4


            Dim p As SqlParameter() = {
                New SqlParameter("@user", CurrentUser.UserID),
                New SqlParameter("@text1", "%" & txtFilterText1.Text & "%")
            }

            Dim dt As DataTable = DB.GetDataTable(query, p)
            dt.TableName = "TestMemoViewer"

            dgrMemoViewer.DataSource = dt

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

        End Try


    End Sub
    Private Sub FormatdgrTestReportViewer()
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
    Private Sub ResetOptions()
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
    Private Sub SelectTestReport()
        Try
            OpenFormTestReportEntry(txtReferenceNumber.Text)
            Close()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
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
