Module BitFields

    ''' <summary>
    ''' Converts a string of zeros and ones (bit field) to a set of enumeration flags. 
    ''' The input bit field string can also be reversed (left-to-right).
    ''' </summary>
    ''' <typeparam name="TEnum">The enumeration type to return. The Enum should be defined with the FlagsAttribute.</typeparam>
    ''' <param name="bitField">The bit field string to convert to enumeration flags.</param>
    ''' <param name="reversed">Whether the input bit field string is reversed (i.e., read left-to-right).</param>
    ''' <returns>An enumeration of type TEnum with flags set.</returns>
    Public Function ConvertBitFieldToEnum(Of TEnum)(ByVal bitField As String, Optional ByVal reversed As Boolean = True) As TEnum
        Return ConvertIntegerToEnum(Of TEnum)(ConvertBitFieldToInteger(bitField, reversed))
    End Function

    Private Function ConvertBitFieldToInteger(ByVal bitField As String, Optional ByVal reversed As Boolean = True) As Int32
        ' bitFlags must contain only zeros and ones (and not be empty)
        If bitField Is Nothing OrElse Not System.Text.RegularExpressions.Regex.IsMatch(bitField, "^[0-1]+$") Then
            Return 0
        End If

        ' Database currently stores bitflags in reversed (left-to-right) order
        If reversed Then bitField = StrReverse(bitField)
        ' Convert the base-2 number to integer
        Return Convert.ToInt32(bitField, 2)
    End Function

    Private Function ConvertIntegerToEnum(Of TEnum)(ByVal i As Int32) As TEnum
        'If [Enum].IsDefined(GetType(TEnum), i) Then
        Return DirectCast([Enum].ToObject(GetType(TEnum), i), TEnum)
        'Else
        '    Return Nothing
        'End If
    End Function

    ''' <summary>
    ''' Converts a set of enumeration flags to a string of zeros and ones (i.e., a bit field). The 
    ''' bit field string can also be output reversed (left-to-right).
    ''' </summary>
    ''' <typeparam name="TEnum">The input enumeration type. The Enum should be defined with the FlagsAttribute.</typeparam>
    ''' <param name="enumeration">The enumeration to convert.</param>
    ''' <param name="totalWidth">The number of characters in the resulting string, equal to the number of original
    ''' characters padded with zeros.</param>
    ''' <param name="reverse">Whether the output string should be reversed (i.e., read left-to-right).</param>
    ''' <returns>A bit field string.</returns>
    Public Function ConvertEnumToBitFlags(Of TEnum)(ByVal enumeration As TEnum, ByVal totalWidth As Integer, Optional ByVal reverse As Boolean = True) As String
        Return ConvertIntegerToBitFlags(Convert.ToInt32(enumeration), totalWidth, reverse)
    End Function

    Private Function ConvertIntegerToBitFlags(ByVal i As Int32, ByVal totalWidth As Integer, Optional ByVal reverse As Boolean = True) As String
        Dim bfs As String = Convert.ToString(i, 2).PadLeft(totalWidth, "0"c)
        If reverse Then bfs = StrReverse(bfs)
        Return bfs
    End Function

End Module
