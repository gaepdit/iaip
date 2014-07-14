Public Class Contact
    Inherits Person

    Public Property CompanyName() As String
        Get
            Return _companyName
        End Get
        Set(ByVal value As String)
            _companyName = value
        End Set
    End Property
    Private _companyName As String

    Private _mailingAddress As Address
    Public Property MailingAddress() As Address
        Get
            Return _mailingAddress
        End Get
        Set(ByVal value As Address)
            _mailingAddress = value
        End Set
    End Property

    Private _secondaryPhoneNumber As String
    Public Property SecondaryPhoneNumber() As String
        Get
            Return _secondaryPhoneNumber
        End Get
        Set(ByVal value As String)
            _secondaryPhoneNumber = value
        End Set
    End Property

    Private _faxNumber As String
    Public Property FaxNumber() As String
        Get
            Return _faxNumber
        End Get
        Set(ByVal value As String)
            _faxNumber = value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

End Class
