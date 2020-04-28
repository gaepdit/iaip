Imports System.Data.SqlClient

Namespace DAL
    Module IaipAppData

        Public Function AppIsEnabled() As Boolean
            Dim query As String = " SELECT FENABLED " &
                " FROM APBMASTERAPP " &
                " WHERE STRAPPLICATIONNAME = @STRAPPLICATIONNAME "
            Dim parameter As SqlParameter = New SqlParameter("@STRAPPLICATIONNAME", APP_NAME)

            Try
                Return DB.GetBoolean(query, parameter)
            Catch ex As SqlException
                Return False
            Catch ex As FormatException
                Return False
            End Try
        End Function

        Public Function GetIaipAccountRoles() As DataTable
            Dim spName As String = "IAIP_USER.GetIaipAccountRoles"
            Dim dt As DataTable = DB.SPGetDataTable(spName)
            dt.PrimaryKey = {dt.Columns("RoleCode")}
            Return dt
        End Function

    End Module
End Namespace
