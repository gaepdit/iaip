Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Namespace DAL
    Module QueryGenerator

        Public Function LogQuery(ByVal kvp As Generic.KeyValuePair(Of String, Integer)) As Boolean

            Dim query As String = _
                " INSERT INTO AIRBRANCH.IAIP_LOG_QUERYGENERATOR " & _
                " (USERSUBMITTING, DATESUBMITTED, ROWSRETURNED, QUERYSUBMITTED) " & _
                " VALUES (:UserSubmitting, :DateSubmitted, :RowsReturned, :QuerySubmitted) "

            Dim parameters As OracleParameter() = { _
                New OracleParameter("UserSubmitting", UserGCode), _
                New OracleParameter("DateSubmitted", Date.Now), _
                New OracleParameter("RowsReturned", kvp.Value), _
                New OracleParameter("QuerySubmitted", kvp.Key) _
            }

            Return DB.RunCommand(query, parameters, failSilently:=True)
        End Function

    End Module
End Namespace
