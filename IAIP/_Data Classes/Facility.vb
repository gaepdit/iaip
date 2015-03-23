Imports System.ComponentModel

Namespace Apb

    ''' <summary>
    ''' Basic information about a stationary source.
    ''' </summary>
    Public Class Facility

#Region " Constructors "
        Public Sub New()
        End Sub

        Public Sub New(ByVal airsNumber As String)
            Me.AirsNumber = airsNumber
        End Sub

        Public Sub New(ByVal airsNumber As ApbFacilityId)
            Me.AirsNumber = airsNumber
        End Sub
#End Region

#Region " Properties "

        Public Property AirsNumber() As ApbFacilityId
            Get
                Return _airsNumber
            End Get
            Set(ByVal value As ApbFacilityId)
                _airsNumber = value
            End Set
        End Property
        Private _airsNumber As ApbFacilityId

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

#Region " Public shared functions "

        Public Shared Function SanitizeFacilityNameForDb(ByVal name As String) As String
            If String.IsNullOrEmpty(name) Then
                Return Nothing
            End If

            Dim sanitizedName As New System.Text.StringBuilder(name)
            sanitizedName.Replace("[", "(").Replace("]", ")")

            Return sanitizedName.ToString
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
        <Flags()> Public Enum AirProgram
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

        Public Shared Function GetAirProgramDbKey(ByVal airProgram As AirProgram) As String
            Select Case airProgram
                Case Facility.AirProgram.AcidPrecipitation
                    Return "A"
                Case Facility.AirProgram.CfcTracking
                    Return "4"
                Case Facility.AirProgram.FederalSIP
                    Return "1"
                Case Facility.AirProgram.FESOP
                    Return "F"
                Case Facility.AirProgram.MACT
                    Return "M"
                Case Facility.AirProgram.NativeAmerican
                    Return "I"
                Case Facility.AirProgram.NESHAP
                    Return "8"
                Case Facility.AirProgram.NonFederalSIP
                    Return "3"
                Case Facility.AirProgram.NSPS
                    Return "9"
                Case Facility.AirProgram.NSR
                    Return "7"
                Case Facility.AirProgram.PSD
                    Return "6"
                Case Facility.AirProgram.RMP
                    Return "R"
                Case Facility.AirProgram.SIP
                    Return "0"
                Case Facility.AirProgram.TitleV
                    Return "V"
                Case Else
                    Return ""
            End Select
        End Function

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

    End Class

End Namespace
