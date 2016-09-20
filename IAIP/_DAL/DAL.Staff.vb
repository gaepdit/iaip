Imports System.Data.SqlClient

Namespace DAL
    Module StaffData

        Public Function GetActiveStaffAsDataTable(Optional branch As Integer = 1) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String =
                " SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS, " &
                "     (STRLASTNAME || ', ' || STRFIRSTNAME) AS AlphaName " &
                " FROM EPDUSERPROFILES " &
                " WHERE NUMEMPLOYEESTATUS = 1 " &
                " AND NUMBRANCH           = @branch " &
                " ORDER BY STRLASTNAME, STRFIRSTNAME "
            Dim parameter As New SqlParameter("@branch", branch)
            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetComplianceStaff() As DataTable
            Dim query As String = "SELECT NUMUSERID, STAFF, STRLASTNAME FROM VW_COMPLIANCESTAFF"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("NUMUSERID")}
            Return dt
        End Function

    End Module
End Namespace