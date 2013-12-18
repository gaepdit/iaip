Imports Oracle.DataAccess.Client

Namespace DAL
    Module StaffInfo

        Public Function GetStaffInfoById(ByVal id As String) As Staff
            Dim query As String = "SELECT STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS " & _
                " FROM AIRBRANCH.EPDUSERPROFILES WHERE NUMUSERID = :pId "

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
            Dim query As String = <s><![CDATA[
                SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS,
                    (STRLASTNAME || ', ' || STRFIRSTNAME) AS AlphaName
                FROM AIRBRANCH.EPDUSERPROFILES
                WHERE NUMEMPLOYEESTATUS = 1
                AND NUMBRANCH           = :pBranch
                ORDER BY STRLASTNAME, STRFIRSTNAME
                ]]></s>.Value
            Dim parameter As New OracleParameter("pBranch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

    End Module
End Namespace