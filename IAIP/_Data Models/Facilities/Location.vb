Public Class Location
    ' Location is a postal address + lat/long and county

    Public Property Address() As Address
    Public Property Latitude() As Decimal?
    Public Property Longitude() As Decimal?
    Public Property County() As String
End Class
