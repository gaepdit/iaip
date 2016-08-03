Public Class BooleanFormatProvider
    Implements ICustomFormatter
    Implements IFormatProvider

    ' Don't forget to set the CellFormatting event on the datagridview/whatever

    Public Function GetFormat(formatType As Type) As Object _
    Implements IFormatProvider.GetFormat

        If formatType Is GetType(ICustomFormatter) Then
            Return Me
        End If

        Return Nothing
    End Function

    Public Enum BooleanFormatProviderFormat
        TrueFalse
        YesNo
        OnOff
    End Enum

    Public Function Format(booleanFormat As String, arg As Object, formatProvider As IFormatProvider) As String _
    Implements ICustomFormatter.Format

        If arg Is Nothing Then
            Return String.Empty
        End If

        Dim value As Boolean = CBool(arg)

        Select Case booleanFormat
            Case "YesNo"
                Return If(value, "Yes", "No")
            Case "OnOff"
                Return If(value, "On", "Off")
            Case Else
                Return value.ToString() 'true/false
        End Select

    End Function

End Class
