Imports System.Collections.Generic

Public Class SharedData
    Private Shared _initLock As Object = New Object()
    Private Shared _tableDictionary As Dictionary(Of Tables, DataTable)

    Public Enum Tables
        Pollutants
        ViolationTypes
        AllComplianceStaff
        IaipAccountRoles
    End Enum

    Private Shared Sub InitializeData(table As Tables)
        SyncLock _initLock

            Dim dt As New DataTable

            Select Case table

                Case Tables.ViolationTypes
                    dt = DAL.Sscp.GetViolationTypes()
                    dt.PrimaryKey = New DataColumn() {dt.Columns("AIRVIOLATIONTYPECODE")}

                Case Tables.AllComplianceStaff
                    dt = DAL.StaffData.GetComplianceStaff()

                Case Tables.Pollutants
                    dt = DAL.CommonData.GetPollutantsTable()
                    dt.PrimaryKey = New DataColumn() {dt.Columns("Pollutant Code")}

                Case Tables.IaipAccountRoles
                    dt = DAL.GetIaipAccountRoles
                    dt.PrimaryKey = New DataColumn() {dt.Columns("RoleCode")}

            End Select

            dt.TableName = table.ToString

            If _tableDictionary.ContainsKey(table) Then
                _tableDictionary.Remove(table)
            End If
            _tableDictionary.Add(table, dt)

        End SyncLock
    End Sub

    Public Shared Function GetTable(table As Tables) As DataTable
        If _tableDictionary Is Nothing Then
            _tableDictionary = New Dictionary(Of Tables, DataTable)
        End If

        If Not _tableDictionary.ContainsKey(table) OrElse _tableDictionary(table) Is Nothing Then
            InitializeData(table)
        End If

        Return _tableDictionary(table)
    End Function

End Class