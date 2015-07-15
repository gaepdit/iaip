Imports Oracle.ManagedDataAccess.Client
Imports Iaip.Apb.SSCP
Imports System.Collections.Generic

Namespace DAL.ISMP

    Module StackTest

        ''' <summary>
        ''' Determines if a stack test reference number exists in the database.
        ''' </summary>
        ''' <param name="referenceNumber">The stack test reference number to check</param>
        ''' <returns>True if the reference number exists; otherwise, false.</returns>
        Public Function StackTestExists(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRREFERENCENUMBER = :ReferenceNumber "
            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Determines if a stack test notification exists in the database.
        ''' </summary>
        ''' <param name="notificationNumber">The stack test notification number to check</param>
        ''' <returns>True if the notification exists; otherwise, false.</returns>
        Public Function TestNotificationExists(ByVal notificationNumber As String) As Boolean
            If notificationNumber = "" OrElse Not Integer.TryParse(notificationNumber, Nothing) Then Return False

            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.ISMPTESTNOTIFICATION " & _
                " WHERE RowNum = 1 " & _
                " AND STRTESTLOGNUMBER = :NotificationNumber "
            Dim parameter As New OracleParameter("NotificationNumber", notificationNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Determines if a stack test has been closed out.
        ''' </summary>
        ''' <param name="referenceNumber">The stack test reference number to check</param>
        ''' <returns>True if the stack test has been closed out; otherwise, false.</returns>
        Public Function StackTestIsClosedOut(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return False

            Dim query As String = "SELECT STRCLOSED " & _
                " FROM AIRBRANCH.ISMPREPORTINFORMATION " & _
                " WHERE STRREFERENCENUMBER = :ReferenceNumber "
            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Private Function GetStackTestDbTable(ByVal referenceNumber As String) As String
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return Nothing

            Dim query As String = _
            "SELECT dt.STRTABLENAME " & _
            "FROM AIRBRANCH.ISMPDocumentType dt " & _
            "INNER JOIN AIRBRANCH.ISMPReportInformation ri " & _
            "ON ri.STRDOCUMENTTYPE = dt.STRKEY " & _
            "WHERE ri.STRREFERENCENUMBER = :ReferenceNumber"

            Dim parameter As New OracleParameter("ReferenceNumber", referenceNumber)

            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        ''' <summary>
        ''' Clears all data from a stack test and resets the document type (but does not delete the test).
        ''' </summary>
        ''' <param name="referenceNumber">The reference number of the stack test to clear.</param>
        ''' <returns>True if the action was successful; otherwise, false.</returns>
        Public Function ClearStackTestData(ByVal referenceNumber As String) As Boolean
            If referenceNumber = "" OrElse Not Integer.TryParse(referenceNumber, Nothing) Then Return Nothing

            Dim tableName As String = GetStackTestDbTable(referenceNumber)

            If tableName Is Nothing Then
                Return Nothing
            End If

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())
            Dim parameter As OracleParameter() = New OracleParameter() { _
                New OracleParameter("ReferenceNumber", referenceNumber) _
            }

            If tableName <> "UNASSIGNED" Then
                queryList.Add("DELETE FROM AIRBRANCH." & tableName & " WHERE strReferenceNumber = :ReferenceNumber")
                parametersList.Add(parameter)
            End If

            queryList.Add(" UPDATE AIRBRANCH.ISMPReportInformation " & _
                          " SET strDocumentType      = '001' " & _
                          " WHERE strReferenceNumber = :ReferenceNumber ")
            parametersList.Add(parameter)

            Return DB.RunCommand(queryList, parametersList)
        End Function

    End Module

End Namespace
