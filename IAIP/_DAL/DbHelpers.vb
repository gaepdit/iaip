Imports Microsoft.Data.SqlClient

Module DbHelpers

    Public Function SqlParameterWithDbType(parameterName As String, dbType As SqlDbType, value As Object) As SqlParameter
        Return New SqlParameter(parameterName, dbType) With {.Value = value}
    End Function

    Public Function SqlParameterAsNull(parameterName As String, dbType As SqlDbType) As SqlParameter
        Return SqlParameterWithDbType(parameterName, dbType, Nothing)
    End Function

End Module
