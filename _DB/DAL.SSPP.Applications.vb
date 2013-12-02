Imports Oracle.DataAccess.Client
Imports JohnGaltProject.Apb.SSPP

Namespace DAL
    Namespace SSPP
        Module Applications

            Public Function ApplicationExists(ByVal appNumber As String) As Boolean
                Dim query As String = "SELECT 'True' " & _
                    " FROM AIRBRANCH.SSPPAPPLICATIONMASTER " & _
                    " WHERE ROWNUM = 1 " & _
                    " AND SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = :pId " 
                Dim parameter As New OracleParameter("pId", appNumber)

                Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
                Return Convert.ToBoolean(result)
            End Function

            Private Sub FillApplicationInfoFromDataRow(ByVal row As DataRow, ByRef appInfo As ApplicationInfo)
                Dim address As New Address
                With address
                    .City = DB.GetNullable(Of String)(row("STRFACILITYCITY"))
                    .State = DB.GetNullable(Of String)(row("STRFACILITYSTATE"))
                End With

                Dim location As New Location
                With location
                    .Address = address
                End With

                Dim facility As New Apb.Facility
                With facility
                    .AirsNumber = DB.GetNullable(Of String)(row("STRAIRSNUMBER"))
                    .Name = DB.GetNullable(Of String)(row("STRFACILITYNAME"))
                    .FacilityLocation = location
                End With

                Dim staff As New Apb.Staff
                With staff
                    .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                    .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                End With

                With appInfo
                    .ApplicationNumber = DB.GetNullable(Of String)(row("STRAPPLICATIONNUMBER"))
                    .ApplicationType = DB.GetNullable(Of String)(row("STRAPPLICATIONTYPEDESC"))
                    .DateIssued = Apb.NormalizeDate(DB.GetNullable(Of Date)(row("DATFINALIZEDDATE")))
                    .PermitType = DB.GetNullable(Of String)(row("STRPERMITTYPEDESCRIPTION"))
                    .Facility = facility
                    .StaffResponsible = staff
                End With
            End Sub

            Public Function GetApplicationInfo(ByVal appNumber As String) As ApplicationInfo
                Dim query As String = <s><![CDATA[
                    SELECT SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER,
                      SSPPAPPLICATIONMASTER.STRAIRSNUMBER,
                      SSPPAPPLICATIONMASTER.DATFINALIZEDDATE,
                      APBFACILITYINFORMATION.STRFACILITYNAME,
                      APBFACILITYINFORMATION.STRFACILITYCITY,
                      APBFACILITYINFORMATION.STRFACILITYSTATE,
                      LOOKUPPERMITTYPES.STRPERMITTYPEDESCRIPTION,
                      LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPEDESC,
                      EPDUSERPROFILES.STRLASTNAME,
                      EPDUSERPROFILES.STRFIRSTNAME
                    FROM SSPPAPPLICATIONMASTER
                    LEFT JOIN APBFACILITYINFORMATION
                    ON SSPPAPPLICATIONMASTER.STRAIRSNUMBER = APBFACILITYINFORMATION.STRAIRSNUMBER
                    LEFT JOIN LOOKUPPERMITTYPES
                    ON SSPPAPPLICATIONMASTER.STRPERMITTYPE = LOOKUPPERMITTYPES.STRPERMITTYPECODE
                    LEFT JOIN LOOKUPAPPLICATIONTYPES
                    ON SSPPAPPLICATIONMASTER.STRAPPLICATIONTYPE = LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPECODE
                    LEFT JOIN EPDUSERPROFILES
                    ON SSPPAPPLICATIONMASTER.STRSTAFFRESPONSIBLE     = EPDUSERPROFILES.NUMUSERID
                    WHERE SSPPAPPLICATIONMASTER.STRAPPLICATIONNUMBER = :pID
                ]]></s>.Value
                Dim parameter As New OracleParameter("pId", appNumber)
                Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
                If dataTable Is Nothing Then Return Nothing

                Dim dataRow As DataRow = dataTable.Rows(0)

                Dim appInfo As New ApplicationInfo
                FillApplicationInfoFromDataRow(dataRow, appInfo)
                Return appInfo
            End Function

        End Module
    End Namespace
End Namespace
