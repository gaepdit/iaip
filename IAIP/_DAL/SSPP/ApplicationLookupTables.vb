Namespace DAL.Sspp
    Module ApplicationLookupTables

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

        Public Function GetEngineersList() As DataTable
            Dim query As String = "SELECT 'N/A' AS EngineerName, 0 AS NUMUSERID
                UNION
                SELECT concat_ws(', ', u.STRLASTNAME, u.STRFIRSTNAME) AS EngineerName, u.NUMUSERID
                from (select NUMUSERID FROM EPDUSERPROFILES WHERE NUMPROGRAM = 5
                      UNION
                      select STRSTAFFRESPONSIBLE from SSPPAPPLICATIONMASTER) t
                    inner join EPDUSERPROFILES u on t.NUMUSERID = u.NUMUSERID"

            Return DB.GetDataTable(query)
        End Function

        Public Function GetPermitTypes() As DataTable
            Dim query As String = "SELECT STRPERMITTYPECODE, STRPERMITTYPEDESCRIPTION 
                FROM LOOKUPPERMITTYPES WHERE STRTYPEUSED <> 'False' OR STRTYPEUSED IS NULL
                UNION SELECT '', ' ' ORDER BY STRPERMITTYPEDESCRIPTION"

            Return DB.GetDataTable(query)
        End Function

        Public Function GetSsppUnits() As DataTable
            Dim query As String = "SELECT STRUNITDESC, NUMUNITCODE FROM LOOKUPEPDUNITS
                WHERE NUMPROGRAMCODE = 5 and Active = 1
                UNION SELECT ' ', 0 ORDER BY STRUNITDESC"

            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace
