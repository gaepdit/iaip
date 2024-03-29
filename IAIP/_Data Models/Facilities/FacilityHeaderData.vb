﻿Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Apb.Facilities
    ''' <summary>
    ''' Facility Header Data includes program-related data for a facility, such as classification, 
    ''' operating status, and air program codes. 
    ''' </summary>
    Public Class FacilityHeaderData

#Region " Constructors "

        Public Sub New()
        End Sub

        Public Sub New(airsNumber As ApbFacilityId)
            Me.AirsNumber = airsNumber
        End Sub

#End Region

#Region " Properties "

#Region " Standard "

        Public Property AirsNumber As ApbFacilityId

        Public Property SicCode As String
            Get
                Return _sic
            End Get
            Set
                _sic = RealStringOrNothing(Value)
            End Set
        End Property

        Private _sic As String

        Public Property StartupDate As Date?

        Public Property ShutdownDate As Date?

        Public Property Naics As String
            Get
                Return _naics
            End Get
            Set
                _naics = RealStringOrNothing(Value)
            End Set
        End Property

        Private _naics As String

        Public Property RmpId As String
            Get
                Return _rmpId
            End Get
            Set
                If IsValidRmpId(Value) Then
                    _rmpId = Value
                Else
                    _rmpId = Nothing
                End If
            End Set
        End Property

        Private _rmpId As String

        Public Property OwnershipTypeCode As String

        ' Currently we are only tracking federally-owned facilities, represented by this OwnershipTypeCode
        Public Const FederallyOwnedTypeCode As String = "FDF"

        Public ReadOnly Property OwnershipType As String
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

        Public Const NspsFeeExemptDesc As String = "NSPS Fee Exempt"
        Public Property NspsFeeExempt As Boolean

        Public Property FacilityDescription As String
            Get
                Return _facilityDescription
            End Get
            Set
                _facilityDescription = RealStringOrNothing(Value)
            End Set
        End Property

        Private _facilityDescription As String

        Public Property HeaderUpdateComment As String
            Get
                Return _headerUpdateComment
            End Get
            Set
                _headerUpdateComment = RealStringOrNothing(Value)
            End Set
        End Property

        Private _headerUpdateComment As String

        Public Property DateDataModified As Date?

        Public Property WhoModified As String

        Public Property WhereModified As HeaderDataModificationLocation

#End Region

#Region " Operational Status "

        Public Property OperationalStatus As FacilityOperationalStatus

        ''' <summary>
        ''' A single-character string representing the operational status. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode operational status. Stored in database as a single-character string.</remarks>
        Public Property OperationalStatusCode As String
            Get
                Return OperationalStatus.ToString()
            End Get
            Set
                OperationalStatus = [Enum].Parse(GetType(FacilityOperationalStatus), Value)
            End Set
        End Property

        Public ReadOnly Property OperationalStatusDescription As String
            Get
                Return OperationalStatus.GetDescription
            End Get
        End Property

#End Region

#Region " Classification "

        Public Property Classification As FacilityClassification

        ''' <summary>
        ''' A one or two-character string representing the facility classification.
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one or two-character encoded string.</value>
        ''' <returns>A one or two-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a one or two-character string.</remarks>
        Public Property ClassificationCode As String
            Get
                Return Classification.ToString()
            End Get
            Set
                Classification = [Enum].Parse(GetType(FacilityClassification), Value)
            End Set
        End Property

        Public ReadOnly Property ClassificationDescription As String
            Get
                Return Classification.GetDescription()
            End Get
        End Property

        Public Property CmsMember As FacilityCmsMember

        ''' <summary>
        ''' A single-character string representing the facility classification. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A one-character encoded string.</value>
        ''' <returns>A one-character encoded string.</returns>
        ''' <remarks>Used to encode facility classification. Stored in database as a single-character string.</remarks>
        Public Property CmsMemberCode As String
            Get
                Return CmsMember.ToString()
            End Get
            Set
                CmsMember = [Enum].Parse(GetType(FacilityCmsMember), Value)
            End Set
        End Property

        Public ReadOnly Property CmsMemberDescription As String
            Get
                Return CmsMember.GetDescription()
            End Get
        End Property

#End Region

#Region " Nonattainment statuses "

        Public Property OneHourOzoneNonAttainment As OneHourOzoneNonattainmentStatus
        Public Property EightHourOzoneNonAttainment As EightHourOzoneNonattainmentStatus
        Public Property PMFineNonAttainmentState As PMFineNonattainmentStatus

        ''' <summary>
        ''' A five-character string representing all nonattainment area statuses. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A five-character encoded string.</value>
        ''' <returns>A five-character encoded string.</returns>
        ''' <remarks>Used to encode OneHourNonAttainmentState, EightHourNonAttainmentState, 
        ''' and PMFineNonAttainmentState. Stored in database as a five-character coded string. 
        ''' Remarkably, only the middle three characters are used.</remarks>
        Public Property NonattainmentStatusesCode As String
            Get
                Return "0" &
                       OneHourOzoneNonAttainment.ToString("D") &
                       EightHourOzoneNonAttainment.ToString("D") &
                       PMFineNonAttainmentState.ToString("D") &
                       "0"
            End Get
            Set
                If String.IsNullOrEmpty(Value) Then
                    OneHourOzoneNonAttainment = OneHourOzoneNonattainmentStatus.No
                    EightHourOzoneNonAttainment = EightHourOzoneNonattainmentStatus.None
                    PMFineNonAttainmentState = PMFineNonattainmentStatus.None
                Else
                    OneHourOzoneNonAttainment = Mid(Value, 2, 1)
                    EightHourOzoneNonAttainment = Mid(Value, 3, 1)
                    PMFineNonAttainmentState = Mid(Value, 4, 1)
                End If
            End Set
        End Property

#End Region

#Region " Program Codes "

        Public Property AirPrograms As AirPrograms

        ''' <summary>
        ''' A 15-character string representing all air programs a facility is subject to. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A 15-character encoded string.</value>
        ''' <returns>A 15-character encoded string.</returns>
        ''' <remarks>Used to encode the Air Programs a facility may be subject to. 
        ''' Stored in database as a 15-character coded string, but only the first 14 are used.</remarks>
        Public Property AirProgramsCode As String
            Get
                Return ConvertEnumToBitFlags(AirPrograms, AirProgramsCodeLength)
            End Get
            Set
                AirPrograms = ConvertBitFieldToEnum(Of AirPrograms)(Value)
            End Set
        End Property

        Private Const AirProgramsCodeLength As Integer = 15

        Public Shared ReadOnly ConvertAirProgramLegacyCodes As New Dictionary(Of String, AirPrograms) From {
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

        Public Shared Function ConvertAirProgramToLegacyCode(key As AirPrograms) As String
            Return _AirProgramToLegacyCode(key.ToString)
        End Function

        Public Shared Function ConvertAirProgramToLegacyCode(key As String) As String
            Return _AirProgramToLegacyCode(key)
        End Function

        Private Shared ReadOnly _AirProgramToLegacyCode As New Dictionary(Of String, String) From {
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

        Public Property AirProgramClassifications As AirProgramClassifications

        ''' <summary>
        ''' A five-character string representing a facility's air program classifications. 
        ''' Used for storage and retrieval from database.
        ''' </summary>
        ''' <value>A five-character encoded string.</value>
        ''' <returns>A five-character encoded string.</returns>
        ''' <remarks>Used to encode a facility's air program classifications. 
        ''' Stored in database as a five-character coded string, but only the first two are used.</remarks>
        Public Property AirProgramClassificationsCode As String
            Get
                Return ConvertEnumToBitFlags(AirProgramClassifications, AirProgramClassificationsCodeLength)
            End Get
            Set
                AirProgramClassifications = ConvertBitFieldToEnum(Of AirProgramClassifications)(Value)
            End Set
        End Property

        Private Const AirProgramClassificationsCodeLength As Integer = 5

#End Region

#End Region

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

    End Class
End Namespace