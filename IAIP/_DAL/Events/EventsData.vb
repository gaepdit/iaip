Imports System.Collections.Generic
Imports Microsoft.Data.SqlClient
Imports GaEpd.DBUtilities
Imports Iaip.Apb.Res

Namespace DAL
    Module EventsData

        Public Function GetRegistrationStatusesAsDictionary(Optional addBlank As Boolean = False, Optional blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_REGISTRATIONSTATUSID, " &
                " STRREGISTRATIONSTATUS " &
                " FROM dbo.RESLK_REGISTRATIONSTATUS " &
                " WHERE ACTIVE = '1' " &
                " ORDER BY STRREGISTRATIONSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then
                d.AddBlankRow(blankPrompt)
            End If
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

        Public Function GetResEventsAsDataTable(showPastEvents As Boolean) As DataTable
            Dim query As String = "SELECT e.NUMRES_EVENTID as [ID],
                   convert(date, e.DATSTARTDATE) as [Date],
                   s.STREVENTSTATUS as [Status],
                   e.STRTITLE as [Event],
                   e.NUMCAPACITY as [Capacity],
                   (SELECT count(*)
                    FROM dbo.RES_REGISTRATION
                    WHERE NUMRES_EVENTID = e.NUMRES_EVENTID
                      AND ACTIVE = '1'
                      AND NUMREGISTRATIONSTATUSCODE = 1) AS [Confirmed],
                   (SELECT count(*)
                    FROM dbo.RES_REGISTRATION
                    WHERE NUMRES_EVENTID = e.NUMRES_EVENTID
                      AND ACTIVE = '1'
                      AND NUMREGISTRATIONSTATUSCODE = 2) AS [WaitingList]
            FROM dbo.RES_EVENT e
                left join dbo.RESLK_EVENTSTATUS s
                on e.NUMEVENTSTATUSCODE = s.NUMRESLK_EVENTSTATUSID
            WHERE e.DATSTARTDATE IS NOT NULL
              AND e.ACTIVE = '1'
              AND (e.DATSTARTDATE >= convert(date, getdate())
                OR @showPastEvents = 1)
            ORDER BY e.DATSTARTDATE desc "

            Dim parameter As New SqlParameter("@showPastEvents", showPastEvents)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetResEventById(eventId As Integer) As ResEvent
            Dim query As String = "select NUMRES_EVENTID,
                   ACTIVE,
                   NUMEVENTSTATUSCODE,
                   STRUSERGCODE,
                   STRTITLE,
                   STRDESCRIPTION,
                   DATSTARTDATE,
                   DATENDDATE,
                   STRVENUE,
                   NUMCAPACITY,
                   STRNOTES,
                   STRPASSCODE,
                   STRADDRESS,
                   STRCITY,
                   STRSTATE,
                   NUMZIPCODE,
                   NUMAPBCONTACT,
                   NUMWEBPHONENUMBER,
                   STREVENTSTARTTIME,
                   STREVENTENDTIME,
                   STRWEBURL
            from dbo.RES_EVENT 
            where NUMRES_EVENTID = @eventId "

            Dim parameter As New SqlParameter("@eventId", eventId)

            Dim row As DataRow = DB.GetDataRow(query, parameter)

            If row Is Nothing Then
                Return Nothing
            End If

            Dim statusCode As Integer = GetNullable(Of Integer)(row("NUMEVENTSTATUSCODE"))
            Dim status As ResEvent.EventState = If(statusCode = 2, ResEvent.EventState.Scheduled, ResEvent.EventState.Cancelled)

            Dim re As New ResEvent
            With re
                .Active = Convert.ToBoolean(Convert.ToInt32(GetNullableString(row("ACTIVE"))))
                .Street = GetNullableString(row("STRADDRESS"))
                .City = GetNullableString(row("STRCITY"))
                .State = GetNullableString(row("STRSTATE"))
                .PostalCode = GetNullableString(row("NUMZIPCODE"))
                .Capacity = GetNullable(Of Integer)(row("NUMCAPACITY"))
                .Description = GetNullableString(row("STRDESCRIPTION"))
                .EndDate = GetNullable(Of Date?)(row("DATENDDATE"))
                .EndTime = GetNullableString(row("STREVENTENDTIME"))
                .EventId = eventId
                .WebLink = GetNullableString(row("STRWEBURL"))
                .EventStatus = status
                .Title = GetNullableString(row("STRTITLE"))
                .Notes = GetNullableString(row("STRNOTES"))
                .PassCode = GetNullableString(row("STRPASSCODE"))
                .StartDate = GetNullable(Of Date)(row("DATSTARTDATE"))
                .StartTime = GetNullableString(row("STREVENTSTARTTIME"))
                .Venue = GetNullableString(row("STRVENUE"))
                .WebContactId = GetNullable(Of Integer)(row("STRUSERGCODE"))
                .WebContactPhone = GetNullableString(row("NUMWEBPHONENUMBER"))
            End With

            Return re
        End Function

        Public Function PasscodeExists(id As String) As Boolean
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM dbo.RES_EVENT WHERE STRPASSCODE = @id"
            Dim parameter As New SqlParameter("@id", id)
            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetRegistrantsByEventId(eventId As Integer) As DataTable
            Dim query As String = "select r.NUMRES_REGISTRATIONID as [ID],
                   r.DATREGISTRATIONDATETIME as [Registration Date],
                   s.STRREGISTRATIONSTATUS as [Registration Status],
                   u.STRSALUTATION as [Salutation],
                   u.STRFIRSTNAME as [First Name],
                   u.STRLASTNAME as [Last Name],
                   u.STRTITLE as [Title],
                   o.STRUSEREMAIL as [Email],
                   u.STRCOMPANYNAME as [Company],
                   u.STRADDRESS as [Address],
                   u.STRCITY as [City],
                   u.STRSTATE as [State],
                   u.STRZIP as [Zip Code],
                   u.STRPHONENUMBER as [Phone],
                   r.STRCOMMENTS as [Comments],
                   u.STRUSERTYPE as [User Type],
                   r.STRCONFIRMATIONNUMBER as [Confirmation Number],
                   r.NUMREGISTRATIONSTATUSCODE as [Status Code]
            from dbo.RES_REGISTRATION r
                left JOIN dbo.RESLK_REGISTRATIONSTATUS s
                ON r.NUMREGISTRATIONSTATUSCODE = s.NUMRESLK_REGISTRATIONSTATUSID
                inner JOIN dbo.OLAPUSERPROFILE u
                ON r.NUMGECOUSERID = u.NUMUSERID
                inner join dbo.OLAPUSERLOGIN o
                on u.NUMUSERID = o.NUMUSERID
            where r.NUMRES_EVENTID = @eventId
            order by r.DATREGISTRATIONDATETIME "

            Dim parameter As New SqlParameter("@eventId", eventId)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function UpdateRegistration(registrationId As Integer, statusCode As Integer) As Boolean
            Dim query As String = "update dbo.RES_REGISTRATION
                set NUMREGISTRATIONSTATUSCODE = @statusCode
                where NUMRES_REGISTRATIONID = @registrationId"

            Dim params As SqlParameter() = {
                New SqlParameter("@statusCode", statusCode),
                New SqlParameter("@registrationId", registrationId)
            }

            Return DB.RunCommand(query, params)
        End Function

        Public Function CreateEvent(resEvent As ResEvent) As Boolean
            Dim query As String = "insert into dbo.RES_EVENT (NUMRES_EVENTID,
                NUMEVENTSTATUSCODE, STRUSERGCODE, STRTITLE, STRDESCRIPTION, DATSTARTDATE,
                DATENDDATE, STRVENUE, NUMCAPACITY, STRNOTES, CREATEDATETIME,
                UPDATEUSER, UPDATEDATETIME, STRPASSCODE, STRADDRESS, STRCITY, STRSTATE,
                STREVENTSTARTTIME, STREVENTENDTIME, STRWEBURL,
                NUMZIPCODE, NUMWEBPHONENUMBER)
            values ((select IIF(max(NUMRES_EVENTID) is null, 1, max(NUMRES_EVENTID) + 1) from RES_EVENT),
                @NUMEVENTSTATUSCODE, @STRUSERGCODE, @STRTITLE, @STRDESCRIPTION, @DATSTARTDATE,
                @DATENDDATE, @STRVENUE, @NUMCAPACITY, @STRNOTES, getdate(),
                @UPDATEUSER, getdate(), @STRPASSCODE, @STRADDRESS, @STRCITY, @STRSTATE,
                @STREVENTSTARTTIME, @STREVENTENDTIME, @STRWEBURL,
                @NUMZIPCODE, @NUMWEBPHONENUMBER) "

            Dim params As SqlParameter() = {
                New SqlParameter("@NUMEVENTSTATUSCODE", resEvent.EventStatus),
                New SqlParameter("@STRUSERGCODE", resEvent.WebContactId),
                New SqlParameter("@STRTITLE", resEvent.Title),
                New SqlParameter("@STRDESCRIPTION", resEvent.Description),
                New SqlParameter("@DATSTARTDATE", resEvent.StartDate),
                New SqlParameter("@DATENDDATE", resEvent.EndDate),
                New SqlParameter("@STRVENUE", resEvent.Venue),
                New SqlParameter("@NUMCAPACITY", resEvent.Capacity),
                New SqlParameter("@STRNOTES", resEvent.Notes),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                New SqlParameter("@STRPASSCODE", resEvent.PassCode),
                New SqlParameter("@STRADDRESS", resEvent.Street),
                New SqlParameter("@STRCITY", resEvent.City),
                New SqlParameter("@STRSTATE", resEvent.State),
                New SqlParameter("@STREVENTSTARTTIME", resEvent.StartTime),
                New SqlParameter("@STREVENTENDTIME", resEvent.EndTime),
                New SqlParameter("@STRWEBURL", resEvent.WebLink),
                SqlParameterWithDbType("@NUMZIPCODE", SqlDbType.VarChar, resEvent.PostalCode),
                New SqlParameter("@NUMWEBPHONENUMBER", resEvent.WebContactPhone)
            }

            Return DB.RunCommand(query, params)
        End Function

        Public Function UpdateEvent(resEvent As ResEvent) As Boolean
            Dim query As String = "update dbo.RES_EVENT
            set NUMEVENTSTATUSCODE = @NUMEVENTSTATUSCODE,
                STRUSERGCODE       = @STRUSERGCODE,
                STRTITLE           = @STRTITLE,
                STRDESCRIPTION     = @STRDESCRIPTION,
                DATSTARTDATE       = @DATSTARTDATE,
                DATENDDATE         = @DATENDDATE,
                STRVENUE           = @STRVENUE,
                NUMCAPACITY        = @NUMCAPACITY,
                STRNOTES           = @STRNOTES,
                UPDATEUSER         = @UPDATEUSER,
                UPDATEDATETIME     = getdate(),
                STRPASSCODE        = @STRPASSCODE,
                STRADDRESS         = @STRADDRESS,
                STRCITY            = @STRCITY,
                STRSTATE           = @STRSTATE,
                STREVENTSTARTTIME  = @STREVENTSTARTTIME,
                STREVENTENDTIME    = @STREVENTENDTIME,
                STRWEBURL          = @STRWEBURL,
                NUMZIPCODE         = @NUMZIPCODE,
                NUMWEBPHONENUMBER  = @NUMWEBPHONENUMBER
            where NUMRES_EVENTID = @eventId "

            Dim params As SqlParameter() = {
                New SqlParameter("@eventId", resEvent.EventId),
                New SqlParameter("@NUMEVENTSTATUSCODE", resEvent.EventStatus),
                New SqlParameter("@STRUSERGCODE", resEvent.WebContactId),
                New SqlParameter("@STRTITLE", resEvent.Title),
                New SqlParameter("@STRDESCRIPTION", resEvent.Description),
                New SqlParameter("@DATSTARTDATE", resEvent.StartDate),
                New SqlParameter("@DATENDDATE", resEvent.EndDate),
                New SqlParameter("@STRVENUE", resEvent.Venue),
                New SqlParameter("@NUMCAPACITY", resEvent.Capacity),
                New SqlParameter("@STRNOTES", resEvent.Notes),
                New SqlParameter("@UPDATEUSER", CurrentUser.UserID),
                New SqlParameter("@STRPASSCODE", resEvent.PassCode),
                New SqlParameter("@STRADDRESS", resEvent.Street),
                New SqlParameter("@STRCITY", resEvent.City),
                New SqlParameter("@STRSTATE", resEvent.State),
                New SqlParameter("@STREVENTSTARTTIME", resEvent.StartTime),
                New SqlParameter("@STREVENTENDTIME", resEvent.EndTime),
                New SqlParameter("@STRWEBURL", resEvent.WebLink),
                SqlParameterWithDbType("@NUMZIPCODE", SqlDbType.VarChar, resEvent.PostalCode),
                New SqlParameter("@NUMWEBPHONENUMBER", resEvent.WebContactPhone)
            }

            Return DB.RunCommand(query, params)
        End Function

        Public Function CancelEvent(eventId As Integer) As Boolean
            Dim query As String = "update dbo.RES_EVENT
                set NUMEVENTSTATUSCODE = 3
                where NUMRES_EVENTID = @eventId"

            Dim param As New SqlParameter("@eventId", eventId)

            Return DB.RunCommand(query, param)
        End Function

        Public Function UncancelEvent(eventId As Integer) As Boolean
            Dim query As String = "update dbo.RES_EVENT
                set NUMEVENTSTATUSCODE = 2
                where NUMRES_EVENTID = @eventId"

            Dim param As New SqlParameter("@eventId", eventId)

            Return DB.RunCommand(query, param)
        End Function

    End Module
End Namespace