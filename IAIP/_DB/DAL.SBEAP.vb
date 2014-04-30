﻿Imports Oracle.DataAccess.Client

Namespace DAL.SBEAP

    Module Sbeap

        Public Function ClientExists(ByVal clientID As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".SBEAPCLIENTS " & _
                " WHERE RowNum = 1 " & _
                " AND CLIENTID = :pId "

            Dim parameter As New OracleParameter("pId", clientID)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function CaseExists(ByVal caseNumber As String) As Boolean
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM " & DBNameSpace & ".SBEAPCASELOG " & _
                " WHERE RowNum = 1 " & _
                " AND NUMCASEID = :pId "

            Dim parameter As New OracleParameter("pId", caseNumber)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

    End Module

End Namespace
