Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Module CollectionExtensions

    <Extension>
    Public Function ContainsAny(Of T)(set1 As ICollection(Of T), set2 As ICollection(Of T)) As Boolean
        If set1 Is Nothing OrElse set2 Is Nothing Then Return False
        If set1 Is set2 Then Return True
        If set1.Count < set2.Count Then
            Dim hs As New HashSet(Of T)(set1)
            For Each v As T In set2
                If hs.Contains(v) Then Return True
            Next
        Else
            Dim hs As New HashSet(Of T)(set2)
            For Each v As T In set1
                If hs.Contains(v) Then Return True
            Next
        End If
        Return False
    End Function

End Module