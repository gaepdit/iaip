Imports System.Collections.Generic
Imports System.IO

Namespace DB
    Module Utilities

        ''' <summary>
        ''' Generic function for converting database values to useable .NET values, handling DBNull values appropriately.
        ''' </summary>
        ''' <typeparam name="T">The expected data type.</typeparam>
        ''' <param name="obj">The database value to convert.</param>
        ''' <returns>If database value is DBNull, returns the default value for the requested data type; otherwise, returns the value unchanged.</returns>
        <DebuggerStepThrough()>
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

        ''' <summary>
        ''' Converts a database value to a nullable DateTime object, handling DBNull values appropriately.
        ''' </summary>
        ''' <param name="obj">The database value to convert.</param>
        ''' <returns>If database value is DBNull or value cannot be converted to a DateTime, returns Nothing; otherwise, returns the value converted to a DateTime.</returns>
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

        ''' <summary>
        ''' Reads a binary file into a Byte array.
        ''' </summary>
        ''' <param name="pathToFile">The path to the file to read.</param>
        ''' <returns>A Byte array.</returns>
        Public Function ReadByteArrayFromFile(ByVal pathToFile As String) As Byte()
            Return File.ReadAllBytes(pathToFile)
        End Function

        ''' <summary>
        ''' Converts a database value to a boolean according to the specified DB conversion type.
        ''' </summary>
        ''' <param name="value">The database value to convert.</param>
        ''' <param name="conversionType">A BooleanDBConversionType indicating how the value is stored in the database.</param>
        ''' <returns>A string that can be stored in the database.</returns>
        Public Function ConvertDBValueToBoolean(value As String, conversionType As BooleanDBConversionType) As Boolean
            Select Case conversionType
                Case BooleanDBConversionType.TrueOrDBNull
                    If value Is Nothing OrElse IsDBNull(value) OrElse value.ToString = "null" Then
                        Return False
                    Else
                        Return Boolean.Parse(value) ' Will throw an exception if value is not equal to Boolean.TrueString
                    End If
            End Select

            ' Fallback
            Return Boolean.Parse(value)
        End Function

        ''' <summary>
        ''' Converts a boolean value to a string that can be stored in the database according to the specified DB conversion type.
        ''' </summary>
        ''' <param name="value">The boolean value to convert.</param>
        ''' <param name="conversionType">A BooleanDBConversionType indicating how the value should be stored in the database.</param>
        ''' <returns>A string that can be stored in the database.</returns>
        Public Function ConvertBooleanToDBValue(value As Boolean, conversionType As BooleanDBConversionType) As String
            Select Case conversionType
                Case BooleanDBConversionType.TrueOrDBNull
                    If value Then
                        Return Boolean.TrueString
                    Else
                        Return Nothing
                    End If
            End Select

            ' Fallback
            Return value.ToString
        End Function

        ''' <summary>
        ''' An enumeration of the different ways pseud-boolean values are stored in the database.
        ''' </summary>
        Public Enum BooleanDBConversionType
            TrueOrDBNull
        End Enum

    End Module
End Namespace
