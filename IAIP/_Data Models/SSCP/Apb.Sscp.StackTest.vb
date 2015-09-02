Namespace Apb.Sscp

    Public Class StackTest
        Inherits WorkItem

        Public Sub New()
            Me.EventType = WorkItemEventType.StackTest
        End Sub

        ' Note:
        ' DateReceived for Stack Tests is date test summary received by SSCP

        ' Data from SSCPTESTREPORTS table
        Public Property IsmpReferenceNumber As Apb.Ismp.ReferenceNumber
        Public Property TestDue As Date?
        Public Property FollowUpActionTaken As Boolean

    End Class

End Namespace