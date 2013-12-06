Imports System.Collections.Generic
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports JohnGaltProject.Apb.Res

Namespace DAL
    Module EventRegistration

#Region "Lookups"

        Public Function GetResEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As Dictionary(Of Integer, String)
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

        Public Function GetResEventsAsDataTable(ByVal toDate As Nullable(Of Date), ByVal fromDate As Nullable(Of Date)) As DataTable
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

#Region "Event Details"

        Public Function GetResEventById(ByVal id As Integer) As ResEvent
            Dim query As String = <s><![CDATA[
                SELECT AIRBRANCH.RES_EVENT.NUMRES_EVENTID,
                  AIRBRANCH.RES_EVENT.ACTIVE,
                  AIRBRANCH.RESLK_EVENTSTATUS.STREVENTSTATUS,
                  AIRBRANCH.RES_EVENT.STRUSERGCODE,
                  AIRBRANCH.RES_EVENT.STRTITLE,
                  AIRBRANCH.RES_EVENT.STRDESCRIPTION,
                  AIRBRANCH.RES_EVENT.DATSTARTDATE,
                  AIRBRANCH.RES_EVENT.DATENDDATE,
                  AIRBRANCH.RES_EVENT.STRVENUE,
                  AIRBRANCH.RES_EVENT.NUMCAPACITY,
                  AIRBRANCH.RES_EVENT.STRNOTES,
                  AIRBRANCH.RES_EVENT.STRLOGINREQUIRED,
                  AIRBRANCH.RES_EVENT.STRPASSCODE,
                  AIRBRANCH.RES_EVENT.STRADDRESS,
                  AIRBRANCH.RES_EVENT.STRCITY,
                  AIRBRANCH.RES_EVENT.STRSTATE,
                  AIRBRANCH.RES_EVENT.NUMZIPCODE,
                  AIRBRANCH.RES_EVENT.NUMAPBCONTACT,
                  AIRBRANCH.RES_EVENT.NUMWEBPHONENUMBER,
                  AIRBRANCH.RES_EVENT.STREVENTSTARTTIME,
                  AIRBRANCH.RES_EVENT.STREVENTENDTIME,
                  EP1.STRLASTNAME,
                  EP1.STRFIRSTNAME,
                  EP1.STRPHONE,
                  EP1.STREMAILADDRESS,
                  EP2.STRLASTNAME     AS STRLASTNAME2,
                  EP2.STRFIRSTNAME    AS STRFIRSTNAME2,
                  EP2.STREMAILADDRESS AS STREMAILADDRESS2,
                  EP2.STRPHONE        AS STRPHONE2
                FROM AIRBRANCH.RES_EVENT,
                  AIRBRANCH.RESLK_EVENTSTATUS,
                  AIRBRANCH.EPDUSERPROFILES EP2,
                  AIRBRANCH.EPDUSERPROFILES EP1
                WHERE AIRBRANCH.RES_EVENT.NUMEVENTSTATUSCODE = AIRBRANCH.RESLK_EVENTSTATUS.NUMRESLK_EVENTSTATUSID(+)
                AND AIRBRANCH.RES_EVENT.STRUSERGCODE         = EP2.NUMUSERID(+)
                AND AIRBRANCH.RES_EVENT.NUMAPBCONTACT        = EP1.NUMUSERID(+)
                AND AIRBRANCH.RES_EVENT.NUMRES_EVENTID      = :pId
            ]]></s>.Value

            Dim parameter As New OracleParameter("pId", id)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)
            Dim resEvent As New ResEvent
            FillResEventInfoFromDataRow(dataRow, resEvent)

            Return resEvent
        End Function

        Private Sub FillResEventInfoFromDataRow(ByVal row As DataRow, ByRef re As ResEvent)
            Dim address As New Address
            With address
                .Street = DB.GetNullable(Of String)(row("STRADDRESS"))
                .City = DB.GetNullable(Of String)(row("STRCITY"))
                .State = DB.GetNullable(Of String)(row("STRSTATE"))
                .PostalCode = Convert.ToString(DB.GetNullable(Of Decimal)(row("NUMZIPCODE")))
            End With

            Dim contact As New Staff
            With contact
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .Phone = DB.GetNullable(Of String)(row("STRPHONE"))
                .Email = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .StaffId = Convert.ToInt32(DB.GetNullable(Of Decimal)(row("NUMAPBCONTACT")))
            End With

            Dim webContact As New Staff
            With webContact
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME2"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME2"))
                .Phone = DB.GetNullable(Of String)(row("STRPHONE2"))
                .Email = DB.GetNullable(Of String)(row("STREMAILADDRESS2"))
                .StaffId = Convert.ToInt32(DB.GetNullable(Of Decimal)(row("STRUSERGCODE")))
            End With

            With re
                .Active = Convert.ToBoolean(Convert.ToInt32(DB.GetNullable(Of String)(row("ACTIVE"))))
                .Address = address
                .Capacity = DB.GetNullable(Of Decimal)(row("NUMCAPACITY"))
                .Contact = contact
                .Description = DB.GetNullable(Of String)(row("STRDESCRIPTION"))
                .EndDate = DB.GetNullable(Of Date)(row("DATENDDATE"))
                .EndTime = DB.GetNullable(Of String)(row("STREVENTENDTIME"))
                .EventId = row("NUMRES_EVENTID")
                .EventStatus = DB.GetNullable(Of String)(row("STREVENTSTATUS"))
                .Title = DB.GetNullable(Of String)(row("STRTITLE"))
                .LoginRequired = Convert.ToBoolean(Convert.ToInt32(DB.GetNullable(Of String)(row("STRLOGINREQUIRED"))))
                .Notes = DB.GetNullable(Of String)(row("STRNOTES"))
                .PassCode = DB.GetNullable(Of String)(row("STRPASSCODE"))
                .StartDate = DB.GetNullable(Of Date)(row("DATSTARTDATE"))
                .StartTime = DB.GetNullable(Of String)(row("STREVENTSTARTTIME"))
                .Venue = DB.GetNullable(Of String)(row("STRVENUE"))
                .WebContact = webContact
            End With
        End Sub

#End Region

#Region "Event Registrants"

        Public Function GetRegistrantsByEventId(ByVal id As Integer) As DataTable
            Dim query As String = <s><![CDATA[
                SELECT AIRBRANCH.RES_REGISTRATION.NUMRES_REGISTRATIONID,
                  AIRBRANCH.RES_REGISTRATION.DATREGISTRATIONDATETIME,
                  AIRBRANCH.RES_REGISTRATION.STRCOMMENTS,
                  AIRBRANCH.RESLK_REGISTRATIONSTATUS.STRREGISTRATIONSTATUS,
                  AIRBRANCH.OLAPUSERPROFILE.STRFIRSTNAME,
                  AIRBRANCH.OLAPUSERPROFILE.STRLASTNAME,
                  AIRBRANCH.OLAPUSERLOGIN.STRUSEREMAIL,
                  AIRBRANCH.OLAPUSERPROFILE.STRCOMPANYNAME,
                  AIRBRANCH.OLAPUSERPROFILE.STRPHONENUMBER,
                  AIRBRANCH.RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE
                FROM AIRBRANCH.RES_REGISTRATION,
                  AIRBRANCH.OLAPUSERPROFILE,
                  AIRBRANCH.OLAPUSERLOGIN,
                  AIRBRANCH.RESLK_REGISTRATIONSTATUS
                WHERE AIRBRANCH.RES_REGISTRATION.NUMGECOUSERID           = AIRBRANCH.OLAPUSERPROFILE.NUMUSERID
                AND AIRBRANCH.RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE = AIRBRANCH.RESLK_REGISTRATIONSTATUS.NUMRESLK_REGISTRATIONSTATUSID
                AND AIRBRANCH.RES_REGISTRATION.NUMGECOUSERID             = AIRBRANCH.OLAPUSERLOGIN.NUMUSERID
                AND AIRBRANCH.RES_REGISTRATION.NUMRES_EVENTID           = :pId
            ]]></s>.Value

            Dim parameter As New OracleParameter("pId", id)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

    End Module
End Namespace