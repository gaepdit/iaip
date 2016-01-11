Imports System.Collections.Generic

Public Class FeesService
    Private Shared _allFeeFacilities As DataTable
    Private Shared _initLock As Object = New Object()

    Public Shared ReadOnly Property AllFeeFacilities As DataTable
        Get
            If (_allFeeFacilities Is Nothing) Then
                InitializeData()
            End If
            Return _allFeeFacilities
        End Get
    End Property

    Private Shared Sub InitializeData()
        SyncLock _initLock
            If (_allFeeFacilities Is Nothing) Then
                _allFeeFacilities = DAL.GetAllFeeFacilities
            End If
        End SyncLock
    End Sub

End Class
