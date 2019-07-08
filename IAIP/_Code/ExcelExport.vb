Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports ClosedXML.Excel

Module ExcelExport

#Region " DataGridView Extension "

    <Extension()>
    Public Sub ExportToExcel(dataGridView As DataGridView, Optional sender As Object = Nothing)
        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then
            MessageBox.Show("Table is empty", "Nothing to export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim dataTable As DataTable = GetDataTableFromDataGridView(dataGridView)
        dataTable.ExportToExcel(sender)
    End Sub

    Private Function GetDataTableFromDataGridView(dataGridView As DataGridView) As DataTable
        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then Return Nothing

        Dim dataTable As New DataTable

        If TypeOf dataGridView.DataSource Is DataSet Then
            dataTable = CType(dataGridView.DataSource, DataSet).Tables(dataGridView.DataMember)
        ElseIf TypeOf dataGridView.DataSource Is DataTable Then
            dataTable = CType(dataGridView.DataSource, DataTable)
        Else
            Dim dtRow As DataRow
            For Each dgvColumn As DataGridViewColumn In dataGridView.Columns
                dataTable.Columns.Add(dgvColumn.Name)
            Next
            For Each dgvRow As DataGridViewRow In dataGridView.Rows
                dtRow = dataTable.NewRow
                For i As Integer = 0 To dataGridView.ColumnCount - 1
                    dtRow.Item(i) = dgvRow.Cells(i).Value
                Next
                dataTable.Rows.Add(dtRow)
            Next
        End If

        ' Replace column names with the defined column header text
        For i As Integer = 0 To dataGridView.Columns.Count - 1
            dataTable.Columns(i).Caption = dataGridView.Columns(i).HeaderText
        Next

        Return dataTable
    End Function

#End Region

#Region " DataTable Extension "

    <Extension()>
    Public Sub ExportToExcel(dataTable As DataTable, Optional sender As Object = Nothing)
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then
            MessageBox.Show("Table is empty.", "Nothing to export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
            CType(sender, Form).Cursor = Cursors.AppStarting
        End If

        Dim fileName As String = GetFileName()

        If fileName Is Nothing Then
            Exit Sub
        End If

        Select Case CreateExcelFileFromDataTable(fileName, dataTable)
            Case CreateExcelFileResult.Success
                If Not Path.GetDirectoryName(fileName) = GetUserSetting(UserSetting.FileDownloadLocation) Then
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

        If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
            CType(sender, Form).Cursor = Nothing
        End If
    End Sub

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

            Return dialog.FileName
        End Using
    End Function

#End Region


    ''' <summary>
    ''' Saves a DataTable to an Excel file on the first sheet
    ''' </summary>
    ''' <param name="filePath">The Excel file to create</param>
    ''' <param name="dataTable">DataTable to write to Excel file</param>
    ''' <returns>True if file successfully created; otherwise, false</returns>
    Private Function CreateExcelFileFromDataTable(filePath As String, dataTable As DataTable) As CreateExcelFileResult
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return CreateExcelFileResult.DataTableEmpty

        If String.IsNullOrWhiteSpace(dataTable.TableName) Then
            dataTable.TableName = "Sheet1"
        End If

        ' Create Excel Workbook 
        Using workbook As New XLWorkbook()

            workbook.AddWorksheet(dataTable)

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
    End Function

    Enum CreateExcelFileResult
        Success
        DataTableEmpty
        FileInUse
        OtherError
    End Enum

End Module
