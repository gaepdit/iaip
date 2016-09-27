Imports System.Data.SqlClient

Namespace DAL.Sbeap

    Module SbeapData

        Public Function ClientExists(clientID As String) As Boolean
            If clientID = "" OrElse Not Integer.TryParse(clientID, Nothing) Then Return False

            Dim query As String = "SELECT 1 " &
                " FROM SBEAPCLIENTS " &
                " WHERE CLIENTID = @id "

            Dim parameter As New SqlParameter("@id", clientID)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function CaseExists(caseNumber As String) As Boolean
            If caseNumber = "" OrElse Not Integer.TryParse(caseNumber, Nothing) Then Return False

            Dim query As String = "SELECT 1 " &
                " FROM SBEAPCASELOG " &
                " WHERE NUMCASEID = @id "

            Dim parameter As New SqlParameter("@id", caseNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

    End Module

End Namespace
