Imports System.Data.SqlClient

Namespace DAL
    Module Organization

        Public Function GetEpdBranchesAsDataTable() As DataTable
            Dim query As String = " SELECT NUMBRANCHCODE AS BranchCode, STRBRANCHDESC AS Description FROM LOOKUPEPDBRANCHES " &
                " WHERE NUMBRANCHCODE <> 0 " &
                " ORDER BY Description "

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("BranchCode")}
            Return dt
        End Function

        Public Function GetEpdProgramsAsDataTable(Optional ByVal branch As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMPROGRAMCODE AS ProgramCode, STRPROGRAMDESC AS Description, NUMBRANCHCODE AS BranchCode FROM LOOKUPEPDPROGRAMS " &
                " WHERE NUMPROGRAMCODE <> 0 "
            If branch > 0 Then query &= " AND NUMBRANCHCODE = @branch "
            query &= " ORDER BY Description "

            Dim parameter As New SqlParameter("@branch", branch)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("ProgramCode")}
            Return dt
        End Function

        Public Function GetEpdUnitsAsDataTable(Optional ByVal program As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMUNITCODE AS UnitCode, STRUNITDESC AS Description, NUMPROGRAMCODE AS ProgramCode FROM LOOKUPEPDUNITS " &
                " WHERE NUMUNITCODE <> 0 "
            If program > 0 Then query &= " AND NUMPROGRAMCODE = @program "
            query &= " ORDER BY Description "

            Dim parameter As New SqlParameter("@program", program)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("UnitCode")}
            Return dt
        End Function

    End Module
End Namespace
