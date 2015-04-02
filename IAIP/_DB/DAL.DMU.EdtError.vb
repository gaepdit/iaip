Imports Oracle.DataAccess.Client
Imports Iaip.DMU

Namespace DAL.DMU

    Module EdtErrorModule

#Region " Read "

        ''' <summary>
        ''' Returns information about an EDT error message (description, category, etc.)
        ''' </summary>
        ''' <param name="errorCode">The EDT Error Code to retrieve information about</param>
        ''' <returns>An EdtErrorMessage object</returns>
        Public Function GetErrorMessageDetail(ByVal errorCode As String) As EdtErrorMessage
            Dim em As EdtErrorMessage = Nothing
            Dim spName As String = "ICIS_EDT_GetErrorMessageDetail"
            Dim parameter As OracleParameter = New OracleParameter("errorCode", errorCode)

            Dim dt As DataTable = DB.SPGetDataTable(spName, parameter)

            If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
                em = MakeEdtErrorMessageFrom(dt.Rows(0))
            End If

            Return em
        End Function

        Private Function MakeEdtErrorMessageFrom(ByVal row As DataRow) As EdtErrorMessage
            If row Is Nothing Then Return Nothing

            Dim em As New EdtErrorMessage
            With em
                .BusinessRuleCode = DB.GetNullable(Of String)(row("BUSINESSRULECODE"))
                .BusinessRuleMessage = DB.GetNullable(Of String)(row("BUSINESSRULE"))
                .DefaultUserID = DB.GetNullable(Of Integer)(row("DEFAULTUSER"))
                .DefaultUserName = DB.GetNullable(Of String)(row("DefaultUserName"))
                .ErrorCategory = DB.GetNullable(Of String)(row("CATEGORY"))
                .ErrorCode = DB.GetNullable(Of String)(row("ERRORCODE"))
                .ErrorMessage = DB.GetNullable(Of String)(row("ERRORMESSAGE"))
            End With

            Return em
        End Function

        ''' <summary>
        ''' Returns summary information on all EDT errors reported organized by EDT Error Code. Includes counts of 
        ''' all errors reported, open errors, and errors assigned to specified user.
        ''' </summary>
        ''' <param name="userID">The user ID for which to return error counts</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrorCounts(ByVal userID As Integer) As DataTable
            Dim spName As String = "ICIS_EDT_GetErrorCounts"
            Dim parameter As OracleParameter = New OracleParameter("userID", userID)

            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns all EDT errors reported for a specific EDT Error Code
        ''' </summary>
        ''' <param name="errorCode">The Error Code for which to return errors</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrors(ByVal errorCode As String) As DataTable
            Dim spName As String = "ICIS_EDT_GetErrors"
            Dim parameter As OracleParameter = New OracleParameter("errorCode", errorCode)

            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns details on a specific error reported by EDT
        ''' </summary>
        ''' <param name="errorID">The Error ID to retrieve</param>
        ''' <returns>An EdtError Object</returns>
        Public Function GetErrorDetail(ByVal errorID As String) As EdtError
            Dim er As EdtError = Nothing
            Dim spName As String = "ICIS_EDT_GetErrorDetail"
            Dim parameter As OracleParameter = New OracleParameter("errorID", errorID)

            Dim dt As DataTable = DB.SPGetDataTable(spName, parameter)

            If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
                er = MakeEdtErrorDetailFrom(dt.Rows(0))
            End If

            Return er
        End Function

        Private Function MakeEdtErrorDetailFrom(ByVal row As DataRow) As EdtError
            If row Is Nothing Then Return Nothing

            Dim es As New EdtSubmission
            With es
                .EdtForeignKeyID = DB.GetNullable(Of String)(row("FOREIGNKEY"))
                .EdtID = DB.GetNullable(Of Integer)(row("ERRORID"))
                .EdtOperation = DB.GetNullable(Of String)(row("OPERATION"))
                .EdtStatus = DB.GetNullable(Of String)(row("STATUS"))
                .EdtSubmitDate = DB.GetNullable(Of Date)(row("SUBMITDATE"))
                .EdtTableName = DB.GetNullable(Of String)(row("TABLENAME"))
            End With

            Dim em As New EdtErrorMessage
            With em
                .BusinessRuleCode = DB.GetNullable(Of String)(row("BUSINESSRULECODE"))
                .BusinessRuleMessage = DB.GetNullable(Of String)(row("BUSINESSRULE"))
                .DefaultUserID = DB.GetNullable(Of Integer)(row("DEFAULTUSER"))
                .DefaultUserName = DB.GetNullable(Of String)(row("DefaultUserName"))
                .ErrorCategory = DB.GetNullable(Of String)(row("CATEGORY"))
                .ErrorCode = DB.GetNullable(Of String)(row("ERRORCODE"))
                .ErrorMessage = DB.GetNullable(Of String)(row("ERRORMESSAGE"))
            End With

            Dim er As New EdtError
            With er
                .AssignedToUserID = DB.GetNullable(Of String)(row("ASSIGNEDTOUSER"))
                .EdtErrorMessageDetail = DB.GetNullable(Of String)(row("STATUSDETAIL"))
                .EdtSubmission = es
                .ErrorID = DB.GetNullable(Of String)(row("ERRORID"))
                .ErrorMessage = em
                .Resolved = Convert.ToBoolean(DB.GetNullable(Of String)(row("RESOLVED")))
                .ResolvedByUserID = DB.GetNullable(Of Integer)(row("RESOLVEDBYUSER"))
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
            Dim spName As String = "ICIS_EDT_SetDefaultUser"

            Dim parameters As OracleParameter() = { _
                New OracleParameter("errorCode", errorCode), _
                New OracleParameter("defaultUserID", defaultUserID) _
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Resolve or re-open an EDT error record
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorID">The EDT error ID</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(ByVal resolved As Boolean, ByVal errorID As String) As Boolean
            Dim errorIDs As String() = {errorID}
            Return SetResolvedStatus(resolved, errorIDs)
        End Function

        ''' <summary>
        ''' Resolve or re-open an array of EDT error records
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(ByVal resolved As Boolean, ByVal errorIDs As String()) As Boolean
            Dim spName As String = "ICIS_EDT_SetResolvedStatus"

            Dim p1 As OracleParameter = New OracleParameter("resolved", resolved.ToString)

            Dim p2 As New OracleParameter
            p2.OracleDbType = OracleDbType.Varchar2
            p2.Direction = ParameterDirection.Input
            p2.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p2.Value = errorIDs
            p2.Size = errorIDs.Length

            Dim parameters As OracleParameter() = {p1, p2}

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Set the user responsible for a specific EDT error report
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the error</param>
        ''' <param name="errorID">The EDT error ID to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(ByVal userId As Integer, ByVal errorID As String) As Boolean
            Dim errorIDs As String() = {errorID}
            Return AssignErrorToUser(userId, errorIDs)
        End Function

        ''' <summary>
        ''' Set the user responsible for an array of EDT error reports
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the errors</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(ByVal userId As Integer, ByVal errorIDs As String()) As Boolean
            Dim spName As String = "ICIS_EDT_ReassignError"

            Dim p1 As OracleParameter = New OracleParameter("userID", userId)

            Dim p2 As New OracleParameter
            p2.OracleDbType = OracleDbType.Varchar2
            p2.Direction = ParameterDirection.Input
            p2.CollectionType = OracleCollectionType.PLSQLAssociativeArray
            p2.Value = errorIDs
            p2.Size = errorIDs.Length

            Dim parameters As OracleParameter() = {p1, p2}

            Return DB.SPRunCommand(spName, parameters)
        End Function

#End Region

    End Module

End Namespace
