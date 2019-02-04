Imports System.Collections.Generic

Public NotInheritable Class ReverseComparer(Of T)
    Implements IComparer(Of T)

    Private ReadOnly original As IComparer(Of T)

    Public Sub New(original As IComparer(Of T))
        Me.original = original
    End Sub

    Public Function Compare(left As T, right As T) As Integer Implements IComparer(Of T).Compare
        Return original.Compare(right, left)
    End Function
End Class
