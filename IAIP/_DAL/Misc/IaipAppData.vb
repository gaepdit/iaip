Imports System.Threading.Tasks
Imports Iaip.ApiCalls.IaipCx

Namespace DAL
    Module IaipAppData

        Private Async Function AppIsEnabledAsync() As Task(Of Boolean)
            Dim result As IaipStatusResult = Await CheckIaipStatusApiAsync().ConfigureAwait(False)
            If Not result.Enabled Then Return False
            Dim minVer As New Version(result.MinimumVersion)
            Return GetCurrentVersionAsMajorMinorBuild().CompareTo(minVer) >= 0
        End Function

        ' The indirection in this function prevents some kind of cross-thread exception.
        Public Async Function IsIaipEnabledAsync() As Task(Of Boolean)
            Return Await Task.Run(
            Function()
                Return AppIsEnabledAsync()
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
