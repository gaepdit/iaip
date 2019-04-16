Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports Iaip.SharedData

Namespace Apb.Facilities

    ''' <summary>
    ''' Facility Header Data includes program-related data for a facility, such as classification, 
    ''' operating status, and air program codes. 
    ''' </summary>
    Public Class FacilityHeaderData

#Region " Constructors "

        Public Sub New()
        End Sub

        Public Sub New(airsNumber As Apb.ApbFacilityId)
            Me.AirsNumber = airsNumber
        End Sub

#End Region

#Region " Properties "

#Region " Standard "

        Public Property AirsNumber() As ApbFacilityId
        Public Property SicCode() As String
            Get
                Return _sic
            End Get
            Set(value As String)
                _sic = RealStringOrNothing(value)
            End Set
        End Property
        Private _sic As String

        Public Property StartupDate() As Date?

        Public Property ShutdownDate() As Date?

        Public Property Naics() As String
            Get
                Return _naics
            End Get
            Set(value As String)
                _naics = RealStringOrNothing(value)
            End Set
        End Property
        Private _naics As String

        Public Property RmpId() As String
            Get
                Return _rmpId
            End Get
            Set(value As String)
                If IsValidRmpId(value) Then
                    _rmpId = value
                Else
                    _rmpId = Nothing
                End If
            End Set
        End Property
        Private _rmpId As String

        Public Property OwnershipTypeCode() As String

        ' Currently we are only tracking federally-owned facilities, represented by this OwnershipTypeCode
        Public Shared ReadOnly FederallyOwnedTypeCode As String = "FDF"

        Public ReadOnly Property OwnershipType() As String
            Get
                If String.IsNullOrEmpty(OwnershipTypeCode) Then
                    Return Nothing
                End If
                Dim dt As DataTable = GetSharedData(SharedTable.FacilityOwnershipTypes)
                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    Return Nothing
                End If
                Dim dr As DataRow = dt.Rows.Find(OwnershipTypeCode)
                If dr Is Nothing Then
                    Return Nothing
                End If
                Return dr.Item(1).ToString
            End Get
        End Property

        Public Property FacilityDescription() As String
            Get
                Return _facilityDescription
            End Get
            Set(value As String)
                _facilityDescription = RealStringOrNothing(value)
            End Set
        End Property
        Private _facilityDescription As String

        Public Property HeaderUpdateComment() As String
            Get
                Return _headerUpdateComment
            End Get
            Set(value As String)
                _headerUpdateComment = RealStringOrNothing(value)
            End Set
        End Property
        Private _headerUpdateComment As String

        Public Property DateDataModified() As Date?

        Public Property WhoModified() As String

        Public Property WhereModified() As HeaderDataModificationLocation

#End Region

#Region " Operational Status "

        Public Property OperationalStatus() As FacilityOperationalStatus

        ''' <summary>
        ''' A single-character string representing the operational status. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode operational status. Stored in database as a single-character string.</remarks>
        Public Property OperationalStatusCode() As String
            Get
                Return OperationalStatus.ToString()
            End Get
            Set(value As String)
                OperationalStatus = [Enum].Parse(GetType(FacilityOperationalStatus), value)
            End Set
        End Property

        Public ReadOnly Property OperationalStatusDescription() As String
            Get
                Return OperationalStatus.GetDescription
            End Get
        End Property

#End Region

#Region " Classification "

        Public Property Classification() As FacilityClassification

        ''' <summary>
        ''' A one or two-character string representing the facility classification.
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one or two-character encoded string.</value>
        ''' <returns>A one or two-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a one or two-character string.</remarks>
        Public Property ClassificationCode() As String
            Get
                Return Classification.ToString()
            End Get
            Set(value As String)
                Classification = [Enum].Parse(GetType(FacilityClassification), value)
            End Set
        End Property

        Public ReadOnly Property ClassificationDescription() As String
            Get
                Return Classification.GetDescription()
            End Get
        End Property

        Public Property CmsMember() As FacilityCmsMember

        ''' <summary>
        ''' A single-character string representing the facility classification. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a single-character string.</remarks>
        Public Property CmsMemberCode() As String
            Get
                Return CmsMember.ToString()
            End Get
            Set(value As String)
                CmsMember = [Enum].Parse(GetType(FacilityCmsMember), value)
            End Set
        End Property

        Public ReadOnly Property CmsMemberDescription() As String
            Get
                Return CmsMember.GetDescription()
            End Get
        End Property

#End Region

#Region " Nonattainment statuses "

        Public Property OneHourOzoneNonAttainment() As OneHourOzoneNonattainmentStatus
        Public Property EightHourOzoneNonAttainment() As EightHourOzoneNonattainmentStatus
        Public Property PMFineNonAttainmentState() As PMFineNonattainmentStatus

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
                Return "0" &
                OneHourOzoneNonAttainment.ToString("D") &
                EightHourOzoneNonAttainment.ToString("D") &
                PMFineNonAttainmentState.ToString("D") &
                "0"
            End Get
            Set(value As String)
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

        Public Property AirPrograms() As AirPrograms

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
                Return ConvertEnumToBitFlags(Of AirPrograms)(Me.AirPrograms, AirProgramsCodeLength)
            End Get
            Set(value As String)
                AirPrograms = ConvertBitFieldToEnum(Of AirPrograms)(value)
            End Set
        End Property

        Private Shared AirProgramsCodeLength As Integer = 15

        Public Shared Property ConvertAirProgramLegacyCodes As Dictionary(Of String, AirPrograms) =
            New Dictionary(Of String, AirPrograms) From {
            {"0", AirPrograms.SIP},
            {"1", AirPrograms.FederalSIP},
            {"3", AirPrograms.NonFederalSIP},
            {"4", AirPrograms.CfcTracking},
            {"6", AirPrograms.PSD},
            {"7", AirPrograms.NSR},
            {"8", AirPrograms.NESHAP},
            {"9", AirPrograms.NSPS},
            {"F", AirPrograms.FESOP},
            {"A", AirPrograms.AcidPrecipitation},
            {"I", AirPrograms.NativeAmerican},
            {"M", AirPrograms.MACT},
            {"V", AirPrograms.TitleV},
            {"R", AirPrograms.RMP}
        }

        Public Shared Property ConvertAirProgramToLegacyCode As Dictionary(Of String, String) =
            New Dictionary(Of String, String) From {
            {AirPrograms.SIP.ToString, "0"},
            {AirPrograms.FederalSIP.ToString, "1"},
            {AirPrograms.NonFederalSIP.ToString, "3"},
            {AirPrograms.CfcTracking.ToString, "4"},
            {AirPrograms.PSD.ToString, "6"},
            {AirPrograms.NSR.ToString, "7"},
            {AirPrograms.NESHAP.ToString, "8"},
            {AirPrograms.NSPS.ToString, "9"},
            {AirPrograms.FESOP.ToString, "F"},
            {AirPrograms.AcidPrecipitation.ToString, "A"},
            {AirPrograms.NativeAmerican.ToString, "I"},
            {AirPrograms.MACT.ToString, "M"},
            {AirPrograms.TitleV.ToString, "V"},
            {AirPrograms.RMP.ToString, "R"}
        }

        Public Property AirProgramClassifications() As AirProgramClassifications

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
            Set(value As String)
                AirProgramClassifications = ConvertBitFieldToEnum(Of AirProgramClassifications)(value)
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
        Public Shared Function IsValidRmpId(rmpID As String) As Boolean
            If rmpID Is Nothing Then Return False
            Return Regex.IsMatch(rmpID, RmpIdPattern)
        End Function

        Public Shared Function GetAirProgramDbKey(selectedAirProgram As AirPrograms) As String
            Select Case selectedAirProgram
                Case AirPrograms.AcidPrecipitation
                    Return "A"
                Case AirPrograms.CfcTracking
                    Return "4"
                Case AirPrograms.FederalSIP
                    Return "1"
                Case AirPrograms.FESOP
                    Return "F"
                Case AirPrograms.MACT
                    Return "M"
                Case AirPrograms.NativeAmerican
                    Return "I"
                Case AirPrograms.NESHAP
                    Return "8"
                Case AirPrograms.NonFederalSIP
                    Return "3"
                Case AirPrograms.NSPS
                    Return "9"
                Case AirPrograms.NSR
                    Return "7"
                Case AirPrograms.PSD
                    Return "6"
                Case AirPrograms.RMP
                    Return "R"
                Case AirPrograms.SIP
                    Return "0"
                Case AirPrograms.TitleV
                    Return "V"
                Case Else
                    Return ""
            End Select
        End Function

#End Region

    End Class

End Namespace