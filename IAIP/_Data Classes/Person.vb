Public Class Person

    Public Property FirstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property
    Private _firstName As String

    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property
    Private _lastName As String

    Public Property EmailAddress() As String
        Get
            Return _emailAddress
        End Get
        Set(ByVal value As String)
            _emailAddress = value
        End Set
    End Property
    Private _emailAddress As String

    Public Property PhoneNumber() As String
        Get
            Return _phoneNumber
        End Get
        Set(ByVal value As String)
            _phoneNumber = value
        End Set
    End Property
    Private _phoneNumber As String

    Public Overrides Function ToString() As String
        Return AlphaName
    End Function

    Public ReadOnly Property FullName() As String
        Get
            Return Convert.ToString(FirstName & " " & LastName)
        End Get
    End Property

    Public ReadOnly Property AlphaName() As String
        Get
            Return Convert.ToString(LastName & ", " & FirstName)
        End Get
    End Property

    Public Property Prefix() As String
        Get
            Return _prefix
        End Get
        Set(ByVal value As String)
            _prefix = value
        End Set
    End Property
    Private _prefix As String

    Public Property Suffix() As String
        Get
            Return _suffix
        End Get
        Set(ByVal value As String)
            _suffix = value
        End Set
    End Property
    Private _suffix As String

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property
    Private _title As String

End Class
