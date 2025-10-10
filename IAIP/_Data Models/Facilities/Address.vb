Public Class Address
    ' Address is simply all the elements of a postal address

    Public Property Street() As String
    Public Property Street2() As String
    Public Property City() As String
    Public Property State() As String
    Public Property PostalCode() As String
    Public Property Country() As String

    Public Overrides Function ToString() As String
        Return CompileAddressString(False)
    End Function

    Public Function ToLinearString() As String
        Return CompileAddressString(True)
    End Function

    Private Function CompileAddressString(linear As Boolean) As String
        Dim cityState As String = {Me.City, Me.State}.ConcatNonEmptyStrings(", ")
        Dim zip As String = FormatPostalCode(Me.PostalCode)
        If linear Then
            Return {Me.Street, Me.Street2, cityState & " " & zip}.ConcatNonEmptyStrings(", ")
        Else
            Return {Me.Street, Me.Street2, cityState & " " & zip}.ConcatNonEmptyStrings(vbNewLine)
        End If
    End Function

    Public Shared Function FormatPostalCode(postalCode As String) As String
        If postalCode IsNot Nothing AndAlso postalCode.Length = 9 AndAlso IsNumeric(postalCode) Then
            Return postalCode.Insert(5, "-")
        Else
            Return postalCode
        End If
    End Function

End Class
