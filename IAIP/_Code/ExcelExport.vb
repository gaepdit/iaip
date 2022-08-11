Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports ClosedXML.Excel

Public Module ExcelExport

    Private ReadOnly BaseDate As New Date(1900, 1, 1)

    <Extension>
    Public Sub ExportToExcel(dataGridView As DataGridView, sender As Object)
        ArgumentNotNull(sender, NameOf(sender))

        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then
            AddBreadcrumb("ExportToExcel: Empty")
            MessageBox.Show("Table is empty", "Nothing to export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fileName As String = GetFileName()

        If fileName Is Nothing Then
            AddBreadcrumb("ExportToExcel: Cancelled")
            Return
        End If

        AddBreadcrumb("ExportToExcel", "Sender", CType(sender, Control).Name)

        If TypeOf sender Is Form Then CType(sender, Form).Cursor = Cursors.AppStarting

        Dim createExcelFileResult As CreateExcelFileResult = CreateExcelFileFromDataGridView(fileName, dataGridView)

        Select Case createExcelFileResult
            Case CreateExcelFileResult.Success
                If Path.GetDirectoryName(fileName) <> GetUserSetting(UserSetting.FileDownloadLocation) Then
                    SaveUserSetting(UserSetting.FileDownloadLocation, Path.GetDirectoryName(fileName))
                End If

                Process.Start("explorer.exe", $"/select,""{fileName}""")

            Case CreateExcelFileResult.DataTableEmpty
                MessageBox.Show("Table is empty.", "Nothing to export", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Case CreateExcelFileResult.FileInUse
                MessageBox.Show("The selected file is in use. Please close it or select a different filename and try again.", "File in use", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Case CreateExcelFileResult.OtherError
                MessageBox.Show("There was an error creating the Excel file.")

        End Select

        If TypeOf sender Is Form Then CType(sender, Form).Cursor = Nothing
    End Sub

    Private Function GetDataTableFromDataGridView(dataGridView As DataGridView) As DataTable
        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then Return Nothing

        Dim dataTable As New DataTable

        If TypeOf dataGridView.DataSource Is DataSet OrElse TypeOf dataGridView.DataSource Is DataTable Then
            If TypeOf dataGridView.DataSource Is DataSet Then
                dataTable = CType(dataGridView.DataSource, DataSet).Tables(dataGridView.DataMember)
            Else
                dataTable = CType(dataGridView.DataSource, DataTable)
            End If

            ' Replace column names with the defined column header text
            For i As Integer = 0 To dataGridView.Columns.Count - 1
                dataTable.Columns(i).Caption = dataGridView.Columns(i).HeaderText
            Next
        Else
            Dim dtRow As DataRow

            For Each dgvColumn As DataGridViewColumn In dataGridView.Columns
                Dim col As DataColumn = dataTable.Columns.Add(dgvColumn.Name)
                col.Caption = dgvColumn.HeaderText
            Next

            For Each dgvRow As DataGridViewRow In dataGridView.Rows
                dtRow = dataTable.NewRow
                For i As Integer = 0 To dataGridView.ColumnCount - 1
                    dtRow.Item(i) = dgvRow.Cells(i).Value
                Next
                dataTable.Rows.Add(dtRow)
            Next
        End If

        FixDates(dataTable)

        Return dataTable
    End Function

    Private Function GetFileName() As String
        Using dialog As New SaveFileDialog()
            With dialog
                .Filter = "Excel File (*.xlsx)|*.xlsx"
                .DefaultExt = ".xlsx"
                .FileName = "IAIP-Export-" & Date.Now.ToString("yyyy.MM.dd-HH.mm.ss") & ".xlsx"
                .InitialDirectory = GetUserSetting(UserSetting.FileDownloadLocation)
            End With

            If dialog.ShowDialog = DialogResult.Cancel Then
                Return Nothing
            End If

            If Not dialog.FileName.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase) Then
                dialog.FileName &= ".xlsx"
            End If

            Return dialog.FileName
        End Using
    End Function

    Private Function CreateExcelFileFromDataGridView(filePath As String, dataGridView As DataGridView) As CreateExcelFileResult
        Using dataTable As DataTable = GetDataTableFromDataGridView(dataGridView)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return CreateExcelFileResult.DataTableEmpty

            If String.IsNullOrWhiteSpace(dataTable.TableName) Then dataTable.TableName = "Sheet1"

            ' Create Excel Workbook 
            Using workbook As New XLWorkbook()
                Dim ws As IXLWorksheet = workbook.AddWorksheet(dataTable)
                ws.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top)
                ws.Columns().AdjustToContents(2, 8.0R, 80.0R)

                For Each col As DataGridViewColumn In dataGridView.Columns
                    If Not col.Visible AndAlso (col.Tag Is Nothing OrElse col.Tag.ToString <> "NotHidden") Then
                        ws.Column(col.Index + 1).Hide()
                    End If
                Next

                Try
                    workbook.SaveAs(filePath)
                Catch ex As IOException
                    If ex.Message.Contains("The process cannot access the file") AndAlso
                        ex.Message.Contains("because it is being used by another process") Then
                        Return CreateExcelFileResult.FileInUse
                    Else
                        Return CreateExcelFileResult.OtherError
                    End If
                Catch ex As Exception
                    ErrorReport(ex, MethodBase.GetCurrentMethod.Name)
                    Return CreateExcelFileResult.OtherError
                End Try

                Return CreateExcelFileResult.Success
            End Using
        End Using
    End Function

    Public Sub FixDates(dataTable As DataTable)
        ArgumentNotNull(dataTable, NameOf(dataTable))

        For Each col As DataColumn In dataTable.Columns
            If col.DataType Is GetType(Date) OrElse col.DataType Is GetType(DateTimeOffset) Then
                For Each row As DataRow In dataTable.Rows
                    If Not IsDBNull(row(col.ColumnName)) AndAlso
                        row(col.ColumnName) IsNot Nothing AndAlso
                        CDate(row(col.ColumnName)) < BaseDate Then

                        row(col.ColumnName) = DBNull.Value

                    End If
                Next
            End If
        Next
    End Sub

    Enum CreateExcelFileResult
        Success
        DataTableEmpty
        FileInUse
        OtherError
    End Enum

End Module
