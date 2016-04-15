Imports System.Collections.Generic
Imports Iaip.Apb.Sscp
Imports Oracle.ManagedDataAccess.Client

Namespace DAL.Sscp

    Module EnforcementData

#Region " Summary tables "

        Public Function GetEnforcementCountForFacility(airs As Apb.ApbFacilityId) As Integer
            Dim query As String = "SELECT COUNT(*) " &
                " FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                " WHERE STRENFORCEMENTFINALIZED = 'False' " &
                " AND STRAIRSNUMBER = :airs"
            Dim parameter As OracleParameter = New OracleParameter("airs", airs.DbFormattedString)
            Return DB.GetSingleValue(Of Integer)(query, parameter)
        End Function

        ''' <summary>
        ''' Returns a DataTable of enforcement summary data for a given facility.
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

#Region " Get enforcement info "

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

            Dim staff As New IaipUser
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

            Dim enfCase As New EnforcementCase
            With enfCase
                .StaffResponsibleId = DB.GetNullable(Of Integer)(row("NUMSTAFFRESPONSIBLE"))
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
                .AfsCoProposedNumber = DB.GetNullable(Of Integer)(row("STRAFSCOPROPOSEDNUMBER"))
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
                .ViolationType = DB.GetNullable(Of String)(row("STRHPV"))
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
                .ProgramPollutants = DB.GetNullable(Of String)(row("STRPOLLUTANTS"))
                .SubmittedToUcCode = DB.GetNullable(Of String)(row("STRSTATUS"))
                .DateModified = DB.GetNullableDateTimeFromString(row("DATMODIFINGDATE"))

                .EnforcementActions = New List(Of EnforcementActionType)
                If .LonSent IsNot Nothing Then
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

#Region " Save enforcement "

        Public Function SaveEnforcement(enforcementCase As EnforcementCase) As Integer
            If enforcementCase.EnforcementId = 0 Then
                enforcementCase.EnforcementId = GetNextEnforcementId()
            End If

            Dim queriesList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            With enforcementCase

                queriesList.Add("INSERT INTO AIRBRANCH.SSCP_ENFORCEMENT " &
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
                        "    (SELECT MAX(ID) + 1 FROM AIRBRANCH.SSCP_ENFORCEMENT), " &
                        "    :STRENFORCEMENTNUMBER, :STRTRACKINGNUMBER, " &
                        "    :STRAIRSNUMBER, :STRENFORCEMENTFINALIZED, " &
                        "    :DATENFORCEMENTFINALIZED, :NUMSTAFFRESPONSIBLE, :STRSTATUS, " &
                        "    :STRACTIONTYPE, :STRGENERALCOMMENTS, :STRDISCOVERYDATE, " &
                        "    :DATDISCOVERYDATE, :STRDAYZERO, :DATDAYZERO, :STRHPV, " &
                        "    :STRPOLLUTANTS, :STRPOLLUTANTSTATUS, :STRLONTOUC, " &
                        "    :DATLONTOUC, :STRLONSENT, :DATLONSENT, :STRLONRESOLVED, " &
                        "    :DATLONRESOLVED, :STRLONCOMMENTS, " &
                        "    :STRNOVTOUC, :DATNOVTOUC, " &
                        "    :STRNOVTOPM, :DATNOVTOPM, :STRNOVSENT, :DATNOVSENT, " &
                        "    :STRNOVRESPONSERECEIVED, :DATNOVRESPONSERECEIVED, " &
                        "    :STRNFATOUC, :DATNFATOUC, :STRNFATOPM, :DATNFATOPM, " &
                        "    :STRNFALETTERSENT, :DATNFALETTERSENT, :STRNOVCOMMENT, " &
                        "    :STRCOTOUC, :DATCOTOUC, " &
                        "    :STRCOTOPM, :DATCOTOPM, :STRCOPROPOSED, :DATCOPROPOSED, " &
                        "    :STRCORECEIVEDFROMCOMPANY, :DATCORECEIVEDFROMCOMPANY, " &
                        "    :STRCORECEIVEDFROMDIRECTOR, :DATCORECEIVEDFROMDIRECTOR, " &
                        "    :STRCOEXECUTED, :DATCOEXECUTED, :STRCONUMBER, " &
                        "    :STRCORESOLVED, :DATCORESOLVED, :STRCOPENALTYAMOUNT, " &
                        "    :STRCOPENALTYAMOUNTCOMMENTS, :STRCOCOMMENT, " &
                        "    :STRAOEXECUTED, :DATAOEXECUTED, :STRAOAPPEALED, " &
                        "    :DATAOAPPEALED, :STRAORESOLVED, :DATAORESOLVED, " &
                        "    :STRAOCOMMENT, :STRAFSKEYACTIONNUMBER, :STRAFSNOVSENTNUMBER, " &
                        "    :STRAFSNOVRESOLVEDNUMBER, :STRAFSCOPROPOSEDNUMBER, " &
                        "    :STRAFSCOEXECUTEDNUMBER, :STRAFSCORESOLVEDNUMBER, " &
                        "    :STRAFSAOTOAGNUMBER, :STRAFSCIVILCOURTNUMBER, " &
                        "    :STRAFSAORESOLVEDNUMBER, :STRMODIFINGPERSON, sysdate " &
                        "  ) ")

                parametersList.Add(New OracleParameter() {
                    New OracleParameter(":STRENFORCEMENTNUMBER", .EnforcementId),
                    New OracleParameter(":STRTRACKINGNUMBER", DB.StoreNothingIfZero(.LinkedWorkItemId)),
                    New OracleParameter(":STRAIRSNUMBER", .AirsNumber.DbFormattedString),
                    New OracleParameter(":STRENFORCEMENTFINALIZED", .DateFinalized.HasValue.ToString),
                    New OracleParameter(":DATENFORCEMENTFINALIZED", .DateFinalized),
                    New OracleParameter(":NUMSTAFFRESPONSIBLE", .StaffResponsibleId),
                    New OracleParameter(":STRSTATUS", .SubmittedToUcCode),
                    New OracleParameter(":STRACTIONTYPE", .EnforcementType.ToString),
                    New OracleParameter(":STRGENERALCOMMENTS", .Comment),
                    New OracleParameter(":STRDISCOVERYDATE", .DiscoveryDate.HasValue.ToString),
                    New OracleParameter(":DATDISCOVERYDATE", .DiscoveryDate),
                    New OracleParameter(":STRDAYZERO", .DayZeroDate.HasValue.ToString),
                    New OracleParameter(":DATDAYZERO", .DayZeroDate),
                    New OracleParameter(":STRHPV", If(.ViolationType = "BLANK", "", .ViolationType)),
                    New OracleParameter(":STRPOLLUTANTS", .ProgramPollutants),
                    New OracleParameter(":STRPOLLUTANTSTATUS", "0"),
                    New OracleParameter(":STRLONTOUC", .LonToUc.HasValue.ToString),
                    New OracleParameter(":DATLONTOUC", .LonToUc),
                    New OracleParameter(":STRLONSENT", .LonSent.HasValue.ToString),
                    New OracleParameter(":DATLONSENT", .LonSent),
                    New OracleParameter(":STRLONRESOLVED", .LonResolved.HasValue.ToString),
                    New OracleParameter(":DATLONRESOLVED", .LonResolved),
                    New OracleParameter(":STRLONCOMMENTS", .LonComment),
                    New OracleParameter(":STRNOVTOUC", .NovToUc.HasValue.ToString),
                    New OracleParameter(":DATNOVTOUC", .NovToUc),
                    New OracleParameter(":STRNOVTOPM", .NovToPm.HasValue.ToString),
                    New OracleParameter(":DATNOVTOPM", .NovToPm),
                    New OracleParameter(":STRNOVSENT", .NovSent.HasValue.ToString),
                    New OracleParameter(":DATNOVSENT", .NovSent),
                    New OracleParameter(":STRNOVRESPONSERECEIVED", .NovResponseReceived.HasValue.ToString),
                    New OracleParameter(":DATNOVRESPONSERECEIVED", .NovResponseReceived),
                    New OracleParameter(":STRNFATOUC", .NfaToUc.HasValue.ToString),
                    New OracleParameter(":DATNFATOUC", .NfaToUc),
                    New OracleParameter(":STRNFATOPM", .NfaToPm.HasValue.ToString),
                    New OracleParameter(":DATNFATOPM", .NfaToPm),
                    New OracleParameter(":STRNFALETTERSENT", .NfaSent.HasValue.ToString),
                    New OracleParameter(":DATNFALETTERSENT", .NfaSent),
                    New OracleParameter(":STRNOVCOMMENT", .NovComment),
                    New OracleParameter(":STRCOTOUC", .CoToUc.HasValue.ToString),
                    New OracleParameter(":DATCOTOUC", .CoToUc),
                    New OracleParameter(":STRCOTOPM", .CoToPm.HasValue.ToString),
                    New OracleParameter(":DATCOTOPM", .CoToPm),
                    New OracleParameter(":STRCOPROPOSED", .CoProposed.HasValue.ToString),
                    New OracleParameter(":DATCOPROPOSED", .CoProposed),
                    New OracleParameter(":STRCORECEIVEDFROMCOMPANY", .CoReceivedFromCompany.HasValue.ToString),
                    New OracleParameter(":DATCORECEIVEDFROMCOMPANY", .CoReceivedFromCompany),
                    New OracleParameter(":STRCORECEIVEDFROMDIRECTOR", .CoReceivedFromDirector.HasValue.ToString),
                    New OracleParameter(":DATCORECEIVEDFROMDIRECTOR", .CoReceivedFromDirector),
                    New OracleParameter(":STRCOEXECUTED", .CoExecuted.HasValue.ToString),
                    New OracleParameter(":DATCOEXECUTED", .CoExecuted),
                    New OracleParameter(":STRCONUMBER", .CoNumber),
                    New OracleParameter(":STRCORESOLVED", .CoResolved.HasValue.ToString),
                    New OracleParameter(":DATCORESOLVED", .CoResolved),
                    New OracleParameter(":STRCOPENALTYAMOUNT", DB.StoreNothingIfZero(.CoPenaltyAmount)),
                    New OracleParameter(":STRCOPENALTYAMOUNTCOMMENTS", .CoPenaltyAmountComment),
                    New OracleParameter(":STRCOCOMMENT", .CoComment),
                    New OracleParameter(":STRAOEXECUTED", .AoExecuted.HasValue.ToString),
                    New OracleParameter(":DATAOEXECUTED", .AoExecuted),
                    New OracleParameter(":STRAOAPPEALED", .AoAppealed.HasValue.ToString),
                    New OracleParameter(":DATAOAPPEALED", .AoAppealed),
                    New OracleParameter(":STRAORESOLVED", .AoResolved.HasValue.ToString),
                    New OracleParameter(":DATAORESOLVED", .AoResolved),
                    New OracleParameter(":STRAOCOMMENT", .AoComment),
                    New OracleParameter(":STRAFSKEYACTIONNUMBER", DB.StoreNothingIfZero(.AfsKeyActionNumber)),
                    New OracleParameter(":STRAFSNOVSENTNUMBER", DB.StoreNothingIfZero(.AfsNovActionNumber)),
                    New OracleParameter(":STRAFSNOVRESOLVEDNUMBER", DB.StoreNothingIfZero(.AfsNfaActionNumber)),
                    New OracleParameter(":STRAFSCOPROPOSEDNUMBER", DB.StoreNothingIfZero(.AfsCoProposedNumber)),
                    New OracleParameter(":STRAFSCOEXECUTEDNUMBER", DB.StoreNothingIfZero(.AfsCoActionNumber)),
                    New OracleParameter(":STRAFSCORESOLVEDNUMBER", DB.StoreNothingIfZero(.AfsCoResolvedActionNumber)),
                    New OracleParameter(":STRAFSAOTOAGNUMBER", DB.StoreNothingIfZero(.AfsAoToAGActionNumber)),
                    New OracleParameter(":STRAFSCIVILCOURTNUMBER", DB.StoreNothingIfZero(.AfsCivilCourtActionNumber)),
                    New OracleParameter(":STRAFSAORESOLVEDNUMBER", DB.StoreNothingIfZero(.AfsAoResolvedActionNumber)),
                    New OracleParameter(":STRMODIFINGPERSON", CurrentUser.UserID)
                })

                For Each prog As String In .LegacyAirPrograms
                    For Each poll As String In .Pollutants
                        queriesList.Add("UPDATE AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                                    "SET STRMODIFINGPERSON = :STRMODIFINGPERSON, " &
                                    "  DATMODIFINGDATE = sysdate " &
                                    "WHERE STRAIRPOLLUTANTKEY = :STRAIRPOLLUTANTKEY AND " &
                                    "  STRPOLLUTANTKEY = :STRPOLLUTANTKEY")
                        parametersList.Add(New OracleParameter() {
                            New OracleParameter(":STRMODIFINGPERSON", CurrentUser.UserID),
                            New OracleParameter(":STRAIRPOLLUTANTKEY", .AirsNumber.DbFormattedString & prog),
                            New OracleParameter(":STRPOLLUTANTKEY", poll)
                        })
                    Next
                Next

                If DB.RunCommand(queriesList, parametersList) Then
                    Dim param As OracleParameter = New OracleParameter("ENFORCEMENT", .EnforcementId)
                    DB.SPRunCommand("AIRBRANCH.PD_SSCPENFORCEMENT", param)
                    Return .EnforcementId
                Else
                    Return 0
                End If

            End With
        End Function

        Private Function GetNextEnforcementId() As Integer
            Dim query As String = "SELECT AIRBRANCH.SSCPENFORCEMENTNUMBER.NEXTVAL FROM DUAL"
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

#End Region

#Region " Delete enforcement "

        Public Function DeleteEnforcement(enforcementId As String) As Boolean
            Dim queries As New List(Of String)
            Dim parameters As New List(Of OracleParameter())
            Dim parameter As OracleParameter() = {New OracleParameter("enforcementId", enforcementId)}

            queries.Add("DELETE FROM AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                        "WHERE STRENFORCEMENTNUMBER = :enforcementId ")
            parameters.Add(parameter)

            queries.Add("DELETE FROM AIRBRANCH.AFSSSCPENFORCEMENTRECORDS " &
                        "WHERE STRENFORCEMENTNUMBER = :enforcementId ")
            parameters.Add(parameter)

            queries.Add("DELETE FROM AIRBRANCH.SSCP_AUDITEDENFORCEMENT " &
                        "WHERE STRENFORCEMENTNUMBER = :enforcementId ")
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
            Dim key As Integer = DB.GetSingleValue(Of Integer)(query, parameter)
            Return key + 1
        End Function

        Public Function SaveNewStipulatedPenalty(enforcementId As String, airsNumber As Apb.ApbFacilityId,
                                                 penalty As Decimal, comment As String) As Boolean
            Dim enfKey As Integer = GetNextStipulatedPenaltyKey(enforcementId)
            Dim afsKey As Integer = GetNextAfsActionNumber(airsNumber)

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
                New OracleParameter("userid", CurrentUser.UserID)
            })

            Return DB.RunCommand(queries, parameters)
        End Function

        Public Function UpdateStipulatedPenalty(enforcementId As String, penalty As Decimal, comment As String, enfKey As Integer) As Boolean
            Dim query As String = "UPDATE AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                "SET STRSTIPULATEDPENALTY = :penalty, " &
                "  STRSTIPULATEDPENALTYCOMMENTS = :penaltyComment, " &
                "  STRMODIFINGPERSON = :userid, DATMODIFINGDATE = SYSDATE " &
                "WHERE STRENFORCEMENTNUMBER = :enforcementId AND " &
                "  STRENFORCEMENTKEY = :enfKey"
            Dim parameters As OracleParameter() = {
                New OracleParameter("penalty", penalty.ToString),
                New OracleParameter("penaltyComment", comment),
                New OracleParameter("userid", CurrentUser.UserID),
                New OracleParameter("enforcementId", enforcementId),
                New OracleParameter("enfKey", enfKey)
            }
            Return DB.RunCommand(query, parameters)
        End Function

        Public Function DeleteStipulatedPenalty(enforcementId As String, enfKey As Integer) As Boolean
            Dim query As String = "DELETE " &
                "FROM AIRBRANCH.SSCPENFORCEMENTSTIPULATED " &
                "WHERE STRENFORCEMENTNUMBER = :enforcementId AND " &
                "  STRENFORCEMENTKEY = :enfKey"
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

#Region " Lookup table "

        ' TODO: Move this to a data service

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

    End Module

End Namespace
