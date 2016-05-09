﻿Imports System.Data.SqlClient
Imports EpdItDbHelper

Namespace DAL.Sspp

    Module ApplicationData

        Public Function ApplicationExists(ByVal appNumber As String) As Boolean
            If appNumber = "" OrElse Not Integer.TryParse(appNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM AIRBRANCH.SSPPAPPLICATIONMASTER " &
                " WHERE ROWNUM = 1 " &
                " AND SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = :pId "
            Dim parameter As New SqlParameter("pId", appNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        '' Not currently used, but may be useful in the future
        'Private Sub FillApplicationInfoFromDataRow(ByVal row As DataRow, ByRef appInfo As ApplicationInfo)
        '    Dim address As New Address
        '    With address
        '        .City = DBUtilities.GetNullable(Of String)(row("STRFACILITYCITY"))
        '        .State = DBUtilities.GetNullable(Of String)(row("STRFACILITYSTATE"))
        '    End With

        '    Dim location As New Location
        '    With location
        '        .Address = address
        '    End With

        '    Dim facility As New Apb.Facilities.Facility
        '    With facility
        '        .AirsNumber = DBUtilities.GetNullable(Of String)(row("STRAIRSNUMBER"))
        '        .FacilityName = DBUtilities.GetNullable(Of String)(row("STRFACILITYNAME"))
        '        .FacilityLocation = location
        '    End With

        '    Dim staff As New Staff
        '    With staff
        '        .FirstName = DBUtilities.GetNullable(Of String)(row("STRFIRSTNAME"))
        '        .LastName = DBUtilities.GetNullable(Of String)(row("STRLASTNAME"))
        '    End With

        '    With appInfo
        '        .ApplicationNumber = DBUtilities.GetNullable(Of String)(row("STRAPPLICATIONNUMBER"))
        '        .ApplicationType = DBUtilities.GetNullable(Of String)(row("STRAPPLICATIONTYPEDESC"))
        '        .DateIssued = General.NormalizeDate(DBUtilities.GetNullable(Of Date)(row("DATFINALIZEDDATE")))
        '        .PermitType = DBUtilities.GetNullable(Of String)(row("STRPERMITTYPEDESCRIPTION"))
        '        .Facility = facility
        '        .StaffResponsible = staff
        '    End With
        'End Sub

        '' Not currently used, but may be useful in the future
        'Public Function GetApplicationInfo(ByVal appNumber As String) As ApplicationInfo
        '    Dim query As String = <s><![CDATA[
        '            SELECT SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER,
        '              SSPPAPPLICATIONMASTER.STRAIRSNUMBER,
        '              SSPPAPPLICATIONMASTER.DATFINALIZEDDATE,
        '              APBFACILITYINFORMATION.STRFACILITYNAME,
        '              APBFACILITYINFORMATION.STRFACILITYCITY,
        '              APBFACILITYINFORMATION.STRFACILITYSTATE,
        '              LOOKUPPERMITTYPES.STRPERMITTYPEDESCRIPTION,
        '              LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPEDESC,
        '              EPDUSERPROFILES.STRLASTNAME,
        '              EPDUSERPROFILES.STRFIRSTNAME
        '            FROM AIRBRANCH.SSPPAPPLICATIONMASTER
        '            LEFT JOIN AIRBRANCH.APBFACILITYINFORMATION
        '            ON SSPPAPPLICATIONMASTER.STRAIRSNUMBER = APBFACILITYINFORMATION.STRAIRSNUMBER
        '            LEFT JOIN AIRBRANCH.LOOKUPPERMITTYPES
        '            ON SSPPAPPLICATIONMASTER.STRPERMITTYPE = LOOKUPPERMITTYPES.STRPERMITTYPECODE
        '            LEFT JOIN AIRBRANCH.LOOKUPAPPLICATIONTYPES
        '            ON SSPPAPPLICATIONMASTER.STRAPPLICATIONTYPE = LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPECODE
        '            LEFT JOIN AIRBRANCH.EPDUSERPROFILES
        '            ON SSPPAPPLICATIONMASTER.STRSTAFFRESPONSIBLE     = EPDUSERPROFILES.NUMUSERID
        '            WHERE SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = :pID
        '        ]]></s>.Value
        '    Dim parameter As New SqlParameter("pId", appNumber)
        '    Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
        '    If dataTable Is Nothing Then Return Nothing

        '    Dim dataRow As DataRow = dataTable.Rows(0)

        '    Dim appInfo As New ApplicationInfo
        '    FillApplicationInfoFromDataRow(dataRow, appInfo)
        '    Return appInfo
        'End Function

    End Module

End Namespace
