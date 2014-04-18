Imports Oracle.DataAccess.Client

Namespace DAL
    Module App

        Public Function AppIsEnabled() As Boolean
            Dim query As String = " SELECT FENABLED " & _
                " FROM " & DBNameSpace & ".APBMASTERAPP " & _
                " WHERE STRAPPLICATIONNAME = :pAppName "
            Dim parameter As OracleParameter = New OracleParameter("pAppName", APP_NAME)

            Try
                Return DB.GetBoolean(query, parameter)
            Catch ex As OracleException
                Return False
            Catch ex As FormatException
                Return False
            End Try
        End Function

    End Module
End Namespace
