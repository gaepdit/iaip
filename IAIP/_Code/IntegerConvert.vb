Public Module IntegerConvert

    ''' <summary>
    ''' Utility for storing zeroed integers as null in the database
    ''' </summary>
    ''' <param name="i">The integer to store</param>
    ''' <returns>Nothing if i is equal to 0; otherwise, returns i.</returns>
    Public Function StoreNothingIfZero(i As Integer) As Integer?
        If i = 0 Then
            Return Nothing
        Else
            Return i
        End If
    End Function

    ''' <summary>
    ''' Utility for storing zeroed decimals as null in the database
    ''' </summary>
    ''' <param name="i">The decimal to store</param>
    ''' <returns>Nothing if i is equal to 0; otherwise, returns i.</returns>
    Public Function StoreNothingIfZero(i As Decimal) As Decimal?
        If i = 0 Then
            Return Nothing
        Else
            Return i
        End If
    End Function

End Module
