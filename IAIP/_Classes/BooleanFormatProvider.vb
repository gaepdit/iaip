Public Class BooleanFormatProvider
    Implements ICustomFormatter
    Implements IFormatProvider

    Public Function GetFormat(ByVal formatType As Type) As Object _
    Implements System.IFormatProvider.GetFormat

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

    Public Function Format(ByVal format__1 As String, ByVal arg As Object, ByVal formatProvider As IFormatProvider) As String _
    Implements System.ICustomFormatter.Format

        If arg Is Nothing Then
            Return String.Empty
        End If

        Dim value As Boolean = CBool(arg)

        Select Case If(format__1, String.Empty)
            Case "YesNo"
                Return If(value, "Yes", "No")
            Case "OnOff"
                Return If(value, "On", "Off")
            Case Else
                Return value.ToString() 'true/false
        End Select

    End Function

End Class
