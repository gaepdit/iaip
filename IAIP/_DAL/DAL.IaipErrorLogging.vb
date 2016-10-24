Imports System.Data.SqlClient

Namespace DAL
    Module IaipErrorLogging

        Friend Function LogError(errorMessage As String, errorLocation As String) As Boolean
            Dim query As String = "INSERT INTO IAIPERRORLOG " &
                " (STRERRORNUMBER, STRUSER, STRERRORLOCATION, STRERRORMESSAGE, DATERRORDATE) " &
                " values (NEXT VALUE FOR IAIPERRORNUMBER, @UserID, @ErrorLocation, @ErrorMessage, GETDATE()) "

            Dim parameters As SqlParameter() = New SqlParameter() {
                New SqlParameter("@UserID", If(CurrentUser IsNot Nothing, CurrentUser.UserID, 0)),
                New SqlParameter("@ErrorLocation", errorLocation),
                New SqlParameter("@ErrorMessage", errorMessage)
            }

            Try
                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
