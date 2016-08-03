Imports System.Data.SqlClient

Namespace DAL.Sbeap

    Module SbeapData

        Public Function ClientExists(clientID As String) As Boolean
            If clientID = "" OrElse Not Integer.TryParse(clientID, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SBEAPCLIENTS " &
                " WHERE RowNum = 1 " &
                " AND CLIENTID = @pId "

            Dim parameter As New SqlParameter("@pId", clientID)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function CaseExists(caseNumber As String) As Boolean
            If caseNumber = "" OrElse Not Integer.TryParse(caseNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " &
                " FROM SBEAPCASELOG " &
                " WHERE RowNum = 1 " &
                " AND NUMCASEID = @pId "

            Dim parameter As New SqlParameter("@pId", caseNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

    End Module

End Namespace
