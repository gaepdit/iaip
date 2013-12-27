Imports System.Collections.Generic
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports JohnGaltProject.Apb.Res

Namespace DAL
    Module EventRegistration

#Region "Lookups"

        Public Function GetResEventStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_EVENTSTATUSID, STREVENTSTATUS " & _
                " FROM " & DBNameSpace & ".RESLK_EVENTSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STREVENTSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

        Public Function GetRegistrationStatusesAsDictionary(Optional ByVal addBlank As Boolean = False, Optional ByVal blankPrompt As String = "") As SortedDictionary(Of Integer, String)
            Dim query As String = " SELECT NUMRESLK_REGISTRATIONSTATUSID, " & _
                " STRREGISTRATIONSTATUS " & _
                " FROM " & DBNameSpace & ".RESLK_REGISTRATIONSTATUS " & _
                " WHERE ACTIVE = '1' " & _
                " ORDER BY STRREGISTRATIONSTATUS "
            Dim d As Dictionary(Of Integer, String) = DB.GetLookupDictionary(query)
            If addBlank Then DB.AddBlankRowToDictionary(d, blankPrompt)
            Return New SortedDictionary(Of Integer, String)(d)
        End Function

#End Region

#Region "Events"

        Public Function GetResEventsAsDataTable(ByVal toDate As Nullable(Of Date), ByVal fromDate As Nullable(Of Date)) As DataTable
            Try
                Dim query As String = _
                    " SELECT RES_EVENT.NUMRES_EVENTID, " & _
                    "     RES_EVENT.STRTITLE, " & _
                    "     RES_EVENT.STRDESCRIPTION, " & _
                    "     RES_EVENT.DATSTARTDATE, " & _
                    "     RES_EVENT.STREVENTSTARTTIME, " & _
                    "     RES_EVENT.STRVENUE, " & _
                    "     RES_EVENT.STRNOTES " & _
                    " FROM " & DBNameSpace & ".RES_EVENT " & _
                    " WHERE " & DBNameSpace & ".RES_EVENT.DATSTARTDATE       IS NOT NULL " & _
                    " AND (TRUNC(" & DBNameSpace & ".RES_EVENT.DATSTARTDATE) >= TRUNC(:pFromDate) " & _
                    " OR :pFromDate                                IS NULL) " & _
                    " AND (TRUNC(" & DBNameSpace & ".RES_EVENT.DATSTARTDATE) <= TRUNC(:pToDate) " & _
                    " OR :pToDate                                  IS NULL) " & _
                    " AND " & DBNameSpace & ".RES_EVENT.ACTIVE                = '1' " & _
                    " ORDER BY " & DBNameSpace & ".RES_EVENT.DATSTARTDATE "

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

        Public Function GetResEventByIdAsDataRow(ByVal id As Integer) As DataRow
            Dim query As String = _
                " SELECT RES_EVENT.NUMRES_EVENTID, " & _
                "   RES_EVENT.ACTIVE, " & _
                "   RESLK_EVENTSTATUS.STREVENTSTATUS, " & _
                "   RES_EVENT.STRUSERGCODE, " & _
                "   RES_EVENT.STRTITLE, " & _
                "   RES_EVENT.STRDESCRIPTION, " & _
                "   RES_EVENT.DATSTARTDATE, " & _
                "   RES_EVENT.DATENDDATE, " & _
                "   RES_EVENT.STRVENUE, " & _
                "   RES_EVENT.NUMCAPACITY, " & _
                "   RES_EVENT.STRNOTES, " & _
                "   RES_EVENT.STRLOGINREQUIRED, " & _
                "   RES_EVENT.STRPASSCODE, " & _
                "   RES_EVENT.STRADDRESS, " & _
                "   RES_EVENT.STRCITY, " & _
                "   RES_EVENT.STRSTATE, " & _
                "   RES_EVENT.NUMZIPCODE, " & _
                "   RES_EVENT.NUMAPBCONTACT, " & _
                "   RES_EVENT.NUMWEBPHONENUMBER, " & _
                "   RES_EVENT.STREVENTSTARTTIME, " & _
                "   RES_EVENT.STREVENTENDTIME, " & _
                "   EP1.STRLASTNAME, " & _
                "   EP1.STRFIRSTNAME, " & _
                "   EP1.STRPHONE, " & _
                "   EP1.STREMAILADDRESS, " & _
                "   EP2.STRLASTNAME     AS STRLASTNAME2, " & _
                "   EP2.STRFIRSTNAME    AS STRFIRSTNAME2, " & _
                "   EP2.STREMAILADDRESS AS STREMAILADDRESS2, " & _
                "   EP2.STRPHONE        AS STRPHONE2 " & _
                " FROM " & DBNameSpace & ".RES_EVENT, " & _
                "   " & DBNameSpace & ".RESLK_EVENTSTATUS, " & _
                "   " & DBNameSpace & ".EPDUSERPROFILES EP2, " & _
                "   " & DBNameSpace & ".EPDUSERPROFILES EP1 " & _
                " WHERE " & DBNameSpace & ".RES_EVENT.NUMEVENTSTATUSCODE = " & DBNameSpace & ".RESLK_EVENTSTATUS.NUMRESLK_EVENTSTATUSID(+) " & _
                " AND " & DBNameSpace & ".RES_EVENT.STRUSERGCODE         = EP2.NUMUSERID(+) " & _
                " AND " & DBNameSpace & ".RES_EVENT.NUMAPBCONTACT        = EP1.NUMUSERID(+) " & _
                " AND " & DBNameSpace & ".RES_EVENT.NUMRES_EVENTID      = :pId "

            Dim parameter As New OracleParameter("pId", id)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Return dataTable.Rows(0)
        End Function

        Public Function GetResEventById(ByVal id As Integer) As ResEvent
            Dim dataRow As DataRow = GetResEventByIdAsDataRow(id)
            Dim resEvent As New ResEvent

            FillResEventInfoFromDataRow(dataRow, ResEvent)

            Return resEvent
        End Function

        Public Sub FillResEventInfoFromDataRow(ByVal row As DataRow, ByRef re As ResEvent)
            Dim address As New Address
            With address
                .Street = DB.GetNullable(Of String)(row("STRADDRESS"))
                .City = DB.GetNullable(Of String)(row("STRCITY"))
                .State = DB.GetNullable(Of String)(row("STRSTATE"))
                Dim p As Nullable(Of Decimal) = DB.GetNullable(Of Decimal)(row("NUMZIPCODE"))
                .PostalCode = If(p, Convert.ToString(p), Nothing)
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
            Dim query As String = _
                " SELECT RES_REGISTRATION.NUMRES_REGISTRATIONID, " & _
                "   RES_REGISTRATION.DATREGISTRATIONDATETIME, " & _
                "   RES_REGISTRATION.STRCOMMENTS, " & _
                "   RESLK_REGISTRATIONSTATUS.STRREGISTRATIONSTATUS, " & _
                "   OLAPUSERPROFILE.STRFIRSTNAME, " & _
                "   OLAPUSERPROFILE.STRLASTNAME, " & _
                "   OLAPUSERLOGIN.STRUSEREMAIL, " & _
                "   OLAPUSERPROFILE.STRCOMPANYNAME, " & _
                "   OLAPUSERPROFILE.STRPHONENUMBER, " & _
                "   RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE " & _
                " FROM " & DBNameSpace & ".RES_REGISTRATION, " & _
                "   " & DBNameSpace & ".OLAPUSERPROFILE, " & _
                "   " & DBNameSpace & ".OLAPUSERLOGIN, " & _
                "   " & DBNameSpace & ".RESLK_REGISTRATIONSTATUS " & _
                " WHERE " & DBNameSpace & ".RES_REGISTRATION.NUMGECOUSERID           = " & DBNameSpace & ".OLAPUSERPROFILE.NUMUSERID " & _
                " AND " & DBNameSpace & ".RES_REGISTRATION.NUMREGISTRATIONSTATUSCODE = " & DBNameSpace & ".RESLK_REGISTRATIONSTATUS.NUMRESLK_REGISTRATIONSTATUSID " & _
                " AND " & DBNameSpace & ".RES_REGISTRATION.NUMGECOUSERID             = " & DBNameSpace & ".OLAPUSERLOGIN.NUMUSERID " & _
                " AND " & DBNameSpace & ".RES_REGISTRATION.NUMRES_EVENTID           = :pId "

            Dim parameter As New OracleParameter("pId", id)

            Return DB.GetDataTable(query, parameter)
        End Function

#End Region

    End Module
End Namespace