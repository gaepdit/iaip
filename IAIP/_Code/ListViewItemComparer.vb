Public Class ListViewItemComparer

    Implements IComparer

    Private ReadOnly col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(column As Integer)
        col = column
    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        NotNull(x, NameOf(x))
        NotNull(y, NameOf(y))

        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        Return [String].Compare(CType(y, ListViewItem).SubItems(col).Text, CType(x, ListViewItem).SubItems(col).Text)
    End Function
End Class
