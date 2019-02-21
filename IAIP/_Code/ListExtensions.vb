Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Module ListExtensions

    <Extension>
    Public Function AddRowToList(l As List(Of String), item As String) As List(Of String)
        l.Insert(0, item)
        Return l
    End Function

End Module