Imports System.Collections.Generic

Public Class OrganizationService
    Private Shared _organizationDataSet As DataSet
    Private Shared _initLock As Object = New Object()

    Public Shared ReadOnly Property OrganizationDataSet() As DataSet
        Get
            If (_organizationDataSet Is Nothing) Then
                InitializeDataSet()
            End If
            Return _organizationDataSet
        End Get
    End Property

    Private Shared Sub InitializeDataSet()
        SyncLock _initLock
            If (_organizationDataSet Is Nothing) Then
                Dim ds As New DataSet("Organization")

                ds.Tables.Add(DAL.GetEpdBranchesAsDataTable)
                ds.Tables(0).TableName = "Branches"
                ds.Tables(0).Rows.Add({0})

                ds.Tables.Add(DAL.GetEpdProgramsAsDataTable)
                ds.Tables(1).TableName = "Programs"
                ds.Tables(1).Rows.Add({0})

                ds.Tables.Add(DAL.GetEpdUnitsAsDataTable)
                ds.Tables(2).TableName = "Units"
                ds.Tables(2).Rows.Add({0})

                _organizationDataSet = ds
            End If
        End SyncLock
    End Sub

End Class
