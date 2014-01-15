Imports Oracle.DataAccess.Client

Namespace DAL
    Module StaffInfo

        ''' <summary>
        ''' Returns a text description of a Branch Program
        ''' </summary>
        ''' <param name="id">The ID of the Program to describe</param>
        ''' <returns>A text description of the Branch Program</returns>
        Public Function GetProgramDescription(ByVal id As Integer) As String
            Dim query As String = "SELECT STRPROGRAMDESC FROM " & DBNameSpace & ".LOOKUPEPDPROGRAMS WHERE NUMPROGRAMCODE = :pId"
            Dim parameter As New OracleParameter("pId", id)
            Return DB.GetSingleValue(Of String)(query, parameter)
        End Function

        Public Function GetStaffInfoById(ByVal id As String) As Staff
            Dim query As String = "SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS " & _
                " FROM " & DBNameSpace & ".EPDUSERPROFILES WHERE NUMUSERID = :pId "

            Dim parameter As New OracleParameter("pId", id)

            Dim dataTable As DataTable = DB.GetDataTable(query, parameter)
            If dataTable Is Nothing Then Return Nothing

            Dim dataRow As DataRow = dataTable.Rows(0)
            Dim staff As New Staff
            FillStaffInfoFromDataRow(dataRow, staff)

            Return staff
        End Function

        Private Sub FillStaffInfoFromDataRow(ByVal row As DataRow, ByRef staff As Staff)
            With staff
                .StaffId = DB.GetNullable(Of Integer)(row("NUMUSERID"))
                .FirstName = DB.GetNullable(Of String)(row("STRFIRSTNAME"))
                .LastName = DB.GetNullable(Of String)(row("STRLASTNAME"))
                .Phone = DB.GetNullable(Of String)(row("STRPHONE"))
                .Email = DB.GetNullable(Of String)(row("STREMAILADDRESS"))
                .ActiveStatus = Convert.ToBoolean(DB.GetNullable(Of Integer)(row("NUMEMPLOYEESTATUS")))
            End With
        End Sub

        Public Function GetAllActiveStaffAsDataTable(Optional ByVal branch As Integer = 1) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String = _
                " SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS, " & _
                "     (STRLASTNAME || ', ' || STRFIRSTNAME) AS AlphaName " & _
                " FROM " & DBNameSpace & ".EPDUSERPROFILES " & _
                " WHERE NUMEMPLOYEESTATUS = 1 " & _
                " AND NUMBRANCH           = :pBranch " & _
                " ORDER BY STRLASTNAME, STRFIRSTNAME "
            Dim parameter As New OracleParameter("pBranch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

    End Module
End Namespace