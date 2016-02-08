﻿Imports Iaip.Apb
Imports Iaip.Apb.Facilities
Imports Iaip.Apb.Sscp
Imports Oracle.ManagedDataAccess.Client

Namespace DAL

    Module PollutantsPrograms

        Public Function GetFacilityPollutants(airsNumber As Apb.ApbFacilityId) As DataTable
            Dim query As String = "SELECT DISTINCT(poll.STRPOLLUTANTKEY), " &
                "  lkpoll.STRPOLLUTANTDESCRIPTION " &
                "FROM AIRBRANCH.APBAIRPROGRAMPOLLUTANTS poll " &
                "INNER JOIN AIRBRANCH.LOOKUPPOLLUTANTS lkpoll ON " &
                "  poll.STRPOLLUTANTKEY = lkpoll.STRPOLLUTANTCODE " &
                "WHERE poll.STRAIRSNUMBER = :airsNumber"
            Dim parameter As New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("STRPOLLUTANTKEY")}
            Return dt
        End Function

        Public Function GetFacilityAirPrograms(airsNumber As Apb.ApbFacilityId) As AirProgram
            Dim query As String = "SELECT STRAIRPROGRAMCODES FROM AIRBRANCH.APBHEADERDATA WHERE STRAIRSNUMBER = :airsNumber"
            Dim parameter As New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            Dim apc As String = DB.GetSingleValue(Of String)(query, parameter)

            If apc Is Nothing Then
                Return AirProgram.None
            Else
                Return ConvertBitFieldToEnum(Of AirProgram)(apc)
            End If
        End Function

        Public Function GetFacilityAirProgramsAsDataTable(airsNumber As Apb.ApbFacilityId, Stringify As Boolean) As DataTable
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
            Dim query As String = "SELECT SUBSTR(app.STRAIRPOLLUTANTKEY, 13, 1) AS " &
                "  ""Air Program Code"", lkpl.STRPOLLUTANTCODE AS " &
                "  ""Pollutant Code"", lkpl.STRPOLLUTANTDESCRIPTION AS " &
                "  ""Pollutant"", 'Status_' || lkcs.STRCOMPLIANCECODE AS " &
                "  ""Legacy Compliance Code"", app.STROPERATIONALSTATUS AS " &
                "  ""Operating Status Code"", app.DATMODIFINGDATE AS " &
                "  ""Date Modified"",(up.STRLASTNAME || ', ' || up.STRFIRSTNAME) " &
                "  AS ""Modified By"" " &
                "FROM AIRBRANCH.APBAirProgramPollutants app " &
                "INNER JOIN AIRBRANCH.LookUPPollutants lkpl ON " &
                "  app.STRPOLLUTANTKEY = lkpl.STRPOLLUTANTCODE " &
                "INNER JOIN AIRBRANCH.LookUpComplianceStatus lkcs ON " &
                "  lkcs.STRCOMPLIANCECODE = app.STRCOMPLIANCESTATUS " &
                "INNER JOIN AIRBRANCH.EPDUserProfiles up ON " &
                "  app.STRMODIFINGPERSON = up.NUMUSERID " &
                "WHERE app.STRAIRSNUMBER = :airsNumber " &
                "ORDER BY ""Air Program Code"", ""Pollutant Code"""
            Dim parameter As New OracleParameter("airsNumber", airsNumber.DbFormattedString)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function SaveFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                        airProgram As AirProgram,
                                                        pollutantCode As String,
                                                        Optional complianceStatus As ComplianceStatus = ComplianceStatus.InCompliance,
                                                        Optional operatingStatus As FacilityOperationalStatus = FacilityOperationalStatus.O
                                                        ) As Boolean

            If FacilityAirProgramPollutantExists(airsNumber, airProgram, pollutantCode) Then
                Return UpdateFacilityAirProgramPollutant(airsNumber, airProgram, pollutantCode, complianceStatus, operatingStatus)
            Else
                Return InsertFacilityAirProgramPollutant(airsNumber, airProgram, pollutantCode, complianceStatus, operatingStatus)
            End If
        End Function

        Private Function InsertFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String,
                                                           complianceStatus As ComplianceStatus,
                                                           operatingStatus As FacilityOperationalStatus
                                                           ) As Boolean

            Dim query As String = "INSERT " &
                "INTO AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                "  ( " &
                "    STRAIRSNUMBER, STRAIRPOLLUTANTKEY, STRPOLLUTANTKEY, " &
                "    STRCOMPLIANCESTATUS, STROPERATIONALSTATUS, " &
                "    STRMODIFINGPERSON, DATMODIFINGDATE " &
                "  ) " &
                "  VALUES " &
                "  ( " &
                "    :STRAIRSNUMBER, :STRAIRPOLLUTANTKEY, :STRPOLLUTANTKEY, " &
                "    :STRCOMPLIANCESTATUS, :STROPERATIONALSTATUS, " &
                "    :STRMODIFINGPERSON, sysdate " &
                "  )"

            Dim parameters As OracleParameter() = {
                New OracleParameter("STRAIRSNUMBER", airsNumber.DbFormattedString),
                New OracleParameter("STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New OracleParameter("STRPOLLUTANTKEY", pollutantCode),
                New OracleParameter("STRCOMPLIANCESTATUS", EnforcementCase.ConvertComplianceStatus(complianceStatus).ToString.Substring(7)),
                New OracleParameter("STROPERATIONALSTATUS", operatingStatus.ToString),
                New OracleParameter("STRMODIFINGPERSON", CurrentUser.UserID)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Private Function UpdateFacilityAirProgramPollutant(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String,
                                                           complianceStatus As ComplianceStatus,
                                                           operatingStatus As FacilityOperationalStatus
                                                           ) As Boolean

            Dim query As String = "UPDATE AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                "SET STRCOMPLIANCESTATUS = :STRCOMPLIANCESTATUS, " &
                "  STROPERATIONALSTATUS = :STROPERATIONALSTATUS, " &
                "  STRMODIFINGPERSON = :STRMODIFINGPERSON, " &
                "  DATMODIFINGDATE = sysdate " &
                "WHERE STRAIRPOLLUTANTKEY = :STRAIRPOLLUTANTKEY AND " &
                "  STRPOLLUTANTKEY = :STRPOLLUTANTKEY "

            Dim parameters As OracleParameter() = {
                New OracleParameter("STRCOMPLIANCESTATUS", EnforcementCase.ConvertComplianceStatus(complianceStatus).ToString.Substring(7)),
                New OracleParameter("STROPERATIONALSTATUS", operatingStatus.ToString),
                New OracleParameter("STRMODIFINGPERSON", CurrentUser.UserID),
                New OracleParameter("STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New OracleParameter("STRPOLLUTANTKEY", pollutantCode)
            }

            Return DB.RunCommand(query, parameters)
        End Function

        Private Function FacilityAirProgramPollutantExists(airsNumber As ApbFacilityId,
                                                           airProgram As AirProgram,
                                                           pollutantCode As String
                                                           ) As Boolean

            Dim query As String = "SELECT STRAIRSNUMBER " &
                "FROM AIRBRANCH.APBAIRPROGRAMPOLLUTANTS " &
                "WHERE STRAIRPOLLUTANTKEY = :STRAIRPOLLUTANTKEY " &
                " AND STRPOLLUTANTKEY = :STRPOLLUTANTKEY "

            Dim parameters As OracleParameter() = {
                New OracleParameter("STRAIRPOLLUTANTKEY", airsNumber.DbFormattedString & FacilityHeaderData.ConvertAirProgramToLegacyCode(airProgram.ToString)),
                New OracleParameter("STRPOLLUTANTKEY", pollutantCode)
            }

            Return DB.ValueExists(query, parameters)
        End Function

    End Module

End Namespace