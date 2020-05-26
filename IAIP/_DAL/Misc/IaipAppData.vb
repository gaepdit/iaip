Imports System.Data.SqlClient

Namespace DAL
    Module IaipAppData

        Public Function AppIsEnabled() As Boolean
            Dim spName As String = "dbo.IsIaipEnabled"
            Dim param As SqlParameter = New SqlParameter("@currentVersion", GetCurrentVersionAsMajorMinorBuild.ToString)

            Try
                Return DB.SPGetBoolean(spName, param)
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
