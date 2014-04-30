Imports Oracle.DataAccess.Client

Namespace DAL
    Module LoginCreds

        Public Function GetIaipUser(ByVal userName As String, ByVal password As String) As IaipUser

            Dim query As String = " SELECT EPDUSERS.NUMUSERID, " & _
                "   EPDUSERS.STRUSERNAME, " & _
                "   IAIPPERMISSIONS.STRIAIPPERMISSIONS, " & _
                "   EPDUSERPROFILES.NUMBRANCH, " & _
                "   EPDUSERPROFILES.NUMPROGRAM, " & _
                "   EPDUSERPROFILES.NUMUNIT, " & _
                "   EPDUSERPROFILES.NUMEMPLOYEESTATUS, " & _
                "   EPDUSERPROFILES.STRPHONE, " & _
                "   EPDUSERPROFILES.STREMAILADDRESS, " & _
                "   EPDUSERPROFILES.STRFIRSTNAME, " & _
                "   EPDUSERPROFILES.STRLASTNAME, " & _
                "   LOOKUPEPDBRANCHES.STRBRANCHDESC, " & _
                "   LOOKUPEPDPROGRAMS.STRPROGRAMDESC, " & _
                "   LOOKUPEPDUNITS.STRUNITDESC " & _
                " FROM " & DBNameSpace & ".EPDUSERS " & _
                " INNER JOIN " & DBNameSpace & ".IAIPPERMISSIONS " & _
                " ON EPDUSERS.NUMUSERID = IAIPPERMISSIONS.NUMUSERID " & _
                " INNER JOIN " & DBNameSpace & ".EPDUSERPROFILES " & _
                " ON EPDUSERS.NUMUSERID = EPDUSERPROFILES.NUMUSERID " & _
                " LEFT JOIN " & DBNameSpace & ".LOOKUPEPDBRANCHES " & _
                " ON EPDUSERPROFILES.NUMBRANCH = LOOKUPEPDBRANCHES.NUMBRANCHCODE " & _
                " LEFT JOIN " & DBNameSpace & ".LOOKUPEPDPROGRAMS " & _
                " ON EPDUSERPROFILES.NUMPROGRAM = LOOKUPEPDPROGRAMS.NUMPROGRAMCODE " & _
                " LEFT JOIN " & DBNameSpace & ".LOOKUPEPDUNITS " & _
                " ON EPDUSERPROFILES.NUMUNIT = LOOKUPEPDUNITS.NUMUNITCODE " & _
                " WHERE UPPER(EPDUSERS.STRUSERNAME) = :pUsername  " & _
                " AND EPDUSERS.STRPASSWORD   = :pPassword "

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() { _
                New OracleParameter("pUserName", userName), _
                New OracleParameter("pPassword", password) _
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)
            Dim user As New IaipUser

            FillIaipUserFromDataRow(dataRow, user)

            Return user
        End Function

        Private Sub FillIaipUserFromDataRow(ByVal row As DataRow, ByRef user As IaipUser)
            Dim staff As New Staff
            With Staff
                .StaffId = DB.GetNullable(Of Integer)(row("numUserID"))
                .FirstName = DB.GetNullable(Of String)(row("strFirstName"))
                .LastName = DB.GetNullable(Of String)(row("strLastName"))
                .Phone = DB.GetNullable(Of String)(row("strPhone"))
                .Email = DB.GetNullable(Of String)(row("strEmailAddress"))
                .ActiveStatus = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("numEmployeeStatus")))
                .BranchID = DB.GetNullable(Of Integer)(row("numBranch"))
                .BranchName = DB.GetNullable(Of String)(row("strBranchDesc"))
                .ProgramID = DB.GetNullable(Of Integer)(row("numProgram"))
                .ProgramName = DB.GetNullable(Of String)(row("strProgramDesc"))
                .UnitId = DB.GetNullable(Of Integer)(row("numUnit"))
                .UnitName = DB.GetNullable(Of String)(row("strUnitDesc"))
            End With

            With user
                .Staff = staff
                .UserName = DB.GetNullable(Of String)(row("strUserName"))
                .PermissionsString = DB.GetNullable(Of String)(row("strIAIPPermissions"))
            End With
        End Sub

    End Module
End Namespace