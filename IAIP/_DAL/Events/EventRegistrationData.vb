Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.Apb.Res
Imports EpdIt

Namespace DAL
    Module EventRegistrationData

#Region "Lookups"

        Public Function GetResEventStatusesAsDictionary(Optional addBlank As Boolean = False, Optional blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT distinct NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " &
                " FROM RESLK_EVENTSTATUS " &
                " WHERE ACTIVE = '1' " &
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then
                d.AddBlankRow(blankPrompt)
            End If
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

        Public Function GetRegistrationStatusesAsDictionary(Optional addBlank As Boolean = False, Optional blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_REGISTRATIONSTATUSID, " &
                " STRREGISTRATIONSTATUS " &
                " FROM RESLK_REGISTRATIONSTATUS " &
                " WHERE ACTIVE = '1' " &
                " ORDER BY STRREGISTRATIONSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then
                d.AddBlankRow(blankPrompt)
            End If
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

#End Region

#Region "Events"

        Public Function GetResEventsAsDataTable(toDate As Date?, fromDate As Date?) As DataTable
            Dim query As String =
                " SELECT convert(int,RES_EVENT.NUMRES_EVENTID) as NUMRES_EVENTID, " &
                "     RES_EVENT.STRTITLE, " &
                "     RES_EVENT.STRDESCRIPTION, " &
                "     RES_EVENT.DATSTARTDATE, " &
                "     RES_EVENT.STREVENTSTARTTIME, " &
                "     RES_EVENT.STRVENUE, " &
                "     RES_EVENT.STRNOTES " &
                " FROM RES_EVENT " &
                " WHERE RES_EVENT.DATSTARTDATE       IS NOT NULL " &
                " AND (RES_EVENT.DATSTARTDATE >= @pFromDate " &
                " OR @pFromDate                                IS NULL) " &
                " AND (RES_EVENT.DATSTARTDATE <= @pToDate " &
                " OR @pToDate                                  IS NULL) " &
                " AND RES_EVENT.ACTIVE                = '1' " &
                " ORDER BY RES_EVENT.DATSTARTDATE "

            Dim parameters As SqlParameter() = {
                    New SqlParameter("@pFromDate", fromDate),
                    New SqlParameter("@pToDate", toDate)
                }

            Return DB.GetDataTable(query, parameters)
        End Function

#End Region

#Region "Event Details"

        Public Function GetResEventByIdAsDataRow(id As Integer) As DataRow
            Dim query As String =
                " SELECT distinct convert(int,RES_EVENT.NUMRES_EVENTID) as NUMRES_EVENTID, " &
                "   RES_EVENT.ACTIVE, " &
                "   RESLK_EVENTSTATUS.STREVENTSTATUS, " &
                "   RES_EVENT.STRUSERGCODE, " &
                "   RES_EVENT.STRTITLE, " &
                "   RES_EVENT.STRDESCRIPTION, " &
                "   RES_EVENT.DATSTARTDATE, " &
                "   RES_EVENT.DATENDDATE, " &
                "   RES_EVENT.STRVENUE, " &
                "   RES_EVENT.NUMCAPACITY, " &
                "   RES_EVENT.STRNOTES, " &
                "   RES_EVENT.STRPASSCODE, " &
                "   RES_EVENT.STRADDRESS, " &
                "   RES_EVENT.STRCITY, " &
                "   RES_EVENT.STRSTATE, " &
                "   RES_EVENT.NUMZIPCODE, " &
                "   RES_EVENT.NUMAPBCONTACT, " &
                "   RES_EVENT.NUMWEBPHONENUMBER, " &
                "   RES_EVENT.STREVENTSTARTTIME, " &
                "   RES_EVENT.STREVENTENDTIME, " &
                "   EP1.STRLASTNAME, " &
                "   EP1.STRFIRSTNAME, " &
                "   EP1.STRPHONE, " &
                "   EP1.STREMAILADDRESS, " &
                "   EP2.STRLASTNAME     AS STRLASTNAME2, " &
                "   EP2.STRFIRSTNAME    AS STRFIRSTNAME2, " &
                "   EP2.STREMAILADDRESS AS STREMAILADDRESS2, " &
                "   EP2.STRPHONE        AS STRPHONE2 " &
                " FROM RES_EVENT " &
                "  left join RESLK_EVENTSTATUS " &
                " on RES_EVENT.NUMEVENTSTATUSCODE = RESLK_EVENTSTATUS.NUMRESLK_EVENTSTATUSID " &
                "  left join EPDUSERPROFILES EP2 " &
                " on RES_EVENT.STRUSERGCODE         = EP2.NUMUSERID " &
                "  left join EPDUSERPROFILES EP1 " &
                " on RES_EVENT.NUMAPBCONTACT        = EP1.NUMUSERID " &
                " where convert(int,RES_EVENT.NUMRES_EVENTID)      = @pId "

            Dim parameter As New SqlParameter("@pId", id)

            Return DB.GetDataRow(query, parameter)
        End Function

        Public Sub FillResEventInfoFromDataRow(row As DataRow, ByRef re As ResEvent)
            Dim address As New Address
            With address
                .Street = DBUtilities.GetNullable(Of String)(row("STRADDRESS"))
                .City = DBUtilities.GetNullable(Of String)(row("STRCITY"))
                .State = DBUtilities.GetNullable(Of String)(row("STRSTATE"))
                Dim p As Decimal? = DBUtilities.GetNullable(Of Decimal)(row("NUMZIPCODE"))
                .PostalCode = If(p.HasValue, Convert.ToString(p.Value), Nothing)
            End With

            Dim contact As New IaipUser
            With contact
                .FirstName = DBUtilities.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DBUtilities.GetNullable(Of String)(row("STRLASTNAME"))
                .PhoneNumber = DBUtilities.GetNullable(Of String)(row("STRPHONE"))
                .EmailAddress = DBUtilities.GetNullable(Of String)(row("STREMAILADDRESS"))
                .UserID = Convert.ToInt32(DBUtilities.GetNullable(Of Decimal)(row("NUMAPBCONTACT")))
            End With

            Dim webContact As New IaipUser
            With webContact
                .FirstName = DBUtilities.GetNullable(Of String)(row("STRFIRSTNAME2"))
                .LastName = DBUtilities.GetNullable(Of String)(row("STRLASTNAME2"))
                .PhoneNumber = DBUtilities.GetNullable(Of String)(row("STRPHONE2"))
                .EmailAddress = DBUtilities.GetNullable(Of String)(row("STREMAILADDRESS2"))
                .UserID = Convert.ToInt32(DBUtilities.GetNullable(Of Decimal)(row("STRUSERGCODE")))
            End With

            With re
                .Active = Convert.ToBoolean(Convert.ToInt32(DBUtilities.GetNullable(Of String)(row("ACTIVE"))))
                .Address = address
                .Capacity = DBUtilities.GetNullable(Of Integer)(row("NUMCAPACITY"))
                .Contact = contact
                .Description = DBUtilities.GetNullable(Of String)(row("STRDESCRIPTION"))
                .EndDate = DBUtilities.GetNullable(Of Date)(row("DATENDDATE"))
                .EndTime = DBUtilities.GetNullable(Of String)(row("STREVENTENDTIME"))
                .EventId = CInt(row("NUMRES_EVENTID"))
                .EventStatus = DBUtilities.GetNullable(Of String)(row("STREVENTSTATUS"))
                .Title = DBUtilities.GetNullable(Of String)(row("STRTITLE"))
                .Notes = DBUtilities.GetNullable(Of String)(row("STRNOTES"))
                .PassCode = DBUtilities.GetNullable(Of String)(row("STRPASSCODE"))
                .StartDate = DBUtilities.GetNullable(Of Date)(row("DATSTARTDATE"))
                .StartTime = DBUtilities.GetNullable(Of String)(row("STREVENTSTARTTIME"))
                .Venue = DBUtilities.GetNullable(Of String)(row("STRVENUE"))
                .WebContact = webContact
            End With
        End Sub

        Public Function PasscodeExists(id As String) As Boolean
            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) FROM RES_EVENT WHERE STRPASSCODE = @id"
            Dim parameter As New SqlParameter("@id", id)
            Return DB.GetBoolean(query, parameter)
        End Function

#End Region

#Region "Event Registrants"

        Public Function GetRegistrantsByEventId(id As Integer) As DataTable
            Try
                Dim query As String =
                "SELECT r.NUMRES_REGISTRATIONID,
                       r.DATREGISTRATIONDATETIME,
                       u.STRFIRSTNAME,
                       u.STRLASTNAME,
                       r.STRCOMMENTS,
                       s.STRREGISTRATIONSTATUS,
                       o.STRUSEREMAIL,
                       u.STRCOMPANYNAME,
                       u.STRPHONENUMBER,
                       u.STRADDRESS,
                       u.STRCITY,
                       u.STRSTATE,
                       u.STRZIP,
                       r.NUMREGISTRATIONSTATUSCODE
                FROM RES_REGISTRATION r
                     inner JOIN RESLK_REGISTRATIONSTATUS s
                                ON r.NUMREGISTRATIONSTATUSCODE = s.NUMRESLK_REGISTRATIONSTATUSID
                     inner JOIN OLAPUSERPROFILE u
                                ON r.NUMGECOUSERID = u.NUMUSERID
                     inner join OLAPUSERLOGIN o
                                on u.NUMUSERID = o.NUMUSERID
                WHERE Convert(int, r.NUMRES_EVENTID) = @pId"

                Dim parameter As New SqlParameter("@pId", id)

                Return DB.GetDataTable(query, parameter)

            Catch ex As Exception
                ErrorReport(ex, "Module DAL.EventRegistrationData." & Reflection.MethodBase.GetCurrentMethod.Name)
                Return Nothing
            End Try
        End Function

#End Region

    End Module
End Namespace