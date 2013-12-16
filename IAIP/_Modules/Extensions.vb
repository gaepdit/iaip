﻿Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Collections.Generic

Module Extensions

#Region "DataGridView"

#Region "Resize columns"

    <Extension()> _
    Public Sub SanelyResizeColumns(ByVal datagridview As DataGridView, _
                                      Optional ByVal maxWidth As Integer = 275)

        ' Resize all columns to fit current content:
        datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Loop through columns & explicitly set column width and undo AutoSizeMode
        Dim currentWidth As Integer
        For Each column As DataGridViewColumn In datagridview.Columns
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            currentWidth = column.Width
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            column.Width = Math.Min(maxWidth, currentWidth)
        Next

        ' Allow user resizing of columns:
        datagridview.AllowUserToResizeColumns = True

    End Sub

#End Region

#Region "Excel export"

    <Extension()> _
    Public Sub ExportToExcel(ByVal dataGridView As DataGridView, _
                                  Optional ByVal sender As Object = Nothing)

        If dataGridView Is Nothing OrElse dataGridView.RowCount = 0 Then Exit Sub

        If sender IsNot Nothing Then
            sender.Cursor = Cursors.AppStarting
        End If

        Dim dialog As New SaveFileDialog()
        With dialog
            .Filter = "Excel File (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            .FileName = "Export_" & System.DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") & ".xlsx"
            .InitialDirectory = GetUserSetting(UserSetting.ExcelExportLocation)
        End With

        If dialog.ShowDialog() = DialogResult.OK Then
            Dim errorMessage As String = ""
            Dim result As Boolean = SaveAsExcelFile(dataGridView, dialog.FileName, errorMessage)

            If result Then
                If Not Path.GetDirectoryName(dialog.FileName) = dialog.InitialDirectory Then
                    SaveUserSetting(UserSetting.ExcelExportLocation, Path.GetDirectoryName(dialog.FileName))
                End If
                System.Diagnostics.Process.Start(dialog.FileName)
            Else
                MessageBox.Show(errorMessage)
            End If
        End If

        dialog.Dispose()

        If sender IsNot Nothing Then
            sender.Cursor = Nothing
        End If
    End Sub

    Private Function SaveAsExcelFile(ByVal dgv As DataGridView, _
                                    ByVal fileName As String, _
                                    <Out()> Optional ByRef errorMessage As String = Nothing _
                                    ) As Boolean

        Dim result As Boolean = False
        errorMessage = ""

        If dgv.RowCount = 0 Then Return result

        Dim dataTable As New DataTable

        Try
            If TypeOf dgv.DataSource Is DataSet Then
                dataTable = dgv.DataSource.Tables(dgv.DataMember)
            ElseIf TypeOf dgv.DataSource Is DataTable Then
                dataTable = dgv.DataSource
            Else
                dataTable = GetDataTableFromDgv(dgv)
            End If

            ' Replace column names with the defined column header text
            For i = 0 To dgv.Columns.Count - 1
                dataTable.Columns(i).Caption = dgv.Columns(i).HeaderText
            Next

            result = ExportDataTableToExcel(fileName, dataTable, errorMessage)
        Catch ex As Exception
            errorMessage = ex.ToString()
            ErrorReport(ex, dgv.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        Return result
    End Function

    Private Function GetDataTableFromDgv(ByVal dgv As DataGridView) As DataTable
        If dgv.RowCount = 0 Then Return Nothing

        Dim dt As New DataTable
        Dim dtColumn As DataColumn
        Dim dtRow As DataRow

        For Each dgvColumn As DataGridViewColumn In dgv.Columns
            dtColumn = dt.Columns.Add(dgvColumn.Name)
        Next

        For Each dgvRow As DataGridViewRow In dgv.Rows
            dtRow = dt.NewRow
            For i As Integer = 0 To dgv.ColumnCount - 1
                dtRow.Item(i) = dgvRow.Cells(i).Value
            Next
            dt.Rows.Add(dtRow)
        Next

        Return dt
    End Function

#End Region

#End Region

#Region "Dictionary"

    <Extension()> _
    Public Function ChangeKey(Of TKey, TValue)(ByVal dict As Dictionary(Of TKey, TValue), ByVal oldKey As TKey, ByVal newKey As TKey) As Boolean
        Dim value As TValue

        If dict.ContainsKey(newKey) Then Return False
        If Not dict.TryGetValue(oldKey, value) Then Return False

        dict.Remove(oldKey)
        dict.Add(newKey, value)
        Return True
    End Function

#End Region

End Module