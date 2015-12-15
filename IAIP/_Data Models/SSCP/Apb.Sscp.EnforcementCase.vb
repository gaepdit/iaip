﻿Imports System.Collections.Generic
Imports Iaip.Apb.Facilities

Namespace Apb.Sscp

    Public Class EnforcementCase

        Public Property EnforcementId As Integer ' STRENFORCEMENTNUMBER	NUMBER(10,0)

        ' General Info
        Public Property AirsNumber As ApbFacilityId ' STRAIRSNUMBER	VARCHAR2(12 BYTE)
        Public Property StaffResponsible As Staff ' NUMSTAFFRESPONSIBLE	NUMBER
        Public Property LinkedWorkItemId As Integer ' STRTRACKINGNUMBER	NUMBER(10,0)
        Public Property Comment As String ' STRGENERALCOMMENTS	VARCHAR2(4000 BYTE)

        ' Enforcement Info
        Public Property EnforcementActions As List(Of EnforcementActionType)
        Public Property LegacyEnforcementType As LegacyEnforcementType ' STRACTIONTYPE	VARCHAR2(15 BYTE)
        Public Property EnforcementTypeCode As String
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

        ' Pollutants
        Public Property Pollutants As List(Of String) ' STRPOLLUTANTS	VARCHAR2(4000 BYTE)

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

    End Class

    Public Enum ComplianceStatus
        Unknown
        InViolation
        MeetingComplianceSchedule
        InCompliance
    End Enum

    Public Enum EnforcementActionType
        LON
        NOV
        CO
        AO
    End Enum

    Public Enum LegacyEnforcementType
        LON
        NOV
        NOVCO
        NOVCOP
        NOVAO
        HPV
        HPVCO
        HPVAO
    End Enum

End Namespace
