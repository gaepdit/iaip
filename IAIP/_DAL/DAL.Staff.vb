Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic

Namespace DAL
    Module StaffData

        ''' <summary>
        ''' Returns a text description of a Branch Program
        ''' </summary>
        ''' <param name="id">The ID of the Program to describe</param>
        ''' <returns>A text description of the Branch Program</returns>
        Public Function GetProgramDescription(ByVal id As Integer) As String
            Dim query As String = "SELECT STRPROGRAMDESC FROM AIRBRANCH.LOOKUPEPDPROGRAMS WHERE NUMPROGRAMCODE = :pId"
            Dim parameter As New OracleParameter("pId", id)
            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        Public Function GetStaff(ByVal id As String) As Staff
            Dim query As String = "SELECT prf.NUMUSERID , prf.STRFIRSTNAME , prf.STRLASTNAME , " &
                "  prf.STREMAILADDRESS , prf.STRPHONE , prf.NUMEMPLOYEESTATUS , " &
                "  prf.NUMBRANCH , prf.NUMPROGRAM , prf.NUMUNIT , " &
                "  br.STRBRANCHDESC , pr.STRPROGRAMDESC , un.STRUNITDESC , " &
                "  prf.STROFFICE " &
                "FROM AIRBRANCH.EPDUSERPROFILES prf " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDBRANCHES br " &
                "ON prf.NUMBRANCH = br.NUMBRANCHCODE " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDPROGRAMS pr " &
                "ON prf.NUMPROGRAM = pr.NUMPROGRAMCODE " &
                "LEFT JOIN AIRBRANCH.LOOKUPEPDUNITS un " &
                "ON prf.NUMUNIT = un.NUMUNITCODE " &
                "WHERE prf.NUMUSERID = :id"
            Dim parameter As New OracleParameter("id", id)

            Return GetStaffFromDataRow(DB.GetDataRow(query, parameter))
        End Function

        Private Function GetStaffFromDataRow(ByVal row As DataRow) As Staff
            Dim staff As New Staff
            With staff
                ' Person
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .EmailAddress = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRPHONE"))

                ' Staff
                .StaffId = DB.GetNullable(Of Integer)(row("NUMUSERID"))
                .ActiveEmployee = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("NUMEMPLOYEESTATUS")))
                .BranchID = DB.GetNullable(Of Integer)(row("NUMBRANCH"))
                .BranchName = DB.GetNullable(Of String)(row("STRBRANCHDESC"))
                .ProgramID = DB.GetNullable(Of Integer)(row("NUMPROGRAM"))
                .ProgramName = DB.GetNullable(Of String)(row("STRPROGRAMDESC"))
                .UnitId = DB.GetNullable(Of Integer)(row("NUMUNIT"))
                .UnitName = DB.GetNullable(Of String)(row("STRUNITDESC"))
                .OfficeNumber = DB.GetNullable(Of String)(row("STROFFICE"))
            End With
            Return staff
        End Function

        Public Function GetAllActiveStaffAsDataTable(Optional ByVal branch As Integer = 1) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String = _
                " SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS, " & _
                "     (STRLASTNAME || ', ' || STRFIRSTNAME) AS AlphaName " & _
                " FROM AIRBRANCH.EPDUSERPROFILES " & _
                " WHERE NUMEMPLOYEESTATUS = 1 " & _
                " AND NUMBRANCH           = :pBranch " & _
                " ORDER BY STRLASTNAME, STRFIRSTNAME "
            Dim parameter As New OracleParameter("pBranch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function UpdateStaffInfo(staff As Staff) As Boolean
            If staff.StaffId = 0 Then Return False

            Dim queryList As New List(Of String)
            Dim parametersList As New List(Of OracleParameter())

            Dim query As String = "UPDATE AIRBRANCH.EPDUSERPROFILES " &
                "SET STRLASTNAME = :LastName , STRFIRSTNAME = :FirstName , " &
                "  STREMAILADDRESS = :EmailAddress , STRPHONE = :PhoneNumber , " &
                "  NUMBRANCH = :BranchID , NUMPROGRAM = :ProgramID , " &
                "  NUMUNIT = :UnitId , STROFFICE = :OfficeNumber , " &
                "  NUMEMPLOYEESTATUS = :ActiveEmployee " &
                "WHERE NUMUSERID = :StaffId"
            Dim parameters As OracleParameter() = { _
                New OracleParameter("StaffId", staff.StaffId), _
                New OracleParameter("LastName", staff.LastName), _
                New OracleParameter("FirstName", staff.FirstName), _
                New OracleParameter("EmailAddress", staff.EmailAddress), _
                New OracleParameter("PhoneNumber", staff.PhoneNumber), _
                New OracleParameter("BranchID", staff.BranchID), _
                New OracleParameter("ProgramID", staff.ProgramID), _
                New OracleParameter("UnitId", staff.UnitId), _
                New OracleParameter("OfficeNumber", staff.OfficeNumber), _
                New OracleParameter("ActiveEmployee", DB.DumbConvertFromBoolean(staff.ActiveEmployee, DB.DumbConvertBooleanType.OneOrZero))
            }
            queryList.Add(query)
            parametersList.Add(parameters)

            query = "UPDATE AIRBRANCH.EPDUSERS " &
                "SET REQUESTPROFILEUPDATE = 'False' " &
                "WHERE NUMUSERID = :StaffId"
            parameters = {New OracleParameter("StaffId", staff.StaffId)}
            queryList.Add(query)
            parametersList.Add(parameters)

            Return DB.RunCommand(queryList, parametersList)
        End Function

    End Module
End Namespace