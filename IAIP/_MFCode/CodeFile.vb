Imports System
Imports System.Security
Imports Oracle.DataAccess.Client

Module CodeFile

    Function Update_FS_Admin_Status(ByVal FeeYear As String, ByVal AIRSNumber As String) As Boolean
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_Status", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function ValidateNAICS(ByVal NAICSCode As String) As Boolean
        Try
            Dim SQL As String = "Select strNAICSCode " & _
            "from AIRBranch.EILookUpNAICS " & _
            "where strNAICSCode = '" & NAICSCode & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                Return True
            Else
                Return False
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function ValidateSIC(ByVal SICCode As String) As Boolean
        Try
            Dim SQL As String = "Select strSICCode " & _
            "from AIRBranch.LookUpSICCodes " & _
            "where strSICCode = '" & SICCode & "' " & _
            "and length(strSICCode) = 4 "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                Return True
            Else
                Return False
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

End Module
