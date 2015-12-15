Imports System.Collections.Generic
Imports System.Linq
Imports Iaip.Apb.Sscp
Imports Iaip.Apb.Facilities
Imports Oracle.ManagedDataAccess.Client

Namespace DAL.Sscp

    Module EnforcementData

#Region " Summary tables "

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data for a given facility
        ''' </summary>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <returns>A DataTable of enforcement summary data</returns>
        Public Function GetEnforcementSummaryDataTable(
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_ENFORCEMENT_SUMMARY " &
                " WHERE 1=1 "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND NUMSTAFFRESPONSIBLE = :staffId "

            Dim parameters As OracleParameter() = {
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data  for a given facility
        ''' </summary>
        ''' <param name="dateRangeEnd">Ending date of a date range to filter for.</param>
        ''' <param name="dateRangeStart">Starting date of a date range to filter for.</param>
        ''' <param name="airs">An optional Facility ID to filter for.</param>
        ''' <param name="staffId">An optional Staff ID to filter for.</param>
        ''' <returns>A DataTable of enforcement summary data</returns>
        Public Function GetEnforcementSummaryDataTable(
                dateRangeStart As Date, dateRangeEnd As Date,
                Optional airs As Apb.ApbFacilityId = Nothing,
                Optional staffId As String = Nothing) As DataTable
            Dim query As String =
                "SELECT * FROM AIRBRANCH.VW_SSCP_ENFORCEMENT_SUMMARY " &
                " WHERE TRUNC(EnforcementDate) BETWEEN :datestart AND :dateend "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = :airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND NUMSTAFFRESPONSIBLE = :staffId "

            Dim parameters As OracleParameter() = {
                New OracleParameter("datestart", dateRangeStart),
                New OracleParameter("dateend", dateRangeEnd),
                New OracleParameter("airs", airs.DbFormattedString),
                New OracleParameter("staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Exists "

        Public Function EnforcementExists(ByVal enforcementId As String) As Boolean
            If enforcementId = "" OrElse Not Integer.TryParse(enforcementId, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                " WHERE RowNum = 1 " &
                " AND STRENFORCEMENTNUMBER = :enforcementId "
            Dim parameter As New OracleParameter("enforcementId", enforcementId)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function EnforcementExistsForTrackingNumber(ByVal trackingNumber As String, ByRef enfNumber As String) As Boolean
            If trackingNumber = "" OrElse Not Integer.TryParse(trackingNumber, Nothing) Then Return False

            Dim query As String = " SELECT STRENFORCEMENTNUMBER " &
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRTRACKINGNUMBER = :trackingNumber "
            Dim parameter As New OracleParameter("trackingNumber", trackingNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)

            If result Is Nothing Then
                Return False
            Else
                enfNumber = result
                Return True
            End If
        End Function

#End Region

#Region " Get info "

        Private Sub FillEnforcementInfoFromDataRow(ByVal row As DataRow, ByRef enfInfo As EnforcementInfo)
            Dim address As New Address
            With address
                .City = DB.GetNullable(Of String)(row("STRFACILITYCITY"))
                .State = DB.GetNullable(Of String)(row("STRFACILITYSTATE"))
            End With

            Dim location As New Location
            With location
                .Address = address
            End With

            Dim facility As New Apb.Facilities.Facility
            With facility
                .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .FacilityName = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
                .FacilityLocation = location
            End With

            Dim staff As New Staff
            With staff
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
            End With

            With enfInfo
                .DiscoveryDate = DB.GetNullable(Of Date?)(row("DATDISCOVERYDATE"))
                .DateFinalized = DB.GetNullable(Of Date?)(row("DATENFORCEMENTFINALIZED"))
                .Open = Not Convert.ToBoolean(row("STRENFORCEMENTFINALIZED"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
                .EnforcementTypeCode = DB.GetNullable(Of String)(row("STRACTIONTYPE"))
                .Facility = facility
                .StaffResponsible = staff
            End With
        End Sub

        Public Function GetEnforcementInfo(ByVal enforcementId As String) As EnforcementInfo
            Dim query As String =
                " SELECT SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER, " &
                "   SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER, " &
                "   APBFACILITYINFORMATION.STRFACILITYNAME, " &
                "   APBFACILITYINFORMATION.STRFACILITYCITY, " &
                "   APBFACILITYINFORMATION.STRFACILITYSTATE, " &
                "   EPDUSERPROFILES.STRFIRSTNAME, " &
                "   EPDUSERPROFILES.STRLASTNAME, " &
                "   SSCP_AUDITEDENFORCEMENT.DATDISCOVERYDATE, " &
                "   SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTFINALIZED, " &
                "   SSCP_AUDITEDENFORCEMENT.DATENFORCEMENTFINALIZED, " &
                "   SSCP_AUDITEDENFORCEMENT.STRACTIONTYPE " &
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                " LEFT JOIN AIRBRANCH.APBFACILITYINFORMATION " &
                " ON APBFACILITYINFORMATION.STRAIRSNUMBER = SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER " &
                " LEFT JOIN AIRBRANCH.EPDUSERPROFILES " &
                " ON EPDUSERPROFILES.NUMUSERID = SSCP_AUDITEDENFORCEMENT.NUMSTAFFRESPONSIBLE " &
                " WHERE SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = :enforcementId "
            Dim parameter As New OracleParameter("enforcementId", enforcementId)
            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)

            Dim enfInfo As New EnforcementInfo
            FillEnforcementInfoFromDataRow(dataRow, enfInfo)
            Return enfInfo
        End Function

        Private Function EnforcementCaseFromDataRow(ByVal row As DataRow) As EnforcementCase
            Dim staff As New Staff
            With staff
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
            End With

            Dim enfCase As New EnforcementCase
            With enfCase
                .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .AoAppealed = DB.GetNullableDateTimeFromString(row("DATAOAPPEALED"))
                .AoComment = DB.GetNullable(Of String)(row("STRAOCOMMENT"))
                .AoExecuted = DB.GetNullableDateTimeFromString(row("DATAOEXECUTED"))
                .AoResolved = DB.GetNullableDateTimeFromString(row("DATAORESOLVED"))
                .AfsAoResolvedActionNumber = DB.GetNullable(Of Integer)(row("STRAFSAORESOLVEDNUMBER"))
                .AfsAoToAGActionNumber = DB.GetNullable(Of Integer)(row("STRAFSAOTOAGNUMBER"))
                .AfsCivilCourtActionNumber = DB.GetNullable(Of Integer)(row("STRAFSCIVILCOURTNUMBER"))
                .AfsCoActionNumber = DB.GetNullable(Of Integer)(row("STRAFSCOEXECUTEDNUMBER"))
                .CoComment = DB.GetNullable(Of String)(row("STRCOCOMMENT"))
                .CoExecuted = DB.GetNullableDateTimeFromString(row("DATCOEXECUTED"))
                .Comment = DB.GetNullable(Of String)(row("STRGENERALCOMMENTS"))
                .CoNumber = DB.GetNullable(Of String)(row("STRCONUMBER"))
                .CoPenaltyAmount = DB.GetNullable(Of Decimal)(row("STRCOPENALTYAMOUNT"))
                .CoPenaltyAmountComment = DB.GetNullable(Of String)(row("STRCOPENALTYAMOUNTCOMMENTS"))
                .CoProposed = DB.GetNullableDateTimeFromString(row("DATCOPROPOSED"))
                .AfsCoProposedNumber = DB.GetNullable(Of Integer?)(row("STRAFSCOPROPOSEDNUMBER"))
                .CoReceivedFromCompany = DB.GetNullableDateTimeFromString(row("DATCORECEIVEDFROMCOMPANY"))
                .CoReceivedFromDirector = DB.GetNullableDateTimeFromString(row("DATCORECEIVEDFROMDIRECTOR"))
                .CoResolved = DB.GetNullableDateTimeFromString(row("DATCORESOLVED"))
                .AfsCoResolvedActionNumber = DB.GetNullable(Of Integer)(row("STRAFSCORESOLVEDNUMBER"))
                .CoToPm = DB.GetNullableDateTimeFromString(row("DATCOTOPM"))
                .CoToUc = DB.GetNullableDateTimeFromString(row("DATCOTOUC"))
                .DateFinalized = DB.GetNullableDateTimeFromString(row("DATENFORCEMENTFINALIZED"))
                .DayZeroDate = DB.GetNullableDateTimeFromString(row("DATDAYZERO"))
                .DiscoveryDate = DB.GetNullableDateTimeFromString(row("DATDISCOVERYDATE"))
                .EnforcementId = DB.GetNullable(Of Integer)(row("STRENFORCEMENTNUMBER"))
                .EnforcementTypeCode = DB.GetNullable(Of String)(row("STRHPV"))
                .AfsKeyActionNumber = DB.GetNullable(Of Integer)(row("STRAFSKEYACTIONNUMBER"))
                .LinkedWorkItemId = DB.GetNullable(Of Integer)(row("STRTRACKINGNUMBER"))
                .LonComment = DB.GetNullable(Of String)(row("STRLONCOMMENTS"))
                .LonResolved = DB.GetNullableDateTimeFromString(row("DATLONRESOLVED"))
                .LonSent = DB.GetNullableDateTimeFromString(row("DATLONSENT"))
                .LonToUc = DB.GetNullableDateTimeFromString(row("DATLONTOUC"))
                .AfsNfaActionNumber = DB.GetNullable(Of Integer)(row("STRAFSNOVRESOLVEDNUMBER"))
                .NfaSent = DB.GetNullableDateTimeFromString(row("DATNFALETTERSENT"))
                .NfaToPm = DB.GetNullableDateTimeFromString(row("DATNFATOPM"))
                .NfaToUc = DB.GetNullableDateTimeFromString(row("DATNFATOUC"))
                .AfsNovActionNumber = DB.GetNullable(Of Integer)(row("STRAFSNOVSENTNUMBER"))
                .NovComment = DB.GetNullable(Of String)(row("STRNOVCOMMENT"))
                .NovResponseReceived = DB.GetNullableDateTimeFromString(row("DATNOVRESPONSERECEIVED"))
                .NovSent = DB.GetNullableDateTimeFromString(row("DATNOVSENT"))
                .NovToPm = DB.GetNullableDateTimeFromString(row("DATNOVTOPM"))
                .NovToUc = DB.GetNullableDateTimeFromString(row("DATNOVTOUC"))
                .Open = Not DB.GetNullable(Of Boolean)(row("STRENFORCEMENTFINALIZED"))
                .LegacyComplianceStatusCode = DB.GetNullable(Of String)(row("STRPOLLUTANTSTATUS"))
                .SubmittedToUcCode = DB.GetNullable(Of String)(row("STRSTATUS"))
                .ViolationType = DB.GetNullable(Of String)(row("STRACTIONTYPE"))
                .DateModified = DB.GetNullableDateTimeFromString(row("DATMODIFINGDATE"))

                .EnforcementActions = New List(Of EnforcementActionType)
                If .LegacyEnforcementType = LegacyEnforcementType.LON Or .LonSent IsNot Nothing Then
                    .EnforcementActions.Add(EnforcementActionType.LON)
                Else
                    If .NovComment <> "" Or AnyOfTheseDatesHasValue(New List(Of Date?) From { .NovSent, .NovToPm, .NovToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.NOV)
                    End If

                    If .CoComment <> "" Or .CoNumber <> "" Or AnyOfTheseDatesHasValue(New List(Of Date?) From { .CoExecuted, .CoProposed, .CoReceivedFromCompany, .CoReceivedFromDirector, .CoResolved, .CoToPm, .CoToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.CO)
                    End If

                    If .AoComment <> "" Or AnyOfTheseDatesHasValue(New List(Of Date?) From { .AoAppealed, .AoExecuted, .AoResolved}) Then
                        .EnforcementActions.Add(EnforcementActionType.AO)
                    End If
                End If

                .SubmittedToEpa = .AfsKeyActionNumber > 0

                .ComplianceStatus = ConvertComplianceStatus(enfCase.LegacyComplianceStatus)
            End With

            Return enfCase
        End Function

        Public Function GetEnforcementCase(ByVal enforcementId As String) As EnforcementCase
            If enforcementId = "" OrElse Not Integer.TryParse(enforcementId, Nothing) Then Return Nothing

            Dim query As String = "SELECT enf.*, " &
                "  up.STRFIRSTNAME, up.STRLASTNAME " &
                "FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT enf " &
                "LEFT JOIN AIRBRANCH.EPDUSERPROFILES up ON up.NUMUSERID = " &
                "  enf.NUMSTAFFRESPONSIBLE " &
                "WHERE enf.STRENFORCEMENTNUMBER = :enforcementId"
            Dim parameter As New OracleParameter("enforcementId", enforcementId)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return EnforcementCaseFromDataRow(dataTable.Rows(0))
        End Function

#End Region

#Region " Stipulated Penalties "

        Public Function GetStipulatedPenalties(enforcementId As String) As DataTable
            Dim query As String = "SELECT STRENFORCEMENTKEY, STRSTIPULATEDPENALTY, " &
                "  STRSTIPULATEDPENALTYCOMMENTS, STRAFSSTIPULATEDPENALTYNUMBER " &
                "FROM AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = :enforcementId " &
                "ORDER BY STRENFORCEMENTKEY"
            Dim parameter As New OracleParameter("enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetNextStipulatedPenaltyKey(enforcementId As String) As Integer
            Dim query As String = "SELECT MAX(es.STRENFORCEMENTKEY) " &
                "FROM AIRBRANCH.SSCPENFORCEMENTSTIPULATED es " &
                "WHERE es.STRENFORCEMENTNUMBER = :enforcementId"
            Dim parameter As New OracleParameter("enforcementId", enforcementId)
            Dim key As Int16 = DB.GetSingleValue(Of Int16)(query, parameter)
            Return key + 1
        End Function

        Public Function SaveNewStipulatedPenalty(enforcementId As String, airsNumber As Apb.ApbFacilityId,
                                                 penalty As Decimal, comment As String,
                                                 ByRef enfKey As Integer, ByRef afsKey As Integer) As Boolean
            enfKey = GetNextStipulatedPenaltyKey(enforcementId)
            afsKey = GetNextAfsActionNumber(airsNumber)

            Dim queries As New List(Of String)
            Dim parameters As New List(Of OracleParameter())

            queries.Add("UPDATE AIRBRANCH.APBSUPPLAMENTALDATA " &
                        "SET STRAFSACTIONNUMBER = :afs " &
                        "WHERE STRAIRSNUMBER = :airsNumber ")
            parameters.Add(New OracleParameter() {
                New OracleParameter("afs", afsKey + 1),
                New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            })

            queries.Add("INSERT " &
                        "INTO AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                        "  ( " &
                        "    STRENFORCEMENTNUMBER, STRENFORCEMENTKEY, " &
                        "    STRSTIPULATEDPENALTY, STRSTIPULATEDPENALTYCOMMENTS, " &
                        "    STRAFSSTIPULATEDPENALTYNUMBER, STRMODIFINGPERSON, " &
                        "    DATMODIFINGDATE " &
                        "  ) " &
                        "  VALUES " &
                        "  ( " &
                        "    :enforcementId, :enfKey, :penalty, :penaltyComment, " &
                        "    :afsKey, :userid, sysdate " &
                        "  ) ")
            parameters.Add(New OracleParameter() {
                New OracleParameter("enforcementId", enforcementId),
                New OracleParameter("enfKey", enfKey.ToString),
                New OracleParameter("penalty", penalty.ToString),
                New OracleParameter("penaltyComment", comment),
                New OracleParameter("afsKey", afsKey.ToString),
                New OracleParameter("userid", UserGCode)
            })

            Return DB.RunCommand(queries, parameters)
        End Function

        Public Function UpdateStipulatedPenalty(enforcementId As String, penalty As Decimal, comment As String, enfKey As Integer) As Boolean
            Dim query As String = "UPDATE AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                "SET STRSTIPULATEDPENALTY = :penalty, " &
                "  STRSTIPULATEDPENALTYCOMMENTS = :penaltyComment, " &
                "  STRMODIFINGPERSON = :userid, DATMODIFINGDATE = SYSDATE " &
                "WHERE STRENFORCEMENTNUMBER = :enforcementId AND STRENFORCEMENTKEY = " &
                "  :enfKey"
            Dim parameters As OracleParameter() = {
                New OracleParameter("penalty", penalty.ToString),
                New OracleParameter("penaltyComment", comment),
                New OracleParameter("userid", UserGCode),
                New OracleParameter("enforcementId", enforcementId),
                New OracleParameter("enfKey", enfKey)
            }
            Return DB.RunCommand(query, parameters)
        End Function

        Public Function DeleteStipulatedPenalty(enforcementId As String, enfKey As Integer) As Boolean
            Dim query As String = "DELETE " &
                "FROM AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = :enforcementId AND STRENFORCEMENTKEY = " &
                "  :enfKey"
            Dim parameters As OracleParameter() = {
                New OracleParameter("enforcementId", enforcementId),
                New OracleParameter("enfKey", enfKey)
            }
            Return DB.RunCommand(query, parameters)
        End Function

#End Region

#Region " Enforcement audit history "

        Public Function GetEnforcementAuditHistory(enforcementId As String) As DataTable
            Dim query As String = "SELECT(upr.STRLASTNAME || ', ' || upr.STRFIRSTNAME) AS " &
                "  StaffResponsible, enf.STRTRACKINGNUMBER, " &
                "  enf.DATENFORCEMENTFINALIZED, enf.STRSTATUS, enf.STRACTIONTYPE " &
                "  , enf.STRGENERALCOMMENTS, enf.DATDISCOVERYDATE, " &
                "  enf.DATDAYZERO, enf.STRHPV, enf.STRPOLLUTANTS, " &
                "  enf.STRPOLLUTANTSTATUS, enf.DATLONTOUC, enf.DATLONSENT, " &
                "  enf.DATLONRESOLVED, enf.STRLONCOMMENTS, enf.DATNOVTOUC, " &
                "  enf.DATNOVTOPM, enf.DATNOVSENT, enf.DATNOVRESPONSERECEIVED, " &
                "  enf.DATNFATOUC, enf.DATNFATOPM, enf.DATNFALETTERSENT, " &
                "  enf.STRNOVCOMMENT, enf.DATCOTOUC, enf.DATCOTOPM, " &
                "  enf.DATCOPROPOSED, enf.DATCORECEIVEDFROMCOMPANY, " &
                "  enf.DATCORECEIVEDFROMDIRECTOR, enf.DATCOEXECUTED, " &
                "  enf.STRCONUMBER, enf.DATCORESOLVED, enf.STRCOCOMMENT, " &
                "  enf.STRCOPENALTYAMOUNT, enf.STRCOPENALTYAMOUNTCOMMENTS, " &
                "  enf.DATAOEXECUTED, enf.DATAOAPPEALED, enf.DATAORESOLVED, " &
                "  enf.STRAOCOMMENT,(upm.STRLASTNAME || ', ' || upm.STRFIRSTNAME " &
                "  ) AS ModifiedBy, enf.DATMODIFINGDATE " &
                "FROM AIRBRANCH.SSCP_ENFORCEMENT enf " &
                "LEFT JOIN AIRBRANCH.EPDUSERPROFILES upr ON " &
                "  enf.NUMSTAFFRESPONSIBLE = upr.NUMUSERID " &
                "LEFT JOIN AIRBRANCH.EPDUSERPROFILES upm ON " &
                "  enf.STRMODIFINGPERSON = upm.NUMUSERID " &
                "WHERE enf.STRENFORCEMENTNUMBER = :enforcementId"
            Dim parameter As New OracleParameter("enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Pollutants/Programs "

        Public Function GetEnforcementPollutants(enforcementId As String) As String()
            Dim p As String() = GetEnforcementProgPoll(enforcementId)
            For i As Integer = 0 To p.Length - 1
                p(i) = p(i).Substring(1)
            Next
            Return p.Distinct.ToArray
        End Function

        Public Function GetEnforcementPrograms(enforcementId As String) As String()
            Dim p As String() = GetEnforcementProgPoll(enforcementId)
            For i As Integer = 0 To p.Length - 1
                p(i) = p(i).Substring(0, 1)
            Next
            Return p.Distinct.ToArray
        End Function

        Private Function GetEnforcementProgPoll(enforcementId As String) As String()
            Dim query As String = "SELECT STRPOLLUTANTS FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT WHERE STRENFORCEMENTNUMBER = :enforcementId"
            Dim parameter As New OracleParameter("enforcementId", enforcementId)
            Dim pollString As String = DB.GetSingleValue(Of String)(query, parameter)
            Return pollString.Split({","c}, StringSplitOptions.RemoveEmptyEntries)
        End Function

#End Region

#Region " Lookup table "

        Public Function GetViolationTypes() As DataTable
            Dim query As String =
                "SELECT AIRVIOLATIONTYPECODE, VIOLATIONTYPEDESC, SEVERITYCODE " &
                " , POLLUTANTREQUIRED, DEPRECATED " &
                "FROM AIRBRANCH.LK_VIOLATION_TYPE " &
                "WHERE STATUS = 'A' "
            Dim dt As DataTable = DB.GetDataTable(query)

            Dim emptyRow As DataRow = dt.NewRow
            emptyRow("AIRVIOLATIONTYPECODE") = "BLANK"
            emptyRow("VIOLATIONTYPEDESC") = " "
            emptyRow("SEVERITYCODE") = "BLANK"
            dt.Rows.Add(emptyRow)

            Return dt
        End Function

#End Region

#Region " Utilities "

        Private Function ConvertComplianceStatus(legacyComplianceStatus As LegacyComplianceStatus) As ComplianceStatus
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

        Private Function AnyOfTheseDatesHasValue(dates As List(Of Date?)) As Boolean
            For Each d As Date? In dates
                If d.HasValue Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

    End Module

End Namespace
