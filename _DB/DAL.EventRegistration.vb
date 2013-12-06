Imports System.Collections.Generic
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace DAL
    Module EventRegistration

#Region "Lookups"

        Public Function GetEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As Dictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " & _
                " FROM AIRBRANCH.RESLK_EVENTSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return d
        End Function

        Public Function GetRegistrationStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As Dictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_REGISTRATIONSTATUSID, " & _
                " STRREGISTRATIONSTATUS " & _
                " FROM AIRBRANCH.RESLK_REGISTRATIONSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STRREGISTRATIONSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return d
        End Function

#End Region

#Region "Events"

        Public Function GetEventsAsDataTable(ByVal toDate As Nullable(Of Date), ByVal fromDate As Nullable(Of Date)) As DataTable
            Try
                Dim query As String = <s><![CDATA[
                    SELECT AIRBRANCH.RES_EVENT.NUMRES_EVENTID,
                        AIRBRANCH.RES_EVENT.STRTITLE,
                        AIRBRANCH.RES_EVENT.STRDESCRIPTION,
                        AIRBRANCH.RES_EVENT.DATSTARTDATE,
                        AIRBRANCH.RES_EVENT.STREVENTSTARTTIME,
                        AIRBRANCH.RES_EVENT.STRVENUE,
                        AIRBRANCH.RES_EVENT.STRNOTES
                    FROM AIRBRANCH.RES_EVENT
                    WHERE AIRBRANCH.RES_EVENT.DATSTARTDATE       IS NOT NULL
                    AND (TRUNC(AIRBRANCH.RES_EVENT.DATSTARTDATE) >= TRUNC(:pFromDate)
                    OR :pFromDate                                IS NULL)
                    AND (TRUNC(AIRBRANCH.RES_EVENT.DATSTARTDATE) <= TRUNC(:pToDate)
                    OR :pToDate                                  IS NULL)
                    AND AIRBRANCH.RES_EVENT.ACTIVE                = '1'
                    ORDER BY AIRBRANCH.RES_EVENT.DATSTARTDATE
                ]]></s>.Value

                Dim parameters As OracleParameter() = { _
                    New OracleParameter("pFromDate", OracleDbType.Date, fromDate, ParameterDirection.Input), _
                    New OracleParameter("pToDate", OracleDbType.Date, toDate, ParameterDirection.Input) _
                }

                Return DB.GetDataTable(query, parameters)
            Catch ex As Exception
                ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
                Return Nothing
            End Try
        End Function

#End Region

    End Module
End Namespace