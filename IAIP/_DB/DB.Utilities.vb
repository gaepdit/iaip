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

        Public Function GetNullableDateTimeFromString(ByVal obj As Object) As DateTime?
            Try

                If obj Is Nothing OrElse IsDBNull(obj) OrElse String.IsNullOrEmpty(obj) Then
                    Return Nothing
                Else
                    Dim newDate As New DateTime
                    If DateTime.TryParse(obj, newDate) Then
                        Return newDate
                    Else
                        Return Nothing
                    End If
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Dim fs As New FileStream(pathToFile, FileMode.Open, FileAccess.Read)

            Dim byteArray As Byte() = File.ReadAllBytes(pathToFile)

            fs.Close()
            fs.Dispose()

            Return byteArray
        End Function

    End Module
End Namespace
