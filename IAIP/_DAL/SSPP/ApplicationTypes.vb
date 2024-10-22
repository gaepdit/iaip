Imports Microsoft.Data.SqlClient

Namespace DAL.Sspp

    Module ApplicationTypes

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
    End Module

End Namespace
