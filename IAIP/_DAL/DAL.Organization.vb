Imports System.Data.SqlClient
Imports Iaip.SharedData
Imports EpdIt.DBUtilities

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

        Public Function GetEpdProgramsAsDataTable(Optional branch As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMPROGRAMCODE AS ProgramCode, STRPROGRAMDESC AS Description, NUMBRANCHCODE AS BranchCode FROM LOOKUPEPDPROGRAMS " &
                " WHERE NUMPROGRAMCODE <> 0 "
            If branch > 0 Then query &= " AND NUMBRANCHCODE = @branch "
            query &= " ORDER BY Description "

            Dim parameter As New SqlParameter("@branch", branch)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("ProgramCode")}
            Return dt
        End Function

        Public Function GetEpdUnitsAsDataTable(Optional program As Integer = 0) As DataTable
            Dim query As String = " SELECT NUMUNITCODE AS UnitCode, STRUNITDESC AS Description, NUMPROGRAMCODE AS ProgramCode FROM LOOKUPEPDUNITS " &
                " WHERE NUMUNITCODE <> 0 "
            If program > 0 Then query &= " AND NUMPROGRAMCODE = @program "
            query &= " ORDER BY Description "

            Dim parameter As New SqlParameter("@program", program)
            Dim dt As DataTable = DB.GetDataTable(query, parameter)
            dt.PrimaryKey = New DataColumn() {dt.Columns("UnitCode")}
            Return dt
        End Function

        Public Function GetEpdManagersAsDataTable() As DataTable
            Dim query As String = "SELECT STRKEY AS Role, STRMANAGEMENTNAME AS Name " &
                "FROM LOOKUPAPBMANAGEMENTTYPE WHERE STRCURRENTCONTACT = 'C'"
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
                Return GetNullable(Of String)(dr("Name"))
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
                If GetNullable(Of String)(dr("Name")) = name Then
                    result = True
                Else
                    result = UpdateEpdManagerName(role, name)
                    ClearSharedData(SharedTable.EpdManagers)
                End If
            End If

            Return result
        End Function

        Private Function SaveNewEpdManagerName(role As EpdManagementTypes, name As String) As Boolean
            Dim query As String = "INSERT INTO LOOKUPAPBMANAGEMENTTYPE " &
                " ( NUMID, STRKEY, STRMANAGEMENTNAME, STRCURRENTCONTACT) " &
                "  VALUES ( (SELECT MAX (NUMID) + 1 FROM LOOKUPAPBMANAGEMENTTYPE), @prole, @pname, 'C')"
            Dim params As SqlParameter() = {
                New SqlParameter("@prole", role.ToString),
                New SqlParameter("@pname", name)
            }
            Return DB.RunCommand(query, params)
        End Function

        Private Function UpdateEpdManagerName(role As EpdManagementTypes, name As String) As Boolean
            Dim query As String = "UPDATE LOOKUPAPBMANAGEMENTTYPE SET STRMANAGEMENTNAME = @pname WHERE STRKEY = @prole"
            Dim params As SqlParameter() = {
                New SqlParameter("@prole", role.ToString),
                New SqlParameter("@pname", name)
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
