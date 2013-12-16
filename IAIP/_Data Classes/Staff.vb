Public Class Staff
    Inherits Person

    Public Property StaffId() As Integer
        Get
            Return _staffId
        End Get
        Set(ByVal value As Integer)
            _staffId = value
        End Set
    End Property
    Private _staffId As Integer

    Public Property ActiveStatus() As Boolean
        Get
            Return _activeStatus
        End Get
        Set(ByVal value As Boolean)
            _activeStatus = value
        End Set
    End Property
    Private _activeStatus As Integer

End Class
