﻿Imports Oracle.ManagedDataAccess.Client

Namespace DAL.Sbeap

    Module SbeapData

        Public Function ClientExists(ByVal clientID As String) As Boolean
            If clientID = "" OrElse Not Integer.TryParse(clientID, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SBEAPCLIENTS " & _
                " WHERE RowNum = 1 " & _
                " AND CLIENTID = :pId "

            Dim parameter As New OracleParameter("pId", clientID)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function CaseExists(ByVal caseNumber As String) As Boolean
            If caseNumber = "" OrElse Not Integer.TryParse(caseNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.SBEAPCASELOG " & _
                " WHERE RowNum = 1 " & _
                " AND NUMCASEID = :pId "

            Dim parameter As New OracleParameter("pId", caseNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

    End Module

End Namespace