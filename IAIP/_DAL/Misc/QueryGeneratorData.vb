Imports System.Data.SqlClient

Namespace DAL
    Module QueryGeneratorData

        Public Function LogQuery(generatedQuery As String, count As Integer) As Boolean
            Dim query As String =
                " INSERT INTO IAIP_LOG_QUERYGENERATOR " &
                " (USERSUBMITTING, ROWSRETURNED, QUERYSUBMITTED) " &
                " VALUES (@UserSubmitting, @RowsReturned, @QuerySubmitted) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("@UserSubmitting", CurrentUser.UserID),
                New SqlParameter("@RowsReturned", count),
                New SqlParameter("@QuerySubmitted", generatedQuery)
            }

            Try
                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
