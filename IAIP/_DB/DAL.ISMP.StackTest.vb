﻿Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.SSCP

Namespace DAL.ISMP

    Module StackTest

        Public Function StackTestExists(ByVal id As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRREFERENCENUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function TestNotificationExists(ByVal id As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPTESTNOTIFICATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRTESTLOGNUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

        Public Function StackTestIsClosedOut(ByVal id As String) As Boolean
            If id = "" OrElse Not Integer.TryParse(id, Nothing) Then Return False

            Dim query As String = "SELECT STRCLOSED " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE STRREFERENCENUMBER = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Dim result As String = DB.GetSingleValue(Of String)(query, parameter)
            Return Convert.ToBoolean(result)
        End Function

    End Module

End Namespace
