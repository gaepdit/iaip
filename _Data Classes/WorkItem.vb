Namespace Apb

    Public Class WorkItem

        Public Property TrackingNumber() As String
            Get
                Return _trackingNumber
            End Get
            Set(ByVal value As String)
                _trackingNumber = value
            End Set
        End Property
        Private _trackingNumber As String

        Public Property Facility() As Facility
            Get
                Return _facility
            End Get
            Set(ByVal value As Facility)
                _facility = value
            End Set
        End Property
        Private _facility As Facility

        Public Property DateReceived() As Date
            Get
                Return _dateReceived
            End Get
            Set(ByVal value As Date)
                _dateReceived = value
            End Set
        End Property
        Private _dateReceived As Date

        Public Property DateComplete() As Date?
            Get
                Return _dateComplete
            End Get
            Set(ByVal value As Date?)
                _dateComplete = value
            End Set
        End Property
        Private _dateComplete As Date?

        Public Property DateAcknowledgmentLetterSent() As Date?
            Get
                Return _dateAcknowledgmentLetterSent
            End Get
            Set(ByVal value As Date?)
                _dateAcknowledgmentLetterSent = value
            End Set
        End Property
        Private _dateAcknowledgmentLetterSent As Date?

        Public MustOverride ReadOnly Property EventType() As String

        Public MustOverride ReadOnly Property EventTypeCode() As String

        Public Property StaffResponsible() As Staff
            Get
                Return _staffResponsible
            End Get
            Set(ByVal value As Staff)
                _staffResponsible = value
            End Set
        End Property
        Private _staffResponsible As Staff

        Public Property Deleted() As Boolean
            Get
                Return _deleted
            End Get
            Set(ByVal value As Boolean)
                _deleted = value
            End Set
        End Property
        Private _deleted As Boolean

        Public Property Comments() As String
            Get
                Return _comments
            End Get
            Set(ByVal value As String)
                _comments = value
            End Set
        End Property
        Private _comments As String
    End Class

End Namespace