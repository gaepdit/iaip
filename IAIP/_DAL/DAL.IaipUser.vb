Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module IaipUserData

        Public Function AuthenticateIaipUser(ByVal username As String, ByVal password As String) As IaipAuthenticationResult
            If username = "" Then Return IaipAuthenticationResult.InvalidUsername
            If password = "" Then Return IaipAuthenticationResult.InvalidLogin

            Dim spName As String = "AIRBRANCH.IAIP_USER.AuthenticateIaipUser"
            Dim parameters As OracleParameter() = {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 20, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("userpassword", password)
            }
            Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            If result AndAlso Not parameters(0).Value.IsNull Then
                Return [Enum].Parse(GetType(IaipAuthenticationResult), parameters(0).Value.ToString)
            Else
                Return IaipAuthenticationResult.InvalidLogin
            End If
        End Function

        Public Enum IaipAuthenticationResult
            Success
            InvalidUsername
            InvalidLogin
            InactiveUser
        End Enum

        Public Function GetIaipUserByUserId(ByVal userid As String) As IaipUser
            Dim id As Integer

            If userid = "" OrElse Not Integer.TryParse(userid, id) Then
                Return Nothing
            End If

            Dim dataTable As DataTable = GetIaipUserByUserIdAsDataTable(id)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        Public Function GetIaipUserByUserIdAsDataTable(ByVal userid As Integer) As DataTable
            If userid = 0 Then Return Nothing

            Dim spName As String = "AIRBRANCH.IAIP_USER.GetIaipUserByUserId"
            Dim parameter As New OracleParameter("userid", userid)

            Return DB.SPGetDataTable(spName, parameter)
        End Function

        Public Function GetIaipUserByUsername(ByVal username As String) As IaipUser
            If username = "" Then Return Nothing

            Dim spName As String = "AIRBRANCH.IAIP_USER.GetIaipUserByUsername"
            Dim parameter As New OracleParameter("username", username)

            Dim dataTable As DataTable = DB.SPGetDataTable(spName, parameter)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        Private Function FillIaipUserFromDataRow(ByVal row As DataRow) As IaipUser
            If row Is Nothing Then Return Nothing

            Dim user As New IaipUser
            With user
                .UserID = DB.GetNullable(Of Integer)(row("UserID"))
                .FirstName = DB.GetNullable(Of String)(row("First name"))
                .LastName = DB.GetNullable(Of String)(row("Last name"))
                .PhoneNumber = DB.GetNullable(Of String)(row("Phone number"))
                .OfficeNumber = DB.GetNullable(Of String)(row("Office"))
                .EmailAddress = DB.GetNullable(Of String)(row("Email address"))
                .ActiveEmployee = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("ActiveEmployee")))
                .BranchID = DB.GetNullable(Of Integer)(row("BranchID"))
                .BranchName = DB.GetNullable(Of String)(row("Branch"))
                .ProgramID = DB.GetNullable(Of Integer)(row("ProgramID"))
                .ProgramName = DB.GetNullable(Of String)(row("Program"))
                .UnitId = DB.GetNullable(Of Integer)(row("UnitID"))
                .UnitName = DB.GetNullable(Of String)(row("Unit"))
                .Username = DB.GetNullable(Of String)(row("Username"))
                .IaipRoles = DB.GetNullable(Of String)(row("RolesString"))
                .RequirePasswordChange = Convert.ToBoolean(row("RequirePasswordChange"))
                .RequestProfileUpdate = Convert.ToBoolean(row("RequestProfileUpdate"))
            End With

            Return user
        End Function

        Public Function GetActiveUsers() As List(Of KeyValuePair(Of Integer, String))
            Dim spName As String = "AIRBRANCH.IAIP_USER.GetActiveUsers"
            Return DB.SPGetListOfKeyValuePair(spName)
        End Function

        Public Function UsernameExists(username As String, Optional ignoreUser As Integer = 0) As Boolean
            If username = "" Then Return False
            Dim spName As String = "AIRBRANCH.IAIP_USER.UsernameExists"
            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 5, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("ignoreUser", ignoreUser)
            }
            DB.SPRunCommand(spName, parameters)
            Return DB.GetNullable(Of Boolean)(parameters(0).Value.ToString)
        End Function

        Public Function EmailIsInUse(email As String, Optional ignoreUser As Integer = 0) As Boolean
            If email.Trim = "" Then Return False
            Dim spName As String = "AIRBRANCH.IAIP_USER.EmailInUse"
            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 5, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("email", email.Trim.ToLower),
                New OracleParameter("ignoreUser", ignoreUser)
            }
            DB.SPRunCommand(spName, parameters)
            Return DB.GetNullable(Of Boolean)(parameters(0).Value.ToString)
        End Function

        Public Function UpdateUserPassword(username As String, newPassword As String, oldPassword As String) As PasswordUpdateResponse
            If username = "" Then Return PasswordUpdateResponse.InvalidUsername
            If newPassword = "" Then Return PasswordUpdateResponse.InvalidNewPassword
            If oldPassword = "" Then Return PasswordUpdateResponse.InvalidLogin

            Dim spName As String = "AIRBRANCH.IAIP_USER.UpdateUserPassword"
            Dim parameters As OracleParameter() = {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 20, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("newpassword", newPassword),
                New OracleParameter("oldpassword", oldPassword)
            }
            Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            If result AndAlso Not parameters(0).Value.IsNull Then
                Return [Enum].Parse(GetType(PasswordUpdateResponse), parameters(0).Value.ToString)
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
            If email = "" OrElse Not IsValidEmailAddress(email) Then
                Return UsernameReminderResponse.InvalidInput
            End If

            If Not EmailIsInUse(email) Then
                Return UsernameReminderResponse.EmailNotExist
            End If

            Dim spName As String = "AIRBRANCH.IAIP_USER.RequestUsername"
            Dim parameter As New OracleParameter("emailaddress", email)
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
            If username = "" OrElse Not UsernameExists(username) Then
                Return RequestPasswordResetResponse.InvalidUsername
            End If

            Dim spName As String = "AIRBRANCH.IAIP_USER.RequestUserPasswordReset"
            Dim parameter As New OracleParameter("username", username)
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
            If username = "" OrElse Not UsernameExists(username) Then
                Return ResetPasswordResponse.InvalidUsername
            End If
            If newPassword = "" Then Return ResetPasswordResponse.InvalidNewPassword
            If resettoken = "" Then Return ResetPasswordResponse.InvalidToken

            Dim spName As String = "AIRBRANCH.IAIP_USER.ResetUserPassword"
            Dim parameters As OracleParameter() = {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 20, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("newpassword", newPassword),
                New OracleParameter("resettoken", resettoken)
            }
            Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            If result AndAlso Not parameters(0).Value.IsNull Then
                Return [Enum].Parse(GetType(ResetPasswordResponse), parameters(0).Value.ToString)
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

            Dim spName As String = "AIRBRANCH.IAIP_USER.CreateNewUser"
            Dim parameters As OracleParameter() = {
                New OracleParameter("ReturnValue", OracleDbType.Int32, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("lastname", lastname),
                New OracleParameter("firstname", firstname),
                New OracleParameter("emailaddress", emailaddress),
                New OracleParameter("phone", phone),
                New OracleParameter("branchid", branchid),
                New OracleParameter("programid", programid),
                New OracleParameter("unitid", unitid),
                New OracleParameter("office", office),
                New OracleParameter("status", DB.ConvertBooleanToDBValue(status, DB.BooleanDBConversionType.OneOrZero)),
                New OracleParameter("createdby", CurrentUser.UserID)
            }
            Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            If result AndAlso Not parameters(0).Value.IsNull Then
                newUserId = Integer.Parse(parameters(0).Value.ToString())
            Else
                newUserId = 0
            End If

            Return (newUserId > 0)
        End Function

        Public Function SearchUsers(lastName As String,
                                    firstName As String,
                                    branch As Integer,
                                    program As Integer,
                                    unit As Integer,
                                    Optional includeInactive As Boolean = False
                                    ) As DataTable
            Dim spName As String = "AIRBRANCH.IAIP_USER.SearchUsers"
            Dim parameters As OracleParameter() = {
                New OracleParameter("firstname", firstName),
                New OracleParameter("lastname", lastName),
                New OracleParameter("branchid", branch),
                New OracleParameter("programid", program),
                New OracleParameter("unitid", unit),
                New OracleParameter("includeinactive", includeInactive.ToString)
            }
            Return DB.SPGetDataTable(spName, parameters)
        End Function

        Public Function UpdateUserProfile(user As IaipUser) As Boolean
            If user.UserID = 0 Then Return False

            Dim spName As String = "AIRBRANCH.IAIP_USER.UpdateUserProfile"
            Dim parameters As OracleParameter() = {
                New OracleParameter("userid", user.UserID),
                New OracleParameter("username", user.Username),
                New OracleParameter("lastname", user.LastName),
                New OracleParameter("firstname", user.FirstName),
                New OracleParameter("emailaddress", user.EmailAddress),
                New OracleParameter("phone", user.PhoneNumber),
                New OracleParameter("branchid", user.BranchID),
                New OracleParameter("programid", user.ProgramID),
                New OracleParameter("unitid", user.UnitId),
                New OracleParameter("office", user.OfficeNumber),
                New OracleParameter("status", DB.ConvertBooleanToDBValue(user.ActiveEmployee, DB.BooleanDBConversionType.OneOrZero)),
                New OracleParameter("updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

        Public Function UpdateUserRoles(userId As Integer, roles As IaipRoles) As Boolean
            If userId = 0 Then Return False

            Dim spName As String = "AIRBRANCH.IAIP_USER.UpdateUserRoles"
            Dim parameters As OracleParameter() = {
                New OracleParameter("userid", userId),
                New OracleParameter("rolesstring", roles.DbString),
                New OracleParameter("updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

    End Module
End Namespace