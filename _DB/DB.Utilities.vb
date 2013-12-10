Imports System.Collections.Generic

Namespace DB
    Module Utilities

#Region "Utilities"

        Public Function GetNullable(Of T)(ByVal obj As Object) As T
            ' http://stackoverflow.com/a/870771/212978
            ' http://stackoverflow.com/a/9953399/212978
            If obj Is Nothing OrElse IsDBNull(obj) Then
                ' returns the default value for the type
                Return CType(Nothing, T)
            Else
                Return CType(obj, T)
            End If
        End Function

        Public Sub AddBlankRowToDictionary(ByRef d As Dictionary(Of Integer, String), Optional ByVal blankPrompt As String = "")
            d.Add(0, blankPrompt)
        End Sub

        Public Sub BindDictionaryToComboBox(ByVal d As Dictionary(Of Integer, String), ByVal c As ComboBox)
            With c
                .DataSource = New BindingSource(d, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End Sub

#End Region

    End Module
End Namespace
