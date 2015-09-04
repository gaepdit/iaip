Public Class Address
    ' Address is simply all the elements of a postal address

    Public Property Street() As String
    Public Property Street2() As String
    Public Property City() As String
    Public Property State() As String
    Public Property PostalCode() As String
    Public Property Country() As String

    Public Overrides Function ToString() As String
        Dim str As String() = {Me.City, Me.State}
        Dim cityState As String = ConcatNonEmptyStrings(", ", str)
        Dim zip As String = Me.PostalCode
        If zip IsNot Nothing AndAlso zip.Length = 9 AndAlso IsNumeric(zip) Then
            zip = zip.Insert(5, "-")
        End If
        Dim str2 As String() = {Me.Street, Me.Street2, cityState & " " & zip}
        Dim address As String = ConcatNonEmptyStrings(Constants.vbNewLine, str2)
        Return address
    End Function

    Public Function ToLinearString() As String
        Dim str As String() = {Me.City, Me.State}
        Dim cityState As String = ConcatNonEmptyStrings(", ", str)
        Dim zip As String = Me.PostalCode
        If zip.Length = 9 AndAlso IsNumeric(zip) Then
            zip = zip.Insert(5, "-")
        End If
        Dim str2 As String() = {Me.Street, Me.Street2, cityState & " " & zip}
        Dim address As String = ConcatNonEmptyStrings(", ", str2)
        Return address
    End Function

End Class
