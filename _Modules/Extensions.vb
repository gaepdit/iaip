Imports System.Runtime.CompilerServices

Module Extensions

#Region "Datagridview"

    <Extension()> _
    Public Sub SanelyResizeColumns(ByVal datagridview As DataGridView, _
                                    Optional ByVal maxWidth As Integer = 275)
        ' Resize all columns to fit current content:
        datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Loop through columns & explicitly set column width:
        Dim currentWidth As Integer
        For Each col As DataGridViewColumn In datagridview.Columns
            currentWidth = col.Width
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            col.Width = Math.Min(maxWidth, currentWidth)
        Next

        ' Allow user resizing of columns:
        datagridview.AllowUserToResizeColumns = True
    End Sub

#End Region

End Module
