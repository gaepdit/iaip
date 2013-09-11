Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Module Extensions

#Region "DataGridView"

    <Extension()>
    Public Sub SanelyResizeColumns(ByVal datagridview As DataGridView,
                                      Optional ByVal maxWidth As Integer = 275)

        ' Resize all columns to fit current content:
        datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Loop through columns & explicitly set column width and undo AutoSizeMode
        Dim currentWidth As Integer
        For Each column As DataGridViewColumn In datagridview.Columns
            currentWidth = column.Width
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            column.Width = Math.Min(maxWidth, currentWidth)
        Next

        ' Allow user resizing of columns:
        datagridview.AllowUserToResizeColumns = True

    End Sub

    <Extension()>
    Public Function SaveAsExcelFile(ByVal dataGridView As DataGridView,
                                    ByVal fileName As String,
                                    <Out()> Optional ByRef errorMessage As String = Nothing
                                                                                    ) As Boolean
        Dim result As Boolean = False
        errorMessage = ""

        If dataGridView.RowCount = 0 Then Return result

        Try
            Dim dataTable As DataTable = dataGridView.DataSource.Tables(dataGridView.DataMember)

            ' Replace column names with the defined column header text
            For i = 0 To dataGridView.Columns.Count - 1
                dataTable.Columns(i).ColumnName = dataGridView.Columns(i).HeaderText
            Next

            result = ExportDataTableToExcel(fileName, dataTable, errorMessage)
        Catch ex As Exception
            errorMessage = ex.ToString()
            ErrorReport(ex.ToString(), dataGridView.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        Return result
    End Function

#End Region

End Module