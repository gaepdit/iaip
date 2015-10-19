﻿Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module IaipUserData

        Public Function LoginIaipUser(ByVal username As String, ByVal password As String) As IaipUser
            If username = "" Or password = "" Then Return Nothing

            Dim query As String = "SELECT usr.NUMUSERID , usr.STRUSERNAME , " &
                "  usr.REQUIREPASSWORDCHANGE , usr.REQUESTPROFILEUPDATE , " &
                "  prm.STRIAIPPERMISSIONS , " &
                "  prf.NUMBRANCH , prf.NUMPROGRAM , prf.NUMUNIT , " &
                "  prf.NUMEMPLOYEESTATUS , prf.STRPHONE , prf.STREMAILADDRESS , " &
                "  prf.STRFIRSTNAME , prf.STRLASTNAME , br.STRBRANCHDESC , " &
                "  pr.STRPROGRAMDESC , un.STRUNITDESC , prf.STROFFICE " &
                "FROM AIRBRANCH.EPDUSERS usr " &
                "INNER JOIN AIRBRANCH.IAIPPERMISSIONS prm " &
                "ON usr.NUMUSERID = prm.NUMUSERID " &
                "INNER JOIN AIRBRANCH.EPDUSERPROFILES prf " &
                "ON usr.NUMUSERID = prf.NUMUSERID " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDBRANCHES br " &
                "ON prf.NUMBRANCH = br.NUMBRANCHCODE " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDPROGRAMS pr " &
                "ON prf.NUMPROGRAM = pr.NUMPROGRAMCODE " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDUNITS un " &
                "ON prf.NUMUNIT = un.NUMUNITCODE " &
                "WHERE LOWER( usr.STRUSERNAME ) = LOWER( :username ) AND " &
                "  usr.STRPASSWORD = :password"

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() { _
                New OracleParameter("username", username), _
                New OracleParameter("password", password) _
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return FillIaipUserFromDataRow(dataTable.Rows(0))
        End Function

        '' Not currently used, but may be useful in the future
        'Public Function GetIaipUser(ByVal userName As String) As IaipUser

        '    Dim query As String = " -- ... " & _
        '        " WHERE UPPER(EPDUSERS.STRUSERNAME) = :username  "

        '    Dim parameters As OracleParameter()

        '    parameters = New OracleParameter() { _
        '        New OracleParameter("username", userName) _
        '    }

        '    Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
        '    If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

        '    Dim dataRow As DataRow = dataTable.Rows(0)
        '    Dim user As IaipUser = FillIaipUserFromDataRow(dataRow)

        '    Return user
        'End Function

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
                .UserName = DB.GetNullable(Of String)(row("STRUSERNAME"))
                .PermissionsString = DB.GetNullable(Of String)(row("STRIAIPPERMISSIONS"))
                .RequirePasswordChange = Convert.ToBoolean(row("REQUIREPASSWORDCHANGE"))
                .RequestProfileUpdate = Convert.ToBoolean(row("REQUESTPROFILEUPDATE"))
            End With

            Return user
        End Function

        Public Function GetActiveUsers() As List(Of KeyValuePair(Of Integer, String))
            Dim spName As String = "AIRBRANCH.IAIP.GetActiveUsers"
            Return DB.SPGetListOfKeyValuePair(spName)
        End Function

        Public Function GetAccountFormAccessLookup() As DataTable
            Dim query As String = " SELECT NUMACCOUNTCODE, STRFORMACCESS FROM AIRBRANCH.LOOKUPIAIPACCOUNTS "
            Return DB.GetDataTable(query)
        End Function

        Public Function UserNameExists(userName As String) As Boolean
            If userName = "" Then Return False
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.EPDUSERS " & _
                " WHERE RowNum = 1 " & _
                " AND lower(STRUSERNAME) = :userName "
            Dim parameter As New OracleParameter("userName", userName.ToLower)
            Return DB.GetBoolean(query, parameter)
        End Function

        Private Function GetNextUserId() As Integer
            Dim query As String = "SELECT( MAX( NUMUSERID ) + 1 ) FROM AIRBRANCH.EPDUSERS"
            Return DB.GetSingleValue(Of Integer)(query)
        End Function

        Public Function UpdateUserPassword(userId As Integer, newPassword As String, oldPassword As String) As PasswordUpdateResponse
            If userId = 0 Then Return PasswordUpdateResponse.InvalidInput
            If newPassword = "" Then Return PasswordUpdateResponse.InvalidNewPassword
            If oldPassword = "" OrElse Not CheckUserPassword(oldPassword) Then Return PasswordUpdateResponse.InvalidOldPassword

            Dim query As String = "UPDATE AIRBRANCH.EPDUSERS " &
                "SET STRPASSWORD = :newPassword , " &
                "REQUIREPASSWORDCHANGE = 'False' " &
                "WHERE NUMUSERID = :userId AND STRPASSWORD = :oldPassword"

            Dim parameters As OracleParameter() = { _
                New OracleParameter("userId", userId), _
                New OracleParameter("newPassword", EncryptDecrypt.EncryptText(newPassword)), _
                New OracleParameter("oldPassword", EncryptDecrypt.EncryptText(oldPassword)) _
            }

            If DB.RunCommand(query, parameters) Then
                Return PasswordUpdateResponse.Success
            Else
                Return PasswordUpdateResponse.UnknownError
            End If
        End Function

        Public Enum PasswordUpdateResponse
            Success
            InvalidInput
            InvalidOldPassword
            InvalidNewPassword
            UnknownError
        End Enum

        Private Function CheckUserPassword(password As String) As Boolean
            If password = "" Then Return False
            Dim query As String = "SELECT '" & Boolean.TrueString & "' " & _
                " FROM AIRBRANCH.EPDUSERS " & _
                " WHERE RowNum = 1 " & _
                " AND STRPASSWORD = :password"
            Dim parameter As New OracleParameter("password", EncryptDecrypt.EncryptText(password))
            Return DB.GetBoolean(query, parameter)
        End Function

        Public Function CreateNewUser(username As String, password As String, lastname As String,
                                      firstname As String, emailaddress As String, phone As String,
                                      branchid As Integer, programid As Integer, unitid As Integer,
                                      office As String, status As Boolean) As Boolean
            Dim nextId As Integer = DAL.GetNextUserId

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            Dim query1 As String = "INSERT INTO AIRBRANCH.EPDUSERS " &
                "( NUMUSERID , STRUSERNAME , STRPASSWORD , CREATED_BY ) " &
                "VALUES " &
                "(:userid, :username, :password, :created_by ) "
            Dim params1 As OracleParameter() = New OracleParameter() { _
                New OracleParameter("userid", nextId), _
                New OracleParameter("username", username), _
                New OracleParameter("password", EncryptDecrypt.EncryptText(password)), _
                New OracleParameter("created_by", CurrentUser.UserID) _
            }
            Dim query2 As String = "INTO AIRBRANCH.EPDUSERPROFILES " &
                "( NUMUSERID , STREMPLOYEEID , STRLASTNAME , STRFIRSTNAME , " &
                "  STREMAILADDRESS , STRPHONE , " &
                "  NUMBRANCH , NUMPROGRAM , NUMUNIT , STROFFICE , " &
                  "NUMEMPLOYEESTATUS ) VALUES " &
                "( :userid, :employeeid, :lastname, :firstname, " &
                "  :emailaddress, :phone, " &
                "  :branchid, :programid, :unitid, :office, " &
                "  :status )"
            Dim params2 As OracleParameter() = New OracleParameter() { _
                New OracleParameter("userid", nextId), _
                New OracleParameter("employeeid", "000"), _
                New OracleParameter("lastname", lastname), _
                New OracleParameter("firstname", firstname), _
                New OracleParameter("emailaddress", emailaddress), _
                New OracleParameter("phone", phone), _
                New OracleParameter("branchid", branchid), _
                New OracleParameter("programid", programid), _
                New OracleParameter("unitid", unitid), _
                New OracleParameter("office", office), _
                New OracleParameter("status", DB.DumbConvertToBoolean(status, DB.DumbConvertBooleanType.OneOrZero)) _
            }

            queryList.Add(query1)
            parametersList.Add(params1)
            queryList.Add(query2)
            parametersList.Add(params2)

            Return DB.RunCommand(queryList, parametersList)
        End Function

    End Module
End Namespace