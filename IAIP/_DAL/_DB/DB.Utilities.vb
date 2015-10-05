Imports System.Collections.Generic
Imports System.IO

Namespace DB
    Module Utilities

        <DebuggerStepThrough()> _
        Public Function GetNullable(Of T)(ByVal obj As Object) As T
            ' http://stackoverflow.com/a/870771/212978
            ' http://stackoverflow.com/a/9953399/212978
            If obj Is Nothing OrElse IsDBNull(obj) OrElse obj.ToString = "null" Then
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

        Public Function AddBlankRowToList(ByRef l As List(Of String), Optional ByVal blankPrompt As String = "") As List(Of String)
            l.Insert(0, blankPrompt)
            Return l
        End Function

        Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Return File.ReadAllBytes(pathToFile)
        End Function

        Public Function DumbConvertToBoolean(value As String, conversionType As DumbConvertBooleanType) As Boolean
            Select Case conversionType
                Case DumbConvertBooleanType.TrueOrDBNull
                    If value Is Nothing OrElse IsDBNull(value) OrElse value.ToString = "null" Then
                        Return False
                    Else
                        Return Boolean.Parse(value) ' Will throw an exception if value is not equal to Boolean.TrueString
                    End If
            End Select

            ' Fallback
            Return Boolean.Parse(value)
        End Function

        Public Function DumbConvertFromBoolean(value As Boolean, conversionType As DumbConvertBooleanType) As String
            Select Case conversionType
                Case DumbConvertBooleanType.TrueOrDBNull
                    If value Then
                        Return Boolean.TrueString
                    Else
                        Return Nothing
                    End If
            End Select

            ' Fallback
            Return value.ToString
        End Function

        Public Enum DumbConvertBooleanType
            TrueOrDBNull
        End Enum

    End Module
End Namespace
