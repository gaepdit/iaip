Imports System
Imports System.Security
Imports Oracle.DataAccess.Client

Module CodeFile

    'Function Update_FS_Admin_Status(ByVal FeeYear As String, ByVal AIRSNumber As String) As Boolean
    '    Try

    '        If CurrentConnection.State = ConnectionState.Closed Then
    '            CurrentConnection.Open()
    '        End If
    '        cmd = New OracleCommand("AIRBranch.PD_FEE_Status", CurrentConnection)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
    '        cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

    '        cmd.ExecuteNonQuery()

    '    Catch ex As Exception
    '        ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
    '    End Try
    'End Function


End Module
