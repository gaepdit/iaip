﻿Imports System.Data.SqlClient
Imports EpdIt

Namespace DAL.Sspp

    Module ApplicationData

        Public Function ApplicationExists(appNumber As String) As Boolean
            If appNumber = "" OrElse Not Integer.TryParse(appNumber, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM SSPPAPPLICATIONMASTER " &
                " WHERE SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = @id "

            Dim parameter As New SqlParameter("@id", appNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetApplicationTypes(Optional includeInactive As Boolean = False) As DataTable
            Dim query As String = "SELECT CONVERT(int, STRAPPLICATIONTYPECODE) AS [Application Type Code], 
                STRAPPLICATIONTYPEDESC AS [Application Type],
                CASE WHEN STRAPPLICATIONTYPEUSED ='False' THEN 'Inactive' ELSE 'Active' END AS [Status]
                FROM LOOKUPAPPLICATIONTYPES"

            If Not includeInactive Then
                query &= " WHERE STRAPPLICATIONTYPEUSED <> 'False' OR STRAPPLICATIONTYPEUSED IS NULL "

            End If

            query &= " ORDER BY STRAPPLICATIONTYPEDESC "

            Return DB.GetDataTable(query)
        End Function

        Public Function SaveNewApplicationType(description As String) As Boolean
            If String.IsNullOrWhiteSpace(description) Then
                Return False
            End If

            Dim query As String = "INSERT INTO LOOKUPAPPLICATIONTYPES 
                (STRAPPLICATIONTYPECODE, STRAPPLICATIONTYPEDESC, STRAPPLICATIONTYPEUSED)
                VALUES 
                ( (SELECT MAX(CONVERT(int, STRAPPLICATIONTYPECODE)) FROM LOOKUPAPPLICATIONTYPES), @apptype, NULL)"

            Dim p As New SqlParameter("@apptype", description)

            Return DB.RunCommand(query, p)
        End Function

        Public Function UpdateApplicationTypeStatus(code As Integer, status As ActiveOrInactive) As Boolean
            Dim query As String = "UPDATE LOOKUPAPPLICATIONTYPES
                SET STRAPPLICATIONTYPEUSED = @status
                WHERE STRAPPLICATIONTYPECODE = @code"

            Dim p As SqlParameter() = {
                New SqlParameter("@status", status.Equals(ActiveOrInactive.Active).ToString),
                New SqlParameter("@code", code.ToString)
            }

            Return DB.RunCommand(query, p)
        End Function

        '' Not currently used, but may be useful in the future
        'Private Sub FillApplicationInfoFromDataRow(row As DataRow, ByRef appInfo As ApplicationInfo)
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
        'Public Function GetApplicationInfo(appNumber As String) As ApplicationInfo
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
        '            FROM SSPPAPPLICATIONMASTER
        '            LEFT JOIN APBFACILITYINFORMATION
        '            ON SSPPAPPLICATIONMASTER.STRAIRSNUMBER = APBFACILITYINFORMATION.STRAIRSNUMBER
        '            LEFT JOIN LOOKUPPERMITTYPES
        '            ON SSPPAPPLICATIONMASTER.STRPERMITTYPE = LOOKUPPERMITTYPES.STRPERMITTYPECODE
        '            LEFT JOIN LOOKUPAPPLICATIONTYPES
        '            ON SSPPAPPLICATIONMASTER.STRAPPLICATIONTYPE = LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPECODE
        '            LEFT JOIN EPDUSERPROFILES
        '            ON SSPPAPPLICATIONMASTER.STRSTAFFRESPONSIBLE     = EPDUSERPROFILES.NUMUSERID
        '            WHERE SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = @pID
        '        ]]></s>.Value
        '    Dim parameter As New SqlParameter("@pId", appNumber)
        '    Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
        '    If dataTable Is Nothing Then Return Nothing

        '    Dim dataRow As DataRow = dataTable.Rows(0)

        '    Dim appInfo As New ApplicationInfo
        '    FillApplicationInfoFromDataRow(dataRow, appInfo)
        '    Return appInfo
        'End Function

    End Module

End Namespace
