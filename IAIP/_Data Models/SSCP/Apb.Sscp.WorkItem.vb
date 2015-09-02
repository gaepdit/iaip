Namespace Apb.Sscp

    Public MustInherit Class WorkItem

        ' Data from SSCPITEMMASTER table
        Public Property SscpTrackingNumber() As String
        Public Property Facility() As Apb.Facilities.Facility
        Public Property DateReceived() As Date
        Public Property StaffResponsible() As Staff
        Public Property DateComplete() As Date?
        Public Property Deleted() As Boolean
        Public Property DeletedDbCode As String
            Get
                Return DB.DumbConvertFromBoolean(Me.Deleted, DB.DumbConvertBooleanType.TrueOrDBNull)
            End Get
            Set(value As String)
                Me.Deleted = DB.DumbConvertToBoolean(value, DB.DumbConvertBooleanType.TrueOrDBNull)
            End Set
        End Property
        Public Property DateAcknowledgmentLetterSent() As Date?
        Public Overridable Property EventType() As WorkItemEventType
        Public Overridable Property EventTypeDbCode() As String
            Get
                Select Case EventType
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
                    Case "01"
                        EventType = WorkItemEventType.Report
                    Case "02"
                        EventType = WorkItemEventType.Inspection
                    Case "03"
                        EventType = WorkItemEventType.StackTest
                    Case "04"
                        EventType = WorkItemEventType.TvAcc
                    Case "05"
                        EventType = WorkItemEventType.Notification
                    Case "06"
                        EventType = WorkItemEventType.Unknown
                    Case "07"
                        EventType = WorkItemEventType.RmpInspection
                    Case Else
                        EventType = WorkItemEventType.Unknown
                End Select
            End Set
        End Property

        ' Common data from event-type-specific tables
        Public Property Comments() As String

        Public Enum WorkItemEventType
            Unknown
            Report
            Inspection
            StackTest
            Notification
            TvAcc
            RmpInspection
        End Enum

    End Class

End Namespace