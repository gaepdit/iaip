Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace DAL.Ismp
    Module StackTestData

        ''' <summary>
        ''' Determines if a stack test reference number exists in the database.
        ''' </summary>
        ''' <param name="referenceNumber">The stack test reference number to check</param>
        ''' <returns>True if the reference number exists; otherwise, false.</returns>
        Public Function StackTestExists(referenceNumber As String) As Boolean
            If String.IsNullOrEmpty(referenceNumber) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM ISMPREPORTINFORMATION " &
                " WHERE STRREFERENCENUMBER = @ReferenceNumber "
            Dim parameter As New SqlParameter("@ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Determines if a stack test notification exists in the database.
        ''' </summary>
        ''' <param name="notificationNumber">The stack test notification number to check</param>
        ''' <returns>True if the notification exists; otherwise, false.</returns>
        Public Function TestNotificationExists(notificationNumber As String) As Boolean
            If notificationNumber = "" OrElse Not Integer.TryParse(notificationNumber, Nothing) Then Return False

            Dim query As String = "SELECT CONVERT( bit, COUNT(*)) " &
                " FROM ISMPTESTNOTIFICATION " &
                " WHERE STRTESTLOGNUMBER = @NotificationNumber "
            Dim parameter As New SqlParameter("@NotificationNumber", notificationNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        ''' <summary>
        ''' Determines if a stack test has been closed out.
        ''' </summary>
        ''' <param name="referenceNumber">The stack test reference number to check</param>
        ''' <returns>True if the stack test has been closed out; otherwise, false.</returns>
        Public Function StackTestIsClosedOut(referenceNumber As String) As Boolean
            If String.IsNullOrEmpty(referenceNumber) Then Return False

            Dim query As String = "SELECT STRCLOSED " &
                " FROM ISMPREPORTINFORMATION " &
                " WHERE STRREFERENCENUMBER = @ReferenceNumber "
            Dim parameter As New SqlParameter("@ReferenceNumber", referenceNumber)

            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function GetFacilityIdForStackTest(referenceNumber As String) As String
            If String.IsNullOrEmpty(referenceNumber) Then Return Nothing

            Dim query As String = "select STRAIRSNUMBER from dbo.ISMPMASTER where STRREFERENCENUMBER = @ref"
            Dim parameter As New SqlParameter("@ref", referenceNumber)

            Return DB.GetString(query, parameter)
        End Function

        Private Function GetStackTestDbTable(referenceNumber As String) As String
            If String.IsNullOrEmpty(referenceNumber) Then Return Nothing

            Dim query As String =
            "SELECT dt.STRTABLENAME " &
            "FROM ISMPDocumentType dt " &
            "INNER JOIN ISMPReportInformation ri " &
            "ON ri.STRDOCUMENTTYPE = dt.STRKEY " &
            "WHERE ri.STRREFERENCENUMBER = @ReferenceNumber"

            Dim parameter As New SqlParameter("@ReferenceNumber", referenceNumber)

            Return DB.GetString(query, parameter)
        End Function

        Public Function GetStackTestDocumentType(referenceNumber As String) As String
            If String.IsNullOrEmpty(referenceNumber) Then Return Nothing

            Dim query As String = "SELECT t.STRDOCUMENTTYPE
                FROM ISMPDOCUMENTTYPE AS t
                INNER JOIN ISMPREPORTINFORMATION AS i ON i.STRDOCUMENTTYPE = t.STRKEY
                WHERE i.STRREFERENCENUMBER = @ref"
            Dim parameter As New SqlParameter("@ref", referenceNumber)

            Return DB.GetString(query, parameter)
        End Function

        ''' <summary>
        ''' Clears all data from a stack test and resets the document type (but does not delete the test).
        ''' </summary>
        ''' <param name="referenceNumber">The reference number of the stack test to clear.</param>
        ''' <returns>True if the action was successful; otherwise, false.</returns>
        Public Function ClearStackTestData(referenceNumber As String) As Boolean
            If String.IsNullOrEmpty(referenceNumber) Then Return False

            Dim tableName As String = GetStackTestDbTable(referenceNumber)

            If tableName Is Nothing Then
                Return Nothing
            End If

            tableName = Regex.Replace(tableName, NonAlphabeticPattern, "")

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of SqlParameter())
            Dim parameter As New SqlParameter("@ReferenceNumber", referenceNumber)

            If tableName <> "UNASSIGNED" Then
                queryList.Add("DELETE FROM " & tableName & " WHERE strReferenceNumber = @ReferenceNumber")
                parametersList.Add({parameter})
            End If

            queryList.Add(" UPDATE ISMPReportInformation " &
                          " SET strDocumentType      = '001' " &
                          " WHERE strReferenceNumber = @ReferenceNumber ")
            parametersList.Add({parameter})

            Return DB.RunCommand(queryList, parametersList)
        End Function

    End Module
End Namespace
