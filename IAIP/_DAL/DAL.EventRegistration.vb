Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Iaip.Apb.Res
Imports EpdIt

Namespace DAL
    Module EventRegistrationData

#Region "Lookups"

        Public Function GetResEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " &
                " FROM RESLK_EVENTSTATUS " &
                " WHERE ACTIVE = '1' " &
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then
                d.AddBlankRow(blankPrompt)
            End If
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

        Public Function GetRegistrationStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As SortedDictionary(Of Integer, String)
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

        Public Function GetResEventsAsDataTable(ByVal toDate As Nullable(Of Date), ByVal fromDate As Nullable(Of Date)) As DataTable
            Throw New NotImplementedException()

            ' TODO: SQL Server migration

            'Try
            '    Dim query As String =
            '        " SELECT RES_EVENT.NUMRES_EVENTID, " &
            '        "     RES_EVENT.STRTITLE, " &
            '        "     RES_EVENT.STRDESCRIPTION, " &
            '        "     RES_EVENT.DATSTARTDATE, " &
            '        "     RES_EVENT.STREVENTSTARTTIME, " &
            '        "     RES_EVENT.STRVENUE, " &
            '        "     RES_EVENT.STRNOTES " &
            '        " FROM RES_EVENT " &
            '        " WHERE RES_EVENT.DATSTARTDATE       IS NOT NULL " &
            '        " AND (TRUNC(RES_EVENT.DATSTARTDATE) >= TRUNC(:pFromDate) " &
            '        " OR :pFromDate                                IS NULL) " &
            '        " AND (TRUNC(RES_EVENT.DATSTARTDATE) <= TRUNC(:pToDate) " &
            '        " OR :pToDate                                  IS NULL) " &
            '        " AND RES_EVENT.ACTIVE                = '1' " &
            '        " ORDER BY RES_EVENT.DATSTARTDATE "

            '    Dim parameters As SqlParameter() = {
            '        New SqlParameter("pFromDate", SqlDbType.DateTime2, fromDate, ParameterDirection.Input),
            '        New SqlParameter("pToDate", SqlDbType.DateTime2, toDate, ParameterDirection.Input)
            '    }

            '    Return DB.GetDataTable(query, parameters)
            'Catch ex As Exception
            '    ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
            '    Return Nothing
            'End Try
        End Function

#End Region

#Region "Event Details"

        Public Function GetResEventByIdAsDataRow(ByVal id As Integer) As DataRow
            Dim query As String =
                " SELECT RES_EVENT.NUMRES_EVENTID, " &
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
                "   RES_EVENT.STRLOGINREQUIRED, " &
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
                " FROM RES_EVENT, " &
                "   RESLK_EVENTSTATUS, " &
                "   EPDUSERPROFILES EP2, " &
                "   EPDUSERPROFILES EP1 " &
                " WHERE RES_EVENT.NUMEVENTSTATUSCODE = RESLK_EVENTSTATUS.NUMRESLK_EVENTSTATUSID(+) " &
                " AND RES_EVENT.STRUSERGCODE         = EP2.NUMUSERID(+) " &
                " AND RES_EVENT.NUMAPBCONTACT        = EP1.NUMUSERID(+) " &
                " AND RES_EVENT.NUMRES_EVENTID      = :pId "

            Dim parameter As New SqlParameter("pId", id)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        '' Not currently used, but may be useful in the future
        'Public Function GetResEventById(ByVal id As Integer) As ResEvent
        '    Dim dataRow As DataRow = GetResEventByIdAsDataRow(id)
        '    Dim resEvent As New ResEvent

        '    FillResEventInfoFromDataRow(dataRow, ResEvent)

        '    Return resEvent
        'End Function

        Public Sub FillResEventInfoFromDataRow(ByVal row As DataRow, ByRef re As ResEvent)
            Dim address As New Address
            With address
                .Street = DBUtilities.GetNullable(Of String)(row("STRADDRESS"))
                .City = DBUtilities.GetNullable(Of String)(row("STRCITY"))
                .State = DBUtilities.GetNullable(Of String)(row("STRSTATE"))
                Dim p As Nullable(Of Decimal) = DBUtilities.GetNullable(Of Decimal)(row("NUMZIPCODE"))
                .PostalCode = If(p, Convert.ToString(p), Nothing)
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
                .Capacity = DBUtilities.GetNullable(Of Decimal)(row("NUMCAPACITY"))
                .Contact = contact
                .Description = DBUtilities.GetNullable(Of String)(row("STRDESCRIPTION"))
                .EndDate = DBUtilities.GetNullable(Of Date)(row("DATENDDATE"))
                .EndTime = DBUtilities.GetNullable(Of String)(row("STREVENTENDTIME"))
                .EventId = row("NUMRES_EVENTID")
                .EventStatus = DBUtilities.GetNullable(Of String)(row("STREVENTSTATUS"))
                .Title = DBUtilities.GetNullable(Of String)(row("STRTITLE"))
                .LoginRequired = Convert.ToBoolean(Convert.ToInt32(DBUtilities.GetNullable(Of String)(row("STRLOGINREQUIRED"))))
                .Notes = DBUtilities.GetNullable(Of String)(row("STRNOTES"))
                .PassCode = DBUtilities.GetNullable(Of String)(row("STRPASSCODE"))
                .StartDate = DBUtilities.GetNullable(Of Date)(row("DATSTARTDATE"))
                .StartTime = DBUtilities.GetNullable(Of String)(row("STREVENTSTARTTIME"))
                .Venue = DBUtilities.GetNullable(Of String)(row("STRVENUE"))
                .WebContact = webContact
            End With
        End Sub

#End Region

#Region "Event Registrants"

        Public Function GetRegistrantsByEventId(ByVal id As Integer) As DataTable
            Dim query As String =
                " SELECT RES_REGISTRATION.NUMRES_REGISTRATIONID, " &
                "   RES_REGISTRATION.DATREGISTRATIONDATETIME, " &
                "   RES_REGISTRATION.STRCOMMENTS, " &
                "   RESLK_REGISTRATIONSTATUS.STRREGISTRATIONSTATUS, " &
                "   OLAPUSERPROFILE.STRFIRSTNAME, " &
                "   OLAPUSERPROFILE.STRLASTNAME, " &
                "   OLAPUSERLOGIN.STRUSEREMAIL, " &
                "   OLAPUSERPROFILE.STRCOMPANYNAME, " &
                "   OLAPUSERPROFILE.STRPHONENUMBER, " &
                "   RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE " &
                " FROM RES_REGISTRATION, " &
                "   OLAPUSERPROFILE, " &
                "   OLAPUSERLOGIN, " &
                "   RESLK_REGISTRATIONSTATUS " &
                " WHERE RES_REGISTRATION.NUMGECOUSERID           = OLAPUSERPROFILE.NUMUSERID " &
                " AND RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE = RESLK_REGISTRATIONSTATUS.NUMRESLK_REGISTRATIONSTATUSID " &
                " AND RES_REGISTRATION.NUMGECOUSERID             = OLAPUSERLOGIN.NUMUSERID " &
                " AND RES_REGISTRATION.NUMRES_EVENTID           = :pId "

            Dim parameter As New SqlParameter("pId", id)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

    End Module
End Namespace