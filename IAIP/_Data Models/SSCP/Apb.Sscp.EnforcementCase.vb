Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text

Namespace Apb.Sscp

    Public Class EnforcementCase

#Region " Properties "

        Public Property EnforcementId As Integer ' STRENFORCEMENTNUMBER	NUMBER(10,0)

        ' General Info
        Public Property AirsNumber As ApbFacilityId ' STRAIRSNUMBER	VARCHAR2(12 BYTE)
        Public Property StaffResponsibleId As Integer ' NUMSTAFFRESPONSIBLE	NUMBER
        Public Property LinkedWorkItemId As Integer ' STRTRACKINGNUMBER	NUMBER(10,0)
        Public Property Comment As String ' STRGENERALCOMMENTS	VARCHAR2(4000 BYTE)

        ' Enforcement Info
        Public Property EnforcementActions As List(Of EnforcementActionType)
        Public Property LegacyEnforcementType As LegacyEnforcementType ' STRACTIONTYPE	VARCHAR2(15 BYTE)
        Public Property LegacyEnforcementTypeCode As String
            Get
                Return LegacyEnforcementType.ToString()
            End Get
            Set(ByVal value As String)
                LegacyEnforcementType = [Enum].Parse(GetType(LegacyEnforcementType), value)
            End Set
        End Property
        Public Property ViolationType As String ' STRHPV	VARCHAR2(15 BYTE)

        ' Status
        Public Property Open As Boolean ' STRENFORCEMENTFINALIZED	VARCHAR2(5 BYTE)
        Public Property DateFinalized As Date? ' DATENFORCEMENTFINALIZED	DATE
        Public Property DateModified As Date? ' 
        Public Property SubmittedToUc As Boolean ' STRSTATUS	VARCHAR2(5 BYTE)
        Public Property SubmittedToEpa As Boolean
        Public Property SubmittedToUcCode As String
            Get
                If SubmittedToUc Then
                    Return "UC"
                Else
                    Return Nothing
                End If
            End Get
            Set(value As String)
                If value = "UC" Then
                    SubmittedToUc = True
                Else
                    SubmittedToUc = False
                End If
            End Set
        End Property

        ' Discovery
        Public Property DiscoveryDate As Date? ' STRDISCOVERYDATE	VARCHAR2(5 BYTE); DATDISCOVERYDATE	DATE
        Public Property DayZeroDate As Date? ' STRDAYZERO	VARCHAR2(5 BYTE); DATDAYZERO	DATE

        ' Programs & Pollutants
        Public Property ProgramPollutants As String ' STRPOLLUTANTS	VARCHAR2(4000 BYTE)
            Get
                Return CombineProgramPollutants()
            End Get
            Set(value As String)
                Pollutants = ParseEnforcementPollutants(value)
                Programs = ParseEnforcementPrograms(value)
            End Set
        End Property

        Public Property Pollutants As List(Of String)
        Public Property Programs As List(Of String)

        ' Compliance status
        Public Property ComplianceStatus As ComplianceStatus
        Public Property LegacyComplianceStatus As LegacyComplianceStatus ' STRPOLLUTANTSTATUS	VARCHAR2(2 BYTE)
        Public Property LegacyComplianceStatusCode As String
            Get
                Return LegacyComplianceStatus.ToString()
            End Get
            Set(ByVal value As String)
                LegacyComplianceStatus = [Enum].Parse(GetType(LegacyComplianceStatus), value)
            End Set
        End Property
        Public Property LegacyComplianceStatusDbCode As String
            Get
                If LegacyComplianceStatus = LegacyComplianceStatus.NoValue Then
                    Return "0"
                End If
                Return LegacyComplianceStatusCode.Substring(7)
            End Get
            Set(ByVal value As String)
                LegacyComplianceStatus = [Enum].Parse(GetType(LegacyComplianceStatus), "Status_" & value)
            End Set
        End Property

        ' LON
        Public Property LonToUc As Date? ' STRLONTOUC	VARCHAR2(5 BYTE); DATLONTOUC	DATE
        Public Property LonSent As Date? ' STRLONSENT	VARCHAR2(5 BYTE); DATLONSENT	DATE
        Public Property LonResolved As Date? ' STRLONRESOLVED	VARCHAR2(5 BYTE); DATLONRESOLVED	DATE
        Public Property LonComment As String ' STRLONCOMMENTS	VARCHAR2(4000 BYTE)

        ' NOV
        Public Property NovToUc As Date? ' STRNOVTOUC	VARCHAR2(5 BYTE); DATNOVTOUC	DATE
        Public Property NovToPm As Date? ' STRNOVTOPM	VARCHAR2(5 BYTE); DATNOVTOPM	DATE
        Public Property NovSent As Date? ' STRNOVSENT	VARCHAR2(5 BYTE); DATNOVSENT	DATE
        Public Property NovResponseReceived As Date? ' STRNOVRESPONSERECEIVED	VARCHAR2(5 BYTE); DATNOVRESPONSERECEIVED	DATE
        Public Property NovComment As String ' STRNOVCOMMENT	VARCHAR2(4000 BYTE)

        ' NFA
        Public Property NfaToUc As Date? ' STRNFATOUC	VARCHAR2(5 BYTE); DATNFATOUC	DATE
        Public Property NfaToPm As Date? ' STRNFATOPM	VARCHAR2(5 BYTE); DATNFATOPM	DATE
        Public Property NfaSent As Date? ' STRNFALETTERSENT	VARCHAR2(5 BYTE); DATNFALETTERSENT	DATE

        ' CO
        Public Property CoToUc As Date? ' STRCOTOUC	VARCHAR2(5 BYTE); DATCOTOUC	DATE
        Public Property CoToPm As Date? ' STRCOTOPM	VARCHAR2(5 BYTE); DATCOTOPM	DATE
        Public Property CoProposed As Date? ' STRCOPROPOSED	VARCHAR2(5 BYTE); DATCOPROPOSED	DATE
        Public Property CoReceivedFromCompany As Date? ' STRCORECEIVEDFROMCOMPANY	VARCHAR2(5 BYTE); DATCORECEIVEDFROMCOMPANY	DATE
        Public Property CoReceivedFromDirector As Date? ' STRCORECEIVEDFROMDIRECTOR	VARCHAR2(5 BYTE); DATCORECEIVEDFROMDIRECTOR	DATE
        Public Property CoExecuted As Date? ' STRCOEXECUTED	VARCHAR2(5 BYTE)' DATCOEXECUTED	DATE
        Public Property CoNumber As String ' STRCONUMBER	VARCHAR2(255 BYTE)
        Public Property CoResolved As Date? ' STRCORESOLVED	VARCHAR2(5 BYTE)' DATCORESOLVED	DATE
        Public Property CoPenaltyAmount As Decimal ' STRCOPENALTYAMOUNT	VARCHAR2(20 BYTE)
        Public Property CoPenaltyAmountComment As String ' STRCOPENALTYAMOUNTCOMMENTS	VARCHAR2(4000 BYTE)
        Public Property CoComment As String ' STRCOCOMMENT	VARCHAR2(4000 BYTE)

        ' AO
        Public Property AoExecuted As Date? ' STRAOEXECUTED	VARCHAR2(5 BYTE)' DATAOEXECUTED	DATE
        Public Property AoAppealed As Date? ' STRAOAPPEALED	VARCHAR2(5 BYTE)' DATAOAPPEALED	DATE
        Public Property AoResolved As Date? ' STRAORESOLVED	VARCHAR2(5 BYTE)' DATAORESOLVED	DATE
        Public Property AoComment As String ' STRAOCOMMENT	VARCHAR2(4000 BYTE)

        ' AFS action numbers
        Public Property AfsKeyActionNumber As Integer ' STRAFSKEYACTIONNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsNovActionNumber As Integer ' STRAFSNOVSENTNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsNfaActionNumber As Integer ' STRAFSNOVRESOLVEDNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsCoProposedNumber As Integer ' STRAFSCOPROPOSEDNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsCoActionNumber As Integer ' STRAFSCOEXECUTEDNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsCoResolvedActionNumber As Integer ' STRAFSCORESOLVEDNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsAoToAGActionNumber As Integer ' STRAFSAOTOAGNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsCivilCourtActionNumber As Integer ' STRAFSCIVILCOURTNUMBER	VARCHAR2(5 BYTE)
        Public Property AfsAoResolvedActionNumber As Integer ' STRAFSAORESOLVEDNUMBER	VARCHAR2(5 BYTE)

#End Region

#Region " EPA IDs "

        Public ReadOnly Property CaseFileId As String
            Get
                Return EpaIdFromActionNumber(AfsKeyActionNumber)
            End Get
        End Property
        Public ReadOnly Property NovEnforcementActionId As String
            Get
                Return EpaIdFromActionNumber(AfsNovActionNumber)
            End Get
        End Property
        Public ReadOnly Property CoEnforcementActionId As String
            Get
                Return EpaIdFromActionNumber(AfsCoActionNumber)
            End Get
        End Property
        Public ReadOnly Property AoEnforcementActionId As String
            Get
                Return EpaIdFromActionNumber(AfsAoToAGActionNumber)
            End Get
        End Property

        Private Function EpaIdFromActionNumber(afs As Integer) As String
            If afs = 0 Then
                Return ""
            End If
            Return GaStateCode & "000A0000" & GaStateNumericCode & AirsNumber.ShortString & afs.ToString("00000")
        End Function

#End Region

#Region " Pollutants/Programs "

        Private Function ParseEnforcementPollutants(progPoll As String) As List(Of String)
            If progPoll = "" OrElse Not progPoll.Contains(","c) Then Return Nothing

            Dim p As String() = progPoll.Split({","c}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To p.Length - 1
                p(i) = p(i).Substring(1)
            Next
            Return p.Distinct.ToList
        End Function

        Private Function ParseEnforcementPrograms(progPoll As String) As List(Of String)
            If progPoll = "" OrElse Not progPoll.Contains(","c) Then Return Nothing

            Dim p As String() = progPoll.Split({","c}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To p.Length - 1
                p(i) = p(i).Substring(0, 1)
            Next
            Return p.Distinct.ToList
        End Function

        Private Function CombineProgramPollutants() As String
            If Programs Is Nothing OrElse
                Programs.Count = 0 OrElse
                Pollutants Is Nothing OrElse
                Pollutants.Count = 0 Then
                Return Nothing
            End If

            Dim result As New StringBuilder
            For Each pr As String In Programs
                For Each pl As String In Pollutants
                    result.Append(pr & pl & ",")
                Next
            Next
            Return result.ToString
        End Function

#End Region

#Region " Compliance status "

        Public Shared Function ConvertLegacyComplianceStatus(legacyComplianceStatus As LegacyComplianceStatus) As ComplianceStatus
            Select Case legacyComplianceStatus
                Case LegacyComplianceStatus.NoValue,
                     LegacyComplianceStatus.Status_P,
                     LegacyComplianceStatus.Status_A,
                     LegacyComplianceStatus.Status_0
                    Return ComplianceStatus.Unknown
                Case LegacyComplianceStatus.Status_B,
                     LegacyComplianceStatus.Status_1,
                     LegacyComplianceStatus.Status_6,
                     LegacyComplianceStatus.Status_W,
                     LegacyComplianceStatus.Status_8
                    Return ComplianceStatus.InViolation
                Case LegacyComplianceStatus.Status_5
                    Return ComplianceStatus.MeetingComplianceSchedule
                Case LegacyComplianceStatus.Status_2,
                     LegacyComplianceStatus.Status_3,
                     LegacyComplianceStatus.Status_4,
                     LegacyComplianceStatus.Status_9,
                     LegacyComplianceStatus.Status_C,
                     LegacyComplianceStatus.Status_M
                    Return ComplianceStatus.InCompliance
            End Select
        End Function

        Public Shared Function ConvertComplianceStatus(complianceStatus As ComplianceStatus) As LegacyComplianceStatus
            Select Case complianceStatus
                Case ComplianceStatus.InCompliance
                    Return LegacyComplianceStatus.Status_C
                Case ComplianceStatus.InViolation
                    Return LegacyComplianceStatus.Status_W
                Case ComplianceStatus.MeetingComplianceSchedule
                    Return LegacyComplianceStatus.Status_5
                Case ComplianceStatus.Unknown
                    Return LegacyComplianceStatus.Status_0
            End Select
        End Function

#End Region

    End Class

    ''' <summary>
    ''' Compliance Status codes. These are applied on a per-pollutant, per-rule basis. 
    ''' </summary>
    ''' <remarks>Stored in database as a one-character string. The enum values are 
    ''' significant as they are used to determine controlling compliance status.</remarks>
    Public Enum LegacyComplianceStatus
        <Description("In violation, procedural & emissions")> Status_B = 35
        <Description("In violation, no schedule")> Status_1 = 34
        <Description("In violation, not meeting schedule")> Status_6 = 33
        <Description("In violation, procedural")> Status_W = 32
        <Description("In violation, no applicable state reg")> Status_8 = 31
        <Description("Unknown compliance status")> Status_P = 23
        <Description("Unknown compliance status")> Status_A = 22
        <Description("Unknown compliance status")> Status_0 = 21
        <Description("Meeting compliance schedule")> Status_5 = 11
        <Description("In compliance, source test")> Status_2 = 6
        <Description("In compliance, inspection ")> Status_3 = 5
        <Description("In compliance, certification ")> Status_4 = 4
        <Description("In compliance, shut down")> Status_9 = 3
        <Description("In compliance, procedural")> Status_C = 2
        <Description("In compliance, CEMS data")> Status_M = 1
        <Description("No value")> NoValue = 0
    End Enum

    Public Enum ComplianceStatus
        <Description("Unknown compliance status")> Unknown
        <Description("In compliance")> InCompliance
        <Description("Subject to compliance schedule")> MeetingComplianceSchedule
        <Description("In violation")> InViolation
    End Enum

    Public Enum EnforcementActionType
        LON
        NOV
        CO
        AO
    End Enum

    Public Enum LegacyEnforcementType
        None
        LON
        NOV
        NOVCO
        NOVCOP
        NOVAO
        HPV
        HPVCO
        HPVCOP
        HPVAO
    End Enum

End Namespace
