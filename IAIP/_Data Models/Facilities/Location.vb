Public Class Location
    ' Location is a postal address + lat/lon and county

    Public Property Address() As Address
    Public Property Latitude() As Nullable(Of Decimal)
    Public Property Longitude() As Nullable(Of Decimal)
    Public Property County() As String
End Class
