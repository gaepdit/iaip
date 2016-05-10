Imports System.Collections.Generic
Imports EpdIt

Namespace Apb.Sscp

    Public Class WorkItem

#Region " Properties "

        ' Data from SSCPITEMMASTER table
        Public Property SscpTrackingNumber() As String
        Public Property Facility() As Apb.Facilities.Facility
        Public Property DateReceived() As Date
        Public Property StaffResponsible() As IaipUser
        Public Property DateComplete() As Date?
        Public Property Deleted() As Boolean
        Public Property DeletedDbCode As String
            Get
                If Deleted Then
                    Return "True"
                Else
                    Return ""
                End If
            End Get
            Set(value As String)
                If value = "True" Then
                    Deleted = True
                Else
                    Deleted = False
                End If
            End Set
        End Property
        Public Property DateAcknowledgmentLetterSent() As Date?
        Public Property EventType() As WorkItemEventType
        Public Property EventTypeDbCode() As String
            Get
                Return EventTypeDbCodes(EventType)
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

        Public Shared EventTypeDbCodes As New Dictionary(Of WorkItemEventType, String) From {
            {WorkItemEventType.Report, "01"},
            {WorkItemEventType.Inspection, "02"},
            {WorkItemEventType.StackTest, "03"},
            {WorkItemEventType.TvAcc, "04"},
            {WorkItemEventType.Notification, "05"},
            {WorkItemEventType.RmpInspection, "07"},
            {WorkItemEventType.Unknown, "00"}
        }

#End Region

#Region " Read-only Display Properties"
        ' Mostly for use by Crystal Reports

        Public ReadOnly Property DisplayFacilityName() As String
            Get
                Return Me.Facility.FacilityName
            End Get
        End Property
        Public ReadOnly Property DisplayAirsNumber() As String
            Get
                Return Me.Facility.AirsNumber.FormattedString
            End Get
        End Property
        Public ReadOnly Property DisplayStaffName() As String
            Get
                Return Me.StaffResponsible.FullName
            End Get
        End Property
        Public ReadOnly Property DisplayFacilityCity() As String
            Get
                Return Me.Facility.FacilityLocation.Address.City
            End Get
        End Property

#End Region

    End Class

End Namespace