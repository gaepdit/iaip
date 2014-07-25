Imports System.ComponentModel

Namespace Apb

    ''' <summary>
    ''' Basic information about a stationary source.
    ''' </summary>
    Public Structure Facility

        Shared Sub New()
        End Sub

        Public Sub New(ByVal airsNumber As String)
            Me.AirsNumber = airsNumber
        End Sub

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

        ''' <summary>
        ''' Facility Location is where the facility is actually located, distinct 
        ''' from a mailing address. Facility Location may not have a real 
        ''' postal address, but will have some of the elements of a postal address
        ''' </summary>
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

        Public Property HeaderData() As FacilityHeaderData
            Get
                Return _headerData
            End Get
            Set(ByVal value As FacilityHeaderData)
                _headerData = value
            End Set
        End Property
        Private _headerData As FacilityHeaderData











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
        Public Shared Function IsValidAirsNumber(ByVal airsNumber As String) As Boolean
            If airsNumber Is Nothing Then Return False
            ' Valid AIRS numbers are in the form 000-00000 or 04-13-000-0000
            ' (with or without the dashes)
            If airsNumber Is Nothing Then Return False
            Dim rgx As New System.Text.RegularExpressions.Regex("^(04-?13-?)?\d{3}-?\d{5}$")
            Return rgx.IsMatch(airsNumber)
        End Function

        ''' <summary>
        ''' Converts an AIRS number to an 8 or 12-character string with no dashes. A return value indicates whether the conversion succeeded.
        ''' </summary>
        ''' <param name="airsNumber">A string containing an AIRS number to convert.
        ''' When this method returns, contains the converted AIRS number, or the original string if the conversion failed. 
        ''' The conversion fails if the original string is not of the correct format for an AIRS number.
        ''' </param>
        ''' <param name="expand">Whether the string should be expanded to 12 characters or not. Default is false (8 characters).</param>
        ''' <returns>True if airsNumber was converted successfully; otherwise, false.</returns>
        Public Shared Function NormalizeAirsNumber(ByRef airsNumber As String, Optional ByVal expand As Boolean = False) As Boolean
            ' Converts a string representation of an AIRS number to the "00000000" form 
            ' (eight numerals, no dashes).
            '
            ' If 'expand' is True, then the AIRS number is expanded to the "041300000000"
            ' form (12 numerals, no dashes, beginning with "0413").
            '
            ' Return value indicates whether the conversion succeeded.

            ' First, validate the raw AIRS number.
            If airsNumber Is Nothing OrElse Not (IsValidAirsNumber(airsNumber)) Then Return False

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
        ''' Converts an AIRS number to its standard string representation, either "000-00000" (eight numerals, one dash), 
        ''' or "04-13-000-00000" (12 numerals, dashes added, beginning with "04-13").
        ''' </summary>
        ''' <param name="airsNumber">A string containing an AIRS number to convert.</param>
        ''' <param name="expand">Whether the string should be expanded to 12 digits or not. Default is false (8 digits).</param>
        ''' <returns>A formatted string representation of an AIRS number.</returns>
        ''' <remarks></remarks>
        Public Shared Function FormatAirsNumber(ByVal airsNumber As String, Optional ByVal expand As Boolean = False) As String
            If Not NormalizeAirsNumber(airsNumber, expand) Then Return Nothing
            If expand Then
                Return Mid(airsNumber, 1, 2) & "-" & Mid(airsNumber, 3, 2) & "-" & _
                    Mid(airsNumber, 5, 3) & "-" & Mid(airsNumber, 8, 5)
            Else
                Return Mid(airsNumber, 1, 3) & "-" & Mid(airsNumber, 4, 5)
            End If
        End Function

#End Region

#Region " Enums "

#Region " Operation "

        ''' <summary>
        ''' The operational status of a facility.
        ''' </summary>
        ''' <remarks>Stored in database as a single-character string.</remarks>
        Public Enum OperationalStatus
            Unspecified
            <Description("Operational")> O
            <Description("Planned")> P
            <Description("Under Construction")> C
            <Description("Temporarily Closed")> T
            <Description("Closed/Dismantled")> X
            <Description("Seasonal Operation")> I
        End Enum

        ''' <summary>
        ''' The source classification of a facility (based on permit type).
        ''' </summary>
        ''' <remarks>Stored in database as a two-character string.</remarks>
        Public Enum Classification
            Unspecified
            <Description("Major source")> A
            <Description("Minor source")> B
            <Description("Synthetic minor")> SM
            <Description("Permit by rule")> PR
            <Description("Unclassified")> C
        End Enum

#End Region

#Region " Nonattainment status "

        ''' <summary>
        ''' Specifies whether a facility is located within a one-hour ozone nonattainment area.
        ''' </summary>
        ''' <remarks>The value of each enumeration member is significant because the members are stored
        ''' and retrieved from the database in a coded string (along with EightHourNonattainmentStatus and
        ''' PMFineNonattainmentStatus.)</remarks>
        Public Enum OneHourNonattainmentStatus
            No = 0
            Yes = 1
            Contribute = 2
        End Enum

        ''' <summary>
        ''' Specifies whether a facility is located within an eight-hour ozone nonattainment area.
        ''' </summary>
        ''' <remarks>The value of each enumeration member is significant because the members are stored
        ''' and retrieved from the database in a coded string (along with OneHourNonattainmentStatus and
        ''' PMFineNonattainmentStatus.)</remarks>
        Public Enum EightHourNonattainmentStatus
            None = 0
            Atlanta = 1
            Macon = 2
        End Enum

        ''' <summary>
        ''' Specifies whether a facility is located within a PM Fine (PM 2.5) nonattainment area.
        ''' </summary>
        ''' <remarks>The value of each enumeration member is significant because the members are stored
        ''' and retrieved from the database in a coded string (along with EightHourNonattainmentStatus and
        ''' OneHourNonattainmentStatus.)</remarks>
        Public Enum PMFineNonattainmentStatus
            None = 0
            Atlanta = 1
            Chattanooga = 2
            Floyd = 3
            Macon = 4
        End Enum

#End Region

#Region " Program Codes "

        ''' <summary>
        ''' Bitwise flag for enumerating which air programs apply to a facility.
        ''' </summary>
        ''' <remarks>The enum value of the flags is significant because the flags are stored 
        ''' in the database as a (reversed) bitwise string. The string is 15 characters, but 
        ''' only the first 14 are used.</remarks>
        <Flags()> Public Enum AirPrograms
            None = 0
            <Description("SIP")> SIP = 1
            <Description("Federal SIP")> FederalSIP = 2
            <Description("Non-Federal SIP")> NonFederalSIP = 4
            <Description("CFC Tracking")> CfcTracking = 8
            <Description("PSD")> PSD = 16
            <Description("NSR")> NSR = 32
            <Description("NESHAP (Part 61)")> NESHAP = 64
            <Description("NSPS")> NSPS = 128
            <Description("FESOP")> FESOP = 256
            <Description("Acid Precipitation")> AcidPrecipitation = 512
            <Description("Native American")> NativeAmerican = 1024
            <Description("MACT (Part63)")> MACT = 2048
            <Description("Title V")> TitleV = 4096
            <Description("Risk Management Plan")> RMP = 8192
        End Enum

        ''' <summary>
        ''' Bitwise flag for enumerating which air program classifications apply to a facility.
        ''' </summary>
        ''' <remarks>The enum value of the flags is significant because the flags are stored 
        ''' in the database as a (reversed) bitwise string. The string is 5 characters, but 
        ''' only the first 2 are used.</remarks>
        <Flags()> Public Enum AirProgramClassifications
            None = 0
            <Description("NSR/PSD Major")> NsrMajor = 1
            <Description("HAPs Major")> HapMajor = 2
        End Enum

#End Region

#End Region

    End Structure

End Namespace
