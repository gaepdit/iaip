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
            Dim query As String = "SELECT STRPROGRAMDESC FROM AIRBRANCH.LOOKUPEPDPROGRAMS WHERE NUMPROGRAMCODE = :id"
            Dim parameter As New OracleParameter("id", id)
            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        Public Function GetStaff(ByVal userid As String) As Staff
            Dim id As Integer

            If userid = "" OrElse Not Integer.TryParse(userid, id) Then
                Return Nothing
            End If

            Dim spName As String = "AIRBRANCH.IAIP_USER.GetStaff"
            Dim parameter As New OracleParameter("userid", id)

            Dim dataTable As DataTable = DB.SPGetDataTable(spName, parameter)
            If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then Return Nothing

            Return GetStaffFromDataRow(dataTable.Rows(0))
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
                " AND NUMBRANCH           = :branch " & _
                " ORDER BY STRLASTNAME, STRFIRSTNAME "
            Dim parameter As New OracleParameter("branch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetAllComplianceStaff() As DataTable
            Dim query As String = "SELECT NUMUSERID, STAFF, STRLASTNAME FROM AIRBRANCH.VW_COMPLIANCESTAFF"
            Return DB.GetDataTable(query)
        End Function
        
        Public Function UpdateStaffInfo(staff As Staff) As Boolean
            If staff.StaffId = 0 Then Return False

            Dim spName As String = "AIRBRANCH.IAIP_USER.UpdateUserProfile"
            Dim parameters As OracleParameter() = {
                New OracleParameter("userid", staff.StaffId),
                New OracleParameter("lastname", staff.LastName),
                New OracleParameter("firstname", staff.FirstName),
                New OracleParameter("emailaddress", staff.EmailAddress),
                New OracleParameter("phone", staff.PhoneNumber),
                New OracleParameter("branchid", staff.BranchID),
                New OracleParameter("programid", staff.ProgramID),
                New OracleParameter("unitid", staff.UnitId),
                New OracleParameter("office", staff.OfficeNumber),
                New OracleParameter("status", DB.ConvertBooleanToDBValue(staff.ActiveEmployee, DB.BooleanDBConversionType.OneOrZero)),
                New OracleParameter("updatedby", CurrentUser.UserID)
            }
            Return DB.SPRunCommand(spName, parameters)
        End Function

    End Module
End Namespace