Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module IaipUserModule

        Public Function LoginIaipUser(ByVal userName As String, ByVal password As String) As IaipUser

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
                " FROM AIRBRANCH.EPDUSERS " & _
                " INNER JOIN AIRBRANCH.IAIPPERMISSIONS " & _
                " ON EPDUSERS.NUMUSERID = IAIPPERMISSIONS.NUMUSERID " & _
                " INNER JOIN AIRBRANCH.EPDUSERPROFILES " & _
                " ON EPDUSERS.NUMUSERID = EPDUSERPROFILES.NUMUSERID " & _
                " LEFT JOIN AIRBRANCH.LOOKUPEPDBRANCHES " & _
                " ON EPDUSERPROFILES.NUMBRANCH = LOOKUPEPDBRANCHES.NUMBRANCHCODE " & _
                " LEFT JOIN AIRBRANCH.LOOKUPEPDPROGRAMS " & _
                " ON EPDUSERPROFILES.NUMPROGRAM = LOOKUPEPDPROGRAMS.NUMPROGRAMCODE " & _
                " LEFT JOIN AIRBRANCH.LOOKUPEPDUNITS " & _
                " ON EPDUSERPROFILES.NUMUNIT = LOOKUPEPDUNITS.NUMUNITCODE " & _
                " WHERE UPPER(EPDUSERS.STRUSERNAME) = :pUsername  " & _
                " AND EPDUSERS.STRPASSWORD   = :pPassword "

            Dim parameters As OracleParameter()

            parameters = New OracleParameter() { _
                New OracleParameter("pUsername", userName), _
                New OracleParameter("pPassword", password) _
            }

            Dim dataTable As DataTable = DB.GetDataTable(query, parameters)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)
            Dim user As IaipUser = FillIaipUserFromDataRow(dataRow)

            Return user
        End Function

        '' Not currently used, but may be useful in the future
        'Public Function GetIaipUser(ByVal userName As String) As IaipUser

        '    Dim query As String = " SELECT EPDUSERS.NUMUSERID, " & _
        '        "   EPDUSERS.STRUSERNAME, " & _
        '        "   IAIPPERMISSIONS.STRIAIPPERMISSIONS, " & _
        '        "   EPDUSERPROFILES.NUMBRANCH, " & _
        '        "   EPDUSERPROFILES.NUMPROGRAM, " & _
        '        "   EPDUSERPROFILES.NUMUNIT, " & _
        '        "   EPDUSERPROFILES.NUMEMPLOYEESTATUS, " & _
        '        "   EPDUSERPROFILES.STRPHONE, " & _
        '        "   EPDUSERPROFILES.STREMAILADDRESS, " & _
        '        "   EPDUSERPROFILES.STRFIRSTNAME, " & _
        '        "   EPDUSERPROFILES.STRLASTNAME, " & _
        '        "   LOOKUPEPDBRANCHES.STRBRANCHDESC, " & _
        '        "   LOOKUPEPDPROGRAMS.STRPROGRAMDESC, " & _
        '        "   LOOKUPEPDUNITS.STRUNITDESC " & _
        '        " FROM AIRBRANCH.EPDUSERS " & _
        '        " INNER JOIN AIRBRANCH.IAIPPERMISSIONS " & _
        '        " ON EPDUSERS.NUMUSERID = IAIPPERMISSIONS.NUMUSERID " & _
        '        " INNER JOIN AIRBRANCH.EPDUSERPROFILES " & _
        '        " ON EPDUSERS.NUMUSERID = EPDUSERPROFILES.NUMUSERID " & _
        '        " LEFT JOIN AIRBRANCH.LOOKUPEPDBRANCHES " & _
        '        " ON EPDUSERPROFILES.NUMBRANCH = LOOKUPEPDBRANCHES.NUMBRANCHCODE " & _
        '        " LEFT JOIN AIRBRANCH.LOOKUPEPDPROGRAMS " & _
        '        " ON EPDUSERPROFILES.NUMPROGRAM = LOOKUPEPDPROGRAMS.NUMPROGRAMCODE " & _
        '        " LEFT JOIN AIRBRANCH.LOOKUPEPDUNITS " & _
        '        " ON EPDUSERPROFILES.NUMUNIT = LOOKUPEPDUNITS.NUMUNITCODE " & _
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
            Dim user As New IaipUser

            Dim staff As New Staff
            With staff
                .StaffId = DB.GetNullable(Of Integer)(row("numUserID"))
                .FirstName = DB.GetNullable(Of String)(row("strFirstName"))
                .LastName = DB.GetNullable(Of String)(row("strLastName"))
                .PhoneNumber = DB.GetNullable(Of String)(row("strPhone"))
                .EmailAddress = DB.GetNullable(Of String)(row("strEmailAddress"))
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
                .UserID = DB.GetNullable(Of Integer)(row("numUserID"))
                .UserName = DB.GetNullable(Of String)(row("strUserName"))
                .PermissionsString = DB.GetNullable(Of String)(row("strIAIPPermissions"))
            End With

            Return user
        End Function

        Public Function GetActiveUsers() As List(Of KeyValuePair(Of Integer, String))
            Dim spName As String = "AIRBRANCH.IAIP.GetActiveUsers"
            Return DB.SPGetListOfKeyValuePair(spName)
        End Function

    End Module
End Namespace