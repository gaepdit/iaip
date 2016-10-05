Imports System.Collections.Generic
Imports System.Data.SqlClient

Namespace DAL
    Module StaffData

        Public Function GetStaffDetailsAsDataTableByBranch(Optional branch As Integer = 1, Optional includeInactive As Boolean = False) As DataTable
            ' Default to Air Branch if no branch code is provided
            Dim query As String =
                " SELECT NUMUSERID, STRLASTNAME, STRFIRSTNAME, STREMAILADDRESS, STRPHONE, NUMEMPLOYEESTATUS, " &
                "     CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS AlphaName " &
                " FROM EPDUSERPROFILES " &
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

        Public Function GetStaffAsDataTableByUnit(unit As String, Optional includeInactive As Boolean = False) As DataTable
            Dim query As String = "SELECT NUMUSERID AS UserID, 
                CONCAT(STRLASTNAME, ', ', STRFIRSTNAME) AS UserName
                FROM EPDUSERPROFILES AS p
                INNER JOIN LOOKUPEPDUNITS AS u ON u.NUMUNITCODE = p.NUMUNIT
                WHERE u.STRUNITDESC = @unit "

            If Not includeInactive Then
                query &= " AND NUMEMPLOYEESTATUS = 1 "
            End If

            query &= " ORDER BY STRLASTNAME, STRFIRSTNAME "

            Dim parameter As New SqlParameter("@unit", unit)

            Return DB.GetDataTable(query, parameter)
        End Function

        Public Function GetComplianceStaff() As DataTable
            Dim query As String = "SELECT * FROM VW_COMPLIANCESTAFF"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("UserID")}
            dt.DefaultView.Sort = "StaffName ASC"
            Return dt
        End Function

    End Module
End Namespace