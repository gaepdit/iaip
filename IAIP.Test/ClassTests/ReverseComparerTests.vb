Imports Xunit

Public Class ReverseComparerTests

    Private ReadOnly mySortedList As SortedList(Of Integer, String)
    Private ReadOnly reverseSortedList As SortedList(Of Integer, String)

    Public Sub New()
        Dim dict As New Dictionary(Of Integer, String) From {
            {1, "abc"},
            {2, "def"},
            {3, "ghi"}
        }

        mySortedList = New SortedList(Of Integer, String)(dict)
        reverseSortedList = New SortedList(Of Integer, String)(dict, New ReverseComparer(Of Integer)(Comparer(Of Integer).Default))
    End Sub

    <Fact>
    Public Sub CompareEachElement()
        Dim count As Integer = mySortedList.Count

        Assert.Equal(count, reverseSortedList.Count)

        For i As Integer = 0 To mySortedList.Count - 1
            Assert.Equal(mySortedList.Values(i), reverseSortedList.Values(count - 1 - i))
        Next
    End Sub

End Class
