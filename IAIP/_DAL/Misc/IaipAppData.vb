Imports System.Threading.Tasks

Namespace DAL
    Module IaipAppData

        Private Function AppIsEnabled() As Boolean
            Dim spName As String = "dbo.IsIaipCurrent"

            Try
                Dim dr As DataRow = DB.SPGetDataRow(spName)
                If dr Is Nothing Then Return False
                If Not CBool(dr("Enabled")) Then Return False
                Dim minVer As New Version(dr("MinimumVersion"))
                Return GetCurrentVersionAsMajorMinorBuild().CompareTo(minVer) >= 0
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
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
