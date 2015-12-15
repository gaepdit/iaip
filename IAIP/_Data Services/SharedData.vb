Imports System.Collections.Generic

Public Class SharedData
    Private Shared _initLock As Object = New Object()
    Private Shared _ds As DataSet

    Public Shared Function GetTable(table As Tables) As DataTable
        If _ds Is Nothing Then
            _ds = New DataSet
        End If

        If Not _ds.Tables.Contains(table.ToString) OrElse _ds.Tables(table.ToString) Is Nothing Then
            InitializeData(table)
        End If

        Return _ds.Tables(table.ToString)
    End Function

    Private Shared Sub InitializeData(table As Tables)
        SyncLock _initLock

            Dim _dt As New DataTable

            Select Case table

                Case Tables.ViolationTypes
                    _dt = DAL.Sscp.GetViolationTypes()
                    _dt.PrimaryKey = New DataColumn() {_dt.Columns("AIRVIOLATIONTYPECODE")}

                Case Tables.AllComplianceStaff
                    _dt = DAL.StaffData.GetAllComplianceStaff()

            End Select

            _dt.TableName = table.ToString
            _ds.Tables.Add(_dt)

        End SyncLock
    End Sub

    Public Enum Tables
        ViolationTypes
        AllComplianceStaff
    End Enum

End Class