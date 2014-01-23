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
            SQL = "Insert into " & DBNameSpace & ".APBContactInformation " & _
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

            SQL = "Select " & _
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
    Function Insert_FS_FeeRate(ByVal FeeYear As String, ByVal PeriodStart As String, _
                          ByVal PeriodEnd As String, ByVal Part70Fee As String, ByVal SMFee As String, _
                          ByVal PerTonRate As String, ByVal NSPSFee As String, ByVal FeeDueDate As String, _
                          ByVal AdminFee As String, ByVal AdminApplicable As String, ByVal Comments As String, _
                          ByVal Active As String, ByVal FirstQrtDue As String, ByVal SecondQrtDue As String, _
                          ByVal ThirdQrtDue As String, ByVal FourthQrtDue As String, ByVal AAThres As String, _
                          ByVal NAThres As String) As Boolean
        Try
            If IsDBNull(FeeYear) Or FeeYear = "" Then
                Return False
            Else
                If IsNumeric(FeeYear) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(Part70Fee) Or Part70Fee = "" Then
            Else
                If IsNumeric(Part70Fee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(SMFee) Or SMFee = "" Then
            Else
                If IsNumeric(SMFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(PerTonRate) Or PerTonRate = "" Then
            Else
                If IsNumeric(PerTonRate) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(NSPSFee) Or NSPSFee = "" Then
            Else
                If IsNumeric(NSPSFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(AdminFee) Or AdminFee = "" Then
            Else
                If IsNumeric(AdminFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(AAThres) Or AAThres = "" Then
            Else
                If IsNumeric(AAThres) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(NAThres) Or NAThres = "" Then
            Else
                If IsNumeric(NAThres) Then
                Else
                    Return False
                End If
            End If

            SQL = "Insert into FS_FeeRate " & _
            "values " & _
            "((Select max(numFeeRateID) + 1 from " & DBNameSpace & ".FS_FeeRate), " & _
            "'" & FeeYear & "', '" & PeriodStart & "', " & _
            "'" & PeriodEnd & "', '" & Part70Fee & "', " & _
            "'" & SMFee & "', '" & PerTonRate & "', " & _
            "'" & NSPSFee & "', '" & FeeDueDate & "', " & _
            "'" & AdminFee & "', " & _
            "'" & AdminApplicable & "', '" & Replace(Comments, "'", "''") & "', " & _
            "'1', '" & UserGCode & "', " & _
            "(to_char(sysdate, 'DD-mon-YY HH12:MI:SS')), " & _
            "(to_char(sysdate, 'DD-mon-YY HH12:MI:SS')), " & _
            "'" & FirstQrtDue & "', '" & SecondQrtDue & "', " & _
            "'" & ThirdQrtDue & "', '" & FourthQrtDue & "', " & _
            "'', '" & AAThres & "', '" & NAThres & "') "

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
    Function Update_FS_FeeRate(ByVal FeeRateID As String, ByVal FeeYear As String, ByVal PeriodStart As String, _
                          ByVal PeriodEnd As String, ByVal Part70Fee As String, ByVal SMFee As String, _
                          ByVal PerTonRate As String, ByVal NSPSFee As String, ByVal FeeDueDate As String, _
                          ByVal AdminFee As String, ByVal AdminApplicable As String, ByVal Comments As String, _
                          ByVal Active As String, ByVal FirstQrtDue As String, ByVal SecondQrtDue As String, _
                          ByVal ThridQrtDue As String, ByVal FourthQrtDue As String, ByVal AAThres As String, _
                          ByVal NAThres As String) As Boolean
        Try
            If IsNumeric(FeeRateID) Then
            Else
                Return False
            End If

            If IsDBNull(FeeYear) Or FeeYear = "" Then
                Return False
            Else
                If IsNumeric(FeeYear) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(Part70Fee) Or Part70Fee = "" Then
            Else
                If IsNumeric(Part70Fee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(SMFee) Or SMFee = "" Then
            Else
                If IsNumeric(SMFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(PerTonRate) Or PerTonRate = "" Then
            Else
                If IsNumeric(PerTonRate) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(NSPSFee) Or NSPSFee = "" Then
            Else
                If IsNumeric(NSPSFee) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(AdminFee) Or AdminFee = "" Then
            Else
                If IsNumeric(AdminFee) Then
                Else
                    Return False
                End If
            End If
            If IsDBNull(AAThres) Or AAThres = "" Then
            Else
                If IsNumeric(AAThres) Then
                Else
                    Return False
                End If
            End If
            If IsDBNull(NAThres) Or NAThres = "" Then
            Else
                If IsNumeric(NAThres) Then
                Else
                    Return False
                End If
            End If

            SQL = "Update " & DBNameSpace & ".FS_FeeRate set " & _
            "numFeeYear = '" & FeeYear & "', " & _
            "datFeePeriodStart = '" & PeriodStart & "', " & _
            "datFeePeriodEnd = '" & PeriodEnd & "', " & _
            "numPart70Fee = '" & Part70Fee & "', " & _
            "numSMFee = '" & SMFee & "', " & _
            "numPerTonRate = '" & PerTonRate & "', " & _
            "numNSPSFee = '" & NSPSFee & "', " & _
            "datFeeDueDate = '" & FeeDueDate & "', " & _
            "numAdminFeeRate = '" & AdminFee & "', " & _
            "datAdminApplicable = '" & AdminApplicable & "', " & _
            "strComments = '" & Replace(Comments, "'", "''") & "', " & _
            "Active = '" & Active & "', " & _
            "UpdateUser = '" & UserGCode & "', " & _
            "upDateDateTime = (to_char(sysdate, 'DD-Mon-YY HH12:MI:SS')), " & _
            "datFirstQrtDue = '" & FirstQrtDue & "', " & _
            "datSecondQrtDue = '" & SecondQrtDue & "', " & _
            "datThirdQrtDue = '" & ThridQrtDue & "', " & _
            "datFourthQrtDue = '" & FourthQrtDue & "',  " & _
            "numAAThres = '" & AAThres & "', " & _
            "numNAThres = '" & NAThres & "' " & _
            "where numFeeRateID = '" & FeeRateID & "' "

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
    Function Insert_FSLK_NSPSReason(ByVal Description As String) As Boolean
        Try
            SQL = "Insert into " & DBNameSpace & ".FSLK_NSPSReason " & _
            "Values " & _
            "((select max(NSPSReasonCode) + 1 from " & DBNameSpace & ".FSLK_NSPSReason), " & _
            "'" & Replace(Description, "'", "''") & "', " & _
            "'1', '" & UserGCode & "', " & _
            "'" & OracleDate & "', '" & OracleDate & "') "

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
    Function Update_FSLK_NSPSReason(ByVal NSPSReasonCode As String, ByVal Description As String, ByVal ActiveStatus As String) As Boolean
        Try
            If Description = "" Then
                SQL = "Update " & DBNameSpace & ".FSLK_NSPSReason set " & _
                "Active = '" & ActiveStatus & "', " & _
                "updateUser = '" & UserGCode & "', " & _
                "UpdateDateTime = '" & OracleDate & "' " & _
                "where NSPSReasonCode = '" & NSPSReasonCode & "' "
            Else
                SQL = "Update " & DBNameSpace & ".FSLK_NSPSReason set " & _
                "Description = '" & Replace(Description, "'", "''") & "', " & _
                "Active = '" & ActiveStatus & "', " & _
                "updateUser = '" & UserGCode & "', " & _
                "UpdateDateTime = '" & OracleDate & "' " & _
                "where NSPSReasonCode = '" & NSPSReasonCode & "' "
            End If

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
    Function Insert_FSLK_NSPSReasonYear(ByVal numFeeYear As String, ByVal NSPSReasonCode As String, ByVal DisplayOrder As String) As Boolean
        Try
            SQL = "Insert into " & DBNameSpace & ".FSLK_NSPSReasonYear " & _
            "values " & _
            "('" & numFeeYear & "', '" & NSPSReasonCode & "', " & _
            "'" & DisplayOrder & "', '1', " & _
            "'" & UserGCode & "', '" & OracleDate & "', " & _
            "'" & OracleDate & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function
    Function Update_FSLK_NSPSReasonYear(ByVal numFeeYear As String, ByVal NSPSReasonCode As String, ByVal DisplayOrder As String, _
                                       ByVal ActiveStatus As String) As Boolean
        Try
            SQL = "Update " & DBNameSpace & ".FSLK_NSPSReasonYear set " & _
            "NSPSReasonCode = '" & NSPSReasonCode & "', " & _
            "DisplayOrder = '" & DisplayOrder & "', " & _
            "Active = '" & ActiveStatus & "', " & _
            "updateUser = '" & UserGCode & "', " & _
            "updateDateTime = '" & OracleDate & "' " & _
            "where numFeeYear = '" & numFeeYear & "' " & _
            "and NSPSReasonCode = '" & NSPSReasonCode & "' "

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
    Function Insert_FS_Admin(ByVal FeeYear As String, ByVal AIRSNumber As String, _
                             ByVal Enrolled As String, _
                             ByVal DateEnrolled As String, ByVal InitialMailOut As String, _
                             ByVal MailoutSent As String, ByVal DateMailOutSent As String, _
                             ByVal Submittal As String, ByVal DateSubmittal As String, _
                             ByVal CurrentStatus As String, _
                             ByVal Comment As String, ByVal Active As String) As Boolean
        Try
            Dim AdminCheck As String = "0"

            If IsDBNull(FeeYear) Then
                Return False
            End If
            If IsDBNull(AIRSNumber) Then
                Return False
            End If

            SQL = "Select " & _
            "count(*) as AdminCount " & _
            "from " & DBNameSpace & ".FS_Admin " & _
            "where numFeeYear = '" & FeeYear & "' " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("AdminCount")) Then
                    AdminCheck = "0"
                Else
                    AdminCheck = dr.Item("AdminCount")
                End If
            End While
            dr.Close()

            If AdminCheck <> "0" Then
                Return False
            End If

            If IsDBNull(Enrolled) Then
                Enrolled = "0"
            Else
                If Enrolled = False Then
                    Enrolled = "0"
                Else
                    Enrolled = "1"
                End If
            End If
            If IsDBNull(InitialMailOut) Then
                InitialMailOut = "0"
            Else
                If InitialMailOut = False Then
                    InitialMailOut = "0"
                Else
                    InitialMailOut = "1"
                End If
            End If
            If IsDate(MailoutSent) Then
                MailoutSent = "0"
            Else
                If MailoutSent = False Then
                    MailoutSent = "0"
                Else
                    MailoutSent = "1"
                End If
            End If
            If IsDBNull(Submittal) Then
                Submittal = "0"
            Else
                If Submittal = False Then
                    Submittal = "0"
                Else
                    Submittal = "1"
                End If
            End If

            SQL = "Insert into " & DBNameSpace & ".FS_Admin " & _
            "values " & _
            "(" & FeeYear & ", '0413" & AIRSNumber & "', " & _
            "'" & Enrolled & "', '', " & _
            "'" & DateEnrolled & "', '" & InitialMailOut & "', " & _
            "'" & MailoutSent & "', '" & DateMailOutSent & "', " & _
            "'" & Submittal & "', '" & DateSubmittal & "', " & _
            "'1', '" & OracleDate & "', " & _
            "'" & Replace(Comment, "'", "''") & "', '1', " & _
            "'IAIP||" & UserName & "', '" & OracleDate & "', " & _
            "'" & OracleDate & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            SQL = "Update " & DBNameSpace & ".FS_Admin set " & _
           "datInitialEnrollment = datEnrollment " & _
           "where numFeeYear = '" & FeeYear & "' " & _
           "and strAIRSnumber = '0413" & AIRSNumber & "' " & _
           "and datInitialEnrollment is null "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_MAILOUT", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_DATA", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

            Update_FS_Admin_Status(FeeYear, AIRSNumber)

            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function
    Function Update_FS_Admin(ByVal FeeYear As String, ByVal AIRSNumber As String, _
                             ByVal Enrolled As String, _
                             ByVal DateEnrolled As String, ByVal InitialMailOut As String, _
                             ByVal MailoutSent As String, ByVal DateMailOutSent As String, _
                             ByVal Submittal As String, ByVal DateSubmittal As String, _
                             ByVal CurrentStatus As String, _
                             ByVal Comment As String, ByVal Active As String) As Boolean
        Try
            SQL = ""
            If IsDBNull(Enrolled) Or Enrolled = "" Then
            Else
                If Enrolled = False Then
                    SQL = SQL & "strEnrolled = '0', " & _
                    "datEnrollment = '', "
                    If IsDBNull(Active) Then
                    Else
                        If Active = False Then
                            SQL = SQL & "Active = '0', "
                        Else
                            SQL = SQL & "Active = '1', "
                        End If
                    End If
                Else
                    SQL = SQL & "strEnrolled = '1', "
                    If IsDBNull(DateEnrolled) Then
                    Else
                        SQL = SQL & "datEnrollment = '" & DateEnrolled & "', "
                    End If

                    If Active = False Then
                        SQL = SQL & "Active = '0', "
                    Else
                        SQL = SQL & "Active = '1', "
                    End If
                End If
            End If
            If IsDBNull(InitialMailOut) Then
            Else
                If InitialMailOut = False Then
                    SQL = SQL & "strInitialMailOut = '0', "
                Else
                    SQL = SQL & "strInitialMailOut = '1', "
                End If
            End If
            If IsDBNull(MailoutSent) Then
            Else
                If MailoutSent = False Then
                    SQL = SQL & "strMailOutsent = '0', " & _
                    "datMailOutSent = '', "
                Else
                    SQL = SQL & "strMailOutSent = '1', "
                    If IsDBNull(DateMailOutSent) Then
                    Else
                        SQL = SQL & "datMailOutSent = '" & DateMailOutSent & "', "
                    End If
                End If
            End If
            If IsDBNull(Submittal) Then
            Else
                If Submittal = False Then
                    SQL = SQL & "intSubmittal = '0', " & _
                    "datSubmittal = '', "
                Else
                    SQL = SQL & "intsubmittal = '1', "
                    If IsDBNull(DateSubmittal) Then
                    Else
                        SQL = SQL & "datSubmittal = '" & DateSubmittal & "', "
                    End If
                End If
            End If
            If IsDBNull(Comment) Then
            Else
                SQL = SQL & "strComment = '" & Replace(Comment, "'", "''") & "', "
            End If

            If SQL = "" Then
                Return False
            Else
                SQL = SQL & _
                "updateUser = 'IAIP||" & UserName & "', " & _
                "updateDateTime = '" & OracleDate & "' "
            End If

            SQL = "Update " & DBNameSpace & ".FS_Admin set " & SQL & _
            "where numFeeYear = '" & FeeYear & "' " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If IsDBNull(FeeYear) Or FeeYear = "" Then
            Else
                If IsNumeric(FeeYear) Then
                Else
                    Return False
                End If
            End If

            If IsDBNull(AIRSNumber) Or AIRSNumber = "" Then
            Else
                If IsNumeric(AIRSNumber) Then
                Else
                    Return False
                End If
            End If

            SQL = "Update " & DBNameSpace & ".FS_Admin set " & _
            "datInitialEnrollment = datEnrollment " & _
            "where numFeeYear = '" & FeeYear & "' " & _
            "and strAIRSnumber = '0413" & AIRSNumber & "' " & _
            "and datInitialEnrollment is null "
            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()


            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_MAILOUT", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_DATA", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

            Update_FS_Admin_Status(FeeYear, AIRSNumber)
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
    Function Insert_FS_Mailout(ByVal FeeYear As String, ByVal AIRSNumber As String, _
                            ByVal FirstName As String, ByVal LastName As String, _
                            ByVal Prefix As String, ByVal Title As String, _
                            ByVal ContactCoName As String, ByVal ContactAddress1 As String, _
                            ByVal ContactAddress2 As String, ByVal ContactCity As String, _
                            ByVal ContactState As String, ByVal ContactZipCode As String, _
                            ByVal ContactEmail As String, ByVal OpStatus As String, _
                            ByVal Classification As String, ByVal NSPS As String, _
                            ByVal Part70 As String, ByVal ShutDownDate As String, _
                            ByVal FacilityName As String, _
                            ByVal FacilityAddress1 As String, ByVal FacilityAddress2 As String, _
                            ByVal FacilityCity As String, ByVal FacilityZipCode As String, _
                            ByVal Comment As String, ByVal Active As String) As Boolean
        Try
            If IsDBNull(FeeYear) Then
                Return False
            End If
            If IsDBNull(AIRSNumber) Then
                Return False
            End If

            If IsDBNull(OpStatus) Then
            Else
                OpStatus = Mid(OpStatus, 1, 1)
            End If
            If NSPS = True Then
                NSPS = "1"
            Else
                NSPS = "0"
            End If
            If Part70 = True Then
                Part70 = "1"
            Else
                Part70 = "0"
            End If
            If Active = False Then
                Active = "0"
            Else
                Active = "1"
            End If

            SQL = "Insert into " & DBNameSpace & ".FS_MailOut " & _
            "values " & _
            "('" & FeeYear & "', '0413" & AIRSNumber & "', " & _
            "'" & Replace(FirstName, "'", "''") & "', '" & Replace(LastName, "'", "''") & "', " & _
            "'" & Replace(Prefix, "'", "''") & "', '" & Replace(Title, "'", "''") & "', " & _
            "'" & Replace(ContactCoName, "'", "''") & "', '" & Replace(ContactAddress1, "'", "''") & "', " & _
            "'" & Replace(ContactAddress2, "'", "''") & "', '" & Replace(ContactCity, "'", "''") & "', " & _
            "'" & Replace(ContactState, "'", "''") & "', '" & Replace(ContactZipCode, "'", "''") & "', " & _
            "'" & Replace(ContactEmail, "'", "''") & "', '" & Replace(OpStatus, "'", "''") & "', " & _
            "'" & Replace(Classification, "'", "''") & "', '" & Replace(NSPS, "'", "''") & "', " & _
            "'" & Replace(Part70, "'", "''") & "', '" & ShutDownDate & "', " & _
            "'" & Replace(FacilityName, "'", "''") & "', " & _
            "'" & Replace(FacilityAddress1, "'", "''") & "', '" & Replace(FacilityAddress2, "'", "''") & "', " & _
            "'" & Replace(FacilityCity, "'", "''") & "', '" & Replace(FacilityZipCode, "'", "''") & "', " & _
            "'" & Replace(Comment, "'", "''") & "', " & _
            "'" & Active & "', " & _
            "'IAIP||" & UserName & "', '" & OracleDate & "', " & _
            "'" & OracleDate & "') "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()
            Update_FS_Admin_Status(FeeYear, AIRSNumber)

            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function
    Function Update_FS_Mailout(ByVal FeeYear As String, ByVal AIRSNumber As String, _
                            ByVal FirstName As String, ByVal LastName As String, _
                            ByVal Prefix As String, ByVal Title As String, _
                            ByVal ContactCoName As String, ByVal ContactAddress1 As String, _
                            ByVal ContactAddress2 As String, ByVal ContactCity As String, _
                            ByVal ContactState As String, ByVal ContactZipCode As String, _
                            ByVal ContactEmail As String, ByVal OpStatus As String, _
                            ByVal Classification As String, ByVal NSPS As String, _
                            ByVal Part70 As String, ByVal ShutDownDate As String, _
                            ByVal FacilityName As String, _
                            ByVal FacilityAddress1 As String, ByVal FacilityAddress2 As String, _
                            ByVal FacilityCity As String, ByVal FacilityZipCode As String, _
                            ByVal Comment As String, ByVal Active As String) As Boolean
        Try
            If IsDBNull(FeeYear) Then
                Return False
            End If
            If IsDBNull(AIRSNumber) Then
                Return False
            End If
            SQL = ""

            If IsDBNull(FirstName) Then
            Else
                SQL = SQL & "strFirstName = '" & Replace(FirstName, "'", "''") & "', "
            End If
            If IsDBNull(LastName) Then
            Else
                SQL = SQL & "strLastName = '" & Replace(LastName, "'", "''") & "', "
            End If
            If IsDBNull(Prefix) Then
            Else
                SQL = SQL & "strPrefix = '" & Replace(Prefix, "'", "''") & "', "
            End If
            If IsDBNull(Title) Then
            Else
                SQL = SQL & "strTitle = '" & Replace(Title, "'", "''") & "', "
            End If
            If IsDBNull(ContactCoName) Then
            Else
                SQL = SQL & "strcontactCoName = '" & Replace(ContactCoName, "'", "''") & "', "
            End If
            If IsDBNull(ContactAddress1) Then
            Else
                SQL = SQL & "strContactAddress1 = '" & Replace(ContactAddress1, "'", "''") & "', "
            End If
            If IsDBNull(ContactAddress2) Then
            Else
                SQL = SQL & "strContactAddress2 = '" & Replace(ContactAddress2, "'", "''") & "', "
            End If
            If IsDBNull(ContactCity) Then
            Else
                SQL = SQL & "strContactCity = '" & Replace(ContactCity, "'", "''") & "', "
            End If
            If IsDBNull(ContactState) Then
            Else
                SQL = SQL & "strContactState = '" & Replace(ContactState, "'", "''") & "', "
            End If
            If IsDBNull(ContactZipCode) Then
            Else
                SQL = SQL & "strContactZipCode = '" & Replace(ContactZipCode, "'", "''") & "', "
            End If
            If IsDBNull(ContactEmail) Then
            Else
                SQL = SQL & "strGECOUserEmail = '" & Replace(ContactEmail, "'", "''") & "', "
            End If
            If IsDBNull(OpStatus) Then
            Else
                OpStatus = Mid(OpStatus, 1, 1)
                SQL = SQL & "strOperationalStatus = '" & Replace(OpStatus, "'", "''") & "', "
            End If
            If IsDBNull(Classification) Then
            Else
                SQL = SQL & "strClass = '" & Replace(Classification, "'", "''") & "', "
            End If
            If IsDBNull(NSPS) Then
            Else
                If NSPS = True Then
                    SQL = SQL & "strNSPS = '1', "
                Else
                    SQL = SQL & "strNSPS = '0', "
                End If
            End If
            If IsDBNull(Part70) Then
            Else
                If Part70 = True Then
                    SQL = SQL & "strPart70 = '1', "
                Else
                    SQL = SQL & "strPart70 = '0', "
                End If
            End If
            If IsDBNull(ShutDownDate) Then
            Else
                SQL = SQL & "datShutDownDate = '" & ShutDownDate & "', "
            End If

            If IsDBNull(FacilityName) Then
            Else
                SQL = SQL & "strFacilityName = '" & Replace(FacilityName, "'", "''") & "', "
            End If
            If IsDBNull(FacilityAddress1) Then
            Else
                SQL = SQL & "strFacilityAddress1 = '" & Replace(FacilityAddress1, "'", "''") & "', "
            End If
            If IsDBNull(FacilityAddress2) Then
            Else
                SQL = SQL & "strFacilityAddress2 = '" & Replace(FacilityAddress2, "'", "''") & "', "
            End If
            If IsDBNull(FacilityCity) Then
            Else
                SQL = SQL & "strFacilityCity = '" & Replace(FacilityCity, "'", "''") & "', "
            End If
            If IsDBNull(FacilityZipCode) Then
            Else
                SQL = SQL & "strFacilityZipCode = '" & Replace(FacilityZipCode, "'", "''") & "', "
            End If
            If IsDBNull(Comment) Then
            Else
                SQL = SQL & "strComment = '" & Replace(Comment, "'", "''") & "', "
            End If
            If IsDBNull(Active) Then
            Else
                If Active = False Then
                    SQL = SQL & "Active = '0', "
                Else
                    SQL = SQL & "Active = '1', "
                End If
            End If
            SQL = "Update " & DBNameSpace & ".FS_MailOut set " & _
            SQL & _
            "updateUser = 'IAIP||" & UserName & "', " & _
            "updateDateTime = '" & OracleDate & "' " & _
            "where numFeeYear = '" & FeeYear & "' " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            Update_FS_Admin_Status(FeeYear, AIRSNumber)
            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function
    Function Validate_FS_Invoices(ByVal FeeYear As String, ByVal AIRSNumber As String) As Boolean
        Try

            SQL = "Update airbranch.FS_FeeInvoice set " & _
            "strInvoiceStatus = '1', " & _
            "UpdateUser = '" & Replace(UserName, "'", "''") & "',  " & _
            "updateDateTime = sysdate " & _
            "where numFeeYear = '" & FeeYear & "' " & _
            "and strAIRSNumber = '0413" & AIRSNumber & "'  " & _
            "and numAmount = '0' " & _
            "and strInvoiceStatus = '0' " & _
            "and active = '1' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader
            dr.Close()

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            cmd = New OracleCommand("AIRBranch.PD_FEE_STATUS", CurrentConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New OracleParameter("FeeYear", OracleDbType.Decimal)).Value = FeeYear
            cmd.Parameters.Add(New OracleParameter("AIRSNumber", OracleDbType.Varchar2)).Value = "0413" & AIRSNumber

            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            ErrorReport(ex, "CodeFile." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Function

    Function ValidateNAICS(ByVal NAICSCode As String) As Boolean
        Try
            SQL = "Select strNAICSCode " & _
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
            SQL = "Select strSICCode " & _
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
