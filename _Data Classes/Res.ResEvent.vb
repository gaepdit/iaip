Namespace Apb.Res

    Public Class ResEvent

        Public Property EventId() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property
        Private _id As Integer
        Public Property Active() As Boolean
            Get
                Return _active
            End Get
            Set(ByVal value As Boolean)
                _active = value
            End Set
        End Property
        Private _active As Boolean
        Public Property EventStatus() As String
            Get
                Return _eventStatus
            End Get
            Set(ByVal value As String)
                _eventStatus = value
            End Set
        End Property
        Private _eventStatus As String
        Public Property WebContact() As Staff
            Get
                Return _webContact
            End Get
            Set(ByVal value As Staff)
                _webContact = value
            End Set
        End Property
        Private _webContact As Staff
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
        Private _title As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
        Private _description As String
        Public Property StartDate() As Date?
            Get
                Return _startDate
            End Get
            Set(ByVal value As Date?)
                _startDate = value
            End Set
        End Property
        Private _startDate As Date?
        Public Property EndDate() As Date?
            Get
                Return _endDate
            End Get
            Set(ByVal value As Date?)
                _endDate = value
            End Set
        End Property
        Private _endDate As Date?
        Public Property Venue() As String
            Get
                Return _venue
            End Get
            Set(ByVal value As String)
                _venue = value
            End Set
        End Property
        Private _venue As String
        Public Property Capacity() As Integer
            Get
                Return _capacity
            End Get
            Set(ByVal value As Integer)
                _capacity = value
            End Set
        End Property
        Private _capacity As Integer
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property
        Private _notes As String
        Public Property LoginRequired() As Boolean
            Get
                Return _loginRequired
            End Get
            Set(ByVal value As Boolean)
                _loginRequired = value
            End Set
        End Property
        Private _loginRequired As Boolean
        Public Property PassCode() As String
            Get
                Return _passCode
            End Get
            Set(ByVal value As String)
                _passCode = value
            End Set
        End Property
        Private _passCode As String
        Public Property Address() As Address
            Get
                Return _address
            End Get
            Set(ByVal value As Address)
                _address = value
            End Set
        End Property
        Private _address As Address
        Public Property Contact() As Staff
            Get
                Return _contact
            End Get
            Set(ByVal value As Staff)
                _contact = value
            End Set
        End Property
        Private _contact As Staff
        Public Property StartTime() As String
            Get
                Return _startTime
            End Get
            Set(ByVal value As String)
                _startTime = value
            End Set
        End Property
        Private _startTime As String
        Public Property EndTime() As String
            Get
                Return _endTime
            End Get
            Set(ByVal value As String)
                _endTime = value
            End Set
        End Property
        Private _endTime As String

    End Class

    Public Class EventRegistration

        Public Property RegistrationId() As Integer
            Get
                Return _registrationId
            End Get
            Set(ByVal value As Integer)
                _registrationId = value
            End Set
        End Property
        Private _registrationId As Integer
        Public Property ResEventId() As Integer
            Get
                Return _resEventId
            End Get
            Set(ByVal value As Integer)
                _resEventId = value
            End Set
        End Property
        Private _resEventId As Integer
        Public Property RegistrationDateTime() As DateTime
            Get
                Return _registrationDateTime
            End Get
            Set(ByVal value As DateTime)
                _registrationDateTime = value
            End Set
        End Property
        Private _registrationDateTime As DateTime
        Public Property Comments() As String
            Get
                Return _comments
            End Get
            Set(ByVal value As String)
                _comments = value
            End Set
        End Property
        Private _comments As String
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property
        Private _status As String
        Public Property StatusCode() As Integer
            Get
                Return _statusCode
            End Get
            Set(ByVal value As Integer)
                _statusCode = value
            End Set
        End Property
        Private _statusCode As Integer
        Public Property Registrant() As Registrant
            Get
                Return _registrant
            End Get
            Set(ByVal value As Registrant)
                _registrant = value
            End Set
        End Property
        Private _registrant As Registrant
    End Class

    Public Class Registrant
        Inherits Person

        Public Property OlapId() As Integer
            Get
                Return _olapId
            End Get
            Set(ByVal value As Integer)
                _olapId = value
            End Set
        End Property
        Private _olapId As Integer
        Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                _company = value
            End Set
        End Property
        Private _company As String
    End Class

End Namespace
