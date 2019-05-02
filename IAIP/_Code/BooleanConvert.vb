Public Module BooleanConvert

    ''' <summary>
    ''' An enumeration of the different ways pseudo-boolean values are stored in the database.
    ''' </summary>
    Public Enum BooleanDBConversionType
        OneOrZero
    End Enum

    ''' <summary>
    ''' Converts a database value to a boolean according to the specified DB conversion type.
    ''' </summary>
    ''' <param name="value">The database value to convert.</param>
    ''' <param name="conversionType">A BooleanDBConversionType indicating how the value is stored in the database.</param>
    ''' <returns>A string that can be stored in the database.</returns>
    Public Function ConvertDBValueToBoolean(value As String, conversionType As BooleanDBConversionType) As Boolean
        Select Case conversionType
            Case BooleanDBConversionType.OneOrZero
                Return Convert.ToBoolean(Integer.Parse(value))
            Case Else
                Throw New ArgumentException("Incorrect conversion type specified.", NameOf(conversionType))
        End Select
    End Function

    ''' <summary>
    ''' Converts a boolean value to a string that can be stored in the database according to the specified DB conversion type.
    ''' </summary>
    ''' <param name="value">The boolean value to convert.</param>
    ''' <param name="conversionType">A BooleanDBConversionType indicating how the value should be stored in the database.</param>
    ''' <returns>A string that can be stored in the database.</returns>
    Public Function ConvertBooleanToDBValue(value As Boolean, conversionType As BooleanDBConversionType) As String
        Select Case conversionType
            Case BooleanDBConversionType.OneOrZero
                If value Then
                    Return "1"
                Else
                    Return "0"
                End If
            Case Else
                Throw New ArgumentException("Incorrect conversion type specified.", NameOf(conversionType))
        End Select
    End Function

End Module
