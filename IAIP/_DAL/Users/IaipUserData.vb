Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports EpdIt

Namespace DAL
    Module IaipUserData

        Public Function AuthenticateIaipUser(username As String, password As String) As IaipAuthenticationResult
            If String.IsNullOrEmpty(username) Then Return IaipAuthenticationResult.InvalidUsername
            If String.IsNullOrEmpty(password) Then Return IaipAuthenticationResult.InvalidLogin

            Dim spName As String = "iaip_user.AuthenticateIaipUser"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@userpassword", password)
            }
            Dim result As String = DB.SPGetSingleValue(Of String)(spName, parameters)

            If result IsNot Nothing Then
                Select Case result
                    Case "InvalidUsername"
                        Return IaipAuthenticationResult.InvalidUsername
                    Case "Success"
                        Return IaipAuthenticationResult.Success
                    Case "InactiveUser"
                        Return IaipAuthenticationResult.InactiveUser
                End Select
            End If

            Return IaipAuthenticationResult.InvalidLogin
        End Function

        Public Enum IaipAuthenticationResult
            Success
            InvalidUsername
            InvalidLogin
            InactiveUser
        End Enum

        Public Function GetIaipUserByUserId(userid As String) As IaipUser
            Dim id As Integer

            If String.IsNullOrEmpty(userid) OrElse Not Integer.TryParse(userid, id) Then
                Return Nothing
            End If

            Dim dataTable As DataTable = GetIaipUserByUserIdAsDataTable(id)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        Public Function GetIaipUserByUserIdAsDataTable(userid As Integer) As DataTable
            If userid = 0 Then Return Nothing

            Dim spName As String = "iaip_user.GetIaipUserByUserId"
            Dim parameter As New SqlParameter("@userid", userid)

            Return DB.SPGetDataTable(spName, parameter)
        End Function

        Public Function GetIaipUserByUsername(username As String) As IaipUser
            If String.IsNullOrEmpty(username) Then Return Nothing

            Dim spName As String = "iaip_user.GetIaipUserByUsername"
            Dim parameter As New SqlParameter("@username", username)

            Dim dataTable As DataTable = DB.SPGetDataTable(spName, parameter)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        Private Function FillIaipUserFromDataRow(row As DataRow) As IaipUser
            If row Is Nothing Then Return Nothing

            Dim user As New IaipUser
            With user
                .UserID = DBUtilities.GetNullable(Of Integer)(row("UserID"))
                .FirstName = DBUtilities.GetNullable(Of String)(row("First name"))
                .LastName = DBUtilities.GetNullable(Of String)(row("Last name"))
                .PhoneNumber = DBUtilities.GetNullable(Of String)(row("Phone number"))
                .OfficeNumber = DBUtilities.GetNullable(Of String)(row("Office"))
                .EmailAddress = DBUtilities.GetNullable(Of String)(row("Email address"))
                .ActiveEmployee = CType(CBool(row("ActiveEmployee")), ActiveOrInactive)
                .BranchID = DBUtilities.GetNullable(Of Integer)(row("BranchID"))
                .BranchName = DBUtilities.GetNullable(Of String)(row("Branch"))
                .ProgramID = DBUtilities.GetNullable(Of Integer)(row("ProgramID"))
                .ProgramName = DBUtilities.GetNullable(Of String)(row("Program"))
                .UnitId = DBUtilities.GetNullable(Of Integer)(row("UnitID"))
                .UnitName = DBUtilities.GetNullable(Of String)(row("Unit"))
                .Username = DBUtilities.GetNullable(Of String)(row("Username"))
                .IaipRoles = New IaipRoles(DBUtilities.GetNullable(Of String)(row("RolesString")))
                .RequirePasswordChange = Convert.ToBoolean(row("RequirePasswordChange"))
                .RequestProfileUpdate = Convert.ToBoolean(row("RequestProfileUpdate"))
            End With

            Return user
        End Function

        Public Function GetActiveUsers() As Dictionary(Of Integer, String)
            Dim spName As String = "iaip_user.GetActiveUsers"
            Return DB.SPGetLookupDictionary(spName)
        End Function

        Public Function UsernameExists(username As String, Optional ignoreUser As Integer = 0) As Boolean
            If String.IsNullOrEmpty(username) Then Return False
            Dim spName As String = "iaip_user.UsernameExists"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@ignoreUser", ignoreUser)
            }
            Return DB.SPGetBoolean(spName, parameters)
        End Function

        Public Function ActiveUserExists(username As String) As Boolean
            If String.IsNullOrEmpty(username) Then Return False
            Dim spName As String = "iaip_user.ActiveUserExists"
            Dim parameter As New SqlParameter("@username", username)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        Public Function EmailIsInUse(email As String, Optional ignoreUser As Integer = 0) As Boolean
            If String.IsNullOrEmpty(email.Trim) Then Return False
            Dim spName As String = "iaip_user.EmailInUse"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@email", email),
                New SqlParameter("@ignoreUser", ignoreUser)
            }
            Return DB.SPGetBoolean(spName, parameters)
        End Function

        Public Function UpdateUserPassword(username As String, newPassword As String, oldPassword As String) As PasswordUpdateResponse
            If String.IsNullOrEmpty(username) Then Return PasswordUpdateResponse.InvalidUsername
            If String.IsNullOrEmpty(newPassword) Then Return PasswordUpdateResponse.InvalidNewPassword
            If String.IsNullOrEmpty(oldPassword) Then Return PasswordUpdateResponse.InvalidLogin

            Dim spName As String = "iaip_user.UpdateUserPassword"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@newpassword", newPassword),
                New SqlParameter("@oldpassword", oldPassword)
            }
            Dim result As String = DB.SPGetSingleValue(Of String)(spName, parameters)

            If result IsNot Nothing Then
                Return [Enum].Parse(GetType(PasswordUpdateResponse), result)
            Else
                Return PasswordUpdateResponse.UnknownError
            End If
        End Function

        Public Enum PasswordUpdateResponse
            Success
            InvalidUsername
            InvalidLogin
            InvalidNewPassword
            UnknownError
        End Enum

        Public Function SendUsernameReminder(email As String) As UsernameReminderResponse
            If Not IsValidEmailAddress(email) Then
                Return UsernameReminderResponse.InvalidInput
            End If

            If Not EmailIsInUse(email) Then
                Return UsernameReminderResponse.EmailNotExist
            End If

            Dim spName As String = "iaip_user.RequestUsername"
            Dim parameter As New SqlParameter("@emailaddress", email)

            If DB.SPRunCommand(spName, parameter) Then
                Return UsernameReminderResponse.Success
            Else
                Return UsernameReminderResponse.UnknownError
            End If
        End Function

        Public Enum UsernameReminderResponse
            Success
            InvalidInput
            EmailNotExist
            UnknownError
        End Enum

        Public Function RequestPasswordReset(username As String) As RequestPasswordResetResponse
            If Not ActiveUserExists(username) Then
                Return RequestPasswordResetResponse.InvalidUsername
            End If

            Dim spName As String = "iaip_user.RequestUserPasswordReset"
            Dim parameter As New SqlParameter("@username", username)

            If DB.SPRunCommand(spName, parameter) Then
                Return RequestPasswordResetResponse.Success
            Else
                Return RequestPasswordResetResponse.UnknownError
            End If
        End Function

        Public Enum RequestPasswordResetResponse
            Success
            InvalidUsername
            UnknownError
        End Enum

        Public Function ResetUserPassword(username As String, newPassword As String, resettoken As String) As ResetPasswordResponse
            If Not ActiveUserExists(username) Then
                Return ResetPasswordResponse.InvalidUsername
            End If

            If String.IsNullOrEmpty(newPassword) Then Return ResetPasswordResponse.InvalidNewPassword
            If String.IsNullOrEmpty(resettoken) Then Return ResetPasswordResponse.InvalidToken

            Dim spName As String = "iaip_user.ResetUserPassword"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@newpassword", newPassword),
                New SqlParameter("@resettoken", resettoken)
            }
            Dim result As String = DB.SPGetSingleValue(Of String)(spName, parameters)

            If result IsNot Nothing Then
                Return [Enum].Parse(GetType(ResetPasswordResponse), result)
            Else
                Return ResetPasswordResponse.UnknownError
            End If
        End Function

        Public Enum ResetPasswordResponse
            Success
            InvalidUsername
            InvalidToken
            InvalidNewPassword
            MaxAttemptsExceeded
            UnknownError
        End Enum

        Public Function CreateNewUser(username As String, lastname As String,
                                      firstname As String, emailaddress As String, phone As String,
                                      branchid As Integer, programid As Integer, unitid As Integer,
                                      office As String, status As Boolean, ByRef newUserId As Integer) As Boolean

            Dim spName As String = "iaip_user.CreateNewUser"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@lastname", lastname),
                New SqlParameter("@firstname", firstname),
                New SqlParameter("@emailaddress", emailaddress),
                New SqlParameter("@phone", phone),
                New SqlParameter("@branchid", branchid),
                New SqlParameter("@programid", programid),
                New SqlParameter("@unitid", unitid),
                New SqlParameter("@office", office),
                New SqlParameter("@status", status),
                New SqlParameter("@createdby", CurrentUser.UserID)
            }

            newUserId = DB.SPGetSingleValue(Of Integer)(spName, parameters)
            Return (newUserId > 0)
        End Function

        Public Function SearchUsers(lastName As String,
                                    firstName As String,
                                    branch As Integer,
                                    program As Integer,
                                    unit As Integer,
                                    Optional includeInactive As Boolean = False
                                    ) As DataTable
            Dim spName As String = "iaip_user.SearchUsers"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@firstname", firstName),
                New SqlParameter("@lastname", lastName),
                New SqlParameter("@branchid", branch),
                New SqlParameter("@programid", program),
                New SqlParameter("@unitid", unit),
                New SqlParameter("@includeinactive", includeInactive)
            }
            Return DB.SPGetDataTable(spName, parameters)
        End Function

        Public Function UpdateUserProfile(user As IaipUser) As Boolean
            If user.UserID = 0 Then Return False

            Dim spName As String = "iaip_user.UpdateUserProfile"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userid", user.UserID),
                New SqlParameter("@username", user.Username),
                New SqlParameter("@lastname", user.LastName),
                New SqlParameter("@firstname", user.FirstName),
                New SqlParameter("@emailaddress", user.EmailAddress),
                New SqlParameter("@phone", user.PhoneNumber),
                New SqlParameter("@branchid", user.BranchID),
                New SqlParameter("@programid", user.ProgramID),
                New SqlParameter("@unitid", user.UnitId),
                New SqlParameter("@office", user.OfficeNumber),
                New SqlParameter("@status", user.ActiveEmployee),
                New SqlParameter("@updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function UpdateUserRoles(userId As Integer, roles As IaipRoles) As Boolean
            If userId = 0 Then Return False

            Dim spName As String = "iaip_user.UpdateUserRoles"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userid", userId),
                New SqlParameter("@rolesstring", roles.DbString),
                New SqlParameter("@updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function SaveSession(userId As Integer) As String
            If userId = 0 Then Return Nothing

            Dim spName As String = "iaip_user.SaveSession"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId),
                New SqlParameter("@machineName", Environment.MachineName),
                New SqlParameter("@windowsUserName", Environment.UserName),
                New SqlParameter("@windowsDomainName", Environment.UserDomainName),
                New SqlParameter("@osVersion", OSFriendlyName())
            }

            Return DB.SPGetString(spName, parameters)
        End Function

        Public Function ValidateSession(userId As Integer, token As String) As String
            Dim spName As String = "iaip_user.ValidateSession"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId),
                New SqlParameter("@machineName", Environment.MachineName),
                New SqlParameter("@windowsUserName", Environment.UserName),
                New SqlParameter("@windowsDomainName", Environment.UserDomainName),
                New SqlParameter("@token", token)
            }

            Return DB.SPGetString(spName, parameters)
        End Function

        Public Function RevokeSession(sessionId As String) As Boolean
            If String.IsNullOrEmpty(sessionId) Then Return False

            Dim spName As String = "iaip_user.RevokeSessionId"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@sessionId", sessionId)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function RevokeSession(userId As Integer) As Boolean
            Dim spName As String = "iaip_user.RevokeSession"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId),
                New SqlParameter("@machineName", Environment.MachineName),
                New SqlParameter("@windowsUserName", Environment.UserName),
                New SqlParameter("@windowsDomainName", Environment.UserDomainName)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function RevokeAllSessions(userId As Integer) As Boolean
            Dim spName As String = "iaip_user.RevokeAllSessions"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function GetSavedSessions(userId As Integer) As DataTable
            Dim query As String = "SELECT Id AS SessionId, 
                concat(MachineName, ' (', OSVersion, ')') AS Computer, 
                concat(WindowsDomainName, '/', WindowsUserName) AS [Windows account], 
                FORMAT(CreatedDate, 'MMMM d, yyyy') AS [Signed in]
                FROM AIRBRANCH.dbo.IAIP_SavedSessions
                WHERE UserId = @userId
                ORDER BY CreatedDate"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId)
            }
            Return DB.GetDataTable(query, parameters)
        End Function

    End Module
End Namespace