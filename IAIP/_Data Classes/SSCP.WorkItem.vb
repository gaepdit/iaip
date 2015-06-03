Imports Iaip.Apb.Facilities
Namespace Apb.SSCP

    Public MustInherit Class WorkItem

#Region " Properties "

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

        Public Overridable Property EventType() As WorkItemEventType
            Get
                Return _eventType
            End Get
            Set(ByVal value As WorkItemEventType)
                _eventType = value
            End Set
        End Property
        Private _eventType As WorkItemEventType

        Public Property EventTypeCode() As String
            Get
                Select Case _eventType
                    Case WorkItemEventType.Unknown
                        Return "00"
                    Case WorkItemEventType.Report
                        Return "01"
                    Case WorkItemEventType.Inspection
                        Return "02"
                    Case WorkItemEventType.StackTest
                        Return "03"
                    Case WorkItemEventType.TvAcc
                        Return "04"
                    Case WorkItemEventType.Notification
                        Return "05"
                    Case WorkItemEventType.RmpInspection
                        Return "07"
                    Case Else
                        Return "00"
                End Select
            End Get
            Set(ByVal value As String)
                Select Case value
                    Case "00"
                        _eventType = WorkItemEventType.Unknown
                    Case "01"
                        _eventType = WorkItemEventType.Report
                    Case "02"
                        _eventType = WorkItemEventType.Inspection
                    Case "03"
                        _eventType = WorkItemEventType.StackTest
                    Case "04"
                        _eventType = WorkItemEventType.TvAcc
                    Case "05"
                        _eventType = WorkItemEventType.Notification
                    Case "06"
                        _eventType = WorkItemEventType.Unknown
                    Case "07"
                        _eventType = WorkItemEventType.RmpInspection
                    Case Else
                        _eventType = WorkItemEventType.Unknown
                End Select
            End Set
        End Property

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

#End Region

#Region " Enums "

        Public Enum WorkItemEventType
            Unknown
            Report
            Inspection
            StackTest
            Notification
            TvAcc
            RmpInspection
        End Enum

#End Region

    End Class

End Namespace