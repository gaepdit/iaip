Imports System.Data.SqlClient
Imports Iaip.Dmu
Imports System.Collections.Generic

Namespace DAL.Dmu

    Module EdtErrorData

#Region " Read "

        ''' <summary>
        ''' Returns information about an EDT error message (description, category, etc.)
        ''' </summary>
        ''' <param name="errorCode">The EDT Error Code to retrieve information about</param>
        ''' <returns>An EdtErrorMessage object</returns>
        Public Function GetErrorMessageDetail(ByVal errorCode As String) As EdtErrorMessage
            Throw New NotImplementedException()

            ' TODO: SQL Server migration

            'Dim em As New EdtErrorMessage
            'Dim spName As String = "AIRBRANCH.ICIS_EDT.GetErrorMessageDetail"
            'Dim parameters As SqlParameter() = {
            '    New SqlParameter("ERRORCODE", SqlDbType.VarChar, errorCode, ParameterDirection.Input),
            '    New SqlParameter("ERRORMESSAGE", SqlDbType.VarChar, 4000, Nothing, ParameterDirection.Output),
            '    New SqlParameter("CATEGORY", SqlDbType.VarChar, 100, Nothing, ParameterDirection.Output),
            '    New SqlParameter("BUSINESSRULECODE", SqlDbType.VarChar, 10, Nothing, ParameterDirection.Output),
            '    New SqlParameter("BUSINESSRULE", SqlDbType.VarChar, 4000, Nothing, ParameterDirection.Output),
            '    New SqlParameter("DefaultUserID", SqlDbType.Int, 22, Nothing, ParameterDirection.Output),
            '    New SqlParameter("DefaultUserName", SqlDbType.VarChar, 202, Nothing, ParameterDirection.Output)
            '}

            'Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            'If result Then
            '    With em
            '        .ErrorCode = errorCode
            '        .ErrorMessage = DB.GetNullable(Of String)(parameters(1).Value.ToString)
            '        .ErrorCategory = DB.GetNullable(Of String)(parameters(2).Value.ToString)
            '        .BusinessRuleCode = DB.GetNullable(Of String)(parameters(3).Value.ToString)
            '        .BusinessRuleMessage = DB.GetNullable(Of String)(parameters(4).Value.ToString)
            '        .DefaultUserID = DB.GetNullable(Of Integer)(parameters(5).Value.ToString)
            '        .DefaultUserName = DB.GetNullable(Of String)(parameters(6).Value.ToString)
            '    End With
            'End If

            'Return em
        End Function

        ''' <summary>
        ''' Returns summary information on all EDT errors reported organized by EDT Error Code. Includes counts of 
        ''' all errors reported, open errors, and errors assigned to specified user.
        ''' </summary>
        ''' <param name="userID">The user ID for which to return error counts</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrorCounts(ByVal userID As Integer) As DataTable
            Dim spName As String = "AIRBRANCH.ICIS_EDT.GetErrorCounts"
            Dim parameter As SqlParameter = New SqlParameter("userID", userID)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns all EDT errors reported for a specific EDT Error Code
        ''' </summary>
        ''' <param name="errorCode">The Error Code for which to return errors</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrors(ByVal errorCode As String) As DataTable
            Dim spName As String = "AIRBRANCH.ICIS_EDT.GetErrors"
            Dim parameter As SqlParameter = New SqlParameter("errorCode", errorCode)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns details on a specific error reported by EDT
        ''' </summary>
        ''' <param name="errorID">The Error ID to retrieve</param>
        ''' <returns>An EdtError Object</returns>
        Public Function GetErrorDetail(ByVal errorID As String) As EdtError
            Dim er As EdtError = Nothing

            Dim spName As String = "AIRBRANCH.ICIS_EDT.GetErrorDetail"
            Dim parameter As SqlParameter = New SqlParameter("errorID", errorID)

            Dim dt As DataTable = DB.SPGetDataTable(spName, parameter)

            If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
                er = MakeEdtErrorDetailFrom(dt.Rows(0))
                er.ErrorID = errorID
            End If

            Return er
        End Function

        Private Function MakeEdtErrorDetailFrom(ByVal row As DataRow) As EdtError
            If row Is Nothing Then Return Nothing

            Dim es As New EdtSubmission
            With es
                .EdtForeignKeyID = DB.GetNullable(Of String)(row("FOREIGNKEY"))
                .EdtID = DB.GetNullable(Of String)(row("EdtID"))
                .EdtOperation = DB.GetNullable(Of String)(row("OPERATION"))
                .EdtStatus = DB.GetNullable(Of String)(row("STATUS"))
                .EdtSubmitDate = DB.GetNullable(Of Date)(row("SUBMITDATE"))
                .EdtTableName = DB.GetNullable(Of String)(row("TABLENAME"))
                .IaipID = DB.GetNullable(Of String)(row("IAIPID"))
                [Enum].TryParse(DB.GetNullable(Of String)(row("IAIPIDCATEGORY")), .IaipIDCategory)
                .IaipForeignID = DB.GetNullable(Of String)(row("IAIPFOREIGNID"))
                [Enum].TryParse(DB.GetNullable(Of String)(row("IAIPFOREIGNIDCATEGORY")), .IaipForeignIDCategory)
            End With

            Dim em As New EdtErrorMessage
            With em
                .BusinessRuleCode = DB.GetNullable(Of String)(row("BUSINESSRULECODE"))
                .BusinessRuleMessage = DB.GetNullable(Of String)(row("BUSINESSRULE"))
                .ErrorCategory = DB.GetNullable(Of String)(row("CATEGORY"))
                .ErrorCode = DB.GetNullable(Of String)(row("ERRORCODE"))
                .ErrorMessage = DB.GetNullable(Of String)(row("ERRORMESSAGE"))
            End With

            Dim er As New EdtError
            With er
                .AssignedToUserID = DB.GetNullable(Of Integer)(row("ASSIGNEDTOUSER"))
                .EdtErrorMessageDetail = DB.GetNullable(Of String)(row("STATUSDETAIL"))
                .EdtSubmission = es
                .ErrorMessage = em
                .Resolved = Convert.ToBoolean(DB.GetNullable(Of String)(row("RESOLVED")))
                .ResolvedByUserID = DB.GetNullable(Of Integer)(row("RESOLVEDBYUSERID"))
                .ResolvedByUserName = DB.GetNullable(Of String)(row("ResolvedByUserName"))
                .ResolvedDate = DB.GetNullable(Of Date)(row("RESOLVEDDATE"))
            End With

            Return er
        End Function

#End Region

#Region " Write "

        ''' <summary>
        ''' Specify the default user for an EDT error message code
        ''' </summary>
        ''' <param name="errorCode">The EDT error code for which to specify the default user</param>
        ''' <param name="defaultUserID">The User ID to set as the default for the error code</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetDefaultUser(ByVal errorCode As String, ByVal defaultUserID As Integer) As Boolean
            Dim spName As String = "AIRBRANCH.ICIS_EDT.SetDefaultUser"

            Dim parameters As SqlParameter() = {
                New SqlParameter("ErrorCode", errorCode),
                New SqlParameter("UserID", defaultUserID)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Resolve or re-open an EDT error record
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorID">The EDT error ID</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(ByVal resolved As Boolean, ByVal errorID As Integer) As Boolean
            Dim errorIDs As Integer() = {errorID}
            Return SetResolvedStatus(resolved, errorIDs)
        End Function

        ''' <summary>
        ''' Resolve or re-open an array of EDT error records
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(ByVal resolved As Boolean, ByVal errorIDs As Integer()) As Boolean
            Throw New NotImplementedException()

            ' TODO: SQL Server migration

            'Dim spName As String = "AIRBRANCH.ICIS_EDT.SetResolvedStatus"

            'Dim p1 As SqlParameter = New SqlParameter("Resolved", resolved.ToString)

            'Dim p2 As SqlParameter = New SqlParameter("UserID", CurrentUser.UserID)

            'Dim p3 As New SqlParameter
            'p3.ParameterName = "ErrorIdArray"
            'p3.SqlDbType = SqlDbType.Int
            'p3.Direction = ParameterDirection.Input
            'p3.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            'p3.Value = errorIDs
            'p3.Size = errorIDs.Length

            'Dim parameters As SqlParameter() = {p1, p2, p3}

            'Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Set the user responsible for a specific EDT error report
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the error</param>
        ''' <param name="errorID">The EDT error ID to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(ByVal userId As Integer, ByVal errorID As Integer) As Boolean
            Dim errorIDs As Integer() = {errorID}
            Return AssignErrorToUser(userId, errorIDs)
        End Function

        ''' <summary>
        ''' Set the user responsible for an array of EDT error reports
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the errors</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(ByVal userId As Integer, ByVal errorIDs As Integer()) As Boolean
            Throw New NotImplementedException()

            ' TODO: SQL Server migration

            'Dim spName As String = "AIRBRANCH.ICIS_EDT.AssignErrors"

            'Dim p1 As SqlParameter = New SqlParameter("UserID", userId)

            'Dim p2 As New SqlParameter
            'p2.ParameterName = "ErrorIdArray"
            'p2.SqlDbType = SqlDbType.Int
            'p2.Direction = ParameterDirection.Input
            'p2.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            'p2.Value = errorIDs
            'p2.Size = errorIDs.Length

            'Dim parameters As SqlParameter() = {p1, p2}

            'Return DB.SPRunCommand(spName, parameters)
        End Function

#End Region

    End Module

End Namespace
