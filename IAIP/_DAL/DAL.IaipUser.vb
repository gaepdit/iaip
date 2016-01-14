Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Public Module IaipUserData

        Public Function AuthenticateIaipUser(ByVal username As String, ByVal password As String) As IaipAuthenticationResult
            If password = "" Or username = "" Then Return IaipAuthenticationResult.InvalidLogin

            Dim spName As String = "AIRBRANCH.IAIP_USER.AuthenticateIaipUser"
            Dim parameters As OracleParameter() = {
                New OracleParameter("username", username),
                New OracleParameter("userpassword", EncryptDecrypt.EncryptText(password)),
                New OracleParameter("authenticationresult", OracleDbType.Varchar2, 15, Nothing, ParameterDirection.Output)
            }
            Dim result As Boolean = DB.SPRunCommand(spName, parameters)

            If result AndAlso Not parameters(2).Value.IsNull Then
                Return [Enum].Parse(GetType(IaipAuthenticationResult), parameters(2).Value.Value)
            Else
                Return IaipAuthenticationResult.InvalidLogin
            End If
        End Function

        Public Enum IaipAuthenticationResult
            Success
            InvalidUserName
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
            Dim parameter As New OracleParameter("username", username)
            Return DB.SPGetBoolean(spName, parameter)
        End Function

        Public Function EmailIsInUse(email As String, Optional ignoreUser As Integer = 0) As Boolean
            If email.Trim = "" Then Return False

            Dim spName As String = "AIRBRANCH.IAIP_USER.EmailInUse"
            Dim parameter As OracleParameter()
            If ignoreUser > 0 Then
                parameter = New OracleParameter() {
                    New OracleParameter("email", email.Trim.ToLower),
                    New OracleParameter("ignoreUser", ignoreUser)
                }
            Else
                parameter = New OracleParameter() {
                    New OracleParameter("email", email.Trim.ToLower)
                }
            End If

            Return DB.SPGetBoolean(spName, parameter)
        End Function

        ' To remove
        Private Function GetNextUserId() As Integer
            Dim query As String = "SELECT( MAX( NUMUSERID ) + 1 ) FROM AIRBRANCH.EPDUSERS"
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

        Public Function UpdateUserPassword(username As String, newPassword As String, oldPassword As String) As PasswordUpdateResponse
            If username = "" Then Return PasswordUpdateResponse.InvalidInput
            If newPassword = "" Then Return PasswordUpdateResponse.InvalidNewPassword
            If oldPassword = "" Then Return PasswordUpdateResponse.InvalidOldPassword

            Select Case AuthenticateIaipUser(username, oldPassword)
                Case IaipAuthenticationResult.InactiveUser Or IaipAuthenticationResult.InvalidUserName
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

        Public Function CreateNewUser(username As String, password As String, lastname As String,
                                      firstname As String, emailaddress As String, phone As String,
                                      branchid As Integer, programid As Integer, unitid As Integer,
                                      office As String, status As Boolean, ByRef newUserId As Integer) As Boolean
            newUserId = DAL.GetNextUserId()
            If newUserId = 0 Then Return False

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            Dim query1 As String = "INSERT INTO AIRBRANCH.EPDUSERS " &
                "( NUMUSERID , STRUSERNAME , STRPASSWORD , CREATED_BY ) " &
                "VALUES " &
                "(:userid, :username, :password, :created_by ) "
            Dim params1 As OracleParameter() = New OracleParameter() {
                New OracleParameter("userid", newUserId),
                New OracleParameter("username", username),
                New OracleParameter("password", EncryptDecrypt.EncryptText(password)),
                New OracleParameter("created_by", CurrentUser.UserID)
            }
            Dim query2 As String = "INSERT INTO AIRBRANCH.EPDUSERPROFILES " &
                "( NUMUSERID , STREMPLOYEEID , STRLASTNAME , STRFIRSTNAME , " &
                "  STREMAILADDRESS , STRPHONE , " &
                "  NUMBRANCH , NUMPROGRAM , NUMUNIT , STROFFICE , " &
                  "NUMEMPLOYEESTATUS ) VALUES " &
                "( :userid, :employeeid, :lastname, :firstname, " &
                "  :emailaddress, :phone, " &
                "  :branchid, :programid, :unitid, :office, " &
                "  :status )"
            Dim params2 As OracleParameter() = New OracleParameter() {
                New OracleParameter("userid", newUserId),
                New OracleParameter("employeeid", "000"),
                New OracleParameter("lastname", lastname),
                New OracleParameter("firstname", firstname),
                New OracleParameter("emailaddress", emailaddress),
                New OracleParameter("phone", phone),
                New OracleParameter("branchid", If(branchid < 1, DBNull.Value, branchid)),
                New OracleParameter("programid", If(programid < 1, DBNull.Value, programid)),
                New OracleParameter("unitid", If(unitid < 1, DBNull.Value, unitid)),
                New OracleParameter("office", office),
                New OracleParameter("status", DB.ConvertBooleanToDBValue(status, DB.Utilities.BooleanDBConversionType.OneOrZero))
            }

            queryList.Add(query1)
            parametersList.Add(params1)
            queryList.Add(query2)
            parametersList.Add(params2)

            Return DB.RunCommand(queryList, parametersList)
        End Function

    End Module
End Namespace