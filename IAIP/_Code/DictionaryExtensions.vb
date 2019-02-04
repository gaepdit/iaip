Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Module DictionaryExtensions

    <Extension()>
    Public Sub AddBlankRow(ByRef d As Dictionary(Of Integer, String), Optional blankPrompt As String = "")
        d.Add(0, blankPrompt)
    End Sub

End Module