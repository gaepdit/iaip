Imports System.Data.SqlClient
Imports Iaip.Dmu
Imports EpdIt

Namespace DAL.Dmu

    Module EdtErrorData

#Region " Read "

        ''' <summary>
        ''' Returns information about an EDT error message (description, category, etc.)
        ''' </summary>
        ''' <param name="errorCode">The EDT Error Code to retrieve information about</param>
        ''' <returns>An EdtErrorMessage object</returns>
        Public Function GetErrorMessageDetail(errorCode As String) As EdtErrorMessage
            Dim em As New EdtErrorMessage
            Dim spName As String = "icis_edt.GetErrorMessageDetail"

            Dim parameter As New SqlParameter("@ErrorCode", errorCode)

            Dim dr As DataRow = DB.SPGetDataRow(spName, parameter)

            If dr IsNot Nothing Then
                With em
                    .ErrorCode = errorCode
                    .ErrorMessage = DBUtilities.GetNullable(Of String)(dr("ErrorMessage"))
                    .ErrorCategory = DBUtilities.GetNullable(Of String)(dr("ErrorCategory"))
                    .BusinessRuleCode = DBUtilities.GetNullable(Of String)(dr("BusinessRuleCode"))
                    .BusinessRuleMessage = DBUtilities.GetNullable(Of String)(dr("BusinessRuleMessage"))
                    .DefaultUserID = DBUtilities.GetNullable(Of Integer)(dr("DefaultUserID"))
                    .DefaultUserName = DBUtilities.GetNullable(Of String)(dr("DefaultUserName"))
                End With
            End If

            Return em
        End Function

        ''' <summary>
        ''' Returns summary information on all EDT errors reported organized by EDT Error Code. Includes counts of 
        ''' all errors reported, open errors, and errors assigned to specified user.
        ''' </summary>
        ''' <param name="userID">The user ID for which to return error counts</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrorCounts(userID As Integer) As DataTable
            Dim spName As String = "icis_edt.GetErrorCounts"
            Dim parameter As SqlParameter = New SqlParameter("@userID", userID)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns all EDT errors reported for a specific EDT Error Code
        ''' </summary>
        ''' <param name="errorCode">The Error Code for which to return errors</param>
        ''' <returns>A DataTable</returns>
        Public Function GetErrors(errorCode As String) As DataTable
            Dim spName As String = "icis_edt.GetErrors"
            Dim parameter As SqlParameter = New SqlParameter("@ErrorCode", errorCode)
            Return DB.SPGetDataTable(spName, parameter)
        End Function

        ''' <summary>
        ''' Returns details on a specific error reported by EDT
        ''' </summary>
        ''' <param name="errorID">The Error ID to retrieve</param>
        ''' <returns>An EdtError Object</returns>
        Public Function GetErrorDetail(errorID As String) As EdtError
            Dim er As EdtError = Nothing

            Dim spName As String = "icis_edt.GetErrorDetail"
            Dim parameter As SqlParameter = New SqlParameter("@errorId", errorID)

            Dim dt As DataTable = DB.SPGetDataTable(spName, parameter)

            If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
                er = MakeEdtErrorDetailFrom(dt.Rows(0))
                er.ErrorID = errorID
            End If

            Return er
        End Function

        Private Function MakeEdtErrorDetailFrom(row As DataRow) As EdtError
            If row Is Nothing Then Return Nothing

            Dim es As New EdtSubmission
            With es
                .EdtForeignKeyID = DBUtilities.GetNullable(Of String)(row("FOREIGNKEY"))
                .EdtID = DBUtilities.GetNullable(Of String)(row("EdtID"))
                .EdtOperation = DBUtilities.GetNullable(Of String)(row("OPERATION"))
                .EdtStatus = DBUtilities.GetNullable(Of String)(row("STATUS"))
                .EdtSubmitDate = DBUtilities.GetNullable(Of Date)(row("SUBMITDATE"))
                .EdtTableName = DBUtilities.GetNullable(Of String)(row("TABLENAME"))
                .IaipID = DBUtilities.GetNullable(Of String)(row("IAIPID"))
                [Enum].TryParse(DBUtilities.GetNullable(Of String)(row("IAIPIDCATEGORY")), .IaipIDCategory)
                .IaipForeignID = DBUtilities.GetNullable(Of String)(row("IAIPFOREIGNID"))
                [Enum].TryParse(DBUtilities.GetNullable(Of String)(row("IAIPFOREIGNIDCATEGORY")), .IaipForeignIDCategory)
            End With

            Dim em As New EdtErrorMessage
            With em
                .BusinessRuleCode = DBUtilities.GetNullable(Of String)(row("BUSINESSRULECODE"))
                .BusinessRuleMessage = DBUtilities.GetNullable(Of String)(row("BUSINESSRULE"))
                .ErrorCategory = DBUtilities.GetNullable(Of String)(row("CATEGORY"))
                .ErrorCode = DBUtilities.GetNullable(Of String)(row("ERRORCODE"))
                .ErrorMessage = DBUtilities.GetNullable(Of String)(row("ERRORMESSAGE"))
            End With

            Dim er As New EdtError
            With er
                .AssignedToUserID = DBUtilities.GetNullable(Of Integer)(row("ASSIGNEDTOUSER"))
                .EdtErrorMessageDetail = DBUtilities.GetNullable(Of String)(row("STATUSDETAIL"))
                .EdtSubmission = es
                .ErrorMessage = em
                .Resolved = Convert.ToBoolean(DBUtilities.GetNullable(Of String)(row("RESOLVED")))
                .ResolvedByUserID = DBUtilities.GetNullable(Of Integer)(row("RESOLVEDBYUSERID"))
                .ResolvedByUserName = DBUtilities.GetNullable(Of String)(row("ResolvedByUserName"))
                .ResolvedDate = DBUtilities.GetNullable(Of Date)(row("RESOLVEDDATE"))
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
        Public Function SetDefaultUser(errorCode As String, defaultUserID As Integer) As Boolean
            Dim spName As String = "icis_edt.SetDefaultUser"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@ErrorCode", errorCode),
                New SqlParameter("@UserId", defaultUserID)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Resolve or re-open an EDT error record
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorID">The EDT error ID</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(resolved As Boolean, errorID As Integer) As Boolean
            Dim errorIDs As Integer() = {errorID}
            Return SetResolvedStatus(resolved, errorIDs)
        End Function

        ''' <summary>
        ''' Resolve or re-open an array of EDT error records
        ''' </summary>
        ''' <param name="resolved">True to set as resolved; false to set as open</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function SetResolvedStatus(resolved As Boolean, errorIDs As Integer()) As Boolean
            Dim spName As String = "icis_edt.SetResolvedStatus"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@Resolved", resolved.ToString),
                New SqlParameter("@UserId", CurrentUser.UserID),
                errorIDs.AsTvpSqlParameter("@ErrorIdArray")
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        ''' <summary>
        ''' Set the user responsible for a specific EDT error report
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the error</param>
        ''' <param name="errorID">The EDT error ID to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(userId As Integer, errorID As Integer) As Boolean
            Dim errorIDs As Integer() = {errorID}
            Return AssignErrorToUser(userId, errorIDs)
        End Function

        ''' <summary>
        ''' Set the user responsible for an array of EDT error reports
        ''' </summary>
        ''' <param name="userId">The user ID of the user assigned to the errors</param>
        ''' <param name="errorIDs">An array of EDT error IDs to modify</param>
        ''' <returns>True if the action was successful; otherwise false</returns>
        Public Function AssignErrorToUser(userId As Integer, errorIDs As Integer()) As Boolean
            Dim spName As String = "icis_edt.AssignErrors"

            Dim parameters As SqlParameter() = {
                New SqlParameter("@UserId", userId),
                errorIDs.AsTvpSqlParameter("ErrorIdArray")
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

#End Region

    End Module

End Namespace
