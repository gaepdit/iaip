Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports System.Data.SqlClient

Namespace DAL

    Module PollutantsPrograms

        Public Function GetFacilityPollutants(airsNumber As ApbFacilityId) As DataTable
            Dim query As String = "SELECT DISTINCT(poll.STRPOLLUTANTKEY), " &
                "  lkpoll.STRPOLLUTANTDESCRIPTION " &
                "FROM APBAIRPROGRAMPOLLUTANTS poll " &
                "INNER JOIN LOOKUPPOLLUTANTS lkpoll ON " &
                "  poll.STRPOLLUTANTKEY = lkpoll.STRPOLLUTANTCODE " &
                "WHERE poll.STRAIRSNUMBER = @airsNumber"
            Dim parameter As New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("STRPOLLUTANTKEY")}
            Return dt
        End Function

        Public Function GetFacilityAirPrograms(airsNumber As ApbFacilityId) As AirProgram
            Dim query As String = "SELECT STRAIRPROGRAMCODES FROM APBHEADERDATA WHERE STRAIRSNUMBER = @airsNumber"
            Dim parameter As New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            Dim apc As String = DB.GetString(query, parameter)

            If apc Is Nothing Then
                Return AirProgram.None
            Else
                Return ConvertBitFieldToEnum(Of AirProgram)(apc)
            End If
        End Function

        Public Function GetFacilityAirProgramsAsDataTable(airsNumber As ApbFacilityId, Stringify As Boolean) As DataTable
            Dim airPrograms As AirProgram = GetFacilityAirPrograms(airsNumber)

            If (airPrograms = AirProgram.None) Then Return Nothing

            Dim dt As New DataTable

            If Stringify Then
                dt.Columns.Add("Key", GetType(String))
                dt.Columns.Add("Description", GetType(String))
                For Each ap As AirProgram In airPrograms.GetUniqueFlags
                    dt.Rows.Add({ap.ToString, ap.GetDescription})
                Next
            Else
                dt.Columns.Add("Key", GetType(AirProgram))
                dt.Columns.Add("Description", GetType(String))
                For Each ap As AirProgram In airPrograms.GetUniqueFlags
                    dt.Rows.Add({ap, ap.GetDescription})
                Next
            End If

            Return dt
        End Function

        Public Function GetFacilityProgramPollutantStatuses(airsNumber As ApbFacilityId) As DataTable
            Dim query As String = "SELECT app.STRAIRPOLLUTANTKEY AS [key], " &
                "  SUBSTRING(app.STRAIRPOLLUTANTKEY, 13, 1) AS " &
                "  [Air Program Code], lkpl.STRPOLLUTANTCODE AS " &
                "  [Pollutant Code], lkpl.STRPOLLUTANTDESCRIPTION AS " &
                "  [Pollutant], app.STROPERATIONALSTATUS AS " &
                "  [Operating Status Code], app.DATMODIFINGDATE AS " &
                "  [Date Modified], CONCAT(up.STRLASTNAME, ', ', up.STRFIRSTNAME) " &
                "  AS [Modified By] " &
                "FROM APBAirProgramPollutants app " &
                "INNER JOIN LookUPPollutants lkpl ON " &
                "  app.STRPOLLUTANTKEY = lkpl.STRPOLLUTANTCODE " &
                "INNER JOIN EPDUserProfiles up ON " &
                "  app.STRMODIFINGPERSON = up.NUMUSERID " &
                "WHERE app.STRAIRSNUMBER = @airsNumber " &
                "ORDER BY [Air Program Code], [Pollutant Code]"
            Dim parameter As New SqlParameter("@airsNumber", airsNumber.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function SaveFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                        airProgram As AirProgram,
                                                        pollutantCode As String,
                                                        Optional operatingStatus As FacilityOperationalStatus = FacilityOperationalStatus.O
                                                        ) As Boolean

            If FacilityAirProgramPollutantExists(airsNumber, airProgram, pollutantCode) Then
                Return UpdateFacilityAirProgramPollutant(airsNumber, airProgram, pollutantCode, operatingStatus)
            Else
                Return InsertFacilityAirProgramPollutant(airsNumber, airProgram, pollutantCode, operatingStatus)
            End If
        End Function

        Public Function InsertFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String,
                                                           operatingStatus As FacilityOperationalStatus
                                                           ) As Boolean

            Dim query As String = "INSERT " &
                "INTO APBAIRPROGRAMPOLLUTANTS " &
                "  ( " &
                "    STRAIRSNUMBER, STRAIRPOLLUTANTKEY, STRPOLLUTANTKEY, " &
                "    STROPERATIONALSTATUS, " &
                "    STRMODIFINGPERSON, DATMODIFINGDATE " &
                "  ) " &
                "  VALUES " &
                "  ( " &
                "    @STRAIRSNUMBER, @STRAIRPOLLUTANTKEY, @STRPOLLUTANTKEY, " &
                "    @STROPERATIONALSTATUS, " &
                "    @STRMODIFINGPERSON, getdate() " &
                "  )"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@STRAIRSNUMBER", airsNumber.DbFormattedString),
                New SqlParameter("@STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New SqlParameter("@STRPOLLUTANTKEY", pollutantCode),
                New SqlParameter("@STROPERATIONALSTATUS", operatingStatus.ToString),
                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Private Function UpdateFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String,
                                                           operatingStatus As FacilityOperationalStatus
                                                           ) As Boolean

            Dim query As String = "UPDATE APBAIRPROGRAMPOLLUTANTS " &
                "SET STROPERATIONALSTATUS = @STROPERATIONALSTATUS, " &
                "  STRMODIFINGPERSON = @STRMODIFINGPERSON, " &
                "  DATMODIFINGDATE = getdate() " &
                "WHERE STRAIRPOLLUTANTKEY = @STRAIRPOLLUTANTKEY AND " &
                "  STRPOLLUTANTKEY = @STRPOLLUTANTKEY "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@STROPERATIONALSTATUS", operatingStatus.ToString),
                New SqlParameter("@STRMODIFINGPERSON", CurrentUser.UserID),
                New SqlParameter("@STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New SqlParameter("@STRPOLLUTANTKEY", pollutantCode)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Private Function FacilityAirProgramPollutantExists(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String
                                                           ) As Boolean

            Dim query As String = "SELECT STRAIRSNUMBER " &
                "FROM APBAIRPROGRAMPOLLUTANTS " &
                "WHERE STRAIRPOLLUTANTKEY = @STRAIRPOLLUTANTKEY " &
                " AND STRPOLLUTANTKEY = @STRPOLLUTANTKEY "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New SqlParameter("@STRPOLLUTANTKEY", pollutantCode)
            }

            Return DB.ValueExists(query, parameters)
        End Function

        Public Function GetPollutantsTable() As DataTable
            Dim query As String = "SELECT STRPOLLUTANTCODE AS [Pollutant Code], " &
                "  STRPOLLUTANTDESCRIPTION AS [Pollutant] " &
                "FROM LOOKUPPOLLUTANTS " &
                "WHERE STRAFSCODE = 'True' " &
                "ORDER BY STRPOLLUTANTDESCRIPTION"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("Pollutant Code")}
            Return dt
        End Function

    End Module

End Namespace
