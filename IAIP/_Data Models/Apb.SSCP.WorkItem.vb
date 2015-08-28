﻿Imports Iaip.Apb.Facilities
Namespace Apb.SSCP

    Public MustInherit Class WorkItem

#Region " Properties "

        Public Property TrackingNumber() As String
        Public Property Facility() As Facility
        Public Property DateReceived() As Date
        Public Property DateComplete() As Date?
        Public Property DateAcknowledgmentLetterSent() As Date?
        Public Overridable Property EventType() As WorkItemEventType
        Public Property EventTypeCode() As String
            Get
                Select Case EventType
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
                        EventType = WorkItemEventType.Unknown
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

        Public Property StaffResponsible() As Staff

        Public Property Deleted() As Boolean
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

#End Region

    End Class

End Namespace