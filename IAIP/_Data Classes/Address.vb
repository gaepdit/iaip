Public Class Address
    ' Address is simply all the elements of a postal address
    Public Sub New()
    End Sub

    Public Sub New(ByVal street1 As String, ByVal city As String, ByVal state As String, _
                   Optional ByVal street2 As String = "", Optional ByVal postalCode As String = "", _
                   Optional ByVal country As String = "")
        Me.Street = street1
        Me.City = city
        Me.State = state
        Me.Street2 = street2
        Me.PostalCode = postalCode
        Me.Country = country
    End Sub

    Public Property Street() As String
        Get
            Return _street
        End Get
        Set(ByVal value As String)
            _street = value
        End Set
    End Property
    Private _street As String
    Public Property Street2() As String
        Get
            Return _street2
        End Get
        Set(ByVal value As String)
            _street2 = value
        End Set
    End Property
    Private _street2 As String
    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property
    Private _city As String
    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
        End Set
    End Property
    Private _state As String
    Public Property PostalCode() As String
        Get
            Return _postalCode
        End Get
        Set(ByVal value As String)
            _postalCode = value
        End Set
    End Property
    Private _postalCode As String
    Public Property Country() As String
        Get
            Return _country
        End Get
        Set(ByVal value As String)
            _country = value
        End Set
    End Property
    Private _country As String

    Public Overrides Function ToString() As String
        Dim str As String() = {Me.City, Me.State}
        Dim cityState As String = ConcatNonEmptyStrings(", ", str)
        Dim str2 As String() = {Me.Street, Me.Street2, cityState & " " & Me.PostalCode}
        Dim address As String = ConcatNonEmptyStrings(Constants.vbNewLine, str2)
        Return address
    End Function

    Public Function ToLinearString() As String
        Dim str As String() = {Me.City, Me.State}
        Dim cityState As String = ConcatNonEmptyStrings(", ", str)
        Dim str2 As String() = {Me.Street, Me.Street2, cityState & " " & Me.PostalCode}
        Dim address As String = ConcatNonEmptyStrings(", ", str2)
        Return address
    End Function

End Class

Public Class Location
    ' Location is a postal address + lat/lon and county

    Public Property Address() As Address
        Get
            Return _address
        End Get
        Set(ByVal value As Address)
            _address = value
        End Set
    End Property
    Private _address As Address
    Public Property Latitude() As Nullable(Of Decimal)
        Get
            Return _latitude
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            _latitude = value
        End Set
    End Property
    Private _latitude As Nullable(Of Decimal)
    Public Property Longitude() As Nullable(Of Decimal)
        Get
            Return _longitude
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            _longitude = value
        End Set
    End Property
    Private _longitude As Nullable(Of Decimal)
    Public Property County() As String
        Get
            Return _county
        End Get
        Set(ByVal value As String)
            _county = value
        End Set
    End Property
    Private _county As String
End Class
