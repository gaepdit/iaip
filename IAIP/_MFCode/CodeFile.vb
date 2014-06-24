Imports System
Imports System.Security
Imports Oracle.DataAccess.Client

Module CodeFile

    Function Insert_APBContactInformation(ByVal AIRSNumber As String, ByVal Key As String, _
                                          ByVal ContactFirstName As String, ByVal ContactLastName As String, _
                                          ByVal ContactPrefix As String, ByVal ContactSuffix As String, _
                                          ByVal ContactTitle As String, ByVal ContactCompanyName As String, _
                                          ByVal ContactPhoneNumber1 As String, ByVal ContactPhoneNumber2 As String, _
                                          ByVal ContactFaxNumber As String, ByVal ContactEmail As String, _
                                          ByVal ContactAddress1 As String, ByVal ContactAddress2 As String, _
                                          ByVal ContactCity As String, ByVal ContactState As String, _
                                          ByVal ContactZipCode As String, ByVal ContactDescription As String) As Boolean
        Try
            If ContactState.Length > 2 Then
                ContactState = "GA"
            End If
            If AIRSNumber = "" Then
                Return False
            End If
            Dim SQL As String = "Insert into " & DBNameSpace & ".APBContactInformation " & _
             "values " & _
             "('0413" & AIRSNumber & Key & "', '0413" & AIRSNumber & "', " & _
             "" & Key & " , '" & Replace(ContactFirstName, "'", "''") & "', " & _
             "'" & Replace(ContactLastName, "'", "''") & "', '" & Replace(ContactPrefix, "'", "''") & "', " & _
             "'" & Replace(ContactSuffix, "'", "''") & "', '" & Replace(ContactTitle, "'", "''") & "', " & _
             "'" & Replace(ContactCompanyName, "'", "''") & "', '" & Replace(ContactPhoneNumber1, "'", "''") & "', " & _
             "'', '" & Replace(ContactFaxNumber, "'", "''") & "', " & _
             "'" & Replace(ContactEmail, "'", "''") & "', '" & Replace(ContactAddress1, "'", "''") & "', " & _
             "'', '" & Replace(ContactCity, "'", "''") & "', " & _
             "'" & Replace(ContactState, "'", "''") & "', '" & Replace(ContactZipCode, "'", "''") & "', " & _
             "'" & UserGCode & "', '" & OracleDate & "', " & _
             "'" & Replace(ContactDescription, "'", "''") & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Function

    Function Update_APBContactInformation(ByVal AIRSNumber As String, ByVal Key As String, _
                                         ByVal ContactFirstName As String, ByVal ContactLastName As String, _
                                         ByVal ContactPrefix As String, ByVal ContactSuffix As String, _
                                         ByVal ContactTitle As String, ByVal ContactCompanyName As String, _
                                         ByVal ContactPhoneNumber1 As String, ByVal ContactPhoneNumber2 As String, _
                                         ByVal ContactFaxNumber As String, ByVal ContactEmail As String, _
                                         ByVal ContactAddress1 As String, ByVal ContactAddress2 As String, _
                                         ByVal ContactCity As String, ByVal ContactState As String, _
                                         ByVal ContactZipCode As String, ByVal ContactDescription As String) As Boolean
        Try
            Dim NewKey As Integer = 0
            If ContactState.Length > 2 Then
                ContactState = "GA"
            End If
            If AIRSNumber = "" Then
                Return False
            End If

            Dim SQL As String = "Select " & _
            "substr(max(strKey) + 1, 2, 1) as NewKey " & _
            "from " & DBNameSpace & ".APBContactInformation " & _
            "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
            "and strKey like '" & Mid(Key, 1, 1) & "%' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("newKey")) Then
                    NewKey = 0
                Else
                    NewKey = dr.Item("newKey")
                End If
            End While
            dr.Close()

            If NewKey = 0 Then
                NewKey = 9
                SQL = "Delete " & DBNameSpace & ".APBContactInformation " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                "and strKey = '" & Mid(Key, 1, 1) & "9'"
                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()
            End If

            Do Until NewKey = 0
                ' MsgBox(NewKey.ToString)

                SQL = "Update " & DBNameSpace & ".APBContactInformation set " & _
                "strKey = '" & Mid(Key, 1, 1) & NewKey & "', " & _
                "strContactKey = '0413" & AIRSNumber & Mid(Key, 1, 1) & NewKey & "' " & _
                "where strAIRSNumber = '0413" & AIRSNumber & "' " & _
                "and strKey = '" & Mid(Key, 1, 1) & (NewKey - 1) & "' "

                cmd = New OracleCommand(SQL, CurrentConnection)
                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If
                dr = cmd.ExecuteReader
                dr.Close()

                NewKey -= 1
            Loop

            SQL = "Insert into " & DBNameSpace & ".APBContactInformation " & _
            "values " & _
            "('0413" & AIRSNumber & Mid(Key, 1, 1) & NewKey & "', " & _
            "'0413" & AIRSNumber & "', '" & Key & "', " & _
            "'" & Replace(ContactFirstName, "'", "''") & "', " & _
            "'" & Replace(ContactLastName, "'", "''") & "', " & _
            "'" & Replace(ContactPrefix, "'", "''") & "', " & _
            "'" & Replace(ContactSuffix, "'", "''") & "', " & _
            "'" & Replace(ContactTitle, "'", "''") & "', " & _
            "'" & Replace(ContactCompanyName, "'", "''") & "', " & _
            "'" & Replace(ContactPhoneNumber1, "'", "''") & "', " & _
            "'" & Replace(ContactPhoneNumber2, "'", "''") & "', " & _
            "'" & Replace(ContactFaxNumber, "'", "''") & "', " & _
            "'" & Replace(ContactEmail, "'", "''") & "', " & _
            "'" & Replace(ContactAddress1, "'", "''") & "', " & _
            "'" & Replace(ContactAddress2, "'", "''") & "', " & _
            "'" & Replace(ContactCity, "'", "''") & "', " & _
            "'" & Replace(ContactState, "'", "''") & "', " & _
            "'" & Replace(ContactZipCode, "'", "''") & "', " & _
            "'" & UserGCode & "', " & _
            "'" & OracleDate & "', " & _
            "'" & Replace(ContactDescription, "'", "''") & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Function

    Function Update_FS_Admin_Status(ByVal FeeYear As String, ByVal AIRSNumber As String) As Boolean
        Try

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_Status", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function ValidateNAICS(ByVal NAICSCode As String) As Boolean
        Try
            Dim SQL As String = "Select strNAICSCode " & _
            "from AIRBranch.EILookUpNAICS " & _
            "where strNAICSCode = '" & NAICSCode & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                Return True
            Else
                Return False
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function ValidateSIC(ByVal SICCode As String) As Boolean
        Try
            Dim SQL As String = "Select strSICCode " & _
            "from AIRBranch.LookUpSICCodes " & _
            "where strSICCode = '" & SICCode & "' " & _
            "and length(strSICCode) = 4 "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                Return True
            Else
                Return False
            End If
            dr.Close()

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

End Module
