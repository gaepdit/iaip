Imports System.Data.SqlClient
Imports System.Threading.Tasks

Namespace DAL
    Module IaipAppData

        Public Function AppIsEnabled() As Boolean
            Dim spName As String = "dbo.IsIaipEnabled"
            Dim param As New SqlParameter("@currentVersion", GetCurrentVersionAsMajorMinorBuild.ToString)

            Try
                Return DB.SPGetBoolean(spName, param)
            Catch
                Return False
            End Try
        End Function

        Public Async Function IsIaipEnabledAsync() As Task(Of Boolean)
            Return Await Task.Run(
            Function()
                Return AppIsEnabled()
            End Function
            ).ConfigureAwait(False)
        End Function

        Public Function GetIaipAccountRoles() As DataTable
            Dim spName As String = "IAIP_USER.GetIaipAccountRoles"
            Dim dt As DataTable = DB.SPGetDataTable(spName)
            dt.PrimaryKey = {dt.Columns("RoleCode")}
            Return dt
        End Function

    End Module
End Namespace
