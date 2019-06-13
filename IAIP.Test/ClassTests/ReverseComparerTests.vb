Imports Xunit

Public Class ReverseComparerTests

    <Fact>
    Public Sub CompareEachElement()
        Dim dict As New Dictionary(Of Integer, String) From {
            {1, "abc"},
            {2, "def"},
            {3, "ghi"}
        }

        Dim mySortedList As New SortedList(Of Integer, String)(dict)
        Dim reverseSortedList As New SortedList(Of Integer, String)(dict, New ReverseComparer(Of Integer)(Comparer(Of Integer).Default))

        Dim count As Integer = mySortedList.Count

        Assert.Equal(count, reverseSortedList.Count)

        For i As Integer = 0 To mySortedList.Count - 1
            Assert.Equal(mySortedList.Values(i), reverseSortedList.Values(count - 1 - i))
        Next
    End Sub

End Class
