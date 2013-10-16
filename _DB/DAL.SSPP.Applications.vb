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
                Dim address As New Apb.Address
                With address
                    .City = row("STRFACILITYCITY")
                    .State = row("STRFACILITYSTATE")
                End With

                Dim location As New Apb.Location
                With location
                    .Address = address
                End With

                Dim facility As New Apb.Facility
                With facility
                    .AirsNumber = row("STRAIRSNUMBER")
                    .Name = row("STRFACILITYNAME")
                    .FacilityLocation = location
                End With

                Dim staff As New Apb.Staff
                With staff
                    .FirstName = row("STRFIRSTNAME")
                    .LastName = row("STRLASTNAME")
                End With

                With appInfo
                    .ApplicationNumber = row("STRAPPLICATIONNUMBER")
                    .ApplicationType = row("STRAPPLICATIONTYPEDESC")
                    .DateIssued = Apb.NormalizeDate(DB.GetNullable(Of Date)(row("DATFINALIZEDDATE")))
                    .PermitType = row("STRPERMITTYPEDESCRIPTION")
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
                    INNER JOIN LOOKUPAPPLICATIONTYPES
                    ON SSPPAPPLICATIONMASTER.STRAPPLICATIONTYPE = LOOKUPAPPLICATIONTYPES.STRAPPLICATIONTYPECODE
                    INNER JOIN EPDUSERPROFILES
                    ON SSPPAPPLICATIONMASTER.STRSTAFFRESPONSIBLE = EPDUSERPROFILES.NUMUSERID
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
