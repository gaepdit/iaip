Imports System.Collections.Generic

Namespace Apb.Sscp

    Public Class Inspection
        Inherits WorkItem

        Public Sub New()
            Me.EventType = WorkItemEventType.Inspection
        End Sub

        ' Data from SSCPINSPECTIONS table
        Public Property InspectionStart As DateTime
        Public Property InspectionEnd As DateTime
        Public Property Reason As InspectionReason
        Public Property ReasonString As String
            Get
                Return InspectionReasonStrings(Reason)
            End Get
            Set(value As String)
                Dim result As InspectionReason
                If InspectionReason.TryParse(value, result) Then
                    Reason = result
                Else
                    Reason = InspectionReason.None
                End If
            End Set
        End Property
        Public Property Weather As String
        Public Property Guide As String
        Public Property FacilityOperating As Boolean
        Public Property ComplianceStatus As InspectionComplianceStatus
        Public Property ComplianceStatusString As String
            Get
                Return InspectionComplianceStatusStrings(ComplianceStatus)
            End Get
            Set(value As String)
                Dim result As InspectionComplianceStatus
                If InspectionComplianceStatus.TryParse(value, result) Then
                    ComplianceStatus = result
                Else
                    ComplianceStatus = InspectionComplianceStatus.Compliant
                End If

            End Set
        End Property
        Public Property FollowUpActionTaken As Boolean

#Region " Enums and Enum Dictionaries "
        Public Enum InspectionReason
            None
            ComplaintInvestigation
            PlannedAnnounced
            PlannedUnannounced
            JointEpdEpa
            Unplanned
            Multimedia
        End Enum

        Private ReadOnly InspectionReasonStrings As New Dictionary(Of InspectionReason, String) From {
            {InspectionReason.None, "N/A"},
            {InspectionReason.ComplaintInvestigation, "Complaint Investigation"},
            {InspectionReason.PlannedAnnounced, "Planned Announced"},
            {InspectionReason.PlannedUnannounced, "Planned Unannounced"},
            {InspectionReason.JointEpdEpa, "Joint EPD/EPA"},
            {InspectionReason.Unplanned, "Unplanned"},
            {InspectionReason.Multimedia, "Multimedia"}
        }

        Public Enum InspectionComplianceStatus
            Compliant
            DeviationsNoted
        End Enum

        Private ReadOnly InspectionComplianceStatusStrings As New Dictionary(Of InspectionComplianceStatus, String) From {
            {InspectionComplianceStatus.Compliant, "Compliant"},
            {InspectionComplianceStatus.DeviationsNoted, "Deviation(s) Noted"}
        }

#End Region

    End Class

End Namespace