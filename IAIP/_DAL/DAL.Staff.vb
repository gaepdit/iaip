Imports Oracle.ManagedDataAccess.Client

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
            Dim query As String = "SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS " & _
                " FROM AIRBRANCH.EPDUSERPROFILES WHERE NUMUSERID = :pId "
            Dim parameter As New OracleParameter("pId", id)

            Return GetStaffFromDataRow(DB.GetDataRow(query, parameter))
        End Function

        Private Function GetStaffFromDataRow(ByVal row As DataRow) As Staff
            Dim staff As New Staff
            With staff
                .StaffId = DB.GetNullable(Of Integer)(row("NUMUSERID"))
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .PhoneNumber = DB.GetNullable(Of String)(row("STRPHONE"))
                .EmailAddress = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .ActiveStatus = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("NUMEMPLOYEESTATUS")))
            End With
            Return staff
        End Function

        Public Function GetAllActiveStaffAsDataTable(Optional ByVal branch As Integer = 1) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String =
                " SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS, " &
                "     (STRLASTNAME || ', ' || STRFIRSTNAME) AS AlphaName " &
                " FROM AIRBRANCH.EPDUSERPROFILES " &
                " WHERE NUMEMPLOYEESTATUS = 1 " &
                " AND NUMBRANCH           = :pBranch " &
                " ORDER BY STRLASTNAME, STRFIRSTNAME "
            Dim parameter As New OracleParameter("pBranch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetAllComplianceStaff() As DataTable
            Dim query As String = "SELECT NUMUSERID, STAFF, STRLASTNAME FROM AIRBRANCH.VW_COMPLIANCESTAFF"
            Return DB.GetDataTable(query)
        End Function

    End Module
End Namespace