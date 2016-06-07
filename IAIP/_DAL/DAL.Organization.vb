Imports Oracle.ManagedDataAccess.Client
Imports Iaip.SharedData

Namespace DAL
    Module Organization

        Public Function GetEpdBranchesAsDataTable() As DataTable
            Dim query As String = " SELECT NUMBRANCHCODE AS BranchCode, STRBRANCHDESC AS Description FROM AIRBRANCH.LOOKUPEPDBRANCHES " &
                " WHERE NUMBRANCHCODE <> 0 " &
                " ORDER BY Description "

            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("BranchCode")}
            Return dt
        End Function

        Public Function GetEpdProgramsAsDataTable(Optional ByVal branch As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMPROGRAMCODE AS ProgramCode, STRPROGRAMDESC AS Description, NUMBRANCHCODE AS BranchCode FROM AIRBRANCH.LOOKUPEPDPROGRAMS " &
                " WHERE NUMPROGRAMCODE <> 0 "
            If branch > 0 Then query &= " AND NUMBRANCHCODE = :branch "
            query &= " ORDER BY Description "

            Dim parameter As New OracleParameter("branch", branch)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("ProgramCode")}
            Return dt
        End Function

        Public Function GetEpdUnitsAsDataTable(Optional ByVal program As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMUNITCODE AS UnitCode, STRUNITDESC AS Description, NUMPROGRAMCODE AS ProgramCode FROM AIRBRANCH.LOOKUPEPDUNITS " &
                " WHERE NUMUNITCODE <> 0 "
            If program > 0 Then query &= " AND NUMPROGRAMCODE = :program "
            query &= " ORDER BY Description "

            Dim parameter As New OracleParameter("program", program)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("UnitCode")}
            Return dt
        End Function

        Public Function GetEpdManagersAsDataTable() As DataTable
            Dim query As String = "SELECT STRKEY AS ""Role"", STRMANAGEMENTNAME AS ""Name"" " &
                "FROM AIRBRANCH.LOOKUPAPBMANAGEMENTTYPE WHERE STRCURRENTCONTACT = 'C'"
            Dim dt As DataTable = DB.GetDataTable(query)
            dt.PrimaryKey = New DataColumn() {dt.Columns("Role")}
            Return dt
        End Function

        Public Function GetEpdManagerName(role As EpdManagementTypes) As String
            Dim dt As DataTable = GetSharedData(SharedTable.EpdManagers)
            Dim dr As DataRow = dt.Rows.Find(role.ToString)
            If dr Is Nothing Then
                Return " "
            Else
                Return DB.GetNullable(Of String)(dr("Name"))
            End If
        End Function

        Public Function SaveEpdManagerName(role As EpdManagementTypes, name As String) As Boolean
            Dim dt As DataTable = GetSharedData(SharedTable.EpdManagers)
            Dim dr As DataRow = dt.Rows.Find(role.ToString)
            Dim result As Boolean

            If dr Is Nothing Then
                result = SaveNewEpdManagerName(role, name)
                ClearSharedData(SharedTable.EpdManagers)
            Else
                If DB.GetNullable(Of String)(dr("Name")) = name Then
                    result = True
                Else
                    result = UpdateEpdManagerName(role, name)
                    ClearSharedData(SharedTable.EpdManagers)
                End If
            End If

            Return result
        End Function

        Private Function SaveNewEpdManagerName(role As EpdManagementTypes, name As String) As Boolean
            Dim query As String = "INSERT INTO AIRBRANCH.LOOKUPAPBMANAGEMENTTYPE " &
                " ( NUMID, STRKEY, STRMANAGEMENTNAME, STRCURRENTCONTACT) " &
                "  VALUES ( (SELECT MAX (NUMID) + 1 FROM AIRBRANCH.LOOKUPAPBMANAGEMENTTYPE), :prole, :pname, 'C')"
            Dim params As OracleParameter() = {
                New OracleParameter("prole", role.ToString),
                New OracleParameter("pname", name)
            }
            Return DB.RunCommand(query, params)
        End Function

        Private Function UpdateEpdManagerName(role As EpdManagementTypes, name As String) As Boolean
            Dim query As String = "UPDATE AIRBRANCH.LOOKUPAPBMANAGEMENTTYPE SET STRMANAGEMENTNAME = :pname WHERE STRKEY = :prole"
            Dim params As OracleParameter() = {
                New OracleParameter("prole", role.ToString),
                New OracleParameter("pname", name)
            }
            Return DB.RunCommand(query, params)
        End Function

        Public Enum EpdManagementTypes
            ' 20 characters max...
            '2345678901234567890
            DnrCommissioner
            EpdDirector
            ApbBranchChief
            IsmpProgramManager
            SscpProgramManager
            SsppProgramManager
        End Enum

    End Module
End Namespace
