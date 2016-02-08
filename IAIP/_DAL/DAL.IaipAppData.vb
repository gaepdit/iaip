﻿Imports Oracle.ManagedDataAccess.Client

Namespace DAL
    Module IaipAppData

        Public Function AppIsEnabled() As Boolean
            Dim query As String = " SELECT FENABLED " &
                " FROM AIRBRANCH.APBMASTERAPP " &
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

        Public Function GetIaipAccountRoles() As DataTable
            Dim spName As String = "AIRBRANCH.IAIP_USER.GetIaipAccountRoles"
            Return DB.SPGetDataTable(spName)
        End Function

    End Module
End Namespace