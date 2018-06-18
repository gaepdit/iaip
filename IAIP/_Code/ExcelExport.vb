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
            MessageBox.Show("Table is empty", "Nothing to export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
            CType(sender, Form).Cursor = Cursors.AppStarting
        End If

        Using dialog As New SaveFileDialog()

            With dialog
                .Filter = "Excel File (*.xlsx)|*.xlsx"
                .DefaultExt = ".xlsx"
                .FileName = "Export_" & Date.Now.ToString("yyyy-MM-dd_HH.mm.ss") & ".xlsx"
                .InitialDirectory = GetUserSetting(UserSetting.ExcelExportLocation)
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                If CreateExcelFileFromDataTable(dialog.FileName, dataTable) Then
                    If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                        SaveUserSetting(UserSetting.ExcelExportLocation, Path.GetDirectoryName(dialog.FileName))
                    End If

                    Process.Start("explorer.exe", "/select,""" & dialog.FileName & """")
                    'Process.Start(dialog.FileName)
                Else
                    MessageBox.Show("There was an error creating the Excel file.")
                End If
            End If

        End Using

        If sender IsNot Nothing AndAlso TypeOf sender Is Form Then
            CType(sender, Form).Cursor = Nothing
        End If
    End Sub

#End Region


    ''' <summary>
    ''' Saves a DataTable to an Excel file on the first sheet
    ''' </summary>
    ''' <param name="filePath">The Excel file to create</param>
    ''' <param name="dataTable">DataTable to write to Excel file</param>
    ''' <returns>True if file successfully created; otherwise, false</returns>
    Private Function CreateExcelFileFromDataTable(filePath As String, dataTable As DataTable) As Boolean
        If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return False

        If String.IsNullOrWhiteSpace(dataTable.TableName) Then
            dataTable.TableName = "Sheet1"
        End If

        Try
            ' Create Excel Workbook 
            Dim workbook As New XLWorkbook()
            workbook.AddWorksheet(dataTable)
            workbook.SaveAs(filePath)

            Return True
        Catch ex As Exception
            ErrorReport(ex, MethodBase.GetCurrentMethod.Name)

            Try
                ' Try to delete the file, but don't crash if you can't
                File.Delete(filePath)
            Catch
            End Try

            Return False
        End Try
    End Function

End Module
