Imports System.Collections.Generic
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports Iaip.Apb.Res

Namespace DAL
    Module EventRegistrationData

#Region "Lookups"

        Public Function GetResEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " &
                " FROM AIRBRANCH.RESLK_EVENTSTATUS " &
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
                " FROM AIRBRANCH.RESLK_REGISTRATIONSTATUS " &
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
            Try
                Dim query As String =
                    " SELECT RES_EVENT.NUMRES_EVENTID, " &
                    "     RES_EVENT.STRTITLE, " &
                    "     RES_EVENT.STRDESCRIPTION, " &
                    "     RES_EVENT.DATSTARTDATE, " &
                    "     RES_EVENT.STREVENTSTARTTIME, " &
                    "     RES_EVENT.STRVENUE, " &
                    "     RES_EVENT.STRNOTES " &
                    " FROM AIRBRANCH.RES_EVENT " &
                    " WHERE AIRBRANCH.RES_EVENT.DATSTARTDATE       IS NOT NULL " &
                    " AND (TRUNC(AIRBRANCH.RES_EVENT.DATSTARTDATE) >= TRUNC(:pFromDate) " &
                    " OR :pFromDate                                IS NULL) " &
                    " AND (TRUNC(AIRBRANCH.RES_EVENT.DATSTARTDATE) <= TRUNC(:pToDate) " &
                    " OR :pToDate                                  IS NULL) " &
                    " AND AIRBRANCH.RES_EVENT.ACTIVE                = '1' " &
                    " ORDER BY AIRBRANCH.RES_EVENT.DATSTARTDATE "

                Dim parameters As OracleParameter() = {
                    New OracleParameter("pFromDate", OracleDbType.Date, fromDate, ParameterDirection.Input),
                    New OracleParameter("pToDate", OracleDbType.Date, toDate, ParameterDirection.Input)
                }

                Return DB.GetDataTable(query, parameters)
            Catch ex As Exception
                ErrorReport(ex, System.Reflection.MethodBase.GetCurrentMethod.Name)
                Return Nothing
            End Try
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
                " FROM AIRBRANCH.RES_EVENT, " &
                "   AIRBRANCH.RESLK_EVENTSTATUS, " &
                "   AIRBRANCH.EPDUSERPROFILES EP2, " &
                "   AIRBRANCH.EPDUSERPROFILES EP1 " &
                " WHERE AIRBRANCH.RES_EVENT.NUMEVENTSTATUSCODE = AIRBRANCH.RESLK_EVENTSTATUS.NUMRESLK_EVENTSTATUSID(+) " &
                " AND AIRBRANCH.RES_EVENT.STRUSERGCODE         = EP2.NUMUSERID(+) " &
                " AND AIRBRANCH.RES_EVENT.NUMAPBCONTACT        = EP1.NUMUSERID(+) " &
                " AND AIRBRANCH.RES_EVENT.NUMRES_EVENTID      = :pId "

            Dim parameter As New OracleParameter("pId", id)

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
                .Street = DB.GetNullable(Of String)(row("STRADDRESS"))
                .City = DB.GetNullable(Of String)(row("STRCITY"))
                .State = DB.GetNullable(Of String)(row("STRSTATE"))
                Dim p As Nullable(Of Decimal) = DB.GetNullable(Of Decimal)(row("NUMZIPCODE"))
                .PostalCode = If(p, Convert.ToString(p), Nothing)
            End With

            Dim contact As New IaipUser
            With contact
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRPHONE"))
                .EmailAddress = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .UserID = Convert.ToInt32(DB.GetNullable(Of Decimal)(row("NUMAPBCONTACT")))
            End With

            Dim webContact As New IaipUser
            With webContact
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME2"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME2"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRPHONE2"))
                .EmailAddress = DB.GetNullable(Of String)(row("STREMAILADDRESS2"))
                .UserID = Convert.ToInt32(DB.GetNullable(Of Decimal)(row("STRUSERGCODE")))
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
                " FROM AIRBRANCH.RES_REGISTRATION, " &
                "   AIRBRANCH.OLAPUSERPROFILE, " &
                "   AIRBRANCH.OLAPUSERLOGIN, " &
                "   AIRBRANCH.RESLK_REGISTRATIONSTATUS " &
                " WHERE AIRBRANCH.RES_REGISTRATION.NUMGECOUSERID           = AIRBRANCH.OLAPUSERPROFILE.NUMUSERID " &
                " AND AIRBRANCH.RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE = AIRBRANCH.RESLK_REGISTRATIONSTATUS.NUMRESLK_REGISTRATIONSTATUSID " &
                " AND AIRBRANCH.RES_REGISTRATION.NUMGECOUSERID             = AIRBRANCH.OLAPUSERLOGIN.NUMUSERID " &
                " AND AIRBRANCH.RES_REGISTRATION.NUMRES_EVENTID           = :pId "

            Dim parameter As New OracleParameter("pId", id)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

    End Module
End Namespace