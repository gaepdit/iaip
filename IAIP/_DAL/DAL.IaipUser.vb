Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module IaipUserData

        Public Function AuthenticateIaipUser(ByVal username As String, ByVal password As String) As IaipAuthenticationResult
            If password = "" Or username = "" Then Return IaipAuthenticationResult.InvalidLogin

            Dim spName As String = "AIRBRANCH.IAIP_USER.AuthenticateIaipUser"
            Dim parameters As OracleParameter() = {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 20, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username),
                New OracleParameter("userpassword", EncryptDecrypt.EncryptText(password))
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

        Public Function GetIaipUser(ByVal username As String) As IaipUser
            If username = "" Then Return Nothing

            Dim spName As String = "AIRBRANCH.IAIP_USER.GetIaipUser"
            Dim parameter As New OracleParameter("username", username)

            Dim dataTable As DataTable = DB.SPGetDataTable(spName, parameter)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        Private Function FillIaipUserFromDataRow(ByVal row As DataRow) As IaipUser
            If row Is Nothing Then Return Nothing

            Dim user As New IaipUser
            With user
                .StaffId = DB.GetNullable(Of Integer)(row("NUMUSERID"))
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRPHONE"))
                .OfficeNumber = DB.GetNullable(Of String)(row("STROFFICE"))
                .EmailAddress = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .ActiveEmployee = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("NUMEMPLOYEESTATUS")))
                .BranchID = DB.GetNullable(Of Integer)(row("NUMBRANCH"))
                .BranchName = DB.GetNullable(Of String)(row("STRBRANCHDESC"))
                .ProgramID = DB.GetNullable(Of Integer)(row("NUMPROGRAM"))
                .ProgramName = DB.GetNullable(Of String)(row("STRPROGRAMDESC"))
                .UnitId = DB.GetNullable(Of Integer)(row("NUMUNIT"))
                .UnitName = DB.GetNullable(Of String)(row("STRUNITDESC"))
                .Username = DB.GetNullable(Of String)(row("STRUSERNAME"))
                .PermissionsString = DB.GetNullable(Of String)(row("STRIAIPPERMISSIONS"))
                .RequirePasswordChange = Convert.ToBoolean(row("REQUIREPASSWORDCHANGE"))
                .RequestProfileUpdate = Convert.ToBoolean(row("REQUESTPROFILEUPDATE"))
            End With

            Return user
        End Function

        Public Function GetActiveUsers() As List(Of KeyValuePair(Of Integer, String))
            Dim spName As String = "AIRBRANCH.IAIP_USER.GetActiveUsers"
            Return DB.SPGetListOfKeyValuePair(spName)
        End Function

        Public Function GetAccountFormAccessLookup() As DataTable
            Dim query As String = " SELECT NUMACCOUNTCODE, STRFORMACCESS FROM AIRBRANCH.LOOKUPIAIPACCOUNTS "
            Return DB.GetDataTable(query)
        End Function

        Public Function UsernameExists(username As String) As Boolean
            If username = "" Then Return False
            Dim spName As String = "AIRBRANCH.IAIP_USER.UsernameExists"
            Dim parameters As OracleParameter() = New OracleParameter() {
                New OracleParameter("ReturnValue", OracleDbType.Varchar2, 5, Nothing, ParameterDirection.ReturnValue),
                New OracleParameter("username", username)
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
            If username = "" Then Return PasswordUpdateResponse.InvalidInput
            If newPassword = "" Then Return PasswordUpdateResponse.InvalidNewPassword
            If oldPassword = "" Then Return PasswordUpdateResponse.InvalidOldPassword

            Select Case AuthenticateIaipUser(username, oldPassword)
                Case IaipAuthenticationResult.InactiveUser Or IaipAuthenticationResult.InvalidUsername
                    Return PasswordUpdateResponse.InvalidInput

                Case IaipAuthenticationResult.InvalidLogin
                    Return PasswordUpdateResponse.InvalidOldPassword

                Case IaipAuthenticationResult.Success
                    Dim spName As String = "AIRBRANCH.IAIP_USER.UpdateUserPassword"
                    Dim parameters As OracleParameter() = {
                        New OracleParameter("username", username),
                        New OracleParameter("newpassword", EncryptDecrypt.EncryptText(newPassword)),
                        New OracleParameter("oldpassword", EncryptDecrypt.EncryptText(oldPassword))
                    }
                    If DB.SPRunCommand(spName, parameters) Then
                        Return PasswordUpdateResponse.Success
                    Else
                        Return PasswordUpdateResponse.UnknownError
                    End If

            End Select
        End Function

        Public Enum PasswordUpdateResponse
            Success
            InvalidInput
            InvalidOldPassword
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
                Return ResetPasswordResponse.Success
            Else
                Return ResetPasswordResponse.UnknownError
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
                New OracleParameter("newpassword", EncryptDecrypt.EncryptText(newPassword)),
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

    End Module
End Namespace