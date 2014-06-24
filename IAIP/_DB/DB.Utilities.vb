Imports System.Collections.Generic
Imports System.IO

Namespace DB
    Module Utilities

        <DebuggerStepThrough()> _
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

        Public Function AddBlankRowToList(ByRef l As List(Of String), Optional ByVal blankPrompt As String = "") As List(Of String)
            l.Insert(0, blankPrompt)
            Return l
        End Function

        Public Sub BindDictionaryToComboBox(ByVal d As Dictionary(Of Integer, String), ByVal c As ComboBox)
            With c
                .DataSource = New BindingSource(d, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End Sub

        Public Sub BindSortedDictionaryToComboBox(ByVal d As SortedDictionary(Of Integer, String), ByVal c As ComboBox)
            With c
                .DataSource = New BindingSource(d, Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End Sub

        Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Dim fs As New FileStream(pathToFile, FileMode.Open, FileAccess.Read)

            Dim byteArray As Byte() = File.ReadAllBytes(pathToFile)

            fs.Close()
            fs.Dispose()

            Return byteArray
        End Function

    End Module
End Namespace
