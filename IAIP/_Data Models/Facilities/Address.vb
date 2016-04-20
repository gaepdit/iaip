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
        Dim cityState As String = ConcatNonEmptyStrings(", ", {Me.City, Me.State})
        Dim zip As String = Me.PostalCode
        If zip IsNot Nothing AndAlso zip.Length = 9 AndAlso IsNumeric(zip) Then
            zip = zip.Insert(5, "-")
        End If
        If linear Then
            Return ConcatNonEmptyStrings(", ", {Me.Street, Me.Street2, cityState & " " & zip})
        Else
            Return ConcatNonEmptyStrings(Constants.vbNewLine, {Me.Street, Me.Street2, cityState & " " & zip})
        End If
    End Function

End Class
