﻿Imports System.Linq
Imports System.Runtime.CompilerServices

Module DataGridViewExtensions

    <Extension()>
    Public Sub SanelyResizeColumns(datagridview As DataGridView,
                                   Optional maxWidth As Integer = 275,
                                   Optional minWidth As Integer = 40)

        ' Resize all columns to fit current content:
        datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Loop through columns & explicitly set column width and undo AutoSizeMode
        Dim currentWidth As Integer

        For Each column As DataGridViewColumn In datagridview.Columns
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            currentWidth = column.Width
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            column.Width = Math.Min(maxWidth, Math.Max(minWidth, currentWidth))
        Next

        ' Allow user resizing of columns:
        datagridview.AllowUserToResizeColumns = True

    End Sub

    <Extension()>
    Public Sub MakeColumnLookLikeLinks(dgv As DataGridView, columnName As String)
        dgv.Columns(columnName).DefaultCellStyle.ForeColor = SystemColors.HotTrack
    End Sub

    <Extension()>
    Public Sub MakeColumnLookLikeLinks(dgv As DataGridView, index As Integer)
        dgv.MakeColumnLookLikeLinks({index})
    End Sub

    <Extension()>
    Public Sub MakeColumnLookLikeLinks(dgv As DataGridView, index As Integer())
        If dgv.Columns.Count = 0 Then
            Return
        End If

        For Each col As Integer In index
            If col < dgv.Columns.Count Then
                dgv.Columns(col).DefaultCellStyle.ForeColor = SystemColors.HotTrack
            End If
        Next
    End Sub

    <Extension()>
    Public Sub MakeCellLookLikeHoveredLink(dgv As DataGridView, rowIndex As Integer, columnName As String, Optional hover As Boolean = True)
        If hover Then
            dgv.Cursor = Cursors.Hand
            dgv.Rows(rowIndex).Cells(columnName).Style.ForeColor = IaipColors.GridHoverForeColor
            dgv.Rows(rowIndex).Cells(columnName).Style.BackColor = IaipColors.GridHoverBackColor
            dgv.Rows(rowIndex).Cells(columnName).Style.SelectionForeColor = IaipColors.GridSelectionHoverForeColor
            dgv.Rows(rowIndex).Cells(columnName).Style.SelectionBackColor = IaipColors.GridSelectionHoverBackColor
            dgv.Rows(rowIndex).Cells(columnName).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Underline)
        Else
            dgv.Cursor = Cursors.Default
            dgv.Rows(rowIndex).Cells(columnName).Style.ForeColor = New Color()
            dgv.Rows(rowIndex).Cells(columnName).Style.BackColor = New Color()
            dgv.Rows(rowIndex).Cells(columnName).Style.SelectionForeColor = New Color()
            dgv.Rows(rowIndex).Cells(columnName).Style.SelectionBackColor = New Color()
            dgv.Rows(rowIndex).Cells(columnName).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Regular)
        End If
    End Sub

    <Extension()>
    Public Sub MakeCellLookLikeHoveredLink(dgv As DataGridView, rowIndex As Integer, columnIndex As Integer, Optional hover As Boolean = True)
        If hover Then
            dgv.Cursor = Cursors.Hand
            dgv.Rows(rowIndex).Cells(columnIndex).Style.ForeColor = IaipColors.GridHoverForeColor
            dgv.Rows(rowIndex).Cells(columnIndex).Style.BackColor = IaipColors.GridHoverBackColor
            dgv.Rows(rowIndex).Cells(columnIndex).Style.SelectionForeColor = IaipColors.GridSelectionHoverForeColor
            dgv.Rows(rowIndex).Cells(columnIndex).Style.SelectionBackColor = IaipColors.GridSelectionHoverBackColor
            dgv.Rows(rowIndex).Cells(columnIndex).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Underline)
        Else
            dgv.Cursor = Cursors.Default
            dgv.Rows(rowIndex).Cells(columnIndex).Style.ForeColor = New Color()
            dgv.Rows(rowIndex).Cells(columnIndex).Style.BackColor = New Color()
            dgv.Rows(rowIndex).Cells(columnIndex).Style.SelectionForeColor = New Color()
            dgv.Rows(rowIndex).Cells(columnIndex).Style.SelectionBackColor = New Color()
            dgv.Rows(rowIndex).Cells(columnIndex).Style.Font =
                New Font(dgv.DefaultCellStyle.Font, FontStyle.Regular)
        End If
    End Sub

    <Extension()>
    Public Function ContainsValue(c As DataGridViewColumn, value As Object) As Boolean
        Return c.DataGridView.Rows.Cast(Of DataGridViewRow).Any(Function(r) r.Cells(c.Index).Value.Equals(value))
    End Function

    <Extension()>
    Public Function RowIndexForValue(c As DataGridViewColumn, value As Object) As Integer?
        Return c.DataGridView.Rows.Cast(Of DataGridViewRow).
            Where(Function(r) r.Cells(c.Index).Value.Equals(value)).
            FirstOrDefault()?.Index
    End Function

End Module