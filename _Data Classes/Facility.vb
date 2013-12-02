Namespace Apb

    Public Class Facility
        ' This is the fundamental class for a stationary source. Currently, only
        ' used for displaying data already in the database, or moving data from
        ' form to form or report. In the future... maybe also creating/editing
        ' data?

        Public Property AirsNumber() As String
            Get
                Return _airsNumber
            End Get
            Set(ByVal value As String)
                If NormalizeAirsNumber(value) Then
                    _airsNumber = value
                Else
                    _airsNumber = Nothing
                End If
            End Set
        End Property
        Private _airsNumber As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Private _name As String

        ' Facility Location is where the facility is actually located,
        ' distinct from a mailing address. Facility Location may not
        ' have a real postal address, but will have some of the elements
        ' of a postal address
        Public Property FacilityLocation() As Location
            Get
                Return _facilityLocation
            End Get
            Set(ByVal value As Location)
                _facilityLocation = value
            End Set
        End Property
        Private _facilityLocation As Location
        Public Property MailingAddress() As Address
            Get
                Return _mailingAddress
            End Get
            Set(ByVal value As Address)
                _mailingAddress = value
            End Set
        End Property
        Private _mailingAddress As Address
        Public Property OperationalStatus() As String
            Get
                Return _operationalStatus
            End Get
            Set(ByVal value As String)
                _operationalStatus = value
            End Set
        End Property
        Private _operationalStatus As String
        Public Property Classification() As String
            Get
                Return _classification
            End Get
            Set(ByVal value As String)
                _classification = value
            End Set
        End Property
        Private _classification As String
        Public Property Sic() As String
            Get
                Return _sic
            End Get
            Set(ByVal value As String)
                _sic = value
            End Set
        End Property
        Private _sic As String
        Public Property Fein() As String
            Get
                Return _fein
            End Get
            Set(ByVal value As String)
                _fein = value
            End Set
        End Property
        Private _fein As String
        Public Property DistrictOffice() As String
            Get
                Return _districtOffice
            End Get
            Set(ByVal value As String)
                _districtOffice = value
            End Set
        End Property
        Private _districtOffice As String
        Public Property StartupDate() As Nullable(Of System.DateTime)
            Get
                Return _startupDate
            End Get
            Set(ByVal value As Nullable(Of System.DateTime))
                _startupDate = value
            End Set
        End Property
        Private _startupDate As Nullable(Of System.DateTime)
        Public Property ShutdownDate() As Nullable(Of System.DateTime)
            Get
                Return _shutdownDate
            End Get
            Set(ByVal value As Nullable(Of System.DateTime))
                _shutdownDate = value
            End Set
        End Property
        Private _shutdownDate As Nullable(Of System.DateTime)
        Public Property CmsStatus() As String
            Get
                Return _cmsStatus
            End Get
            Set(ByVal value As String)
                _cmsStatus = value
            End Set
        End Property
        Private _cmsStatus As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
        Private _description As String
        Public Property Naics() As String
            Get
                Return _naics
            End Get
            Set(ByVal value As String)
                _naics = value
            End Set
        End Property
        Private _naics As String
        Public Property RmpId() As String
            Get
                Return _rmpId
            End Get
            Set(ByVal value As String)
                _rmpId = value
            End Set
        End Property
        Private _rmpId As String

    End Class

End Namespace
