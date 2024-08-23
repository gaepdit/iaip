Imports System.Threading.Tasks
Imports Microsoft.Data.SqlClient
Imports System.Collections.Generic
Imports GaEpd

Namespace DAL
    Module IaipUserData

        Public Async Function AuthenticateIaipUserAsync(username As String, password As String) As Task(Of IaipAuthenticationResult)
            If String.IsNullOrEmpty(username) Then Return IaipAuthenticationResult.InvalidUsername
            If String.IsNullOrEmpty(password) Then Return IaipAuthenticationResult.InvalidLogin

            Dim result As String = Await ValidateLoginApiAsync(New LoginCredentials With {.Username = username, .Password = password}).ConfigureAwait(False)

            If result Is Nothing Then Return IaipAuthenticationResult.InvalidLogin

            Select Case result
                Case "Success"
                    Return IaipAuthenticationResult.Success
                Case "InvalidUsername"
                    Return IaipAuthenticationResult.InvalidUsername
                Case "InactiveUser"
                    Return IaipAuthenticationResult.InactiveUser
                Case Else
                    Return IaipAuthenticationResult.InvalidLogin
            End Select
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

            Const spName As String = "iaip_user.GetIaipUserByUserId"
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
            Const spName As String = "iaip_user.GetActiveUsers"
            Return DB.SPGetLookupDictionary(spName)
        End Function

        Public Function UsernameExists(username As String, Optional ignoreUser As Integer = 0) As Boolean
            If String.IsNullOrEmpty(username) Then Return False
            Const spName As String = "iaip_user.UsernameExists"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@ignoreUser", ignoreUser)
            }
            Return DB.SPGetBoolean(spName, parameters)
        End Function

        Private Function ActiveUserExists(username As String) As Boolean
            If String.IsNullOrEmpty(username) Then Return False
            Const spName As String = "iaip_user.ActiveUserExists"
            Dim parameter As New SqlParameter("@username", username)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        Public Function EmailIsInUse(email As String, Optional ignoreUser As Integer = 0) As Boolean
            If String.IsNullOrEmpty(email.Trim) Then Return False
            Const spName As String = "iaip_user.EmailInUse"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@email", email),
                New SqlParameter("@ignoreUser", ignoreUser)
            }
            Return DB.SPGetBoolean(spName, parameters)
        End Function

        Public Function UpdateUserPassword(username As String, newPassword As String, oldPassword As String) As PasswordUpdateResponse
            If String.IsNullOrEmpty(username) Then Return PasswordUpdateResponse.InvalidUsername
            If IsValidPassword(newPassword) <> StringValidationResult.Valid Then Return PasswordUpdateResponse.InvalidNewPassword
            If String.IsNullOrEmpty(oldPassword) Then Return PasswordUpdateResponse.InvalidLogin

            Const spName As String = "iaip_user.UpdateUserPassword"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@newpassword", newPassword),
                New SqlParameter("@oldpassword", oldPassword)
            }

            Dim result As String = DB.SPGetSingleValue(Of String)(spName, parameters)
            If result Is Nothing Then Return PasswordUpdateResponse.UnknownError
            Return [Enum].Parse(GetType(PasswordUpdateResponse), result)
        End Function

        Public Enum PasswordUpdateResponse
            Success
            InvalidUsername
            InvalidLogin
            InvalidNewPassword
            UnknownError
        End Enum

        Public Async Function SendUsernameReminderAsync(email As String) As Task(Of UsernameReminderResponse)
            If Not email.IsValidEmailAddress() Then Return UsernameReminderResponse.InvalidInput
            Dim result As String = Await RequestUsernameApiAsync(New UsernameRequest With {.Email = email})
            If result Is Nothing Then Return UsernameReminderResponse.UnknownError
            Return [Enum].Parse(GetType(UsernameReminderResponse), result)
        End Function

        Public Enum UsernameReminderResponse
            Success
            InvalidInput
            EmailNotExist
            UnknownError
        End Enum

        Public Async Function RequestPasswordResetAsync(username As String) As Task(Of RequestPasswordResetResponse)
            Dim result As String = Await RequestUserPasswordResetApiAsync(New PasswordResetRequest With {.Username = username})
            If result Is Nothing Then Return RequestPasswordResetResponse.UnknownError
            Return [Enum].Parse(GetType(RequestPasswordResetResponse), result)
        End Function

        Public Enum RequestPasswordResetResponse
            Success
            InvalidUsername
            UnknownError
        End Enum

        Public Async Function ResetUserPasswordAsync(username As String, newPassword As String, resetToken As String) As Task(Of ResetPasswordResponse)
            If IsValidPassword(newPassword) <> StringValidationResult.Valid Then Return ResetPasswordResponse.InvalidNewPassword
            If String.IsNullOrEmpty(resetToken) Then Return ResetPasswordResponse.InvalidToken

            Dim result As String = Await ResetUserPasswordApiAsync(New PasswordReset _
                With {.Username = username, .NewPassword = newPassword, .ResetToken = resetToken})

            If result Is Nothing Then Return ResetPasswordResponse.UnknownError
            Return [Enum].Parse(GetType(ResetPasswordResponse), result)
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
                                      firstname As String, emailAddress As String, phone As String,
                                      branchId As Integer, programId As Integer, unitId As Integer,
                                      office As String, status As Boolean, ByRef newUserId As Integer) As Boolean

            Const spName As String = "iaip_user.CreateNewUser"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@username", username),
                New SqlParameter("@lastname", lastname),
                New SqlParameter("@firstname", firstname),
                New SqlParameter("@emailaddress", emailAddress),
                New SqlParameter("@phone", phone),
                New SqlParameter("@branchid", branchId),
                New SqlParameter("@programid", programId),
                New SqlParameter("@unitid", unitId),
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
            Const spName As String = "iaip_user.SearchUsers"
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

            Const spName As String = "iaip_user.UpdateUserProfile"
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

            Const spName As String = "iaip_user.UpdateUserRoles"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userid", userId),
                New SqlParameter("@rolesstring", roles.DbString),
                New SqlParameter("@updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function SaveSession(userId As Integer) As String
            If userId = 0 Then Return Nothing

            Const spName As String = "iaip_user.SaveSession"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId),
                New SqlParameter("@machineName", Environment.MachineName),
                New SqlParameter("@windowsUserName", Environment.UserName),
                New SqlParameter("@windowsDomainName", Environment.UserDomainName),
                New SqlParameter("@osVersion", OSFriendlyName())
            }

            Return DB.SPGetString(spName, parameters)
        End Function

        Public Function ValidateSessionAsync(userId As Integer, token As String) As Task(Of String)
            Return ValidateSessionApiAsync(
                New SessionCredentials With {
                    .UserId = userId,
                    .Token = token,
                    .MachineName = Environment.MachineName,
                    .WindowsUserName = Environment.UserName,
                    .WindowsDomainName = Environment.UserDomainName
                })
        End Function

        Public Function RevokeSession(sessionId As String) As Boolean
            If String.IsNullOrEmpty(sessionId) Then Return False

            Const spName As String = "iaip_user.RevokeSessionId"
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
            Const spName As String = "iaip_user.RevokeAllSessions"
            Dim parameters As SqlParameter() = {
                New SqlParameter("@userId", userId)
            }

            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function GetSavedSessions(userId As Integer) As DataTable
            Const query As String = "SELECT Id AS SessionId, 
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
