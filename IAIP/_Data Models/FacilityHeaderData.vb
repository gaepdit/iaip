Imports System.ComponentModel
Imports Iaip.Apb.Facilities.Facility

Namespace Apb.Facilities

    ''' <summary>
    ''' Facility Header Data includes program-related data for a facility, such as classification, 
    ''' operating status, and air program codes. 
    ''' </summary>
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
                _sic = RealStringOrNothing(value)
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
                _naics = RealStringOrNothing(value)
            End Set
        End Property
        Private _naics As String

        Public Property RmpId() As String
            Get
                Return _rmpId
            End Get
            Set(ByVal value As String)
                If IsValidRmpId(value) Then
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
                _facilityDescription = RealStringOrNothing(value)
            End Set
        End Property
        Private _facilityDescription As String

        Public Property HeaderUpdateComment() As String
            Get
                Return _headerUpdateComment
            End Get
            Set(ByVal value As String)
                _headerUpdateComment = RealStringOrNothing(value)
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

        Public Property WhereModified() As HeaderDataModificationLocation
            Get
                Return _whereModified
            End Get
            Set(ByVal value As HeaderDataModificationLocation)
                _whereModified = value
            End Set
        End Property
        Private _whereModified As HeaderDataModificationLocation
        Public Property WhereModifiedCode() As String
            Get
                Return _whereModified.ToString
            End Get
            Set(ByVal value As String)
                _whereModified = [Enum].Parse(GetType(HeaderDataModificationLocation), value)
            End Set
        End Property
        Public ReadOnly Property WhereModifiedDescription() As String
            Get
                Return _whereModified.GetDescription()
            End Get
        End Property
#End Region

#Region " Operational Status "

        Public Property OperationalStatus() As FacilityOperationalStatus
            Get
                Return _operationalStatus
            End Get
            Set(ByVal value As FacilityOperationalStatus)
                _operationalStatus = value
            End Set
        End Property
        Private _operationalStatus As FacilityOperationalStatus

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
                _operationalStatus = [Enum].Parse(GetType(FacilityOperationalStatus), value)
            End Set
        End Property

        Public ReadOnly Property OperationalStatusDescription() As String
            Get
                Return _operationalStatus.GetDescription
            End Get
        End Property

#End Region

#Region " Classification "

        Public Property Classification() As FacilityClassification
            Get
                Return _classification
            End Get
            Set(ByVal value As FacilityClassification)
                _classification = value
            End Set
        End Property
        Private _classification As FacilityClassification

        ''' <summary>
        ''' A one or two-character string representing the facility classification.
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one or two-character encoded string.</value>
        ''' <returns>A one or two-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a one or two-character string.</remarks>
        Public Property ClassificationCode() As String
            Get
                Return _classification.ToString()
            End Get
            Set(ByVal value As String)
                _classification = [Enum].Parse(GetType(FacilityClassification), value)
            End Set
        End Property

        Public ReadOnly Property ClassificationDescription() As String
            Get
                Return _classification.GetDescription()
            End Get
        End Property

        Public Property CmsMember() As FacilityCmsMember
            Get
                Return _cmsMember
            End Get
            Set(ByVal value As FacilityCmsMember)
                _cmsMember = value
            End Set
        End Property
        Private _cmsMember As FacilityCmsMember

        ''' <summary>
        ''' A single-character string representing the facility classification. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a single-character string.</remarks>
        Public Property CmsMemberCode() As String
            Get
                Return _cmsMember.ToString()
            End Get
            Set(ByVal value As String)
                _cmsMember = [Enum].Parse(GetType(FacilityCmsMember), value)
            End Set
        End Property

        Public ReadOnly Property CmsMemberDescription() As String
            Get
                Return _cmsMember.GetDescription()
            End Get
        End Property

#End Region

#Region " Nonattainment statuses "

        Private _oneHourOzone As OneHourOzoneNonattainmentStatus
        Public Property OneHourOzoneNonAttainment() As OneHourOzoneNonattainmentStatus
            Get
                Return _oneHourOzone
            End Get
            Set(ByVal value As OneHourOzoneNonattainmentStatus)
                _oneHourOzone = value
            End Set
        End Property

        Private _eightHourOzone As EightHourOzoneNonattainmentStatus
        Public Property EightHourOzoneNonAttainment() As EightHourOzoneNonattainmentStatus
            Get
                Return _eightHourOzone
            End Get
            Set(ByVal value As EightHourOzoneNonattainmentStatus)
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
                OneHourOzoneNonAttainment.ToString("D") & _
                EightHourOzoneNonAttainment.ToString("D") & _
                PMFineNonAttainmentState.ToString("D") & _
                "0"
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) Then
                    OneHourOzoneNonAttainment = OneHourOzoneNonattainmentStatus.No
                    EightHourOzoneNonAttainment = EightHourOzoneNonattainmentStatus.None
                    PMFineNonAttainmentState = PMFineNonattainmentStatus.None
                Else
                    OneHourOzoneNonAttainment = Mid(value, 2, 1)
                    EightHourOzoneNonAttainment = Mid(value, 3, 1)
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

        Private _airProgramClassifications As AirProgramClassification
        Public Property AirProgramClassifications() As AirProgramClassification
            Get
                Return _airProgramClassifications
            End Get
            Set(ByVal value As AirProgramClassification)
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
                Return ConvertEnumToBitFlags(Of AirProgramClassification)(Me.AirProgramClassifications, AirProgramClassificationsCodeLength)
            End Get
            Set(ByVal value As String)
                _airProgramClassifications = ConvertBitFieldToEnum(Of AirProgramClassification)(value)
            End Set
        End Property
        Private Shared AirProgramClassificationsCodeLength As Integer = 5

#End Region

#End Region

#Region " Shared Functions "

        ''' <summary>
        ''' Tests whether a given string represents a valid RMP ID in format
        ''' (not whether the RMP ID is actually in use).
        ''' </summary>
        ''' <param name="rmpID">The string to test</param>
        ''' <returns>True if test string is in the format of a valid RMP ID. Otherwise, false.</returns>
        Public Shared Function IsValidRmpId(ByVal rmpID As String) As Boolean
            If rmpID Is Nothing Then Return False

            ' Valid RMP IDs are in the form 0000-0000-0000 (with the dashes)
            Dim rgx As New System.Text.RegularExpressions.Regex("^\d{4}-\d{4}-\d{4}$")
            Return rgx.IsMatch(rmpID)
        End Function

#End Region

    End Class

End Namespace