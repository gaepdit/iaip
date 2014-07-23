﻿Namespace Apb

    Public Class Facility
        ' This is the fundamental class for a stationary source. 

#Region " Properties "

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

        Public Property FacilityName() As String
            Get
                Return _facilityName
            End Get
            Set(ByVal value As String)
                _facilityName = value
            End Set
        End Property
        Private _facilityName As String

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

        Private _subjectToNsps As Boolean?
        Public Property SubjectToNsps() As Boolean?
            Get
                Return _subjectToNsps
            End Get
            Set(ByVal value As Boolean?)
                _subjectToNsps = value
            End Set
        End Property

        Private _subjectToPart70 As Boolean?
        Public Property SubjectToPart70() As Boolean?
            Get
                Return _subjectToPart70
            End Get
            Set(ByVal value As Boolean?)
                _subjectToPart70 = value
            End Set
        End Property

        Private _comment As String
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property

#End Region

#Region "Shared Functions"

        ''' <summary>
        ''' Determines whether a string is in the format of a valid AIRS number.
        ''' </summary>
        ''' <param name="airsNumber">The string to test</param>
        ''' <returns>True if airsNumber is valid; otherwise, false.</returns>
        ''' <remarks>Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000 (with or without the dashes)</remarks>
        Public Shared Function IsAirsNumberValid(ByVal airsNumber As String) As Boolean
            ' Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000
            ' (with or without the dashes)

            Dim rgx As New System.Text.RegularExpressions.Regex("^(04-?13-?)?\d{3}-?\d{5}$")
            Return rgx.IsMatch(airsNumber.Replace(" ", ""))
        End Function

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "00000000" form. If 'expand' is True, then 
        ''' the AIRS number is expanded to the "041300000000" form. A return value indicates whether the conversion 
        ''' succeeded.
        ''' </summary>
        ''' <param name="airsNumber">A string containing the AIRS number to convert. When this method returns, contains
        ''' the formatted AIRS number if the conversion succeeded, or the original string if the conversion failed.</param>
        ''' <param name="expand">Whether to expand to the 12-digit form.</param>
        ''' <returns>true if airsNumber was converted successfully; otherwise, false.</returns>
        Public Shared Function NormalizeAirsNumber(ByRef airsNumber As String, Optional ByVal expand As Boolean = False) As Boolean
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").
            '
            ' Return value indicates whether the conversion succeeded.

            ' First, validate the raw AIRS number.
            If airsNumber Is Nothing OrElse Not (IsAirsNumberValid(airsNumber)) Then Return False

            ' If okay, then convert.
            airsNumber = GetNormalizedAirsNumber(airsNumber, expand)
            Return True
        End Function

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "00000000" form. If 'expand' is True, then 
        ''' the AIRS number is expanded to the "041300000000" form.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to convert.</param>
        ''' <param name="expand">Whether to expand to the 12-digit form.</param>
        ''' <returns>A string representation of an AIRS number in the "00000000" or "041300000000" form.</returns>
        Public Shared Function GetNormalizedAirsNumber(ByVal airsNumber As String, Optional ByVal expand As Boolean = False) As String
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").

            ' Remove spaces and dashes.
            airsNumber = airsNumber.Replace("-", "").Replace(" ", "")

            If expand Then
                ' Expand the short form to the long form
                If airsNumber.Length = 8 Then airsNumber = "0413" & airsNumber
            Else
                ' Contract the long form to the short form
                If airsNumber.Length = 12 Then airsNumber = airsNumber.Remove(0, 4)
            End If

            Return airsNumber
        End Function

        ''' <summary>
        ''' Converts a string representation of an AIRS number to the "000-00000" form. If expand = True, then 
        ''' the AIRS number is expanded to the "04-13-000-00000" form.
        ''' </summary>
        ''' <param name="airsNumber">The AIRS number to format</param>
        ''' <param name="expand">Whether to expand to the 12-digit form</param>
        ''' <returns>A string representation of an AIRS number in the "000-00000" or "04-13-000-00000" form.</returns>
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

#End Region

    End Class

End Namespace
