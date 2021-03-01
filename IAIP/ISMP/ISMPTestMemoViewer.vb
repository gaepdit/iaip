Imports System.Data.SqlClient

Public Class ISMPTestMemoViewer

    Private Sub ISMPTestMemoViewer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LoadDataSet(True)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadDataSet(Loading As Boolean)
        Dim query As String
        Dim SQLLine As String = " "
        Dim SQLLine2 As String = "AND ("
        Dim SQLLine3 As String = "AND ("
        Dim SQLLine4 As String

        Try

            If Loading Then
                SQLLine = "and ISMPReportInformation.strReviewingEngineer = @user " &
                "and strClosed = 'False' "
            End If

            If txtFilterText1.Text <> "" Then
                SQLLine = "And strMemorandumField like @text1 "
            End If

            If chbOpen.Checked Then
                SQLLine2 = SQLLine2 & "strClosed = 'False' or "
            End If
            If chbClosed.Checked Then
                SQLLine2 = SQLLine2 & "strClosed = 'True' or "
            End If
            If SQLLine2 = "AND (" Then
                SQLLine2 = ""
            Else
                SQLLine2 = Mid(SQLLine2, 1, (Len(SQLLine2) - 4)) & ") "
            End If

            If chbComplianceStatus1.Checked Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '01' or "
            End If
            If chbComplianceStatus2.Checked Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '02' or "
            End If
            If chbComplianceStatus3.Checked Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '03' or "
            End If
            If chbComplianceStatus4.Checked Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '04' or "
            End If
            If chbComplianceStatus5.Checked Then
                SQLLine3 = SQLLine3 & "ISMPReportInformation.strCOmplianceStatus = '05' or "
            End If
            If SQLLine3 = "AND (" Then
                SQLLine3 = ""
            Else
                SQLLine3 = Mid(SQLLine3, 1, (Len(SQLLine3) - 4)) & ") "
            End If
            If chbDelete.Checked Then
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

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dgrMemoViewer.DataSource = dt
                dgrMemoViewer.Columns("strReferenceNumber").HeaderText = "Reference Number"
                dgrMemoViewer.Columns("strMemorandumField").HeaderText = "Memo Text Body"
                dgrMemoViewer.SanelyResizeColumns()
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
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
        End Try
    End Sub
    Private Sub dgrMemoViewer_MouseUp(sender As Object, e As MouseEventArgs) Handles dgrMemoViewer.MouseUp
        Dim hti As DataGridView.HitTestInfo = dgrMemoViewer.HitTest(e.X, e.Y)

        Try

            If hti.Type = DataGrid.HitTestType.Cell AndAlso
                Not IsDBNull(dgrMemoViewer(0, hti.RowIndex)) AndAlso
                Not IsDBNull(dgrMemoViewer(1, hti.RowIndex)) Then
                txtReferenceNumber.Text = dgrMemoViewer(0, hti.RowIndex).Value.ToString
                txtReferenceNumber2.Text = dgrMemoViewer(0, hti.RowIndex).Value.ToString
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

End Class
