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
        'Public Property LinkedWorkItemId As Integer ' STRTRACKINGNUMBER	NUMBER(10,0)
        Public Property Comment As String ' STRGENERALCOMMENTS	VARCHAR2(4000 BYTE)

        ' Enforcement Info

        ''' <summary>
        ''' This property saves a value in the database that is solely used for determining whether 
        ''' enforcement/case file information is sent to EPA (ICIS-Air): 
        ''' If the value equals LON, then data is NOT sent.
        ''' Otherwise, data IS sent. 
        ''' 
        ''' The keyword "LON" does not necessarily indicate the presence of a LON enforcement action.
        ''' It is merely a legacy keyword used by the ICIS-Air db procedures and views. All other 
        ''' possible values ("CASEFILE", "HPV", "NOVCO", etc.) are considered equivalent to each other 
        ''' and have no meaning other than that contained herein.
        ''' </summary>
        ''' <returns>EnforcementType Enum (LON or CASEFILE)</returns>
        Public ReadOnly Property EnforcementType As EnforcementType ' STRACTIONTYPE	VARCHAR2(15 BYTE)
            Get
                With Me.EnforcementActions
                    If .Contains(EnforcementActionType.NOV) Or .Contains(EnforcementActionType.CO) Or .Contains(EnforcementActionType.AO) Then
                        Return EnforcementType.CASEFILE
                    Else
                        Return EnforcementType.LON
                    End If
                End With
            End Get
        End Property
        Public Property EnforcementActions As List(Of EnforcementActionType)
        Public Property ViolationType As String ' STRHPV	VARCHAR2(15 BYTE)

        ' Status
        Public Property Open As OpenOrClosed ' STRENFORCEMENTFINALIZED	VARCHAR2(5 BYTE)
        Public ReadOnly Property EnforcementStatus As EnforcementStatus
            Get
                If Not Open Then
                    Return EnforcementStatus.CaseClosed
                ElseIf LonResolved.HasValue Or NfaSent.HasValue Or CoResolved.HasValue Or AoResolved.HasValue Then
                    Return EnforcementStatus.CaseResolved
                ElseIf CoExecuted.HasValue Or AoExecuted.HasValue Then
                    Return EnforcementStatus.SubjectToComplianceSchedule
                Else
                    Return EnforcementStatus.CaseOpen
                End If
            End Get
        End Property
        Public Property DateFinalized As Date? ' DATENFORCEMENTFINALIZED	DATE
        Public Property DateModified As Date? ' DATMODIFINGDATE	DATE
        Public Property SubmittedToUc As Boolean ' STRSTATUS	VARCHAR2(5 BYTE)
        Public Property SubmittedToEpa As Boolean ' STRAFSKEYACTIONNUMBER	VARCHAR2(5 BYTE)
        Public Property SubmittedToUcCode As String ' STRSTATUS	VARCHAR2(5 BYTE)
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
                LegacyAirPrograms = ParseEnforcementPrograms(value)
            End Set
        End Property

        Public Property Pollutants As List(Of String)

        Public ReadOnly Property AirPrograms As List(Of String)
            Get
                If LegacyAirPrograms Is Nothing Then
                    Return Nothing
                End If

                Dim ap As New List(Of String)
                For Each p As String In LegacyAirPrograms
                    ap.Add(Facilities.FacilityHeaderData.ConvertAirProgramLegacyCodes(p).ToString)
                Next
                Return ap
            End Get
        End Property
        Public Property LegacyAirPrograms As List(Of String)

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
                Return "N/A"
            End If
            Return GA_STATE_CODE & "000A0000" & GA_STATE_NUMERIC_CODE & AirsNumber.ShortString & afs.ToString("00000")
        End Function

#End Region

#Region " Pollutants/Programs functions "

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
            If LegacyAirPrograms Is Nothing OrElse
                LegacyAirPrograms.Count = 0 OrElse
                Pollutants Is Nothing OrElse
                Pollutants.Count = 0 Then
                Return Nothing
            End If

            Dim result As New StringBuilder
            For Each pr As String In LegacyAirPrograms
                For Each pl As String In Pollutants
                    result.Append(pr & pl & ",")
                Next
            Next
            Return result.ToString
        End Function

#End Region

    End Class

#Region " Enums "

    ''' <summary>
    ''' Enforcement Status. These are applied on a per-enforcement case basis.
    ''' </summary>
    ''' <remarks>Stored in database as a string.</remarks>
    Public Enum EnforcementStatus
        <Description("Open enforcement case")> CaseOpen
        <Description("Subject to compliance schedule")> SubjectToComplianceSchedule
        <Description("Enforcement case resolved")> CaseResolved
        <Description("Enforcement case closed")> CaseClosed
    End Enum

    Public Enum EnforcementType
        LON
        CASEFILE
    End Enum

    Public Enum EnforcementActionType
        LON
        NOV
        CO
        AO
    End Enum

#End Region

End Namespace
