Imports System.Collections.Generic
Imports Iaip.Apb.Sscp
Imports System.Data.SqlClient
Imports EpdIt

Namespace DAL.Sscp

    Module EnforcementData

#Region " Summary tables "

        Public Function GetEnforcementCountForFacility(airs As Apb.ApbFacilityId) As Integer
            Dim query As String = "SELECT COUNT(*) " &
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRENFORCEMENTFINALIZED = 'False' " &
                " AND STRAIRSNUMBER = @airs"
            Dim parameter As SqlParameter = New SqlParameter("@airs", airs.DbFormattedString)
            Return DB.GetSingleValue(Of Integer)(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data for a given facility for a given date range.
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
                "SELECT * FROM VW_SSCP_ENFORCEMENT_SUMMARY " &
                " WHERE TRUNC(EnforcementDate) BETWEEN @datestart AND @dateend "

            If airs IsNot Nothing Then query &= " AND STRAIRSNUMBER = @airs "
            If Not String.IsNullOrEmpty(staffId) Then query &= " AND NUMSTAFFRESPONSIBLE = @staffId "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@datestart", dateRangeStart),
                New SqlParameter("@dateend", dateRangeEnd),
                New SqlParameter("@airs", airs.DbFormattedString),
                New SqlParameter("@staffId", staffId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region " Exists "

        Public Function EnforcementExists(ByVal enforcementId As String) As Boolean
            If enforcementId = "" OrElse Not Integer.TryParse(enforcementId, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " WHERE RowNum = 1 " &
                " AND STRENFORCEMENTNUMBER = @enforcementId "
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function EnforcementExistsForTrackingNumber(ByVal trackingNumber As String, ByRef enfNumber As String) As Boolean
            If trackingNumber = "" OrElse Not Integer.TryParse(trackingNumber, Nothing) Then Return False

            Dim query As String = " SELECT STRENFORCEMENTNUMBER " &
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRTRACKINGNUMBER = @trackingNumber "
            Dim parameter As New SqlParameter("@trackingNumber", trackingNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)

            If result Is Nothing Then
                Return False
            Else
                enfNumber = result
                Return True
            End If
        End Function

#End Region

#Region " Get enforcement info "

        Private Sub FillEnforcementInfoFromDataRow(ByVal row As DataRow, ByRef enfInfo As EnforcementInfo)
            Dim address As New Address
            With address
                .City = DBUtilities.GetNullable(Of String)(row("STRFACILITYCITY"))
                .State = DBUtilities.GetNullable(Of String)(row("STRFACILITYSTATE"))
            End With

            Dim location As New Location
            With location
                .Address = address
            End With

            Dim facility As New Apb.Facilities.Facility
            With facility
                .AirsNumber = DBUtilities.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .FacilityName = DBUtilities.GetNullable(Of String)(row("STRFACILITYNAME"))
                .FacilityLocation = location
            End With

            Dim staff As New IaipUser
            With staff
                .FirstName = DBUtilities.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DBUtilities.GetNullable(Of String)(row("STRLASTNAME"))
            End With

            With enfInfo
                .DiscoveryDate = DBUtilities.GetNullable(Of Date?)(row("DATDISCOVERYDATE"))
                .DateFinalized = DBUtilities.GetNullable(Of Date?)(row("DATENFORCEMENTFINALIZED"))
                .Open = Not Convert.ToBoolean(row("STRENFORCEMENTFINALIZED"))
                .EnforcementNumber = row("STRENFORCEMENTNUMBER")
                .EnforcementTypeCode = DBUtilities.GetNullable(Of String)(row("STRACTIONTYPE"))
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
                " FROM SSCP_AUDITEDENFORCEMENT " &
                " LEFT JOIN APBFACILITYINFORMATION " &
                " ON APBFACILITYINFORMATION.STRAIRSNUMBER = SSCP_AUDITEDENFORCEMENT.STRAIRSNUMBER " &
                " LEFT JOIN EPDUSERPROFILES " &
                " ON EPDUSERPROFILES.NUMUSERID = SSCP_AUDITEDENFORCEMENT.NUMSTAFFRESPONSIBLE " &
                " WHERE SSCP_AUDITEDENFORCEMENT.STRENFORCEMENTNUMBER = @enforcementId "
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)

            Dim enfInfo As New EnforcementInfo
            FillEnforcementInfoFromDataRow(dataRow, enfInfo)
            Return enfInfo
        End Function

        Private Function EnforcementCaseFromDataRow(ByVal row As DataRow) As EnforcementCase

            Dim enfCase As New EnforcementCase
            With enfCase
                .StaffResponsibleId = DBUtilities.GetNullable(Of Integer)(row("NUMSTAFFRESPONSIBLE"))
                .AirsNumber = DBUtilities.GetNullable(Of String)(row("STRAIRSNUMBER"))
                .AoAppealed = DBUtilities.GetNullableDateTimeFromString(row("DATAOAPPEALED"))
                .AoComment = DBUtilities.GetNullable(Of String)(row("STRAOCOMMENT"))
                .AoExecuted = DBUtilities.GetNullableDateTimeFromString(row("DATAOEXECUTED"))
                .AoResolved = DBUtilities.GetNullableDateTimeFromString(row("DATAORESOLVED"))
                .AfsAoResolvedActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSAORESOLVEDNUMBER"))
                .AfsAoToAGActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSAOTOAGNUMBER"))
                .AfsCivilCourtActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSCIVILCOURTNUMBER"))
                .AfsCoActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSCOEXECUTEDNUMBER"))
                .CoComment = DBUtilities.GetNullable(Of String)(row("STRCOCOMMENT"))
                .CoExecuted = DBUtilities.GetNullableDateTimeFromString(row("DATCOEXECUTED"))
                .Comment = DBUtilities.GetNullable(Of String)(row("STRGENERALCOMMENTS"))
                .CoNumber = DBUtilities.GetNullable(Of String)(row("STRCONUMBER"))
                .CoPenaltyAmount = DBUtilities.GetNullable(Of Decimal)(row("STRCOPENALTYAMOUNT"))
                .CoPenaltyAmountComment = DBUtilities.GetNullable(Of String)(row("STRCOPENALTYAMOUNTCOMMENTS"))
                .CoProposed = DBUtilities.GetNullableDateTimeFromString(row("DATCOPROPOSED"))
                .AfsCoProposedNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSCOPROPOSEDNUMBER"))
                .CoReceivedFromCompany = DBUtilities.GetNullableDateTimeFromString(row("DATCORECEIVEDFROMCOMPANY"))
                .CoReceivedFromDirector = DBUtilities.GetNullableDateTimeFromString(row("DATCORECEIVEDFROMDIRECTOR"))
                .CoResolved = DBUtilities.GetNullableDateTimeFromString(row("DATCORESOLVED"))
                .AfsCoResolvedActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSCORESOLVEDNUMBER"))
                .CoToPm = DBUtilities.GetNullableDateTimeFromString(row("DATCOTOPM"))
                .CoToUc = DBUtilities.GetNullableDateTimeFromString(row("DATCOTOUC"))
                .DateFinalized = DBUtilities.GetNullableDateTimeFromString(row("DATENFORCEMENTFINALIZED"))
                .DayZeroDate = DBUtilities.GetNullableDateTimeFromString(row("DATDAYZERO"))
                .DiscoveryDate = DBUtilities.GetNullableDateTimeFromString(row("DATDISCOVERYDATE"))
                .EnforcementId = DBUtilities.GetNullable(Of Integer)(row("STRENFORCEMENTNUMBER"))
                .ViolationType = DBUtilities.GetNullable(Of String)(row("STRHPV"))
                .AfsKeyActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSKEYACTIONNUMBER"))
                .LinkedWorkItemId = DBUtilities.GetNullable(Of Integer)(row("STRTRACKINGNUMBER"))
                .LonComment = DBUtilities.GetNullable(Of String)(row("STRLONCOMMENTS"))
                .LonResolved = DBUtilities.GetNullableDateTimeFromString(row("DATLONRESOLVED"))
                .LonSent = DBUtilities.GetNullableDateTimeFromString(row("DATLONSENT"))
                .LonToUc = DBUtilities.GetNullableDateTimeFromString(row("DATLONTOUC"))
                .AfsNfaActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSNOVRESOLVEDNUMBER"))
                .NfaSent = DBUtilities.GetNullableDateTimeFromString(row("DATNFALETTERSENT"))
                .NfaToPm = DBUtilities.GetNullableDateTimeFromString(row("DATNFATOPM"))
                .NfaToUc = DBUtilities.GetNullableDateTimeFromString(row("DATNFATOUC"))
                .AfsNovActionNumber = DBUtilities.GetNullable(Of Integer)(row("STRAFSNOVSENTNUMBER"))
                .NovComment = DBUtilities.GetNullable(Of String)(row("STRNOVCOMMENT"))
                .NovResponseReceived = DBUtilities.GetNullableDateTimeFromString(row("DATNOVRESPONSERECEIVED"))
                .NovSent = DBUtilities.GetNullableDateTimeFromString(row("DATNOVSENT"))
                .NovToPm = DBUtilities.GetNullableDateTimeFromString(row("DATNOVTOPM"))
                .NovToUc = DBUtilities.GetNullableDateTimeFromString(row("DATNOVTOUC"))
                .Open = Not DBUtilities.GetNullable(Of Boolean)(row("STRENFORCEMENTFINALIZED"))
                .ProgramPollutants = DBUtilities.GetNullable(Of String)(row("STRPOLLUTANTS"))
                .SubmittedToUcCode = DBUtilities.GetNullable(Of String)(row("STRSTATUS"))
                .DateModified = DBUtilities.GetNullableDateTimeFromString(row("DATMODIFINGDATE"))

                .EnforcementActions = New List(Of EnforcementActionType)
                If .LonComment <> "" Or AnyOfTheseDatesHasValue({ .LonResolved, .LonSent, .LonToUc}) Then
                    .EnforcementActions.Add(EnforcementActionType.LON)
                Else
                    If .NovComment <> "" Or AnyOfTheseDatesHasValue({ .NovSent, .NovToPm, .NovToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.NOV)
                    End If

                    If .CoComment <> "" Or .CoNumber <> "" Or AnyOfTheseDatesHasValue({ .CoExecuted, .CoProposed, .CoReceivedFromCompany, .CoReceivedFromDirector, .CoResolved, .CoToPm, .CoToUc}) Then
                        .EnforcementActions.Add(EnforcementActionType.CO)
                    End If

                    If .AoComment <> "" Or AnyOfTheseDatesHasValue({ .AoAppealed, .AoExecuted, .AoResolved}) Then
                        .EnforcementActions.Add(EnforcementActionType.AO)
                    End If
                End If

                .SubmittedToEpa = .AfsKeyActionNumber > 0
            End With

            Return enfCase
        End Function

        Public Function GetEnforcementCase(ByVal enforcementId As String) As EnforcementCase
            If enforcementId = "" OrElse Not Integer.TryParse(enforcementId, Nothing) Then Return Nothing

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
            If enforcementCase.EnforcementId = 0 Then
                enforcementCase.EnforcementId = GetNextEnforcementId()
            End If

            Dim queriesList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())

            With enforcementCase

                queriesList.Add("INSERT INTO SSCP_ENFORCEMENT " &
                        "  ( " &
                        "    ID, STRENFORCEMENTNUMBER, STRTRACKINGNUMBER, STRAIRSNUMBER, " &
                        "    STRENFORCEMENTFINALIZED, DATENFORCEMENTFINALIZED, " &
                        "    NUMSTAFFRESPONSIBLE, STRSTATUS, STRACTIONTYPE, " &
                        "    STRGENERALCOMMENTS, STRDISCOVERYDATE, DATDISCOVERYDATE, " &
                        "    STRDAYZERO, DATDAYZERO, STRHPV, STRPOLLUTANTS, " &
                        "    STRPOLLUTANTSTATUS, STRLONTOUC, DATLONTOUC, STRLONSENT, " &
                        "    DATLONSENT, STRLONRESOLVED, DATLONRESOLVED, STRLONCOMMENTS, " &
                        "    STRNOVTOUC, DATNOVTOUC, " &
                        "    STRNOVTOPM, DATNOVTOPM, STRNOVSENT, DATNOVSENT, " &
                        "    STRNOVRESPONSERECEIVED, DATNOVRESPONSERECEIVED, STRNFATOUC, " &
                        "    DATNFATOUC, STRNFATOPM, DATNFATOPM, STRNFALETTERSENT, " &
                        "    DATNFALETTERSENT, STRNOVCOMMENT, " &
                        "    STRCOTOUC, DATCOTOUC, STRCOTOPM, DATCOTOPM, STRCOPROPOSED, " &
                        "    DATCOPROPOSED, STRCORECEIVEDFROMCOMPANY, " &
                        "    DATCORECEIVEDFROMCOMPANY, STRCORECEIVEDFROMDIRECTOR, " &
                        "    DATCORECEIVEDFROMDIRECTOR, STRCOEXECUTED, DATCOEXECUTED, " &
                        "    STRCONUMBER, STRCORESOLVED, DATCORESOLVED, " &
                        "    STRCOPENALTYAMOUNT, STRCOPENALTYAMOUNTCOMMENTS, " &
                        "    STRCOCOMMENT, " &
                        "    STRAOEXECUTED, DATAOEXECUTED, " &
                        "    STRAOAPPEALED, DATAOAPPEALED, STRAORESOLVED, DATAORESOLVED, " &
                        "    STRAOCOMMENT, STRAFSKEYACTIONNUMBER, STRAFSNOVSENTNUMBER, " &
                        "    STRAFSNOVRESOLVEDNUMBER, STRAFSCOPROPOSEDNUMBER, " &
                        "    STRAFSCOEXECUTEDNUMBER, STRAFSCORESOLVEDNUMBER, " &
                        "    STRAFSAOTOAGNUMBER, STRAFSCIVILCOURTNUMBER, " &
                        "    STRAFSAORESOLVEDNUMBER, STRMODIFINGPERSON, DATMODIFINGDATE " &
                        "  ) " &
                        "  VALUES " &
                        "  ( " &
                        "    (SELECT MAX(ID) + 1 FROM SSCP_ENFORCEMENT), " &
                        "    @STRENFORCEMENTNUMBER, @STRTRACKINGNUMBER, " &
                        "    @STRAIRSNUMBER, @STRENFORCEMENTFINALIZED, " &
                        "    @DATENFORCEMENTFINALIZED, @NUMSTAFFRESPONSIBLE, @STRSTATUS, " &
                        "    @STRACTIONTYPE, @STRGENERALCOMMENTS, @STRDISCOVERYDATE, " &
                        "    @DATDISCOVERYDATE, @STRDAYZERO, @DATDAYZERO, @STRHPV, " &
                        "    @STRPOLLUTANTS, @STRPOLLUTANTSTATUS, @STRLONTOUC, " &
                        "    @DATLONTOUC, @STRLONSENT, @DATLONSENT, @STRLONRESOLVED, " &
                        "    @DATLONRESOLVED, @STRLONCOMMENTS, " &
                        "    @STRNOVTOUC, @DATNOVTOUC, " &
                        "    @STRNOVTOPM, @DATNOVTOPM, @STRNOVSENT, @DATNOVSENT, " &
                        "    @STRNOVRESPONSERECEIVED, @DATNOVRESPONSERECEIVED, " &
                        "    @STRNFATOUC, @DATNFATOUC, @STRNFATOPM, @DATNFATOPM, " &
                        "    @STRNFALETTERSENT, @DATNFALETTERSENT, @STRNOVCOMMENT, " &
                        "    @STRCOTOUC, @DATCOTOUC, " &
                        "    @STRCOTOPM, @DATCOTOPM, @STRCOPROPOSED, @DATCOPROPOSED, " &
                        "    @STRCORECEIVEDFROMCOMPANY, @DATCORECEIVEDFROMCOMPANY, " &
                        "    @STRCORECEIVEDFROMDIRECTOR, @DATCORECEIVEDFROMDIRECTOR, " &
                        "    @STRCOEXECUTED, @DATCOEXECUTED, @STRCONUMBER, " &
                        "    @STRCORESOLVED, @DATCORESOLVED, @STRCOPENALTYAMOUNT, " &
                        "    @STRCOPENALTYAMOUNTCOMMENTS, @STRCOCOMMENT, " &
                        "    @STRAOEXECUTED, @DATAOEXECUTED, @STRAOAPPEALED, " &
                        "    @DATAOAPPEALED, @STRAORESOLVED, @DATAORESOLVED, " &
                        "    @STRAOCOMMENT, @STRAFSKEYACTIONNUMBER, @STRAFSNOVSENTNUMBER, " &
                        "    @STRAFSNOVRESOLVEDNUMBER, @STRAFSCOPROPOSEDNUMBER, " &
                        "    @STRAFSCOEXECUTEDNUMBER, @STRAFSCORESOLVEDNUMBER, " &
                        "    @STRAFSAOTOAGNUMBER, @STRAFSCIVILCOURTNUMBER, " &
                        "    @STRAFSAORESOLVEDNUMBER, @STRMODIFINGPERSON, sysdate " &
                        "  ) ")

                parametersList.Add(New SqlParameter() {
                    New SqlParameter("@STRENFORCEMENTNUMBER", .EnforcementId),
                    New SqlParameter("@STRTRACKINGNUMBER", StoreNothingIfZero(.LinkedWorkItemId)),
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
                    New SqlParameter("@STRPOLLUTANTSTATUS", "0"),
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
                })

                If DB.RunCommand(queriesList, parametersList) Then
                    Dim param As SqlParameter = New SqlParameter("@ENFORCEMENT", .EnforcementId)
                    DB.SPRunCommand("PD_SSCPENFORCEMENT", param)
                    Return .EnforcementId
                Else
                    Return 0
                End If

            End With
        End Function

        Private Function GetNextEnforcementId() As Integer
            Dim query As String = "SELECT SSCPENFORCEMENTNUMBER.NEXTVAL FROM DUAL"
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

#End Region

#Region " Delete enforcement "

        Public Function DeleteEnforcement(enforcementId As String) As Boolean
            Dim queries As New List(Of String)
            Dim parameters As New List(Of SqlParameter())
            Dim parameter As SqlParameter() = {New SqlParameter("@enforcementId", enforcementId)}

            queries.Add("DELETE FROM SSCPENFORCEMENTSTIPULATED " &
                        "WHERE STRENFORCEMENTNUMBER = @enforcementId ")
            parameters.Add(parameter)

            queries.Add("DELETE FROM AFSSSCPENFORCEMENTRECORDS " &
                        "WHERE STRENFORCEMENTNUMBER = @enforcementId ")
            parameters.Add(parameter)

            queries.Add("DELETE FROM SSCP_AUDITEDENFORCEMENT " &
                        "WHERE STRENFORCEMENTNUMBER = @enforcementId ")
            parameters.Add(parameter)

            Return DB.RunCommand(queries, parameters)
        End Function

#End Region

#Region " Stipulated Penalties "

        Public Function GetStipulatedPenalties(enforcementId As String) As DataTable
            Dim query As String = "SELECT STRENFORCEMENTKEY, STRSTIPULATEDPENALTY, " &
                " CASE WHEN STRSTIPULATEDPENALTYCOMMENTS IS NULL THEN 'N/A' " &
                "  ELSE STRSTIPULATEDPENALTYCOMMENTS END STRSTIPULATEDPENALTYCOMMENTS, " &
                " STRAFSSTIPULATEDPENALTYNUMBER " &
                "FROM SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = @enforcementId " &
                "ORDER BY STRENFORCEMENTKEY"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetNextStipulatedPenaltyKey(enforcementId As String) As Integer
            Dim query As String = "SELECT MAX(es.STRENFORCEMENTKEY) " &
                "FROM SSCPENFORCEMENTSTIPULATED es " &
                "WHERE es.STRENFORCEMENTNUMBER = @enforcementId"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Dim key As Integer = DB.GetSingleValue(Of Integer)(query, parameter)
            Return key + 1
        End Function

        Public Function SaveNewStipulatedPenalty(enforcementId As String, airsNumber As Apb.ApbFacilityId,
                                                 penalty As Decimal, comment As String) As Boolean
            Dim enfKey As Integer = GetNextStipulatedPenaltyKey(enforcementId)
            Dim afsKey As Integer = GetNextAfsActionNumber(airsNumber)

            Dim queries As New List(Of String)
            Dim parameters As New List(Of SqlParameter())

            queries.Add("UPDATE APBSUPPLAMENTALDATA " &
                        "SET STRAFSACTIONNUMBER = @afs " &
                        "WHERE STRAIRSNUMBER = @airsNumber ")
            parameters.Add(New SqlParameter() {
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
                        "    @afsKey, @userid, sysdate " &
                        "  ) ")
            parameters.Add(New SqlParameter() {
                New SqlParameter("@enforcementId", enforcementId),
                New SqlParameter("@enfKey", enfKey.ToString),
                New SqlParameter("@penalty", penalty.ToString),
                New SqlParameter("@penaltyComment", comment),
                New SqlParameter("@afsKey", afsKey.ToString),
                New SqlParameter("@userid", CurrentUser.UserID)
            })

            Return DB.RunCommand(queries, parameters)
        End Function

        Public Function UpdateStipulatedPenalty(enforcementId As String, penalty As Decimal, comment As String, enfKey As Integer) As Boolean
            Dim query As String = "UPDATE SSCPENFORCEMENTSTIPULATED " &
                "SET STRSTIPULATEDPENALTY = @penalty, " &
                "  STRSTIPULATEDPENALTYCOMMENTS = @penaltyComment, " &
                "  STRMODIFINGPERSON = @userid, DATMODIFINGDATE = SYSDATE " &
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

        Public Function DeleteStipulatedPenalty(enforcementId As String, enfKey As Integer) As Boolean
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

        Public Function GetEnforcementAuditHistory(enforcementId As String) As DataTable
            Dim query As String = "SELECT(upr.STRLASTNAME || ', ' || upr.STRFIRSTNAME) AS " &
                "  StaffResponsible, enf.STRTRACKINGNUMBER, " &
                "  enf.DATENFORCEMENTFINALIZED, enf.STRSTATUS, enf.STRACTIONTYPE " &
                "  , enf.STRGENERALCOMMENTS, enf.DATDISCOVERYDATE, " &
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
                "  enf.STRAOCOMMENT,(upm.STRLASTNAME || ', ' || upm.STRFIRSTNAME " &
                "  ) AS ModifiedBy, enf.DATMODIFINGDATE " &
                "FROM SSCP_ENFORCEMENT enf " &
                "LEFT JOIN EPDUSERPROFILES upr ON " &
                "  enf.NUMSTAFFRESPONSIBLE = upr.NUMUSERID " &
                "LEFT JOIN EPDUSERPROFILES upm ON " &
                "  enf.STRMODIFINGPERSON = upm.NUMUSERID " &
                "WHERE enf.STRENFORCEMENTNUMBER = @enforcementId"
            Dim parameter As New SqlParameter("@enforcementId", enforcementId)
            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

#Region " Lookup table "

        ' TODO: Move this to a data service

        Public Function GetViolationTypes() As DataTable
            Dim query As String =
                "SELECT AIRVIOLATIONTYPECODE, VIOLATIONTYPEDESC, SEVERITYCODE " &
                " , POLLUTANTREQUIRED, DEPRECATED " &
                "FROM LK_VIOLATION_TYPE " &
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

    End Module

End Namespace
