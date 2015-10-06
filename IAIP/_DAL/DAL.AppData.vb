Imports Oracle.ManagedDataAccess.Client

Namespace DAL
    Module AppData

        Public Function AppIsEnabled() As Boolean
            Dim query As String = " SELECT FENABLED " & _
                " FROM AIRBRANCH.APBMASTERAPP " & _
                " WHERE STRAPPLICATIONNAME = :pAppName "
            Dim parameter As OracleParameter = New OracleParameter("pAppName", APP_NAME)

            Try
                Return DB.GetBoolean(query, parameter, True)
            Catch ex As OracleException
                Return False
            Catch ex As FormatException
                Return False
            End Try
        End Function

    End Module
End Namespace
