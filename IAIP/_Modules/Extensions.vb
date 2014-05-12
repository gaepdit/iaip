Imports System.Runtime.CompilerServices
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
            For i As Integer = 0 To dgv.Columns.Count - 1
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

#Region "SplitContainer"

    ''' <summary>
    ''' Sets the SplitContainer.SplitterDistance property, while taking into account the SplitContainer's dimensions
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="dist">The desired SplitterDistance</param>
    ''' <remarks>If the desired SplitterDistance is incompatible with the dimension of the 
    ''' SplitContainer, nothing is changed and no error is returned</remarks>
    <Extension()> _
    Public Sub SanelySetSplitterDistance(ByVal sc As SplitContainer, ByVal dist As Integer)
        Dim i As Integer = dist

        If sc.Orientation = Orientation.Horizontal Then

            ' It may not seem possible for the size of a SplitContainer to be smaller than
            ' the minimum sizes of its parts, but it can happen if the SplitContainer is
            ' docked in a Form that is itself resized until the SplitContainer is too small.
            ' In this situation, don't try to fix things, just bail.
            If (sc.Height < sc.Panel1MinSize + sc.Panel2MinSize) Then Exit Sub

            ' The order here shouldn't matter
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Height - sc.Panel2MinSize)

        Else
            ' Same as above, except for vertical orientation
            If (sc.Width < sc.Panel1MinSize + sc.Panel2MinSize) Then Exit Sub
            i = Math.Max(i, sc.Panel1MinSize)
            i = Math.Min(i, sc.Width - sc.Panel2MinSize)
        End If

        sc.SplitterDistance = i
    End Sub

    ''' <summary>
    ''' Toggle SplitContainer.SplitterDistance between two given values
    ''' </summary>
    ''' <param name="sc">The SplitContainer to modify</param>
    ''' <param name="a">One of the values to toggle between</param>
    ''' <param name="b">One of the values to toggle between</param>
    ''' <remarks>The order of the parameters does not matter. If either parameter is incompatible with 
    ''' the dimension of the SplitContainer, nothing is changed and no error is returned.</remarks>
    <Extension()> _
    Public Sub ToggleSplitterDistance(ByVal sc As SplitContainer, ByVal a As Integer, ByVal b As Integer)
        ' Bail if a or b are outside the allowable values for SplitterDistance
        If (a < sc.Panel1MinSize) OrElse (b < sc.Panel1MinSize) Then Exit Sub
        If (sc.Orientation = Orientation.Vertical) Then
            If (a > sc.Width - sc.Panel2MinSize) OrElse (b > sc.Width - sc.Panel2MinSize) Then Exit Sub
        Else
            If (a > sc.Height - sc.Panel2MinSize) OrElse (b > sc.Height - sc.Panel2MinSize) Then Exit Sub
        End If

        ' If current SplitterDistance is smaller than the average, set it to the larger value;
        ' otherwise, set it to the smaller value
        If (sc.SplitterDistance < (a + b) / 2) Then
            sc.SanelySetSplitterDistance(Math.Max(a, b))
        Else
            sc.SanelySetSplitterDistance(Math.Min(a, b))
        End If
    End Sub

#End Region

End Module