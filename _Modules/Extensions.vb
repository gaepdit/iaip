Imports System.Runtime.CompilerServices

Module Extensions

#Region "Datagridview"

    <Extension()> _
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
    Public Sub SaveAsExcelFile(ByVal datagridview As DataGridView)
        If datagridview.RowCount = 0 Then Exit Sub
        Dim errorMessage As String = ""
        Dim result As Boolean = False

        Try
            Dim dialog As New SaveFileDialog() With {
                .Filter = "Excel File (*.xlsx)|*.xlsx",
                .DefaultExt = ".xlsx",
                .FileName = "Export_" & System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") & ".xlsx"
            }

            If dialog.ShowDialog() = DialogResult.OK Then

                Dim dataTable As DataTable = datagridview.DataSource.Tables(datagridview.DataMember)

                For i = 0 To datagridview.Columns.Count - 1
                    dataTable.Columns(i).ColumnName = datagridview.Columns(i).HeaderText
                Next

                result = ExportDataTableToExcel(dialog.FileName, dataTable, errorMessage)

                If result Then
                    System.Diagnostics.Process.Start(dialog.FileName)
                Else
                    ErrorReport(errorMessage, datagridview.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
                End If
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), datagridview.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

#End Region

End Module