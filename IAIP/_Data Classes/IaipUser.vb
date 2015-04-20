Public Class IaipUser

    Private _staff As Staff
    Public Property Staff() As Staff
        Get
            Return _staff
        End Get
        Set(ByVal value As Staff)
            _staff = value
        End Set
    End Property

    Private _permissionsString As String
    Public Property PermissionsString() As String
        Get
            Return _permissionsString
        End Get
        Set(ByVal value As String)
            _permissionsString = value
        End Set
    End Property

    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _userID As Integer
    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal value As Integer)
            _userID = value
        End Set
    End Property

End Class
