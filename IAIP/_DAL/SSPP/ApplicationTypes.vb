Imports Microsoft.Data.SqlClient

Namespace DAL.Sspp

    Module ApplicationTypes

        Public Function SaveNewApplicationType(description As String) As Boolean
            If String.IsNullOrWhiteSpace(description) Then
                Return False
            End If

            ClearSharedData(SharedTable.ApplicationTypes)

            Dim query As String = "INSERT INTO LOOKUPAPPLICATIONTYPES 
                (STRAPPLICATIONTYPECODE, STRAPPLICATIONTYPEDESC, STRAPPLICATIONTYPEUSED)
                VALUES 
                ( (SELECT MAX(CONVERT(int, STRAPPLICATIONTYPECODE)) FROM LOOKUPAPPLICATIONTYPES), @apptype, NULL)"

            Dim p As New SqlParameter("@apptype", description)

            Return DB.RunCommand(query, p)
        End Function

        Public Function UpdateApplicationTypeStatus(code As Integer, status As ActiveOrInactive) As Boolean
            ClearSharedData(SharedTable.ApplicationTypes)

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
