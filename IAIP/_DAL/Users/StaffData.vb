Imports Microsoft.Data.SqlClient

Namespace DAL
    Module StaffData

        Public Function GetStaffDetailsAsDataTableByBranch(Optional branch As Integer = 1, Optional includeInactive As Boolean = False) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String =
                " SELECT NUMUSERID, CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS AlphaName, STRPHONE " &
                " FROM dbo.EPDUSERPROFILES " &
                " WHERE NUMBRANCH = @branch "

            If Not includeInactive Then
                query &= " AND NUMEMPLOYEESTATUS = 1 "
            End If

            query &= " ORDER BY STRLASTNAME, STRFIRSTNAME "

            Dim parameter As New SqlParameter("@branch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetStaffAsDataTableByBranch(Optional branch As Integer = 1, Optional includeInactive As Boolean = False) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String =
                " SELECT NUMUSERID as UserID, CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName " &
                " FROM EPDUSERPROFILES " &
                " WHERE NUMBRANCH = @branch "

            If Not includeInactive Then
                query &= " AND NUMEMPLOYEESTATUS = 1 "
            End If

            query &= " ORDER BY STRLASTNAME, STRFIRSTNAME "

            Dim parameter As New SqlParameter("@branch", branch)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetComplianceStaff() As DataTable
            Dim query As String = "SELECT * FROM VW_COMPLIANCESTAFF"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = {dt.Columns("UserID")}
            dt.DefaultView.Sort = "StaffName ASC"
            Return dt
        End Function

    End Module
End Namespace