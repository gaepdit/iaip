Imports System.Collections.Generic

Public Class UsersService
    Private Shared _activeUsers As List(Of KeyValuePair(Of Integer, String))
    Private Shared _initLock As Object = New Object()

    Public Shared ReadOnly Property ActiveUsers As List(Of KeyValuePair(Of Integer, String))
        Get
            If (_activeUsers Is Nothing) Then
                InitializeData()
            End If
            Return _activeUsers
        End Get
    End Property

    Private Shared Sub InitializeData()
        SyncLock _initLock
            If (_activeUsers Is Nothing) Then
                _activeUsers = DAL.GetActiveUsers()
            End If
        End SyncLock
    End Sub

End Class
