Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports GaEpd.DBUtilities
Imports Iaip.Apb.Sscp

Namespace DAL.Sscp

    Module EnforcementData

#Region " Get info "

        ''' <summary>
        ''' Returns a count of open enforcement cases for the specified facility
        ''' </summary>
        ''' <param name="airs">The Facility ID.</param>
        ''' <returns>An Integer count of open enforcement cases.</returns>
        Public Function GetOpenEnforcementCountForFacility(airs As Apb.ApbFacilityId) As Integer
            Dim query As String = "SELECT COUNT(*) " &
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRENFORCEMENTFINALIZED = 'False' " &
                " AND STRAIRSNUMBER = @airs " &
                " and (IsDeleted = 0 or IsDeleted is null) "

            Dim parameter As New SqlParameter("@airs", airs.DbFormattedString)

            Return DB.GetInteger(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a boolean indicating where an enforcement ID exists.
        ''' </summary>
        ''' <param name="enforcementId">The enforcement ID to check.</param>
        ''' <returns>A boolean indicating where the enforcement ID exists.</returns>
        Public Function EnforcementExists(enforcementId As String) As Boolean
            If String.IsNullOrEmpty(enforcementId) OrElse Not Integer.TryParse(enforcementId, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRENFORCEMENTNUMBER = @enforcementId "
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a boolean indicating whether a tracking number is associated with any enforcement cases.
        ''' </summary>
        ''' <param name="trackingNumber">The tracking number to check.</param>
        ''' <returns>A boolean indicating where enforcement exists for the given tracking number.</returns>
        Public Function TrackingNumberHasEnforcement(trackingNumber As Integer) As Boolean
            Dim query As String = "SELECT CONVERT(BIT, COUNT(*))
                FROM SSCP_EnforcementEvents
                WHERE TrackingNumber = @trackingNumber"
            Dim parameter As New SqlParameter("@trackingNumber", trackingNumber)
            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Retrieves all enforcement cases for a given SSCP work item.
        ''' </summary>
        ''' <param name="trackingNumber">The tracking number of the SSCP work item.</param>
        ''' <returns>Datatable of all enforcement cases for the work item.</returns>
        Public Function GetAllEnforcementForTrackingNumber(trackingNumber As Integer) As DataTable
            Dim query As String = "SELECT EnforcementNumber
                FROM SSCP_EnforcementEvents
                WHERE TrackingNumber = @trackingNumber
                ORDER BY EnforcementNumber"
            Dim parameter As New SqlParameter("@trackingNumber", trackingNumber)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Get enforcement info "

        Private Function GetEnforcementInfoFromDataRow(row As DataRow) As EnforcementInfo
            Dim address As New Address
            With address
                .City = GetNullableString(row("STRFACILITYCITY"))
                .State = GetNullableString(row("STRFACILITYSTATE"))
            End With

            Dim location As New Location
            With location
                .Address = address
            End With

            Dim facility As New Apb.Facilities.Facility
            With facility
                .AirsNumber = New Apb.ApbFacilityId(GetNullableString(row("STRAIRSNUMBER")))
                .FacilityName = GetNullableString(row("STRFACILITYNAME"))
                .FacilityLocation = location
            End With

            Dim staff As New IaipUser
            With staff
                .FirstName = GetNullableString(row("STRFIRSTNAME"))
                .LastName = GetNullableString(row("STRLASTNAME"))
            End With

            Dim enforcementInfo As New EnforcementInfo
            With enforcementInfo
                .DiscoveryDate = GetNullable(Of Date?)(row("DATDISCOVERYDATE"))
                .DateFinalized = GetNullable(Of Date?)(row("DATENFORCEMENTFINALIZED"))
                .Open = Not Convert.ToBoolean(row("STRENFORCEMENTFINALIZED"))
                .EnforcementNumber = CInt(row("STRENFORCEMENTNUMBER"))
                .EnforcementTypeCode = GetNullableString(row("STRACTIONTYPE"))
                .Facility = facility
                .StaffResponsible = staff

                If IsDBNull(row("IsDeleted")) Then
                    .IsDeleted = False
                Else
                    .IsDeleted = Convert.ToBoolean(row("IsDeleted"))
                End If
            End With

            Return enforcementInfo
        End Function

        Public Function GetEnforcementInfo(enforcementId As String) As EnforcementInfo
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
                "   SSCP_AUDITEDENFORCEMENT.STRACTIONTYPE, " &
                "   SSCP_AUDITEDENFORCEMENT.IsDeleted " &
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " LEFT JOIN APBFACILITYINFORMATION " &
                " ON APBFACILITYINFORMATION.STRAIRSNUMBER = SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER " &
                " LEFT JOIN EPDUSERPROFILES " &
                " ON EPDUSERPROFILES.NUMUSERID = SSCP_AUDITEDENFORCEMENT.NUMSTAFFRESPONSIBLE " &
                " WHERE SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = @enforcementId "

            Dim parameter As New SqlParameter("@enforcementId", enforcementId)

            Dim dataRow As DataRow = DB.GetDataRow(query, parameter)

            Return GetEnforcementInfoFromDataRow(dataRow)
        End Function

        Private Function EnforcementCaseFromDataRow(row As DataRow) As EnforcementCase

            Dim enfCase As New EnforcementCase
            With enfCase
                .StaffResponsibleId = GetNullable(Of Integer)(row("NUMSTAFFRESPONSIBLE"))
                .AirsNumber = New Apb.ApbFacilityId(GetNullableString(row("STRAIRSNUMBER")))
                .AoAppealed = GetNullableDateTime(row("DATAOAPPEALED"))
                .AoComment = GetNullableString(row("STRAOCOMMENT"))
                .AoExecuted = GetNullableDateTime(row("DATAOEXECUTED"))
                .AoResolved = GetNullableDateTime(row("DATAORESOLVED"))
                .AfsAoResolvedActionNumber = GetNullable(Of Integer)(row("STRAFSAORESOLVEDNUMBER"))
                .AfsAoToAGActionNumber = GetNullable(Of Integer)(row("STRAFSAOTOAGNUMBER"))
                .AfsCivilCourtActionNumber = GetNullable(Of Integer)(row("STRAFSCIVILCOURTNUMBER"))
                .AfsCoActionNumber = GetNullable(Of Integer)(row("STRAFSCOEXECUTEDNUMBER"))
                .CoComment = GetNullableString(row("STRCOCOMMENT"))
                .CoExecuted = GetNullableDateTime(row("DATCOEXECUTED"))
                .Comment = GetNullableString(row("STRGENERALCOMMENTS"))
                .CoNumber = GetNullableString(row("STRCONUMBER"))
                .CoPenaltyAmount = GetNullable(Of Decimal)(row("STRCOPENALTYAMOUNT"))
                .CoPenaltyAmountComment = GetNullableString(row("STRCOPENALTYAMOUNTCOMMENTS"))
                .CoProposed = GetNullableDateTime(row("DATCOPROPOSED"))
                .AfsCoProposedNumber = GetNullable(Of Integer)(row("STRAFSCOPROPOSEDNUMBER"))
                .CoReceivedFromCompany = GetNullableDateTime(row("DATCORECEIVEDFROMCOMPANY"))
                .CoReceivedFromDirector = GetNullableDateTime(row("DATCORECEIVEDFROMDIRECTOR"))
                .CoResolved = GetNullableDateTime(row("DATCORESOLVED"))
                .AfsCoResolvedActionNumber = GetNullable(Of Integer)(row("STRAFSCORESOLVEDNUMBER"))
                .CoToPm = GetNullableDateTime(row("DATCOTOPM"))
                .CoToUc = GetNullableDateTime(row("DATCOTOUC"))
                .DateFinalized = GetNullableDateTime(row("DATENFORCEMENTFINALIZED"))
                .DayZeroDate = GetNullableDateTime(row("DATDAYZERO"))
                .DiscoveryDate = GetNullableDateTime(row("DATDISCOVERYDATE"))
                .EnforcementId = GetNullable(Of Integer)(row("STRENFORCEMENTNUMBER"))
                .ViolationType = GetNullableString(row("STRHPV"))
                .AfsKeyActionNumber = GetNullable(Of Integer)(row("STRAFSKEYACTIONNUMBER"))
                .LonComment = GetNullableString(row("STRLONCOMMENTS"))
                .LonResolved = GetNullableDateTime(row("DATLONRESOLVED"))
                .LonSent = GetNullableDateTime(row("DATLONSENT"))
                .LonToUc = GetNullableDateTime(row("DATLONTOUC"))
                .AfsNfaActionNumber = GetNullable(Of Integer)(row("STRAFSNOVRESOLVEDNUMBER"))
                .NfaSent = GetNullableDateTime(row("DATNFALETTERSENT"))
                .NfaToPm = GetNullableDateTime(row("DATNFATOPM"))
                .NfaToUc = GetNullableDateTime(row("DATNFATOUC"))
                .AfsNovActionNumber = GetNullable(Of Integer)(row("STRAFSNOVSENTNUMBER"))
                .NovComment = GetNullableString(row("STRNOVCOMMENT"))
                .NovResponseReceived = GetNullableDateTime(row("DATNOVRESPONSERECEIVED"))
                .NovSent = GetNullableDateTime(row("DATNOVSENT"))
                .NovToPm = GetNullableDateTime(row("DATNOVTOPM"))
                .NovToUc = GetNullableDateTime(row("DATNOVTOUC"))
                .Open = CType(Not GetNullable(Of Boolean)(row("STRENFORCEMENTFINALIZED")), OpenOrClosed)
                .ProgramPollutants = GetNullableString(row("STRPOLLUTANTS"))
                .SubmittedToUcCode = GetNullableString(row("STRSTATUS"))
                .DateModified = GetNullableDateTime(row("DATMODIFINGDATE"))

                If IsDBNull(row("IsDeleted")) Then
                    .IsDeleted = False
                Else
                    .IsDeleted = Convert.ToBoolean(row("IsDeleted"))
                End If

                .EnforcementActions = New List(Of EnforcementActionType)
                If .LonComment <> "" OrElse AnyOfTheseDatesHasValue({ .LonResolved, .LonSent, .LonToUc}) Then
                    .EnforcementActions.Add(EnforcementActionType.LON)
                Else
                    If .NovComment <> "" OrElse AnyOfTheseDatesHasValue({ .NovSent, .NovToPm, .NovToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.NOV)
                    End If

                    If .CoComment <> "" OrElse .CoNumber <> "" OrElse AnyOfTheseDatesHasValue({ .CoExecuted, .CoProposed, .CoReceivedFromCompany, .CoReceivedFromDirector, .CoResolved, .CoToPm, .CoToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.CO)
                    End If

                    If .AoComment <> "" OrElse AnyOfTheseDatesHasValue({ .AoAppealed, .AoExecuted, .AoResolved}) Then
                        .EnforcementActions.Add(EnforcementActionType.AO)
                    End If
                End If

                .SubmittedToEpa = .AfsKeyActionNumber > 0
            End With

            Return enfCase
        End Function

        Public Function GetEnforcementCase(enforcementId As Integer) As EnforcementCase
            If enforcementId = 0 Then Return Nothing

            Dim query As String = "SELECT enf.*, " &
                "  up.STRFIRSTNAME, up.STRLASTNAME " &
                "FROM SSCP_AUDITEDENFORCEMENT enf " &
                "LEFT JOIN EPDUSERPROFILES up ON up.NUMUSERID = " &
                "  enf.NUMSTAFFRESPONSIBLE " &
                "WHERE enf.STRENFORCEMENTNUMBER = @enforcementId"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return EnforcementCaseFromDataRow(dataTable.Rows(0))
        End Function

#End Region

#Region " Save enforcement "

        Public Function SaveEnforcement(enforcementCase As EnforcementCase) As Integer
            Dim spName As String = "dbo.PD_EnforcementUpdate"

            With enforcementCase
                Dim params As SqlParameter() = {
                    New SqlParameter("@STRENFORCEMENTNUMBER", .EnforcementId),
                    New SqlParameter("@STRAIRSNUMBER", .AirsNumber.DbFormattedString),
                    New SqlParameter("@STRENFORCEMENTFINALIZED", .DateFinalized.HasValue.ToString),
                    New SqlParameter("@DATENFORCEMENTFINALIZED", .DateFinalized),
                    New SqlParameter("@NUMSTAFFRESPONSIBLE", .StaffResponsibleId),
                    New SqlParameter("@STRSTATUS", .SubmittedToUcCode),
                    New SqlParameter("@STRACTIONTYPE", .EnforcementType.ToString),
                    New SqlParameter("@STRGENERALCOMMENTS", .Comment),
                    New SqlParameter("@STRDISCOVERYDATE", .DiscoveryDate.HasValue.ToString),
                    New SqlParameter("@DATDISCOVERYDATE", .DiscoveryDate),
                    New SqlParameter("@STRDAYZERO", .DayZeroDate.HasValue.ToString),
                    New SqlParameter("@DATDAYZERO", .DayZeroDate),
                    New SqlParameter("@STRHPV", If(.ViolationType = "BLANK", "", .ViolationType)),
                    New SqlParameter("@STRPOLLUTANTS", .ProgramPollutants),
                    New SqlParameter("@STRLONTOUC", .LonToUc.HasValue.ToString),
                    New SqlParameter("@DATLONTOUC", .LonToUc),
                    New SqlParameter("@STRLONSENT", .LonSent.HasValue.ToString),
                    New SqlParameter("@DATLONSENT", .LonSent),
                    New SqlParameter("@STRLONRESOLVED", .LonResolved.HasValue.ToString),
                    New SqlParameter("@DATLONRESOLVED", .LonResolved),
                    New SqlParameter("@STRLONCOMMENTS", .LonComment),
                    New SqlParameter("@STRNOVTOUC", .NovToUc.HasValue.ToString),
                    New SqlParameter("@DATNOVTOUC", .NovToUc),
                    New SqlParameter("@STRNOVTOPM", .NovToPm.HasValue.ToString),
                    New SqlParameter("@DATNOVTOPM", .NovToPm),
                    New SqlParameter("@STRNOVSENT", .NovSent.HasValue.ToString),
                    New SqlParameter("@DATNOVSENT", .NovSent),
                    New SqlParameter("@STRNOVRESPONSERECEIVED", .NovResponseReceived.HasValue.ToString),
                    New SqlParameter("@DATNOVRESPONSERECEIVED", .NovResponseReceived),
                    New SqlParameter("@STRNFATOUC", .NfaToUc.HasValue.ToString),
                    New SqlParameter("@DATNFATOUC", .NfaToUc),
                    New SqlParameter("@STRNFATOPM", .NfaToPm.HasValue.ToString),
                    New SqlParameter("@DATNFATOPM", .NfaToPm),
                    New SqlParameter("@STRNFALETTERSENT", .NfaSent.HasValue.ToString),
                    New SqlParameter("@DATNFALETTERSENT", .NfaSent),
                    New SqlParameter("@STRNOVCOMMENT", .NovComment),
                    New SqlParameter("@STRCOTOUC", .CoToUc.HasValue.ToString),
                    New SqlParameter("@DATCOTOUC", .CoToUc),
                    New SqlParameter("@STRCOTOPM", .CoToPm.HasValue.ToString),
                    New SqlParameter("@DATCOTOPM", .CoToPm),
                    New SqlParameter("@STRCOPROPOSED", .CoProposed.HasValue.ToString),
                    New SqlParameter("@DATCOPROPOSED", .CoProposed),
                    New SqlParameter("@STRCORECEIVEDFROMCOMPANY", .CoReceivedFromCompany.HasValue.ToString),
                    New SqlParameter("@DATCORECEIVEDFROMCOMPANY", .CoReceivedFromCompany),
                    New SqlParameter("@STRCORECEIVEDFROMDIRECTOR", .CoReceivedFromDirector.HasValue.ToString),
                    New SqlParameter("@DATCORECEIVEDFROMDIRECTOR", .CoReceivedFromDirector),
                    New SqlParameter("@STRCOEXECUTED", .CoExecuted.HasValue.ToString),
                    New SqlParameter("@DATCOEXECUTED", .CoExecuted),
                    New SqlParameter("@STRCONUMBER", .CoNumber),
                    New SqlParameter("@STRCORESOLVED", .CoResolved.HasValue.ToString),
                    New SqlParameter("@DATCORESOLVED", .CoResolved),
                    New SqlParameter("@STRCOPENALTYAMOUNT", StoreNothingIfZero(.CoPenaltyAmount)),
                    New SqlParameter("@STRCOPENALTYAMOUNTCOMMENTS", .CoPenaltyAmountComment),
                    New SqlParameter("@STRCOCOMMENT", .CoComment),
                    New SqlParameter("@STRAOEXECUTED", .AoExecuted.HasValue.ToString),
                    New SqlParameter("@DATAOEXECUTED", .AoExecuted),
                    New SqlParameter("@STRAOAPPEALED", .AoAppealed.HasValue.ToString),
                    New SqlParameter("@DATAOAPPEALED", .AoAppealed),
                    New SqlParameter("@STRAORESOLVED", .AoResolved.HasValue.ToString),
                    New SqlParameter("@DATAORESOLVED", .AoResolved),
                    New SqlParameter("@STRAOCOMMENT", .AoComment),
                    New SqlParameter("@STRAFSKEYACTIONNUMBER", StoreNothingIfZero(.AfsKeyActionNumber)),
                    New SqlParameter("@STRAFSNOVSENTNUMBER", StoreNothingIfZero(.AfsNovActionNumber)),
                    New SqlParameter("@STRAFSNOVRESOLVEDNUMBER", StoreNothingIfZero(.AfsNfaActionNumber)),
                    New SqlParameter("@STRAFSCOPROPOSEDNUMBER", StoreNothingIfZero(.AfsCoProposedNumber)),
                    New SqlParameter("@STRAFSCOEXECUTEDNUMBER", StoreNothingIfZero(.AfsCoActionNumber)),
                    New SqlParameter("@STRAFSCORESOLVEDNUMBER", StoreNothingIfZero(.AfsCoResolvedActionNumber)),
                    New SqlParameter("@STRAFSAOTOAGNUMBER", StoreNothingIfZero(.AfsAoToAGActionNumber)),
                    New SqlParameter("@STRAFSCIVILCOURTNUMBER", StoreNothingIfZero(.AfsCivilCourtActionNumber)),
                    New SqlParameter("@STRAFSAORESOLVEDNUMBER", StoreNothingIfZero(.AfsAoResolvedActionNumber)),
                    New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID)
                }

                Return DB.SPGetSingleValue(Of Integer)(spName, params)
            End With
        End Function

#End Region

#Region " Delete enforcement "

        Public Function DeleteEnforcement(enforcementId As Integer) As Boolean
            Dim spName As String = "dbo.PD_EnforcementDelete"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@EnforcementId", enforcementId),
                New SqlParameter("@ModifiedBy", CurrentUser.UserID)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

#End Region

#Region " Linked compliance events "

        Public Function GetLinkedComplianceEvents(enforcementId As Integer) As DataTable
            Dim query As String = "SELECT
                    TrackingNumber AS [Tracking #],
                    lk.STRACTIVITYNAME  AS [Type],
                    im.DATRECEIVEDDATE  AS [Event Date],
                    im.STRAIRSNUMBER    AS [AIRS #],
                    CreatedDate         AS [Date Linked]
                FROM SSCP_EnforcementEvents ee
                    INNER JOIN SSCPITEMMASTER im
                        ON im.STRTRACKINGNUMBER = ee.TrackingNumber
                    INNER JOIN LOOKUPCOMPLIANCEACTIVITIES AS lk
                        ON im.STREVENTTYPE = lk.STRACTIVITYTYPE
                WHERE EnforcementNumber = @enforcementId
                ORDER BY [Date Linked] ASC"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function SaveLinkedComplianceEvent(enforcementId As Integer, trackingNumber As Integer) As Boolean
            Dim query As String = "INSERT INTO SSCP_EnforcementEvents (EnforcementNumber, TrackingNumber, CreatedBy)
                SELECT
                    @enforcementId,
                    @trackingNumber,
                    @userId
                WHERE NOT EXISTS(
                    SELECT 1
                    FROM SSCP_EnforcementEvents
                    WHERE EnforcementNumber = @enforcementId
                          AND TrackingNumber = @trackingNumber)"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@trackingNumber", trackingNumber),
                New SqlParameter("@userId", CurrentUser.UserID)
            }
            Return DB.RunCommand(query, parameters)
        End Function

        Public Function DeleteLinkedComplianceEvent(enforcementId As Integer, trackingNumber As Integer) As Boolean
            Dim query As String = "DELETE FROM SSCP_EnforcementEvents
                WHERE EnforcementNumber = @enforcementId
                      AND TrackingNumber = @trackingNumber"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@trackingNumber", trackingNumber)
            }
            Return DB.RunCommand(query, parameters)
        End Function

#End Region

#Region " Stipulated Penalties "

        Public Function GetStipulatedPenalties(enforcementId As Integer) As DataTable
            Dim query As String = "SELECT STRENFORCEMENTKEY, STRSTIPULATEDPENALTY, " &
                " CASE WHEN STRSTIPULATEDPENALTYCOMMENTS IS NULL THEN 'N/A' " &
                "  ELSE STRSTIPULATEDPENALTYCOMMENTS END AS STRSTIPULATEDPENALTYCOMMENTS, " &
                " STRAFSSTIPULATEDPENALTYNUMBER " &
                "FROM SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = @enforcementId " &
                "ORDER BY STRENFORCEMENTKEY"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetNextStipulatedPenaltyKey(enforcementId As Integer) As Integer
            Dim query As String = "SELECT MAX(STRENFORCEMENTKEY) " &
                "FROM SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = @enforcementId"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Dim key As Integer = DB.GetInteger(query, parameter)
            Return key + 1
        End Function

        Public Function SaveNewStipulatedPenalty(enforcementId As Integer, airsNumber As Apb.ApbFacilityId,
                                                 penalty As Decimal, comment As String) As Boolean
            Dim enfKey As Integer = GetNextStipulatedPenaltyKey(enforcementId)
            Dim afsKey As Integer = GetNextAfsActionNumber(airsNumber)

            Dim queries As New List(Of String)
            Dim parameters As New List(Of SqlParameter())

            queries.Add("UPDATE APBSUPPLAMENTALDATA " &
                        "SET STRAFSACTIONNUMBER = @afs " &
                        "WHERE STRAIRSNUMBER = @airsNumber ")
            parameters.Add({
                New SqlParameter("@afs", afsKey + 1),
                New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            })

            queries.Add("INSERT " &
                        "INTO SSCPENFORCEMENTSTIPULATED " &
                        "  ( " &
                        "    STRENFORCEMENTNUMBER, STRENFORCEMENTKEY, " &
                        "    STRSTIPULATEDPENALTY, STRSTIPULATEDPENALTYCOMMENTS, " &
                        "    STRAFSSTIPULATEDPENALTYNUMBER, STRMODIFINGPERSON, " &
                        "    DATMODIFINGDATE " &
                        "  ) " &
                        "  VALUES " &
                        "  ( " &
                        "    @enforcementId, @enfKey, @penalty, @penaltyComment, " &
                        "    @afsKey, @userid, GETDATE() " &
                        "  ) ")
            parameters.Add({
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@enfKey", enfKey.ToString),
                New SqlParameter("@penalty", penalty.ToString),
                New SqlParameter("@penaltyComment", comment),
                New SqlParameter("@afsKey", afsKey.ToString),
                New SqlParameter("@userid", CurrentUser.UserID)
            })

            Return DB.RunCommand(queries, parameters)
        End Function

        Public Function UpdateStipulatedPenalty(enforcementId As Integer, penalty As Decimal, comment As String, enfKey As Integer) As Boolean
            Dim query As String = "UPDATE SSCPENFORCEMENTSTIPULATED " &
                "SET STRSTIPULATEDPENALTY = @penalty, " &
                "  STRSTIPULATEDPENALTYCOMMENTS = @penaltyComment, " &
                "  STRMODIFINGPERSON = @userid, DATMODIFINGDATE = GETDATE() " &
                "WHERE STRENFORCEMENTNUMBER = @enforcementId AND " &
                "  STRENFORCEMENTKEY = @enfKey"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@penalty", penalty.ToString),
                New SqlParameter("@penaltyComment", comment),
                New SqlParameter("@userid", CurrentUser.UserID),
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@enfKey", enfKey)
            }
            Return DB.RunCommand(query, parameters)
        End Function

        Public Function DeleteStipulatedPenalty(enforcementId As Integer, enfKey As Integer) As Boolean
            Dim query As String = "DELETE " &
                "FROM SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = @enforcementId AND " &
                "  STRENFORCEMENTKEY = @enfKey"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@enfKey", enfKey)
            }
            Return DB.RunCommand(query, parameters)
        End Function

#End Region

#Region " Enforcement audit history "

        Public Function GetEnforcementAuditHistory(enforcementId As Integer) As DataTable
            Dim query As String = "SELECT ID, enf.DATMODIFINGDATE, " &
                "  CONCAT(upm.STRLASTNAME, ', ', upm.STRFIRSTNAME) AS ModifiedBy, " &
                "  CONCAT(upr.STRLASTNAME, ', ', upr.STRFIRSTNAME) AS " &
                "  StaffResponsible, enf.STRTRACKINGNUMBER, " &
                "  enf.DATENFORCEMENTFINALIZED, enf.STRSTATUS, enf.STRACTIONTYPE, " &
                "  enf.STRGENERALCOMMENTS, enf.DATDISCOVERYDATE, " &
                "  enf.DATDAYZERO, enf.STRHPV, enf.STRPOLLUTANTS, " &
                "  enf.DATLONTOUC, enf.DATLONSENT, " &
                "  enf.DATLONRESOLVED, enf.STRLONCOMMENTS, enf.DATNOVTOUC, " &
                "  enf.DATNOVTOPM, enf.DATNOVSENT, enf.DATNOVRESPONSERECEIVED, " &
                "  enf.DATNFATOUC, enf.DATNFATOPM, enf.DATNFALETTERSENT, " &
                "  enf.STRNOVCOMMENT, enf.DATCOTOUC, enf.DATCOTOPM, " &
                "  enf.DATCOPROPOSED, enf.DATCORECEIVEDFROMCOMPANY, " &
                "  enf.DATCORECEIVEDFROMDIRECTOR, enf.DATCOEXECUTED, " &
                "  enf.STRCONUMBER, enf.DATCORESOLVED, enf.STRCOCOMMENT, " &
                "  enf.STRCOPENALTYAMOUNT, enf.STRCOPENALTYAMOUNTCOMMENTS, " &
                "  enf.DATAOEXECUTED, enf.DATAOAPPEALED, enf.DATAORESOLVED, " &
                "  enf.STRAOCOMMENT, IsDeleted " &
                "FROM SSCP_ENFORCEMENT enf " &
                "LEFT JOIN EPDUSERPROFILES upr ON " &
                "  enf.NUMSTAFFRESPONSIBLE = upr.NUMUSERID " &
                "LEFT JOIN EPDUSERPROFILES upm ON " &
                "  enf.STRMODIFINGPERSON = upm.NUMUSERID " &
                "WHERE enf.STRENFORCEMENTNUMBER = @enforcementId " &
                " order by id desc "
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Lookup table "

        Public Function GetViolationTypes() As DataTable
            Dim query As String =
            "SELECT AIRVIOLATIONTYPECODE, VIOLATIONTYPEDESC, SEVERITYCODE " &
            " , POLLUTANTREQUIRED, DEPRECATED " &
            "FROM LK_VIOLATION_TYPE " &
            "WHERE STATUS = 'A' "
            Dim dt As DataTable = DB.GetDataTable(query)

            Dim dr As DataRow = dt.NewRow
            dr("AIRVIOLATIONTYPECODE") = "BLANK"
            dr("VIOLATIONTYPEDESC") = " "
            dr("SEVERITYCODE") = "BLANK"
            dr("POLLUTANTREQUIRED") = "FALSE"
            dr("DEPRECATED") = "TRUE"
            dt.Rows.Add(dr)

            dt.PrimaryKey = {dt.Columns("AIRVIOLATIONTYPECODE")}
            Return dt
        End Function

#End Region

    End Module

End Namespace
