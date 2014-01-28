Imports Oracle.DataAccess.Client

Namespace DAL
    Module LoginCreds

        Public Function GetLoginCred(ByVal userName As String, ByVal password As String) As LoginCred

            Dim query As String = " SELECT EPDUsers.numUserID, strUserName, strIAIPPermissions, " & _
                "   numBranch, numProgram, numUnit, numEmployeeStatus, " & _
                "   strPhone, strEmailAddress, strFirstName, strLastName " & _
                " FROM " & DBNameSpace & ".EPDUsers, " & _
                " " & DBNameSpace & ".IAIPPermissions, " & _
                " " & DBNameSpace & ".EPDUserProfiles " & _
                " WHERE EPDUsers.numUserID = IAIPPermissions.numUserID " & _
                " AND EPDUsers.numUserID = EPDUserProfiles.numUserId " & _
                " AND upper(strUserName) = :pUsername " & _
                " AND strPassword = :pPassword "

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() { _
                New OracleParameter("pUserName", userName), _
                New OracleParameter("pPassword", password) _
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)
            Dim loginCred As New LoginCred

            FillLoginCredFromDataRow(dataRow, loginCred)

            Return loginCred
        End Function

        Private Sub FillLoginCredFromDataRow(ByVal row As DataRow, ByRef loginCred As LoginCred)
            Dim staff As New Staff
            With Staff
                .StaffId = DB.GetNullable(Of Integer)(row("numUserID"))
                .FirstName = DB.GetNullable(Of String)(row("strFirstName"))
                .LastName = DB.GetNullable(Of String)(row("strLastName"))
                .Phone = DB.GetNullable(Of String)(row("strPhone"))
                .Email = DB.GetNullable(Of String)(row("strEmailAddress"))
                .ActiveStatus = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("numEmployeeStatus")))
                .BranchID = DB.GetNullable(Of Integer)(row("numBranch"))
                .ProgramID = DB.GetNullable(Of Integer)(row("numProgram"))
                .UnitId = DB.GetNullable(Of Integer)(row("numUnit"))
            End With

            With loginCred
                .Staff = staff
                .UserName = DB.GetNullable(Of String)(row("strUserName"))
                .PermissionsString = DB.GetNullable(Of String)(row("strIAIPPermissions"))
            End With
        End Sub

    End Module
End Namespace