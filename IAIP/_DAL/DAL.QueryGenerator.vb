Imports System.Data.SqlClient
Imports Oracle.ManagedDataAccess.Types

Namespace DAL
    Module QueryGeneratorData

        Public Function LogQuery(ByVal kvp As Generic.KeyValuePair(Of String, Integer)) As Boolean
            Dim query As String =
                " INSERT INTO IAIP_LOG_QUERYGENERATOR " &
                " (USERSUBMITTING, DATESUBMITTED, ROWSRETURNED, QUERYSUBMITTED) " &
                " VALUES (:UserSubmitting, :DateSubmitted, :RowsReturned, :QuerySubmitted) "

            Dim parameters As SqlParameter() = {
                New SqlParameter("UserSubmitting", CurrentUser.UserID),
                New SqlParameter("DateSubmitted", Date.Now),
                New SqlParameter("RowsReturned", kvp.Value),
                New SqlParameter("QuerySubmitted", kvp.Key)
            }

            Try
                Return DB.RunCommand(query, parameters)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Module
End Namespace
