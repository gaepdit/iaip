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

        Public ReadOnly Property AirsNumberFormatted() As String
            Get
                Return FormatAirsNumber(AirsNumber)
            End Get
        End Property

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

        Public Shared Function IsAirsNumberValid(ByVal airsNumber As String) As Boolean
            ' Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000
            ' (with or without the dashes)

            ' Remove dashes and spaces (the only non-numeral characters allowed)
            Dim a As String = airsNumber.Replace("-", "").Replace(" ", "")

            ' Test to see if remaining string can be parsed as an integer
            ' (i.e., only numerals remain)
            If Not (Int64.TryParse(a, Nothing)) Then _
                Return False
            If Not (a.Length = 8 Or a.Length = 12) Then _
                Return False
            If (a.Length = 12 And Mid(a, 1, 4) <> "0413") Then _
                Return False

            ' No red flags? Give a green light (to mix metaphors)
            Return True
        End Function

        Public Shared Function NormalizeAirsNumber(ByRef airsNumber As String, Optional ByVal expand As Boolean = False) As Boolean
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").
            '
            ' Return value indicates whether the conversion succeeded.

            ' First, validate the raw AIRS number
            If Not IsAirsNumberValid(airsNumber) Then Return False

            ' If okay, then remove spaces and dashes
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")

            If expand Then
                ' Expand the short form to the long form
                If airsNumber.Length = 8 Then airsNumber = "0413" & airsNumber
            Else
                ' Contract the long form to the short form
                If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)
            End If

            Return True
        End Function

        Public Shared Function FormatAirsNumber(ByVal airsNumber As String, Optional ByVal expand As Boolean = False) As String
            ' Converts a string representation of an AIRS number to the "000-00000" form 
            ' (eight numerals, one dash).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "04-13-000-00000"
            ' form (12 numerals, dashes added, beginning with "04-13").

            If Not NormalizeAirsNumber(airsNumber, expand) Then Return Nothing
            If expand Then
                Return Mid(airsNumber, 1, 2) & "-" & Mid(airsNumber, 3, 2) & "-" & _
                    Mid(airsNumber, 5, 3) & "-" & Mid(airsNumber, 8, 5)
            Else
                Return Mid(airsNumber, 1, 3) & "-" & Mid(airsNumber, 4, 5)
            End If
        End Function

    End Class

End Namespace
