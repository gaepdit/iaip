Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Namespace DAL
    Module QueryGeneratorData

        Public Function LogQuery(ByVal kvp As Generic.KeyValuePair(Of String, Integer)) As Boolean

            Dim query As String =
                " INSERT INTO AIRBRANCH.IAIP_LOG_QUERYGENERATOR " &
                " (USERSUBMITTING, DATESUBMITTED, ROWSRETURNED, QUERYSUBMITTED) " &
                " VALUES (:UserSubmitting, :DateSubmitted, :RowsReturned, :QuerySubmitted) "

            Dim parameters As OracleParameter() = {
                New OracleParameter("UserSubmitting", CurrentUser.UserID),
                New OracleParameter("DateSubmitted", Date.Now),
                New OracleParameter("RowsReturned", kvp.Value),
                New OracleParameter("QuerySubmitted", kvp.Key)
            }

            Return DB.RunCommand(query, parameters, failSilently:=True)
        End Function

    End Module
End Namespace
