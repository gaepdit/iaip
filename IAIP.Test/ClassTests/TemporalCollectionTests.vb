Imports Xunit

Public Class TemporalCollectionTests

    Private ReadOnly coll As TemporalCollection(Of String)

    Public Sub New()
        Dim dict As New Dictionary(Of Date, String) From {
            {New Date(2015, 1, 1), "abc"},
            {New Date(2016, 1, 1), "def"},
            {New Date(2017, 1, 1), "ghi"},
            {New Date(2018, 1, 1), "jkl"},
            {New Date(2089, 1, 1), "uvw"},
            {New Date(2099, 1, 1), "xyz"}
        }

        coll = New TemporalCollection(Of String)(dict)
    End Sub

    <Fact>
    Public Sub CheckValues()
        Assert.Equal("abc", coll.GetValueAt(New Date(2015, 1, 1)))
        Assert.Equal("def", coll.GetValueAt(New Date(2016, 6, 1)))
        Assert.Equal(coll.GetValueAt(New Date(2017, 4, 1)), coll.GetValueAt(New Date(2017, 8, 1)))
    End Sub

    <Fact>
    Public Sub CheckCurrentValues()
        Assert.Equal("jkl", coll.GetCurrentValue)
    End Sub

    <Fact>
    Public Sub CheckFutureValues()
        Assert.Equal("uvw", coll.GetValueAt(New Date(2095, 1, 1)))
        Assert.Equal("xyz", coll.GetValueAt(New Date(2150, 1, 1)))
    End Sub

    <Fact>
    Public Sub CheckEarliestDate()
        Assert.Equal(New Date(2015, 1, 1), coll.EarliestDate)
    End Sub

    <Fact>
    Public Sub AddNewValue()
        coll.AddValue(New Date(2009, 1, 1), "mno")
        Assert.Equal("mno", coll.GetValueAt(New DateTime(2010, 1, 1)))
    End Sub

    <Fact>
    Public Sub ErrorIfDateOutOfRange()
        Assert.Throws(Of ArgumentOutOfRangeException)(Function() coll.GetValueAt(New Date(2010, 1, 1)))
    End Sub

    <Fact>
    Public Sub ErrorIfEmptyCollection()
        Dim emptyColl As New TemporalCollection(Of String)

        Assert.Equal(0, emptyColl.Count)
        Assert.Throws(Of InvalidOperationException)(Function() emptyColl.GetValueAt(New Date(2010, 1, 1)))
        Assert.Throws(Of InvalidOperationException)(Function() emptyColl.EarliestDate)
    End Sub

End Class
