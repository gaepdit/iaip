Imports System.ComponentModel
Imports Iaip.Apb.Facility

Namespace Apb

    ''' <summary>
    ''' Facility Header Data includes program-related data for a facility, such as classification, 
    ''' operating status, and air program codes. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FacilityHeaderData

#Region " Constructors "

        Public Sub New()
        End Sub

        Public Sub New(ByVal airsNumber As String)
            Me.AirsNumber = airsNumber
        End Sub

        Public Sub New(ByVal airsNumber As Apb.ApbFacilityId)
            Me.AirsNumber = airsNumber
        End Sub

#End Region

#Region " Properties "

#Region " Standard "

        Public Property AirsNumber() As ApbFacilityId
            Get
                Return _airsNumber
            End Get
            Set(ByVal value As ApbFacilityId)
                _airsNumber = value
            End Set
        End Property
        Private _airsNumber As ApbFacilityId

        Public Property SicCode() As String
            Get
                Return _sic
            End Get
            Set(ByVal value As String)
                _sic = NothingifyEmptyString(value)
            End Set
        End Property
        Private _sic As String

        Public Property StartupDate() As Date?
            Get
                Return _startupDate
            End Get
            Set(ByVal value As Date?)
                _startupDate = value
            End Set
        End Property
        Private _startupDate As Date?

        Public Property ShutdownDate() As Date?
            Get
                Return _shutdownDate
            End Get
            Set(ByVal value As Date?)
                _shutdownDate = value
            End Set
        End Property
        Private _shutdownDate As Date?

        Public Property Naics() As String
            Get
                Return _naics
            End Get
            Set(ByVal value As String)
                _naics = NothingifyEmptyString(value)
            End Set
        End Property
        Private _naics As String

        Public Property RmpId() As String
            Get
                Return _rmpId
            End Get
            Set(ByVal value As String)
                If ValidRmpId(value) Then
                    _rmpId = value
                Else
                    _rmpId = Nothing
                End If
            End Set
        End Property
        Private _rmpId As String

        Public Property FacilityDescription() As String
            Get
                Return _facilityDescription
            End Get
            Set(ByVal value As String)
                _facilityDescription = NothingifyEmptyString(value)
            End Set
        End Property
        Private _facilityDescription As String

        Public Property HeaderUpdateComment() As String
            Get
                Return _headerUpdateComment
            End Get
            Set(ByVal value As String)
                _headerUpdateComment = NothingifyEmptyString(value)
            End Set
        End Property
        Private _headerUpdateComment As String

        Public Property DateDataModified() As Date?
            Get
                Return _DateDataModified
            End Get
            Set(ByVal value As Date?)
                _DateDataModified = value
            End Set
        End Property
        Private _DateDataModified As Date?

        Public Property WhoModified() As String
            Get
                Return _WhoModified
            End Get
            Set(ByVal value As String)
                _WhoModified = value
            End Set
        End Property
        Private _WhoModified As String

        Public Property WhereModified() As String
            Get
                Return _WhereModified
            End Get
            Set(ByVal value As String)
                _WhereModified = NothingifyEmptyString(value)
            End Set
        End Property
        Private _WhereModified As String

#End Region

#Region " Operational Status "

        Public Property OperationalStatus() As OperationalStatus
            Get
                Return _operationalStatus
            End Get
            Set(ByVal value As OperationalStatus)
                _operationalStatus = value
            End Set
        End Property
        Private _operationalStatus As OperationalStatus

        ''' <summary>
        ''' A single-character string representing the operational status. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode operational status. Stored in database as a single-character string.</remarks>
        Public Property OperationalStatusCode() As String
            Get
                Return _operationalStatus.ToString()
            End Get
            Set(ByVal value As String)
                _operationalStatus = [Enum].Parse(GetType(OperationalStatus), value)
            End Set
        End Property

        Public ReadOnly Property OperationalStatusDescription() As String
            Get
                Return _operationalStatus.GetDescription
            End Get
        End Property

#End Region

#Region " Classification "

        Public Property Classification() As Classification
            Get
                Return _classification
            End Get
            Set(ByVal value As Classification)
                _classification = value
            End Set
        End Property
        Private _classification As Classification

        ''' <summary>
        ''' A single-character string representing the facility classification. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a single-character string.</remarks>
        Public Property ClassificationCode() As String
            Get
                Return _classification.ToString()
            End Get
            Set(ByVal value As String)
                _classification = [Enum].Parse(GetType(Classification), value)
            End Set
        End Property

        Public ReadOnly Property ClassificationDescription() As String
            Get
                Return _classification.GetDescription()
            End Get
        End Property

#End Region

#Region " Nonattainment statuses "

        Private _oneHourOzone As OneHourNonattainmentStatus
        Public Property OneHourNonAttainmentState() As OneHourNonattainmentStatus
            Get
                Return _oneHourOzone
            End Get
            Set(ByVal value As OneHourNonattainmentStatus)
                _oneHourOzone = value
            End Set
        End Property

        Private _eightHourOzone As EightHourNonattainmentStatus
        Public Property EightHourNonAttainmentState() As EightHourNonattainmentStatus
            Get
                Return _eightHourOzone
            End Get
            Set(ByVal value As EightHourNonattainmentStatus)
                _eightHourOzone = value
            End Set
        End Property

        Private _pmFine As PMFineNonattainmentStatus
        Public Property PMFineNonAttainmentState() As PMFineNonattainmentStatus
            Get
                Return _pmFine
            End Get
            Set(ByVal value As PMFineNonattainmentStatus)
                _pmFine = value
            End Set
        End Property

        ''' <summary>
        ''' A five-character string representing all nonattainment area statuses. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A five-character encoded string.</value>
        ''' <returns>A five-character encoded string.</returns>
        ''' <remarks>Used to encode OneHourNonAttainmentState, EightHourNonAttainmentState, 
        ''' and PMFineNonAttainmentState. Stored in database as a five-character coded string. 
        ''' Remarkably, only the middle three characters are used.</remarks>
        Public Property NonattainmentStatusesCode() As String
            Get
                Return "0" & _
                OneHourNonAttainmentState.ToString("D") & _
                EightHourNonAttainmentState.ToString("D") & _
                PMFineNonAttainmentState.ToString("D") & _
                "0"
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) Then
                    OneHourNonAttainmentState = Facility.OneHourNonattainmentStatus.No
                    EightHourNonAttainmentState = Facility.EightHourNonattainmentStatus.None
                    PMFineNonAttainmentState = Facility.PMFineNonattainmentStatus.None
                Else
                    OneHourNonAttainmentState = Mid(value, 2, 1)
                    EightHourNonAttainmentState = Mid(value, 3, 1)
                    PMFineNonAttainmentState = Mid(value, 4, 1)
                End If
            End Set
        End Property

#End Region

#Region " Program Codes "

        Private _airPrograms As AirProgram
        Public Property AirPrograms() As AirProgram
            Get
                Return _airPrograms
            End Get
            Set(ByVal value As AirProgram)
                _airPrograms = value
            End Set
        End Property

        ''' <summary>
        ''' A 15-character string representing all air programs a facility is subjec to. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A 15-character encoded string.</value>
        ''' <returns>A 15-character encoded string.</returns>
        ''' <remarks>Used to encode the Air Programs a facility may be subject to. 
        ''' Stored in database as a 15-character coded string, but only the first 14 are used.</remarks>
        Public Property AirProgramsCode() As String
            Get
                Return ConvertEnumToBitFlags(Of AirProgram)(Me.AirPrograms, AirProgramsCodeLength)
            End Get
            Set(ByVal value As String)
                _airPrograms = ConvertBitFieldToEnum(Of AirProgram)(value)
            End Set
        End Property
        Private Shared AirProgramsCodeLength As Integer = 15

        Private _airProgramClassifications As AirProgramClassifications
        Public Property AirProgramClassifications() As AirProgramClassifications
            Get
                Return _airProgramClassifications
            End Get
            Set(ByVal value As AirProgramClassifications)
                _airProgramClassifications = value
            End Set
        End Property

        ''' <summary>
        ''' A five-character string representing a facility's air program classifications. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A five-character encoded string.</value>
        ''' <returns>A five-character encoded string.</returns>
        ''' <remarks>Used to encode a facility's air program classifications. 
        ''' Stored in database as a five-character coded string, but only the first two are used.</remarks>
        Public Property AirProgramClassificationsCode() As String
            Get
                Return ConvertEnumToBitFlags(Of AirProgramClassifications)(Me.AirProgramClassifications, AirProgramClassificationsCodeLength)
            End Get
            Set(ByVal value As String)
                _airProgramClassifications = ConvertBitFieldToEnum(Of AirProgramClassifications)(value)
            End Set
        End Property
        Private Shared AirProgramClassificationsCodeLength As Integer = 5

#End Region

#End Region ' End Properties

#Region " Shared Functions "

        ''' <summary>
        ''' Tests whether a given string represents a valid RMP ID in format
        ''' (not whether the RMP ID is actually in use).
        ''' </summary>
        ''' <param name="rmpID">The string to test</param>
        ''' <returns>True if test string is in the format of a valid RMP ID. Otherwise, false.</returns>
        Public Shared Function ValidRmpId(ByVal rmpID As String) As Boolean
            If rmpID Is Nothing Then Return False

            ' Valid RMP IDs are in the form 0000-0000-0000 (with the dashes)
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{4}-\d{4}-\d{4}$")
            Return rgx.IsMatch(rmpID)
        End Function

#End Region

#Region " Enums "

        ''' <summary>
        ''' The action or user interface that initiates a change in facility header data
        ''' </summary>
        ''' <remarks>Stored in database as a numeric key.</remarks>
        Public Enum ModificationLocation
            Unspecified
            <Description("Permitting Action")> PermittingAction = 1
            <Description("Facility Header Editor")> HeaderDataEditor = 2
            <Description("SSCP Shutdown Notification")> SscpNotification = 3
            <Description("IAIP Facility Creation Tool")> FacilityCreationTool = 4
        End Enum

        ''' <summary>
        ''' Compliance Status codes
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum PollutantComplianceStatus
            <Description("Unknown compliance status")> UnknownComplianceStatus = 0
            <Description("Violation, Procedural and emissions")> ViolationProceduralAndEmissions
            <Description("Violation, No schedule")> ViolationNoSchedule
            <Description("Violation, Not meeting schedule")> ViolationNotMeetingSchedule
            <Description("Violation, Procedural")> ViolationProcedural
            <Description("Violation, Meeting compliance schedule")> ViolationMeetingComplianceSchedule
            <Description("Violation, No applicable State reg")> ViolationNoStateReg
            <Description("In Compliance, Source test")> ComplianceSourceTest
            <Description("In Compliance, Inspection")> ComplianceInspection
            <Description("In Compliance, Certification")> ComplianceCertification
            <Description("In Compliance, Shut down")> ComplianceShutDown
            <Description("In Compliance, Procedural")> ComplianceProcedural
            <Description("In Compliance, CEMS data")> ComplianceCems
        End Enum

#End Region

    End Class

End Namespace