Imports Oracle.ManagedDataAccess.Client

Namespace DAL
    Module Organization

        Public Function GetEpdBranchesAsDataTable() As DataTable
            Dim query As String = " SELECT NUMBRANCHCODE, STRBRANCHDESC FROM AIRBRANCH.LOOKUPEPDBRANCHES " &
                " WHERE NUMBRANCHCODE <> 0 " &
                " ORDER BY STRBRANCHDESC "

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("NUMBRANCHCODE")}
            Return dt
        End Function

        Public Function GetEpdProgramsAsDataTable(Optional ByVal branch As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMPROGRAMCODE, STRPROGRAMDESC, NUMBRANCHCODE FROM AIRBRANCH.LOOKUPEPDPROGRAMS " &
                " WHERE NUMPROGRAMCODE <> 0 "
            If branch > 0 Then query &= " AND NUMBRANCHCODE = :branch "
            query &= " ORDER BY STRPROGRAMDESC "

            Dim parameter As New OracleParameter("branch", branch)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("NUMPROGRAMCODE")}
            Return dt
        End Function

        Public Function GetEpdUnitsAsDataTable(Optional ByVal program As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMUNITCODE, STRUNITDESC, NUMPROGRAMCODE FROM AIRBRANCH.LOOKUPEPDUNITS " &
                " WHERE NUMUNITCODE <> 0 "
            If program > 0 Then query &= " AND NUMPROGRAMCODE = :program "
            query &= " ORDER BY STRUNITDESC "

            Dim parameter As New OracleParameter("program", program)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("NUMUNITCODE")}
            Return dt
        End Function

    End Module
End Namespace
